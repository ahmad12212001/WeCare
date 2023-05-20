using Microsoft.AspNetCore.Mvc;
using WeCare.Application.RequestVolunteers.Commands.CreateRequestVolunteer;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestVolunteersController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<int>> Post(RequestVolunteerCommand command)
    {
        return await Mediator.Send(command);
    }
}
