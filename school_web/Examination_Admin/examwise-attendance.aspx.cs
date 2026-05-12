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
    public partial class examwise_attendance : System.Web.UI.Page
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


                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                    mycode.bind_all_ddl_with_id(ddl_term, "select DISTINCT Term,Term_id from Exam_termwise_pt_for_attendance where Session='" + ddlsession.SelectedValue + "'");
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
                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term", "warning");
                ddl_term.Focus();
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam", "warning");
                ddl_exam.Focus();
            }
            else
            { 
                string totl_no_of_class = txt_total_no_of_class.Text; 
                string query = "Select admissionserialnumber,studentname,rollnumber,'" + totl_no_of_class + "' as  No_of_Class from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and   Status='1' and StudentStatus!='TC' order by rollnumber";
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
                    lbl_max_marks.Text = txt_total_no_of_class.Text;
                    txt_mm.Text = txt_total_no_of_class.Text;
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
                TextBox txt_no_of_class = ((TextBox)e.Item.FindControl("txt_no_of_class")) as TextBox;
                txt_no_of_class.Text = txt_total_no_of_class.Text;

                DataTable dt = mycode.FillData("select * from Exam_Exam_Wise_Attendance where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "'  and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Exam_type='" + ddl_exam.Text + "'");
                if (dt.Rows.Count == 0)
                {
                    txt_marks.Text = "";
                }
                else
                {
                    txt_marks.Text = dt.Rows[0]["No_of_class_Attendance"].ToString();

                    try
                    {
                        if (dt.Rows[0]["Total_no_of_class"].ToString() == "")
                        {
                        }
                        else
                        {
                            txt_no_of_class.Text = dt.Rows[0]["Total_no_of_class"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                save_marks();
                Alertme("Attendance has been saved successfully.", "success");
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
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term", "warning");
                ddl_term.Focus();
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam", "warning");
                ddl_exam.Focus();
            }
            else
            {
                int i;
                int gridview_rowcount = RPDetails.Items.Count;
                for (i = 0; i < gridview_rowcount; i++)
                {
                    Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                    TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                    TextBox txt_no_of_class = (TextBox)RPDetails.Items[i].FindControl("txt_no_of_class");
                    if (txt_marks.Text != "" || txt_no_of_class.Text != "")
                    {
                        DataTable dt = mycode.FillData("select Id from Exam_Exam_Wise_Attendance where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Section='" + ddl_section.Text + "' and Exam_Term_Id=" + ddl_term.SelectedValue + "   and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Exam_type='" + ddl_exam.Text + "'");
                        if (dt.Rows.Count == 0)
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Exam_Wise_Attendance (Session_id,Branch_id,Section,Admission_no,Class_id,No_of_class_Attendance,Cretaed_by,Created_date,Exam_Term_Id,Total_no_of_class,Exam_type) values (@Session_id,@Branch_id,@Section,@Admission_no,@Class_id,@No_of_class_Attendance,@Cretaed_by, @Created_date,@Exam_Term_Id,@Total_no_of_class,@Exam_type)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", ddl_term.SelectedValue);
                            cmd.Parameters.AddWithValue("@No_of_class_Attendance", txt_marks.Text);
                            cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());
                            cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                            cmd.Parameters.AddWithValue("@Total_no_of_class", txt_no_of_class.Text);
                            cmd.Parameters.AddWithValue("@Exam_type", ddl_exam.Text);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            string id = dt.Rows[0]["Id"].ToString();
                            SqlCommand cmd;
                            string query = "Update Exam_Exam_Wise_Attendance set  No_of_class_Attendance=@No_of_class_Attendance,Cretaed_by=@Cretaed_by,Created_date=@Created_date,Total_no_of_class=@Total_no_of_class  where  Id= @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@No_of_class_Attendance", txt_marks.Text);
                            cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());
                            cmd.Parameters.AddWithValue("@Total_no_of_class", txt_no_of_class.Text);
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

        protected void txt_marks_TextChanged(object sender, EventArgs e)
        {
            TextBox lnk = (TextBox)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_no_of_class = (Label)row.FindControl("lbl_no_of_class");
            TextBox txt_marks = (TextBox)row.FindControl("txt_marks");
            if (My.toInt(lbl_no_of_class.Text) >= My.toInt(txt_marks.Text))
            {
            }
            else
            {
                Alertme("Sorry you can't attendance more than actual term wise attendance.", "warning");
            }
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                My.bind_ddl_select(ddl_exam, "select Pt_name from Exam_termwise_pt_for_attendance where Term_id='" + ddl_term.SelectedValue + "' and Session='" + ddlsession.SelectedValue + "'");
            }
            catch (Exception ex)
            {
            }
        }
    }
}