﻿namespace Core.Application.DTOs.TMDB
{
    public class TMDBResult
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; }
        public bool adult { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public string overview { get; set; }
        public string video { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        public string release_date { get; set; }
    }
}
