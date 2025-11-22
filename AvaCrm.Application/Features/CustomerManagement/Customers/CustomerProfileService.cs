using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Responses;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly INoteRepository _noteRepository;

        private readonly ICustomerService _customerService;
        // سایر repositoryها

        public CustomerProfileService(
            ICustomerRepository customerRepository,
            IInteractionRepository interactionRepository,
            INoteRepository noteRepository, ICustomerService customerService)
        {
            _customerRepository = customerRepository;
            _interactionRepository = interactionRepository;
            _noteRepository = noteRepository;
            _customerService = customerService;
        }

        public async Task<GlobalResponse<CustomerProfileDto>> GetCustomerProfile(long customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(customerId, cancellationToken);
                if (customer.Data == null)
                {
                    return new GlobalResponse<CustomerProfileDto>()
                    {
                        Message = "مشتری یافت نشد",
                        StatusCode = 404
                    };
                }

                var interactions = await _interactionRepository.GetByCustomerId(customerId, cancellationToken);
                var notes = await _noteRepository.GetByCustomerId(customerId, cancellationToken);
                // محاسبه آمار
                var totalInteractions = interactions.Count;
                var totalNotes = notes.Count;
                var lastInteraction = interactions
                    .OrderByDescending(x => x.CreationDate)
                    .FirstOrDefault();

                var profileDto = new CustomerProfileDto
                {
                    CustomerDetail = customer.Data,
                    TotalInteractions = totalInteractions,
                    TotalNotes = totalNotes,
                    TotalOrders = 0, // بعداً با ماژول سفارشات پر می‌شود
                    TotalOrderValue = 0,
                    OpenTickets = 0,

                    LastInteractionDate = lastInteraction?.CreationDate,
                    LastInteractionType = lastInteraction?.InteractionType.ToString()
                };

                return new GlobalResponse<CustomerProfileDto>()
                {
                    Data = profileDto,
                    Message = "اطلاعات پروفایل با موفقیت دریافت شد",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new GlobalResponse<CustomerProfileDto> { Message = $"خطا در دریافت پروفایل: {ex.Message}", StatusCode = 500 };
            }
        }

        public async Task<GlobalResponse<PaginatedResult<CustomerActivityDto>>> GetCustomerActivities(
            long customerId,
            PaginationRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var activities = new List<CustomerActivityDto>();

                // جمع‌آوری فعالیت‌ها از منابع مختلف
                var interactions = await _interactionRepository
                    .GetAll()
                    .Where(x => x.CustomerId == customerId)
                    .OrderByDescending(x => x.CreationDate)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var notes = await _noteRepository
                    .GetAll()
                    .Where(x => x.CustomerId == customerId)
                    .OrderByDescending(x => x.CreationDate)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                // تبدیل interactions به activities
                foreach (var interaction in interactions)
                {
                    activities.Add(new CustomerActivityDto
                    {
                        Id = interaction.Id,
                        ActivityType = "Interaction",
                        Title = $"تعامل {interaction.InteractionType}",
                        Description = interaction.Subject,
                        CreatedDate = interaction.CreationDate,
                        CreatedBy = interaction.CreatedBy.ToString(),
                        Icon = GetInteractionIcon(interaction.InteractionType),
                        Color = GetInteractionColor(interaction.InteractionType)
                    });
                }

                // تبدیل notes به activities
                foreach (var note in notes)
                {
                    activities.Add(new CustomerActivityDto
                    {
                        Id = note.Id,
                        ActivityType = "Note",
                        Title = "یادداشت جدید",
                        Description = note.Content.Length > 100
                            ? note.Content.Substring(0, 100) + "..."
                            : note.Content,
                        CreatedDate = note.CreationDate,
                        CreatedBy = note.CreatedBy.ToString(),
                        Icon = "fas fa-sticky-note",
                        Color = "warning"
                    });
                }

                // مرتب‌سازی بر اساس تاریخ
                activities = activities.OrderByDescending(x => x.CreatedDate).ToList();

                var totalCount = (await _interactionRepository.GetByCustomerId(customerId)).Count
                    + (await _noteRepository.GetByCustomerId(customerId)).Count;

                var result = new PaginatedResult<CustomerActivityDto>
                {
                    Items = activities,
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };

                return new GlobalResponse<PaginatedResult<CustomerActivityDto>>()
                {
                    Data = result,
                    StatusCode = 200,
                    Message = "فعالیت‌ها با موفقیت دریافت شد"
                };
            }
            catch (Exception ex)
            {
                return new GlobalResponse<PaginatedResult<CustomerActivityDto>>()
                {
                    Message = $"خطا در دریافت فعالیت‌ها: {ex.Message}",
                    StatusCode = 500,
                };
            }
        }

        public async Task<GlobalResponse<CustomerStatsDto>> GetCustomerStats(long customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                var customerInteractions = await _interactionRepository.GetByCustomerId(customerId);
                var totalInteractions = customerInteractions.Count;

                var thisMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var interactionsThisMonth = (customerInteractions.Where(x =>
                    x.CustomerId == customerId && x.CreationDate >= thisMonthStart)).Count();

                var lastMonthStart = thisMonthStart.AddMonths(-1);
                var lastMonthEnd = thisMonthStart.AddDays(-1);
                var interactionsLastMonth =  (customerInteractions
                    .Where(x=> x.CreationDate >= lastMonthStart && x.CreationDate <= lastMonthEnd)).Count();

                var interactionGrowth = interactionsLastMonth > 0
                    ? (int)((interactionsThisMonth - interactionsLastMonth) / (double)interactionsLastMonth * 100)
                    : interactionsThisMonth > 0 ? 100 : 0;

                var stats = new CustomerStatsDto
                {
                    TotalInteractions = totalInteractions,
                    InteractionsThisMonth = interactionsThisMonth,
                    TotalOrders = 0,
                    OrdersThisMonth = 0,
                    TotalValue = 0,
                    ValueThisMonth = 0,
                    SatisfactionRate = 85.5, // مقدار نمونه
                    AverageResponseTime = 2.5, // مقدار نمونه
                    InteractionGrowth = interactionGrowth,
                    OrderGrowth = 0,
                    ValueGrowth = 0
                };

                return new GlobalResponse<CustomerStatsDto>()
                {
                    Data = stats,
                    Message =  "آمار با موفقیت دریافت شد",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new GlobalResponse<CustomerStatsDto>()
                {
                    Message = $"خطا در دریافت آمار: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        // متدهای کمکی
        private string GetInteractionIcon(Domain.Enums.CustomerManagement.InteractionType interactionType)
        {
            return interactionType switch
            {
                Domain.Enums.CustomerManagement.InteractionType.Call => "fas fa-phone",
                Domain.Enums.CustomerManagement.InteractionType.Email => "fas fa-envelope",
                Domain.Enums.CustomerManagement.InteractionType.Meeting => "fas fa-handshake",
                Domain.Enums.CustomerManagement.InteractionType.Sms => "fas fa-sms",
                Domain.Enums.CustomerManagement.InteractionType.Online => "fas fa-globe",
                _ => "fas fa-question"
            };
        }

        private string GetInteractionColor(Domain.Enums.CustomerManagement.InteractionType interactionType)
        {
            return interactionType switch
            {
                Domain.Enums.CustomerManagement.InteractionType.Call => "success",
                Domain.Enums.CustomerManagement.InteractionType.Email => "primary",
                Domain.Enums.CustomerManagement.InteractionType.Meeting => "info",
                Domain.Enums.CustomerManagement.InteractionType.Sms => "warning",
                Domain.Enums.CustomerManagement.InteractionType.Online => "secondary",
                _ => "dark"
            };
        }
    }
}