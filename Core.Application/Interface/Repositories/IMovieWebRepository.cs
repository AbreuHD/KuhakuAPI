using Core.Application.DTOs.Scraping;
using Core.Domain.Entities.WebScraping;

namespace Core.Application.Interface.Repositories
{
    public interface IMovieWebRepository : IGenericRepository<MovieWeb>
    {
        Task<List<MovieWebDTO>> Exist(List<MovieWebDTO> movie);
    }
}
