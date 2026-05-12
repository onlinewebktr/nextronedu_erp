using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class view_result_examwise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    string session_id = My.get_session_id();
                    string adm_no = Request.QueryString["regid"].ToString();

                    pnl_syllabus.Visible = false;
                    pnl_bo_found.Visible = true;
                    DataTable dt = My.dataTable("select *,(select top 1 Assessment_Name from Exam_Assessment_Details where Session_Id=t1.Session_id and Exam_Term_Id=t1.Term_id and Assessment_Id=t1.Assesment_id) as Exam_name,(select top 1 Class_id from admission_registor where Session_Id=t1.Session_id and admissionserialnumber=t1.Admission_no) as Class_id,(select top 1 Section from admission_registor where Session_Id=t1.Session_id and admissionserialnumber=t1.Admission_no) as Section from Exam_publish_result_examwise t1 where Session_id='" + session_id + "' and Admission_no='" + adm_no + "' order by id desc");
                    if (dt.Rows.Count > 0)
                    {
                        string websiteurL = getUrls();
                        pnl_syllabus.Visible = true;
                        pnl_bo_found.Visible = false;
                        rp_syllabus.DataSource = dt;
                        rp_syllabus.DataBind();
                    }
                }
            }
        }

        private string getUrls()
        {
            string returN = "0";
            DataTable dt = My.dataTable("select URL from Global");
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0][0].ToString();
                return returN;
            }
        }

        protected void rp_syllabus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_term_id = ((Label)e.Item.FindControl("lbl_term_id")) as Label;
                Label lbl_assesment_id = ((Label)e.Item.FindControl("lbl_assesment_id")) as Label;
                Label lbl_section = ((Label)e.Item.FindControl("lbl_section")) as Label;
                HtmlAnchor rpcard_link = (HtmlAnchor)e.Item.FindControl("rpcard_link");

                string repage = getRp_page(lbl_class_id.Text);
                string websiteurL = getUrls();
                rpcard_link.HRef = websiteurL + "Examination_Admin/slip/" + repage + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=1&Term_id=" + lbl_term_id.Text + "&RequestFrom=1&ismob=1&assment=" + lbl_assesment_id.Text + "&section=0";
            }
        }

        private string getRp_page(string class_id)
        {
            string returN = "";
            string querym = "select Report_card_single,Final_single,Assesment_rp_page from Exam_report_card_setting_classwise where  Class_id=" + class_id + " and Branch_id='1'";
            DataTable dtm = My.dataTable(querym);
            if (dtm.Rows.Count > 0)
            {
                returN = dtm.Rows[0]["Assesment_rp_page"].ToString();
            }
            return returN;
        }
    }
}