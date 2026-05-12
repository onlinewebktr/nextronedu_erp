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
    public partial class publish_result : System.Web.UI.Page
    {
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
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();
                        bind_class();
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_term.Items.Insert(0, new ListItem("Select", "0"));
                        //bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }

        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            string qry = "";
            qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Section='" + ddl_section.SelectedItem.Text + "' and  em.Term=" + ddl_term.SelectedValue + " and em.Admission_no not in (select Admission_no from Exam_publish_result where Session_id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Admission_no=em.Admission_no and Term_id='" + ddl_term.SelectedValue + "') order by ar.rollnumber asc";
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_pub_result.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_pub_result.Visible = true;
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    bind_terms();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_terms()
        {
           bind_term_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Term_Name asc");
        }

        private void bind_term_ddl_with_id(DropDownList ddl, string strQuery)
        { 
            DataTable dt = My.dataTable(strQuery); 
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
            ddl.Items.Insert(rowcount, new ListItem("Final", "Final"));
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlsession.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("please select section.", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("please select term.", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void hdrChkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int growcount = rd_view.Items.Count;
                int k = 0;
                for (int i = 0; i < growcount; i++)
                {
                    CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("rowChkBox");
                    if (hdrChkBox.Checked == true)
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                    k++;
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_pub_result_Click(object sender, EventArgs e)
        {
            try
            {
                save_publish_result();
                if (ViewState["statusUp"].ToString() == "1")
                { 
                    Alertme("Result has been published successfully.", "success");
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_publish_result()
        {
            //=====================
            ViewState["statusUp"] = "0";
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {
                    Label lbl_session_id = (Label)rd_view.Items[i].FindControl("lbl_session_id");
                    Label lbl_term_id = (Label)rd_view.Items[i].FindControl("lbl_term_id");
                    Label lbl_adm_no = (Label)rd_view.Items[i].FindControl("lbl_adm_no");
                    if (mycode.IsUserExist("select Admission_no from Exam_publish_result where Admission_no='" + lbl_adm_no.Text + "' and Session_id='" + lbl_session_id.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Term_id='" + lbl_term_id.Text + "'"))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Exam_publish_result (Session_id,Branch_id,Admission_no,Term_id,Status,Published_date,Published_idate) values (@Session_id,@Branch_id,@Admission_no,@Term_id,@Status,@Published_date,@Published_idate)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", lbl_session_id.Text);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                        cmd.Parameters.AddWithValue("@Term_id", lbl_term_id.Text);
                        cmd.Parameters.AddWithValue("@Status", "1");
                        cmd.Parameters.AddWithValue("@Published_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Published_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    ViewState["statusUp"] = "1";
                }
                else
                {
                    k++;
                }
            }
            if (k == growcount)
            {
                Alertme("Please check minimum one row list.", "warning");
                ViewState["statusUp"] = "0";
            }
        }
    }
}