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
    public class SettingGroupsController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SettingGroups/

        public ViewResult Index(long id, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = id;

            var saconfig_settinggroups = db.saconfig_SettingGroups.Include("saconfig_tServices");
            return View(saconfig_settinggroups.Where(t => t.DataOwnerID == userID && t.Services == id).ToList());
        }

        //
        // GET: /SettingGroups/Details/5

        public ViewResult Details(long id/*SettingGroups id.*/, long servicesID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = servicesID;

            saconfig_SettingGroups saconfig_settinggroups = db.saconfig_SettingGroups.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_settinggroups);
        }

        //
        // GET: /SettingGroups/Create

        public ActionResult Create(long servicesID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = servicesID;

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /SettingGroups/Create

        [HttpPost]
        public ActionResult Create(saconfig_SettingGroups saconfig_settinggroups, long servicesID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = servicesID;

            if (ModelState.IsValid)
            {
                saconfig_settinggroups.DataOwnerID = userID;
                saconfig_settinggroups.Services = servicesID;

                db.saconfig_SettingGroups.AddObject(saconfig_settinggroups);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = servicesID, backURL = backURL });  
            }

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_settinggroups.Services);
            return View(saconfig_settinggroups);
        }
        
        //
        // GET: /SettingGroups/Edit/5

        public ActionResult Edit(long id, long servicesID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = servicesID;

            saconfig_SettingGroups saconfig_settinggroups = db.saconfig_SettingGroups.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_settinggroups.Services);
            return View(saconfig_settinggroups);
        }

        //
        // POST: /SettingGroups/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_SettingGroups saconfig_settinggroups, long servicesID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = servicesID;

            if (ModelState.IsValid)
            {
                saconfig_settinggroups.DataOwnerID = userID;
                saconfig_settinggroups.Services = servicesID;

                db.saconfig_SettingGroups.Attach(saconfig_settinggroups);
                db.ObjectStateManager.ChangeObjectState(saconfig_settinggroups, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = servicesID, backURL = backURL });
            }
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_settinggroups.Services);
            return View(saconfig_settinggroups);
        }

        //
        // GET: /SettingGroups/Delete/5

        public ActionResult Delete(long id, long servicesID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = servicesID;

            saconfig_SettingGroups saconfig_settinggroups = db.saconfig_SettingGroups.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_settinggroups);
        }

        //
        // POST: /SettingGroups/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long servicesID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.ServicesID = servicesID;

            saconfig_SettingGroups saconfig_settinggroups = db.saconfig_SettingGroups.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_SettingGroups.DeleteObject(saconfig_settinggroups);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = servicesID, backURL = backURL });
        }

        public ActionResult ServiceYesNoList(long id /*SettingGroups id.*/, long servicesID, string backURL, string elementName, string ownerType)
        {
            return RedirectToAction("Index", "ServiceYesNo", new { id = id, backURL = Url.Action("Edit", "SettingGroups", new { id = id, servicesID = servicesID, backURL = backURL }), elementName = elementName, ownerType = ownerType });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}