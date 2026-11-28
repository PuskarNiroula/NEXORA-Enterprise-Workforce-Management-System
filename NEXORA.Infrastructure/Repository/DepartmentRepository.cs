using Microsoft.EntityFrameworkCore;
using NEXORA.Application.Dto.Department;
using NEXORA.Application.Exception.Department;
using NEXORA.Application.Interfaces.RepositoryInterfaces;
using NEXORA.Domain.Entities;
using NEXORA.Domain.Enums;
using NEXORA.Infrastructure.Data;

namespace NEXORA.Infrastructure.Repository;

public class DepartmentRepository(ApplicationDbContext context) : IDepartmentRepository
{
    public async Task<List<Department>> GetAllAsync()
    {
        return await context.Departments.ToListAsync();
    }

    public async Task<bool> Activate(Guid id)
    {
        var department = await context.Departments.FindAsync(id);

        if (department == null)
            throw new DepartmentNotFoundException(id.ToString());

        department.Status = DepartmentStatusEnum.Active;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Deactivate(Guid id)
    {
        var department = await context.Departments.FindAsync(id);

        if (department == null)
            throw new DepartmentNotFoundException(id.ToString());

        department.Status = DepartmentStatusEnum.Inactive;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CreateAsync(DepartmentCreateDto dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            Status = DepartmentStatusEnum.Active
        };

        await context.Departments.AddAsync(department);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(DepartmentUpdateDto dto)
    {
        var department = await context.Departments.FindAsync(dto.DepartmentId);

        if (department == null)
            throw new DepartmentNotFoundException(dto.DepartmentId.ToString());

        department.Name = dto.Name;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Department?> GetDepartmentByNameAsync(string name, Guid? id = null)
    {
        var query = context.Departments
            .Where(s => s.Name == name);

        if (id.HasValue)
        {
            query = query.Where(s => s.DepartmentId != id.Value);
        }

        return await query.FirstOrDefaultAsync();
    }
}
