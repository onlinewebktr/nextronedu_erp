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

namespace school_web.Admin
{
    public partial class Print_Sport_Certificte : System.Web.UI.Page
    {
        My mycode = new My();
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

                        string pagename_current = "certificate-master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        find_firm_details();
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();
                       
                        try
                        {
                            if (Session["querysports"].ToString() != null)
                            {
                                bind_grid_data(Session["querysports"].ToString());
                            }
                            else
                            {
                                bind_created_certificate();
                            }
                        }
                        catch
                        {
                            bind_created_certificate();
                        }
                        

                       
                        
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Print_Sport_Certificte");
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
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
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

        private void bind_created_certificate()
        {
            string query = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                query = "select t1.*,t2.studentname,t2.fathername,t2.class,t2.Section,t2.rollnumber,t2.session from Certificate_Master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Certificate_type='Sport' and t1.Session_id=" + ddlsession.SelectedValue + " order by t3.Position,t2.Section,t2.rollnumber asc";
            }
            else

            {
                query = "select t1.*,t2.studentname,t2.fathername,t2.class,t2.Section,t2.rollnumber,t2.session from Certificate_Master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Certificate_type='Sport' and t1.Session_id=" + ddlsession.SelectedValue + " and t1.Class_id='" + ddlclass.SelectedValue + "' order by t3.Position,t2.Section,t2.rollnumber asc";
            }
            bind_grid_data(query);
            
        }

        private void bind_grid_data(string query)
        {
            ViewState["query"] = query;
            Session["querysports"] = query;
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                lbl_class22.Text = "";
                btn_excels.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
                Alertme("Data Not Found...", "warning");
            }
            else
            {

                lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
                btn_excels.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                bind_created_certificate();

            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string excelname = My.with_excel_name("Sports");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename="+ excelname+ ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
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


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_certificate_no = (Label)row.FindControl("lbl_certificate_no");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                mycode.executequery("delete from Certificate_Master where Id=" + lbl_id.Text + "  and Certificate_type='Sport'");
                string desc = "Sport certificate has been delete for certificate no. " + lbl_certificate_no.Text + ".";
                log_hostory.delete_log(lbl_session_id.Text, lbl_class_id.Text, lbl_adm_no.Text, "Sport Certificate", desc, "sports_certificate.aspx", ViewState["Userid"].ToString());

                Alertme("Record has been deleted successfully.", "success");
                bind_created_certificate();
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }
        }


        #region update
        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
            Label lbl_id = (Label)row.FindControl("lbl_id");
            Label lbl_class = (Label)row.FindControl("lbl_class");
            Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
            Label lbl_Participated_in = (Label)row.FindControl("lbl_Participated_in");
            Label lbl_Division = (Label)row.FindControl("lbl_Division");
            Label lbl_Remarks = (Label)row.FindControl("lbl_Remarks");
            Label lbl_Issue_date = (Label)row.FindControl("lbl_Issue_date");
            ViewState["id"] = lbl_id.Text;
            lbl_admisn_no.Text = lbl_adm_no.Text;
            lbl_std_name.Text = lbl_studentname.Text;
            lbl_classss.Text = lbl_class.Text;
            txt_Participated_in_the.Text = lbl_Participated_in.Text;
            txt_Division.Text = lbl_Division.Text;
            txt_Remarks.Text = lbl_Remarks.Text;
            txt_issue_date.Text = lbl_Issue_date.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
 
        }
        #endregion

        protected void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Participated_in_the.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    Alertme("Please enter  participated name", "warning");
                    txt_Participated_in_the.Focus();
                }
                else if (txt_Division.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    Alertme("Please enter  division name", "warning");
                    txt_Division.Focus();
                }
                else if (txt_Remarks.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    Alertme("Please enter  remarks", "warning");
                    txt_Remarks.Focus();
                }
                else if (txt_issue_date.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter issue date.", "warning");
                    txt_issue_date.Focus();
                }
                else
                {
                    SqlCommand cmd;
                    

                    string query = "Update Certificate_Master set  Participated_in=@Participated_in,Division=@Division,Remarks=@Remarks,Create_date=@Create_date,Create_idate=@Create_idate,User_id=@User_id,Issue_date=@Issue_date where Id=@Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                    cmd.Parameters.AddWithValue("@Participated_in", txt_Participated_in_the.Text.Trim());
                    cmd.Parameters.AddWithValue("@Division", txt_Division.Text.Trim());
                    cmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text.Trim());
                    cmd.Parameters.AddWithValue("@Create_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Create_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Issue_date", txt_issue_date.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Sport certificate has been updated successfully.", "success");

                        try
                        {
                            SqlCommand cmd1 = new SqlCommand("sp_Sports_Participate_Master_Insert");
                            cmd1.Parameters.AddWithValue("@Participate_Name", txt_Participated_in_the.Text.Trim());
                            cmd1.Parameters.AddWithValue("@Division_name", txt_Division.Text.Trim());
                            cmd1.Parameters.AddWithValue("@Remark_name", txt_Remarks.Text.Trim());
                            cmd1.Parameters.AddWithValue("@Date", mycode.date());
                            cmd1.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                            int rowsAffected = Store_procedure_code.executeNonQuery(cmd1);
                            if (rowsAffected > 0)
                            {
                            }
                        }
                        catch
                        {

                        }

                        bind_grid_data(ViewState["query"].ToString());





                    }

                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}