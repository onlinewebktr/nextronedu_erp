using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.InstructorProfile
{
    public partial class Set_Attendance_Student_Term_Wise : System.Web.UI.Page
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
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");

                    mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where    UserID='" + ViewState["teacher"].ToString() + "'  order by section");
                 

                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and    UserID='" + ViewState["teacher"].ToString() + "'  order by section");

                mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
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
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
                ddl_term.Focus();
            }
            else
            {


                string query = "Select admissionserialnumber,studentname,rollnumber,( select top 1 No_of_Class  from dbo.[Exam_Term_Details] where Session_Id=" + ViewState["sesssionid"].ToString() + " and Branch_Id='" + ViewState["Branchid"] + "' and Class_id=" + ddl_CourseCat.SelectedValue + " and Exam_Term_Id='" + ddl_term.SelectedValue + "') as  No_of_Class  from admission_registor where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Status='1' and StudentStatus!='TC' order by rollnumber";




                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = mycode.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    txt_mm.Text = "0";
                    lbl_max_marks.Text = "0";
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }
                else
                {
                    lbl_max_marks.Text = dt.Rows[0]["No_of_Class"].ToString();
                    txt_mm.Text = dt.Rows[0]["No_of_Class"].ToString();
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
        }
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;


                DataTable dt = mycode.FillData("select No_of_class_Attendance from Exam_Term_Wise_Attendance where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "'  and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    txt_marks.Text = "";

                }
                else
                {
                    txt_marks.Text = dt.Rows[0]["No_of_class_Attendance"].ToString();

                }
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                save_marks();
                Alert("Attendance has been saved successfully.");
            }
            catch (Exception ex)
            {
            }

        }

        private void save_marks()
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class.");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section.");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term.");
                ddl_term.Focus();
            }
            else
            {
                int i;
                int gridview_rowcount = RPDetails.Items.Count;
                for (i = 0; i < gridview_rowcount; i++)
                {
                    Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                    TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                    if (txt_marks.Text != "")
                    {
                        DataTable dt = mycode.FillData("select Id from Exam_Term_Wise_Attendance where Session_id=" + ViewState["sesssionid"].ToString() + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Section='" + ddl_section.Text + "' and Exam_Term_Id=" + ddl_term.SelectedValue + "   and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                        if (dt.Rows.Count == 0)
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Term_Wise_Attendance (Session_id,Branch_id,Section,Admission_no,Class_id,No_of_class_Attendance,Cretaed_by,Created_date,Exam_Term_Id) values (@Session_id,@Branch_id,@Section,@Admission_no,@Class_id,@No_of_class_Attendance,@Cretaed_by, @Created_date,@Exam_Term_Id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", ddl_term.SelectedValue);
                            cmd.Parameters.AddWithValue("@No_of_class_Attendance", txt_marks.Text);
                            cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["teacher"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());


                            cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);

                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            string id = dt.Rows[0]["Id"].ToString();
                            SqlCommand cmd;
                            string query = "Update Exam_Term_Wise_Attendance set  No_of_class_Attendance=@No_of_class_Attendance,Cretaed_by=@Cretaed_by,Created_date=@Created_date  where  Id= @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@No_of_class_Attendance", txt_marks.Text);
                            cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["teacher"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());
                            cmd.Parameters.AddWithValue("@Id", id);


                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }
                    else
                    {

                    }
                }

            }

        }

    
    }
}