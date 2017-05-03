using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MultiLanguageWebSite.Controllers
{
    public class LanguageController : Controller
    {
        public ActionResult Change(string lang)
        {
            if (lang != null)
            {

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = lang;
            Response.Cookies.Add(cookie);

            return View("Change");
        }
        public class CustomJsonResult
        {
            public bool Code { get; set; }
            public string Description { get; set; }
        }


        [HttpPost]
        public JsonResult ChangeLang(string LangIso)
        {
            CustomJsonResult result = new CustomJsonResult();
            result.Code = false;
            result.Description = "";

            try
            {
                if (LangIso != null)
                {

                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangIso);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangIso);

                }
                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = LangIso;
                Response.Cookies.Add(cookie);

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