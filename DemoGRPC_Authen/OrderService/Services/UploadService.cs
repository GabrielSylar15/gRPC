using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Upload;

namespace OrderService.Services
{
    public class UploadService : Uploader.UploaderBase
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public UploadService(ILoggerFactory loggerFactory, IConfiguration config)
        {
            _logger = loggerFactory.CreateLogger<UploadService>();
            _config = config;
        }

        public override async Task<UploadFileResponse> UploadFile(IAsyncStreamReader<UploadFileRequest> requestStream, ServerCallContext context)
        {

            var uploadPath = "C:\\Users\\ADMIN\\Desktop\\note\\store\\1bffx3ta.bu0";

            Directory.CreateDirectory("C:\\Users\\ADMIN\\Desktop\\note\\store\\1bffx3ta.bu0");

            await using var writeStream = File.Create(Path.Combine(uploadPath, "data.bin"));

            await foreach (var message in requestStream.ReadAllAsync())
            {
                if (message.MetaData != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(uploadPath, "metadata.json"), message.MetaData.ToString());
                }
                if (message.Data != null)
                {
                    await writeStream.WriteAsync(message.Data.Memory);
                }
            }

            return new UploadFileResponse { Id = "Succ" };
        }
    }
}
