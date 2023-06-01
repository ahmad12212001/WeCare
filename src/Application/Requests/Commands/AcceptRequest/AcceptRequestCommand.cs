using MediatR;
using Microsoft.Extensions.Configuration;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.Requests.Commands.AcceptRequest;

public record AcceptRequestCommand : IRequest<bool>
{
    public int RequestId { get; set; }

    public int VolunteerStudentId { get; set; }
}

public class AcceptRequestCommandHandler : IRequestHandler<AcceptRequestCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;
    public AcceptRequestCommandHandler(IApplicationDbContext applicationDbContext, IEmailService emailService,
        IConfiguration configuration, IIdentityService identityService)
    {
        _applicationDbContext = applicationDbContext;
        _emailService = emailService;
        _configuration = configuration;
        _identityService = identityService;
    }

    public async Task<bool> Handle(AcceptRequestCommand request, CancellationToken cancellationToken)
    {
        var currentRequest = (await _applicationDbContext.Requests.FindAsync(request.RequestId))!;

        if (currentRequest.AssignedVolunteerStudentId.HasValue && currentRequest.AssignedVolunteerStudentId == request.VolunteerStudentId)
        {
            currentRequest.LastModified = DateTime.UtcNow;
            if (currentRequest.RequestType == RequestType.Assistance)
            {
                await SendEmail(
                   (await _applicationDbContext.VolunteerStudents.FindAsync(request.VolunteerStudentId))!,
                   currentRequest.DisabilityStudent, currentRequest);
            }
        }
        else
        {
            if (currentRequest.RequestStatus == RequestStatus.ReAssigning)
            {
                if (currentRequest.RequestType == RequestType.Assistance)
                {
                    await SendEmail(
                    (await _applicationDbContext.VolunteerStudents.FindAsync(request.VolunteerStudentId))!,
                    currentRequest.DisabilityStudent, currentRequest);

                }

                var volunteer = (await _applicationDbContext.VolunteerStudents.FindAsync(request.VolunteerStudentId))!;
                await SendPartnerInfoEmail(volunteer, currentRequest, currentRequest.DisabilityStudent.User.FirstName, currentRequest.DisabilityStudent.User.Email!);
                await SendPartnerInfoEmail(currentRequest.DisabilityStudent, currentRequest, volunteer.User.FirstName, volunteer.User.Email!);

            }
            else
            {
                var volunteer = (await _applicationDbContext.VolunteerStudents.FindAsync(request.VolunteerStudentId))!;
                await SendPartnerInfoEmail(volunteer, currentRequest, currentRequest.DisabilityStudent.User.FirstName, currentRequest.DisabilityStudent.User.Email!);
                await SendPartnerInfoEmail(currentRequest.DisabilityStudent, currentRequest, volunteer.User.FirstName, volunteer.User.Email!);

            }

            currentRequest.RequestStatus = RequestStatus.AcceptedByVolunteer;
            currentRequest.AssignedVolunteerStudentId = request.VolunteerStudentId;
        }

        _applicationDbContext.Requests.Update(currentRequest);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);


        return true;
    }

    private async Task SendEmail(VolunteerStudent volunteerStudent, DisabilityStudent disabilityStudent, Request request)
    {
        var templateHtml = File.ReadAllText(_configuration["EmailTemplates:VolunteerInfoTemplate"]!)!;
        templateHtml = templateHtml.Replace("@DisabilityStudent", $"{disabilityStudent.User.FirstName} {disabilityStudent.User.LastName}");
        templateHtml = templateHtml.Replace("@ExamDate", request!.Exam!.Name);
        templateHtml = templateHtml.Replace("@VolunteerName", $"{volunteerStudent.User.FirstName} {volunteerStudent.User.LastName}");
        templateHtml = templateHtml.Replace("@StudentId", volunteerStudent.StudentId);
        templateHtml = templateHtml.Replace("@Major", volunteerStudent.Major.Name);
        templateHtml = templateHtml.Replace("@Rate", volunteerStudent.Rate.ToString());
        templateHtml = templateHtml.Replace("@DisabilityStudentId", disabilityStudent.StudentId);
        templateHtml = templateHtml.Replace("@Major", disabilityStudent.Major.Name);
        templateHtml = templateHtml.Replace("@Rate", disabilityStudent.Rate.ToString());

        var colection = (await _identityService.GetUsersInRoleAsync("DeanOffice")).ToDictionary(i => i.Email!, i => i.FirstName);
        var academicStaff = request.Exam.Course.User;
        colection.Add(academicStaff.Email!, academicStaff.FirstName);
        await _emailService.SendEmailHtmlAsync(new EmailMessage
        {
            Subject = "Request Informationm",
            Content = templateHtml,
            To = colection
        });
    }

    private async Task SendPartnerInfoEmail(Student student, Request request, string name, string email)
    {
        var templateHtml = File.ReadAllText(_configuration["EmailTemplates:PartnerInfoTemplate"]!)!;
        templateHtml = templateHtml.Replace("@RequestDescription", request.Description);
        templateHtml = templateHtml.Replace("@Name", $"{student.User.FirstName} {student.User.LastName}");
        templateHtml = templateHtml.Replace("@Email", student.User.Email!);
        templateHtml = templateHtml.Replace("@PhoneNumber", student.User.PhoneNumber);

        await _emailService.SendEmailHtmlAsync(new EmailMessage
        {
            Subject = "Request Informationm",
            Content = templateHtml,
            To = new Dictionary<string, string> { { email, name } }
        });
    }
}
