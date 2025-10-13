using AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface IIndividualCustomerService
    {
		Task<GlobalResponse<IndividualCustomerListDto>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<IndividualCustomerListDto>> Create(IndividualCustomerCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<IndividualCustomerListDto>> Update(IndividualCustomerUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
	}
}