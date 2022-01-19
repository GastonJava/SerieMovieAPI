using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieMovieAPI.Models
{
    public class GenreModel // entidad principal 
    {
        [Key]
        public int GenreId { get; set; }

        public string Name_Genre { get; set; }

        public byte[] Image_Genre { get; set;} 

        public virtual MovieserieModel Movieserie {  get; set; } /* ESTE ES EL ULTIMO CAMBIO ------------------------------------*/

        /* public IEnumerable<MovieserieModel> Movieseries { get; set;} */

        //ImagenId es la clave foranea
        /* [ForeignKey("ImageId")] */
        /*public int ImageFK { get; set; } */
        /* public ImageModel Image {  get; set; } */
    }
}
