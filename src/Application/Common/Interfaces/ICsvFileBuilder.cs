using WeCare.Application.TodoLists.Queries.ExportTodos;

namespace WeCare.Application.Common.Interfaces;
public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
