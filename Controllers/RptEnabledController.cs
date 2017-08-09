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
    public class RptEnabledController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /RptEnabled/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_trptenabled = db.saconfig_tRptEnabled.Include("saconfig_tReportControl");
            return View(saconfig_trptenabled.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /RptEnabled/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tRptEnabled saconfig_trptenabled = db.saconfig_tRptEnabled.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_trptenabled);
        }

        //
        // GET: /RptEnabled/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /RptEnabled/Create

        [HttpPost]
        public ActionResult Create(saconfig_tRptEnabled saconfig_trptenabled)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_trptenabled.DataOwnerID = userID;
                db.saconfig_tRptEnabled.AddObject(saconfig_trptenabled);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_trptenabled.ReportControl);
            return View(saconfig_trptenabled);
        }
        
        //
        // GET: /RptEnabled/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tRptEnabled saconfig_trptenabled = db.saconfig_tRptEnabled.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_trptenabled.ReportControl);
            return View(saconfig_trptenabled);
        }

        //
        // POST: /RptEnabled/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tRptEnabled saconfig_trptenabled)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_trptenabled.DataOwnerID = userID;
                db.saconfig_tRptEnabled.Attach(saconfig_trptenabled);
                db.ObjectStateManager.ChangeObjectState(saconfig_trptenabled, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_trptenabled.ReportControl);
            return View(saconfig_trptenabled);
        }

        //
        // GET: /RptEnabled/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tRptEnabled saconfig_trptenabled = db.saconfig_tRptEnabled.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_trptenabled);
        }

        //
        // POST: /RptEnabled/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tRptEnabled saconfig_trptenabled = db.saconfig_tRptEnabled.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tRptEnabled.DeleteObject(saconfig_trptenabled);
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