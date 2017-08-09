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
    public class DAController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DA/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tda = db.saconfig_tDA.Include("saconfig_tAttributeNameEnum").Include("saconfig_tBasicTypeEnum").Include("saconfig_tDOType").Include("saconfig_tFCEnum").Include("saconfig_tValKindEnum");
            return View(saconfig_tda.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DA/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDA saconfig_tda = db.saconfig_tDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tda);
        }

        //
        // GET: /DA/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value");
            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc");
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value");
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /DA/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDA saconfig_tda)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tda.DataOwnerID = userID;
                db.saconfig_tDA.AddObject(saconfig_tda);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tda.name);
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value", saconfig_tda.bType);
            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tda.DOType);
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value", saconfig_tda.fc);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tda.valKind);
            return View(saconfig_tda);
        }
        
        //
        // GET: /DA/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDA saconfig_tda = db.saconfig_tDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tda.name);
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value", saconfig_tda.bType);
            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tda.DOType);
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value", saconfig_tda.fc);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tda.valKind);
            return View(saconfig_tda);
        }

        //
        // POST: /DA/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDA saconfig_tda)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tda.DataOwnerID = userID;
                db.saconfig_tDA.Attach(saconfig_tda);
                db.ObjectStateManager.ChangeObjectState(saconfig_tda, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tda.name);
            ViewBag.bType = new SelectList(db.saconfig_tBasicTypeEnum, "ID", "value", saconfig_tda.bType);
            ViewBag.DOType = new SelectList(db.saconfig_tDOType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tda.DOType);
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value", saconfig_tda.fc);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tda.valKind);
            return View(saconfig_tda);
        }

        //
        // GET: /DA/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDA saconfig_tda = db.saconfig_tDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tda);
        }

        //
        // POST: /DA/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDA saconfig_tda = db.saconfig_tDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tDA.DeleteObject(saconfig_tda);
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