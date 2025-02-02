﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Application.Courses.Dtos;

namespace WeCare.Application.Courses.Queries.GetCourse;
[Authorize]
public record GetCourseQuery : IRequest<CourseDto>
{

    public int CourseId { get; set; }
}

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetCourseQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = _context.Courses.Select(i => new CourseDto
        {
            Id = i.Id,
            MajorGroupId = i.MajorGroupId,
            AccadmeicStaffName = $"{i.User.FirstName} {i.User.LastName}",
            MajorGroupName = i.MajorGroup.Name,
            Name = i.Name,
            UserId = i.UserId
        }).AsNoTracking().Single(t => t.Id/**/ == request.CourseId);
        return course;



    }






}