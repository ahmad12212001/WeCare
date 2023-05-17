using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Majors.Commands.CreateMajors;
using WeCare.Application.Majors.Commands.DeleteMajors;
using WeCare.Application.Majors.Commands.UpdateMajors;
using WeCare.Application.Majors.Dtos;
using WeCare.Application.Majors.Queries.GetMajor;


namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MajorsController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PaginatedList<MajorDto>>> Get([FromQuery] GetMajorsQuery getMajorsQuery)
    {
        return await Mediator.Send(getMajorsQuery);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MajorDto>> GetMajor(int id)
    {
        return await Mediator.Send(new GetMajorQuery() { MajorId = id});
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateMajor(CreateMajorCommand createMajorCommand)
    {
        return await Mediator.Send(createMajorCommand);
    }
    [HttpPut]
    public async Task<ActionResult<MajorDto>> Update(UpdateMajorsCommand command)
    {
        return await Mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
        return await Mediator.Send(new DeleteMajorsCommand { MajorId = id });
    }
}