using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.HtmlControls;

namespace school_web.Student_Profile.webview
{
    public partial class View_Class_Activity : System.Web.UI.Page
    {
        My mycode = new My();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    ViewState["sessionid"] = code.get_session_id_use();

                    txt_date.Text = mycode.date();
                    txt_enddate.Text = mycode.date();
                    bind_student_class_and_section();

                    Fil_data_pageload();

                }

            }
        }



        private void bind_student_class_and_section()
        {
            DataTable dt = code.FillTable("Select Class_id,Section,Branch_id from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc  ");
            if (dt.Rows.Count == 0)
            {

                ViewState["classid"] = "0";
                ViewState["section"] = "0";
            }
            else
            {
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                ViewState["section"] = dt.Rows[0]["Section"].ToString();
                ViewState["branchid"] = dt.Rows[0]["Branch_id"].ToString();

                mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["classid"].ToString() + "' and smn.Session_id='" + ViewState["sessionid"].ToString() + "' and smn.Section='" + ViewState["section"].ToString() + "' order by sm.Subject_position");

                //ddl_subject.SelectedValue = My.get_top_onle_subject(ViewState["regid"].ToString(), ViewState["sessionid"].ToString());
            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void Fil_data_pageload()
        {

            try
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                {
                    string query = "";
                    if (ddl_subject.SelectedItem.Text == "ALL")
                    {
                        query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ViewState["classid"].ToString() + " and acd.Section_data='" + ViewState["section"].ToString() + "' and acd.Subject_id in ("+ My.getstudent_subjectname(ViewState["regid"].ToString(), ViewState["section"].ToString(), ViewState["sessionid"].ToString(), ViewState["classid"].ToString()) + ") order by acd.idate asc  ";
                    }
                    else
                    {
                        query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ViewState["classid"].ToString() + " and acd.Subject_id=" + ddl_subject.SelectedValue + " and acd.Section_data='" + ViewState["section"].ToString() + "' order by acd.idate asc  ";
                    }


                    DataTable dt = code.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        Alert("Sorry there are no data list exist");
                        GrdView.DataSource = null;
                        GrdView.DataBind();
                    }
                    else
                    {
                        GrdView.DataSource = dt;
                        GrdView.DataBind();
                    }
                }
                else
                {
                    Alert("Please select valid date ");
                }

            }
            catch (Exception ex)
            {

            }


        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Fil_data_pageload();
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_attachment = (Label)e.Row.FindControl("lbl_attachment");
                HtmlAnchor hlProject = (HtmlAnchor)e.Row.FindControl("attachmentss");
                if (lbl_attachment.Text == "#" || lbl_attachment.Text == "")
                {
                    hlProject.Visible = false;
                }
                else
                {
                    hlProject.Visible = true;
                }
            }
        }
    }
}
