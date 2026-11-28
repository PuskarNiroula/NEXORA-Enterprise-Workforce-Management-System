using NEXORA.Domain.Enums;

namespace NEXORA.Application.Dto.Department;

public class DepartmentDto
{
    public Guid DepartmentId { get; set; }
    public string Name { get; set; }
    public DepartmentStatusEnum Status { get; set; }
}