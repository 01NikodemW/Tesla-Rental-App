using Workshop.API.Extensions;
using Workshop.API.Middlewares;
using Workshop.Application.Extensions;
using Workshop.Infrastructure.Extensions;
using Workshop.Infrastructure.Seeders;
using Serilog;
using Workshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WorkshopDbContext>();
    context.Database.Migrate();

    var seeder = scope.ServiceProvider.GetRequiredService<IWorkshopSeeder>();

    await seeder.Seed();
}




app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["Frontend:Url"]));

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}