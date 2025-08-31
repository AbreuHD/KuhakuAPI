using Core.Application.Features.Scraping.PelisPlusLat.Commands.GetPelisPlusLatMovies;
using Core.Application.Interface.Scraping;
using MediatR;
using Microsoft.AspNetCore.Http;
using Quartz;

namespace Infrastructure.Jobs.Scraping
{
    [DisallowConcurrentExecution]
    public class ScrapingJob(IScrapingService scrapingService) : IJob
    {
        private readonly IScrapingService _scrapingService = scrapingService;

        public async Task Execute(IJobExecutionContext context)
        {
            await _scrapingService.RunScrapingAsync();
        }
    }
}
