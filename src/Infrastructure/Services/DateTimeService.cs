using WeCare.Application.Common.Interfaces;

namespace WeCare.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
