namespace PbtASystem.PbtASupport;

public interface iUSDB
{
	public event EventHandler DataReady;
	public event EventHandler<string> DebugSuccessMessage;
	public event EventHandler<string> DebugErrorMessage;
	List<Rumor> Rumors { get; }
	public List<FactionAction> FactionActions { get; }
	Character CurrentCharacter { get; set; }
	Faction CurrentFaction { get; set; }
	Circles CurrentCircle { get; set; }
	Task StoreDebt(Debt debt);
	Task DeleteDebt(Debt debt);
	Task StoreCharacter(Character ch);
	Task StoreFaction(Faction fac);
	Task StoreRumors();
	Task ForceDataRefresh();

	void FixRellationships();

	List<Character> AllCharacters { get; }
	List<Faction> AllFactions { get; }

	List<Debt> AllDebts { get; }

	bool IsDataReady { get; }

	Faction? GetFaction(Guid factionID);
	Faction? GetFaction(Guid? factionID);
	List<Character> GetMembersOfFaction(Faction f);
}

public abstract class USBDbase : iUSDB
{
	public event EventHandler DataReady;
	public event EventHandler<string> DebugSuccessMessage;
	public event EventHandler<string> DebugErrorMessage;
	public event EventHandler<Character> CharacterChanged;

	public List<Rumor> Rumors { get; protected set; } = new();
	public List<FactionAction> FactionActions { get; protected set; } = new();
	private bool _isDataReady = false;
	public bool IsDataReady
	{
		get { return _isDataReady; }
		protected set
		{
			_isDataReady = value;
			if (_isDataReady) DataReady?.Invoke(this, EventArgs.Empty);
			_isDataReady = value;
		}
	}

	protected void ShowSuccess(string message) => DebugSuccessMessage?.Invoke(this, message);
	protected void ShowError(string message) => DebugErrorMessage?.Invoke(this, message);
	protected void ShowResult(bool result, string messageOk, string messageError)
	{
		if (result) DebugSuccessMessage.Invoke(this, messageOk);
		else DebugErrorMessage?.Invoke(this, messageError);
	}

	public event EventHandler ForceUIUpdate;
	private Character? _currentCharacter = new();

	/// <summary>
	/// The Character that is currently selected for inspection. NOT the one MANAGED by the human player.
	/// </summary>
	public Character? CurrentCharacter
	{
		get { return _currentCharacter; }
		set { 
			_currentCharacter = value;
			ForceUIUpdate?.Invoke(this, EventArgs.Empty);
			CharacterChanged?.Invoke(this, _currentCharacter);
		}
	}

	public Faction? CurrentFaction { get; set; } = new();
	public Circles CurrentCircle { get; set; } = Circles.Mortalis;

	public List<Character> AllCharacters { get; protected set; } = new();
	public List<Faction> AllFactions { get; protected set; } = new();
	public List<Debt> AllDebts { get; protected set; } = new();

	public abstract Task LoadAllAsync();
	public abstract Task ForceDataRefresh();
	public abstract void LoadAllFactionsFromServer();
	public abstract void LoadAllCharactersFromServer();
	public abstract void LoadAllDebtsFromServer();
	public void FixRellationships()
	{
		Faction MortalisAutonomous = new Faction
		{
			ID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
			Name = "Autonomos mortales",
			Circle = Circles.Mortalis,
			MasterSeeds = "Mortales que no están asociados a ninguna facción en particular."
		};
		Faction NightAutonomous = new Faction
		{
			ID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"),
			Name = "Autonomos de la Noche",
			Circle = Circles.Noche,
			Description = "Noche que no están asociados a ninguna facción en particular."
		};
		Faction PowerAutonomous = new Faction
		{
			ID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF02"),
			Name = "Autonomos de Poder",
			Circle = Circles.Poder,
			Description = "Poder que no están asociados a ninguna facción en particular."
		};
		Faction VeilAutonomous = new Faction
		{
			ID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF03"),
			Name = "Autonomos de Velo",
			Circle = Circles.Velo,
			Description = "Velo que no están asociados a ninguna facción en particular."
		};

		if (AllFactions.Find(x => x.ID == MortalisAutonomous.ID) == null)
			AllFactions.Add(MortalisAutonomous);
		if (AllFactions.Find(x => x.ID == NightAutonomous.ID) == null)
			AllFactions.Add(NightAutonomous);
		if (AllFactions.Find(x => x.ID == PowerAutonomous.ID) == null)
			AllFactions.Add(PowerAutonomous);
		if (AllFactions.Find(x => x.ID == VeilAutonomous.ID) == null)
			AllFactions.Add(VeilAutonomous);
	}

	public Faction? GetFaction(Guid factionid) => AllFactions.Find(x => x.ID == factionid);
	public Faction? GetFaction(Guid? factionid)
	{
		Guid FID = factionid ?? default(Guid);
		return AllFactions.Find(x => x.ID == FID);
	}

	public List<Character> GetMembersOfFaction(Faction f)
	{
		return AllCharacters.FindAll(x => x.FactionID == f.ID);
	}

	public abstract Task StoreDebt(Debt debt);
	public abstract Task DeleteDebt(Debt debt);
	public abstract Task StoreCharacter(Character debt);
	public abstract Task StoreFaction(Faction debt);
	public abstract Task StoreRumors();
}