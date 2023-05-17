using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Materials.Commands.CreateMaterial;

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
}
