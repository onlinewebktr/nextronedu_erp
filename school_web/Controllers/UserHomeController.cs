using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace school_web.Controllers
{
    [Authorize]
    public class UserHomeController : Controller
    {
        // GET: UserHome 
        public ActionResult Index(Dictionary<string,object> data)
        {
            return View("abc");
        }
        [Route("myapp/test")]
        public ActionResult Test()
        {
            return View("test");
        }
        
    }
     
}