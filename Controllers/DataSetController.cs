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
    public class DataSetController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DataSet/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tdataset = db.saconfig_tDataSet.Include("saconfig_DataSetOwnerType").Include("saconfig_tLN").Include("saconfig_tLN0");
            return View(saconfig_tdataset.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DataSet/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDataSet saconfig_tdataset = db.saconfig_tDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdataset);
        }

        //
        // GET: /DataSet/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_DataSetOwnerType, "ID", "OwnerType");
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /DataSet/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDataSet saconfig_tdataset)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdataset.DataOwnerID = userID;
                db.saconfig_tDataSet.AddObject(saconfig_tdataset);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_DataSetOwnerType, "ID", "OwnerType", saconfig_tdataset.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdataset.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdataset.LN0);
            return View(saconfig_tdataset);
        }
        
        //
        // GET: /DataSet/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDataSet saconfig_tdataset = db.saconfig_tDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_DataSetOwnerType, "ID", "OwnerType", saconfig_tdataset.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdataset.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdataset.LN0);
            return View(saconfig_tdataset);
        }

        //
        // POST: /DataSet/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDataSet saconfig_tdataset)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdataset.DataOwnerID = userID;
                db.saconfig_tDataSet.Attach(saconfig_tdataset);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdataset, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_DataSetOwnerType, "ID", "OwnerType", saconfig_tdataset.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdataset.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tdataset.LN0);
            return View(saconfig_tdataset);
        }

        //
        // GET: /DataSet/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDataSet saconfig_tdataset = db.saconfig_tDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tdataset);
        }

        //
        // POST: /DataSet/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDataSet saconfig_tdataset = db.saconfig_tDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tDataSet.DeleteObject(saconfig_tdataset);
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