using Microsoft.EntityFrameworkCore;
using NEXORA.Application.Dto.Department;
using NEXORA.Application.Exception.Department;
using NEXORA.Application.Interfaces.RepositoryInterfaces;
using NEXORA.Domain.Entities;
using NEXORA.Domain.Enums;
using NEXORA.Infrastructure.Data;

namespace NEXORA.Infrastructure.Repository;

public class DepartmentRepository(ApplicationDbContext context):IDepartmentRepository
{
    public async Task<List<Department>> GetAllAsync()
    {
        return await context.Departments.ToListAsync();

    }

    public async Task<bool> Activate(string id)
    {
        var dep = await context.Departments.FindAsync(id);

        if (dep == null)
            throw new DepartmentNotFoundException(id);

        dep.Status = DepartmentStatusEnum.Active;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Deactivate(string id)
    {
        var dep = await context.Departments.FindAsync(id);

        if (dep == null)
            throw new DepartmentNotFoundException(id);

        dep.Status = DepartmentStatusEnum.Inactive;

        await context.SaveChangesAsync();

        return true;
    }

    public Task<bool> Create(DepartmentCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(DepartmentUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}