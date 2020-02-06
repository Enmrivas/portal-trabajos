using System;
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
        private TrabajosContext db = new TrabajosContext();

        // GET: Trabajos
        public async Task<ActionResult> Index()
        {
            return View(await db.Trabajos.ToListAsync());
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

                //ViewBag.MyCategory = new SelectList(dbc.Categories, "CatID", "Categoria");
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

        // POST: Trabajos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
