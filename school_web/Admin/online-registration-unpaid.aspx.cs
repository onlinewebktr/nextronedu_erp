using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class online_registration_unpaid : System.Web.UI.Page
    {
        string testname = "Select top 1 Test_name from Online_reg_exam_test_master where Test_id=Online_Admission.Test_id";
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "online-registration.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        txt_transaction_no.Text = "NA";
                        find_firm_details();

                        ViewState["flag"] = "0";
                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();

                        bind_session();
                        bind_class();

                        //bind_all_data();
                        find_by_c_s_a();

                        bind_data();

                        if (Session["msG"] != null)
                        {
                            Alertme(Session["msG"].ToString(), "success");
                            Session["msG"] = null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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
                try
                {
                    if (dt.Rows[0]["QR_Code_path"].ToString() == "#")
                    {
                        ViewState["QRadd"] = "No"; 
                    }
                    else
                    {
                        ViewState["QRadd"] = "Yes";
                        ddl_payment_mode.Text = "Online";
                        pnl_mode_t_n_dv.Visible = true;

                    }
                }
                catch
                {
                    
                    ViewState["QRadd"] = "No";
                    
                }


            }
        }
        protected void btn_btn_find_date_Click(object sender, EventArgs e)
        {
            bind_all_data();
        }
        private void bind_all_data()
        {
            if (ddl_test_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select test name", "warning");
                ddl_test_name.Focus();
            }
            else if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {

                string sdate = txt_s_date.Text;
                string sday = sdate.Substring(0, 2);
                string smonth = sdate.Substring(3, 2);
                string syear = sdate.Substring(6, 4);

                string edate = txt_e_date.Text;
                string eday = edate.Substring(0, 2);
                string emonth = edate.Substring(3, 2);
                string eyear = edate.Substring(6, 4);

                int idate = Convert.ToInt32(syear + smonth + sday);
                int idate2 = Convert.ToInt32(eyear + emonth + eday);

                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where  (Is_transfer is null or Is_transfer='') and Payment_Status='Unpaid' and  idate>='" + idate + "' and idate<='" + idate2 + "' and Test_id=" + ddl_test_name.SelectedValue + " and Reg_payment_type='1' and Steps_done='10' order by idate asc");
                    lbl_class22.Text = "Date From :" + txt_s_date.Text + " End Date : " + txt_e_date.Text;
                }
            }



        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_test_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select test name", "warning");
                    ddl_test_name.Focus();
                }
                else
                {
                    find_by_class();
                    ViewState["flag"] = "3";
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void find_by_class()
        {
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where Steps_done='10' and (Is_transfer is null or Is_transfer='') and  Class_id='" + ddlclass.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " and Payment_Status='Unpaid' and Session_id=" + ddlsession.SelectedValue + "  and Reg_payment_type='1' order by id desc");

            lbl_class22.Text = " Session :" + ddlsession.SelectedValue + " Test : " + ddl_test_name.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
        }


        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id from session_details where Old_Use_Mode='2'   order by ID asc");

            ddlsession.SelectedValue = My.get_session_id_onlinereg();


            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddlsession.SelectedValue + "' order by  Test_name asc");
            ddl_test_name.SelectedValue = My.get_top_one_test_name(ddlsession.SelectedValue);

        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
            ddlclass.SelectedValue = My.get_top_one_class();
        }


        #region CountDataA
        private void bind_data()
        {
            string datEtim = mycode.date();
            DateTime startTime = DateTime.ParseExact(datEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string OneWeekDate = startTime.AddDays(-7).ToShortDateString();
            int oneWeek = My.DateConvertToIdate(OneWeekDate);


            // int oneWeek = 20210801;

            string sql = @"select  count(id) as ttl_admisiion from Online_Admission where Steps_done='10' and (Is_transfer is null or Is_transfer='') and Payment_Status='Unpaid' and Reg_payment_type='1' ;
                                       select  count(id) as ttl_admisiion_last_week from Online_Admission where Steps_done='10' and Payment_Status='Unpaid' and (Is_transfer is null or Is_transfer='')  and Reg_payment_type='1' and  idate>" + oneWeek + @";

                                       select count(id) as ttl_admisiion_in_hostel from Online_Admission where Steps_done='10' and Payment_Status='Unpaid' and (Is_transfer is null or Is_transfer='')  and Reg_payment_type='1' and Services='Hosteler';
                                       select count(id) as ttl_admisiion_in_hostel_lst_week from Online_Admission where Steps_done='10' and Payment_Status='Unpaid' and (Is_transfer is null or Is_transfer='') and Reg_payment_type='1' and Services='Hosteler' and idate>" + oneWeek + @";

                                        select count(id) as ttl_admisiion_in_days from Online_Admission where Steps_done='10' and Payment_Status='Unpaid' and (Is_transfer is null or Is_transfer='') and Reg_payment_type='1' and Services='Day Scholar';
                                         select count(id) as ttl_admisiion_in_days_lst_week from Online_Admission where Steps_done='10' and Payment_Status='Unpaid' and (Is_transfer is null or Is_transfer='') and Reg_payment_type='1' and Services='Day Scholar' and idate>" + oneWeek + @";";

            DataSet ds = mycode.Fill_Data_set(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[0];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlodR.InnerText = dtTemp.Rows[0]["ttl_admisiion"].ToString();
                }
                else
                {
                    ttlodR.InnerText = "00";
                }
            }
            else
            {
                ttlodR.InnerText = "00"; ;
            }


            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[1];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlodRLstWeeK.InnerText = dtTemp.Rows[0]["ttl_admisiion_last_week"].ToString() + "+ from last week";
                }
                else
                {
                    ttlodRLstWeeK.InnerText = "00";
                }
            }
            else
            {
                ttlodRLstWeeK.InnerText = "00"; ;
            }

            //============

            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[2];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlRvnuE.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_hostel"].ToString();
                }
                else
                {
                    ttlRvnuE.InnerText = "00";
                }
            }
            else
            {
                ttlRvnuE.InnerText = "00"; ;
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[3];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlRevenueLstWeeK.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_hostel_lst_week"].ToString() + "+ from last week";
                }
                else
                {
                    ttlRevenueLstWeeK.InnerText = "00";
                }
            }
            else
            {
                ttlRevenueLstWeeK.InnerText = "00"; ;
            }


            //============

            if (ds.Tables[4].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[4];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlCancelAmt.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_days"].ToString();
                }
                else
                {
                    ttlCancelAmt.InnerText = "00";
                }
            }
            else
            {
                ttlCancelAmt.InnerText = "00"; ;
            }


            if (ds.Tables[5].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[5];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlCancelAmtLstWeek.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_days_lst_week"].ToString() + "+ from last week";
                }
                else
                {
                    ttlCancelAmtLstWeek.InnerText = "00";
                }
            }
            else
            {
                ttlCancelAmtLstWeek.InnerText = "00"; ;
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
        #endregion

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                //Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_excels.Visible = false;
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                    hd_id.Value = lbl_Id.Text;
                    Response.Redirect("student-registration.aspx?stdid=" + lbl_Id.Text + "&admno=" + lbl_admissionserialnumber.Text, false);
                }
                else
                {

                }
            }
            catch
            {

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddl_test_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select test name", "warning");
                    ddl_test_name.Focus();
                }
                else
                {
                    find_by_c_s_a();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_c_s_a()
        {
            string qry = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                qry = "select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where Session_id='" + ddlsession.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " and (Is_transfer is null or Is_transfer='') and  Steps_done='10' and Payment_Status='Unpaid' and Reg_payment_type='1' order by id desc";
            }
            else
            {
                qry = "select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " and (Is_transfer is null or Is_transfer='') and  Steps_done='10' and Payment_Status='Unpaid' and Reg_payment_type='1' order by id desc";
            }


            bind_grd_view(qry);
            lbl_class22.Text = " Session :" + ddlsession.SelectedValue + " Test : " + ddl_test_name.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                    find_by_session();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_session()
        {
            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddlsession.SelectedValue + "' order by  Test_name asc");

            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where Steps_done='10' and (Is_transfer is null or Is_transfer='')  and  Session_id='" + ddlsession.SelectedValue + "' and Payment_Status='Unpaid'  and Reg_payment_type='1'  order by id desc");
            lbl_class22.Text = " Session :" + ddlsession.SelectedValue;
        }

        protected void btn_fnd_by_days_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_days_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please select admission in.", "warning");
                    ddl_days_hostel.Focus();
                }
                else
                {
                    find_by_admission_in();
                    ViewState["flag"] = "4";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission_in()
        {
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name ,(" + testname + ") as Test_name from Online_Admission where Steps_done='10' and (Is_transfer is null or Is_transfer='')  and  Services='" + ddl_days_hostel.SelectedValue + "' and Payment_Status='Unpaid'  and Reg_payment_type='1' order by id desc");
        }

        protected void ddl_test_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_test_name.SelectedValue == "")
            {
                Alertme("Please select test name in.", "warning");
            }
            else
            {

                bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where Steps_done='10' and (Is_transfer is null or Is_transfer='')  and  Session_id='" + ddlsession.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " and Payment_Status='Unpaid'  and Reg_payment_type='1'  order by id desc");


                lbl_class22.Text = " Session :" + ddlsession.SelectedValue + " Test : " + ddl_test_name.SelectedItem.Text;
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



        protected void ddl_payment_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_payment_mode.SelectedItem.Text == "Cash")
            {
                pnl_mode_t_n_dv.Visible = false;
                txt_transaction_no.Text = "NA";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Pos")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Netbanking")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Deposited In Bank")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Sbdebit")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Cheque")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Cheque No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "NEFT")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "UTR No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Debitcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Creditcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Otherdcard")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "UPI")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            if (ddl_payment_mode.SelectedItem.Text == "Demand Draft(DD)")
            {
                pnl_mode_t_n_dv.Visible = true;
                lbl_mode_trns_no_adm.InnerText = "Transaction No.";
                txt_transaction_no.Text = "";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_transaction_no.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter transaction no.", "warning");
                }
                else
                {
                    update_payment();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_payment()
        {
            string qry = "update Online_Admission set Payment_Status='Paid',Payment_remarks='" + txt_remark.Text + "',razorpay_payment_id='" + txt_transaction_no.Text + "',Payment_mode='" + ddl_payment_mode.Text + "' where Registration_id='" + ViewState["regNos"].ToString() + "'";
            My.exeSql(qry);

            Alertme("Payment has been updated successfully.", "success");
            if (ViewState["flag"].ToString() == "0")
            {
                find_by_c_s_a();
            }
            if (ViewState["flag"].ToString() == "1")
            {
                find_by_c_s_a();
            }
            if (ViewState["flag"].ToString() == "2")
            {
                find_by_session();
            }
            if (ViewState["flag"].ToString() == "3")
            {
                find_by_class();
            }
            if (ViewState["flag"].ToString() == "4")
            {
                find_by_admission_in();
            }
        }

        protected void lnk_update_payment_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Registration_id = (Label)row.FindControl("lbl_Registration_id");
                Label lbl_payment_slip1 = (Label)row.FindControl("lbl_payment_slip1");

                Label lbl_name = (Label)row.FindControl("lbl_name");
                Label lbl_fee_amount = (Label)row.FindControl("lbl_fee_amount");

               
                if (lbl_payment_slip1.Text == "" || lbl_payment_slip1.Text == "#")
                { }
                else
                {
                    Image2.ImageUrl = lbl_payment_slip1.Text;
                    Image2.Visible = true;
                }
                lbl_std_name.Text = lbl_name.Text;
                lbl_reg_no.Text = lbl_Registration_id.Text;
                lbl_reg_fee.Text = lbl_fee_amount.Text;


                ViewState["regNos"] = lbl_Registration_id.Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {
            }
        }
    }
}