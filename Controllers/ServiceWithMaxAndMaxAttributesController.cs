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
    public class ServiceWithMaxAndMaxAttributesController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ServiceWithMaxAndMaxAttributes/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tservicewithmaxandmaxattributes = db.saconfig_tServiceWithMaxAndMaxAttributes.Include("saconfig_ServiceWithMaxAndMaxAttributesElementName").Include("saconfig_tServices");
            return View(saconfig_tservicewithmaxandmaxattributes.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ServiceWithMaxAndMaxAttributes/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMaxAndMaxAttributes saconfig_tservicewithmaxandmaxattributes = db.saconfig_tServiceWithMaxAndMaxAttributes.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tservicewithmaxandmaxattributes);
        }

        //
        // GET: /ServiceWithMaxAndMaxAttributes/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithMaxAndMaxAttributesElementName, "ID", "ElementName");
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /ServiceWithMaxAndMaxAttributes/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServiceWithMaxAndMaxAttributes saconfig_tservicewithmaxandmaxattributes)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tservicewithmaxandmaxattributes.DataOwnerID = userID;
                db.saconfig_tServiceWithMaxAndMaxAttributes.AddObject(saconfig_tservicewithmaxandmaxattributes);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithMaxAndMaxAttributesElementName, "ID", "ElementName", saconfig_tservicewithmaxandmaxattributes.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithmaxandmaxattributes.Services);
            return View(saconfig_tservicewithmaxandmaxattributes);
        }
        
        //
        // GET: /ServiceWithMaxAndMaxAttributes/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMaxAndMaxAttributes saconfig_tservicewithmaxandmaxattributes = db.saconfig_tServiceWithMaxAndMaxAttributes.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithMaxAndMaxAttributesElementName, "ID", "ElementName", saconfig_tservicewithmaxandmaxattributes.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithmaxandmaxattributes.Services);
            return View(saconfig_tservicewithmaxandmaxattributes);
        }

        //
        // POST: /ServiceWithMaxAndMaxAttributes/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServiceWithMaxAndMaxAttributes saconfig_tservicewithmaxandmaxattributes)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tservicewithmaxandmaxattributes.DataOwnerID = userID;
                db.saconfig_tServiceWithMaxAndMaxAttributes.Attach(saconfig_tservicewithmaxandmaxattributes);
                db.ObjectStateManager.ChangeObjectState(saconfig_tservicewithmaxandmaxattributes, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceWithMaxAndMaxAttributesElementName, "ID", "ElementName", saconfig_tservicewithmaxandmaxattributes.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tservicewithmaxandmaxattributes.Services);
            return View(saconfig_tservicewithmaxandmaxattributes);
        }

        //
        // GET: /ServiceWithMaxAndMaxAttributes/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMaxAndMaxAttributes saconfig_tservicewithmaxandmaxattributes = db.saconfig_tServiceWithMaxAndMaxAttributes.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tservicewithmaxandmaxattributes);
        }

        //
        // POST: /ServiceWithMaxAndMaxAttributes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceWithMaxAndMaxAttributes saconfig_tservicewithmaxandmaxattributes = db.saconfig_tServiceWithMaxAndMaxAttributes.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServiceWithMaxAndMaxAttributes.DeleteObject(saconfig_tservicewithmaxandmaxattributes);
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