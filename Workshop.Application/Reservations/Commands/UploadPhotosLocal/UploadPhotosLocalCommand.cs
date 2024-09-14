using MediatR;
using Microsoft.AspNetCore.Http;

namespace Workshop.Application.Reservations.Commands.UploadPhotosLocal;

public class UploadPhotosLocalCommand : IRequest<string>
{
    public IFormFile Photo { get; set; }
}