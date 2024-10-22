using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers();
            
            var app = builder.Build();

            app.MapControllers();

            app.MapGet("/", () => Results.Redirect("api/Book"));
            
            app.Run();
        }
    }
}

