using NEXORA.Application.Dto.Department;

namespace NEXORA.Application.Interfaces.ServiceInterfaces;

public interface IDepartmentService
{
    public Task<List<DepartmentDto>> GetAllDepartmentsAsync();
    public Task<bool> ActivateDepartment(string departmentId);
    public Task<bool> DeactivateDepartment(string departmentId);
    
}