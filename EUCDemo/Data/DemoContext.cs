using EUCDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EUCDemo.Data
{
	public class DemoContext : DbContext
	{
		public DemoContext(DbContextOptions<DemoContext> options) : base(options)
		{

		}

		public DbSet<Osoba> Osoba { get; set; }
		public DbSet<Stat> Stat { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Stat>().ToTable("STAT");
			modelBuilder.Entity<Osoba>().ToTable("OSOBA");

			base.OnModelCreating(modelBuilder);
		}
	}
}
