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
    public class TerminalController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Terminal/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tterminal = db.saconfig_tTerminal.Include("saconfig_tConductingEquipment").Include("saconfig_TerminalOwnerType").Include("saconfig_tTransformerWinding");
            return View(saconfig_tterminal.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Terminal/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTerminal saconfig_tterminal = db.saconfig_tTerminal.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tterminal);
        }

        //
        // GET: /Terminal/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.OwnerType = new SelectList(db.saconfig_TerminalOwnerType, "ID", "OwnerType");
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            return View();
        } 

        //
        // POST: /Terminal/Create

        [HttpPost]
        public ActionResult Create(saconfig_tTerminal saconfig_tterminal)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tterminal.DataOwnerID = userID;
                db.saconfig_tTerminal.AddObject(saconfig_tterminal);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tterminal.ConductingEqupment);
            ViewBag.OwnerType = new SelectList(db.saconfig_TerminalOwnerType, "ID", "OwnerType", saconfig_tterminal.OwnerType);
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tterminal.TransformerWinding);
            return View(saconfig_tterminal);
        }
        
        //
        // GET: /Terminal/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTerminal saconfig_tterminal = db.saconfig_tTerminal.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tterminal.ConductingEqupment);
            ViewBag.OwnerType = new SelectList(db.saconfig_TerminalOwnerType, "ID", "OwnerType", saconfig_tterminal.OwnerType);
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tterminal.TransformerWinding);
            return View(saconfig_tterminal);
        }

        //
        // POST: /Terminal/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tTerminal saconfig_tterminal)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tterminal.DataOwnerID = userID;
                db.saconfig_tTerminal.Attach(saconfig_tterminal);
                db.ObjectStateManager.ChangeObjectState(saconfig_tterminal, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConductingEqupment = new SelectList(db.saconfig_tConductingEquipment.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tterminal.ConductingEqupment);
            ViewBag.OwnerType = new SelectList(db.saconfig_TerminalOwnerType, "ID", "OwnerType", saconfig_tterminal.OwnerType);
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tterminal.TransformerWinding);
            return View(saconfig_tterminal);
        }

        //
        // GET: /Terminal/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTerminal saconfig_tterminal = db.saconfig_tTerminal.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tterminal);
        }

        //
        // POST: /Terminal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTerminal saconfig_tterminal = db.saconfig_tTerminal.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tTerminal.DeleteObject(saconfig_tterminal);
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