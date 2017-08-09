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
    public class HitemController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Hitem/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_thitem = db.saconfig_tHitem.Include("saconfig_History");
            return View(saconfig_thitem.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Hitem/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tHitem saconfig_thitem = db.saconfig_tHitem.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_thitem);
        }

        //
        // GET: /Hitem/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.History = new SelectList(db.saconfig_History.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /Hitem/Create

        [HttpPost]
        public ActionResult Create(saconfig_tHitem saconfig_thitem)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_thitem.DataOwnerID = userID;
                db.saconfig_tHitem.AddObject(saconfig_thitem);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.History = new SelectList(db.saconfig_History.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_thitem.History);
            return View(saconfig_thitem);
        }
        
        //
        // GET: /Hitem/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tHitem saconfig_thitem = db.saconfig_tHitem.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.History = new SelectList(db.saconfig_History.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_thitem.History);
            return View(saconfig_thitem);
        }

        //
        // POST: /Hitem/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tHitem saconfig_thitem)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_thitem.DataOwnerID = userID;
                db.saconfig_tHitem.Attach(saconfig_thitem);
                db.ObjectStateManager.ChangeObjectState(saconfig_thitem, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.History = new SelectList(db.saconfig_History.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_thitem.History);
            return View(saconfig_thitem);
        }

        //
        // GET: /Hitem/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tHitem saconfig_thitem = db.saconfig_tHitem.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_thitem);
        }

        //
        // POST: /Hitem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tHitem saconfig_thitem = db.saconfig_tHitem.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tHitem.DeleteObject(saconfig_thitem);
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