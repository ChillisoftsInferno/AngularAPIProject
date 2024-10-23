using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
            
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCors", builder =>
            {
                builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
            
        var app = builder.Build();

        app.UseCors("MyCors");

        app.MapControllers();

        app.MapGet("/", () => Results.Redirect("api/Book"));
            
        app.Run();
    }
}