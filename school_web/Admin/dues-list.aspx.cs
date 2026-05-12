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
    public partial class dues_list : System.Web.UI.Page
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
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        #region sms
                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Transport Dues");
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



                        lbl_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
                        ddl_session.SelectedValue = My.get_session_id();
                        bind_month();
                        find_firm_details();





                        Dictionary<string, object> dc2 = mycode.Firm_details();
                        ViewState["firm_name"] = (String)dc2["firm_name"];

                      
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
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
            {
                lbl_date.Text = " " + mycode.date() + " " + ddl_class.SelectedItem.Text;
                find_student_dues_by_month();
            }
            scrpt = "<script> click_empty();</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        private void find_student_dues_by_month()
        {
            DataTable fdt = new DataTable();
            fdt.Columns.Add("Admission_no");
            fdt.Columns.Add("Session");
            fdt.Columns.Add("Class");


            string lbl_class_id = ddl_class.SelectedValue;
            string qry = "";

            if (ddl_class.SelectedItem.Text == "ALL")
            {
                if (ddl_section.SelectedItem.Text != "ALL")
                {
                    Alertme("Please select class.", "warning");
                    ddl_class.Focus();
                    return;
                }

                if (ddl_fee_type.SelectedItem.Text == "ALL")
                {
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
                }
                else
                {
                    if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.From_month_name as Month_name,t2.From_month_id as Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Hostel_assign_master t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t2.Status='1' and t1.hosteltaken='Yes' order by t3.Position,t1.Section,t1.rollnumber asc";
                    }
                    else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "'   and Status='1' and t1.hosteltaken='No'  order by t3.Position,t1.Section,t1.rollnumber asc";
                    }
                    else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position,t1.Section,t1.rollnumber asc";
                    }
                    else //Bus
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t3.Position,t1.Section,t1.rollnumber asc";
                    }
                }
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddl_fee_type.SelectedItem.Text == "ALL")
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
                    }
                    else
                    {
                        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.From_month_name as Month_name,t2.From_month_id as Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Hostel_assign_master t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t2.Status='1' and t1.hosteltaken='Yes' order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No'  order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                        else //Bus
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                    }
                }
                else
                {
                    if (ddl_fee_type.SelectedItem.Text == "ALL")
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' order by t3.Position,t1.Section,t1.rollnumber asc";
                    }
                    else
                    {
                        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.From_month_name as Month_name,t2.From_month_id as Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Hostel_assign_master t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t2.Status='1' and t1.hosteltaken='Yes' order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1'  and t1.hosteltaken='No' order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1'  and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                        else //Bus
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t3.Position,t1.Section,t1.rollnumber asc";
                        }
                    }
                }
            }
            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            int srowcount = dt.Rows.Count;
            int mgrowcount1 = rp_month.Items.Count;
            if (srowcount > 0)
            { 
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
                dt.Columns.Add("Total", Type.GetType("System.Double"));
                fdt.Columns.Add("Total", Type.GetType("System.Double"));

                if (kls == mgrowcount1)
                {
                    Alertme("Please check minimum one month.", "warning");
                    return;
                }


                foreach (DataRow dr in dt.Rows)
                { 
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
                    sendsms.Visible = true;
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
                    GridView2.FooterRow.Cells[6].Text = "Total";
                    GridView2.FooterRow.Cells[6].Font.Bold = true;
                    GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    for (int k = 18; k < dt.Columns.Count; k++)
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
            if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and class_id=" + dr["Class_id"].ToString() + " and session='" + ddl_session.SelectedItem.Text + "' and month='" + month + "' and (parameter='" + ViewState["parameter"].ToString() + "' or parameter='HostelMonthlyFee') and content_id='1002'").Rows.Count > 0)
            {
                feedt = My.dataTable("select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,isnull(paid,'0') previously_paid,(select  sum(convert(float, disc_amt)) from Monthly_Fee_Collection_Slip where slipno=Typewise_fee_collection.transection and content_id=Typewise_fee_collection.content_id and Content= Typewise_fee_collection.feetype and Month=Typewise_fee_collection.month  and session='" + session + "') as disc_amount from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and class_id=" + dr["Class_id"].ToString() + " and content_id='1002') t");
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
                feedt = My.dataTableSP("sp_Fetch_transport_dues", dc); 
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
            e.Row.Cells[7].Visible = false;
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
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 7; i < e.Row.Cells.Count; i++)
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
                        GridView2.RenderControl(hw);
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
                    for (int i = 7; i < e.Row.Cells.Count; i++)
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
                        }
                        else
                        {
                            
                            

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
                            if (father_mob.Length > 9)
                            {
                                string message = Uri.EscapeDataString(sms);
                                string mobile_no = "91" + My.get_whatsapp(ddl_session.SelectedValue, admission_no);
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
                                    Mobile_No = mobno,
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