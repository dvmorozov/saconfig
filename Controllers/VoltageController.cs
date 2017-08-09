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
    public class VoltageController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Voltage/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tvoltage = db.saconfig_tVoltage.Include("saconfig_tSIUnitEnum").Include("saconfig_tUnitMultiplierEnum").Include("saconfig_tVoltageLevel");
            return View(saconfig_tvoltage.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Voltage/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltage saconfig_tvoltage = db.saconfig_tVoltage.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tvoltage);
        }

        //
        // GET: /Voltage/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value");
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value");
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Voltage/Create

        [HttpPost]
        public ActionResult Create(saconfig_tVoltage saconfig_tvoltage)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tvoltage.DataOwnerID = userID;
                db.saconfig_tVoltage.AddObject(saconfig_tvoltage);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value", saconfig_tvoltage.unit);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tvoltage.multiplier);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tvoltage.VoltageLevel);
            return View(saconfig_tvoltage);
        }
        
        //
        // GET: /Voltage/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltage saconfig_tvoltage = db.saconfig_tVoltage.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value", saconfig_tvoltage.unit);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tvoltage.multiplier);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tvoltage.VoltageLevel);
            return View(saconfig_tvoltage);
        }

        //
        // POST: /Voltage/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tVoltage saconfig_tvoltage)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tvoltage.DataOwnerID = userID;
                db.saconfig_tVoltage.Attach(saconfig_tvoltage);
                db.ObjectStateManager.ChangeObjectState(saconfig_tvoltage, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.unit = new SelectList(db.saconfig_tSIUnitEnum, "ID", "value", saconfig_tvoltage.unit);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tvoltage.multiplier);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tvoltage.VoltageLevel);
            return View(saconfig_tvoltage);
        }

        //
        // GET: /Voltage/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltage saconfig_tvoltage = db.saconfig_tVoltage.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tvoltage);
        }

        //
        // POST: /Voltage/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltage saconfig_tvoltage = db.saconfig_tVoltage.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tVoltage.DeleteObject(saconfig_tvoltage);
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