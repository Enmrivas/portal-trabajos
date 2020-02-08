using System;
using X.PagedList;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalWebTrabajos.Models;

namespace PortalWebTrabajos.Controllers
{
    public class TrabajosController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: Trabajos
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string SearchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LocSortParm = String.IsNullOrEmpty(sortOrder) ? "loc_desc" : "";
            ViewBag.ComSortParm = sortOrder == "Company" ? "comp_desc" : "Company";

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;

            var job = from t in db.Trabajos
                      select t;

            if (!String.IsNullOrEmpty(SearchString))
            {
                job = job.Where(t => t.Category.Contains(SearchString));
            }
            switch (sortOrder)
            {
                case "loc_desc":
                    job = job.OrderByDescending(t => t.Location);
                    break;
                case "Company":
                    job = job.OrderBy(t => t.Company);
                    break;
                case "comp_desc":
                    job = job.OrderByDescending(t => t.Company);
                    break;
                default:
                    job = job.OrderBy(t => t.Location);
                    break;
            }

            

            return View(await job.ToListAsync());
        }
        public async Task<ActionResult> ListaTrabajos(string sortOrder, string searchStringUser)
        {
            ViewBag.LocSortParm = String.IsNullOrEmpty(sortOrder) ? "loc_desc" : "";
            ViewBag.ComSortParm = sortOrder == "Company" ? "comp_desc" : "Company";
            var job = from t in db.Trabajos
                           select t;
            if (!String.IsNullOrEmpty(searchStringUser))
            {
                job = job.Where(t => t.Category.Contains(searchStringUser));
            }
            switch (sortOrder)
            {
                case "loc_desc":
                    job = job.OrderByDescending(t => t.Location);
                    break;
                case "Company":
                    job = job.OrderBy(t => t.Company);
                    break;
                case "comp_desc":
                    job = job.OrderByDescending(t => t.Company);
                    break;
                default:
                    job = job.OrderBy(t => t.Location);
                    break;
            }

            return View(await job.ToListAsync());
        }

        // GET: Trabajos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajos trabajos = await db.Trabajos.FindAsync(id);
            if (trabajos == null)
            {
                return HttpNotFound();
            }
            return View(trabajos);
        }

        // GET: Trabajos/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "JobID,Category,Location,Company,Position,Description")] Trabajos trabajos)
        {
            if (ModelState.IsValid)
            {
                db.Trabajos.Add(trabajos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(trabajos);
        }

        // GET: Trabajos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajos trabajos = await db.Trabajos.FindAsync(id);
            if (trabajos == null)
            {
                return HttpNotFound();
            }
            return View(trabajos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "JobID,Category,Location,Company,Position,Description")] Trabajos trabajos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabajos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trabajos);
        }

        // GET: Trabajos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajos trabajos = await db.Trabajos.FindAsync(id);
            if (trabajos == null)
            {
                return HttpNotFound();
            }
            return View(trabajos);
        }

        // POST: Trabajos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Trabajos trabajos = await db.Trabajos.FindAsync(id);
            db.Trabajos.Remove(trabajos);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
