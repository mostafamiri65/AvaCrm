using AvaCrm.Application.DTOs.CustomerManagement.ContactPersons;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface IContactPersonService
    {
		Task<GlobalResponse<ContactPersonListDto>> GetById(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<ContactPersonListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ContactPersonListDto>> Create(ContactPersonCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ContactPersonListDto>> Update(ContactPersonUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
	}
}