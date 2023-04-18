using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ModelGSB;
using ORMGSB;

namespace BackGsb.Controllers
{
    public class DepartementsController : ApiController
    {
        private GsbOrm db = new GsbOrm();

        // GET: api/Departements
        public IQueryable<Departement> GetDepartements()
        {
            return db.Departements;
        }

        // GET: api/Departements/5
        [ResponseType(typeof(Departement))]
        public async Task<IHttpActionResult> GetDepartement(string id)
        {
            Departement departement = await db.Departements.FindAsync(id);
            if (departement == null)
            {
                return NotFound();
            }

            return Ok(departement);
        }

        // PUT: api/Departements/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDepartement(string id, Departement departement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departement.IdDepartement)
            {
                return BadRequest();
            }

            db.Entry(departement).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartementExists(id))
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

        // POST: api/Departements
        //[ResponseType(typeof(Departement))]
        //public async Task<IHttpActionResult> PostDepartement(Departement departement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Departements.Add(departement);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DepartementExists(departement.IdDepartement))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = departement.IdDepartement }, departement);
        //}

        // DELETE: api/Departements/5
        [ResponseType(typeof(Departement))]
        //public async Task<IHttpActionResult> DeleteDepartement(string id)
        //{
        //    Departement departement = await db.Departements.FindAsync(id);
        //    if (departement == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Departements.Remove(departement);
        //    await db.SaveChangesAsync();

        //    return Ok(departement);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool DepartementExists(string id)
        {
            return db.Departements.Count(e => e.IdDepartement == id) > 0;
        }
    }
}