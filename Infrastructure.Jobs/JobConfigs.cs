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
            var env = Environment.GetEnvironmentVariable("CronJobInMinutes");
            var appSetting = configuration["CronJobInMinutes"];
            var intervalString = env ?? appSetting ?? "1140";

            int intervalMinutes = 1140;
            if (!int.TryParse(intervalString, out intervalMinutes) || intervalMinutes < 1)
            {
                intervalMinutes = 1140;
                LoggerHelper.CustomLog(CustomLogLevel.Scraping,
                    "Invalid value for ‘CronJobInMinutes’. Using 1140 minutes by default.",
                    LogLevels.Error);
            }

            var jobKey = new JobKey("ScrapingJob");

            q.AddJob<ScrapingJob>(opts => opts
                .WithIdentity(jobKey)
                .StoreDurably());

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"ScrapingJob-trigger-{intervalMinutes}m")
                .StartNow() 
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMinutes(intervalMinutes))
                    .RepeatForever()
                    .WithMisfireHandlingInstructionIgnoreMisfires()));
        }
    }
}
