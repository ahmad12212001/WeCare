using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Courses.Dtos;
using WeCare.Application.Exams.Dto;
using WeCare.Domain.Entities;

namespace WeCare.Application.Exams.Commands.UpdateExam;
public record UpdateExamCommand : IRequest<ExamDto>
{
    public int Id { get; set; }
    public DateTime DueDate { get; set; }
    public string Hallno { get; set; } = null!;
    public string Location { get; set; } = null!;
}

public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, ExamDto> 
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public UpdateExamCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _context = applicationDbContext;
        _mapper = mapper;
    }
    public async Task<ExamDto> Handle(UpdateExamCommand request, CancellationToken cancellationToken) 
    {
        var exam = (await _context.Exams.FindAsync(request.Id))!;
        exam.Location = request.Location;
        exam.HallNo= request.Hallno;
        exam.DueDate =request.DueDate.ToUniversalTime();
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ExamDto>(exam);
    }

}
