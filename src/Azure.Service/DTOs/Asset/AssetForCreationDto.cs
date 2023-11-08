using Microsoft.AspNetCore.Http;

namespace Azure.Service.DTOs.Asset
{
    public class AssetForCreationDto
    {
        public string FolderPath { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
