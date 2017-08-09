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
    public class BayController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Bay/

        public ViewResult Index()
        {
            var saconfig_tbay = db.saconfig_tBay.Include("saconfig_tVoltageLevel");
            Guid userID = GetUserID();
            return View(saconfig_tbay.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Bay/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBay saconfig_tbay = db.saconfig_tBay.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tbay);
        }

        //
        // GET: /Bay/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Bay/Create

        [HttpPost]
        public ActionResult Create(saconfig_tBay saconfig_tbay)
        {
            Guid userID = GetUserID();

            if (ModelState.IsValid)
            {
                saconfig_tbay.DataOwnerID = GetUserID();
                db.saconfig_tBay.AddObject(saconfig_tbay);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbay.VoltageLevel);
            return View(saconfig_tbay);
        }
        
        //
        // GET: /Bay/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBay saconfig_tbay = db.saconfig_tBay.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbay.VoltageLevel);
            return View(saconfig_tbay);
        }

        //
        // POST: /Bay/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tBay saconfig_tbay)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tbay.DataOwnerID = userID;
                db.saconfig_tBay.Attach(saconfig_tbay);
                db.ObjectStateManager.ChangeObjectState(saconfig_tbay, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbay.VoltageLevel);
            return View(saconfig_tbay);
        }

        //
        // GET: /Bay/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBay saconfig_tbay = db.saconfig_tBay.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tbay);
        }

        //
        // POST: /Bay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBay saconfig_tbay = db.saconfig_tBay.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tBay.DeleteObject(saconfig_tbay);
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