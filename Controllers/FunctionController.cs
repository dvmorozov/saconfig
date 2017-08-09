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
    public class FunctionController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Function/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tfunction = db.saconfig_tFunction.Include("saconfig_FunctionOwnerType").Include("saconfig_tBay").Include("saconfig_tSubstation").Include("saconfig_tVoltageLevel");
            return View(saconfig_tfunction.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Function/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFunction saconfig_tfunction = db.saconfig_tFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tfunction);
        }

        //
        // GET: /Function/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_FunctionOwnerType, "ID", "FunctionOwnerType");
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Function/Create

        [HttpPost]
        public ActionResult Create(saconfig_tFunction saconfig_tfunction)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tfunction.DataOwnerID = userID;
                db.saconfig_tFunction.AddObject(saconfig_tfunction);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_FunctionOwnerType, "ID", "FunctionOwnerType", saconfig_tfunction.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.Bay);
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.Substantion);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.VoltageLevel);
            return View(saconfig_tfunction);
        }
        
        //
        // GET: /Function/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFunction saconfig_tfunction = db.saconfig_tFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_FunctionOwnerType, "ID", "FunctionOwnerType", saconfig_tfunction.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.Bay);
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.Substantion);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.VoltageLevel);
            return View(saconfig_tfunction);
        }

        //
        // POST: /Function/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tFunction saconfig_tfunction)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tfunction.DataOwnerID = userID;
                db.saconfig_tFunction.Attach(saconfig_tfunction);
                db.ObjectStateManager.ChangeObjectState(saconfig_tfunction, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.OwnerType = new SelectList(db.saconfig_FunctionOwnerType, "ID", "FunctionOwnerType", saconfig_tfunction.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.Bay);
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.Substantion);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfunction.VoltageLevel);
            return View(saconfig_tfunction);
        }

        //
        // GET: /Function/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFunction saconfig_tfunction = db.saconfig_tFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tfunction);
        }

        //
        // POST: /Function/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFunction saconfig_tfunction = db.saconfig_tFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tFunction.DeleteObject(saconfig_tfunction);
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