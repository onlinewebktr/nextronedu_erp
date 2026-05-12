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
    public partial class leaving_certificate : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["adm_no"] != null && Request.QueryString["clssid"] != null && Request.QueryString["sessnid"] != null && Request.QueryString["crtificateno"] != null)
                {
                    ViewState["admissionno"] = Request.QueryString["adm_no"];
                    ViewState["classid"] = Request.QueryString["clssid"];
                    ViewState["sessionid"] = Request.QueryString["sessnid"];
                    ViewState["crtificateno"] = Request.QueryString["crtificateno"];

                    Bind_crtificate_info(); Bind_schoolinfo();
                } 
            }
        }

        private void Bind_crtificate_info()
        {
            DataTable dt = mycode.FillData("select * from dbo.[Certificate_Master] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber and t1.Certificate_type='Leaving' and t1.Certificate_no='" + ViewState["crtificateno"] + "'  and t1.Session_id='" + ViewState["sessionid"] + "'   and t1.Class_id='" + ViewState["classid"] + "'   and t1.Admission_no='" + ViewState["admissionno"] + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_crtificate_no.Text = dt.Rows[0]["Certificate_no"].ToString();
                lbl_adm_no.Text = dt.Rows[0]["Admission_no"].ToString();
                lbl_std_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_fther_name.Text = dt.Rows[0]["fathername"].ToString();
                //lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_class.Text = dt.Rows[0]["class"].ToString();
                llb_session.Text = "April, 2022";
                lbl_date.Text = dt.Rows[0]["Create_date"].ToString();
                lbl_adm_no1.Text = dt.Rows[0]["Admission_no"].ToString();
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
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
                try
                {
                    Image2.Visible = true;
                    if (dt.Rows[0]["Is_certificate_watermark_hide"].ToString() == "True")
                    {
                        Image2.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
                //lbl_website.Text = lbl_website1.Text = dt.Rows[0]["website"].ToString();
                //lbl_contact_details.Text = lbl_contact_details1.Text = dt.Rows[0]["contact_no"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../leaving-certificate-report.aspx", false);
        }
    }
}