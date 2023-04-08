using System.Globalization;
using CsvHelper;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.TodoLists.Queries.ExportTodos;
using WeCare.Infrastructure.Files.Maps;

namespace WeCare.Infrastructure.Files;
public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
