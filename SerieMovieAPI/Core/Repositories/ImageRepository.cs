using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SerieMovieAPI.Core.IRepositories;
using System;
using System.Drawing;
using System.IO;

namespace SerieMovieAPI.Core.Repositories
{
    public class ImageRepository : IImageRepository
    {

        private readonly IWebHostEnvironment env;

        public ImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            env = webHostEnvironment;
        }

        public bool byteArrayToDirectoryImage(IFormFile file)
        {
            bool resultado = false;

            string filename = file.FileName;

            string nombrecarpeta = "/StaticFiles/images/character/";

            if (string.IsNullOrWhiteSpace(env.ContentRootPath))
            {
                env.ContentRootPath = Path.Combine(Directory.GetCurrentDirectory(), nombrecarpeta);
            }

            string rutaraiz = env.ContentRootPath;

            string rutacompleta = rutaraiz + nombrecarpeta;

            if (!Directory.Exists(rutacompleta))
            {
                Directory.CreateDirectory(rutacompleta);
            }

            if (filename.Length > 0)
            {
                string NombreArchivo = file.FileName + Guid.NewGuid().ToString();

                string RutaFullCompleta = Path.Combine(rutacompleta, NombreArchivo);


                using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                {
                    file.CopyTo(stream);
                    resultado = true;
                }

            }

            return resultado;
        }

        public Image byteToImageFromMemory(byte[] bytearray)
        {
            MemoryStream ms = new MemoryStream(bytearray);
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            Image returnImage = Image.FromStream(ms);
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
            return returnImage;
        }

        public byte[] imageToBytearrayFromMemory(IFormFile file)
        {
            MemoryStream ms = new MemoryStream();

            file.CopyTo(ms);
            var filebytes = ms.ToArray();
            return filebytes;
        }
    }
}
