using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip.kes
{
    public partial class final_rp : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["clss_id"] != null && Request.QueryString["ssion_id"] != null && Request.QueryString["Branch_id"] != null && Request.QueryString["admNo"] != null)
                {
                    hd_adm_no.Value = Request.QueryString["admNo"];
                    hd_section.Value = Request.QueryString["Section"];
                    hd_class_id.Value = Request.QueryString["clss_id"];
                    hd_session_id.Value = Request.QueryString["ssion_id"];
                    hd_branch_id.Value = Request.QueryString["Branch_id"];

                    find_terms();
                    hd_user_type.Value = "User";
                    try
                    {
                        hd_user_type.Value = Session["userType"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void find_terms()
        {
            string query = "select Short_Name,Grade_System_Id,Exam_Term_Id from Exam_Term_Details where Session_Id=" + hd_session_id.Value + " and Branch_Id='" + hd_branch_id.Value + "' and Class_id=" + hd_class_id.Value + " order by Sequence_No asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 2)
            {
                hd_term1.Value = dt.Rows[0]["Exam_Term_Id"].ToString();
                hd_term2.Value = dt.Rows[1]["Exam_Term_Id"].ToString();
                hd_term3.Value = "0";
                Examination.map_term_assesment(hd_session_id.Value, hd_branch_id.Value, hd_class_id.Value, hd_term1.Value, hd_term2.Value);
            }
            if (dt.Rows.Count == 3)
            {
                hd_term1.Value = dt.Rows[0]["Exam_Term_Id"].ToString();
                hd_term2.Value = dt.Rows[1]["Exam_Term_Id"].ToString();
                hd_term3.Value = dt.Rows[2]["Exam_Term_Id"].ToString();
                Examination.map_term_assesment(hd_session_id.Value, hd_branch_id.Value, hd_class_id.Value, hd_term1.Value, hd_term2.Value);
            }
        }



        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../student-result-final.aspx", false);
        }
    }
}