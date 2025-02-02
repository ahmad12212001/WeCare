﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Courses.Commands.CreateCourse;
public class CreateCourseCommandValidation : AbstractValidator<CreateCourseCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCourseCommandValidation(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext; 
        RuleFor(v => v.Name).Must(IsCourseNotExists)
            .MaximumLength(200)
            .NotEmpty().NotNull();
    }

    private bool IsCourseNotExists(string courseName)
    {
        return !(_context.Courses.AsNoTracking().Any(c => c.Name == courseName));
    }
}