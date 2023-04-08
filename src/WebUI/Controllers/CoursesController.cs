using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Courses.Commands.CreateCourse;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : ApiControllerBase
{
  

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCourseCommand command)
    {
        return await Mediator.Send(command);
    }
}
