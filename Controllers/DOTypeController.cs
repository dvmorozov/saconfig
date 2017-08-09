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
    public class DOTypeController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DOType/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tdotype = db.saconfig_tDOType.Include("saconfig_tCDCEnum").Include("saconfig_tDataTypeTemplates");
            return View(saconfig_tdotype.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DOType/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOType saconfig_tdotype = db.saconfig_tDOType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            return View(saconfig_tdotype);
        }

        //
        // GET: /DOType/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.cdc = new SelectList(db.saconfig_tCDCEnum.Where(t => t.DataOwnerID == userID || !t.Extended).ToList(), "ID", "value");
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /DOType/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDOType saconfig_tdotype)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdotype.DataOwnerID = userID;
                db.saconfig_tDOType.AddObject(saconfig_tdotype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.cdc = new SelectList(db.saconfig_tCDCEnum.Where(t => t.DataOwnerID == userID || !t.Extended).ToList(), "ID", "value", saconfig_tdotype.cdc);
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tdotype.DataTypeTemplates);
            return View(saconfig_tdotype);
        }
        
        //
        // GET: /DOType/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOType saconfig_tdotype = db.saconfig_tDOType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            ViewBag.cdc = new SelectList(db.saconfig_tCDCEnum.Where(t => t.DataOwnerID == userID || !t.Extended).ToList(), "ID", "value", saconfig_tdotype.cdc);
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tdotype.DataTypeTemplates);
            return View(saconfig_tdotype);
        }

        //
        // POST: /DOType/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDOType saconfig_tdotype)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdotype.DataOwnerID = userID;
                db.saconfig_tDOType.Attach(saconfig_tdotype);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdotype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cdc = new SelectList(db.saconfig_tCDCEnum.Where(t => t.DataOwnerID == userID || !t.Extended).ToList(), "ID", "value", saconfig_tdotype.cdc);
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tdotype.DataTypeTemplates);
            return View(saconfig_tdotype);
        }

        //
        // GET: /DOType/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOType saconfig_tdotype = db.saconfig_tDOType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            return View(saconfig_tdotype);
        }

        //
        // POST: /DOType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDOType saconfig_tdotype = db.saconfig_tDOType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            db.saconfig_tDOType.DeleteObject(saconfig_tdotype);
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