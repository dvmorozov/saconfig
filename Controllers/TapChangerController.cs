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
    public class TapChangerController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /TapChanger/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_ttapchanger = db.saconfig_tTapChanger.Include("saconfig_tTransformerWinding");
            return View(saconfig_ttapchanger.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /TapChanger/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTapChanger saconfig_ttapchanger = db.saconfig_tTapChanger.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_ttapchanger);
        }

        //
        // GET: /TapChanger/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type");
            return View();
        } 

        //
        // POST: /TapChanger/Create

        [HttpPost]
        public ActionResult Create(saconfig_tTapChanger saconfig_ttapchanger)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_ttapchanger.DataOwnerID = userID;
                db.saconfig_tTapChanger.AddObject(saconfig_ttapchanger);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_ttapchanger.TransformerWinding);
            return View(saconfig_ttapchanger);
        }
        
        //
        // GET: /TapChanger/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTapChanger saconfig_ttapchanger = db.saconfig_tTapChanger.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_ttapchanger.TransformerWinding);
            return View(saconfig_ttapchanger);
        }

        //
        // POST: /TapChanger/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tTapChanger saconfig_ttapchanger)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_ttapchanger.DataOwnerID = userID;
                db.saconfig_tTapChanger.Attach(saconfig_ttapchanger);
                db.ObjectStateManager.ChangeObjectState(saconfig_ttapchanger, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransformerWinding = new SelectList(db.saconfig_tTransformerWinding.Where(t => t.DataOwnerID == userID).ToList(), "ID", "type", saconfig_ttapchanger.TransformerWinding);
            return View(saconfig_ttapchanger);
        }

        //
        // GET: /TapChanger/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTapChanger saconfig_ttapchanger = db.saconfig_tTapChanger.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_ttapchanger);
        }

        //
        // POST: /TapChanger/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tTapChanger saconfig_ttapchanger = db.saconfig_tTapChanger.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tTapChanger.DeleteObject(saconfig_ttapchanger);
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