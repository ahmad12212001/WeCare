using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Courses.Commands.CreateCourse;
using WeCare.Application.Courses.Commands.DeleteCourse;
using WeCare.Application.Courses.Commands.UpdateCourse;
using WeCare.Application.Courses.Dtos;

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

    [HttpPut]
    public async Task<ActionResult<CourseDto>> Create(UpdateCourseCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
        return await Mediator.Send(new DeleteCourseCommand { CourseId = id });
    }
}
