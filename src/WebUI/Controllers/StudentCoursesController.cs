using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Courses.Dtos;
using WeCare.Application.StudentCourses.Commands.CreateStudentCourse;
using WeCare.Application.StudentCourses.Query.GetStudentAvailableCourses;
using WeCare.Domain.Entities;
using WeCare.WebUI.Controllers;

namespace WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentCoursesController : ApiControllerBase
{

    [HttpPost()]
    public async Task<ActionResult<Unit>> StudentCourses(StudentCourseCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("{studentId}")]
    public async Task<ActionResult<List<CourseDto>>> GetStudentAvailableCourses(int studentId)
    {
        var getStudentAvailableCoursesQuery = new GetStudentAvailableCoursesQuery()
        {
            StudentId = studentId
        };

        return await Mediator.Send(getStudentAvailableCoursesQuery);
    }
}
