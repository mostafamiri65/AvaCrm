using AvaCrm.Application.Models.Identity;
using AvaCrm.Persistence;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.Configure<JwtSettings>(
	builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped(sp =>
	sp.GetRequiredService<IOptions<JwtSettings>>().Value);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
