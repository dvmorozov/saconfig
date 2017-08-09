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
    public class EnumValController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /EnumVal/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tenumval = db.saconfig_tEnumVal.Include("saconfig_tEnumType");
            return View(saconfig_tenumval.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /EnumVal/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumVal saconfig_tenumval = db.saconfig_tEnumVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tenumval);
        }

        //
        // GET: /EnumVal/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.EnumType = new SelectList(db.saconfig_tEnumType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc");
            return View();
        } 

        //
        // POST: /EnumVal/Create

        [HttpPost]
        public ActionResult Create(saconfig_tEnumVal saconfig_tenumval)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tenumval.DataOwnerID = userID;
                db.saconfig_tEnumVal.AddObject(saconfig_tenumval);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.EnumType = new SelectList(db.saconfig_tEnumType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tenumval.EnumType);
            return View(saconfig_tenumval);
        }
        
        //
        // GET: /EnumVal/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumVal saconfig_tenumval = db.saconfig_tEnumVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.EnumType = new SelectList(db.saconfig_tEnumType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tenumval.EnumType);
            return View(saconfig_tenumval);
        }

        //
        // POST: /EnumVal/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tEnumVal saconfig_tenumval)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tenumval.DataOwnerID = userID;
                db.saconfig_tEnumVal.Attach(saconfig_tenumval);
                db.ObjectStateManager.ChangeObjectState(saconfig_tenumval, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnumType = new SelectList(db.saconfig_tEnumType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tenumval.EnumType);
            return View(saconfig_tenumval);
        }

        //
        // GET: /EnumVal/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumVal saconfig_tenumval = db.saconfig_tEnumVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tenumval);
        }

        //
        // POST: /EnumVal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumVal saconfig_tenumval = db.saconfig_tEnumVal.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tEnumVal.DeleteObject(saconfig_tenumval);
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