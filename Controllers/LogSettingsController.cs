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
    public class LogSettingsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /LogSettings/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tlogsettings = db.saconfig_tLogSettings.Include("saconfig_tServices").Include("saconfig_tServiceSettingsEnum").Include("saconfig_tServiceSettingsEnum1").Include("saconfig_tServiceSettingsEnum2");
            return View(saconfig_tlogsettings.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /LogSettings/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogSettings saconfig_tlogsettings = db.saconfig_tLogSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlogsettings);
        }

        //
        // GET: /LogSettings/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.logEna = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /LogSettings/Create

        [HttpPost]
        public ActionResult Create(saconfig_tLogSettings saconfig_tlogsettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlogsettings.DataOwnerID = userID;
                db.saconfig_tLogSettings.AddObject(saconfig_tlogsettings);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tlogsettings.Services);
            ViewBag.logEna = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.logEna);
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.trgOps);
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.intgPd);
            return View(saconfig_tlogsettings);
        }
        
        //
        // GET: /LogSettings/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogSettings saconfig_tlogsettings = db.saconfig_tLogSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tlogsettings.Services);
            ViewBag.logEna = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.logEna);
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.trgOps);
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.intgPd);
            return View(saconfig_tlogsettings);
        }

        //
        // POST: /LogSettings/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tLogSettings saconfig_tlogsettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlogsettings.DataOwnerID = userID;
                db.saconfig_tLogSettings.Attach(saconfig_tlogsettings);
                db.ObjectStateManager.ChangeObjectState(saconfig_tlogsettings, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tlogsettings.Services);
            ViewBag.logEna = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.logEna);
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.trgOps);
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tlogsettings.intgPd);
            return View(saconfig_tlogsettings);
        }

        //
        // GET: /LogSettings/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogSettings saconfig_tlogsettings = db.saconfig_tLogSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlogsettings);
        }

        //
        // POST: /LogSettings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogSettings saconfig_tlogsettings = db.saconfig_tLogSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tLogSettings.DeleteObject(saconfig_tlogsettings);
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