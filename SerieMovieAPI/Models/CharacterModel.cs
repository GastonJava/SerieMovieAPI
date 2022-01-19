using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieMovieAPI.Models
{
    public class CharacterModel
    {
        [Key]
        public int CharacterId {  get; set; }

        /*
        [Display(Name = "Imagen")]
        [Required]
        [NotMapped]
        public IFormFile Imagen_Character { get; set; }
        */

        [Display(Name = "Imagen")]
        /* [NotMapped] */
       
        [System.Text.Json.Serialization.JsonIgnore]

        [SwaggerSchema(ReadOnly = false)]
        public byte[] Image_Characterbyte { get; set; }
        
        public string Name_Character { get; set; }

        public int Age_Character { get; set;}

        public Double Weight_Character { get; set; }

        public String History_Character { get; set; }

       /* public int MovieserieId { get; set; }
        public virtual MovieserieModel Movie { get; set; } */

        public string titulo_movie { get; set; }
        public virtual ICollection<MovieserieModel> Movieseries { get; set; } 

        /* [NotMapped] */
        /*public IFormFile file { get; set; } */

        /* cambio 
         public MovieserieModel Movieserie {  get; set; } 
        /* cambio */

        /* public virtual IEnumerable<CharacterMovieserie> CharacterMovieseries {  get; set; } */

        /* public IEnumerable<ImageModel> Images {  get; set; } */  
    }
}
