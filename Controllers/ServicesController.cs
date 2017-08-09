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
    public class ServicesController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Services/

        public ViewResult Index(long id /*owner id.*/, string backURL, string ownerType)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.IEDID = id;
            ViewBag.OwnerType = ownerType;

            var saconfig_tservices = db.saconfig_tServices.Include("saconfig_tIED").Include("saconfig_tAccessPoint").Include("saconfig_ServicesOwnerType");

            return View(saconfig_tservices.Where(t => t.DataOwnerID == userID && 
                ((t.saconfig_ServicesOwnerType.OwnerType == "IED" && t.IED == id) ||
                 (t.saconfig_ServicesOwnerType.OwnerType == "AccessPoint" && t.AccessPoint == id)
                )).ToList());
        }

        //
        // GET: /Services/Details/5

        public ViewResult Details(long id/*Services id.*/, long iedID, string backURL, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServices saconfig_tservices = db.saconfig_tServices.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.BackURL = backURL;
            ViewBag.IEDID = iedID;
            ViewBag.OwnerType = ownerType;

            return View(saconfig_tservices);
        }

        //
        // GET: /Services/Create

        public ActionResult Create(long id/*owner id*/, string backURL, string ownerType)
        {
            Guid userID = GetUserID();
            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name");

            ViewBag.BackURL = backURL;
            ViewBag.IEDID = id;
            ViewBag.OwnerType = ownerType;

            return View();
        } 

        //
        // POST: /Services/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServices saconfig_tservices, long iedID, string backURL, string ownerType)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.IEDID = iedID;
            ViewBag.OwnerType = ownerType;

            if (ModelState.IsValid)
            {
                saconfig_tservices.DataOwnerID = userID;
                saconfig_ServicesOwnerType saconfig_servicesOwnerType;

                if(ownerType == "IED")
                {
                    saconfig_tservices.IED = iedID;
                    saconfig_servicesOwnerType = db.saconfig_ServicesOwnerType.Single(s => s.OwnerType == "IED");
                    saconfig_tservices.OwnerType_ = saconfig_servicesOwnerType.ID;
                    
                    db.saconfig_tServices.AddObject(saconfig_tservices);
                    db.SaveChanges();
                }
                else
                if(ownerType == "AccessPoint")
                {
                    saconfig_tservices.AccessPoint = iedID;
                    saconfig_servicesOwnerType = db.saconfig_ServicesOwnerType.Single(s => s.OwnerType == "AccessPoint");
                    saconfig_tservices.OwnerType_ = saconfig_servicesOwnerType.ID;

                    db.saconfig_tServices.AddObject(saconfig_tservices);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", new { id = iedID, backURL = backURL, ownerType = ownerType });  
            }

            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_tservices.IED);
            return View(saconfig_tservices);
        }
        
        //
        // GET: /Services/Edit/5

        public ActionResult Edit(long id, long iedID, string backURL, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServices saconfig_tservices = db.saconfig_tServices.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_tservices.IED);

            ViewBag.IEDID = iedID;
            ViewBag.BackURL = backURL;
            ViewBag.OwnerType = ownerType;

            return View(saconfig_tservices);
        }

        //
        // POST: /Services/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServices saconfig_tservices, long iedID, string backURL, string ownerType)
        {
            Guid userID = GetUserID();

            ViewBag.IEDID = iedID;
            ViewBag.BackURL = backURL;
            ViewBag.OwnerType = ownerType;

            if (ModelState.IsValid)
            {
                saconfig_tservices.DataOwnerID = userID;

                saconfig_ServicesOwnerType saconfig_servicesOwnerType;
                switch (ownerType)
                {
                    case "IED":
                        saconfig_tservices.IED = iedID;
                        saconfig_servicesOwnerType = db.saconfig_ServicesOwnerType.Single(s => s.OwnerType == "IED");
                        saconfig_tservices.OwnerType_ = saconfig_servicesOwnerType.ID;
                        break;
                    case "AccessPoint":
                        saconfig_tservices.AccessPoint = iedID;
                        saconfig_servicesOwnerType = db.saconfig_ServicesOwnerType.Single(s => s.OwnerType == "AccessPoint");
                        saconfig_tservices.OwnerType_ = saconfig_servicesOwnerType.ID;
                        break;
                }

                db.saconfig_tServices.Attach(saconfig_tservices);
                db.ObjectStateManager.ChangeObjectState(saconfig_tservices, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = iedID, backURL = backURL, ownerType = ownerType });
            }
            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_tservices.IED);
            return View(saconfig_tservices);
        }

        //
        // GET: /Services/Delete/5

        public ActionResult Delete(long id, long iedID, string backURL, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServices saconfig_tservices = db.saconfig_tServices.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.iedID = iedID;
            ViewBag.BackURL = backURL;
            ViewBag.OwnerType = ownerType;

            return View(saconfig_tservices);
        }

        //
        // POST: /Services/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long iedID, string backURL, string ownerType)
        {
            Guid userID = GetUserID();
            saconfig_tServices saconfig_tservices = db.saconfig_tServices.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServices.DeleteObject(saconfig_tservices);
            db.SaveChanges();

            ViewBag.IEDID = iedID;
            ViewBag.BackURL = backURL;
            ViewBag.OwnerType = ownerType;

            return RedirectToAction("Index", new { id = iedID, backURL = backURL, ownerType = ownerType });
        }

        public ActionResult ServiceYesNoList(long id /*Services id.*/, long iedID, string backURL, string elementName, string ownerType)
        {
            return RedirectToAction("Index", "ServiceYesNo", new { id = id, backURL = Url.Action("Edit", "Services", new { id = id, iedID = iedID, backURL = backURL }), elementName = elementName, ownerType = ownerType });
        }

        public ActionResult SettingGroupList(long id /*Services id.*/, long iedID, string backURL)
        {
            return RedirectToAction("Index", "SettingGroups", new { id = id, backURL = Url.Action("Edit", "Services", new { id = id, iedID = iedID, backURL = backURL }) });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}