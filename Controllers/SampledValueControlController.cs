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
    public class SampledValueControlController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /SampledValueControl/

        public ViewResult Index()
        {
            Guid userID = GetUserID();
            var saconfig_tsampledvaluecontrol = db.saconfig_tSampledValueControl.Include("saconfig_SmpMod").Include("saconfig_tLN0");
            return View(saconfig_tsampledvaluecontrol.Where(t => t.DataOwnerID == userID).ToList());
        }

        //
        // GET: /SampledValueControl/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSampledValueControl saconfig_tsampledvaluecontrol = db.saconfig_tSampledValueControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsampledvaluecontrol);
        }

        //
        // GET: /SampledValueControl/Create

        public ActionResult Create()
        {
            Guid userID = GetUserID();
            ViewBag.smpMod = new SelectList(db.saconfig_SmpMod, "ID", "value");
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc");
            return View();
        } 

        //
        // POST: /SampledValueControl/Create

        [HttpPost]
        public ActionResult Create(saconfig_tSampledValueControl saconfig_tsampledvaluecontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsampledvaluecontrol.DataOwnerID = userID;
                db.saconfig_tSampledValueControl.AddObject(saconfig_tsampledvaluecontrol);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.smpMod = new SelectList(db.saconfig_SmpMod, "ID", "value", saconfig_tsampledvaluecontrol.smpMod);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsampledvaluecontrol.LN0);
            return View(saconfig_tsampledvaluecontrol);
        }
        
        //
        // GET: /SampledValueControl/Edit/5
 
        public ActionResult Edit(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSampledValueControl saconfig_tsampledvaluecontrol = db.saconfig_tSampledValueControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.smpMod = new SelectList(db.saconfig_SmpMod, "ID", "value", saconfig_tsampledvaluecontrol.smpMod);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsampledvaluecontrol.LN0);
            return View(saconfig_tsampledvaluecontrol);
        }

        //
        // POST: /SampledValueControl/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tSampledValueControl saconfig_tsampledvaluecontrol)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_tsampledvaluecontrol.DataOwnerID = userID;
                db.saconfig_tSampledValueControl.Attach(saconfig_tsampledvaluecontrol);
                db.ObjectStateManager.ChangeObjectState(saconfig_tsampledvaluecontrol, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.smpMod = new SelectList(db.saconfig_SmpMod, "ID", "value", saconfig_tsampledvaluecontrol.smpMod);
            ViewBag.LN0 = new SelectList(db.saconfig_tLN0.Where(t => t.DataOwnerID == userID).ToList(), "ID", "desc", saconfig_tsampledvaluecontrol.LN0);
            return View(saconfig_tsampledvaluecontrol);
        }

        //
        // GET: /SampledValueControl/Delete/5
 
        public ActionResult Delete(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSampledValueControl saconfig_tsampledvaluecontrol = db.saconfig_tSampledValueControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_tsampledvaluecontrol);
        }

        //
        // POST: /SampledValueControl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Guid userID = GetUserID();
            saconfig_tSampledValueControl saconfig_tsampledvaluecontrol = db.saconfig_tSampledValueControl.Single(s => s.ID == id && s.DataOwnerID == userID);
            db.saconfig_tSampledValueControl.DeleteObject(saconfig_tsampledvaluecontrol);
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