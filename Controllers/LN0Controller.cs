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
    public class LN0Controller : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /LN0/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tln0 = db.saconfig_tLN0.Include("saconfig_tLDevice").Include("saconfig_tLNClassEnum");
            return View(saconfig_tln0.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /LN0/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN0 saconfig_tln0 = db.saconfig_tLN0.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tln0);
        }

        //
        // GET: /LN0/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            return View();
        } 

        //
        // POST: /LN0/Create

        [HttpPost]
        public ActionResult Create(saconfig_tLN0 saconfig_tln0)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tln0.DataOwnerID = userID;
                db.saconfig_tLN0.AddObject(saconfig_tln0);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln0.LDevice);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tln0.lnClass);
            return View(saconfig_tln0);
        }
        
        //
        // GET: /LN0/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN0 saconfig_tln0 = db.saconfig_tLN0.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln0.LDevice);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tln0.lnClass);
            return View(saconfig_tln0);
        }

        //
        // POST: /LN0/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tLN0 saconfig_tln0)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tln0.DataOwnerID = userID;
                db.saconfig_tLN0.Attach(saconfig_tln0);
                db.ObjectStateManager.ChangeObjectState(saconfig_tln0, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln0.LDevice);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tln0.lnClass);
            return View(saconfig_tln0);
        }

        //
        // GET: /LN0/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN0 saconfig_tln0 = db.saconfig_tLN0.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tln0);
        }

        //
        // POST: /LN0/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN0 saconfig_tln0 = db.saconfig_tLN0.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tLN0.DeleteObject(saconfig_tln0);
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