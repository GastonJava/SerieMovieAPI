using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieMovieAPI.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }
        public string Name_Image {  get; set; }
        public string Title_Image {  get; set; }

        [NotMapped]
        public string Url_Image {  get; set; }

        public byte[] Data_Image {  get; set;}

        public MovieserieModel Movieserie { get; set; }

        public CharacterModel Character {  get; set; }

        public GenreModel Genre {  get; set; }

    }
}
