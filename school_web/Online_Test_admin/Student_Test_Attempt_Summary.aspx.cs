using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Test_admin
{
    public partial class Student_Test_Attempt_Summary : System.Web.UI.Page
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
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Student_Test_Attempt_Summary.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_Onlinetest(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        ddl_class.SelectedValue = My.get_top_one_class();
                        txt_date.Text = mycode.date();
                        Bind_data_all();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Test_Attempt_Summary");
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
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_all();
        }
        private void Bind_data_all()
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_Student_Test_Attempt_Summary");
                cmd.Parameters.AddWithValue("@live_date", txt_date.Text);
                if(ddl_class.SelectedItem.Text=="ALL")
                {
                    cmd.Parameters.AddWithValue("@classid", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@classid", ddl_class.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@sp_status", "Summary");
                DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    
                    btn_excels.Visible = false;
                    print1.Visible = false;
                    Alertme("Sorry, there are no records available.", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                    

                    GrdView.DataSource = null;
                    GrdView.DataBind();
                    
                }
                else
                {
                    btn_excels.Visible = true;
                    print1.Visible = true;
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    GrdView.DataSource = dt;
                    GrdView.DataBind();
                    
                }

            }
            catch
            {

            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                 
                string exportname = My.with_excel_name("Student_Test_Summary");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + exportname + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

      
    }
}