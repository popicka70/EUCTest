using EUCDemo.Models;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace EUCDemo.Resources
{
	public class Resources
	{
		public static IHost? App { get; set; }

		public static string LocalizedString(ICultureHolder? record, string ResourceName)
		{
			string msg = ResourceName ?? String.Empty;
			if (App != null && ResourceName != null && record != null)
			{
				if (record.Culture != null)
					CultureInfo.CurrentUICulture = record.Culture;
				using var scope = App.Services.CreateScope();
				var stringLocalizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<Resources>>();
				if (stringLocalizer != null)
				{
					msg = stringLocalizer.GetString(ResourceName);
				}
			}
			return msg;
		}
	}
}
