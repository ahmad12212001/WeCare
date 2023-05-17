using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Requests.Commands.CreateRequest;
using WeCare.Application.Requests.Dto;
using WeCare.Application.Requests.Queries.GetDisabilityStudentRequests;
using WeCare.WeCare.WebUI.Controllers;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateRequestCommand createRequestCommand)
    {
        return await Mediator.Send(createRequestCommand);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<RequestDto>>> Get([FromQuery] GetDisabilityStudentRequestsPaginationQuery getRequestQuery)
    {
        return await Mediator.Send(getRequestQuery);
    }
}
