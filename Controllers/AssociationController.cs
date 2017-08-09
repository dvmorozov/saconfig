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
    public class AssociationController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Association/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tassociation = db.saconfig_tAssociation.Include("saconfig_tAssociationKindEnum").Include("saconfig_tLNClassEnum").Include("saconfig_tServer");
            return View(saconfig_tassociation.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Association/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAssociation saconfig_tassociation = db.saconfig_tAssociation.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tassociation);
        }

        //
        // GET: /Association/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.kind = new SelectList(db.saconfig_tAssociationKindEnum, "ID", "value");
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Association/Create

        [HttpPost]
        public ActionResult Create(saconfig_tAssociation saconfig_tassociation)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tassociation.DataOwnerID = userID;
                db.saconfig_tAssociation.AddObject(saconfig_tassociation);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.kind = new SelectList(db.saconfig_tAssociationKindEnum, "ID", "value", saconfig_tassociation.kind);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tassociation.lnClass);
            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tassociation.Server);
            return View(saconfig_tassociation);
        }
        
        //
        // GET: /Association/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAssociation saconfig_tassociation = db.saconfig_tAssociation.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.kind = new SelectList(db.saconfig_tAssociationKindEnum, "ID", "value", saconfig_tassociation.kind);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tassociation.lnClass);
            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tassociation.Server);
            return View(saconfig_tassociation);
        }

        //
        // POST: /Association/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tAssociation saconfig_tassociation)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tassociation.DataOwnerID = userID;
                db.saconfig_tAssociation.Attach(saconfig_tassociation);
                db.ObjectStateManager.ChangeObjectState(saconfig_tassociation, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kind = new SelectList(db.saconfig_tAssociationKindEnum, "ID", "value", saconfig_tassociation.kind);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tassociation.lnClass);
            ViewBag.Server = new SelectList(db.saconfig_tServer.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tassociation.Server);
            return View(saconfig_tassociation);
        }

        //
        // GET: /Association/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAssociation saconfig_tassociation = db.saconfig_tAssociation.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tassociation);
        }

        //
        // POST: /Association/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAssociation saconfig_tassociation = db.saconfig_tAssociation.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tAssociation.DeleteObject(saconfig_tassociation);
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