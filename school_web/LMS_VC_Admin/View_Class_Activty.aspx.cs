using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class View_Class_Activty : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        string teachername = "Select top 1 name from user_details where user_id=acd.Teacher_id";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();
                    ViewState["branchid"] = "1";
                    code.bind_all_ddl_with_all(ddl_teacher_list, "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal') and status='Active'  order by Name  asc");
                    ViewState["sessionid"] = code.get_session_id_use();
                    BindGridView(1);
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindGridView(int p)
        {
            string query = "";

            ViewState["flag"] = p.ToString();

            if (p == 1)
            {

                query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + code.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + code.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + "  order by idate asc";



            }
            else if (p == 2)
            {
                if (ddl_teacher_list.SelectedItem.Text == "ALL")
                {
                    query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + code.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + code.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + "  order by idate asc";


                }
                else
                {
                    query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + code.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + code.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Teacher_id='" + ddl_teacher_list.SelectedValue + "'  order by idate asc";



                }

            }
            bind_data_grid(query);
        }
        My mycode1 = new My();
        private void bind_data_grid(string query)
        {
            DataTable dt = mycode1.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry there are no data list exist");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_teacher_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGridView(1);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            BindGridView(2);
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor hlProject = (HtmlAnchor)e.Item.FindControl("attachmentss");
                hlProject.Visible = true;
                if (((Label)e.Item.FindControl("lbl_atachment")).Text == "" || ((Label)e.Item.FindControl("lbl_atachment")).Text == "#")
                {
                    hlProject.Visible = false;
                }
            }
        }
    }
}