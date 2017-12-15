using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeraWebApi.Models
{
    public class VmCity
    {
        public int id_city { get; set; }
        public string description { get; set; }
        public int countryId { get; set; }
    }
}