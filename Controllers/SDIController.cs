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
    public class SDIController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SDI/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsdi = db.saconfig_tSDI.Include("saconfig_SDIOwnerType").Include("saconfig_tAttributeNameEnum").Include("saconfig_tDOI").Include("saconfig_tSDI2");
            return View(saconfig_tsdi.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SDI/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDI saconfig_tsdi = db.saconfig_tSDI.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsdi);
        }

        //
        // GET: /SDI/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_SDIOwnerType, "ID", "OwnerType");
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.SD = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /SDI/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSDI saconfig_tsdi)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsdi.DataOwnerID = userID;
                db.saconfig_tSDI.AddObject(saconfig_tsdi);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_SDIOwnerType, "ID", "OwnerType", saconfig_tsdi.OwnerType);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tsdi.name);
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsdi.DOI);
            ViewBag.SD = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsdi.SD);
            return View(saconfig_tsdi);
        }
        
        //
        // GET: /SDI/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDI saconfig_tsdi = db.saconfig_tSDI.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_SDIOwnerType, "ID", "OwnerType", saconfig_tsdi.OwnerType);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tsdi.name);
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsdi.DOI);
            ViewBag.SD = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsdi.SD);
            return View(saconfig_tsdi);
        }

        //
        // POST: /SDI/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSDI saconfig_tsdi)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsdi.DataOwnerID = userID;
                db.saconfig_tSDI.Attach(saconfig_tsdi);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsdi, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_SDIOwnerType, "ID", "OwnerType", saconfig_tsdi.OwnerType);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tsdi.name);
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsdi.DOI);
            ViewBag.SD = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsdi.SD);
            return View(saconfig_tsdi);
        }

        //
        // GET: /SDI/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDI saconfig_tsdi = db.saconfig_tSDI.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsdi);
        }

        //
        // POST: /SDI/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSDI saconfig_tsdi = db.saconfig_tSDI.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSDI.DeleteObject(saconfig_tsdi);
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