using Microsoft.JSInterop;
using PbtASystem.PbtASupport;

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

		}
	}
}
