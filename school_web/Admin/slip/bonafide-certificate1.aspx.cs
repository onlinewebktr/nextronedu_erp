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
    public partial class bonafide_certificate1 : System.Web.UI.Page
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
                    ViewState["signatureuserid"] = "0";

                    Bind_crtificate_info();
                    Bind_schoolinfo();
                    Bind_certificate_setting();
                    Bind_signature_setting();

                }
            }
        }
        private void Bind_signature_setting()
        {
            try
            {
                bool matchclassteacher = false;
                DataTable dt = mycode.FillData("Select  ssm.*,drm.Menu_name as Certificate_name,ud.name as user_name,CASE WHEN ssm.Is_class_teacher= 1 THEN 'Yes' WHEN ssm.Is_class_teacher = '0' THEN 'No'  END AS isclass_teacher,CASE WHEN ssm.Is_signature_display= 1 THEN 'Yes' WHEN ssm.Is_signature_display = '0' THEN 'No'  END AS issignature_display,CASE WHEN ssm.Istatus= 1 THEN 'ON' WHEN ssm.Istatus = '0' THEN 'OFF'  END AS Status from Setting_Signature_Master ssm join Dashboard_report_menu drm on ssm.Menu_id=drm.Menu_id join user_details ud on ssm.user_id=ud.user_id where drm.Menu_name='Character Certificate' and ssm.Istatus='1'  order by ssm.Position");
                if (dt.Rows.Count == 0)
                {
                    bydefult.Visible = true;
                    Sig_setting.Visible = false;
                }
                else
                {
                    bydefult.Visible = false;
                    Sig_setting.Visible = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Position = dt.Rows[i]["Position"].ToString();
                        string Is_signature_display = dt.Rows[i]["Is_signature_display"].ToString();
                        string Is_class_teacher = dt.Rows[i]["Is_class_teacher"].ToString();
                        string Istatus = dt.Rows[i]["Istatus"].ToString();
                        string user_id = dt.Rows[i]["user_id"].ToString();
                        string Signature = dt.Rows[i]["Signature"].ToString();
                        string Designation_Name = dt.Rows[i]["Designation_Name"].ToString();

                        string placment = "";
                        if (Position == "1")
                        {
                            placment = "1";
                        }
                        else if (Position == "2")
                        {
                            placment = "2";
                        }
                        else if (Position == "3")
                        {
                            placment = "3";
                        }

                        if (Is_class_teacher == "1")
                        {
                            if (matchclassteacher == false)
                            {
                                if (ViewState["signatureuserid"].ToString() == user_id)
                                {
                                    if (placment == "1")
                                    {
                                        if (Is_signature_display == "1")
                                        {
                                            sign1.Src = Signature;
                                        }
                                        else
                                        {
                                            sign1.Visible = false;
                                            Position1.Visible = true;
                                        }
                                        lbl_deg1.Text = "Signature of Class Teacher";
                                        Position1.Visible = true;

                                    }
                                    else if (placment == "2")
                                    {
                                        if (Is_signature_display == "1")
                                        {
                                            sign2.Src = Signature;
                                        }
                                        else
                                        {
                                            sign2.Visible = false;
                                        }
                                        lbl_deg2.Text = "Signature of Class Teacher";
                                        Position1.Visible = true;

                                    }
                                    else if (placment == "3")
                                    {
                                        if (Is_signature_display == "1")
                                        {
                                            sign3.Src = Signature;
                                        }
                                        else
                                        {
                                            sign3.Visible = false;
                                        }
                                        lbl_deg3.Text = "Signature of Class Teacher";
                                        Position3.Visible = true;
                                    }
                                }
                                else
                                {
                                }
                            }

                        }
                        else
                        {
                            if (placment == "1")
                            {
                                if (Is_signature_display == "1")
                                {
                                    sign1.Src = Signature;
                                }
                                else
                                {
                                    sign1.Visible = false;
                                }
                                lbl_deg1.Text = "Signature of " + Designation_Name;
                                Position1.Visible = true;

                            }
                            else if (placment == "2")
                            {
                                if (Is_signature_display == "1")
                                {
                                    sign2.Src = Signature;
                                }
                                else
                                {
                                    sign2.Visible = false;
                                }
                                lbl_deg2.Text = "Signature of " + Designation_Name;
                                Position2.Visible = true;

                            }
                            else if (placment == "3")
                            {
                                if (Is_signature_display == "1")
                                {
                                    sign3.Src = Signature;
                                }
                                else
                                {
                                    sign3.Visible = false;
                                }
                                lbl_deg3.Text = "Signature of " + Designation_Name;
                                Position3.Visible = true;
                            }

                        }
                    }

                }

            }
            catch
            {
                bydefult.Visible = true;
                Sig_setting.Visible = false;
            }

        }
        private void Bind_certificate_setting()
        {
            try
            {
                DataTable dt = mycode.FillData("select * from Header_templete where Module_type='Certificate'");
                if (dt.Rows.Count == 0)
                {
                    header_txt.Visible = true;
                    header_img.Visible = false;



                }
                else
                {
                    if (dt.Rows[0]["Status"].ToString() == "1")
                    {
                        header_txt.Visible = false;
                        header_img.Visible = true;
                        img_header.ImageUrl = dt.Rows[0]["Path"].ToString();

                    }
                    else
                    {

                        header_txt.Visible = true;
                        header_img.Visible = false;

                    }
                }
            }
            catch
            {
                header_txt.Visible = true;
                header_img.Visible = false;

            }
        }
        private void Bind_crtificate_info()
        {
            DataTable dt = mycode.FillData("select * from dbo.[Certificate_Master] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber and t1.Certificate_type='Income' and t1.Session_id='" + ViewState["sessionid"] + "'   and t1.Class_id='" + ViewState["classid"] + "'   and t1.Admission_no='" + ViewState["admissionno"] + "' and t1.Certificate_no='" + ViewState["crtificateno"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                string Section = dt.Rows[0]["Section"].ToString();
                string Class_id = dt.Rows[0]["Class_id"].ToString();
                string Session_id = dt.Rows[0]["Session_id"].ToString();
                ViewState["signatureuserid"] = My.get_user_id_class_teacher(Session_id, Class_id, Section);

                lbl_crtificate_no.Text = dt.Rows[0]["Certificate_no"].ToString();
                lbl_adm_no.Text = dt.Rows[0]["Admission_no"].ToString();
                lbl_std_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_fther_name.Text = dt.Rows[0]["fathername"].ToString();
                lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_class.Text = dt.Rows[0]["class"].ToString();
                llb_session.Text = dt.Rows[0]["session"].ToString();
                lbl_date.Text = dt.Rows[0]["Create_date"].ToString();
                lbl_datep.Text = dt.Rows[0]["Create_date"].ToString();

                lbl_dob.Text = dt.Rows[0]["dob"].ToString();
                try
                {
                    string dob = dt.Rows[0]["dob"].ToString();
                    string dday = dob.Substring(0, 2);
                    string dmonth = dob.Substring(3, 2);
                    string dyear = dob.Substring(6, 4);
                    string monthName = My.getMonthS_full_name(dmonth);
                    string inword_day = mycode.NumberToWords(Convert.ToInt32(dday));
                    string inword_year = mycode.NumberToWords(Convert.ToInt32(dyear));
                    lbl_dob_in_word.Text = "(" + inword_day + " " + monthName + ", " + inword_year + ")";
                }
                catch (Exception ex)
                {
                    lbl_dob.Text = "";
                }
            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
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
            Response.Redirect("../print-income-certificate.aspx", false);
        }
    }
}