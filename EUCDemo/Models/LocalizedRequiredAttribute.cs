using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EUCDemo.Models
{
	public class LocalizedRequiredAttribute : ValidationAttribute
	{
		public string? ResourceName { get; set; }

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			ICultureHolder? record = validationContext.ObjectInstance as ICultureHolder;
			string msg = Resources.Resources.LocalizedString(record, ResourceName ?? validationContext.MemberName ?? string.Empty);
			if (value == null)
				return new ValidationResult(msg);
			if (string.IsNullOrWhiteSpace(value.ToString()))
				return new ValidationResult(msg);
			return ValidationResult.Success;
		}
	}
}
