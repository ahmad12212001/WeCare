using MediatR;
using Microsoft.AspNetCore.Identity;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Application.Students.Commands.CreateStudent;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.Users.Commands.CreateUser;
public record CreateUserCommand : IRequest<string>
{
    public string StudentId { get; set; } = null!;
    public string Major { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Role { get; set; } = null!;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IMediator _mediator;
    public CreateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService, IPasswordGenerator passwordGenerator, IMediator mediator)
    {
        _userManager = userManager;
        _passwordGenerator = passwordGenerator;
        _emailService = emailService;
        _mediator = mediator;
    }
    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {


        switch (request.Role)
        {
            case "VolunteerStudent":
            case "DisabilityStudent":
                var studentResult = await _mediator.Send(new CreateStudentCommand()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Major = request.Major,
                    PhoneNumber = request.PhoneNumber,
                    StudentId = request.StudentId,
                    Type = (StudentType)Enum.Parse(typeof(StudentType), request.Role)
                });

                return studentResult.ToString();

            default:
                var applicationUser = new ApplicationUser()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IsActive = true,
                    NormalizedEmail = request.Email.ToUpper(),
                    UserName = request.Email,
                    NormalizedUserName = request.Email.ToUpper(),
                    PhoneNumber = request.PhoneNumber,

                };

                var password = _passwordGenerator.GeneratePassword();

                var result = await _userManager.CreateAsync(applicationUser, password);
                if (result.Succeeded)
                {
                    await _emailService.SendEmailAsync(new EmailMessage
                    {
                        Subject = "Registration Completed",
                        Content = "You have successfully Registered in (We care). \n" +
                        "Kindly Find you Login options. \n" +
                        "UserName:" + request.Email + "\n" +
                        "Password:" + password,
                        To = new Dictionary<string, string>()
                {
                    { request.Email,request.FirstName }
                }
                    });

                    await _userManager.AddToRoleAsync(applicationUser, request.Role);

                    return applicationUser.Id;

                }
                return string.Empty;
        }

    }
}
