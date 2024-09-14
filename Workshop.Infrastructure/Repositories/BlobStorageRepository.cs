using Azure.Storage.Blobs;
using Workshop.Domain.Repositories;

namespace Workshop.Infrastructure.Repositories;

public class BlobStorageRepository(BlobServiceClient blobServiceClient)
    : IBlobStorageRepository
{
    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient("example");
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, true);
        return blobClient.Uri.ToString();
    }
}