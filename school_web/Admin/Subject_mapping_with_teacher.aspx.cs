using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Admin
{
    public partial class Subject_mapping_with_subject : System.Web.UI.Page
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
                        ViewState["branchid"] = Session["branchid"].ToString();

                        try
                        {
                            string isSubjAutoAssign = My.get_single_column_data("select Is_subj_auto_assign_to_teacher as Column_Name from Firm_Details");
                            if (isSubjAutoAssign == "1")
                            {
                                lnk_auto_assign_subject.Visible = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddlsession.SelectedValue = My.get_session_id();


                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table");

                        mycode.bind_ddl(ddl_section, "Select distinct Section  from section_master  order by Section");
                        mycode.bind_all_ddl_with_id(ddl_teacher, "Select  name+','+mobile , user_id from user_details where Istatus='1' and   (User_Type='Teacher' or User_Type='Principal') order by name");

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }

        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        string type;

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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }

                else
                {
                    bind_subject();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_subject()
        {
            rightpnl.Visible = false;
            DataTable dt = mycode.FillData("select *   from Subject_Master where   course_id='" + ddlclass.SelectedValue + "'");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no subject list exist", "warning");
                rp_subJ_list.DataSource = null;
                rp_subJ_list.DataBind();
            }
            else
            {
                rightpnl.Visible = true;
                rp_subJ_list.DataSource = dt;
                rp_subJ_list.DataBind();
            }
        }

        protected void btn_map_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                ddlsession.Focus();
            }
            else if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddlclass.Focus();
            }

            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_section.Focus();

            }
            else if (ddl_teacher.SelectedItem.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_teacher.Focus();
            }
            else
            {
                save_map_course();
            }
        }

        private void save_map_course()
        {
            hd_type.Value = My.data("  select Type from dbo.[Add_course_table] where course_id='" + ddlclass.SelectedValue + "'");
            bool delete = false;
            int growcountS = rp_subJ_list.Items.Count;
            int kS = 0;
            for (int iS = 0; iS < growcountS; iS++)
            {
                ViewState["statusUp"] = "0";
                CheckBox chkS = (CheckBox)rp_subJ_list.Items[iS].FindControl("rowChkBox1");
                if (chkS.Checked == true)
                {

                    Label lbl_course_id = (Label)rp_subJ_list.Items[iS].FindControl("lbl_course_id");
                    Label lbl_subject_id = (Label)rp_subJ_list.Items[iS].FindControl("lbl_subject_id");
                    Label lbl_subject_name = (Label)rp_subJ_list.Items[iS].FindControl("lbl_subject_name");
                    if (delete == false)
                    {
                        //mycode.executequery("delete from TeacherCourseSubjectMaping where CategoryID='" + ddlclass.SelectedValue + "'  and section='" + ddl_section.Text + "'   and Session_id='" + ddlsession.SelectedValue + "' and UserID='" + ddl_teacher.SelectedValue + "'   ");
                    }

                    //===============
                    if (mycode.IsUserExist("select UserID from TeacherCourseSubjectMaping where CategoryID='" + ddlclass.SelectedValue + "' and AssignCourseID=" + lbl_subject_id.Text + " and section='" + ddl_section.Text + "' and Session_id='" + ddlsession.SelectedValue + "' and UserID='" + ddl_teacher.SelectedValue + "' "))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO TeacherCourseSubjectMaping (UserID,AssignCourseID,Istatus,Date,Idate,CategoryID,section,sync_status,Acamedic_Semester_Id,Type,Session_id,Branch_id,Created_by) values (@UserID,@AssignCourseID,@Istatus,@Date,@Idate,@CategoryID,@section,@sync_status,@Acamedic_Semester_Id,@Type,@Session_id,@Branch_id,@Created_by)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@UserID", ddl_teacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@AssignCourseID", lbl_subject_id.Text);
                        cmd.Parameters.AddWithValue("@Istatus", 1);
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@CategoryID", lbl_course_id.Text);
                        cmd.Parameters.AddWithValue("@section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@sync_status", "0");
                        cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                        cmd.Parameters.AddWithValue("@Type", hd_type.Value);
                        cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                        delete = true;
                    }
                }
                else
                {
                    kS++;
                }
            }

            if (kS == growcountS)
            {
                Alertme("Please check minimum one subject list.", "warning");
                ViewState["statusUp"] = "0";
            }
            else
            {
                Alertme("Teacher maped with subject", "success");
                bind_subject(); find_mapped_subject();
            }
        }

        protected void ddl_teacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    pnl_mapped_subj.Visible = false;
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    find_mapped_subject();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void find_mapped_subject()
        {
            string query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where  tcsm.UserID='" + ddl_teacher.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + " order by cm.Position,tcsm.section asc";
            BindRepeater(query, RPDetails);
        }
        private void BindRepeater(string query, Repeater rPDetails)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count != 0)
            {
                pnl_mapped_subj.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                pnl_mapped_subj.Visible = false;
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
        }

        protected void lnk_auto_assign_subject_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    return;
                }
                DataTable dt = My.dataTable("select user_id from user_details where User_Type='Teacher'");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        My.is_classTeacher_auto_assign_for_all_class(ddlsession.SelectedValue, dr["user_id"].ToString());
                    }
                }
                Alertme("All the teachers have been assigned all subjects for every class.", "success");
            }
            catch (Exception ex)
            {
            }
        }
    }
}