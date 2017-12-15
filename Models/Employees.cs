using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeraWebApi.Models
{
    public class Employees
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string cedula { get; set; }
    }
}