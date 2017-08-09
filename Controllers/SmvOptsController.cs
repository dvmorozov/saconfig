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
    public class SmvOptsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SmvOpts/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_smvopts = db.saconfig_SmvOpts.Include("saconfig_tSampledValueControl");
            return View(saconfig_smvopts.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SmvOpts/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_SmvOpts saconfig_smvopts = db.saconfig_SmvOpts.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_smvopts);
        }

        //
        // GET: /SmvOpts/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /SmvOpts/Create

        [HttpPost]
        public ActionResult Create(saconfig_SmvOpts saconfig_smvopts)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_smvopts.DataOwnerID = userID;
                db.saconfig_SmvOpts.AddObject(saconfig_smvopts);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_smvopts.SampledValueControl);
            return View(saconfig_smvopts);
        }
        
        //
        // GET: /SmvOpts/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_SmvOpts saconfig_smvopts = db.saconfig_SmvOpts.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_smvopts.SampledValueControl);
            return View(saconfig_smvopts);
        }

        //
        // POST: /SmvOpts/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_SmvOpts saconfig_smvopts)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_smvopts.DataOwnerID = userID;
                db.saconfig_SmvOpts.Attach(saconfig_smvopts);
                db.ObjectStateManager.ChangeObjectState(saconfig_smvopts, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SampledValueControl = new SelectList(db.saconfig_tSampledValueControl.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_smvopts.SampledValueControl);
            return View(saconfig_smvopts);
        }

        //
        // GET: /SmvOpts/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_SmvOpts saconfig_smvopts = db.saconfig_SmvOpts.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_smvopts);
        }

        //
        // POST: /SmvOpts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_SmvOpts saconfig_smvopts = db.saconfig_SmvOpts.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_SmvOpts.DeleteObject(saconfig_smvopts);
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