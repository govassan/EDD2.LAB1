using EDD2.LAB1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Web;

namespace EDD2.LAB1.Controllers
{
    public class HomeController : Controller

    {
        public static List<Persona> personas = new List<Persona>();//lista global desde archivo
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("IngresarPersonas")]
        public IActionResult IngresarPersonas()
        {
            return View();
        }
        [HttpPost("IngresarPersonas")]
        public IActionResult IngresarPersonas(IFormFile file)
        {
            if (file == null)
            {
                try
                {
                    //subir archivo a una carpeta temporal
                    string ruta = Path.Combine(Path.GetTempPath(), file.Name);
                    using (var stream = new FileStream(ruta, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    //lectura de archivo
                    string allFileData = System.IO.File.ReadAllText(ruta);
                    //recorrer el archivo
                    foreach (string lineaActual in allFileData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(lineaActual))
                        {
                            string[] informacion = lineaActual.Split(',');
                            var personaActual = new Persona();


                            //Guardar Informacion
                            personaActual.tipo = informacion[0];
                            personaActual.nombre = informacion[1];
                            personaActual.dpi = informacion[2];
                            personaActual.nacimiento = informacion[3];
                            personaActual.direccion = informacion[4];
                               

                        }
                    }

                }
                catch (Exception e)
                {

                    ViewBag.Error = e.Message;
                }
            }
            return View(personas);
        }

        public IActionResult MostrarPersonas()
        {
            return View();
        }

    }
}
