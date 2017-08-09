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
    public class LNodeController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /LNode/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tlnode = db.saconfig_tLNode.Include("saconfig_NodeOwnerType").Include("saconfig_tBay").Include("saconfig_tConnectivityNode").Include("saconfig_tLNClassEnum").Include("saconfig_tSubstation").Include("saconfig_tVoltageLevel");
            return View(saconfig_tlnode.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /LNode/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLNode saconfig_tlnode = db.saconfig_tLNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlnode);
        }

        //
        // GET: /LNode/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.OwnerType = new SelectList(db.saconfig_NodeOwnerType, "ID", "OwnerType");
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.ConnectivityNode = new SelectList(db.saconfig_tConnectivityNode.Where(t => t.DataOwnerID == userID).ToList(), "ID", "pathName");
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /LNode/Create

        [HttpPost]
        public ActionResult Create(saconfig_tLNode saconfig_tlnode)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlnode.DataOwnerID = userID;
                db.saconfig_tLNode.AddObject(saconfig_tlnode);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OwnerType = new SelectList(db.saconfig_NodeOwnerType, "ID", "OwnerType", saconfig_tlnode.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.Bay);
            ViewBag.ConnectivityNode = new SelectList(db.saconfig_tConnectivityNode.Where(t => t.DataOwnerID == userID).ToList(), "ID", "pathName", saconfig_tlnode.ConnectivityNode);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tlnode.lnClass);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.VoltageLevel);
            return View(saconfig_tlnode);
        }
        
        //
        // GET: /LNode/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLNode saconfig_tlnode = db.saconfig_tLNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.OwnerType = new SelectList(db.saconfig_NodeOwnerType, "ID", "OwnerType", saconfig_tlnode.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.Bay);
            ViewBag.ConnectivityNode = new SelectList(db.saconfig_tConnectivityNode.Where(t => t.DataOwnerID == userID).ToList(), "ID", "pathName", saconfig_tlnode.ConnectivityNode);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tlnode.lnClass);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.VoltageLevel);
            return View(saconfig_tlnode);
        }

        //
        // POST: /LNode/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tLNode saconfig_tlnode)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tlnode.DataOwnerID = userID;
                db.saconfig_tLNode.Attach(saconfig_tlnode);
                db.ObjectStateManager.ChangeObjectState(saconfig_tlnode, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerType = new SelectList(db.saconfig_NodeOwnerType, "ID", "OwnerType", saconfig_tlnode.OwnerType);
            ViewBag.Bay = new SelectList(db.saconfig_tBay.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.Bay);
            ViewBag.ConnectivityNode = new SelectList(db.saconfig_tConnectivityNode.Where(t => t.DataOwnerID == userID).ToList(), "ID", "pathName", saconfig_tlnode.ConnectivityNode);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tlnode.lnClass);
            ViewBag.Substation = new SelectList(db.saconfig_tSubstation.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.Substation);
            ViewBag.VoltageLevel = new SelectList(db.saconfig_tVoltageLevel.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tlnode.VoltageLevel);
            return View(saconfig_tlnode);
        }

        //
        // GET: /LNode/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLNode saconfig_tlnode = db.saconfig_tLNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tlnode);
        }

        //
        // POST: /LNode/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tLNode saconfig_tlnode = db.saconfig_tLNode.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tLNode.DeleteObject(saconfig_tlnode);
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