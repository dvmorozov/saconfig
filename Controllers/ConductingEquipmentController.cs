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
    public class ConductingEquipmentController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ConductingEquipment/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tconductingequipment = db.saconfig_tConductingEquipment.Include("saconfig_ConductingEquipmentOwnerType").Include("saconfig_tBay").Include("saconfig_tCommonConductingEquipmentEnum").Include("saconfig_tFunction").Include("saconfig_tSubFunction");
            return View(saconfig_tconductingequipment.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ConductingEquipment/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConductingEquipment saconfig_tconductingequipment = db.saconfig_tConductingEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconductingequipment);
        }

        //
        // GET: /ConductingEquipment/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_ConductingEquipmentOwnerType, "ID", "OwnerType");
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.type = new SelectList(db.saconfig_tCommonConductingEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            return View();
        } 

        //
        // POST: /ConductingEquipment/Create

        [HttpPost]
        public ActionResult Create(saconfig_tConductingEquipment saconfig_tconductingequipment)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconductingequipment.DataOwnerID = userID;
                db.saconfig_tConductingEquipment.AddObject(saconfig_tconductingequipment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
                        
            ViewBag.OwnerType = new SelectList(db.saconfig_ConductingEquipmentOwnerType, "ID", "OwnerType", saconfig_tconductingequipment.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconductingequipment.Bay);
            ViewBag.type = new SelectList(db.saconfig_tCommonConductingEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tconductingequipment.type);
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tconductingequipment.Function);
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tconductingequipment.SubFunction);
            return View(saconfig_tconductingequipment);
        }
        
        //
        // GET: /ConductingEquipment/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConductingEquipment saconfig_tconductingequipment = db.saconfig_tConductingEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_ConductingEquipmentOwnerType, "ID", "OwnerType", saconfig_tconductingequipment.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconductingequipment.Bay);
            ViewBag.type = new SelectList(db.saconfig_tCommonConductingEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tconductingequipment.type);
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tconductingequipment.Function);
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tconductingequipment.SubFunction);
            return View(saconfig_tconductingequipment);
        }

        //
        // POST: /ConductingEquipment/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tConductingEquipment saconfig_tconductingequipment)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconductingequipment.DataOwnerID = userID;
                db.saconfig_tConductingEquipment.Attach(saconfig_tconductingequipment);
                db.ObjectStateManager.ChangeObjectState(saconfig_tconductingequipment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.OwnerType = new SelectList(db.saconfig_ConductingEquipmentOwnerType, "ID", "OwnerType", saconfig_tconductingequipment.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconductingequipment.Bay);
            ViewBag.type = new SelectList(db.saconfig_tCommonConductingEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tconductingequipment.type);
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tconductingequipment.Function);
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tconductingequipment.SubFunction);
            return View(saconfig_tconductingequipment);
        }

        //
        // GET: /ConductingEquipment/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConductingEquipment saconfig_tconductingequipment = db.saconfig_tConductingEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconductingequipment);
        }

        //
        // POST: /ConductingEquipment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConductingEquipment saconfig_tconductingequipment = db.saconfig_tConductingEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tConductingEquipment.DeleteObject(saconfig_tconductingequipment);
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