using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace Jobs;

public class VolunteerReminderJob : IRecurringJob
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    public VolunteerReminderJob(IServiceProvider serviceProvider)
    {
        _applicationDbContext = serviceProvider.GetRequiredService<IApplicationDbContext>();
        _emailService = serviceProvider.GetRequiredService<IEmailService>();
        _configuration = serviceProvider.GetRequiredService<IConfiguration>();
    }

    public void Run()
    {
        var requests = _applicationDbContext.Requests
                       .Where(i => i.RequestStatus == RequestStatus.AcceptedByVolunteer &&
                              i.DueDate.Date < DateTime.UtcNow.Date.AddDays(1)
                              && i.RequestType == RequestType.Assistance);

        foreach (var request in requests)
        {
            SendEmail(request);
        }

    }


    private void SendEmail(Request requestVolunteer)
    {
        var templateHtml = File.ReadAllText(_configuration["EmailTemplates:ReminderTemplate"]!)!;
        string acceptAction = $"{_configuration["ServerSettings:BaseUrl"]}api/Requests/Accept/{requestVolunteer.Id}/{requestVolunteer.AssignedVolunteerStudentId}";
        string rejectAction = $"{_configuration["ServerSettings:BaseUrl"]}api/Requests/Reject/{requestVolunteer.Id}/{requestVolunteer.AssignedVolunteerStudentId}";
        templateHtml = templateHtml.Replace("@RequestDescription", requestVolunteer.Description);
        templateHtml = templateHtml.Replace("@AcceptAction", acceptAction);
        templateHtml = templateHtml.Replace("@RejectAction", rejectAction);

        _emailService.SendEmailHtmlAsync(new EmailMessage
        {
            Subject = "Request Reminder",
            Content = templateHtml,
            To = new Dictionary<string, string>()
            {
             { requestVolunteer.AssignedVolunteerStudent.User.Email,requestVolunteer.AssignedVolunteerStudent.User.FirstName }
             }
        }).GetAwaiter().GetResult();
    }


}
