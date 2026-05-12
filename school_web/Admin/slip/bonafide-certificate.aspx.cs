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
    public partial class bonafide_certificate : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["adm_no"] != null && Request.QueryString["clssid"] != null && Request.QueryString["sessnid"] != null)
                {
                    ViewState["admissionno"] = Request.QueryString["adm_no"];
                    ViewState["classid"] = Request.QueryString["clssid"];
                    ViewState["sessionid"] = Request.QueryString["sessnid"];
                    Bind_crtificate_info();
                    Bind_schoolinfo();
                }
            }
        }

        private void Bind_crtificate_info()
        {
            DataTable dt = mycode.FillData("select * from Bonafied_certificate where Session_id='" + ViewState["sessionid"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'   and Admission_no='" + ViewState["admissionno"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                lbl_crtificate_no.Text = dt.Rows[0]["Certificate_no"].ToString();
                lbl_student_name_c.Text = dt.Rows[0]["Student_name1"].ToString();
                lbl_guardian_name.Text = dt.Rows[0]["Father_name"].ToString();
                lbl_class_grade.Text = dt.Rows[0]["Class_1"].ToString();
                lbl_class_c.Text = dt.Rows[0]["Class_2"].ToString();
                lbl_year.Text = dt.Rows[0]["Year"].ToString(); 
                lbl_dob_c.Text = dt.Rows[0]["Date_of_birth"].ToString();
                lbl_admission_no_c.Text = dt.Rows[0]["Admission_no"].ToString();
                lbl_student_name_c1.Text = dt.Rows[0]["Student_name2"].ToString();
                lbl_date_of_admission_c.Text = dt.Rows[0]["Date_of_admission"].ToString();
                lbl_remark.Text = dt.Rows[0]["Remark"].ToString(); 
                lbl_date.Text = dt.Rows[0]["Created_date"].ToString();
            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                try
                {
                    Image2.Visible = true;  
                }
                catch (Exception ex)
                {
                } 
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../bonafide-certificate.aspx", false);
        }
    }
}