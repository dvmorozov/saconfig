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
    public class TextController : PersonalizedController
    {
        private SAConfigEntities db = new SAConfigEntities();

        //
        // GET: /Text/Details/5

        public ViewResult Details(long id)
        {
            Guid userID = GetUserID();
            saconfig_tText saconfig_ttext = db.saconfig_tText.Single(s => s.ID == id && s.DataOwnerID == userID);
            return View(saconfig_ttext);
        }

        //
        // GET: /Text/Create

        public ActionResult Create(String backUrl)
        {
            if (backUrl == null || backUrl == "")
                throw new Exception("Inadmissible back URL");
            Guid userID = GetUserID();
            ViewBag.BackUrl = backUrl;
            return View();
        } 

        //
        // POST: /Text/Create

        [HttpPost]
        public ActionResult Create(saconfig_tText saconfig_ttext, String backUrl)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid && !(backUrl == null || backUrl == ""))
            {
                saconfig_ttext.DataOwnerID = userID;
                db.saconfig_tText.AddObject(saconfig_ttext);
                db.SaveChanges();
                //  here ID has valid value!
                return Redirect(backUrl + "?textId=" + saconfig_ttext.ID.ToString());  
            }

            return View(saconfig_ttext);
        }
        
        //
        // GET: /Text/Edit/5
 
        public ActionResult Edit(long id, String backDeleteUrl, String backEditUrl)
        {
            Guid userID = GetUserID();
            saconfig_tText saconfig_ttext = db.saconfig_tText.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackDeleteUrl = backDeleteUrl;
            ViewBag.BackEditUrl = backEditUrl;
            return View(saconfig_ttext);
        }

        //
        // POST: /Text/Edit/5

        [HttpPost]
        public ActionResult Edit(saconfig_tText saconfig_ttext, String backEditUrl)
        {
            Guid userID = GetUserID();
            if (ModelState.IsValid)
            {
                saconfig_ttext.DataOwnerID = userID;
                db.saconfig_tText.Attach(saconfig_ttext);
                db.ObjectStateManager.ChangeObjectState(saconfig_ttext, EntityState.Modified);
                db.SaveChanges();
                return Redirect(backEditUrl);
            }
            return View(saconfig_ttext);
        }

        //
        // GET: /Text/Delete/5
 
        public ActionResult Delete(long id, String backDeleteUrl)
        {
            Guid userID = GetUserID();
            saconfig_tText saconfig_ttext = db.saconfig_tText.Single(s => s.ID == id && s.DataOwnerID == userID);
            ViewBag.BackDeleteUrl = backDeleteUrl;
            return View(saconfig_ttext);
        }

        //
        // POST: /Text/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id, String backDeleteUrl)
        {
            Guid userID = GetUserID();
            saconfig_tText saconfig_ttext = db.saconfig_tText.Single(s => s.ID == id && s.DataOwnerID == userID);
            saconfig_ttext.value = "";
            db.SaveChanges();
            return Redirect(backDeleteUrl);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}