using Microsoft.JSInterop;
using Blazored.Toast.Services;

namespace PbtASystem.Services;

public class FirebaseAuth
{
    private IToastService Toaster;
    CharacterSelectionService CharacterSelectionService;
    FirebaseData DB;
    public FirebaseAuth(IToastService toaster, CharacterSelectionService CharSelector, FirebaseData dB)
    {
        Toaster = toaster;
        CharacterSelectionService = CharSelector;
        DB = dB;
    }

    public event EventHandler<string> UserChanged;
    public bool IsUserConnected = false;
    public bool IsMaster = false;
    private bool _google = false;
    public bool IsConnectedViaGoogle
    {
        get { 
            return _google; 
        }
        set
        {
            _google = value;
        }
    }


	public string ConnectedUserName { get; set; } = "Not Connected";
    private DotNetObjectReference<FirebaseAuth>? dotNetHelper;
    private IJSObjectReference Firebase;

    public bool IsConnectorSet => Firebase != null;

    public async Task StablishJSConnector(IJSObjectReference reference)
    {
        Firebase = reference;

        dotNetHelper = DotNetObjectReference.Create(this);
        await Firebase.InvokeVoidAsync("SetDotNetReferenceAuth", dotNetHelper);
    }

    public async Task TryLogin()
    {
        await Firebase.InvokeVoidAsync("GoogleLogIn");
    }

    public async Task LoginWithoutLogin()
    {
        if(await DB.IsDefaultCharacterIDSet())
        {
            DB.PlayerCharacter = DB.GetCharacterByID(await DB.GetDefaultCharacterID());
        }
        else
        {
            await AllowPlayerToChooseCharacter();
        }

        IsUserConnected = true;
        IsConnectedViaGoogle = false;
	}

    public async Task LoginUserPass(string mail, string pass)
    {
		await Firebase.InvokeVoidAsync("LoginUserPass", mail, pass);
	}
	public async Task CreateUserPass(string mail, string pass)
	{
		await Firebase.InvokeVoidAsync("CreateUserPass", mail, pass);
	}


	public async Task AllowPlayerToChooseCharacter()
    {
        var Selected = await CharacterSelectionService.SelectViaSelector();
        await DB.StoreDefaultCharacter(Selected);
        DB.PlayerCharacter = Selected;
    }

    public async Task TryLogout()
    {
        await Firebase.InvokeVoidAsync("SignOut");
        IsConnectedViaGoogle = false;

	}
    [JSInvokable]
    public void ShowError(string msg) => Toaster.ShowError(msg);

    [JSInvokable]
    public void ShowInfo(string info) => Toaster?.ShowInfo(info);  

	[JSInvokable]
    public void UpdatePlayer(string newName)
    {
        ConnectedUserName = newName;
        IsUserConnected = true;
        if (newName == "")
        {
            ConnectedUserName = "No user connected";
            IsUserConnected = false;
            IsConnectedViaGoogle = false;

			Toaster.ShowInfo("User disconnected");
            IsDoingInit = false;
        }
        else
        {
            Toaster.ShowSuccess($"{newName} is connected");
            IsConnectedViaGoogle = true;
            DB.ConnectedPlaerIDLocalCopyDoNotUse = newName;

			DB.SetDefaultCharacterToCurrentPlayer(ConnectedUserName);
            IsDoingInit = false;
		}

        UserChanged?.Invoke(this, ConnectedUserName);
    }

    public bool IsDoingInit = true;

    public void UpdateMasterID(string chronicleMasterID)
    {
        if (ConnectedUserName == chronicleMasterID)
            IsMaster = true;
        else
            IsMaster = false;
    }


	public void Dispose() => dotNetHelper?.Dispose();
}
