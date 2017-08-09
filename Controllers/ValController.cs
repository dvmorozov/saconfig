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
    public class ValController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Val/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tval = db.saconfig_tVal.Include("saconfig_tBDA").Include("saconfig_tDA").Include("saconfig_tDAI").Include("saconfig_ValOwnerType");
            return View(saconfig_tval.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Val/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVal saconfig_tval = db.saconfig_tVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tval);
        }

        //
        // GET: /Val/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.BDA = new SelectList(db.saconfig_tBDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.DA = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.DAI = new SelectList(db.saconfig_tDAI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.OwnerType = new SelectList(db.saconfig_ValOwnerType, "ID", "OwnerType");
            return View();
        } 

        //
        // POST: /Val/Create

        [HttpPost]
        public ActionResult Create(saconfig_tVal saconfig_tval)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tval.DataOwnerID = userID;
                db.saconfig_tVal.AddObject(saconfig_tval);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.BDA = new SelectList(db.saconfig_tBDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.BDA);
            ViewBag.DA = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.DA);
            ViewBag.DAI = new SelectList(db.saconfig_tDAI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.DAI);
            ViewBag.OwnerType = new SelectList(db.saconfig_ValOwnerType, "ID", "OwnerType", saconfig_tval.OwnerType);
            return View(saconfig_tval);
        }
        
        //
        // GET: /Val/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVal saconfig_tval = db.saconfig_tVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BDA = new SelectList(db.saconfig_tBDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.BDA);
            ViewBag.DA = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.DA);
            ViewBag.DAI = new SelectList(db.saconfig_tDAI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.DAI);
            ViewBag.OwnerType = new SelectList(db.saconfig_ValOwnerType, "ID", "OwnerType", saconfig_tval.OwnerType);
            return View(saconfig_tval);
        }

        //
        // POST: /Val/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tVal saconfig_tval)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tval.DataOwnerID = userID;
                db.saconfig_tVal.Attach(saconfig_tval);
                db.ObjectStateManager.ChangeObjectState(saconfig_tval, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BDA = new SelectList(db.saconfig_tBDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.BDA);
            ViewBag.DA = new SelectList(db.saconfig_tDA.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.DA);
            ViewBag.DAI = new SelectList(db.saconfig_tDAI.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tval.DAI);
            ViewBag.OwnerType = new SelectList(db.saconfig_ValOwnerType, "ID", "OwnerType", saconfig_tval.OwnerType);
            return View(saconfig_tval);
        }

        //
        // GET: /Val/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVal saconfig_tval = db.saconfig_tVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tval);
        }

        //
        // POST: /Val/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVal saconfig_tval = db.saconfig_tVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tVal.DeleteObject(saconfig_tval);
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