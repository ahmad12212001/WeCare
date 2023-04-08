using WeCare.Application.Common.Mappings;
using WeCare.Domain.Entities;

namespace WeCare.Application.TodoLists.Queries.ExportTodos;
public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
