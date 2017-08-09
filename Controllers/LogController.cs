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
    public class LogController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Log/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tlog = db.saconfig_tLog.Include("saconfig_LogOwnerType").Include("saconfig_tLN").Include("saconfig_tLN0");
            return View(saconfig_tlog.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Log/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLog saconfig_tlog = db.saconfig_tLog.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlog);
        }

        //
        // GET: /Log/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_LogOwnerType, "ID", "OwnerType");
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Log/Create

        [HttpPost]
        public ActionResult Create(saconfig_tLog saconfig_tlog)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlog.DataOwnerID = userID;
                db.saconfig_tLog.AddObject(saconfig_tlog);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_LogOwnerType, "ID", "OwnerType", saconfig_tlog.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlog.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlog.LN0);
            return View(saconfig_tlog);
        }
        
        //
        // GET: /Log/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLog saconfig_tlog = db.saconfig_tLog.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_LogOwnerType, "ID", "OwnerType", saconfig_tlog.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlog.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlog.LN0);
            return View(saconfig_tlog);
        }

        //
        // POST: /Log/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tLog saconfig_tlog)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlog.DataOwnerID = userID;
                db.saconfig_tLog.Attach(saconfig_tlog);
                db.ObjectStateManager.ChangeObjectState(saconfig_tlog, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_LogOwnerType, "ID", "OwnerType", saconfig_tlog.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlog.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlog.LN0);
            return View(saconfig_tlog);
        }

        //
        // GET: /Log/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLog saconfig_tlog = db.saconfig_tLog.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlog);
        }

        //
        // POST: /Log/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLog saconfig_tlog = db.saconfig_tLog.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tLog.DeleteObject(saconfig_tlog);
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