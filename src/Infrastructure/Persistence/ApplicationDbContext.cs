using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;
using WeCare.Infrastructure.Persistence.Interceptors;

namespace WeCare.Infrastructure.Persistence;
public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<Course> Courses => Set<Course>();

    public DbSet<Document> Documents => Set<Document>();

    public DbSet<Exam> Exams => Set<Exam>();

    public DbSet<Major> Majors => Set<Major>();

    public DbSet<Material> Materials => Set<Material>();

    public DbSet<Request> Requests => Set<Request>();

    public DbSet<RequestFeedBack> RequestFeedBacks => Set<RequestFeedBack>();

    public DbSet<RequestVolunteer> RequestVolunteers => Set<RequestVolunteer>();

    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);

    }
}
