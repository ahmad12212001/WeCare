using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Majors.Commands.CreateMajor;
using WeCare.Application.Majors.Dtos;
using WeCare.Application.Majors.Queries.GetMajor;
using WeCare.Application.TodoLists.Queries.ExportTodos;
using WeCare.Application.TodoLists.Queries.GetTodos;

namespace WeCare.WebUI.Controllers;

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
}