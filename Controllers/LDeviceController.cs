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
    public class LDeviceController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /LDevice/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tldevice = db.saconfig_tLDevice.Include("saconfig_tServer");
            return View(saconfig_tldevice.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /LDevice/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLDevice saconfig_tldevice = db.saconfig_tLDevice.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tldevice);
        }

        //
        // GET: /LDevice/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.tServer = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /LDevice/Create

        [HttpPost]
        public ActionResult Create(saconfig_tLDevice saconfig_tldevice)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tldevice.DataOwnerID = userID;
                db.saconfig_tLDevice.AddObject(saconfig_tldevice);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.tServer = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tldevice.tServer);
            return View(saconfig_tldevice);
        }
        
        //
        // GET: /LDevice/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLDevice saconfig_tldevice = db.saconfig_tLDevice.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.tServer = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tldevice.tServer);
            return View(saconfig_tldevice);
        }

        //
        // POST: /LDevice/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tLDevice saconfig_tldevice)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tldevice.DataOwnerID = userID;
                db.saconfig_tLDevice.Attach(saconfig_tldevice);
                db.ObjectStateManager.ChangeObjectState(saconfig_tldevice, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tServer = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tldevice.tServer);
            return View(saconfig_tldevice);
        }

        //
        // GET: /LDevice/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLDevice saconfig_tldevice = db.saconfig_tLDevice.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tldevice);
        }

        //
        // POST: /LDevice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLDevice saconfig_tldevice = db.saconfig_tLDevice.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tLDevice.DeleteObject(saconfig_tldevice);
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