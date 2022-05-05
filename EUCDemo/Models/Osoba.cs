using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EUCDemo.Models
{
	public enum Pohlavi {
		Nezvoleno = 0,
		Muz = 1,
		Zena = 2 ,
		Treti = 3
	}
	public class Osoba : ICultureHolder
	{
		public int OsobaId { get; set; }
		[LocalizedRequired(ResourceName = "nameRequired")]
		public string Jmeno { get; set; } = string.Empty;
		[LocalizedRequired(ResourceName = "surnameRequired")]
		public string Prijmeni { get; set; } = string.Empty;
		[RCValidation]
		public string RC { get; set; } = string.Empty;
		public bool MaRc { get; set; } = true;
		[LocalizedRequired(ResourceName = "birthnumberRequired")]
		public DateTime? DatNarozeni { get; set; }
		[Gender]
		public Pohlavi Pohlavi { get; set; } = Pohlavi.Nezvoleno;
		[LocalizedEmail]
		public string Email { get; set; } = string.Empty;
		[GDPR]
		public bool SouhlasSGDPR { get; set; } = false;
		[LocalizedRequired(ResourceName = "countryRequired")]
		public int? StatId { get; set; }

		[NotMapped]
		public CultureInfo? Culture { get; set; }
	}

	public class LocalizedEmailAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			Osoba osoba = (Osoba)validationContext.ObjectInstance;
			if (osoba != null && string.IsNullOrWhiteSpace(osoba.Email))
			{
				string msg = Resources.Resources.LocalizedString(osoba, "emailRequired");
				return new ValidationResult(msg);
			}
			if (osoba != null)
			{
				if (!Regex.IsMatch(osoba.Email,
					@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
					RegexOptions.IgnoreCase))
				{
					string msg = Resources.Resources.LocalizedString(osoba, "emailInvalid");
					return new ValidationResult(msg);
				}
			}

			return ValidationResult.Success;
		}
	}


	public class GenderAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			Osoba osoba = (Osoba)validationContext.ObjectInstance;
			if (osoba != null && osoba.Pohlavi == Pohlavi.Nezvoleno)
			{
				string msg = Resources.Resources.LocalizedString(osoba, "genderRequired");
				return new ValidationResult(msg);
			}

			return ValidationResult.Success;
		}
	}


	public class GDPRAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			Osoba osoba = (Osoba)validationContext.ObjectInstance;
			if (osoba!=null && !osoba.SouhlasSGDPR)
			{
				string msg = Resources.Resources.LocalizedString(osoba, "gdprConsent");
				return new ValidationResult(msg);
			}

			return ValidationResult.Success;
		}
	}
	public class RCValidation : ValidationAttribute
	{
		Regex rc_regex = new Regex(@"^\s*(\d\d)(\d\d)(\d\d)[ /]*(\d\d\d)(\d?)\s*$");

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			Osoba osoba = (Osoba)validationContext.ObjectInstance;
			if (osoba != null && osoba.MaRc)
			{
				if (value == null)
				{
					string msg = Resources.Resources.LocalizedString(osoba, "birthNumberRequired");
					return new ValidationResult(msg);
				}
				if (value is string str)
				{
					if (!rc_regex.IsMatch(str))
					{
						string msg = Resources.Resources.LocalizedString(osoba, "birthNumberInvalid");
						return new ValidationResult(msg);
					}
				}
			}
			return ValidationResult.Success;
		}
	}
}
