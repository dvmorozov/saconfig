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
    public class AccessPointController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /AccessPoint/

        public ViewResult Index(long id /*IED id.*/, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.BackURL = backURL;
            ViewBag.IEDID = id;

            var saconfig_taccesspoint = db.saconfig_tAccessPoint.Include("saconfig_tIED");
            return View(saconfig_taccesspoint.Where(t => t.DataOwnerID == userID && t.IED == id).ToList());
        }

        //
        // GET: /AccessPoint/Details/5

        public ViewResult Details(long id/*AccessPoint id.*/, long iedID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tAccessPoint saconfig_taccesspoint = db.saconfig_tAccessPoint.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.BackURL = backURL;
            ViewBag.IEDID = iedID;

            return View(saconfig_taccesspoint);
        }

        //
        // GET: /AccessPoint/Create

        public ActionResult Create(long id /*IED id*/, string backURL)
        {
            Guid userID = GetUserID();
            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name");

            ViewBag.BackURL = backURL;
            ViewBag.IEDID = id;

            return View();
        } 

        //
        // POST: /AccessPoint/Create

        [HttpPost]
        public ActionResult Create(saconfig_tAccessPoint saconfig_taccesspoint, long iedID, string backURL)
        {
            Guid userID = GetUserID();
            
            ViewBag.IEDID = iedID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_taccesspoint.DataOwnerID = userID;
                saconfig_taccesspoint.IED = iedID;

                db.saconfig_tAccessPoint.AddObject(saconfig_taccesspoint);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = iedID, backURL = backURL });  
            }

            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_taccesspoint.IED);
            return View(saconfig_taccesspoint);
        }
        
        //
        // GET: /AccessPoint/Edit/5

        public ActionResult Edit(long id, long iedID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tAccessPoint saconfig_taccesspoint = db.saconfig_tAccessPoint.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_taccesspoint.IED);

            ViewBag.IEDID = iedID;
            ViewBag.BackURL = backURL;

            ViewBag.OwnerType = "AccessPoint";
            return View(saconfig_taccesspoint);
        }

        //
        // POST: /AccessPoint/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tAccessPoint saconfig_taccesspoint, long iedID, string backURL)
        {
            Guid userID = GetUserID();

            ViewBag.IEDID = iedID;
            ViewBag.BackURL = backURL;

            if (ModelState.IsValid)
            {
                saconfig_taccesspoint.DataOwnerID = userID;
                saconfig_taccesspoint.IED = iedID;

                db.saconfig_tAccessPoint.Attach(saconfig_taccesspoint);
                db.ObjectStateManager.ChangeObjectState(saconfig_taccesspoint, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = iedID, backURL = backURL });
            }
            ViewBag.IED = new SelectList(db.saconfig_tIED.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_taccesspoint.IED);
            return View(saconfig_taccesspoint);
        }

        //
        // GET: /AccessPoint/Delete/5

        public ActionResult Delete(long id, long iedID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tAccessPoint saconfig_taccesspoint = db.saconfig_tAccessPoint.Single(s => s.ID == id && s.DataOwnerID == userID);

            ViewBag.iedID = iedID;
            ViewBag.BackURL = backURL;

            return View(saconfig_taccesspoint);
        }

        //
        // POST: /AccessPoint/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, long iedID, string backURL)
        {
            Guid userID = GetUserID();
            saconfig_tAccessPoint saconfig_taccesspoint = db.saconfig_tAccessPoint.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tAccessPoint.DeleteObject(saconfig_taccesspoint);
            db.SaveChanges();

            ViewBag.IEDID = iedID;
            ViewBag.BackURL = backURL;

            return RedirectToAction("Index", new { id = iedID, backURL = backURL });
        }

        public ActionResult ServerAtList(long id /*AccessPoint id.*/, long iedID, string backURL/*back URL to SCL-document*/)
        {
            return RedirectToAction("Index", "ServerAt", new { id = id, backURL = Url.Action("Edit", "AccessPoint", new { id = id, iedID = iedID, backURL = backURL }) });
        }

        public ActionResult ServerList(long id /*AccessPoint id.*/, long iedID, string backURL/*back URL to SCL-document*/)
        {
            return RedirectToAction("Index", "Server", new { id = id, backURL = Url.Action("Edit", "AccessPoint", new { id = id, iedID = iedID, backURL = backURL }) });
        }

        public ActionResult ServicesList(long id /*AccessPoint id.*/, long iedID, string backURL/*back URL to SCL-document*/)
        {
            return RedirectToAction("Index", "Services", new { id = id, backURL = Url.Action("Edit", "AccessPoint", new { id = id, iedID = iedID, backURL = backURL }), ownerType = "AccessPoint" });
        }

        public ActionResult CertificateList(long id /*AccessPoint id.*/, long iedID, string backURL/*back URL to SCL-document*/, string elementName)
        {
            return RedirectToAction("Index", "Certificate", new { id = id, backURL = Url.Action("Edit", "AccessPoint", new { id = id, iedID = iedID, backURL = backURL }), elementName = elementName });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}