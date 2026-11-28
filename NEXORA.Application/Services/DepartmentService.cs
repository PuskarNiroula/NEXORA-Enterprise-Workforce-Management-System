using NEXORA.Application.Dto.Department;
using NEXORA.Application.Interfaces.RepositoryInterfaces;
using NEXORA.Application.Interfaces.ServiceInterfaces;

namespace NEXORA.Application.Services;

public class DepartmentService(IDepartmentRepository repository):IDepartmentService
{
    public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
    {
        var deps =await repository.GetAllAsync();

        return deps.Select(s => new DepartmentDto
        {
            DepartmentId = s.DepartmentId,
            Name = s.Name,
            Status = s.Status
        }).ToList();
    }

    public async Task<bool> ActivateDepartment(string departmentId)
    {
        return await repository.Activate(departmentId);
    } 
    public async Task<bool> DeactivateDepartment(string departmentId)
    {
        return await repository.Deactivate(departmentId);
    }
}