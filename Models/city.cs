//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrimeraWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class city
    {
        public city()
        {
            this.usuario = new HashSet<usuario>();
        }
    
        public int id_city { get; set; }
        public string description { get; set; }
        public Nullable<int> countryId { get; set; }
    
        public virtual country country { get; set; }
        public virtual ICollection<usuario> usuario { get; set; }
    }
}
