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
    public class TrgOpsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /TrgOps/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_ttrgops = db.saconfig_tTrgOps.Include("saconfig_LogControlOwnerType").Include("saconfig_tLogControl").Include("saconfig_tReportControl");
            return View(saconfig_ttrgops.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /TrgOps/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTrgOps saconfig_ttrgops = db.saconfig_tTrgOps.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_ttrgops);
        }

        //
        // GET: /TrgOps/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType");
            ViewBag.LogControl = new SelectList(db.saconfig_tLogControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /TrgOps/Create

        [HttpPost]
        public ActionResult Create(saconfig_tTrgOps saconfig_ttrgops)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_ttrgops.DataOwnerID = userID;
                db.saconfig_tTrgOps.AddObject(saconfig_ttrgops);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType", saconfig_ttrgops.OwnerType);
            ViewBag.LogControl = new SelectList(db.saconfig_tLogControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_ttrgops.LogControl);
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_ttrgops.ReportControl);
            return View(saconfig_ttrgops);
        }
        
        //
        // GET: /TrgOps/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTrgOps saconfig_ttrgops = db.saconfig_tTrgOps.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType", saconfig_ttrgops.OwnerType);
            ViewBag.LogControl = new SelectList(db.saconfig_tLogControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_ttrgops.LogControl);
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_ttrgops.ReportControl);
            return View(saconfig_ttrgops);
        }

        //
        // POST: /TrgOps/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tTrgOps saconfig_ttrgops)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_ttrgops.DataOwnerID = userID;
                db.saconfig_tTrgOps.Attach(saconfig_ttrgops);
                db.ObjectStateManager.ChangeObjectState(saconfig_ttrgops, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType", saconfig_ttrgops.OwnerType);
            ViewBag.LogControl = new SelectList(db.saconfig_tLogControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_ttrgops.LogControl);
            ViewBag.ReportControl = new SelectList(db.saconfig_tReportControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_ttrgops.ReportControl);
            return View(saconfig_ttrgops);
        }

        //
        // GET: /TrgOps/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTrgOps saconfig_ttrgops = db.saconfig_tTrgOps.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_ttrgops);
        }

        //
        // POST: /TrgOps/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTrgOps saconfig_ttrgops = db.saconfig_tTrgOps.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tTrgOps.DeleteObject(saconfig_ttrgops);
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