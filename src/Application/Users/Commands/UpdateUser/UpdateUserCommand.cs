using MediatR;
using Microsoft.AspNetCore.Identity;
using WeCare.Application.Students.Commands.UpdateStudent;
using WeCare.Application.Users.Dtos;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.Users.Commands.UpdateUser;
public record UpdateUserCommand : IRequest<ApplicationUserDto?>
{
    public string StudentId { get; set; } = null!;
    public string Major { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Id { get; set; } = null!;
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationUserDto?>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;
    public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<ApplicationUserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user == null)
        {
            return null;
        }

        user.PhoneNumber = request.PhoneNumber;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        await _userManager.UpdateAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);

        await _userManager.AddToRoleAsync(user, request.Role);

        switch (request.Role)
        {
            case "VolunteerStudent":
            case "DisabilityStudent":
                await _mediator.Send(new UpdateStudentCommand()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Major = request.Major,
                    PhoneNumber = request.PhoneNumber,
                    StudentId = request.StudentId,
                    Type = (StudentType)Enum.Parse(typeof(StudentType), request.Role),
                    UserId=request.Id
                });
                break;
        }

        return new ApplicationUserDto
        {
            Email = user.Email,
            FirstName=user.FirstName,
            LastName=user.LastName,
            Major = request.Major,
            PhoneNumber = user.PhoneNumber,
            StudentId = request.StudentId,
            Id = user.Id,
            Role= request.Role
        };


    }
}