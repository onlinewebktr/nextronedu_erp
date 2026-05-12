
using school_web.AppCode;
using school_web.MyCode;
using Newtonsoft.Json;
using school_web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace school_web.Controllers
{
    [Authorize]
    public class MasterController : Controller
    {
        [Route("")]
        [AllowAnonymous]
        public ActionResult gotohome()
        {
            return Redirect("~/Default.aspx");
        }
        [Route("home")]
        [Route("index")]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //Global gb = new Global();
            // gb.update_database_and_version();


            ViewBag.firm = getFirmDetails();
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.firmName = "M/S Purnank Enterprise";
                ViewBag.firmLogo = "../Content/Images/logo.png";
                HR_MasterPageConfig mpc = new HR_MasterPageConfig();
                ViewBag.mpc = mpc;

                if (Session["IsHR"] == null)
                {
                    Session["IsHR"] = PayrollMy.data($"select IsHr from HR_UserProfile where UserId='{User.Identity.Name}'");
                }
                if (Session["IsHR"].ToString() == "1")
                {
                    ViewBag.isHome = true;
                    ViewBag.applications = PayrollMy.data("select count(id) from HR_Employee_Online_Apply where Payment_Status='Paid'");
                    ViewBag.jobs = PayrollMy.data("select count(id) from HR_HiringParameterSetup");
                    ViewBag.deparments = PayrollMy.data("select count(id) from HR_Department_Master");
                    ViewBag.interviews = PayrollMy.data($"select count(id) from HR_InterviewSchedules where InterViewDate='{PayrollMy.Now.ToString("dd-MMM-yyyy")}'");
                    ViewBag.employee = PayrollMy.data($"select count(id) from HR_Employee_Master ");
                    ViewBag.selected = PayrollMy.data("select count(id) from HR_Employee_Online_Apply where Payment_Status='Paid' and Verification_Status not in ('Pending','Rejected')");
                }
                else
                {
                    return Redirect("~/Default.aspx");
                }
                return View("Index");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                //return View();
                return Redirect("~/Default.aspx");
            }
        }


        private Firm_Details getFirmDetails_new(string emp_postcode)//selryslip 
        {
            var data = PayrollMy.dataTable("select top 1 * from Firm_Details").toJsonString();
            var firm = JsonConvert.DeserializeObject<List<Firm_Details>>(data);
            if (firm.Count > 0)
            {

                if (firm[0].firm_name == "SUDHIR MEMORIAL INSTITUTE")
                {
                    if (emp_postcode == "PT0")
                    {

                        data = PayrollMy.dataTable("select top 1 * from Firm_Details_2").toJsonString();
                        firm = JsonConvert.DeserializeObject<List<Firm_Details>>(data);
                        Session["Firm_Details2"] = firm[0];
                        ViewBag.firmName = firm[0].firm_name;
                        ViewBag.firmLogo = firm[0].logo;


                    }
                    else if (emp_postcode == "CIP")
                    {
                        data = PayrollMy.dataTable("select top 1 * from Firm_Details_3").toJsonString();
                        firm = JsonConvert.DeserializeObject<List<Firm_Details>>(data);
                        Session["Firm_Details2"] = firm[0];
                        ViewBag.firmName = firm[0].firm_name;
                        ViewBag.firmLogo = firm[0].logo;
                    }
                    else
                    {
                        Session["Firm_Details2"] = firm[0];
                        ViewBag.firmName = firm[0].firm_name;
                        ViewBag.firmLogo = firm[0].logo;
                    }
                    return firm[0];

                }
                else
                {
                    Session["Firm_Details2"] = firm[0];
                    ViewBag.firmName = firm[0].firm_name;
                    ViewBag.firmLogo = firm[0].logo;
                    return firm[0];
                }

            }
            else
            {
                ViewBag.firmName = "";//"M/S Edunextg";
                ViewBag.firmLogo = "";//"../Content/Images/logo.png";

            }
            return new Firm_Details();

        }


        [Route("hrms")]
        [Authorize]
        public ActionResult EmployeeDashboard(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dt = PayrollMy.dataTable($"select * from HR_Employee_Master where Employee_id = '{User.Identity.Name}'");
                var emps = JsonConvert.DeserializeObject<List<HR_Employee_Master>>(dt.toJsonString());
                ViewBag.firm = getFirmDetails();
                if (emps.Count > 0)
                {
                    ViewBag.emp = emps[0];
                    ViewBag.Designaion = PayrollMy.data($"select name from HR_Designation_Master where Designation_id='{emps[0].Designation_id}'");
                }

                return View("hrms");
            }
            else
            {
                return Redirect("~/login");
            }

        }

        [Route("leave-request")]
        public ActionResult LeaveRequest()
        {
            if (Session["IsHR"] == null)
            {
                Session["IsHR"] = PayrollMy.data($"select IsHr from HR_UserProfile where UserId='{User.Identity.Name}'");
            }
            ViewBag.isHr = Session["IsHR"].ToString() == "1";

            return View();
        }

        [Route("approved-leave")]
        public ActionResult ApprovedLeave()
        {
            ViewBag.isHr = isHR();
            return View("Leave");
        }

        [Route("salary-slip/{id}/{cal_c}/{month}")]
        public ActionResult SalarySlip(string id, string cal_c, string month)
        {
            //if (!isHR())
            //{
            //    return Redirect("~/home");
            //}
            // var date = Convert.ToDateTime($"{year}-{month}-01");
            var dt = PayrollMy.dataTable($"select top 1 *,(Substring (Emp_Code,0,4)) as start_code from HR_Employee_Master where emp_code='{id}';");
            if (dt.Rows.Count == 0)
            {
                return Redirect("~/home");
            }
            var dt1 = PayrollMy.dataTable($"select top 1 * from HR_Salary_Calculation_Table where EmployeeId='{dt.Rows[0]["Employee_Id"]}' and Calculation_Id='{cal_c}'");
            if (dt1.Rows.Count == 0)
            {
                return Redirect("~/home");
            }

            var dt2 = PayrollMy.dataTable($"select top 1 * from Firm_Details;");

            if (dt2.Rows[0]["firm_name"].ToString() == "SUDHIR MEMORIAL INSTITUTE")
            {
                ViewBag.firm = getFirmDetails_new(dt.Rows[0]["start_code"].ToString());
            }
            else
            {
                ViewBag.firm = getFirmDetails();
            }



            ViewBag.sl = (JsonConvert.DeserializeObject<List<HR_Salary_Calculation_Table>>(dt1.toJsonString()))[0];
            ViewBag.emp = (JsonConvert.DeserializeObject<List<HR_Employee_Master>>(dt.toJsonString()))[0];
            ViewBag.Designation = "";

            if (dt2.Rows[0]["firm_name"].ToString() == "SUDHIR MEMORIAL INSTITUTE")
            {
                ViewBag.firm = getFirmDetails_new(dt.Rows[0]["start_code"].ToString());
            }
            else
            {
                ViewBag.firm = getFirmDetails();
            }
            return View("salary_slip");
        }


        private bool IsAdmin()
        {
            if (Session["IsAdmin"] == null)
            {
                Session["IsAdmin"] = PayrollMy.data($"select IsAdmin from HR_UserProfile where UserId='{User.Identity.Name}'");
            }
            return Session["IsAdmin"].ToString() == "1";
        }
        private bool isHR()
        {
            if (Session["IsHR"] == null)
            {
                Session["IsHR"] = PayrollMy.data($"select IsHr from HR_UserProfile where UserId='{User.Identity.Name}'");
            }
            return Session["IsHR"].ToString() == "1";
        }

        [Route("roster-rearrange")]
        public ActionResult RosterReArrange()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("roster_re_arrange");
        }
        [Route("employee-upload")]
        public ActionResult EmployeeUpload()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("EmployeeUploadToDevice");
        }
        [Route("duty-roster")]
        public ActionResult DutyRoster()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("duty_roster");
        }
        [Route("dummy-attendance")]
        public ActionResult DutyRostedr()
        {
            var dt = PayrollMy.dataTable(@"select  EmployeeId,Shift_Id,In_Time,Out_Time,Start_Date,End_Date from HR_Duty_Roster join HR_ShiftSetting on HR_ShiftSetting.ShiftId=HR_Duty_Roster.Shift_Id join HR_Rotation_Details on HR_Rotation_Details.RotationId=HR_Duty_Roster.RotationId");
            var dt1 = PayrollMy.dataTable(@"select Employee_Id  from hr_Employee_master where Employee_Id not in (select EmployeeId from HR_Duty_Roster)");
            foreach (DataRow dr in dt.Rows)
            {
                var qry = $@"DECLARE @Employees TABLE (Id int IDENTITY(1, 1), Employee_id varchar(50));
INSERT INTO @Employees (Employee_id)
VALUES
    ('{dr["EmployeeId"]}');

DECLARE @StartDate DATE = '{dr["Start_Date"]}';
DECLARE @EndDate DATE = '{dr["End_Date"]}';
DECLARE @Counter INT = 1;

WHILE @StartDate <= @EndDate
BEGIN
    INSERT INTO dbo.[HR_Daily_Attendance_Record] (Employee_id, Date, In_Time, Out_Time,Shift_Id,AttendanceSource,Idate)
    SELECT
        E.Employee_id,
        CONVERT(VARCHAR(10), @StartDate, 120), -- Date in yyyy-MM-dd format
        CONVERT(VARCHAR(5), DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * 60 - 30, '{dr["In_Time"]}'), 108), -- In_Time
        CONVERT(VARCHAR(5), DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * 60 - 30, '{dr["Out_Time"]}'), 108), -- Out_Time
'{dr["Shift_Id"]}','Dummy',format(@StartDate,'yyyyMMdd')
    FROM @Employees E;

    SET @StartDate = DATEADD(DAY, 1, @StartDate);
    SET @Counter = @Counter + 1;
END;";
                PayrollMy.execute(qry);
            }

            foreach (DataRow dr in dt1.Rows)
            {
                var qry = $@"DECLARE @Employees TABLE (Id int IDENTITY(1, 1), Employee_id varchar(50));
INSERT INTO @Employees (Employee_id)
VALUES
    ('{dr["Employee_Id"]}');

DECLARE @StartDate DATE = '2023-08-01';
DECLARE @EndDate DATE = '2023-08-31';
DECLARE @Counter INT = 1;

WHILE @StartDate <= @EndDate
BEGIN
    INSERT INTO dbo.[HR_Daily_Attendance_Record] (Employee_id, Date, In_Time, Out_Time,Shift_Id,AttendanceSource,Idate)
    SELECT
        E.Employee_id,
        CONVERT(VARCHAR(10), @StartDate, 120), -- Date in yyyy-MM-dd format
        CONVERT(VARCHAR(5), DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * 60 - 30, '9:30'), 108), -- In_Time
        CONVERT(VARCHAR(5), DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * 60 - 59, '18:00'), 108), -- Out_Time
'0','Dummy',format(@StartDate,'yyyyMMdd')
    FROM @Employees E;

    SET @StartDate = DATEADD(DAY, 1, @StartDate);
    SET @Counter = @Counter + 1;
