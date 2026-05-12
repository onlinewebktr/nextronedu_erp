using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace school_web
{
    public class MethodResponce
    {
        public bool error = false;
        public string message = "";
        public object data;
    }
    public class CustomMethod
    {
        private FormCollection form;
        private Dictionary<string, object> dict;
        private bool edit;
        private HR_MasterPageConfig mpc;
        private String userId;
        private Dictionary<string, object> res = new Dictionary<string, object>();
        public CustomMethod(FormCollection form, Dictionary<string, object> dict, bool edit, HR_MasterPageConfig mpc, string userId)
        {
            this.form = form;
            this.dict = dict;
            this.edit = edit;
            this.mpc = mpc;
            this.userId = userId;
            this.res = new Dictionary<string, object>();
            res["error"] = true;
        }


        public Dictionary<string, object> Execude(string method_id)
        {
            var res = new Dictionary<string, object>();
            res["error"] = true;
            switch (method_id.ToLower())
            {
                case "csholiday":
                    return csholiday();
                case "departments":
                    return departments();
                case "applyleave":
                    return applyleave();
                case "incomehead":
                    return incomehead();
                case "deduction-head":
                    return deduction_head();
                case "duty-hour":
                    return duty_hour();
                case "rotation-duration":
                    return rotation_duration();
                case "user-info":
                    return user_info();
                case "leave-allotment-setups":
                    return leave_allotment_setups();
                case "leave-allotment-individual":
                    return leave_allotment_individual();

                case "emp-code-change":
                    {
                        var emp_dt = My.dataTable($"select * from {mpc.tableName}  where id='{form["HdId"]}'");
                        if (emp_dt.Rows.Count == 1)
                        {
                            var dr = emp_dt.Rows[0];
                            var emp_id = dr["Employee_id"].ToString();
                            var old_emp_code = dr["Emp_Code"].ToString();
                            var Emp_Code = dict["Emp_Code"].ToString();//Emp_Code
                            if (PayrollMy.IsDataExist($"select * from {mpc.tableName}  where id!='{form["HdId"]}' and Emp_Code='{Emp_Code}'"))
                            {
                                res["error"] = true;
                                res["message"] = $"Employee Code already assigned to another employee";
                                return res;
                            }
                            //var qry = $"Update HR_UserProfile set EmployeeCode='{Emp_Code}' where UserId='{emp_id}'; ";
                            //qry += $"Update HR_Employee_Master set Emp_Code='{Emp_Code}' where Employee_id='{emp_id}';";
                            //qry += $"Update HR_Attendance_Log set Emp_Code='{Emp_Code}' where Employee_id='{emp_id}';";

                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "sp_Update_Employee_code";
                            cmd.Parameters.AddWithValue("@Old_employee_code", old_emp_code);
                            cmd.Parameters.AddWithValue("@New_employee_code", Emp_Code);
                            cmd.Parameters.AddWithValue("@updated_by", userId);
                            cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Status", "UpdateEmployeeCode");
                            if (UsesCode.InsertUpdateData_sp(cmd))
                            {

                                
                            }



                            //PayrollMy.execute(qry);





                            //try
                            //{
                            //                //connect device
                            //    //download fp_details
                            //    //download face_details

                            //    //upload fp_details
                            //    //upload face_details
                            //    //delete user 
                            //    var ip = My.data("select DeviceIP from HR_Attendance_Device");
                            //    if (Device.Connect(ip))
                            //    {

                            //        {
                            //            string userID = "123";
                            //            string name = "John Doe";
                            //            string cardNo = "12345";
                            //            string password = "1234";
                            //            int oldPrivilege = 0;
                            //            Device.objZkeeper.GetUserInfo(1,userID, out name, out password, out oldPrivilege, out cardNo)
                            //            bool getUserInfoSuccess = device.GetUserInfo(device.MachineNumber, old_emp_code, out name, out password, out oldPrivilege, out cardNo);



                            //            device.SSR_SetUserInfo(1, userID, name, password, 1, true);

                            //        }
                            //        Device.objZkeeper.EnableDevice(1, false);
                            //        Device.objZkeeper.ReadAllUserID(1);
                            //        Device.objZkeeper.ReadAllTemplate(1);//read all the users' fingerprint templates to the memory 
                            //        var strName = "";
                            //        var strPassword = "";
                            //        var iPrivilege = 0;
                            //        var bEnabled = true;
                            //        Device.objZkeeper.SSR_GetUserInfo(1, old_emp_code, out strName, out strPassword, out iPrivilege, out bEnabled);
                            //        var strCardno = "";
                            //        Device.objZkeeper.GetStrCardNumber(out strCardno);
                            //        if (strCardno.Equals("0"))
                            //        {
                            //            strCardno = "";
                            //        }
                            //        var lst = new List<dynamic>(); 
                            //        for (var idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                            //        {
                            //            var iFlag = 0;
                            //            var sFPTmpData = "";
                            //            var iFPTmpLength = 0;
                            //            if (Device.objZkeeper.GetUserTmpExStr(1, old_emp_code, idwFingerIndex, out iFlag, out sFPTmpData, out iFPTmpLength))//get the corresponding templates string and length from the memory
                            //            {
                            //                lst.Add(new
                            //                {
                            //                    idwFingerIndex = idwFingerIndex,
                            //                    iFlag = iFlag,
                            //                    sFPTmpData = sFPTmpData,
                            //                });
                            //            }
                            //        }
                            //        if(lst.Count>0)
                            //        {

                            //        }
                            //    }
                            //}
                            //catch
                            //{

                            //}
                            res["isUpdate"] = true;
                            res["error"] = false;
                            res["message"] = $"Employee Code Uodated Successfully";
                            res["data"] = My.dataTable($"select * from HR_Employee_Master where id='{form["HdId"]}'").toJsonObject();
                            return res;
                        }
                        res["error"] = true;
                        res["message"] = $"Unable to Update Employee Code for this employee";
                        return res;
                    }



            }
            return res;
        }

        private Dictionary<string, object> leave_allotment_individual()
        {  
            var lst = new List<dynamic>();
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("leave_"))
                {
                    lst.Add(new { Leave_Id = key.Replace("leave_", ""), Leave_Type = form["hd_" + key], No_of_Leave = form[key] });
                }
            }
            foreach (var ld in lst)
            {
                PayrollMy.InsertOrUpdate("HR_EmployeeLeaveAvailabilityDetails", new {
                    Session=PayrollMy.currentSession,
                    EmployeeId = form["Employee"],
                    LeaveTypeId = ld.Leave_Id,
                    No_of_Leave = ld.No_of_Leave,
                    IsIndividualLeaveSetuped = true,
                }, "EmployeeId,Session,LeaveTypeId"); 
            }
             
            
            res["isUpdate"] = false; 
            res["error"] = false;
            res["message"] = $"Employee Leave Allotment Updated successfully";
            res["data"] = PayrollMy.dataTable(mpc.dataQuery).toJsonObject();
            return res;
        }
        

        private Dictionary<string, object> leave_allotment_setups()
        { 
            var dt = new DataTable();
            var leaveDetails = "na";
            var lst = new List<dynamic>();
            foreach (var key in form.AllKeys)
            {
                if (key.StartsWith("leave_"))
                {
                    lst.Add(new { Leave_Id = key.Replace("leave_", ""), Leave_Type = form["hd_" + key], No_of_Leave = form[key] });
                }
            }
            leaveDetails = (new JavaScriptSerializer()).Serialize(lst);
            var sessionyear = PayrollMy.Now.AddMonths(-3);
            var session = $"{sessionyear.Year}-{sessionyear.AddYears(1).Year}";
            if (edit)
            {

                PayrollMy.Update("HR_DesignationWiseLeaveSetups", new { LeaveDetails = leaveDetails, }, $"id='{form["HdId"]}'");
                
                var ed = PayrollMy.dataTable($"select Employee_id from HR_Employee_Master where Designation_id='{form["HdDesignation"]}'");
                if(ed.Rows.Count>0)
                {
                    foreach(DataRow dr1 in ed.Rows)
                    {
                        var mt = PayrollMy.Table("HR_EmployeeLeaveAvailabilityDetails", $"EmployeeId='{dr1[0]}'");
                        if(mt.Rows.Count>0)
                        {
                            if(mt.Rows[0]["IsIndividualLeaveSetuped"].ToString()=="True")
                            {
                                continue;
                            }
                        }
                        foreach(var ld in lst)
                        {
                            var drs = mt.dt.Select($"LeaveTypeId='{ld.Leave_Id}'");
                            if (drs.Length == 0)
                            {
                                var dr = mt.NewRow();
                                dr["session"] = session;
                                dr["EmployeeId"] = dr1[0];
                                dr["LeaveTypeId"] = ld.Leave_Id;
                                dr["No_of_Leave"] = ld.No_of_Leave;
                                mt.Rows.Add(dr);
                            }
                            else
                            {

                                drs[0]["No_of_Leave"] = ld.No_of_Leave;
                            }
                        }
                        mt.Update(); 
                    }
                }
                 
                //update epmlouee wise details
            }
            else
            {
                if (dict["DesignationId"].ToString() == "0")
                {
                    if (dict["DepartmentId"].ToString() == "0")
                    {
                        dt = PayrollMy.dataTable("select * from HR_Designation_Master");
                    }
                    else
                    {
                        dt = PayrollMy.dataTable($"select * from HR_Designation_Master where DepartmentId='{dict["DepartmentId"]}'");
                    }
                }
                else
                { 
                    dt = PayrollMy.dataTable($"select * from HR_Designation_Master where Designation_id='{dict["DesignationId"]}'");
                }
                foreach (DataRow dr1 in dt.Rows)
                {
                    PayrollMy.InsertOrUpdate("HR_DesignationWiseLeaveSetups", new { DepartmentId = dr1["DepartmentId"], DesignationId = dr1["Designation_id"], LeaveDetails = leaveDetails, }, "DepartmentId,DesignationId");

                    var ed = PayrollMy.dataTable($"select Employee_id from HR_Employee_Master where Designation_id='{dr1["Designation_id"]}'");
                    if (ed.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in ed.Rows)
                        {
                            var mt = PayrollMy.Table("HR_EmployeeLeaveAvailabilityDetails", $"EmployeeId='{dr2[0]}'");
                            if (mt.Rows.Count > 0)
                            {
                                if (mt.Rows[0]["IsIndividualLeaveSetuped"].ToString() == "True")
                                {
                                    continue;
                                }
                            }
                            foreach (var ld in lst)
                            {
                                var drs = mt.dt.Select($"LeaveTypeId='{ld.Leave_Id}'");
                                if (drs.Length == 0)
                                {
                                    var dr = mt.NewRow();
                                    dr["session"] = session;
                                    dr["EmployeeId"] = dr2[0];
                                    dr["LeaveTypeId"] = ld.Leave_Id;
                                    dr["No_of_Leave"] = ld.No_of_Leave;
                                    mt.Rows.Add(dr);
                                }
                                else
                                {

                                    drs[0]["No_of_Leave"] = ld.No_of_Leave;
                                }
                            }
                            mt.Update();
                        }
                    }

                }
            }
            
            
            res["isUpdate"] = false; 
            res["error"] = false;
            res["message"] = $"Leave setups updated successfully";
            res["data"] = PayrollMy.dataTable($"select lvs.*,dpm.name department, dsm.name designation from HR_DesignationWiseLeaveSetups lvs left join HR_Department_Master dpm on dpm.department_id=lvs.DepartmentId left join HR_Designation_Master dsm on dsm.Designation_id=lvs.DesignationId").toJsonObject();
            return res;
        }

        private Dictionary<string, object> user_info()
        {
            if(edit)
            {
                var bf_data = PayrollMy.Table("HR_UserProfile", $"id ='{form["HdId"]}'");
                PayrollMy.Update(mpc.tableName, dict, where: $"id='{form["HdId"]}'");
                if(PayrollMy.isHospital)
                {
                    var dt = PayrollMy.Table("HR_UserProfile", $"id ='{form["HdId"]}'");
                    PayrollMy.Update("HMS_user_details", new {
                        user_id = dt.Rows[0]["HMS_UserId"],
                        password = dt.Rows[0]["Password"],
                    }, where: $"user_id='{bf_data.Rows[0]["HMS_UserId"]}'");
                }
                res["isUpdate"] = true;
                res["error"] = false;
                res["message"] = $"User detail updated successfully";
                res["data"] = PayrollMy.dataTable($"select * from HR_UserProfile where id='{form["HdId"]}'").toJsonObject();
            }
           
            return res;

        }

        private Dictionary<string, object> rotation_duration()
        {
            if (!edit)
            {
                if (PayrollMy.IsDataExist("HR_Rotation_Duration", $" Month='{dict["Month"]}' and Year='{dict["Year"]}' and DutyHourId='{form["DutyHourId"]}'"))
                {
                    res["message"] = "Rotation Duration Already Created for this Month";
                    return res;
                }
                var start_date = Convert.ToDateTime(form["Roration_1_From"]);
                var monthyear = start_date.ToString("MMMM yyyy");
                var last_date = start_date.AddMonths(1).AddDays(-1);
                var RotationHeadId = PayrollMy.AutoId("RotationHeadId");
                var Rotation_Details = "";
                for (int i = 1; i <= 20; i++)
                {
                    var from_date = Convert.ToDateTime(form[$"Roration_{i}_From"]);
                    var to_date = Convert.ToDateTime(form[$"Roration_{i}_To"]);
                    PayrollMy.Insert("HR_Rotation_Details", new
                    {
                        RotationHeadId = RotationHeadId,
                        RotationId = PayrollMy.AutoId("RotationId"),
                        MonthYear = monthyear,
                        Start_Date = form[$"Roration_{i}_From"],
                        End_Date = form[$"Roration_{i}_To"],
                        Description = $"{from_date.ToString("dd-MMM-yyyy")} to {to_date.ToString("dd-MMM-yyyy")}",
                        Start_IDate = from_date.ToString("yyyyMMdd"),
                        End_IDate = to_date.ToString("yyyyMMdd"),
                        Status = 1,
                    });
                    Rotation_Details += $"{from_date.ToString("dd-MMM-yyyy")} to {to_date.ToString("dd-MMM-yyyy")},";
                    if (to_date == last_date)
                    {
                        break;
                    }
                }
                PayrollMy.Insert("HR_Rotation_Duration", new
                {
                    Rotation_Month = monthyear,
                    DutyHourId = form["DutyHourId"],
                    Rotation_Details = Rotation_Details.TrimEnd(','),
                    RotationHeadId = RotationHeadId,
                    Year = dict["Year"],
                    Month = dict["Month"],
                });
                res["isUpdate"] = false;
                res["error"] = false;
                res["message"] = $"Rotation Duration created successfully";
                res["data"] = PayrollMy.dataTable("select top 1  rd.*,Duty_Hour_Name from HR_Rotation_Duration rd left join HR_Duty_Hours dr on rd.DutyHourId=dr.Duty_Hour_id order by rd.id desc").toJsonObject();
            }
            else
            {
                if (PayrollMy.IsDataExist("HR_Rotation_Duration", $" Month='{dict["Month"]}' and Year='{dict["Year"]}'  and DutyHourId='{form["DutyHourId"]}' and id != '{form["HdId"]}' "))
                {
                    res["message"] = "Rotation Duration Already Created for this Month";
                    return res;
                }
                var start_date = Convert.ToDateTime(form["Roration_1_From"]);
                var monthyear = start_date.ToString("MMMM yyyy");
                var last_date = start_date.AddMonths(1).AddDays(-1);
                var RotationHeadId = PayrollMy.data($"select RotationHeadId from HR_Rotation_Duration where id = '{form["HdId"]}'");  
                var Rotation_Details = "";
                var mt = PayrollMy.Table("HR_Rotation_Details", $"RotationHeadId='{RotationHeadId}'");
                int i = 1;
                for (i = 1; i <= 20; i++)
                {
                    var from_date = Convert.ToDateTime(form[$"Roration_{i}_From"]);
                    var to_date = Convert.ToDateTime(form[$"Roration_{i}_To"]);
                    if (mt.Rows.Count >= i)
                    {
                        //update
                        var dr = mt.Rows[i - 1];
                        dr["MonthYear"] = monthyear;
                        dr["Start_Date"] = form[$"Roration_{i}_From"];
                        dr["End_Date"] = form[$"Roration_{i}_To"];
                        dr["Description"] = $"{ from_date.ToString("dd-MMM-yyyy")} to {to_date.ToString("dd-MMM-yyyy")}";
                        dr["Start_IDate"] = from_date.ToString("yyyyMMdd");
                        dr["End_IDate"] = to_date.ToString("yyyyMMdd");
                        dr["Status"] = 1;
                    }
                    else
                    {  // insert
                        var dr = mt.NewRow();
                        dr["RotationHeadId"] = RotationHeadId;
                        dr["RotationId"] = PayrollMy.AutoId("RotationId");
                        dr["MonthYear"] = monthyear;
                        dr["Start_Date"] = form[$"Roration_{i}_From"];
                        dr["End_Date"] = form[$"Roration_{i}_To"];
                        dr["Description"] = $"{ from_date.ToString("dd-MMM-yyyy")} to {to_date.ToString("dd-MMM-yyyy")}";
                        dr["Start_IDate"] = from_date.ToString("yyyyMMdd");
                        dr["End_IDate"] = to_date.ToString("yyyyMMdd");
                        dr["Status"] = 1;
                        mt.Rows.Add(dr);
                    }

                    Rotation_Details += $"{from_date.ToString("dd-MMM-yyyy")} to {to_date.ToString("dd-MMM-yyyy")},";
                    if (to_date == last_date)
                    {
                        break;
                    }
                }
                while (mt.Rows.Count > i)
                {
                    mt.Rows[i].Delete();
                    i++;
                }
                mt.Update();
                PayrollMy.Update("HR_Rotation_Duration", new
                {
                    Rotation_Month = monthyear,
                    DutyHourId = form["DutyHourId"],
                    Rotation_Details = Rotation_Details.TrimEnd(','),
                    Year = dict["Year"],
                    Month = dict["Month"],
                }, $"id = '{form["HdId"]}'");
                res["isUpdate"] = true;
                res["error"] = false;
                res["message"] = $"Rotation Duration update successfully";
                res["data"] = PayrollMy.dataTable($"select  rd.*,Duty_Hour_Name from HR_Rotation_Duration rd left join HR_Duty_Hours dr on rd.DutyHourId=dr.Duty_Hour_id where rd.id='{form["HdId"]}'").toJsonObject();
            }
            return res;
        }

        private Dictionary<string, object> duty_hour()
        {
            var no_of_shift = dict["No_of_Shift"].ToInt();
            var cur_shift = 1;
            while (cur_shift <= no_of_shift)
            {
                if (form[$"Shift_{cur_shift}_Name"] == "")
                {
                    res["message"] = $"Shift {cur_shift} Name is required";
                    return res;
                }
                if (form[$"Shift_{cur_shift}_Intime"] == "")
                {
                    res["message"] = $"In Time for Shift {cur_shift} is required";
                    return res;
                }
                if (form[$"Shift_{cur_shift}_workingHour"] == "")
                {
                    res["message"] = $"Shift {cur_shift} Working Hour is required";
                    return res;
                }
                if (form[$"Shift_{cur_shift}_outtime"] == "")
                {
                    res["message"] = $"Out Time for Shift {cur_shift} is required";
                    return res;
                }
                cur_shift++;
            }
            cur_shift = 1;
            if (!edit)
            {

                var dutyHourId = PayrollMy.AutoId("DutyHourId", "D", "00");
                PayrollMy.Insert("HR_Duty_Hours", new
                {
                    Duty_Hour_Name = dict["Duty_Hour_Name"],
                    No_of_Shift = dict["No_of_Shift"],
                    Duty_Hour_id = dutyHourId,
                });
                var mt = PayrollMy.Table("HR_ShiftSetting", $"DutyHourId='{dutyHourId}'");
                while (cur_shift <= no_of_shift)
                {
                    var dr = mt.NewRow();
                    dr["DutyHourId"] = dutyHourId;
                    dr["ShiftName"] = form[$"Shift_{cur_shift}_Name"];
                    dr["In_Time"] = Convert.ToDateTime(form[$"Shift_{cur_shift}_Intime"]).ToString("HH:mm");
                    dr["StartITime"] = Convert.ToDateTime(form[$"Shift_{cur_shift}_Intime"]).ToString("HHmm");
                    dr["Out_Time"] = Convert.ToDateTime(form[$"Shift_{cur_shift}_outtime"]).ToString("HH:mm");
                    dr["Working_Hour"] = form[$"Shift_{cur_shift}_workingHour"];
                    dr["ShiftId"] = $"{dutyHourId}_{cur_shift}";
                    dr["Status"] = true;
                    dr["ShiftDesc"] = $"{form[$"Shift_{cur_shift}_Name"]} ({ Convert.ToDateTime(form[$"Shift_{cur_shift}_Intime"]).ToString("hh:mm tt")} to { Convert.ToDateTime(form[$"Shift_{cur_shift}_outtime"]).ToString("hh:mm tt")})";
                    cur_shift++;
                    mt.Rows.Add(dr);
                }
                while (cur_shift <= 10)
                {
                    var dr = mt.NewRow();
                    dr["DutyHourId"] = dutyHourId;
                    dr["ShiftName"] = "";
                    dr["In_Time"] = "";
                    dr["Out_Time"] = "";
                    dr["StartITime"] = "0";
                    dr["Working_Hour"] = "";
                    dr["ShiftId"] = $"{dutyHourId}_{cur_shift}";
                    dr["Status"] = false;
                    cur_shift++;
                    mt.Rows.Add(dr);
                }
                mt.Update();
                res["isUpdate"] = false;
                res["error"] = false;
                res["message"] = $"Duty Hour created successfully";
                res["data"] = PayrollMy.dataTable("select top 1 * from HR_Duty_Hours order by id desc").toJsonObject();
            }
            else
            {
                var dutyHourId = PayrollMy.data($"select Duty_Hour_id  from HR_Duty_Hours where Id='{form["HdId"]}'");
                PayrollMy.Update("HR_Duty_Hours", new
                {
                    Duty_Hour_Name = dict["Duty_Hour_Name"],
                    No_of_Shift = dict["No_of_Shift"],
                }, $"Id='{form["HdId"]}'");
                var mt = PayrollMy.Table("HR_ShiftSetting", $"DutyHourId='{dutyHourId}'");
                while (cur_shift <= no_of_shift)
                {
                    var drs = mt.dt.Select($"ShiftId='{dutyHourId}_{cur_shift}'");
                    var dr = drs.Length == 0 ? mt.NewRow() : drs[0];
                    dr["DutyHourId"] = dutyHourId;
                    dr["ShiftName"] = form[$"Shift_{cur_shift}_Name"];
                    dr["In_Time"] = Convert.ToDateTime(form[$"Shift_{cur_shift}_Intime"]).ToString("HH:mm");
                    dr["StartITime"] = Convert.ToDateTime(form[$"Shift_{cur_shift}_Intime"]).ToString("HHmm");
                    dr["Out_Time"] = Convert.ToDateTime(form[$"Shift_{cur_shift}_outtime"]).ToString("HH:mm");
                    dr["Working_Hour"] = form[$"Shift_{cur_shift}_workingHour"];
                    dr["ShiftId"] = $"{dutyHourId}_{cur_shift}";
                    dr["Status"] = true;
                    dr["ShiftDesc"] = $"{form[$"Shift_{cur_shift}_Name"]} ({ Convert.ToDateTime(form[$"Shift_{cur_shift}_Intime"]).ToString("hh:mm tt")} to { Convert.ToDateTime(form[$"Shift_{cur_shift}_outtime"]).ToString("hh:mm tt")})";
                    cur_shift++;
                    if (drs.Length == 0)
                    {
                        mt.Rows.Add(dr);
                    }
                }
                while (cur_shift <= 10)
                {
                    var drs = mt.dt.Select($"ShiftId='{dutyHourId}_{cur_shift}'");
                    var dr = drs.Length == 0 ? mt.NewRow() : drs[0];
                    dr["DutyHourId"] = dutyHourId;
                    dr["ShiftName"] = "";
                    dr["In_Time"] = "";
                    dr["Out_Time"] = "";
                    dr["StartITime"] = "0";
                    dr["Working_Hour"] = "";
                    dr["ShiftId"] = $"{dutyHourId}_{cur_shift}";
                    dr["Status"] = false;
                    cur_shift++;
                    if (drs.Length == 0)
                    {
                        mt.Rows.Add(dr);
                    }
                }
                mt.Update();
                res["isUpdate"] = true;
                res["error"] = false;
                res["message"] = $"Duty Hour updated successfully";
                res["data"] = PayrollMy.dataTable($"select * from HR_Duty_Hours where id ='{form["HdId"]}'").toJsonObject();
            }

            return res;
        }

        private Dictionary<string, object> deduction_head()
        {
            //IsFixed, Yes
            //ApplyOn, N/A
            //BassedOn,

            if (dict["IsFixed"].ToString() == "Yes")
            {
                if (dict["ApplyOn"].ToString() != "N/A")
                {
                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?$")).IsMatch(dict["BassedOn"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Amount";
                        res["error"] = true;
                        return res;
                    }
                }

                if (dict["IsDeductionOnBoth"].ToString() == "Both")
                {

                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?$")).IsMatch(dict["Employee_Contribution"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Employee Deduction Ammount in Rupees";
                        res["error"] = true;
                        return res;
                    }
                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?$")).IsMatch(dict["Employer_Contribution"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Employer Contribution Amount in Rupees";
                        res["error"] = true;
                        return res;
                    }
                }
                else
                {
                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?$")).IsMatch(dict["Employee_Contribution"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Employee Deduction in Rupees";
                        res["error"] = true;
                        return res;
                    }
                }


            }
            else
            {
                if (dict["ApplyOn"].ToString() == "")
                {
                    res["message"] = "Please select  Calculation Based On";
                    res["error"] = true;
                    return res;
                }
                else if (dict["ApplyOn"].ToString().Contains("Above") || dict["ApplyOn"].ToString().Contains("Bellow"))
                {
                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?$")).IsMatch(dict["BassedOn"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Amount";
                        res["error"] = true;
                        return res;
                    }
                    if (dict["BassedOn"].ToDouble() <= 0)
                    {
                        res["message"] = "Amount must be greater than zero";
                        res["error"] = true;
                        return res;
                    }

                }
                if (dict["IsDeductionOnBoth"].ToString() == "Both")
                {
                    if (!dict["Employee_Contribution"].ToString().Contains("%"))
                    {
                        dict["Employee_Contribution"] = $"{dict["Employee_Contribution"]}%";
                    }
                    if (!dict["Employer_Contribution"].ToString().Contains("%"))
                    {
                        dict["Employer_Contribution"] = $"{dict["Employer_Contribution"]}%";
                    }

                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?%?$")).IsMatch(dict["Employee_Contribution"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Employee Deduction";
                        res["error"] = true;
                        return res;
                    }
                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?%?$")).IsMatch(dict["Employer_Contribution"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Employer Contribution";
                        res["error"] = true;
                        return res;
                    }
                }
                else
                {
                    if (!dict["Employee_Contribution"].ToString().Contains("%"))
                    {
                        dict["Employee_Contribution"] = $"{dict["Employee_Contribution"]}%";
                    }
                    if (!(new Regex(@"^[0-9]+(\.[0-9]+)?%?$")).IsMatch(dict["Employee_Contribution"].ToString()))
                    {
                        res["message"] = "Please Enter Valid Employee";
                        res["error"] = true;
                        return res;
                    }
                }


            }

            if (dict["GradeId"].ToString() == "0")
            {
                var dt = PayrollMy.dataTable("select grade_id from HR_Grade_Master");
                var pre_deduction_type = dict["DeductionType"].ToString();
                var pre_deduction_desc = dict["DeductionDesc"].ToString();

                if (edit)
                {
                    var pdt = PayrollMy.dataTable($"select DeductionType,DeductionDesc from HR_SalaryDeductionHead where  id ='{form["HdId"]}'");
                    pre_deduction_type = pdt.Rows[0]["DeductionType"].ToString();
                    pre_deduction_desc = pdt.Rows[0]["DeductionDesc"].ToString();
                }
                var d_condition = "";
                if (dict["DeductionType"].ToString() == "Other" || pre_deduction_type == "Other")
                {
                    d_condition = $" (DeductionDesc ='{dict["DeductionDesc"]}' or DeductionDesc ='{pre_deduction_desc}')";
                }
                else
                {
                    d_condition = $" (DeductionType ='{dict["DeductionType"]}' or DeductionType ='{pre_deduction_type}')";
                }
                foreach (DataRow dr in dt.Rows)
                {

                    dict["GradeId"] = dr[0].ToString();

                    var idt = PayrollMy.dataTable($"select Id from {mpc.tableName} where GradeId='{dict["GradeId"]}' and  {d_condition}");
                    if (idt.Rows.Count == 1)
                    {
                        PayrollMy.Update(mpc.tableName, dict, $"GradeId='{dict["GradeId"]}' and  {d_condition} ");
                    }
                    else if (idt.Rows.Count > 1)
                    {

                    }
                    else
                    {
                        PayrollMy.Insert(mpc.tableName, dict);
                    }

                }
                res["isReset"] = true;
                res["isUpdate"] = false;
                res["error"] = false;
                res["data"] = PayrollMy.dataTable("select grade_name,hs.* from HR_SalaryDeductionHead hs join hr_Grade_Master gm on hs.GradeId=gm.grade_id order by hs.id desc").toJsonObject();
            }
            else
            {
                if (edit)
                {
                    if (PayrollMy.IsDataExist(mpc.tableName, $"DeductionDesc ='{dict["DeductionDesc"]}'  and GradeId='{dict["GradeId"]}' and id!='{form["HdId"]}'"))
                    {
                        res["message"] = "Deduction Type already added";
                        res["error"] = true;
                        return res;
                    }
                    else
                    {
                        PayrollMy.Update(mpc.tableName, dict, $"id='{form["HdId"]}'");
                    }

                    res["isUpdate"] = true;
                    res["data"] = PayrollMy.dataTable($"select grade_name,hs.* from HR_SalaryDeductionHead hs join hr_Grade_Master gm on hs.GradeId=gm.grade_id where hs.id='{form["HdId"]}'").toJsonObject();
                }
                else
                {
                    if (PayrollMy.IsDataExist(mpc.tableName, $"DeductionDesc ='{dict["DeductionDesc"]}' and GradeId='{dict["GradeId"]}'"))
                    {
                        res["message"] = "Deduction Type already added";
                        res["error"] = true;
                        return res;
                    }
                    else
                    {
                        PayrollMy.Insert(mpc.tableName, dict);
                    }

                    res["isUpdate"] = false;
                    res["error"] = false;
                    res["data"] = PayrollMy.dataTable("select grade_name,hs.* from HR_SalaryDeductionHead hs join hr_Grade_Master gm on hs.GradeId=gm.grade_id order by hs.id desc").toJsonObject();
                }
                res["error"] = false;

            }
            res["message"] = "Deduction head configured successfully";

            return res;
        }

        private Dictionary<string, object> incomehead()
        {
            if (dict["IsVeriable"].ToString() == "Yes")
            {
                if (!dict["PercentageValue"].ToString().Contains("%"))
                {
                    dict["PercentageValue"] = $"{dict["PercentageValue"].ToString().Trim()}%";
                }
                Regex regex = new Regex(@"^[0-9]+(\.[0-9]+)?%?$");
                if (!regex.IsMatch(dict["PercentageValue"].ToString()))
                {
                    res["message"] = "Invalid Value";
                    res["error"] = true;
                    return res;
                }
                if (dict["CalculationBassedOn"].ToString() == "")
                {
                    res["message"] = "Please select 'Calculation Based On.' If no options are shown under 'Calculation Based On,' it means you have not added any Income head for the selected Grade.";
                    res["error"] = true;
                    return res;
                }
            }
            else
            {
                Regex regex = new Regex(@"^[0-9]+(\.[0-9]+)?$");
                if (!regex.IsMatch(dict["PercentageValue"].ToString()))
                {
                    res["message"] = "Invalid Value";
                    res["error"] = true;
                    return res;
                }

            }
            if (dict["GradeId"].ToString() == "0")
            {
                var dt = PayrollMy.dataTable("select grade_id from HR_Grade_Master");

                var calculationBassedOn = dict["CalculationBassedOn"].ToString();
                var pre_incomeHeadId = "";
                var pre_desc = dict["IncomeHead"].ToString();
                if (edit)
                {
                    pre_desc = PayrollMy.data($"select IncomeHead   from HR_SalaryIncomeHead where id='{form["HdId"]}'");
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dict["IsVeriable"].ToString() == "Yes")
                    {
                        var kc = PayrollMy.data($"select IncomeHeadId   from HR_SalaryIncomeHead where GradeId='{dr[0]}' and IncomeHead='{calculationBassedOn}'");
                        dict["CalculationBassedOn"] = kc;
                        if (kc == "")
                        {
                            continue;
                        }
                    }
                    dict["GradeId"] = dr[0].ToString();
                    if (PayrollMy.IsDataExist(mpc.tableName, $"GradeId='{dict["GradeId"]}' and (IncomeHead ='{pre_desc}' or IncomeHead ='{dict["IncomeHead"]}' )"))
                    {
                        if (dict.ContainsKey("IncomeHeadId"))
                        {
                            pre_incomeHeadId = dict["IncomeHeadId"].ToString();
                            dict.Remove("IncomeHeadId");
                        }
                        PayrollMy.Update(mpc.tableName, dict, $"GradeId='{dict["GradeId"]}' and (IncomeHead ='{pre_desc}' or IncomeHead ='{dict["IncomeHead"]}' )");
                        if (!edit)
                        {
                            dict["IncomeHeadId"] = pre_incomeHeadId;
                        }
                    }
                    else
                    {
                        if (edit)
                        {
                            dict["IncomeHeadId"] = PayrollMy.AutoId("IncomeHeadId");
                        }

                        edit = true;
                        PayrollMy.Insert(mpc.tableName, dict);
                    }

                }
                res["isReset"] = true;
                res["isUpdate"] = false;
                res["error"] = false;
                res["data"] = PayrollMy.dataTable("select  (select grade_name from HR_Grade_Master where grade_id=ich.GradeId) Grade,Replace(Replace(IsBasic,'No','Allowances'),'Yes','Basic Salary') IsBasic1,* from HR_SalaryIncomeHead ich order by ich.id desc").toJsonObject();
            }
            else
            {

                if (edit)
                {
                    PayrollMy.Update(mpc.tableName, dict, "id='" + form["HdId"] + "'");
                    res["isUpdate"] = true;
                    res["data"] = PayrollMy.dataTable($"select  (select grade_name from HR_Grade_Master where grade_id=ich.GradeId) Grade,Replace(Replace(IsBasic,'No','Allowances'),'Yes','Basic Salary') IsBasic1,* from HR_SalaryIncomeHead ich where ich.id='{form["HdId"]}'").toJsonObject();
                }
                else
                {
                    PayrollMy.Insert(mpc.tableName, dict);
                    res["isUpdate"] = false;
                    res["error"] = false;
                    res["data"] = PayrollMy.dataTable("select top 1  (select grade_name from HR_Grade_Master where grade_id=ich.GradeId) Grade,Replace(Replace(IsBasic,'No','Allowances'),'Yes','Basic Salary') IsBasic1,* from HR_SalaryIncomeHead ich order by ich.id desc").toJsonObject();
                }
                res["error"] = false;

            }
            res["message"] = "Income head configured successfully";

            return res;
        }

        private Dictionary<string, object> applyleave()
        {
            var LeaveFrom = Convert.ToDateTime(dict["LeaveFrom"]);
            var LeaveTo = Convert.ToDateTime(dict["LeaveTo"]);
            var no_of_days = (LeaveTo - LeaveFrom).TotalDays + 1;
            if (LeaveFrom > LeaveTo)
            {
                res["message"] = "Leave To Date must be greater then  Leave From Date";
                res["error"] = true;
            }
            else
            {

                // check if already applied leave on this dates
                if(PayrollMy.IsDataExist($"select Id from HR_LeaveRequest  where EmployeeId='{userId}'  and StartIdate<={LeaveTo.idate()} and EndIdate>={LeaveFrom.idate()} and Status in ('Pending','Approved')"))
                {
                    res["message"] = $"Already Leave taken between {dict["LeaveFrom"]} to {dict["LeaveTo"]}";
                    res["error"] = true;
                    return res;
                }
                var lt = PayrollMy.dataTable($"select * from HR_LeaveTypes where LeaveId='{dict["LeaveTypeId"]}'");
                // For el,cl,sl, other check balance
                // Metarnity leave check check is female
                // LWP no need to check balance
                var leave_type = lt.Rows[0]["LeaveType"].ToString(); 
                if(leave_type=="Loss of Pay")
                {
                    
                }
                //employee must be female and Married
                else if (leave_type == "Maternity Leave")
                {
                    var em = PayrollMy.dataTable($"select Employee_Name,Gender,Marital_Status from HR_Employee_Master where employee_id='{userId}'");
                    if(em.Rows[0]["Gender"].ToString().equalIgnoreCase("Male") || !em.Rows[0]["Marital_Status"].ToString().equalIgnoreCase("MARRIED"))
                    { 
                        res["message"] = "Maternity Leave is only allowed to Married Female Employee";
                        res["error"] = true;
                    }
                    return res;
                }
                else
                {
                    res["message"] = "There is no leave balance for apply this leave";
                    res["error"] = true;
                    var dt = PayrollMy.dataTable($"select  * from HR_EmployeeLeaveAvailabilityDetails where LeaveTypeId='{dict["LeaveTypeId"]}' and EmployeeId='{userId}' and Session='{PayrollMy.currentSession}'");
                    if(dt.Rows.Count==0)
                    {
                        return res;
                    }
                    else
                    {
                        var balance = dt.Rows[0]["No_of_Leave"].ToInt()- dt.Rows[0]["ConsumeLeave"].ToInt();
                        var pre_leave = PayrollMy.data($"select LeaveDayCount from HR_LeaveRequest where LeaveTypeId='{dict["LeaveTypeId"]}' and EmployeeId='{userId}' and status='Pending'  ").ToDouble();
                        if (balance < (no_of_days + pre_leave))
                        {
                            return res;
                        } 
                    }

                }
                dict["EmployeeId"] = userId;
                dict["Status"] = "Pending";
                dict["LeaveDayCount"] = no_of_days;
                var desig = PayrollMy.data($"select Designation_id from HR_Employee_Master where Employee_id='{userId}'");
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
                if(rt.Rows.Count == 0)
                {
                    rt = PayrollMy.dataTable($"select top 1 UserId from HR_UserProfile where IsHr='1' order by id desc");
                }

                if (rt.Rows.Count > 0)
                {
                    dict["LeaveRequestId"] = PayrollMy.AutoId("HR_LeaveRequestId");
                    dict["RequestSentTo"] = rt.Rows[0][0];
                    dict["StartIdate"] = LeaveFrom.idate();
                    dict["EndIdate"] = LeaveTo.idate();
                    PayrollMy.Insert(mpc.tableName, dict);
                    res["message"] = "Leave Request Sent successfully";
                    res["isUpdate"] = false;
                    res["data"] = PayrollMy.dataTable($"select top 1 Emp_Code,lt.LeaveName,LeaveCode,(select Employee_Name from HR_Employee_Master where employee_id=lr.RequestSentTo) RequestTo,(select Employee_Name from HR_Employee_Master where employee_id=lr.ForwardedTo) RequestForwadedTo, lr.*  from HR_LeaveRequest lr join  HR_Employee_Master on HR_Employee_Master.Employee_id=lr.EmployeeId join HR_LeaveTypes lt on lt.LeaveId=lr.LeaveTypeId where lr.EmployeeId ='{userId}' order by lr.id desc ").toJsonObject();
                    res["error"] = false;
                }
                else
                {
                    res["message"] = "Unable to sent your leave request";
                    res["error"] = true;
                }

            }
            return res;
        }

        private Dictionary<string, object> departments()
        {
            if (edit)
            {

                PayrollMy.Update(mpc.tableName, dict, "Id='" + form["HdId"] + "'");
                if (ConfigurationManager.AppSettings["clientType"] == "hospital")
                {
                    var dep = PayrollMy.data("select department_id from HR_Department_Master where Id='" + form["HdId"] + "'");
                    PayrollMy.Update("HMS_Department_Master", new
                    {
                        Dept_name = dict["name"],
                    }, "Dept_id='" + dep + "'");
                }

                res["message"] = "Department Updated successfully";
                res["isUpdate"] = true;
                res["data"] = PayrollMy.dataTable("select * from HR_Department_Master   where id='" + form["HdId"] + "'").toJsonObject();
                res["error"] = false;
            }
            else
            {
                PayrollMy.Insert(mpc.tableName, dict);
                if (ConfigurationManager.AppSettings["clientType"] == "hospital")
                {
                    PayrollMy.execute("Update HMS_Global_Master set Dept_id=Dept_id+1");
                    PayrollMy.Insert("HMS_Department_Master", new
                    {
                        Dept_name = dict["name"],
                        Dept_id = dict["department_id"],
                        IsOPD = 0,
                        IsIPD = 0,
                        IsLab = 0,
                        FollowUp_days = 0,
                        Istatus = 1,
                        Created_by = 1,
                        Created_date = PayrollMy.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"),
                        Created_idate = PayrollMy.Now.ToString("yyyyMMdd"),
                    });
                }

                res["message"] = "Department added successfully";
                res["isUpdate"] = false;
                res["data"] = PayrollMy.dataTable("select top 1 * from HR_Department_Master order by id desc ").toJsonObject();
                res["error"] = false;
            }
            return res;
        }

        private Dictionary<string, object> csholiday()
        {

            if (edit)
            {
                PayrollMy.Update(mpc.tableName, new
                {
                    HolidayName = dict["HolidayName"],
                    StartDate = dict["StartDate"],
                    EndDate = dict["EndDate"],
                    Session = dict["Session"],
                    DisplayDate = dict["StartDate"].ToString() == dict["EndDate"].ToString() ? PayrollMy.isValidDate(dict["StartDate"].ToString(), "dd MMM") : $"{PayrollMy.isValidDate(dict["StartDate"].ToString(), "dd MMM yyyy")} To {PayrollMy.isValidDate(dict["EndDate"].ToString(), "dd MMM yyyy")}",
                    Days = dict["StartDate"].ToString() == dict["EndDate"].ToString() ? PayrollMy.isValidDate(dict["StartDate"].ToString(), "ddd") : $"{PayrollMy.isValidDate(dict["StartDate"].ToString(), "ddd")} To {PayrollMy.isValidDate(dict["EndDate"].ToString(), "ddd")}",

                }, "Id='" + form["HdId"] + "'");
                res["message"] = "Data Updated successfully";
                res["isUpdate"] = true;
                res["data"] = PayrollMy.dataTable("select hl.*,sl.SessionName  from HR_HolidayList hl join HR_SessionList sl on hl.Session=sl.SessionId where hl.id='" + form["HdId"] + "'").toJsonObject();
                res["error"] = false;
            }
            else
            {
                PayrollMy.Insert(mpc.tableName, new
                {
                    HolidayName = dict["HolidayName"],
                    StartDate = dict["StartDate"],
                    EndDate = dict["EndDate"],
                    Session = dict["Session"],
                    DisplayDate = dict["StartDate"] == dict["EndDate"] ? PayrollMy.isValidDate(dict["StartDate"].ToString(), "dd MMM") : $"{PayrollMy.isValidDate(dict["StartDate"].ToString(), "dd MMM")}-{PayrollMy.isValidDate(dict["EndDate"].ToString(), "dd MMM")}",
                    Days = dict["StartDate"] == dict["EndDate"] ? PayrollMy.isValidDate(dict["StartDate"].ToString(), "ddd") : $"{PayrollMy.isValidDate(dict["StartDate"].ToString(), "ddd")}-{PayrollMy.isValidDate(dict["EndDate"].ToString(), "ddd")}",

                });
                res["message"] = "Data added successfully";
                res["isUpdate"] = false;
                res["data"] = PayrollMy.dataTable("select top 1 hl.*,sl.SessionName  from HR_HolidayList hl join HR_SessionList sl on hl.Session=sl.SessionId order by hl.id desc ").toJsonObject();
                res["error"] = false;
            }
            return res;
        }
    }
}