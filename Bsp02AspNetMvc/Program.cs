using Bsp02.Models;
using Bsp02.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Bsp02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            //EINTRAGEN - FluentValidation AutoValidation und ClientsideAdapters registrieren
            builder.Services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            // EINTRAGEN - Manuelle Registrierung eines einzelnen Validators
            builder.Services.AddScoped<IValidator<Benutzer>, BenutzerValidator>();



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