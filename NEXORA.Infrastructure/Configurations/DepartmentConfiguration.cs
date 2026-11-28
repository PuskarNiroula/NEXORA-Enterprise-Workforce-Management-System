using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Configurations;

public class DepartmentConfiguration:IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("department");

        builder.HasKey(e => e.DepartmentId);
        
        builder.Property(e=>e.DepartmentId).HasColumnName("department_id").ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasColumnName("name").IsRequired();
        builder.Property(e => e.Status).HasColumnName("status").IsRequired().HasDefaultValue(1);


    }
}