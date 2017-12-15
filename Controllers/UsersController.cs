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
    public class UsersController : ApiController
    {
        List<VmUsuarios> ListUsers = new List<VmUsuarios>();
        db_escuelaEntities db = new db_escuelaEntities();
        // GET api/users
        public IEnumerable<VmUsuarios> Get()
        {
            foreach (var i in db.usuario.ToList())
            {
                VmUsuarios us = new VmUsuarios()
                {
                    nombre = i.nombre,
                    codigo_usuario = i.codigo_usuario,
                    codigo_cargo = i.codigo_cargo,
                    desc_cargo = i.cargo.descripcion,
                    country = i.country.description,
                    city = i.city.description
                };
                ListUsers.Add(us);

            }


            return ListUsers;
        }
        // GET api/Users/5
        public VmUsuarios Getusuario(int id)
        {
            VmUsuarios us = new VmUsuarios();

            foreach (var i in db.usuario.Where(a=> a.codigo_usuario == id))
            {
                us = new VmUsuarios()
                {
                    nombre = i.nombre,
                    codigo_usuario = i.codigo_usuario,
                    codigo_cargo = i.codigo_cargo,
                    desc_cargo = i.cargo.descripcion,
                    country = i.country.description,
                    city = i.city.description,
                    id_city = i.id_city,
                    id_country = i.id_country
                };
            }
            if (us == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return us;
        }

        // PUT api/Users/5
        public HttpResponseMessage Putusuario(int id, usuario usuario)
        {
            if (ModelState.IsValid && id == usuario.codigo_usuario)
            {
                db.Entry(usuario).State = EntityState.Modified;

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

        // POST api/Users
        public HttpResponseMessage Postusuario(usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, usuario);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = usuario.codigo_usuario }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Users/5
        public HttpResponseMessage Deleteusuario(int id)
        {
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.usuario.Remove(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, usuario);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}