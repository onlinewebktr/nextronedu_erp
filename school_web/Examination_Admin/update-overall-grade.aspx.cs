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
    public partial class update_overall_grade : System.Web.UI.Page
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
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                    find_firm_details();
                }
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_CourseCat.SelectedItem.Text == "Select")
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
                    Alertme("Please select term.", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    lbl_subject.Text = "Marks sheet for Class : " + ddl_CourseCat.SelectedItem.Text + ",  Section : " + ddl_section.SelectedItem.Text + ", Term : " + ddl_term.SelectedItem.Text;
                    find_students();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_students()
        {
            lbl_term_name.Text = ddl_term.SelectedItem.Text;
            string query = "Select distinct ar.admissionserialnumber,ar.studentname,ar.rollnumber from admission_registor ar  where ar.Session_id='" + ddlsession.SelectedValue + "' and ar.Class_id='" + ddl_CourseCat.SelectedValue + "' and ar.Section='" + ddl_section.Text + "'  and   ar.Branch_Id='" + ViewState["Branchid"].ToString() + "' and  ar.StudentStatus!='TC' and  ar.Status='1' order by ar.rollnumber";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                print1.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;
                DataTable dt = mycode.FillData("select * from Exam_overall_grade_termwise where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
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


        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
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
                    Alertme("Please select term.", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    save_marks();
                    Alertme("Marks has been saved successfully.", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_marks()
        {
            string qrys = "delete from Exam_overall_grade_termwise where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "'  and Branch_id='" + ViewState["Branchid"].ToString() + "'";
            My.exeSql(qrys);
            string Is_character = "0";
            int i;
            int gridview_rowcount = RPDetails.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");

                if (txt_marks.Text == "") { }
                else
                {
                    DataTable dt = mycode.FillData("select Id from Exam_overall_grade_termwise where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Exam_overall_grade_termwise (Session_id,Class_id,Section,Term,Admission_no,Marks,Branch_id,Created_by,Created_date,Created_idate) values (@Session_id,@Class_id,@Section,@Term,@Admission_no,@Marks,@Branch_id,@Created_by,@Created_date,@Created_idate)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Term", ddl_term.SelectedValue);
                        cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                        cmd.Parameters.AddWithValue("@Marks", txt_marks.Text);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        { }
                    }
                    else
                    {
                        string id = dt.Rows[0]["Id"].ToString();
                        SqlCommand cmd;
                        string query = "Update Exam_overall_grade_termwise set Marks=@Marks,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id=" + id + "";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Marks", txt_marks.Text);
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        { }
                    }
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
                    mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}