using AvaCrm.Application.Models.Identity;
using AvaCrm.Persistence;
using Microsoft.Extensions.Options;
using AvaCrm.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.Configure<JwtSettings>(
	builder.Configuration.GetSection("Jwt"));
#region JWT
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});
#endregion
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(sp =>
	sp.GetRequiredService<IOptions<JwtSettings>>().Value);
builder.Services.ConfigureApplicationServices();

#region Cors
// Configure CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularApp",
		policy =>
		{
			policy.WithOrigins("http://localhost:4200") // Your Angular app URL
				  .AllowAnyHeader()
				  .AllowAnyMethod()
				  .AllowCredentials(); // If you're using cookies/auth
		});
});
#endregion

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.MapOpenApi();
}
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
