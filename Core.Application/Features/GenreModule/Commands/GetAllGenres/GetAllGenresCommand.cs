using Core.Application.DTOs.General;
using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.GenreModule.Commands.GetAllGenres
{
    public class GetAllGenresCommand : IRequest<GenericApiResponse<string>>
    {
    }
    public class GetAllGenresCommandHandler : IRequestHandler<GetAllGenresCommand, GenericApiResponse<string>>
    {
        private readonly GetTMDBData _getTMDBData;
        private readonly IGenreRepository _genreRepository;

        public GetAllGenresCommandHandler(GetTMDBData getTMDBData, IGenreRepository genreRepository)
        {
            _getTMDBData = getTMDBData;
            _genreRepository = genreRepository;
        }

        public async Task<GenericApiResponse<string>> Handle(GetAllGenresCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int i = 0;
                var res = await _getTMDBData.GetAllGenres();
                foreach (var m in res.Movies)
                {
                    var InDb = await _genreRepository.Exist(m.id);
                    if (!InDb)
                    {
                        i++;
                        await _genreRepository.AddAsync(new Genre
                        {
                            GenreID = m.id,
                            Name = m.name,
                            IsMovie = true
                        });
                    }
                }
                foreach (var m in res.Series)
                {
                    var InDb = await _genreRepository.Exist(m.id);
                    if (!InDb)
                    {
                        i++;
                        await _genreRepository.AddAsync(new Genre
                        {
                            GenreID = m.id,
                            Name = m.name,
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
