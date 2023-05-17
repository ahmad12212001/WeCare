using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Courses.Commands.CreateCourse;
using WeCare.Application.Courses.Commands.DeleteCourse;
using WeCare.Application.Courses.Commands.UpdateCourse;
using WeCare.Application.Courses.Dtos;
using WeCare.Application.Courses.Queries.GetCourse;
using WeCare.Application.Courses.Queries.GetCourses;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<CourseDto>>> Get([FromQuery] GetCoursesPaginationQuery getCoursesQuery)
    {
        return await Mediator.Send(getCoursesQuery);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<CourseDto>>> Get()
    {
        var coursesQuery = new GetCoursesQuery();
        return await Mediator.Send(coursesQuery);
    }

    [HttpGet("academic")]
    public async Task<ActionResult<List<CourseDto>>> Get([FromQuery] GetAcademicStaffCoursesQuery getCoursesQuery)
    {
        return await Mediator.Send(getCoursesQuery);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        return await Mediator.Send(new GetCourseQuery() { CourseId = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCourseCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CourseDto>> Update(int id, UpdateCourseCommand command)
    {
        var course = await Mediator.Send(new GetCourseQuery() { CourseId = id });
        if (course == null)
        {
            return NotFound();
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        return await Mediator.Send(new DeleteCourseCommand { CourseId = id });
    }
}
