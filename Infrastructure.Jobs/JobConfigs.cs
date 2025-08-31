using Core.Application.Helpers.Logger;
using Infrastructure.Jobs.Scraping;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;

namespace Infrastructure.Jobs
{
    public static class JobConfigs
    {
        public static void ConfigureQuartz(IConfiguration configuration, IServiceCollectionQuartzConfigurator q)
        {
            // 1) Lee primero de ENV, luego de appsettings (clave simple), y por último usa 1140
            var env = Environment.GetEnvironmentVariable("CronJobInMinutes");
            var appSetting = configuration["CronJobInMinutes"]; // O "Quartz:ScrapingIntervalMinutes" si prefieres anidado
            var intervalString = env ?? appSetting ?? "1140";

            int intervalMinutes = 1140;
            if (!int.TryParse(intervalString, out intervalMinutes) || intervalMinutes < 1)
            {
                intervalMinutes = 1140; // fallback
                LoggerHelper.CustomLog(CustomLogLevel.Scraping,
                    "Valor inválido para 'CronJobInMinutes'. Usando 1140 minutos por defecto.",
                    LogLevels.Error);
            }

            var jobKey = new JobKey("ScrapingJob");

            q.AddJob<ScrapingJob>(opts => opts
                .WithIdentity(jobKey)
                .StoreDurably()); // opcional pero útil si reusas el Job

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"ScrapingJob-trigger-{intervalMinutes}m")
                .StartNow() // ejecuta inmediatamente al iniciar el scheduler
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMinutes(1))
                    .RepeatForever()
                    .WithMisfireHandlingInstructionIgnoreMisfires())); // evita saltos si el proceso se pausa
        }
    }
}
