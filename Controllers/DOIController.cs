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
    public class DOIController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DOI/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tdoi = db.saconfig_tDOI.Include("saconfig_DOIOwnerType").Include("saconfig_tLN").Include("saconfig_tLN0");
            return View(saconfig_tdoi.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DOI/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOI saconfig_tdoi = db.saconfig_tDOI.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdoi);
        }

        //
        // GET: /DOI/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_DOIOwnerType, "ID", "OwnerType");
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /DOI/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDOI saconfig_tdoi)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdoi.DataOwnerID = userID;
                db.saconfig_tDOI.AddObject(saconfig_tdoi);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_DOIOwnerType, "ID", "OwnerType", saconfig_tdoi.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdoi.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdoi.LN0);
            return View(saconfig_tdoi);
        }
        
        //
        // GET: /DOI/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOI saconfig_tdoi = db.saconfig_tDOI.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_DOIOwnerType, "ID", "OwnerType", saconfig_tdoi.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdoi.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdoi.LN0);
            return View(saconfig_tdoi);
        }

        //
        // POST: /DOI/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDOI saconfig_tdoi)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdoi.DataOwnerID = userID;
                db.saconfig_tDOI.Attach(saconfig_tdoi);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdoi, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_DOIOwnerType, "ID", "OwnerType", saconfig_tdoi.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdoi.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdoi.LN0);
            return View(saconfig_tdoi);
        }

        //
        // GET: /DOI/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOI saconfig_tdoi = db.saconfig_tDOI.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdoi);
        }

        //
        // POST: /DOI/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOI saconfig_tdoi = db.saconfig_tDOI.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tDOI.DeleteObject(saconfig_tdoi);
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