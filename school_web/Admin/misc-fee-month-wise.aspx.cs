using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class misc_fee_month_wise : System.Web.UI.Page
    {
        DataTable month_dt = new DataTable();
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
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from dbo.[session_details]");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id from dbo.[session_details]");
                        //ddl_session_st.SelectedItem.Text = My.current_session;
                        ViewState["sessionid"] = My.get_session_id();
                        ddl_apply_for.DataSource = new string[] { "Hostel", "Day Scholar", "Both" };
                        ddl_apply_for.DataBind();
                        ddl_apply_for.SelectedIndex = 0;
                        mycode.bind_all_ddl_with_id(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master");
                        ddl_day_boarding.DataSource = new string[] { "N/A", "With Lunch", "Without Lunch" };
                        ddl_day_boarding.DataBind();
                        ddl_day_boarding.SelectedIndex = 0;
                        ddl_ledger.DataSource = new string[] { "School", "Hostel" };
                        ddl_ledger.DataBind();
                        ddl_ledger.SelectedIndex = 0;
                        bind_grd_view_all();
                        ViewState["flag"] = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }

        private void bind_month()
        {
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


        My mycode = new My();
        private void bind_grd_view_all()
        {
            bind_grd_view("select Row_Number() over(order by mf.id) sl,mf.*,hm.Hostel_Name from Miscellaneous_Fee_Master mf left  join  Hostel_master hm on mf.Hostel_id=hm.Hostel_id where mf.session_id="+ ViewState["sessionid"].ToString() + "");
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



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {
                if (btn_Submit.Text == "Add")
                {
                    ViewState["issubmit"] = "0";
                    save_data();
                    if (ViewState["issubmit"].ToString() == "1")
                    {
                        empty_form();
                        if (ViewState["flag"].ToString() == "0")
                        {
                            bind_grd_view_all();
                        }
                        if (ViewState["flag"].ToString() == "1")
                        {
                            bind_grid_by_session();
                        }
                        if (ViewState["flag"].ToString() == "2")
                        {
                            bind_grid_by_class();
                        }
                        Alertme("Misc Fee master created successfully", "success");
                    }
                }
                else
                {
                    if (ddl_session.Text == "Select")
                    {
                        Alertme("Please select session", "warning");
                        ddl_session.Focus();
                        return;
                    }
                    if (txt_perticular.Text == "")
                    {
                        Alertme("Please enter Particular", "warning");
                        txt_perticular.Focus();
                        return;
                    } 
                    if (ddl_apply_for.Text == "Hostel")
                    {
                        if (ddl_hostel.Text == "Select")
                        {
                            Alertme("Please select Hostel", "warning");
                            ddl_hostel.Focus();
                            return;
                        }
                    }
                    if (ddl_ledger.Text == "Select")
                    {
                        Alertme("Please select Ledger", "warning");
                        ddl_ledger.Focus();
                        return;
                    }
                    int hostel_id = 0;
                    if (pnl_hostel.Visible == true)
                    {
                        hostel_id = My.toint(ddl_hostel.SelectedValue);
                    }
                    else
                    {
                        hostel_id = 0;
                    }

                    try
                    {
                        string applyFor = ddl_apply_for.Text;
                        if (applyFor == "Hostel")
                        {
                            applyFor = "Hosteller";
                        }
                        bool isupdate = false;
                        My.exeSql("update Miscellaneous_Fee_Master set Amount=" + My.toDouble(txt_amount.Text) + ",Apply_for='" + applyFor + "',Hostel_id='" + hostel_id + "',Ledger='" + ddl_ledger.Text + "'  where id=" + hd_id.Value + "");
                        isupdate = true;
                        if (isupdate)
                        {
                            empty_form();
                            if (ViewState["flag"].ToString() == "0")
                            {
                                bind_grd_view_all();
                            }
                            if (ViewState["flag"].ToString() == "1")
                            {
                                bind_grid_by_session();
                            }
                            if (ViewState["flag"].ToString() == "2")
                            {
                                bind_grid_by_class();
                            }
                            Alertme(" Misc Fee master updated successfully", "success");
                        }
                    }
                    catch (Exception ex)
                    {
                        My.Save_Exception(ex.Message);
                    }

                }
            }
            catch (Exception exe)
            {
            }
        }

        private void save_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
                ddl_session.Focus();
                return;
            }
            if (txt_perticular.Text == "")
            {
                Alertme("Please enter Particular", "warning");
                txt_perticular.Focus();
                return;
            }
            if (txt_amount.Text == "")
            {
                Alertme("Please enter amount", "warning");
                txt_amount.Focus();
                return;
            }
            if (ddl_apply_for.Text == "Hostel")
            {
                if (ddl_hostel.Text == "Select")
                {
                    Alertme("Please select Hostel", "warning");
                    ddl_hostel.Focus();
                    return;
                }
            }



            bool issubmit = false;
            int hostel_id = 0;
            if (pnl_hostel.Visible == true)
            {
                hostel_id = My.toint(ddl_hostel.SelectedValue);
            }
            else
            {
                hostel_id = 0;
            }
            int growcounts = rp_month.Items.Count;
            int ks = 0;
            for (int ixs = 0; ixs < growcounts; ixs++)
            {
                CheckBox chksMnths = (CheckBox)rp_month.Items[ixs].FindControl("chk_month_name");
                if (chksMnths.Checked == true)
                {
                    Label lbl_value = (Label)rp_month.Items[ixs].FindControl("lbl_value");
                    Label lbl_month_name = (Label)rp_month.Items[ixs].FindControl("lbl_month_name");
                    //=====*********************************

                    int growcount = rp_class.Items.Count;
                    int k = 0;
                    for (int ix = 0; ix < growcount; ix++)
                    {
                        CheckBox chk = (CheckBox)rp_class.Items[ix].FindControl("chk_class");
                        if (chk.Checked == true)
                        {
                            Label lbl_class_id = (Label)rp_class.Items[ix].FindControl("lbl_class_id");
                            Label lbl_course_name = (Label)rp_class.Items[ix].FindControl("lbl_course_name");
                            Label lbl_status = (Label)rp_class.Items[ix].FindControl("lbl_status");
                            //=====****

                            int class_id = My.toint(lbl_class_id.Text);
                            string applyFor = ddl_apply_for.Text;
                            if (applyFor == "Hostel")
                            {
                                applyFor = "Hosteller";
                            }
                            SqlDataAdapter ad = new SqlDataAdapter("select * from Miscellaneous_Fee_Master where class_id=" + class_id + " and session_id=" + ddl_session.SelectedValue + " and Apply_for='" + applyFor + "' and Month='" + lbl_month_name.Text + "' and Particular='" + txt_perticular.Text + "'  and Hostel_id='" + hostel_id + "'", My.conn);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Fee_Master");
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Class"] = lbl_course_name.Text;
                                dr["Session"] = ddl_session.SelectedItem.Text;
                                dr["Month"] = lbl_month_name.Text;
                                dr["Particular"] = txt_perticular.Text;
                                dr["Amount"] = My.toDouble(txt_amount.Text);
                                dr["Apply_for"] = applyFor;
                                dr["class_id"] = class_id;
                                dr["session_id"] = ddl_session.SelectedValue;
                                dr["Hostel_id"] = hostel_id;
                                dr["Ledger"] = ddl_ledger.Text;
                                if (ddl_day_boarding.SelectedIndex != 0)
                                {
                                    if (ddl_day_boarding.SelectedIndex == 1)
                                    {
                                        dr["day_boarding"] = false;
                                        dr["day_boarding_with_lunch"] = true;
                                    }
                                    else if (ddl_day_boarding.SelectedIndex == 2)
                                    {
                                        dr["day_boarding"] = true;
                                        dr["day_boarding_with_lunch"] = false;
                                    }
                                }
                                dt.Rows.Add(dr);
                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                ad.Update(dt);
                                issubmit = true;
                                ViewState["issubmit"] = "1";
                            }
                            else
                            { 
                                My.exeSql("delete from Miscellaneous_Fee_Master where class_id=" + class_id + " and session_id=" + ddl_session.SelectedValue + " and Apply_for='" + ddl_apply_for.Text + "' and Month='" + lbl_month_name.Text + "'  and Particular='" + txt_perticular.Text + "' and Hostel_id='" + hostel_id + "'");
                                DataRow dr = dt.NewRow();
                                dr["Class"] = lbl_course_name.Text;
                                dr["Session"] = ddl_session.SelectedItem.Text;
                                dr["Month"] = lbl_month_name.Text;
                                dr["Particular"] = txt_perticular.Text;
                                dr["Amount"] = My.toDouble(txt_amount.Text);
                                dr["Apply_for"] = applyFor;
                                dr["class_id"] = class_id;
                                dr["session_id"] = ddl_session.SelectedValue;
                                dr["Hostel_id"] = hostel_id;
                                dr["Ledger"] = ddl_ledger.Text;
                                if (ddl_day_boarding.SelectedIndex != 0)
                                {
                                    if (ddl_day_boarding.SelectedIndex == 1)
                                    {
                                        dr["day_boarding"] = false;
                                        dr["day_boarding_with_lunch"] = true;
                                    }
                                    else if (ddl_day_boarding.SelectedIndex == 2)
                                    {
                                        dr["day_boarding"] = true;
                                        dr["day_boarding_with_lunch"] = false;
                                    }
                                }
                                dt.Rows.Add(dr);
                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                ad.Update(dt);
                                issubmit = true;
                                ViewState["issubmit"] = "1";
                            }
                        }
                        else
                        {
                            k++;
                        }
                    }

                    if (k == growcount)
                    {
                        Alertme("Please select at least one class.", "warning");
                        return;
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



        private void empty_form()
        {
            ViewState["DoEditStatuS"] = "0";
            Bind_course_details();
            bind_month();
            txt_amount.Text = "";
            txt_perticular.Text = "";
            ddl_session.Enabled = true;
            ddl_session.Focus();
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_course_details();
            }
            catch (Exception ex)
            {
            }
        }

        private void Bind_course_details()
        {
            DataTable dt = mycode.FillData("select Course_Name,course_id,'false' status,case when course_id in ( select class_id from Fee_Master where parameter_id=0 and session_id='" + ddl_session.SelectedValue + "') then 'false' else 'true' end enable_status  from dbo.[Add_course_table] order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_class.DataSource = null;
                rp_class.DataBind();
            }
            else
            {
                rp_class.DataSource = dt;
                rp_class.DataBind();
            }
        }

        protected void ddl_apply_for_SelectedIndexChanged(object sender, EventArgs e)
        {
            do_work();
        }

        private void do_work()
        {
            if (ddl_apply_for.SelectedItem != null)
            {
                if (ddl_apply_for.SelectedItem.Text == "Hostel")
                {
                    pnl_hostel.Visible = true;
                    pnl_day_boarding.Visible = false;
                    ddl_ledger.Text = "Hostel";
                    ddl_ledger.Enabled = false;
                    ddl_ledger.CssClass = "form-select";
                }
                else if (ddl_apply_for.SelectedItem.ToString() == "Day Scholar")
                {
                    pnl_hostel.Visible = false;
                    pnl_day_boarding.Visible = true;
                    ddl_ledger.Text = "School";
                    ddl_ledger.Enabled = true;
                    ddl_ledger.CssClass = "form-select";
                }
                else
                {
                    pnl_hostel.Visible = false;
                    pnl_day_boarding.Visible = false;
                    ddl_ledger.Text = "School";
                    ddl_ledger.Enabled = true;
                    ddl_ledger.CssClass = "form-select";
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
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_hostel_id = (Label)row.FindControl("lbl_hostel_id");
                Label lbl_particular = (Label)row.FindControl("lbl_particular");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_month = (Label)row.FindControl("lbl_month");
                Label lbl_apply_for = (Label)row.FindControl("lbl_apply_for");
                Label lbl_day_boarding = (Label)row.FindControl("lbl_day_boarding");
                Label lbl_day_boarding_with_lunch = (Label)row.FindControl("lbl_day_boarding_with_lunch");
                Label lbl_Ledger = (Label)row.FindControl("lbl_Ledger");
                ViewState["courseID"] = lbl_class_id.Text;
                ViewState["MonthName"] = lbl_month.Text;
                ViewState["DoEditStatuS"] = "1";
                hd_id.Value = lbl_Id.Text;
                ddl_session.SelectedValue = lbl_session_id.Text;
                Bind_course_details();
                txt_perticular.Text = lbl_particular.Text;
                txt_amount.Text = lbl_amount.Text;
                 
                if (lbl_apply_for.Text == "Hosteller")
                {
                    ddl_apply_for.Text = "Hostel";
                }
                else
                {
                    ddl_apply_for.Text = lbl_apply_for.Text;
                }

                
                do_work();
                ddl_hostel.SelectedValue = lbl_hostel_id.Text;
                bind_month();
                if (lbl_day_boarding.Text == "true")
                {
                    ddl_day_boarding.Text = "Without Lunch";
                }
                else if (lbl_day_boarding_with_lunch.Text == "true")
                {
                    ddl_day_boarding.Text = "With Lunch";
                }
                else
                {
                    ddl_day_boarding.Text = "N/A";
                }
                ddl_ledger.Text = lbl_Ledger.Text;

                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                mycode.executequery("delete from Miscellaneous_Fee_Master where Id='" + lbl_Id.Text + "'");

                Alertme("Deletion process has been successfully done", "success");
                if (ViewState["flag"].ToString() == "0")
                {
                    bind_grd_view_all();
                }
                if (ViewState["flag"].ToString() == "1")
                {
                    bind_grid_by_session();
                }
                if (ViewState["flag"].ToString() == "2")
                {
                    bind_grid_by_class();
                }
                empty_form();
            }
            catch
            {
            }
        }

        protected void rp_class_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["DoEditStatuS"].ToString() == "1")
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Enabled = false;
                    if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                    {
                        ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
                    }
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Enabled = true;
                }
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

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_All(ddlclass, "select Course_Name,course_id from dbo.[Add_course_table]");
                ViewState["flag"] = "1";
                bind_grid_by_session();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_grid_by_session()
        {
            bind_grd_view("select Row_Number() over(order by mf.id) sl,mf.*,hm.Hostel_Name from Miscellaneous_Fee_Master mf left  join  Hostel_master hm on mf.Hostel_id=hm.Hostel_id where session_id=" + ddlsession.SelectedValue + "");
        }

        protected void btn_fnd_by_class_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    ViewState["flag"] = "2";
                    bind_grid_by_class();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_grid_by_class()
        {
            if (ddlclass.SelectedItem.Text == "All")
            {
                bind_grd_view("select Row_Number() over(order by mf.id) sl,mf.*,hm.Hostel_Name from Miscellaneous_Fee_Master mf left  join  Hostel_master hm on mf.Hostel_id=hm.Hostel_id where session_id=" + ddlsession.SelectedValue + "");
            }
            else
            {
                bind_grd_view("select Row_Number() over(order by mf.id) sl,mf.*,hm.Hostel_Name from Miscellaneous_Fee_Master mf left  join  Hostel_master hm on mf.Hostel_id=hm.Hostel_id where session_id=" + ddlsession.SelectedValue + " and class_id=" + ddlclass.SelectedValue + "");
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

        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_class.Items.Count; j++)
            {
                CheckBox chk_class = rp_class.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }
    }
}