using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip
{
    public partial class print_admit_card_a5 : System.Web.UI.Page
    {
        My mycode = new My();
        Examination ec = new Examination();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["classid"] != null && Request.QueryString["session_Id"] != null && Request.QueryString["branch_id"] != null && Request.QueryString["examterm"] != null && Request.QueryString["section"] != null && Request.QueryString["examid"] != null)
                {
                    hd_session_id.Value = Request.QueryString["session_Id"].ToString();
                    hd_class_id.Value = Request.QueryString["classid"].ToString();
                    hd_term_id.Value = Request.QueryString["examterm"].ToString();
                    hd_section.Value = Request.QueryString["section"].ToString();
                    hd_branch_id.Value = Request.QueryString["branch_id"].ToString();
                    hd_admission_no.Value = Request.QueryString["admin"].ToString();
                    hd_exam_id.Value = Request.QueryString["examid"].ToString();
                    hd_session_name.Value = mycode.get_session(hd_session_id.Value);
                    hd_page.Value = Request.QueryString["page"].ToString();
                    hd_print_from.Value = "0";
                    hd_checked.Value = "0";

                    try
                    {
                        if (Request.QueryString["checked"] != null)
                        {
                            hd_checked.Value = Request.QueryString["checked"].ToString();
                            if (hd_checked.Value == "1")
                            {
                                hd_admission_no.Value = hd_admission_no.Value.Remove(hd_admission_no.Value.Length - 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        if (Request.QueryString["from"] != null)
                        {
                            hd_print_from.Value = Request.QueryString["from"].ToString();
                            if (hd_print_from.Value == "stdwise")
                            {
                                lnk_back.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    hd_user_type.Value = "User";
                    try
                    {
                        hd_user_type.Value = Session["userType"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }

                    fetch_imp_note();
                }
                else
                {
                }
            }
        }

        private void fetch_imp_note()
        {
            DataTable dt = My.dataTable("select * from Exam_admitcard_guideline  where Session_id=" + hd_session_id.Value + " and Class_id=" + hd_class_id.Value + " and Exam_id='" + hd_exam_id.Value + "'");
            if (dt.Rows.Count > 0)
            {
                lbl_imp_note.Text = dt.Rows[0]["Guideline"].ToString();
            }
        }
        protected void lnk_back_Click(object sender, EventArgs e)
        {
            if (hd_print_from.Value == "stdwise")
            {
                Response.Redirect("~/Examination_Admin/admit-card-student-wise.aspx", false);
            }
            else
            {
                Response.Redirect("~/Examination_Admin/Exam_Time_Table_List.aspx", false);
            }
        }
    }
}