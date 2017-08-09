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
    public class CertificateController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Certificate/

        public ViewResult Index(long id /*AccessPoint id.*/, string backURL, string elementName)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            ViewBag.ElementName = elementName;

            var saconfig_tcertificate = db.saconfig_tCertificate.Include("saconfig_CertificateElementName").Include("saconfig_tAccessPoint");
            return View(saconfig_tcertificate.Where(t => t.DataOwnerID == userID && t.AccessPoint == id).ToList());
        }

        //
        // GET: /Certificate/Details/5

        public ViewResult Details(long id/*Certificate id.*/, long upperLevelID, string backURL, string elementName)
        {
            Guid userID = GetUserID();
            saconfig_tCertificate saconfig_tcertificate = db.saconfig_tCertificate.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.ElementName = elementName;

            return View(saconfig_tcertificate);
        }

        //
        // GET: /Certificate/Create

        public ActionResult Create(long id /*AccessPoint id.*/, string backURL, string elementName)
        {
            Guid userID = GetUserID();
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");

            ViewBag.BackURL = backURL;
            ViewBag.UpperLevelID = id;
            ViewBag.ElementName = elementName;

            return View();
        } 

        //
        // POST: /Certificate/Create

        [HttpPost]
        public ActionResult Create(saconfig_tCertificate saconfig_tcertificate, long upperLevelID, string backURL, string elementName)
        {
            Guid userID = GetUserID();

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;

            if (ModelState.IsValid)
            {
                saconfig_tcertificate.AccessPoint = upperLevelID;
                saconfig_tcertificate.DataOwnerID = userID;

                saconfig_CertificateElementName elName = db.saconfig_CertificateElementName.Single(s => s.ElementName == elementName);
                saconfig_tcertificate.Element = elName.ID;

                db.saconfig_tCertificate.AddObject(saconfig_tcertificate);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL, elementName = elementName });  
            }

            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tcertificate.AccessPoint);
            return View(saconfig_tcertificate);
        }
        
        //
        // GET: /Certificate/Edit/5

        public ActionResult Edit(long id, long upperLevelID, string backURL, string elementName)
        {
            Guid userID = GetUserID();
            saconfig_tCertificate saconfig_tcertificate = db.saconfig_tCertificate.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tcertificate.AccessPoint);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;
            return View(saconfig_tcertificate);
        }

        //
        // POST: /Certificate/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tCertificate saconfig_tcertificate, long upperLevelID, string backURL, string elementName)
        {
            Guid userID = GetUserID();
            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;

            if (ModelState.IsValid)
            {
                saconfig_tcertificate.AccessPoint = upperLevelID;
                saconfig_tcertificate.DataOwnerID = userID;
                saconfig_CertificateElementName elName = db.saconfig_CertificateElementName.Single(s => s.ElementName == elementName);
                saconfig_tcertificate.Element = elName.ID;

                db.saconfig_tCertificate.Attach(saconfig_tcertificate);
                db.ObjectStateManager.ChangeObjectState(saconfig_tcertificate, EntityState.Modified);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL, elementName = elementName });
            }
            ViewBag.AccessPoint = new SelectList(db.saconfig_tAccessPoint.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tcertificate.AccessPoint);
            return View(saconfig_tcertificate);
        }

        //
        // GET: /Certificate/Delete/5

        public ActionResult Delete(long id, long upperLevelID, string backURL, string elementName)
        {
            Guid userID = GetUserID();
            saconfig_tCertificate saconfig_tcertificate = db.saconfig_tCertificate.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;

            return View(saconfig_tcertificate);
        }

        //
        // POST: /Certificate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long upperLevelID, string backURL, string elementName)
        {
            Guid userID = GetUserID();
            saconfig_tCertificate saconfig_tcertificate = db.saconfig_tCertificate.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tCertificate.DeleteObject(saconfig_tcertificate);
            db.SaveChanges();

            ViewBag.UpperLevelID = upperLevelID;
            ViewBag.BackURL = backURL;
            ViewBag.ElementName = elementName;

            return RedirectToAction("Index", new { id = upperLevelID, backURL = backURL, elementName = elementName });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}