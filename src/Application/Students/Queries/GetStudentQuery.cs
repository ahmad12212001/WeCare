using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Majors.Dtos;
using WeCare.Application.Students.StudentDto;
using WeCare.Domain.Entities;

namespace WeCare.Application.Students.Queries;
public record GetStudentQuery : IRequest<StudentsDto>
{
    public string StudentId { get; set; } = null!;

}
public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentsDto> { 
    private readonly IApplicationDbContext _context;
private readonly IMapper _mapper;

public GetStudentQueryHandler(IApplicationDbContext context, IMapper mapper)
{
    _context = context;
    _mapper = mapper;
}

    public async Task<StudentsDto> Handle(GetStudentQuery request, CancellationToken cancellationToken) { 
    var student= _context.Students.AsNoTracking().Single(t => t.StudentId == request.StudentId);
        return _mapper.Map<StudentsDto>(student);



    }
}