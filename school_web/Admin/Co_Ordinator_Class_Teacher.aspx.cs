using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Co_Ordinator_Class_Teacher : System.Web.UI.Page
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
                        ViewState["userid"] = Session["Admin"].ToString();
                        string pagename_current = "Co_Ordinator_Class_Teacher.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_session_search, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session_search.SelectedValue = ddl_session.SelectedValue;

                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_SearchCategory, "Select Course_Name, course_id from Add_course_table order by Position");

                        find_section_search();

                        mycode.bind_all_ddl_with_id(ddl_teacher, "select name,user_id from user_details where User_Type in ('Teacher','Principal','Coordinator') and  Istatus='1'  order by name  asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_searchInstructor, " select name,user_id   from user_details where User_Type in ('Teacher','Principal','Coordinator') and  Istatus='1'  order by name  asc");
                        search_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                lnk_sync_class_teacher.Visible = false;

                try
                {
                    if (dt.Rows[0]["Is_class_teacher_auto_assign"].ToString() == "1")
                    {
                        lnk_sync_class_teacher.Visible = true;
                    }
                }
                catch
                {
                    lnk_sync_class_teacher.Visible = false;
                }
               
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
        #region search data

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_search.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session_search.Focus();
                }
                else
                {
                    search_data();
                }
            }
            catch
            {
            }
        }

        private void search_data()
        {
            string query;
            if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_searchInstructor.SelectedItem.Text != "ALL")
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm  join Add_course_table cm on ptm.CategoryID=cm.course_id where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";
                    BindRepeater(query);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm  join Add_course_table cm on ptm.CategoryID=cm.course_id where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and Section='" + ddl_section_serch.Text + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + "  order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }


            }

            else if (ddl_SearchCategory.SelectedItem.Text == "ALL" && ddl_searchInstructor.SelectedItem.Text != "ALL")
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where  ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm  join Add_course_table cm on ptm.CategoryID=cm.course_id where  ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  BHAKAT  where  ptm.UserID='" + ddl_searchInstructor.SelectedValue + "' and Section='" + ddl_section_serch.Text + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }
            }

            else if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_searchInstructor.SelectedItem.Text == "ALL")
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + "  order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + "   order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where ptm.CategoryID='" + ddl_SearchCategory.SelectedValue + "'  and Section='" + ddl_section_serch.Text + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";

                    BindRepeater(query);
                }
            }
            else
            {
                if (ddl_section_serch.Text == "ALL")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id where ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";
                    BindRepeater(query);
                }
                else if (ddl_section_serch.Text == "")
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 Name from InstructorProfile where UserID=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id where   ptm.Session_Id=" + ddl_session_search.SelectedValue + "   order by cm.Position,ptm.Section asc";
                    BindRepeater(query);
                }
                else
                {
                    query = " Select ptm.*,cm.Course_Name,(select top 1 name from user_details where user_id=ptm.UserID) as UserName,(select top 1 password from user_details where user_id=ptm.UserID) as Password  from Ptm_class_teacher_mapping ptm join Add_course_table cm on ptm.CategoryID=cm.course_id  where  Section='" + ddl_section_serch.Text + "' and ptm.Session_Id=" + ddl_session_search.SelectedValue + " order by cm.Position,ptm.Section asc";
                    BindRepeater(query);
                }
            }
        }

        private void BindRepeater(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                Alertme("Sorry there are no data list exist", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
        }
        #endregion
        UsesCode code = new UsesCode();
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else if (ddl_teacher.SelectedItem.Text == "Select")
            {
                Alertme("Please select teacher", "warning");
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    save_or_update_data();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    save_or_update_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void save_or_update_data()
        {
            if (btn_Submit.Text == "Add")
            {
                if (code.IsExist("Select * from Ptm_class_teacher_mapping where CategoryID='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and UserID='" + ddl_teacher.SelectedValue + "' and Session_Id=" + ddl_session.SelectedValue + " "))
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Ptm_class_teacher_mapping (CategoryID,Section,UserID,Date,idate,time,Session_Id,Branch_id) values (@CategoryID,@Section,@UserID,@Date,@idate,@time,@Session_Id,@Branch_id)";
                    cmd = new SqlCommand(query);
                    if (ddl_class.SelectedValue == "0")
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", ddl_class.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    cmd.Parameters.AddWithValue("@UserID", ddl_teacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@idate", code.idate());
                    cmd.Parameters.AddWithValue("@time", code.time());
                    cmd.Parameters.AddWithValue("@Session_Id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alertme("Teacher & class has been mapped successfully", "success");
                        //try
                        //{
                        //    ddl_class.SelectedValue = "0";
                        //    ddl_teacher.SelectedValue = "0";
                        //    btn_cancel.Visible = false;
                        //}
                        //catch
                        //{
                        //}
                        search_data();
                    }
                }
                else
                {
                    Alertme("Already added", "warning");

                }
            }
            else
            {
                if (code.IsExist("Select * from Ptm_class_teacher_mapping where CategoryID='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and UserID='" + ddl_teacher.SelectedValue + "' and Session_Id=" + ddl_session.SelectedValue + " and Id!='" + hd_id.Value + "' "))
                {
                    SqlCommand cmd;
                    string query = "Update Ptm_class_teacher_mapping set CategoryID=@CategoryID,Section=@Section,UserID=@UserID,Date=@Date,idate=@idate,time=@time,Branch_id=@Branch_id where Id = @Id";
                    cmd = new SqlCommand(query);
                    if (ddl_class.SelectedValue == "0")
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", ddl_class.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    cmd.Parameters.AddWithValue("@UserID", ddl_teacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@idate", code.idate());
                    cmd.Parameters.AddWithValue("@time", code.time());
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alertme("Teacher and class has been mapped successfully", "success");
                        btn_Submit.Text = "Add";
                        //try
                        //{
                        //    ddl_class.SelectedValue = "0";
                        //    ddl_teacher.SelectedValue = "0";
                        //    btn_cancel.Visible = false;
                        //}
                        //catch
                        //{
                        //}
                        search_data();
                    }
                }
                else
                {
                    Alertme("Already added", "warning");
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Co_Ordinator_Class_Teacher.aspx");
        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_id = (Label)row.FindControl("lbl_Id");
                    SqlCommand cmd = new SqlCommand("Delete from Ptm_class_teacher_mapping where Id='" + lbl_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                    search_data();
                    Alertme("successfully Deleted.", "success");
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {

                    btn_Submit.Text = "Update";
                    btn_cancel.Visible = true;
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_UserID = (Label)row.FindControl("lbl_UserID");
                    Label lbl_Section = (Label)row.FindControl("lbl_Section");
                    Label lbl_CategoryID = (Label)row.FindControl("lbl_CategoryID");
                    hd_id.Value = lbl_Id.Text;

                    try
                    {
                        ddl_class.SelectedValue = lbl_CategoryID.Text;
                        Find_Section();
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        ddl_section.Text = lbl_Section.Text;
                    }
                    catch (Exception ex)
                    {
                    }

                    ddl_teacher.SelectedValue = lbl_UserID.Text;
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                search_data();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Find_Section();
            }
            catch (Exception ex)
            {
            }
        }

        private void Find_Section()
        {
            mycode.bind_ddl(ddl_section, "select distinct Section from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and Session_id='" + ddl_session.SelectedValue + "' order by Section asc");
        }

        protected void ddl_SearchCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_section_search();
            }
            catch (Exception ex)
            {
            }
        }

        private void find_section_search()
        {
            mycode.bind_ddlall(ddl_section_serch, "select distinct Section from admission_registor where Class_id='" + ddl_SearchCategory.SelectedValue + "' and Session_id='" + ddl_session_search.SelectedValue + "' order by Section asc");
        }


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=class_teacher_list" + mycode.date() + "_" + mycode.time() + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
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

        protected void lnk_sync_class_teacher_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    return;
                }
                DataTable dt = My.dataTable("select user_id from user_details where User_Type='Teacher'");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        My.is_classTeacher_auto_assign_for_all_class(ddl_session.SelectedValue, dr["user_id"].ToString());
                    }
                }
                Alertme("All the teachers are assigned as class teachers for every class.", "success");
            }
            catch (Exception ex)
            {
            }
        }
    }
}