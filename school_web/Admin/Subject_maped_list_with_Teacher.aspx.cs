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

namespace school_web.Admin
{
    public partial class Subject_maped_list_with_Teacher : System.Web.UI.Page
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

                        string pagename_current = "Subject_maped_list_with_Teacher.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        find_firm_details();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_All(ddlclass, "Select Course_Name,course_id from Add_course_table");
                        ddlclass.SelectedValue = My.get_top_one_class();

                        My.bind_ddl_all(ddl_section_serch, "Select distinct Section from admission_registor order by Section");

                        ddl_section_serch.Text = My.get_top_one_section();


                        mycode.bind_all_ddl_with_id_All(ddl_teacher, "Select  name+','+mobile , user_id from user_details where Istatus='1' and   (User_Type='Teacher' or User_Type='Principal') order by name");
                        find_data();
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
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
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

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Label lbl_course_id = ((Label)e.Item.FindControl("lbl_course_id")) as Label;
                //Label lbl_subjectid = ((Label)e.Item.FindControl("lbl_subjectid")) as Label;
                //Label lbl_Session_id = ((Label)e.Item.FindControl("lbl_Session_id")) as Label;
                //Label lbl_Acamedic_Semester_Id = ((Label)e.Item.FindControl("lbl_Acamedic_Semester_Id")) as Label;
                //Label lbl_Type = ((Label)e.Item.FindControl("lbl_Type")) as Label;
                //Label lbl_semester_year = ((Label)e.Item.FindControl("lbl_semester_year")) as Label;
                //lbl_semester_year.Text = mycode.get_semester_yearname(lbl_Acamedic_Semester_Id.Text, lbl_Session_id.Text);

            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            find_data();
          
           
        }

        private void find_data()
        {
            string query = "";
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning"); 
                } 
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning"); 
                }
                else if (ddl_teacher.SelectedItem.Text == "Select")
                {
                    Alertme("Please select teacher", "warning"); 
                }
                else if (ddlclass.SelectedItem.Text != "All" && ddl_teacher.SelectedItem.Text != "All")
                {
                    if (ddl_section_serch.Text == "All")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where  tcsm.UserID='" + ddl_teacher.SelectedValue + "' and   tcsm.CategoryID='" + ddlclass.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + " order by cm.Position,tcsm.section asc";

                         BindRepeater(query, RPDetails);
                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where  tcsm.UserID='" + ddl_teacher.SelectedValue + "' and   tcsm.CategoryID='" + ddlclass.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + " order by cm.Position,tcsm.section asc";

                         BindRepeater(query, RPDetails);
                    }
                    else
                    { 
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where  tcsm.UserID='" + ddl_teacher.SelectedValue + "' and   tcsm.CategoryID='" + ddlclass.Text + "' and tcsm.section='" + ddl_section_serch.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + " order by cm.Position,tcsm.section asc";
                        BindRepeater(query, RPDetails);
                    }
                }
                else if (ddlclass.SelectedItem.Text == "All" && ddl_teacher.SelectedItem.Text != "All")
                {
                    if (ddl_section_serch.Text == "All")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where  tcsm.UserID='" + ddl_teacher.SelectedValue + "' and tcsm.Session_id=" + ddlsession.SelectedValue + "  order by cm.Position,tcsm.section asc";

                        



                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where  tcsm.UserID='" + ddl_teacher.SelectedValue + "' and tcsm.Session_id=" + ddlsession.SelectedValue + "  order by cm.Position,tcsm.section asc";


                    }
                    else
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where  tcsm.UserID='" + ddl_teacher.SelectedValue + "' and   tcsm.section='" + ddl_section_serch.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + " order by cm.Position,tcsm.section asc";


                    }

                    BindRepeater(query, RPDetails);

                }
                else if (ddlclass.SelectedItem.Text != "All" && ddl_teacher.SelectedItem.Text == "All")
                {
                    if (ddl_section_serch.Text == "All")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where     tcsm.CategoryID='" + ddlclass.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + "  order by cm.Position,tcsm.section asc";
                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where      tcsm.CategoryID='" + ddlclass.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + "   order by cm.Position,tcsm.section asc";



                    }
                    else
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where      tcsm.CategoryID='" + ddlclass.Text + "' and tcsm.section='" + ddl_section_serch.Text + "' and tcsm.Session_id=" + ddlsession.SelectedValue + "  order by cm.Position,tcsm.section asc";




                    }
                     BindRepeater(query, RPDetails);
                }

                else if (ddlclass.SelectedItem.Text == "All" && ddl_teacher.SelectedItem.Text == "All")
                {
                    if (ddl_section_serch.Text == "All")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where   tcsm.Session_id=" + ddlsession.SelectedValue + "     order by cm.Position,tcsm.section asc";
                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where      tcsm.Session_id=" + ddlsession.SelectedValue + "     order by cm.Position,tcsm.section asc";
                    }
                    else
                    {


                        query = " Select tcsm.Acamedic_Semester_Id,tcsm.Type,tcsm.Session_id,cm.Course_Name,cm.course_id,csm.Subject_name,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select name from user_details where user_id=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join Add_course_table cm on tcsm.CategoryID=cm.course_id join Subject_Master csm on tcsm.CategoryID=csm.course_id and tcsm.AssignCourseID=csm.Subject_id  where   and tcsm.section='" + ddl_section_serch.Text + "'  and tcsm.Session_id=" + ddlsession.SelectedValue + "     order by cm.Position,tcsm.section asc";

                      
                    }
                    BindRepeater(query, RPDetails);
                }


            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindRepeater(string query, Repeater rPDetails)
        {
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count != 0)
            {
                
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
            else
            {
                print1.Visible = false;
                btn_excels.Visible = false;
                lbl_class22.Text = " Session : "+ddlsession.SelectedItem.Text+ " Class :"+ddlclass.SelectedItem.Text+ " Section :"+ddl_section_serch.SelectedItem.Text+ " Teacher : "+ ddl_teacher.SelectedItem.Text;
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                

            }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    SqlCommand cmd = new SqlCommand("Delete from TeacherCourseSubjectMaping where Id='" + lbl_Id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                    find_data();

                    Alertme("Subject maping deletion process has been done", "warning");
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


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export" + ddlclass.SelectedItem.Text + ".xls");
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
                    Alertme("SORRY! You have not permission for this work.", "warning");
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