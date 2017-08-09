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
    public class SMVSettingsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SMVSettings/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsvmsettings = db.saconfig_tSVMSettings.Include("saconfig_SVMSettingsInternalElementName").Include("saconfig_tServices").Include("saconfig_tServiceSettingsEnum").Include("saconfig_tServiceSettingsEnum1").Include("saconfig_tServiceSettingsEnum2").Include("saconfig_tServiceSettingsEnum3").Include("saconfig_tServiceSettingsEnum4").Include("saconfig_tServiceSettingsEnum5");
            return View(saconfig_tsvmsettings.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SMVSettings/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSVMSettings saconfig_tsvmsettings = db.saconfig_tSVMSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsvmsettings);
        }

        //
        // GET: /SMVSettings/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.InternalElement = new SelectList(db.saconfig_SVMSettingsInternalElementName, "ID", "ElementName");
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.svID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.smpRate = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.samplesPerSec = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.cbName = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.datSet = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /SMVSettings/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSVMSettings saconfig_tsvmsettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsvmsettings.DataOwnerID = userID;
                db.saconfig_tSVMSettings.AddObject(saconfig_tsvmsettings);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.InternalElement = new SelectList(db.saconfig_SVMSettingsInternalElementName, "ID", "ElementName", saconfig_tsvmsettings.InternalElement);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tsvmsettings.Services);
            ViewBag.svID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.svID);
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.optFields);
            ViewBag.smpRate = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.smpRate);
            ViewBag.samplesPerSec = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.samplesPerSec);
            ViewBag.cbName = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.cbName);
            ViewBag.datSet = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.datSet);
            return View(saconfig_tsvmsettings);
        }
        
        //
        // GET: /SMVSettings/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSVMSettings saconfig_tsvmsettings = db.saconfig_tSVMSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.InternalElement = new SelectList(db.saconfig_SVMSettingsInternalElementName, "ID", "ElementName", saconfig_tsvmsettings.InternalElement);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tsvmsettings.Services);
            ViewBag.svID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.svID);
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.optFields);
            ViewBag.smpRate = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.smpRate);
            ViewBag.samplesPerSec = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.samplesPerSec);
            ViewBag.cbName = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.cbName);
            ViewBag.datSet = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.datSet);
            return View(saconfig_tsvmsettings);
        }

        //
        // POST: /SMVSettings/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSVMSettings saconfig_tsvmsettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsvmsettings.DataOwnerID = userID;
                db.saconfig_tSVMSettings.Attach(saconfig_tsvmsettings);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsvmsettings, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InternalElement = new SelectList(db.saconfig_SVMSettingsInternalElementName, "ID", "ElementName", saconfig_tsvmsettings.InternalElement);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tsvmsettings.Services);
            ViewBag.svID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.svID);
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.optFields);
            ViewBag.smpRate = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.smpRate);
            ViewBag.samplesPerSec = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.samplesPerSec);
            ViewBag.cbName = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.cbName);
            ViewBag.datSet = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_tsvmsettings.datSet);
            return View(saconfig_tsvmsettings);
        }

        //
        // GET: /SMVSettings/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSVMSettings saconfig_tsvmsettings = db.saconfig_tSVMSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsvmsettings);
        }

        //
        // POST: /SMVSettings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSVMSettings saconfig_tsvmsettings = db.saconfig_tSVMSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSVMSettings.DeleteObject(saconfig_tsvmsettings);
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