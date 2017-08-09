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
    public class CertController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Cert/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tcert = db.saconfig_tCert.Include("saconfig_CertElementName").Include("saconfig_tCertificate");
            return View(saconfig_tcert.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Cert/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tCert saconfig_tcert = db.saconfig_tCert.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tcert);
        }

        //
        // GET: /Cert/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ElementName = new SelectList(db.saconfig_CertElementName, "ID", "ElementName");
            ViewBag.Certificate = new SelectList(db.saconfig_tCertificate.Where(t => t.DataOwnerID == userID).ToList(), "ID", "serialNumber");
            return View();
        } 

        //
        // POST: /Cert/Create

        [HttpPost]
        public ActionResult Create(saconfig_tCert saconfig_tcert)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tcert.DataOwnerID = userID;
                db.saconfig_tCert.AddObject(saconfig_tcert);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ElementName = new SelectList(db.saconfig_CertElementName, "ID", "ElementName", saconfig_tcert.ElementName);
            ViewBag.Certificate = new SelectList(db.saconfig_tCertificate.Where(t => t.DataOwnerID == userID).ToList(), "ID", "serialNumber", saconfig_tcert.Certificate);
            return View(saconfig_tcert);
        }
        
        //
        // GET: /Cert/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tCert saconfig_tcert = db.saconfig_tCert.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ElementName = new SelectList(db.saconfig_CertElementName, "ID", "ElementName", saconfig_tcert.ElementName);
            ViewBag.Certificate = new SelectList(db.saconfig_tCertificate.Where(t => t.DataOwnerID == userID).ToList(), "ID", "serialNumber", saconfig_tcert.Certificate);
            return View(saconfig_tcert);
        }

        //
        // POST: /Cert/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tCert saconfig_tcert)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tcert.DataOwnerID = userID;
                db.saconfig_tCert.Attach(saconfig_tcert);
                db.ObjectStateManager.ChangeObjectState(saconfig_tcert, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElementName = new SelectList(db.saconfig_CertElementName, "ID", "ElementName", saconfig_tcert.ElementName);
            ViewBag.Certificate = new SelectList(db.saconfig_tCertificate.Where(t => t.DataOwnerID == userID).ToList(), "ID", "serialNumber", saconfig_tcert.Certificate);
            return View(saconfig_tcert);
        }

        //
        // GET: /Cert/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tCert saconfig_tcert = db.saconfig_tCert.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tcert);
        }

        //
        // POST: /Cert/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tCert saconfig_tcert = db.saconfig_tCert.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tCert.DeleteObject(saconfig_tcert);
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