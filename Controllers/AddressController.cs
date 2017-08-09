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
    public class AddressController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Address/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_taddress = db.saconfig_tAddress.Include("saconfig_AddressOwnerType").Include("saconfig_tConnectedAP").Include("saconfig_tGSE").Include("saconfig_tSMV");
            return View(saconfig_taddress.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Address/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAddress saconfig_taddress = db.saconfig_tAddress.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_taddress);
        }

        //
        // GET: /Address/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_AddressOwnerType, "ID", "OwnerType");
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.SMV = new SelectList(db.saconfig_tSMV.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /Address/Create

        [HttpPost]
        public ActionResult Create(saconfig_tAddress saconfig_taddress)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_taddress.DataOwnerID = userID;
                db.saconfig_tAddress.AddObject(saconfig_taddress);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_AddressOwnerType, "ID", "OwnerType", saconfig_taddress.OwnerType);
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.ConnectedAP);
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.GSE);
            ViewBag.SMV = new SelectList(db.saconfig_tSMV.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.SMV);
            return View(saconfig_taddress);
        }
        
        //
        // GET: /Address/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAddress saconfig_taddress = db.saconfig_tAddress.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_AddressOwnerType, "ID", "OwnerType", saconfig_taddress.OwnerType);
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.ConnectedAP);
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.GSE);
            ViewBag.SMV = new SelectList(db.saconfig_tSMV.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.SMV);
            return View(saconfig_taddress);
        }

        //
        // POST: /Address/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tAddress saconfig_taddress)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_taddress.DataOwnerID = userID;
                db.saconfig_tAddress.Attach(saconfig_taddress);
                db.ObjectStateManager.ChangeObjectState(saconfig_taddress, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_AddressOwnerType, "ID", "OwnerType", saconfig_taddress.OwnerType);
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.ConnectedAP);
            ViewBag.GSE = new SelectList(db.saconfig_tGSE.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.GSE);
            ViewBag.SMV = new SelectList(db.saconfig_tSMV.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_taddress.SMV);
            return View(saconfig_taddress);
        }

        //
        // GET: /Address/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAddress saconfig_taddress = db.saconfig_tAddress.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_taddress);
        }

        //
        // POST: /Address/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tAddress saconfig_taddress = db.saconfig_tAddress.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tAddress.DeleteObject(saconfig_taddress);
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