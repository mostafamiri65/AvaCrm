using AvaCrm.Persistence.Data;
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


		return services;
	}
}