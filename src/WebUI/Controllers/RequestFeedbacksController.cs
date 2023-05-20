using Microsoft.AspNetCore.Mvc;
using WeCare.Application.RequestFeedbacks.Commands;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestFeedbacksController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<int>> Post(RequestFeedbackCommand requestFeedbackCommand)
    {
        return await Mediator.Send(requestFeedbackCommand);
    }
}
