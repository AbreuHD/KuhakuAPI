using Core.Application.Features.Scraping.Cuevana.Cuevana3.ch.Commands.GetAllCuevanaMovies;
using Core.Application.Helpers.Logger;
using Core.Application.Interface.Repositories;
using Core.Application.Interface.Scraping;
using MediatR;

namespace Core.Application.Services.WebScrapers.Common
{
    public class ScrapingService : IScrapingService
    {
        private readonly IScrapPageRepository _pageRepository;
        private readonly IMediator _mediator;

        public ScrapingService(IScrapPageRepository pageRepository, IMediator mediator)
        {
            _pageRepository = pageRepository;
            _mediator = mediator;
        }

        public async Task RunScrapingAsync()
        {
            var page = await _pageRepository.GetByIdAsync(1);

            if (page.IsOn || page.Disabled)
            {
                LoggerHelper.CustomLog(CustomLogLevel.Scraping,
                    $"Trying to scrap {page.Name} but status IsOn={page.IsOn} Disabled={page.Disabled}",
                    LogLevels.Information);
                return;
            }

            LoggerHelper.CustomLog(CustomLogLevel.Scraping, $"Starting Scraping for {page.Name}", LogLevels.Information);

            var start = DateTime.Now;
            await _pageRepository.UpdateAsync(new Domain.Entities.WebScraping.ScrapPage
            {
                ID = page.ID,
                Name = page.Name,
                Img = page.Img,
                Info = page.Info,
                Url = page.Url,
                LastScrapStart = start,
                LastScrapEnd = page.LastScrapEnd,
                IsOn = true,
                Disabled = page.Disabled,
                MovieWeb = page.MovieWeb
            }, page.ID);

            try
            {
                await _mediator.Send(new GetAllCuevanaMoviesCommand());
            }
            catch (Exception ex)
            {
                LoggerHelper.CustomLog(CustomLogLevel.Scraping,
                    $"Error scraping page {page.Name}: {ex.Message}", LogLevels.Error);
                await _pageRepository.UpdateAsync(new Domain.Entities.WebScraping.ScrapPage
                {
                    ID = page.ID,
                    Name = page.Name,
                    Img = page.Img,
                    Info = page.Info,
                    Url = page.Url,
                    LastScrapStart = start,
                    LastScrapEnd = DateTime.Now,
                    IsOn = true,
                    Disabled = page.Disabled,
                    MovieWeb = page.MovieWeb
                }, page.ID);
            }
        }
    }
}
