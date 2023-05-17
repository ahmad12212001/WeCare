using Microsoft.AspNetCore.Http;

namespace WeCare.Application.Common.Models;
public class BlobStorageRequest
{
    public IFormFile File { get; set; } = null!;
    public string Path { get; set; } = null!;

}
