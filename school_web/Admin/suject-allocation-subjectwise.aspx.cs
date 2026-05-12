using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class suject_allocation_subjectwise : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        hd_branch_id.Value = ViewState["branchid"].ToString();
                        hd_user_id.Value = ViewState["Userid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table  order by Position asc");

                        // Bind_bind_data_maped_data_subject();
                        ddl_session.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' ");
                        bind_subject_dll();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        //if (ViewState["Is_Print"].ToString() == "1")
                        //{
                        //    print1.Visible = true;
                        //}
                        //else
                        //{
                        //    print1.Visible = false;
                        //}
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Mapped-Sub-With-Student");
            }
        }


        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            { }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString(); 
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();



                imglogo1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address1.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid1.Text = dt.Rows[0]["email"].ToString(); 
                lbl_contact_details1.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading1.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }



        private void bind_subject_dll()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "select Subject_name,Subject_id from Subject_Master where course_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' order by Subject_position asc");
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select Class", "warning");
            }
            else
            {
                bind_subject_dll();
                mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' ");
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
         
    }
}