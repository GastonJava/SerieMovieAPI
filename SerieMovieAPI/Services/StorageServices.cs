using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SerieMovieAPI.Services
{
    public class StorageServices : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public StorageServices(
            BlobServiceClient blobServiceClient,
            IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public void Subir(IFormFile formFile)
        {
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(formFile.FileName);

            using var stream = formFile.OpenReadStream();
            blobClient.Upload(stream, true);
        }
    }
}

