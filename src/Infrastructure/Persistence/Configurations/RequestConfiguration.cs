using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Entities;

namespace WeCare.Infrastructure.Persistence.Configurations;
public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.Property(i => i.Rate).HasDefaultValue(0);
    }
}
