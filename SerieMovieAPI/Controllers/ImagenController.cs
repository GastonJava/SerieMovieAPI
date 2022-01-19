using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;

namespace SerieMovieAPI.Controllers
{
    public class ImagenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Traer la imagen desde su directorio
        /*
        [HttpGet("directorio")]
        public IActionResult directorioImagen() 
        {

            string nombrecarpeta = "/StaticFiles/images/Character/";
            string lista = "";
            var paths = Directory.GetFiles(Path.Combine(env.ContentRootPath, "StaticFiles"));
            foreach (var path in paths) 
            {

                Byte[] b = System.IO.File.ReadAllBytes(@"E:\\Test.jpg");          
                return File(b, "image/jpeg");
            }

            
            return Ok(lista);
        }
        */

        [HttpGet("obtenerimage")]
        //convertir byte a imagen
        public byte[] ObtenerImage(string base64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(base64String))
            {
                bytes = Convert.FromBase64String(base64String);

                MemoryStream ms = new MemoryStream();
                Image imagen = Image.FromStream(ms);
            }

            return bytes;
        }
    }
}
