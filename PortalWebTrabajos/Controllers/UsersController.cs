﻿using System;
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
    public class UsersController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: Users
        public async Task<ActionResult> Index(string sortOrder)
        {

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.userNameSortParam = sortOrder == "Username" ? "user_desc" : "Username";
            var users = from u in db.Users select u;
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.Name);
                    break;
                case "Username":
                    users = users.OrderBy(u => u.Username);
                    break;
                case "user_desc":
                    users = users.OrderByDescending(u => u.Username);
                    break;
                default:
                    users = users.OrderBy(u => u.Name);
                    break;
            }
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "UserID,Username,Name,Email,Password,Admin")] Users users)
        {
            if (ModelState.IsValid && !db.Users.Any(u => u.Username == users.Username) && !db.Users.Any(u=>u.Email == users.Email))
            {
                users.Admin = false;
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return RedirectToAction("PaginaUsuarioNormal");
            }
            else
            {
                ModelState.AddModelError("", "Usuario ya existe en la base de datos ");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Username,Name,Email,Password,Admin")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Users users = await db.Users.FindAsync(id);
            db.Users.Remove(users);
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users user)
        {
            using (UsersContext db = new UsersContext())
            {
                var usr = db.Users.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null && usr.Admin == true)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Name"] = usr.Name.ToString();
                    return RedirectToAction("PaginaPrincipal");
                }
                else if(usr.Admin != true){
                    return RedirectToAction("PaginaUsuarioNormal");
                }else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                    return View();
                }
            }
        }

        public ActionResult PaginaPrincipal()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult PaginaUsuarioNormal()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
