using System;
using System.Collections.Generic;
using System.Text;

namespace AvaCrm.Domain.Entities.ProjectManagement;

public class UserProject
{
    [Key,Column(Order = 0)]
    public long  UserId { get; set; }

    [Key,Column(Order = 1)]
    public long ProjectId { get; set; }


    [ForeignKey(nameof(ProjectId))]
    public virtual Project Project { get; set; } = null!;
}
