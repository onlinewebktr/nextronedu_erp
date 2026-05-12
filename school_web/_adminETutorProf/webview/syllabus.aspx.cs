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
    public partial class syllabus : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    ViewState["sessionid"] = My.get_session_id();
                    mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select   Course_Name, course_id from Add_course_table  order by Position asc");
                    mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where  Session_id='" + ViewState["sessionid"].ToString() + "'   order by Section");
                    fill_data_pageload();

                }

            }
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            fill_data_pageload();
        }
        private void Alert(string message, string type)
        {
            if (type == "success")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertSuccess('" + message + "');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertError('" + message + "');", true);

            }
        }
        private void fill_data_pageload()
        {
            try
            {
                var condition = "where 1=1 ";
                condition += $" and  sy.Session_id='{ViewState["sessionid"].ToString()}' ";
                //condition += $" and  oen.Status='Active' ";
                if (ddl_class.SelectedItem.Text != "ALL")
                {
                    condition += $" and sy.Class_id='{ddl_class.SelectedValue}' ";
                }
                if (ddl_section.SelectedItem.Text != "ALL")
                {
                    condition += $" and sy.Section='{ddl_section.SelectedItem.Text}' ";
                }
                DataTable dt = My.MydataTable($@" select sy.*,ct.Course_Name from Syllabus_master_new sy join Add_course_table ct on ct.course_id = sy.Class_id {condition} order by  ct.Position,sy.Section ");
                if (dt.Rows.Count == 0)
                {
                    msg12.Visible = true;
                    Alert("The class syllabus could not be found.","Error") ;
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();

                }
                else
                {
                    msg12.Visible = false;

                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();

                }

            }
            catch
            {

            }
        }


        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class", "warning");
            }
            else
            {
                 
                    mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  and  Session_id='" + ViewState["sessionid"].ToString() + "'   order by Section");
                
            }

        }

      
    }
}