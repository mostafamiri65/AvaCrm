using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface IFollowUpRepository : IGenericRepository<FollowUp>
    {
        Task<List<FollowUp>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
        Task<List<FollowUp>> GetUpcomingFollowUps(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);

	}
}