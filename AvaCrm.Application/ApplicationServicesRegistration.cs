
using System.Reflection;
using AvaCrm.Application.Features.Account;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Features.CustomerManagement.Tags;
using AvaCrm.Application.Features.Dashboard;
using AvaCrm.Application.Features.ProjectManagement;

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
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService,ProjectService>();
        services.AddScoped<ITaskItemService,TaskItemService>();
        services.AddScoped<IFileStorage, LocalFileStorage>();
        services.AddScoped<IFileValidator, FileValidator>();
        services.AddScoped<IAttachmentService, AttachmentService>();
    }
}
