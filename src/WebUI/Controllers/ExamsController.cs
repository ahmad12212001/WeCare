using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Models;
using WeCare.Application.Exams.Commands.CreateExam;
using WeCare.Application.Exams.Commands.DeleteCourse;
using WeCare.Application.Exams.Commands.UpdateExam;
using WeCare.Application.Exams.Dto;
using WeCare.Application.Exams.Queries.GetExam;
using WeCare.Application.Exams.Queries.GetExams;

namespace WeCare.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamsController : ApiControllerBase
{
    [HttpGet("{name}")]
    public async Task<ActionResult<ExamDto>> GetExam(string name)
    {
        return await Mediator.Send(new GetExamQuery() { Name = name });
    }

    [HttpGet()]
    public async Task<ActionResult<PaginatedList<ExamDto>>> GetExams([FromQuery] GetPaginationExamsQuery getExamsQuery)
    {
        return await Mediator.Send(getExamsQuery);
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<ExamDto>>> GetExams()
    {
        var getExamsQuery = new GetExamsQuery();

        return await Mediator.Send(getExamsQuery);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExamDto>> GetExam(int id)
    {
        return await Mediator.Send(new GetExamByIdQuery() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateExam(CreateExamCommand createExamCommand)
    {
        return await Mediator.Send(createExamCommand);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExamDto>> Update(int id, UpdateExamCommand command)
    {
        var exam = await Mediator.Send(new GetExamByIdQuery() { Id = id });
        if(exam == null)
        {
            return NotFound();
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        return await Mediator.Send(new DeleteExamCommand { Id = id });
    }
}
