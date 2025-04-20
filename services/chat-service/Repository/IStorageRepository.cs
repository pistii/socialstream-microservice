using chat_service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace chat_service.Repository
{
    public interface IStorageRepository
    {
        public Task<IActionResult> GetFile(string fileName);
        public Task<string> AddFile([FromForm] FileUploadDto fileUpload);
        Task<byte[]> GetFileAsByte(string fileName);
        Task<IActionResult> GetVideoChunk(string fileName, long rangeStart, long rangeEnd);
        Task<IActionResult> GetVideo(string fileName);
        Task<byte[]> GetVideoChunkBytes(string fileName, long rangeStart, long rangeEnd);
    }
}
