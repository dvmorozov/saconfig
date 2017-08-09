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
    public class SubFunctionController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SubFunction/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsubfunction = db.saconfig_tSubFunction.Include("saconfig_tFunction");
            return View(saconfig_tsubfunction.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SubFunction/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubFunction saconfig_tsubfunction = db.saconfig_tSubFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsubfunction);
        }

        //
        // GET: /SubFunction/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            return View();
        } 

        //
        // POST: /SubFunction/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSubFunction saconfig_tsubfunction)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsubfunction.DataOwnerID = userID;
                db.saconfig_tSubFunction.AddObject(saconfig_tsubfunction);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList() , "ID", "type", saconfig_tsubfunction.Function);
            return View(saconfig_tsubfunction);
        }
        
        //
        // GET: /SubFunction/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubFunction saconfig_tsubfunction = db.saconfig_tSubFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Function = new SelectList(db.saconfig_tFunction, "ID", "type", saconfig_tsubfunction.Function);
            return View(saconfig_tsubfunction);
        }

        //
        // POST: /SubFunction/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSubFunction saconfig_tsubfunction)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsubfunction.DataOwnerID = userID;
                db.saconfig_tSubFunction.Attach(saconfig_tsubfunction);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsubfunction, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.Function = new SelectList(db.saconfig_tFunction.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_tsubfunction.Function);
            return View(saconfig_tsubfunction);
        }

        //
        // GET: /SubFunction/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubFunction saconfig_tsubfunction = db.saconfig_tSubFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsubfunction);
        }

        //
        // POST: /SubFunction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubFunction saconfig_tsubfunction = db.saconfig_tSubFunction.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSubFunction.DeleteObject(saconfig_tsubfunction);
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