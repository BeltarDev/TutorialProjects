using Microsoft.EntityFrameworkCore;
using TimeRegistration.UI.TimeRegistration;

namespace TimeRegistration.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // configure custom / User feature
            builder.ConfigureTimeRegistrationFeature();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "api/{controller}/{action=Index}/{id?}");


            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
