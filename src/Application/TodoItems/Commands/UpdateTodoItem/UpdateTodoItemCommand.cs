using MediatR;
using WeCare.Application.Common.Exceptions;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.TodoItems.Commands.UpdateTodoItem;
public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        //var entity = await _context.TodoItems
        //    .FindAsync(new object[] { request.Id }, cancellationToken);

        //if (entity == null)
        //{
        //    throw new NotFoundException(nameof(TodoItem), request.Id);
        //}

        //entity.Title = request.Title;
        //entity.Done = request.Done;

        //await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
