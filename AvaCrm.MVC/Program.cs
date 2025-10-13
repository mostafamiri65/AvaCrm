using AvaCrm.Application;
using AvaCrm.Application.Models.Identity;
using AvaCrm.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.Configure<JwtSettings>(
	builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped(sp =>
	sp.GetRequiredService<IOptions<JwtSettings>>().Value);


#region Authentication

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
	options.LoginPath = "/Login";
	options.LogoutPath = "/Logout";
	options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

#endregion

#region Encode

builder.Services.AddSingleton<HtmlEncoder>(
	HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
