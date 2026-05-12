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
    public partial class Set_Course_Fee : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Set_Online_Fee.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id_onlinereg();

                        mycode.bind_all_ddl_with_id(ddl_session_fee, "Select Session,session_id from session_details order by Session asc");
                        ddl_session_fee.SelectedValue = My.get_session_id_onlinereg();

                        mycode.bind_all_ddl_with_id(ddl_coursename, "Select Course_Name,course_id from Add_course_table order by Position asc");

                        bind_test(); 
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Course_Fee");
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
                //lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                //lbl_website.Text = dt.Rows[0]["website"].ToString();
                //lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void bind_test()
        {
            mycode.bind_all_ddl_with_id(ddl_test, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddl_session_fee.SelectedValue + "' and  Is_active=1 order by  Test_name asc");

            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddl_session_fee.SelectedValue + "' order by  Test_name asc");

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
        private void bind_grd_view()
        {

            string query = " Select orf.*,act.Course_Name,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Active' WHEN Isactive = '0' THEN 'Inactive'  WHEN Isactive = '' THEN 'Inactive' END AS activestatus,(Select top  1 Test_name from Online_reg_exam_test_master where Session_id=orf.Session_id and Test_id=orf.Test_id) as Test_name   from Online_reg_fees orf   join Add_course_table  act on act.course_id=orf.Class_id where Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "' order by act.Position asc";

            bind_final_grid_data(query);


        }

        private void bind_final_grid_data(string query)
        {
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData(query);
            ViewState["query"] = query;
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
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

        protected void ddl_test_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_test_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select test name", "warning");
            }
            else
            {
                string query = " Select orf.*,act.Course_Name,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Active' WHEN Isactive = '0' THEN 'Inactive'  WHEN Isactive = '' THEN 'Inactive' END AS activestatus,(Select top  1 Test_name from Online_reg_exam_test_master where Session_id=orf.Session_id and Test_id=orf.Test_id) as Test_name   from Online_reg_fees orf   join Add_course_table  act on act.course_id=orf.Class_id where Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "' order by act.Position asc";
                bind_final_grid_data(query);
            }

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                { 
                    txt_coursefee.Text = "";
                    hd_id.Value = "";
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_courseid = (Label)row.FindControl("lbl_courseid");
                    Label lbl_course_Name = (Label)row.FindControl("lbl_course_Name");
                    Label lbl_Course_Fee = (Label)row.FindControl("lbl_Course_Fee");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");
                    Label lbl_Start_Date = (Label)row.FindControl("lbl_Start_Date");
                    Label lbl_end_date = (Label)row.FindControl("lbl_end_date");
                    Label lbl_test_id = (Label)row.FindControl("lbl_test_id");
                    Label lbl_no_seat = (Label)row.FindControl("lbl_no_seat");
                    Label lbl_no_application = (Label)row.FindControl("lbl_no_application");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_sessions_ids = (Label)row.FindControl("lbl_sessions_ids");
                    Label lbl_payment_mode = (Label)row.FindControl("lbl_payment_mode");
                    ddl_session_fee.SelectedValue = lbl_sessions_ids.Text;
                    ddl_test.SelectedValue = lbl_test_id.Text;
                    ddl_coursename.SelectedValue = lbl_courseid.Text;

                    ddl_session_fee.Enabled = false;
                    ddl_test.Enabled = false;
                    ddl_coursename.Enabled = false;
                    ddl_session_fee.CssClass = "form-select";
                    ddl_test.CssClass = "form-select";
                    ddl_coursename.CssClass = "form-select";
                    ddl_payment_mode.Text = lbl_payment_mode.Text;

                    txt_coursefee.Text = lbl_Course_Fee.Text;
                    txt_no_seat.Text = lbl_no_seat.Text;
                    txt_no_application.Text = lbl_no_application.Text;
                    txt_start_date.Text = lbl_Start_Date.Text;
                    txt_end_date.Text = lbl_end_date.Text;
                    hd_id.Value = lbl_Id.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        protected void btn_Submit_Click(object sender, EventArgs e)
        { 
            if (ddl_session_fee.SelectedItem.Text == "Select")
            {
                ddl_session_fee.Focus(); 
                Alertme("Please select session", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_test.SelectedItem.Text == "Select")
            {
                ddl_test.Focus(); 
                Alertme("Please select test", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_coursename.SelectedItem.Text == "Select")
            {
                ddl_coursename.Focus(); 
                Alertme("Please select course name", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_coursefee.Text == "")
            {
                txt_coursefee.Focus();
                Alertme("Please enter fees", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_no_seat.Text == "")
            {
                txt_no_seat.Focus();
                Alertme("Please enter no. of seat.", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_no_application.Text == "")
            {
                txt_no_application.Focus();
                Alertme("Please enter number of application fill.", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            else if (txt_start_date.Text == "")
            {
                txt_start_date.Focus();
                Alertme("Please enter start date.", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            else if (txt_end_date.Text == "")
            {
                txt_end_date.Focus();
                Alertme("Please enter end date.", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_payment_mode.Text == "Select")
            {
                ddl_payment_mode.Focus();
                Alertme("Please select payment mode.", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            { 
                int idate = mycode.ConvertStringToiDate(txt_start_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_end_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning"); 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (btn_Submit.Text == "Add")
                    { 
                        SqlCommand cmd;
                        DataTable dt = mycode.FillData("Select * from Online_reg_fees where  Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "'  and Class_id='" + ddl_coursename.SelectedValue + "' and Test_id='" + ddl_test.SelectedValue + "' ");
                        if (dt.Rows.Count == 0)
                        {
                            double amot = My.toDouble(txt_coursefee.Text);
                            string query = "INSERT INTO Online_reg_fees (Session_id,Class_id,Fees,Branchi_id,User_id,Date_time,no_seat,no_application,start_date,start_Idate,end_date,end_Idate,Isactive,Test_id,Payment_mode) values (@Session_id,@Class_id,@Fees,@Branchi_id,@User_id,@Date_time,@no_seat,@no_application,@start_date,@start_Idate,@end_date,@end_Idate,@Isactive,@Test_id,@Payment_mode)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ddl_session_fee.SelectedValue);
                            cmd.Parameters.AddWithValue("@Class_id", ddl_coursename.SelectedValue);
                            cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                            cmd.Parameters.AddWithValue("@Branchi_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                            cmd.Parameters.AddWithValue("@no_seat", txt_no_seat.Text);
                            cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                            cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                            cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                            cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                            cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                            cmd.Parameters.AddWithValue("@Isactive", 1);
                            cmd.Parameters.AddWithValue("@Test_id", ddl_test.Text);
                            cmd.Parameters.AddWithValue("@Payment_mode", ddl_payment_mode.Text); 
                            if (My.InsertUpdateData(cmd))
                            { 
                                Alertme("Fees has been saved successfully", "success");
                                txt_coursefee.Text = "";
                                ddl_session_fee.Enabled = true;
                                ddl_test.Enabled = true;
                                ddl_coursename.Enabled = true;
                                txt_no_seat.Text = "";
                                txt_no_application.Text = "";
                                txt_start_date.Text = "";
                                txt_end_date.Text = "";
                                hd_id.Value = "0";
                                btn_Submit.Text = "Add";
                                bind_grd_view();
                            }
                        }
                        else
                        { 
                            Alertme("Sorry your selected class fees already added", "warning");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                    else
                    { 
                        SqlCommand cmd;
                        double amot = My.toDouble(txt_coursefee.Text); 
                        string query = "Update Online_reg_fees set  Fees=@Fees,Date_time=@Date_time,no_seat=@no_seat,no_application=@no_application,start_date=@start_date,start_Idate=@start_Idate,end_date=@end_date,end_Idate=@end_Idate,Test_id=@Test_id,Payment_mode=@Payment_mode where Id = @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                        cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                        cmd.Parameters.AddWithValue("@no_seat", txt_no_seat.Text);
                        cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                        cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                        cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                        cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                        cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                        cmd.Parameters.AddWithValue("@Test_id", ddl_test.Text);
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        cmd.Parameters.AddWithValue("@Payment_mode", ddl_payment_mode.Text);
                        if (My.InsertUpdateData(cmd))
                        {
                            ddl_session_fee.Enabled = true;
                            ddl_test.Enabled = true;
                            ddl_coursename.Enabled = true; 
                            Alertme("Fees has been saved successfully", "success");
                            txt_coursefee.Text = "";
                            txt_no_seat.Text = "";
                            txt_no_application.Text = "";
                            txt_start_date.Text = "";
                            txt_end_date.Text = "";
                            hd_id.Value = "0";
                            btn_Submit.Text = "Add";
                            bind_grd_view();
                        }
                    }
                }
            }
        }
         

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            { 
                Alertme("Please select session", "warning");
            }
            else
            {
                bind_grd_view(); 
            } 
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_Isactive")).Text == "1")
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Inactive";
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Active"; 
                    }
                    string courseid = ((Label)e.Item.FindControl("lbl_courseid")).Text; 
                    string lbl_no_application = ((Label)e.Item.FindControl("lbl_no_application")).Text; 
                }
            }
            catch { }
        }

        #region active and inactive
        protected void lnkActive_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");

                    if (lbl_Isactive.Text == "")// Inactive
                    {
                        mycode.executequery("update Online_reg_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select class has been activated successfully", "success");

                    }
                    else if (lbl_Isactive.Text == "0")// Inactive
                    {
                        mycode.executequery("update Online_reg_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select class has been activated successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "1")// Inactive
                    {
                        mycode.executequery("update Online_reg_fees set Isactive='0' where Id='" + lbl_Id.Text + "'");

                        Alertme("Your select class has been Inactivated successfully", "success");
                    }
                    bind_grd_view();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");

                    if (lbl_Isactive.Text == "")// Inactive
                    {
                        mycode.executequery("update Online_reg_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select class has been activated successfully", "success");

                    }
                    else if (lbl_Isactive.Text == "0")// Inactive
                    {
                        mycode.executequery("update Online_reg_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select class has been activated successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "1")// Inactive
                    {
                        mycode.executequery("update Online_reg_fees set Isactive='0' where Id='" + lbl_Id.Text + "'");

                        Alertme("Your select class has been Inactivated successfully", "success");
                    }
                    bind_grd_view();

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {


            }


        }
        #endregion

        protected void ddl_session_fee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                bind_test();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
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

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ddl_session_fee.Enabled = true;
            ddl_test.Enabled = true;
            ddl_coursename.Enabled = true; 
            txt_coursefee.Text = "";
            txt_no_seat.Text = "";
            txt_no_application.Text = "";
            txt_start_date.Text = "";
            txt_end_date.Text = "";
            hd_id.Value = "0";
            btn_Submit.Text = "Add";
        }
    }
}