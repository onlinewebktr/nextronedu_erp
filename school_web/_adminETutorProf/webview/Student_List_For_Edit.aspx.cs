using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web._adminETutorProf.webview
{
    public partial class Student_List_For_Edit : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    try
                    {
                        ViewState["sessionid"] = My.get_session_id();
                        ViewState["Usertype"] = My.get_user_type(ViewState["teacher"].ToString());
                        if (ViewState["Usertype"].ToString() == "Teacher")
                        {
                            code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  Ptm_class_teacher_mapping   where UserID='" + ViewState["teacher"].ToString() + "'  and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");
                        }
                        else
                        {
                            code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table  order by Position asc");
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        if (Session["msg"] != null)
                        {
                            Alert(Session["msg"].ToString(), "success");
                            Session["msg"] = null;
                            Bind_grid_data(Session["query"].ToString());

                        }

                    }
                    catch
                    {

                    }
                }
            }
        }
        private void Alert(string msg, string panel)
        {

            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class", "warning");
            }
            else
            {
                if (ViewState["Usertype"].ToString() == "Teacher")
                {
                    code.bind_ddl(ddl_section, "Select distinct section  from Ptm_class_teacher_mapping where CategoryID ='" + ddl_class.SelectedValue + "'   and  UserID='" + ViewState["teacher"].ToString() + "'   order by section");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  and  Session_id='" + ViewState["sessionid"] + "'   order by Section");
                }
            }

        }

        private void Bind_grid_data(string query)
        {
            try
            {
                ViewState["query"] = query;
                Session["query"] = query;
                DataTable dt = code.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alert("Sorry there are no record exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            catch
            {

            }

        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section", "warning");
            }
            else
            {
                string query = "Select Id,Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,dob from admission_registor where Session_id='" + ViewState["sessionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' ) and Status='1' and Section ='" + ddl_section.Text + "' order by rollnumber";//
                Session["query"] = query;
                Bind_grid_data(query);
            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Response.Redirect("student_profile_Edit.aspx?id=" + lbl_Id.Text + "&regid=" + ViewState["teacher"].ToString(), false);
        }
    }
}