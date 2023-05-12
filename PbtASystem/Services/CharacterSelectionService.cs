using Blazored.Toast.Services;
using PbtASystem.PbtASupport;

namespace PbtASystem.Services;

public class CharacterSelectionService
{
	public event EventHandler StartSelection;
    public event EventHandler UI_ForceUpdate;
    public event EventHandler UI_Show;
	public event EventHandler UI_Hide;
    public bool IsVisible { get; set; }

	public string HeaderMessage { get; set; } = "";
	public bool AllowExit { get; set; } = true;



    private IToastService Toaster;
	private FirebaseData DB;
	private Character DefaultCharacter;
	public CharacterSelectionService(IToastService _toaster, FirebaseData _db)
	{
		Toaster = _toaster;
		DB = _db;
	}

    TaskCompletionSource<Character> tcs;
    
	public void Show()
	{
		IsVisible = true;
		UI_Show?.Invoke(this, EventArgs.Empty);
	}

	public void Hide()
	{
		IsVisible = false;
		UI_Hide.Invoke(this, EventArgs.Empty);
	}

	public async Task<Character> SelectViaSelector(string _headerMessage = "", bool _allowExit = true)
	{
		HeaderMessage = _headerMessage;
		AllowExit = _allowExit;

		var _default = DB.AllCharacters[0];
        try
        {
            Show();
            var selected = await SelectViaSelectorInternal();
            Hide();
            return selected;
        }
        catch (OperationCanceledException)
        {
            return _default;
        }
        catch (Exception ex)
        {
            Toaster.ShowError(ex.Message);
            return _default;
        }
    }

	public async Task<Character> SelectViaSelector(Character _default)
	{
		try
		{
			Show();
			var selected = await SelectViaSelectorInternal();
			Hide();
			return selected;
		}
		catch(OperationCanceledException)
		{
            Hide();
            return _default;
		}
		catch (Exception ex)
		{
			Toaster.ShowError(ex.Message);
            return _default;
        }
	}

    private Task<Character> SelectViaSelectorInternal()
    {
		tcs = new TaskCompletionSource<Character>();
        return tcs.Task;
    }

	public void Selected(Character ch)
	{
		tcs.SetResult(ch);
	}
	public void CancelSelection()
	{
		Hide();
		tcs.SetCanceled();
	}

}
