using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface ICustomerTagRepository 
    {
        Task<List<CustomerTag>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
        Task<List<CustomerTag>> GetByTagId(int tagId, CancellationToken cancellationToken = default);
        Task<CustomerTag?> GetByCustomerAndTag(long customerId, int tagId, CancellationToken cancellationToken = default);
        Task<bool> CreateCustomerTag(CustomerTag customerTag);
        Task<bool> RemoveCustomerTag(CustomerTag customerTag);
    }
}