using Core.Application.DTOs.General;
using Core.Application.DTOs.Genres;
using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using MediatR;
using System.Net;

namespace Core.Application.Features.GenreModule.Commands.GetAllGenres
{
    public class GetAllGenresCommand : IRequest<GenericApiResponse<string>>
    {
    }
    public class GetAllGenresCommandHandler(GetTmdbData getTmdbData, IGenreRepository genreRepository) : IRequestHandler<GetAllGenresCommand, GenericApiResponse<string>>
    {
        private readonly GetTmdbData _getTmdbData = getTmdbData;
        private readonly IGenreRepository _genreRepository = genreRepository;

        public async Task<GenericApiResponse<string>> Handle(GetAllGenresCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int i = 0;
                var res = _getTmdbData.GetAllGenres();
                foreach (var m in res.Movies ?? Enumerable.Empty<TmdbGenreResponseDto>())
                {
                    var InDb = await _genreRepository.Exist(m.Id);
                    if (!InDb)
                    {
                        i++;
                        await _genreRepository.AddAsync(new Genre
                        {
                            GenreID = m.Id,
                            Name = m.Name,
                            IsMovie = true
                        });
                    }
                }
                foreach (var m in res.Series ?? Enumerable.Empty<TmdbGenreResponseDto>())
                {
                    var InDb = await _genreRepository.Exist(m.Id);
                    if (!InDb)
                    {
                        i++;
                        await _genreRepository.AddAsync(new Genre
                        {
                            GenreID = m.Id,
                            Name = m.Name,
                            IsMovie = false
                        });
                    }
                }
                return new GenericApiResponse<string>
                {
                    Statuscode = 200,
                    Message = $"{i} Genres Added Successfully",
                    Payload = HttpStatusCode.OK.ToString(),
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new GenericApiResponse<string>
                {
                    Statuscode = 500,
                    Message = e.Message,
                    Payload = HttpStatusCode.InternalServerError.ToString(),
                    Success = false
                };
            }
        }
    }
}
