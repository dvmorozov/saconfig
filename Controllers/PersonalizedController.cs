using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SAConfig.Models;

namespace SAConfig.Controllers
{
    public class PersonalizedController : Controller
    {
        protected SAConfigEntities entities;

        public void ExceptionMessageToViewBag(Exception e)
        {
            ViewBag.ExceptionMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
        }

        //  the method fills ViewBag with application name
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            String appName1, appName2;

            if (null != Request.Url.DnsSafeHost && Request.Url.DnsSafeHost.ToLower().Contains("townbreath.com"))
            {
                appName1 = "Town";
                appName2 = "Breath";
            }
            else
            {
                appName1 = "Rational";
                appName2 = "City";
            }
            ViewBag.AppName = appName1 + " " + appName2;
            ViewBag.AppName1 = appName1;
            ViewBag.AppName2 = appName2;
            ViewBag.Title = "Welcome " + (String)ViewBag.AppName;
        }

        public Guid GetUserID()
        {
            /*???
            if (User != null)
            {   //  check for unit-testing mode
                MembershipUser mu = Membership.GetUser(User.Identity.Name);
                if (mu != null)
                {
                    return (Guid)mu.ProviderUserKey;
                }
                else
                {
                    Guid g = new Guid();
                    return g;
                }
            }
            else
            {
                Guid g = new Guid();
                return g;
            }
            */
            return new Guid("00000000-0000-0000-0000-000000000000");
        }

        protected override void Dispose(bool disposing)
        {
            if (entities != null) entities.Dispose();
            base.Dispose(disposing);
        }
    }
}