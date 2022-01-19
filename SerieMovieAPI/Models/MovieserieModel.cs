using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieMovieAPI.Models
{
    public class MovieserieModel // DEPENDIENTE  de genero
    {
        [Key]
        public int MovieserieId {  get; set; }

        public string Title_Movserie {  get; set; }

        public byte[] Image_Movserie { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date_Movserie {  get; set; }

        public int Rating_Movserie { get; set; }

        /* public IEnumerable<CharacterModel> Characters { get; set; } */

        /* public int CharacterId { get; set; } ------>
         * no es necesario declarar foranea,
         * automaticamente lo relaciona al ID DE tabla CHARACTER */

        /* public int CharacterId { get; set; }
        public virtual CharacterModel Character {  get; set; } */


        /*[ForeignKey("characterId")]
        public int characterId {  get; set;} */
        /* public virtual CharacterModel Character { get; set; } */


        public virtual ICollection<CharacterModel> Characters {  get; set; } 
       /* public virtual IEnumerable<CharacterMovieserie> CharacterMovieseries { get; set; } */

        /*public GenreModel GenreModel { get; set; }*/ /* pelicula puede tener 1 solo Genero */

        /* public int ImageFK { get; set; } */
        /* public ImageModel Image {  get; set; } */
    }
}
