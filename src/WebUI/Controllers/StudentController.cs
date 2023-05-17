using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Students.Commands.CreateStudent;
using WeCare.Application.Students.Queries;
using WeCare.Application.Students.StudentDto;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentController : ApiControllerBase
{
  
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentsDto>> GetStudent(string id)
    {
        return await Mediator.Send(new GetStudentQuery() { StudentId = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateStudentCommand command)
    {
        return await Mediator.Send(command);
    }

   
}
