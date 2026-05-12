using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class certificates : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    ViewState["Id"] = Request.QueryString["Id"];
                    Bind_school_data();


                }
            }
        }

        private void Bind_school_data()
        {
            string query = "Select * from Certificate_master_multiple where Id=" + ViewState["Id"].ToString() + "";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                lbl_date.Text = dt.Rows[0]["Issue_date"].ToString();
                string query1 = "select session,studentname,class,Section from admission_registor where admissionserialnumber='" + dt.Rows[0]["Admisison_no"].ToString() + "' and Session_id='" + dt.Rows[0]["Session_id"].ToString() + "' and Class_id='" + dt.Rows[0]["Class_id"].ToString() + "' ";
                DataTable dt1 = My.dataTable(query1);
                if (dt1.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Certificate_type_id"].ToString() == "1") // Admission
                    {
                        img_templete.ImageUrl = "https://smsmalda.com/uploads/attendance-certificate.jpg";
                        pnl_attendance_certificate.Visible = true;

                        lbl_name.Text = dt1.Rows[0]["studentname"].ToString() + " (" + dt.Rows[0]["Admisison_no"].ToString() + ")";
                        lbl_class.Text = dt1.Rows[0]["class"].ToString();
                        lbl_section.Text = dt1.Rows[0]["Section"].ToString();
                        lbl_academic_year.Text = dt1.Rows[0]["session"].ToString();

                        string princSign = My.get_single_column_data("select Signature as Column_Name from user_details where User_Type='Principal' and status='Active'");
                        img_pric_sign.Visible = true;
                        if (princSign == "")
                        {
                            img_pric_sign.Visible = false;
                        }
                        img_pric_sign.ImageUrl = princSign;
                    }
                    if (dt.Rows[0]["Certificate_type_id"].ToString() == "2") // Competition
                    {
                        img_templete.ImageUrl = "https://smsmalda.com/uploads/competition-certificate.jpg";
                        pnl_competition_certificate.Visible = true;

                        lbl_std_name1.Text = dt1.Rows[0]["studentname"].ToString() + " (" + dt.Rows[0]["Admisison_no"].ToString() + ")";
                        lbl_class1.Text = dt1.Rows[0]["class"].ToString();
                        lbl_section1.Text = dt1.Rows[0]["Section"].ToString();
                        lbl_academic_year1.Text = dt1.Rows[0]["session"].ToString();
                        lbl_securing.Text = dt.Rows[0]["Securing_position_rank"].ToString();
                        lbl_competition_name.Text = dt.Rows[0]["Competition_name"].ToString();
                        lbl_issue_date.Text = dt.Rows[0]["Issue_date"].ToString();
                        string princSign = My.get_single_column_data("select Signature as Column_Name from user_details where User_Type='Principal' and status='Active'");
                        Image2.Visible = true;
                        if (princSign == "")
                        {
                            Image2.Visible = false;
                        }
                        Image2.ImageUrl = princSign;
                    }
                    if (dt.Rows[0]["Certificate_type_id"].ToString() == "3") // Rank
                    {
                        img_templete.ImageUrl = "https://smsmalda.com/uploads/rank-certificate.jpg";
                        pnl_rank_certificate.Visible = true;


                        lbl_std_name2.Text = dt1.Rows[0]["studentname"].ToString() + " (" + dt.Rows[0]["Admisison_no"].ToString() + ")";
                        lbl_class2.Text = dt1.Rows[0]["class"].ToString();
                        lbl_section2.Text = dt1.Rows[0]["Section"].ToString();
                        lbl_academic_year2.Text = dt1.Rows[0]["session"].ToString();
                        lbl_securing2.Text = dt.Rows[0]["Securing_position_rank"].ToString();

                        lbl_issue_date2.Text = dt.Rows[0]["Issue_date"].ToString();
                        string princSign = My.get_single_column_data("select Signature as Column_Name from user_details where User_Type='Principal' and status='Active'");
                        Image3.Visible = true;
                        if (princSign == "")
                        {
                            Image3.Visible = false;
                        }
                        Image3.ImageUrl = princSign;
                    }
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../print-multiple-certificate.aspx", false);
        }
    }
}