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
    public class VoltageLevelController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /VoltageLevel/

        public ViewResult Index()
        {
            var saconfig_tvoltagelevel = db.saconfig_tVoltageLevel.Include("saconfig_tSubstation");
            Guid userID = GetUserID();
            return View(saconfig_tvoltagelevel.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /VoltageLevel/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltageLevel saconfig_tvoltagelevel = db.saconfig_tVoltageLevel.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tvoltagelevel);
        }

        //
        // GET: /VoltageLevel/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /VoltageLevel/Create

        [HttpPost]
        public ActionResult Create(saconfig_tVoltageLevel saconfig_tvoltagelevel)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tvoltagelevel.DataOwnerID = userID;
                db.saconfig_tVoltageLevel.AddObject(saconfig_tvoltagelevel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tvoltagelevel.Substantion);
            return View(saconfig_tvoltagelevel);
        }
        
        //
        // GET: /VoltageLevel/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltageLevel saconfig_tvoltagelevel = db.saconfig_tVoltageLevel.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tvoltagelevel.Substantion);
            return View(saconfig_tvoltagelevel);
        }

        //
        // POST: /VoltageLevel/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tVoltageLevel saconfig_tvoltagelevel)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tvoltagelevel.DataOwnerID = userID;
                db.saconfig_tVoltageLevel.Attach(saconfig_tvoltagelevel);
                db.ObjectStateManager.ChangeObjectState(saconfig_tvoltagelevel, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.Substantion = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tvoltagelevel.Substantion);
            return View(saconfig_tvoltagelevel);
        }

        //
        // GET: /VoltageLevel/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltageLevel saconfig_tvoltagelevel = db.saconfig_tVoltageLevel.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tvoltagelevel);
        }

        //
        // POST: /VoltageLevel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tVoltageLevel saconfig_tvoltagelevel = db.saconfig_tVoltageLevel.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tVoltageLevel.DeleteObject(saconfig_tvoltagelevel);
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