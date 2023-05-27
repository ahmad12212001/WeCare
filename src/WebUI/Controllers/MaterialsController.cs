using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Materials.Commands.ChangeMaterialStatus;
using WeCare.Application.Materials.Commands.CreateMaterial;
using WeCare.Application.Materials.Dto;
using WeCare.Application.Materials.Queries.GetMaterials;

namespace WeCare.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterialsController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<int>> CreateMaterail([FromForm] CreateMaterialCommand createMaterialCommand)
    {
        return await Mediator.Send(createMaterialCommand);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<MaterialDto>>> Get([FromQuery] GetMaterialsQuery getMaterialsQuery)
    {
        return await Mediator.Send(getMaterialsQuery);
    }

    [HttpPut("UpdateStatus")]
    public async Task<ActionResult<bool>> UpdateStatus([FromBody] ChangeMaterialStatusCommand changeMaterialStatusCommand)
    {
        return await Mediator.Send(changeMaterialStatusCommand);
    }
}
