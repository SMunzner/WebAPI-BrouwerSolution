using BrouwerWebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrouwerWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //configure
            builder.Services.AddDbContext<BierlandContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("bierland")));
            builder.Services.AddScoped<IBrouwerRepository, BrouwerRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
