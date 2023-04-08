﻿using MediatR;
using WeCare.Application.Common.Exceptions;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;
using WeCare.Domain.Events;

namespace WeCare.Application.TodoItems.Commands.DeleteTodoItem;
public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        //var entity = await _context.TodoItems
        //    .FindAsync(new object[] { request.Id }, cancellationToken);

        //if (entity == null)
        //{
        //    throw new NotFoundException(nameof(TodoItem), request.Id);
        //}

        //_context.TodoItems.Remove(entity);

        //entity.AddDomainEvent(new TodoItemDeletedEvent(entity));

        //await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}