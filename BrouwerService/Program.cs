
using BrouwerService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BrouwerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();



            //Hiermee kan je een BierlandContext object injecteren in BrouwerRepository
            builder.Services.AddDbContext<BierlandContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("bierland")));

            //Hiermee kan je straks een BrouwerRepository object,
            //onder de gedaante van IBrouwerRepository, injecteren in je controller
            builder.Services.AddScoped<IBrouwerRepository, BrouwerRepository>();




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.EnableAnnotations());     //SwashBuckle --> Annotations for doc

            //configure 
            builder.Services.AddScoped<IFiliaalRepository, FiliaalRepository>();


            //Cross origin resource sharing --> CORS
            builder.Services.AddCors();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c => c.PreSerializeFilters.Add((swagger, request) => 
                swagger.Servers = new List<OpenApiServer>
                { new OpenApiServer { Url = $"{request.Scheme}://{request.Host.Value}" }
                }));

                app.UseSwaggerUI();
                //CORS
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
