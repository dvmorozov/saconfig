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
    public class DataTypeTemplatesController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /DataTypeTemplates/

        public ViewResult Index(long id /*SCL id.*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            var saconfig_tdatatypetemplates = db.saconfig_tDataTypeTemplates.Include("saconfig_SCL");
            return View(saconfig_tdatatypetemplates.Where(t => t.DataOwnerID == userID && t.SCL == id).ToList());
        }

        //
        // GET: /DataTypeTemplates/Details/5

        public ViewResult Details(long id/*DataTypeTemplates id.*/, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tDataTypeTemplates saconfig_tdatatypetemplates = db.saconfig_tDataTypeTemplates.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = sclID;
            return View(saconfig_tdatatypetemplates);
        }

        //
        // GET: /DataTypeTemplates/Create

        public ActionResult Create(long id /*SCL id*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.BackURL = backURL;
            ViewBag.SCLID = id;
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version");
            return View();
        } 

        //
        // POST: /DataTypeTemplates/Create

        [HttpPost]
        public ActionResult Create(saconfig_tDataTypeTemplates saconfig_tdatatypetemplates, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tdatatypetemplates.DataOwnerID = userID;
                saconfig_tdatatypetemplates.SCL = sclID;
                db.saconfig_tDataTypeTemplates.AddObject(saconfig_tdatatypetemplates);
                db.SaveChanges();

                ViewBag.SCLID = sclID;
                ViewBag.BackURL = backURL;
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });  
            }

            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tdatatypetemplates.SCL);
            return View(saconfig_tdatatypetemplates);
        }
        
        //
        // GET: /DataTypeTemplates/Edit/5

        public ActionResult Edit(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tDataTypeTemplates saconfig_tdatatypetemplates = db.saconfig_tDataTypeTemplates.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tdatatypetemplates.SCL);

            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tdatatypetemplates);
        }

        //
        // POST: /DataTypeTemplates/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tDataTypeTemplates saconfig_tdatatypetemplates, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_tdatatypetemplates.DataOwnerID = userID;
                saconfig_tdatatypetemplates.SCL = sclID;

                db.saconfig_tDataTypeTemplates.Attach(saconfig_tdatatypetemplates);
                db.ObjectStateManager.ChangeObjectState(saconfig_tdatatypetemplates, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = sclID, backURL = backURL });
            }
            ViewBag.SCL = new SelectList(db.saconfig_SCL.Where(t => t.DataOwnerID == userID).ToList(), "ID", "version", saconfig_tdatatypetemplates.SCL);
            return View(saconfig_tdatatypetemplates);
        }

        //
        // GET: /DataTypeTemplates/Delete/5

        public ActionResult Delete(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tDataTypeTemplates saconfig_tdatatypetemplates = db.saconfig_tDataTypeTemplates.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return View(saconfig_tdatatypetemplates);
        }

        //
        // POST: /DataTypeTemplates/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long sclID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tDataTypeTemplates saconfig_tdatatypetemplates = db.saconfig_tDataTypeTemplates.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tDataTypeTemplates.DeleteObject(saconfig_tdatatypetemplates);
            db.SaveChanges();

            ViewBag.SCLID = sclID;
            ViewBag.BackURL = backURL;
            return RedirectToAction("Index", new { id = sclID, backURL = backURL });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}