END;";
                PayrollMy.execute(qry);
            }
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("duty_roster");
        }
        [Route("view-duty-roster")]
        public ActionResult ViewDutyRoster()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("view_duty_roster");
        }
        [Route("view-duty-roster-new")]
        public ActionResult NewViewDutyRoster()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("view_duty_roster_new");
        }

        [Route("duty-roster-weekoff-setting")]
        public ActionResult DutyRosterWeekOff()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("duty_roster_week_off_setting");
        }
        [Route("duty-hours")]
        public ActionResult DutyHour()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("duty_hour");
        }


        [Route("duty-hour-mapping")]
        public ActionResult DutyHourMapping()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View();
        }
        [Route("rotation-duration")]
        public ActionResult RotationDuration()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("duty_Rotation_master");
        }


        [Route("frame")]
        public ActionResult IframeView()
        {
            ViewBag.Title = Request.QueryString["q"];
            return View();
        }
        private Firm_Details getFirmDetails()
        {
            if (Session["Firm_Details"] == null)
            {
                var data = PayrollMy.dataTable("select top 1 * from Firm_Details").toJsonString();
                var firm = JsonConvert.DeserializeObject<List<Firm_Details>>(data);
                if (firm.Count > 0)
                {
                    Session["Firm_Details"] = firm[0];
                    ViewBag.firmName = firm[0].firm_name;
                    ViewBag.firmLogo = firm[0].logo;
                    return firm[0];
                }
                else
                {
                    ViewBag.firmName = "M/S Purnank Enterprise";
                    ViewBag.firmLogo = "../Content/Images/logo.png";
                }
                return new Firm_Details();
            }
            else
            {
                var firm = Session["Firm_Details"] as Firm_Details;
                ViewBag.firmName = firm.firm_name;
                ViewBag.firmLogo = firm.logo;
                return firm;
            }
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return Redirect("~/Default.aspx");
        }

        // GET: Master
        [Route("mpc")]
        [Authorize]
        public ActionResult MasterPageConfig()
        {
            if (!isHR())
            {
                return Redirect("home");
            }
            var dt = PayrollMy.dataTable("select TABLE_NAME from information_schema.tables order by TABLE_NAME");
            List<string> lst = new List<string>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                lst.Add(dr[0].ToString());
            }
            ViewBag.Tables = lst;

            var dt1 = PayrollMy.dataTable("select PageTitle,PageId from HR_MasterPageConfig ");
            var pages = new List<SelectListItem>();
            pages.Add(new SelectListItem() { Text = "New", Value = "new" });
            foreach (System.Data.DataRow dr in dt1.Rows)
            {
                pages.Add(new SelectListItem() { Text = dr[0].ToString(), Value = dr[1].ToString() });
            }
            ViewBag.Pages = pages;
            ViewBag.firm = getFirmDetails();
            return View();
        }

        [Route("hiring-application-list")]
        [Authorize]
        public ActionResult EmployeeApplyList()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            ViewBag.Title = "Hiring Application List";
            return View("hiring");
        }
        [Route("leave-setting")]
        [Authorize]
        public ActionResult LeaveSetting()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            return View("leave-setting");
        }

        [Route("rejected-hiring-list")]
        [Authorize]
        public ActionResult RejectedHiringList()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            ViewBag.Title = "Rejected Hiring Application List";
            return View("hiring-rejected-list");
        }
        [Route("joining/{id}")]
        [Authorize]
        public ActionResult JoiningProcess(string id)
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }
            var dt = PayrollMy.dataTable($"select top 1 * from HR_Employee_Online_Apply where Apply_id = '{id}'");
            if (dt.Rows.Count == 0)
            {
                return Redirect("~/home");
            }
            else
            {

                if (dt.Rows[0]["CurrentStatus"].ToString() != "Offered")
                {
                    if (dt.Rows[0]["CurrentStatus"].ToString() == "Joined")
                    {
                        return Redirect("~/joiningletter/" + dt.Rows[0]["Emp_code"]);
                    }
                    else
                    {
                        return Redirect("~/home");
                    }
                }
            }
            var data = dt.toJsonString();
            var dt1 = PayrollMy.dataTable($"select top 1 * from PRL_Employee_Master where Emp_Code = '{id}'");

            var ea1 = JsonConvert.DeserializeObject<List<PRL_Employee_Master>>(dt1.toJsonString());
            //var emp2 = ViewBag.emp2 as List<PRL_Employee_Master>;
            if (ea1.Count == 0)
            {
                ViewBag.emp2 = new PRL_Employee_Master();

            }
            else
            {
                ViewBag.emp2 = ea1[0];

            }

            var ea = JsonConvert.DeserializeObject<List<Employee_Apply>>(data);
            ViewBag.Title = "Employee Joining Process";
            ViewBag.emp = ea[0];

            ViewBag.grade = PayrollMy.data($"select grade_name from HR_Grade_Master where grade_id='{dt.Rows[0]["Grade"]}' ");
            ViewBag.department = PayrollMy.data($"select name from HR_Department_Master where department_id='{dt.Rows[0]["DepartmentId"]}' ");
            ViewBag.designation = PayrollMy.data($"select name from HR_Designation_Master where Designation_id='{dt.Rows[0]["DesignationId"]}' ");

            return View();
        }
        [Route("editEmployee/{id}")]
        [Authorize]
        public ActionResult EditEmployee(string id)
        {

            if (!isHR())
            {
                return Redirect("~/home");
            }

            var e_dt = PayrollMy.dataTable($"select top 1 * from HR_Employee_Master where Employee_Id = '{id}'");
            if (e_dt.Rows.Count == 0)
            {
                return Redirect("~/home");
            }
            var dt = PayrollMy.dataTable($"select top 1 * from HR_Employee_Online_Apply where Emp_code = '{e_dt.Rows[0]["Emp_Code"]}'");
            if (dt.Rows.Count == 0)
            {
                return Redirect("~/home");
            }

            var data = dt.toJsonString();
            var em = JsonConvert.DeserializeObject<List<HR_Employee_Master>>(e_dt.toJsonString());
            var ea = JsonConvert.DeserializeObject<List<Employee_Apply>>(data);
            ViewBag.Title = "Employee Detail Modification";
            ViewBag.emp = ea[0];
            ViewBag.em = em[0];
            return View("employeeEdit");
        }
        [Route("selected-for-interview")]
        [Authorize]
        public ActionResult InterviewList()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            ViewBag.Title = "Scheduled For Interview";
            return View("hiring-for-interview");
        }
        [Route("employeelist")]
        [Authorize]
        public ActionResult EmployeeList()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            return View();
        }
        [Route("change-password")]
        [Authorize]
        public ActionResult change_password()
        {
            return View();
        }
        [Route("selected-for-hiring")]
        [Authorize]
        public ActionResult HiringList()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            ViewBag.Title = "Selected Condidates";
            return View("hiring-generetate-offer-letter");
        }
        [Route("print-application-receipt/{id}")]
        [AllowAnonymous]
        public ActionResult PrintReceipt(string id)
        {
            var data = PayrollMy.dataTable($"select top 1 * from HR_Employee_Online_Apply where Apply_id = '{id}'").toJsonString();
            var ea = JsonConvert.DeserializeObject<List<Employee_Apply>>(data);
            ViewBag.Title = "Print Application Receipt";
            ViewBag.emp = ea[0];
            Firm_Details firm = getFirmDetails();
            ViewBag.firmLogo = firm.logo;
            ViewBag.amount = PayrollMy.NumberToWords(ea[0].Payable_amount.ToInt()) + " Only.";
            ViewBag.Session = PayrollMy.data($"select SessionName from HR_SessionList where SessionId='{ea[0].Session_id}'");
            ViewBag.firmName = firm.firm_name;
            ViewBag.firmEmail = firm.Email;
            ViewBag.firmWebsite = firm.Website;
            ViewBag.firmContact = firm.contact_no;
            return View("print_application_receipt");
        }

        [Route("generated-offer-leter")]
        [Authorize]
        public ActionResult OfferLetter()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            ViewBag.Title = "Offer Letter Generated";
            return View("hiring-generated-offer-leter-list");
        }
        [Route("offer-leter-canceled")]
        [Authorize]
        public ActionResult CanceledOfferLetter()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            ViewBag.Title = "Canceled Offer Letter";
            return View("canceled-offer-letter");
        }
        [Route("employee_salary_structure")]
        [Authorize]
        public ActionResult SalarayStructure()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            return View();
        }

        [Route("view-offer-letter/{id}")]
        [Authorize]
        public ActionResult ViewOfferLetter(string id)
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            var dt = PayrollMy.dataTable($"select * from HR_Employee_Online_Apply where Apply_id = '{id}'");
            ViewBag.Title = "Offer Letter";
            if (dt.Rows.Count > 0)
            {
                var Salutation = ""; //PayrollMy.data($"select Initial_name from HMS_Initial_Master where Initial_id ='{dt.Rows[0]["Salutation"].ToInt(3)}'");

                Firm_Details firm = getFirmDetails();
                ViewBag.name = $"{Salutation} {dt.Rows[0]["First_Name"]} {dt.Rows[0]["Middle_Name"]} {dt.Rows[0]["Last_Name"]}".Trim();
                ViewBag.date = PayrollMy.Now.ToString("dd-MMM-yyyy");
                ViewBag.firmName = firm.firm_name;
                ViewBag.mobile = dt.Rows[0]["mobile_no_CA"];
                ViewBag.email = dt.Rows[0]["Emailid"];
                ViewBag.post = $"{dt.Rows[0]["Apply_for"]}";
                ViewBag.joiningDate = $"{dt.Rows[0]["JoiningDate"]}";
                ViewBag.salary = $"{dt.Rows[0]["Salary"]}";
            }

            return View("view_offer_letter");
        }
        [Route("offer-letter/{id}")]
        public ActionResult OfferLetter(string id)
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            var dt = PayrollMy.dataTable($"select * from HR_Employee_Master where Employee_id = '{id}'");
            ViewBag.Title = "Offer Letter";
            if (dt.Rows.Count > 0)
            {
                var Salutation = "Mr.";
                try
                {
                    Salutation = ""; //PayrollMy.data($"select Initial_name from HMS_Initial_Master where Initial_id in  (select Salutation from HR_Employee_Online_Apply where Emp_code = '{dt.Rows[0]["Emp_Code"]}')");
                }
                catch
                {

                }

                Firm_Details firm = getFirmDetails();
                ViewBag.name = $"{Salutation} {dt.Rows[0]["Employee_Name"]}";
                ViewBag.date = PayrollMy.Now.ToString("dd-MMM-yyyy");
                ViewBag.mobile = dt.Rows[0]["Mobile"];
                ViewBag.email = dt.Rows[0]["Email"];
                ViewBag.firmName = firm.firm_name;
                ViewBag.post = PayrollMy.data($"select name  from HR_Designation_Master where Designation_id='{dt.Rows[0]["Designation_id"]}' ");
                ViewBag.joiningDate = $"{dt.Rows[0]["Date_of_Joining"]}";
                ViewBag.salary = $"{dt.Rows[0]["basic_salary"]}";
            }

            return View("view_offer_letter");
        }
        [Route("salaryctc/{id}")]
        [Authorize]
        public ActionResult salaryctc(string id)
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            var dt = PayrollMy.dataTable($"select * from HR_Employee_Master where Employee_id = '{id}'");
            ViewBag.Title = "Joining Letter";
            if (dt.Rows.Count > 0)
            {
                Firm_Details firm = getFirmDetails();
                var emps = JsonConvert.DeserializeObject<List<HR_Employee_Master>>(dt.toJsonString());
                ViewBag.date = PayrollMy.Now.ToString("dd-MMM-yyyy");
                ViewBag.firmName = firm.firm_name;
                ViewBag.Designation = PayrollMy.data($"select name  from HR_Designation_Master where Designation_id='{emps[0].Designation_id}' ");
                ViewBag.emp = emps[0];
                ViewBag.incomes = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(emps[0].income_heads);
                ViewBag.deductions = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(emps[0].deduction_heads);
            }
            return View();
        }
        [Route("joiningletter/{id}")]
        [Authorize]
        public ActionResult joiningletter(string id)
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            var dt = PayrollMy.dataTable($"select * from HR_Employee_Master where Employee_id = '{id}'");
            ViewBag.Title = "Joining Letter";
            if (dt.Rows.Count > 0)
            {


                Firm_Details firm = getFirmDetails();
                var emps = JsonConvert.DeserializeObject<List<HR_Employee_Master>>(dt.toJsonString());
                var Salutation = PayrollMy.data($"select Initial_name from HMS_Initial_Master where Initial_id in  (select Salutation from HR_Employee_Online_Apply where Emp_code = '{emps[0].Emp_Code}')");
                ViewBag.date = PayrollMy.Now.ToString("dd-MMM-yyyy");
                ViewBag.firmName = firm.firm_name;
                ViewBag.NameTitle = Salutation;
                ViewBag.Designation = PayrollMy.data($"select name  from HR_Designation_Master where Designation_id='{emps[0].Designation_id}' "); ;
                ViewBag.CTC_month = PayrollMy.data($"select CTC_month  from HR_Employee_Online_Apply where Emp_code='{id}' "); ;
                ViewBag.emp = emps[0];
            }
            return View();
        }
        [Route("idcard/{id}")]
        [Authorize]
        public ActionResult idcard(string id)
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            var dt = PayrollMy.dataTable($"select * from HR_Employee_Master where Employee_id = '{id}'");
            ViewBag.Title = "Employee Id Card";
            if (dt.Rows.Count > 0)
            {
                Firm_Details firm = getFirmDetails();
                var emps = JsonConvert.DeserializeObject<List<HR_Employee_Master>>(dt.toJsonString());
                ViewBag.date = PayrollMy.Now.ToString("dd-MMM-yyyy");
                ViewBag.firmName = firm.firm_name;
                ViewBag.firmaddress = firm.address1;
                ViewBag.firmlogo = firm.logo;
                string str = ViewBag.firmName + "\r\nName : " + emps[0].Employee_Name + ",\r\n" + "Emp. Id : " + emps[0].Employee_id + ", " + "\r\nAddress : " + emps[0].Address;
                ViewBag.qrCode = PayrollMy.generate_qr_code(str, 100, 100);
                ViewBag.dob = Convert.ToDateTime(dt.Rows[0]["Date_of_birth"]).ToString("dd-MMM-yyyy");
                ViewBag.Designation = PayrollMy.data($"select name  from HR_Designation_Master where Designation_id='{emps[0].Designation_id}' "); ;
                ViewBag.emp = emps[0];
            }
            return View();
        }

        [Route("getmenu/{id}")]
        [Authorize]
        [OutputCache(Duration = 30, Location = System.Web.UI.OutputCacheLocation.Client)]
        public JsonResult getmenu(string id)
        {
            string empcode = PayrollMy.get_emp_code_from_emp_Employee_id(id);
            string usertype = My.get_user_type(empcode);
            if (usertype == "Admin")
            {
                var isEmployee = "";
                if (!PayrollMy.IsDataExist($"select Id from Hr_Employee_Master where Employee_id='{User.Identity.Name}' "))
                {
                    isEmployee = " and Menu_Id!='47'";
                }
                var dt = PayrollMy.dataTable($"select  *,'' child from HR_Menu_Master where Parent_id=0 and Status=1 and RestrictedMenu=0 {isEmployee} order by Sequence");




                var dtall = PayrollMy.dataTable($"select *  from HR_Menu_Master where Parent_id!=0 and Status=1  order by Sequence");


                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    var drs = dtall.Select("Parent_id='" + dr["Menu_Id"] + "'", "Sequence");
                    if (drs.Length > 0)
                    {
                        dr["child"] = drs.toJson(dtall.Columns);
                    }
                }
                return Json(dt.toJsonString(), JsonRequestBehavior.AllowGet);

                //var dt = PayrollMy.dataTable($"select  *,'' child from HR_Menu_Master where Parent_id='37' and Status=1 and RestrictedMenu=0  order by Sequence");
                //var dtall = PayrollMy.dataTable($"select *  from HR_Menu_Master where Parent_id!=0 and Status=1 order by Sequence");
                //foreach (System.Data.DataRow dr in dt.Rows)
                //{
                //    var drs = dtall.Select("Parent_id='" + dr["Menu_Id"] + "'", "Sequence");
                //    if (drs.Length > 0)
                //    {
                //        dr["child"] = drs.toJson(dtall.Columns);
                //    }
                //}
                //return Json(dt.toJsonString(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var isEmployee = "";
                if (!PayrollMy.IsDataExist($"select Id from Hr_Employee_Master where Employee_id='{User.Identity.Name}' "))
                {
                    isEmployee = " and Menu_Id!='47'";
                }
                // var dt = PayrollMy.dataTable($"select  *,'' child from HR_Menu_Master where Parent_id=0 and Status=1 and RestrictedMenu=0 {isEmployee} order by Sequence");

                var dt = PayrollMy.dataTable($"select hm.*,'' child from HR_Menu_Master hm join HR_MenuPermission mp on mp.MenuID = hm.Menu_Id where hm.Parent_id = 0 and hm.Status = 1 and hm.RestrictedMenu = 0 and mp.Employee_id = '" + id + "' and Menu_Id!= '47'  order by hm.Sequence");

                var dtall = PayrollMy.dataTable($"select *  from HR_Menu_Master where Parent_id!=0 and Status=1  order by Sequence");
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    var drs = dtall.Select("Parent_id='" + dr["Menu_Id"] + "'", "Sequence");
                    if (drs.Length > 0)
                    {
                        dr["child"] = drs.toJson(dtall.Columns);
                    }
                }
                return Json(dt.toJsonString(), JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        [Route("my/upload")]
        public JsonResult upload(HttpPostedFileBase file, string filetype)
        {
            if (file != null && file.ContentLength > 0)
            {
                var ext = Path.GetExtension(file.FileName);
                if (filetype.Contains("image") && !file.ContentType.Contains("image"))
                {
                    return Json(new { success = false, message = "Only Image file supported" });
                }
                else if (filetype.Contains("pdf") && !file.ContentType.Contains("pdf"))
                {
                    if (!filetype.Contains(ext))
                    {
                        return Json(new { success = false, message = "Selected File type is not supported" });
                    }
                }
                // var fileName = Path.GetFileName(file.FileName);
                var fileName = Guid.NewGuid().ToString() + ext;
                var dir = Server.MapPath("~/uploads");
                var accesspath = $"/uploads/{fileName}";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var path = Path.Combine(dir, fileName);
                file.SaveAs(path);
                return Json(new { success = true, path = accesspath });
            }
            return Json(new { success = false, message = "No File Selected" });
        }
        [Route("mpc/saveingdata")]
        [HttpPost]
        [Authorize]
        public JsonResult saveingdata(FormCollection form)
        {
            var _data = (new JavaScriptSerializer()).Serialize(new
            {
                message = "",
                error = true,
                data = ""
            });
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        [Route("data/{dataId}")]
        [HttpPost]
        [Authorize]
        public ActionResult getdata(string dataId, Dictionary<string, object> data)
        {
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Some thing goes worng",
                error = true,
                dataId = dataId,
                data = data,
            });

            if (dataId == "myapp3")
            {
                if (data["name"].ToString() == "Rajesh")
                {
                    PayrollMy.Insert("myaptest", data);
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = $"Hello {data["name"]}",
                        error = false,
                    });
                }
            }

            if (dataId == "change-employee-status")
            {
                var current_status = "";
                var change_status = "";
                var change_status_value = "0";
                var id = data["Employee_id"].ToString();
                string get_empcode = My.get_emp_code_from_Employee_id(id);
                /// data base 
                //DataTable dt = PayrollMy.dataTable("Select * from HR_Employee_Master where Emp_Code='" + id + "'");
                DataTable dt = PayrollMy.dataTable("Select * from HR_Employee_Master where Employee_id='" + id + "'");
                if (dt.Rows.Count == 0)
                {
                    current_status = "Inactive";
                    change_status = "Active";
                    change_status_value = "1";
                }
                else
                {
                    if (dt.Rows[0]["ActiveStatus"].ToString() == "")
                    {
                        current_status = "Inactive";
                    }
                    else if (dt.Rows[0]["ActiveStatus"].ToString() == "Inactive")
                    {
                        current_status = "Inactive";
                    }
                    else
                    {
                        current_status = "Active";
                    }

                    if (current_status == "Inactive")
                    {
                        change_status = "Active";
                        change_status_value = "1";
                    }
                    else
                    {
                        change_status = "Inactive";
                        change_status_value = "0";
                    }
                    My.exeSql("update HR_Employee_Master set ActiveStatus='" + change_status + "' where Employee_id='" + id + "'");
                    My.exeSql("update PRL_Employee_Master set Status='" + change_status + "' where Emp_Code='" + get_empcode + "'");
                    My.exeSql("update user_details set status='" + change_status + "',Istatus=" + change_status_value + " where user_id='" + get_empcode + "'");
                }
                /// 
                //var status = "Inactive";
                var status = change_status;
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = $"success",
                    status = status,
                    error = false,
                });
            }
            if (dataId == "export")
            {
                // return exeport_frm(data);
                //return exeport_form(data);
            }
            if (dataId == "salary")
            {
                if (data["type"].ToString() == "salary-chart")
                {
                    var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                    var dt = PayrollMy.dataTable($"select * from HR_Salary_Calculation_Table where Month='{date.ToString("MMMM")}' and Year='{date.Year}'");
                    if (dt.Rows.Count == 0)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "No record found",
                            error = true,
                            data = dt.toJsonObject(),
                        });

                    }
                    else
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "success",
                            error = false,
                            data = dt.toJsonObject(),
                        });
                    }

                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "salary-breakup")
                {
                    //var ds = PayrollMy.dataSet($"select * from Hr_Employee_Salary_Income_Head_Wise where Calculation_Id='{data["calc_id"]}' and Employee_Id='{data["Employee_Id"]}' ; select * from Hr_Employee_Salary_Deduction_Head_Wise where Calculation_Id='{data["calc_id"]}' and Employee_Id='{data["Employee_Id"]}'");

                    var ds = PayrollMy.dataSet($"select * from Hr_Employee_Salary_Income_Head_Wise where Calculation_Id='{data["calc_id"]}' and Employee_Id='{data["Employee_Id"]}' and (cast(NetValue as float) )>0 ; select * from Hr_Employee_Salary_Deduction_Head_Wise where Calculation_Id='{data["calc_id"]}' and Employee_Id='{data["Employee_Id"]}' and (cast(Employee_Deduction as float) )>0");

                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        incomes = ds.Tables[0].toJsonObject(),
                        deductions = ds.Tables[1].toJsonObject()
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "salary-calculation-details")
                {
                    var firm = My.data("select firm_name from Firm_Details");
                    var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                    var last_date = date.AddMonths(1).AddDays(-1);
                    var type = PayrollMy.isalaryCalculationManual ? "Yes" : "No";
                    var dt = PayrollMy.dataTable($"sp_HR_before_salary_calculation '{data["Year"]}','{data["Month"]}','{type}'");
                    var at = PayrollMy.dataTable($"select top 50 * from HR_Salary_And_Attendance_Rule");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Employee_id"].ToString() == "EMP024")
                        {
                            string a = dr["Employee_id"].ToString();
                        }
                        if (dr["week_off"].ToString() != dr["Calculated_Week_off"].ToString())
                        {
                            if (dr["week_off"].ToInt() == 5)
                            {
                                if (dr["p1"].ToDouble() / 6.0 > 3)
                                {
                                    dr["Calculated_Week_off"] = Math.Round((dr["p1"].ToDouble() / 6.0) + 1, 0);
                                }
                            }
                            else
                            {
                                if (dr["p1"].ToDouble() / 6.0 > 3 && dr["p1"].ToDouble() / 6.0 < 4)
                                {
                                    dr["Calculated_Week_off"] = Math.Round((dr["p1"].ToDouble() / 6.0) + 1, 0);

                                    if (dr["Calculated_Week_off"].ToDouble() >= 4)
                                    {
                                        dr["Calculated_Week_off"] = 4;
                                    }
                                }
                            }
                        }



                        dr["Paid_Days"] = dr["Paid_Days"].ToDouble() + dr["Calculated_Week_off"].ToDouble();
                        var Late_Comming_Days = dr["Late_Comming_Days"].ToInt();
                        double Full_Day_Deduction = 0;
                        double Half_Day_Deduction = 0;
                        if (at.Rows.Count > 0)
                        {
                            var Full_Day = at.Rows[0]["Full_Day_Leave_Count"].ToInt();
                            if (Full_Day > 0)
                            {
                                Full_Day_Deduction = Late_Comming_Days / Full_Day;
                            }
                            var Half_Day = at.Rows[0]["Halft_Day_Leave_Count"].ToInt();
                            if (Half_Day > 0)
                            {
                                int ss = Late_Comming_Days % Full_Day;
                                Half_Day_Deduction = ss / Half_Day;
                            }
                        }
                        double deduction_days = Full_Day_Deduction + (Half_Day_Deduction / 2.0);
                        dr["Late_Deduction"] = deduction_days;
                        dr["Paid_Days"] = dr["Paid_Days"].ToDouble() - deduction_days;
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        data = dt.toJsonObject()
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "calculate-now")
                {
                    var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                    if (PayrollMy.IsDataExist($"select * from HR_Salary_Calculation_Table where Month='{date.ToString("MMMM")}' and Year='{date.Year}'"))
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = $"Salary Already created for the month of {date.ToString("MMMM")} {data["Year"]}",
                            error = true,
                            recalculate = true,
                        });
                        return Json(resp, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return calculateSalary(data);
                    }
                }
                if (data["type"].ToString() == "re-calculate")
                {

                    var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                    var dt = PayrollMy.dataTable($"select * from HR_Salary_Calculation_Table where Month='{date.ToString("MMMM")}' and Year='{date.Year}'");
                    PayrollMy.execute($"delete from HR_Salary_Calculation_Table where Calculation_Id='{dt.Rows[0]["Calculation_Id"]}'; delete from Hr_Employee_Salary_Deduction_Head_Wise where Calculation_Id='{dt.Rows[0]["Calculation_Id"]}'; delete from Hr_Employee_Salary_Income_Head_Wise where Calculation_Id='{dt.Rows[0]["Calculation_Id"]}'; ");
                    return calculateSalary(data);

                }
                if (data["type"].ToString() == "latess")
                {

                    var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                    var days_in_month = DateTime.DaysInMonth(date.Year, date.Month);
                    var dt = PayrollMy.dataTable($"select  Employee_Name,Employee_Id,Emp_Code,'{data["Year"]}' Year, '{data["Month"]}' Month, {days_in_month} Days_in_Month,0 Working_Days_In_a_Month	,(select count(id) from HR_Roster_Wise_Weekoff_setting where EmployeeID=em.Employee_Id and Idate like '%{data["Year"]}{data["Month"]}%') roster_week_off,(select count(id) from HR_Calendar where idate like '%{data["Year"]}{data["Month"]}%'  and (select weekly_off from HR_Grade_Master where grade_id=em.Grade_Id)  like '%'+day_name+'%' ) default_week_off ,Grade_ID	,( select count(AttendanceStatus) from HR_Daily_Attendance_Record where AttendanceStatus='Present' and Employee_id=em.Employee_id) Full_Day_Persent , 0	Half_Day_Persent, 0	Late_Half_Day_Deduction, 0	Full_Day_Deduction, 0	Late_Comming_Days	, 0 Paid_Leave	, 0 UnPaidLeave	, 0 Half_Pay_leave  ,0.0 Working_Days,0.0 Dect_days,income_heads,deduction_heads,em.Department_id,(select top 1 grade_name from HR_Grade_Master where grade_id=em.Grade_Id) Grade  from HR_Employee_Master em");
                    var cal_id = PayrollMy.AutoId("Salary_Calculation_id", "SC", "0000");
                    foreach (DataRow dr in dt.Rows)
                    {
                        var dpm = PayrollMy.dataTable($"select dsm.name Designation,dpm.name Department from HR_Designation_Master dsm join HR_Department_Master dpm on dsm.DepartmentId=dpm.department_id where dsm.Designation_id='{dr["Department_id"]}'");
                        double total_working_days = 0;
                        double worked_days = 0;
                        var week_off = Convert.ToInt32(dr["roster_week_off"]);
                        if (week_off == 0)
                        {
                            week_off = Convert.ToInt32(dr["default_week_off"]);
                        }

                        dr["Working_Days_In_a_Month"] = total_working_days = days_in_month - week_off;

                        worked_days = dr["Full_Day_Persent"].ToInt() + (dr["Half_Day_Persent"].ToInt() / 2.0) + dr["Paid_Leave"].ToInt() + (dr["Half_Pay_leave"].ToInt() / 2.0);
                        dr["Working_Days"] = worked_days;
                        double deduction_days = dr["Full_Day_Deduction"].ToInt() + (dr["Late_Half_Day_Deduction"].ToInt() / 2.0);

                        dr["Dect_days"] = deduction_days;
                        double net_wroked_days = worked_days - deduction_days; //26-15


                        var income_heads = (new JavaScriptSerializer()).Deserialize<List<Dictionary<string, string>>>(dr["income_heads"].ToString());
                        var deduction_heads = (new JavaScriptSerializer()).Deserialize<List<Dictionary<string, string>>>(dr["deduction_heads"].ToString());

                        var basic = 0.0;
                        var variable_allwance = 0.0;
                        var allwance = 0.0;
                        foreach (var ih in income_heads)
                        {
                            if (ih["IsBasic"].ToString() == "Yes")
                            {
                                basic += ih["inc"].ToDouble();
                            }
                        }

                        foreach (var ih in income_heads)
                        {
                            if (ih["IsBasic"].ToString() == "No")
                            {
                                if (ih["IsVeriable"].ToString() == "No")
                                {
                                    allwance += ih["inc"].ToDouble();
                                }
                                else
                                {
                                    variable_allwance += ih["inc"].ToDouble();
                                }
                            }
                        }

                        var fixed_deduction = 0.0;
                        var fixed_deduction_employer = 0.0;
                        var deduction = 0.0;
                        var deduction_employer = 0.0;
                        foreach (var ih in deduction_heads)
                        {
                            if (ih["IsFixed"].ToString() == "Yes")
                            {
                                fixed_deduction += ih["deduct"].ToDouble();
                                fixed_deduction_employer += ih["deduct_emp"].ToDouble();
                            }
                            else if (ih["IsFixed"].ToString() == "No")
                            {
                                deduction += ih["deduct"].ToDouble();
                                deduction_employer += ih["deduct_emp"].ToDouble();
                            }
                        }
                        //working days wise calculation 

                        foreach (var ih in income_heads)
                        {
                            object ss = null;
                            if (ih["IsVeriable"].ToString() == "No")
                            {
                                ss = new
                                {
                                    Employee_Id = dr["Employee_Id"],
                                    Month = date.ToString("MMMM"),
                                    Year = date.Year,
                                    IncomeHead = ih["IncomeHead"],
                                    DefaultValue = ih["inc"],
                                    NetValue = ih["inc"],
                                    Calculation_Id = cal_id
                                };
                            }
                            else
                            {
                                ss = new
                                {
                                    Employee_Id = dr["Employee_Id"],
                                    Month = date.ToString("MMMM"),
                                    Year = date.Year,
                                    IncomeHead = ih["IncomeHead"],
                                    DefaultValue = ih["inc"],
                                    NetValue = Math.Round((ih["inc"].ToDouble() / total_working_days) * net_wroked_days, 0),
                                    Calculation_Id = cal_id
                                };
                            }

                            PayrollMy.Insert("Hr_Employee_Salary_Income_Head_Wise", ss);
                        }
                        foreach (var ih in deduction_heads)
                        {
                            object ss = null;
                            //if (ih["IsFixed"].ToString() == "Yes" || ih["DeductionType"].ToString() == "PF" || ih["DeductionType"].ToString() == "ESI")
                            //{
                            ss = new
                            {
                                Employee_Id = dr["Employee_Id"],
                                Month = date.ToString("MMMM"),
                                Year = date.Year,
                                Deduction_Head = ih["DeductionDesc"],
                                Employee_Deduction = ih["deduct"],
                                Employer_Deduction = ih["deduct_emp"],
                                Calculation_Id = cal_id
                            };
                            //}
                            //else if (ih["IsFixed"].ToString() == "No")
                            //{ 
                            //    ss = new
                            //    {
                            //        Employee_Id = dr["Employee_Id"],
                            //        Month = date.ToString("MMMM"),
                            //        Year = date.Year,
                            //        Deduction_Head = ih["DeductionDesc"],
                            //        Employee_Deduction = Math.Round((ih["deduct"].ToDouble() / total_working_days) * net_wroked_days, 0),
                            //        Employer_Deduction = Math.Round((ih["deduct_emp"].ToDouble() / total_working_days) * net_wroked_days, 0),
                            //    Calculation_Id = cal_id
                            //    };
                            //}
                            PayrollMy.Insert("Hr_Employee_Salary_Deduction_Head_Wise", ss);
                        }
                        if (dr["Late_Comming_Days"].ToDouble() > 0)
                        {
                            PayrollMy.Insert("Hr_Employee_Salary_Deduction_Head_Wise", new
                            {
                                Employee_Id = dr["Employee_Id"],
                                Month = date.ToString("MMMM"),
                                Year = date.Year,
                                Deduction_Head = $"Late Comming : {dr["Late_Comming_Days"]} Days",
                                Employee_Deduction = Math.Round((basic / total_working_days) * deduction_days, 0),
                                Employer_Deduction = 0,
                                Calculation_Id = cal_id
                            });
                        }
                        var net_basic_salary = Math.Round((basic / total_working_days) * worked_days, 0);

                        // var net_allwances = Math.Round(((variable_allwance / total_working_days) * worked_days) + allwance, 0);
                        var net_allwances = Math.Round((((variable_allwance + allwance) / total_working_days) * worked_days), 0);
                        var new_deduction = fixed_deduction + deduction + Math.Round((basic / total_working_days) * deduction_days, 0);
                        var new_gross = net_basic_salary + net_allwances;



                        PayrollMy.InsertOrUpdate("HR_Salary_Calculation_Table", new
                        {

                            EmployeeId = dr["Employee_Id"],
                            EmployeeName = dr["Employee_Name"],
                            EmployeeCode = dr["Emp_Code"],
                            Month = date.ToString("MMMM"),
                            Year = date.Year,
                            Days_in_Month = days_in_month,
                            Working_Days_In_a_Month = total_working_days,
                            Grade = dr["Grade"],
                            Full_Day_Present = dr["Full_Day_Persent"],
                            Half_Day_Present = dr["Half_Day_Persent"],
                            Late_Half_Day_Deduction = dr["Half_Day_Persent"],
                            Late_Full_Day_Deduction = dr["Full_Day_Deduction"],
                            Late_Coming_Days = dr["Late_Comming_Days"],
                            Paid_Leave = dr["Paid_Leave"],
                            UnPaidLeave = dr["UnPaidLeave"],
                            Half_Pay_leave = dr["Half_Pay_leave"],
                            Basic = basic,
                            Allwance = allwance + variable_allwance,
                            Gross = basic + allwance + variable_allwance,
                            Deduction = deduction + fixed_deduction,
                            Net = basic + allwance + variable_allwance - (deduction + fixed_deduction),
                            Calculeted_Basic = net_basic_salary,
                            Calculeted_Allwance = net_allwances,
                            Calculeted_Gross = net_basic_salary + net_allwances,
                            Calculeted_Deduction = new_deduction,
                            Calculeted_Net = new_gross - new_deduction,

                            Worked_Days = worked_days,
                            Deduction_Days = deduction_days,
                            Net_Wroked_Days = net_wroked_days,
                            Designation = dpm.Rows[0]["Designation"],
                            Department = dpm.Rows[0]["Department"],
                            Calculation_Id = cal_id,

                        }, "EmployeeId,Month,Year");


                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Salary Calculated Successfully.",
                        error = false,
                        data = dt.toJsonObject()
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }

                if (data["type"].ToString() == "salry-calculate-tt")
                {
                    var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                    var days_in_month = DateTime.DaysInMonth(date.Year, date.Month);
                    var dt = PayrollMy.dataTable($"select  Employee_Name,Employee_Id,Emp_Code,'{data["Year"]}' Year, '{data["Month"]}' Month, {days_in_month} Days_in_Month,0 Working_Days_In_a_Month	,(select count(id) from HR_Roster_Wise_Weekoff_setting where EmployeeID=em.Employee_Id and Idate like '%{data["Year"]}{data["Month"]}%') roster_week_off,(select count(id) from HR_Calendar where idate like '%{data["Year"]}{data["Month"]}%'  and (select weekly_off from HR_Grade_Master where grade_id=em.Grade_Id)  like '%'+day_name+'%' ) default_week_off ,Grade_ID	,( select count(AttendanceStatus) from HR_Daily_Attendance_Record where AttendanceStatus='Present' and Employee_id=em.Employee_id) Full_Day_Persent , 0	Half_Day_Persent, 0	Late_Half_Day_Deduction, 0	Full_Day_Deduction, 0	Late_Comming_Days	, 0 Paid_Leave	, 0 UnPaidLeave	, 0 Half_Pay_leave  ,0.0 Working_Days,0.0 Dect_days,income_heads,deduction_heads,em.Department_id,(select top 1 grade_name from HR_Grade_Master where grade_id=em.Grade_Id) Grade  from HR_Employee_Master em");
                    var cal_id = PayrollMy.AutoId("Salary_Calculation_id", "SC", "0000");
                    foreach (DataRow dr in dt.Rows)
                    {
                        var dpm = PayrollMy.dataTable($"select dsm.name Designation,dpm.name Department from HR_Designation_Master dsm join HR_Department_Master dpm on dsm.DepartmentId=dpm.department_id where dsm.Designation_id='{dr["Department_id"]}'");
                        double total_working_days = 0;
                        double worked_days = 0;
                        var week_off = Convert.ToInt32(dr["roster_week_off"]);
                        if (week_off == 0)
                        {
                            week_off = Convert.ToInt32(dr["default_week_off"]);
                        }

                        dr["Working_Days_In_a_Month"] = total_working_days = days_in_month - week_off;

                        worked_days = dr["Full_Day_Persent"].ToInt() + (dr["Half_Day_Persent"].ToInt() / 2.0) + dr["Paid_Leave"].ToInt() + (dr["Half_Pay_leave"].ToInt() / 2.0);
                        dr["Working_Days"] = worked_days;
                        double deduction_days = dr["Full_Day_Deduction"].ToInt() + (dr["Late_Half_Day_Deduction"].ToInt() / 2.0);

                        dr["Dect_days"] = deduction_days;
                        double net_wroked_days = worked_days - deduction_days; //26-15


                        var income_heads = (new JavaScriptSerializer()).Deserialize<List<Dictionary<string, string>>>(dr["income_heads"].ToString());
                        var deduction_heads = (new JavaScriptSerializer()).Deserialize<List<Dictionary<string, string>>>(dr["deduction_heads"].ToString());

                        var basic = 0.0;
                        var variable_allwance = 0.0;
                        var allwance = 0.0;
                        foreach (var ih in income_heads)
                        {
                            if (ih["IsBasic"].ToString() == "Yes")
                            {
                                basic += ih["inc"].ToDouble();
                            }
                        }

                        foreach (var ih in income_heads)
                        {
                            if (ih["IsBasic"].ToString() == "No")
                            {
                                if (ih["IsVeriable"].ToString() == "No")
                                {
                                    allwance += ih["inc"].ToDouble();
                                }
                                else
                                {
                                    variable_allwance += ih["inc"].ToDouble();
                                }
                            }
                        }

                        var fixed_deduction = 0.0;
                        var fixed_deduction_employer = 0.0;
                        var deduction = 0.0;
                        var deduction_employer = 0.0;
                        foreach (var ih in deduction_heads)
                        {
                            if (ih["IsFixed"].ToString() == "Yes")
                            {
                                fixed_deduction += ih["deduct"].ToDouble();
                                fixed_deduction_employer += ih["deduct_emp"].ToDouble();
                            }
                            else if (ih["IsFixed"].ToString() == "No")
                            {
                                deduction += ih["deduct"].ToDouble();
                                deduction_employer += ih["deduct_emp"].ToDouble();
                            }
                        }
                        //working days wise calculation 

                        foreach (var ih in income_heads)
                        {
                            object ss = null;
                            if (ih["IsVeriable"].ToString() == "No")
                            {
                                ss = new
                                {
                                    Employee_Id = dr["Employee_Id"],
                                    Month = date.ToString("MMMM"),
                                    Year = date.Year,
                                    IncomeHead = ih["IncomeHead"],
                                    DefaultValue = ih["inc"],
                                    NetValue = ih["inc"],
                                    Calculation_Id = cal_id
                                };
                            }
                            else
                            {
                                ss = new
                                {
                                    Employee_Id = dr["Employee_Id"],
                                    Month = date.ToString("MMMM"),
                                    Year = date.Year,
                                    IncomeHead = ih["IncomeHead"],
                                    DefaultValue = ih["inc"],
                                    NetValue = Math.Round((ih["inc"].ToDouble() / total_working_days) * net_wroked_days, 0),
                                    Calculation_Id = cal_id
                                };
                            }

                            PayrollMy.Insert("Hr_Employee_Salary_Income_Head_Wise", ss);
                        }
                        foreach (var ih in deduction_heads)
                        {
                            object ss = null;
                            //if (ih["IsFixed"].ToString() == "Yes" || ih["DeductionType"].ToString() == "PF" || ih["DeductionType"].ToString() == "ESI")
                            //{
                            ss = new
                            {
                                Employee_Id = dr["Employee_Id"],
                                Month = date.ToString("MMMM"),
                                Year = date.Year,
                                Deduction_Head = ih["DeductionDesc"],
                                Employee_Deduction = ih["deduct"],
                                Employer_Deduction = ih["deduct_emp"],
                                Calculation_Id = cal_id
                            };
                            //}
                            //else if (ih["IsFixed"].ToString() == "No")
                            //{ 
                            //    ss = new
                            //    {
                            //        Employee_Id = dr["Employee_Id"],
                            //        Month = date.ToString("MMMM"),
                            //        Year = date.Year,
                            //        Deduction_Head = ih["DeductionDesc"],
                            //        Employee_Deduction = Math.Round((ih["deduct"].ToDouble() / total_working_days) * net_wroked_days, 0),
                            //        Employer_Deduction = Math.Round((ih["deduct_emp"].ToDouble() / total_working_days) * net_wroked_days, 0),
                            //    Calculation_Id = cal_id
                            //    };
                            //}
                            PayrollMy.Insert("Hr_Employee_Salary_Deduction_Head_Wise", ss);
                        }
                        if (dr["Late_Comming_Days"].ToDouble() > 0)
                        {
                            PayrollMy.Insert("Hr_Employee_Salary_Deduction_Head_Wise", new
                            {
                                Employee_Id = dr["Employee_Id"],
                                Month = date.ToString("MMMM"),
                                Year = date.Year,
                                Deduction_Head = $"Late Comming : {dr["Late_Comming_Days"]} Days",
                                Employee_Deduction = Math.Round((basic / total_working_days) * deduction_days, 0),
                                Employer_Deduction = 0,
                                Calculation_Id = cal_id
                            });
                        }
                        var net_basic_salary = Math.Round((basic / total_working_days) * worked_days, 0);

                        // var net_allwances = Math.Round(((variable_allwance / total_working_days) * worked_days) + allwance, 0);
                        var net_allwances = Math.Round((((variable_allwance + allwance) / total_working_days) * worked_days), 0);
                        var new_deduction = fixed_deduction + deduction + Math.Round((basic / total_working_days) * deduction_days, 0);
                        var new_gross = net_basic_salary + net_allwances;



                        PayrollMy.InsertOrUpdate("HR_Salary_Calculation_Table", new
                        {

                            EmployeeId = dr["Employee_Id"],
                            EmployeeName = dr["Employee_Name"],
                            EmployeeCode = dr["Emp_Code"],
                            Month = date.ToString("MMMM"),
                            Year = date.Year,
                            Days_in_Month = days_in_month,
                            Working_Days_In_a_Month = total_working_days,
                            Grade = dr["Grade"],
                            Full_Day_Present = dr["Full_Day_Persent"],
                            Half_Day_Present = dr["Half_Day_Persent"],
                            Late_Half_Day_Deduction = dr["Half_Day_Persent"],
                            Late_Full_Day_Deduction = dr["Full_Day_Deduction"],
                            Late_Coming_Days = dr["Late_Comming_Days"],
                            Paid_Leave = dr["Paid_Leave"],
                            UnPaidLeave = dr["UnPaidLeave"],
                            Half_Pay_leave = dr["Half_Pay_leave"],
                            Basic = basic,
                            Allwance = allwance + variable_allwance,
                            Gross = basic + allwance + variable_allwance,
                            Deduction = deduction + fixed_deduction,
                            Net = basic + allwance + variable_allwance - (deduction + fixed_deduction),
                            Calculeted_Basic = net_basic_salary,
                            Calculeted_Allwance = net_allwances,
                            Calculeted_Gross = net_basic_salary + net_allwances,
                            Calculeted_Deduction = new_deduction,
                            Calculeted_Net = new_gross - new_deduction,

                            Worked_Days = worked_days,
                            Deduction_Days = deduction_days,
                            Net_Wroked_Days = net_wroked_days,
                            Designation = dpm.Rows[0]["Designation"],
                            Department = dpm.Rows[0]["Department"],
                            Calculation_Id = cal_id,

                        }, "EmployeeId,Month,Year");


                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Ping success",
                        error = false,
                        data = dt.toJsonObject()
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }

            }
            if (dataId == "api")
            {
                if (data.ContainsKey("code"))
                {
                    if (data["code"].ToString() != "")
                    {
                        var sqt = PayrollMy.dataTable($"select * from HR_SQL_API where Api_Code='{data["code"]}'");
                        if (sqt.Rows.Count > 0)
                        {
                            try
                            {
                                var dc = processDataQuery(sqt.Rows[0]["Api_Query"].ToString(), data, "");
                                var ds = PayrollMy.dataSet(dc);
                                var lst = new List<object>();
                                foreach (DataTable dt in ds.Tables)
                                {
                                    lst.Add(dt.toJsonObject());
                                }
                                resp = (new JavaScriptSerializer()).Serialize(new
                                {
                                    message = sqt.Rows[0]["SuccessMessage"].ToString(),
                                    error = false,
                                    data = lst,
                                });
                            }
                            catch
                            {

                            }
                        }


                    }
                }
            }

            if (dataId == "dbms")
            {
                if (data["type"].ToString() == "table")
                {
                    var dt = PayrollMy.dataTable("select TABLE_NAME from information_schema.tables where TABLE_TYPE='BASE TABLE' order by TABLE_NAME ");

                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Ping success",
                        error = false,
                        data = dt.toJsonObject()
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "execute")
                {
                    try
                    {
                        var ds = PayrollMy.dataSet(data["qry"].ToString());
                        string pattern = @"\bSELECT\b.*?(?=\bSELECT\b|$)";
                        MatchCollection matches = Regex.Matches(data["qry"].ToString(), pattern, RegexOptions.IgnoreCase);
                        var tables = new List<string>();
                        foreach (Match m in matches)
                        {
                            string pattern1 = @"(?i)\bSELECT\b(.*?)\bFROM\b";
                            MatchCollection matches1 = Regex.Matches(m.Value, pattern1, RegexOptions.IgnoreCase);
                            string pattern11 = @"\bFROM\b\s+(?<TableName>[^\s]+)";
                            Match m2 = Regex.Match(m.Value, pattern11, RegexOptions.IgnoreCase);
                            if (m2.Success)
                            {
                                tables.Add(m2.Groups["TableName"].Value);
                            }

                        }



                        var reloadTable = false;
                        var lst = new List<object>();
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataTable _dt in ds.Tables)
                            {
                                var cols = columnLost(_dt);
                                var canEdit = false;
                                var tableName = "";
                                //var canEdit = cols.Contains("Id"); 
                                if (!canEdit)
                                {
                                    canEdit = cols.Contains("id");
                                }
                                lst.Add(new
                                {
                                    canEdit = canEdit,
                                    tableName = tableName,
                                    cols = cols,
                                    data = _dt.toJsonObject(),
                                });
                            }
                            reloadTable = true;
                        }
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Success",
                            error = false,
                            reloadTable = reloadTable,
                            data = lst
                        }); ;
                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }

                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
            }
            if (dataId == "device")
            {
                if (data["type"].ToString() == "ping")
                {
                    var isping = UniversalStatic.PingTheDevice("192.168.1.201");
                    if (isping)
                    {

                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Ping success",
                            error = false,
                        });
                    }
                }
                if (data["type"].ToString() == "connect")
                {

                    try
                    {
                        var ip = PayrollMy.data("select DeviceIP from HR_Attendance_Device");

                        if (Device.Connect(ip))
                        {
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Device Connected",
                                error = false,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }
                }
                if (data["type"].ToString() == "delete-user")
                {

                    try
                    {
                        //var ip = PayrollMy.data("select DeviceIP from HR_Attendance_Device");

                        //if (Device.Connect(ip))
                        //{
                        //    var ar = Device.manipulator.ClearData(Device.objZkeeper, 1, MyCode.ClearFlag.UserData);
                        //    resp = (new JavaScriptSerializer()).Serialize(new
                        //    {
                        //        message = "All user data deleted from device.",
                        //        error = false,
                        //    });
                        //}
                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }

                }
                if (data["type"].ToString() == "upload-user")
                {
                    try
                    {
                        var ip = PayrollMy.data("select DeviceIP from HR_Attendance_Device");

                        if (Device.Connect(ip))
                        {
                            var dt = PayrollMy.dataTable("select Emp_Code,Employee_Name from HR_Employee_Master");
                            foreach (DataRow dr in dt.Rows)
                            {
                                Device.pushUser(dr[1].ToString(), dr[0].ToString());
                            }
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "All employee data upload to device.",
                                error = false,
                            });
                        }
                        else
                        {
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Unable to connect device.",
                                error = true,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }

                }
                if (data["type"].ToString() == "get-user")
                {

                    try
                    {
                        var ip = PayrollMy.data("select DeviceIP from HR_Attendance_Device");

                        if (Device.Connect(ip))
                        {
                            var ru = Device.manipulator.GetAllUserInfo(Device.objZkeeper, 1);
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "User downloaded",
                                error = false,
                                data = ru,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }
                }
                if (data["type"].ToString() == "fetch-attendance-old")
                {

                    try
                    {
                        var ip = PayrollMy.data("select DeviceIP from HR_Attendance_Device");

                        if (Device.Connect(ip))
                        {
                            var lst = Device.GetAttendanceLog();
                            Device.SaveAttendanceLogToCsv(lst, Server.MapPath("~/AttendanceLog"));
                            var et = PayrollMy.dataTable("select Emp_Code, Employee_id from HR_Employee_Master");
                            var emp = new Dictionary<string, string>();
                            foreach (DataRow dr in et.Rows)
                            {
                                emp[dr["Emp_Code"].ToString()] = dr["Employee_id"].ToString();
                            }
                            var mt = PayrollMy.Table("HR_Attendance_Log", where: $"DateTime>='{PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01")} 00:00:01 AM'");

                            var mt2 = PayrollMy.Table("HR_Daily_Attendance_Record", where: $"Idate>='{PayrollMy.Now.AddMonths(-3).ToString("yyyyMM01")}'");

                            var latdate = Convert.ToDateTime(PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01") + " 00:00:01 AM");
                            foreach (var a in lst)
                            {
                                if (a.Date < latdate)
                                {
                                    continue;
                                }
                                if (mt.dt.Select($"Emp_Code='{a.Id}' and Itime='{a.Date.ToString("yyMMddHHmmss")}'").Length == 0)
                                {
                                    var dr = mt.NewRow();
                                    dr["Employee_id"] = emp.ContainsKey(a.Id) ? emp[a.Id] : "";
                                    dr["DateTime"] = a.Date;
                                    dr["Itime"] = a.Date.ToString("yyMMddHHmmss");
                                    dr["Emp_Code"] = a.Id;
                                    mt.Rows.Add(dr);
                                }
                                if (emp.ContainsKey(a.Id))
                                {
                                    var drs = mt2.dt.Select($"Employee_id='{emp[a.Id]}' and Idate='{a.Date.ToString("yyyyMMdd")}'");
                                    if (drs.Length == 0)
                                    {
                                        var dr = mt2.NewRow();
                                        dr["Employee_id"] = emp[a.Id];
                                        dr["Date"] = a.Date.ToString("dd-MMM-yyyy");
                                        dr["In_Time"] = a.Date.ToString("hh:mm:ss tt");
                                        // dr["Out_Time"] = "";
                                        dr["AttendanceSource"] = "Device Punch";
                                        dr["Shift_Id"] = 0;
                                        dr["Idate"] = a.Date.ToString("yyyyMMdd");
                                        mt2.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        var dr = drs[0];
                                        if (dr["In_Time"].ToString() == a.Date.ToString("hh:mm:ss tt"))
                                        {

                                        }
                                        else if (dr["Out_Time"].ToString() == "")
                                        {
                                            var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                            if ((a.Date - intime).TotalSeconds > 30)
                                            {
                                                dr["Out_Time"] = a.Date.ToString("hh:mm:ss tt");
                                            }

                                        }
                                        else
                                        {
                                            var lastpucnch = Convert.ToDateTime($"{dr["Date"]} {dr["Out_Time"]}");
                                            if (lastpucnch < a.Date)
                                            {
                                                dr["Out_Time"] = a.Date.ToString("hh:mm:ss tt");
                                            }
                                        }
                                    }
                                }

                            }
                            mt.Update();
                            mt2.Update();
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Attendance fetched successfully.",
                                error = false,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }
                }

                if (data["type"].ToString() == "fetch-attendance")
                {

                    try
                    {
                        var ip = PayrollMy.data("select DeviceIP from HR_Attendance_Device");

                        if (Device.Connect(ip))
                        {
                            var lst = Device.GetAttendanceLog();
                            Device.SaveAttendanceLogToCsv(lst, Server.MapPath("~/AttendanceLog"));
                            var et = PayrollMy.dataTable("select Emp_Code, Employee_id from HR_Employee_Master");
                            var emp = new Dictionary<string, string>();
                            foreach (DataRow dr in et.Rows)
                            {
                                emp[dr["Emp_Code"].ToString()] = dr["Employee_id"].ToString();
                            }
                            var mt = PayrollMy.Table("HR_Attendance_Log", where: $"DateTime>='{PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01")} 00:00:01 AM'");
                            var mt2 = PayrollMy.Table("HR_Daily_Attendance_Record", where: $"Idate>='{PayrollMy.Now.AddMonths(-3).ToString("yyyyMM01")}'");
                            var sdt = PayrollMy.dataTable($"select dr.EmployeeId,sf.*,Start_IDate,End_IDate from HR_Duty_Roster   dr join HR_ShiftSetting sf on dr.Shift_Id=sf.ShiftId and sf.Status=1 join HR_Rotation_Details rd on dr.RotationId =rd.RotationId and rd.Status=1 and Start_IDate>={PayrollMy.Now.AddMonths(-3).AddDays(-1).ToString("yyyyMM01")}");
                            var cs_shift = PayrollMy.dataTable($"select sc.Employee_Id, ss.*,Idate from HR_Date_Wise_Shift_Customized sc join HR_ShiftSetting  ss on sc.Shift_Id=ss.ShiftId where idate >=   '{PayrollMy.Now.AddMonths(-3).AddDays(-1).ToString("yyyyMM01")}'");
                            var latdate = Convert.ToDateTime(PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01") + " 00:00:01 AM");
                            var at = PayrollMy.dataTable("select top 1 * from HR_Salary_And_Attendance_Rule");
                            var allow_late_come = 30;
                            var allow_early_leave = 30;
                            if (at.Rows.Count > 0)
                            {
                                allow_late_come = at.Rows[0]["Allowed_Late_Come"].ToInt();
                                allow_early_leave = at.Rows[0]["Allowed_Early_Leave"].ToInt();
                            }
                            foreach (var a in lst)
                            {
                                if (a.Date < latdate)
                                {
                                    continue;
                                }


                                if (mt.dt.Select($"Emp_Code='{a.Id}' and Itime='{a.Date.ToString("yyMMddHHmmss")}'").Length == 0)
                                {
                                    var dr = mt.NewRow();
                                    dr["Employee_id"] = emp.ContainsKey(a.Id) ? emp[a.Id] : "";
                                    dr["DateTime"] = a.Date;
                                    dr["Itime"] = a.Date.ToString("yyMMddHHmmss");
                                    dr["Emp_Code"] = a.Id;
                                    mt.Rows.Add(dr);
                                }
                                if (emp.ContainsKey(a.Id))
                                {

                                    var date = a.Date;
                                    var prev_date = date.AddDays(-1);
                                    var attendance_idate = date.ToString("yyyyMMdd");
                                    var attendance_date = date;
                                    var time = date.ToString("hh:mm:ss tt");
                                    var isNightShift = false;
                                    var employee_id = emp[a.Id];
                                    var isInTimeAttendance = true;
                                    var isShiftAssigned = false;
                                    var isCurrentDay = true;
                                    var shift_Id = "0";
                                    var shift_intime = DateTime.Now;
                                    var shift_outtime = DateTime.Now;
                                    #region prev day  
                                    var sdrs = cs_shift.Select($"Employee_Id='{employee_id}' and Idate = '{prev_date.ToString("yyyyMMdd")}'");
                                    if (sdrs.Length == 0)
                                    {
                                        sdrs = sdt.Select($"EmployeeId='{employee_id}' and Start_IDate  <= {prev_date.ToString("yyyyMMdd")}   and End_IDate>='{prev_date.ToString("yyyyMMdd")}'");
                                    }
                                    if (sdrs.Length > 0)
                                    {
                                        var r = sdrs[0];
                                        shift_intime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["In_Time"]}");//8:00pm
                                        shift_outtime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["Out_Time"]}");//8:00am

                                        //if attendance time near previous day's shift outtime  we take 3 hour range employee may leave 3 hour early or may work 3 hour late work due to over time 
                                        if (date >= shift_outtime.AddHours(-3) && date <= shift_outtime.AddHours(3))
                                        {
                                            isCurrentDay = false;
                                            attendance_idate = prev_date.ToString("yyyyMMdd");
                                            attendance_date = prev_date;
                                        }
                                    }
                                    #endregion
                                    if (isCurrentDay)
                                    {
                                        sdrs = cs_shift.Select($"Employee_Id='{employee_id}' and Idate = '{attendance_idate}'");
                                        if (sdrs.Length == 0)
                                        {
                                            sdrs = sdt.Select($"EmployeeId='{employee_id}' and Start_IDate  <= {attendance_idate}   and End_IDate>='{attendance_idate}'");
                                        }
                                    }

                                    if (sdrs.Length > 0)
                                    {
                                        var r = sdrs[0];
                                        isShiftAssigned = true;
                                        shift_intime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["In_Time"]}");
                                        shift_outtime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["Out_Time"]}");
                                        if (shift_outtime < shift_intime)
                                        {
                                            isNightShift = true;
                                            shift_outtime = shift_outtime.AddDays(1);
                                        }
                                        shift_Id = r["ShiftId"].ToString();
                                        if (date >= shift_outtime.AddHours(-3) && date <= shift_outtime.AddHours(3))
                                        {
                                            isInTimeAttendance = false;
                                        }
                                    }

                                    var drs = mt2.dt.Select($"Employee_id='{employee_id}' and Idate='{attendance_idate}'");

                                    var dr = drs.Length == 0 ? mt2.NewRow() : drs[0];
                                    if (drs.Length == 0)
                                    {

                                        dr["Employee_id"] = employee_id;
                                        dr["Date"] = attendance_date.ToString("dd-MMM-yyyy");
                                        dr["In_Time"] = time;
                                        dr["AttendanceSource"] = "Device Punch";
                                        dr["Idate"] = attendance_idate;
                                        dr["IsNightShift"] = isNightShift;
                                        dr["Shift_Id"] = shift_Id;
                                        dr["AttendanceStatus"] = "In Office";
                                        if (isShiftAssigned)
                                        {
                                            if (!isInTimeAttendance)
                                            {
                                                dr["In_Time"] = DBNull.Value;
                                                dr["Out_Time"] = time;
                                                //var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                                if ((shift_outtime - date).TotalMinutes >= 1)
                                                {
                                                    dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                                }
                                                if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                                {
                                                    dr["IsEarlyLeave"] = 1;
                                                }
                                                // var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                                dr["Total_Wroking_Time"] = 0;
                                                dr["AttendanceStatus"] = "Persent";
                                            }
                                            else
                                            {
                                                if ((date - shift_intime).TotalMinutes >= 1)
                                                {
                                                    dr["Late_Minute"] = (int)(date - shift_intime).TotalMinutes;
                                                }
                                                if ((date - shift_intime).TotalMinutes >= allow_late_come)
                                                {
                                                    dr["IsLate"] = 1;
                                                }
                                            }


                                        }
                                        mt2.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        if (dr["In_Time"].ToString() == time)
                                        {
                                            continue;
                                        }
                                        bool is_out_time_changed = false;
                                        if (dr["Out_Time"].ToString() == "")
                                        {
                                            var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                            if ((date - intime).TotalSeconds > 50)
                                            {
                                                dr["Out_Time"] = time;
                                                is_out_time_changed = true;
                                            }
                                        }
                                        else
                                        {
                                            var lastpucnch = Convert.ToDateTime($"{dr["Date"]} {dr["Out_Time"]}");
                                            if (lastpucnch < date)
                                            {
                                                dr["Out_Time"] = time;
                                                is_out_time_changed = true;
                                            }
                                        }
                                        if (isShiftAssigned && is_out_time_changed)
                                        {
                                            var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                            if ((shift_outtime - date).TotalMinutes >= 1)
                                            {
                                                dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                            }
                                            if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                            {
                                                dr["IsEarlyLeave"] = 1;
                                            }
                                            var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                            dr["Total_Wroking_Time"] = Total_Wroking_Time;
                                            dr["AttendanceStatus"] = "Persent";
                                        }

                                    }
                                }

                            }
                            mt.Update();
                            mt2.Update();
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Attendance fetched successfully.",
                                error = false,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }
                }
                if (data["type"].ToString() == "rearrange-attendance")
                {
                    try
                    {
                        var mt = PayrollMy.Table("HR_Attendance_Log", where: $"DateTime>='{PayrollMy.Now.AddMonths(-3).ToString("yyyy-MM-01")} 00:00:01 AM'");
                        var sdt = PayrollMy.dataTable($"select dr.EmployeeId,sf.*,Start_IDate,End_IDate from HR_Duty_Roster   dr join HR_ShiftSetting sf on dr.Shift_Id=sf.ShiftId and sf.Status=1 join HR_Rotation_Details rd on dr.RotationId =rd.RotationId and rd.Status=1 and Start_IDate>={PayrollMy.Now.AddMonths(-3).AddDays(-1).ToString("yyyyMM01")}");

                        var mt2 = PayrollMy.Table("HR_Daily_Attendance_Record", where: $"Idate>='{PayrollMy.Now.AddMonths(-3).ToString("yyyyMM01")}'");
                        var allow_late_come = 30;
                        var allow_early_leave = 30;
                        var at = PayrollMy.dataTable("select top 1 * from HR_Salary_And_Attendance_Rule");
                        if (at.Rows.Count > 0)
                        {

                        }
                        foreach (DataRow row in mt.dt.Rows)
                        {
                            var date = Convert.ToDateTime(row["DateTime"]);
                            var prev_date = date.AddDays(-1);
                            var attendance_idate = date.ToString("yyyyMMdd");
                            var attendance_date = date;
                            var time = date.ToString("hh:mm:ss tt");
                            var isNightShift = false;
                            var isInTimeAttendance = true;
                            var isShiftAssigned = false;
                            var isCurrentDay = true;
                            var shift_Id = "0";
                            var shift_intime = DateTime.Now;
                            var shift_outtime = DateTime.Now;
                            #region prev day  
                            var sdrs = sdt.Select($"EmployeeId='{row["Employee_id"]}' and Start_IDate  <= {prev_date.ToString("yyyyMMdd")}   and End_IDate>='{prev_date.ToString("yyyyMMdd")}'");
                            if (sdrs.Length > 0)
                            {
                                var r = sdrs[0];
                                shift_intime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["In_Time"]}");//8:00pm
                                shift_outtime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["Out_Time"]}");//8:00am

                                //if attendance time near previous day's shift outtime  we take 3 hour range employee may leave 3 hour early or may work 3 hour late work due to over time 
                                if (date >= shift_outtime.AddHours(-3) && date <= shift_outtime.AddHours(3))
                                {
                                    isCurrentDay = false;
                                    attendance_idate = prev_date.ToString("yyyyMMdd");
                                    attendance_date = prev_date;
                                }
                            }
                            #endregion
                            if (isCurrentDay)
                                sdrs = sdt.Select($"EmployeeId='{row["Employee_id"]}' and Start_IDate  <= {attendance_idate}   and End_IDate>='{attendance_idate}'");
                            if (sdrs.Length > 0)
                            {
                                var r = sdrs[0];
                                isShiftAssigned = true;
                                shift_intime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["In_Time"]}");
                                shift_outtime = Convert.ToDateTime($"{date.ToString("dd-MMM-yyyy")} {r["Out_Time"]}");
                                if (shift_outtime < shift_intime)
                                {
                                    isNightShift = true;
                                    shift_outtime = shift_outtime.AddDays(1);
                                }

                                if (date >= shift_outtime.AddHours(-3) && date <= shift_outtime.AddHours(3))
                                {
                                    isInTimeAttendance = false;
                                }
                            }

                            var drs = mt2.dt.Select($"Employee_id='{row["Employee_id"]}' and Idate='{attendance_idate}'");

                            var dr = drs.Length == 0 ? mt2.NewRow() : drs[0];
                            if (drs.Length == 0)
                            {

                                dr["Employee_id"] = row["Employee_id"];
                                dr["Date"] = attendance_date.ToString("dd-MMM-yyyy");
                                dr["In_Time"] = time;
                                dr["AttendanceSource"] = "Device Punch";
                                dr["Idate"] = attendance_idate;
                                dr["IsNightShift"] = isNightShift;
                                dr["Shift_Id"] = shift_Id;
                                dr["AttendanceStatus"] = "In Office";
                                if (isShiftAssigned)
                                {
                                    if (!isInTimeAttendance)
                                    {
                                        dr["In_Time"] = DBNull.Value;
                                        dr["Out_Time"] = time;
                                        //var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                        if ((shift_outtime - date).TotalMinutes >= 1)
                                        {
                                            dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                        }
                                        if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                        {
                                            dr["IsEarlyLeave"] = 1;
                                        }
                                        // var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                        dr["Total_Wroking_Time"] = 0;
                                        dr["AttendanceStatus"] = "Persent";
                                    }
                                    else
                                    {
                                        if ((date - shift_intime).TotalMinutes >= 1)
                                        {
                                            dr["Late_Minute"] = (int)(date - shift_intime).TotalMinutes;
                                        }
                                        if ((date - shift_intime).TotalMinutes >= allow_late_come)
                                        {
                                            dr["IsLate"] = 1;
                                        }
                                    }


                                }
                                mt2.Rows.Add(dr);
                            }
                            else
                            {
                                if (dr["In_Time"].ToString() == time)
                                {
                                    continue;
                                }
                                bool is_out_time_changed = false;
                                if (dr["Out_Time"].ToString() == "")
                                {
                                    var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                    if ((date - intime).TotalSeconds > 50)
                                    {
                                        dr["Out_Time"] = time;
                                        is_out_time_changed = true;
                                    }
                                }
                                else
                                {
                                    var lastpucnch = Convert.ToDateTime($"{dr["Date"]} {dr["Out_Time"]}");
                                    if (lastpucnch < date)
                                    {
                                        dr["Out_Time"] = time;
                                        is_out_time_changed = true;
                                    }
                                }
                                if (isShiftAssigned && is_out_time_changed)
                                {
                                    var intime = Convert.ToDateTime($"{dr["Date"]} {dr["In_Time"]}");
                                    if ((shift_outtime - date).TotalMinutes >= 1)
                                    {
                                        dr["Early_Leave_Minute"] = (int)(shift_outtime - date).TotalMinutes;
                                    }
                                    if ((date - shift_intime).TotalMinutes >= allow_early_leave)
                                    {
                                        dr["IsEarlyLeave"] = 1;
                                    }
                                    var Total_Wroking_Time = (int)(date - intime).TotalMinutes;
                                    dr["Total_Wroking_Time"] = Total_Wroking_Time;
                                    dr["AttendanceStatus"] = "Persent";
                                }

                            }
                        }
                        mt2.Update();
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Attendance fetched successfully.",
                            error = false,
                        });

                    }
                    catch (Exception ex)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = ex.Message,
                            error = true,
                        });
                    }
                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (dataId == "table")
            {
                var dt = PayrollMy.dataTable($"select  COLUMN_NAME  from information_schema.columns where TABLE_NAME='{data["table"]}' and COLUMN_NAME!= 'Id'");

                return Json(dt.toJsonString(), JsonRequestBehavior.AllowGet);
            }

            if (dataId == "edit")
            {
                var mpc = getMPCById(data["pageId"].ToString());
                if (mpc.methodId == "duty-hour")
                {
                    var dt = PayrollMy.dataTable($"select  *,'' extra  from {mpc.tableName} where id='{data["id"]}'");
                    var dt1 = PayrollMy.dataTable($"select * from HR_ShiftSetting where DutyHourId='{dt.Rows[0]["Duty_Hour_id"]}' and Status=1");
                    dt.Rows[0]["extra"] = dt1.toJsonString();
                    return Json(dt.toJsonString(), JsonRequestBehavior.AllowGet);
                }
                else if (mpc.methodId == "rotation-duration")
                {
                    //select  * from HR_Rotation_Details where RotationHeadId=''
                    var dt = PayrollMy.dataTable($"select  *,'' extra  from {mpc.tableName} where id='{data["id"]}'");
                    var dt1 = PayrollMy.dataTable($"select * from HR_Rotation_Details where RotationHeadId='{dt.Rows[0]["RotationHeadId"]}' and Status=1");
                    dt.Rows[0]["extra"] = dt1.toJsonString();
                    return Json(dt.toJsonString(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var dt = PayrollMy.dataTable($"select  *  from {mpc.tableName} where id='{data["id"]}'");
                    return Json(dt.toJsonString(), JsonRequestBehavior.AllowGet);
                }


            }
            if (dataId == "delete")
            {
                try
                {
                    var mpc = getMPCById(data["pageId"].ToString());
                    if (PayrollMy.isHospital)
                    {
                        if (mpc.methodId == "departments")
                        {
                            if (PayrollMy.isHospital)
                            {
                                PayrollMy.execute($"delete from  HMS_Department_Master  where Dept_id in (select department_id from HR_Department_Master where Id='" + data["id"] + "')");
                            }
                        }
                    }
                    if (mpc.methodId == "duty-hour")
                    {
                        // select Duty_Hour_id from HR_Duty_Hours where Id='{data["id"]}'
                        PayrollMy.execute($"delete from  HR_ShiftSetting  where DutyHourId in (select Duty_Hour_id from HR_Duty_Hours where Id='{data["id"]}')");
                    }
                    else if (mpc.methodId == "rotation-duration")
                    {
                        PayrollMy.execute($"delete from  HR_Rotation_Details  where RotationHeadId in (select RotationHeadId from  {mpc.tableName}  where Id='" + data["id"] + "')");
                    }
                    PayrollMy.execute($"delete from  {mpc.tableName}  where id='{data["id"]}'");

                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        message = "Data Deleted Successfully",
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = true,
                        message = "Unable to Delete Data",
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);

                }


            }
            if (dataId == "page")
            {
                var dt = PayrollMy.dataTable($"select * from HR_MasterPageConfig where pageId='{data["pageId"]}'");
                var new_dt = PayrollMy.dataTable($"select  COLUMN_NAME  from information_schema.columns where TABLE_NAME='{dt.Rows[0]["tableName"]}' and COLUMN_NAME!= 'Id'");
                List<string> lst = new List<string>();
                foreach (System.Data.DataRow dr in new_dt.Rows)
                {
                    lst.Add(dr[0].ToString());
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    error = false,
                    cols = lst.ToArray(),
                    data = dt.toJsonObject(),
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (dataId == "attendance")
            {

                if (data["type"].ToString() == "daily-attendance")
                {
                    var idate = Convert.ToDateTime(data["Date"]).ToString("yyyyMMdd");
                    var dt1 = PayrollMy.dataTable($"select * from HR_Employee_Master   where Employee_id='{data["EmployeeId"]}'");
                    var dt2 = PayrollMy.dataTable($"select *,'Working' Day_Status  from HR_Daily_Attendance_Record  where Employee_id='{data["EmployeeId"]}' and Idate='{idate}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = dt2.toJsonObject(),
                        details = dt1.toJsonObject(),
                    });
                }
                if (data["type"].ToString() == "monthly-attendance")
                {
                    var dt1 = PayrollMy.dataTable($"select * from HR_Employee_Master   where Employee_id='{data["EmployeeId"]}'");
                    var dt2 = PayrollMy.dataTable($"select *,'Working' Day_Status  from HR_Daily_Attendance_Record  where Employee_id='{data["EmployeeId"]}' and Idate like '{data["Year"]}{data["Month"]}%' order by idate");
                    var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = dt2.toJsonObject(),
                        details = new
                        {
                            Employee_Name = dt1.Rows[0]["Employee_Name"],
                            Emp_Code = dt1.Rows[0]["Emp_Code"],
                            Month = date.ToString("MMM yyyy"),
                            Day_in_month = DateTime.DaysInMonth(date.Year, date.Month),
                            Persent = dt2.Select("AttendanceStatus in ('Present')").Length,
                            Absent = dt2.Select("AttendanceStatus='Absent'").Length,
                            Onleave = PayrollMy.data($"select count(id) from HR_LeaveRequest_DateWise  where Idate like '{data["Year"]}{data["Month"]}%' and EmployeeId='{data["EmployeeId"]}'"),
                            total_working = dt2.Select("AttendanceStatus in ('HalfDay','Present','FullDay')").Length,
                            Half_Day = dt2.Select("AttendanceStatus='HalfDay'").Length,
                        },
                    });
                }
                if (data["type"].ToString() == "daily-attendance-overall")
                {
                    var date = Convert.ToDateTime(data["Date"]);
                    var idate = date.ToString("yyyyMMdd");
                    var qry = $"select ad.*,'Working' Day_Status,em.Employee_Name,gm.grade_name  from HR_Daily_Attendance_Record ad join HR_Employee_Master em on em.Employee_id=ad.Employee_id  join HR_Grade_Master gm on gm.grade_id=em.Grade_id  where ad.Idate='{idate}' and em.ActiveStatus='Active'";
                    if (data["Grade"].ToString() != "0")
                    {
                        qry += $" and em.Grade_id='{data["Grade"]}'";
                    }
                    var dt = PayrollMy.dataTable(qry);
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = dt.toJsonObject(),
                        details = new
                        {
                            date = date.ToString("dd-MMM-yyyy"),
                            Emp_Total = PayrollMy.data($"select count(id) from HR_Employee_Master  where  ActiveStatus='Active' and Grade_id = '{data["Grade"]}' or '0'='{data["Grade"]}' "),
                            Persent = dt.Select("AttendanceStatus in ('FullDay','HalfDay')").Length,
                            Absent = dt.Select("AttendanceStatus='Absent'").Length,
                            Onleave = PayrollMy.data($"select count(id) from HR_LeaveRequest_DateWise  where Idate = {idate} and ( EmployeeId in (select Employee_id from HR_Employee_Master  where Grade_id = '{data["Grade"]}' or '0'='{data["Grade"]}'))"),
                        },
                    });



                }
                if (data["type"].ToString() == "monthly-attendance-overall")
                {
                    var start_date = $"{data["Year"]}{data["Month"]}01";
                    var end_date = $"{data["Year"]}{data["Month"]}31";

                    var _dt = PayrollMy.dataTable($"sp_HR_Attendance_Chart_Monthly '{start_date}' ,'{end_date}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = _dt.toJsonObject(),
                    });
                }
                if (data["type"].ToString() == "monthly-status-overall")
                {
                    var start_date = $"{data["Year"]}{data["Month"]}01";
                    var end_date = $"{data["Year"]}{data["Month"]}31";
                    var _d = new List<object>();
                    var dt = PayrollMy.dataTable($"select * from HR_Employee_Master where   ActiveStatus='Active' ");
                    foreach (DataRow dr in dt.Rows)
                    {
                        var _dt = PayrollMy.dataTable($"sp_HR_Attendance_Report '{start_date}' ,'{end_date}','{dr["Employee_id"]}'");
                        _d.Add(new
                        {
                            Emoloyee_Name = dr["Employee_Name"],
                            Emoloyee_Code = dr["Emp_Code"],
                            Emoloyee_Image = dr["Employee_image"],
                            details = _dt.toJsonObject(),
                        });
                    }

                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = _d.ToArray(),
                    });
                }
                if (data["type"].ToString() == "employee-monthly-status-overall")
                {
                    var start_date = $"{data["Year"]}{data["Month"]}01";
                    var end_date = $"{data["Year"]}{data["Month"]}31";
                    var _d = new List<object>();
                    var dt = PayrollMy.dataTable($"select * from HR_Employee_Master where Employee_id='{data["Employee"]}' and ActiveStatus='Active'");
                    foreach (DataRow dr in dt.Rows)
                    {
                        var _dt = PayrollMy.dataTable($"sp_HR_Attendance_Report '{start_date}' ,'{end_date}','{dr["Employee_id"]}'");
                        _d.Add(new
                        {
                            Emoloyee_Name = dr["Employee_Name"],
                            Emoloyee_Code = dr["Emp_Code"],
                            Emoloyee_Image = dr["Employee_image"],
                            details = _dt.toJsonObject(),
                        });
                    }

                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = _d.ToArray(),
                    });
                }
                if (data["type"].ToString() == "attendance-log")
                {
                    var dt1 = PayrollMy.dataTable($"select * from HR_Employee_Master   where Employee_id='{data["EmployeeId"]}' and ActiveStatus='Active'");
                    var dt2 = PayrollMy.dataTable($@"SELECT  format(hc.Date,'dd-MMM-yyyy') Date,format(hc.Date,'dddd') Day,STUFF((
    SELECT ', ' + format(DateTime,'hh:mm tt')
    FROM HR_Attendance_Log al
    WHERE format(DateTime,'yyyyMMdd') = hc.idate  and al.Employee_id='{data["EmployeeId"]}' order by DateTime
    FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS atn_log,'Working' Day_Status
  FROM HR_Calendar hc where hc.idate like '{data["Year"]}{data["Month"]}%'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = dt2.toJsonObject(),
                        details = new
                        {
                            Employee_Name = dt1.Rows[0]["Employee_Name"],
                            Emp_Code = dt1.Rows[0]["Emp_Code"],
                        },
                    });
                }
                if (data["type"].ToString() == "attendance-log-datewise")
                {
                    var idate = Convert.ToDateTime(data["Date"]).ToString("yyyyMMdd");
                    //Rajesh

                    //                 var dt2 = PayrollMy.dataTable($@"SELECT  Employee_Name,STUFF((
                    //SELECT ', ' + format(DateTime,'hh:mm tt')
                    //FROM HR_Attendance_Log al
                    //WHERE  al.Employee_id=em.Employee_id and Itime>={idate}000000 and Itime<{idate}250000
                    //FOR XML PATH(''), TYPE
                    //).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS atn_log 
                    //FROM HR_Employee_Master em"); 

                    var dt2 = PayrollMy.dataTable($@"SELECT  Employee_Name,STUFF((
    SELECT ', ' + format(DateTime,'hh:mm tt')
    FROM HR_Attendance_Log al
    WHERE  al.Employee_id=em.Employee_id and format(DateTime, 'yyyyMMdd')>={idate} and format(DateTime, 'yyyyMMdd')<={idate} and em.ActiveStatus='Active'   order by DateTime
    FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS atn_log 
    FROM HR_Employee_Master em");

                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        error = false,
                        data = dt2.toJsonObject(),
                    });
                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (dataId == "salary_structure_apply")
            {
                var dt = PayrollMy.dataTable($"select income_heads,deduction_head from HR_Employee_Online_Apply where Apply_id='{data["App_id"]}'");
                var income_heads = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dt.Rows[0]["income_heads"].ToString());
                var deduction_head = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dt.Rows[0]["deduction_head"].ToString());
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    error = false,
                    income_heads = income_heads,
                    deduction_head = deduction_head,
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (dataId == "salaray_structure")
            {
                var ds = PayrollMy.dataSet($@"select  em.* ,dpm.name Department, dsm.name Designation , gm.grade_name from HR_Employee_Master em left join HR_Grade_Master gm on em.Grade_id=gm.grade_id left join HR_Designation_Master dsm on dsm.Designation_id=em.Designation_id left join HR_Department_Master dpm on dpm.department_id =em.Department_id where em.Employee_id ='{data["Employee_id"]}'; ");
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Success",
                    error = false,
                    data = ds.Tables[0].toJsonObject(),
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (dataId == "getleavereaquest")
            {
                object _data;
                if (data["type"].ToString().isEqual("pageLoad"))
                {
                    var dt = PayrollMy.dataTable($@" select  Emp_Code,Employee_Name,Employee_image,name ,isnull((select Employee_Name from HR_Employee_Master where employee_id=lr.RequestSentTo),lr.RequestSentTo) RequestTo,(select Employee_Name from HR_Employee_Master where employee_id=lr.ForwardedTo) RequestForwadedTo,lt.LeaveName,LeaveCode, lr.* from HR_LeaveRequest lr join  HR_Employee_Master em on em.Employee_id=lr.EmployeeId join HR_LeaveTypes lt on lt.LeaveId=lr.LeaveTypeId left join HR_Designation_Master dm on em.Designation_id =dm.Designation_id  where (RequestSentTo='{User.Identity.Name}' or ForwardedTo ='{User.Identity.Name}' or 'true'='{isHR()}') and Status in ('Pending','Forwarded') and lr.EmployeeId != '{User.Identity.Name}'");
                    _data = dt.toJsonObject();
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = _data,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString().isEqual("approved"))
                {
                    var mt = PayrollMy.Table("HR_LeaveRequest", $"LeaveRequestId='{data["mId"]}'");
                    if (mt.Rows[0]["Status"].ToString() == "Approved")
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Leave already Approved",
                            error = true,
                        });
                    }
                    else
                    {
                        var dr = mt.Rows[0];
                        var LeaveFrom = Convert.ToDateTime(dr["LeaveFrom"]);
                        var LeaveTo = Convert.ToDateTime(dr["LeaveTo"]);
                        var days = (LeaveTo - LeaveFrom).TotalDays + 1;
                        var max_leave_accept = PayrollMy.data($"select Max_Leave_Accepted from HR_LeaveAccecptRight where DesignationId in (select DesignationId from hr_employee_master where employee_id='{User.Identity.Name}')").ToDouble();
                        if (max_leave_accept == 0)
                        {
                            if (IsAdmin())
                            {
                                max_leave_accept = 1000;
                            }
                        }
                        if (max_leave_accept < days)
                        {
                            var msg = "You are not allowed to accept any leave. You must transfer the leave request to your senior.";
                            if (max_leave_accept > 0)
                            {
                                msg = $"You can only accept a leave request of up to {max_leave_accept} days.";
                            }
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = msg,
                                error = true,
                            });
                            return Json(resp, JsonRequestBehavior.AllowGet);
                        }
                        dr["Status"] = "Approved";
                        dr["Remarks"] = data["remarks"];
                        mt.Update();
                        while (LeaveFrom <= LeaveTo)
                        {
                            PayrollMy.Insert("HR_LeaveRequest_DateWise", new
                            {
                                EmployeeId = dr["EmployeeId"],
                                LeaveTypeId = dr["LeaveTypeId"],
                                LeaveRequestId = dr["LeaveRequestId"],
                                Date = LeaveFrom.ToString("dd-MMM-yyyy"),
                                Idate = LeaveFrom.ToString("yyyyMMdd"),
                            });
                            LeaveFrom = LeaveFrom.AddDays(1);
                        }
                        var mt1 = PayrollMy.Table($"select  * from HR_EmployeeLeaveAvailabilityDetails where EmployeeId='{dr["EmployeeId"]}' and LeaveTypeId='{dr["LeaveTypeId"]}' and Session='{PayrollMy.currentSession}'");
                        if (mt1.Rows.Count > 0)
                        {
                            mt1.Rows[0]["ConsumeLeave"] = mt1.Rows[0]["ConsumeLeave"].ToDouble() + days;
                            mt1.Update();
                        }

                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Leave Approved",
                            error = false,
                        });
                    }
                    //   PayrollMy.Update("HR_LeaveRequest", new { Status="Approved", Remarks=data["remarks"] }, where: $"LeaveRequestId='{data["requestId"]}'");

                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString().isEqual("reject"))
                {

                    var mt = PayrollMy.Table("HR_LeaveRequest", $"LeaveRequestId='{data["mId"]}'");
                    if (mt.Rows[0]["Status"].ToString() == "Approved")
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Leave already Approved",
                            error = true,
                        });
                    }
                    else if (mt.Rows[0]["Status"].ToString() == "Rejected")
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Leave request was already Rejected",
                            error = true,
                        });
                    }
                    else
                    {
                        var dr = mt.Rows[0];
                        dr["Status"] = "Rejected";
                        dr["Remarks"] = data["remarks"];
                        mt.Update();
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Leave request Rejected",
                            error = false,
                        });
                    }
                    return Json(resp, JsonRequestBehavior.AllowGet);

                }
                else if (data["type"].ToString().isEqual("forward"))
                {
                    var lrdt = PayrollMy.dataTable($"select * from HR_LeaveRequest where LeaveRequestId='{data["mId"]}'");
                    var ForwardedTo = lrdt.Rows[0]["RequestSentTo"].ToString();
                    if (lrdt.Rows[0]["ForwardedTo"].ToString() != "")
                    {
                        ForwardedTo = lrdt.Rows[0]["ForwardedTo"].ToString();
                    }
                    var desig = PayrollMy.data($"select Designation_id from HR_Employee_Master where Employee_id='{ForwardedTo}'");
                    var reportingDesignation = PayrollMy.data($"select ReportingTo from HR_Designation_Master where Designation_id='{desig}'");
                    var rt = PayrollMy.dataTable($"select Employee_id from HR_Employee_Master where Designation_id='{reportingDesignation}'");
                    while (rt.Rows.Count == 0)
                    {
                        reportingDesignation = PayrollMy.data($"select ReportingTo from HR_Designation_Master where Designation_id='{reportingDesignation}'");
                        if (reportingDesignation == "")
                        {
                            break;
                        }
                        rt = PayrollMy.dataTable($"select Employee_id from HR_Employee_Master where Designation_id='{reportingDesignation}'");
                    }

                    PayrollMy.Update("HR_LeaveRequest", new { Status = "Forwared", Remarks = data["remarks"], ForwardedTo = rt.Rows[0][0] }, where: $"LeaveRequestId='{data["requestId"]}'");
                    var toname = PayrollMy.data($"select name from HR_Designation_Master where Designation_id='{reportingDesignation}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = $"Leave request forwared To {toname}",
                        error = false,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Submethod error",
                        error = true,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }



            }
            else if (dataId == "tabledata")
            {
                var mpc = getMPCById(data["pageId"].ToString());
                var qry = $"select * from {mpc.tableName}";
                if (mpc.isCustomisedGrid && mpc.dataQuery != "")
                {
                    qry = processDataQuery(mpc.dataQuery, data);
                }
                DataTable dt;
                try
                {
                    dt = PayrollMy.dataTable(qry);
                }
                catch
                {
                    dt = PayrollMy.dataTable($"select top 10 * from {mpc.tableName}");
                }

                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "success",
                    error = false,
                    dataId = dataId,
                    data = dt.toJsonObject(),
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else if (dataId == "report")
            {
                DataTable dt = new DataTable();
                if (data["type"].ToString() == "duty-roster-view")
                {
                    var qry = $"select Emp_Code,Employee_Name,Employee_image,Employee_id,RotationId from HR_Employee_Master  em  join  HR_Duty_Roster dr on em.Employee_id=dr.EmployeeId  and dr.RotationId='{data["RotationId"]}'  where Designation_id = '{data["designation"]}'";

                    dt = PayrollMy.dataTable($"select  * from HR_ShiftSetting where status=1 and DutyHourId in (select DutyHourId from HR_Duty_Hour_Maping where DesignationId='{data["designation"]}') order by StartITime");
                    var lst = new Dictionary<string, object>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        lst[dr["ShiftId"].ToString()] = PayrollMy.dataTable($"select Emp_Code,Employee_Name,Employee_image,Employee_id from HR_Employee_Master  em  join  HR_Duty_Roster dr on em.Employee_id=dr.EmployeeId  and dr.RotationId='{data["RotationId"]}'  where Designation_id = '{data["designation"]}' and  dr.Shift_Id ='{dr["ShiftId"]}'").toJsonObject();
                    }

                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        data = dt.toJsonObject(),
                        shift_data = lst,
                    }); ;
                }

                if (data["type"].ToString() == "duty-roster-view-new")
                {

                    dt = PayrollMy.dataTable($"select  Employee_Name,Emp_Code,Room_category,Start_Date,End_Date,ShiftName,DutyIncharge,EmployeeId,em.grade_id from HR_Duty_Roster dr join HR_Employee_Master em on dr.EmployeeId=em.Employee_Id left join HMS_Room_Category rc on rc.Room_categoryid=dr.WorkLocation join HR_Rotation_Details rd on dr.RotationId=rd.RotationId join HR_ShiftSetting sf on sf.ShiftId=dr.Shift_Id and sf.Status=1 where dr.RotationId='{data["RotationId"]}' and em.Designation_id = '{data["designation"]}'");
                    var et = new DataTable();
                    et.Columns.Add("Employee Name");
                    et.Columns.Add("Emp Code");
                    et.Columns.Add("Work Location");
                    var incharge = "";
                    var shift_details = "";
                    if (dt.Rows.Count > 0)
                    {
                        var start_date = Convert.ToDateTime(dt.Rows[0]["Start_Date"].ToString());
                        var end_date = Convert.ToDateTime(dt.Rows[0]["End_Date"].ToString());
                        var cnt = 3;
                        incharge = PayrollMy.data($"select Employee_Name from HR_Employee_Master  where employee_id='{dt.Rows[0]["DutyIncharge"]}'");
                        var shifts = PayrollMy.dataTable($"select  * from HR_ShiftSetting where status=1 and DutyHourId in (select DutyHourId from HR_Duty_Hour_Maping where DesignationId='{data["designation"]}') order by StartITime");
                        shift_details = "";
                        foreach (DataRow row in shifts.Rows)
                        {
                            shift_details += $"{row["ShiftName"]} : ({Convert.ToDateTime(row["In_Time"]).ToString("hh:mm tt")} to {Convert.ToDateTime(row["Out_Time"]).ToString("hh: mm tt")}),    ";
                        }
                        for (var d = start_date; d <= end_date;)
                        {
                            et.Columns.Add($"{d.ToString("d-MMM")}");
                            d = d.AddDays(1);
                            cnt++;
                        }
                        var wt = PayrollMy.dataTable($"select * from HR_Roster_Wise_Weekoff_setting  where Idate>={start_date.idate()} AND  Idate<={end_date.idate()} ");
                        var gt = PayrollMy.dataTable($"select * from HR_Grade_Master");

                        foreach (DataRow r in dt.Rows)
                        {
                            var dr = et.NewRow();
                            dr[0] = r["Employee_Name"];
                            dr[1] = r["Emp_Code"];
                            dr[2] = r["Room_category"];
                            cnt = 3;
                            var gtrs = gt.Select($"grade_id='{r["grade_id"]}'");
                            var default_weekoff = "Sun";
                            if (gtrs.Length > 0)
                            {
                                default_weekoff = gtrs[0]["weekly_off"].ToString();
                            }
                            var cs_shift = PayrollMy.dataTable($"select sc.*,ShiftName from HR_Date_Wise_Shift_Customized sc join HR_ShiftSetting  ss on sc.Shift_Id=ss.ShiftId where idate >=   '{start_date.idate()}' and idate <=   '{end_date.idate()}' ");
                            var leaves = PayrollMy.dataTable($"select  lr.*,LeaveCode from HR_LeaveRequest_DateWise lr join HR_LeaveTypes lt on lr.LeaveTypeId=lt.LeaveId  where idate >=   '{start_date.idate()}' and idate <=   '{end_date.idate()}' ");

                            for (var d = start_date; d <= end_date;)
                            {
                                var idate = d.ToString("yyyyMMdd");
                                var isWeekoff = false;
                                if (wt.Select($"EmployeeID='{r["EmployeeId"]}' and idate='{idate}'").Length > 0)
                                {
                                    isWeekoff = true;
                                }
                                else if (wt.Select($"EmployeeID='{r["EmployeeId"]}'").Length == 0)
                                {
                                    if (default_weekoff.Contains(d.ToString("ddd")))
                                    {
                                        isWeekoff = true;
                                    }
                                }
                                if (isWeekoff)
                                {
                                    dr[cnt] = "WOFF";
                                }
                                else
                                {
                                    var ldrs = leaves.Select($"EmployeeId='{r["EmployeeId"]}' and idate='{idate}'");
                                    if (ldrs.Length > 0)
                                    {
                                        dr[cnt] = ldrs[0]["LeaveCode"];
                                    }
                                    else
                                    {
                                        var cdrs = cs_shift.Select($"Employee_Id='{r["EmployeeId"]}' and idate='{idate}'");

                                        if (cdrs.Length > 0)
                                        {
                                            dr[cnt] = cdrs[0]["ShiftName"];
                                        }
                                        else
                                        {
                                            dr[cnt] = r["ShiftName"];
                                        }
                                    }
                                }



                                d = d.AddDays(1);
                                cnt++;
                            }


                            et.Rows.Add(dr);
                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        shift_details = shift_details,
                        incharge = incharge,
                        error = false,
                        data = et.toJsonObject(),
                    });
                }

                if (data["type"].ToString() == "duty-roster-weekoff-seetting")
                {

                    dt = PayrollMy.dataTable($"select  Grade_id,Employee_Name,Emp_Code, Employee_Id from   HR_Employee_Master em  where em.Designation_Id='{data["designation"]}'   order by em.Employee_Name");
                    var et = new DataTable();
                    et.Columns.Add("Employee Name");
                    et.Columns.Add("W-Off");
                    et.Columns.Add("EmployeeId");
                    if (dt.Rows.Count > 0)
                    {
                        var start_date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                        var end_date = start_date.AddMonths(1).AddDays(-1);
                        var cnt = 3;
                        for (var d = start_date; d <= end_date;)
                        {
                            et.Columns.Add($" {d.Day}");
                            d = d.AddDays(1);
                            cnt++;
                        }
                        var wt = PayrollMy.dataTable($"select * from HR_Roster_Wise_Weekoff_setting where Idate like '{data["Year"]}{data["Month"]}%'");
                        var gt = PayrollMy.dataTable($"select * from HR_Grade_Master");
                        var leaves = PayrollMy.dataTable($"select  lr.*,LeaveCode from HR_LeaveRequest_DateWise lr join HR_LeaveTypes lt on lr.LeaveTypeId=lt.LeaveId  where idate >=   '{start_date.idate()}' and idate <=   '{end_date.idate()}' ");
                        foreach (DataRow r in dt.Rows)
                        {
                            var w_off = 0;
                            var dr = et.NewRow();
                            dr[0] = $"{r["Employee_Name"]} ({ r["Emp_Code"]})";
                            dr[2] = r["Employee_Id"];
                            var c = 3;
                            var gtrs = gt.Select($"grade_id='{r["grade_id"]}'");
                            var default_weekoff = "Sun";
                            if (gtrs.Length > 0)
                            {
                                default_weekoff = gtrs[0]["weekly_off"].ToString();
                            }

                            for (var d = start_date; d <= end_date;)
                            {
                                var idate = d.ToString("yyyyMMdd");
                                var ldrs = leaves.Select($"EmployeeId='{r["Employee_Id"]}' and idate='{idate}'");
                                if (ldrs.Length > 0)
                                {
                                    dr[c] = ldrs[0]["LeaveCode"];
                                }
                                else if (wt.Select($"EmployeeID='{r["Employee_Id"]}' and idate='{idate}'").Length > 0)
                                {
                                    dr[c] = '1';
                                    w_off++;
                                }
                                else if (wt.Select($"EmployeeID='{r["Employee_Id"]}'").Length == 0)
                                {
                                    if (default_weekoff.Contains(d.ToString("ddd")))
                                    {
                                        dr[c] = true;
                                        w_off++;
                                    }
                                    else
                                    {
                                        dr[c] = '0';
                                    }
                                }
                                else
                                {
                                    dr[c] = '0';
                                }
                                d = d.AddDays(1);
                                c++;
                            }
                            dr[1] = w_off;
                            et.Rows.Add(dr);
                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        data = et.toJsonObject(),
                    });
                }
                if (data["type"].ToString() == "roster-rearrange")
                {

                    dt = PayrollMy.dataTable($"select  Grade_id,Employee_Name,Emp_Code,Room_category,Start_Date,End_Date,ShiftName,DutyIncharge,EmployeeId,dr.Shift_Id from HR_Duty_Roster dr join HR_Employee_Master em on dr.EmployeeId=em.Employee_Id left join HMS_Room_Category rc on rc.Room_categoryid=dr.WorkLocation join HR_Rotation_Details rd on dr.RotationId=rd.RotationId join HR_ShiftSetting sf on sf.ShiftId=dr.Shift_Id and sf.Status=1 where dr.RotationId='{data["RotationId"]}' and em.Designation_id = '{data["designation"]}' order by em.Emp_Code");
                    var et = new DataTable();
                    et.Columns.Add("Employee Name");
                    et.Columns.Add("EmployeeId");
                    et.Columns.Add("Shift_Id");
                    var shift_details = "";
                    if (dt.Rows.Count > 0)
                    {
                        var start_date = Convert.ToDateTime(dt.Rows[0]["Start_Date"].ToString());
                        var end_date = Convert.ToDateTime(dt.Rows[0]["End_Date"].ToString());
                        var cnt = 2;

                        var shifts = PayrollMy.dataTable($"select  * from HR_ShiftSetting where status=1 and DutyHourId in (select DutyHourId from HR_Duty_Hour_Maping where DesignationId='{data["designation"]}') order by StartITime");
                        //`<option value="D19">General (1 Shift)</option><option value="D20">Nurshing (3 Shift)</option></select>` 
                        foreach (DataRow sr in shifts.Rows)
                        {
                            shift_details += $"<option value='{sr["ShiftId"]}'>{sr["ShiftName"]}</option>";
                        }
                        for (var d = start_date; d <= end_date;)
                        {
                            et.Columns.Add($"{d.ToString("d-MMM")}");
                            d = d.AddDays(1);
                            cnt++;
                        }
                        var wt = PayrollMy.dataTable($"select * from HR_Roster_Wise_Weekoff_setting where Idate like '{data["Year"]}{data["Month"]}%'");
                        var cs_shift = PayrollMy.dataTable($"select * from HR_Date_Wise_Shift_Customized where Idate like '{data["Year"]}{data["Month"]}%'");

                        var gt = PayrollMy.dataTable($"select * from HR_Grade_Master");
                        foreach (DataRow r in dt.Rows)
                        {
                            var dr = et.NewRow();
                            dr[0] = r["Employee_Name"];
                            dr[1] = r["EmployeeId"];
                            dr[2] = r["Shift_Id"];

                            var c = 3;
                            var gtrs = gt.Select($"grade_id='{r["grade_id"]}'");
                            var default_weekoff = "Sun";
                            if (gtrs.Length > 0)
                            {
                                default_weekoff = gtrs[0]["weekly_off"].ToString();
                            }
                            for (var d = start_date; d <= end_date;)
                            {
                                var idate = d.ToString("yyyyMMdd");
                                var isWeekoff = false;
                                if (wt.Select($"EmployeeID='{r["EmployeeId"]}' and idate='{idate}'").Length > 0)
                                {
                                    isWeekoff = true;
                                }
                                else if (wt.Select($"EmployeeID='{r["EmployeeId"]}'").Length == 0)
                                {
                                    if (default_weekoff.Contains(d.ToString("ddd")))
                                    {
                                        isWeekoff = true;
                                    }
                                }
                                if (isWeekoff)
                                {
                                    dr[c] = "WOFF";
                                }
                                else
                                {
                                    var cdrs = cs_shift.Select($"Employee_Id='{r["EmployeeId"]}' and idate='{idate}'");
                                    // is custom shift
                                    if (cdrs.Length > 0)
                                    {
                                        dr[c] = cdrs[0]["Shift_Id"];
                                    }
                                    else
                                    {
                                        dr[c] = r["Shift_Id"];
                                    }
                                }
                                d = d.AddDays(1);
                                c++;
                            }
                            et.Rows.Add(dr);
                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        shift_details = shift_details,
                        data = et.toJsonObject(),
                    });
                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else if (dataId == "ddl")
            {
                DataTable dt = new DataTable();
                if (data["type"].ToString() == "salaray_structure")
                {
                    var ds = PayrollMy.dataSet(@"select grade_name,grade_id from HR_Grade_Master; 
select name,department_id from HR_Department_Master;
select name,Designation_id from HR_Designation_Master;
select Employee_Name+' ('+Emp_Code+')' Name,Employee_id from HR_Employee_Master;");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        grade = ds.Tables[0].toJsonObject(),
                        department = ds.Tables[1].toJsonObject(),
                        designation = ds.Tables[2].toJsonObject(),
                        employee = ds.Tables[3].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "get-employee")
                {
                    var condition = "where 1=1 ";
                    if (data["Grade"].ToString() != "")
                    {
                        condition += $" and  Grade_id='{data["Grade"]}' ";
                    }
                    if (data["Department"].ToString() != "")
                    {
                        condition += $" and  Department_id='{data["Department"]}' ";
                    }
                    if (data["Designation"].ToString() != "")
                    {
                        condition += $" and  Designation_id='{data["Designation"]}' ";
                    }
                    var ds = PayrollMy.dataSet($@"select Employee_Name+' ('+Emp_Code+')' Name,Employee_id from HR_Employee_Master {condition};");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        employee = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "GradeDesignation")
                {
                    var ds = PayrollMy.dataSet(@"select grade_name,grade_id from HR_Grade_Master; 
select name,department_id from HR_Department_Master;
select name,Designation_id from HR_Designation_Master;");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        grade = ds.Tables[0].toJsonObject(),
                        department = ds.Tables[1].toJsonObject(),
                        designation = ds.Tables[2].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "IncomeDeduct")
                {
                    var ds = PayrollMy.dataSet($@"select  *, '' cbc, 0.0 inc  from HR_SalaryIncomeHead   where GradeId='{data["grade"]}' ; 
select  * from HR_SalaryDeductionHead where GradeId ='{data["grade"]}' ;");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        income = ds.Tables[0].toJsonObject(),
                        deduction = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "duty-department")
                {
                    var ds = PayrollMy.dataSet($@"select Duty_Hour_Name,Duty_Hour_id from HR_Duty_Hours; 
select 'All' name, '0' department_id  union all select name,department_id from HR_Department_Master;");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        duty = ds.Tables[0].toJsonObject(),
                        department = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "duty-designation")
                {
                    dt = PayrollMy.dataTable($@"select * from (select ds.name,dp.name department,ds.Designation_id, DutyHourId from HR_Designation_Master ds join HR_Department_Master dp on ds.DepartmentId=dp.department_id left join HR_Duty_Hour_Maping dhm on dhm.DesignationId=ds.Designation_id  where ds.DepartmentId ='{data["Department"]}' or '0'='{data["Department"]}' ) t where isnull(DutyHourId,'{data["Duty_Hour"]}')='{data["Duty_Hour"]}'");

                    var dt1 = PayrollMy.dataTable($@"select * from (select ds.name,dp.name department,ds.Designation_id, Duty_Hour_Name,DutyHourId from HR_Designation_Master ds join HR_Department_Master dp on ds.DepartmentId=dp.department_id   join HR_Duty_Hour_Maping dhm on dhm.DesignationId=ds.Designation_id join HR_Duty_Hours dh on dh.Duty_Hour_id=dhm.DutyHourId  where ds.DepartmentId ='{data["Department"]}' or '0'='{data["Department"]}' ) t where DutyHourId !='{data["Duty_Hour"]}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = dt.toJsonObject(),
                        mapped = dt1.toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "interview-type")
                {
                    dt = PayrollMy.dataTable(@"select InterviewType,InterViewTypeId from HR_InterviewTypes;");
                }
                else if (data["type"].ToString() == "employees")
                {
                    dt = PayrollMy.dataTable(@"select Employee_Name,Emp_Code,Employee_id from HR_Employee_Master ");
                }
                else if (data["type"].ToString() == "hms-userdetails")
                {
                    dt = PayrollMy.dataTable($@"select * from HMS_user_details where mobile ='{data["mobile"]}'");
                }
                else if (data["type"].ToString() == "name_initital")
                {
                    dt = PayrollMy.dataTable(@"select distinct Initial_name,Initial_id from HMS_Initial_Master order by Initial_id");
                }
                else if (data["type"].ToString() == "leave-type-allotment")
                {
                    dt = PayrollMy.dataTable(@"select * from HR_LeaveTypes  where LeaveType in ('Casual Leave','Sick Leave','Other')");
                }
                else if (data["type"].ToString() == "field-bind")
                {
                    var mpc = getMPCById(data["pageId"].ToString());
                    var qry = "";
                    foreach (var p in mpc.pcd)
                    {
                        if (p.columnName == data["field_name"].ToString())
                        {
                            var dc = p.requiredData.Substring(4);
                            qry = processDataQuery(dc, data, "");
                            break;
                        }
                    }
                    dt = PayrollMy.dataTable(qry);
                    var lst = new List<object>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        lst.Add(new { name = dr[0].ToString(), id = dr[1].ToString(), });
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = lst,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "interview-type-designation")
                {
                    var dtm = PayrollMy.dataTable($"select * from HR_InterviewProfiler where InterviewTypeId='{data["interview-type"]}'");
                    var qry = "select name,Designation_id from HR_Designation_Master where 1=1 ";
                    if (dtm.Rows.Count > 0)
                    {
                        if (dtm.Rows[0]["Departments"].ToString() != "0")
                            qry += $" and DepartmentId='{dtm.Rows[0]["Departments"]}'";
                        if (dtm.Rows[0]["Designations"].ToString() != "0")
                            qry += $" and Designation_id='{dtm.Rows[0]["Designations"]}'";
                    }
                    dt = PayrollMy.dataTable(qry);
                }
                else if (data["type"].ToString() == "Grade")
                {
                    dt = PayrollMy.dataTable(@"select grade_name,grade_id from HR_Grade_Master;");
                }
                else if (data["type"].ToString() == "department")
                {
                    dt = PayrollMy.dataTable($"select name,department_id from HR_Department_Master");
                }
                else if (data["type"].ToString() == "designation")
                {
                    dt = PayrollMy.dataTable($"select name,Designation_id from HR_Designation_Master where DepartmentId='{data["DepartmentId"]}'");
                }
                else if (data["type"].ToString() == "duty-hour")
                {
                    dt = PayrollMy.dataTable($"select concat(Duty_Hour_Name ,' (',No_of_Shift,' Shift)')  Duty_Hour_Name,Duty_Hour_id from HR_Duty_Hours");
                }
                else if (data["type"].ToString() == "rotation-duration")
                {
                    dt = PayrollMy.dataTable($"select Description,RotationId,rd.RotationHeadId,DutyHourId from HR_Rotation_Details hr join HR_Rotation_Duration rd on hr.RotationHeadId=rd.RotationHeadId where DutyHourId='{data["dutyhour"]}' and  Start_Date like '{data["year"]}-{data["month"]}%' and  Status=1");
                }
                else if (data["type"].ToString() == "get-duty-roster-designagion-emoloyees")
                {
                    //RotationHeadId  
                    bool isFirstShift = PayrollMy.data($"select top 1 ShiftId from HR_ShiftSetting where DutyHourId='{data["Duty_HourId"]}' and Status=1  order by StartITime") == data["shift"].ToString();
                    var lastShiftId = PayrollMy.data($"select top 1 ShiftId from HR_ShiftSetting where DutyHourId='{data["Duty_HourId"]}' and Status=1  order by StartITime desc");
                    var prevRotationId = PayrollMy.data($"select  top 1 RotationId from HR_Rotation_Details where RotationHeadId='{data["rotationHead"]}' and Start_IDate<(select Start_IDate from HR_Rotation_Details where RotationId={data["RotationId"]}) order by Start_IDate desc");
                    bool isLastShift = lastShiftId == data["shift"].ToString();
                    var start_idate = PayrollMy.data($"select Start_IDate from HR_Rotation_Details where RotationId='{data["RotationId"]}'");
                    //if prevRotationId is empty
                    var qry = $"select Emp_Code,Employee_Name,Employee_image,Employee_id,RotationId,'0' isenable ,(select top 1 s1.ShiftName  from HR_ShiftSetting s1 join HR_Duty_Roster s2 on s1.ShiftId=s2.Shift_Id join HR_Rotation_Details s3 on s2.RotationId=s3.RotationId where s2.EmployeeId=em.Employee_id and Start_IDate< '{start_idate}' order by s2.id desc) PrevShift,WorkLocation  from HR_Employee_Master  em left join  HR_Duty_Roster dr on em.Employee_id=dr.EmployeeId  and dr.RotationId='{data["RotationId"]}'  where Designation_id = '{data["designation"]}' and  dr.Shift_Id not in (select ShiftId from HR_ShiftSetting where Shift_Id!='{data["shift"]}' and DutyHourId='{data["Duty_HourId"]}' )";
                    dt = PayrollMy.dataTable(qry);
                    if (prevRotationId != "")
                    {
                        if (isFirstShift || isLastShift)
                        {
                            // qry += $" and   em.Employee_id not in (select EmployeeId from HR_Duty_Roster where RotationId='{prevRotationId}' and Shift_Id ='{lastShiftId}' ) ";
                            var et = PayrollMy.dataTable($"select EmployeeId from HR_Duty_Roster where RotationId='{prevRotationId}' and Shift_Id ='{lastShiftId}'");
                            var eids = new List<string>();
                            foreach (DataRow dr in et.Rows)
                            {
                                eids.Add(dr[0].ToString());
                            }
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (eids.Contains(dr["Employee_id"].ToString()))
                                {
                                    dr["isenable"] = 1;
                                }
                            }

                        }
                    }



                    var dt1 = PayrollMy.dataTable($"select ShiftId from HR_ShiftSetting where DutyHourId='{data["Duty_HourId"]}'");
                    var duty_incharge = PayrollMy.dataTable($"select Employee_id,Department_id,Designation_id from HR_Employee_Master where employee_id in (select top 1 DutyIncharge from HR_Duty_Roster where  Shift_Id='{data["shift"]}'  and RotationId='{data["RotationId"]}')");
                    var lst = new Dictionary<string, object>();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        lst[dr["ShiftId"].ToString()] = PayrollMy.dataTable($"select Employee_Name from HR_Employee_Master  em  join  HR_Duty_Roster dr on em.Employee_id=dr.EmployeeId  and dr.RotationId='{data["RotationId"]}'  where Designation_id = '{data["designation"]}' and  dr.Shift_Id ='{dr["ShiftId"]}'").toJsonObject();
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = dt.toJsonObject(),
                        shift_data = lst,
                        duty_incharge = duty_incharge.toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "duty-hour-designation")
                {
                    var ds = PayrollMy.dataSet($"select name,Designation_id from HR_Designation_Master   where Designation_id in (select DesignationId from HR_Duty_Hour_Maping where DutyHourId='{data["Duty_Hour_id"]}'); select ShiftName,ShiftDesc,ShiftId  from HR_ShiftSetting where Status=1 and DutyHourId='{data["Duty_Hour_id"]}' order by StartITime;");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        designation = ds.Tables[0].toJsonObject(),
                        shift = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Success",
                    error = false,
                    data = dt.toJsonObject(),
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else if (dataId == "dataonchange")
            {
                var pcd_text = PayrollMy.data($"select pcd from HR_MasterPageConfig where  pageId ='{data["frm_pageId"]}'");
                var pcds = JsonConvert.DeserializeObject<List<FieldConfigData>>(pcd_text);
                FieldConfigData pcd = null;
                foreach (var p in pcds)
                {
                    if (p.columnName == data["frm_targetId"].ToString())
                    {
                        pcd = p;
                        break;
                    }
                }
                bool isSQL = false;
                var dc = pcd.requiredData;
                if (pcd.requiredData.Trim().StartsWith("sql:"))
                {
                    isSQL = true;
                    dc = dc.Substring(4);
                }

                dc = processDataQuery(dc, data, "");
                if (pcd.fieldType.Contains("DDL"))
                {
                    List<string> lst = new List<string>();
                    lst.Add($"<option value=''>Select</option>");
                    if (isSQL)
                    {
                        var dt = PayrollMy.dataTable(dc);
                        if (dt.Columns.Count >= 2)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                lst.Add($"<option Value='{dr[1]}'>{dr[0]}</option>");
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                lst.Add($"<option>{dr[0]}</option>");
                            }
                        }
                    }
                    else
                    {

                        foreach (var s in dc.Split(','))
                        {
                            lst.Add($"<option>{s.Trim()}</option>");
                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        data = lst,
                    });
                }
                else
                {
                    if (isSQL)
                        dc = PayrollMy.data(dc);
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        data = dc,
                    });
                }

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else if (dataId == "onchange")
            {
                List<string> lst = new List<string>();
                var filters = JsonConvert.DeserializeObject<List<Filter>>(PayrollMy.data($"select filters from HR_MasterPageConfig where pageId='{data["pageId"]}'"));
                var dc = "{[Start Date].Idate} and {[End Date].Idate}";
                var fiterId = data["fiterId"].ToString();
                foreach (var vl in filters)
                {
                    bool found = false;
                    foreach (var v in vl.filteritems)
                    {
                        if (v.id == fiterId)
                        {
                            dc = v.dataquery;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        break;
                }
                dc = processDataQuery(dc, data);
                var dt = PayrollMy.dataTable(dc);
                if (dt.Columns.Count >= 2)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lst.Add($"<option Value='{dr[1]}'>{dr[0]}</option>");
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lst.Add($"<option>{dr[0]}</option>");
                    }
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "success",
                    error = false,
                    dataId = dataId,
                    data = lst.ToArray(),
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            //04/12/2023
            else if (dataId == "sudhir-manual-attendance")
            {
                if (data["type"].ToString() == "employee-list")
                {
                    var date = Convert.ToDateTime($"{data["Year"]}/{data["Month"]}/01");
                    var working_days = DateTime.DaysInMonth(date.Year, date.Month) - Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month))
           .Select(day => new DateTime(date.Year, date.Month, day))
           .Count(d => d.DayOfWeek == DayOfWeek.Sunday);
                    var ds = PayrollMy.dataSet($"select  em.Employee_Name,em.Emp_Code,em.Employee_id,'{date.ToString("MMM")}' Month,'{date.Year}' Year,'{DateTime.DaysInMonth(date.Year, date.Month)}' Days_in_Month,'{working_days}'  working_days,isnull(full_day,0) full_day,isnull(half_day,0) half_day,isnull(Paid_Days,0) Paid_Days from HR_Employee_Master em left join HR_Manual_Attendance_Record ar on em.Employee_id=ar.Employee_id and ar.Year='{date.Year}' and ar.Month='{date.ToString("MMM")}';");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "make-attendance")
                {

                    var sj = new JavaScriptSerializer();
                    var data_list = sj.Deserialize<List<Dictionary<string, string>>>(data["data_list"].ToString());
                    foreach (var d in data_list)
                    {
                        d["Created_By"] = User.Identity.Name;
                        d["Created_On"] = PayrollMy.Now.ToString();
                        PayrollMy.InsertOrUpdate("HR_Manual_Attendance_Record", d, "Employee_id,Month,Year");
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Attendance Updated Successfully",
                        error = false,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "filter")
                {
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
            }
            if (dataId == "employeeList")
            {
                if (data["type"].ToString() == "loading")
                {
                    //string qu = "Select top 1 password from user_details where user_id=em.Emp_Code";
                    // (" + qu + ") as pwd

                    var ds = PayrollMy.dataSet(@"select  em.*,dpm.name Department, dsm.name Designation , gm.grade_name from HR_Employee_Master em left join HR_Grade_Master gm on em.Grade_id=gm.grade_id left join HR_Designation_Master dsm on dsm.Designation_id=em.Designation_id left join HR_Department_Master dpm on dpm.department_id =em.Department_id where em.ActiveStatus='Active' ;
select name,department_id from HR_Department_Master ;
select grade_name,grade_id from HR_Grade_Master ;
select name,Designation_id from HR_Designation_Master;
");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                        department = ds.Tables[1].toJsonObject(),
                        grade = ds.Tables[2].toJsonObject(),
                        designation = ds.Tables[3].toJsonObject(),

                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "filter")
                {
                    var condition = "where 1=1 ";
                    if (data["Grade"].ToString() != "")
                    {
                        condition += $" and em.Grade_id='{data["Grade"]}' ";
                    }
                    if (data["Department"].ToString() != "")
                    {
                        condition += $" and em.Department_id='{data["Department"]}' ";
                    }
                    if (data["Designation"].ToString() != "")
                    {
                        condition += $" and em.Designation_id='{data["Designation"]}' ";
                    }
                    if (data["ActiveStatus"].ToString() != "")
                    {
                        condition += $" and em.ActiveStatus='{data["ActiveStatus"]}' ";
                    }
                    var ds = PayrollMy.dataSet($@"select  em.* ,dpm.name Department, dsm.name Designation , gm.grade_name from HR_Employee_Master em left join HR_Grade_Master gm on em.Grade_id=gm.grade_id left join HR_Designation_Master dsm on dsm.Designation_id=em.Designation_id left join HR_Department_Master dpm on dpm.department_id =em.Department_id   {condition} ; ");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
            }
            if (dataId == "employeePendingList")
            {
                if (data["type"].ToString() == "pageLoad")
                {
                    var ds = PayrollMy.dataSet(@"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,razorpay_payment_id from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId  where ea.Verification_Status='Pending' and ea.Payment_Status='Paid';
select SessionName,SessionId from HR_SessionList ;
");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                        session = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "filter")
                {
                    var ds = PayrollMy.dataSet($"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,razorpay_payment_id from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId where Session_id='{data["Session"]}' and Hiring_id='{data["Hiring"]}' and (Apply_for='{data["Post_Applied"]}' or '{data["Post_Applied"]}'='0') and  ea.Verification_Status='Pending'  and ea.Payment_Status='Paid';");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "rejected-hiring")
                {
                    var ds = PayrollMy.dataSet(@"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,Remarks,Verification_Status from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId  where ea.Verification_Status='Rejected';
select SessionName,SessionId from HR_SessionList ;
");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                        session = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "rejected-hiring-filter")
                {
                    var ds = PayrollMy.dataSet($"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,Remarks,Verification_Status from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId where Session_id='{data["Session"]}' and Hiring_id='{data["Hiring"]}' and (Apply_for='{data["Post_Applied"]}' or '{data["Post_Applied"]}'='0') and  ea.Verification_Status='Rejected';");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "canceled-offer-letter")
                {
                    var ds = PayrollMy.dataSet(@"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,JoiningDate,Salary,JoiningRemarks from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId  where ea.CurrentStatus='Canceled';
select SessionName,SessionId from HR_SessionList ;
");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                        session = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "canceled-offer-letter-filter")
                {
                    var ds = PayrollMy.dataSet($"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,JoiningDate,Salary,JoiningRemarks from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId where Session_id='{data["Session"]}' and Hiring_id='{data["Hiring"]}' and (Apply_for='{data["Post_Applied"]}' or '{data["Post_Applied"]}'='0') and  ea.CurrentStatus='Canceled';");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "offered-hiring")
                {
                    var ds = PayrollMy.dataSet(@"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,JoiningDate,Salary,JoiningRemarks from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId  where ea.CurrentStatus='Offered';
select SessionName,SessionId from HR_SessionList ;
");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                        session = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "offered-hiring-filter")
                {
                    var ds = PayrollMy.dataSet($"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,JoiningDate,Salary,JoiningRemarks from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId where Session_id='{data["Session"]}' and Hiring_id='{data["Hiring"]}' and (Apply_for='{data["Post_Applied"]}' or '{data["Post_Applied"]}'='0') and  ea.CurrentStatus='Offered';");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "selectedList-for-hiring")
                {
                    var ds = PayrollMy.dataSet(@"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,razorpay_payment_id from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId  where ea.CurrentStatus='Selected';
select SessionName,SessionId from HR_SessionList ;
");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                        session = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "selectedList-for-hiring-filter")
                {
                    var ds = PayrollMy.dataSet($"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,razorpay_payment_id from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId where Session_id='{data["Session"]}' and Hiring_id='{data["Hiring"]}' and (Apply_for='{data["Post_Applied"]}' or '{data["Post_Applied"]}'='0') and  ea.CurrentStatus='Selected';");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }

                if (data["type"].ToString() == "selectedList-for-interview")
                {
                    var ds = PayrollMy.dataSet(@"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,razorpay_payment_id,ins.*,it.InterviewType interveiw_type from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId join HR_InterviewSchedules ins on ins.ApplicationId=ea.Apply_id and  ins.Status='Pending'  join HR_InterviewTypes it on ins.InterviewType=it.InterViewTypeId  where ea.CurrentStatus='Intervew';
select SessionName,SessionId from HR_SessionList ;
");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                        session = ds.Tables[1].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "selectedList-for-interview-filter")
                {
                    var ds = PayrollMy.dataSet($"select ea.Id, SessionName,Apply_id,ea.Date,ea.Apply_for,First_Name+' '+ Middle_Name +' '+ Last_Name as fullName, ea.subject_name,Emailid,mobile_no_CA,razorpay_payment_id,ins.*,it.InterviewType interveiw_type from HR_Employee_Online_Apply ea join HR_SessionList ss on ea.Session_id=ss.SessionId join HR_InterviewSchedules ins on ins.ApplicationId=ea.Apply_id and  ins.Status='Pending'  join HR_InterviewTypes it on ins.InterviewType=it.InterViewTypeId  where ea.CurrentStatus='Intervew' and Session_id='{data["Session"]}' and Hiring_id='{data["Hiring"]}' and (Apply_for='{data["Post_Applied"]}' or '{data["Post_Applied"]}'='0');");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "session")
                {
                    var ds = PayrollMy.dataSet($"select HiringName,Vacancy_id Hiring_id from HR_HiringVacancy where SessionId='{data["session"]}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "hiring")
                {
                    //  data["hiring"]
                    var qry = $"select 'All' name,'0' Designation_id union all select name, name Designation_id from HR_Designation_Master ";
                    if (data["hiring"].ToString() != "0")
                    {
                        qry += $" where Designation_id in (select HiringFor from HR_HiringParameterSetup where Vacancy_Id ='{data["hiring"].ToString()}')";
                    }
                    var ds = PayrollMy.dataSet(qry);
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "designation")
                {
                    var ds = PayrollMy.dataSet($"select 'Admin' name,'0' Designation_id union all select name,Designation_id from HR_Designation_Master");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "reject")
                {
                    PayrollMy.Update("HR_Employee_Online_Apply", new
                    {
                        Verification_Status = "Rejected",
                        CurrentStatus = $"Rejected",
                        Remarks = data["remark"],
                    }, where: $"Apply_id='{data["id"]}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "takenby")
                {
                    var ds = PayrollMy.dataSet($"select Name,UserId from HR_UserProfile where UserTypeId='{data["designation"]}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Success",
                        error = false,
                        data = ds.Tables[0].toJsonObject(),
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "hirenow")
                {

                    PayrollMy.Update("HR_Employee_Online_Apply", new
                    {
                        Verification_Status = $"Selected",
                        CurrentStatus = $"Selected",
                        Remarks = data["remark"]
                    }, where: $"Apply_id='{data["id"]}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = $"Employee Hired, but HR Process Still Pending",
                        error = false,
                    });

                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "select-for-offerletter")
                {
                    PayrollMy.Update("HR_Employee_Online_Apply", new
                    {
                        Verification_Status = $"Selected",
                        CurrentStatus = $"Selected",
                        Remarks = data["remark"],
                    }, where: $"Apply_id='{data["id"]}'");



                    var interviewPhase = 1;
                    var mt = PayrollMy.Table($"select top 1 * from HR_InterviewSchedules where  ApplicationId ='{data["id"]}'  order by id desc", where: "");

                    if (mt.Rows.Count > 0)
                    {
                        var dr = mt.Rows[0];
                        interviewPhase = dr["InterviewPhase"].ToInt() + 1;
                        dr["Status"] = "Cleared";
                        mt.Update();
                    }


                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = $"Applicant mark as selected.",
                        error = false,
                    });

                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "generate-offer-letter")
                {
                    var date = Convert.ToDateTime(data["date"]);

                    PayrollMy.Update("HR_Employee_Online_Apply", new
                    {
                        Verification_Status = $"Offer Letter Generated",
                        CurrentStatus = $"Offered",
                        JoiningDate = date.ToString("dd-MMM-yyyy"),
                        Salary = data["gross"],
                        Gross = data["gross"],
                        Grade = data["Grade"],
                        // Grade = data["Grade"],
                        //  Grade = data["Grade"],
                        Deduction = data["deduction"],
                        Netsalary = data["netsalary"],
                        Emp_contribution = data["emp_cont"],
                        CTC_year = data["CTC_year"],
                        CTC_month = data["CTC_month"],
                        income_heads = data["income_heads"],
                        deduction_head = data["deduction_head"],
                        JoiningRemarks = data["remark"],
                    }, where: $"Apply_id='{data["id"]}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = $"Offer Letter Generated.",
                        error = false,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "cancel-offerletter")
                {
                    PayrollMy.Update("HR_Employee_Online_Apply", new
                    {
                        Verification_Status = $"Offer Letter Canceled",
                        CurrentStatus = $"Canceled",
                        Remarks = data["remark"],
                    }, where: $"Apply_id='{data["id"]}'");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = $"Offer Letter Canceled.",
                        error = false,
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "select-for-hiring")
                {
                    var error = false;
                    var message = "";
                    var selectedidate = Convert.ToDateTime(data["date"]);
                    if (selectedidate.idate() < PayrollMy.Now.idate())
                    {
                        error = true;
                        message = "Invalid Interview date";
                    }
                    var interview_time = Convert.ToDateTime(data["interview_time"]);
                    if (!error && selectedidate.idate() == PayrollMy.Now.idate())
                    {
                        if (interview_time.itime() <= PayrollMy.Now.itime())
                        {
                            error = true;
                            message = "Invalid Interview time";
                        }
                    }

                    var reporting_time = Convert.ToDateTime(data["reporting_time"]);
                    if (!error && reporting_time.itime() >= interview_time.itime())
                    {
                        error = true;
                        message = "Reporting time should be greater then interview time";
                    }
                    if (!error)
                    {
                        PayrollMy.Update("HR_Employee_Online_Apply", new
                        {
                            Verification_Status = $"Selected For {data["Interview_type"]} Intervew",
                            CurrentStatus = $"Intervew",
                        }, where: $"Apply_id='{data["id"]}'");
                        var interviewPhase = 1;
                        var mt = PayrollMy.Table($"select top 1 * from HR_InterviewSchedules where  ApplicationId ='{data["id"]}'  order by id desc", where: "");

                        if (mt.Rows.Count > 0)
                        {
                            var dr = mt.Rows[0];
                            interviewPhase = dr["InterviewPhase"].ToInt() + 1;
                            dr["Status"] = "Cleared";
                            mt.Update();
                        }
                        PayrollMy.InsertOrUpdate("HR_InterviewSchedules", new
                        {
                            ApplicationId = data["id"],
                            InterViewDate = selectedidate.ToString("dd-MMM-yyyy"),
                            InterviewTime = interview_time.ToString("hh:mm tt"),
                            ReportingTime = reporting_time.ToString("hh:mm tt"),
                            Status = "Pending",
                            Venue = data["venue"],
                            InterViewTakenBy = data["interview_takenby"],
                            InterviewType = data["Interview_type"],
                            IsWritten = 0,
                            InterviewPhase = interviewPhase,
                        }, compareColumns: "ApplicationId,Status");

                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = $"Interview scheduled on {selectedidate.ToString("dd-MMM-yyyy")} at {interview_time.ToString("hh:mm tt")}",
                            error = false,
                        });
                    }
                    else
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = message,
                            error = error,
                        });
                    }
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }


            }
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        int CountSundays(DateTime startDate, DateTime endDate)
        {
            int sundayCount = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    sundayCount++;
                }
            }
            return sundayCount;
        }
        private ActionResult calculateSalary(Dictionary<string, object> data)
        {

            var date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
            var last_date = date.AddMonths(1).AddDays(-1);
            var days_in_month = DateTime.DaysInMonth(date.Year, date.Month);
            var type = PayrollMy.isalaryCalculationManual ? "Yes" : "No";
            var dt = PayrollMy.dataTable($"sp_HR_before_salary_calculation '{data["Year"]}','{data["Month"]}','{type}'");
            var cal_id = PayrollMy.AutoId("Salary_Calculation_id", "SC", "0000");
            var at = PayrollMy.dataTable($"select top 50 * from HR_Salary_And_Attendance_Rule");
            var gt = PayrollMy.dataTable($"select grade_name,grade_id from HR_Grade_Master");
            var firm = My.data("select firm_name from Firm_Details");
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Employee_id"].ToString() == "EMP024")
                {
                    string a = dr["Employee_id"].ToString();
                }



                if (dr["week_off"].ToString() != dr["Calculated_Week_off"].ToString())
                {
                    if (dr["week_off"].ToInt() == 5)
                    {
                        if (dr["p1"].ToDouble() / 6.0 > 3)
                        {
                            dr["Calculated_Week_off"] = Math.Round((dr["p1"].ToDouble() / 6.0) + 1, 0);
                        }
                    }
                    else
                    {
                        if (dr["p1"].ToDouble() / 6.0 > 3 && dr["p1"].ToDouble() / 6.0 < 4)
                        {
                            dr["Calculated_Week_off"] = Math.Round((dr["p1"].ToDouble() / 6.0) + 1, 0);

                            if (dr["Calculated_Week_off"].ToDouble() >= 4)
                            {
                                dr["Calculated_Week_off"] = 4;
                            }


                        }

                    }
                }


                dr["Paid_Days"] = dr["Paid_Days"].ToDouble() + dr["Calculated_Week_off"].ToDouble();

                if (dr["Emp_Code"].ToString() == "PT001")
                {
                    string a = dr["Emp_Code"].ToString();
                }
                var dpm = PayrollMy.dataTable($"select dsm.name Designation,dpm.name Department from HR_Designation_Master dsm join HR_Department_Master dpm on dsm.DepartmentId=dpm.department_id where dsm.Designation_id='{dr["Designation_id"]}'");
                var Late_Comming_Days = dr["Late_Comming_Days"].ToInt();
                double Full_Day_Deduction = 0;
                double Half_Day_Deduction = 0;
                double paid_days = 0;
                if (at.Rows.Count > 0)
                {
                    var Full_Day = at.Rows[0]["Full_Day_Leave_Count"].ToInt();
                    if (Full_Day > 0)
                    {
                        Full_Day_Deduction = Late_Comming_Days / Full_Day;
                    }
                    var Half_Day = at.Rows[0]["Halft_Day_Leave_Count"].ToInt();
                    if (Half_Day > 0)
                    {
                        int ss = Late_Comming_Days % Full_Day;
                        Half_Day_Deduction = ss / Half_Day;
                    }
                }
                double deduction_days = Full_Day_Deduction + (Half_Day_Deduction / 2.0);
                dr["Late_Deduction"] = deduction_days;
                paid_days = dr["Full_Day_Persent"].ToDouble() + dr["Extra_Shift"].ToDouble() + dr["Half_Day_Persent"].ToDouble() / 2.0 + dr["Half_Pay_leave"].ToDouble() / 2.0 + dr["Paid_Leave"].ToDouble() + dr["Calculated_Week_off"].ToDouble() + dr["night_off"].ToDouble();
                double total_working_days = dr["Working_Days"].ToDouble();
                var income_heads = (new JavaScriptSerializer()).Deserialize<List<Dictionary<string, string>>>(dr["income_heads"].ToString());
                var deduction_heads = (new JavaScriptSerializer()).Deserialize<List<Dictionary<string, string>>>(dr["deduction_heads"].ToString());

                var basic = 0.0;
                var variable_allwance = 0.0;
                var allwance = 0.0;

                foreach (var ih in income_heads)
                {
                    if (ih["IsBasic"].ToString() == "Yes")
                    {
                        basic += ih["inc"].ToDouble();
                    }
                }

                foreach (var ih in income_heads)
                {
                    if (ih["IsBasic"].ToString() == "No")
                    {
                        if (ih["IsVeriable"].ToString() == "No")
                        {
                            allwance += ih["inc"].ToDouble();
                        }
                        else
                        {
                            variable_allwance += ih["inc"].ToDouble();
                        }
                    }
                }

                var fixed_deduction = 0.0;
                var fixed_deduction_employer = 0.0;
                var deduction = 0.0;
                var deduction_employer = 0.0;
                var gross = basic + allwance + variable_allwance;
                foreach (var ih in deduction_heads)
                {
                    if (ih["DeductionType"].ToString() == "PF")
                    {
                        ih["deduct"] = "0";
                        ih["deduct_emp"] = "0";

                        var pf_on = Math.Round((basic / days_in_month) * paid_days, 0);
                        if (pf_on > 15000)
                        {
                            pf_on = 15000;
                        }

                        //if (gross <= ih["BassedOn"].ToDouble())
                        //{
                        if (ih["isEnable"].ToBool())
                        {
                            ih["deduct"] = (pf_on * ih["Employee_Contribution"].Replace("%", "").Replace(" ", "").ToDouble() / 100).ToString("0");
                            ih["deduct_emp"] = (pf_on * ih["Employer_Contribution"].Replace("%", "").Replace(" ", "").ToDouble() / 100).ToString("0");
                        }

                        //}

                    }
                    else if (ih["DeductionType"].ToString() == "ESI")
                    {


                        ih["deduct"] = "0";
                        ih["deduct_emp"] = "0";

                        if (ih["isEnable"].ToBool())
                        {

                            var calulatedgross = Math.Round((gross / days_in_month) * paid_days, 0);
                            // if (calulatedgross <= ih["BassedOn"].ToDouble())
                            if (calulatedgross <= 21000)
                            {
                                ih["deduct"] = (calulatedgross * ih["Employee_Contribution"].Replace("%", "").Replace(" ", "").ToDouble() / 100).ToString("0");
                                ih["deduct_emp"] = (calulatedgross * ih["Employer_Contribution"].Replace("%", "").Replace(" ", "").ToDouble() / 100).ToString("0");
                            }

                        }



                    }
                    else if (ih["DeductionType"].ToString() == "Professional Tax")
                    {
                        ih["deduct"] = "0";
                        ih["deduct_emp"] = "0";
                        var calulatedgross = Math.Round((gross / days_in_month) * paid_days, 0);
                        if (ih["isEnable"].ToBool())
                        {
                            if (calulatedgross >= 0 && calulatedgross <= 10000)
                            {


                            }
                            else if (calulatedgross >= 10001 && calulatedgross <= 15000)
                            {

                                ih["deduct"] = "110";
                            }
                            else if (calulatedgross >= 15001 && calulatedgross <= 25000)
                            {

                                ih["deduct"] = "130";
                            }

                            else if (calulatedgross >= 25001 && calulatedgross <= 40000)
                            {

                                ih["deduct"] = "150";
                            }
                            else
                            {
                                ih["deduct"] = "200";

                            }


                        }
                    }

                    else
                    {

                    }
                    if (ih["IsFixed"].ToString() == "Yes")
                    {
                        fixed_deduction += ih["deduct"].ToDouble();
                        fixed_deduction_employer += ih["deduct_emp"].ToDouble();
                    }
                    else if (ih["IsFixed"].ToString() == "No")
                    {
                        deduction += ih["deduct"].ToDouble();
                        deduction_employer += ih["deduct_emp"].ToDouble();
                    }
                }
                //working days wise calculation 

                foreach (var ih in income_heads)
                {
                    object ss = new
                    {
                        Employee_Id = dr["Employee_Id"],
                        Month = date.ToString("MMMM"),
                        Year = date.Year,
                        IncomeHead = ih["IncomeHead"],
                        DefaultValue = ih["inc"],
                        NetValue = Math.Round((ih["inc"].ToDouble() / days_in_month) * paid_days, 0),
                        Calculation_Id = cal_id
                    };
                    PayrollMy.Insert("Hr_Employee_Salary_Income_Head_Wise", ss);
                }

                foreach (var ih in deduction_heads)
                {
                    object ss = null;
                    ss = new
                    {
                        Employee_Id = dr["Employee_Id"],
                        Month = date.ToString("MMMM"),
                        Year = date.Year,
                        Deduction_Head = ih["DeductionDesc"],
                        Employee_Deduction = Math.Round(ih["deduct"].ToDouble(), 0),
                        Employer_Deduction = Math.Round(ih["deduct_emp"].ToDouble(), 0),
                        Calculation_Id = cal_id
                    };
                    PayrollMy.Insert("Hr_Employee_Salary_Deduction_Head_Wise", ss);
                }
                if (dr["Late_Comming_Days"].ToDouble() > 0)
                {
                    PayrollMy.Insert("Hr_Employee_Salary_Deduction_Head_Wise", new
                    {
                        Employee_Id = dr["Employee_Id"],
                        Month = date.ToString("MMMM"),
                        Year = date.Year,
                        Deduction_Head = $"Late Comming : {dr["Late_Comming_Days"]} Days , Deduction : {deduction_days} Day",
                        Employee_Deduction = Math.Round((gross / days_in_month) * deduction_days, 0),
                        Employer_Deduction = 0,
                        Calculation_Id = cal_id
                    });
                }
                var net_basic_salary = Math.Round((basic / days_in_month) * paid_days, 0);

                // var net_allwances = Math.Round(((variable_allwance / total_working_days) * worked_days) + allwance, 0);
                var net_allwances = Math.Round((((variable_allwance + allwance) / days_in_month) * paid_days), 0);
                var new_deduction = fixed_deduction + deduction + Math.Round((basic / days_in_month) * deduction_days, 0);
                var new_gross = net_basic_salary + net_allwances;



                PayrollMy.InsertOrUpdate("HR_Salary_Calculation_Table", new
                {

                    EmployeeId = dr["Employee_Id"],
                    EmployeeName = dr["Employee_Name"],
                    EmployeeCode = dr["Emp_Code"],
                    Month = date.ToString("MMMM"),
                    Year = date.Year,
                    Days_in_Month = days_in_month,
                    Working_Days_In_a_Month = total_working_days,
                    Grade = dr["Grade"],
                    Full_Day_Present = dr["Full_Day_Persent"],
                    Half_Day_Present = dr["Half_Day_Persent"],
                    Late_Half_Day_Deduction = Half_Day_Deduction,
                    Late_Full_Day_Deduction = Full_Day_Deduction,
                    Late_Coming_Days = dr["Late_Comming_Days"],
                    Paid_Leave = dr["Paid_Leave"],
                    UnPaidLeave = dr["UnPaidLeave"],
                    Half_Pay_leave = dr["Half_Pay_leave"],
                    Basic = basic,
                    Allwance = allwance + variable_allwance,
                    Gross = basic + allwance + variable_allwance,
                    Deduction = deduction + fixed_deduction,
                    Net = basic + allwance + variable_allwance - (deduction + fixed_deduction),
                    Calculeted_Basic = net_basic_salary,
                    Calculeted_Allwance = net_allwances,
                    Calculeted_Gross = net_basic_salary + net_allwances,
                    Calculeted_Deduction = new_deduction,
                    Calculeted_Net = new_gross - new_deduction,

                    Worked_Days = dr["Full_Day_Persent"].ToDouble() + dr["Half_Day_Persent"].ToDouble() + dr["Extra_Shift"].ToDouble(),
                    Deduction_Days = deduction_days,
                    Net_Wroked_Days = dr["Paid_Days"],
                    Designation = dpm.Rows[0]["Designation"],
                    Department = dpm.Rows[0]["Department"],
                    Calculation_Id = cal_id,
                    week_off = dr["week_off"],
                    Late_Deduction = dr["Late_Deduction"],
                    Calculated_Week_off = dr["Calculated_Week_off"],
                    Extra_Shift = dr["Extra_Shift"],

                }, "EmployeeId,Month,Year");


            }
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Salary Calculated Successfully.",
                error = false,
                cal_id = cal_id
            });
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        private List<string> columnLost(DataTable dt)
        {
            var cols = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                cols.Add(dc.ColumnName);
            }
            return cols;
        }

        [Route("export/{id}")]
        public ActionResult exeport_form(string id)
        {
            var path = Server.MapPath("~/Views/Master/form_template.cshtml");
            var temp = System.IO.File.ReadAllText(path);
            temp = temp.Replace("{", "pp@@").Replace("}", "@@pp").Replace("#start#", "{").Replace("#end#", "}");

            var fileds = "";
            var tableheads = "";
            var mpc = getMPCById(id);
            //
            Dictionary<string, string> tps = new Dictionary<string, string>() {
    { "TextBox", "text" },
    { "DatePicker", "date" },
    { "TimePicker", "time" },
    { "MultiLine", "multiline" },
    { "File", "file" },
    { "File-Image", "file" },
    { "File-Pdf", "file" }
};
            foreach (var p in mpc.pcd)
            {
                var required = p.isRequired ? "true" : "false";
                if (p.fieldType == "DDL")
                {
                    if (p.onChanged)
                    {
                        fileds += $"@Html.LabelEditor(\"{p.columnName}\",\"ddl\",\"{p.displayText}\",\"\",{required})\r\n";
                        fileds += $@"
<script>
                             $('#{p.onChangedField}').on('change',function () {{
                                var data = {{ type: 'field-bind' }};
                                data['field_name'] = '{p.columnName}';
                                data['pageId'] = $('#frm-page-Id').val(); 
                                var formData = $('#main-form').serializeArray();
                                $.map(formData, function (input) {{
                                    data[input.name] = input.value;
                                }});
                                gowithData('../../data/ddl', 'Post', JSON.stringify({{ data: data }}), function (data) {{
                                    if (!data.error) {{
                                        var ddl = $('#{p.columnName}');
                                        $('#{p.columnName} option:not(:first)').remove();
                                        $.each(data.data, function (i, item) {{
                                            ddl.append($('<option>').text(item.name).val(item.id));
                                        }});
                                    }}
                                }})
                            }});
                        </script>
";

                    }
                    else if (p.requiredData.ToLower().StartsWith("sql:"))
                    {
                        fileds += $"@Html.LabelEditor(\"{p.columnName}\",\"ddl\",\"{p.displayText}\",\"\",{required})\r\n";
                        fileds += $@"
<script>
                            $(function () {{
                                var data = {{ type: 'field-bind' }};
                                data['field_name'] = '{p.columnName}';
                                data['pageId'] = $('#frm-page-Id').val(); 
                                var formData = $('#main-form').serializeArray();
                                $.map(formData, function (input) {{
                                    data[input.name] = input.value;
                                }});
                                gowithData('../../data/ddl', 'Post', JSON.stringify({{ data: data }}), function (data) {{
                                    if (!data.error) {{
                                        var ddl = $('#{p.columnName}');
                                        $('#{p.columnName} option:not(:first)').remove();
                                        $.each(data.data, function (i, item) {{
                                            ddl.append($('<option>').text(item.name).val(item.id));
                                        }});
                                    }}
                                }})
                            }});
                        </script>
";
                    }
                    else
                    {
                        string pattern = @"{([^}]*)}";
                        var matches = Regex.Matches(p.requiredData, pattern);
                        if (matches.Count > 0)
                        {
                            var lst = new List<string>();

                            foreach (Match m in matches)
                            {
                                var sss = m.Groups[1].Value.Split(',');
                                lst.Add($"ddl.append($('<option>').text({sss[0]}).val({sss[1]}));");
                            }
                            fileds += $"@Html.LabelEditor(\"{p.columnName}\",\"ddl\",\"{p.displayText}\",\"\",{required})\r\n";
                            fileds += $@"
<script>
                            $(function () {{
                                 var ddl = $('#{p.columnName}');
                                 {string.Join("\r\n", lst)} 
                            }});
                        </script>";

                        }
                        else
                        {
                            fileds += $"@Html.LabelEditor(\"{p.columnName}\",\"ddl\",\"{p.displayText}\",\"\",{required},ddl_comma_seperated:\"{p.requiredData}\")\r\n";
                        }
                    }

                }
                else if (tps.ContainsKey(p.fieldType))
                {
                    fileds += $"@Html.LabelEditor(\"{p.columnName}\",\"{tps[p.fieldType]}\",\"{p.displayText}\",\"\",{required})\r\n";
                }
            }
            foreach (var p in mpc.gcd)
            {
                if (p.isShowOnGrid)
                {
                    tableheads += $"<th>{p.displayText}</th>\r\n";
                }
            }
            //empty fields
            var empty_fields = "";
            foreach (var p in mpc.pcd)
            {
                empty_fields += $"$('#{p.columnName}').val('{p.defaultValue}');\r\n";
                if (p.onChanged)
                {
                    empty_fields += $"$('#{p.onChangedField}').removeAttr('data-{p.columnName}');\r\n";
                    empty_fields += $"$('#{p.onChangedField}').change();\r\n";
                }
                if (p.fieldType.Contains("File"))
                {
                    empty_fields += $"$('#file_{p.columnName}').val('');\r\n";
                    empty_fields += $"$('#img_{p.columnName}').hide();\r\n";
                }
            }

            //bind table data
            var bind_table_data = "";
            foreach (var g in mpc.gcd)
            {
                if (g.isShowOnGrid)
                {
                    if (g.dataType == "Image")
                    {
                        bind_table_data += $" $('<td>').html('<a href='+d.{g.columnName}+' target=\"_blank\"><img src='+d.{g.columnName}+' class=\"img-thumbnail\" style=\"height: 50px;\" ></a>'),\r\n";
                    }
                    else if (g.dataType == "Other File")
                    {
                        bind_table_data += $" $('<td>').html('<a href='+d.{g.columnName}+' target=\"_blank\" download > <i class=\"fas fa-file-download\" style=\"font-size: 2rem;\"></i></a>'),\r\n";
                    }
                    else if (g.dataType == "Status")
                    {
                        bind_table_data += $" $('<td>').html(getstatusview('{g.requiredData}', d.{g.columnName})),\r\n";
                    }
                    else
                    {
                        bind_table_data += $" $('<td>').text(d.{g.columnName}),\r\n";
                    }
                }
            }
            //edit data bind
            var bind_edit_data = "";
            foreach (var p in mpc.pcd)
            {
                if (p.isField)
                {

                    if (p.fieldType.Contains("File"))
                    {
                        bind_edit_data += $"$('#{p.columnName}').val(data[0].{p.columnName})\r\n";
                        bind_edit_data += $"$('#img_{p.columnName}').attr('src',data[0].{p.columnName})\r\n";
                        bind_edit_data += $"$('#img_{p.columnName}').show()\r\n";
                    }
                    else if (p.fieldType.Contains("TimePicker"))
                    {
                        bind_edit_data += $"$('#{p.columnName}').val(convertTo24Hour(data[0].{p.columnName}))\r\n";
                    }
                    else if (p.fieldType.Contains("DatePicker"))
                    {
                        bind_edit_data += $"$('#{p.columnName}').val(formatDate(data[0].{p.columnName}))\r\n";
                    }
                    else if (p.fieldType.Contains("CheckBox-List"))
                    {
                        bind_edit_data += $"muticheck('{p.columnName}',data[0].{p.columnName})\r\n";
                    }
                    else if (p.fieldType.Contains("RadioButton"))
                    {
                        bind_edit_data += $"muticheck('{p.columnName}',data[0].{p.columnName})\r\n";
                        bind_edit_data += $"$('input[name=\"{p.columnName}\"]').change()\r\n";

                    }
                    else
                    {
                        bind_edit_data += $"$('#{p.columnName}').val(data[0].{p.columnName})\r\n";
                    }
                    if (p.onChanged)
                    {
                        bind_edit_data += $"$('#{p.onChangedField}').attr('data-{p.columnName}',data[0].{p.columnName})\r\n";
                        bind_edit_data += $"$('#{p.onChangedField}').change()\r\n";
                    }

                }
            }
            temp = String.Format(temp, fileds, tableheads, empty_fields, bind_table_data, bind_edit_data);
            //Form
            //Table
            //submit data
            //bind table
            temp = temp.Replace("pp@@", "{").Replace("@@pp", "}");
            byte[] byteArray = Encoding.UTF8.GetBytes(temp);

            var fileStream = new MemoryStream(byteArray);
            return File(fileStream,
                  "text/plain",
                   string.Format("{0}.txt", mpc.pageUrl));
        }
        private JsonResult exeport_frm(Dictionary<string, object> data)
        {
            var mpc = getMPCById(data["pageId"].ToString());

            var httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
            var routeData = new RouteData();
            var controllerContext = new ControllerContext(new RequestContext(httpContext, routeData), new MasterController());

            var viewContext = new ViewContext(controllerContext, new WebFormView(controllerContext, "dummy"), new ViewDataDictionary(), new TempDataDictionary(), TextWriter.Null);
            var viewPage = new ViewPage { ViewContext = viewContext };
            HtmlHelper html = new HtmlHelper(viewContext, viewPage);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<style> .row { --bs-gutter-x: 0.5rem; --bs-gutter-y: 0; } .table.dataTable { margin-top: 0px !important; } #mytable { min-height: 100px; } .dataTables_scroll { border: 1px solid #ddd !important; margin-top: 0.25rem !important; } .filter-btn { display: flex; flex-direction: column; justify-content: flex-end; } </style>");
            sb.AppendLine("<div>");
            #region breadcrumb
            sb.AppendLine(@"<div class=""page-breadcrumb d-flex align-items-center mb-2"">");
            sb.AppendLine($@"            <div class=""ps-3 d-none d-sm-flex"">
                <nav aria-label=""breadcrumb"">
                    <ol class=""breadcrumb mb-0 p-0"">
                        <li class=""breadcrumb-item"">
                            <a href=""javascript:;""><i class=""bx bx-home-alt""></i></a>
                        </li>
                        <li class=""breadcrumb-item active"" aria-current=""page"">{mpc.pageTitle}</li>
                    </ol>
                </nav>
            </div>");
            if (mpc.pageType == "Form")
            {
                if (mpc.formType == "ButtonAdd")
                {
                    sb.AppendLine($@"<div class=""ms-auto"">
                        <button type=""button"" onclick=""openModel()"" id=""btn_new"" class=""btn btn-success btn-sm""><i class=""bx bx-plus-circle""></i> Add New</button>
                    </div>");
                }
                if (mpc.formType == "PopUp")
                {
                    sb.AppendLine($@"<div class=""ms-auto"">
                        <button type=""button"" onclick=""openModel()"" id=""btn_new"" class=""btn btn-success btn-sm""><i class=""bx bx-plus-circle""></i> Add New</button>
                    </div>");
                }
            }
            sb.AppendLine("</div>");
            #endregion breadcrumb

            #region main row
            sb.AppendLine(@"        <div class=""row"">");
            if (mpc.pageType == "Form")
            {
                if (mpc.formType == "PopUp")
                {

                    sb.AppendLine($@"<div class=""modal fade"" id=""model-form"" tabindex=""-1"" aria-hidden=""true"" data-bs-backdrop=""static"" data-bs-keyboard=""false"">
                        <div class=""modal-dialog modal-lg"">
                            @using (Ajax.BeginForm(""page"", ""save"", null, new AjaxOptions
                            {{
                                OnSuccess = ""handleSuccess"",
                                OnFailure = ""fnfailor"",
                                OnBegin = ""fnbegin"",
                            }}, new {{ @class = ""my-form"", id = ""main-form"" }}))
                            {{ 
                                @Html.Hidden(""frm-page-Id"", {mpc.pageId})
                                @Html.Hidden(""HdId"")
                                <div class=""modal-content"">
                                    <div class=""modal-header"">
                                        <h5 class=""modal-title"">Add {mpc.pageTitle}</h5>
                                        <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                                    </div>
                                    <div class=""modal-body"">

                                        <div class=""row"">");

                    foreach (var p in mpc.pcd)
                    {
                        if (!p.isField)
                        { }
                        else if (p.fieldType == "MultiLine")
                        {
                            sb.AppendLine(html.MasterField(p, "col-lg-12 col-md-6").ToHtmlString());
                        }
                        else
                        {
                            sb.AppendLine(html.MasterField(p, "col-xxl-6 col-xl-6  col-lg-12  col-md-4 col-sm-6").ToHtmlString());
                        }
                    }

                    sb.AppendLine($@"   </div>
                                    </div>
                                    <div class=""modal-footer p-2 d-block"">
                                        <div class=""row"">
                                            <div class=""col-sm-12"">

                                                <button id=""btn-add"" type=""submit"" class=""btn btn-primary"">Add</button>
                                                <button id=""btn-cancel"" type=""button"" style=""display: none;"" class=""btn btn-dark"">Cancel</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            }}
                        </div>
                    </div>");
                }
                else
                {
                    sb.AppendLine($@"<div class=""@formclass"">

                        @using (Ajax.BeginForm(""page"", ""save"", null, new AjaxOptions
                        {{
                            OnSuccess = ""handleSuccess"",
                            OnFailure = ""fnfailor"",
                            OnBegin = ""fnbegin"",
                        }}, new {{ @class = ""my-form"", id = ""main-form"" }}))
                        {{ 
                            @Html.Hidden(""frm-page-Id"", @mpc.pageId.ToString())
                            @Html.Hidden(""HdId"")
                            <div class=""card"">
                                <div class=""card-body"">
                                    <div class=""row"">");

                    foreach (var p in mpc.pcd)
                    {
                        if (!p.isField)
                        { }
                        else if (p.fieldType == "MultiLine")
                        {
                            sb.AppendLine(html.MasterField(p, "col-lg-12 col-md-6").ToHtmlString());
                        }
                        else
                        {
                            sb.AppendLine(html.MasterField(p, "col-xxl-6 col-xl-6  col-lg-12  col-md-4 col-sm-6").ToHtmlString());
                        }
                    }

                    sb.AppendLine($@"
                                    </div>
                                </div>
                                <div class=""card-footer"">
                                    <div class=""row"">
                                        <div class=""col-sm-12"">

                                            <button id=""btn-add"" type=""submit"" class=""btn btn-primary"">Add</button>
                                            <button id=""btn-cancel"" type=""button"" style=""display: none;"" class=""btn btn-dark"">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        }}

                    </div>");



                }
            }
            else
            {
                sb.AppendLine(@"@Html.Hidden(""frm-page-Id"", @mpc.pageId.ToString())");
                sb.AppendLine(@"@Html.Hidden(""HdId"")");
            }
            sb.AppendLine($@" < div class=""@dataclass"">
                <div class=""card"">
                    @if (mpc.filters.Count > 0)
                    {{
                        <div class=""card-header pb-0"">
                            <div class=""row"">
                                @foreach (var f in mpc.filters)
                                {{
                                    <form id=""@f.uniqueId"" class=""d-flex mb-2 w-auto"">

                                        @foreach (var fi in f.filteritems)
                                        {{
                                            @Html.MasterField(fi, ""input-group me-2"")
                                        }}
                                        <div class=""filter-btn"">
                                            <button class=""btn btn-primary"" type=""button"" data-id=""@f.uniqueId"" onclick=""apply_filter(this)"" style=""width: auto; "">@f.btnName</button>
                                        </div>
                                    </form>
                                }}
                            </div>
                        </div>
                    }}
                    <div class=""card-body px-2 py-1"">

                        <table class=""table table-bordered table-striped"" id=""mytable"">
                            <thead>
                                <tr>
                                    @if (mpc.pageType == ""Form"")
                                    {{
                                        <th>Action</th>
                                    }}
                                    else if (mpc.action.actionList != null)
                                    {{
                                        if (mpc.action.actionList.Length > 0)
                                        {{
                                            <th>Action</th>
                                        }}
                                    }}

                                    @foreach (var p in mpc.gcd)
                                    {{
                                        if (p.isShowOnGrid)
                                        {{
                                            <th>@p.displayText</th>
                                        }}
                                    }}

                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                        <div id=""grid_prog"" style=""margin : 0px auto; width:200px"">
                            <span class=""spinner-border spinner-border-sm me-2""> </span>
                            Please wait....
                        </div>

                    </div>
                </div>
            </div>
            @Html.Hidden(""HdId"")
            @if (mpc.action.actionList != null)
            {{
                foreach (ActionList al in mpc.action.actionList)
                {{
                    if (al.type == ""Url"")
                    {{
                        continue;
                    }}
                    <div class=""modal fade"" id=""model-@al.uniqueId"" tabindex=""-1"" aria-hidden=""true"" data-bs-backdrop=""static"" data-bs-keyboard=""false"">
                        <div class=""modal-dialog modal-lg"">
                            @using (Ajax.BeginForm(""action_form"", ""save"", null, new AjaxOptions
                            {{
                                OnSuccess = ""handleSuccess"",
                                OnFailure = ""fnfailor"",
                                OnBegin = ""fnbegin"",
                            }}, new {{ @class = ""my-form"", id = ""action-form-"" + @al.uniqueId }}))
                            {{
                                @Html.Hidden(""frm-page-Id"", @mpc.pageId.ToString())
                                @Html.Hidden(""act-cust-Id"", al.action_id)
                                @Html.Hidden(""HdId"")
                                <div class=""modal-content"">
                                    <div class=""modal-header"">
                                        <h5 class=""modal-title"">@al.name</h5>
                                        <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                                    </div>
                                    <div class=""modal-body"">
                                        <div>

                                        </div>
                                        <div class=""row"">
                                            @if (al.type == ""Form"")
                                            {{
                                                foreach (var p in mpc.pcd)
                                                {{
                                                    if (!al.col_list.Contains(p.columnName))
                                                    {{
                                                        continue;
                                                    }}
                                                    if (!p.isField)
                                                    {{

                                                    }}
                                                    else if (p.fieldType == ""MultiLine"")
                                                    {{
                                                        @Html.MasterField(p, ""col-lg-12 col-md-6"")
                                                    }}
                                                    else
                                                    {{
                                                        @Html.MasterField(p, ""col-xxl-6 col-xl-6  col-lg-12  col-md-4 col-sm-6"")
                                                    }}

                                                }}
                                            }}
                                            else
                                            {{
                                                foreach (var p in al.act_pcd)
                                                {{

                                                    if (p.fieldType == ""MultiLine"")
                                                    {{
                                                        @Html.MasterField(p, ""col-lg-12 col-md-6"")
                                                    }}
                                                    else
                                                    {{
                                                        @Html.MasterField(p, ""col-xxl-6 col-xl-6  col-lg-12  col-md-4 col-sm-6"")
                                                    }}
                                                }}
                                            }}

                                        </div>
                                    </div>
                                    <div class=""modal-footer p-2 d-block"">
                                        <div class=""row"">
                                            <div class=""col-sm-12"">

                                                <button id=""btn-add-@al.uniqueId"" type=""submit"" class=""btn btn-primary"">@al.btn_text</button>
                                                <button type=""button"" data-bs-dismiss=""modal"" class=""btn btn-dark"">Cancel</button>
                                               
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            }}
                        </div>
                    </div>");

            sb.AppendLine("</div>");
            #endregion main row

            sb.AppendLine("</div>");
            System.IO.File.WriteAllText("D:\\index.html", sb.ToString());
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                error = false,
                message = "Generated Successfully",
                data = sb.ToString()
            });
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [Route("filter/{filterId}")]
        [HttpPost]
        [Authorize]
        public JsonResult filter(string filterId, Dictionary<string, object> data)
        {
            var filters = JsonConvert.DeserializeObject<List<Filter>>(PayrollMy.data($"select filters from HR_MasterPageConfig where pageId='{data["pageId"]}'"));
            var dc = "";// "{[Start Date].Idate} and {[End Date].Idate}";
            foreach (var v in filters)
            {
                if (v.uniqueId == filterId)
                {
                    dc = v.filterquery;
                    break;
                }
            }
            if (dc == "")
            {
                var resp1 = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Unable to Get Data",
                    error = true,
                    pageId = filterId,
                });
                return Json(resp1, JsonRequestBehavior.AllowGet);
            }
            dc = processDataQuery(dc, data);

            var dt = PayrollMy.dataTable(dc);
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Data Fetch successfully",
                error = false,
                pageId = filterId,
                data = dt.toJsonObject()
            });
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        private string processDataQuery(string dataquery, Dictionary<string, object> data, string extnsn = "flt-")
        {
            var dc = dataquery;
            string pattern = @"{([^}]*)}";
            var matches = Regex.Matches(dc, pattern);
            foreach (Match match in matches)
            {
                string vrbl = match.Groups[1].Value;
                if (vrbl == "Idate")
                {
                    dc = dc.Replace(match.Value, PayrollMy.Now.ToString("yyyyMMdd"));
                }
                else if (vrbl == "UserId" || vrbl == "currentUser")
                {
                    dc = dc.Replace(match.Value, User.Identity.Name);
                }
                else if (vrbl.StartsWith("Session["))
                {
                    string sessionName = Regex.Match(vrbl, @"\[(.*?)\]").Groups[1].Value.Replace(" ", "_");
                    var val = Session[sessionName] ?? "";
                    dc = dc.Replace(match.Value, val.ToString());
                }
                else if (vrbl.StartsWith("Date.Format("))
                {
                    string format = Regex.Match(vrbl, @"\(""(.*?)""\)").Groups[1].Value;
                    dc = dc.Replace(match.Value, PayrollMy.Now.ToString(format));
                }
                else if (vrbl.EndsWith(".Idate"))
                {
                    string fieldName = extnsn + Regex.Match(vrbl, @"\[(.*?)\]").Groups[1].Value.Replace(" ", "_");
                    if (data.ContainsKey(fieldName))
                    {
                        if (data[fieldName].ToString() != "")
                        {
                            dc = dc.Replace(match.Value, Convert.ToDateTime(data[fieldName]).ToString("yyyyMMdd"));
                        }
                        else
                        {
                            dc = dc.Replace(match.Value, "");
                        }
                    }
                }
                else if (vrbl.Contains("].Format("))
                {
                    string format = Regex.Match(vrbl, @"\(""(.*?)""\)").Groups[1].Value;
                    string fieldName = extnsn + Regex.Match(vrbl, @"\[(.*?)\]").Groups[1].Value.Replace(" ", "_");
                    if (data.ContainsKey(fieldName))
                    {
                        if (data[fieldName].ToString() != "")
                        {
                            dc = dc.Replace(match.Value, Convert.ToDateTime(data[fieldName]).ToString(format));
                        }
                        else
                        {
                            dc = dc.Replace(match.Value, "");
                        }

                    }
                }
                else if (vrbl.EndsWith(".Text"))
                {
                    string fieldName = extnsn + Regex.Match(vrbl, @"\[(.*?)\]").Groups[1].Value.Replace(" ", "_");
                    if (data.ContainsKey(fieldName))
                    {
                        dc = dc.Replace(match.Value, data[fieldName].ToString());
                    }
                }
                else if (vrbl.EndsWith(".Value"))
                {
                    string fieldName = extnsn + Regex.Match(vrbl, @"\[(.*?)\]").Groups[1].Value.Replace(" ", "_") + "_Value";
                    if (data.ContainsKey(fieldName))
                    {
                        dc = dc.Replace(match.Value, data[fieldName].ToString());
                    }
                }
            }
            pattern = pattern = @"\{(.*?)\}";
            matches = Regex.Matches(dc, pattern);
            foreach (Match match in matches)
            {
                string vrbl = match.Groups[1].Value;

                foreach (var k in data.Keys)
                {
                    if (k.ToString().equalIgnoreCase(vrbl))
                    {
                        dc = dc.Replace(match.Value, data[k].ToString());
                        break;
                    }
                }
                //if (data.ContainsKey(vrbl))
                //{
                //    dc = dc.Replace(match.Value, data[vrbl].ToString());
                //}
            }

            matches = Regex.Matches(dc, pattern);
            foreach (Match match in matches)
            {
                string vrbl = match.Groups[1].Value;
                if (vrbl.equalIgnoreCase("currentSession"))
                {
                    dc = dc.Replace(match.Value, PayrollMy.currentSession);
                }
                else if (vrbl.equalIgnoreCase("currentUser"))
                {
                    dc = dc.Replace(match.Value, User.Identity.Name);
                }
                else if (vrbl.equalIgnoreCase("isHR"))
                {
                    dc = dc.Replace(match.Value, $"{isHR()}");
                }
                else if (vrbl.equalIgnoreCase("isAdmin"))
                {
                    dc = dc.Replace(match.Value, $"{IsAdmin()}");
                }
                else if (vrbl.equalIgnoreCase("currentUser"))
                {
                    dc = dc.Replace(match.Value, User.Identity.Name);
                }
            }

            return dc;
        }


        [Route("actn/login")]
        [HttpPost]
        [AllowAnonymous]
        public JsonResult logindata(Dictionary<string, object> data = null, FormCollection form = null)
        {
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Invalid User Id or Password",
                error = true,
                data = "",
            });

            var dt = PayrollMy.dataTable($"select top 1 * from HR_UserProfile where UserId='{data["UserId"]}' or Mobile='{data["UserId"]}' or EmployeeCode='{data["UserId"]}'  or HMS_UserId='{data["UserId"]}' order by id desc");
            if (dt.Rows.Count == 0)
            {
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Invalid User Id or Password",
                    error = true,
                    data = "",
                });
            }
            else
            {
                if (dt.Rows[0]["Password"].ToString() == data["Password"].ToString())
                {
                    FormsAuthentication.SetAuthCookie(dt.Rows[0]["UserId"].ToString(), false);
                    Session["UserType"] = dt.Rows[0]["UserType"].ToString();
                    Session["UserName"] = dt.Rows[0]["Name"].ToString();
                    Session["Userid"] = dt.Rows[0]["UserId"].ToString();
                    Session["IsHR"] = dt.Rows[0]["IsHr"].ToString();
                    Session["UserProfileImage"] = dt.Rows[0]["ProfileImage"].ToString();
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "",
                        error = false,
                        data = "",
                    });
                }
                else
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Invalid User Id or Password",
                        error = true,
                        data = "",
                    });
                }

            }



            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        My mycode = new My();
        [Route("save/{method_id}")]
        [Route("actn/{method_id}")]
        [HttpPost]
        [Authorize]

        public JsonResult savedata(string method_id, Dictionary<string, object> data = null, FormCollection form = null)
        {
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Some thing goes worng",
                error = true,
                method_id = method_id,
                data = data,
            });

            if (method_id.equalIgnoreCase("joiningemployee"))
            {

                var emp_dt = PayrollMy.dataTable($"select * from HR_Employee_Online_Apply  where Apply_id='{data["applyId"]}' ");
                if (emp_dt.Rows.Count == 0)
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Unable get employee details",
                        error = true,
                    });
                }
                else
                {
                    if (emp_dt.Rows[0]["CurrentStatus"].ToString() != "Offered")
                    {
                        var mess = "";
                        if (emp_dt.Rows[0]["CurrentStatus"].ToString() == "Joined")
                        {
                            mess = "This employee is already joined";
                        }
                        else if (emp_dt.Rows[0]["CurrentStatus"].ToString() == "Canceled")
                        {
                            mess = "The Offer letter is canceled for this employee";
                        }
                        else if (emp_dt.Rows[0]["CurrentStatus"].ToString() == "Intervew")
                        {
                            mess = "Looks like the candidates not clear the interview";
                        }
                        else
                        {
                            mess = "Looks like the candidates not clear the interview";
                        }
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = mess,
                            error = true,
                        });

                    }
                }
                var emp = JsonConvert.DeserializeObject<Employee_Apply>(data["formdata"].ToString());
                var frm = JsonConvert.DeserializeObject<Dictionary<string, object>>(data["formdata"].ToString());
                var documents = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["documents"].ToString());
                var incomes = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["incomes"].ToString());
                var deductions = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["deductions"].ToString());
                //Employee_Code
                //chk_emp
                var emp_Code = "";
                var employeeId = "";
                if (emp_dt.Rows[0]["Emp_code"].ToString() == "")
                {
                    employeeId = emp_Code = PayrollMy.AutoId("EmployeeId", "EMP", "000");
                }
                else
                {
                    employeeId = emp_Code = emp_dt.Rows[0]["Emp_code"].ToString();
                }
                if (frm.ContainsKey("Employee_Code"))
                {
                    emp_Code = frm["Employee_Code"].ToString();
                }


                try
                {
                    var name1 = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}";
                    mycode.send_data_Create_employee(emp_Code, name1, emp.Gender, emp.Date_birthday, emp.City_CA, "", "", emp.mobile_no_CA, emp.State_CA, "", Convert.ToDateTime(frm["date_of_joining"]).ToString("dd/MM/yyyy"));
                }
                catch
                {
                }



                PayrollMy.Update("HR_Employee_Online_Apply", new
                {
                    Salutation = emp.Salutation,
                    First_Name = emp.First_Name,
                    Middle_Name = emp.Middle_Name,
                    Last_Name = emp.Last_Name,
                    Emailid = emp.Emailid,
                    Date_birthday = emp.Date_birthday,
                    Gender = emp.Gender,
                    Place_Of_Birth = emp.Place_Of_Birth,
                    Birth_State = emp.Birth_State,
                    Religion = emp.Religion,
                    Nationality = emp.Nationality,
                    Marital_Status = emp.Marital_Status,
                    Address_CA = emp.Address_CA,
                    City_CA = emp.City_CA,
                    State_CA = emp.State_CA,
                    Pincode_CA = emp.Pincode_CA,
                    mobile_no_CA = emp.mobile_no_CA,
                    Residence_telephone_no_CA = emp.Residence_telephone_no_CA,
                    address_pa = emp.address_pa,
                    city_pa = emp.city_pa,
                    state_pa = emp.state_pa,
                    pin_pa = emp.pin_pa,
                    chiled_name1 = emp.chiled_name1,
                    chiled_gender1 = emp.chiled_gender1,
                    chiled_age1 = emp.chiled_age1,
                    chiled_name2 = emp.chiled_name2,
                    chiled_gender2 = emp.chiled_gender2,
                    chiled_age2 = emp.chiled_age2,
                    chiled_name3 = emp.chiled_name3,
                    chiled_gender3 = emp.chiled_gender3,
                    chiled_age3 = emp.chiled_age3,
                    fathername = emp.fathername,
                    father_occupation = emp.father_occupation,
                    mother_name = emp.mother_name,
                    mother_occupation = emp.mother_occupation,
                    Spouse_name = emp.Spouse_name,
                    Spouses_job_is_transferable = emp.Spouses_job_is_transferable,
                    spouse_qualification = emp.spouse_qualification,
                    spouse_profession = emp.spouse_profession,
                    spouse_organization = emp.spouse_organization,
                    spouse_designation = emp.spouse_designation,
                    completed_years = emp.completed_years,
                    teaching_years = emp.teaching_years,
                    Administration_year = emp.Administration_year,
                    any_other = emp.any_other,
                    Current_name_of_instituation = emp.Current_name_of_instituation,
                    instituation_address = emp.instituation_address,
                    Contact_Numbe_instituation = emp.Contact_Numbe_instituation,
                    Designation_work = emp.Designation_work,
                    //joining_date = emp.joining_date,
                    place_of_posting = emp.place_of_posting,
                    Present_Salary = emp.Present_Salary,
                    Basic_Salary_Present = emp.Basic_Salary_Present,
                    Allowance_Present = emp.Allowance_Present,
                    Other_Benefits_Present = emp.Other_Benefits_Present,
                    Under_Service_Bond = emp.Under_Service_Bond,
                    years_service_bond = emp.years_service_bond,
                    Expected_Salary = emp.Expected_Salary,
                    English_read = emp.English_read == "true" ? "Yes" : "No",
                    English_write = emp.English_write == "true" ? "Yes" : "No",
                    English_Speak = emp.English_Speak == "true" ? "Yes" : "No",
                    Hindi_read = emp.Hindi_read == "true" ? "Yes" : "No",
                    Hindi_write = emp.Hindi_write == "true" ? "Yes" : "No",
                    Hindi_speak = emp.Hindi_speak == "true" ? "Yes" : "No",
                    Bangla_read = emp.Bangla_read == "true" ? "Yes" : "No",
                    Bangla_write = emp.Bangla_write == "true" ? "Yes" : "No",
                    Bangla_speak = emp.Bangla_speak == "true" ? "Yes" : "No",
                    Other_Language = emp.Other_Language,
                    Proficiency_In_Computer = emp.Proficiency_In_Computer,
                    passport_photo = emp.passport_photo,
                    Signature = emp.Signature,
                    Emp_code = emp_Code,
                    district_ca = emp.district_ca,
                    ps_ca = emp.ps_ca,
                    district_pa = emp.district_pa,
                    ps_pa = emp.ps_pa,
                    spouse_mobile_no = emp.spouse_mobile_no,

                    Verification_Status = "Joined",
                    CurrentStatus = "Joined",

                }, $"Apply_id='{data["applyId"]}'");



                PayrollMy.InsertOrUpdate("HR_UserProfile", new
                {
                    UserId = employeeId,
                    EmployeeCode = emp_Code,
                    Mobile = emp.mobile_no_CA,
                    
                    Password = frm["Password"],
                    Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                    ProfileImage = emp.passport_photo,
                    UserTypeId = emp_dt.Rows[0]["DesignationId"],
                    UserType = PayrollMy.get_user_type_HR_Employee_Online_Apply(emp_dt.Rows[0]["Apply_id"].ToString()),
                }, "Mobile");



                PayrollMy.InsertOrUpdate("user_details", new
                {
                    name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                    mobile = emp.mobile_no_CA,
                    user_id = emp_Code,
                    password = frm["Password"],
                    status = "Active",
                    date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"),
                    firm = My.get_firm_id(),
                    User_Type = PayrollMy.get_user_type_HR_Employee_Online_Apply(emp_dt.Rows[0]["Apply_id"].ToString()),
                    Branch_id = "1",
                    Istatus = "1",

                    ProfilePhoto = emp.passport_photo,
                    Signature = emp.Signature,
                    email = frm["Emailid"],
                }, "user_id");
                PayrollMy.InsertOrUpdate("PRL_Employee_Master", new
                {
                    Employee_Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                    Gender = emp.Gender,
                    Date_of_birth = emp.Date_birthday,

                    Blood_group = frm["BloodGroup"],
                    Religion = emp.Religion,
                    Marital_Status = emp.Marital_Status,
                    Father_Name = emp.fathername,
                    Pan = frm["PAN"],
                    Address = emp.Address_CA,
                    City = emp.City_CA,
                    Pincode = emp.Pincode_CA,
                    State = emp.State_CA,

                    //State_code = emp.State_code,
                    Email = emp.Emailid,
                    Mobile = emp.mobile_no_CA,
                    Employee_image = emp.passport_photo,
                    Emp_Code = emp_Code,
                    Punch_Card_no = frm["Punch_card"],
                    Official_email_id = frm["office_email"],
                    Grade_id = emp_dt.Rows[0]["Grade"],
                    Department_id = emp_dt.Rows[0]["DepartmentId"],
                    Designation_id = emp_dt.Rows[0]["DesignationId"],
                    EPF_no = frm["EPF_No"],
                    ESIC_no = frm["esic_no"],
                    Employee_id = emp_Code, //employeeId,
                    Date_of_Joining = Convert.ToDateTime(frm["date_of_joining"]).ToString("dd-MMM-yyyy"),
                    Bank_Name = frm["Bank_Name"],
                    Branch = frm["Branch"],
                    Ifsc = frm["IFSC"],
                    Micr = frm["MICR"],
                    iDOB = Convert.ToDateTime(emp.Date_birthday).ToString("yyyyMMdd"),
                    College_Name = My.get_college_name(),
                    employee_type = PayrollMy.get_user_type_HR_Employee_Online_Apply(emp_dt.Rows[0]["Apply_id"].ToString()),
                    Status = "Active",


                }, "Emp_Code");

                // insert account ledger



                try
                {
                    menuPermission.update_menu_permission_for_user(emp_Code, PayrollMy.get_user_type_HR_Employee_Online_Apply(emp_dt.Rows[0]["Apply_id"].ToString()));
                }
                catch
                {

                }



                foreach (var doc in documents)
                {
                    PayrollMy.Insert("HR_Employee_Documents", new
                    {
                        EmployeeId = employeeId,
                        Doc_Name = doc["Doc_Name"],
                        file_path = doc["file"],
                    });
                }
                foreach (var inc in incomes)
                {
                    PayrollMy.Insert("HR_Emp_Salary_Structure_IncomeHead", new
                    {
                        EmployeeId = employeeId,
                        IncomeHeadId = inc["IncomeHeadId"],
                        IncomeValue = inc["inc"],
                    });
                }



                PayrollMy.Insert("HR_Employee_Master", new
                {
                    Employee_Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                    Gender = emp.Gender,
                    Date_of_birth = emp.Date_birthday,

                    Blood_group = frm["BloodGroup"],
                    Religion = emp.Religion,
                    Marital_Status = emp.Marital_Status,
                    Father_Name = emp.fathername,
                    Pan = frm["PAN"],
                    Address = emp.Address_CA,
                    City = emp.City_CA,
                    Pincode = emp.Pincode_CA,
                    State = emp.State_CA,
                    //State_code = emp.State_code,
                    Email = emp.Emailid,
                    Mobile = emp.mobile_no_CA,
                    Employee_image = emp.passport_photo,
                    Emp_Code = emp_Code,
                    Punch_Card_no = frm["Punch_card"],
                    Official_email_id = frm["office_email"],
                    Grade_id = emp_dt.Rows[0]["Grade"],
                    Department_id = emp_dt.Rows[0]["DepartmentId"],
                    Designation_id = emp_dt.Rows[0]["DesignationId"],
                    EPF_no = frm["EPF_No"],
                    // EPF_Join_date = emp.EPF_Join_date,
                    // PF_Leaving_date = emp.PF_Leaving_date,
                    //PF_leaving_Reagion = emp.PF_leaving_Reagion,
                    ESIC_no = frm["esic_no"],
                    //ESIC_join_date = emp.ESIC_join_date,
                    //ESIC_leaving_date = emp.ESIC_leaving_date,
                    //ESIC_leaving_Reagion = emp.ESIC_leaving_Reagion,
                    Employee_id = employeeId,
                    //document_type = emp.document_type,
                    Date_of_Joining = Convert.ToDateTime(frm["date_of_joining"]).ToString("dd-MMM-yyyy"),
                    Bank_Name = frm["Bank_Name"],
                    Branch = frm["Branch"],
                    Ifsc = frm["IFSC"],
                    Micr = frm["MICR"],
                    // iDOB =  Convert.ToDateTime(frm["date_of_joining"]).ToString("yyyyMMdd"),
                    Account_no = frm["Account_No"],
                    basic_salary = frm["Gross"],
                    income_heads = data["incomes"],
                    deduction_heads = data["deductions"],
                    College_Name = My.get_short_school_name(),

                    ActiveStatus = "Active",
                    Password = frm["Password"],

                    Emp_Device_Id = emp_Code
                    //employee_type = emp.employee_type,
                    // Qualification = emp.spouse_qualification,

                }) ;


                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Joining Process Completed Successfully",
                    error = false,
                    EmpId = employeeId,
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }


            if (method_id.equalIgnoreCase("editemployee"))
            {
                return edit_employee_details(data, form);
            }
            if (method_id.equalIgnoreCase("duty-mapping"))
            {
                var data_list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["data_list"].ToString());
                foreach (var v in data_list)
                {
                    if (v["DutyHourId"] == null)
                    {
                    }
                    else if (v["DutyHourId"].ToString() == "True")
                    {
                        PayrollMy.InsertOrUpdate("HR_Duty_Hour_Maping", new { DesignationId = v["Designation_id"], DutyHourId = data["Duty_Hour"] }, "DesignationId,DutyHourId");
                    }
                    else if (v["DutyHourId"].ToString() == "False")
                    {
                        PayrollMy.execute($"delete from HR_Duty_Hour_Maping where DesignationId = '{v["Designation_id"]}' and DutyHourId = '{data["Duty_Hour"]}'");
                    }
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Duty hour mapped successfully",
                    error = false,
                    data = "",
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }

            if (method_id.equalIgnoreCase("duty-roster"))
            {
                var data_list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["data_list"].ToString());
                foreach (var v in data_list)
                {
                    if (!v.ContainsKey("IsSelected"))
                    {
                    }
                    else if (v["IsSelected"].ToString() == "True")
                    {
                        PayrollMy.InsertOrUpdate("HR_Duty_Roster", new
                        {
                            EmployeeId = v["Employee_id"],
                            Shift_Id = data["Shift"],
                            RotationId = data["RotationId"],
                            DutyIncharge = data["DutyIncharge"],
                            WorkLocation = v["WorkLocation"]
                        }, "EmployeeId,RotationId");

                    }
                    else if (v["IsSelected"].ToString() == "False")
                    {
                        if (v["RotationId"] != null)
                        {
                            PayrollMy.execute($"delete from HR_Duty_Roster where EmployeeId = '{v["Employee_id"]}' and RotationId = '{data["RotationId"]}'");
                        }
                    }
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Duty Roster created successfully",
                    error = false,
                    data = "",
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }

            if (method_id.equalIgnoreCase("duty-roster-weekoff"))
            {
                var data_list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["data_list"].ToString());
                var start_date = Convert.ToDateTime($"{data["Year"]}-{data["Month"]}-01");
                var end_date = start_date.AddMonths(1).AddDays(-1);
                foreach (var v in data_list)
                {
                    var mt = PayrollMy.Table($"HR_Roster_Wise_Weekoff_setting", $"Idate like '{data["Year"]}{data["Month"]}%' and EmployeeID='{v["EmployeeId"]}'");
                    for (var d = start_date; d <= end_date;)
                    {
                        var idate = d.ToString("yyyyMMdd");
                        var drs = mt.dt.Select($"Idate='{idate}'");
                        var staus = v[d.ToString($" {d.Day}")].ToString();
                        if (staus == "True")
                        {
                            if (drs.Length == 0)
                            {
                                var dr = mt.NewRow();
                                // dr["RosterId"] = data["RotationId"];
                                dr["EmployeeID"] = v["EmployeeId"];
                                dr["Date"] = d.ToString("yyyy-MM-dd");
                                dr["Idate"] = idate;
                                mt.Rows.Add(dr);
                            }
                        }
                        if (staus != "1")
                        {
                            if (drs.Length > 0)
                            {
                                drs[0].Delete();
                            }
                        }
                        d = d.AddDays(1);
                    }
                    mt.Update();
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Weekoff setting successfully Updated",
                    error = false,
                    data = "",
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }

            if (method_id.equalIgnoreCase("update-duty-roster"))
            {
                var data_list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["data_list"].ToString());
                var rt = PayrollMy.dataTable($"select * from HR_Rotation_Details where RotationId='{data["RotationId"]}'");
                var start_date = Convert.ToDateTime(rt.Rows[0]["Start_Date"].ToString());
                var end_date = Convert.ToDateTime(rt.Rows[0]["End_Date"].ToString());
                foreach (var v in data_list)
                {
                    var mt = PayrollMy.Table($"HR_Date_Wise_Shift_Customized", $"Idate like '{data["Year"]}{data["Month"]}%' and Employee_Id='{v["EmployeeId"]}'");
                    for (var d = start_date; d <= end_date;)
                    {
                        var idate = d.ToString("yyyyMMdd");
                        var drs = mt.dt.Select($"Idate='{idate}'");
                        var curent_shift = v["Shift_Id"].ToString();
                        var staus = v[$"{d.ToString("d-MMM")}"].ToString();
                        if (curent_shift != staus && staus != "WOFF")
                        {
                            if (drs.Length == 0)
                            {
                                var dr = mt.NewRow();
                                dr["Employee_Id"] = v["EmployeeId"];
                                dr["Shift_Id"] = staus;
                                dr["Idate"] = idate;
                                mt.Rows.Add(dr);
                            }
                            else if (drs[0]["Shift_Id"].ToString() != staus)
                            {
                                drs[0]["Shift_Id"] = staus;
                            }
                        }
                        else if (drs.Length > 0)
                        {
                            drs[0].Delete();
                        }

                        d = d.AddDays(1);
                    }
                    mt.Update();
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Duty Roster Updated Successfully",
                    error = false,
                    data = "",
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }

            if (method_id.equalIgnoreCase("gcd"))
            {
                var dt = PayrollMy.dataTable(data["qry"].ToString());
                var ct = new DataTable();
                ct.Columns.Add("COLUMN_NAME");
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.ToLower() == "id")
                    {
                        continue;
                    }
                    ct.Rows.Add(dc.ColumnName);
                }
                return Json(ct.toJsonString(), JsonRequestBehavior.AllowGet);
            }
            if (method_id.equalIgnoreCase("myprofile"))
            {

                PayrollMy.Update("HR_Employee_Master", new
                {
                    Employee_Name = data["name"],
                    Mobile = data["mobile"],
                    Employee_image = data["image"],
                    Marital_Status = data["marital_status"],
                    Address = data["address"],
                    Email = data["email"],
                }, $"Employee_Id='{User.Identity.Name}'");
                PayrollMy.Update("HR_UserProfile", new
                {
                    Name = data["name"],
                    Mobile = data["mobile"],
                    ProfileImage = data["image"],
                }, $"UserId='{User.Identity.Name}'");
                Session["UserName"] = data["name"].ToString();
                Session["UserProfileImage"] = data["image"].ToString();
                if (PayrollMy.isHospital)
                {
                    PayrollMy.Update("HMS_Employee_Details", new
                    {
                        Name = data["name"],
                        Mobile_no = data["mobile"],
                        Address = data["address"],
                        Email_id = data["email"],
                    }, $"Employee_Id='{User.Identity.Name}'");
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Profile Update",
                    error = false,
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id.equalIgnoreCase("change-password"))
            {
                var error = false;
                var message = "Password Changed Successfully";
                if (data["con_pwd"].ToString() == "")
                {
                    error = true;
                    message = "Enter Valid New Password";
                }
                if (!error)
                {
                    var dt = PayrollMy.dataTable($"select * from HR_UserProfile where UserId='{User.Identity.Name}'");
                    if (dt.Rows.Count == 0)
                    {
                        error = true;
                        message = "Unable to change your password";

                    }
                    else
                    {
                        string UserId = dt.Rows[0]["UserId"].ToString();
                        if (dt.Rows[0]["Password"].ToString() == data["cr_pwd"].ToString())
                        {
                            PayrollMy.Update("HR_UserProfile", new
                            {
                                Password = data["con_pwd"]
                            }, $"UserId='{User.Identity.Name}'");


                            PayrollMy.execute("update HR_Employee_Master set Password='" + data["con_pwd"] + "' where Emp_Code ='" + UserId + "'");

                            PayrollMy.execute("update user_details set password='" + data["con_pwd"] + "' where user_id ='" + UserId + "'");


                        }
                        else
                        {
                            error = true;
                            message = "Invalid Current Password";

                        }

                    }
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = message,
                    error = error,
                    user = User.Identity.Name,
                });
                return Json(resp, JsonRequestBehavior.AllowGet);

            }
            if (method_id.equalIgnoreCase("mpc"))
            {
                if (data["pageId"].ToString() == "new")
                {
                    data["pageId"] = PayrollMy.AutoId("MPC", "PCD", "000");
                    data["Status"] = 1;
                }
                PayrollMy.InsertOrUpdate("HR_MasterPageConfig", data, compareColumns: "pageId");
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Page Configured successfully",
                    error = false,
                    pageId = data["pageId"],
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id.equalIgnoreCase("action_form"))
            {
                var mpc = getMPCById(data["pageId"].ToString());
                var Id = data["ID"];
                ActionList act = null;
                foreach (var a in mpc.action.actionList)
                {
                    if (a.action_id == data["action_id"].ToString())
                    {
                        act = a;
                        break;
                    }
                }
                if (act.type == "CustomForm")
                {
                    foreach (var p in act.act_pcd)
                    {
                        if (p.isRequired)
                        {
                            if (data[p.columnName].ToString() == "")
                            {
                                resp = (new JavaScriptSerializer()).Serialize(new
                                {
                                    message = $"{p.displayText} is Required.",
                                    error = true
                                });
                                return Json(resp, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                if (data["action_id"].ToString() == "111")
                {
                    var mt = PayrollMy.Table("HR_LeaveRequest", $"id='{Id}'");
                    if (mt.Rows.Count > 0)
                    {
                        if (mt.Rows[0]["Status"].ToString() == "Canceled")
                        {
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Your Leave Request was already Canceled",
                                error = true
                            });
                            return Json(resp, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            mt.Rows[0]["Remarks"] = data["col_0"];
                            mt.Rows[0]["Status"] = "Canceled";
                            mt.Update();
                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Leave Request Canceled Successfully",
                        error = false,
                        isUpdate = false,
                        data = PayrollMy.dataTable($"select  Emp_Code,lt.LeaveName,LeaveCode,(select Employee_Name from HR_Employee_Master where employee_id=lr.RequestSentTo) RequestTo,(select Employee_Name from HR_Employee_Master where employee_id=lr.ForwardedTo) RequestForwadedTo, lr.*  from HR_LeaveRequest lr join  HR_Employee_Master on HR_Employee_Master.Employee_id=lr.EmployeeId join HR_LeaveTypes lt on lt.LeaveId=lr.LeaveTypeId where lr.EmployeeId ='{User.Identity.Name}' order by lr.id desc ").toJsonObject(),
                    });
                }
                else
                {
                    if (data["action_id"].ToString() != "")
                    {
                        var sqt = PayrollMy.dataTable($"select * from HR_SQL_API where Api_Code='{data["action_id"].ToString()}'");
                        if (sqt.Rows.Count > 0)
                        {
                            try
                            {
                                var dc = processDataQuery(sqt.Rows[0]["Api_Query"].ToString(), data, "");
                                var ds = PayrollMy.dataSet(dc);
                                var lst = new List<object>();
                                object _data = null;
                                if (act.act_resp_type == "reload")
                                {
                                    var qry = $"select * from {mpc.tableName}";
                                    if (mpc.isCustomisedGrid && mpc.dataQuery != "")
                                    {
                                        qry = processDataQuery(mpc.dataQuery, data);
                                    }
                                    DataTable dt;
                                    try
                                    {
                                        dt = PayrollMy.dataTable(qry);
                                    }
                                    catch
                                    {
                                        dt = PayrollMy.dataTable($"select top 10 * from {mpc.tableName}");
                                    }
                                    _data = dt.toJsonObject();
                                }
                                else
                                {
                                    foreach (DataTable dt in ds.Tables)
                                    {
                                        lst.Add(dt.toJsonObject());
                                    }
                                    _data = lst;

                                }


                                resp = (new JavaScriptSerializer()).Serialize(new
                                {
                                    message = sqt.Rows[0]["SuccessMessage"].ToString(),
                                    error = false,
                                    data_action = act.act_resp_type,
                                    data = _data,
                                });
                            }
                            catch
                            {

                            }
                        }


                    }
                }

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id.equalIgnoreCase("page"))
            {

                bool edit = form["HdId"] != "";
                bool error = false;

                var res = new Dictionary<string, object>();
                var pageId = form.Get("frm-page-Id");
                var dict = new Dictionary<string, object>();
                var mpc = getMPCById(pageId);
                foreach (var p in mpc.pcd)
                {
                    if (p.isField)
                    {
                        if (p.isRequired && form[p.columnName] == "")
                        {
                            error = true;
                            res["message"] = $"{p.displayText} is Required";
                            break;
                        }
                        else if (p.isRequired && form.GetValues(p.columnName) == null)
                        {
                            error = true;
                            res["message"] = $"{p.displayText} is Required";
                            break;
                        }
                        else if (p.fieldType == "DatePicker")
                        {
                            try
                            {
                                dict[p.columnName] = Convert.ToDateTime(form[p.columnName]).ToString("dd-MMM-yyyy");
                            }
                            catch
                            {

                            }
                        }
                        else if (p.fieldType == "CheckBox")
                        {
                            dict[p.columnName] = form[p.columnName];
                            var ss = form[p.columnName];
                        }
                        else if (p.fieldType == "CheckBox-List")
                        {
                            string[] chk_selected = form.GetValues(p.columnName);
                            dict[p.columnName] = String.Join(",", chk_selected);
                        }
                        else if (p.fieldType == "TimePicker")
                        {
                            try
                            {
                                dict[p.columnName] = Convert.ToDateTime(form[p.columnName]).ToString("hh:mm tt");
                            }
                            catch
                            {

                            }
                        }
                        else
                            dict[p.columnName] = form[p.columnName];
                    }
                }
                if (!error)
                {
                    foreach (var p in mpc.pcd)
                    {
                        if (!p.isField)
                        {
                            if (p.fieldType == "Not a Field")
                            {

                            }
                            else if (p.fieldType == "AutoId")
                            {
                                if (!edit)
                                    dict[p.columnName] = PayrollMy.AutoId(p.columnName, format: p.requiredData);
                            }
                            else if (p.fieldType == "CurrentDate")
                            {
                                if (!edit)
                                    dict[p.columnName] = PayrollMy.Now.ToString("dd-MMM-yyyy");
                            }
                            else if (p.fieldType == "CurrentIdate")
                            {
                                if (!edit)
                                    dict[p.columnName] = PayrollMy.Now.ToString("yyyyMMdd");
                            }
                            else if (p.fieldType == "CurrentTime")
                            {
                                if (!edit)
                                    dict[p.columnName] = PayrollMy.Now.ToString(" hh:mm:ss tt");
                            }
                            else if (p.fieldType == "Current-ITime")
                            {
                                if (!edit)
                                    dict[p.columnName] = PayrollMy.Now.ToString("HHmmss");
                            }
                            else if (p.fieldType == "Create Date")
                            {
                                if (!edit)
                                    dict[p.columnName] = PayrollMy.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                            }
                            else if (p.fieldType == "Modified Date")
                            {
                                if (edit)
                                    dict[p.columnName] = PayrollMy.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                            }
                            else if (p.fieldType == "UserId")
                            {
                                dict[p.columnName] = User.Identity.Name;
                            }
                            else if (p.fieldType == "DatePickerIdate")
                            {
                                try
                                {
                                    dict[p.columnName] = Convert.ToDateTime(form[p.requiredData]).ToString("yyyyMMdd");
                                }
                                catch
                                {

                                }
                            }
                            else if (p.fieldType == "TimePicker-Itime")
                            {
                                try
                                {
                                    dict[p.columnName] = Convert.ToDateTime(form[p.requiredData]).ToString("HHmm");
                                }
                                catch
                                {

                                }
                            }

                        }
                    }
                    if (mpc.methodId == "0" || mpc.methodId == "")
                    {
                        try
                        {

                            if (edit)
                            {
                                if (mpc.action.duplicate_filter != null && mpc.action.duplicate_filter.Trim() != "")
                                {
                                    var d_qry = $"select * from {mpc.tableName} where  Id!='{form["HdId"]}'";
                                    foreach (var a in mpc.action.duplicate_filter.Trim().Split(','))
                                    {
                                        var d = a.Trim();
                                        if (dict.ContainsKey(d))
                                        {
                                            d_qry += $" and {d}='{dict[d]}'";
                                        }
                                    }
                                    if (PayrollMy.IsDataExist(d_qry))
                                    {
                                        res["message"] = "Duplicate Entry";
                                        error = true;
                                    }
                                }
                                if (!error)
                                {
                                    var qry = mpc.dataQuery.Trim() == "" ? $"select top 1 * from {mpc.tableName} where Id='" + form["HdId"] + "'" : " select * from (" + mpc.dataQuery + ") trnm where  Id='" + form["HdId"] + "'";
                                    PayrollMy.Update(mpc.tableName, dict, "Id='" + form["HdId"] + "'");
                                    res["data"] = PayrollMy.dataTable(qry).toJsonObject();
                                    res["message"] = "Data Updated Successfully";
                                    res["isUpdate"] = true;
                                    error = false;
                                }

                            }
                            else
                            {
                                //checking duplicate entry
                                if (mpc.action.duplicate_filter != null && mpc.action.duplicate_filter.Trim() != "")
                                {
                                    var d_qry = $"select * from {mpc.tableName} where ";
                                    int ib = 0;
                                    foreach (var a in mpc.action.duplicate_filter.Trim().Split(','))
                                    {
                                        var d = a.Trim();
                                        if (dict.ContainsKey(d))
                                        {
                                            if (ib > 0)
                                            {
                                                d_qry += " and  ";
                                            }
                                            d_qry += $"{d}='{dict[d]}'";
                                            ib++;
                                        }
                                    }
                                    if (PayrollMy.IsDataExist(d_qry))
                                    {
                                        res["message"] = "Duplicate Entry";
                                        error = true;
                                    }
                                }
                                if (!error)
                                {
                                    PayrollMy.Insert(mpc.tableName, dict);
                                    var qry = mpc.dataQuery.Trim() == "" ? $"select top 1 * from {mpc.tableName} order by id desc" : "select top 1 * from (" + mpc.dataQuery + ") trmn  order by id desc";
                                    res["data"] = PayrollMy.dataTable(qry).toJsonObject();
                                    res["message"] = "Data Added Successfully";
                                    res["isUpdate"] = false;
                                    error = false;
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            res["message"] = "Some thing goes wrong";
                            error = true;
                        }
                        res["error"] = error;
                    }
                    else
                    {
                        var code = new CustomMethod(form, dict, edit, mpc, User.Identity.Name);
                        res = code.Execude(mpc.methodId);
                    }
                    //if (!error)
                    //{
                    //    var table = form["DataQuery"];
                    //    if (table.StartsWith("c-"))
                    //    {
                    //        var dt1 = PayrollMy.dataTable($"select * from MasterPageDetails where id='{table.Substring(2)}'");
                    //        if (dt1.Rows.Count == 0)
                    //        {

                    //        }
                    //        else
                    //        {
                    //            table = dt1.Rows[0]["TableName"].ToString();
                    //            if (dt1.Rows[0]["DataQuery"].ToString() != "")
                    //            {
                    //                table = $"({dt1.Rows[0]["DataQuery"].ToString()}) t";
                    //            }
                    //        }
                    //    }
                    //    var dt = PayrollMy.dataTable($"select top 10 * from {table} order by id desc");
                    //    data = dt.toJsonObject();
                    //}


                }
                else
                {
                    res["error"] = error;
                }

                //System.Threading.Thread.Sleep(2000);
                return Json((new JavaScriptSerializer()).Serialize(res), JsonRequestBehavior.AllowGet);
            }
            return Json((new JavaScriptSerializer()).Serialize(data), JsonRequestBehavior.AllowGet);
        }

        private JsonResult edit_employee_details(Dictionary<string, object> data, FormCollection form)
        {
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Unable get employee details",
                error = true,
            });
            var emp_dt = PayrollMy.dataTable($"select * from HR_Employee_Online_Apply  where Apply_id='{data["applyId"]}' ");
            if (emp_dt.Rows.Count == 0)
            {
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Unable get employee details",
                    error = true,
                });
            }

            var emp = JsonConvert.DeserializeObject<Employee_Apply>(data["formdata"].ToString());
            var frm = JsonConvert.DeserializeObject<Dictionary<string, object>>(data["formdata"].ToString());
            var documents = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["documents"].ToString());
            var incomes = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["incomes"].ToString());
            var deductions = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["deductions"].ToString());
            //Employee_Code
            //chk_emp
            var emp_Code = frm["Employee_Code"];
            var employeeId = frm["employee_id"].ToString();

            #region HR_Employee_Online_Apply 

            PayrollMy.Update("HR_Employee_Online_Apply", new
            {
                Salutation = emp.Salutation,
                First_Name = emp.First_Name,
                Middle_Name = emp.Middle_Name,
                Last_Name = emp.Last_Name,
                Emailid = emp.Emailid,
                Date_birthday = emp.Date_birthday,
                Gender = emp.Gender,
                Place_Of_Birth = emp.Place_Of_Birth,
                Birth_State = emp.Birth_State,
                Religion = emp.Religion,
                Nationality = emp.Nationality,
                Marital_Status = emp.Marital_Status,
                Address_CA = emp.Address_CA,
                City_CA = emp.City_CA,
                State_CA = emp.State_CA,
                Pincode_CA = emp.Pincode_CA,
                mobile_no_CA = emp.mobile_no_CA,
                Residence_telephone_no_CA = emp.Residence_telephone_no_CA,
                address_pa = emp.address_pa,
                city_pa = emp.city_pa,
                state_pa = emp.state_pa,
                pin_pa = emp.pin_pa,
                chiled_name1 = emp.chiled_name1,
                chiled_gender1 = emp.chiled_gender1,
                chiled_age1 = emp.chiled_age1,
                chiled_name2 = emp.chiled_name2,
                chiled_gender2 = emp.chiled_gender2,
                chiled_age2 = emp.chiled_age2,
                chiled_name3 = emp.chiled_name3,
                chiled_gender3 = emp.chiled_gender3,
                chiled_age3 = emp.chiled_age3,
                fathername = emp.fathername,
                father_occupation = emp.father_occupation,
                mother_name = emp.mother_name,
                mother_occupation = emp.mother_occupation,
                Spouse_name = emp.Spouse_name,
                Spouses_job_is_transferable = emp.Spouses_job_is_transferable,
                spouse_qualification = emp.spouse_qualification,
                spouse_profession = emp.spouse_profession,
                spouse_organization = emp.spouse_organization,
                spouse_designation = emp.spouse_designation,
                completed_years = emp.completed_years,
                teaching_years = emp.teaching_years,
                Administration_year = emp.Administration_year,
                any_other = emp.any_other,
                Current_name_of_instituation = emp.Current_name_of_instituation,
                instituation_address = emp.instituation_address,
                Contact_Numbe_instituation = emp.Contact_Numbe_instituation,
                Designation_work = emp.Designation_work,
                //joining_date = emp.joining_date,
               
                place_of_posting = emp.place_of_posting,
                Present_Salary = emp.Present_Salary,
                Basic_Salary_Present = emp.Basic_Salary_Present,
                Allowance_Present = emp.Allowance_Present,
                Other_Benefits_Present = emp.Other_Benefits_Present,
                Under_Service_Bond = emp.Under_Service_Bond,
                years_service_bond = emp.years_service_bond,
                Expected_Salary = emp.Expected_Salary,
                English_read = emp.English_read.ToLower() == "true" ? "Yes" : "No",
                English_write = emp.English_write.ToLower() == "true" ? "Yes" : "No",
                English_Speak = emp.English_Speak.ToLower() == "true" ? "Yes" : "No",
                Hindi_read = emp.Hindi_read.ToLower() == "true" ? "Yes" : "No",
                Hindi_write = emp.Hindi_write.ToLower() == "true" ? "Yes" : "No",
                Hindi_speak = emp.Hindi_speak.ToLower() == "true" ? "Yes" : "No",
                Bangla_read = emp.Bangla_read.ToLower() == "true" ? "Yes" : "No",
                Bangla_write = emp.Bangla_write.ToLower() == "true" ? "Yes" : "No",
                Bangla_speak = emp.Bangla_speak.ToLower() == "true" ? "Yes" : "No",
                Other_Language = emp.Other_Language,
                Proficiency_In_Computer = emp.Proficiency_In_Computer,
                passport_photo = emp.passport_photo,
                Signature = emp.Signature,
                Emp_code = emp_Code,
                Verification_Status = "Joined",
                CurrentStatus = "Joined",
              
                JoiningDate = Convert.ToDateTime(frm["date_of_joining"]).ToString("dd-MMM-yyyy"),
                district_ca = frm["district_ca"],
                ps_ca = frm["ps_ca"],
                district_pa = frm["district_pa"],
                ps_pa = frm["ps_pa"],
                spouse_mobile_no = frm["spouse_mobile_no"],


                //Gross = frm["Gross"],
                //Deduction = frm["Deduction"],
                //Netsalary = frm["Net_Salary"],
                //Emp_contribution = frm["Employeer_Contribution"],
                //CTC_year = frm["CTC_year"],
                //CTC_month = frm["CTC_month"],



            }, $"Apply_id='{data["applyId"]}'");

            #endregion HR_Employee_Online_Apply

            var userType = PayrollMy.data($"select name from HR_Designation_Master where Designation_id='{frm["Designation"]}'");
            var ut = PayrollMy.dataTable($"select * from HR_UserProfile where UserId='{employeeId}'");
            var hms_user_id = employeeId;
            if (ut.Rows.Count > 0)
            {
                hms_user_id = ut.Rows[0]["HMS_UserId"].ToString();
            }
            PayrollMy.InsertOrUpdate("HR_UserProfile", new
            {
                Mobile = emp.mobile_no_CA,
                UserType = userType,
                EmployeeCode = emp_Code,
                Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                ProfileImage = emp.passport_photo,
                UserTypeId = frm["Designation"],
                UserId = employeeId,
                HMS_UserId = hms_user_id,
            }, "UserId");
            PayrollMy.InsertOrUpdate("user_details", new
            {
                name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                mobile = emp.mobile_no_CA,
                user_id = emp_Code,
                firm = My.get_firm_id(),
                User_Type = userType,
                ProfilePhoto = emp.passport_photo,
                Signature = emp.Signature,
                email = frm["Emailid"],

            }, "user_id");


            PayrollMy.InsertOrUpdate("PRL_Employee_Master", new
            {
                Employee_Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                Gender = emp.Gender,
                Date_of_birth = emp.Date_birthday,
                Blood_group = frm["BloodGroup"],
                Religion = emp.Religion,
                Marital_Status = emp.Marital_Status,
                Father_Name = emp.fathername,
                Pan = frm["PAN"],
                Address = emp.Address_CA,
                City = emp.City_CA,
                Pincode = emp.Pincode_CA,
                State = emp.State_CA,

                //State_code = emp.State_code,
                Email = emp.Emailid,
                Mobile = emp.mobile_no_CA,
                Employee_image = emp.passport_photo,
                Emp_Code = emp_Code,
                Punch_Card_no = frm["Punch_card"],
                Official_email_id = frm["office_email"],
                Grade_id = emp_dt.Rows[0]["Grade"],
                Department_id = emp_dt.Rows[0]["DepartmentId"],
                Designation_id = emp_dt.Rows[0]["DesignationId"],
                EPF_no = frm["EPF_No"],
                ESIC_no = frm["esic_no"],
                //Employee_id = employeeId,
                Date_of_Joining = Convert.ToDateTime(frm["date_of_joining"]).ToString("dd-MMM-yyyy"),
             
                Bank_Name = frm["Bank_Name"],
                Branch = frm["Branch"],
                Ifsc = frm["IFSC"],
                Micr = frm["MICR"],
                iDOB = Convert.ToDateTime(emp.Date_birthday).ToString("yyyyMMdd"),
            }, "Emp_Code");

            try
            {
                menuPermission.update_menu_permission_for_user(emp_Code.ToString(), userType);

            }
            catch (Exception ex)
            {
            }

            foreach (var doc in documents)
            {
                PayrollMy.Insert("HR_Employee_Documents", new
                {
                    EmployeeId = employeeId,
                    Doc_Name = doc["Doc_Name"],
                    file_path = doc["file"],
                });
            }
            PayrollMy.execute($"delete from HR_Emp_Salary_Structure_IncomeHead where EmployeeId='{employeeId}'");
            foreach (var inc in incomes)
            {
                PayrollMy.Insert("HR_Emp_Salary_Structure_IncomeHead", new
                {
                    EmployeeId = employeeId,
                    IncomeHeadId = inc["IncomeHeadId"],
                    IncomeValue = inc["inc"],
                });
            }

            PayrollMy.Update("HR_Employee_Master", new
            {
                Employee_Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}",
                Gender = emp.Gender,
                Date_of_birth = emp.Date_birthday,

                Blood_group = frm["BloodGroup"],
                Religion = emp.Religion,
                Marital_Status = emp.Marital_Status,
                Father_Name = emp.fathername,
                Pan = frm["PAN"],
                Address = emp.Address_CA,
                City = emp.City_CA,
                Pincode = emp.Pincode_CA,
                State = emp.State_CA,
                //State_code = emp.State_code,
                Email = emp.Emailid,
                Mobile = emp.mobile_no_CA,
                Employee_image = emp.passport_photo,
                Emp_Code = emp_Code,
                Punch_Card_no = frm["Punch_card"],
                Official_email_id = frm["office_email"],
                Grade_id = frm["Grade"],
                Department_id = frm["Department"],
                Designation_id = frm["Designation"],
                EPF_no = frm["EPF_No"],
                // EPF_Join_date = emp.EPF_Join_date,
                // PF_Leaving_date = emp.PF_Leaving_date,
                //PF_leaving_Reagion = emp.PF_leaving_Reagion,
                ESIC_no = frm["esic_no"],
                //ESIC_join_date = emp.ESIC_join_date,
                //ESIC_leaving_date = emp.ESIC_leaving_date,
                //ESIC_leaving_Reagion = emp.ESIC_leaving_Reagion,
                //Employee_id = employeeId,
                //document_type = emp.document_type,
                //Date_of_Joining = Convert.ToDateTime(frm["date_of_joining"]).ToString("dd-MMM-yyyy"),
                Bank_Name = frm["Bank_Name"],
                Branch = frm["Branch"],
                Ifsc = frm["IFSC"],
                Micr = frm["MICR"],
                // iDOB =  Convert.ToDateTime(frm["date_of_joining"]).ToString("yyyyMMdd"),
                Account_no = frm["Account_No"],
                basic_salary = frm["Gross"],
                income_heads = data["incomes"],
                deduction_heads = data["deductions"],
                //employee_type = emp.employee_type,
                // Qualification = emp.spouse_qualification,
                
                Date_of_Joining = Convert.ToDateTime(frm["date_of_joining"]).ToString("dd-MMM-yyyy"),
            }, $"Employee_id = '{employeeId}'");
            if (PayrollMy.isHospital)
            {
                if (!PayrollMy.IsDataExist($"select id from HMS_user_details where   user_id = '{hms_user_id}' "))
                {
                    var login_id = PayrollMy.AutoId("HMS_UserId", "999", "00");
                    PayrollMy.Insert("HMS_user_details", new
                    {
                        name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}".Trim(),
                        mobile = emp.mobile_no_CA,
                        user_id = emp_Code,
                        password = 123,
                        status = "Active",
                        date = PayrollMy.Now,
                        firm = 1,
                        user_type = userType,
                        login_Id = login_id,
                    });

                    PayrollMy.Insert("HMS_Employee_Details", new
                    {
                        Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}".Trim(),
                        Employee_id = employeeId,
                        Dept_id = emp_dt.Rows[0]["DepartmentId"],
                        Initial_id = PayrollMy.ToInt(emp.Salutation, 3),
                        Address = emp.Address_CA,
                        Email_id = emp.Emailid,
                        Mobile_no = emp.mobile_no_CA,
                        Desg_id = emp_dt.Rows[0]["DesignationId"],
                        Type = userType,
                        User_Type_id = emp_dt.Rows[0]["DesignationId"],
                        login_Id = login_id,
                        unique_entry_id = login_id,
                        Created_by = "1",
                        Istatus = "1",
                        is_login_required = "1",
                        Module_id = "1",
                        OPD_Room_id = "1",
                        Created_date = PayrollMy.Now,
                        Created_idate = PayrollMy.Now.ToString("yyyyMMdd"),
                        Username = emp_Code,
                    });
                }
                else
                {
                    PayrollMy.Update("HMS_Employee_Details", new
                    {
                        Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}".Trim(),
                        Initial_id = PayrollMy.ToInt(frm["Salutation"], 3),
                        Address = emp.Address_CA,
                        Email_id = emp.Emailid,
                        Mobile_no = emp.mobile_no_CA,
                    }, $"Employee_id='{employeeId}'");

                    PayrollMy.Update("HMS_Employee_Details", new
                    {
                        Name = $"{emp.First_Name} {emp.Middle_Name} {emp.Last_Name}".Trim(),
                        Employee_id = employeeId,
                        Dept_id = emp_dt.Rows[0]["DepartmentId"],
                        Initial_id = PayrollMy.ToInt(emp.Salutation, 3),
                        Address = emp.Address_CA,
                        Email_id = emp.Emailid,
                        Mobile_no = emp.mobile_no_CA,
                        Desg_id = emp_dt.Rows[0]["DesignationId"],
                        Type = userType,
                        User_Type_id = emp_dt.Rows[0]["DesignationId"],
                        Username = emp_Code,
                    }, $"Employee_id = '{employeeId}'");
                }
            }


            resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Employee Detail Updated Successfully",
                error = false,
                EmpId = employeeId,
            });
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [Route("menu")]
        [Authorize]
        public ActionResult MenuMaster()
        {
            if (!isHR())
            {
                return Redirect("~/home");
            }

            return View();
        }

        [Route("{pageUrl}")]
        [Authorize]
        public ActionResult Index(String pageUrl)
        {


            if (isController(pageUrl))
            {
                var rv = new RouteValueDictionary();
                foreach (var k in Request.QueryString.AllKeys)
                {
                    rv[k] = Request.QueryString[k];
                }
                ViewBag.firm = getFirmDetails();
                return RedirectToAction("Home", pageUrl, rv);
            }
            else
            {
                ViewBag.firm = getFirmDetails();
                HR_MasterPageConfig mpc = getMPC(pageUrl);
                ViewBag.isHome = false;
                ViewBag.mpc = mpc;
                if (!isHR())
                {
                    var pages = new string[] { "PCD028", "PCD026", "PCD029", "PCD046", "PCD047", "PCD048" };
                    if (!pages.Contains(mpc.pageId))
                    {
                        return Redirect("~/home");
                    }

                }
                if (mpc.OtherData != null)
                {
                    if (mpc.OtherData.ContainsKey("Custom_View"))
                    {
                        if (!(mpc.OtherData["Custom_View"] == null || mpc.OtherData["Custom_View"].Trim() == ""))
                        {
                            return View(mpc.OtherData["Custom_View"]);
                        }
                    }
                }
            }

            return View();
        }

        private HR_MasterPageConfig getMPC(string url)
        {
            var dt = PayrollMy.dataTable($"select * from HR_MasterPageConfig where pageUrl='{url}'");
            ViewBag.pageNotFound = dt.Rows.Count == 0;
            if (dt.Rows.Count == 0)
            {
                if (Session["IsHR"] == null)
                {
                    Session["IsHR"] = PayrollMy.data($"select IsHr from HR_UserProfile where UserId='{User.Identity.Name}'");
                }
                if (Session["IsHR"].ToString() == "1")
                {
                    ViewBag.IsHR = "1";
                }
                else
                {
                    ViewBag.IsHR = "0";
                }
            }
            return getMPC(dt);
        }
        private HR_MasterPageConfig getMPC(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return new HR_MasterPageConfig();
            }
            var row = dt.Rows[0];
            var mpc = new HR_MasterPageConfig()
            {
                pageId = row["pageId"].ToString(),
                tableName = row["tableName"].ToString(),
                pageTitle = row["pageTitle"].ToString(),
                pageUrl = row["pageUrl"].ToString(),
                pageType = row["pageType"].ToString(),
                methodId = row["methodId"].ToString(),
                formType = row["formType"].ToString(),
                dataQuery = row["dataQuery"].ToString(),
                isCustomisedGrid = row["isCustomisedGrid"].ToString().ToBool(),
                pcd = JsonConvert.DeserializeObject<List<FieldConfigData>>(row["pcd"].ToString()),
                events = JsonConvert.DeserializeObject<List<Events>>(row["events"].ToString()),
                gcd = JsonConvert.DeserializeObject<List<GridConfigData>>(row["gcd"].ToString()),
                filters = JsonConvert.DeserializeObject<List<Filter>>(row["filters"].ToString()),
                action = JsonConvert.DeserializeObject<ActionModel>(row["action"].ToString()),
                OtherData = JsonConvert.DeserializeObject<Dictionary<string, string>>(row["OtherData"].ToString()),
            };
            var noinputsfield = new String[] { "AutoId", "CurrentDate", "CurrentIdate", "UserId", "CurrentTime","Not a Field",
            "DatePickerIdate","TimePicker-Itime","Current-ITime","Create Date","Modified Date"};
            foreach (var p in mpc.pcd)
            {
                if (p.columnName.ToUpper() == "ID")
                    p.isField = false;
                else
                    p.isField = !noinputsfield.Contains(p.fieldType);
            }
            Session[row["pageId"].ToString()] = mpc;
            return mpc;
        }
        private HR_MasterPageConfig getMPCById(string pageId)
        {
            if (Session[pageId] != null)
            {
                return Session[pageId] as HR_MasterPageConfig;
            }
            var dt = PayrollMy.dataTable($"select * from HR_MasterPageConfig where pageId='{pageId}'");

            return getMPC(dt);
        }

        private bool isControllerAction(String ctr, String id)
        {
            var controllerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && typeof(Controller).IsAssignableFrom(t));
            foreach (var type in controllerTypes)
            {
                if (type.Name.ToLower() == ctr.ToLower() + "controller")
                {
                    if (id == null)
                    {
                        return true;
                    }
                    var actions = new ReflectedControllerDescriptor(type).GetCanonicalActions();
                    foreach (var action in actions)
                    {

                        if (id.ToLower() == action.ActionName.ToLower())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool isController(String ctr)
        {
            var controllerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && typeof(Controller).IsAssignableFrom(t));
            foreach (var type in controllerTypes)
            {
                if (type.Name.ToLower() == ctr.ToLower() + "controller")
                {
                    return true;
                }
            }
            return false;
        }
    }
}