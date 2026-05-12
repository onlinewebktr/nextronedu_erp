using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace school_web.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        // GET: Attendance
        [Route("salay/calculate")] 
        public ActionResult SalaryCalculate()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View();
        }
        [Route("salary/salary-chart")] 
        public ActionResult SalaryChart()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("SalaryChart");
        }
        private bool isHR()
        {
            if (Session["IsHR"] == null)
            {
                Session["IsHR"] = PayrollMy.data($"select IsHr from HR_UserProfile where UserId='{User.Identity.Name}'");
            }
            return Session["IsHR"].ToString() == "1";
        }
        [Route("attendance/{type}")]
        public ActionResult DailyAttendance(string type)
        {
            if(type.isEqual("device"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                return View("attendanceDevice");
            }
            else if(type.isEqual("fetching"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                return View("attendanceFetching");
            }
            else if(type.isEqual("daily-attendance"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                return View("dailyAttendanceReport");
            }
            else if (type.isEqual("monthly-attendance"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                return View("monthlyAttendanceReport");
            }
            else if (type.isEqual("daily-attendance-overall"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                ViewBag.Title = $"Daily Overall Attendance Report";
                return View("attendanceReport");
            }
            else if (type.isEqual("monthly-attendance-overall"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                ViewBag.Title = $"Monthly Overall Attendance Chart";
                return View("monthlyAttendanceChart");
            } 
            else if (type.isEqual("log-history"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                ViewBag.Title = $"Employee Attendance Log History";
                return View("attendanceLogHistory");
            }
            else if (type.isEqual("log-history-datewise"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                ViewBag.Title = $"Employee Attendance Log History";
                return View("attendanceLogHistoryDateWise");
            }
            else if (type.isEqual("monthly-attendance"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                ViewBag.Title = $"Employee Attendance Log History";
                return View("monthlyAttendanceReport");
            }
            else if (type.isEqual("monthly-status-overall"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                ViewBag.Title = $"Overall Monthly Status";
                ViewBag.IsEmployee = 0;
                return View("monthly-status-overall");
            }
            else if (type.isEqual("employee-monthly-status-overall"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                ViewBag.IsEmployee = 1;
                ViewBag.Title = $"Employee Monthly Status";
                return View("monthly-status-overall");
            }
            ///04/12/2023
            ///
            else if (type.isEqual("sudhir-manual"))
            {
                if (!isHR())
                {
                    return Redirect("~/home");
                }
                return View("SudhirManualAttendance");
            }




            else
            {
                return Redirect("~/home");
            }
            ViewBag.Title = $"{PayrollMy.displayText(type)} Attendance Report";
            return View("attendanceReport");
        } 
    }
}