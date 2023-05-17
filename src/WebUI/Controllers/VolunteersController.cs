using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.RequestVolunteers.Dtos;
using WeCare.Application.RequestVolunteers.Query.GetRequestVolunteers;
using WeCare.WeCare.WebUI.Controllers;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VolunteersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<VolunteerDto>>> GetVolunteers([FromQuery] GetRequestVolunteerQuery getRequestVolunteerQuery)
    {
        return await Mediator.Send(getRequestVolunteerQuery);
    }
}
