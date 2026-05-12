using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class update_annual_admission_fee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                get_all_student();
            }
            catch (Exception ex)
            {
            }
        }

        private void get_all_student()
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select * from dbo.[admission_registor] where Session_id='3'", My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //if (dt.Rows[0]["hosteltaken"].ToString() == "")
                    //{
                    //    ViewState["hostaltakenDues"] = "No";
                    //}
                    //else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                    //{
                    //    ViewState["hostaltakenDues"] = "No";
                    //}
                    //else
                    //{
                    //    ViewState["hostaltakenDues"] = dt.Rows[0]["hosteltaken"].ToString();
                    //}

                    
                    //ddlclass.SelectedValue = dr["Class_id"].ToString();
                    //ddlsession.SelectedValue = dr["Session_id"].ToString();
                    //ddl_session_student.SelectedValue = dr["Session_id"].ToString();
                    //ddlsessionad.SelectedValue = dr["Session_id"].ToString(); 
                    //txt_section.Text = dr["Section"].ToString();
                    //txtrollnumber.Text = dr["rollnumber"].ToString();
                    //lbl_name.Text = dr["studentname"].ToString();
                    //txt_student_name.Text = dr["studentname"].ToString();
                    //lbl_father_name.Text = dr["fathername"].ToString();
                    //lblclass.Text = dr["class"].ToString() + " / " + dr["Section"].ToString();
                    //ViewState["class_id"] = dr["Class_id"].ToString();
                    //txtroll_no.Text = dr["rollnumber"].ToString();
                    //lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    //txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    //lblhostel.Text = dr["hosteltaken"].ToString();




                    //ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    //ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    //ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();
                     
                    //ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());

                    //// confussion 
                    //ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    //ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);


                    //ViewState["group_id"] = "3";
                    //ViewState["category_id"] = dr["category_id"].ToString();
                    //ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    //ViewState["classid"] = dr["Class_id"].ToString();
                    //ViewState["Section"] = dr["Section"].ToString();
                    //ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    //ViewState["sessionid"] = dr["Session_id"].ToString();
                    //ViewState["session"] = dr["session"].ToString();
                    //ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    //Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                    //ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                    //ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                    //ViewState["From_month_name"] = (String)dc1["From_month_name"];
                    //ViewState["From_month_id"] = (String)dc1["From_month_id"];
                    //ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                    //ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];


                    //if (ViewState["Transfer_Status"].ToString() == "New")
                    //{
                    //    lbl_dues_pay_type.Text = "AdmissionFees";
                    //    lbl_studentype.Text = "New";
                    //    find_admission_dues_fee();

                    //    lbl_Monthsplit.Text = "Admission Fees";
                    //}
                    //else
                    //{
                    //    lbl_Monthsplit.Text = "Annual Fees";
                    //    lbl_dues_pay_type.Text = "AnnualFees";
                    //    lbl_studentype.Text = "Old";
                    //    find_annual_dues_fee();
                    //}
                    //try
                    //{
                    //    SqlConnection con = new SqlConnection(My.conn);
                    //    Bind_split_month_data(lbl_admission_no.Text, ViewState["sessionIDs"].ToString(), con);
                    //}
                    //catch
                    //{
                    //}



                } 
            }
        }
    }
}