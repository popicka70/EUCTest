using EUCDemo.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EUCDemo.Pages
{
	public partial class Index
	{
		Osoba? _osoba;
		List<Stat>? _staty;
		EditContext? editContext;
		Dictionary<string, string> _strings = new Dictionary<string, string>();
		protected async override Task OnInitializedAsync()
		{
			if (string.IsNullOrWhiteSpace(culture))
				culture = "cs-CZ";
			CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);
			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);

			var allstrings = stringLocalizer.GetAllStrings(true);
			_strings = allstrings.ToDictionary(s => s.Name, s => s.Value);

			await Inicializace();
			await base.OnInitializedAsync();
		}

		protected async override Task OnParametersSetAsync()
		{
			if (string.IsNullOrWhiteSpace(culture))
				culture = "cs-CZ";
			CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);
			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);

			var allstrings = stringLocalizer.GetAllStrings(true);
			_strings = allstrings.ToDictionary(s => s.Name, s => s.Value);

			if (_osoba != null)
			{
				_osoba.Culture = CultureInfo.CurrentUICulture;
			}

			if (editContext != null)
				editContext.Validate();
			await base.OnParametersSetAsync();
		}

		string GetString(string name)
		{
			if (_strings.TryGetValue(name, out var value))
				return value;
			return name;
		}

		protected override Task OnAfterRenderAsync(bool firstRender)
		{
			return base.OnAfterRenderAsync(firstRender);
		}
		async Task Inicializace()
		{
			this._staty = await dbContext.Stat.ToListAsync();
			this._osoba = new Osoba();
			this.editContext = new EditContext(_osoba);
			this.editContext.OnFieldChanged += EditContext_OnFieldChanged;
		}

		private void EditContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
		{
			try
			{
				if (e.FieldIdentifier.FieldName == "RC" && _osoba != null)
				{
					string rok = _osoba.RC.Substring(0, 2);
					string mesic = _osoba.RC.Substring(2, 2);
					string den = _osoba.RC.Substring(4, 2);

					int imesic = int.Parse(mesic);
					if (imesic > 50)
						imesic -= 50;

					int irok = int.Parse(rok) + 2000;
					if (irok > DateTime.Today.Year)
						irok -= 100;

					DateTime dnar = new DateTime(irok, imesic, int.Parse(den));
					_osoba.DatNarozeni = dnar;
					StateHasChanged();
				}
			}
			catch
			{
				_osoba.DatNarozeni = null;
			}
		}

		Exception? _last_exception = null;

		async Task ZalozitOsobu()
		{
			if (this._osoba != null)
			{
				try
				{
					this.dbContext.Osoba.Add(this._osoba);
					await this.dbContext.SaveChangesAsync();
					navigation.NavigateTo("osoba-zalozena");
				}
				catch (Exception ex)
				{
					this._last_exception = ex;
					StateHasChanged();
				}
			}
			else
			{
				//
			}
		}

		async Task Zrusit()
		{
			navigation.NavigateTo("hic-sunt-leones");
			await Task.CompletedTask;
		}

	}
}
