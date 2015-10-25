using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1.Models
{
    public class _80sMovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public float Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Seen { get; set; }
    }
}