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
    public class BDAController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /BDA/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tbda = db.saconfig_tBDA.Include("saconfig_tAttributeNameEnum").Include("saconfig_tBasicTypeEnum").Include("saconfig_tDA").Include("saconfig_tValKindEnum");
            return View(saconfig_tbda.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /BDA/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBDA saconfig_tbda = db.saconfig_tBDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tbda);
        }

        //
        // GET: /BDA/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value");
            ViewBag.DAType = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /BDA/Create

        [HttpPost]
        public ActionResult Create(saconfig_tBDA saconfig_tbda)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tbda.DataOwnerID = userID;
                db.saconfig_tBDA.AddObject(saconfig_tbda);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tbda.name);
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value", saconfig_tbda.bType);
            ViewBag.DAType = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbda.DAType);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tbda.valKind);
            return View(saconfig_tbda);
        }
        
        //
        // GET: /BDA/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBDA saconfig_tbda = db.saconfig_tBDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tbda.name);
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value", saconfig_tbda.bType);
            ViewBag.DAType = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbda.DAType);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tbda.valKind);
            return View(saconfig_tbda);
        }

        //
        // POST: /BDA/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tBDA saconfig_tbda)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tbda.DataOwnerID = userID;
                db.saconfig_tBDA.Attach(saconfig_tbda);
                db.ObjectStateManager.ChangeObjectState(saconfig_tbda, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tbda.name);
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value", saconfig_tbda.bType);
            ViewBag.DAType = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbda.DAType);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tbda.valKind);
            return View(saconfig_tbda);
        }

        //
        // GET: /BDA/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBDA saconfig_tbda = db.saconfig_tBDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tbda);
        }

        //
        // POST: /BDA/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBDA saconfig_tbda = db.saconfig_tBDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tBDA.DeleteObject(saconfig_tbda);
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