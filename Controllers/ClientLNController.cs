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
    public class ClientLNController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ClientLN/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tclientln = db.saconfig_tClientLN.Include("saconfig_tLNClassEnum");
            return View(saconfig_tclientln.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ClientLN/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientLN saconfig_tclientln = db.saconfig_tClientLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tclientln);
        }

        //
        // GET: /ClientLN/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value");
            return View();
        } 

        //
        // POST: /ClientLN/Create

        [HttpPost]
        public ActionResult Create(saconfig_tClientLN saconfig_tclientln)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tclientln.DataOwnerID = userID;
                db.saconfig_tClientLN.AddObject(saconfig_tclientln);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tclientln.lnClass);
            return View(saconfig_tclientln);
        }
        
        //
        // GET: /ClientLN/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientLN saconfig_tclientln = db.saconfig_tClientLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tclientln.lnClass);
            return View(saconfig_tclientln);
        }

        //
        // POST: /ClientLN/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tClientLN saconfig_tclientln)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tclientln.DataOwnerID = userID;
                db.saconfig_tClientLN.Attach(saconfig_tclientln);
                db.ObjectStateManager.ChangeObjectState(saconfig_tclientln, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.lnClass = new SelectList(db.saconfig_tLNClassEnum.Where(t => t.DataOwnerID == userID || !t.Extension).ToList(), "ID", "value", saconfig_tclientln.lnClass);
            return View(saconfig_tclientln);
        }

        //
        // GET: /ClientLN/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientLN saconfig_tclientln = db.saconfig_tClientLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tclientln);
        }

        //
        // POST: /ClientLN/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tClientLN saconfig_tclientln = db.saconfig_tClientLN.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tClientLN.DeleteObject(saconfig_tclientln);
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