using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace Jobs;

public class AssignVolunterJob : IRecurringJob
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    public AssignVolunterJob(IServiceProvider serviceProvider)
    {
        _applicationDbContext = serviceProvider.GetRequiredService<IApplicationDbContext>();
        _emailService = serviceProvider.GetRequiredService<IEmailService>();
        _configuration = serviceProvider.GetRequiredService<IConfiguration>();
    }

    public void Run()
    {
        var requests = _applicationDbContext.RequestVolunteers.Where(r => r.Request.RequestStatus != RequestStatus.Done &&
                       r.Request.RequestStatus != RequestStatus.AcceptedByVolunteer && r.Request.DueDate >= DateTime.UtcNow &&
                       r.Status == RequestVolunterStatus.None).OrderByDescending(s => s.VolunteerStudent.Rate)
                       .GroupBy(i => i.RequestId)
                       .Select(i => i.First()).ToListAsync().GetAwaiter().GetResult();

        var examRequests = requests.Where(r => r.Request.RequestType == RequestType.Assistance);
        HandleRequests(examRequests);

        var otherRequests = requests.Where(r => r.Request.RequestType != RequestType.Assistance);
        HandleRequests(otherRequests);
    }

    private void HandleRequests(IEnumerable<RequestVolunteer> requestVolunteers)
    {

        foreach (var requestVolunteer in requestVolunteers)
        {
            requestVolunteer.Status = RequestVolunterStatus.Sent;
            SendEmail(requestVolunteer);
        }

        var requests = requestVolunteers.Select(i => i.Request).ToList();

        _applicationDbContext.Requests.UpdateRange(requests);

        _applicationDbContext.SaveChangesAsync(default).GetAwaiter().GetResult();

    }

    private void SendEmail(RequestVolunteer requestVolunteer)
    {
        var templateHtml = File.ReadAllText(_configuration["EmailTemplates:AutoAssignTemplate"]!)!;
        string acceptAction = $"{_configuration["ServerSettings:BaseUrl"]}api/Requests/Accept/{requestVolunteer.RequestId}/{requestVolunteer.VolunteerStudentId}";
        string rejectAction = $"{_configuration["ServerSettings:BaseUrl"]}api/Requests/Reject/{requestVolunteer.RequestId}/{requestVolunteer.VolunteerStudentId}";
        templateHtml = templateHtml.Replace("@RequestDescription", requestVolunteer.Request.Description);
        templateHtml = templateHtml.Replace("@AcceptAction", acceptAction);
        templateHtml = templateHtml.Replace("@RejectAction", rejectAction);

        _emailService.SendEmailHtmlAsync(new EmailMessage
        {
            Subject = "Request Approvel",
            Content = templateHtml,
            To = new Dictionary<string, string>()
            {
             { requestVolunteer.VolunteerStudent.User.Email,requestVolunteer.VolunteerStudent.User.FirstName }
             }
        }).GetAwaiter().GetResult();
    }


}
