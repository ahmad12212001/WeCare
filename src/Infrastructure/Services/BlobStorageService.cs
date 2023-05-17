using Microsoft.Extensions.Configuration;
using Minio;
using Minio.AspNetCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;

namespace WeCare.Infrastructure.Services;
public class BlobStorageService : IBlobStorageService
{
    private readonly MinioClient _minioClient;
    private readonly IConfiguration _configuration;
    public BlobStorageService(IMinioClientFactory minioClientFactory, IConfiguration configuration)
    {
        _minioClient = minioClientFactory.CreateClient();
        _configuration = configuration;

    }
    public async Task<(Result Result, string Path)> UploadFileAsync(BlobStorageRequest blobStorageRequest)
    {
        try
        {
            string filePath = $"{blobStorageRequest.Path}/{blobStorageRequest.File.FileName}";
            using (var ms = new MemoryStream())
            {
                await blobStorageRequest.File.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var putObjectArgs = new PutObjectArgs()
                        .WithBucket("wecare")
                        .WithObject(filePath)
                        .WithStreamData(ms).WithObjectSize(blobStorageRequest.File.Length);
                var response = await _minioClient.PutObjectAsync(putObjectArgs);
                if (response.Size > 0)
                {
                    return (new Result(true, Array.Empty<string>()), $"{_configuration["Bucket:BasePath"]}/{filePath}");
                }

            }



            return (new Result(false, Array.Empty<string>()), string.Empty);
        }
        catch (Exception ex)
        {
            string[] errors = new string[1] { ex.Message };
            return (new Result(false, errors), string.Empty); ;
        }

    }
}
