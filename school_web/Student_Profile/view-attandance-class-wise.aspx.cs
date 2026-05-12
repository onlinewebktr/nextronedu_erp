using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class view_attandance_class_wise : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        find_student_details();
                        Bind_data();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
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

        private void find_student_details()
        {
            string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV'    and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alertme("Something is wrong", "warning");

                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["email_id"] = dr["email_id"].ToString();
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["rollnumber"] = dr["rollnumber"].ToString();
                    ViewState["studentname"] = dr["studentname"].ToString();
                    ViewState["fathername"] = dr["fathername"].ToString();
                    ViewState["class"] = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";
                    ViewState["transportationtaken"] = dr["transportationtaken"].ToString();
                    ViewState["mobilenumber"] = dr["father_mob"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")

                        if (dr["Section"].ToString() == "")
                        {
                            ViewState["Section"] = "A";
                        }
                        else if (dr["Section"].ToString() == "&nbsp;")
                        {

                            ViewState["Section"] = "A";
                        }
                        else
                        {
                            ViewState["Section"] = dr["Section"].ToString();
                        }
                }
            }
        }



        private void Bind_data()
        {
            if (txt_from_date.Text == "")
            {
                txt_from_date.Focus();
                Alertme("Please select start date", "warning");

            }
            else if (txt_to_date.Text == "")
            {
                txt_from_date.Focus();
                Alertme("Please select end date", "warning");
            }
            else
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)))
                { 
                    string query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ViewState["classid"].ToString() + "' and ar.section='" + ViewState["Section"].ToString() + "' and ar.Session_id='" + ViewState["Session_id"].ToString() + "'  and ar.admissionserialnumber='" + ViewState["regid"].ToString() + "'  and   sas.Attendance_IDate>=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) + " and sas.Attendance_IDate<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)) + "  order by sas.Attendance_IDate asc";


                    DataTable dt = mycode.FillTable(query);
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
                else
                {
                    Alertme("Please select valid date ", "warning");
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data();
        }
    }
}