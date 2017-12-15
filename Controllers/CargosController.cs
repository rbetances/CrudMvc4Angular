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
    public class CargosController : ApiController
    {
        private db_escuelaEntities db = new db_escuelaEntities();

        List<cargo> ListaCargos = new List<cargo>();

        // GET api/Cargos
        public IEnumerable<cargo> Getcargoes()
        {
            foreach (var i in db.cargo.AsEnumerable())
            {
                cargo cargo = new cargo()
                {
                    codigo_cargo = i.codigo_cargo,
                    descripcion = i.descripcion
                };
                ListaCargos.Add(cargo);
            }

            

            return ListaCargos;
        }

        // GET api/Cargos/5
        public cargo Getcargo(int id)
        {
            cargo cargo = db.cargo.Find(id);
            if (cargo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return cargo;
        }

        // PUT api/Cargos/5
        public HttpResponseMessage Putcargo(int id, cargo cargo)
        {
            if (ModelState.IsValid && id == cargo.codigo_cargo)
            {
                db.Entry(cargo).State = EntityState.Modified;

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

        // POST api/Cargos
        public HttpResponseMessage Postcargo(cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.cargo.Add(cargo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cargo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cargo.codigo_cargo }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Cargos/5
        public HttpResponseMessage Deletecargo(int id)
        {
            cargo cargo = db.cargo.Find(id);
            if (cargo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.cargo.Remove(cargo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cargo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}