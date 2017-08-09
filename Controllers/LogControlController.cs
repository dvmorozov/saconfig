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
    public class LogControlController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /LogControl/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tlogcontrol = db.saconfig_tLogControl.Include("saconfig_LogControlOwnerType").Include("saconfig_tLN").Include("saconfig_tLN0").Include("saconfig_tLNClassEnum");
            return View(saconfig_tlogcontrol.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /LogControl/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogControl saconfig_tlogcontrol = db.saconfig_tLogControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlogcontrol);
        }

        //
        // GET: /LogControl/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType");
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            return View();
        } 

        //
        // POST: /LogControl/Create

        [HttpPost]
        public ActionResult Create(saconfig_tLogControl saconfig_tlogcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlogcontrol.DataOwnerID = userID;
                db.saconfig_tLogControl.AddObject(saconfig_tlogcontrol);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType", saconfig_tlogcontrol.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlogcontrol.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlogcontrol.LN0);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tlogcontrol.lnClass);
            return View(saconfig_tlogcontrol);
        }
        
        //
        // GET: /LogControl/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogControl saconfig_tlogcontrol = db.saconfig_tLogControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType", saconfig_tlogcontrol.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlogcontrol.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlogcontrol.LN0);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tlogcontrol.lnClass);
            return View(saconfig_tlogcontrol);
        }

        //
        // POST: /LogControl/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tLogControl saconfig_tlogcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlogcontrol.DataOwnerID = userID;
                db.saconfig_tLogControl.Attach(saconfig_tlogcontrol);
                db.ObjectStateManager.ChangeObjectState(saconfig_tlogcontrol, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_LogControlOwnerType, "ID", "OwnerType", saconfig_tlogcontrol.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlogcontrol.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlogcontrol.LN0);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tlogcontrol.lnClass);
            return View(saconfig_tlogcontrol);
        }

        //
        // GET: /LogControl/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogControl saconfig_tlogcontrol = db.saconfig_tLogControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlogcontrol);
        }

        //
        // POST: /LogControl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLogControl saconfig_tlogcontrol = db.saconfig_tLogControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tLogControl.DeleteObject(saconfig_tlogcontrol);
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