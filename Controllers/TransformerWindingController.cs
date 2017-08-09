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
    public class TransformerWindingController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /TransformerWinding/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_ttransformerwinding = db.saconfig_tTransformerWinding.Include("saconfig_tPowerTransformer");
            return View(saconfig_ttransformerwinding.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /TransformerWinding/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTransformerWinding saconfig_ttransformerwinding = db.saconfig_tTransformerWinding.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_ttransformerwinding);
        }

        //
        // GET: /TransformerWinding/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.PowerTransformer = new SelectList(db.saconfig_tPowerTransformer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            return View();
        } 

        //
        // POST: /TransformerWinding/Create

        [HttpPost]
        public ActionResult Create(saconfig_tTransformerWinding saconfig_ttransformerwinding)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_ttransformerwinding.DataOwnerID = userID;
                db.saconfig_tTransformerWinding.AddObject(saconfig_ttransformerwinding);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PowerTransformer = new SelectList(db.saconfig_tPowerTransformer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_ttransformerwinding.PowerTransformer);
            return View(saconfig_ttransformerwinding);
        }
        
        //
        // GET: /TransformerWinding/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTransformerWinding saconfig_ttransformerwinding = db.saconfig_tTransformerWinding.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.PowerTransformer = new SelectList(db.saconfig_tPowerTransformer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_ttransformerwinding.PowerTransformer);
            return View(saconfig_ttransformerwinding);
        }

        //
        // POST: /TransformerWinding/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tTransformerWinding saconfig_ttransformerwinding)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_ttransformerwinding.DataOwnerID = userID;
                db.saconfig_tTransformerWinding.Attach(saconfig_ttransformerwinding);
                db.ObjectStateManager.ChangeObjectState(saconfig_ttransformerwinding, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PowerTransformer = new SelectList(db.saconfig_tPowerTransformer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_ttransformerwinding.PowerTransformer);
            return View(saconfig_ttransformerwinding);
        }

        //
        // GET: /TransformerWinding/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTransformerWinding saconfig_ttransformerwinding = db.saconfig_tTransformerWinding.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_ttransformerwinding);
        }

        //
        // POST: /TransformerWinding/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTransformerWinding saconfig_ttransformerwinding = db.saconfig_tTransformerWinding.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tTransformerWinding.DeleteObject(saconfig_ttransformerwinding);
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