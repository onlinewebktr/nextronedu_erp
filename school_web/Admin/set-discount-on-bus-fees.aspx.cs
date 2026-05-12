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
    public partial class set_discount_on_bus_fees : System.Web.UI.Page
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
                        ViewState["firm_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());


                        ViewState["discount_on"] = "TransportFee";
                        ViewState["group_id"] = "51";

                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");

                        ddl_session.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_Vehicle_Name, "select transport_name,transport_id from  Transport_Master order by transport_name");

                        ddl_Vehicle_Name.SelectedValue = My.get_top_one_vechile();

                        mycode.bind_all_ddl_with_id(ddl_transp_path, "Select distinct tp.Pathname,tp.TransportationPath_id from TransportationPath tp join Transportation_Boarding_Point tbp on    tp.Transportation_Id=tbp.Transportation_Id and tp.TransportationPath_id=tbp.TransportationPath_id where Session_Id='" + ddl_session.SelectedValue + "' and tp.Transportation_Id='"+ ddl_Vehicle_Name.SelectedValue + "' order by Pathname asc");

                        ddl_transp_path.SelectedValue = My.gettop_one_trans_path(ddl_Vehicle_Name.SelectedValue);


                        mycode.bind_all_ddl_with_id(ddl_boarding_point, " Select  Boarding_Point, Boarding_Point_id from Transportation_Boarding_Point  where Session_Id='" + ddl_session.SelectedValue + "' and Transportation_Id=" + ddl_Vehicle_Name.SelectedValue + " and TransportationPath_id=" + ddl_transp_path.SelectedValue + " order by Boarding_Point asc");

                        ddl_boarding_point.SelectedValue = My.get_topone_boarding_point(ddl_session.SelectedValue,ddl_Vehicle_Name.SelectedValue, ddl_transp_path.SelectedValue);




                        mnthsDv.Visible = true;
                        bind_month();

                        bind_grd_view_all();
                        ViewState["flag"] = "0";
                        ddl_transp_path.Focus();

                        string pagename_current = "set-discount-on-admission-fee.aspx";
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




                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Master");
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

        private void bind_month()
        {
            DataTable dt = mycode.FillData("select t2.* from dbo.[Transportation_Fee_Master] t1 join Month_Index t2 on t1.Month_id=t2.Month_Id where t1.session_id='" + ddl_session.SelectedValue + "' and t1.Transportation_path_id='" + ddl_transp_path.SelectedValue + "' and t1.Boarding_Point_id=" + ddl_boarding_point.SelectedValue + "    order by t2.Position asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Please set Bus fee details first.", "warning");
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




        private void bind_grd_view(string qry)
        {
            DataTable dt1 = mycode.FillData(qry);
            if (dt1.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                rd_viewaddedfee.DataSource = null;
                rd_viewaddedfee.DataBind();
            }
            else
            {

                rd_viewaddedfee.DataSource = dt1;
                rd_viewaddedfee.DataBind();
            }
        }

        protected void txt_fee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bindttl_fee();
            }
            catch (Exception ex)
            {
            }
        }

        private void bindttl_fee()
        {
            int i;
            double totalrate = 0; double totaldisc = 0;
            int gridview_rowcount = grd_fee.Rows.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox lbl_net_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                TextBox txt_disc_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_disc_fee");
                if (lbl_net_fee.Text != "")
                {
                    totalrate = totalrate + Convert.ToDouble(lbl_net_fee.Text);
                }
                if (txt_disc_fee.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(txt_disc_fee.Text);
                }
            }
            lbl_totalmrp.Text = totalrate.ToString();
            lbl_ttl_disc.Text = totaldisc.ToString();
        }

        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_content_id = (Label)e.Row.FindControl("lbl_content_id");
                TextBox txt_fee = (TextBox)e.Row.FindControl("txt_fee");
                Label lbl_totalmrp = (Label)e.Row.FindControl("lbl_totalmrp");
            }
        }

        protected void txt_disc_fee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bindttl_fee();
            }
            catch (Exception ex)
            {
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
            bind_fee_of_checked_month();
        }
        protected void ddl_Vehicle_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_Vehicle_Name.SelectedItem.Text == "Select")
            {
                Alertme("Please select vehicle name", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_transp_path, "Select distinct tp.Pathname,tp.TransportationPath_id from TransportationPath tp join Transportation_Boarding_Point tbp on    tp.Transportation_Id=tbp.Transportation_Id and tp.TransportationPath_id=tbp.TransportationPath_id where Session_Id='" + ddl_session.SelectedValue + "' and tp.Transportation_Id='"+ ddl_Vehicle_Name.SelectedValue + "' order by Pathname asc");

            }
        }
        protected void ddl_transp_path_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mnthsDv.Visible = false;
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_Vehicle_Name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select vehicle name", "warning");
                }
                else if (ddl_transp_path.SelectedItem.Text == "Select")
                {
                    Alertme("Please select vehicle Raute", "warning");
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_boarding_point, "Select  Boarding_Point, Boarding_Point_id from Transportation_Boarding_Point  where Session_Id='" + ddl_session.SelectedValue + "' and Transportation_Id=" + ddl_Vehicle_Name.SelectedValue + " and TransportationPath_id=" + ddl_transp_path.SelectedValue + " order by Boarding_Point asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_boarding_point_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnl_fee_grid.Visible = false;
                mnthsDv.Visible = false;
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_Vehicle_Name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select vehicle name", "warning");
                }
                else if (ddl_transp_path.SelectedItem.Text == "Select")
                {
                    Alertme("Please select vehicle Raute", "warning");
                }
                else if (ddl_boarding_point.SelectedItem.Text == "Select")
                {
                    Alertme("Please select boarding point", "warning");
                }
                else
                {
                    mnthsDv.Visible = true;
                    bind_month();
                    bind_grd_view_all();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void bind_grd_view_all()
        {
            bind_grd_view("select t1.*,t2.*, (cast(amount as float)-cast(disc_amount as float)) after_disc,(select top 1 Pathname from TransportationPath where TransportationPath_id=t1.Bus_path) as Path_name,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.Bus_path and Boarding_Point_id=t1.Boarding_Point_id) as Boarding_Point from dbo.[Discount_Master_for_bus] t1  join Transportation_Fee_Master t2 on t1.session_id=t2.session_id and t1.Month=t2.month and t1.Boarding_Point_id=t2.Boarding_Point_id  join Month_Index t3 on t2.Month_id=t3.Month_Id where t1.Transportation_Id=" + ddl_Vehicle_Name.SelectedValue+" and t1.group_id='" + ViewState["group_id"].ToString() + "' and t2.Transportation_path_id='" + ddl_transp_path.SelectedValue + "' and t2.Boarding_Point_id=" + ddl_boarding_point.SelectedValue + " and t1.admission_no='All' and t1.session_id=" + ddl_session.SelectedValue + " order by t3.Position asc");
        }
        protected void chk_month_name_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bind_fee_of_checked_month();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_fee_of_checked_month()
        {
            int i;
            string months = "0"; ViewState["checkmntH"] = "0";
            int gridview_rowcount = rp_month.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                CheckBox chk_month_name = (CheckBox)rp_month.Items[i].FindControl("chk_month_name");
                Label lbl_month_id = (Label)rp_month.Items[i].FindControl("lbl_month_id");
                if (chk_month_name.Checked == true)
                {
                    if (ViewState["checkmntH"].ToString() == "0")
                    {
                        months = "t1.Month_id=" + lbl_month_id.Text;
                        ViewState["checkmntH"] = "1";
                    }
                    else
                    {
                        months = months + " or t1.Month_id=" + lbl_month_id.Text;
                    }
                }
            }

            string dd = ddl_session.SelectedValue;
            string query1 = "select t1.* from dbo.[Transportation_Fee_Master] t1 join Month_Index t2 on t1.Month_id=t2.Month_Id where (" + months + ") and t1.session_id='" + ddl_session.SelectedValue + "' and t1.Transportation_path_id='" + ddl_transp_path.SelectedValue + "' and  t1.Boarding_Point_id="+ddl_boarding_point.SelectedValue+" order by t2.Position asc";
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
                pnl_fee_grid.Visible = false;
            }
            else
            {
                btn_Submit.Visible = true;
                grd_fee.DataSource = dt1;
                grd_fee.DataBind();
                pnl_fee_grid.Visible = true;
                bindttl_fee();
            }
        }


        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    ViewState["issubmit"] = "0";
                    save_data();
                    if (ViewState["issubmit"].ToString() == "1")
                    {
                        empty_form();
                        bind_grd_view_all();
                        Alertme("Discount master created successfully", "success");
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    ViewState["issubmit"] = "0";
                    save_data();
                    if (ViewState["issubmit"].ToString() == "1")
                    {
                        empty_form();
                        bind_grd_view_all();
                        Alertme("Discount master created successfully", "success");
                    }
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

        private void save_data()
        {
            if (ddl_transp_path.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus path.", "warning");
                ddl_transp_path.Focus();
                return;
            }



            //==========
            bool issubmit = false;
            bool isamountfill = false;
            for (int i = 0; i < grd_fee.Rows.Count; i++)
            {
                TextBox lbl_reg_id = (TextBox)grd_fee.Rows[i].FindControl("txt_disc_fee");
                if (mycode.chkenum(lbl_reg_id.Text) == true)
                {
                    isamountfill = true;
                }
            }
            if (isamountfill == true)
            {
                int mgrowcount = rp_month.Items.Count;
                int kl = 0;
                for (int ixi = 0; ixi < mgrowcount; ixi++)
                {
                    CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                    if (chkM.Checked == true)
                    {
                        Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                        Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                        #region #fff
                        double totla = 0;
                        for (int i = 0; i < grd_fee.Rows.Count; i++)
                        {
                            TextBox txt_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                            TextBox txt_disc_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_disc_fee");
                            Label lbl_content_id = (Label)grd_fee.Rows[i].FindControl("lbl_content_id");
                            Label lbl_content = (Label)grd_fee.Rows[i].FindControl("lbl_content");
                            totla = totla + My.toDouble(txt_fee.Text);
                            //==============*************** 
                            if (My.toDouble(txt_disc_fee.Text) > 0)
                            {
                                if (My.toDouble(txt_disc_fee.Text) <= My.toDouble(txt_fee.Text))
                                {
                                    //  bool chek_fee = My.find_bus_fee_tkn("TransportFee", ddl_session.SelectedItem.Text, lbl_month_name.Text);
                                    bool chek_fee = My.find_month_transp_fee_collected_N_discunt_chek(ddl_session.SelectedValue, ddl_Vehicle_Name.SelectedValue, ddl_transp_path.SelectedValue, ddl_boarding_point.SelectedValue, lbl_month_name.Text);
                                    if (chek_fee == false)
                                    {
                                        Alertme("You can't add/update discount  because fee has been taken.", "warning");


                                    }
                                    else
                                    {
                                        SqlDataAdapter ad = new SqlDataAdapter("select * from Discount_Master_for_bus where Bus_path=" + ddl_transp_path.SelectedValue + " and session_id='" + ddl_session.SelectedValue + "' and month='" + lbl_month_name.Text + "' and admission_no='ALL' and fee_head_id='" + lbl_content_id.Text + "' and Transportation_Id=" + ddl_Vehicle_Name.SelectedValue + " and TransportationPath_id=" + ddl_transp_path.SelectedValue + " and Boarding_Point_id=" + ddl_boarding_point.SelectedValue + "", My.conn);
                                        DataSet ds = new DataSet();
                                        ad.Fill(ds, "Discount_Master_for_bus");
                                        DataTable dt = ds.Tables[0];
                                        if (dt.Rows.Count == 0)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dr["Class_id"] = "0";
                                            dr["Discount_on"] = ViewState["discount_on"].ToString();
                                            dr["session"] = ddl_session.SelectedItem.Text;
                                            dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                            dr["group_id"] = My.toint(ViewState["group_id"].ToString());
                                            dr["admission_no"] = "ALL";
                                            dr["month"] = lbl_month_name.Text;
                                            dr["fee_head_id"] = lbl_content_id.Text;
                                            dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                            dr["parameter_id"] = "0";
                                            dr["category_id"] = "0";
                                            dr["sub_category_id"] = "0";
                                            dr["Bus_path"] = ddl_transp_path.SelectedValue;
                                            dr["TransportationPath_id"] = ddl_transp_path.SelectedValue;
                                            dr["session_id"] = ddl_session.SelectedValue;
                                            dr["Branch_id"] = ViewState["firm_id"].ToString();
                                            dr["User_id"] = ViewState["Userid"].ToString();
                                            dr["Date"] = mycode.date();
                                            dr["time"] = mycode.time();
                                            dr["Transportation_Id"] = ddl_Vehicle_Name.SelectedValue;
                                            dr["TransportationPath_id"] = ddl_transp_path.SelectedValue;
                                            dr["Boarding_Point_id"] = ddl_boarding_point.SelectedValue;
                                            dr["discount_for"] = "TransportFee";
                                            
                                            dt.Rows.Add(dr);
                                            issubmit = true;
                                            ViewState["issubmit"] = "1";
                                        }
                                        else
                                        {
                                            foreach (DataRow dr in dt.Rows)
                                            {
                                                string ss = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2).ToString();
                                                string sdd = txt_disc_fee.Text;
                                                dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                                dr["fee_head_id"] = lbl_content_id.Text;
                                                dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                                dr["category_id"] = "0";
                                                dr["sub_category_id"] = "0";
                                            }
                                            issubmit = true;
                                            ViewState["issubmit"] = "1";
                                        }
                                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                        ad.Update(dt);
                                    }
                                }
                                else
                                {
                                    Alertme("Discount amount not greater then fees amount.", "warning");
                                }
                            }
                            else
                            {
                                Alertme("Please enter discount amount.", "warning");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        kl++;
                    }
                }

                if (kl == mgrowcount)
                {
                    Alertme("Please check minimum one month.", "warning");
                    return;
                }
            }
            else
            {
                Alertme("Please enter discount amount.", "warning");
            }
        }

        private void empty_form()
        {
            int i;
            int gridview_rowcount = grd_fee.Rows.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox txt_disc_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_disc_fee");
                txt_disc_fee.Text = "0";
            }
            bind_month();
            chk_all_month.Checked = false;
            lbl_ttl_disc.Text = "0";
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            pnl_fee_grid.Visible = false;
            ddl_session.SelectedValue = My.get_session_id();
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_month = (Label)row.FindControl("lbl_mnths");
                    Label lbl_session = (Label)row.FindControl("lbl_session");
                    Label lbl_Transportation_Id = (Label)row.FindControl("lbl_Transportation_Id");
                    Label lbl_TransportationPath_id = (Label)row.FindControl("lbl_TransportationPath_id");
                    Label lbl_Boarding_Point_id = (Label)row.FindControl("lbl_Boarding_Point_id");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    //bool chek_fee = My.find_bus_fee_tkn("TransportFee", lbl_session.Text, lbl_month.Text);
                    bool chek_fee = My.find_month_transp_fee_collected_N_discunt_chek(lbl_session_id.Text, lbl_Transportation_Id.Text, lbl_TransportationPath_id.Text, lbl_Boarding_Point_id.Text, lbl_month.Text);

                    if (chek_fee == false)
                    {
                        Alertme("You can't dalete because fee has been taken.", "warning");
                        return;
                    }

                    ddl_session.SelectedValue = My.get_session_id();
                    mycode.executequery("delete from Discount_Master_for_bus where Id=" + lbl_Id.Text + "");
                    Alertme("Deletion process has been successfully done", "success");
                    bind_grd_view_all();
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