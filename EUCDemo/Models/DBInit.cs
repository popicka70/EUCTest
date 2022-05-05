using EUCDemo.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace EUCDemo.Models
{
	public class DBInit
	{
		public async static Task InitializeDB(IHost host)
		{
			using IServiceScope scope = host.Services.CreateScope();
			using DemoContext dbContext = scope.ServiceProvider.GetRequiredService<DemoContext>();
			await InitializeDB(dbContext);
		}
		public async static Task InitializeDB(DemoContext dbContext)
		{
			await dbContext.Database.EnsureCreatedAsync();


			var countries = JsonSerializer.Deserialize<Country[]>(File.ReadAllText("Models/countries.json"));
			if (countries != null)
			{
				var staty = await dbContext.Stat.ToListAsync();
				var missing = countries.Where(c => !staty.Exists(s => s.Kod == c.CountryCode)).ToList();
				if (missing.Any())
				{
					foreach (var country in missing)
					{
						if (country.CountryCode!=null && country.Name!=null)
						{
							await dbContext.Stat.AddAsync(new Stat() { Kod = country.CountryCode, Nazev = country.Name });
						}
					}
					await dbContext.SaveChangesAsync();
				}
			}

			await Task.CompletedTask;
		}
	}

	class Country
	{
		[JsonPropertyName("name")]
		public string? Name { get; set; }
		[JsonPropertyName("code")]
		public string? CountryCode { get; set; }
	}
}
