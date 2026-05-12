using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.InstructorProfile
{
    public partial class promoted_to_class : System.Web.UI.Page
    {
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        UsesCode mycode = new UsesCode();
        My my = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null)
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
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["Branchid"] = mycode.getbranchid(Session["teacher"].ToString());
                    ViewState["teacher"] = Session["teacher"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table where course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");
                    My.bind_ddl_select(ddl_promoted_to_class, "Select Course_Name from Add_course_table order by Position asc");
                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and    UserID='" + ViewState["teacher"].ToString() + "'  and Session_id='" + ViewState["sesssionid"].ToString() + "'  order by section");
                 
            }
            catch (Exception ex)
            {
            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                    TextBox txt_promoted_to = ((TextBox)e.Item.FindControl("txt_promoted_to")) as TextBox; 
                    DataTable dt = mycode.FillData("select Promoted_to_class,School_reopen_on from Exam_promoted_to_class where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count == 0) 
                    {
                        if (ddl_promoted_to_class.SelectedItem.Text == "Select")
                        { }
                        else
                        {
                            txt_promoted_to.Text = ddl_promoted_to_class.Text; 
                        }
                    }
                    else
                    {
                        txt_promoted_to.Text = dt.Rows[0]["Promoted_to_class"].ToString();
                        if (txt_promoted_to.Text == "")
                        {
                            txt_promoted_to.Text = ddl_promoted_to_class.Text;
                        }
                    }

                    if (txt_school_reopen_on.Text == "")
                    {
                        txt_school_reopen_on.Text = dt.Rows[0]["School_reopen_on"].ToString();
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class.");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
                ddl_section.Focus();
            } 
            else
            {
                string query = "Select admissionserialnumber,studentname,rollnumber  from admission_registor where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and  Status='1' order by rollnumber";
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
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class.");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
                ddl_section.Focus();
            }
            else if (txt_school_reopen_on.Text == "")
            {
                Alert("Please enter school reopen on.");
                txt_school_reopen_on.Focus();
            }
            else
            {
                try
                {
                    save_marks();
                    Alert("Record has been saved successfully.");
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void save_marks()
        {
            int i;
            int gridview_rowcount = RPDetails.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                TextBox txt_promoted_to = (TextBox)RPDetails.Items[i].FindControl("txt_promoted_to");

                DataTable dt = mycode.FillData("select Id from Exam_promoted_to_class where Session_id=" + ViewState["sesssionid"].ToString() + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'  ");
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_promoted_to_class (Session_id,Class_id,Admission_no,Branch_id,Section,Promoted_to_class,School_reopen_on,Cretaed_by,Created_date,Created_idate,Created_time) values (@Session_id,@Class_id,@Admission_no,@Branch_id,@Section,@Promoted_to_class,@School_reopen_on,@Cretaed_by,@Created_date,@Created_idate,@Created_time)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                    cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    cmd.Parameters.AddWithValue("@Promoted_to_class", txt_promoted_to.Text);
                    cmd.Parameters.AddWithValue("@School_reopen_on", txt_school_reopen_on.Text);
                    cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["teacher"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Exam_promoted_to_class set Promoted_to_class=@Promoted_to_class,School_reopen_on=@School_reopen_on,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate,Updated_time=@Updated_time where Id=@Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Promoted_to_class", txt_promoted_to.Text);
                    cmd.Parameters.AddWithValue("@School_reopen_on", txt_school_reopen_on.Text);
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["teacher"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                    cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }
    }
}