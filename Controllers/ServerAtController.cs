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
    public class ServerAtController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ServerAt/

        public ViewResult Index(long id /*AccessPoint id.*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            var saconfig_tserverat = db.saconfig_tServerAt.Include("saconfig_tAccessPoint");
            return View(saconfig_tserverat.Where(t => t.DataOwnerID == userID && t.AccessPoint == id).ToList());
        }

        //
        // GET: /ServerAt/Details/5

        public ViewResult Details(long id/*ServerAt id.*/, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServerAt saconfig_tserverat = db.saconfig_tServerAt.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = upperLevelID;
            return View(saconfig_tserverat);
        }

        //
        // GET: /ServerAt/Create

        public ActionResult Create(long id /*AccessPoint id.*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            return View();
        } 

        //
        // POST: /ServerAt/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServerAt saconfig_tserverat, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_tserverat.DataOwnerID = userID;
                saconfig_tserverat.AccessPoint = upperLevelID;

                db.saconfig_tServerAt.AddObject(saconfig_tserverat);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL });  
            }

            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tserverat.AccessPoint);
            return View(saconfig_tserverat);
        }
        
        //
        // GET: /ServerAt/Edit/5

        public ActionResult Edit(long id, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServerAt saconfig_tserverat = db.saconfig_tServerAt.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tserverat.AccessPoint);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tserverat);
        }

        //
        // POST: /ServerAt/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServerAt saconfig_tserverat, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_tserverat.DataOwnerID = userID;
                saconfig_tserverat.AccessPoint = upperLevelID;

                db.saconfig_tServerAt.Attach(saconfig_tserverat);
                db.ObjectStateManager.ChangeObjectState(saconfig_tserverat, EntityState.Modified);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL });
            }
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tserverat.AccessPoint);
            return View(saconfig_tserverat);
        }

        //
        // GET: /ServerAt/Delete/5

        public ActionResult Delete(long id, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServerAt saconfig_tserverat = db.saconfig_tServerAt.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tserverat);
        }

        //
        // POST: /ServerAt/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServerAt saconfig_tserverat = db.saconfig_tServerAt.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServerAt.DeleteObject(saconfig_tserverat);
            db.SaveChanges();

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}