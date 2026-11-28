using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");

        builder.HasKey(e => e.EmployeeId);

        builder.Property(e => e.EmployeeId)
            .HasColumnName("employee_id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id");
        

        builder.Property(e => e.EmployeeCode)
            .HasColumnName("employee_code")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => e.EmployeeCode)
            .IsUnique();

        builder.Property(e => e.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired();

        builder.HasIndex(e => e.DepartmentId);

        builder.Property(e => e.WorkEmail)
            .HasColumnName("work_email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.PersonalEmail)
            .HasColumnName("personal_email")
            .HasMaxLength(255);

        builder.Property(e => e.Image)
            .HasColumnName("image")
            .HasMaxLength(500);

        builder.Property(e => e.DOB)
            .HasColumnName("dob")
            .HasColumnType("date");

        builder.Property(e => e.Gender)
            .HasColumnName("gender")
            .HasMaxLength(20);

        builder.Property(e => e.JoiningDate)
            .HasColumnName("joining_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(e => e.EmploymentStatus)
            .HasColumnName("employment_status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(e => e.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Employee>(e => e.UserId);
        
        builder.HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId);
    }
}
