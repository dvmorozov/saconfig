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
    public class ConnectedAPController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ConnectedAP/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tconnectedap = db.saconfig_tConnectedAP.Include("saconfig_tSubNetwork");
            return View(saconfig_tconnectedap.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ConnectedAP/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectedAP saconfig_tconnectedap = db.saconfig_tConnectedAP.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconnectedap);
        }

        //
        // GET: /ConnectedAP/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /ConnectedAP/Create

        [HttpPost]
        public ActionResult Create(saconfig_tConnectedAP saconfig_tconnectedap)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconnectedap.DataOwnerID = userID;
                db.saconfig_tConnectedAP.AddObject(saconfig_tconnectedap);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconnectedap.SubNetwork);
            return View(saconfig_tconnectedap);
        }
        
        //
        // GET: /ConnectedAP/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectedAP saconfig_tconnectedap = db.saconfig_tConnectedAP.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconnectedap.SubNetwork);
            return View(saconfig_tconnectedap);
        }

        //
        // POST: /ConnectedAP/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tConnectedAP saconfig_tconnectedap)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconnectedap.DataOwnerID = userID;
                db.saconfig_tConnectedAP.Attach(saconfig_tconnectedap);
                db.ObjectStateManager.ChangeObjectState(saconfig_tconnectedap, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubNetwork = new SelectList(db.saconfig_tSubNetwork.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconnectedap.SubNetwork);
            return View(saconfig_tconnectedap);
        }

        //
        // GET: /ConnectedAP/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectedAP saconfig_tconnectedap = db.saconfig_tConnectedAP.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconnectedap);
        }

        //
        // POST: /ConnectedAP/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectedAP saconfig_tconnectedap = db.saconfig_tConnectedAP.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tConnectedAP.DeleteObject(saconfig_tconnectedap);
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