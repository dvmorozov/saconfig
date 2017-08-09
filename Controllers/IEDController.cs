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
    public class IEDController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /IED/

        public ViewResult Index(long id /*SCL id.*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            var saconfig_tied = db.saconfig_tIED.Include("saconfig_SCL").Include("saconfig_tRightEnum");
            return View(saconfig_tied.Where(t => t.DataOwnerID == userID && t.SCL == id).ToList());
        }

        //
        // GET: /IED/Details/5

        public ViewResult Details(long id/*IED id.*/, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tIED saconfig_tied = db.saconfig_tIED.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = sclID;
            return View(saconfig_tied);
        }

        //
        // GET: /IED/Create

        public ActionResult Create(long id /*SCL id*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version");
            ViewBag.engRight = new SelectList(db.saconfig_tRightEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /IED/Create

        [HttpPost]
        public ActionResult Create(saconfig_tIED saconfig_tied, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tied.DataOwnerID = userID;
                saconfig_tied.SCL = sclID;
                db.saconfig_tIED.AddObject(saconfig_tied);
                db.SaveChanges();

                ViewBag.SCLID = sclID;
                ViewBag.BackURL = backURL;
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });  
            }

            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tied.SCL);
            ViewBag.engRight = new SelectList(db.saconfig_tRightEnum, "ID", "value", saconfig_tied.engRight);
            return View(saconfig_tied);
        }
        
        //
        // GET: /IED/Edit/5

        public ActionResult Edit(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tIED saconfig_tied = db.saconfig_tIED.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tied.SCL);
            ViewBag.engRight = new SelectList(db.saconfig_tRightEnum, "ID", "value", saconfig_tied.engRight);

            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            ViewBag.OwnerType = "IED";
            return View(saconfig_tied);
        }

        //
        // POST: /IED/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tIED saconfig_tied, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_tied.DataOwnerID = userID;
                saconfig_tied.SCL = sclID;

                db.saconfig_tIED.Attach(saconfig_tied);
                db.ObjectStateManager.ChangeObjectState(saconfig_tied, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });
            }
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tied.SCL);
            ViewBag.engRight = new SelectList(db.saconfig_tRightEnum, "ID", "value", saconfig_tied.engRight);
            return View(saconfig_tied);
        }

        //
        // GET: /IED/Delete/5

        public ActionResult Delete(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tIED saconfig_tied = db.saconfig_tIED.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tied);
        }

        //
        // POST: /IED/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tIED saconfig_tied = db.saconfig_tIED.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tIED.DeleteObject(saconfig_tied);
            db.SaveChanges();

            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return RedirectToAction("Index", new { id = sclID, backURL = backURL });
        }

        public ActionResult AccessPointList(long id /*IED id.*/, long sclID, string backURL/*back URL to SCL-document*/)
        {
            return RedirectToAction("Index", "AccessPoint", new { id = id, backURL = Url.Action("Edit", "IED", new { id = id, sclID = sclID, backURL = backURL }) });
        }

        public ActionResult ServicesList(long id /*IED id.*/, long sclID, string backURL/*back URL to SCL-document*/)
        {
            return RedirectToAction("Index", "Services", new { id = id, backURL = Url.Action("Edit", "IED", new { id = id, sclID = sclID, backURL = backURL }), ownerType = "IED" });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}