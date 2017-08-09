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
    public class SubNetworkController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SubNetwork/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsubnetwork = db.saconfig_tSubNetwork.Include("saconfig_tCommunication");
            return View(saconfig_tsubnetwork.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SubNetwork/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubNetwork saconfig_tsubnetwork = db.saconfig_tSubNetwork.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsubnetwork);
        }

        //
        // GET: /SubNetwork/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Communication = new SelectList(db.saconfig_tCommunication.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /SubNetwork/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSubNetwork saconfig_tsubnetwork)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsubnetwork.DataOwnerID = userID;
                db.saconfig_tSubNetwork.AddObject(saconfig_tsubnetwork);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Communication = new SelectList(db.saconfig_tCommunication.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsubnetwork.Communication);
            return View(saconfig_tsubnetwork);
        }
        
        //
        // GET: /SubNetwork/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubNetwork saconfig_tsubnetwork = db.saconfig_tSubNetwork.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Communication = new SelectList(db.saconfig_tCommunication.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsubnetwork.Communication);
            return View(saconfig_tsubnetwork);
        }

        //
        // POST: /SubNetwork/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSubNetwork saconfig_tsubnetwork)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsubnetwork.DataOwnerID = userID;
                db.saconfig_tSubNetwork.Attach(saconfig_tsubnetwork);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsubnetwork, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Communication = new SelectList(db.saconfig_tCommunication.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsubnetwork.Communication);
            return View(saconfig_tsubnetwork);
        }

        //
        // GET: /SubNetwork/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubNetwork saconfig_tsubnetwork = db.saconfig_tSubNetwork.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsubnetwork);
        }

        //
        // POST: /SubNetwork/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSubNetwork saconfig_tsubnetwork = db.saconfig_tSubNetwork.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSubNetwork.DeleteObject(saconfig_tsubnetwork);
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