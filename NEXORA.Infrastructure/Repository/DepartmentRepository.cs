using Microsoft.EntityFrameworkCore;
using NEXORA.Application.Interfaces.RepositoryInterfaces;
using NEXORA.Domain.Entities;
using NEXORA.Infrastructure.Data;

namespace NEXORA.Infrastructure.Repository;

public class DepartmentRepository(ApplicationDbContext context):IDepartmentRepository
{
    public async Task<List<Department>> GetAllAsync()
    {
        return await context.Departments.ToListAsync();

    }
}