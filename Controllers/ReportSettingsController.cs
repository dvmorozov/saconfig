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
    public class ReportSettingsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ReportSettings/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_treportsettings = db.saconfig_tReportSettings.Include("saconfig_tServices").Include("saconfig_tServiceSettingsEnum").Include("saconfig_tServiceSettingsEnum1").Include("saconfig_tServiceSettingsEnum2").Include("saconfig_tServiceSettingsEnum3").Include("saconfig_tServiceSettingsEnum4");
            return View(saconfig_treportsettings.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ReportSettings/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportSettings saconfig_treportsettings = db.saconfig_tReportSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_treportsettings);
        }

        //
        // GET: /ReportSettings/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.bufTime = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            ViewBag.rptID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /ReportSettings/Create

        [HttpPost]
        public ActionResult Create(saconfig_tReportSettings saconfig_treportsettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_treportsettings.DataOwnerID = userID;
                db.saconfig_tReportSettings.AddObject(saconfig_treportsettings);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_treportsettings.Services);
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.intgPd);
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.optFields);
            ViewBag.bufTime = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.bufTime);
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.trgOps);
            ViewBag.rptID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.rptID);
            return View(saconfig_treportsettings);
        }
        
        //
        // GET: /ReportSettings/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportSettings saconfig_treportsettings = db.saconfig_tReportSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_treportsettings.Services);
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.intgPd);
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.optFields);
            ViewBag.bufTime = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.bufTime);
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.trgOps);
            ViewBag.rptID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.rptID);
            return View(saconfig_treportsettings);
        }

        //
        // POST: /ReportSettings/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tReportSettings saconfig_treportsettings)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_treportsettings.DataOwnerID = userID;
                db.saconfig_tReportSettings.Attach(saconfig_treportsettings);
                db.ObjectStateManager.ChangeObjectState(saconfig_treportsettings, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_treportsettings.Services);
            ViewBag.intgPd = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.intgPd);
            ViewBag.optFields = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.optFields);
            ViewBag.bufTime = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.bufTime);
            ViewBag.trgOps = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.trgOps);
            ViewBag.rptID = new SelectList(db.saconfig_tServiceSettingsEnum, "ID", "value", saconfig_treportsettings.rptID);
            return View(saconfig_treportsettings);
        }

        //
        // GET: /ReportSettings/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportSettings saconfig_treportsettings = db.saconfig_tReportSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_treportsettings);
        }

        //
        // POST: /ReportSettings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tReportSettings saconfig_treportsettings = db.saconfig_tReportSettings.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tReportSettings.DeleteObject(saconfig_treportsettings);
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