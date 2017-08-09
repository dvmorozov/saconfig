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
    public class PowerTransformerController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /PowerTransformer/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tpowertransformer = db.saconfig_tPowerTransformer.Include("saconfig_PowerTransformerOwnerType").Include("saconfig_tBay").Include("saconfig_tSubstation").Include("saconfig_tVoltageLevel");
            return View(saconfig_tpowertransformer.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /PowerTransformer/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPowerTransformer saconfig_tpowertransformer = db.saconfig_tPowerTransformer.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tpowertransformer);
        }

        //
        // GET: /PowerTransformer/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_PowerTransformerOwnerType, "ID", "OwnerType");
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /PowerTransformer/Create

        [HttpPost]
        public ActionResult Create(saconfig_tPowerTransformer saconfig_tpowertransformer)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tpowertransformer.DataOwnerID = userID;
                db.saconfig_tPowerTransformer.AddObject(saconfig_tpowertransformer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            
            ViewBag.OwnerType = new SelectList(db.saconfig_PowerTransformerOwnerType, "ID", "OwnerType", saconfig_tpowertransformer.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.Bay);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.VoltageLevel);
            return View(saconfig_tpowertransformer);
        }
        
        //
        // GET: /PowerTransformer/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPowerTransformer saconfig_tpowertransformer = db.saconfig_tPowerTransformer.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_PowerTransformerOwnerType, "ID", "OwnerType", saconfig_tpowertransformer.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.Bay);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.VoltageLevel);
            return View(saconfig_tpowertransformer);
        }

        //
        // POST: /PowerTransformer/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tPowerTransformer saconfig_tpowertransformer)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tpowertransformer.DataOwnerID = userID;
                db.saconfig_tPowerTransformer.Attach(saconfig_tpowertransformer);
                db.ObjectStateManager.ChangeObjectState(saconfig_tpowertransformer, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.OwnerType = new SelectList(db.saconfig_PowerTransformerOwnerType, "ID", "OwnerType", saconfig_tpowertransformer.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.Bay);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tpowertransformer.VoltageLevel);
            return View(saconfig_tpowertransformer);
        }

        //
        // GET: /PowerTransformer/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPowerTransformer saconfig_tpowertransformer = db.saconfig_tPowerTransformer.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tpowertransformer);
        }

        //
        // POST: /PowerTransformer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPowerTransformer saconfig_tpowertransformer = db.saconfig_tPowerTransformer.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tPowerTransformer.DeleteObject(saconfig_tpowertransformer);
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