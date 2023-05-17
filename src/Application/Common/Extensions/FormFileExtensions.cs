
namespace Microsoft.AspNetCore.Http;
public static class FormFileExtensions
{
    public static async Task<Stream> GetStreamAsync(this IFormFile formFile)
    {
        await using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }
}
