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
    public partial class mapped_sub_with_student : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_course_search, "Select Course_Name,course_id from Add_course_table  order by Position asc");

                        // Bind_bind_data_maped_data_subject();
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_course_search.SelectedValue = My.get_top_one_class();
                        lbl_class22.Text = "Session:- " + ddl_session.SelectedItem.Text + " Class:- " + ddl_course_search.SelectedItem.Text;
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course_search.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' ");

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                        find_firm_details();


                        if (Request.QueryString["sid"] != null && Request.QueryString["cid"] != null && Request.QueryString["sec"] != null)
                        {
                            ddl_session.SelectedValue = Request.QueryString["sid"].ToString();
                            ddl_course_search.SelectedValue = Request.QueryString["cid"].ToString();
                            ddl_section.Text = Request.QueryString["sec"].ToString();
                            find_record();
                        }
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
            {




            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();

                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        private void Bind_bind_data_maped_data_subject()
        {
            string query = "select * from admission_registor where admissionserialnumber in(select Admission_no from Subject_Mapping_New) where Branch_id='" + ViewState["branchid"].ToString() + "'";
            ViewState["query"] = query;
            Bind_grid_data(query);
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


        protected void ddl_course_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_course_search.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                //string query = "select admissionserialnumber,session,class,Section,rollnumber,studentname,Class_id,Session_id,Session_id from admission_registor where admissionserialnumber in(select Admission_no from Subject_Mapping_New) and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course_search.SelectedValue + "' order by rollnumber";
                lbl_class22.Text = "Session:- " + ddl_session.SelectedItem.Text + " Class:- " + ddl_course_search.SelectedItem.Text;
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course_search.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' ");
                //ViewState["query"] = query;
                // Bind_grid_data(query);
            }
        }

        private void Bind_grid_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rd_subject_view = ((Repeater)e.Item.FindControl("rd_subject_view"));
                Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                Label lbl_class = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_session = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_Subject_name = ((Label)e.Item.FindControl("lbl_Subject_name")) as Label;

                string query = "select (select top 1 Subject_name from Subject_Master where Subject_id=Subject_Mapping_New.Sub_id and course_id=Subject_Mapping_New.Class_id) as Subject_name from Subject_Mapping_New where Session_id='" + lbl_session.Text + "' and Class_id='" + lbl_class.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    rd_subject_view.DataSource = null;
                    rd_subject_view.DataBind();
                }
                else
                {
                    rd_subject_view.DataSource = dt;
                    rd_subject_view.DataBind();
                }
            }
        }


        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                lbl_class22.Text = "Session:- " + ddl_session.SelectedItem.Text;
                string query = "select admissionserialnumber,session,class,Section,rollnumber,studentname,Class_id,Session_id,Session_id from admission_registor where admissionserialnumber in(select Admission_no from Subject_Mapping_New) and Session_id='" + ddl_session.SelectedValue + "' and  Status='1' and Branch_id='" + ViewState["branchid"].ToString() + "' order by rollnumber";
                ViewState["query"] = query;
                Bind_grid_data(query);
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Response.Redirect("Individual_edit_subject_mapping.aspx?adm=" + lbl_adm_no.Text + "&classid=" + lbl_class_id.Text + "&sessionid=" + lbl_session_id.Text, false);
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_section = (Label)row.FindControl("lbl_section");
                    mycode.executequery("delete from Subject_Mapping_New where Class_id='" + lbl_class_id.Text + "' and Session_id='" + lbl_session_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "'  and Section='" + lbl_section.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");


                    Bind_grid_data(ViewState["query"].ToString());

                    Alertme("Subject mapping list has been deleteed sucessfully", "warning");
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

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_course_search.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else
            {
                if (ddl_section.Text == "ALL")
                {
                    lbl_class22.Text = "Session:- " + ddl_session.SelectedItem.Text + " Class:- " + ddl_course_search.SelectedItem.Text + " Secction:- " + ddl_section.Text;
                    query = "select admissionserialnumber,session,class,Section,rollnumber,studentname,Class_id,Session_id,Session_id from admission_registor where admissionserialnumber in(select Admission_no from Subject_Mapping_New) and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course_search.SelectedValue + "' and  Status='1' and Branch_id='" + ViewState["branchid"].ToString() + "' order by rollnumber";
                }
                else
                {
                    lbl_class22.Text = "Session:- " + ddl_session.SelectedItem.Text + " Class:- " + ddl_course_search.SelectedItem.Text + " Secction:- " + ddl_section.Text;
                    query = "select admissionserialnumber,session,class,Section,rollnumber,studentname,Class_id,Session_id,Session_id from admission_registor where admissionserialnumber in(select Admission_no from Subject_Mapping_New) and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course_search.SelectedValue + "' and Section='" + ddl_section.Text + "' and  Status='1' and Branch_id='" + ViewState["branchid"].ToString() + "' order by rollnumber";

                }



                ViewState["query"] = query;
                Bind_grid_data(query);

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                find_record();
            }
            catch (Exception ex)
            {
            }
        }

        private void find_record()
        {
            lbl_class22.Text = "";
            string query = "";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_course_search.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                if (ddl_section.Text == "ALL")
                {
                    lbl_class22.Text = "Session:- " + ddl_session.SelectedItem.Text + " Class:- " + ddl_course_search.SelectedItem.Text + " Secction:- " + ddl_section.Text;
                    query = "select admissionserialnumber,session,class,Section,rollnumber,studentname,Class_id,Session_id,Session_id from admission_registor where admissionserialnumber in(select Admission_no from Subject_Mapping_New) and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course_search.SelectedValue + "' and  Status='1' and Branch_id='" + ViewState["branchid"].ToString() + "' order by rollnumber";
                }
                else
                {
                    lbl_class22.Text = "Session:- " + ddl_session.SelectedItem.Text + " Class:- " + ddl_course_search.SelectedItem.Text + " Secction:- " + ddl_section.Text;
                    query = "select admissionserialnumber,session,class,Section,rollnumber,studentname,Class_id,Session_id,Session_id from admission_registor where admissionserialnumber in(select Admission_no from Subject_Mapping_New) and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course_search.SelectedValue + "' and Section='" + ddl_section.Text + "' and  Status='1' and Branch_id='" + ViewState["branchid"].ToString() + "' order by rollnumber";

                }



                ViewState["query"] = query;
                Bind_grid_data(query);

            }
        }
    }
}