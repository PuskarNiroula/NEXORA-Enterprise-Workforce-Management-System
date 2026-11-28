using NEXORA.Application.Dto.Department;

namespace NEXORA.Application.Interfaces.ServiceInterfaces;

public interface IDepartmentService
{
    public Task<List<DepartmentDto>> GetAllDepartmentsAsync();
    public Task<bool> ActivateDepartment(Guid departmentId);
    public Task<bool> DeactivateDepartment(Guid departmentId);
    public Task CreateDepartmentAsync(DepartmentCreateDto dto);
    public Task UpdateDepartmentAsync(DepartmentUpdateDto dto);
    
}