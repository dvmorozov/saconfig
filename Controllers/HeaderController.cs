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
    public class HeaderController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Header/

        public ViewResult Index(long id /*SCL id.*/, string backURL)
        {
            Guid userID = GetUserID();
            var saconfig_theader = db.saconfig_tHeader.Include("saconfig_SCL");
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            return View(saconfig_theader.Where(t => t.DataOwnerID == userID && t.SCL == id).ToList());
        }

        //
        // GET: /Header/Details/5

        public ViewResult Details(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tHeader saconfig_theader = db.saconfig_tHeader.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            return View(saconfig_theader);
        }

        //
        // GET: /Header/Create

        public ActionResult Create(long id /*SCL id*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version");
            return View();
        } 

        //
        // POST: /Header/Create

        [HttpPost]
        public ActionResult Create(saconfig_tHeader saconfig_theader, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_theader.DataOwnerID = userID;
                saconfig_theader.SCL = sclID;
                db.saconfig_tHeader.AddObject(saconfig_theader);
                db.SaveChanges();
                ViewBag.SCLID = sclID;
                ViewBag.BackURL = backURL;
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });  
            }

            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_theader.SCL);
            return View(saconfig_theader);
        }
        
        //
        // GET: /Header/Edit/5

        public ActionResult Edit(long id/*header id.*/, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tHeader saconfig_theader = db.saconfig_tHeader.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_theader.SCL);
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_theader);
        }

        //
        // POST: /Header/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tHeader saconfig_theader, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_theader.DataOwnerID = userID;
                db.saconfig_tHeader.Attach(saconfig_theader);
                db.ObjectStateManager.ChangeObjectState(saconfig_theader, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });
            }
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_theader.SCL);
            return View(saconfig_theader);
        }

        //
        // GET: /Header/Delete/5
 
        public ActionResult Delete(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tHeader saconfig_theader = db.saconfig_tHeader.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_theader);
        }

        //
        // POST: /Header/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tHeader saconfig_theader = db.saconfig_tHeader.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            db.saconfig_tHeader.DeleteObject(saconfig_theader);
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