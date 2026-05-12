using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class admit_card_guidline : System.Web.UI.Page
    {
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


        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                if (!IsPostBack)
                {
                    try
                    {
                        const string quote = "\"";
                        string tinyMC = My.get_single_column_data("select TinyMC_link as Column_Name from Firm_Details");
                        if (tinyMC != "")
                        {
                            lt_meata.Text = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
                        }
                        else
                        {
                            lt_meata.Text = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id_cap_All(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position asc");

                    mycode.bind_all_ddl_with_id(ddl_exam, "select distinct Assessment_Name,Assessment_Name as retret from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "'  and Istatus=1");
                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "ALL")
                { }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_exam, "select distinct Assessment_Name,Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "'  and Istatus=1 and Class_id='" + ddl_CourseCat.SelectedValue + "'");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    ddlsession.Focus();
                    Alertme("Please select session.", "warning");
                    return;
                }
                else if (ddl_exam.SelectedItem.Text == "Select")
                {
                    ddl_exam.Focus();
                    Alertme("Please select exam.", "warning");
                    return;
                }
                else
                {
                    save_record();
                    Alertme("Guideline has been updated successfully.", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_record()
        {
            if (ddl_CourseCat.SelectedItem.Text == "ALL")
            {
                DataTable dt = My.dataTable("select * from Add_course_table order by Position asc");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataTable dtE = My.dataTable("select Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "' and Class_id='" + dr["course_id"].ToString() + "' and Istatus=1 and Assessment_Name='" + ddl_exam.SelectedValue + "'");
                        if (dtE.Rows.Count > 0)
                        {
                            foreach (DataRow drE in dtE.Rows)
                            {
                                save_guidelines(ddlsession.SelectedValue, dr["course_id"].ToString(), drE["Assessment_Id"].ToString());
                            }
                        }
                    }
                } 
            }
            else
            {
                save_guidelines(ddlsession.SelectedValue, ddl_CourseCat.SelectedValue, ddl_exam.SelectedValue);
            }
        }  

        private void save_guidelines(string session_id, string class_id, string Assessment_Id)
        {
            if (mycode.IsUserExist("select Id from Exam_admitcard_guideline where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + class_id + " and Exam_id=" + Assessment_Id + ""))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_admitcard_guideline (Session_id,Class_id,Term_id,Guideline,Exam_id) values (@Session_id,@Class_id,@Term_id,@Guideline,@Exam_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Term_id", "0");
                cmd.Parameters.AddWithValue("@Guideline", txt_info.Value);
                cmd.Parameters.AddWithValue("@Exam_id", Assessment_Id);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Exam_admitcard_guideline set Guideline=@Guideline where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + class_id + " and Exam_id=" + Assessment_Id + "";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Guideline", txt_info.Value);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
        }





        private void fetch_guideline()
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session.", "warning");
                return;
            }
            if (ddl_exam.SelectedItem.Text == "ALL")
            {
                ddl_exam.Focus();
                Alertme("Please select exam.", "warning");
                return;
            }

            if (ddl_CourseCat.SelectedItem.Text == "ALL")
            {
                string examId = My.get_single_column_data("select top 1 Assessment_Id as Column_Name from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "'  and Istatus=1 and Assessment_Name='"+ ddl_exam.SelectedValue + "'");
                DataTable dt = My.dataTable("select top 1 * from Exam_admitcard_guideline where Session_id=" + ddlsession.SelectedValue + " and Exam_id=" + examId + "");
                if (dt.Rows.Count > 0)
                {
                    txt_info.Value = dt.Rows[0]["Guideline"].ToString();
                }
                else
                {
                    txt_info.Value = "";
                }
            }
            else
            {
                DataTable dt = My.dataTable("select top 1 * from Exam_admitcard_guideline where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Exam_id=" + ddl_exam.SelectedValue + "");
                if (dt.Rows.Count > 0)
                {
                    txt_info.Value = dt.Rows[0]["Guideline"].ToString();
                }
                else
                {
                    txt_info.Value = "";
                }
            }

        }

        protected void ddl_exam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_guideline();
            }
            catch (Exception ex)
            {
            }
        }
    }
}