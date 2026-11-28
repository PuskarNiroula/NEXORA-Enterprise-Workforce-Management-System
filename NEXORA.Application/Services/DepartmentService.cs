using NEXORA.Application.Dto.Department;
using NEXORA.Application.Exception.Department;
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

    public async Task<bool> ActivateDepartment(Guid departmentId)
    {
        return await repository.Activate(departmentId);
    } 
    public async Task<bool> DeactivateDepartment(Guid departmentId)
    {
        return await repository.Deactivate(departmentId);
    }

    public async Task CreateDepartmentAsync(DepartmentCreateDto dto)
    {
        if(await repository.GetDepartmentByNameAsync(dto.Name)!=null)
            throw new DuplicateDepartmentNameException(dto.Name);
       
        await repository.CreateAsync(dto);
    }
    public async Task UpdateDepartmentAsync(DepartmentUpdateDto dto)
    {
        if(await repository.GetDepartmentByNameAsync(dto.Name,dto.DepartmentId)!=null)
            throw new DuplicateDepartmentNameException(dto.Name);
       
        await repository.UpdateAsync(dto);
    }
    
}