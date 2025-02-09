using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs.TMDB
{
    public class TmdbResult
    {
        public int ID { get; set; }

        [JsonProperty("title")]
        public required string Title { get; set; }

        [JsonProperty("original_title")]
        public string? OriginalTitle { get; set; }

        [JsonProperty("genre_ids")]
        public List<int>? GenreIds { get; set; }

        [JsonProperty("adult")]
        public bool? Adult { get; set; }

        [JsonProperty("vote_average")]
        public double? VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public int? VoteCount { get; set; }

        [JsonProperty("overview")]
        public string? Overview { get; set; }

        [JsonProperty("video")]
        public string? Video { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("backdrop_path")]
        public string? BackdropPath { get; set; }

        [JsonProperty("release_date")]
        public string? ReleaseDate { get; set; }
    }
}
