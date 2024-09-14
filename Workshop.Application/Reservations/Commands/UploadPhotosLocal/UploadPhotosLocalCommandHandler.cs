using MediatR;

namespace Workshop.Application.Reservations.Commands.UploadPhotosLocal;

public class UploadPhotosCommandLocalHandler : IRequestHandler<UploadPhotosLocalCommand, string>
{
    public Task<string> Handle(UploadPhotosLocalCommand request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.Photo.FileName);
        var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}{extension}";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", fileName);

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        using var stream = new FileStream(path, FileMode.Create);
        request.Photo.CopyTo(stream);

        return Task.FromResult(path);
    }
}
