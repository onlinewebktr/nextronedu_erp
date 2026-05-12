using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class monthly_dues : System.Web.UI.Page
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
                        ViewState["RepeatFine"] = "No";
                        ViewState["flags1"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        lbl_date.Text = " for : " + ddl_fee_type.SelectedItem.Text + " Students, Date : " + mycode.date();
                        //mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        find_firm_details();
                        bind_class();

                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
                        ddl_session.SelectedValue = My.get_session_id();
                        bind_month();

                        Dictionary<string, object> dc2 = mycode.Firm_details();
                        ViewState["firm_name"] = (String)dc2["firm_name"];
                        #region sms
                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Month Dues");
                        ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                        ViewState["VariableName"] = (String)autosms["VariableName"];
                        ViewState["SMSType"] = (String)autosms["SMSType"];
                        ViewState["Send_From"] = (String)autosms["Send_From"];
                        ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                        ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];






                        pnl_0.Visible = pnl_1.Visible = pnl_2.Visible = pnl_3.Visible = pnl_4.Visible = pnl_5.Visible = pnl_6.Visible = pnl_7.Visible = pnl_8.Visible = false;
                        var vrls = ViewState["VariableName"].ToString().Split(',');
                        txt_message.Text = ViewState["SMS_Tempate"].ToString();
                        if (vrls.Length > 0)
                        {
                            pnl_0.Visible = true;
                            lbl_0.Text = vrls[0];
                            txt_0.Text = "";
                        }
                        if (vrls.Length > 1)
                        {
                            pnl_1.Visible = true;
                            lbl_1.Text = vrls[1];
                            txt_1.Text = "";
                        }
                        if (vrls.Length > 2)
                        {
                            pnl_2.Visible = true;
                            lbl_2.Text = vrls[2];
                            txt_2.Text = "";
                        }
                        if (vrls.Length > 3)
                        {
                            pnl_3.Visible = true;
                            lbl_3.Text = vrls[3];
                            txt_3.Text = "";
                        }
                        if (vrls.Length > 4)
                        {
                            pnl_4.Visible = true;
                            lbl_4.Text = vrls[4];
                            txt_4.Text = "";
                        }
                        if (vrls.Length > 5)
                        {
                            pnl_5.Visible = true;
                            lbl_5.Text = vrls[5];
                            txt_5.Text = "";
                        }
                        if (vrls.Length > 6)
                        {
                            pnl_6.Visible = true;
                            lbl_6.Text = vrls[6];
                            txt_6.Text = "";
                        }
                        if (vrls.Length > 7)
                        {
                            pnl_7.Visible = true;
                            lbl_7.Text = vrls[7];
                            txt_7.Text = "";
                        }
                        if (vrls.Length > 8)
                        {
                            pnl_8.Visible = true;
                            lbl_8.Text = vrls[8];
                            txt_8.Text = "";
                        }
                        #endregion

                        //try
                        //{
                        //    mycode.bind_all_ddl_with_id(ddl_template, "select  " +
                        //  " SMS_Tempate, id from SMS_Template_Setting  where Send_From='Month Dues'");
                        //    ddl_template.SelectedValue = My.get_top_one_sms_data("Month Dues");
                        //    find_templatedata();
                        //}
                        //catch(Exception ex)
                        //{
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
            }
        }

        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();


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
                try
                {
                    if (dt.Rows[0]["Is_quarterwise_payment"].ToString() == "True")
                    {
                        ViewState["Is_quarterwise_payment"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }
                try
                {
                    if (dt.Rows[0]["Is_fine_repeat"].ToString() == "True")
                    {
                        ViewState["RepeatFine"] = "Yes";
                    }
                    else
                    {
                        ViewState["RepeatFine"] = "No";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void bind_month()
        {
            DataTable dt = mycode.FillData("select Month,'false' as Value,Month_Id from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();
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



        protected void chk_all_month_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_month.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_month.Items[j].FindControl("chk_month_name") as CheckBox;
                if (chk_all_month.Checked)
                {
                    chk_month_name.Checked = true;
                }
                else
                {
                    chk_month_name.Checked = false;
                }
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            bool isClassSelectd = false; string selectClassid = "0";
            foreach (ListItem item in ddl_classs.Items)
            {
                if (item.Selected)
                {
                    selectClassid = selectClassid + item.Value + ",";
                    isClassSelectd = true;
                }
            }
            if (isClassSelectd == false)
            {
                Alertme("Please select class.", "warning");
                return;
            }
            else
            {
                lbl_date.Text = " for : " + ddl_fee_type.SelectedItem.Text + " Students, Date : " + mycode.date();
                find_student_dues_by_month();
            }
            scrpt = "<script> click_empty();</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        private void find_student_dues_by_month()
        {
            string selectClassid = "";
            foreach (ListItem item in ddl_classs.Items)
            {
                if (item.Selected)
                {
                    selectClassid = selectClassid + "'" + item.Value + "',";
                }
            }
            selectClassid = selectClassid.Remove(selectClassid.Length - 1);
            DataTable fdt = new DataTable();
            fdt.Columns.Add("Admission_no");
            fdt.Columns.Add("Session");
            fdt.Columns.Add("Class");



            string qry = "";
            //if (ddl_class.SelectedItem.Text == "ALL")
            //{
            //    if (ddl_section.SelectedItem.Text != "ALL")
            //    {
            //        Alertme("Please select class.", "warning");
            //        ddl_class.Focus();
            //        return;
            //    }

            //    if (ddl_fee_type.SelectedItem.Text == "ALL")
            //    {
            //        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Status='1' and t1.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //    }
            //    else
            //    {
            //        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.From_month_name as Month_name,t2.From_month_id as Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Hostel_assign_master t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Status='1' and t1.hosteltaken='Yes' and t2.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Status='1' and t1.hosteltaken='No' and t1.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Status='1' and t1.hosteltaken='No' and t1.Status='1' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id and Istatus='1') order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //        else //Bus
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' and t1.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //    }
            //}
            //else
            //{
            //if (ddl_section.SelectedItem.Text == "ALL")
            //{
            if (ddl_fee_type.SelectedItem.Text == "ALL")
            {
                qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as [Student Name],t1.class as [Class],t1.Section,t1.rollnumber as [Roll No.],t1.fathername as [Father Name],t1.father_mob as [Mobile No.],t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            }
            else
            {
                if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                {
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as [Student Name],t1.class as [Class],t1.Section,t1.rollnumber as [Roll No.],t1.fathername as [Father Name],t1.father_mob as [Mobile No.],t1.Session_id,t1.Class_id,t2.From_month_name as Month_name,t2.From_month_id as Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Hostel_assign_master t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Status='1' and t1.hosteltaken='Yes' and t2.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                {
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as [Student Name],t1.class as [Class],t1.Section,t1.rollnumber as [Roll No.],t1.fathername as [Father Name],t1.father_mob as [Mobile No.],t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Status='1' and t1.hosteltaken='No'  order by t3.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                {
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as [Student Name],t1.class as [Class],t1.Section,t1.rollnumber as [Roll No.],t1.fathername as [Father Name],t1.father_mob as [Mobile No.],t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_fee_type.SelectedValue == "5")  //DAY SCHOLAR Without Transport
                {
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as [Student Name],t1.class as [Class],t1.Section,t1.rollnumber as [Roll No.],t1.fathername as [Father Name],t1.father_mob as [Mobile No.],t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Status='1' and t1.transportationtaken='No' and t1.hosteltaken='No'  order by t3.Position,t1.Section,t1.rollnumber asc";
                }
                else //Bus
                {
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as [Student Name],t1.class as [Class],t1.Section,t1.rollnumber as [Roll No.],t1.fathername as [Father Name],t1.father_mob as [Mobile No.],t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t3.Position,t1.Section,t1.rollnumber asc";
                }
            }
            // }
            //else
            //{
            //    if (ddl_fee_type.SelectedItem.Text == "ALL")
            //    {
            //        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //    }
            //    else
            //    {
            //        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.From_month_name as Month_name,t2.From_month_id as Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Hostel_assign_master t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Status='1' and t1.hosteltaken='Yes' and t2.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Status='1'  and t1.hosteltaken='No' and t1.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Status='1'  and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id and Istatus='1') order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //        else //Bus
            //        {
            //            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' and t1.Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
            //        }
            //    }
            //}
            //}
            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            int srowcount = dt.Rows.Count;
            int mgrowcount1 = rp_month.Items.Count;
            if (srowcount > 0)
            {
                dt.Columns.Add("Last Payment", Type.GetType("System.String"));
                fdt.Columns.Add("Last Payment", Type.GetType("System.String"));
                int kls = 0;
                for (int ixi = 0; ixi < mgrowcount1; ixi++)
                {
                    CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                    if (chkM.Checked == true)
                    {
                        Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                        Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                        dt.Columns.Add(lbl_month_name.Text, Type.GetType("System.Double"));
                        fdt.Columns.Add(lbl_month_name.Text, Type.GetType("System.Double"));
                    }
                    else
                    {
                        kls++;
                    }
                }
                if (ddl_calculate_with_Fine.SelectedValue == "1")
                {
                    dt.Columns.Add("Late Fine", Type.GetType("System.Double"));
                    fdt.Columns.Add("Late Fine", Type.GetType("System.Double"));
                }


                dt.Columns.Add("Total", Type.GetType("System.Double"));
                fdt.Columns.Add("Total", Type.GetType("System.Double"));

                if (kls == mgrowcount1)
                {
                    Alertme("Please check minimum one month.", "warning");
                    return;
                }


                foreach (DataRow dr in dt.Rows)
                {
                    string last_date_of_payment = get_last_date_of_pay(dr["Admission_no"].ToString(), dr["Class_id"].ToString(), ddl_session.SelectedItem.Text);
                    dr["Last Payment"] = last_date_of_payment;
                    double total_amt = 0;
                    int mgrowcount = rp_month.Items.Count;
                    for (int ixi = 0; ixi < mgrowcount; ixi++)
                    {
                        CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                        if (chkM.Checked == true)
                        {
                            Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                            Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");

                            dr[lbl_month_name.Text] = find_dues(lbl_month_name.Text, lbl_month_id.Text, dr);
                            total_amt += My.toDouble(dr[lbl_month_name.Text].ToString());
                        }
                    }

                    if (ddl_calculate_with_Fine.SelectedValue == "1")
                    {
                        double fine_amt = 0;
                        if (total_amt > 0)
                        {
                            fine_calculations(dr["Admission_no"].ToString(), ViewState["RepeatFine"].ToString());
                            DataTable dtFine = My.dataTable("select isnull(sum(convert(float, Fine_amount)),0) as Fine_amt from Temp_fine_monthwise where Admission_no='" + dr["Admission_no"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                            if (dtFine.Rows.Count > 0)
                            {
                                fine_amt = My.toDouble(dtFine.Rows[0]["Fine_amt"].ToString());
                            }
                        }
                        dr["Late Fine"] = fine_amt.ToString("0.00");
                        total_amt = total_amt + fine_amt;
                    }

                    dr["total"] = total_amt.ToString("0.00");
                    if (total_amt == 0)
                    {
                        dr.Delete();
                    }
                }
                //================
                dt.AcceptChanges();
                if (dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt.DefaultView;
                    GridView2.DataBind();

                    ViewState["fttable"] = dt;
                    btn_excels.Visible = true;

                    bool sendsmspnl = false;
                    if (ViewState["Is_Send_SMS"].ToString().ToUpper() == "TRUE")
                    {
                        sendsmspnl = true;
                        rd_sms.Visible = true;
                    }
                    else
                    {
                        rd_sms.Visible = false;
                        rd_sms.Checked = false;
                        rd_whatassp.Checked = true;
                    }

                    if (ViewState["Is_Send_WhatsApp"].ToString().ToUpper() == "TRUE")
                    {
                        rd_whatassp.Checked = true;
                        sendsmspnl = true;
                        rd_whatassp.Visible = true;
                    }
                    else
                    {
                        rd_whatassp.Visible = false;
                        rd_whatassp.Checked = false;
                    }
                    if (sendsmspnl == true)
                    {
                        sendsms.Visible = true;
                    }
                    else
                    {
                        sendsms.Visible = false;

                    }



                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        print1.Visible = true;
                    }
                    else
                    {
                        print1.Visible = false;
                    }

                    //GridView2.Columns.FromKey()
                    //GridView2.Columns[7].Visible = false;
                    //String Total_mrp = Convert.ToDouble(dt.Compute("SUM(March)", string.Empty)).ToString();


                    double total = 0;
                    GridView2.FooterRow.Cells[8].Text = "Total";
                    GridView2.FooterRow.Cells[8].Font.Bold = true;
                    GridView2.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                    for (int k = 20; k < dt.Columns.Count; k++)
                    {
                        total = dt.AsEnumerable().Sum(row => row.Field<Double>(dt.Columns[k].ToString()));
                        GridView2.FooterRow.Cells[k].Text = total.ToString("0.00");
                        GridView2.FooterRow.Cells[k].Font.Bold = true;
                        GridView2.FooterRow.BackColor = System.Drawing.Color.Beige;
                    }
                }
                else
                {
                    sendsms.Visible = false;
                    print1.Visible = false;
                    Alertme("Student dues not found.", "warning");
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }
            else
            {
                sendsms.Visible = false;
                print1.Visible = false;
                Alertme("Student not found.", "warning");
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }

        private void fine_calculations(string admission_no, string RepeatFine)
        {
            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                string qry = "delete from Temp_fine_monthwise where Session_id='" + ddl_session.SelectedValue + "' and Admission_no='" + admission_no + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                My.exeSql(qry);

                #region DayRanGEWise
                string pay_date = mycode.date();
                int payidate = My.DateConvertToIdate(pay_date);


                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = ddl_session.SelectedItem.Text;
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_yearFx = s_year;

                string CheckedMonth = "0"; string CheckedMonthN = "0";
                int mgrowcount = rp_month.Items.Count;
                for (int ixi = 0; ixi < mgrowcount; ixi++)
                {
                    if (CheckedMonth == "0")
                    {
                        CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                        if (chkM.Checked == true)
                        {
                            Label lbl_Month = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                            CheckedMonthN = lbl_Month.Text;
                            CheckedMonth = "1";
                        }
                    }
                }

                int mnth_idss = My.tomonth_number(CheckedMonthN);
                string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());
                int pay_month = My.toint(pay_month_two_digit);
                s_year = My.check_start_months(pay_month, s_year);

                int pay_month_with_year = My.toint(s_year + pay_month_two_digit);
                int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                //Advance Payment Check



                if (crnt_month_with_year >= pay_month_with_year)
                {
                    string isCalculated = "0";
                    int mgrowcounts = rp_month.Items.Count;
                    for (int ixi = 0; ixi < mgrowcounts; ixi++)
                    {
                        CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                        if (chkM.Checked == true)
                        {
                            if (isCalculated == "0")
                            {
                                Label lbl_Month = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                                mnth_idss = My.tomonth_number(lbl_Month.Text);
                                pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());
                                int last_day_of_payments_year = My.check_start_months(My.toInt(pay_month_two_digit), s_yearFx);

                                DataTable dtz = mycode.FillData("select top 1 * from Fine_master_day_range");
                                if (dtz.Rows.Count != 0)
                                {
                                    ViewState["FineType"] = "DayWise";
                                    string last_day_of_payments = "01" + "/" + pay_month_two_digit + "/" + last_day_of_payments_year;

                                    DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                    System.TimeSpan diff = enddate1.Subtract(startdate1);
                                    int totaldays = Convert.ToInt32(diff.Days);



                                    if (ViewState["Is_quarterwise_payment"].ToString() == "1")
                                    {
                                        if (lbl_Month.Text == "April" || lbl_Month.Text == "July" || lbl_Month.Text == "October" || lbl_Month.Text == "January")
                                        {
                                            if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee')").Rows.Count > 0)
                                            {
                                            }
                                            else
                                            {
                                                isCalculated = "1";
                                                DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                                                if (dt_fine.Rows.Count != 0)
                                                {
                                                    save_fine_amount(ddl_session.SelectedValue, admission_no, pay_month_two_digit, My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()), ViewState["Branchid"].ToString());
                                                }
                                                else
                                                {

                                                    DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                                                    if (dt_fines.Rows.Count != 0)
                                                    {
                                                        save_fine_amount(ddl_session.SelectedValue, admission_no, pay_month_two_digit, My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()), ViewState["Branchid"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee')").Rows.Count > 0)
                                        {
                                        }
                                        else
                                        {
                                            isCalculated = "1";
                                            DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                                            if (dt_fine.Rows.Count != 0)
                                            {
                                                save_fine_amount(ddl_session.SelectedValue, admission_no, pay_month_two_digit, My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()), ViewState["Branchid"].ToString());
                                            }
                                            else
                                            {

                                                DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                                                if (dt_fines.Rows.Count != 0)
                                                {
                                                    save_fine_amount(ddl_session.SelectedValue, admission_no, pay_month_two_digit, My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()), ViewState["Branchid"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region FINES
                DataTable dt = mycode.FillData("select top 1 * from Fine_master where Status='1' and Session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count != 0)
                {
                    string pay_date = mycode.date();
                    int payidate = My.DateConvertToIdate(pay_date);
                    string fineType = dt.Rows[0]["Fine_type"].ToString();

                    if (fineType == "MonthWise") //===== MonthWise
                    {
                        #region MonthWise
                        string qry = "delete from Temp_fine_monthwise where Session_id='" + ddl_session.SelectedValue + "' and Admission_no='" + admission_no + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                        My.exeSql(qry);

                        int mgrowcount = rp_month.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                            if (chkM.Checked == true)
                            {
                                string cunrt_session = ddl_session.SelectedItem.Text;
                                string session_frst_year = cunrt_session.Substring(0, 4);
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);
                                int s_yearFx = s_year;
                                Label lbl_Month = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                                int mnth_idss = My.tomonth_number(lbl_Month.Text);
                                string pay_month_two_digit = My.getMonthS_twoDigit(mnth_idss.ToString());

                                int pay_month = My.toint(pay_month_two_digit);
                                s_year = My.check_start_months(pay_month, s_year);

                                ViewState["FineType"] = "MonthWise";
                                string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");

                                int last_day_of_payments_year = My.check_start_months(My.toInt(pay_month_two_digit), s_yearFx);
                                string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + last_day_of_payments_year;
                                DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                                int monthsDifference = GetMonthsDifference(startdate1, enddate1);
                                int monthNumber = startdate1.Month;
                                string till_date_paymnt = dt.Rows[0]["Last_day_to_deposit_fees"].ToString();
                                double ttl_fine = 0; bool isPaidMonth = false;
                                for (int m = 0; m < monthsDifference; m++)
                                {
                                    if (isPaidMonth == false)
                                    {
                                        string monthNumbertwodgt = My.getMonthS_twoDigit(monthNumber.ToString());
                                        string monthName = My.getMonthS_full_name(monthNumbertwodgt);
                                        int updated_year = My.check_start_months(My.toInt(monthNumbertwodgt), s_yearFx);
                                        string last_idate_of_payments = updated_year.ToString() + monthNumbertwodgt + till_date_paymnt;


                                        int pay_month_with_years = My.toint(updated_year + monthNumbertwodgt);
                                        int fine_aplicable_years = My.check_start_months(My.toInt(applicable_month), s_yearFx);
                                        string fine_applicable_months = fine_aplicable_years + applicable_month;
                                        if (My.toint(fine_applicable_months) <= pay_month_with_years)
                                        {
                                            if (RepeatFine == "Yes")
                                            {
                                                if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + monthName + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee')").Rows.Count > 0)
                                                {
                                                    isPaidMonth = true;
                                                }
                                                else
                                                {
                                                    if (My.toIntS(last_idate_of_payments) < payidate)
                                                    {
                                                        ttl_fine = ttl_fine + My.toDouble(dt.Rows[0]["Fine_amt_per_day_or_month"].ToString());
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + monthName + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee')").Rows.Count > 0)
                                                {
                                                    isPaidMonth = true;
                                                }
                                                else
                                                {
                                                    if (My.toIntS(last_idate_of_payments) < payidate)
                                                    {
                                                        ttl_fine = My.toDouble(dt.Rows[0]["Fine_amt_per_day_or_month"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    monthNumber++;
                                    if (monthNumber == 13)
                                    {
                                        monthNumber = 1;
                                    }
                                }

                                if (ttl_fine > 0)
                                {
                                    save_fine_amount(ddl_session.SelectedValue, admission_no, pay_month_two_digit, ttl_fine, ViewState["Branchid"].ToString());
                                }
                                else
                                {
                                    save_fine_amount(ddl_session.SelectedValue, admission_no, pay_month_two_digit, 0, ViewState["Branchid"].ToString());
                                }
                            }
                        }
                        #endregion
                    }
                    if (fineType == "DayWise")
                    {
                        #region DayWise

                        string qry = "delete from Temp_fine_monthwise where Session_id='" + ddl_session.SelectedValue + "' and Admission_no='" + admission_no + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                        My.exeSql(qry);

                        string isCalculated = "0";
                        int mgrowcount = rp_month.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                            if (chkM.Checked == true)
                            {
                                if (isCalculated == "0")
                                {
                                    string cunrt_session = ddl_session.SelectedItem.Text;
                                    string session_frst_year = cunrt_session.Substring(0, 4);
                                    int session_s_year = My.toint(session_frst_year);
                                    int s_year = My.toint(session_frst_year);
                                    Label lbl_Month = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                                    int mnth_idss = My.tomonth_number(lbl_Month.Text);
                                    string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());

                                    int pay_month = My.toint(month_id_in_two_dgts);
                                    s_year = My.check_start_months(pay_month, s_year);

                                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and transection!=''").Rows.Count > 0)
                                    {
                                    }
                                    else
                                    {
                                        DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        string paymentMonthno = enddate1q.ToString("MM");

                                        int pay_month_with_year = My.toint(s_year + month_id_in_two_dgts);
                                        int crnt_month_with_year = My.toint(mycode.year() + paymentMonthno);
                                        //Advance Payment Check 
                                        int fine_aplicable_year = My.check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_year);

                                        if (crnt_month_with_year >= pay_month_with_year)
                                        {
                                            ViewState["FineType"] = "DayWise";
                                            string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                            string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;
                                            string fine_applicable_month = fine_aplicable_year + applicable_month;
                                            if (My.toint(fine_applicable_month) <= pay_month_with_year)
                                            {
                                                string last_day_of_payments = "";
                                                if (My.toint(fine_applicable_month) >= pay_month_with_year)
                                                {
                                                    last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;
                                                }
                                                else
                                                {
                                                    last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;
                                                }
                                                DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                                DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                                System.TimeSpan diff = enddate1.Subtract(startdate1);
                                                int totaldays = Convert.ToInt32(diff.Days);


                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    isCalculated = "1";
                                                    string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                                    save_fine_amount(ddl_session.SelectedValue, admission_no, month_id_in_two_dgts, ttl_fine_amt, ViewState["Branchid"].ToString());
                                                }
                                                else
                                                {
                                                    isCalculated = "1";
                                                    save_fine_amount(ddl_session.SelectedValue, admission_no, month_id_in_two_dgts, 0, ViewState["Branchid"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    if (fineType == "QuarterWise")
                    {
                        string qry = "delete from Temp_fine_monthwise where Session_id = '" + ddl_session.SelectedValue + "' and Admission_no = '" + admission_no + "' and Branch_id = '" + ViewState["Branchid"].ToString() + "'";
                        My.exeSql(qry);


                        #region QuarterWise
                        string uncheckd = "1";
                        int mgrowcount = rp_month.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                            if (chkM.Checked == true)
                            {
                                uncheckd = "0";
                                string cunrt_session = ddl_session.SelectedItem.Text;
                                string session_frst_year = cunrt_session.Substring(0, 4);
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);

                                Label lbl_Month = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                                int mnth_idss = My.tomonth_number(lbl_Month.Text);
                                string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());

                                int pay_month = My.toint(month_id_in_two_dgts);
                                s_year = My.check_start_months(pay_month, s_year);

                                if (My.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + lbl_Month.Text + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and transection!=''").Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    #region QuarterWise
                                    ViewState["FineType"] = "QuarterWise";
                                    double fnl_fine_amt = 0;
                                    SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + ddl_session.SelectedValue + "' and Q_start_month='" + month_id_in_two_dgts + "' and Q_start_year='" + s_year + "'  order by Q_start_month asc", My.conn);
                                    DataSet ds = new DataSet();
                                    ad.Fill(ds, "Fine_master");
                                    DataTable dtm = ds.Tables[0];
                                    int rowcount = ds.Tables[0].Rows.Count;
                                    if (rowcount == 0)
                                    {
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in dtm.Rows)
                                        {
                                            string endmnth_in_two = My.getMonthS_twoDigit(dr["Q_start_month"].ToString());
                                            string last_day_of_payment_q = dr["Last_day_to_deposit_fees"].ToString() + "/" + endmnth_in_two + "/" + dr["Q_start_year"].ToString();


                                            DateTime startdate1q = DateTime.ParseExact(last_day_of_payment_q, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                            if (dr["Q_payment_mode"].ToString() == "Day")
                                            {
                                                System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                                int totaldays = Convert.ToInt32(diff.Days);
                                                ViewState["late_fine_no_of_day_month"] = totaldays;

                                                if (ViewState["flags1"].ToString() == "0")
                                                {
                                                    ViewState["fine_date_From"] = last_day_of_payment_q;
                                                    ViewState["flags1"] = "1";
                                                }

                                                ViewState["fine_date_To"] = pay_date;

                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                                    fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                                }
                                                else
                                                {
                                                    fnl_fine_amt = fnl_fine_amt += 0;
                                                }
                                            }
                                            else
                                            {
                                                System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                                int totaldays = Convert.ToInt32(diff.Days);
                                                ViewState["late_fine_no_of_day_month"] = dtm.Rows.Count.ToString();

                                                if (ViewState["flags1"].ToString() == "0")
                                                {
                                                    ViewState["fine_date_From"] = last_day_of_payment_q;
                                                    ViewState["flags1"] = "1";
                                                }

                                                ViewState["fine_date_To"] = pay_date;
                                                if (My.toDouble(totaldays) > 0)
                                                {
                                                    string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                                    double ttl_fine_amt = My.toDouble(fine_amt);
                                                    fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                                }
                                                else
                                                {
                                                    fnl_fine_amt = fnl_fine_amt += 0;
                                                }
                                            }
                                        }
                                        save_fine_amount(ddl_session.SelectedValue, admission_no, month_id_in_two_dgts, fnl_fine_amt, ViewState["Branchid"].ToString());
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion
            }
        }

        static int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            // Calculate the total months difference
            int monthsApart = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;

            // If the day of the end date is less than the day of the start date, subtract one month
            if (monthsApart <= 0)
            {
                monthsApart = 1;
            }
            else
            {
                monthsApart++;
            }
            return monthsApart;
        }

        private void save_fine_amount(string session_id, string admission_no, string month_id_in_two_dgts, double ttl_fine_amt, string Branch_id)
        {
            if (mycode.IsUserExist("select Id from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + Branch_id + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Temp_fine_monthwise (Session_id,Admission_no,Month_id,Fine_amount,Branch_id) values (@Session_id,@Admission_no,@Month_id,@Fine_amount,@Branch_id);";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Month_id", month_id_in_two_dgts);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Temp_fine_monthwise set Fine_amount=@Fine_amount  where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + Branch_id + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        private string get_last_date_of_pay(string adm_No, string class_id, string session)
        {
            string lastDate = "NA";
            DataTable dt = My.dataTable("select top 1 Date from Student_Payment_History where Addmission_no='" + adm_No + "' and Class_id='" + class_id + "' and Session='" + session + "' order by Idate desc ");
            if (dt.Rows.Count > 0)
            {
                lastDate = dt.Rows[0]["Date"].ToString();
            }
            return lastDate;
        }

        private string find_dues(string month, string month_id, DataRow dr)
        {
            DataTable feedt = new DataTable();
            if (dr["Transportation_Id"].ToString() == "")
            {
                ViewState["transportID"] = "0";
            }
            else
            {
                ViewState["transportID"] = dr["Transportation_Id"].ToString();
            }
            ViewState["parameter"] = dr["hosteltaken"].ToString().ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
            ViewState["IsBoarding"] = "0";
            ViewState["parameteridS"] = "4";
            if (dr["Month_id"].ToString() != "")
            {
                ViewState["LunchMnthName"] = dr["Month_name"].ToString();
                ViewState["LunchMnthId"] = dr["Month_id"].ToString();
                ViewState["IsBoarding"] = "1";
            }


            string dues = "0";
            //====================
            if (ViewState["IsBoarding"].ToString() == "1")
            {
                int mnthids = My.toint(month_id);
                if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                {
                    ViewState["parameteridS"] = "44";
                }
                else
                {
                    ViewState["parameteridS"] = "4";
                }
            }
            string session = ddl_session.SelectedItem.Text;
            string type = "";
            if (My.dataTable("select * from dbo.[Typewise_fee_collection] where admission_no='" + dr["Admission_no"].ToString() + "' and class_id=" + dr["Class_id"].ToString() + " and session='" + ddl_session.SelectedItem.Text + "' and month='" + month + "' and (parameter='" + ViewState["parameter"].ToString() + "' or parameter='HostelMonthlyFee')").Rows.Count > 0)
            {
                //feedt = My.dataTable("select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,isnull(paid,'0') previously_paid,(select  sum(convert(float, disc_amt)) from Monthly_Fee_Collection_Slip where slipno=Typewise_fee_collection.transection and content_id=Typewise_fee_collection.content_id and Content= Typewise_fee_collection.feetype and Month=Typewise_fee_collection.month  and session='" + session + "') as disc_amount from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee' and class_id=" + dr["Class_id"].ToString() + ") and content_id!='6121') t");
                feedt = My.dataTable("select isnull((sum(convert(float, payable))-sum(convert(float, isnull((Disc),'0')))-sum(convert(float, isnull((paid),'0')))),0) as Total_Dues from (select * from dbo.[Typewise_fee_collection] where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and class_id=" + dr["Class_id"].ToString() + " and content_id!='6121') t");
                if (feedt.Rows.Count.ToString() != "0")
                {
                    dues = feedt.Rows[0]["Total_Dues"].ToString();
                }
                else
                {
                    dues = "0";
                }
            }
            else
            {
                string cunrt_session = ddl_session.SelectedItem.Text;
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);

                Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ddl_session.SelectedValue, dr["Class_id"].ToString(), dr["Admission_no"].ToString());
                ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                ViewState["From_month_name"] = (String)dc1["From_month_name"];
                ViewState["From_month_id"] = (String)dc1["From_month_id"];
                ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];

                if (ViewState["Hostel_id"].ToString() == "0")
                { ViewState["IsHostelTaken"] = "No"; }
                else { ViewState["IsHostelTaken"] = "Yes"; }

                Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(ddl_session.SelectedValue, dr["Class_id"].ToString(), dr["Admission_no"].ToString());

                ViewState["Transport_id"] = (String)dc2["Transport_id"];
                ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                ViewState["Month_name"] = (String)dc2["Month_name"];
                ViewState["Month_id"] = (String)dc2["Month_id"];
                ViewState["Year_month"] = (String)dc2["Year_month"];
                ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];



                //dr["bac_colour"] = "White";


                Dictionary<string, object> dc = new Dictionary<string, object>();
                dc["admission_no"] = dr["Admission_no"].ToString();
                dc["session_id"] = ddl_session.SelectedValue;
                dc["class"] = dr["class"].ToString();
                dc["session"] = session;
                dc["class_id"] = dr["Class_id"].ToString();
                dc["hosteltaken"] = ViewState["IsHostelTaken"].ToString().ToLower();
                dc["months"] = month;
                dc["tr_ledger"] = My.is_combine ? "School" : "Transport";

                dc["day_boarding"] = My.toBool(dr["is_applied_dayboarding"]);
                dc["day_boarding_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                dc["category_id"] = dr["category_id"].ToString();
                dc["sub_category_id"] = dr["SubCategory_id"].ToString();


                dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                dc["parameter_id"] = ViewState["parameteridS"].ToString();

                string monthid = My.tomonth_numberstring(month);
                int pay_month = My.toint(monthid);
                s_year = My.check_start_months(pay_month, s_year);
                dc["monthid"] = s_year + monthid;

                dc["sp_status"] = "1";

                feedt = My.dataTableSP("sp_Fetch_month_dues", dc);

                if (feedt.Rows.Count.ToString() != "0")
                {
                    dues = feedt.Rows[0]["Total_Dues"].ToString();
                }
                else
                {
                    dues = "0";
                }
                if (My.toDouble(dues) == 0)
                {
                    dues = "0";
                }
            }
            return dues;
        }






        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Label lbl_footr_totalCollection = (Label)e.Row.FindControl("lbl_footr_totalCollection");
                //Label lbl_Depositr_totalCollection = (Label)e.Row.FindControl("lbl_Depositr_totalCollection");

                //lbl_footr_totalCollection.Text = total_collection.ToString();
                //lbl_Depositr_totalCollection.Text = total_deposit.ToString();
            }
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;

            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;

        }


        protected void btn_empty_Click(object sender, EventArgs e)
        {
            add_sl_no();
        }


        int slnos = 1;
        private void add_sl_no()
        {
            try
            {
                foreach (GridViewRow row in GridView2.Rows)
                {
                    row.Cells[0].Text = slnos.ToString();
                    slnos++;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridView2_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        for (int i = 9; i < e.Row.Cells.Count; i++)
            //        {
            //            decimal value;
            //            if (decimal.TryParse(e.Row.Cells[i].Text.Trim(), out value))
            //            {
            //                e.Row.Cells[i].Text = value.ToString("0.00");
            //            }
            //        }
            //    }
            //}
            //catch
            //{ }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Dues_List_Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_excel.RenderControl(hw);
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

        protected void GridView2_RowDataBound2(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 9; i < e.Row.Cells.Count; i++)
                    {
                        decimal value;
                        if (decimal.TryParse(e.Row.Cells[i].Text.Trim(), out value))
                        {
                            e.Row.Cells[i].Text = value.ToString("0.00");
                        }
                    }
                }


            }
            catch
            { }
        }



        #region send message
        protected void btn_msgPreview_Click(object sender, EventArgs e)
        {
            try
            {




                var vrls = ViewState["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = txt_0.Text;
                }
                if (vrls.Length > 1)
                {
                    lst[1] = txt_1.Text;
                }
                if (vrls.Length > 2)
                {
                    lst[2] = txt_2.Text;
                }
                if (vrls.Length > 3)
                {

                    lst[3] = txt_3.Text;
                }
                if (vrls.Length > 4)
                {

                    lst[4] = txt_4.Text;
                }
                if (vrls.Length > 5)
                {

                    lst[5] = txt_5.Text;
                }
                if (vrls.Length > 6)
                {

                    lst[6] = txt_6.Text;
                }
                if (vrls.Length > 7)
                {

                    lst[7] = txt_7.Text;
                }
                if (vrls.Length > 8)
                {

                    lst[8] = txt_8.Text;
                }
                txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                pnl_msg.Visible = true;


            }
            catch (Exception ex)
            {

            }


        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool send = false;
                if (rd_sms.Checked == true)
                {
                    var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        ViewState["api_key"] = dt.Rows[0]["uid"].ToString();
                        ViewState["Sender_id"] = dt.Rows[0]["sender"].ToString();

                        send = true;
                    }
                    else
                    {
                        this.Alertme("Please set sms configuration", "warning");
                        return;

                    }
                }
                else if (rd_whatassp.Checked == true)
                {
                    var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        send = true;



                        ViewState["whatsapp_mobile_no"] = dt.Rows[0]["SMS_API"].ToString();
                        ViewState["Whatsapp_api_url"] = dt.Rows[0]["url"].ToString();


                    }
                    else
                    {
                        this.Alertme("Please set Whatsapp configuration", "warning");
                        return;
                    }
                }
                else
                {
                    this.Alertme("Please select sms or whatsapp", "warning");
                    return;
                }



                if (send == true)
                {


                    string confirmValue = string.Empty;
                    confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {

                        if (GridView2.Rows.Count == 0)
                        {
                            Alertme("Please dues calculated", "warning");
                            return;
                        }
                        else
                        {
                            //foreach (GridViewRow row in GridView2.Rows)
                            //{
                            //string class12 = GridView2.Rows[GridView2.Rows.Count - 1].Cells[4].Text


                            DataTable dt = (DataTable)ViewState["fttable"];

                            var index = dt.Columns.Count - 1;
                            string monthname = dt.Columns[index - 1].ColumnName; ;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string Admission_no = dt.Rows[i][1].ToString(); ///GridView2.Rows[i].Cells[1].Text;
                                string class_name = dt.Rows[i][2].ToString();
                                string father_mob = dt.Rows[i][3].ToString();
                                string Section = dt.Rows[i][4].ToString();
                                string rollnumber = dt.Rows[i][5].ToString();
                                string Student_Name = dt.Rows[i][6].ToString();

                                string total = dt.Rows[i][index].ToString();

                                sendSMS(Admission_no, class_name, father_mob, Section, rollnumber, Student_Name, total, monthname);



                            }
                            this.Alertme("SMS Send Successfully", "success");
                            pnl_msg.Visible = false;
                            sendsms.Visible = false;

                        }
                    }
                    else
                    {
                        Alertme("Sorry you have clicked no", "warning");
                    }
                }
                else
                {
                    this.Alertme("Please check sms and whatsapp configuration", "warning");
                }
            }
            catch
            {

            }


        }

        private void sendSMS(string admission_no, string class_name, string father_mob, string section, string rollnumber, string student_Name, string total, string monthname)
        {
            if (father_mob == "N/A")
            {

            }
            else if (father_mob == "")
            {

            }
            else if (My.toDouble(total) == 0)
            {

            }
            else
            {
                string mobno = father_mob;
                string admissionId = admission_no;
                txt_0.Text = student_Name;
                txt_1.Text = ViewState["firm_name"].ToString();
                txt_2.Text = class_name;
                txt_3.Text = section;
                txt_4.Text = rollnumber;
                txt_5.Text = admissionId;
                txt_6.Text = monthname;
                txt_7.Text = total;

                //txt_5.Text = student_Name;
                //txt_6.Text = student_Name;
                //txt_7.Text = student_Name;
                //txt_8.Text = student_Name;


                string type = "";

                if (ViewState["SMSType"].ToString() == "Unicode")
                {
                    type = "unicode";
                }
                else
                {
                    type = "english";
                }
                ViewState["type"] = type;


                var vrls = ViewState["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = txt_0.Text;
                }
                if (vrls.Length > 1)
                {
                    lst[1] = txt_1.Text;
                }
                if (vrls.Length > 2)
                {
                    lst[2] = txt_2.Text;
                }
                if (vrls.Length > 3)
                {

                    lst[3] = txt_3.Text;
                }
                if (vrls.Length > 4)
                {

                    lst[4] = txt_4.Text;
                }
                if (vrls.Length > 5)
                {

                    lst[5] = txt_5.Text;
                }
                if (vrls.Length > 6)
                {

                    lst[6] = txt_6.Text;
                }

                if (vrls.Length > 7)
                {

                    lst[7] = txt_7.Text;
                }
                if (vrls.Length > 8)
                {

                    lst[8] = txt_8.Text;
                }
                txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);




                try
                {





                    if (rd_sms.Checked == true)
                    {
                        string api_key = ViewState["api_key"].ToString();
                        string Sender_id = ViewState["Sender_id"].ToString();
                        string msgtype = ViewState["type"].ToString();


                        string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + txt_message.Text + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobno + "&smsContentType=" + type;

                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        this.Alertme("SMS Send Successfully", "success");
                        My.Insert("Message_Details", new
                        {
                            Mobile_No = mobno,
                            Message = txt_message.Text,
                            Date = mycode.date(),
                            Idate = mycode.idate(),
                            Time = mycode.time(),
                            Result = results,
                            User_id = ViewState["Userid"].ToString(),
                            Mesage_Type = msgtype,
                            Groupcode = "SMS",
                            Status = "SEND",
                            Url = url,
                            Message_to_Type = "Student",
                            admin_user_id = admissionId,
                        });
                    }
                    else if (rd_whatassp.Checked == true)
                    {
                        string sms = String.Format(ViewState["SMS_Tempate"].ToString(), lst);

                        try
                        {
                            string mobilewhatsapp = My.get_whatsapp(ddl_session.SelectedValue, admission_no);
                            string mobile_no = "91" + mobilewhatsapp;
                            if (mobile_no.Length > 9)
                            {
                                string message = Uri.EscapeDataString(sms);

                                string _url = "";




                                if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                                {
                                    //exampe url
                                    //https://app.allexpert.in/send-message?api_key=q0hRX9FA1z5S7JttEIzLtxJU3lvoOv&sender=62888xxxx&number=9162888xxxx&message=Hello World
                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, message);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }
                                if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                                {
                                    // _url = My.Whatsapp_api_url + My.whatsapp_mobile_no + "&message=" + message + "&phone=91" + mobile_no;
                                    //https://api4ws.com/sendMessage.php?AUTH_KEY=918877804016&message=User Defined Whatsapp Message&phone=919812345678
                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }
                                else
                                {

                                    //https://fastwsapi.com/sendMessage.php?AUTH_KEY=DEMOEDUNEXTG@123&instance_id=575051&message=User Defined Whatsapp Message&phone=919812345678

                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }


                                //ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                string results = sr.ReadToEnd();
                                sr.Close();

                                My.Insert("Message_Details", new
                                {
                                    Mobile_No = mobilewhatsapp,
                                    Message = txt_message.Text,
                                    Date = mycode.date(),
                                    Idate = mycode.idate(),
                                    Time = mycode.time(),
                                    Result = results,
                                    User_id = ViewState["Userid"].ToString(),
                                    Mesage_Type = ViewState["type"].ToString(),
                                    Groupcode = "Wahataap",
                                    Status = "SEND",
                                    Url = _url,
                                    Message_to_Type = "Student",
                                    admin_user_id = admissionId,

                                });

                            }
                            //return true;
                        }
                        catch (Exception ex)
                        {
                            My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                            //return false;
                        }


                    }
                    else
                    {

                    }




                }
                catch (Exception ex)
                {
                    this.Alertme(ex.Message, "warning");
                }

            }



        }
        #endregion




    }
}