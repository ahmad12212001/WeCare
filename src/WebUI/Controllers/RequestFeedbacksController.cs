using Microsoft.AspNetCore.Mvc;
using WeCare.Application.RequestFeedbacks.Commands.CreateRequestFeedback;
using WeCare.Application.RequestFeedbacks.Dtos;
using WeCare.Application.RequestFeedbacks.Queries.GetRequestFeedbacks;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestFeedbacksController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<int>> Post(CreateRequestFeedbackCommand requestFeedbackCommand)
    {
        return await Mediator.Send(requestFeedbackCommand);
    }


    [HttpGet]
    public async Task<ActionResult<List<RequestFeedbackDto>>> Get([FromQuery] GetRequestFeedbacksQuery getRequestFeedbacksQuery)
    {
        return await Mediator.Send(getRequestFeedbacksQuery);
    }
}
