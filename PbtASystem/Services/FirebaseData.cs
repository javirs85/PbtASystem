using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PbtASystem.PbtASupport;
using Blazored.LocalStorage;
using System;
using PbtASystem.Components;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PbtASystem.Services;

public class FirebaseData : USBDbase
{
	private Character _playerCharacter = new Character { Name = "Not selected" };

	/// <summary>
	/// The player that the connected user uses as his own character in the selected campaign
	/// </summary>
	public Character PlayerCharacter { 
		get
		{
			return _playerCharacter;
		}
		set
		{
			_playerCharacter = value;
			CurrentCharacter = _playerCharacter;
        }
    }
    public CharacterSheet CurrentPlayerSheet = new CharacterSheet {ID = new Guid()};
	public bool IsCharacterSheetReady => PlayerCharacter is not null &&
									     PlayerCharacter.SheetID.IsNotZero() &&
									     CurrentPlayerSheet != null && 
										 CurrentPlayerSheet.ID.IsNotZero()	&&
										 CurrentPlayerSheet.ID == PlayerCharacter.SheetID;

	public Chronicle Chronicle { get; set; } = new Chronicle();

	public event EventHandler DataIsReady;

	private IJSObjectReference Firebase;
	private DotNetObjectReference<FirebaseData>? dotNetHelper;
	private ILocalStorageService LocalStorage;
	private IToastService Toaster;

	public string ConnectedPlaerIDLocalCopyDoNotUse = "";

	public FirebaseData(IToastService toaster, ILocalStorageService _localStorage)
	{
		Toaster = toaster;
		LocalStorage = _localStorage;
	}
	public event EventHandler ShowLoadingCover;
	public event EventHandler HideLoadingCover;

	public bool IsConnectorSet => Firebase != null;

	public int LocalDataAgeInDays { get; private set; }

	public async Task StablishJSConnector(IJSObjectReference reference)
	{
		Firebase = reference;

		dotNetHelper = DotNetObjectReference.Create(this);
		await Firebase.InvokeVoidAsync("SetDotNetReferenceData", dotNetHelper);
	}

	public void Dispose() => dotNetHelper?.Dispose();

	private class RumorsPack
	{
		public List<Rumor> AllRumors { get; set; } = new();
	}

	private List<Image64> AllImages = new();

	public override async Task LoadAllAsync()
	{
		ShowLoadingCover.Invoke(this, EventArgs.Empty);

		try
		{
			AllCharacters = await LocalStorage.GetItemAsync<List<Character>>("AllCharacters");
			AllFactions = await LocalStorage.GetItemAsync<List<Faction>>("AllFactions");
			AllDebts = await LocalStorage.GetItemAsync<List<Debt>>("AllDebts");
			await LocalStorage.RemoveItemAsync("AllImages");
			AllImages = await LocalStorage.GetItemAsync<List<Image64>>("AllImages");
		}catch (Exception ex)
		{
			await LocalStorage.RemoveItemAsync("AllCharacters");
		}

		if (AllImages == null)
		{
			AllImages = new List<Image64>();
		}

		var pack = await LocalStorage.GetItemAsync<RumorsPack>("AllRumors");
		if (pack is not null)
		{
			foreach (var r in pack.AllRumors)
				Rumors.Add(r);
		}

		if (AllCharacters is null || AllCharacters.Count == 0)
		{
			Toaster.ShowInfo("No data in the local memory");
			await ForceDataRefresh();
		}
		else
		{
			var LastUpdateTime = await LocalStorage.GetItemAsync<DateTime>("LastUpdateTime");
			LocalDataAgeInDays = (DateTime.Now - LastUpdateTime).Days;
			int numChars = AllCharacters?.Count ?? 0;
			int numFactions = AllFactions?.Count ?? 0;
			int numDebts = AllDebts?.Count ?? 0;
			int numRumors = Rumors?.Count ?? 0;

			// Toaster.ShowSuccess($"{numChars} characters loaded from local memory. \n {numFactions} factions \n {numDebts} Debts \n {numRumors} rumors \n {LocalDataAge.Days} days old");

			if (await IsDefaultCharacterIDSet())
			{
				PlayerCharacter = GetCharacterByID(await GetDefaultCharacterID());
				if(PlayerCharacter.SheetID.IsNotZero() && PlayerCharacter.SheetID.ToString() != new Guid().ToString())
				{
					if(await CheckIfSheetExists((Guid)PlayerCharacter.SheetID))
						CurrentPlayerSheet = await GetCharacterSheetByID((Guid)PlayerCharacter.SheetID);
				}
			}

			IsDataReady = true;
			DataIsReady?.Invoke(this, EventArgs.Empty);

			HideLoadingCover.Invoke(this, EventArgs.Empty);
		}
	}
	public async Task<bool> CheckIfNeedToLoad()
	{
		if(IsDataReady) 
			return false;
		else
		{
			await LoadAllAsync();
			return true;
		}
	}
	public override async Task ForceDataRefresh()
	{
		ShowLoadingCover.Invoke(this, EventArgs.Empty);
		AllCharacters = await Firebase.InvokeAsync<List<Character>>("GetCharactersFromDB");
		AllFactions = await Firebase.InvokeAsync<List<Faction>>("GetFactionsFromDB");
		AllDebts = await Firebase.InvokeAsync<List<Debt>>("GetDebtsFromDB");
		AllImages = await Firebase.InvokeAsync<List<Image64>>("GetImagesFromDB");

		RumorsPack pack = await Firebase.InvokeAsync<RumorsPack>("GetRumorsFromDB");
		foreach (var r in pack.AllRumors)
			Rumors.Add(r);

		LocalStorage?.SetItemAsync("AllCharacters", AllCharacters);
		LocalStorage?.SetItemAsync("AllFactions", AllFactions);
		LocalStorage?.SetItemAsync("AllDebts", AllDebts);
		LocalStorage?.SetItemAsync("AllImages", AllImages);
		LocalStorage?.SetItemAsync("AllRumors", pack);
		LocalStorage?.SetItemAsync("LastUpdateTime", DateTime.Now);

		IsDataReady = true;

		if (AllCharacters is not null && AllCharacters.Count > 0) { Toaster.ShowSuccess($"{AllCharacters.Count} characters loaded"); };

		DataIsReady?.Invoke(this, EventArgs.Empty);
		CurrentCharacter = CurrentCharacter;

		HideLoadingCover.Invoke(this, EventArgs.Empty);	
	}


