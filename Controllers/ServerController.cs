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
    public class ServerController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Server/

        public ViewResult Index(long id /*AccessPoint id.*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            var saconfig_tserver = db.saconfig_tServer.Include("saconfig_tAccessPoint");
            return View(saconfig_tserver.Where(t => t.DataOwnerID == userID && t.AccessPoint == id).ToList());
        }

        //
        // GET: /Server/Details/5

        public ViewResult Details(long id/*ServerAt id.*/, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServer saconfig_tserver = db.saconfig_tServer.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = upperLevelID;
            return View(saconfig_tserver);
        }

        //
        // GET: /Server/Create

        public ActionResult Create(long id /*AccessPoint id.*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            return View();
        } 

        //
        // POST: /Server/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServer saconfig_tserver, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tserver.DataOwnerID = userID;
                saconfig_tserver.AccessPoint = upperLevelID;

                db.saconfig_tServer.AddObject(saconfig_tserver);
                db.SaveChanges();

                ViewBag.UpperLevelID = upperLevelID;
                ViewBag.BackURL = backURL;
                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL });  
            }

            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tserver.AccessPoint);
            return View(saconfig_tserver);
        }
        
        //
        // GET: /Server/Edit/5

        public ActionResult Edit(long id, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServer saconfig_tserver = db.saconfig_tServer.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tserver.AccessPoint);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tserver);
        }

        //
        // POST: /Server/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServer saconfig_tserver, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_tserver.DataOwnerID = userID;
                saconfig_tserver.AccessPoint = upperLevelID;

                db.saconfig_tServer.Attach(saconfig_tserver);
                db.ObjectStateManager.ChangeObjectState(saconfig_tserver, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL });
            }
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tserver.AccessPoint);
            return View(saconfig_tserver);
        }

        //
        // GET: /Server/Delete/5

        public ActionResult Delete(long id, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServer saconfig_tserver = db.saconfig_tServer.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tserver);
        }

        //
        // POST: /Server/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long upperLevelID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tServer saconfig_tserver = db.saconfig_tServer.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServer.DeleteObject(saconfig_tserver);
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