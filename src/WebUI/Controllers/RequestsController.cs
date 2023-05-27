using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Requests.Commands.AcceptRequest;
using WeCare.Application.Requests.Commands.CreateRequest;
using WeCare.Application.Requests.Commands.RejectRequest;
using WeCare.Application.Requests.Commands.TerminateRequest;
using WeCare.Application.Requests.Dtos;
using WeCare.Application.Requests.Queries.GetDisabilityStudentRequests;

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
    public async Task<ActionResult<PaginatedList<RequestDto>>> Get([FromQuery] GetRequestsPaginationQuery getRequestQuery)
    {
        return await Mediator.Send(getRequestQuery);
    }

    [HttpGet("Accept/{id}/{volunteerStudentId}")]
    public async Task<ActionResult<bool>> Accept(int id, int volunteerStudentId)
    {
        return await Mediator.Send(new AcceptRequestCommand
        {
            VolunteerStudentId = volunteerStudentId,
            RequestId = id
        });
    }

    [HttpGet("Reject/{id}/{volunteerStudentId}")]
    public async Task<ActionResult<bool>> Reject(int id, int volunteerStudentId)
    {
        return await Mediator.Send(new RejectRequestCommand
        {
            VolunteerStudentId = volunteerStudentId,
            RequestId = id
        });
    }

    [HttpPut("Terminate/{id}")]
    public async Task<ActionResult<bool>> Terminate(int id)
    {
        return await Mediator.Send(new TerminateRequestCommand
        {
            RequestId = id
        });
    }
}
