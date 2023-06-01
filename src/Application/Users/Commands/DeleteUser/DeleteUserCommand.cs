using MediatR;
using Microsoft.AspNetCore.Identity;
using WeCare.Domain.Entities;

namespace WeCare.Application.Users.Commands.DeleteUser;
public record DeleteUserCommand : IRequest<string>
{
    public string Id { get; set; } = null!;
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user == null)
        {
            return string.Empty;
        }

        await _userManager.DeleteAsync(user);

        return user.Id;
    }
}