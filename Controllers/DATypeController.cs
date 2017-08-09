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
    public class DATypeController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DAType/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tdatype = db.saconfig_tDAType.Include("saconfig_tDataTypeTemplates");
            return View(saconfig_tdatype.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /DAType/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAType saconfig_tdatype = db.saconfig_tDAType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            return View(saconfig_tdatype);
        }

        //
        // GET: /DAType/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /DAType/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDAType saconfig_tdatype)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdatype.DataOwnerID = userID;
                db.saconfig_tDAType.AddObject(saconfig_tdatype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tdatype.DataTypeTemplates);
            return View(saconfig_tdatype);
        }
        
        //
        // GET: /DAType/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAType saconfig_tdatype = db.saconfig_tDAType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tdatype.DataTypeTemplates);
            return View(saconfig_tdatype);
        }

        //
        // POST: /DAType/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDAType saconfig_tdatype)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdatype.DataOwnerID = userID;
                db.saconfig_tDAType.Attach(saconfig_tdatype);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdatype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tdatype.DataTypeTemplates);
            return View(saconfig_tdatype);
        }

        //
        // GET: /DAType/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAType saconfig_tdatype = db.saconfig_tDAType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            return View(saconfig_tdatype);
        }

        //
        // POST: /DAType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tDAType saconfig_tdatype = db.saconfig_tDAType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            db.saconfig_tDAType.DeleteObject(saconfig_tdatype);
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