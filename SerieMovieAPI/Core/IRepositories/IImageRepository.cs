using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace SerieMovieAPI.Core.IRepositories
{
    public interface IImageRepository
    {
        public Image byteToImageFromMemory(byte[] bytearray);

        public byte[] imageToBytearrayFromMemory(IFormFile file);

        public bool byteArrayToDirectoryImage(IFormFile file);
    }
}
