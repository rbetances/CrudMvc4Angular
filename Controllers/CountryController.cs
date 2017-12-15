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
    public class CountryController : ApiController
    {
        private db_escuelaEntities db = new db_escuelaEntities();

        List<country> listCountry = new List<country>();

        // GET api/Country
        public IEnumerable<country> Getcountries()
        {
            foreach(var i in db.country)
            {
                country coutry = new country()
                {
                    id_country = i.id_country,
                    description = i.description
                    
                };
                listCountry.Add(coutry);
            }
            

            return listCountry;
        }

        // GET api/Country/5
        public VmCountry Getcountry(int id)
        {
            VmCountry vmc = new VmCountry();

            country country = db.country.Where(a=>a.id_country == id).FirstOrDefault();

            foreach (var i in db.country.Where(a => a.id_country == id))
            {
                vmc = new VmCountry()
                {
                    id_country = i.id_country,
                    description = i.description
                };
            }

            if (country == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return vmc;
        }

        // PUT api/Country/5
        public HttpResponseMessage Putcountry(int id, country country)
        {
            if (ModelState.IsValid && id == country.id_country)
            {
                db.Entry(country).State = EntityState.Modified;

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

        // POST api/Country
        public HttpResponseMessage Postcountry(country country)
        {
            if (ModelState.IsValid)
            {
                db.country.Add(country);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, country);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = country.id_country }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Country/5
        public HttpResponseMessage Deletecountry(int id)
        {
            country country = db.country.Find(id);
            if (country == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.country.Remove(country);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, country);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}