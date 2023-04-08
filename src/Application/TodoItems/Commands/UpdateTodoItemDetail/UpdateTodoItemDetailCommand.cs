using MediatR;
using WeCare.Application.Common.Exceptions;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.TodoItems.Commands.UpdateTodoItemDetail;
public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        //var entity = await _context.TodoItems
        //    .FindAsync(new object[] { request.Id }, cancellationToken);

        //if (entity == null)
        //{
        //    throw new NotFoundException(nameof(TodoItem), request.Id);
        //}

        //entity.ListId = request.ListId;
        //entity.Priority = request.Priority;
        //entity.Note = request.Note;

        //await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
