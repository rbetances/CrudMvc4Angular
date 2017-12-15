using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeraWebApi.Models
{
    public class VmUsuarios
    {
        public int codigo_usuario { get; set; }
        public string nombre { get; set; }
        public Nullable<int> codigo_cargo { get; set; }
        public string desc_cargo { get; set; }
        public Nullable<int> id_country { get; set; }
        public Nullable<int> id_city { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }
}