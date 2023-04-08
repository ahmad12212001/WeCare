﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Domain.Enums;

namespace WeCare.Application.TodoLists.Queries.GetTodos;
[Authorize]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        //return new TodosVm
        //{
        //    PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
        //        .Cast<PriorityLevel>()
        //        .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
        //        .ToList(),

        //    Lists = await _context.TodoLists
        //        .AsNoTracking()
        //        .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
        //        .OrderBy(t => t.Title)
        //        .ToListAsync(cancellationToken)
        //};
        return null;
    }
}
