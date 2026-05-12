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
    public partial class Commentary_Remarks_Entry : System.Web.UI.Page
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
                    mycode.bind_all_ddl_with_id(ddl_remarkstype, "Select   Commentary_Remark_Types, Remaks_Id from Exam_Commentary_Remark_Types where Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Commentary_Remark_Types asc");


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


        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;
                DropDownList ddl_remarkstypenew = ((DropDownList)e.Item.FindControl("ddl_remarkstypenew")) as DropDownList;

                mycode.bind_ddl_no_select(ddl_remarkstypenew, "Select   Commentary_Remark_Types from Exam_Commentary_Remark_Types where Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Commentary_Remark_Types asc");


                DataTable dt = mycode.FillData("select Remarks from Exam_Commentary_Remark_Term_Wise_Entry where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "'  and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' ");
                if (dt.Rows.Count == 0)
                {
                    txt_marks.Text = ddl_remarkstype.SelectedItem.Text;
                    ddl_remarkstypenew.Text = ddl_remarkstype.SelectedItem.Text;
                }
                else
                {
                    txt_marks.Text = dt.Rows[0]["Remarks"].ToString();
                    ddl_remarkstypenew.Text = dt.Rows[0]["Remarks"].ToString();
                }
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
                string query = "Select admissionserialnumber,studentname,rollnumber  from admission_registor where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Status='1' and StudentStatus!='TC' order by rollnumber";
                lbl_activity_type.Text = ddl_remarkstype.SelectedItem.Text;
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
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
                ddl_term.Focus();
            }
            else if (ddl_remarkstype.SelectedItem.Text == "Select")
            {
                Alert("Please personality traits");
                ddl_term.Focus();
            }
            else
            {
                try
                {
                    save_marks();
                    Alert("Commentary Remarks has been saved successfully");
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
                
                DropDownList ddl_remarkstypenew = (DropDownList)RPDetails.Items[i].FindControl("ddl_remarkstypenew");

                
                    DataTable dt = mycode.FillData("select Id from Exam_Commentary_Remark_Term_Wise_Entry where Session_id=" + ViewState["sesssionid"].ToString() + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Section='" + ddl_section.Text + "' and Exam_Term_Id=" + ddl_term.SelectedValue + "   and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'   ");
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Exam_Commentary_Remark_Term_Wise_Entry (Session_id,Branch_id,Section,Class_id,Exam_Term_Id,Remarks_id,Admission_no,Remarks,Cretaed_by,Created_date) values (@Session_id,@Branch_id,@Section,@Class_id,@Exam_Term_Id,@Remarks_id,@Admission_no,@Remarks,@Cretaed_by,@Created_date)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Exam_Term_Id", ddl_term.SelectedValue);
                        cmd.Parameters.AddWithValue("@Remarks_id", ddl_remarkstype.SelectedValue);
                        cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                        cmd.Parameters.AddWithValue("@Remarks", ddl_remarkstypenew.Text);
                        cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["teacher"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());



                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        string id = dt.Rows[0]["Id"].ToString();
                        SqlCommand cmd;
                        string query = "Update Exam_Commentary_Remark_Term_Wise_Entry set  Remarks=@Remarks,Cretaed_by=@Cretaed_by,Created_date=@Created_date  where  Id= @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Remarks", ddl_remarkstypenew.Text);
                        cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["teacher"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());
                        cmd.Parameters.AddWithValue("@Id", id);


                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                 
            }
        }

    }
}