using AvaCrm.Application.DTOs.CustomerManagement.CustomerAddresses;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface ICustomerAddressService
    {
		Task<GlobalResponse<CustomerAddressListDto>> GetById(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<CustomerAddressListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<CustomerAddressListDto>> Create(CustomerAddressCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<CustomerAddressListDto>> Update(CustomerAddressUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
	}
}