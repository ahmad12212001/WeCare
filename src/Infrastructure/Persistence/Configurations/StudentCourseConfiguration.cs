using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Entities;

namespace WeCare.Infrastructure.Persistence.Configurations;
public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.HasKey(sc => new { sc.StudentId, sc.CourseId });
        builder.HasOne<Student>(sc => sc.Student)
               .WithMany(s => s.Courses)
               .HasForeignKey(sc => sc.StudentId);
        builder.HasOne<Course>(sc => sc.Course)
               .WithMany(s => s.Students)
               .HasForeignKey(sc => sc.CourseId);
    }
}
