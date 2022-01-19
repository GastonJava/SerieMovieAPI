using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SerieMovieAPI.Models.DTOs.Movieseries
{
    public class MovieseriesDTO
    {
        internal int MovieserieId { get; set; }

        public string Title_Movserie { get; set; }

        internal byte[] Image_Movserie { get; set; }

        public IFormFile moviefile { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date_Movserie { get; set; }

        public int Rating_Movserie { get; set; }
    }
}
