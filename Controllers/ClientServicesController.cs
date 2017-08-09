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
    public class ClientServicesController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ClientServices/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tclientservices = db.saconfig_tClientServices.Include("saconfig_tServices");
            return View(saconfig_tclientservices.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ClientServices/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientServices saconfig_tclientservices = db.saconfig_tClientServices.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tclientservices);
        }

        //
        // GET: /ClientServices/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /ClientServices/Create

        [HttpPost]
        public ActionResult Create(saconfig_tClientServices saconfig_tclientservices)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tclientservices.DataOwnerID = userID;
                db.saconfig_tClientServices.AddObject(saconfig_tclientservices);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tclientservices.Services);
            return View(saconfig_tclientservices);
        }
        
        //
        // GET: /ClientServices/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientServices saconfig_tclientservices = db.saconfig_tClientServices.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tclientservices.Services);
            return View(saconfig_tclientservices);
        }

        //
        // POST: /ClientServices/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tClientServices saconfig_tclientservices)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tclientservices.DataOwnerID = userID;
                db.saconfig_tClientServices.Attach(saconfig_tclientservices);
                db.ObjectStateManager.ChangeObjectState(saconfig_tclientservices, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tclientservices.Services);
            return View(saconfig_tclientservices);
        }

        //
        // GET: /ClientServices/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientServices saconfig_tclientservices = db.saconfig_tClientServices.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tclientservices);
        }

        //
        // POST: /ClientServices/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientServices saconfig_tclientservices = db.saconfig_tClientServices.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tClientServices.DeleteObject(saconfig_tclientservices);
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