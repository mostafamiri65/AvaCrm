
namespace AvaCrm.Domain.Entities;

public class BaseEntity
{
	[Key]
	public long Id { get; set; }
	public DateTime CreationDate { get; set; } = DateTime.Now;
	public long CreatedBy { get; set; }
	public DateTime ModifiedDate { get; set; } = DateTime.Now;
	public long ModifiedBy { get; set; }
	public bool IsDelete { get; set; } = false;
}

