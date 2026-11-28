using NEXORA.Domain.Entities;
using NEXORA.Domain.Enums;

namespace NEXORA.Infrastructure.Data.Seeder;

using Microsoft.EntityFrameworkCore;



public static class DepartmentSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Departments.AnyAsync())
            return;

        var departments = new List<Department>
        {
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Human Resources",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Information Technology",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Finance",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Marketing",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Sales",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Operations",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Administration",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Customer Support",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Research and Development",
                Status = DepartmentStatusEnum.Active
            },
            new()
            {
                DepartmentId = Guid.NewGuid(),
                Name = "Legal and Compliance",
                Status = DepartmentStatusEnum.Active
            }
        };

        await context.Departments.AddRangeAsync(departments);
        await context.SaveChangesAsync();
    }
}