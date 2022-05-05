using System.Globalization;

namespace EUCDemo.Models
{
	public interface ICultureHolder
	{
		CultureInfo? Culture { get; set; }
	}
}
