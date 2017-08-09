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
    public class OptFieldsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /OptFields/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_optfields = db.saconfig_OptFields.Include("saconfig_tReportControl");
            return View(saconfig_optfields.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /OptFields/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_OptFields saconfig_optfields = db.saconfig_OptFields.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_optfields);
        }

        //
        // GET: /OptFields/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /OptFields/Create

        [HttpPost]
        public ActionResult Create(saconfig_OptFields saconfig_optfields)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_optfields.DataOwnerID = userID;
                db.saconfig_OptFields.AddObject(saconfig_optfields);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_optfields.ReportControl);
            return View(saconfig_optfields);
        }
        
        //
        // GET: /OptFields/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_OptFields saconfig_optfields = db.saconfig_OptFields.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_optfields.ReportControl);
            return View(saconfig_optfields);
        }

        //
        // POST: /OptFields/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_OptFields saconfig_optfields)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_optfields.DataOwnerID = userID;
                db.saconfig_OptFields.Attach(saconfig_optfields);
                db.ObjectStateManager.ChangeObjectState(saconfig_optfields, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_optfields.ReportControl);
            return View(saconfig_optfields);
        }

        //
        // GET: /OptFields/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_OptFields saconfig_optfields = db.saconfig_OptFields.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_optfields);
        }

        //
        // POST: /OptFields/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_OptFields saconfig_optfields = db.saconfig_OptFields.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_OptFields.DeleteObject(saconfig_optfields);
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