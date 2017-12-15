using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PrimeraWebApi.Models;

namespace PrimeraWebApi.Controllers
{
    public class CityController : ApiController
    {
        private db_escuelaEntities db = new db_escuelaEntities();
        List<VmCity> ListCities = new List<VmCity>();


        // GET api/City
        public IEnumerable<VmCity> Getcities()
        {
            

            foreach (var i in db.city)
            {
                VmCity City = new VmCity() {
                    id_city = i.id_city,
                    description = i.description,
                    countryId = i.country.id_country
                };
                ListCities.Add(City);
            }

            
            return ListCities;
        }

        // GET api/City/5
        public IEnumerable<VmCity> Getcity(int id)
        {
            foreach (var i in db.city.Where(a => a.countryId == id))
            {
                VmCity City = new VmCity()
                {
                    id_city = i.id_city,
                    description = i.description,
                    countryId = i.country.id_country
                };
                ListCities.Add(City);
            }


            return ListCities;
        }

        // PUT api/City/5
        public HttpResponseMessage Putcity(int id, city city)
        {
            if (ModelState.IsValid && id == city.id_city)
            {
                db.Entry(city).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/City
        public HttpResponseMessage Postcity(city city)
        {
            if (ModelState.IsValid)
            {
                db.city.Add(city);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, city);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = city.id_city }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/City/5
        public HttpResponseMessage Deletecity(int id)
        {
            city city = db.city.Find(id);
            if (city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.city.Remove(city);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}