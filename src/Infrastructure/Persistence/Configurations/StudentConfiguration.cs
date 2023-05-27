using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Entities;

namespace WeCare.Infrastructure.Persistence.Configurations;
public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(i => i.Rate).HasDefaultValue(0);
        builder.Property(i => i.TotalRequest).HasDefaultValue(0);

    }
}
