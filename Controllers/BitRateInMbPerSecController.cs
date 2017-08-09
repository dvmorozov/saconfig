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
    public class BitRateInMbPerSecController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /BitRate/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tbitrateinmbpersec = db.saconfig_tBitRateInMbPerSec.Include("saconfig_tSubNetwork").Include("saconfig_tUnitMultiplierEnum");
            return View(saconfig_tbitrateinmbpersec.ToList());
        }

        //
        // GET: /BitRate/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBitRateInMbPerSec saconfig_tbitrateinmbpersec = db.saconfig_tBitRateInMbPerSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tbitrateinmbpersec);
        }

        //
        // GET: /BitRate/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /BitRate/Create

        [HttpPost]
        public ActionResult Create(saconfig_tBitRateInMbPerSec saconfig_tbitrateinmbpersec)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tbitrateinmbpersec.DataOwnerID = userID;
                db.saconfig_tBitRateInMbPerSec.AddObject(saconfig_tbitrateinmbpersec);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbitrateinmbpersec.SubNetwork);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tbitrateinmbpersec.multiplier);
            return View(saconfig_tbitrateinmbpersec);
        }
        
        //
        // GET: /BitRate/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBitRateInMbPerSec saconfig_tbitrateinmbpersec = db.saconfig_tBitRateInMbPerSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbitrateinmbpersec.SubNetwork);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tbitrateinmbpersec.multiplier);
            return View(saconfig_tbitrateinmbpersec);
        }

        //
        // POST: /BitRate/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tBitRateInMbPerSec saconfig_tbitrateinmbpersec)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tbitrateinmbpersec.DataOwnerID = userID;
                db.saconfig_tBitRateInMbPerSec.Attach(saconfig_tbitrateinmbpersec);
                db.ObjectStateManager.ChangeObjectState(saconfig_tbitrateinmbpersec, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tbitrateinmbpersec.SubNetwork);
            ViewBag.multiplier = new SelectList(db.saconfig_tUnitMultiplierEnum, "ID", "value", saconfig_tbitrateinmbpersec.multiplier);
            return View(saconfig_tbitrateinmbpersec);
        }

        //
        // GET: /BitRate/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBitRateInMbPerSec saconfig_tbitrateinmbpersec = db.saconfig_tBitRateInMbPerSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tbitrateinmbpersec);
        }

        //
        // POST: /BitRate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tBitRateInMbPerSec saconfig_tbitrateinmbpersec = db.saconfig_tBitRateInMbPerSec.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tBitRateInMbPerSec.DeleteObject(saconfig_tbitrateinmbpersec);
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