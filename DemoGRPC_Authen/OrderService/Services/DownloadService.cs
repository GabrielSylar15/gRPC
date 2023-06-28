using Azure.Core;
using Download;
using Google.Protobuf;
using Grpc.Core;
using System.IO;

namespace OrderService.Services
{
    public class DownloadService : Downloader.DownloaderBase
    {
        private readonly ILogger _logger;
        private const int ChunkSize = 1024 * 32;

        public DownloadService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DownloadService>();
        }

        public override async Task Download(DownloadFileRequest request, IServerStreamWriter<DownloadFileResponse> responseStream, ServerCallContext context)
        {
            var requestParam = request.Id;
            var filename = requestParam + ".png";

            await responseStream.WriteAsync(new DownloadFileResponse
            {
                MetaData = new FileMetadata { FileName = filename }
            });

            var buffer = new byte[ChunkSize];
            await using var fileStream = File.OpenRead("C:\\Users\\ADMIN\\Desktop\\note\\store\\"+filename);

            while (true)
            {
                var numBytesRead = await fileStream.ReadAsync(buffer);
                if (numBytesRead == 0)
                {
                    break;
                }

                _logger.LogInformation("Sending data chunk of {numBytesRead} bytes", numBytesRead);
                await responseStream.WriteAsync(new DownloadFileResponse
                {
                    Data = UnsafeByteOperations.UnsafeWrap(buffer.AsMemory(0, numBytesRead))
                });
            }
        }
    }
}

