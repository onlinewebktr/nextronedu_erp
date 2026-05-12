using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
namespace school_web.LMS_VC_Admin
{
    public partial class Class_Routine_List : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admin"] != null)
                {
                    ViewState["Admin"] = Session["Admin"].ToString();
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");
                    code.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
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

        public void Alert(string Message)
        {

            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else
            {
                code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else
            {
                Bind_grid_data();
            }
        }

        private void Bind_grid_data()
        {
            string query = "Select crm.*,format(crm.Start_Time, 'hh:mm:ss tt') as Start_Time1,format(crm.End_time, 'hh:mm:ss tt') as End_time1,cm.Course_Name as Classname,(select top 1 Subject_name from Subject_name where course_id=crm.Class_id  and Subject_id=crm.Subject_id) as subjectname,(select top 1 Session from session_details where session_id=crm.Session_id) as Session from Class_Routine_Master crm join ClassMaster cm on crm.Class_id=cm.CategoryID where crm.Session_id=" + ddl_session.SelectedValue + " and crm.Class_id=" + ddl_class.SelectedValue + " and crm.Section='" + ddl_section.Text + "'  order by crm.Class_period asc ";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                imgexcel2.Visible = false;
                grid111.Visible = false;
                grd_class.DataSource = null;
                grd_class.DataBind();

            }
            else
            {
                imgexcel2.Visible = true;
               
                grid111.Visible = true;
                grd_class.DataSource = dt;
                grd_class.DataBind();
            }
        }


        protected void imgexcel2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Routine" + "_"+ddl_class.SelectedItem.Text+"_"+ddl_section.Text + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grd_class.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}