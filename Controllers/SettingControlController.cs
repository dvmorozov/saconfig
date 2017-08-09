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
    public class SettingControlController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SettingControl/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsettingcontrol = db.saconfig_tSettingControl.Include("saconfig_tLN0");
            return View(saconfig_tsettingcontrol.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SettingControl/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSettingControl saconfig_tsettingcontrol = db.saconfig_tSettingControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsettingcontrol);
        }

        //
        // GET: /SettingControl/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /SettingControl/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSettingControl saconfig_tsettingcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsettingcontrol.DataOwnerID = userID;
                db.saconfig_tSettingControl.AddObject(saconfig_tsettingcontrol);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsettingcontrol.LN0);
            return View(saconfig_tsettingcontrol);
        }
        
        //
        // GET: /SettingControl/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSettingControl saconfig_tsettingcontrol = db.saconfig_tSettingControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsettingcontrol.LN0);
            return View(saconfig_tsettingcontrol);
        }

        //
        // POST: /SettingControl/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSettingControl saconfig_tsettingcontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsettingcontrol.DataOwnerID = userID;
                db.saconfig_tSettingControl.Attach(saconfig_tsettingcontrol);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsettingcontrol, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsettingcontrol.LN0);
            return View(saconfig_tsettingcontrol);
        }

        //
        // GET: /SettingControl/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSettingControl saconfig_tsettingcontrol = db.saconfig_tSettingControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsettingcontrol);
        }

        //
        // POST: /SettingControl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSettingControl saconfig_tsettingcontrol = db.saconfig_tSettingControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSettingControl.DeleteObject(saconfig_tsettingcontrol);
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