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
    public class DAIController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DAI/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tdai = db.saconfig_tDAI.Include("saconfig_DAIOwnerType").Include("saconfig_tAttributeNameEnum").Include("saconfig_tDOI").Include("saconfig_tSDI").Include("saconfig_tValKindEnum");
            return View(saconfig_tdai.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DAI/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAI saconfig_tdai = db.saconfig_tDAI.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdai);
        }

        //
        // GET: /DAI/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_DAIOwnerType, "ID", "OwnerType");
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.SDI = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /DAI/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDAI saconfig_tdai)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdai.DataOwnerID = userID;
                db.saconfig_tDAI.AddObject(saconfig_tdai);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_DAIOwnerType, "ID", "OwnerType", saconfig_tdai.OwnerType);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tdai.name);
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdai.DOI);
            ViewBag.SDI = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdai.SDI);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tdai.valKind);
            return View(saconfig_tdai);
        }
        
        //
        // GET: /DAI/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAI saconfig_tdai = db.saconfig_tDAI.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_DAIOwnerType, "ID", "OwnerType", saconfig_tdai.OwnerType);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tdai.name);
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdai.DOI);
            ViewBag.SDI = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdai.SDI);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tdai.valKind);
            return View(saconfig_tdai);
        }

        //
        // POST: /DAI/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDAI saconfig_tdai)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdai.DataOwnerID = userID;
                db.saconfig_tDAI.Attach(saconfig_tdai);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdai, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_DAIOwnerType, "ID", "OwnerType", saconfig_tdai.OwnerType);
            ViewBag.name = new SelectList(db.saconfig_tAttributeNameEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tdai.name);
            ViewBag.DOI = new SelectList(db.saconfig_tDOI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdai.DOI);
            ViewBag.SDI = new SelectList(db.saconfig_tSDI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdai.SDI);
            ViewBag.valKind = new SelectList(db.saconfig_tValKindEnum, "ID", "value", saconfig_tdai.valKind);
            return View(saconfig_tdai);
        }

        //
        // GET: /DAI/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAI saconfig_tdai = db.saconfig_tDAI.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdai);
        }

        //
        // POST: /DAI/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAI saconfig_tdai = db.saconfig_tDAI.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tDAI.DeleteObject(saconfig_tdai);
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