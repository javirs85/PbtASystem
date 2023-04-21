using Blazored.Toast;
using Blazored.Toast.Services;
using PbtASystem.Components;

namespace PbtASystem.Services;

public static class ToastExtensions
{
	public static void ShowRollToast(this IToastService toaster, string playerName, int d1, int d2, string? MoveName = null, string? bonusName = null, int? bonus = null)
	{
		var value = d1 + d2;
		string Mname = "";
		string details = "";
		if (MoveName is not null)
			Mname = MoveName;

		if(bonus is not null && bonusName is not null)
		{
			value += (int)bonus;
			details = $"{d1} + {d2} + {bonusName.ToLower()}({bonus}) = {value}";
		}			
		else
			details = $"{d1} + {d2}  = {value}";


		ShowRollToast(toaster, playerName, Mname, details, value);
	}

	public static void ShowRollToast(this IToastService toaster, string playerName, string MoveName, string details, int EndResult)
	{
		ToastParameters paras = new();
		paras.Add(nameof(RollToast.Name), playerName);
		paras.Add(nameof(RollToast.MoveName), MoveName);
		paras.Add(nameof(RollToast.Text), details);
		paras.Add(nameof(RollToast.TotalRolledValue), EndResult);

		toaster.ShowToast<RollToast>(paras);
	}

}
