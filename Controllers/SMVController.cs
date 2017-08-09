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
    public class SMVController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SMV/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsmv = db.saconfig_tSMV.Include("saconfig_tConnectedAP");
            return View(saconfig_tsmv.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SMV/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSMV saconfig_tsmv = db.saconfig_tSMV.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsmv);
        }

        //
        // GET: /SMV/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /SMV/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSMV saconfig_tsmv)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsmv.DataOwnerID = userID;
                db.saconfig_tSMV.AddObject(saconfig_tsmv);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsmv.ConnectedAP);
            return View(saconfig_tsmv);
        }
        
        //
        // GET: /SMV/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSMV saconfig_tsmv = db.saconfig_tSMV.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsmv.ConnectedAP);
            return View(saconfig_tsmv);
        }

        //
        // POST: /SMV/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSMV saconfig_tsmv)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsmv.DataOwnerID = userID;
                db.saconfig_tSMV.Attach(saconfig_tsmv);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsmv, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsmv.ConnectedAP);
            return View(saconfig_tsmv);
        }

        //
        // GET: /SMV/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSMV saconfig_tsmv = db.saconfig_tSMV.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsmv);
        }

        //
        // POST: /SMV/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSMV saconfig_tsmv = db.saconfig_tSMV.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSMV.DeleteObject(saconfig_tsmv);
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