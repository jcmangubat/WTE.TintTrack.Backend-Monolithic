using Imagekit.Models;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Extensions;
using System.Text.Json;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Infrastructure.Shared.Services.ImageKit.DTOs;

namespace WTE.TintTrack.Infrastructure.Shared.Services.ImageKit;

public partial class ImageKitUploadService(ILogger<ImageKitUploadService> logger,
                                    ImageKitCredentials credentials) : IImageKitUploadService
{
    private readonly ILogger<ImageKitUploadService> _logger = logger;
    private readonly ImagekitClient _imageKitClient = new(credentials.StandardPublicKey,
                                                            credentials.StandardPrivateKey,
                                                            credentials.UrlEndpoint);

    public async Task<string> UploadFileAsync(IFormFile formFile, string assetFolder)
    {
        try
        {
            if (formFile == null || formFile.Length == 0)
                throw new ArgumentNullException(nameof(formFile));

            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var fileBytes = memoryStream.ToArray();

            // Upload the file to ImageKit.io
            var request = new FileCreateRequest()
            {
                file = fileBytes,
                fileName = Path.GetFileNameWithoutExtension(formFile.FileName),
                folder = assetFolder,
                overwriteFile = true,
                isPrivateFile = false,
                useUniqueFileName = true,
                overwriteCustomMetadata = true
            };

            var uploadResponse = await _imageKitClient.UploadAsync(request);

            if (uploadResponse.HttpStatusCode == (int)System.Net.HttpStatusCode.OK)
            {
                var cdnUrl = uploadResponse.url;
                return cdnUrl;
            }
            else
            {
                ImageKitErrorResponse? errorResponse = null;
                if (!string.IsNullOrEmpty(uploadResponse.Raw))
                    errorResponse = JsonSerializer.Deserialize<ImageKitErrorResponse>(uploadResponse.Raw);

                var logMessage = $"Error uploading file to ImageKit.io. Status code: {uploadResponse.HttpStatusCode}";
                logMessage += errorResponse != null ?
                                $"\r\nDetails: {errorResponse.Message.ToUpperFirstChar()}. {errorResponse.Help.ToUpperFirstChar()}" :
                                string.Empty;

                throw new Exception(logMessage);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file to ImageKit.io");
            throw;
        }
    }

    public async Task DeleteFileAsync(string cdnUrl)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cdnUrl))
            {
                _logger.LogWarning("Provided CDN URL is null or empty.");
                return;
            }

            // Extract the file path from the CDN URL
            var uri = new Uri(cdnUrl);
            var filePath = uri.AbsolutePath.TrimStart('/'); // Removes leading '/'
            var filePathParts = filePath.Split('/', StringSplitOptions.None);
            var strippedParts = filePathParts.Skip(1).Take(filePathParts.Length - 2).ToArray();

            // Fetch file details using ListFilesAsync to get the fileId
            var fileListRequest = new GetFileListRequest
            {
                Path = string.Join("/", strippedParts)
            };

            // Try listing files by the path
            var resultList = _imageKitClient.GetFileListRequest(fileListRequest);
            if (string.IsNullOrEmpty(resultList.Raw))
                return;

            var rootObjects = JsonSerializer.Deserialize<List<Root>>(resultList.Raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when mapping JSON to C# properties
            });

            var targetResource = rootObjects?.FirstOrDefault(p => cdnUrl.Contains(p.filePath));

            if (targetResource == null)
            {
                _logger.LogWarning($"No file found for URL: {cdnUrl}. Path: {filePath}");
                return; // No file to delete
            }

            var fileId = targetResource.fileId;

            // Delete the file using the fileId
            var response = await _imageKitClient.DeleteFileAsync(fileId);

            if (response.HttpStatusCode == StatusCodes.Status200OK)
            {
                _logger.LogInformation($"File deleted successfully. File ID: {fileId}");
            }
            else
            {
                _logger.LogWarning($"Failed to delete file. Status: {response.HttpStatusCode}, Message: {response.Raw}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file from ImageKit.io");
            throw;
        }
    }

}
