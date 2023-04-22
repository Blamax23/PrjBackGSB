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
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace FrontGSB.Controllers
{
    public class MedecinsController : Controller
    {

        // GET: Medecins
        public async Task<ActionResult> Index(string searchString)
        {
            var medecins = new List<Medecin>();
            if (!string.IsNullOrEmpty(searchString))
            {
                string url = "https://localhost:44333/api/Medecins?nom=" + searchString;

                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        medecins = response.Content.ReadAsAsync<List<Medecin>>().Result;
                    }
                }
                return View(medecins);
                
            }
            else
            {
                string url = "https://localhost:44333/api/Medecins";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", "azerty");
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception();
                    }

                    var allmedecins = await response.Content.ReadAsAsync<IEnumerable<Medecin>>();

                    return View(allmedecins);
                }
            }
        }

        // GET: Medecins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string url = "https://localhost:44333/api/Medecins/" + id;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var medecin = await response.Content.ReadAsAsync<Medecin>();
                return View(medecin);
            }
        }

        // GET: Medecins/Create
        [Authorize] //pour l'IHM
        public async Task<ActionResult> Create()
        {
            string url = "https://localhost:44333/api/Departements";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
                var departements = response.Content.ReadAsAsync<IEnumerable<Departement>>().Result.ToList();
                ViewBag.IdDepartement = new SelectList(departements, "IdDepartement", "NomDep");
                return View();
            }
        }

        // POST: Medecins/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdMedecin,NomMed,PrenomMed,AdresseMed,TelephoneMed,SpecialiteComplementaire,IdDepartement")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(medecin);

                using (HttpClient client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44333/api/Medecins"))
                    {
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                        // envoie des infos
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                        if (!send.IsSuccessStatusCode)
                            throw new Exception();

                        send.EnsureSuccessStatusCode();
                        //var departements = await send.Content.ReadAsAsync<IEnumerable<Departement>>();
                        //ViewBag.IdDepartement = new SelectList(departements, "IdDepartement", "NomDep", medecin.IdDepartement);
                        return RedirectToAction("Index");

                    }
                }
            }

            return View(medecin);
        }

        // GET: Medecins/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string url = "https://localhost:44333/api/Medecins/" + id;
            string url_dep = "https://localhost:44333/api/Departements";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(url);
                HttpResponseMessage response_dep = await client.GetAsync(url_dep);

                if (!response.IsSuccessStatusCode || !response_dep.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var departements = response_dep.Content.ReadAsAsync<IEnumerable<Departement>>().Result.ToList();

                var medecin = await response.Content.ReadAsAsync<Medecin>();
                ViewBag.IdDepartement = new SelectList(departements, "IdDepartement", "NomDep", medecin.IdDepartement);
                return View(medecin);
            }
        }

        // POST: Medecins/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdMedecin,NomMed,PrenomMed,AdresseMed,TelephoneMed,SpecialiteComplementaire,IdDepartement")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(medecin);
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                    HttpContent cont = new StringContent(json, Encoding.UTF8, "application/json");
                    // envoie des infos
                    var send = await client.PutAsync("https://localhost:44333/api/Medecins/" + medecin.IdMedecin, cont).ConfigureAwait(false);

                    if (!send.IsSuccessStatusCode)
                        throw new Exception();

                    send.EnsureSuccessStatusCode();
                    //var departements = await send.Content.ReadAsAsync<IEnumerable<Departement>>();
                    //ViewBag.IdDepartement = new SelectList(departements, "IdDepartement", "NomDep", medecin.IdDepartement);
                    return RedirectToAction("Index");
                }
            }
            return View(medecin);
        }

        // GET: Medecins/Delete/5
        [Authorize] //pour l'IHM
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string url = "https://localhost:44333/api/Medecins/" + id;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var medecin = await response.Content.ReadAsAsync<Medecin>();
                return View(medecin);
            }
        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            string url = "https://localhost:44333/api/Medecins/" + id;

            //controle indispensable
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string ReadToken()
        {
            string token = string.Empty;
            try
            {
                string filename = @"C:\tmp\token.txt";
                token = System.IO.File.ReadAllText(filename);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return token;
        }

        public FileContentResult DownloadJson(string searchString)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44333/api/Medecins?nom=" + searchString);

            var response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var medecins = JsonConvert.DeserializeObject<IEnumerable<Medecin>>(json);

                var bytes = Encoding.ASCII.GetBytes(json);
                return File(bytes, "application/json", "liste_medecins.json");
            }
            else
            {
                throw new Exception("Erreur lors de la récupération des données de la base de données.");
            }
        }

        public FileContentResult DownloadJsonFull()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44333/api/Medecins/");

            var response = client.GetAsync(client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var medecins = JsonConvert.DeserializeObject<IEnumerable<Medecin>>(json);

                var bytes = Encoding.ASCII.GetBytes(json);
                return File(bytes, "application/json", "liste_medecins_complete.json");
            }
            else
            {
                throw new Exception("Erreur lors de la récupération des données de la base de données.");
            }
        }
    }
}
