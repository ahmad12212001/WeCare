using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Students;
using WeCare.Application.Students.Commands.CreateStudent;
using WeCare.Application.Students.Commands.UpdateStudent;
using WeCare.Application.Students.Queries;
using WeCare.Domain.Entities;

namespace WeCare.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ApiControllerBase
{

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        return await Mediator.Send(new GetStudentQuery() { StudentId = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateStudentCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<StudentDto>>> GetStudents([FromQuery] GetStudentsPaginationQuery getStudentsPaginationQuery)
    {
        return await Mediator.Send(getStudentsPaginationQuery);
    }

    [HttpPut]
    public async Task<ActionResult<Student?>> UpdateStudent(UpdateStudentCommand updateStudentCommand)
    {
        return await Mediator.Send(updateStudentCommand);
    }
}
