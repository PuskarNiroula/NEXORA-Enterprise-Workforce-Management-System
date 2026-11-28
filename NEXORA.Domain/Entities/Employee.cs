using NEXORA.Domain.Enums;

namespace NEXORA.Domain.Entities;

public class Employee
{
    public Guid EmployeeId { get; set; }
    public Guid? UserId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public string WorkEmail { get; set; } = string.Empty;
    public string? PersonalEmail { get; set; }
    public string? Image { get; set; }
    public DateOnly? DOB { get; set; }
    public string? Gender { get; set; }
    public DateOnly JoiningDate { get; set; }
    public EmploymentStatus EmploymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }



    public User? User;
    public Department? Department;
}
