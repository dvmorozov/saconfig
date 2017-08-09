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
    public class ServiceConfReportControlController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ServiceConfReportControl/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tserviceconfreportcontrol = db.saconfig_tServiceConfReportControl.Include("saconfig_tServiceConfReportControlBufModeEnum").Include("saconfig_tServiceConfReportControlElementName").Include("saconfig_tServices");
            return View(saconfig_tserviceconfreportcontrol.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ServiceConfReportControl/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceConfReportControl saconfig_tserviceconfreportcontrol = db.saconfig_tServiceConfReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tserviceconfreportcontrol);
        }

        //
        // GET: /ServiceConfReportControl/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.bufMode = new SelectList(db.saconfig_tServiceConfReportControlBufModeEnum, "ID", "value");
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceConfReportControlElementName, "ID", "ElementName");
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /ServiceConfReportControl/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServiceConfReportControl saconfig_tserviceconfreportcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tserviceconfreportcontrol.DataOwnerID = userID;
                db.saconfig_tServiceConfReportControl.AddObject(saconfig_tserviceconfreportcontrol);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.bufMode = new SelectList(db.saconfig_tServiceConfReportControlBufModeEnum, "ID", "value", saconfig_tserviceconfreportcontrol.bufMode);
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceConfReportControlElementName, "ID", "ElementName", saconfig_tserviceconfreportcontrol.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceconfreportcontrol.Services);
            return View(saconfig_tserviceconfreportcontrol);
        }
        
        //
        // GET: /ServiceConfReportControl/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceConfReportControl saconfig_tserviceconfreportcontrol = db.saconfig_tServiceConfReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.bufMode = new SelectList(db.saconfig_tServiceConfReportControlBufModeEnum, "ID", "value", saconfig_tserviceconfreportcontrol.bufMode);
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceConfReportControlElementName, "ID", "ElementName", saconfig_tserviceconfreportcontrol.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceconfreportcontrol.Services);
            return View(saconfig_tserviceconfreportcontrol);
        }

        //
        // POST: /ServiceConfReportControl/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServiceConfReportControl saconfig_tserviceconfreportcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tserviceconfreportcontrol.DataOwnerID = userID;
                db.saconfig_tServiceConfReportControl.Attach(saconfig_tserviceconfreportcontrol);
                db.ObjectStateManager.ChangeObjectState(saconfig_tserviceconfreportcontrol, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bufMode = new SelectList(db.saconfig_tServiceConfReportControlBufModeEnum, "ID", "value", saconfig_tserviceconfreportcontrol.bufMode);
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceConfReportControlElementName, "ID", "ElementName", saconfig_tserviceconfreportcontrol.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceconfreportcontrol.Services);
            return View(saconfig_tserviceconfreportcontrol);
        }

        //
        // GET: /ServiceConfReportControl/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceConfReportControl saconfig_tserviceconfreportcontrol = db.saconfig_tServiceConfReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tserviceconfreportcontrol);
        }

        //
        // POST: /ServiceConfReportControl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceConfReportControl saconfig_tserviceconfreportcontrol = db.saconfig_tServiceConfReportControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServiceConfReportControl.DeleteObject(saconfig_tserviceconfreportcontrol);
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