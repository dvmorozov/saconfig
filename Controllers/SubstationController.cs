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
    public class SubstationController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Substation/

        public ViewResult Index(long id /*SCL id.*/, string backURL)
        {
            // doesn't work when substituted directly into the expression, intermediate variable is necessary!
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            var v = db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID && t.SCL == id);
            return View(v.ToList());
        }

        //
        // GET: /Substation/Details/5

        public ViewResult Details(long id/*substation id.*/, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tSubstation saconfig_tsubstation = db.saconfig_tSubstation.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = sclID;
            return View(saconfig_tsubstation);
        }

        //
        // GET: /Substation/Create

        public ActionResult Create(long id /*SCL id*/, string backURL)
        {
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            return View();
        } 

        //
        // POST: /Substation/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSubstation saconfig_tsubstation, long sclID, string backURL)
        {
            if (ModelState.IsValid)
            {
                saconfig_tsubstation.DataOwnerID = GetUserID();
                saconfig_tsubstation.SCL = sclID;
                db.saconfig_tSubstation.AddObject(saconfig_tsubstation);
                db.SaveChanges();

                ViewBag.SCLID = sclID;
                ViewBag.BackURL = backURL;
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });  
            }

            return View(saconfig_tsubstation);
        }
        
        //
        // GET: /Substation/Edit/5

        public ActionResult Edit(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tSubstation saconfig_tsubstation = db.saconfig_tSubstation.Single(s => s.ID == id && s.DataOwnerID == userID);
           
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tsubstation);
        }

        //
        // POST: /Substation/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSubstation saconfig_tsubstation, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_tsubstation.DataOwnerID = userID;
                saconfig_tsubstation.SCL = sclID;
                db.saconfig_tSubstation.Attach(saconfig_tsubstation);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsubstation, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });
            }
            return View(saconfig_tsubstation);
        }

        //
        // GET: /Substation/Delete/5

        public ActionResult Delete(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tSubstation saconfig_tsubstation = db.saconfig_tSubstation.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tsubstation);
        }

        //
        // POST: /Substation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tSubstation saconfig_tsubstation = db.saconfig_tSubstation.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSubstation.DeleteObject(saconfig_tsubstation);
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