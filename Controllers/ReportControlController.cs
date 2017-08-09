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
    public class ReportControlController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ReportControl/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_treportcontrol = db.saconfig_tReportControl.Include("saconfig_ReportControlOwnerType").Include("saconfig_tLN0").Include("saconfig_tLN");
            return View(saconfig_treportcontrol.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ReportControl/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportControl saconfig_treportcontrol = db.saconfig_tReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_treportcontrol);
        }

        //
        // GET: /ReportControl/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_ReportControlOwnerType, "ID", "OwnerType");
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /ReportControl/Create

        [HttpPost]
        public ActionResult Create(saconfig_tReportControl saconfig_treportcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_treportcontrol.DataOwnerID = userID;
                db.saconfig_tReportControl.AddObject(saconfig_treportcontrol);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_ReportControlOwnerType, "ID", "OwnerType", saconfig_treportcontrol.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_treportcontrol.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_treportcontrol.LN0);
            return View(saconfig_treportcontrol);
        }
        
        //
        // GET: /ReportControl/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportControl saconfig_treportcontrol = db.saconfig_tReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_ReportControlOwnerType, "ID", "OwnerType", saconfig_treportcontrol.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_treportcontrol.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_treportcontrol.LN0);
            return View(saconfig_treportcontrol);
        }

        //
        // POST: /ReportControl/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tReportControl saconfig_treportcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_treportcontrol.DataOwnerID = userID;
                db.saconfig_tReportControl.Attach(saconfig_treportcontrol);
                db.ObjectStateManager.ChangeObjectState(saconfig_treportcontrol, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_ReportControlOwnerType, "ID", "OwnerType", saconfig_treportcontrol.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_treportcontrol.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_treportcontrol.LN0);
            return View(saconfig_treportcontrol);
        }

        //
        // GET: /ReportControl/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportControl saconfig_treportcontrol = db.saconfig_tReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_treportcontrol);
        }

        //
        // POST: /ReportControl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportControl saconfig_treportcontrol = db.saconfig_tReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tReportControl.DeleteObject(saconfig_treportcontrol);
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