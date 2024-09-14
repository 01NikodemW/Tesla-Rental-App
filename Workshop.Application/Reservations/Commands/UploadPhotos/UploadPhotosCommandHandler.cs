using MediatR;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Reservations.Commands.UploadPhotos;

public class UploadPhotosCommandHandler(IBlobStorageRepository blobStorageRepository)
    : IRequestHandler<UploadPhotosCommand, string>
{
    public async Task<string> Handle(UploadPhotosCommand request, CancellationToken cancellationToken)
    {
        await using var stream = request.Photo.OpenReadStream();
        var fileName = Path.GetFileName(request.Photo.FileName);
        var uri = await blobStorageRepository.UploadFileAsync(stream, fileName);

        return uri;
    }
}