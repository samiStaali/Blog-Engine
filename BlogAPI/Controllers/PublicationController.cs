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
    public class PublicationController : ApiController
    {
        private BDModel db = new BDModel();

        // GET: api/Publication
        public IQueryable<Publication> GetPublications()
        {
            return db.Publications;
        }

        // GET: api/Publication/5
        [ResponseType(typeof(Publication))]
        public IHttpActionResult GetPublication(long id)
        {
            Publication publication = db.Publications.Find(id);
            if (publication == null)
            {
                return NotFound();
            }

            return Ok(publication);
        }

        // PUT: api/Publication/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPublication(long id, Publication publication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publication.PublicationID)
            {
                return BadRequest();
            }

            db.Entry(publication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Publication
        [ResponseType(typeof(Publication))]
        public IHttpActionResult PostPublication(Publication publication)
        {
            var codeRetour = "200";

            if (TitreExists(publication))
            {
                codeRetour = "400";
                return Ok(new { codeRetour = codeRetour });
            }

            if (publication.PublicationID == 0)
                db.Publications.Add(publication);
            else  {
                var categorie = db.Categories.Where(l => l.CategorieID == publication.CategorieID).Single();
                publication.Categorie = categorie;

                db.Entry(publication).State = EntityState.Modified;
                }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = publication.PublicationID }, publication);
        }

        // DELETE: api/Publication/5
        [ResponseType(typeof(Publication))]
        public IHttpActionResult DeletePublication(long id)
        {
            Publication publication = db.Publications.Find(id);
            if (publication == null)
            {
                return NotFound();
            }

            db.Publications.Remove(publication);
            db.SaveChanges();

            return Ok(publication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool TitreExists(Publication publication)
        {
            var publications = db.Publications;
            var resultat = db.Publications.Count(e => e.PublicationID != publication.CategorieID && e.Titre == publication.Titre) > 0;
            return db.Publications.Count(e => e.PublicationID != publication.PublicationID && e.Titre == publication.Titre) > 0;
        }

        private bool PublicationExists(long id)
        {
            return db.Publications.Count(e => e.PublicationID == id) > 0;
        }
    }
}