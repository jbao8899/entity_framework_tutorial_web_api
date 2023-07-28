// Go to package management console -> dotnet watch run -> run web app, update whenever
// changes are made to code
// https://cognizant.udemy.com/course/aspnet-web-api-2-hands-on/learn/lecture/21142456#overview
// See section on installing Entity Framework
// Add connection string to appsettings.json
// dotnet ef migrations add InitialCreate --context DataContext -> create database
// dotnet ef database update --context DataContext -> had to add Encrypt=False; to launchSettings.json for it to work

using Microsoft.EntityFrameworkCore;

namespace entity_framework_tutorial_web_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Added
            builder.Services.AddDbContext<DbContext>(x => x.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}