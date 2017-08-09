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
    public class ProjectController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Project/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            return View(db.saconfig_Project.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /Project/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_Project saconfig_project = db.saconfig_Project.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_project);
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Project/Create

        [HttpPost]
        public ActionResult Create(saconfig_Project saconfig_project)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_project.DataOwnerID = userID;
                db.saconfig_Project.AddObject(saconfig_project);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(saconfig_project);
        }
        
        //
        // GET: /Project/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_Project saconfig_project = db.saconfig_Project.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_project);
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_Project saconfig_project)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_project.DataOwnerID = userID;
                db.saconfig_Project.Attach(saconfig_project);
                db.ObjectStateManager.ChangeObjectState(saconfig_project, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saconfig_project);
        }

        //
        // GET: /Project/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_Project saconfig_project = db.saconfig_Project.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_project);
        }

        //
        // POST: /Project/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_Project saconfig_project = db.saconfig_Project.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_Project.DeleteObject(saconfig_project);
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