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
    public class GSESettingsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /GSESettings/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tgsesettings = db.saconfig_tGSESettings.Include("saconfig_tServices").Include("saconfig_tServiceSettingsEnum").Include("saconfig_tServiceSettingsEnum1");
            return View(saconfig_tgsesettings.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /GSESettings/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSESettings saconfig_tgsesettings = db.saconfig_tGSESettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgsesettings);
        }

        //
        // GET: /GSESettings/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.appID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.dataLabel = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /GSESettings/Create

        [HttpPost]
        public ActionResult Create(saconfig_tGSESettings saconfig_tgsesettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgsesettings.DataOwnerID = userID;
                db.saconfig_tGSESettings.AddObject(saconfig_tgsesettings);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tgsesettings.Services);
            ViewBag.appID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tgsesettings.appID);
            ViewBag.dataLabel = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tgsesettings.dataLabel);
            return View(saconfig_tgsesettings);
        }
        
        //
        // GET: /GSESettings/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSESettings saconfig_tgsesettings = db.saconfig_tGSESettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tgsesettings.Services);
            ViewBag.appID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tgsesettings.appID);
            ViewBag.dataLabel = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tgsesettings.dataLabel);
            return View(saconfig_tgsesettings);
        }

        //
        // POST: /GSESettings/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tGSESettings saconfig_tgsesettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgsesettings.DataOwnerID = userID;
                db.saconfig_tGSESettings.Attach(saconfig_tgsesettings);
                db.ObjectStateManager.ChangeObjectState(saconfig_tgsesettings, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tgsesettings.Services);
            ViewBag.appID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tgsesettings.appID);
            ViewBag.dataLabel = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tgsesettings.dataLabel);
            return View(saconfig_tgsesettings);
        }

        //
        // GET: /GSESettings/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSESettings saconfig_tgsesettings = db.saconfig_tGSESettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgsesettings);
        }

        //
        // POST: /GSESettings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSESettings saconfig_tgsesettings = db.saconfig_tGSESettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tGSESettings.DeleteObject(saconfig_tgsesettings);
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