using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelGSB;
using ORMGSB;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FrontGSB.Controllers
{
    public class DepartementsController : Controller
    {
        //private GsbOrm db = new GsbOrm();

        // GET: Departements
        public async Task<ActionResult> Index()
        {
            string url = "https://localhost:44333/api/Departements";

            using ( HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", "azerty");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var departements = await response.Content.ReadAsAsync<IEnumerable<Departement>>();

                    return View(departements);
            }
        }

        // GET: Departements/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string url = "https://localhost:44333/api/Departements/"+id;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", "azerty");
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var departmt = await response.Content.ReadAsAsync<Departement>();

                return View(departmt);
            }
        }

        public FileContentResult DownloadJson()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44333/api/Departements/"); // l'URI de l'API

            var response = client.GetAsync(client.BaseAddress).Result; // appel de la méthode HTTP GET pour récupérer les données des départements

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var departements = JsonConvert.DeserializeObject<IEnumerable<Departement>>(json); // désérialisation des données JSON en objets Departement

                var bytes = Encoding.ASCII.GetBytes(json);
                return File(bytes, "application/json", "liste_departements.json");
            }
            else
            {
                throw new Exception("Erreur lors de la récupération des données de la base de données.");
            }
        }

        // GET: Departements/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Departements/Create
        //// Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "IdDepartement,NomDep,NomRegion")] Departement departement)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Departements.Add(departement);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(departement);
        //}

        //// GET: Departements/Edit/5
        //public async Task<ActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Departement departement = await db.Departements.FindAsync(id);
        //    if (departement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(departement);
        //}

        //// POST: Departements/Edit/5
        //// Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "IdDepartement,NomDep,NomRegion")] Departement departement)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(departement).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(departement);
        //}

        //// GET: Departements/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Departement departement = await db.Departements.FindAsync(id);
        //    if (departement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(departement);
        //}

        //// POST: Departements/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    Departement departement = await db.Departements.FindAsync(id);
        //    db.Departements.Remove(departement);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
