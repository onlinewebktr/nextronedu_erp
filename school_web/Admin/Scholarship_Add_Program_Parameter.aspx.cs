using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Scholarship_Add_Program_Parameter : System.Web.UI.Page
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
                        ViewState["attechment"] = "0";
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Scholarship_set_Parameter_of_Scholarship.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session");

                        ddl_session.SelectedValue = My.get_session_id_onlinereg();
                        mycode.bind_all_ddl_with_id(ddl_Scholorship_Name, "select Test_name,Test_id from Scholarship_Program where Session_id='" + ddl_session.SelectedValue + "' and  Is_active=1 order by  Test_name asc");

                        mycode.bind_all_ddl_with_id_cap_All(ddl_Scholorshipclass, "select Course_Name,course_id from Add_course_table order by Position asc");
                        lbl_edit_dis.Visible = false;
                        if (Request.QueryString["id"] != null)
                        {

                            hd_id.Value = Request.QueryString["id"].ToString();
                            btn_add.Text = "Update";
                            Bind_data_Scholarship_Parameter_fees();
                        }


                        //fetch_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Scholarship_Add_Program_Parameter");
            }
        }

        private void Bind_data_Scholarship_Parameter_fees()
        {
            DataTable dt = mycode.FillData("Select * from Scholarship_Parameter_fees where  id='" + hd_id.Value + "' ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                ddl_session.SelectedValue = dt.Rows[0]["Session_id"].ToString();

                mycode.bind_all_ddl_with_id(ddl_Scholorship_Name, "select Test_name,Test_id from Scholarship_Program where Session_id='" + ddl_session.SelectedValue + "' and  Is_active=1 order by  Test_name asc");

                lbl_edit_dis.Visible = true;

                ddl_Scholorship_Name.SelectedValue = dt.Rows[0]["Test_id"].ToString();
                ddl_Scholorshipclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                ddl_Scholorshipclass.Enabled = false;
                ddl_Scholorshipclass.CssClass = "form-select";
                txt_coursefee.Text = dt.Rows[0]["Fees"].ToString();
                txt_no_application.Text = dt.Rows[0]["no_application"].ToString();
                txt_start_date.Text = dt.Rows[0]["start_date"].ToString();
                txt_end_date.Text = dt.Rows[0]["end_date"].ToString();
                txt_info.Value = dt.Rows[0]["Scholorship_Guidelines"].ToString();
                txt_Scholorship_Benefit.Value = dt.Rows[0]["Scholorship_Benefit"].ToString();
                ddl_payment_mode.Text = dt.Rows[0]["Payment_mode"].ToString();
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

        #region add conndition

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_Scholorship_Name, "select Test_name,Test_id from Scholarship_Program where Session_id='" + ddl_session.SelectedValue + "' and  Is_active=1 order by  Test_name asc");

            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    insert_data_Scholarship_Program();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    insert_data_Scholarship_Program();
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

        private void insert_data_Scholarship_Program()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_Scholorship_Name.SelectedItem.Text == "Select")
            {
                ddl_Scholorship_Name.Focus();
                Alertme("Please select scholorship name", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_Scholorshipclass.SelectedItem.Text == "Select")
            {
                ddl_Scholorshipclass.Focus();
                Alertme("Please select scholorship for", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_coursefee.Text == "")
            {
                txt_coursefee.Focus();
                Alertme("Please enter scholorship fees", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            else if (txt_no_application.Text == "")
            {
                txt_no_application.Focus();
                Alertme("Please enter maximum application form allowed", "warning");
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
                    if (btn_add.Text == "Add")
                    {
                        bool sendto = false;
                        //int class_row_count = My.get_class_row_count();

                        string query1 = " select   * from dbo.[Add_course_table] order by Position";
                        DataTable dt1 = mycode.FillData(query1);
                        if (dt1.Rows.Count == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                string course_id = dt1.Rows[i]["course_id"].ToString();


                                SqlCommand cmd;
                                DataTable dt = mycode.FillData("Select * from Scholarship_Parameter_fees where  Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "' and Class_id='" + course_id + "' and Test_id='" + ddl_Scholorship_Name.SelectedValue + "' ");
                                if (dt.Rows.Count == 0)
                                {
                                    double amot = My.toDouble(txt_coursefee.Text);
                                    string query = "INSERT INTO Scholarship_Parameter_fees (Session_id,Class_id,Fees,Branchi_id,User_id,Date_time,no_seat,no_application,start_date,start_Idate,end_date,end_Idate,Isactive,Test_id,Payment_mode,Scholorship_Guidelines,Scholorship_Benefit) values (@Session_id,@Class_id,@Fees,@Branchi_id,@User_id,@Date_time,@no_seat,@no_application,@start_date,@start_Idate,@end_date,@end_Idate,@Isactive,@Test_id,@Payment_mode,@Scholorship_Guidelines,@Scholorship_Benefit)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Class_id", course_id);
                                    cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                                    cmd.Parameters.AddWithValue("@Branchi_id", ViewState["branchid"].ToString());
                                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                                    cmd.Parameters.AddWithValue("@no_seat", "0");
                                    cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                                    cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                                    cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                                    cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                                    cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                                    cmd.Parameters.AddWithValue("@Isactive", 0);
                                    cmd.Parameters.AddWithValue("@Test_id", ddl_Scholorship_Name.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Payment_mode", ddl_payment_mode.Text);
                                    cmd.Parameters.AddWithValue("@Scholorship_Guidelines", txt_info.Value);
                                    cmd.Parameters.AddWithValue("@Scholorship_Benefit", txt_Scholorship_Benefit.Value);
                                    if (My.InsertUpdateData(cmd))
                                    {
                                        Session["msgs"] = "Scholarship Program Parameter has been saved successfully";
                                        Alertme("Scholarship parameter has been saved successfully", "success");
                                        sendto = true;
                                    }
                                }
                                else
                                {
                                    Alertme("Sorry your selected class fees already added", "warning");
                                    
                                }


                            }
                            if (sendto==true)
                            {
                                txt_coursefee.Text = "";
                                ddl_session.Enabled = true;
                                ddl_Scholorship_Name.Enabled = true;
                                ddl_Scholorshipclass.Enabled = true;
                                txt_no_application.Text = "";
                                txt_start_date.Text = "";
                                txt_end_date.Text = "";
                                hd_id.Value = "0";
                                btn_add.Text = "Add";
                                ddl_Scholorshipclass.SelectedValue = "0";
                                ddl_Scholorship_Name.SelectedValue = "0";
                                ddl_payment_mode.Text = "Select";
                                txt_info.Value = "";
                                txt_Scholorship_Benefit.Value = "";
                                Response.Redirect("Scholarship_set_Parameter_of_Scholarship.aspx", false);
                            }
                        }

                    }
                    else
                    {
                        SqlCommand cmd;
                        double amot = My.toDouble(txt_coursefee.Text);
                        string query = "Update Scholarship_Parameter_fees set  Fees=@Fees,Date_time=@Date_time,no_seat=@no_seat,no_application=@no_application,start_date=@start_date,start_Idate=@start_Idate,end_date=@end_date,end_Idate=@end_Idate,Test_id=@Test_id,Payment_mode=@Payment_mode,Scholorship_Guidelines=@Scholorship_Guidelines,Scholorship_Benefit=@Scholorship_Benefit where Id = @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                        cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                        cmd.Parameters.AddWithValue("@no_seat", "0");
                        cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                        cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                        cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                        cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                        cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                        cmd.Parameters.AddWithValue("@Test_id", ddl_Scholorship_Name.SelectedValue);
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        cmd.Parameters.AddWithValue("@Payment_mode", ddl_payment_mode.Text);
                        cmd.Parameters.AddWithValue("@Scholorship_Guidelines", txt_info.Value);
                        cmd.Parameters.AddWithValue("@Scholorship_Benefit", txt_Scholorship_Benefit.Value);
                        if (My.InsertUpdateData(cmd))
                        {

                            Session["msgs"] = "Scholarship Program Parameter has been updated successfully";
                            ddl_session.Enabled = true;
                            ddl_Scholorship_Name.Enabled = true;
                            ddl_Scholorshipclass.Enabled = true;
                            Alertme("Fees has been saved successfully", "success");
                            txt_coursefee.Text = "";

                            txt_no_application.Text = "";
                            txt_start_date.Text = "";
                            txt_end_date.Text = "";
                            hd_id.Value = "0";
                            btn_add.Text = "Add";
                            ddl_Scholorshipclass.SelectedValue = "0";
                            ddl_Scholorship_Name.SelectedValue = "0";
                            ddl_payment_mode.Text = "Select";
                            txt_info.Value = "";
                            txt_Scholorship_Benefit.Value = "";
                            Response.Redirect("Scholarship_set_Parameter_of_Scholarship.aspx", false);
                        }
                    }
                }
            }

        }

        #endregion

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Session["msgs"] = null;
            Response.Redirect("Scholarship_set_Parameter_of_Scholarship.aspx", false);
        }
    }
}