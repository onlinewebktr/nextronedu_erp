using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    
    public partial class class_Activity : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        bind_student_class_and_section();
                        Get_data_taken();

                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
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
        private void bind_student_class_and_section()
        {
            DataTable dt = mycode.FillData("Select Class_id,Section,Branch_id from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sesssionid"].ToString() + "'  and  Transfer_Status in ('New','NT') and  StudentStatus='AV'  order by id desc  ");
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

                mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["classid"].ToString() + "' and smn.Session_id='" + ViewState["sesssionid"].ToString() + "' and smn.Section='" + ViewState["section"].ToString() + "' order by sm.Subject_position");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                Get_data_taken();
            }
            catch
            {

            }
        }

        private void Get_data_taken()
        {
            
       

            try
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)))
                {
                    string query = "";
                    if (ddl_subject.SelectedItem.Text == "ALL")
                    {
                        query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_from_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_to_date.Text) + "' and acd.Session_id=" + ViewState["sesssionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ViewState["classid"].ToString() + " and acd.Section_data='" + ViewState["section"].ToString() + "' and acd.Subject_id in (" + My.getstudent_subjectname(ViewState["regid"].ToString(), ViewState["section"].ToString(), ViewState["sesssionid"].ToString(), ViewState["classid"].ToString()) + ") order by acd.idate asc  ";
                    }
                    else
                    {
                        query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_from_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_to_date.Text) + "' and acd.Session_id=" + ViewState["sesssionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ViewState["classid"].ToString() + " and acd.Subject_id=" + ddl_subject.SelectedValue + " and acd.Section_data='" + ViewState["section"].ToString() + "' order by acd.idate asc  ";
                    }


                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        Alertme("Sorry! there are no class activity list exist", "warning");
                        rd_view.DataSource = null;
                        rd_view.DataBind();
                    }
                    else
                    {
                        rd_view.DataSource = dt;
                        rd_view.DataBind();
                    }
                }
                else
                {
                    Alertme("Please select valid date ", "warning");
                }

            }
            catch (Exception ex)
            {

            }


        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl_attachment = (Label)e.Item.FindControl("lbl_attachment");
            HtmlAnchor hlProject = (HtmlAnchor)e.Item.FindControl("attachmentss");

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