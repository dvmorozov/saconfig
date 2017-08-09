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
    public class EnumTypeController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /EnumType/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tenumtype = db.saconfig_tEnumType.Include("saconfig_tDataTypeTemplates");
            return View(saconfig_tenumtype.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /EnumType/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumType saconfig_tenumtype = db.saconfig_tEnumType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            return View(saconfig_tenumtype);
        }

        //
        // GET: /EnumType/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /EnumType/Create

        [HttpPost]
        public ActionResult Create(saconfig_tEnumType saconfig_tenumtype)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tenumtype.DataOwnerID = userID;
                db.saconfig_tEnumType.AddObject(saconfig_tenumtype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tenumtype.DataTypeTemplates);
            return View(saconfig_tenumtype);
        }
        
        //
        // GET: /EnumType/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumType saconfig_tenumtype = db.saconfig_tEnumType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tenumtype.DataTypeTemplates);
            return View(saconfig_tenumtype);
        }

        //
        // POST: /EnumType/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tEnumType saconfig_tenumtype)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tenumtype.DataOwnerID = userID;
                db.saconfig_tEnumType.Attach(saconfig_tenumtype);
                db.ObjectStateManager.ChangeObjectState(saconfig_tenumtype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DataTypeTemplates = new SelectList(db.saconfig_tDataTypeTemplates.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tenumtype.DataTypeTemplates);
            return View(saconfig_tenumtype);
        }

        //
        // GET: /EnumType/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumType saconfig_tenumtype = db.saconfig_tEnumType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            return View(saconfig_tenumtype);
        }

        //
        // POST: /EnumType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tEnumType saconfig_tenumtype = db.saconfig_tEnumType.Single(s => s.ID_ == id && s.DataOwnerID == userID);
            db.saconfig_tEnumType.DeleteObject(saconfig_tenumtype);
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