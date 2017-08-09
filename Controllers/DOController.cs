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
    public class DOController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DO/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tdo = db.saconfig_tDO.Include("saconfig_tLNodeType");
            return View(saconfig_tdo.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DO/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDO saconfig_tdo = db.saconfig_tDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdo);
        }

        //
        // GET: /DO/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.LNodeType = new SelectList(db.saconfig_tLNodeType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc");
            return View();
        } 

        //
        // POST: /DO/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDO saconfig_tdo)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdo.DataOwnerID = userID;
                db.saconfig_tDO.AddObject(saconfig_tdo);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.LNodeType = new SelectList(db.saconfig_tLNodeType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tdo.LNodeType);
            return View(saconfig_tdo);
        }
        
        //
        // GET: /DO/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDO saconfig_tdo = db.saconfig_tDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.LNodeType = new SelectList(db.saconfig_tLNodeType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tdo.LNodeType);
            return View(saconfig_tdo);
        }

        //
        // POST: /DO/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDO saconfig_tdo)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdo.DataOwnerID = userID;
                db.saconfig_tDO.Attach(saconfig_tdo);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdo, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LNodeType = new SelectList(db.saconfig_tLNodeType.Where(t => t.DataOwnerID == userID).ToList(), "ID_", "desc", saconfig_tdo.LNodeType);
            return View(saconfig_tdo);
        }

        //
        // GET: /DO/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDO saconfig_tdo = db.saconfig_tDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdo);
        }

        //
        // POST: /DO/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDO saconfig_tdo = db.saconfig_tDO.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tDO.DeleteObject(saconfig_tdo);
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