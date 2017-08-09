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
    public class PhysConnController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /PhysConn/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tphysconn = db.saconfig_tPhysConn.Include("saconfig_tConnectedAP");
            return View(saconfig_tphysconn.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /PhysConn/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPhysConn saconfig_tphysconn = db.saconfig_tPhysConn.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tphysconn);
        }

        //
        // GET: /PhysConn/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /PhysConn/Create

        [HttpPost]
        public ActionResult Create(saconfig_tPhysConn saconfig_tphysconn)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tphysconn.DataOwnerID = userID;
                db.saconfig_tPhysConn.AddObject(saconfig_tphysconn);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tphysconn.ConnectedAP);
            return View(saconfig_tphysconn);
        }
        
        //
        // GET: /PhysConn/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPhysConn saconfig_tphysconn = db.saconfig_tPhysConn.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tphysconn.ConnectedAP);
            return View(saconfig_tphysconn);
        }

        //
        // POST: /PhysConn/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tPhysConn saconfig_tphysconn)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tphysconn.DataOwnerID = userID;
                db.saconfig_tPhysConn.Attach(saconfig_tphysconn);
                db.ObjectStateManager.ChangeObjectState(saconfig_tphysconn, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tphysconn.ConnectedAP);
            return View(saconfig_tphysconn);
        }

        //
        // GET: /PhysConn/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPhysConn saconfig_tphysconn = db.saconfig_tPhysConn.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tphysconn);
        }

        //
        // POST: /PhysConn/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tPhysConn saconfig_tphysconn = db.saconfig_tPhysConn.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tPhysConn.DeleteObject(saconfig_tphysconn);
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