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
    public class ConfLNsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ConfLNs/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tconflns = db.saconfig_tConfLNs.Include("saconfig_tServices");
            return View(saconfig_tconflns.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ConfLNs/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConfLNs saconfig_tconflns = db.saconfig_tConfLNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconflns);
        }

        //
        // GET: /ConfLNs/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /ConfLNs/Create

        [HttpPost]
        public ActionResult Create(saconfig_tConfLNs saconfig_tconflns)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconflns.DataOwnerID = userID;
                db.saconfig_tConfLNs.AddObject(saconfig_tconflns);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tconflns.Services);
            return View(saconfig_tconflns);
        }
        
        //
        // GET: /ConfLNs/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConfLNs saconfig_tconflns = db.saconfig_tConfLNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tconflns.Services);
            return View(saconfig_tconflns);
        }

        //
        // POST: /ConfLNs/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tConfLNs saconfig_tconflns)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconflns.DataOwnerID = userID;
                db.saconfig_tConfLNs.Attach(saconfig_tconflns);
                db.ObjectStateManager.ChangeObjectState(saconfig_tconflns, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID), "ID", "ID", saconfig_tconflns.Services);
            return View(saconfig_tconflns);
        }

        //
        // GET: /ConfLNs/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConfLNs saconfig_tconflns = db.saconfig_tConfLNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconflns);
        }

        //
        // POST: /ConfLNs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConfLNs saconfig_tconflns = db.saconfig_tConfLNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tConfLNs.DeleteObject(saconfig_tconflns);
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