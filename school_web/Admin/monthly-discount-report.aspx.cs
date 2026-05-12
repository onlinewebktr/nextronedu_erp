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

namespace school_web.Admin
{
    public partial class monthly_discount_report : System.Web.UI.Page
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
                        string pagename_current = "admission-discount-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        lbl_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
                        ddl_session.SelectedValue = My.get_session_id();
                        bind_month();
                        find_firm_details();
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
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' order by t3.Position asc";
                }
                else
                {
                    if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='Yes' order by t3.Position asc";
                    }
                    else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "'   and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position asc";
                    }
                    else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position asc";
                    }
                    else //Bus
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t3.Position asc";
                    }
                }
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddl_fee_type.SelectedItem.Text == "ALL")
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' order by t3.Position asc";
                    }
                    else
                    {
                        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='Yes' order by t3.Position asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position asc";
                        }
                        else //Bus
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t3.Position asc";
                        }
                    }
                }
                else
                {
                    if (ddl_fee_type.SelectedItem.Text == "ALL")
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' order by t3.Position asc";
                    }
                    else
                    {
                        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='Yes' order by t3.Position asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='Yes' and t1.hosteltaken='No' and t1.admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='Yes' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t3.Position asc";
                        }
                        else //Bus
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t3.Position asc";
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

                            dr[lbl_month_name.Text] = find_discount(lbl_month_name.Text, lbl_month_id.Text, dr);
                            total_amt += My.toDouble(dr[lbl_month_name.Text].ToString());
                        }
                    }
                    dr["total"] = total_amt;
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
                    btn_excels.Visible = true;
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



                    // String Total_mrp = Convert.ToDouble(dt.Compute("SUM(March)", string.Empty)).ToString();


                    double total = 0; ;
                    GridView2.FooterRow.Cells[6].Text = "Total";
                    GridView2.FooterRow.Cells[6].Font.Bold = true;
                    GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    for (int k = 18; k < dt.Columns.Count; k++)
                    {
                        total = dt.AsEnumerable().Sum(row => row.Field<Double>(dt.Columns[k].ToString()));
                        GridView2.FooterRow.Cells[k].Text = total.ToString();
                        GridView2.FooterRow.Cells[k].Font.Bold = true;
                        GridView2.FooterRow.BackColor = System.Drawing.Color.Beige;
                    }
                }
                else
                {
                    Alertme("Student dues not found.", "warning");
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }
            else
            {
                Alertme("Student not found.", "warning");
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }

        private string find_discount(string month, string month_id, DataRow dr)
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
            ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
            ViewState["IsBoarding"] = "0";
            ViewState["parameteridS"] = "4";
            if (dr["Month_id"].ToString() != "")
            {
                ViewState["LunchMnthName"] = dr["Month_name"].ToString();
                ViewState["LunchMnthId"] = dr["Month_id"].ToString();
                ViewState["IsBoarding"] = "1";
            }


            string discounts = "0";
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
            if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + month + "' and (parameter='" + ViewState["parameter"].ToString() + "' or parameter='HostelMonthlyFee')").Rows.Count > 0)
            {
                feedt = My.dataTable("select isnull(sum(convert(float, disc_amount)),0)  Total_discount from (select  (select  sum(convert(float, disc_amt)) from Monthly_Fee_Collection_Slip where slipno=Typewise_fee_collection.transection and content_id=Typewise_fee_collection.content_id and Month=Typewise_fee_collection.month) as disc_amount from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and  session='" + ddl_session.SelectedItem.Text + "' and month='" + month + "' and (parameter='" + ViewState["parameter"].ToString() + "' or parameter='HostelMonthlyFee') and content_id!='6121') t");
                if (feedt.Rows.Count.ToString() != "0")
                {
                    discounts = feedt.Rows[0]["Total_discount"].ToString();
                }
                else
                {
                    discounts = "0";
                }
            }

            return discounts;
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
    }
}