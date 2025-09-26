using Core.Application.DTOs.Movies;

namespace Core.Application.DTOs.ShareList
{
    public class PreviewShareListDto
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public int? ProfileId { get; set; }
        public string Username { get; set; }
        public List<PreviewSearchMovieDto> Movies { get; set; }
    }
}
