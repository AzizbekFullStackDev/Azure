using Azure.Service.DTOs.Asset;

namespace Azure.Service.Interfaces
{
    public interface IFileUploadService
    {
        public Task<AssetForResultDto> FileUploadAsync(AssetForCreationDto dto);
        public Task<bool> DeleteFileAsync(string filePath);
    }
}
