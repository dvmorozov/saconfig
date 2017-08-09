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
    public class AccessControlController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /AccessControl/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_taccesscontrol = db.saconfig_tAccessControl.Include("saconfig_tLDevice");
            return View(saconfig_taccesscontrol.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /AccessControl/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAccessControl saconfig_taccesscontrol = db.saconfig_tAccessControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_taccesscontrol);
        }

        //
        // GET: /AccessControl/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /AccessControl/Create

        [HttpPost]
        public ActionResult Create(saconfig_tAccessControl saconfig_taccesscontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_taccesscontrol.DataOwnerID = userID;
                db.saconfig_tAccessControl.AddObject(saconfig_taccesscontrol);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taccesscontrol.LDevice);
            return View(saconfig_taccesscontrol);
        }
        
        //
        // GET: /AccessControl/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAccessControl saconfig_taccesscontrol = db.saconfig_tAccessControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taccesscontrol.LDevice);
            return View(saconfig_taccesscontrol);
        }

        //
        // POST: /AccessControl/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tAccessControl saconfig_taccesscontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_taccesscontrol.DataOwnerID = userID;
                db.saconfig_tAccessControl.Attach(saconfig_taccesscontrol);
                db.ObjectStateManager.ChangeObjectState(saconfig_taccesscontrol, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taccesscontrol.LDevice);
            return View(saconfig_taccesscontrol);
        }

        //
        // GET: /AccessControl/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAccessControl saconfig_taccesscontrol = db.saconfig_tAccessControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_taccesscontrol);
        }

        //
        // POST: /AccessControl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAccessControl saconfig_taccesscontrol = db.saconfig_tAccessControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tAccessControl.DeleteObject(saconfig_taccesscontrol);
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