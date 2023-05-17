using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Entities;

namespace WeCare.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<Exam> Exams { get; }
    DbSet<Job> Jobs { get; }
    DbSet<Major> Majors { get; }
    DbSet<Material> Materials { get; }
    DbSet<Request> Requests { get; }
    DbSet<RequestFeedBack> RequestFeedBacks { get; }
    DbSet<RequestVolunteer> RequestVolunteers { get; }
    DbSet<Student> Students { get; }
    DbSet<DisabilityStudent> DisabilityStudents { get; }
    DbSet<VolunteerStudent> VolunteerStudents { get; }
    DbSet<StudentCourse> StudentCourses { get; }
    DbSet<MajorGroup> MajorGroups { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
