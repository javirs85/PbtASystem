namespace PbtASystem.PbtASupport
{
	public static class URLSupport
	{
		public static bool IsRelease = false;
		public static string URLAppendix{
			get
			{
				if (IsRelease) return "/UrbanShadows/";
				return "";
			}
		}
	}
}
