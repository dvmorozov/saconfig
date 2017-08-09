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
    public class SDOController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SDO/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsdo = db.saconfig_tSDO.Include("saconfig_tDOType");
            return View(saconfig_tsdo.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SDO/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDO saconfig_tsdo = db.saconfig_tSDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsdo);
        }

        //
        // GET: /SDO/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc");
            return View();
        } 

        //
        // POST: /SDO/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSDO saconfig_tsdo)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsdo.DataOwnerID = userID;
                db.saconfig_tSDO.AddObject(saconfig_tsdo);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tsdo.DOType);
            return View(saconfig_tsdo);
        }
        
        //
        // GET: /SDO/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDO saconfig_tsdo = db.saconfig_tSDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tsdo.DOType);
            return View(saconfig_tsdo);
        }

        //
        // POST: /SDO/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSDO saconfig_tsdo)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsdo.DataOwnerID = userID;
                db.saconfig_tSDO.Attach(saconfig_tsdo);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsdo, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tsdo.DOType);
            return View(saconfig_tsdo);
        }

        //
        // GET: /SDO/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDO saconfig_tsdo = db.saconfig_tSDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsdo);
        }

        //
        // POST: /SDO/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDO saconfig_tsdo = db.saconfig_tSDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSDO.DeleteObject(saconfig_tsdo);
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