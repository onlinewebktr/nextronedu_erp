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
    public partial class transport_fee_master : System.Web.UI.Page
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
                        ViewState["DoEditStatuS"] = "0";
                        bind_month();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "Admission_Fee_Master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
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
                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();


                        //mycode.bind_all_ddl_with_id(ddl_transp_path, "  select  busno+', Distance : '+ Transfer_Status,TransportationPath_id from  TransportationPath");
                        //mycode.bind_all_ddl_with_id(ddl_srch_t_path, "  select  busno+', Distance : '+ Transfer_Status,TransportationPath_id from  TransportationPath");

                        mycode.bind_all_ddl_with_id(ddl_transp_path, "  select  Pathname,TransportationPath_id from  TransportationPath");
                        mycode.bind_all_ddl_with_id(ddl_srch_t_path, "  select  Pathname,TransportationPath_id from  TransportationPath");

                        bind_grd_fee();
                        ViewState["flag"] = "0";





                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
        private void bind_grd_fee()
        {
            bind_grd_view("select t1.*,t2.* from Transportation_Fee_Master t1 join TransportationPath t2 on t1.Transportation_path_id=t2.TransportationPath_id join Month_Index t3 on t1.Month_id=t3.Month_Id where t1.session_id=" + ddl_session.SelectedValue + " order by t3.Position asc");
        }

        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        private void bind_month()
        {
            mycode.bind_all_ddl_with_id_All_New(ddl_srch_months, "select Month,Month_Id from Month_Index order by Position asc");

            string monthname = mycode.get_current_monthname();
            ddl_srch_months.SelectedValue = My.tomonth_numberstring(monthname);

            DataTable dt = mycode.FillData("select Month,'false' as Value,Month_Id from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();
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

        protected void ddl_transp_path_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_transp_path.SelectedItem.Text == "Select")
                {
                    rd_std_list.DataSource = null;
                    rd_std_list.DataBind();
                    sdtList.Visible = false;
                }
                else
                {
                    // find_related_student();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_related_student()
        {
            DataTable dt = mycode.FillData("select * from admission_registor where Transportation_Id='" + ddl_transp_path.SelectedValue + "' and Session_id='" + ddl_session.SelectedValue + "' order by studentname asc");
            if (dt.Rows.Count == 0)
            {
                sdtList.Visible = false;
                rd_std_list.DataSource = null;
                rd_std_list.DataBind();
            }
            else
            {
                sdtList.Visible = true;
                rd_std_list.DataSource = dt;
                rd_std_list.DataBind();
            }
        }


        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                fina_save_data();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                fina_save_data();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void fina_save_data()
        {
            try
            {
                if (ddl_transp_path.SelectedItem.Text == "Select")
                {
                    Alertme("Please select transportation path", "warning");
                    ddl_transp_path.Focus();
                    return;
                }
                if (txt_fee.Text == "")
                {
                    Alertme("Please enter transportation fee", "warning");
                    txt_fee.Focus();
                    return;
                }

                if (btn_Submit.Text == "Add")
                {
                    ViewState["issubmit"] = "0";
                    save_data();
                    if (ViewState["issubmit"].ToString() == "1")
                    {
                        bind_month();
                        empty_form();
                        if (ViewState["flag"].ToString() == "0")
                        {
                            bind_grd_fee();
                        }
                        if (ViewState["flag"].ToString() == "1")
                        {
                            find_fee_by_t_path();
                        }
                        if (ViewState["flag"].ToString() == "2")
                        {
                            find_fee_by_t_path_and_mnths();
                        }
                        Alertme("Transport Fee master created successfully", "success");
                    }
                }
                else
                {

                    bool chek_fee = My.find_mnthly_transp_fee_collected("MonthlyFee", ddl_session.SelectedValue, ddl_transp_path.SelectedValue, ViewState["MnthS"].ToString());
                    if (chek_fee == false)
                    {
                        Alertme("You can't add/update fee  because fee has been taken.", "warning");
                        return;
                    }
                    else
                    {
                        bool isupdate = false;
                        My.exeSql("update Transportation_Fee_Master set Amount=" + txt_fee.Text + " where id=" + hd_id.Value + "");
                        isupdate = true;
                        if (isupdate)
                        {
                            empty_form();
                            bind_month();
                            if (ViewState["flag"].ToString() == "0")
                            {
                                bind_grd_fee();
                            }
                            if (ViewState["flag"].ToString() == "1")
                            {
                                find_fee_by_t_path();
                            }
                            if (ViewState["flag"].ToString() == "2")
                            {
                                find_fee_by_t_path_and_mnths();
                            }
                            Alertme("Transport Fee master updated successfully", "success");
                        }
                    }
                }

            }
            catch (Exception exe)
            {
                My.Save_Exception(exe.Message);
            }
        }

        private void empty_form()
        {
            ViewState["DoEditStatuS"] = "0";
            bind_month();
            txt_fee.Text = "";
            ddl_transp_path.Focus();
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }
        private void save_data()
        {
            int growcounts = rp_month.Items.Count;
            int ks = 0;
            for (int ixs = 0; ixs < growcounts; ixs++)
            {
                CheckBox chksMnths = (CheckBox)rp_month.Items[ixs].FindControl("chk_month_name");
                if (chksMnths.Checked == true)
                {
                    Label lbl_value = (Label)rp_month.Items[ixs].FindControl("lbl_value");
                    Label lbl_month_name = (Label)rp_month.Items[ixs].FindControl("lbl_month_name");
                    Label lbl_mnth_id = (Label)rp_month.Items[ixs].FindControl("lbl_mnth_id");
                    //=====*********************************

                    bool chek_fee = My.find_mnthly_transp_fee_collected("MonthlyFee", ddl_session.SelectedItem.Text, ddl_transp_path.SelectedValue, lbl_month_name.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't add/update fee  because fee has been taken.", "warning");
                    }
                    else
                    {
                        if (My.toDouble(txt_fee.Text) > 0)
                        {
                            SqlDataAdapter ad = new SqlDataAdapter("select * from Transportation_Fee_Master where Transportation_path_id='" + ddl_transp_path.SelectedValue + "' and session_id=" + ddl_session.SelectedValue + " and Month='" + lbl_month_name.Text + "'", My.conn);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Transportation_Fee_Master");
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Session"] = ddl_session.SelectedItem.Text;
                                dr["Parameter"] = "TransportFee";
                                dr["Amount"] = My.toDouble(txt_fee.Text).ToString("0.00");
                                dr["session_id"] = ddl_session.SelectedValue;
                                dr["parameter_id"] = "1002";
                                dr["Type"] = "Monthwise";
                                dr["User_id"] = ViewState["Userid"].ToString();
                                dr["Date"] = mycode.date();
                                dr["time"] = mycode.time();
                                dr["Ledger"] = "School";

                                dr["Month"] = lbl_month_name.Text;
                                dr["Month_id"] = lbl_mnth_id.Text;
                                dr["Ledger"] = "School";
                                dr["Transportation_path_id"] = ddl_transp_path.SelectedValue;
                                dt.Rows.Add(dr);
                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                ad.Update(dt);
                                ViewState["issubmit"] = "1";
                            }
                            else
                            {
                                My.exeSql("delete from Transportation_Fee_Master where Transportation_path_id='" + ddl_transp_path.SelectedValue + "' and session_id=" + ddl_session.SelectedValue + " and Month='" + lbl_month_name.Text + "'");
                                DataRow dr = dt.NewRow();
                                dr["Session"] = ddl_session.SelectedItem.Text;
                                dr["Parameter"] = "TransportFee";
                                dr["Amount"] = My.toDouble(txt_fee.Text).ToString("0.00");
                                dr["session_id"] = ddl_session.SelectedValue;
                                dr["parameter_id"] = "1002";
                                dr["Type"] = "Monthwise";
                                dr["User_id"] = ViewState["Userid"].ToString();
                                dr["Date"] = mycode.date();
                                dr["time"] = mycode.time();
                                dr["Ledger"] = "School";

                                dr["Month"] = lbl_month_name.Text;
                                dr["Month_id"] = lbl_mnth_id.Text;
                                dr["Ledger"] = "School";
                                dr["Transportation_path_id"] = ddl_transp_path.SelectedValue;
                                dt.Rows.Add(dr);
                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                ad.Update(dt);
                                ViewState["issubmit"] = "1";
                            }
                        }
                        else
                        {
                            Alertme("Please enter bus fee", "warning");
                            txt_fee.Focus();
                        }
                    }
                }
                else
                {
                    ks++;
                }
            }

            if (ks == growcounts)
            {
                Alertme("Please select at least one Month.", "warning");
                return;
            }
        }



        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_transportation_path_id = (Label)row.FindControl("lbl_transportation_path_id");
                    Label lbl_month_id = (Label)row.FindControl("lbl_month_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_Month = (Label)row.FindControl("lbl_Month");
                    Label lbl_Amount = (Label)row.FindControl("lbl_Amount");
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");

                    bool chek_fee = My.find_mnthly_transp_fee_collected("MonthlyFee", lbl_Session.Text, lbl_transportation_path_id.Text, lbl_Month.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't edit fee because fee has been taken.", "warning");
                        return;
                    }
                    ViewState["MnthS"] = lbl_Month.Text;
                    ViewState["MonthName"] = lbl_Month.Text;
                    ViewState["DoEditStatuS"] = "1";
                    hd_id.Value = lbl_Id.Text;
                    ddl_transp_path.SelectedValue = lbl_transportation_path_id.Text;
                    find_related_student();
                    txt_fee.Text = lbl_Amount.Text;
                    bind_month();
                    btn_Submit.Text = "Update";
                    btn_cancel.Visible = true;
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

        protected void rp_month_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["DoEditStatuS"].ToString() == "1")
                {
                    ((CheckBox)e.Item.FindControl("chk_month_name")).Enabled = false;
                    if (((Label)e.Item.FindControl("lbl_month_name")).Text == ViewState["MonthName"].ToString())
                    {
                        ((CheckBox)e.Item.FindControl("chk_month_name")).Checked = true;
                    }
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("chk_month_name")).Enabled = true;
                }
            }
        }

        protected void ddl_srch_t_path_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_fee_by_t_path();
                ViewState["flag"] = "1";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_fee_by_t_path()
        {
            bind_grd_view("select t1.*,t2.* from Transportation_Fee_Master t1 join TransportationPath t2 on t1.Transportation_path_id=t2.TransportationPath_id join Month_Index t3 on t1.Month_id=t3.Month_Id where t1.Transportation_path_id='" + ddl_srch_t_path.SelectedValue + "' and   t1.session_id=" + ddl_session.SelectedValue + "  order by t3.Position asc");
        }

        protected void btn_fnd_by_pth_mnth_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_srch_t_path.SelectedItem.Text == "Select")
                {
                    Alertme("Please select transportation path.", "warning");
                    ddl_srch_t_path.Focus();
                }
                else if (ddl_srch_months.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month.", "warning");
                    ddl_srch_months.Focus();
                }
                else
                {
                    find_fee_by_t_path_and_mnths();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_fee_by_t_path_and_mnths()
        {
            if (ddl_srch_months.SelectedItem.Text == "ALL")
            {
                bind_grd_view("select t1.*,t2.* from Transportation_Fee_Master t1 join TransportationPath t2 on t1.Transportation_path_id=t2.TransportationPath_id join Month_Index t3 on t1.Month_id=t3.Month_Id where t1.Transportation_path_id='" + ddl_srch_t_path.SelectedValue + "'  and  t1.session_id=" + ddl_session.SelectedValue + " order by t3.Position asc");
            }
            else
            {


                bind_grd_view("select t1.*,t2.* from Transportation_Fee_Master t1 join TransportationPath t2 on t1.Transportation_path_id=t2.TransportationPath_id join Month_Index t3 on t1.Month_id=t3.Month_Id where t1.Transportation_path_id='" + ddl_srch_t_path.SelectedValue + "' and t1.Month='" + ddl_srch_months.SelectedItem.Text + "' and  t1.session_id=" + ddl_session.SelectedValue + " order by t3.Position asc");
            }
        }

        protected void chk_all_month_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_month.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_month.Items[j].FindControl("chk_month_name") as CheckBox;
                if (chk_all_month.Checked)
                {
                    chk_month_name.Checked = true;
                }
                else
                {
                    chk_month_name.Checked = false;
                }
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    Label lbl_transportation_path_id = (Label)row.FindControl("lbl_transportation_path_id");
                    Label lbl_month_id = (Label)row.FindControl("lbl_month_id");
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");
                    Label lbl_Month = (Label)row.FindControl("lbl_Month");
                    Label lbl_Amount = (Label)row.FindControl("lbl_Amount");


                    bool chek_fee = My.find_mnthly_transp_fee_collected("MonthlyFee", lbl_Session.Text, lbl_transportation_path_id.Text, lbl_Month.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't delete fee because fee has been taken.", "warning");
                        return;
                    }


                    mycode.executequery("delete from Transportation_Fee_Master where Id='" + lbl_Id.Text + "'");

                    Alertme("Deletion process has been successfully done", "success");
                    if (ViewState["flag"].ToString() == "0")
                    {
                        bind_grd_fee();
                    }
                    if (ViewState["flag"].ToString() == "1")
                    {
                        find_fee_by_t_path();
                    }
                    if (ViewState["flag"].ToString() == "2")
                    {
                        find_fee_by_t_path_and_mnths();
                    }
                    empty_form();
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
    }
}