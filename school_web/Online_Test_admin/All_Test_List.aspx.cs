using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Test_admin
{
    public partial class All_Test_List : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Exam_list.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_Onlinetest(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        ddl_class.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall_subject(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where   course_id='" + ddl_class.SelectedValue + "' and Is_mandatory=1 order by Subject_position asc");

                       

                        Bind_data_all();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();

                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_subject.Enabled = true;
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                ddl_subject.Enabled = false;
                mycode.bind_ddlall_subject(ddl_subject, "Select distinct Subject_position ,Subject_name,Subject_id from Subject_Master   where  Is_mandatory=1 order by Subject_position asc");
                Bind_data_all();
            }
            else
            {
                mycode.bind_ddlall_subject(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where   course_id='" + ddl_class.SelectedValue + "' and Is_mandatory=1 order by Subject_position asc");
                Bind_data_all();
            }

        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_all();
        }
        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                Bind_data_all();
            }
        }
        private void Bind_data_all()
        {
            try
            {
                var condition = "where 1=1 ";
                condition += $" and  oen.Session_id='{ddlsession.SelectedValue}' ";
                //condition += $" and  oen.Status='Active' ";
                if (ddl_class.SelectedItem.Text != "ALL")
                {
                    condition += $" and oen.Class_id='{ddl_class.SelectedValue}' ";
                }
                if (ddl_subject.Text != "0")
                {
                    condition += $" and oen.subjectname='{ddl_subject.SelectedValue}' ";
                }
                 
            

                DataTable dt = My.MydataTable($@" 
 select oen.*,(select top 1 Session from session_details where session_id=oen.Session_id) as session,(Select count(*) from question_info where test_id=oen.Exam_id) as toquestion,format(oen.live_date,'dd/MM/yyyy') as live_date_one,format(oen.live_date,'hh:mm tt') as live_time_one,ad.Course_Name,(select top 1 Subject_name from Subject_Master where Subject_id=oen.subjectname) as subjectname from  OlineTest_Exam_name oen join  Add_course_table ad  on oen.Class_id=ad.course_id    {condition} order by ad.Position ,oen.Exam_name ");
                if (dt.Rows.Count == 0)
                {
                    btn_excels.Visible = false;
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();

                }
                else
                {
                    btn_excels.Visible = true;

                    rd_view.DataSource = dt;
                    rd_view.DataBind();

                }

            }
            catch
            {

            }

        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_subject = (Label)e.Item.FindControl("lbl_subject");
                Label lbl_subjectname_view = (Label)e.Item.FindControl("lbl_subjectname_view");
                if (lbl_subject.Text == "0")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else if (lbl_subject.Text == "")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else
                {
                    lbl_subjectname_view.Text = lbl_subject.Text;
                }



            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string exportname = My.with_excel_name("All_CreatedTestlist");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + exportname + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

      
    }
}