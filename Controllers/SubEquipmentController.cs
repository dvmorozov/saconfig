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
    public class SubEquipmentController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SubEquipment/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsubequipment = db.saconfig_tSubEquipment.Include("saconfig_SubEquipmentOwnerType").Include("saconfig_tConductingEquipment").Include("saconfig_tPhaseEnum").Include("saconfig_tTransformerWinding");
            return View(saconfig_tsubequipment.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SubEquipment/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubEquipment saconfig_tsubequipment = db.saconfig_tSubEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsubequipment);
        }

        //
        // GET: /SubEquipment/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_SubEquipmentOwnerType, "ID", "OwnerType");
            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.phase = new SelectList(db.saconfig_tPhaseEnum, "ID", "value");
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            return View();
        } 

        //
        // POST: /SubEquipment/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSubEquipment saconfig_tsubequipment)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsubequipment.DataOwnerID = userID;
                db.saconfig_tSubEquipment.AddObject(saconfig_tsubequipment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_SubEquipmentOwnerType, "ID", "OwnerType", saconfig_tsubequipment.OwnerType);
            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tsubequipment.ConductingEqupment);
            ViewBag.phase = new SelectList(db.saconfig_tPhaseEnum, "ID", "value", saconfig_tsubequipment.phase);
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tsubequipment.TransformerWinding);
            return View(saconfig_tsubequipment);
        }
        
        //
        // GET: /SubEquipment/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubEquipment saconfig_tsubequipment = db.saconfig_tSubEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_SubEquipmentOwnerType, "ID", "OwnerType", saconfig_tsubequipment.OwnerType);
            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tsubequipment.ConductingEqupment);
            ViewBag.phase = new SelectList(db.saconfig_tPhaseEnum, "ID", "value", saconfig_tsubequipment.phase);
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tsubequipment.TransformerWinding);
            return View(saconfig_tsubequipment);
        }

        //
        // POST: /SubEquipment/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSubEquipment saconfig_tsubequipment)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsubequipment.DataOwnerID = userID;
                db.saconfig_tSubEquipment.Attach(saconfig_tsubequipment);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsubequipment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_SubEquipmentOwnerType, "ID", "OwnerType", saconfig_tsubequipment.OwnerType);
            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tsubequipment.ConductingEqupment);
            ViewBag.phase = new SelectList(db.saconfig_tPhaseEnum, "ID", "value", saconfig_tsubequipment.phase);
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tsubequipment.TransformerWinding);
            return View(saconfig_tsubequipment);
        }

        //
        // GET: /SubEquipment/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubEquipment saconfig_tsubequipment = db.saconfig_tSubEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsubequipment);
        }

        //
        // POST: /SubEquipment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubEquipment saconfig_tsubequipment = db.saconfig_tSubEquipment.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSubEquipment.DeleteObject(saconfig_tsubequipment);
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