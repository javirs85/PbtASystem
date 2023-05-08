using Blazored.Toast.Services;
using PbtASystem.PbtASupport;

namespace PbtASystem.Services
{
	public class ConfirmationModalService
	{
		public event EventHandler OpenConfirmationModal;
		public event EventHandler CloseConfirmationModal;
		IToastService Toaster;

		public ConfirmationModalService(IToastService toaster) => Toaster = toaster;

		public string HeaderText = "Estar seguro?";
		public string BodyText = "Confirma que deseas hacerlo";
		public string ButtonText = "Borrar Personaje";
		public string CancelButtonText = "Cancelar";

		TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

		public async Task<bool> AskUserForConfirmation(string _bodyText = "Confirma que deseas hacerlo", string _buttonText="Borrar", string _headerText = "Estas seguro?", string _cancelButtonText = "Cancelar")
		{
			HeaderText = _headerText;
			BodyText = _bodyText;
			ButtonText = _buttonText;
			CancelButtonText = _cancelButtonText;

			try
			{
				Open();
				var selected = await AskUserForConfirmationInternal();
				Close();
				return selected;
			}
			catch (OperationCanceledException)
			{
				Close();
				return false;
			}
			catch (Exception ex)
			{
				Toaster.ShowError(ex.Message);
				return false;
			}
		}

		private void Open()
		{
			OpenConfirmationModal?.Invoke(this, EventArgs.Empty);
		}
		private void Close()
		{
			CloseConfirmationModal?.Invoke(this, EventArgs.Empty);
		}

		private Task<bool> AskUserForConfirmationInternal()
		{
			tcs = new TaskCompletionSource<bool>();
			return tcs.Task;
		}

		public void Confirm()
		{
			tcs.SetResult(true);
		}
		public void Dismiss()
		{
			Close();
			tcs.SetCanceled();
		}

	}
}
