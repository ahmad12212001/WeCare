using WeCare.Application.Common.Models;

namespace WeCare.Application.Common.Interfaces;
public interface IBlobStorageService
{
    public Task<(Result Result, string Path)> UploadFileAsync(BlobStorageRequest blobStorageRequest);
}
