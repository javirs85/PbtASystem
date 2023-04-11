using Microsoft.JSInterop;

namespace PbtASystem.Services
{
	public static class Connectors
	{
		private static IJSObjectReference FirebaseJSRef;

		public static async Task SetConnectors(IJSRuntime JS, FirebaseData? Data = null, FirebaseAuth? Auth = null)
		{
			if (Auth != null && !Auth.IsConnectorSet)
			{
				if (FirebaseJSRef == null)
					FirebaseJSRef = await JS.InvokeAsync<IJSObjectReference>("import", "./JS/Firebase.js");

				await Auth.StablishJSConnector(FirebaseJSRef);
			}

			if(Data != null && !Data.IsConnectorSet) {
				if (FirebaseJSRef == null)
					FirebaseJSRef = await JS.InvokeAsync<IJSObjectReference>("import", "./JS/Firebase.js");
				await Data.StablishJSConnector(FirebaseJSRef);
			}

			if (Data != null && Data.AllCharacters.Count == 0)
				await Data.LoadAllAsync();

			if (Data != null && Data.Chronicle.Name == "")
				await Data.GetChronicle(new Guid("6210b905-e706-477a-bfc9-8d6528fe6e19"));
		}
	}
}
