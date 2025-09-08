
using System.Reflection;

namespace AvaCrm.Application;

public static class ApplicationServicesRegistration
{
	public static void ConfigureApplicationServices(this IServiceCollection services)
	{
		// AutoMapper
		services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
		// MediatR 
		services.AddMediatR(config =>
			config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddScoped<IAuthService,AuthService>();
		services.AddSingleton<IHashingService, HashingService>();
	}
}
