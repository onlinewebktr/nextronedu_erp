using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterMVC.Areas.Attendance.Controllers
{
    [RouteArea("attendance")]
    public class HomeController : Controller
    {
        
        // GET: Attendance/Home
        public ActionResult Index()
        {
            return View();
        }
        [Route("att")]
        public ActionResult Attendance()
        {
            return View();
        }
    }
}