using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Globalization;
using System.IO;

namespace school_web.Admin
{
    public partial class Add_Student_Calender : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!IsPostBack)
                    {

                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["courseID"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id();

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        txt_startdate.Text = mycode.date();
                        txt_end_date.Text = mycode.date();
                        Bind_course_fee_details();


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Master");
            }
        }

        private void Bind_course_fee_details()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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

        protected void chk_Recurring_CheckedChanged(object sender, EventArgs e)
        {
            txt_end_date.Text = txt_startdate.Text;
            if (chk_Recurring.Checked == true)
            {
                lblstartdate.InnerText = "Start Date";
                enddatpnl.Visible = true;
            }
            else
            {
                lblstartdate.InnerText = "Date";
                enddatpnl.Visible = false;
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
                }
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {



            if (ViewState["Is_add"].ToString() == "1")
            {
                add_update_add();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                add_update_add();
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }

        }

        private void add_update_add()
        {
            try
            {
                if (ddl_session.SelectedValue == "Select")
                {

                    Alertme("Please select session", "warning");

                }

                else if (ddl_type.Text == "Select")
                {

                    Alertme("Please check minimum one course.", "warning");

                }

                else
                {
                    if (chk_Recurring.Checked == true)
                    {
                        if (txt_startdate.Text == "")
                        {
                            Alertme("Please enter start date", "warning");
                        }
                        else if (txt_end_date.Text == "")
                        {
                            Alertme("Please enter end date", "warning");
                        }
                        //else if (txt_details.Text == "")
                        //{
                        //    Alertme("Please enter details", "warning");
                        //}
                        else
                        {
                            add_School_calendar();
                        }
                    }
                    else
                    {
                        if (txt_startdate.Text == "")
                        {
                            Alertme("Please enter start date", "warning");
                        }
                        //else if (txt_details.Text == "")
                        //{
                        //    Alertme("Please details", "warning");
                        //}
                        else
                        {
                            add_School_calendar();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void add_School_calendar()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int ix = 0; ix < growcount; ix++)
            {
                CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                if (chk.Checked == true)
                {
                    Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                    Label lbl_course_name = (Label)rd_view.Items[ix].FindControl("lbl_course_name");
                    insert_into_Fee_master_content_wise(lbl_class_id.Text);
                    ViewState["statusUp"] = "1";
                    ViewState["msg"] = "Fee master has been created successfully";
                }
                else
                {
                    k++;
                }
            }
            if (k == growcount)
            {
                Alertme("Please check minimum one class.", "warning");
                return;
            }
            else
            {
                Alertme("School Calendar has been saved successfully", "success");
                txt_details.Text = "";
                chk_Recurring.Checked = false;
                chk_all.Checked = false;
                enddatpnl.Visible = false;
                txt_end_date.Text = "";
                Bind_course_fee_details();

            }
        }
        UsesCode imp = new UsesCode();
        private void insert_into_Fee_master_content_wise(string classid)
        {
            DateTime startdate = DateTime.ParseExact(txt_startdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (chk_Recurring.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_end_date.Text)))
                {
                    int daycount = mycode.get_no_day_towdateselection(txt_startdate.Text, txt_end_date.Text);
                    for (int i = 0; i < daycount; i++)
                    {
                        string finaldate = startdate.ToString("dd/MM/yyyy");
                        string year = imp.ConvertStringToyear(finaldate);

                        string query = " select  * from School_Holiday_Calendar where   Date='" + finaldate + "' and Class_id=" + classid + " and Session_id='" + ddl_session.SelectedValue + "'";
                        DataTable dt = imp.FillTable(query);
                        if (dt.Rows.Count == 0)
                        {
                            string type = ddl_type.Text;
                            string details = txt_details.Text;
                            string day_name = imp.getdayname(finaldate);
                            if (day_name == "Sunday")
                            {
                                if (ddl_is_sunday.Text == "Yes")
                                {
                                    //details = "Holiday";
                                    type = "Holiday";
                                    details = "Weekly off";
                                }
                            }

                            //=================
                            if (ddl_is_sat_holiday_or_no_acd.Text == "Yes")
                            {
                                if (day_name == ddl_holiday_or_non_acd_day.Text)
                                {
                                    //details = ddl_is_saturday.Text;
                                    type = ddl_is_saturday.Text;
                                }
                            }
                            SqlCommand cmd;
                            query = "INSERT INTO School_Holiday_Calendar (Date,Idate,Day,Details,Year,Month,Holiday_date,Type,Session_id,Branchi_id,Createdby,Created_date,Class_id) values (@Date,@Idate,@Day,@Details,@Year,@Month,@Holiday_date,@Type,@Session_id,@Branchi_id,@Createdby,@Created_date,@Class_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Date", finaldate);
                            cmd.Parameters.AddWithValue("@Idate", imp.ConvertStringToiDate(finaldate));
                            cmd.Parameters.AddWithValue("@Day", imp.getdayname(finaldate));
                            cmd.Parameters.AddWithValue("@Details", details);
                            cmd.Parameters.AddWithValue("@Year", year);
                            cmd.Parameters.AddWithValue("@Month", imp.ConvertStringTomonth(finaldate));
                            cmd.Parameters.AddWithValue("@Holiday_Date", imp.ConvertStringToday(finaldate));
                            cmd.Parameters.AddWithValue("@Type", type);
                            cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branchi_id", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Createdby", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Class_id", classid);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            string type = ddl_type.Text;
                            string details = txt_details.Text;
                            string day_name = imp.getdayname(finaldate);
                            if (day_name == "Sunday")
                            {
                                if (ddl_is_sunday.Text == "Yes")
                                {
                                    // details = "Holiday";
                                    type = "Holiday";
                                    details = "Weekly off";
                                }
                            }

                            //=================
                            if (ddl_is_sat_holiday_or_no_acd.Text == "Yes")
                            {
                                if (day_name == ddl_holiday_or_non_acd_day.Text)
                                {
                                    // details = ddl_is_saturday.Text;
                                    type = ddl_is_saturday.Text;
                                }
                            }

                            string id = dt.Rows[0]["Id"].ToString();
                            SqlCommand cmd;
                            cmd = new SqlCommand("update School_Holiday_Calendar set Details=@Details,Type=@Type,Updated_By=@Updated_By,Updated_date=@Updated_date,Send_Status=@Send_Status where Id=@Id");
                            cmd.Parameters.AddWithValue("@Details", details);
                            cmd.Parameters.AddWithValue("@Type", type);
                            cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Send_Status", "Send");
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                            }
                        }
                        startdate = startdate.AddDays(1);
                    }
                }
                else
                {
                    Alertme("Please select date valid", "warning");
                }
            }
            else
            {
                int daycount = 1;// mycode.get_no_day_towdateselection(txt_startdate.Text, txt_end_date.Text);
                for (int i = 0; i < daycount; i++)
                {
                    string finaldate = startdate.ToString("dd/MM/yyyy");
                    string year = imp.ConvertStringToyear(finaldate);

                    string type = ddl_type.Text;
                    string details = txt_details.Text;
                    string day_name = imp.getdayname(finaldate);
                    if (day_name == "Sunday")
                    {
                        if (ddl_is_sunday.Text == "Yes")
                        {
                            type = "Holiday";
                            details = "Weekly off";
                        }
                    }

                    //=================
                    if (ddl_is_sat_holiday_or_no_acd.Text == "Yes")
                    {
                        if (day_name == ddl_holiday_or_non_acd_day.Text)
                        {
                            type = ddl_is_saturday.Text;
                        }
                    }

                    string query = " select  * from School_Holiday_Calendar where   Date='" + finaldate + "' and Class_id=" + classid + " and Session_id='" + ddl_session.SelectedValue + "'";
                    DataTable dt = imp.FillTable(query);
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        query = "INSERT INTO School_Holiday_Calendar (Date,Idate,Day,Details,Year,Month,Holiday_date,Type,Session_id,Branchi_id,Createdby,Created_date,Class_id) values (@Date,@Idate,@Day,@Details,@Year,@Month,@Holiday_date,@Type,@Session_id,@Branchi_id,@Createdby,@Created_date,@Class_id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Date", finaldate);
                        cmd.Parameters.AddWithValue("@Idate", imp.ConvertStringToiDate(finaldate));
                        cmd.Parameters.AddWithValue("@Day", imp.getdayname(finaldate));
                        cmd.Parameters.AddWithValue("@Details", details);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@Month", imp.ConvertStringTomonth(finaldate));
                        cmd.Parameters.AddWithValue("@Holiday_Date", imp.ConvertStringToday(finaldate));
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Branchi_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Createdby", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Class_id", classid);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        string id = dt.Rows[0]["Id"].ToString();
                        SqlCommand cmd;
                        cmd = new SqlCommand("update School_Holiday_Calendar set Details=@Details,Type=@Type,Updated_By=@Updated_By,Updated_date=@Updated_date,Send_Status=@Send_Status where Id=@Id");
                        cmd.Parameters.AddWithValue("@Details", details);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Send_Status", "Send");
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                        }
                    }
                    startdate = startdate.AddDays(1);
                }
            }
        }
        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rd_view.Items.Count; j++)
            {
                CheckBox chk_class = rd_view.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Student_Calender.aspx", false);
        }
    }
}