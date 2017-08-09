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
    public class ExtRefController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ExtRef/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_textref = db.saconfig_tExtRef.Include("saconfig_tInputs").Include("saconfig_tLNClassEnum");
            return View(saconfig_textref.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ExtRef/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tExtRef saconfig_textref = db.saconfig_tExtRef.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_textref);
        }

        //
        // GET: /ExtRef/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Inputs = new SelectList(db.saconfig_tInputs.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            return View();
        } 

        //
        // POST: /ExtRef/Create

        [HttpPost]
        public ActionResult Create(saconfig_tExtRef saconfig_textref)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_textref.DataOwnerID = userID;
                db.saconfig_tExtRef.AddObject(saconfig_textref);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Inputs = new SelectList(db.saconfig_tInputs.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_textref.Inputs);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_textref.lnClass);
            return View(saconfig_textref);
        }
        
        //
        // GET: /ExtRef/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tExtRef saconfig_textref = db.saconfig_tExtRef.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Inputs = new SelectList(db.saconfig_tInputs.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_textref.Inputs);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_textref.lnClass);
            return View(saconfig_textref);
        }

        //
        // POST: /ExtRef/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tExtRef saconfig_textref)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_textref.DataOwnerID = userID;
                db.saconfig_tExtRef.Attach(saconfig_textref);
                db.ObjectStateManager.ChangeObjectState(saconfig_textref, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Inputs = new SelectList(db.saconfig_tInputs.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_textref.Inputs);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_textref.lnClass);
            return View(saconfig_textref);
        }

        //
        // GET: /ExtRef/Delete/5
 
        public ActionResult Delete(long id)
        {
            saconfig_tExtRef saconfig_textref = db.saconfig_tExtRef.Single(s => s.ID == id);
            return View(saconfig_textref);
        }

        //
        // POST: /ExtRef/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            saconfig_tExtRef saconfig_textref = db.saconfig_tExtRef.Single(s => s.ID == id);
            db.saconfig_tExtRef.DeleteObject(saconfig_textref);
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