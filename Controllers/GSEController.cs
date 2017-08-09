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
    public class GSEController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /GSE/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tgse = db.saconfig_tGSE.Include("saconfig_tConnectedAP");
            return View(saconfig_tgse.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /GSE/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSE saconfig_tgse = db.saconfig_tGSE.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgse);
        }

        //
        // GET: /GSE/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /GSE/Create

        [HttpPost]
        public ActionResult Create(saconfig_tGSE saconfig_tgse)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgse.DataOwnerID = userID;
                db.saconfig_tGSE.AddObject(saconfig_tgse);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgse.ConnectedAP);
            return View(saconfig_tgse);
        }
        
        //
        // GET: /GSE/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSE saconfig_tgse = db.saconfig_tGSE.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgse.ConnectedAP);
            return View(saconfig_tgse);
        }

        //
        // POST: /GSE/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tGSE saconfig_tgse)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tgse.DataOwnerID = userID;
                db.saconfig_tGSE.Attach(saconfig_tgse);
                db.ObjectStateManager.ChangeObjectState(saconfig_tgse, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConnectedAP = new SelectList(db.saconfig_tConnectedAP.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tgse.ConnectedAP);
            return View(saconfig_tgse);
        }

        //
        // GET: /GSE/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSE saconfig_tgse = db.saconfig_tGSE.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tgse);
        }

        //
        // POST: /GSE/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tGSE saconfig_tgse = db.saconfig_tGSE.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tGSE.DeleteObject(saconfig_tgse);
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