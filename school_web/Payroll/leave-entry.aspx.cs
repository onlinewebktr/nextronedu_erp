using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class leave_entry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_employee, "select Employee_Name+','+Emp_Code Employee_Name,Employee_id,Grade_id,Emp_Code from dbo.[PRL_Employee_Master] order by Employee_Name asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Department_Master");
            }
        }





        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select led.*,Employee_Name,case when Short_Name is null then led.Leave_Type else  Short_Name end Short_Name ,format(start_Date,'" + My.Format_Sample + "') s_date,format(end_Date,'" + My.Format_Sample + "') e_date,format(start_Date, 'dd/MM/yyyy') as L_start_Date,format(end_Date, 'dd/MM/yyyy') as L_end_Date from dbo.[PRL_Leave_Entry_details] led join PRL_Employee_Master em on led.Employee_id=em.Employee_id left join PRL_Leave_Name_Master lnm on led.Leave_Type=cast(lnm.Leave_Name_Id as varchar(50)) where led.Employee_id='" + ddl_employee.SelectedValue + "'");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Submit.Text == "Add")
                {
                    if (ddl_employee.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select employee", "warning");
                        ddl_employee.Focus();
                        return;
                    }
                    if (ddl_leave_type.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select leave type", "warning");
                        ddl_leave_type.Focus();
                        return;
                    }
                    if (txt_start_date.Text == "")
                    {
                        Alertme("Please Enter Start Date", "warning");
                        txt_start_date.Focus();
                        return;
                    }
                    if (txt_end_date.Text == "")
                    {
                        Alertme("Please Enter End Date", "warning");
                        txt_end_date.Focus();
                        return;
                    }
                    if (txt_remark.Text == "")
                    {
                        Alertme("Please Enter Remark", "warning");
                        txt_remark.Focus();
                        return;
                    }
                    if (My.toDateTime(txt_start_date.Text) > My.toDateTime(txt_end_date.Text))
                    {
                        Alertme("Start date must be less than equal to End date", "warning");
                        txt_start_date.Focus();
                        return;
                    }

                    if (ddl_leave_type.SelectedValue != "OTH")
                    {
                        if (ddl_leave_type.SelectedValue != "LOP")
                        {
                            if (My.toDouble(allow_leave) < My.toDouble(txt_total_leave.Text))
                            {
                                Alertme("Sorry! you haven't this leave", "warning");
                                txt_start_date.Focus();
                                return;
                            }
                        }
                    }
                    if (My.dataTable(" select idate from dbo.[PRL_Emp_Date_wise_leave_entry] where   idate>=" + My.DateConvertToIdate(txt_start_date.Text) + " and  idate<=" + My.DateConvertToIdate(txt_end_date.Text) + "  and Employee_Id='" + ddl_employee.SelectedValue + "'").Rows.Count > 0)
                    {
                        Alertme("Leave already taken between thease date.", "warning");
                        txt_start_date.Focus();
                        return;
                    }


                    submit_details();
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    if (txt_start_date.Text == "")
                    {
                        Alertme("Please Enter Start Date", "warning");
                        txt_start_date.Focus();
                        return;
                    }
                    if (txt_end_date.Text == "")
                    {
                        Alertme("Please Enter End Date", "warning");
                        txt_end_date.Focus();
                        return;
                    }
                    if (ddl_leave_type.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select Leave Type", "warning");
                        ddl_leave_type.Focus();
                        return;
                    }

                    if (txt_remark.Text == "")
                    {
                        Alertme("Please Enter Remark", "warning");
                        txt_remark.Focus();
                        return;
                    }
                    if (My.toDateTime(txt_start_date.Text) > My.toDateTime(txt_end_date.Text))
                    {
                        Alertme("Start date must be less than equal to End date", "warning");
                        //  txt_start_date.Text = "";
                        txt_start_date.Focus();
                        return;
                    }

                    if (ddl_leave_type.SelectedValue != "OTH")
                    {
                        if (ddl_leave_type.SelectedValue != "LOP")
                        {
                            if (My.toDouble(allow_leave) < My.toDouble(txt_total_leave.Text))
                            {
                                Alertme("Sorry! you haven't this leave", "warning");
                                //   txt_start_date.Text = "";
                                txt_start_date.Focus();
                                return;
                            }
                        }
                    }
                    if (My.dataTable(" select idate from dbo.[PRL_Emp_Date_wise_leave_entry] where   idate>=" + My.DateConvertToIdate(txt_start_date.Text) + " and  idate<=" + My.DateConvertToIdate(txt_end_date.Text) + "  and Employee_Id='" + ddl_employee.SelectedValue + "' and leave_entry_id!='" + ViewState["leave_entry_id"].ToString() + "'").Rows.Count > 0)
                    {
                        Alertme("Leave already taken between thease date.", "warning");
                        txt_start_date.Focus();
                        return;
                    }

                    try
                    {
                        update_update_details();
                        empty_form();
                        bind_grd_view();
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Leave_Entry_details where Id='" + ViewState["edtID"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = ddl_employee.SelectedValue;
                dr[2] = txt_start_date.Text;
                dr[3] = txt_end_date.Text;
                dr[4] = ddl_leave_type.SelectedValue;
                dr[5] = txt_approved_by.Text;
                dr[6] = txt_remark.Text;
                try
                {
                    if (FileUpload1.PostedFile.ContentLength > 0)
                    {
                        string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        string FileName1 = ddl_employee.SelectedValue + extension;

                        HttpPostedFile psotedf1 = FileUpload1.PostedFile;
                        psotedf1.SaveAs(MapPath("../Master_Img/" + FileName1));
                        dr["Application_File"] = "../Master_Img/" + Path.GetFileName(FileName1);
                    }
                }
                catch
                { }
                dr["Total_leave"] = txt_total_leave.Text;
                dr["LOP"] = txt_lop_leave.Text;
                dr["LWP"] = txt_lwp_leave.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            delete_and_update_previous_leave_details();

            send_to_datewise_leave(ViewState["leave_entry_id"].ToString());
            Alertme("Leave Entry Details  Updated Successfully", "success");
        }

        private void delete_and_update_previous_leave_details()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Emp_Date_wise_leave_entry where  leave_entry_id ='" + ViewState["leave_entry_id"].ToString() + "'", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Emp_Date_wise_leave_entry");
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DateTime date = My.toDateTime(dr["Date"].ToString());
                remove_leave_from_attendance_chart(date);
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void submit_details()
        {
            string leave_entry_id;
            leave_entry_id = My.auto_serialS("leave_entry_id");
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Leave_Entry_details", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = ddl_employee.SelectedValue;
            dr[2] = txt_start_date.Text;
            dr[3] = txt_end_date.Text;
            dr[4] = ddl_leave_type.SelectedValue;
            dr[5] = txt_approved_by.Text;
            dr[6] = txt_remark.Text;

            if (FileUpload1.PostedFile.ContentLength > 0)
            {

                string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FileName1 = ddl_employee.SelectedValue + extension;

                HttpPostedFile psotedf1 = FileUpload1.PostedFile;
                psotedf1.SaveAs(MapPath("../Master_Img/" + FileName1));
                dr["Application_File"] = "../Master_Img/" + Path.GetFileName(FileName1);
            }
            else
            {
                dr["Application_File"] = null;
            }


            dr[8] = "Approved";
            dr["Total_leave"] = txt_total_leave.Text;
            dr["LOP"] = txt_lop_leave.Text;
            dr["LWP"] = txt_lwp_leave.Text;
            dr["leave_entry_id"] = leave_entry_id;
            dr["Doc_Type"] = null;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            send_to_datewise_leave(leave_entry_id);
            Alertme("Leave Entry Details Created Successfully", "success");
        }

        private void send_to_datewise_leave(string leave_entry_id)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Emp_Date_wise_leave_entry", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Emp_Date_wise_leave_entry");
            DataTable dt = ds.Tables[0];
            double total = My.toDouble(txt_total_leave.Text);

            double lop = My.toDouble(txt_lop_leave.Text);
            double lwp = My.toDouble(txt_lwp_leave.Text);
            DateTime date = Convert.ToDateTime(txt_start_date.Text);
            if (ddl_leave_type.SelectedValue == "OTH")
            {
            }
            else if (ddl_leave_type.SelectedValue == "LOP")
            {
                lwp = 0;
            }
            else
            {
                lwp = 0; lop = 0;
            }

            for (int i = 1; i <= total; i++)
            {
                DataRow dr = dt.NewRow();
                dr["idate"] = date.ToString("yyyyMMdd");
                dr["Date"] = date.ToString("dd-MMM-yyyy");
                if (i <= lop)
                {
                    dr["Leave_Type"] = "LOP";
                    dr["Leave_id"] = "LOP";
                    update_attendance(date, "LOP");
                }
                else if (i <= lop + lwp)
                {
                    dr["Leave_Type"] = "LWP";
                    dr["Leave_id"] = "LWP";
                    update_attendance(date, "LWP");
                }
                else
                {
                    dr["Leave_Type"] = ddl_leave_type.SelectedItem.Text;
                    dr["Leave_id"] = ddl_leave_type.SelectedValue;
                    update_attendance(date, ddl_leave_type.SelectedItem.Text);
                }

                dr["leave_entry_id"] = leave_entry_id;
                dr["Employee_Id"] = ddl_employee.SelectedValue;
                dt.Rows.Add(dr);
                date = date.AddDays(1);
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void update_attendance(DateTime date, string leave_name)
        {
            string emp_id = ddl_employee.SelectedValue.ToString();
            string update_qry = "";
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Daily_attendance_chart where Employee_id='" + emp_id + "' and idate='" + date.ToString("yyyyMMdd") + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Employee_Daily_attendance_chart");
            DataTable dt = ds.Tables[0];
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Employee_id"] = emp_id;
            dr["Date"] = date.ToString("dd/MM/yyyy");
            dr["idate"] = date.ToString("yyyyMMdd"); ;
            dr["Shift_1_in"] = leave_name;
            dr["Shift_1_out"] = leave_name;
            dr["Shift_2_in"] = leave_name;
            dr["Shift_2_out"] = leave_name;
            dr["Shift_1_Working"] = "-2";
            dr["Shift_2_Working"] = "-2";
            dr["attendance_type"] = "";
            string year = date.ToString("yyyy");
            string month = date.ToString("MM");
            int day = My.toIntS(date.ToString("dd"));
            if (My.dataTable("select Employee_id from PRL_Employee_Attendance where Employee_id='" + emp_id + "' and Month='" + month + "' and Year='" + year + "'").Rows.Count == 0)
            {
                update_qry = "insert into PRL_Employee_Attendance(Employee_id,Month,Year,S1_" + day + "_in,S1_" + day + "_out,S2_" + day + "_in,S2_" + day + "_out) values ('" + emp_id + "','" + month + "','" + year + "','" + dr["Shift_1_in"] + "','" + dr["Shift_1_out"] + "','" + dr["Shift_2_in"] + "','" + dr["Shift_2_out"] + "');";
            }
            else
            {
                update_qry = "update PRL_Employee_Attendance set S1_" + day + "_in='" + dr["Shift_1_in"] + "',S1_" + day + "_out='" + dr["Shift_1_out"] + "',S2_" + day + "_in='" + dr["Shift_2_in"] + "',S2_" + day + "_out='" + dr["Shift_2_out"] + "' where Employee_id='" + emp_id + "' and Month='" + month + "' and Year='" + year + "'";
            }
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dr);
            }
            My.exeSql(update_qry);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void remove_leave_from_attendance_chart(DateTime date)
        {
            string idate = date.ToString("yyyyMMdd");
            string emp_id = ddl_employee.SelectedValue.ToString();
            string Emp_Code = My.ddl_value(ddl_employee.SelectedItem, "Emp_Code");
            string Grade_id = My.ddl_value(ddl_employee.SelectedItem, "Grade_id");
            DataTable at_log = My.dataTable("select *, format(DateTime,'yyyyMMdd') idate, format(DateTime,'hh:mm tt') time, format(DateTime,'HHmm') itime from PRL_Attendance_Log where Employee_id='" + Emp_Code + "'");

            string update_qry = "";
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Daily_attendance_chart where Employee_id='" + emp_id + "' and idate='" + idate + "'", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "PRL_Employee_Daily_attendance_chart");
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                var drs = at_log.Select("idate='" + idate + "'");
                dr["Shift_1_in"] = "";
                dr["Shift_1_out"] = "";
                dr["Shift_2_in"] = "";
                dr["Shift_2_out"] = "";
                dr["Shift_1_Working"] = DBNull.Value;
                dr["Shift_2_Working"] = DBNull.Value;
                foreach (DataRow r in drs)
                {
                    int shift = My.toIntS(My.data("select  case when " + r["itime"] + "<=iMorning_out then 1 else 2 end from PRL_Attendance_Timing_Setting     where Grade_id='" + Grade_id + "' "));
                    if (shift == 1)
                    {
                        if (dr["Shift_1_in"].ToString() == "")
                        {
                            dr["Shift_1_in"] = r["time"];
                            //send_to_atn_chart(staff_id, attendance_time, "1", "in");
                        }
                        else if (dr["Shift_1_in"].ToString() == r["time"].ToString())
                        {

                        }
                        else
                        {
                            dr["Shift_1_out"] = r["time"].ToString();
                            DateTime _in = My.toDateTime(dr["Shift_1_in"].ToString());
                            DateTime _out = My.toDateTime(dr["Shift_1_out"].ToString());
                            dr["Shift_1_Working"] = (_out - _in).TotalHours;
                            // send_to_atn_chart(staff_id, attendance_time, "1", "out");
                        }

                    }
                    else
                    {
                        if (dr["Shift_1_in"].ToString() != "" && dr["Shift_1_out"].ToString() == "")
                        {
                            dr["Shift_1_out"] = r["time"].ToString();
                            DateTime _in = My.toDateTime(dr["Shift_2_in"].ToString());
                            DateTime _out = My.toDateTime(dr["Shift_2_out"].ToString());
                            dr["Shift_2_Working"] = (_out - _in).TotalHours;
                            dr["Shift_1_Working"] = (_out - _in).TotalHours;
                            // send_to_atn_chart(staff_id, attendance_time, "1", "out");
                            shift = 1;
                        }
                        else if (dr["Shift_2_in"].ToString() == "")
                        {
                            dr["Shift_2_in"] = r["time"].ToString();
                            //  send_to_atn_chart(staff_id, attendance_time, "2", "in");
                        }
                        else if (dr["Shift_2_in"].ToString() == r["time"].ToString())
                        {

                        }
                        else
                        {
                            dr["Shift_2_out"] = r["time"].ToString();
                            //  send_to_atn_chart(staff_id, attendance_time, "2", "out");

                            DateTime _in = My.toDateTime(dr["Shift_2_in"].ToString());
                            DateTime _out = My.toDateTime(dr["Shift_2_out"].ToString());
                            dr["Shift_2_Working"] = (_out - _in).TotalHours;
                        }
                    }
                    dr["attendance_type"] = "device";
                    //Shift_1_in
                }
                string year = date.ToString("yyyy");
                string month = date.ToString("MM");
                int day = My.toIntS(date.ToString("dd"));

                update_qry = " update  PRL_Employee_Attendance set S1_" + day + "_in='" + dr["Shift_1_in"] + "',S1_" + day + "_out='" + dr["Shift_1_out"] + "',S2_" + day + "_in='" + dr["Shift_2_in"] + "',S2_" + day + "_out='" + dr["Shift_2_out"] + "' where Employee_id='" + emp_id + "' and Month='" + month + "' and Year='" + year + "'";

                My.exeSql(update_qry);
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void empty_form()
        {
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_start_date.Text = "";
            txt_end_date.Text = "";
            txt_approved_by.Text = "";
            txt_remark.Text = "";
            txt_total_leave.Text = "0";
            txt_lop_leave.Text = "0";
            txt_lwp_leave.Text = "0";
        }



        My mycode = new My();
        string allow_leave = "";
        protected void ddl_leave_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_leave_type.SelectedItem.Text != "Select")
            {
                allow_leave = "";
                pnl_lop.Visible = false;
                if (ddl_leave_type.SelectedValue == "OTH")
                {
                    pnl_lop.Visible = true; pnl_leave.Visible = false;

                    txt_lwp_leave.Text = "0";
                }
                else if (ddl_leave_type.SelectedValue == "LOP")
                {
                    pnl_lop.Visible = false; pnl_leave.Visible = false;
                    txt_lwp_leave.Text = "0";
                }
                else
                {
                    txt_lop_leave.Text = "0";
                    txt_lwp_leave.Text = "0";
                    allow_leave = find_allow_leave(ViewState["GraDeiD"].ToString());
                    pnl_leave.Visible = true;
                    txt_leave.Text = allow_leave;
                }
            }
        }

        private string find_allow_leave(string grade_id)
        {
            double allow_leave = 0;
            DataTable dt = My.dataTable("select * from PRL_Staff_Leave_Setup where Grade_id='" + grade_id + "' and Leave_id='" + ddl_leave_type.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Leave_Type"].ToString() == "Fixed")
                    {
                        allow_leave = My.toDouble(dr["No_of_Leave"]);
                    }
                    else
                    {
                        double nol = My.toDouble(dr["No_of_Leave"].ToString());
                        double total_working = find_working_day();
                        double day_worked = My.toDouble(dr["No_of_Leave"].ToString());
                        double el = My.toDouble(dr["Earned_Leave"].ToString());
                        double al = (total_working / day_worked) * el;
                        if (nol > al)
                        {
                            allow_leave = al;
                        }
                        else
                        {
                            allow_leave = nol;
                        }
                    }
                }
            }
            int leave_taken = My.dataTable(" select Leave_Type from dbo.[PRL_Emp_Date_wise_leave_entry] where Leave_id='" + ddl_leave_type.SelectedValue + "' and idate like '" + DateTime.Now.Year + "%' and Employee_Id='" + ddl_employee.SelectedValue + "'").Rows.Count;
            double modify = 0;
            if (ViewState["leavetypeUID"].ToString() == ddl_leave_type.SelectedValue.ToString())
            {
                modify = My.toDouble(ViewState["leavetypeTAG"].ToString());
            }
            return (allow_leave - leave_taken + modify).ToString();
        }

        int present = 0, half_day = 0;
        private double find_working_day()
        {
            present = 0; half_day = 0;
            int year = My.toIntS(My.toDateTime(txt_end_date.Text).ToString("yyyy"));
            DataTable dt = My.dataTable("select * from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + year + "%' and Employee_id='" + ddl_employee.SelectedValue + "'  order by idate");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Shift_1_in"].ToString() != "" && dr["Shift_1_out"].ToString() != "" && dr["Shift_2_in"].ToString() != "" && dr["Shift_2_out"].ToString() != "")
                {
                    if (dr["Shift_1_Working"].ToString() == "-2" && dr["Shift_2_Working"].ToString() == "-2")
                    {

                    }
                    else
                    {
                        present += 1;
                    }
                }
                else if (dr["Shift_1_in"].ToString() == "" && dr["Shift_1_out"].ToString() == "" && dr["Shift_2_in"].ToString() != "" && dr["Shift_2_out"].ToString() != "")
                {
                    half_day += 1;
                }
                else if (dr["Shift_1_in"].ToString() != "" && dr["Shift_1_out"].ToString() != "" && dr["Shift_2_in"].ToString() == "" && dr["Shift_2_out"].ToString() == "")
                {
                    half_day += 1;
                }
            }
            return present + (half_day / 2);
        }

        protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fnd_grade_id();

                bind_leave_type();
                bind_grd_view();

            }
            catch (Exception ex)
            {
            }
        }

        private void fnd_grade_id()
        {
            DataTable dt = mycode.FillData("select Grade_id from PRL_Employee_Master where Employee_id='" + ddl_employee.SelectedValue + "'");
            ViewState["GraDeiD"] = dt.Rows[0]["Grade_id"].ToString();
        }
        private void bind_leave_type()
        {
            mycode.bind_all_ddl_with_id(ddl_leave_type, "select lnm.Short_Name,cast(eal.Leave_Name_id as varchar(50)) Leave_Name_id from dbo.[PRL_Employee_Applicabe_Leave] eal join  PRL_Leave_Name_Master lnm on eal.Leave_Name_id=lnm.Leave_Name_Id where eal.Employee_id='" + ddl_employee.SelectedValue + "'  union all select 'LOP' Short_Name,'LOP' Leave_Name_id union all select 'Other' Short_Name,'OTH' Leave_Name_id");
        }

        protected void txt_start_date_TextChanged(object sender, EventArgs e)
        {
            if (txt_start_date.Text != "" && txt_end_date.Text != "")
            {
                if (My.toDateTime(txt_start_date.Text) > My.toDateTime(txt_end_date.Text))
                {

                }
                else
                {
                    calculate_leave();
                }

            }
        }

        private void calculate_leave()
        {
            TimeSpan t = My.toDateTime(txt_end_date.Text) - My.toDateTime(txt_start_date.Text);
            int day = t.Days + 1;
            txt_total_leave.Text = day.ToString();
            calc_lwp();
        }

        private void calc_lwp()
        {
            if (ddl_leave_type.SelectedValue == "OTH")
            {
                txt_lwp_leave.Text = (Convert.ToInt32(txt_total_leave.Text) - Convert.ToInt32(txt_lop_leave.Text)).ToString();
            }
            else
            {
                txt_lwp_leave.Text = "0";
            }
        }

        protected void txt_end_date_TextChanged(object sender, EventArgs e)
        {
            if (txt_start_date.Text != "" && txt_end_date.Text != "")
            {
                if (My.toDateTime(txt_start_date.Text) > My.toDateTime(txt_end_date.Text))
                {
                }
                else
                {
                    calculate_leave();
                }

            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_id = (Label)row.FindControl("lbl_id");

            DataTable dt = mycode.FillData("select led.*,Employee_Name,case when Short_Name is null then led.Leave_Type else  Short_Name end Short_Name ,format(start_Date,'dd/MM/yyyy') s_date,format(end_Date,'dd/MM/yyyy') e_date,format(start_Date, 'dd/MM/yyyy') as L_start_Date,format(end_Date, 'dd/MM/yyyy') as L_end_Date from dbo.[PRL_Leave_Entry_details] led join PRL_Employee_Master em on led.Employee_id=em.Employee_id left join PRL_Leave_Name_Master lnm on led.Leave_Type=cast(lnm.Leave_Name_Id as varchar(50)) where led.Id='" + lbl_id.Text + "'");

            ViewState["status"] = dt.Rows[0]["Status"].ToString();
            ViewState["edtID"] = dt.Rows[0]["Id"].ToString();
            ViewState["leave_entry_id"] = dt.Rows[0]["leave_entry_id"].ToString();
            ddl_employee.SelectedValue = dt.Rows[0]["Employee_id"].ToString();
            fnd_grade_id();
            txt_start_date.Text = My.toDateTime(dt.Rows[0]["start_Date"].ToString()).ToString(My.Format_Sample);
            txt_end_date.Text = My.toDateTime(dt.Rows[0]["end_Date"].ToString()).ToString(My.Format_Sample);

            ViewState["leavetypeUID"] = dt.Rows[0]["Leave_Type"].ToString();
            ViewState["leavetypeTAG"] = dt.Rows[0]["Total_leave"].ToString();
            ddl_leave_type.SelectedValue = dt.Rows[0]["Leave_Type"].ToString();

            if (ddl_leave_type.SelectedValue != "OTH")
            {
                if (ddl_leave_type.SelectedValue != "LOP")
                {
                    allow_leave = find_allow_leave(ViewState["GraDeiD"].ToString());
                    pnl_leave.Visible = true;
                    txt_leave.Text = allow_leave;
                }
            }
            txt_approved_by.Text = dt.Rows[0][5].ToString();
            txt_remark.Text = dt.Rows[0][6].ToString();

            txt_total_leave.Text = dt.Rows[0]["Total_leave"].ToString();
            txt_lop_leave.Text = dt.Rows[0]["LOP"].ToString();
            txt_lwp_leave.Text = dt.Rows[0]["LWP"].ToString();
            btn_Submit.Text = "Update";
            btn_cancel.Visible = true;
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_leave_entry_id = (Label)row.FindControl("lbl_leave_entry_id");
                ViewState["leave_entry_id"] = lbl_leave_entry_id.Text;
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Leave_Entry_details where Id='" + lbl_id.Text + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr.Delete();
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                delete_and_update_previous_leave_details();
                Alertme("Leave Entry Details deleted Successfully", "success");
                bind_grd_view();
                empty_form();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }
    }
}