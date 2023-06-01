using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.MajorGroups.Commands.CreateMajorGroup;
using WeCare.Application.MajorGroups.Commands.DeleteMajorGroup;
using WeCare.Application.MajorGroups.Commands.UpdateMajorGrouo;
using WeCare.Application.MajorGroups.Dtos;
using WeCare.Application.MajorGroups.Queries.GetMajorGroup;
using WeCare.Application.MajorGroups.Queries.GetMajorGroups;
using WeCare.Application.MajorGroups.Queries.GetMajorGroupsPagination;
using WeCare.Domain.Entities;
using WeCare.WebUI.Controllers;

namespace WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MajorGroupsController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateMajorGroupCommand createMajorGroupCommand)
    {
        return await Mediator.Send(createMajorGroupCommand);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MajorGroup>> Update(int id, [FromBody] UpdateMajorGroupCommand createMajorGroupCommand)
    {
        return await Mediator.Send(createMajorGroupCommand);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MajorGroup>> Delete(int id)
    {
        var deleteCommand = new DeleteMajorGroupCommand()
        {
            Id = id
        };

        return await Mediator.Send(deleteCommand);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MajorGroup?>> Get(int id)
    {
        var getMajorGroupQuery = new GetMajorGroupQuery()
        {
            Id = id
        };

        return await Mediator.Send(getMajorGroupQuery);
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<MajorGroup>>> Get()
    {
        var getMajorGroupsQuery = new GetMajorGroupsQuery();

        return await Mediator.Send(getMajorGroupsQuery);
    }

    [HttpGet()]
    public async Task<ActionResult<PaginatedList<MajorGroupDto>>> GetPagination([FromQuery] GetMajorGroupsPaginationQuery getMajorGroupsPaginationQuery)
    {
        return await Mediator.Send(getMajorGroupsPaginationQuery);
    }
}
