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
    public class GSEControlController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /GSEControl/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tgsecontrol = db.saconfig_tGSEControl.Include("saconfig_tGSEControlTypeEnum").Include("saconfig_tLN0");
            return View(saconfig_tgsecontrol.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /GSEControl/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSEControl saconfig_tgsecontrol = db.saconfig_tGSEControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgsecontrol);
        }

        //
        // GET: /GSEControl/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.type = new SelectList(db.saconfig_tGSEControlTypeEnum, "ID", "value");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /GSEControl/Create

        [HttpPost]
        public ActionResult Create(saconfig_tGSEControl saconfig_tgsecontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgsecontrol.DataOwnerID = userID;
                db.saconfig_tGSEControl.AddObject(saconfig_tgsecontrol);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.type = new SelectList(db.saconfig_tGSEControlTypeEnum, "ID", "value", saconfig_tgsecontrol.type);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgsecontrol.LN0);
            return View(saconfig_tgsecontrol);
        }
        
        //
        // GET: /GSEControl/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSEControl saconfig_tgsecontrol = db.saconfig_tGSEControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.type = new SelectList(db.saconfig_tGSEControlTypeEnum, "ID", "value", saconfig_tgsecontrol.type);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgsecontrol.LN0);
            return View(saconfig_tgsecontrol);
        }

        //
        // POST: /GSEControl/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tGSEControl saconfig_tgsecontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgsecontrol.DataOwnerID = userID;
                db.saconfig_tGSEControl.Attach(saconfig_tgsecontrol);
                db.ObjectStateManager.ChangeObjectState(saconfig_tgsecontrol, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type = new SelectList(db.saconfig_tGSEControlTypeEnum, "ID", "value", saconfig_tgsecontrol.type);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgsecontrol.LN0);
            return View(saconfig_tgsecontrol);
        }

        //
        // GET: /GSEControl/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSEControl saconfig_tgsecontrol = db.saconfig_tGSEControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgsecontrol);
        }

        //
        // POST: /GSEControl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSEControl saconfig_tgsecontrol = db.saconfig_tGSEControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tGSEControl.DeleteObject(saconfig_tgsecontrol);
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