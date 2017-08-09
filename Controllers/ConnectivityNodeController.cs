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
    public class ConnectivityNodeController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ConnectivityNode/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tconnectivitynode = db.saconfig_tConnectivityNode.Include("saconfig_tBay");
            return View(saconfig_tconnectivitynode.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ConnectivityNode/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectivityNode saconfig_tconnectivitynode = db.saconfig_tConnectivityNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconnectivitynode);
        }

        //
        // GET: /ConnectivityNode/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /ConnectivityNode/Create

        [HttpPost]
        public ActionResult Create(saconfig_tConnectivityNode saconfig_tconnectivitynode)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconnectivitynode.DataOwnerID = userID;
                db.saconfig_tConnectivityNode.AddObject(saconfig_tconnectivitynode);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconnectivitynode.Bay);
            return View(saconfig_tconnectivitynode);
        }
        
        //
        // GET: /ConnectivityNode/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectivityNode saconfig_tconnectivitynode = db.saconfig_tConnectivityNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconnectivitynode.Bay);
            return View(saconfig_tconnectivitynode);
        }

        //
        // POST: /ConnectivityNode/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tConnectivityNode saconfig_tconnectivitynode)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tconnectivitynode.DataOwnerID = userID;
                db.saconfig_tConnectivityNode.Attach(saconfig_tconnectivitynode);
                db.ObjectStateManager.ChangeObjectState(saconfig_tconnectivitynode, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tconnectivitynode.Bay);
            return View(saconfig_tconnectivitynode);
        }

        //
        // GET: /ConnectivityNode/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectivityNode saconfig_tconnectivitynode = db.saconfig_tConnectivityNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tconnectivitynode);
        }

        //
        // POST: /ConnectivityNode/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tConnectivityNode saconfig_tconnectivitynode = db.saconfig_tConnectivityNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tConnectivityNode.DeleteObject(saconfig_tconnectivitynode);
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