using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Requests.Commands.CreateRequest;
using WeCare.WebUI.Controllers;

namespace WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateRequestCommand createRequestCommand)
    {
        return await Mediator.Send(createRequestCommand);
    }
}
