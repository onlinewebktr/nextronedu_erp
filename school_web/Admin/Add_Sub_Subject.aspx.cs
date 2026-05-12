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
    public partial class Add_Sub_Subject : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_course, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_course_search, "Select Course_Name,course_id from Add_course_table order by Position");
                        //Bind_All_subject();

                        ddl_course_search.SelectedValue = My.get_top_one_class();


                        Bind_All_subject();
                        find_firm_details();

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




                    }
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
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

        private void Bind_All_subject()
        {
            string query = "";
            if (ddl_course_search.SelectedItem.Text == "Select")
            {
                query = "Select ssm.*,cm.Course_Name,sm.Subject_name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id join Sub_Subject_Master ssm on ssm.course_id=sm.course_id and ssm.Subject_id=sm.Subject_id order BY Course_Name";
            }
            else
            {
                query = "Select ssm.*,cm.Course_Name,sm.Subject_name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id join Sub_Subject_Master ssm on ssm.course_id=sm.course_id and ssm.Subject_id=sm.Subject_id  where cm.course_id=" + ddl_course_search.SelectedValue + " order BY Course_Name";
            }

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no subject list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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

        protected void ddl_course_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select class name", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where course_id=" + ddl_course.SelectedValue + " order by  Subject_position");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }





        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select course name", "warning");
            }
            else if (ddl_subject.SelectedValue == "")
            {
                Alertme("Please select subject name", "warning");
            }
            else
            {
                if (btn_Submit.Text == "Add")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        SqlCommand cmd;
                        DataTable dt = mycode.FillData("Select * from Sub_Subject_Master where course_id='" + ddl_course.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Sub_Subject_name='" + txt_subjectsubject.Text + "' ");
                        if (dt.Rows.Count == 0)
                        {
                            string subsubjectid = create_subjectid();
                            string query = "INSERT INTO Sub_Subject_Master (course_id,Subject_id,Sub_Subject_id,Branch_id,Sub_Subject_name,Sub_Subject_position,Date,Idate,Time,Created_by) values (@course_id,@Subject_id,@Sub_Subject_id,@Branch_id,@Sub_Subject_name,@Sub_Subject_position,@Date,@Idate,@Time,@Created_by)";
                            cmd = new SqlCommand(query);

                            cmd.Parameters.AddWithValue("@course_id", ddl_course.SelectedValue);
                            cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Sub_Subject_id", subsubjectid);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Sub_Subject_name", txt_subjectsubject.Text.Trim());
                            cmd.Parameters.AddWithValue("@Sub_Subject_position", txt_subjectposition.Text);
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {

                                Alertme("Sub-Subject has been sucessfully added", "success");
                                string msg = ViewState["Userid"].ToString() + " Create Subject, subject id=" + subsubjectid + " subject name=" + txt_subjectsubject.Text.Trim() + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                                mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                                txt_subjectsubject.Text = "";
                                txt_subjectposition.Text = "";
                                btn_cancel.Visible = false;
                                btn_Submit.Text = "Add";
                                Bind_All_subject();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            }
                        }
                        else
                        {
                            Alertme("Sorry your sub-subject name already exit ", "warning");
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
                else
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        SqlCommand cmd;
                        DataTable dt = mycode.FillData("Select * from Sub_Subject_Master where course_id='" + ddl_course.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "'  and Sub_Subject_name='" + txt_subjectsubject.Text.Trim() + "' and Sub_Subject_id!=" + hd_subjectid.ValidateRequestMode + " ");
                        if (dt.Rows.Count == 0)
                        {
                            string query = "Update Sub_Subject_Master set course_id=@course_id,Subject_id=@Subject_id,Sub_Subject_id=@Sub_Subject_id,Branch_id=@Branch_id,Sub_Subject_name=@Sub_Subject_name,Sub_Subject_position=@Sub_Subject_position,Date=@Date,Idate=@Idate,Time=@Time where Sub_Subject_id = @Sub_Subject_id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@course_id", ddl_course.SelectedValue);
                            cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                            cmd.Parameters.AddWithValue("@Sub_Subject_id", hd_subjectid.Value);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Sub_Subject_name", txt_subjectsubject.Text.Trim());

                            cmd.Parameters.AddWithValue("@Sub_Subject_position", txt_subjectposition.Text);
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());

                            if (My.InsertUpdateData(cmd))
                            {


                                Alertme("Sub-Subject has been sucessfully updated", "success");
                                string msg = ViewState["Userid"].ToString() + " Create Subject, subject id=" + hd_subjectid.Value + " subject name=" + txt_subjectsubject.Text.Trim() + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                                mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                                txt_subjectsubject.Text = "";
                                txt_subjectposition.Text = "";
                                btn_cancel.Visible = false;
                                btn_Submit.Text = "Add";
                                Bind_All_subject();
                            }
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
            }
        }

        private string create_subjectid()
        {
            bool duplicate = false;
            string subject_id = mycode.auto_serial("Sub_Subject_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Sub_Subject_id from dbo.[Sub_Subject_Master] where Sub_Subject_id='" + subject_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    subject_id = mycode.auto_serial("Sub_Subject_id");
                }
            }
            return subject_id;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_subjectsubject.Text = "";
            txt_subjectposition.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void ddl_course_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_course_search.SelectedItem.Text == "Select")
            {
                Alertme("Sorry your class name already exit ", "warning");
            }
            else
            {
                Bind_All_subject();
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
                    Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
                    Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                    Label lbl_Sub_Subject_Master = (Label)row.FindControl("lbl_Sub_Subject_Master");
                    Label lbl_Sub_Subject_id = (Label)row.FindControl("lbl_Sub_Subject_id");
                    Label lbl_Subject_position = (Label)row.FindControl("lbl_Subject_position");
                    ddl_course.SelectedValue = lbl_Class_id.Text;
                    ddl_subject.SelectedValue = lbl_Subject_id.Text;
                    txt_subjectsubject.Text = lbl_Sub_Subject_Master.Text;
                    txt_subjectposition.Text = lbl_Subject_position.Text;
                    hd_subjectid.Value = lbl_Sub_Subject_id.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                    Label lbl_Sub_Subject_id = (Label)row.FindControl("lbl_Sub_Subject_id");
                    mycode.executequery("delete from Sub_Subject_Master where Sub_Subject_id='" + lbl_Sub_Subject_id.Text + "' ");
                    Bind_All_subject();
                    txt_subjectsubject.Text = "";
                    txt_subjectposition.Text = "";
                    btn_cancel.Visible = false;
                    btn_Submit.Text = "Add";
                    Alertme("Sub-subject  name has been deleted  done ", "success");
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
    }
}