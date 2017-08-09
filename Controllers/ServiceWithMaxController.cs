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
    public class ServiceWithMaxController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ServiceWithMax/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tservicewithmax = db.saconfig_tServiceWithMax.Include("saconfig_tServices").Include("saconfig_tServiceWithMaxElementName");
            return View(saconfig_tservicewithmax.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ServiceWithMax/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMax saconfig_tservicewithmax = db.saconfig_tServiceWithMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tservicewithmax);
        }

        //
        // GET: /ServiceWithMax/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceWithMaxElementName, "ID", "ElementName");
            return View();
        } 

        //
        // POST: /ServiceWithMax/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServiceWithMax saconfig_tservicewithmax)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tservicewithmax.DataOwnerID = userID;
                db.saconfig_tServiceWithMax.AddObject(saconfig_tservicewithmax);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithmax.Services);
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceWithMaxElementName, "ID", "ElementName", saconfig_tservicewithmax.ElementName);
            return View(saconfig_tservicewithmax);
        }
        
        //
        // GET: /ServiceWithMax/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMax saconfig_tservicewithmax = db.saconfig_tServiceWithMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithmax.Services);
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceWithMaxElementName, "ID", "ElementName", saconfig_tservicewithmax.ElementName);
            return View(saconfig_tservicewithmax);
        }

        //
        // POST: /ServiceWithMax/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServiceWithMax saconfig_tservicewithmax)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tservicewithmax.DataOwnerID = userID;
                db.saconfig_tServiceWithMax.Attach(saconfig_tservicewithmax);
                db.ObjectStateManager.ChangeObjectState(saconfig_tservicewithmax, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithmax.Services);
            ViewBag.ElementName = new SelectList(db.saconfig_tServiceWithMaxElementName, "ID", "ElementName", saconfig_tservicewithmax.ElementName);
            return View(saconfig_tservicewithmax);
        }

        //
        // GET: /ServiceWithMax/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMax saconfig_tservicewithmax = db.saconfig_tServiceWithMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tservicewithmax);
        }

        //
        // POST: /ServiceWithMax/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMax saconfig_tservicewithmax = db.saconfig_tServiceWithMax.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServiceWithMax.DeleteObject(saconfig_tservicewithmax);
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