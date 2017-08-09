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
    public class GeneralEquipmentController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /GeneralEquipment/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tgeneralequipment = db.saconfig_tGeneralEquipment.Include("saconfig_GeneralEquipmentOwnerType").Include("saconfig_tBay").Include("saconfig_tFunction").Include("saconfig_tGeneralEquipmentEnum").Include("saconfig_tSubFunction").Include("saconfig_tSubstation").Include("saconfig_tVoltageLevel");
            return View(saconfig_tgeneralequipment.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /GeneralEquipment/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGeneralEquipment saconfig_tgeneralequipment = db.saconfig_tGeneralEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgeneralequipment);
        }

        //
        // GET: /GeneralEquipment/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_GeneralEquipmentOwnerType, "ID", "GeneralEquipmentOwnerType");
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            ViewBag.type = new SelectList(db.saconfig_tGeneralEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "type");
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /GeneralEquipment/Create

        [HttpPost]
        public ActionResult Create(saconfig_tGeneralEquipment saconfig_tgeneralequipment)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgeneralequipment.DataOwnerID = userID;
                db.saconfig_tGeneralEquipment.AddObject(saconfig_tgeneralequipment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_GeneralEquipmentOwnerType, "ID", "GeneralEquipmentOwnerType", saconfig_tgeneralequipment.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.Bay);
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tgeneralequipment.Function);
            ViewBag.type = new SelectList(db.saconfig_tGeneralEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "type", saconfig_tgeneralequipment.type);
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tgeneralequipment.SubFunction);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.VoltageLevel);
            return View(saconfig_tgeneralequipment);
        }
        
        //
        // GET: /GeneralEquipment/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGeneralEquipment saconfig_tgeneralequipment = db.saconfig_tGeneralEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_GeneralEquipmentOwnerType, "ID", "GeneralEquipmentOwnerType", saconfig_tgeneralequipment.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.Bay);
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tgeneralequipment.Function);
            ViewBag.type = new SelectList(db.saconfig_tGeneralEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "type", saconfig_tgeneralequipment.type);
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tgeneralequipment.SubFunction);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.VoltageLevel);
            return View(saconfig_tgeneralequipment);
        }

        //
        // POST: /GeneralEquipment/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tGeneralEquipment saconfig_tgeneralequipment)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgeneralequipment.DataOwnerID = userID;
                db.saconfig_tGeneralEquipment.Attach(saconfig_tgeneralequipment);
                db.ObjectStateManager.ChangeObjectState(saconfig_tgeneralequipment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_GeneralEquipmentOwnerType, "ID", "GeneralEquipmentOwnerType", saconfig_tgeneralequipment.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.Bay);
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tgeneralequipment.Function);
            ViewBag.type = new SelectList(db.saconfig_tGeneralEquipmentEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "type", saconfig_tgeneralequipment.type);
            ViewBag.SubFunction = new SelectList(db.saconfig_tSubFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tgeneralequipment.SubFunction);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgeneralequipment.VoltageLevel);
            return View(saconfig_tgeneralequipment);
        }

        //
        // GET: /GeneralEquipment/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGeneralEquipment saconfig_tgeneralequipment = db.saconfig_tGeneralEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgeneralequipment);
        }

        //
        // POST: /GeneralEquipment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGeneralEquipment saconfig_tgeneralequipment = db.saconfig_tGeneralEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tGeneralEquipment.DeleteObject(saconfig_tgeneralequipment);
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