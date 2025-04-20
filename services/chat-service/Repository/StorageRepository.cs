using chat_service.DTO;
using chat_service.Storage;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace chat_service.Repository
{
    public class StorageRepository : ControllerBase, IStorageRepository
    {
        protected readonly string BASE_URL = "https://storage.googleapis.com/";
        protected readonly string BUCKET = "socialstream_chat";

        private readonly IChatStorage _chatStorage;

        public StorageRepository(IChatStorage chatStorage)
        {
            _chatStorage = chatStorage;
        }

        public string Url { get; set; } = string.Empty;


        /// <summary>
        /// Send request towards cloud
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="selectedBucket"></param>
        /// <returns>Returns the selected file as IActionResult</returns>
        public async Task<IActionResult> GetFile(string fileName)
        {
            var client = await StorageClient.CreateAsync();
            var stream = new MemoryStream(); //Stream will be updated on download just need an empty one to store the data
            var obj = await client.DownloadObjectAsync(BUCKET, fileName, stream);
            stream.Position = 0;

            return File(stream, obj.ContentType, obj.Name);
        }

        public async Task<byte[]> GetFileAsByte(string fileName)
        {
            var client = await StorageClient.CreateAsync();
            using (var stream = new MemoryStream())
            {
                await client.DownloadObjectAsync(BUCKET, fileName, stream);
                stream.Position = 0;

                var fileByte = stream.ToArray();
                return fileByte;
            }
        }

        public async Task<string> AddFile(FileUploadDto fileUpload)
        {
            var client = await StorageClient.CreateAsync();
            Google.Apis.Storage.v1.Data.Object obj;
            using (Stream stream = fileUpload.File.OpenReadStream())
            {
                obj = await client.UploadObjectAsync(BUCKET, Guid.NewGuid().ToString(), fileUpload.Type, stream);
            }
            return obj.Name;
        }


        public async Task<IActionResult> GetVideoChunk(string fileName, long rangeStart, long rangeEnd)
        {
            var file = await GetFileAsByte(fileName);

            if (file.Length == 0)
                return NotFound();

            var contentLength = rangeEnd - rangeStart + 1;
            var buffer = new byte[contentLength];

            using (var memoryStream = new MemoryStream(file))
            {
                memoryStream.Seek(rangeStart, SeekOrigin.Begin);
                await memoryStream.ReadAsync(buffer, 0, buffer.Length);
            }

            Response.StatusCode = 206; // Partial Content
            Response.Headers["Content-Range"] = $"bytes={rangeStart}-{rangeEnd}/{file.Length}";

            return File(buffer, "video/mp4", enableRangeProcessing: true);
        }

        public async Task<byte[]> GetVideoChunkBytes(string fileName, long rangeStart, long rangeEnd)
        {
            var file = await GetFileAsByte(fileName);

            if (file.Length == 0)
                return null;

            //using (var memoryStream = new MemoryStream(file))
            //{
            //    var contentLength = rangeEnd - rangeStart + 1;
            //    var buffer = new byte[contentLength];

            //    memoryStream.Seek(rangeStart, SeekOrigin.Begin);
            //    await memoryStream.ReadAsync(buffer, 0, buffer.Length);

            //    var response = new FileContentResult(buffer, "video/mp4")
            //    {
            //        FileDownloadName = fileName,
            //        EnableRangeProcessing = true
            //    };

            //    Response.Headers["Content-Range"] = $"bytes {rangeStart}-{rangeEnd}/{file.Length}";
            //    Response.Headers["Accept-Ranges"] = "bytes";
            //    Response.Headers["Content-Length"] = buffer.Length.ToString();

            //    return File(buffer, "video/mp4", true);
            //}


            var contentLength = rangeEnd - rangeStart + 1;
            var buffer = new byte[contentLength];

            using (var memoryStream = new MemoryStream(file))
            {
                memoryStream.Seek(rangeStart, SeekOrigin.Begin);
                await memoryStream.ReadAsync(buffer, 0, buffer.Length);
            }


            return buffer;
        }


        [HttpGet("video/{fileName}")]
        public async Task<IActionResult> GetVideo(string fileName)
        {
            return null;
            if (Request == null)
            {
                var file = _chatStorage.GetValue(fileName); //Return file from cache if available
                if (file == null)
                {
                    file = await GetFileAsByte(fileName); //Get file from cloud and store in cache
                    _chatStorage.Create(fileName, file);
                }
                return File(file, "video/mp4", enableRangeProcessing: true);
            }
            if (Request.Headers.ContainsKey("Range")) //In case if it's range request
            {
                var rangeHeader = Request.Headers["Range"].ToString();
                var range = rangeHeader.Split(new[] { '=', '-' });
                var rangeStart = long.Parse(range[1]);
                var rangeEnd = range.Length > 2 && long.TryParse(range[2], out long end) ? end : rangeStart + 1_000_000 - 1;

                return await GetVideoChunk(fileName, rangeStart, rangeEnd);
            }
            return NotFound();
        }
    }
}
