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
    public class DurationInMilliSecController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DurationInMilliSec/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tdurationinmillisec = db.saconfig_tDurationInMilliSec.Include("saconfig_DurationInMilliSecElementName").Include("saconfig_tGSE").Include("saconfig_tSIUnitEnum").Include("saconfig_tUnitMultiplierEnum");
            return View(saconfig_tdurationinmillisec.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DurationInMilliSec/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDurationInMilliSec saconfig_tdurationinmillisec = db.saconfig_tDurationInMilliSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdurationinmillisec);
        }

        //
        // GET: /DurationInMilliSec/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ElementName = new SelectList(db.saconfig_DurationInMilliSecElementName, "ID", "ElementName");
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value");
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /DurationInMilliSec/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDurationInMilliSec saconfig_tdurationinmillisec)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdurationinmillisec.DataOwnerID = userID;
                db.saconfig_tDurationInMilliSec.AddObject(saconfig_tdurationinmillisec);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ElementName = new SelectList(db.saconfig_DurationInMilliSecElementName, "ID", "ElementName", saconfig_tdurationinmillisec.ElementName);
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdurationinmillisec.GSE);
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value", saconfig_tdurationinmillisec.unit);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tdurationinmillisec.multiplier);
            return View(saconfig_tdurationinmillisec);
        }
        
        //
        // GET: /DurationInMilliSec/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDurationInMilliSec saconfig_tdurationinmillisec = db.saconfig_tDurationInMilliSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ElementName = new SelectList(db.saconfig_DurationInMilliSecElementName, "ID", "ElementName", saconfig_tdurationinmillisec.ElementName);
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdurationinmillisec.GSE);
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value", saconfig_tdurationinmillisec.unit);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tdurationinmillisec.multiplier);
            return View(saconfig_tdurationinmillisec);
        }

        //
        // POST: /DurationInMilliSec/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDurationInMilliSec saconfig_tdurationinmillisec)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdurationinmillisec.DataOwnerID = userID;
                db.saconfig_tDurationInMilliSec.Attach(saconfig_tdurationinmillisec);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdurationinmillisec, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElementName = new SelectList(db.saconfig_DurationInMilliSecElementName, "ID", "ElementName", saconfig_tdurationinmillisec.ElementName);
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdurationinmillisec.GSE);
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value", saconfig_tdurationinmillisec.unit);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tdurationinmillisec.multiplier);
            return View(saconfig_tdurationinmillisec);
        }

        //
        // GET: /DurationInMilliSec/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDurationInMilliSec saconfig_tdurationinmillisec = db.saconfig_tDurationInMilliSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdurationinmillisec);
        }

        //
        // POST: /DurationInMilliSec/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDurationInMilliSec saconfig_tdurationinmillisec = db.saconfig_tDurationInMilliSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tDurationInMilliSec.DeleteObject(saconfig_tdurationinmillisec);
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