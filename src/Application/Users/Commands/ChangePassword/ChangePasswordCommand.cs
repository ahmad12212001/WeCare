using MediatR;
using Microsoft.AspNetCore.Identity;
using WeCare.Domain.Entities;

namespace WeCare.Application.Users.Commands.ChangePassword;
public record ChangePasswordCommand : IRequest<bool>
{
    public string Id { get; set; } = null!;
    public string Password { get; set; } = null!;
}


public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            return false;
        }

        var result = await _userManager.RemovePasswordAsync(user);
        if (result.Succeeded)
        {
            result = await _userManager.AddPasswordAsync(user, request.Password);

            return result.Succeeded;
        }

        return false;

    }
}