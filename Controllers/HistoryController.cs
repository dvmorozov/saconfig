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
    public class HistoryController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /History/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_history = db.saconfig_History.Include("saconfig_tHeader");
            return View(saconfig_history.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /History/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_History saconfig_history = db.saconfig_History.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_history);
        }

        //
        // GET: /History/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Header = new SelectList(db.saconfig_tHeader.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "ident");
            return View();
        } 

        //
        // POST: /History/Create

        [HttpPost]
        public ActionResult Create(saconfig_History saconfig_history)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_history.DataOwnerID = userID;
                db.saconfig_History.AddObject(saconfig_history);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Header = new SelectList(db.saconfig_tHeader.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "ident", saconfig_history.Header);
            return View(saconfig_history);
        }
        
        //
        // GET: /History/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_History saconfig_history = db.saconfig_History.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Header = new SelectList(db.saconfig_tHeader.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "ident", saconfig_history.Header);
            return View(saconfig_history);
        }

        //
        // POST: /History/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_History saconfig_history)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_history.DataOwnerID = userID;
                db.saconfig_History.Attach(saconfig_history);
                db.ObjectStateManager.ChangeObjectState(saconfig_history, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Header = new SelectList(db.saconfig_tHeader.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "ident", saconfig_history.Header);
            return View(saconfig_history);
        }

        //
        // GET: /History/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_History saconfig_history = db.saconfig_History.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_history);
        }

        //
        // POST: /History/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_History saconfig_history = db.saconfig_History.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_History.DeleteObject(saconfig_history);
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