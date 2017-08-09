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
    public class IEDNameController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /IEDName/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_iedname = db.saconfig_IEDName.Include("saconfig_IEDNameOwnerType").Include("saconfig_tControlWithIEDName").Include("saconfig_tGSEControl").Include("saconfig_tLNClassEnum").Include("saconfig_tSampledValueControl");
            return View(saconfig_iedname.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /IEDName/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_IEDName saconfig_iedname = db.saconfig_IEDName.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_iedname);
        }

        //
        // GET: /IEDName/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_IEDNameOwnerType, "ID", "OwnerType");
            ViewBag.ControlWithIEDName = new SelectList(db.saconfig_tControlWithIEDName.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.GSEControl = new SelectList(db.saconfig_tGSEControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /IEDName/Create

        [HttpPost]
        public ActionResult Create(saconfig_IEDName saconfig_iedname)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_iedname.DataOwnerID = userID;
                db.saconfig_IEDName.AddObject(saconfig_iedname);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_IEDNameOwnerType, "ID", "OwnerType", saconfig_iedname.OwnerType);
            ViewBag.ControlWithIEDName = new SelectList(db.saconfig_tControlWithIEDName.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.ControlWithIEDName);
            ViewBag.GSEControl = new SelectList(db.saconfig_tGSEControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.GSEControl);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_iedname.lnClass);
            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.SampledValueControl);
            return View(saconfig_iedname);
        }
        
        //
        // GET: /IEDName/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_IEDName saconfig_iedname = db.saconfig_IEDName.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_IEDNameOwnerType, "ID", "OwnerType", saconfig_iedname.OwnerType);
            ViewBag.ControlWithIEDName = new SelectList(db.saconfig_tControlWithIEDName.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.ControlWithIEDName);
            ViewBag.GSEControl = new SelectList(db.saconfig_tGSEControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.GSEControl);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_iedname.lnClass);
            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.SampledValueControl);
            return View(saconfig_iedname);
        }

        //
        // POST: /IEDName/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_IEDName saconfig_iedname)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_iedname.DataOwnerID = userID;
                db.saconfig_IEDName.Attach(saconfig_iedname);
                db.ObjectStateManager.ChangeObjectState(saconfig_iedname, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_IEDNameOwnerType, "ID", "OwnerType", saconfig_iedname.OwnerType);
            ViewBag.ControlWithIEDName = new SelectList(db.saconfig_tControlWithIEDName.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.ControlWithIEDName);
            ViewBag.GSEControl = new SelectList(db.saconfig_tGSEControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.GSEControl);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_iedname.lnClass);
            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_iedname.SampledValueControl);
            return View(saconfig_iedname);
        }

        //
        // GET: /IEDName/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_IEDName saconfig_iedname = db.saconfig_IEDName.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_iedname);
        }

        //
        // POST: /IEDName/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_IEDName saconfig_iedname = db.saconfig_IEDName.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_IEDName.DeleteObject(saconfig_iedname);
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