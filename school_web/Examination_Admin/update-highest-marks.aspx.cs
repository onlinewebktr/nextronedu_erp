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
    public partial class update_highest_marks : System.Web.UI.Page
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

        UsesCode mycode = new UsesCode();
        My my = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());

                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");
                    ddl_CourseCat.SelectedValue = My.get_top_one_class();
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");

                    mycode.bind_all_ddl_with_all(ddl_subject, "select Subject_name,Subject_id from Subject_Master where course_id='" + ddl_CourseCat.SelectedValue + "' order by Subject_position asc");
                }
            }
            catch (Exception ex)
            {
            }
        }




        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else
                {
                    find_subjects();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_subjects()
        {
            ViewState["SectioN"] = ddl_section.Text;
            string query = "";
            if (ddl_subject.SelectedItem.Text == "ALL")
            {
                query = "select *,(select top 1 Course_Name from Add_course_table where course_id=Subject_Master.course_id) as Class_name from Subject_Master where course_id='" + ddl_CourseCat.SelectedValue + "' order by Subject_position asc";
            }
            else
            {
                query = "select *,(select top 1 Course_Name from Add_course_table where course_id=Subject_Master.course_id) as Class_name from Subject_Master where course_id='" + ddl_CourseCat.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' order by Subject_position asc";
            }

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                save_marks();
                Alertme("Marks has been saved successfully.", "success");
            }
            catch (Exception ex)
            {
            }
        }

        private void save_marks()
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_section.Focus();
            }
            else
            {
                int i;
                int gridview_rowcount = RPDetails.Items.Count;
                for (i = 0; i < gridview_rowcount; i++)
                {
                    Label lbl_subject_id = (Label)RPDetails.Items[i].FindControl("lbl_subject_id");
                    Label lbl_course_id = (Label)RPDetails.Items[i].FindControl("lbl_course_id");
                    TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks"); 

                    DataTable dt = mycode.FillData("select Id from Exam_highest_mark_of_subject where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + lbl_course_id.Text + "' and Section='" + ViewState["SectioN"].ToString() + "' and Subject_id='" + lbl_subject_id.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Exam_highest_mark_of_subject (Session_id,Branch_id,Class_id,Section,Subject_id,Marks,Created_date,Created_idate,Created_by) values (@Session_id,@Branch_id,@Class_id,@Section,@Subject_id,@Marks,@Created_date,@Created_idate,@Created_by)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", lbl_course_id.Text);
                        cmd.Parameters.AddWithValue("@Section", ViewState["SectioN"].ToString());
                        cmd.Parameters.AddWithValue("@Subject_id", lbl_subject_id.Text);
                        cmd.Parameters.AddWithValue("@Marks", txt_marks.Text); 
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate()); 
                        if (My.InsertUpdateData(cmd))
                        { 
                        }
                    }
                    else
                    {
                        string id = dt.Rows[0]["Id"].ToString();  
                        SqlCommand cmd;
                        string query = "Update Exam_highest_mark_of_subject set Marks=@Marks where Id=" + id + " ";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Marks", txt_marks.Text); 
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        { 
                        }
                    } 
                }
            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_course_id = ((Label)e.Item.FindControl("lbl_course_id")) as Label;
                Label lbl_subject_id = ((Label)e.Item.FindControl("lbl_subject_id")) as Label;
                TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;

                DataTable dt = mycode.FillData("select Marks from Exam_highest_mark_of_subject where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + lbl_course_id.Text + "' and Section='" + ViewState["SectioN"].ToString() + "' and Subject_id='" + lbl_subject_id.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    txt_marks.Text = ""; 
                }
                else
                {
                    txt_marks.Text = dt.Rows[0]["Marks"].ToString(); 
                }
            }
        }
    }
}