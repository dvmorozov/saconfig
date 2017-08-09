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
    public class InputsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Inputs/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tinputs = db.saconfig_tInputs.Include("saconfig_InputsOwnerType").Include("saconfig_tLN").Include("saconfig_tLN0");
            return View(saconfig_tinputs.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Inputs/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tInputs saconfig_tinputs = db.saconfig_tInputs.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tinputs);
        }

        //
        // GET: /Inputs/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_InputsOwnerType, "ID", "OwnerType");
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Inputs/Create

        [HttpPost]
        public ActionResult Create(saconfig_tInputs saconfig_tinputs)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tinputs.DataOwnerID = userID;
                db.saconfig_tInputs.AddObject(saconfig_tinputs);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_InputsOwnerType, "ID", "OwnerType", saconfig_tinputs.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tinputs.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tinputs.LN0);
            return View(saconfig_tinputs);
        }
        
        //
        // GET: /Inputs/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tInputs saconfig_tinputs = db.saconfig_tInputs.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_InputsOwnerType, "ID", "OwnerType", saconfig_tinputs.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tinputs.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tinputs.LN0);
            return View(saconfig_tinputs);
        }

        //
        // POST: /Inputs/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tInputs saconfig_tinputs)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tinputs.DataOwnerID = userID;
                db.saconfig_tInputs.Attach(saconfig_tinputs);
                db.ObjectStateManager.ChangeObjectState(saconfig_tinputs, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_InputsOwnerType, "ID", "OwnerType", saconfig_tinputs.OwnerType);
            ViewBag.LN = new SelectList(db.saconfig_tLN.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tinputs.LN);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tinputs.LN0);
            return View(saconfig_tinputs);
        }

        //
        // GET: /Inputs/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tInputs saconfig_tinputs = db.saconfig_tInputs.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tinputs);
        }

        //
        // POST: /Inputs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tInputs saconfig_tinputs = db.saconfig_tInputs.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tInputs.DeleteObject(saconfig_tinputs);
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