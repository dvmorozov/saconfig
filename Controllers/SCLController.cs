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
    public class SCLController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SCL/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_scl = db.saconfig_SCL.Include("saconfig_Project").Include("saconfig_tText");
            return View(saconfig_scl.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SCL/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_SCL saconfig_scl = db.saconfig_SCL.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_scl);
        }

        //
        // GET: /SCL/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.Project = new SelectList(db.saconfig_Project.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name");
            return View();
        } 

        //
        // POST: /SCL/Create

        [HttpPost]
        public ActionResult Create(saconfig_SCL saconfig_scl)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_scl.DataOwnerID = userID;
                db.saconfig_SCL.AddObject(saconfig_scl);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Project = new SelectList(db.saconfig_Project.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_scl.Project);
            return View(saconfig_scl);
        }
        
        //
        // GET: /SCL/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_SCL saconfig_scl = db.saconfig_SCL.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.Project = new SelectList(db.saconfig_Project.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_scl.Project);
            return View(saconfig_scl);
        }

        //
        // POST: /SCL/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_SCL saconfig_scl)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_scl.DataOwnerID = userID;
                db.saconfig_SCL.Attach(saconfig_scl);
                db.ObjectStateManager.ChangeObjectState(saconfig_scl, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Project = new SelectList(db.saconfig_Project.Where(t => t.DataOwnerID == userID).ToList(), "ID", "name", saconfig_scl.Project);
            return View(saconfig_scl);
        }

        //
        // GET: /SCL/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_SCL saconfig_scl = db.saconfig_SCL.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_scl);
        }

        //
        // POST: /SCL/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_SCL saconfig_scl = db.saconfig_SCL.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_SCL.DeleteObject(saconfig_scl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TextCreateEdit(long id/*SCL id.*/)
        {
            Guid userID = GetUserID();
            saconfig_SCL saconfig_scl = db.saconfig_SCL.Single(s => s.ID == id && s.DataOwnerID == userID);
            if (saconfig_scl.Text == null)
            {
                //  redirect to create
                return RedirectToAction("Create", "Text", new { backUrl = Url.Action("BackTextCreate", "SCL", new { id = id } ) });
            }
            else
            { 
                //  redirect to edit
                return RedirectToAction("Edit", "Text", new { id = saconfig_scl.Text, backEditUrl = Url.Action("BackTextEdit", "SCL", new { id = id }), backDeleteUrl = Url.Action("BackTextDelete", "SCL", new { id = id }) });
            }
        }

        public ActionResult BackTextCreate(long id/*SCL id.*/, long textId)
        {
            Guid userID = GetUserID();
            saconfig_SCL saconfig_scl = db.saconfig_SCL.Single(s => s.ID == id && s.DataOwnerID == userID);
            if (ModelState.IsValid && saconfig_scl != null)
            {
                saconfig_scl.Text = textId;
                db.ObjectStateManager.ChangeObjectState(saconfig_scl, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult BackTextEdit(long id/*SCL id.*/)
        {
            return RedirectToAction("Index");
        }

        public ActionResult BackTextDelete(long id/*SCL id.*/)
        {
            Guid userID = GetUserID();
            saconfig_SCL saconfig_scl = db.saconfig_SCL.Single(s => s.ID == id && s.DataOwnerID == userID);
            if (ModelState.IsValid && saconfig_scl != null)
            {
                db.ObjectStateManager.ChangeObjectState(saconfig_scl, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult BackTextCancel(long id /*SCL id.*/)
        {
            return RedirectToAction("Index");
        }

        public ActionResult HeaderList(long id /*SCL id.*/)
        {
            return RedirectToAction("Index", "Header", new { id = id, backURL = Url.Action("Edit", "SCL", new { id = id }) });
        }

        public ActionResult SubstationList(long id /*SCL id.*/)
        {
            return RedirectToAction("Index", "Substation", new { id = id, backURL = Url.Action("Edit", "SCL", new { id = id }) });
        }

        public ActionResult CommunicationList(long id /*SCL id.*/)
        {
            return RedirectToAction("Index", "Communication", new { id = id, backURL = Url.Action("Edit", "SCL", new { id = id }) });
        }

        public ActionResult IEDList(long id /*SCL id.*/)
        {
            return RedirectToAction("Index", "IED", new { id = id, backURL = Url.Action("Edit", "SCL", new { id = id }) });
        }

        public ActionResult DataTypeTemplatesList(long id /*SCL id.*/)
        {
            return RedirectToAction("Index", "DataTypeTemplates", new { id = id, backURL = Url.Action("Edit", "SCL", new { id = id }) });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}