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
    public class ServiceWithOptionalMaxController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ServiceWithOptionalMax/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tservicewithoptionalmax = db.saconfig_tServiceWithOptionalMax.Include("saconfig_ServiceWithOptionalMaxElementName").Include("saconfig_tServices");
            return View(saconfig_tservicewithoptionalmax.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ServiceWithOptionalMax/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithOptionalMax saconfig_tservicewithoptionalmax = db.saconfig_tServiceWithOptionalMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tservicewithoptionalmax);
        }

        //
        // GET: /ServiceWithOptionalMax/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithOptionalMaxElementName, "ID", "ElementName");
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /ServiceWithOptionalMax/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServiceWithOptionalMax saconfig_tservicewithoptionalmax)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tservicewithoptionalmax.DataOwnerID = userID;
                db.saconfig_tServiceWithOptionalMax.AddObject(saconfig_tservicewithoptionalmax);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithOptionalMaxElementName, "ID", "ElementName", saconfig_tservicewithoptionalmax.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithoptionalmax.Services);
            return View(saconfig_tservicewithoptionalmax);
        }
        
        //
        // GET: /ServiceWithOptionalMax/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithOptionalMax saconfig_tservicewithoptionalmax = db.saconfig_tServiceWithOptionalMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithOptionalMaxElementName, "ID", "ElementName", saconfig_tservicewithoptionalmax.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithoptionalmax.Services);
            return View(saconfig_tservicewithoptionalmax);
        }

        //
        // POST: /ServiceWithOptionalMax/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServiceWithOptionalMax saconfig_tservicewithoptionalmax)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tservicewithoptionalmax.DataOwnerID = userID;
                db.saconfig_tServiceWithOptionalMax.Attach(saconfig_tservicewithoptionalmax);
                db.ObjectStateManager.ChangeObjectState(saconfig_tservicewithoptionalmax, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithOptionalMaxElementName, "ID", "ElementName", saconfig_tservicewithoptionalmax.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithoptionalmax.Services);
            return View(saconfig_tservicewithoptionalmax);
        }

        //
        // GET: /ServiceWithOptionalMax/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithOptionalMax saconfig_tservicewithoptionalmax = db.saconfig_tServiceWithOptionalMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tservicewithoptionalmax);
        }

        //
        // POST: /ServiceWithOptionalMax/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithOptionalMax saconfig_tservicewithoptionalmax = db.saconfig_tServiceWithOptionalMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServiceWithOptionalMax.DeleteObject(saconfig_tservicewithoptionalmax);
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