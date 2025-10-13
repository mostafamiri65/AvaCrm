using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Contracts.Dashboard;
using AvaCrm.Persistence.Data;
using AvaCrm.Persistence.Repositories.Accounts;
using AvaCrm.Persistence.Repositories.Commons;
using AvaCrm.Persistence.Repositories.CustomerManagement;
using AvaCrm.Persistence.Repositories.Dashboard;
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
		services.AddScoped<ICountryRepository,CountryRepository>();
		services.AddScoped<IProvinceRepository, ProvinceRepository>();
		services.AddScoped<ICityRepository,CityRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IIndividualCustomerRepository, IndividualCustomerRepository>();
        services.AddScoped<IOrganizationCustomerRepository, OrganizationCustomerRepository>();
        services.AddScoped<IContactPersonRepository, ContactPersonRepository>();
        services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
        services.AddScoped<IFollowUpRepository, FollowUpRepository>();
        services.AddScoped<IInteractionRepository, InteractionRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<ICustomerTagRepository, CustomerTagRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();
		return services;
	}
}