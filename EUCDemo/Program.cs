using EUCDemo.Data;
using EUCDemo.Models;
using EUCDemo.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalization();

builder.Services.AddDbContext<DemoContext>(options =>
				options.UseSqlServer(config.GetConnectionString("DBConnection")));

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseRequestLocalization(new RequestLocalizationOptions()
	.AddSupportedCultures(new[] { "en", "cs-CZ" })
	.AddSupportedUICultures(new[] { "en", "cs-CZ" }));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await DBInit.InitializeDB(app);

var cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
string cci = CultureInfo.CurrentCulture.Name;

Resources.App = app;

app.Run();
