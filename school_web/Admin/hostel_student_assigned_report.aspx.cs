using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
using System.IO;
using System.Transactions;

namespace school_web.Admin
{
    public partial class hostel_student_assigned_report : System.Web.UI.Page
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
                        string pagename_current = "hostel_student_assigned_report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        mycode.bind_all_ddl_with_id_All(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_hostel.SelectedValue = My.get_top_one_hostel_id();
                        mycode.bind_all_ddl_with_id(ddl_room_cat, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                        ddl_room_cat.SelectedValue = My.get_top_one_hostel_catogery(ddl_hostel.SelectedValue, ddlsession.SelectedValue);
                        bind_all_data();


                    }
                }
            }
            catch
            {

            }
        }

        private void bind_all_data()
        {
            ViewState["flag"] = "1";
            bind_grd_view("1");
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

        private void bind_grd_view(string from)
        {
            ViewState["flag"] = from;
            SqlCommand cmd = new SqlCommand();
            //if (from == "1")
            //{
            //    cmd.Parameters.AddWithValue("@sp_status", '6');
            //}
            if (from == "1")
            {
                cmd.Parameters.AddWithValue("@sp_status", '1');
                cmd.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
            }
            if (from == "2")
            {
                cmd.Parameters.AddWithValue("@sp_status", 2);
                cmd.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
            }
            if (from == "3")
            {
                cmd.Parameters.AddWithValue("@sp_status", 3);
                cmd.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
                cmd.Parameters.AddWithValue("@Room_category", ddl_room_cat.SelectedValue);
            }
            if (from == "4")
            {
                cmd.Parameters.AddWithValue("@sp_status", 3);
                cmd.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Room_category", ddl_room_cat.SelectedValue);
            }
            cmd.CommandText = "sp_Hostel_reports_new";
            DataTable dt = My.Getdata_sp(cmd);


            if (dt.Rows.Count == 0)
            {
                //Alertme("Sorry there are no assigned student in the hostel", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                ddlsession.Focus();
            }
            else
            {
                if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select room category.", "warning");
                    ddl_room_cat.Focus();
                }
                else
                {
                    if (ddl_hostel.SelectedItem.Text == "All")
                    {
                        bind_grd_view("4");
                    }
                    else
                    {
                        bind_grd_view("3");

                    }
                }


            }

        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_status")).Text == "1")
                {
                    ((LinkButton)e.Item.FindControl("lnk_status")).Text = "Yes";
                    ((LinkButton)e.Item.FindControl("lnk_status")).CssClass = "GreenBtnS";
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnk_status")).Text = "No";
                    ((LinkButton)e.Item.FindControl("lnk_status")).CssClass = "RedsBtnS";
                }
            }
        }
        protected void lnk_status_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_status = (Label)row.FindControl("lbl_status");
                Label lbl_hostel_assign_id = (Label)row.FindControl("lbl_hostel_assign_id");

                string updateStatus = "1";
                if (lbl_status.Text == "1")
                {
                    updateStatus = "0";
                }

                string qrys = "update Hostel_assign_master set Status='" + updateStatus + "' where Hostel_assign_id='" + lbl_hostel_assign_id.Text + "'";
                mycode.executequery(qrys);
                Alertme("Status has been updated successfully.", "success");

                bind_grd_view(ViewState["flag"].ToString());

            }
            catch
            {

            }
        }
        protected void lnk_delete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_hostel_assign_id = (Label)row.FindControl("lbl_hostel_assign_id");
            Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
            Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
            Label lbl_session = (Label)row.FindControl("lbl_session");
            Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
            ViewState["RmovEID"] = lbl_hostel_assign_id.Text;
            ViewState["admissionserialnumbe"] = lbl_admissionserialnumber.Text;
            ViewState["session_id"] = lbl_session_id.Text;
            ViewState["session"] = lbl_session.Text;
            ViewState["class_id"] = lbl_class_id.Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);

        }
        protected void btn_conf_remove_Click(object sender, EventArgs e)
        {
            try
            {
                string parameter = "AdmissionFee";
                string parameter_old = "HostelAdmissionFee";
                if (txt_reason.Text == "")
                {
                    Alertme("Please enter reason.", "warning");
                    txt_reason.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
                }
                else
                {
                    string studenttype = My.get_student_old_new(ViewState["admissionserialnumbe"].ToString(), ViewState["session_id"].ToString(), ViewState["session"].ToString());

                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        payments.exeSql("update Hostel_assign_master set Status='0',Remove_cause='" + txt_reason.Text + "',Updated_by='" + ViewState["Userid"].ToString() + "',Updated_date='" + mycode.date() + "',Updated_idate='" + mycode.idate() + "' where Hostel_assign_id='" + ViewState["RmovEID"].ToString() + "'", con);
                        payments.exeSql("update admission_registor set hosteltaken='No',Hostel_id='0' where admissionserialnumber='" + ViewState["admissionserialnumbe"].ToString() + "' and Session_id='" + ViewState["session_id"].ToString() + "' ", con);


                        string parameter_m_old = "HostelMonthlyFee";
                        string parameter_m_new = "MonthlyFee";
                        if (studenttype == "New")
                        {
                            parameter = "AdmissionFee";
                            parameter_old = "HostelAdmissionFee";
                        }
                        else
                        {
                            parameter = "AnnualFee";
                            parameter_old = "HostelAnnualFee";
                        }

                        payments.exeSql("update Typewise_fee_collection set parameter='" + parameter_m_new + "'  where admission_no='" + ViewState["admissionserialnumbe"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_m_old + "'; update Typewise_fee_collection set parameter='" + parameter + "'  where admission_no='" + ViewState["admissionserialnumbe"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_old + "' ", con);

                        dues_update_headwise_transaction.update_student_dues(ViewState["session_id"].ToString(), ViewState["class_id"].ToString(), ViewState["admissionserialnumbe"].ToString(), "0", "0", con);
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }
                    if (flag == true)
                    {
                        Alertme("Student removed from hostel successfully.", "success");
                        bind_grd_view(ViewState["flag"].ToString());
                    }
                    else
                    {
                        Alertme("Something went wrong. Please try again.", "warning");
                    }

                   
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {

                bind_grd_view("1");

            }
        }

        protected void ddl_hostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                if (ddl_hostel.SelectedItem.Text == "All")
                {
                    bind_grd_view("1");

                }
                else
                {
                    bind_grd_view("2");

                }
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                hd_id.Value = lbl_Id.Text;
                Response.Redirect("Hostel_Assign_to_student.aspx?admNo=" + lbl_admissionserialnumber.Text + "&Sessionid=" + lbl_session_id.Text, false);
            }
            catch
            {

            }
        }
    }

}
