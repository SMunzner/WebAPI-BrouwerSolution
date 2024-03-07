
using BrouwerService.Repositories;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
