namespace Core.Application.DTOs.Genres
{
    public class TmdbGenreResponseListDto
    {
        public List<TmdbGenreResponseDto>? Movies { get; set; }
        public List<TmdbGenreResponseDto>? Series { get; set; }
    }
}
