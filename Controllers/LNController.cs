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
    public class LNController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /LN/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tln = db.saconfig_tLN.Include("saconfig_LNOwnerType").Include("saconfig_tAccessPoint").Include("saconfig_tLDevice").Include("saconfig_tLNClassEnum");
            return View(saconfig_tln.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /LN/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN saconfig_tln = db.saconfig_tLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tln);
        }

        //
        // GET: /LN/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_LNOwnerType, "ID", "OwnerType");
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            return View();
        } 

        //
        // POST: /LN/Create

        [HttpPost]
        public ActionResult Create(saconfig_tLN saconfig_tln)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tln.DataOwnerID = userID;
                db.saconfig_tLN.AddObject(saconfig_tln);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_LNOwnerType, "ID", "OwnerType", saconfig_tln.OwnerType);
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln.AccessPoint);
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln.LDevice);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tln.lnClass);
            return View(saconfig_tln);
        }
        
        //
        // GET: /LN/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN saconfig_tln = db.saconfig_tLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_LNOwnerType, "ID", "OwnerType", saconfig_tln.OwnerType);
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln.AccessPoint);
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln.LDevice);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tln.lnClass);
            return View(saconfig_tln);
        }

        //
        // POST: /LN/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tLN saconfig_tln)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tln.DataOwnerID = userID;
                db.saconfig_tLN.Attach(saconfig_tln);
                db.ObjectStateManager.ChangeObjectState(saconfig_tln, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_LNOwnerType, "ID", "OwnerType", saconfig_tln.OwnerType);
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln.AccessPoint);
            ViewBag.LDevice = new SelectList(db.saconfig_tLDevice.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tln.LDevice);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tln.lnClass);
            return View(saconfig_tln);
        }

        //
        // GET: /LN/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN saconfig_tln = db.saconfig_tLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tln);
        }

        //
        // POST: /LN/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLN saconfig_tln = db.saconfig_tLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tLN.DeleteObject(saconfig_tln);
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