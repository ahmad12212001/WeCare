using WeCare.Application.Common.Mappings;
using WeCare.Domain.Entities;

namespace WeCare.Application.TodoLists.Queries.GetTodos;
public class TodoListDto : IMapFrom<TodoList>
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Colour { get; set; }

    public IList<TodoItemDto> Items { get; set; }
}
