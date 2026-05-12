using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class optional_subject : System.Web.UI.Page
    {
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
                        ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table order by Position asc");
                        get_template();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "IdCardTemplate");
            }
        }

        private void get_template()
        {
            DataTable dt = mycode.FillData("select * from (select *,(select top 1 Course_Name from Add_course_table where course_id=t1.Class_id) as Class_name,(select top 1 Term_Name from Exam_Term_Details where Session_Id=t1.Session_id and Class_id=t1.Class_id and Exam_Term_Id=t1.Term_id) as Term_name, (select top 1 Subject_name from Subject_Master where course_id=t1.Class_id and Subject_id=t1.Subject_id) as Subject_name from Exam_optional_subject_master t1) t order by Subject_name asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Pleae select session.", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alertme("Pleae select class.", "warning");
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    ddl_term.Focus();
                    Alertme("Pleae select term.", "warning");
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    ddl_subject.Focus();
                    Alertme("Pleae select subject.", "warning");
                }
                else
                {
                    save_data(); 
                }
            }
            catch (Exception ex)
            {
            }
        }



        My mycode = new My();
        private void save_data()
        {
            if (mycode.IsUserExist("select Subject_id from Exam_optional_subject_master where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_optional_subject_master (Session_id,Class_id,Term_id,Subject_id,Updated_by,Updated_date) values (@Session_id,@Class_id,@Term_id,@Subject_id,@Updated_by,@Updated_date)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Subject has been added successfully.", "success");
                    get_template();
                }
            }
            else
            {
                Alertme("Your selected subject is already exist.", "warning");
            }
        }





        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_term(); fetch_subjects();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_subjects()
        {
            mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm where sm.course_id='" + ddl_class.SelectedValue + "' and sm.Is_mandatory=1 and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and Subject_id not in(select Subject_id from Exam_optional_subject_master where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "') order by sm.Subject_position");
        }

        private void fetch_term()
        {
            mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                mycode.executequery("delete from  Exam_optional_subject_master where  Id='" + lbl_id.Text + "'");
                Alertme("Subject details has been deleted", "success");
                get_template();
            }
            catch
            {
            } 
        } 
    }
}