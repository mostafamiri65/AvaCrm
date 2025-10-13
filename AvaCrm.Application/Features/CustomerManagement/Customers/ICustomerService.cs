using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.CustomerManagement.Customers;

public interface ICustomerService
{
    Task<GlobalResponse<CustomerDetailDto>> GetCustomerById(long id, CancellationToken cancellationToken = default);
    Task<GlobalResponse<PaginatedResult<CustomerListDto>>> GetAllCustomers(PaginationRequest request,long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<CustomerListDto>> CreateCustomer(CustomerCreateDto createDto, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<CustomerListDto>> UpdateCustomer(CustomerUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> DeleteCustomer(long id, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> IsCodeUnique(string code, long? excludeCustomerId = null, CancellationToken cancellationToken = default);
}
