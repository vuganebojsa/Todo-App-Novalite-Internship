
using Azure.Storage;
using Azure.Storage.Sas;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Api.Services
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _configuration;
        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateSaSToken(SasPermission permission, string fileName)
        {
            string containerName = _configuration.GetValue<string>("AzureBlobContainerName");

            BlobSasBuilder sasBuilder = GetSasBuilder(fileName, containerName);
            if (permission == SasPermission.Create) sasBuilder.SetPermissions(BlobSasPermissions.Create);
            else sasBuilder.SetPermissions(BlobSasPermissions.Read);

            BlobSasQueryParameters sasToken = GetSasToken(sasBuilder);
            return sasToken.ToString();
        }

        private static BlobSasBuilder GetSasBuilder(string fileName, string containerName)
        {
            return new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = fileName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.Now.AddSeconds(30),

            };
        }

        private BlobSasQueryParameters GetSasToken(BlobSasBuilder sasBuilder)
        {
            return sasBuilder.ToSasQueryParameters(
                             new StorageSharedKeyCredential(_configuration.GetValue<string>("AzureBlobAccountName"), Environment.GetEnvironmentVariable("AZUREBLOBNOVALITEKEY")));
        }
    }
}
