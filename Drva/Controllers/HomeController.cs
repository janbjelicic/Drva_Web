using Drva.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drva.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //DataImport.ImportData();
            return View();
        }
    }
}