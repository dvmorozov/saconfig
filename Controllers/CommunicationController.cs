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
    public class CommunicationController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Communication/

        public ViewResult Index(long id /*SCL id.*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            var saconfig_tcommunication = db.saconfig_tCommunication.Include("saconfig_SCL");
            return View(saconfig_tcommunication.Where(t => t.DataOwnerID == userID && t.SCL == id).ToList());
        }

        //
        // GET: /Communication/Details/5

        public ViewResult Details(long id/*communication id.*/, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tCommunication saconfig_tcommunication = db.saconfig_tCommunication.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = sclID;
            return View(saconfig_tcommunication);
        }

        //
        // GET: /Communication/Create

        public ActionResult Create(long id /*SCL id*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version");
            return View();
        } 

        //
        // POST: /Communication/Create

        [HttpPost]
        public ActionResult Create(saconfig_tCommunication saconfig_tcommunication, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tcommunication.DataOwnerID = userID;
                saconfig_tcommunication.SCL = sclID;
                db.saconfig_tCommunication.AddObject(saconfig_tcommunication);
                db.SaveChanges();
          
                ViewBag.SCLID = sclID;
                ViewBag.BackURL = backURL;
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });  
            }

            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tcommunication.SCL);
            return View(saconfig_tcommunication);
        }
        
        //
        // GET: /Communication/Edit/5

        public ActionResult Edit(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tCommunication saconfig_tcommunication = db.saconfig_tCommunication.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tcommunication.SCL);

            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tcommunication);
        }

        //
        // POST: /Communication/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tCommunication saconfig_tcommunication, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_tcommunication.DataOwnerID = userID;
                saconfig_tcommunication.SCL = sclID;
                db.saconfig_tCommunication.Attach(saconfig_tcommunication);
                db.ObjectStateManager.ChangeObjectState(saconfig_tcommunication, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });
            }
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tcommunication.SCL);
            return View(saconfig_tcommunication);
        }

        //
        // GET: /Communication/Delete/5

        public ActionResult Delete(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tCommunication saconfig_tcommunication = db.saconfig_tCommunication.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tcommunication);
        }

        //
        // POST: /Communication/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tCommunication saconfig_tcommunication = db.saconfig_tCommunication.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tCommunication.DeleteObject(saconfig_tcommunication);
            db.SaveChanges();

            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return RedirectToAction("Index", new { id = sclID, backURL = backURL });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}