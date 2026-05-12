using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class view_log_book : System.Web.UI.Page
    {
        My mycode = new My();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    ViewState["sessionid"] = code.get_session_id_use();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["teacher"].ToString());
                    txt_date.Text = mycode.date();
                    txt_enddate.Text = mycode.date();

                    mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    My.bind_ddl_all_Cap(ddl_section, "select DISTINCT Section from admission_registor order by Section asc");

                    //code.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    Fil_data_pageload();
                }
            }
        }

        private void Fil_data_pageload()
        {
            string query = "Select top 20 *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name from Teacher_log_book acd  order by acd.idate desc";
            final_bind_grid(query);

           
        }

        private void Fil_data()
        {
            try
            {
                string query = "";
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                {
                    if (ddl_class.SelectedItem.Text == "ALL")
                    {
                        query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name from Teacher_log_book acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + "  order by acd.idate desc  ";
                    }
                    else if (ddl_section.SelectedItem.Text == "ALL")
                    {
                        query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name from Teacher_log_book acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " order by acd.idate desc  ";
                    }
                    else
                    {
                        query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name from Teacher_log_book acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Section='" + ddl_section.SelectedValue + "'  order by acd.idate desc  ";
                    }
                    final_bind_grid(query);

                    
                }
                else
                {
                    Alert("Please select valid date ");
                }

            }
            catch
            {

            }

        }

        private void final_bind_grid(string query)
        {
           try
            {
                ViewState["query"] = query;
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
            catch
            {

            }
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
              My.bind_ddl_all_Cap(ddl_section, "select DISTINCT Section from admission_registor where Class_id='" + ddl_class.SelectedValue + "' order by Section asc");
            }

        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Fil_data();
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_attachment = (Label)e.Row.FindControl("lbl_Attachments");
                HtmlAnchor hlProject = (HtmlAnchor)e.Row.FindControl("a1");
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow parent = (GridViewRow)((Control)sender).Parent.Parent;
                Label lbl_id = (Label)parent.FindControl("lbl_id");
                My.exeSql("delete from Teacher_log_book where Id='" + lbl_id.Text + "'");
                Alert("Record has been successfully deleted");
                final_bind_grid(ViewState["query"].ToString());
            }
            catch
            {

            }
            

        }
    }
}