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
    public class ServiceForConfDataSetController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /ServiceForConfDataSet/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tserviceforconfdataset = db.saconfig_tServiceForConfDataSet.Include("saconfig_ServiceForConfDataSetElementName").Include("saconfig_tServices");
            return View(saconfig_tserviceforconfdataset.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /ServiceForConfDataSet/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceForConfDataSet saconfig_tserviceforconfdataset = db.saconfig_tServiceForConfDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tserviceforconfdataset);
        }

        //
        // GET: /ServiceForConfDataSet/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceForConfDataSetElementName, "ID", "ElementName");
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID");
            return View();
        } 

        //
        // POST: /ServiceForConfDataSet/Create

        [HttpPost]
        public ActionResult Create(saconfig_tServiceForConfDataSet saconfig_tserviceforconfdataset)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tserviceforconfdataset.DataOwnerID = userID;
                db.saconfig_tServiceForConfDataSet.AddObject(saconfig_tserviceforconfdataset);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ElementName = new SelectList(db.saconfig_ServiceForConfDataSetElementName, "ID", "ElementName", saconfig_tserviceforconfdataset.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceforconfdataset.Services);
            return View(saconfig_tserviceforconfdataset);
        }
        
        //
        // GET: /ServiceForConfDataSet/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceForConfDataSet saconfig_tserviceforconfdataset = db.saconfig_tServiceForConfDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceForConfDataSetElementName, "ID", "ElementName", saconfig_tserviceforconfdataset.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceforconfdataset.Services);
            return View(saconfig_tserviceforconfdataset);
        }

        //
        // POST: /ServiceForConfDataSet/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tServiceForConfDataSet saconfig_tserviceforconfdataset)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tserviceforconfdataset.DataOwnerID = userID;
                db.saconfig_tServiceForConfDataSet.Attach(saconfig_tserviceforconfdataset);
                db.ObjectStateManager.ChangeObjectState(saconfig_tserviceforconfdataset, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElementName = new SelectList(db.saconfig_ServiceForConfDataSetElementName, "ID", "ElementName", saconfig_tserviceforconfdataset.ElementName);
            ViewBag.Services = new SelectList(db.saconfig_tServices.Where(t => t.DataOwnerID == userID).ToList(), "ID", "ID", saconfig_tserviceforconfdataset.Services);
            return View(saconfig_tserviceforconfdataset);
        }

        //
        // GET: /ServiceForConfDataSet/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceForConfDataSet saconfig_tserviceforconfdataset = db.saconfig_tServiceForConfDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tserviceforconfdataset);
        }

        //
        // POST: /ServiceForConfDataSet/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tServiceForConfDataSet saconfig_tserviceforconfdataset = db.saconfig_tServiceForConfDataSet.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tServiceForConfDataSet.DeleteObject(saconfig_tserviceforconfdataset);
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