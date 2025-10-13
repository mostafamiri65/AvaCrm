
using System.Reflection;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Features.CustomerManagement.Tags;

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
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IIndividualCustomerService, IndividualCustomerService>();
        services.AddScoped<IContactPersonService, ContactPersonService>();
        services.AddScoped<IFollowUpService, FollowUpService>();
        services.AddScoped<IOrganizationCustomerService, OrganizationCustomerService>();
        services.AddScoped<ICustomerAddressService, CustomerAddressService>();
        services.AddScoped<IInteractionService, InteractionService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ICustomerTagService, CustomerTagService>();
    }
}
