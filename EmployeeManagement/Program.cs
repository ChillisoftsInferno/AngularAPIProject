using EmployeeManagement.Data;
using EmployeeManagement.Interfaces.Employee;
using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<AppDbContext>
        (
            options => options.UseInMemoryDatabase("EmployeeDb")
        );
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCors", corsBuilder =>
            {
                corsBuilder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        //Dependency Injections
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        
        //Controllers
        builder.Services.AddControllers();

        //Swagger Integration
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        //Build Project
        var app = builder.Build();

        //Configure Environments
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                }
            );
        }

        app.UseCors("MyCors");

        app.MapControllers();

        app.Run();   
    }
}