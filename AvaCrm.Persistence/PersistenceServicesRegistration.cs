using AvaCrm.Persistence.Data;
using AvaCrm.Persistence.Repositories.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AvaCrm.Persistence;

public static class PersistenceServicesRegistration
{
	public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<AvaCrmContext>(options =>
		{
			options.UseSqlServer(configuration
				.GetConnectionString("AvaCrmConnectionString"));
		});

		services.AddScoped<IRoleRepository, RoleRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IPermissionRepository, PermissionRepository>();

		return services;
	}
}