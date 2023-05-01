using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Courses.Commands.DeleteCourse;
using WeCare.Application.Exams.Commands.CreateExam;
using WeCare.Application.Exams.Commands.DeleteCourse;
using WeCare.Application.Exams.Commands.UpdateExam;
using WeCare.Application.Exams.Dto;
using WeCare.Application.Exams.Queries.GetExam;
using WeCare.Application.Majors.Commands.CreateMajors;
using WeCare.Application.Majors.Commands.UpdateMajors;
using WeCare.Application.Majors.Dtos;
using WeCare.Application.Majors.Queries.GetMajor;
using WeCare.WebUI.Controllers;

namespace WeCare.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExamController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ExamDto>> GetExam(string name)
    {
        return await Mediator.Send(new GetExamQuery() { Name = name });
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateExam(CreateExamsCommand createExamCommand)
    {
        return await Mediator.Send(createExamCommand);
    }
    [HttpPut]
    public async Task<ActionResult<ExamDto>> Update(UpdateExamCommand command)
    {
        return await Mediator.Send(command);
    }
   
    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        return await Mediator.Send(new DeleteExamCommand { Id = id });
    }
}