	public Character GetPayingCharacterFromDebt(Debt debt)
	{
		var c = AllCharacters.Find(x => x.ID == debt.PayingID);
		if (c is not null) return c;
		else return new Character();
	}

	internal async Task StoreDefaultCharacter(Character selected)
	{
		await LocalStorage.SetItemAsync<Guid>("DefaultCharacter", selected.ID);
	}

	internal async Task<Guid> GetDefaultCharacterID() => await LocalStorage.GetItemAsync<Guid>("DefaultCharacter");
	internal async Task<bool> IsDefaultCharacterIDSet() => await LocalStorage.ContainKeyAsync("DefaultCharacter");
	internal async Task ClearDefaultCharacter()
	{
		await LocalStorage.RemoveItemAsync("DefaultCharacter");
		PlayerCharacter = new Character { Name = "Not set" };
	}

	internal Character GetCharacterByID(Guid id) => AllCharacters?.Find(x=>x.ID == id)??new Character { Name = "Character not found"};

	#region not implemented
	public override void LoadAllFactionsFromServer()
	{
		throw new NotImplementedException();
	}
	public override void LoadAllCharactersFromServer()
	{
		throw new NotImplementedException();
	}
	public override void LoadAllDebtsFromServer()
	{
		throw new NotImplementedException();
	}
	#endregion

	public async Task<CharacterSheet> GetCharacterSheetByID(Guid id) => await Firebase.InvokeAsync<CharacterSheet>("GetCharacterSheetByID", id);
	public async Task StoreCharacterSheet() => await StoreCharacterSheet(CurrentPlayerSheet);
	public async Task StoreCharacterSheet(CharacterSheet sheet) => await Firebase.InvokeVoidAsync("StoreCharacterSheet", sheet);
	public async Task<bool> CheckIfSheetExists(Guid id) => await Firebase.InvokeAsync<bool>("CheckIfSheetExists", id);



	public async Task StoreChronicle() => await Firebase.InvokeVoidAsync("StoreChronicle", Chronicle);
	public async Task StoreChronicle(Chronicle ch) => await Firebase.InvokeVoidAsync("StoreChronicle", ch);
	public async Task GetChronicle(Guid ID) => Chronicle = await Firebase.InvokeAsync<Chronicle>("GetChronicle", ID);

	internal void SetDefaultCharacterToCurrentPlayer(string connectedPlayerCode)
	{
		var reference = Chronicle.PlayerLinks.Find(x=> x.PlayerID == connectedPlayerCode);
		if(reference != null) {
			PlayerCharacter = AllCharacters.Find(x => x.ID == reference.CharacterID) ?? new Character() { Name = "Not set yet" };
		}

		CurrentCircle = PlayerCharacter.Circle;
		CurrentCharacter = PlayerCharacter;
	}
	

