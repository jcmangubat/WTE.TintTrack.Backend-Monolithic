
using Microsoft.AspNetCore.Http;

namespace WTE.TintTrack.Application.Shared.Interfaces;

public interface IImageKitUploadService
{
    Task<string> UploadFileAsync(IFormFile formFile, string assetFolder);
    Task DeleteFileAsync(string cdnUrl);
}
