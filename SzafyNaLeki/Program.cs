using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SzafyNaLeki.Entities;
using SzafyNaLeki.Services;
using NLog;
using NLog.Web;
using SzafyNaLeki.Middleware;
using Hangfire;
using Microsoft.AspNetCore.Hosting.Server;
using SzafyNaLeki.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using SzafyNaLeki.Controllers;

namespace SzafyNaLeki
{
    public class Program
    {
        public static IServiceProvider _serviceProvider;
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Dodaj us³ugi do kontenera.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SzafaDbContext>();
            builder.Services.AddScoped<SeederSzaf>();
            builder.Services.AddScoped<AlarmBasic>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<ISzafaService, SzafaService>();
            builder.Services.AddScoped<IAlarmService, AlarmService>();
            builder.Services.AddScoped<ErrorHoldingMiddleware>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", builder =>
                {
                    builder.WithOrigins("http://localhost:4200") 
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            builder.Services.AddHangfire(configuration => configuration
            .UseSqlServerStorage("Server=DESKTOP-E1AFSPB;Database=SzafaDb;TrustServerCertificate=True;Integrated Security=True")); 

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SzafyNaLeki API V1");
                });
            }
            app.UseMiddleware<ErrorHoldingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseCors("AllowAngularDev");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<SeederSzaf>();
                    dbContext.Seed();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the database.");
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<AlarmBasic>();
                    dbContext.Default();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the database.");
                }
            }

            app.UseCors("AllowLocalhost4200");

            app.MapControllers();
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=DESKTOP-E1AFSPB;Database=SzafaDb;TrustServerCertificate=True;Integrated Security=True");
            RecurringJob.AddOrUpdate("TriggerJob", () => ExecuteGetAll(), Cron.Hourly);



            app.Run();
        }
        public static void ExecuteGetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var szafaService = scope.ServiceProvider.GetRequiredService<ISzafaService>();
                var controller = scope.ServiceProvider.GetRequiredService<SzafaController>();

                try
                {
                    var result = szafaService.GetAll();
                    controller.Ok(result);
                }
                catch (Exception ex)
                {
                    controller.StatusCode(500, $"Wyst¹pi³ b³¹d: {ex.Message}");
                }
            }
        }
    }
}
