using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDD2.LAB1.Models
{
    public class Persona
    {
        public string tipo { get; set; }
        public string nombre { get; set; }
        public string dpi { get; set; }
        public string nacimiento { get; set; }
        public string direccion { get; set; }
       

        public Persona(string nombre, string dpi, string nacimiento, string direccion) //CONSTRUCTOR DE PERSONA
        {
            this.tipo = tipo;
            this.nombre = nombre;
            this.dpi = dpi;
            this.nacimiento = nacimiento;
            this.direccion = direccion;

        }
        public Persona()
        {

        }
    }
}
