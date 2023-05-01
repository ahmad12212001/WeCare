﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Majors.Commands.DeleteMajors;
public class DeleteMajorsValidator : AbstractValidator<DeleteMajorsCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMajorsValidator(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
        RuleFor(v => v.MajorId).Must(IsMajorExists)
           .NotEmpty().NotNull();
    }
    private bool IsMajorExists(int MajorId) {
        return _context.Courses.AsNoTracking().Any(m => m.Id == MajorId);
    }
}