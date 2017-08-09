using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAConfig.Models;

namespace SAConfig.Controllers
{ 
    public class AuthenticationController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Authentication/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_authentication = db.saconfig_Authentication.Include("saconfig_tServer");
            return View(saconfig_authentication.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Authentication/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_Authentication saconfig_authentication = db.saconfig_Authentication.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_authentication);
        }

        //
        // GET: /Authentication/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Authentication/Create

        [HttpPost]
        public ActionResult Create(saconfig_Authentication saconfig_authentication)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_authentication.DataOwnerID = userID;
                db.saconfig_Authentication.AddObject(saconfig_authentication);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_authentication.Server);
            return View(saconfig_authentication);
        }
        
        //
        // GET: /Authentication/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_Authentication saconfig_authentication = db.saconfig_Authentication.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_authentication.Server);
            return View(saconfig_authentication);
        }

        //
        // POST: /Authentication/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_Authentication saconfig_authentication)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_authentication.DataOwnerID = userID;
                db.saconfig_Authentication.Attach(saconfig_authentication);
                db.ObjectStateManager.ChangeObjectState(saconfig_authentication, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_authentication.Server);
            return View(saconfig_authentication);
        }

        //
        // GET: /Authentication/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_Authentication saconfig_authentication = db.saconfig_Authentication.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_authentication);
        }

        //
        // POST: /Authentication/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_Authentication saconfig_authentication = db.saconfig_Authentication.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_Authentication.DeleteObject(saconfig_authentication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}