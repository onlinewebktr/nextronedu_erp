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

namespace school_web.LMS_VC_Admin
{
    public partial class View_Class_Activity_Report_Subject_Class_wise : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
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
                    ViewState["branchid"] = "1";
                    ViewState["sessionid"] = My.get_session_id();
                    code.bind_all_ddl_with_id(ddl_class, "Select  ct.Course_Name, ct.course_id  from Add_course_table ct     order by ct.Position asc");
                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();
                    ViewState["branchid"] = "1";
                    BindGridView(1);


                }

            }

        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        string teachername = "Select top 1 name from user_details where user_id=acd.Teacher_id";
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_id=" + ddl_class.SelectedValue + " ");
                code.bind_all_ddl_with_id(ddl_subject, "Select distinct  sm.Subject_name, sm.Subject_id,sm.Subject_position from Subject_Master sm join Activity_Class_Details scs on sm.course_id=scs.Class_id and sm.Subject_id=scs.Subject_id where  scs.Session_id=" + ViewState["sessionid"].ToString() + "  and scs.Class_id=" + ddl_class.SelectedValue + "   order by sm.Subject_position asc");
            }
        }

        private void BindGridView(int p)
        {
            string query = "";

            ViewState["flag"] = p.ToString();

            if (p == 1)
            {

                query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + code.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + code.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + "  order by idate asc";



            }
            else if (p == 2)// class and subject and section All
            {


                if (ddl_section.Text == "ALL")
                {
                    query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Subject_id=" + ddl_subject.SelectedValue + "  order by idate asc";


                }
                else//Section not all
                {
                    query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where    acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Subject_id=" + ddl_subject.SelectedValue + "  and acd.Section_data='" + ddl_section.Text + "'  order by idate asc";
                }

            }
            else if (p == 3)// class and subject and section All and date
            {
                if (ddl_section.Text == "ALL")
                {
                    query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + code.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + code.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Subject_id=" + ddl_subject.SelectedValue + "  order by idate asc";


                }
                else//Section not all  and date
                {
                    query = "Select *,(" + teachername + ") as teachername,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + code.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + code.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Subject_id=" + ddl_subject.SelectedValue + "  and acd.Section_data='" + ddl_section.Text + "'  order by idate asc";
                }
            }
            bind_data_grid(query);
        }

        private void bind_data_grid(string query)
        {
            DataTable dt = mycode.FillData(query);
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

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if(ddl_subject.SelectedItem.Text=="Select")
            {
                Alert("Please select subject name");
            }
           
            else
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                {
                    BindGridView(3);
                }
                else
                {
                    Alert("Please select valid date ");
                }
            }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
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