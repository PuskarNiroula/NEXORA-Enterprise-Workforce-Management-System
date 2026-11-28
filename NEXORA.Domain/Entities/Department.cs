using NEXORA.Domain.Enums;

namespace NEXORA.Domain.Entities;

public class Department
{
    public Guid DepartmentId { get; set; }
    public string Name { get; set; }
    public DepartmentStatusEnum Status { get; set; }
}