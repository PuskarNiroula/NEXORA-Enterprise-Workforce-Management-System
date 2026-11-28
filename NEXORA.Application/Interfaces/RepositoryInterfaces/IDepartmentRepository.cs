using NEXORA.Application.Dto.Department;
using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces.RepositoryInterfaces;

public interface IDepartmentRepository
{
    public Task<List<Department>> GetAllAsync();
    public Task<bool> Activate(string id);
    public Task<bool> Deactivate(string id);
    public Task<bool> Create(DepartmentCreateDto dto);
    public Task<bool> Update(DepartmentUpdateDto dto);
    
}