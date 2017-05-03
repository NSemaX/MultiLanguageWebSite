using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiLanguageWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NotificationBarTest()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Language = "en";

            // Get Browser languages.
            var userLanguages = Request.UserLanguages;
            CultureInfo ci;
            if (userLanguages.Count() > 0)
            {
                try
                {
                    ci = new CultureInfo(userLanguages[0]);
                }
                catch (CultureNotFoundException)
                {
                    ci = CultureInfo.InvariantCulture;
                }
            }
            else
            {
                ci = CultureInfo.InvariantCulture;
            }
            // Here CultureInfo should already be set to either user's prefereable language
            // or to InvariantCulture if user transmitted invalid culture ID
            ViewBag.Language = ci.TwoLetterISOLanguageName;
            ViewBag.LanguageName = ci.NativeName;

            return View();
        }



        [HttpPost]
        public JsonResult ClearCookie()
        {
            LanguageController.CustomJsonResult result = new LanguageController.CustomJsonResult();
            result.Code = false;
            result.Description = "";

            try
            {
                string[] myCookies = Request.Cookies.AllKeys;
                foreach (string cookie in myCookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }

                Session["Language"] = null;

                result.Code = true;
            }
            catch
            {
                result.Description = "Error.";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}