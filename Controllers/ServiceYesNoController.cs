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
    public class ServiceYesNoController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ServiceYesNo/

        public ViewResult Index(long id /*upper level element id.*/, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;

            var saconfig_tserviceyesno = db.saconfig_tServiceYesNo.Include("saconfig_ServiceYesNoElementName").Include("saconfig_ServiceYesNoOwnerType").Include("saconfig_SettingGroups").Include("saconfig_tServices");
            return View(saconfig_tserviceyesno.Where(t => t.DataOwnerID == userID && t.saconfig_ServiceYesNoElementName.ElementName == elementName &&
                ((t.saconfig_ServiceYesNoOwnerType.OwnerType == "SettingGroups" && t.SettingGroups == id) ||
                 (t.saconfig_ServiceYesNoOwnerType.OwnerType == "Services" && t.Services == id)
                 )).ToList());
        }

        //
        // GET: /ServiceYesNo/Details/5

        public ViewResult Details(long id/*Certificate id.*/, long upperLevelID, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServiceYesNo saconfig_tserviceyesno = db.saconfig_tServiceYesNo.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;

            return View(saconfig_tserviceyesno);
        }

        //
        // GET: /ServiceYesNo/Create

        public ActionResult Create(long id /*AccessPoint id.*/, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceYesNoElementName, "ID", "ElementName");
            ViewBag.SettingGroups = new SelectList(db.saconfig_SettingGroups.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");

            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;
            
            return View();
        } 

        //
        // POST: /ServiceYesNo/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServiceYesNo saconfig_tserviceyesno, long upperLevelID, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;

            if (ModelState.IsValid)
            {
                saconfig_tserviceyesno.DataOwnerID = userID;

                saconfig_ServiceYesNoOwnerType ownerTypeID;
                saconfig_ServiceYesNoElementName elName = db.saconfig_ServiceYesNoElementName.Single(s => s.ElementName == elementName);
                saconfig_tserviceyesno.Element = elName.ID;

                if (ownerType == "SettingGroups")
                {
                    saconfig_tserviceyesno.SettingGroups = upperLevelID;
                    ownerTypeID = db.saconfig_ServiceYesNoOwnerType.Single(s => s.OwnerType == "SettingGroups");
                    saconfig_tserviceyesno.OwnerType_ = ownerTypeID.ID;

                    db.saconfig_tServiceYesNo.AddObject(saconfig_tserviceyesno);
                    db.SaveChanges();
                }
                else
                    if (ownerType == "Services")
                    {
                        saconfig_tserviceyesno.Services = upperLevelID;
                        ownerTypeID = db.saconfig_ServiceYesNoOwnerType.Single(s => s.OwnerType == "Services");
                        saconfig_tserviceyesno.OwnerType_ = ownerTypeID.ID;

                        db.saconfig_tServiceYesNo.AddObject(saconfig_tserviceyesno);
                        db.SaveChanges();
                    }

                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL, elementName = elementName, ownerType = ownerType });  
            }

            ViewBag.SettingGroups = new SelectList(db.saconfig_SettingGroups.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceyesno.SettingGroups);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceyesno.Services);
            return View(saconfig_tserviceyesno);
        }
        
        //
        // GET: /ServiceYesNo/Edit/5

        public ActionResult Edit(long id, long upperLevelID, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServiceYesNo saconfig_tserviceyesno = db.saconfig_tServiceYesNo.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.SettingGroups = new SelectList(db.saconfig_SettingGroups.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceyesno.SettingGroups);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceyesno.Services);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;

            return View(saconfig_tserviceyesno);
        }

        //
        // POST: /ServiceYesNo/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServiceYesNo saconfig_tserviceyesno, long upperLevelID, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;

            if (ModelState.IsValid)
            {
                saconfig_tserviceyesno.DataOwnerID = userID;

                saconfig_ServiceYesNoOwnerType ownerTypeID;
                saconfig_ServiceYesNoElementName elName = db.saconfig_ServiceYesNoElementName.Single(s => s.ElementName == elementName);
                saconfig_tserviceyesno.Element = elName.ID;

                if (ownerType == "SettingGroups")
                {
                    saconfig_tserviceyesno.SettingGroups = upperLevelID;
                    ownerTypeID = db.saconfig_ServiceYesNoOwnerType.Single(s => s.OwnerType == "SettingGroups");
                    saconfig_tserviceyesno.OwnerType_ = ownerTypeID.ID;
                }
                else
                    if (ownerType == "Services")
                    {
                        saconfig_tserviceyesno.Services = upperLevelID;
                        ownerTypeID = db.saconfig_ServiceYesNoOwnerType.Single(s => s.OwnerType == "Services");
                        saconfig_tserviceyesno.OwnerType_ = ownerTypeID.ID;
                    }

                db.saconfig_tServiceYesNo.Attach(saconfig_tserviceyesno);
                db.ObjectStateManager.ChangeObjectState(saconfig_tserviceyesno, EntityState.Modified);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL, elementName = elementName, ownerType = ownerType });
            }

            ViewBag.SettingGroups = new SelectList(db.saconfig_SettingGroups.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceyesno.SettingGroups);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceyesno.Services);
            return View(saconfig_tserviceyesno);
        }

        //
        // GET: /ServiceYesNo/Delete/5

        public ActionResult Delete(long id, long upperLevelID, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServiceYesNo saconfig_tserviceyesno = db.saconfig_tServiceYesNo.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;

            return View(saconfig_tserviceyesno);
        }

        //
        // POST: /ServiceYesNo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long upperLevelID, string backURL, string elementName, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServiceYesNo saconfig_tserviceyesno = db.saconfig_tServiceYesNo.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServiceYesNo.DeleteObject(saconfig_tserviceyesno);
            db.SaveChanges();

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;
            ViewBag.OwnerType = ownerType;

            return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL, elementName = elementName, ownerType = ownerType });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}