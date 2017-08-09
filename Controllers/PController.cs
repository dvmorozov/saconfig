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
    public class PController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /P/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tp = db.saconfig_tP.Include("saconfig_POwnerType").Include("saconfig_tAddress").Include("saconfig_tPhysConn").Include("saconfig_tPTypeEnum");
            return View(saconfig_tp.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /P/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tP saconfig_tp = db.saconfig_tP.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tp);
        }

        //
        // GET: /P/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_POwnerType, "ID", "OwnerType");
            ViewBag.Address = new SelectList(db.saconfig_tAddress.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.PhysConn = new SelectList(db.saconfig_tPhysConn.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            ViewBag.type = new SelectList(db.saconfig_tPTypeEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            return View();
        } 

        //
        // POST: /P/Create

        [HttpPost]
        public ActionResult Create(saconfig_tP saconfig_tp)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tp.DataOwnerID = userID;
                db.saconfig_tP.AddObject(saconfig_tp);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_POwnerType, "ID", "OwnerType", saconfig_tp.OwnerType);
            ViewBag.Address = new SelectList(db.saconfig_tAddress.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tp.Address);
            ViewBag.PhysConn = new SelectList(db.saconfig_tPhysConn.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tp.PhysConn);
            ViewBag.type = new SelectList(db.saconfig_tPTypeEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tp.type);
            return View(saconfig_tp);
        }
        
        //
        // GET: /P/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tP saconfig_tp = db.saconfig_tP.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_POwnerType, "ID", "OwnerType", saconfig_tp.OwnerType);
            ViewBag.Address = new SelectList(db.saconfig_tAddress.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tp.Address);
            ViewBag.PhysConn = new SelectList(db.saconfig_tPhysConn.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tp.PhysConn);
            ViewBag.type = new SelectList(db.saconfig_tPTypeEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tp.type);
            return View(saconfig_tp);
        }

        //
        // POST: /P/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tP saconfig_tp)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tp.DataOwnerID = userID;
                db.saconfig_tP.Attach(saconfig_tp);
                db.ObjectStateManager.ChangeObjectState(saconfig_tp, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_POwnerType, "ID", "OwnerType", saconfig_tp.OwnerType);
            ViewBag.Address = new SelectList(db.saconfig_tAddress.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tp.Address);
            ViewBag.PhysConn = new SelectList(db.saconfig_tPhysConn.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tp.PhysConn);
            ViewBag.type = new SelectList(db.saconfig_tPTypeEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tp.type);
            return View(saconfig_tp);
        }

        //
        // GET: /P/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tP saconfig_tp = db.saconfig_tP.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tp);
        }

        //
        // POST: /P/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tP saconfig_tp = db.saconfig_tP.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tP.DeleteObject(saconfig_tp);
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