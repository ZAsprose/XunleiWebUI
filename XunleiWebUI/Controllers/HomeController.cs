using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XunleiWebUI.Models;
using ThunderAgentLib;

namespace XunleiWebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/

        [HttpPost]
        public bool AddXunleiDownloadTask(string magenet)
        {
            if (String.IsNullOrEmpty(magenet.Trim()))
                return false;
            return WindowsUtil.generateAndSubmitAXunleiDownloadTask(magenet);
        }
    }
}
