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
    public partial class Hostel_student_wise_discount : System.Web.UI.Page
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
                        ViewState["Admission_no"] = "";
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





                        mycode.bind_all_ddl_with_id(ddlsessionad, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");

                        ddlsessionad.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_session_student, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");

                        ddl_session_student.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_hostel_ad, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        ddl_hostel_ad.SelectedValue = My.get_top_one_hostel_name();

                        mycode.bind_all_ddl_with_id(ddl_hostel_month, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        ddl_hostel_month.SelectedValue = My.get_top_one_hostel_name();

                        mycode.bind_all_ddl_with_id(ddl_room_catogery_month, "select Category_name,Category_id from Hostel_room_category_master    order by Category_name asc");

                        //mycode.bind_all_ddl_with_id(ddl_discount_on_srch, "select Group_name,Group_id from Hostel_discount_group where Status=1    order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_discount_on, "select Group_name,Group_id from Hostel_discount_group where Status=1 and Group_id=4 order by Position asc");

                        mycode.bind_all_ddl_with_id(ddl_discount_on_srch, "select Group_name,Group_id from Hostel_discount_group where Status=1 and Group_id=4 order by Position asc");

                        ddl_discount_on_srch.SelectedValue = "4";
                        ddl_discount_on.SelectedValue = "4";
                        bind_month();
                        bind_grid_by_admission_or_annul_discount_va_admission_no();




                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hostel_Admission_Fee_or_Annual_Master_Discount");
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
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_discount_month.Visible = false;
                pnl_discount_annul_or_admission_fee.Visible = false;
                Panel1.Visible = false;
                pnl_fee_grid.Visible = false;
                string query = "";

                query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' and hosteltaken='Yes'  order by id asc";




                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
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
        private void find_details(string query)
        {

            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                std_basic_infoS.Visible = false;
                Alertme(" This student has  not  mapping with the hostel", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    std_basic_infoS.Visible = true;
                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();
                    ddlsessionad.SelectedValue = dr["Session_id"].ToString();
                    txt_admission_no.Text = ViewState["Admission_no"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    lbl_roll_no.Text = dr["rollnumber"].ToString();
                    lbl_section.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();

                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";

                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";


                    ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();

                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());

                    try
                    {
                        ddl_hostel_ad.SelectedValue = ViewState["hostel_id"].ToString();
                        ddl_hostel_month.SelectedValue = ViewState["hostel_id"].ToString();
                    }
                    catch
                    {

                    }
                    // confussion 
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);


                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        lbl_student_type.Text = "New";



                        mycode.bind_all_ddl_with_id(ddl_discount_on, "select Group_name,Group_id from Hostel_discount_group where Status=1 and Group_id=4 order by Position asc");

                    }
                    else
                    {
                        lbl_student_type.Text = "Old";

                        mycode.bind_all_ddl_with_id(ddl_discount_on, "select Group_name,Group_id from Hostel_discount_group where Status=1 and Group_id=4 order by Position asc");

                    }
                }
            }
        }


        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                Panel1.Visible = false;
                pnl_fee_grid.Visible = false;
                pnl_discount_month.Visible = false;
                pnl_discount_annul_or_admission_fee.Visible = false;
                if (ddl_session_student.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_student.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }
                string query = "";

                query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' and  Status='1' order by id asc";



                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                    myModal2.Visible = false;
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    myModal2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");

                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' order by id asc";

                find_details(query);
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }




        protected void ddl_discount_on_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_discount_month.Visible = false;
            pnl_discount_annul_or_admission_fee.Visible = false;
            pnl_servicesmatser.Visible = false;
            if (ddl_discount_on.SelectedItem.Text == "Select")
            {
                pnl_discount_annul_or_admission_fee.Visible = false;
            }
            else if (ddl_discount_on.SelectedValue == "1")// admission fee
            {
                ViewState["group_id"] = "1";
                pnl_discount_annul_or_admission_fee.Visible = true;
                Bind_fee_for_admission_or_annul_fee_head();
            }
            else if (ddl_discount_on.SelectedValue == "2")// annul fee
            {
                ViewState["group_id"] = "2";
                pnl_discount_annul_or_admission_fee.Visible = true;
                Bind_fee_for_admission_or_annul_fee_head();

            }

            else if (ddl_discount_on.SelectedValue == "3")// Month fee
            {
                pnl_discount_month.Visible = true; ;
                pnl_discount_annul_or_admission_fee.Visible = false;
                ViewState["group_id"] = "3";
            }
            else if (ddl_discount_on.SelectedValue == "4")// Month fee
            {
                chk_all_month_servicesmaster.Checked = false;
                pnl_discount_month.Visible = false;
                pnl_discount_annul_or_admission_fee.Visible = false;
                pnl_servicesmatser.Visible = true;
                ViewState["group_id"] = "4";
                ddl_discount_on_srch.SelectedValue = ddl_discount_on.SelectedValue;
                Bind_fee_head_other_services();


            }


        }



        protected void ddl_hostel_ad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_fee_for_admission_or_annul_fee_head();
        }
        private void Bind_fee_for_admission_or_annul_fee_head()
        {
            try
            {
                if (ddl_hostel_ad.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel", "warning");
                }
                else
                {

                    string query1 = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' disccount from dbo.[Hostel_Fee_master_content_wise] fmc join Hostel_fee_head_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["classid"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and cm.group_id='" + ddl_discount_on.SelectedValue + "' and fmc.Hostel_id=" + ddl_hostel_ad.SelectedValue + "";
                    DataTable dt1 = mycode.FillData(query1);
                    if (dt1.Rows.Count == 0)
                    {
                        grd_fee.DataSource = null;
                        grd_fee.DataBind();
                        pnl_fee_grid.Visible = false;
                    }
                    else
                    {
                        grd_fee.DataSource = dt1;
                        grd_fee.DataBind();
                        pnl_fee_grid.Visible = true;
                        bindttl_fee();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #region discount admission annual fee
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

            var footerRow = grd_fee.FooterRow;
            Label lbl_full_amount = (footerRow.FindControl("lbl_full_amount")) as Label;
            lbl_full_amount.Text = lbl_totalmrp.Text;
            Label lbl_full_discount = (footerRow.FindControl("lbl_full_discount")) as Label;
            lbl_full_discount.Text = lbl_ttl_disc.Text;
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
                bindttl_fee();

            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_hostel_ad.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel", "warning");
            }

            else
            {



                if (ViewState["Is_add"].ToString() == "1")
                {
                    ViewState["issubmit"] = "0";
                    save_data_annual_admission();
                    if (ViewState["issubmit"].ToString() == "1")
                    {

                        bind_grid_by_admission_or_annul_discount_va_admission_no();
                        Session["msg"] = ddl_discount_on.SelectedItem.Text + " Discount Saved successfully.";
                        Alertme(Session["msg"].ToString(), "success");


                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    ViewState["issubmit"] = "0";
                    save_data_annual_admission();
                    if (ViewState["issubmit"].ToString() == "1")
                    {

                        bind_grid_by_admission_or_annul_discount_va_admission_no();
                        Session["msg"] = ddl_discount_on.SelectedItem.Text + " Discount Saved successfully.";

                        Alertme(Session["msg"].ToString(), "success");
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void save_data_annual_admission()
        {
            string parameter_New = "";
            string parameter_id = "0";
            if (ddl_discount_on.SelectedValue == "1") //Hostel Admission Fee
            {
                ViewState["discount_on"] = "Admission";
                parameter_New = "HostelAdmissionFee";//5
                parameter_id = "5";
            }
            else
            {
                ViewState["discount_on"] = "Annual";
                parameter_New = "HostelAnnualFee";//6
                parameter_id = "6";
            }

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
                            bool chek_fee = My.find_disc_fee_collected_hostel_admission(ViewState["category_id"].ToString(), ViewState["sub_category_id"].ToString(), ViewState["session"].ToString(), ViewState["classid"].ToString(),
lbl_content_id.Text, parameter_id, ddl_hostel_ad.SelectedValue, ViewState["Admission_no"].ToString());
                            if (chek_fee == false)
                            {
                                Alertme("You can't update because fee has been taken.", "warning");
                            }
                            else
                            {
                                SqlDataAdapter ad = new SqlDataAdapter("select * from Hostel_Discount_master where class_id=" + ViewState["classid"].ToString() + " and session_id='" + ViewState["sessionIDs"].ToString() + "' and group_id='" + ddl_discount_on.SelectedValue + "' and month='NA' and admission_no='" + ViewState["Admission_no"].ToString() + "' and content_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "' and Hostel_id=" + ddl_hostel_ad.SelectedValue + "", My.conn);


                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Hostel_Discount_master");
                                DataTable dt = ds.Tables[0];
                                if (dt.Rows.Count == 0)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["class_id"] = ViewState["classid"].ToString();
                                    dr["Discount_on"] = ViewState["discount_on"].ToString();
                                    dr["session"] = ViewState["session"].ToString();
                                    dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                    dr["group_id"] = ddl_discount_on.SelectedValue;
                                    dr["admission_no"] = ViewState["Admission_no"].ToString();
                                    dr["month"] = "NA";
                                    dr["content_id"] = lbl_content_id.Text;
                                    dr["content"] = lbl_content.Text;

                                    dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                    dr["parameter"] = parameter_New;
                                    dr["parameter_id"] = parameter_id;
                                    dr["category_id"] = ViewState["category_id"].ToString();
                                    dr["sub_category_id"] = ViewState["sub_category_id"].ToString();

                                    dr["session_id"] = ViewState["sessionIDs"].ToString();
                                    dr["Branch_id"] = ViewState["branchid"].ToString();
                                    dr["User_id"] = ViewState["Userid"].ToString();
                                    dr["Date"] = mycode.date();
                                    dr["time"] = mycode.time();
                                    dr["Hostel_id"] = ddl_hostel_ad.SelectedValue;
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
                                        dr["category_id"] = ViewState["category_id"].ToString();
                                        dr["sub_category_id"] = ViewState["sub_category_id"].ToString();
                                        dr["Hostel_id"] = ddl_hostel_ad.SelectedValue;
                                        dr["content"] = lbl_content.Text;
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
        protected void btn_fnd_by_cat_Click(object sender, EventArgs e)
        {
            ViewState["Admission_no"] = "";
            bind_grid_by_admission_or_annul_discount_va_admission_no();
        }
        private void bind_grid_by_admission_or_annul_discount_va_admission_no()
        {
            string query = "";
            if (ddl_discount_on_srch.SelectedValue == "4")
            {
                if (ViewState["Admission_no"].ToString() != "")
                {
                    query = " select dm.*,fmc.Service_amount as amount,fmc.Service_id as content,( Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id ) as Hostel_name,(Select top 1 studentname from admission_registor where admissionserialnumber=dm.admission_no) as studentname, (cast(Service_amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_service_master fmc on dm.content_id=fmc.Service_id    where  dm.session_id='" + ddlsessionad.SelectedValue + "' and dm.group_id='" + ddl_discount_on_srch.SelectedValue + "' and dm.admission_no='" + txt_admission_no.Text + "'   ";
                }

                else
                {
                    query = " select dm.*,fmc.Service_amount as amount,fmc.Service_id as content,( Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id ) as Hostel_name,(Select top 1 studentname from admission_registor where admissionserialnumber=dm.admission_no) as studentname, (cast(Service_amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_service_master fmc on dm.content_id=fmc.Service_id    where  dm.session_id='" + ddlsessionad.SelectedValue + "' and dm.group_id='" + ddl_discount_on_srch.SelectedValue + "' and dm.admission_no!='ALL'   ";
                }
            }
            else
            {
                if (ViewState["Admission_no"].ToString() != "")
                {
                    query = "select dm.*,fmc.amount,fmc.content,( Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id ) as Hostel_name,(Select top 1 studentname from admission_registor where admissionserialnumber=dm.admission_no) as studentname, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id    and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id    where  dm.session_id='" + ddlsessionad.SelectedValue + "' and dm.group_id='" + ddl_discount_on_srch.SelectedValue + "' and dm.admission_no='" + txt_admission_no.Text + "'   ";

                }
                else
                {
                    query = "select dm.*,fmc.amount,fmc.content,( Select top 1 Hostel_name from Hostels_master where Hostel_id=dm.Hostel_id ) as Hostel_name,(Select top 1 studentname from admission_registor where admissionserialnumber=dm.admission_no) as studentname, (cast(amount as float)-cast(disc_amount as float)) after_disc,( select Category_Name from dbo.[Category_Details] cd where cd.Category_Id=dm.category_id) category,(select Sub_CategoryName from dbo.[Sub_Category_Details] scd where scd.Sub_CategoryId=dm.sub_category_id) sub_category from dbo.[Hostel_Discount_master] dm  join Hostel_Fee_master_content_wise fmc on dm.content_id=fmc.content_id   and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.Hostel_id=fmc.Hostel_id    where  dm.session_id='" + ddlsessionad.SelectedValue + "' and dm.group_id='" + ddl_discount_on_srch.SelectedValue + "' and dm.admission_no!='ALL'   ";
                }
            }

            bind_grd_view(query);
        }

        private void bind_grd_view(string query)
        {
            print1.Visible = false;
            btn_excels.Visible = false;
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {

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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_session = (Label)row.FindControl("lbl_session");

                    Label lbl_fee_head_id = (Label)row.FindControl("lbl_fee_head_id");

                    Label lbl_Hostel_id = (Label)row.FindControl("lbl_Hostel_id");
                    Label lbl_parameter = (Label)row.FindControl("lbl_parameter");
                    Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                    bool chek_fee = My.find_disc_fee_collected_hostel_delete(lbl_session.Text, lbl_fee_head_id.Text, lbl_Hostel_id.Text, lbl_admissionserialnumber.Text, lbl_parameter.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't delete because fee has been taken.", "warning");
                        return;
                    }
                    mycode.executequery("delete from Hostel_Discount_master where Id=" + lbl_Id.Text + "");
                    Alertme("Deletion process has been successfully done", "success");
                    bind_grid_by_admission_or_annul_discount_va_admission_no();
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





        #endregion


        #region month discount
        protected void ddl_hostel_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_room_catogery_month, "select Category_name,Category_id from Hostel_room_category_master    order by Category_name asc");
        }
        protected void ddl_room_catogery_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                chk_all_month.Checked = false;
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
            bind_month();


            if (ddl_hostel_month.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel", "warning");
            }
            else if (ddl_room_catogery_month.SelectedItem.Text == "Select")
            {
                Alertme("Please select romm catogery", "warning");
            }
            else
            {
                string query1 = "select distinct fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' disccount from dbo.[Hostel_Fee_master_content_wise] fmc join Hostel_fee_head_master cm on fmc.content_id=cm.content_id  where     fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and cm.group_id='3' and fmc.Hostel_id=" + ddl_hostel_month.SelectedValue + "  and fmc.Room_Category_id='" + ddl_room_catogery_month.SelectedValue + "'";
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    btn_submit_data_month.Visible = false;

                    grd_fee_month.DataSource = null;
                    grd_fee_month.DataBind();
                    Panel1.Visible = false;
                }
                else
                {
                    btn_submit_data_month.Visible = true;
                    grd_fee_month.DataSource = dt1;
                    grd_fee_month.DataBind();
                    Panel1.Visible = true;
                    bindttl_month_fee();
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

                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_months.DataSource = dt;
                rp_months.DataBind();

                rp_month.DataSource = dt;
                rp_month.DataBind();

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

        protected void txt_disc_fee_month_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox btnc = (TextBox)sender;
                GridViewRow row = (GridViewRow)btnc.Parent.Parent;

                string txt_fee = ((TextBox)row.FindControl("txt_fee")).Text;
                string txt_disc_fee = ((TextBox)row.FindControl("txt_disc_fee_month")).Text;
                if (My.toDouble(txt_disc_fee) <= My.toDouble(txt_fee))
                {
                }
                else
                {
                    ((TextBox)row.FindControl("txt_disc_fee")).Text = "0";
                    Alertme("Please enter discount less than fees amount.", "warning");
                }




                //find_room_catogery_data();
                bindttl_month_fee();

            }
            catch (Exception ex)
            {
            }
        }
        private void bindttl_month_fee()
        {
            int i;
            double totalrate = 0; double totaldisc = 0;
            int gridview_rowcount = grd_fee_month.Rows.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox lbl_net_fee = (TextBox)grd_fee_month.Rows[i].FindControl("txt_fee");
                TextBox txt_disc_fee = (TextBox)grd_fee_month.Rows[i].FindControl("txt_disc_fee_month");
                if (lbl_net_fee.Text != "")
                {
                    totalrate = totalrate + Convert.ToDouble(lbl_net_fee.Text);
                }
                if (txt_disc_fee.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(txt_disc_fee.Text);
                }
            }
            lbl_totalmrp_month.Text = totalrate.ToString("0.00");
            lbl_ttl_disc_month.Text = totaldisc.ToString("0.00");



            // footer data bound
            var footerRow = grd_fee_month.FooterRow;
            Label lbl_full_amount = (footerRow.FindControl("lbl_full_amount")) as Label;
            lbl_full_amount.Text = lbl_totalmrp_month.Text;
            Label lbl_full_discount = (footerRow.FindControl("lbl_full_discount")) as Label;
            lbl_full_discount.Text = lbl_ttl_disc_month.Text;


        }
        double totalamount_month = 0, todatl_discount_month = 0;
        protected void grd_fee_month_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_content_id = (Label)e.Row.FindControl("lbl_content_id");
                TextBox txt_fee = (TextBox)e.Row.FindControl("txt_fee");
                Label lbl_totalmrp = (Label)e.Row.FindControl("lbl_totalmrp");

                TextBox txt_disc_fee = (TextBox)e.Row.FindControl("txt_disc_fee_month");

                totalamount_month = totalamount_month + My.toDouble(txt_fee.Text);

                todatl_discount_month = todatl_discount_month + My.toDouble(txt_disc_fee.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_footr_totalCollection = (Label)e.Row.FindControl("lbl_full_amount");
                Label lbl_full_discount = (Label)e.Row.FindControl("lbl_full_discount");

                lbl_footr_totalCollection.Text = totalamount_month.ToString("0.00");
                lbl_full_discount.Text = todatl_discount_month.ToString("0.00");
            }

        }



        protected void btn_submit_data_month_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_hostel_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel", "warning");
                }
                else if (ddl_room_catogery_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select room category", "warning");
                }
                else
                {
                    ddl_discount_on_srch.SelectedValue = ddl_discount_on.SelectedValue;
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        ViewState["issubmit"] = "0";
                        save_data_month();
                        if (ViewState["issubmit"].ToString() == "1")
                        {

                            bind_grid_by_admission_or_annul_discount_va_admission_no();
                            Session["msg"] = ddl_discount_on.SelectedItem.Text + " Discount Saved successfully.";
                            Alertme(Session["msg"].ToString(), "success");

                            btn_Submit.Visible = false;
                            grd_fee.DataSource = null;
                            grd_fee.DataBind();
                            pnl_fee_grid.Visible = false;
                            bind_month();
                            find_room_catogery_data();
                            chk_all_month.Checked = false;
                        }
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        ViewState["issubmit"] = "0";
                        save_data_month();
                        if (ViewState["issubmit"].ToString() == "1")
                        {

                            bind_grid_by_admission_or_annul_discount_va_admission_no();


                            Session["msg"] = ddl_discount_on.SelectedItem.Text + " Discount Saved successfully.";
                            Alertme(Session["msg"].ToString(), "success");

                            btn_Submit.Visible = false;
                            grd_fee.DataSource = null;
                            grd_fee.DataBind();
                            pnl_fee_grid.Visible = false;
                            bind_month();
                            find_room_catogery_data();
                            chk_all_month.Checked = false;
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
            }
            catch
            {

            }

        }

        private void save_data_month()
        {
            string parameter_New = "";
            string parameter_id = "0";



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
                    for (int i = 0; i < grd_fee_month.Rows.Count; i++)
                    {
                        TextBox lbl_reg_id = (TextBox)grd_fee_month.Rows[i].FindControl("txt_disc_fee_month");
                        if (mycode.chkenum(lbl_reg_id.Text) == true)
                        {
                            isamountfill = true;
                        }
                    }
                    if (isamountfill == true)
                    {
                        double totla = 0;
                        for (int i = 0; i < grd_fee_month.Rows.Count; i++)
                        {
                            TextBox txt_fee = (TextBox)grd_fee_month.Rows[i].FindControl("txt_fee");
                            TextBox txt_disc_fee = (TextBox)grd_fee_month.Rows[i].FindControl("txt_disc_fee_month");
                            Label lbl_content_id = (Label)grd_fee_month.Rows[i].FindControl("lbl_content_id");
                            Label lbl_content = (Label)grd_fee_month.Rows[i].FindControl("lbl_content");
                            totla = totla + My.toDouble(txt_fee.Text);
                            //==============*************** 


                            if (My.toDouble(txt_disc_fee.Text) > 0)
                            {
                                if (My.toDouble(txt_disc_fee.Text) <= My.toDouble(txt_fee.Text))
                                {
                                    bool chek_fee = My.find_disc_fee_collected_hostel_delete(ViewState["session"].ToString(), lbl_content_id.Text, ddl_hostel_month.Text, ViewState["Admission_no"].ToString(), parameter_New);
                                    if (chek_fee == false)
                                    {
                                        Alertme("You can't update because fee has been taken.", "warning");
                                    }
                                    else
                                    {
                                        SqlDataAdapter ad = new SqlDataAdapter("select * from Hostel_Discount_master where  session_id='" + ViewState["sessionIDs"].ToString() + "' and group_id='3' and month='" + lbl_month_name.Text + "' and admission_no='" + ViewState["Admission_no"].ToString() + "' and content_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "' and Month='" + lbl_month_name.Text + "' and Room_Category_id=" + ddl_room_catogery_month.SelectedValue + " and   Hostel_id=" + ddl_hostel_month.SelectedValue + "", My.conn);
                                        DataSet ds = new DataSet();
                                        ad.Fill(ds, "Hostel_Discount_master");
                                        DataTable dt = ds.Tables[0];
                                        if (dt.Rows.Count == 0)
                                        {
                                            DataRow dr = dt.NewRow();

                                            dr["Discount_on"] = ddl_discount_on.SelectedItem.Text;
                                            dr["session"] = ViewState["session"].ToString();
                                            dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                            dr["group_id"] = "3";
                                            dr["admission_no"] = ViewState["Admission_no"].ToString();
                                            dr["month"] = lbl_month_name.Text;
                                            dr["Month_id"] = lbl_month_id.Text;
                                            dr["content_id"] = lbl_content_id.Text;
                                            dr["content"] = lbl_content.Text;

                                            dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                            dr["parameter"] = parameter_New;
                                            dr["parameter_id"] = parameter_id;
                                            dr["category_id"] = ViewState["category_id"].ToString();
                                            dr["sub_category_id"] = ViewState["sub_category_id"].ToString();

                                            dr["session_id"] = ViewState["sessionIDs"].ToString();
                                            dr["Branch_id"] = ViewState["branchid"].ToString();
                                            dr["User_id"] = ViewState["Userid"].ToString();
                                            dr["Date"] = mycode.date();
                                            dr["time"] = mycode.time();
                                            dr["Hostel_id"] = ddl_hostel_month.SelectedValue;
                                            dr["Room_Category_id"] = ddl_room_catogery_month.SelectedValue;
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
                                                dr["category_id"] = ViewState["category_id"].ToString();
                                                dr["sub_category_id"] = ViewState["sub_category_id"].ToString();

                                                dr["Hostel_id"] = ddl_hostel_month.SelectedValue;
                                                dr["content"] = lbl_content.Text;
                                                dr["Room_Category_id"] = ddl_room_catogery_month.SelectedValue;
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

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export_Student_Discount_Hostel.xls");
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
        #endregion
        protected void txt_disc_fee_other_services_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox lnk = (TextBox)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                TextBox txt_disc_fee = (TextBox)row.FindControl("txt_disc_fee_other_services");

                TextBox txt_fee = (TextBox)row.FindControl("txt_fee");
                if (My.toDouble(txt_disc_fee) <= My.toDouble(txt_fee))
                {
                    bindttl_other_services_fee();
                }
                else
                {
                    ((TextBox)row.FindControl("txt_disc_fee_other_services")).Text = "0";
                    Alertme("Please enter discount less than fees amount.", "warning");
                }

            }
            catch (Exception ex)
            {
            }
        }

        #region otherservicess

        protected void chk_all_month_servicesmaster_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_month.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_month.Items[j].FindControl("chk_month_name") as CheckBox;
                if (chk_all_month_servicesmaster.Checked)
                {
                    chk_month_name.Checked = true;
                }
                else
                {
                    chk_month_name.Checked = false;
                }
            }
        }
        private void Bind_fee_head_other_services()
        {
            string query = "select Service_name as Content_name,Service_id as Content_id,Service_amount as Fees_amount,isnull((select top 1 disc_amount from Hostel_Discount_Master where group_id='4' and Content_id=Hostel_service_master.Service_id),'0') as Discount_amt from Hostel_service_master  order by Service_name asc";
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();

            }
            else
            {
                rd_view.DataSource = dt1;
                rd_view.DataBind();

                bindttl_other_services_fee();
            }

        }



        private void bindttl_other_services_fee()
        {
            int i;
            double totalrate = 0; double totaldisc = 0;
            int gridview_rowcount = rd_view.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox lbl_net_fee = (TextBox)rd_view.Items[i].FindControl("txt_fee");
                TextBox txt_disc_fee = (TextBox)rd_view.Items[i].FindControl("txt_disc_fee_other_services");
                if (lbl_net_fee.Text != "")
                {
                    totalrate = totalrate + Convert.ToDouble(lbl_net_fee.Text);
                }
                if (txt_disc_fee.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(txt_disc_fee.Text);
                }
            }
            lbl_totalmrp_other.Text = totalrate.ToString("0.00");
            lbl_ttl_disc_other.Text = totaldisc.ToString("0.00");
        }

        protected void btn_submit_services_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    ViewState["issubmit"] = "0";
                    save_data_Services();
                    if (ViewState["issubmit"].ToString() == "1")
                    {
                        Bind_fee_head_other_services();
                        bindttl_other_services_fee();
                        bind_month();
                        chk_all_month.Checked = false;
                        Alertme("Services Discount saved successfully.", "success");
                        ddl_discount_on_srch.SelectedValue = ddl_discount_on.SelectedValue;
                        bind_grid_by_admission_or_annul_discount_va_admission_no();
                    }
                }
                else if (ViewState["Is_add"].ToString() == "1")
                {
                    ViewState["issubmit"] = "0";
                    save_data_Services();
                    if (ViewState["issubmit"].ToString() == "1")
                    {
                        Bind_fee_head_other_services();
                        bindttl_other_services_fee();
                        bind_month();
                        chk_all_month.Checked = false;
                        Alertme("Services Discount saved successfully.", "success");
                        ddl_discount_on_srch.SelectedValue = ddl_discount_on.SelectedValue;
                        bind_grid_by_admission_or_annul_discount_va_admission_no();
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

        private void save_data_Services()
        {
            ViewState["discount_on"] = "Services Charge";
            string parameter_New = "ServicesCharge";//555// other services charges
            string parameter_id = "555";

            int mgrowcount = rp_month.Items.Count;
            int kl = 0;
            for (int ixi = 0; ixi < mgrowcount; ixi++)
            {
                CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                if (chkM.Checked == true)
                {
                    Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                    Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");

                    #region FFFF
                    bool issubmit = false;
                    bool isamountfill = false;
                    for (int i = 0; i < rd_view.Items.Count; i++)
                    {
                        TextBox lbl_reg_id = (TextBox)rd_view.Items[i].FindControl("txt_disc_fee_other_services");
                        if (mycode.chkenum(lbl_reg_id.Text) == true)
                        {
                            isamountfill = true;
                        }
                    }
                    if (isamountfill == true)
                    {
                        double totla = 0;
                        for (int i = 0; i < rd_view.Items.Count; i++)
                        {
                            TextBox txt_fee = (TextBox)rd_view.Items[i].FindControl("txt_fee");
                            TextBox txt_disc_fee = (TextBox)rd_view.Items[i].FindControl("txt_disc_fee_other_services");
                            Label lbl_content_id = (Label)rd_view.Items[i].FindControl("lbl_content_id");
                            Label lbl_content = (Label)rd_view.Items[i].FindControl("lbl_content");
                            totla = totla + My.toDouble(txt_fee.Text);
                            //==============*************** 


                            if (My.toDouble(txt_disc_fee.Text) > 0)
                            {
                                if (My.toDouble(txt_disc_fee.Text) <= My.toDouble(txt_fee.Text))
                                {
                                    bool chek_fee = My.find_disc_fee_collected_hostel_delete(ViewState["session"].ToString(), lbl_content_id.Text, ddl_hostel_month.Text, ViewState["Admission_no"].ToString(), "");
                                    if (chek_fee == false)
                                    {
                                        Alertme("You can't update because fee has been taken.", "warning");
                                    }
                                    else
                                    {
                                        SqlDataAdapter ad = new SqlDataAdapter("select * from Hostel_Discount_master where  session_id='" + ViewState["sessionIDs"].ToString() + "' and group_id='3' and month='" + lbl_month_name.Text + "' and admission_no='" + ViewState["Admission_no"].ToString() + "' and content_id='" + lbl_content_id.Text + "' and parameter_id='" + parameter_id + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "' and Month='" + lbl_month_name.Text + "' and Room_Category_id=" + ddl_room_catogery_month.SelectedValue + " and   Hostel_id=" + ddl_hostel_month.SelectedValue + "", My.conn);
                                        DataSet ds = new DataSet();
                                        ad.Fill(ds, "Hostel_Discount_master");
                                        DataTable dt = ds.Tables[0];
                                        if (dt.Rows.Count == 0)
                                        {
                                            DataRow dr = dt.NewRow();

                                            dr["Discount_on"] = ddl_discount_on.SelectedItem.Text;
                                            dr["session"] = ViewState["session"].ToString();
                                            dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                            dr["group_id"] = "4";
                                            dr["admission_no"] = ViewState["Admission_no"].ToString();
                                            dr["month"] = lbl_month_name.Text;
                                            dr["Month_id"] = lbl_month_id.Text;
                                            dr["content_id"] = lbl_content_id.Text;
                                            dr["content"] = lbl_content.Text;

                                            dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                            dr["parameter"] = parameter_New;
                                            dr["parameter_id"] = parameter_id;
                                            dr["category_id"] = ViewState["category_id"].ToString();
                                            dr["sub_category_id"] = ViewState["sub_category_id"].ToString();

                                            dr["session_id"] = ViewState["sessionIDs"].ToString();
                                            dr["Branch_id"] = ViewState["branchid"].ToString();
                                            dr["User_id"] = ViewState["Userid"].ToString();
                                            dr["Date"] = mycode.date();
                                            dr["time"] = mycode.time();
                                            dr["Hostel_id"] = ddl_hostel_month.SelectedValue;

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
                                                dr["category_id"] = ViewState["category_id"].ToString();
                                                dr["sub_category_id"] = ViewState["sub_category_id"].ToString();

                                                dr["Hostel_id"] = ddl_hostel_month.SelectedValue;
                                                dr["content"] = lbl_content.Text;

                                            }
                                            issubmit = true;
                                            ViewState["issubmit"] = "1";
                                        }
                                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                        ad.Update(dt);
                                    }





                                    //SqlDataAdapter ad = new SqlDataAdapter("select * from Hostel_Discount_Master where group_id='" + ViewState["group_id"].ToString() + "' and month='" + lbl_month_name.Text + "' and Class_id=" + ViewState["classid"].ToString() + " and session='" + ddlsession.SelectedItem.Text + "' and  admission_no='" + txt_admission_no.Text + "'  and fee_head_id='" + lbl_content_id.Text + "' and Acamedic_Semester_Id='" + ViewState["Academic_Sem_id"].ToString() + "'", My.conn);
                                    //DataSet ds = new DataSet();
                                    //ad.Fill(ds, "Discount_Master");
                                    //DataTable dt = ds.Tables[0];
                                    //if (dt.Rows.Count == 0)
                                    //{
                                    //    DataRow dr = dt.NewRow();
                                    //    dr["Class_id"] = ViewState["classid"].ToString();
                                    //    dr["Discount_on"] = ViewState["discount_on"].ToString();
                                    //    dr["session"] = ddlsession.SelectedItem.Text;
                                    //    dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                    //    dr["group_id"] = My.toint(ViewState["group_id"].ToString());
                                    //    dr["admission_no"] = ViewState["Addmission_no"].ToString();
                                    //    dr["month"] = lbl_month_name.Text;
                                    //    dr["Month_id"] = lbl_month_id.Text;
                                    //    dr["fee_head_id"] = lbl_content_id.Text;
                                    //    dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                    //    dr["parameter_id"] = "0";
                                    //    dr["discount_for"] = "ALL";
                                    //    dr["Acamedic_Semester_Id"] = ViewState["Academic_Sem_id"].ToString();
                                    //    dr["session_id"] = ViewState["sessionIDs"].ToString();
                                    //    dr["Branch_id"] = ViewState["firm_id"].ToString();
                                    //    dr["User_id"] = ViewState["Userid"].ToString();
                                    //    dr["Date"] = mycode.date();
                                    //    dr["time"] = mycode.time();
                                    //    dt.Rows.Add(dr);
                                    //    issubmit = true;
                                    //    ViewState["issubmit"] = "1";
                                    //}
                                    //else
                                    //{
                                    //    foreach (DataRow dr in dt.Rows)
                                    //    {
                                    //        dr["Discount_per"] = My.Round((My.toDouble(txt_disc_fee.Text) * 100) / My.toDouble(txt_fee.Text), 2);
                                    //        dr["fee_head_id"] = lbl_content_id.Text;
                                    //        dr["disc_amount"] = My.toDouble(txt_disc_fee.Text).ToString("0.00");
                                    //    }
                                    //    issubmit = true;
                                    //    ViewState["issubmit"] = "1";
                                    //}
                                    //SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                    //ad.Update(dt);
                                }
                                else
                                {
                                    Alertme("Discount amount will be not greater than fees amount.", "warning");
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
        #endregion
    }



}
