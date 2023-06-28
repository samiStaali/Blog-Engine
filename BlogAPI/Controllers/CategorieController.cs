using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BlogAPI.Models;

namespace BlogAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategorieController : ApiController
    {
        private BDModel db = new BDModel();

        // GET: api/Categorie
        public IQueryable<Categorie> GetCategories()
        {
            var r = db.Categories;
            return db.Categories;
        }

        // GET: api/Categorie/5
        [ResponseType(typeof(Categorie))]
        public IHttpActionResult GetCategorie(long id)
        {
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            return Ok(categorie);
        }

        // PUT: api/Categorie/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategorie(long id, Categorie categorie)
        {
            var codeRetour = "200";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categorie.CategorieID)
            {
                return BadRequest();
            }

            if (TitreExists(categorie))
            {
                codeRetour = "400";
                return Ok(new { codeRetour = codeRetour });
            }

            db.Entry(categorie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { id = categorie.CategorieID, codeRetour = codeRetour });

            // return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categorie
        [ResponseType(typeof(Categorie))]
        public IHttpActionResult PostCategorie(Categorie categorie)
        {
            var codeRetour = "200";

            if (TitreExists(categorie))
            {
                codeRetour = "400";
                return Ok(new { codeRetour = codeRetour });
            }

            //Order table
            if (categorie.CategorieID == 0)
                db.Categories.Add(categorie);
            else
                db.Entry(categorie).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { id = categorie.CategorieID, codeRetour = codeRetour });
        }

        // DELETE: api/Categorie/5
        [ResponseType(typeof(Categorie))]
        public IHttpActionResult DeleteCategorie(long id)
        {
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            db.Categories.Remove(categorie);
            db.SaveChanges();

            return Ok(categorie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TitreExists(Categorie categorie)
        {
            var categories = db.Categories;
            var resultat = db.Categories.Count(e => e.CategorieID != categorie.CategorieID && e.Titre == categorie.Titre) > 0;
            return db.Categories.Count(e => e.CategorieID != categorie.CategorieID && e.Titre == categorie.Titre) > 0;
        }

        private bool CategorieExists(long id)
        {
            return db.Categories.Count(e => e.CategorieID == id) > 0;
        }
    }
}