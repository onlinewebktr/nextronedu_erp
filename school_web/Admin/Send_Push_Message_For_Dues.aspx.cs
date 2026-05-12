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
    public partial class Send_Push_Message_For_Dues : System.Web.UI.Page
    {
        My mycode = new My();
        string query = "Select select top 1 gcm_id from admission_registor where admissionserialnumber=";
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
                        lbl_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Position from Month_Index order by Position asc");
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                else if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month", "warning");

                }
                else
                {
                    lbl_date.Text = " " + mycode.date() + " " + ddl_class.SelectedItem.Text;
                    find_student_dues_by_month();
                }

            }
            catch (Exception ex)
            {

            }

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
                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' order by t1.rollnumber asc";
                }
                else
                {
                    if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='Yes' order by t1.rollnumber asc";
                    }
                    else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "'   and Status='1' and t1.hosteltaken='No'order by t1.rollnumber asc";
                    }
                    else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t1.rollnumber asc";
                    }
                    else //Bus
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t1.rollnumber asc";
                    }
                }
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddl_fee_type.SelectedItem.Text == "ALL")
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' order by t1.rollnumber asc";
                    }
                    else
                    {
                        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='Yes' order by t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No'  order by t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t1.rollnumber asc";
                        }
                        else //Bus
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t1.rollnumber asc";
                        }
                    }
                }
                else
                {
                    if (ddl_fee_type.SelectedItem.Text == "ALL")
                    {
                        qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' order by t1.rollnumber asc";
                    }
                    else
                    {
                        if (ddl_fee_type.SelectedValue == "1")  //HOSTEL
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='Yes' order by t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "2")  //DAY SCHOLAR
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1'   and t1.hosteltaken='No'   order by t1.rollnumber asc";
                        }
                        else if (ddl_fee_type.SelectedValue == "3")  //Day Boarding with Lunch
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='Yes' and t1.hosteltaken='No' and t1.admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=t1.admissionserialnumber and Session_id=t1.Session_id and Class_id=t1.Class_id) order by t1.rollnumber asc";
                        }
                        else //Bus
                        {
                            qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,t1.gcm_id,t1.session from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and Status='1' and t1.hosteltaken='No' and t1.transportationtaken='Yes' order by t1.rollnumber asc";
                        }
                    }
                }
            }
            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            int srowcount = dt.Rows.Count;

            if (srowcount > 0)
            {
                //dt.Columns.Add(ddl_month.SelectedItem.Text, Type.GetType("System.Double"));
                //fdt.Columns.Add(ddl_month.SelectedItem.Text, Type.GetType("System.Double"));
                dt.Columns.Add("Monthname", Type.GetType("System.Double"));
                fdt.Columns.Add("Monthname", Type.GetType("System.Double"));
                foreach (DataRow dr in dt.Rows)
                {

                    // dr[ddl_month.SelectedItem.Text] = find_dues(ddl_month.SelectedItem.Text, ddl_month.SelectedValue, dr);
                    dr["Monthname"] = find_dues(ddl_month.SelectedItem.Text, ddl_month.SelectedValue, dr);
                }

                //================

                if (dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt.DefaultView;
                    GridView2.DataBind();
                    btn_excels.Visible = true;
                    print1.Visible = true;



                }
            }
            else
            {
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
            string session = dr["session"].ToString();
            string type = "";
            if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + ddl_session.SelectedItem.Text + "' and month='" + month + "' and (parameter='" + ViewState["parameter"].ToString() + "' or parameter='HostelMonthlyFee')").Rows.Count > 0)
            {
                feedt = My.dataTable("select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + dr["Admission_no"].ToString() + "' and session='" + session + "' and month='" + month + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and content_id!='6121') t");
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

                //dr["bac_colour"] = "White";
                Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ddl_session.SelectedValue, dr["Class_id"].ToString(), dr["Admission_no"].ToString());
                ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                ViewState["From_month_name"] = (String)dc1["From_month_name"];
                ViewState["From_month_id"] = (String)dc1["From_month_id"];
                ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];

                Dictionary<string, object> dc = new Dictionary<string, object>();
                dc["admission_no"] = dr["Admission_no"].ToString();
                dc["session_id"] = ddl_session.SelectedValue;
                dc["class"] = dr["class"].ToString();
                dc["session"] = ddl_session.SelectedItem.Text;
                dc["class_id"] = dr["Class_id"].ToString();
                dc["hosteltaken"] = dr["hosteltaken"].ToString().ToLower();
                dc["months"] = month;
                dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                dc["hostel_id"] = ViewState["Hostel_id"].ToString();//lblhostel.Text == "yes" ? My.toint(ViewState["hostel_id"].ToString()) : 0;
                dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                dc["day_boarding"] = My.toBool(dr["is_applied_dayboarding"].ToString());
                dc["day_boarding_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                dc["category_id"] = dr["category_id"].ToString();
                dc["sub_category_id"] = dr["SubCategory_id"].ToString();

                if (dr["Transportation_Id"].ToString() == "")
                {
                    ViewState["transportID"] = "0";
                }
                else if (dr["Transportation_Id"].ToString() == "&nbsp;")
                {
                    ViewState["transportID"] = "0";
                }
                else
                {
                    if (ViewState["Hostel_id"].ToString() == "0")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }
                }
                dc["transportportation_id"] = ViewState["transportID"].ToString();

                dc["parameter_id"] = ViewState["parameteridS"].ToString();
                dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
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






        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btn_send_push_Click(object sender, EventArgs e)
        {
            if (GridView2.Rows.Count == 0)
            {

            }
            else
            {

                try
                {
                    int growcount = GridView2.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GridView2.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_Admission_no = (Label)GridView2.Rows[i].FindControl("lbl_Admission_no");
                            Label lbl_Student_Name = (Label)GridView2.Rows[i].FindControl("lbl_Student_Name");
                            Label lbl_Monthname = (Label)GridView2.Rows[i].FindControl("lbl_Monthname");
                            Label lbl_gcm_id = (Label)GridView2.Rows[i].FindControl("lbl_gcm_id");

                            if (My.toDouble(lbl_Monthname.Text) > 0)
                            {

                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = "Dear " + lbl_Student_Name.Text + " , your month " + ddl_month.SelectedItem.Text + " dues amount :" + lbl_Monthname.Text + "/- Please pay  as soon as possible";
                                ss["title"] = "Monthly Dues";
                                ss["messagetype"] = "MonthlyDues";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = lbl_Admission_no.Text;
                                UsesCode.SendNotification(lbl_gcm_id.Text, ss);


                            }


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from dues list", "warning");
                    }
                    else
                    {


                    }
                }
                catch
                {
                }



            }
        }
    }
}