using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using PrimeraWebApi.Models;

namespace PrimeraWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(@"Data Source=Rony\SQLEXPRESS;Initial Catalog=Proyecto;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;

        // GET api/values
        public IEnumerable<Employees> Get()
        {
            List<Employees> emps = new List<Employees>();
         
            con.Open();

            cmd = new SqlCommand("select * from empleados", con);

            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                Employees emp = new Employees(){
                    id = Convert.ToInt32(dr["codigo_empleado"]),
                    nombre = dr["nombres"].ToString(),
                    apellido = dr["apellidos"].ToString(),
                    cedula = dr["cedula"].ToString()

                };
                emps.Add(emp);
            }
            con.Close();

            return emps.ToList();
        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }


        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}