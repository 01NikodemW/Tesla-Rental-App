namespace Workshop.Domain.Repositories;

public interface IBlobStorageRepository
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);
}