using school_web.AppCode;
using System;
using System.Collections;
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
    public partial class set_discount_on_monthly_fee : System.Web.UI.Page
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


                        ViewState["discount_on"] = "Monthly";
                        ViewState["group_id"] = "3";

                        string q = "";
                        q = "select 'Day Scholar' Parameter_Name,'4' parameter_id union all  select 'Day Boarding with Lunch' Parameter_Name,'44' parameter_id union all  select 'Hostel' Parameter_Name,'3' parameter_id";
                        //q = "select 'Days' Parameter_Name,'4' parameter_id union all  select 'Day Boarding with Lunch' Parameter_Name,'44' parameter_id";
                        mycode.bind_all_ddl_with_id_no_select(ddl_parameter, q);
                        mycode.bind_all_ddl_with_id_no_select(ddl_discount_for_srch, q);
                        bind_month(); bind_months();
                        mycode.bind_all_ddl_with_id(ddl_class, "select Course_Name,course_id  from dbo.[Add_course_table]  order by Position asc");
                        mycode.bind_all_ddl_with_id_All(ddlclass, "select Course_Name,course_id  from dbo.[Add_course_table]  order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();

                        bind_grd_view_all(ddl_session.SelectedItem.Text);
                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id_no_select(ddl_category, "select Category_Name,Category_Id from Category_Details");
                        mycode.bind_all_ddl_with_id(ddl_hostel_name, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        mycode.bind_all_ddl_with_id(ddl_room_catogery, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                        bind_sub_category();
                        ddl_class.Focus();

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

        private void bind_months()
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
            mycode.bind_all_ddl_with_id(ddl_fnd_months, "select Month,Month_Id from Month_Index order by Position asc");
        }

        private void bind_grd_view_all(string session)
        {
            bind_grd_view("select ac.Course_Name,dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id where dm.session='" + session + "' and dm.group_id='" + ViewState["group_id"].ToString() + "' and dm.admission_no='All' and dm.parameter_id='" + ddl_discount_for_srch.SelectedValue + "'  order by ac.Position asc");
        }


        private void bind_grd_view(string qry)
        {
            DataTable dt1 = mycode.FillData(qry);
            if (dt1.Rows.Count == 0)
            {
                rd_viewaddedfee.DataSource = null;
                rd_viewaddedfee.DataBind();
            }
            else
            {
                lbl_class22.Text = "Discount For :" + ddl_discount_for_srch.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
                rd_viewaddedfee.DataSource = dt1;
                rd_viewaddedfee.DataBind();
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
                        bind_grd_view_all(ddl_session.SelectedItem.Text);
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
                        bind_grd_view_all(ddl_session.SelectedItem.Text);
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
            if (ddl_parameter.SelectedItem.Text == "Select")
            {
                Alertme("Please select Discount For", "warning");
                ddl_parameter.Focus();
                return;
            }
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select Class", "warning");
                ddl_class.Focus();
                return;
            }
            if (ddl_parameter.SelectedValue == "3")
            {
                if (ddl_hostel_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Class", "warning");
                    ddl_hostel_name.Focus();
                    return;

                }
                else if (ddl_room_catogery.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Class", "warning");
                    ddl_hostel_name.Focus();
                    return;

                }
            }




            //==========
            bool issubmit = false;
            bool isamountfill = false;
            for (int i = 0; i < rp_fee_details.Items.Count; i++)
            {
                TextBox txt_disc_fee = (TextBox)rp_fee_details.Items[i].FindControl("txt_disc_fee");
                if (My.toDouble(txt_disc_fee.Text) > 0)
                {
                    isamountfill = true;
                }
            }


            //====CHECKED MONTH 
            bool isMonthChecked = false;
            for (int i = 0; i < rp_month.Items.Count; i++)
            {
                CheckBox chkMM = (CheckBox)rp_month.Items[i].FindControl("chk_month_name");
                if (chkMM.Checked == true)
                {
                    isMonthChecked = true;
                }
            }
            if (isMonthChecked == false)
            {
                Alertme("Please select the month during which you want to apply the discount.", "warning");
                return;
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

                        #region DISCOUNTS
                        bool chek_fee = false;
                        string query = "";
                        double totla = 0;
                        for (int i = 0; i < rp_fee_details.Items.Count; i++)
                        {
                            TextBox txt_fee = (TextBox)rp_fee_details.Items[i].FindControl("txt_fee");
                            TextBox txt_disc_fee = (TextBox)rp_fee_details.Items[i].FindControl("txt_disc_fee");
                            Label lbl_content_id = (Label)rp_fee_details.Items[i].FindControl("lbl_content_id");
                            Label lbl_content = (Label)rp_fee_details.Items[i].FindControl("lbl_content");
                            totla = totla + My.toDouble(txt_fee.Text);
                            //==============*************** 
                            if (My.toDouble(txt_disc_fee.Text) > 0)
                            {
                                string discountAmt = txt_disc_fee.Text;
                                if (My.toDouble(discountAmt) > My.toDouble(txt_fee.Text))
                                {
                                    discountAmt = txt_fee.Text;
                                }


                                if (ddl_parameter.SelectedValue == "3")
                                {
                                    chek_fee = My.find_disc_fee_collected_hostel_month_fee(ddl_category.SelectedValue, ddl_sub_category.SelectedValue, ddl_session.SelectedItem.Text,
         lbl_content_id.Text, ddl_parameter.SelectedValue, ddl_hostel_name.SelectedValue, lbl_month_name.Text, ddl_room_catogery.SelectedValue);

                                }
                                else
                                {
                                    chek_fee = My.find_mnth_disc_fee_collected("MonthlyFee", ddl_category.SelectedValue, ddl_sub_category.SelectedValue, ddl_session.SelectedItem.Text, ddl_class.SelectedValue, lbl_content_id.Text, ddl_parameter.SelectedValue, lbl_month_name.Text);
                                }
                                if (chek_fee == false)
                                {
                                    Alertme("You can't update discount because fee has been taken.", "warning");
                                }
                                else
                                {
                                    if (ddl_parameter.SelectedValue == "3")
                                    {
                                        query = "select * from Discount_Master where Class_id=" + ddl_class.SelectedValue + " and session='" + ddl_session.SelectedItem.Text + "' and group_id='" + ViewState["group_id"].ToString() + "' and month='" + lbl_month_name.Text + "' and admission_no='ALL' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ddl_parameter.SelectedValue + "' and category_id='" + ddl_category.SelectedValue + "' and sub_category_id='" + ddl_sub_category.SelectedValue + "' and Hostel_id=" + ddl_hostel_name.SelectedValue + " and Room_Category_id=" + ddl_room_catogery.SelectedValue + "";
                                    }
                                    else
                                    {
                                        query = "select * from Discount_Master where Class_id=" + ddl_class.SelectedValue + " and session='" + ddl_session.SelectedItem.Text + "' and group_id='" + ViewState["group_id"].ToString() + "' and month='" + lbl_month_name.Text + "' and admission_no='ALL' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ddl_parameter.SelectedValue + "' and category_id='" + ddl_category.SelectedValue + "' and sub_category_id='" + ddl_sub_category.SelectedValue + "'";
                                    }

                                    SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);

                                    DataSet ds = new DataSet();
                                    ad.Fill(ds, "Discount_Master");
                                    DataTable dt = ds.Tables[0];
                                    if (dt.Rows.Count == 0)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Class_id"] = ddl_class.SelectedValue;
                                        dr["Discount_on"] = ViewState["discount_on"].ToString();
                                        dr["session"] = ddl_session.SelectedItem.Text;
                                        dr["Discount_per"] = My.Round((My.toDouble(discountAmt) * 100) / My.toDouble(txt_fee.Text), 2);
                                        dr["group_id"] = My.toint(ViewState["group_id"].ToString());
                                        dr["admission_no"] = "ALL";
                                        dr["month"] = lbl_month_name.Text;
                                        dr["fee_head_id"] = lbl_content_id.Text;
                                        dr["disc_amount"] = My.toDouble(discountAmt).ToString("0.00");
                                        dr["parameter_id"] = ddl_parameter.SelectedValue;
                                        dr["category_id"] = ddl_category.SelectedValue;
                                        dr["sub_category_id"] = ddl_sub_category.SelectedValue;

                                        dr["session_id"] = ddl_session.SelectedValue;
                                        dr["Branch_id"] = ViewState["firm_id"].ToString();
                                        dr["User_id"] = ViewState["Userid"].ToString();
                                        dr["Date"] = mycode.date();
                                        dr["time"] = mycode.time();
                                        if (ddl_parameter.SelectedValue == "3")
                                        {
                                            dr["Hostel_id"] = ddl_hostel_name.SelectedValue;
                                            dr["Room_Category_id"] = ddl_room_catogery.SelectedValue;
                                        }
                                        else
                                        {
                                            dr["Hostel_id"] = "0";
                                            dr["Room_Category_id"] = "0";
                                        }

                                        dt.Rows.Add(dr);
                                        issubmit = true;
                                        ViewState["issubmit"] = "1";
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            string ss = My.Round((My.toDouble(discountAmt) * 100) / My.toDouble(txt_fee.Text), 2).ToString();
                                            string sdd = discountAmt;
                                            dr["Discount_per"] = My.Round((My.toDouble(discountAmt) * 100) / My.toDouble(txt_fee.Text), 2);
                                            dr["fee_head_id"] = lbl_content_id.Text;
                                            dr["disc_amount"] = My.toDouble(discountAmt).ToString("0.00");
                                            dr["category_id"] = ddl_category.SelectedValue;
                                            dr["sub_category_id"] = ddl_sub_category.SelectedValue;
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
                                Alertme("Please enter discount amount.", "warning");
                            }
                        }
                        #endregion
                    }
                }
            }
            else
            {
                Alertme("Please enter the amount for which you would like to provide a discount.", "warning");
            }


        }

        private void empty_form()
        {
            int i;
            int gridview_rowcount = rp_fee_details.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox txt_disc_fee = (TextBox)rp_fee_details.Items[i].FindControl("txt_disc_fee");
                txt_disc_fee.Text = "0";
            }


            int j;
            int rp_months = rp_month.Items.Count;
            for (j = 0; j < rp_months; j++)
            {
                CheckBox chkMM = (CheckBox)rp_month.Items[j].FindControl("chk_month_name");
                chkMM.Checked = false;
            }
            ddl_parameter.Enabled = true;
            ddl_class.Enabled = true;
            btn_Submit.Text = "Save";
            btn_cancel.Visible = false;
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

                    Label lbl_category_id = (Label)row.FindControl("lbl_category_id");
                    Label lbl_sub_category_id = (Label)row.FindControl("lbl_sub_category_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_fee_head_id = (Label)row.FindControl("lbl_fee_head_id");
                    Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");
                    Label lbl_mnths = (Label)row.FindControl("lbl_mnths");
                    bool chek_fee = My.find_mnth_disc_fee_collected("MonthlyFee", lbl_category_id.Text, lbl_sub_category_id.Text, ddl_session.SelectedItem.Text, lbl_class_id.Text, lbl_fee_head_id.Text, lbl_parameter_id.Text, lbl_mnths.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't delete because fee has been taken.", "warning");
                        return;
                    }

                    mycode.executequery("delete from Discount_Master where Id=" + lbl_Id.Text + "");
                    Alertme("Deletion process has been successfully done", "success");
                    if (ViewState["flag"].ToString() == "0")
                    {
                        bind_grd_view_all(ddl_session.SelectedItem.Text);
                    }
                    if (ViewState["flag"].ToString() == "1")
                    {
                        bind_grid_by_class(ddl_session.SelectedItem.Text);
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

        protected void btn_fnd_by_class_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_fnd_months.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month", "warning");
                    ddl_fnd_months.Focus();
                }
                else
                {
                    bind_grid_by_clas_month(ddl_session.SelectedItem.Text);
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_grid_by_clas_month(string session)
        {
            if (ddlclass.SelectedItem.Text == "All")
            {
                bind_grd_view("select ac.Course_Name,dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id where  dm.session='" + session + "' and dm.group_id='" + ViewState["group_id"].ToString() + "' and dm.admission_no='All' and dm.month='" + ddl_fnd_months.SelectedItem.Text + "'  and dm.parameter_id='" + ddl_discount_for_srch.SelectedValue + "' order by ac.Position asc");
            }
            else
            {
                bind_grd_view("select ac.Course_Name,dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id where  dm.session='" + session + "' and dm.group_id='" + ViewState["group_id"].ToString() + "' and dm.admission_no='All' and dm.Class_id='" + ddlclass.SelectedValue + "'  and dm.parameter_id='" + ddl_discount_for_srch.SelectedValue + "' order by ac.Position asc");
            }
        }

        private void bind_grid_by_class(string session)
        {
            if (ddlclass.SelectedItem.Text == "All")
            {
                bind_grd_view("select ac.Course_Name,dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id where  dm.session='" + session + "' and dm.group_id='" + ViewState["group_id"].ToString() + "' and dm.admission_no='All' and dm.parameter_id='" + ddl_discount_for_srch.SelectedValue + "' order by ac.Position asc");
            }
            else
            {
                bind_grd_view("select ac.Course_Name,dm.*,fmc.amount,fmc.content, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id where  dm.session='" + session + "' and dm.group_id='" + ViewState["group_id"].ToString() + "' and dm.admission_no='All' and dm.Class_id='" + ddlclass.SelectedValue + "'  and dm.parameter_id='" + ddl_discount_for_srch.SelectedValue + "' order by ac.Position asc");
            }
        }


        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_grid_by_class(ddl_session.SelectedItem.Text);
            ViewState["flag"] = "1";
        }

        protected void ddl_discount_for_srch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_grd_view_all(ddl_session.SelectedItem.Text);
        }

        protected void ddl_parameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_parameter.SelectedValue == "3")
            {
                hostel_name.Visible = true;
                hostel_room_catogery.Visible = true;
            }
            else
            {
                hostel_name.Visible = false;
                hostel_room_catogery.Visible = false;
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

        protected void rd_viewaddedfee_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                string value1 = ((Label)e.Item.FindControl("lbl_amount")).Text;

                decimal value;
                if (decimal.TryParse(value1, out value))
                {
                    ((Label)e.Item.FindControl("lbl_amount")).Text = value.ToString("0.00");
                }

                string value2 = ((Label)e.Item.FindControl("lbl_disc_amount")).Text;


                if (decimal.TryParse(value2, out value))
                {
                    ((Label)e.Item.FindControl("lbl_disc_amount")).Text = value.ToString("0.00");
                }


                string value3 = ((Label)e.Item.FindControl("lbl_after_disc")).Text;


                if (decimal.TryParse(value3, out value))
                {
                    ((Label)e.Item.FindControl("lbl_after_disc")).Text = value.ToString("0.00");
                }
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query1 = "";
                string dd = ddl_session.SelectedValue;
                if (ddl_parameter.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Discount For.", "warning");
                    ddl_parameter.Focus();
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddl_class.Focus();
                }
                else
                {
                    string startmonthname = My.get_start_month();
                    if (ddl_parameter.SelectedValue == "3")
                    {
                        if (ddl_hostel_name.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select hostel.", "warning");
                            ddl_hostel_name.Focus();
                        }
                        else if (ddl_room_catogery.SelectedValue == "Select")
                        {
                            Alertme("Please select room category", "warning");
                            ddl_hostel_name.Focus();
                        }
                        else
                        {
                            query1 = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' disccount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id join Add_course_table act on fmc.class_id=act.course_id where fmc.class_id='" + ddl_class.SelectedValue + "' and fmc.session_id='" + ddl_session.SelectedValue + "' and fmc.parameter_id='" + ddl_parameter.SelectedValue + "' and fmc.Month='" + startmonthname + "' and fmc.Hostel_Id=" + ddl_hostel_name.SelectedValue + " and fmc.Room_Category_id=" + ddl_room_catogery.SelectedValue + " order by act.Position asc";
                            //query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' disccount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id join Add_course_table act on fmc.class_id=act.course_id where fmc.class_id='" + ddl_class.SelectedValue + "' and fmc.session_id='" + ddl_session.SelectedValue + "' and fmc.parameter_id='" + ddl_parameter.SelectedValue + "' and fmc.Hostel_Id=" + ddl_hostel_name.SelectedValue + " and fmc.Room_Category_id=" + ddl_room_catogery.SelectedValue + ""; 
                        }
                    }
                    else
                    {
                        query1 = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' disccount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id join Add_course_table act on fmc.class_id=act.course_id where fmc.class_id='" + ddl_class.SelectedValue + "' and fmc.session_id='" + ddl_session.SelectedValue + "' and fmc.parameter_id='" + ddl_parameter.SelectedValue + "' and fmc.Month='" + startmonthname + "' order by act.Position asc";
                        //query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' disccount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id join Add_course_table act on fmc.class_id=act.course_id where fmc.class_id='" + ddl_class.SelectedValue + "' and fmc.session_id='" + ddl_session.SelectedValue + "' and fmc.parameter_id='" + ddl_parameter.SelectedValue + "' ";
                    }

                    DataTable dt1 = mycode.FillData(query1);
                    if (dt1.Rows.Count == 0)
                    {
                        rp_fee_details.DataSource = null;
                        rp_fee_details.DataBind();
                        pnl_fee_grid.Visible = false;
                    }
                    else
                    {
                        rp_fee_details.DataSource = dt1;
                        rp_fee_details.DataBind();
                        pnl_fee_grid.Visible = true;
                        //bindttl_fee();
                    }
                }
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
        }
    }
}