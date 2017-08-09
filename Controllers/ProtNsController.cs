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
    public class ProtNsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ProtNs/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_protns = db.saconfig_ProtNs.Include("saconfig_tDAType");
            return View(saconfig_protns.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ProtNs/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_ProtNs saconfig_protns = db.saconfig_ProtNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_protns);
        }

        //
        // GET: /ProtNs/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.DAType = new SelectList(db.saconfig_tDAType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc");
            return View();
        } 

        //
        // POST: /ProtNs/Create

        [HttpPost]
        public ActionResult Create(saconfig_ProtNs saconfig_protns)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_protns.DataOwnerID = userID;
                db.saconfig_ProtNs.AddObject(saconfig_protns);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DAType = new SelectList(db.saconfig_tDAType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_protns.DAType);
            return View(saconfig_protns);
        }
        
        //
        // GET: /ProtNs/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_ProtNs saconfig_protns = db.saconfig_ProtNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.DAType = new SelectList(db.saconfig_tDAType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_protns.DAType);
            return View(saconfig_protns);
        }

        //
        // POST: /ProtNs/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_ProtNs saconfig_protns)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_protns.DataOwnerID = userID;
                db.saconfig_ProtNs.Attach(saconfig_protns);
                db.ObjectStateManager.ChangeObjectState(saconfig_protns, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DAType = new SelectList(db.saconfig_tDAType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_protns.DAType);
            return View(saconfig_protns);
        }

        //
        // GET: /ProtNs/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_ProtNs saconfig_protns = db.saconfig_ProtNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_protns);
        }

        //
        // POST: /ProtNs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_ProtNs saconfig_protns = db.saconfig_ProtNs.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_ProtNs.DeleteObject(saconfig_protns);
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