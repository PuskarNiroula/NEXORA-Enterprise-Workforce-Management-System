using NEXORA.Application.Dto.Department;
using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces.RepositoryInterfaces;

public interface IDepartmentRepository
{
    public Task<List<Department>> GetAllAsync();
    public Task<bool> Activate(Guid id);
    public Task<bool> Deactivate(Guid id);
    public Task<bool> CreateAsync(DepartmentCreateDto dto);
    public Task<bool> UpdateAsync(DepartmentUpdateDto dto);
    public Task<Department?> GetDepartmentByNameAsync(string name,Guid? id=null);
    
}