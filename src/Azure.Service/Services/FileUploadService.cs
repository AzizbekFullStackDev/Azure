using Azure.Service.DTOs.Asset;
using Azure.Service.Helpers;
using Azure.Service.Interfaces;

namespace Azure.Service.Services
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<bool> DeleteFileAsync(string filePath)
        {
            var result = Path.Combine(WebEnvironmentHost.WebRootPath, filePath);
            if(File.Exists(result))
            {
                File.Delete(result);
                return true;
            }
            return false;
        }

        public async Task<AssetForResultDto> FileUploadAsync(AssetForCreationDto dto)
        {
            var wwwRootPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets", dto.FolderPath);
            var assetsFolderPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets");
            var assetPath = Path.Combine(assetsFolderPath, dto.FolderPath);

            if (!Directory.Exists(assetsFolderPath))
            {
                Directory.CreateDirectory(assetsFolderPath);
            }
            if (!Directory.Exists(assetPath))
            {
                Directory.CreateDirectory(assetPath);
            }

            var FileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.FormFile.FileName);
            var FullPath = Path.Combine(wwwRootPath, FileName);
            using (var streamFile = File.OpenWrite(FullPath))
            {
                await dto.FormFile.CopyToAsync(streamFile);
            };

            var result = new AssetForResultDto()
            {
                AssetPath = Path.Combine("Assets", dto.FolderPath, FileName),
            };

            return result;

        }
    }
}
