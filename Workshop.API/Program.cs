using Workshop.API.Extensions;
using Workshop.API.Middlewares;
using Workshop.Application.Extensions;
using Workshop.Infrastructure.Extensions;
using Workshop.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IWorkshopSeeder>();

await seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapGroup("api/identity")
//     .WithTags("Identity")
//     .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();