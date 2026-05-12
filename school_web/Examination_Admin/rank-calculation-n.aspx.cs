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
    public partial class rank_calculation_n : System.Web.UI.Page
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
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        find_firm_details();
                        bind_session();
                        bind_class();
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor order by Section"); 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = My.dataTable("select firm_id from Firm_Details");
            ViewState["firm_id"] = dt.Rows[0]["firm_id"].ToString();
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
                else
                {
                    ViewState["ISUpdated"] = "0";
                    save_rank();
                    if (ViewState["ISUpdated"].ToString() == "1")
                    {
                        Alertme("Final Year rank has been calculated successfully.", "success");

                        string qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t1.Percentage as Mark_percentage,t1.* from Exam_rank_master_final_year t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Section='" + ddl_section.SelectedItem.Text + "' and t2.Status='1'  order by Rank asc";
                        DataTable dt = mycode.FillData(qry);
                        if (dt.Rows.Count == 0)
                        {
                            Alertme("Sorry there are no data list exist", "warning");
                            rd_view.DataSource = null;
                            rd_view.DataBind();
                            grdswpr.Visible = false;
                        }
                        else
                        {
                            rd_view.DataSource = dt;
                            rd_view.DataBind();
                            grdswpr.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_rank()
        {
            mycode.executequery("delete from Exam_rank_master_final_year where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'");
            string qry = "select * from Exam_overall_no_for_rank where Session_id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.SelectedItem.Text + "' and Term_id='Final' order by Persentage desc";
            DataTable dt = mycode.FillData(qry);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                Alertme("Please calculate term rank first.", "warning");
            }
            else
            {
                double prev_mark = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    double percentage = My.toDouble(dr["Persentage"].ToString());
                    if (prev_mark != percentage)
                    {
                        i++;
                        prev_mark = percentage;
                    }



                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_rank_master_final_year (Session_id,Branch_id,Class_id,Section,Admission_no,Percentage,Rank,Created_by,Created_date,Created_idate,Total_obtained_mark,Total_full_mark) values (@Session_id,@Branch_id,@Class_id,@Section,@Admission_no,@Percentage,@Rank,@Created_by,@Created_date,@Created_idate,@Total_obtained_mark,@Total_full_mark)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", dr["Session_id"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_id", dr["Branch_id"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Section", dr["Section"].ToString());
                    cmd.Parameters.AddWithValue("@Admission_no", dr["Admission_no"].ToString());
                    cmd.Parameters.AddWithValue("@Percentage", My.toDouble(dr["Persentage"].ToString()).ToString("0.00"));
                    cmd.Parameters.AddWithValue("@Rank", i);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Total_obtained_mark", dr["Obtained_mark"].ToString());
                    cmd.Parameters.AddWithValue("@Total_full_mark", dr["Full_mark"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        ViewState["ISUpdated"] = "1";
                    }
                }
            }
        }
    }
}