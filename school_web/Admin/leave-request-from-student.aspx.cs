using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class leave_request_from_student : System.Web.UI.Page
    {
        My mycode = new My();
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "leave-request-from-student.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        find_firm_details();
                        txt_from_date.Text = "01/" + mycode.monthYear();
                        txt_to_date.Text = mycode.date();
                        get_all_leave_and_datewise();
                        ViewState["flag"] = "0";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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



        private void get_leave_info(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_total_leave.Text = dt.Rows.Count.ToString();
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_excels.Visible = false;
            }
            else
            {
                lbl_class22.Text = " Leave Status : " + ddl_Status.SelectedItem.Text + " Start Date :" + txt_from_date.Text + " To " + txt_to_date.Text;


                lbl_total_leave.Text = dt.Rows.Count.ToString();
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
        }

        int pending = 0; int approved = 0; int rejected = 0;
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_status = (Label)e.Item.FindControl("lbl_status");
                Label lbl_Total_day_count = (Label)e.Item.FindControl("lbl_Total_day_count");
                LinkButton lnk_accept_leave = (LinkButton)e.Item.FindControl("lnk_accept_leave");

                if (lbl_status.Text == "Pending")
                {
                    pending++;
                }
                if (lbl_status.Text == "Approved")
                {
                    approved++;
                }
                if (lbl_status.Text == "Rejected")
                {
                    rejected++;
                }

                if (lbl_status.Text == "Pending")
                {
                    lnk_accept_leave.Visible = true;
                }
                else
                {
                    lnk_accept_leave.Visible = false;
                }
            }

            lbltotal_pending.Text = pending.ToString();
            lbltotal_approved.Text = approved.ToString();
            lbl_rejected_leave.Text = rejected.ToString();
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Status.SelectedItem.Text == "ALL")
                {
                    get_all_leave_and_datewise();
                    ViewState["flag"] = "0";
                }
                else
                {
                    get_leave_statuswise_and_datewise();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void get_all_leave_and_datewise()
        {
            get_leave_info("select * from Student_Leave_Apply where Apply_Idate>=" + My.DateConvertToIdate(txt_from_date.Text) + " and Apply_Idate<=" + My.DateConvertToIdate(txt_to_date.Text) + " order by id desc");
        }

        private void get_leave_statuswise_and_datewise()
        {
            get_leave_info("select * from Student_Leave_Apply where Status='" + ddl_Status.SelectedItem.Text + "' and  Apply_Idate>=" + My.DateConvertToIdate(txt_from_date.Text) + " and Apply_Idate<=" + My.DateConvertToIdate(txt_to_date.Text) + " order by id desc");
        }

        protected void lnk_accept_leave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label Id = (Label)row.FindControl("lbl_Id");

                    Label lbl_leave_from = (Label)row.FindControl("lbl_leave_from");
                    Label lbl_leave_to = (Label)row.FindControl("lbl_leave_to");
                    Label lbl_section = (Label)row.FindControl("lbl_section");
                    Label lbl_class_name = (Label)row.FindControl("lbl_class_name");
                    Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");


                    ViewState["leaveFrom"] = lbl_leave_from.Text;
                    ViewState["leaveTo"] = lbl_leave_to.Text;

                    ViewState["Section"] = lbl_section.Text;
                    ViewState["Class_name"] = lbl_class_name.Text;
                    ViewState["Admission_no"] = lbl_admission_no.Text;

                    ViewState["rowID"] = Id.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label Id = (Label)row.FindControl("lbl_Id");

                    Label lbl_leave_from = (Label)row.FindControl("lbl_leave_from");
                    Label lbl_leave_to = (Label)row.FindControl("lbl_leave_to");
                    Label lbl_section = (Label)row.FindControl("lbl_section");
                    Label lbl_class_name = (Label)row.FindControl("lbl_class_name");
                    Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");


                    ViewState["leaveFrom"] = lbl_leave_from.Text;
                    ViewState["leaveTo"] = lbl_leave_to.Text;

                    ViewState["Section"] = lbl_section.Text;
                    ViewState["Class_name"] = lbl_class_name.Text;
                    ViewState["Admission_no"] = lbl_admission_no.Text;

                    ViewState["rowID"] = Id.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void lnk_submit_leave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_remark.Text == "")
                {
                    Alertme("please enter remarks.", "warning");
                    txt_remark.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    string qry = "update Student_Leave_Apply set Status='" + ddl_update_status.SelectedValue + "',Updated_remark='" + txt_remark.Text + "',Updated_by='" + ViewState["Userid"].ToString() + "',Updated_date='" + mycode.date() + "' where Id=" + ViewState["rowID"].ToString() + "";
                    My.exeSql(qry);

                    if (ddl_update_status.SelectedItem.Text == "Approved")
                    {
                        int totaldays = 0;
                        int leaveFrom = My.DateConvertToIdate(ViewState["leaveFrom"].ToString());
                        int leaveTo = My.DateConvertToIdate(ViewState["leaveTo"].ToString());


                        DateTime startdate1 = DateTime.ParseExact(ViewState["leaveFrom"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(ViewState["leaveTo"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        totaldays = Convert.ToInt32(diff.Days);
                        totaldays++;

                        string class_id = get_class_id(ViewState["Class_name"].ToString());
                        for (int i = 0; i < totaldays; i++)
                        {
                            string curentLeaveDay = "";
                            if (i == 0)
                            {
                                curentLeaveDay = ViewState["leaveFrom"].ToString();
                            }
                            else
                            {
                                DateTime SevenstartTime = DateTime.ParseExact(ViewState["leaveFrom"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                curentLeaveDay = SevenstartTime.AddDays(1).ToShortDateString();
                            }


                            DateTime c_datetime = DateTime.ParseExact(curentLeaveDay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            //======================
                            SqlCommand cmd;
                            string query = "INSERT INTO Student_Attendance_saved_Class_Wise (Session_id,Class_id,Section,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no) values (@Session_id,@Class_id,@Section,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
                            cmd.Parameters.AddWithValue("@Class_id", class_id);
                            cmd.Parameters.AddWithValue("@Section", ViewState["Section"].ToString());
                            cmd.Parameters.AddWithValue("@Class_period", "0");
                            cmd.Parameters.AddWithValue("@Day", c_datetime.ToString("dddd"));
                            cmd.Parameters.AddWithValue("@Attendance_Date", c_datetime.ToString("dd/MM/yyyy"));
                            cmd.Parameters.AddWithValue("@Attendance_IDate", c_datetime.ToString("yyyyMMdd"));
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@time", mycode.idate());
                            cmd.Parameters.AddWithValue("@Created_By", Session["Admin"].ToString());

                            cmd.Parameters.AddWithValue("@Attendance_Status", "Leave");
                            cmd.Parameters.AddWithValue("@Admission_no", ViewState["Admission_no"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }

                    Alertme("Status of leave has been updated successfully.", "success");
                    if (ViewState["flag"].ToString() == "0")
                    {
                        get_all_leave_and_datewise();
                    }
                    if (ViewState["flag"].ToString() == "1")
                    {
                        get_leave_statuswise_and_datewise();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private string get_class_id(string class_name)
        {
            string returN = "0";
            DataTable dt = mycode.FillData("select course_id from Add_course_table where Course_Name='" + class_name + "'");
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0][0].ToString();
                return returN;
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}
