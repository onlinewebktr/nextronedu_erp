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
    public partial class Hsotel_discount_on_monthly_fee : System.Web.UI.Page
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
                        btn_Submit.Visible = false;
                        ViewState["Userid"] = Session["Admin"].ToString();

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Hostel_Master.aspx";
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

                        mycode.bind_all_ddl_with_id_no_select(ddl_category, "select Category_Name,Category_Id from Category_Details");

                        mycode.bind_all_ddl_with_id(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        mycode.bind_all_ddl_with_id(ddl_hostel_search, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        ddl_hostel_search.SelectedValue = My.get_top_one_hostel_name();

                        mycode.bind_all_ddl_with_id(ddl_room_catogery, "select Category_name,Category_id from Hostel_room_category_master   order by Category_name asc");

                        mycode.bind_all_ddl_with_id_All_New(ddl_room_category_search, "select Category_name,Category_id from Hostel_room_category_master   order by Category_name asc");

                        ddl_room_catogery.SelectedValue = My.get_top_one_hostel_catogery(ddl_hostel.SelectedValue, ddl_hostel.SelectedValue);
                        ddl_room_category_search.SelectedValue = My.get_top_one_hostel_catogery(ddl_hostel.SelectedValue, ddl_hostel.SelectedValue);




                        mycode.bind_all_ddl_with_id_All_New(ddl_month_srch, "select Month,Month_Id from Month_Index order by Position asc");

                        bind_sub_category();

                        bind_grid_by_class();

                        bind_month();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hsotel_discount_on_monthly_fee");
            }
        }

        protected void ddl_hostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Submit.Visible = false;
            grd_fee.DataSource = null;
            grd_fee.DataBind();
            pnl_fee_grid.Visible = false;
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_category.SelectedItem.Text == "Select")
            {
                Alertme("Please select category", "warning");
            }
            else if (ddl_sub_category.SelectedItem.Text == "Select")
            {
                Alertme("Please select sub-category", "warning");
            }


            else if (ddl_hostel.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_room_catogery, "select Category_name,Category_id from Hostel_room_category_master    order by Category_name asc");

                ddl_room_catogery.SelectedValue = My.get_top_one_hostel_catogery(ddl_hostel.SelectedValue, ddl_hostel.SelectedValue);

                mycode.bind_all_ddl_with_id_All_New(ddl_room_category_search, "select Category_name,Category_id from Hostel_room_category_master    order by Category_name asc");

                ddl_room_category_search.SelectedValue = My.get_top_one_hostel_catogery(ddl_hostel.SelectedValue, ddl_hostel.SelectedValue);
            }
        }
        protected void ddl_room_catogery_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_room_catogery_data();
              
            }
            catch (Exception ex)
            {
            }
        }

        private void find_room_catogery_data()
        {
            btn_Submit.Visible = false;
            grd_fee.DataSource = null;
            grd_fee.DataBind();
            pnl_fee_grid.Visible = false;
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_category.SelectedItem.Text == "Select")
            {
                Alertme("Please select category", "warning");
            }
            else if (ddl_sub_category.SelectedItem.Text == "Select")
            {
                Alertme("Please select sub-category", "warning");
            }


            else if (ddl_hostel.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel", "warning");
            }
            else
            {
                string query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' disccount from dbo.[Hostel_Fee_master_content_wise] fmc join Hostel_fee_head_master cm on fmc.content_id=cm.content_id  where     fmc.session_id='" + ddl_session.SelectedValue + "' and cm.group_id='3' and fmc.Hostel_id=" + ddl_hostel.SelectedValue + "  and fmc.Room_Category_id='" + ddl_room_catogery.SelectedValue + "'";
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    btn_Submit.Visible = false;
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
        }

        private void bind_month()
        {
            DataTable dt = mycode.FillData("select Month,'false' as Value,Month_Id from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_months.DataSource = null;
                rp_months.DataBind();
            }
            else
            {
                rp_months.DataSource = dt;
                rp_months.DataBind();
            }
        }
        protected void rp_month_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //if (((Label)e.Item.FindControl("lbl_month_name")).Text == ViewState["monthS"].ToString())
                //{
                //    ((CheckBox)e.Item.FindControl("chk_month_name")).Checked = true;
                //}
            }
        }
        protected void chk_all_month_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_months.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_months.Items[j].FindControl("chk_month_name") as CheckBox;
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
        protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_sub_category();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_sub_category()
        {
            mycode.bind_all_ddl_with_id_no_select(ddl_sub_category, "select Sub_CategoryName,Sub_CategoryId from dbo.[Sub_Category_Details] where Category_Id='" + ddl_category.SelectedValue + "'");
        }

        private void bind_grd_view(string qry)
        {
            print1.Visible = false;
            btn_excels.Visible = false;
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
                btn_excels.Visible = true;
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



        protected void txt_fee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox btnc = (TextBox)sender;
                GridViewRow row = (GridViewRow)btnc.Parent.Parent;

                string txt_fee = ((TextBox)row.FindControl("txt_fee")).Text;
                string txt_disc_fee = ((TextBox)row.FindControl("txt_disc_fee")).Text;
                if (My.toDouble(txt_disc_fee) <= My.toDouble(txt_fee))
                {
                }
                else
                {
                    ((TextBox)row.FindControl("txt_disc_fee")).Text = "0";
                    Alertme("Please enter discount less than fees amount.", "warning");
                }
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
            lbl_totalmrp.Text = totalrate.ToString("0.00");
            lbl_ttl_disc.Text = totaldisc.ToString("0.00");


            
             // footer data bound
            var footerRow = grd_fee.FooterRow;
            Label lbl_full_amount = (footerRow.FindControl("lbl_full_amount")) as Label;
            lbl_full_amount.Text = lbl_totalmrp.Text;
            Label lbl_full_discount = (footerRow.FindControl("lbl_full_discount")) as Label;
            lbl_full_discount.Text = lbl_ttl_disc.Text;


        }

        double totalamount = 0, todatl_discount = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_content_id = (Label)e.Row.FindControl("lbl_content_id");
                TextBox txt_fee = (TextBox)e.Row.FindControl("txt_fee");
                Label lbl_totalmrp = (Label)e.Row.FindControl("lbl_totalmrp");

                TextBox txt_disc_fee = (TextBox)e.Row.FindControl("txt_disc_fee");

                totalamount = totalamount + My.toDouble(txt_fee.Text);

                todatl_discount = todatl_discount + My.toDouble(txt_disc_fee.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_footr_totalCollection = (Label)e.Row.FindControl("lbl_full_amount");
                Label lbl_full_discount = (Label)e.Row.FindControl("lbl_full_discount");

                lbl_footr_totalCollection.Text = totalamount.ToString("0.00");
                lbl_full_discount.Text = todatl_discount.ToString("0.00");
            }
        }

        protected void txt_disc_fee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox btnc = (TextBox)sender;
                GridViewRow row = (GridViewRow)btnc.Parent.Parent;

                string txt_fee = ((TextBox)row.FindControl("txt_fee")).Text;
                string txt_disc_fee = ((TextBox)row.FindControl("txt_disc_fee")).Text;
                if (My.toDouble(txt_disc_fee) <= My.toDouble(txt_fee))
                {
                }
                else
                {
                    ((TextBox)row.FindControl("txt_disc_fee")).Text = "0";
                    Alertme("Please enter discount less than fees amount.", "warning");
                }




                //find_room_catogery_data();
                bindttl_fee();

            }
            catch (Exception ex)
            {
            }
        }



        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_category.SelectedItem.Text == "Select")
                {
                    Alertme("Please select category", "warning");
                }
                else if (ddl_sub_category.SelectedItem.Text == "Select")
                {
                    Alertme("Please select sub-category", "warning");
                }


                else if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel", "warning");
                }
                else if (ddl_room_catogery.SelectedItem.Text == "Select")
                {
                    Alertme("Please select room category", "warning");
                }

                else
                {

                    ddl_hostel_search.SelectedValue = ddl_hostel.SelectedValue;
                    ddl_room_category_search.SelectedValue = ddl_room_catogery.SelectedValue;

                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        ViewState["issubmit"] = "0";
                        save_data();
                        if (ViewState["issubmit"].ToString() == "1")
                        {
                            empty_form();
                            bind_grid_by_class();
                            Alertme("Month Discount Saved successfully.", "success");
                        }
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        ViewState["issubmit"] = "0";
                        save_data();
                        if (ViewState["issubmit"].ToString() == "1")
                        {
                            empty_form();
                            bind_grid_by_class();
                            Alertme("Month Discount Saved successfully.", "success");
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_data()
        {


            string parameter_New = "";
            string parameter_id = "0";
            parameter_New = "HostelMonthlyFee";//5
            parameter_id = "3";


            ViewState["discount_on"] = "Monthly";
            parameter_New = "HostelMonthlyFee";//5
            parameter_id = "3";


            bool issubmit = false;
            bool isamountfill = false;


            int mgrowcount = rp_months.Items.Count;
            int kl = 0;
            for (int ixi = 0; ixi < mgrowcount; ixi++)
            {
                CheckBox chkM = (CheckBox)rp_months.Items[ixi].FindControl("chk_month_name");
                if (chkM.Checked == true)
                {
                    Label lbl_month_id = (Label)rp_months.Items[ixi].FindControl("lbl_month_id");
                    Label lbl_month_name = (Label)rp_months.Items[ixi].FindControl("lbl_month_name");
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
                                    bool chek_fee = My.find_disc_fee_collected_hostel_month_fee(ddl_category.SelectedValue, ddl_sub_category.SelectedValue, ddl_session.SelectedItem.Text,
        lbl_content_id.Text, parameter_id, ddl_hostel.SelectedValue, lbl_month_name.Text, ddl_room_catogery.SelectedValue);
                                    if (chek_fee == false)
                                    {
                                        Alertme("You can't update because fee has been taken.", "warning");
                                    }
                                    else
                                    {
                                        SqlDataAdapter ad = new SqlDataAdapter("select * from Hostel_Discount_master where  session_id='" + ddl_session.SelectedValue + "' and group_id='3' and month='"+ lbl_month_name.Text + "' and admission_no='ALL' and content_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and category_id='" + ddl_category.SelectedValue + "' and sub_category_id='" + ddl_sub_category.SelectedValue + "' and Month='" + lbl_month_name.Text + "' and Room_Category_id=" + ddl_room_catogery.SelectedValue + " and   Hostel_id=" + ddl_hostel.SelectedValue + "", My.conn);
                                        DataSet ds = new DataSet();
                                        ad.Fill(ds, "Hostel_Discount_master");
                                        DataTable dt = ds.Tables[0];
                                        if (dt.Rows.Count == 0)
                                        {
                                            DataRow dr = dt.NewRow();

                                            dr["Discount_on"] = ViewState["discount_on"].ToString();
                                            dr["session"] = ddl_session.SelectedItem.Text;
                                            dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                            dr["group_id"] = "3";
                                            dr["admission_no"] = "ALL";
                                            dr["month"] = lbl_month_name.Text;
                                            dr["Month_id"] = lbl_month_id.Text;
                                            dr["content_id"] = lbl_content_id.Text;
                                            dr["content"] = lbl_content.Text;

                                            dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                            dr["parameter"] = parameter_New;
                                            dr["parameter_id"] = parameter_id;
                                            dr["category_id"] = ddl_category.SelectedValue;
                                            dr["sub_category_id"] = ddl_sub_category.SelectedValue;

                                            dr["session_id"] = ddl_session.SelectedValue;
                                            dr["Branch_id"] = ViewState["branchid"].ToString();
                                            dr["User_id"] = ViewState["Userid"].ToString();
                                            dr["Date"] = mycode.date();
                                            dr["time"] = mycode.time();
                                            dr["Hostel_id"] = ddl_hostel.SelectedValue;
                                            dr["Room_Category_id"] = ddl_room_catogery.SelectedValue;
                                            dt.Rows.Add(dr);
                                            issubmit = true;
                                            ViewState["issubmit"] = "1";
                                        }
                                        else
                                        {
                                            foreach (DataRow dr in dt.Rows)
                                            {
                                                dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                                dr["content_id"] = lbl_content_id.Text;
                                                dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                                dr["category_id"] = ddl_category.SelectedValue;
                                                dr["sub_category_id"] = ddl_sub_category.SelectedValue;
                                                dr["sub_category_id"] = ddl_sub_category.SelectedValue;
                                                dr["Hostel_id"] = ddl_hostel.SelectedValue;
                                                dr["content"] = lbl_content.Text;
                                                dr["Room_Category_id"] = ddl_room_catogery.SelectedValue;
                                            }
                                            issubmit = true;
                                            ViewState["issubmit"] = "1";
                                        }
                                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                        ad.Update(dt);
                                    }
                                }
                            }
                            else
                            {
                                Alertme("Please enter discount amount.", "warning");
                            }
                        }
                    }
                    else
                    {
                        Alertme("Please enter discount amount.", "warning");
                    }
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

        private void empty_form()
        {
            int i;
            int gridview_rowcount = grd_fee.Rows.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox txt_disc_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_disc_fee");
                txt_disc_fee.Text = "0";
            }
            lbl_ttl_disc.Text = "0";


            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    Label lbl_category_id = (Label)row.FindControl("lbl_category_id");
                    Label lbl_sub_category_id = (Label)row.FindControl("lbl_sub_category_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_fee_head_id = (Label)row.FindControl("lbl_fee_head_id");
                    Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");
                    Label lbl_Hostel_id = (Label)row.FindControl("lbl_Hostel_id");

                    bool chek_fee = My.find_disc_fee_collected_hostel(lbl_category_id.Text, lbl_sub_category_id.Text, ddl_session.SelectedItem.Text, lbl_class_id.Text, lbl_fee_head_id.Text, lbl_parameter_id.Text, lbl_Hostel_id.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't delete because fee has been taken.", "warning");
                        return;
                    }
                    mycode.executequery("delete from Hostel_Discount_master where Id=" + lbl_Id.Text + "");
                    Alertme("Deletion process has been successfully done", "success");
                    bind_grid_by_class();
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

        protected void btn_fnd_by_class_Click(object sender, EventArgs e)
        {
            try
            {
                bind_grid_by_class();
                ViewState["flag"] = "1";
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_grid_by_class()
        {


            if (ddl_room_category_search.SelectedItem.Text == "ALL")
            {

                lbl_class22.Text = "Hostel :" + ddl_hostel.SelectedItem.Text + " Room Category :" + ddl_room_category_search.SelectedItem.Text + ", Month :" + ddl_month_srch.SelectedItem.Text;


                if (ddl_month_srch.SelectedItem.Text == "ALL")
                {
                    bind_grd_view("select distinct (Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id) as Hostel_name,(Select top 1 Category_name from Hostel_room_category_master where Category_id=dm.Room_Category_id) as room_name_catogery,dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id and dm.Room_Category_id=fmc.Room_Category_id where  dm.session_id='" + ddl_session.SelectedValue + "' and dm.group_id='3'  and dm.Hostel_id='" + ddl_hostel_search.SelectedValue + "'  and dm.admission_no='ALL'  ");
                }
                else
                {
                    bind_grd_view("select distinct (Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id) as Hostel_name,(Select top 1 Category_name from Hostel_room_category_master where Category_id=dm.Room_Category_id) as room_name_catogery, dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id and dm.Room_Category_id=fmc.Room_Category_id where  dm.session_id='" + ddl_session.SelectedValue + "' and dm.group_id='3'  and dm.Hostel_id='" + ddl_hostel_search.SelectedValue + "'  and dm.admission_no='ALL' and  dm.Month_id=" + ddl_month_srch.SelectedValue + " ");
                }

            }

            else if (ddl_room_category_search.SelectedItem.Text != "ALL")
            {
                if (ddl_month_srch.SelectedItem.Text == "ALL")
                {
                    lbl_class22.Text = " Hostel :" + ddl_hostel.SelectedItem.Text + " Room Category :" + ddl_room_category_search.SelectedItem.Text;
                    bind_grd_view("select distinct (Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id) as Hostel_name,(Select top 1 Category_name from Hostel_room_category_master where Category_id=dm.Room_Category_id) as room_name_catogery, dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id and dm.Room_Category_id=fmc.Room_Category_id where  dm.session_id='" + ddl_session.SelectedValue + "' and dm.group_id='3'  and dm.Hostel_id='" + ddl_hostel_search.SelectedValue + "' and dm.Room_Category_id=" + ddl_room_category_search.SelectedValue + "  and dm.admission_no='ALL'   ");

                }
                else
                {
                    lbl_class22.Text = "Hostel :" + ddl_hostel.SelectedItem.Text + " Room Category :" + ddl_room_category_search.SelectedItem.Text;
                    bind_grd_view("select distinct (Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id) as Hostel_name,(Select top 1 Category_name from Hostel_room_category_master where Category_id=dm.Room_Category_id) as room_name_catogery, dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id and dm.Room_Category_id=fmc.Room_Category_id where  dm.session_id='" + ddl_session.SelectedValue + "' and dm.group_id='3'  and dm.Hostel_id='" + ddl_hostel_search.SelectedValue + "' and dm.Room_Category_id=" + ddl_room_category_search.SelectedValue + "  and dm.admission_no='ALL' and  dm.Month_id=" + ddl_month_srch.SelectedValue + "  ");
                }

            }
            else
            {
                lbl_class22.Text = " Hostel :" + ddl_hostel.SelectedItem.Text + " Room Category :" + ddl_room_category_search.SelectedItem.Text;
                if (ddl_month_srch.SelectedItem.Text == "ALL")
                {
                    bind_grd_view("select distinct (Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id) as Hostel_name,(Select top 1 Category_name from Hostel_room_category_master where Category_id=dm.Room_Category_id) as room_name_catogery, dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id and dm.Room_Category_id=fmc.Room_Category_id where  dm.session_id='" + ddl_session.SelectedValue + "' and dm.group_id='3'  and  dm.Hostel_id=" + ddl_hostel_search.SelectedValue + " and dm.admission_no='ALL'    and dm.Room_Category_id=" + ddl_room_category_search.SelectedValue + "  ");
                }
                else
                {
                    bind_grd_view("select distinct (Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id) as Hostel_name,(Select top 1 Category_name from Hostel_room_category_master where Category_id=dm.Room_Category_id) as room_name_catogery, dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id and dm.Room_Category_id=fmc.Room_Category_id where  dm.session_id='" + ddl_session.SelectedValue + "' and dm.group_id='3'  and  dm.Hostel_id=" + ddl_hostel_search.SelectedValue + " and dm.admission_no='ALL'  and dm.Room_Category_id=" + ddl_room_category_search.SelectedValue + "  and dm.Month_id = " + ddl_month_srch.SelectedValue + "  ");

                }




            }

        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {

            bind_grid_by_class();
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