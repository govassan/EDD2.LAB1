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
using CustomStructures.AVL_Tree;
using System.Data;
using Newtonsoft.Json;



namespace EDD2.LAB1.Controllers
{
    public class HomeController : Controller

    {
    

        public static List<Persona> personas = new List<Persona>();//lista global desde archivo
     
        public static int ComparerData(Persona p1, Persona p2)
        {
            return p1.dpi.CompareTo(p2.dpi);
        }

        static AVLTree<Persona> Arbol = new AVLTree<Persona>(ComparerData);

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
   
        public IActionResult Index()
        {
            return View();
        }


        
        public IActionResult BusquedaPersona()
        {
            string nombre = Request.Form["txtFiltro"].ToString();
            List<Persona> lista = Arbol.InOrder();
            if (lista.Count > 0)
            {
                //query para la busqueda de datos
                var filtro = from x in lista where (x.name.Contains(nombre)) select (x);
                ViewData["ListaPersonas"] = filtro.ToList();
                return View("./MostrarPersonas");
            }
            else
            {
                ViewData["ListaPersonas"] = lista ;
                return View("./MostrarPersonas");
            }
        }
        public IActionResult VerLista() {
            ViewData["ListaPersonas"] = Arbol.InOrder();
            return View("./MostrarPersonas");
        }

        public IActionResult IngresarPersonas() {
            return View();
        }
        [HttpPost]
        public IActionResult CargarArchivo(IFormFile ArchivoCargado)
        {
                if (ArchivoCargado != null)
                {
                    try
                    {
                        //subir archivo a una carpeta temporal
                        string ruta = Path.Combine(Path.GetTempPath(), ArchivoCargado.Name);
                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                        ArchivoCargado.CopyTo(stream);
                        }
                        //lectura de archivo
                        string allFileData = System.IO.File.ReadAllText(ruta);
                        //recorrer el archivo
                        foreach (string lineaActual in allFileData.Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(lineaActual))
                            {
                                string[] informacion = lineaActual.Split('{');
                            Persona _persona;

                            informacion[1] = "{" + informacion[1];
                            _persona = JsonConvert.DeserializeObject<Persona>(informacion[1]);
                            var inicador = informacion[0];
                            if (inicador.Contains("INSERT")) {
                                Arbol.Add(_persona);
                            
                            } else if (inicador.Contains("PATCH")) {
                                Arbol.Add(_persona);

                            } else {
                                Arbol.Remove(_persona);
                            }

                              

                            }
                        }

                    }
                    catch (Exception e)
                    {

                        ViewBag.Error = e.Message;
                    }
                }
            ViewData["ListaPersonas"] = Arbol.InOrder();
                return View("./MostrarPersonas");
            }


    }
}
