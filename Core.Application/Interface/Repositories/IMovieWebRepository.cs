using Core.Application.DTOs.Scraping;
using Core.Domain.Entities.WebScraping;

namespace Core.Application.Interface.Repositories
{
    public interface IMovieWebRepository : IGenericRepository<MovieWeb>
    {
        Task<List<MovieWebDto>> Exist(List<MovieWebDto> movie);
    }
}