	public async Task StoreMapPlayerCharacter(string PlayerID, Guid CharacterID)
	{
		var map = Chronicle.PlayerLinks.Find(x => x.PlayerID == PlayerID);
		if(map != null)
			map.CharacterID = CharacterID;
		else 
			Chronicle.PlayerLinks.Add(new MapPlayerCharacter { PlayerID = PlayerID, CharacterID = CharacterID });

		await StoreChronicle();
	}

	public override async Task StoreDebt(Debt debt)
	{
		await Firebase.InvokeVoidAsync("StoreDebt", debt);
		if(!AllDebts.Contains(debt))
		{
			AllDebts.Add(debt);
		}     
		await LocalStorage.SetItemAsync<List<Debt>>("AllDebts", AllDebts);
		await ForceDataRefresh();
	}

	public override async Task DeleteDebt(Debt debt)
	{
		await Firebase.InvokeVoidAsync("DeleteDebt", debt);
		AllDebts.Remove(debt);
		await LocalStorage.SetItemAsync<List<Debt>>("AllDebts", AllDebts);
	}

	public async Task<Faction> CreateNewFactionAtSelectedCircle()
	{
		Faction newFaction = new Faction
		{
			Circle = CurrentCircle,
			Name = "New Faction",
			ID = Guid.NewGuid(),
		};
		AllFactions.Add(newFaction);
		CurrentFaction = newFaction;
		await StoreFaction(newFaction);
		Chronicle.FactionIDs.Add(newFaction.ID);
		await StoreChronicle();
		await ForceDataRefresh();

		return newFaction;
	}

	public async Task<Character> CreatePlayerAtFaction(Faction fac)
	{
		CurrentFaction = fac;
		Character ch = new Character
		{
			Circle = CurrentCircle,
			FactionID = CurrentFaction.ID,
			ID = Guid.NewGuid(),
			Name = "Nombre"
		};
		AllCharacters.Add(ch);
		CurrentCircle = ch.Circle;
		CurrentCharacter = ch;
		await StoreCharacter(ch);
		Chronicle.CharacterIDs.Add(ch.ID);
		await StoreChronicle();
		await ForceDataRefresh();

		return ch;
	}

	public async override Task StoreCharacter(Character ch)
	{
		await Firebase.InvokeVoidAsync("StoreCharacter", ch);
		if (!AllCharacters.Contains(ch))
		{
			AllCharacters.Add(ch);
		}
		if(!Chronicle.CharacterIDs.Contains(ch.ID))
		{
			Chronicle.CharacterIDs.Add(ch.ID);
			await StoreChronicle();
		}
		await LocalStorage.SetItemAsync<List<Character>>("AllCharacters", AllCharacters);
	}

	public async Task<Character> DeleteCharacter(Character ch)
	{
		await Firebase.InvokeVoidAsync("DeleteCharacter", ch);
		AllCharacters.Remove(ch);
		await LocalStorage.SetItemAsync<List<Character>>("AllCharacters", AllCharacters);

		Chronicle.CharacterIDs.Remove(ch.ID);
		await StoreChronicle();
		await ForceDataRefresh();

		var friend = AllCharacters.Find(x => x.FactionID == ch.FactionID);
		if (friend == null)
		{
			friend = AllCharacters.Find(x => x.Circle == ch.Circle);
			if (friend is null)
			{
				friend = AllCharacters[0];
			}
		}

		CurrentCircle = friend.Circle;
		CurrentFaction = GetFaction(friend.FactionID);
		CurrentCharacter = friend;

		return friend;
	}

	private class Image64
	{
		public string Data { get; set; } = "";
		public Guid ID { get; set; } = new();
	}

	public async Task StoreImageBase64(string base64, Guid ID)
	{
		var Data = new Image64 { Data = base64, ID = ID };
		await Firebase.InvokeVoidAsync("StoreImageBase64", Data) ;
	}

	private Guid DefaultImageGuid = new Guid("6e60e392-fa8d-471f-a58f-32a58d50307c");
	public async Task<string> getImageBase64Async()
	{
		var pack = AllImages.Find(x => x.ID == CurrentCharacter.ID);
		if (pack == null)
		{
			pack = await Firebase.InvokeAsync<Image64>("getImageBase64", CurrentCharacter.ID.ToString(), DefaultImageGuid.ToString());
			pack.ID = CurrentCharacter.ID;
			AllImages.Add(pack);
		}

		return pack.Data;
	}

	public override async Task StoreFaction(Faction Faction)
	{
		await Firebase.InvokeVoidAsync("StoreFaction", Faction);
	}

	public override Task StoreRumors()
	{
		throw new NotImplementedException();
	}
}
