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
    public class FCDAController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /FCDA/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tfcda = db.saconfig_tFCDA.Include("saconfig_tDataSet").Include("saconfig_tFCEnum");
            return View(saconfig_tfcda.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /FCDA/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFCDA saconfig_tfcda = db.saconfig_tFCDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tfcda);
        }

        //
        // GET: /FCDA/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.DataSet = new SelectList(db.saconfig_tDataSet.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value");
            return View();
        } 

        //
        // POST: /FCDA/Create

        [HttpPost]
        public ActionResult Create(saconfig_tFCDA saconfig_tfcda)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tfcda.DataOwnerID = userID;
                db.saconfig_tFCDA.AddObject(saconfig_tfcda);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DataSet = new SelectList(db.saconfig_tDataSet.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfcda.DataSet);
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value", saconfig_tfcda.fc);
            return View(saconfig_tfcda);
        }
        
        //
        // GET: /FCDA/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFCDA saconfig_tfcda = db.saconfig_tFCDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.DataSet = new SelectList(db.saconfig_tDataSet.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfcda.DataSet);
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value", saconfig_tfcda.fc);
            return View(saconfig_tfcda);
        }

        //
        // POST: /FCDA/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tFCDA saconfig_tfcda)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tfcda.DataOwnerID = userID;
                db.saconfig_tFCDA.Attach(saconfig_tfcda);
                db.ObjectStateManager.ChangeObjectState(saconfig_tfcda, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DataSet = new SelectList(db.saconfig_tDataSet.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tfcda.DataSet);
            ViewBag.fc = new SelectList(db.saconfig_tFCEnum, "ID", "value", saconfig_tfcda.fc);
            return View(saconfig_tfcda);
        }

        //
        // GET: /FCDA/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFCDA saconfig_tfcda = db.saconfig_tFCDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tfcda);
        }

        //
        // POST: /FCDA/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tFCDA saconfig_tfcda = db.saconfig_tFCDA.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tFCDA.DeleteObject(saconfig_tfcda);
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