using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SerieMovieAPI.Miscelanious.Swagger_FIlters.Scheme_Filter;
using SerieMovieAPI.Models.DTOs.Movieseries;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SerieMovieAPI.Models.DTOs.Characters
{
    public class CharactersDTO
    {
        //[JsonIgnore]
        internal int CharacterId { get; set; }

        [Display(Name = "Imagen")]
        //[ApiModelProperty(Hidden = true)]
        [JsonIgnore]
        //[SwaggerIgnore]
        //[SwaggerSchema(ReadOnly = true)]
        internal byte[] Image_Characterbyte { get; set; }
        
        public virtual IFormFile File { get; set; }

        public string Name_Character { get; set; }

        public int Age_Character { get; set; }

        public Double Weight_Character { get; set; }

        public string History_Character { get; set; }

        public string Title_movie { get; set; }

        [JsonIgnore]
        internal ICollection<MovieseriesDTO> Movieseries { get; set; } = new List<MovieseriesDTO>(); 

        /*
        public virtual string title_movie { get; set; }

        public DateTime date_movie { get; set; }

        public int rating_movie { get; set; }
        */


    }

   
}
