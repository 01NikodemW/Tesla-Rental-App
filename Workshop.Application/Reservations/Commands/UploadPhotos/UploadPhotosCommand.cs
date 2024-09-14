using MediatR;
using Microsoft.AspNetCore.Http;

namespace Workshop.Application.Reservations.Commands.UploadPhotos;

public class UploadPhotosCommand : IRequest<string>
{
    public IFormFile Photo { get; set; }
}