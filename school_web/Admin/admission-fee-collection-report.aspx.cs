using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class admission_fee_collection_report : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        hd_session.Value = My.get_session_id();
                        hd_sessions.Value = My.get_session();
                        ViewState["flag"] = "1";
                        Session["paymentmode"] = "All";
                        find_by_date();
                        find_firm_details();
                       


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void find_all()
        {
            hd_from_date.Value = "0";
            hd_to_date.Value = "0";
            //bind_grd_view("select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t1.Acamedic_Semester_Id,t2.Session_id,t1.Class_id,(select top 1 Type from Session_Academic where Acamedic_Semester_Id=t1.Acamedic_Semester_Id) as Semester_year,isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Payble_amt,isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt,(isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)-(isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)+isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)))  as Total_dues_amt, isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Disc_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t1.Adjust_type is null and t2.Session_id='" + hd_session.Value + "' order by t1.id desc");


            bind_grd_view("select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t1.Acamedic_Semester_Id,t2.Session_id,t1.Class_id, isnull(convert(float, t1.Amount),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t1.Adjust_type is null and t2.Session_id='" + hd_session.Value + "' order by t1.id desc");
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_s_date.Text == "")
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
                    ViewState["flag"] = "1";
                    find_by_date();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_date()
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
                final_find_report_by_date(idate, idate2);
            }
        }

        private void final_find_report_by_date(int idate, int idate2)
        {
            hd_from_date.Value = idate.ToString();
            hd_to_date.Value = idate2.ToString();

            if (ddl_paymentmode.Text == "All")
            {
                Session["paymentmode"] = "All";
                //  bind_grd_view("select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t1.Acamedic_Semester_Id,t2.Session_id,t1.Class_id,(select top 1 Type from Session_Academic where Acamedic_Semester_Id=t1.Acamedic_Semester_Id) as Semester_year,isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Payble_amt,isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt,(isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)-(isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)+isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)))  as Total_dues_amt, isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Disc_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission'  and t2.Session_id=" + hd_session.Value + "  and t1.Idate>=" + idate + " and t1.Idate<=" + idate2 + "  order by t1.Idate desc");


                bind_grd_view("select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t1.Acamedic_Semester_Id,t2.Session_id,t1.Class_id,isnull(convert(float, t1.Amount),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t1.Idate>=" + idate + " and t1.Idate<=" + idate2 + "  order by t1.Idate desc");


            }
            else
            {
                Session["paymentmode"] = ddl_paymentmode.Text;

                bind_grd_view("select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t1.Acamedic_Semester_Id,t2.Session_id,t1.Class_id, isnull(convert(float, t1.Amount),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t1.Idate>=" + idate + " and t1.Idate<=" + idate2 + " and  t1.mode='" + ddl_paymentmode.Text + "' order by t1.Idate desc");
            }
        }

        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                // lbl_fnl_payble.Text = "00";
                lbl_fnl_paid.Text = "00";
                //lbl_fnl_duesS.Text = "00";
                // lbl_fnl_disc_amt.Text = "00";
                lbl_by_cash.Text = "00";
                lbl_by_netbanking.Text = "00";
                lbl_by_deposit.Text = "00";
                lbl_by_sb.Text = "00";
                lbl_by_chequeS.Text = "00";
                lbl_by_neft.Text = "00";
                lbl_by_debitcard.Text = "00";
                lbl_by_credit_card.Text = "00";
                lbl_by_other_card.Text = "00";
                lbl_by_upi.Text = "00";
                lbl_by_branch.Text = "00";
                lbl_pos.Text = "0.00";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                lbl_class22.Text = "";
                btn_excels.Visible = false;
                print1.Visible = false;
            }
            else
            {
                lbl_class22.Text =" Form : "+ txt_s_date.Text + " To : " + txt_e_date.Text+ " Payment Mode" + ddl_paymentmode.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
               
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }


                string qryS = ""; string qrySS = "";
                if (ViewState["flag"].ToString() == "0")
                {
                    if (ddl_paymentmode.Text == "All")
                    {
                        // qryS = "select isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Payble_amt,isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt,(isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)-(isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)+isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)))  as Total_dues_amt, isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Disc_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t2.Session_id='" + hd_session.Value + "' and t1.Adjust_type is null";

                        qryS = "select isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Payble_amt,isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt,(isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)-(isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)+isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)))  as Total_dues_amt, isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Disc_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission'   and t1.Adjust_type is null and t2.Session_id='" + hd_session.Value + "'";//




                        qrySS = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,mode from Student_Payment_History where Type='Admission' and Session='" + hd_sessions.Value + "' group by mode";
                    }
                    else
                    {
                        qryS = "select isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Payble_amt,isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt,(isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)-(isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)+isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)))  as Total_dues_amt, isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Disc_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t2.Session_id='" + hd_session.Value + "' and t1.Adjust_type is null and t1.mode='" + ddl_paymentmode.Text + "' ";


                        qrySS = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,mode from Student_Payment_History where Type='Admission' and Session='" + hd_sessions.Value + "' and  mode='" + ddl_paymentmode.Text + "' group by mode";
                    }
                }
                if (ViewState["flag"].ToString() == "1")
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



                    if (ddl_paymentmode.Text == "All")
                    {

                        //update code 25/03/2023
                        //qryS = "select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,(select top 1 Type from Session_Academic where Acamedic_Semester_Id=t1.Acamedic_Semester_Id) as Semester_year,isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Payble_amt,isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt,(isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)-(isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)+isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)))  as Total_dues_amt, isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Disc_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t2.Session_id='" + hd_session.Value + "' and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.Adjust_type is null order by t1.id desc";

                        qryS = "select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,isnull(convert(float, t1.Amount),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission'  and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.Adjust_type is null order by t1.id desc";//and t2.Session_id='" + hd_session.Value + "'

                        qrySS = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,mode from Student_Payment_History where Type='Admission'  and Idate>='" + idate + "' and Idate<='" + idate2 + "' group by mode";//and Session='" + hd_sessions.Value + "'
                    }
                    else
                    {



                        // qryS = "select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,(select top 1 Type from Session_Academic where Acamedic_Semester_Id=t1.Acamedic_Semester_Id) as Semester_year,isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Payble_amt,isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt,(isnull((select  sum(isnull(convert(float, payable),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)-(isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)+isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0)))  as Total_dues_amt, isnull((select  sum(isnull(convert(float, Disc),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_Disc_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t2.Session_id='" + hd_session.Value + "' and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.Adjust_type is null and t1.mode = '" + ddl_paymentmode.Text + "' order by t1.id desc";

                        qryS = "select  t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,isnull(convert(float, t1.Amount),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission'  and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.Adjust_type is null and t1.mode = '" + ddl_paymentmode.Text + "' order by t1.id desc";//and t2.Session_id='" + hd_session.Value + "'




                        qrySS = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,mode from Student_Payment_History where Type='Admission'  and Idate>='" + idate + "' and Idate<='" + idate2 + "' and mode = '" + ddl_paymentmode.Text + "' group by mode";//and Session='" + hd_sessions.Value + "'
                    }
                }
                DataTable dtS = mycode.FillData(qryS);
                if (dtS.Rows.Count == 0)
                {
                    // lbl_fnl_payble.Text = "00";
                    lbl_fnl_paid.Text = "00";
                    //  lbl_fnl_duesS.Text = "00";
                    //  lbl_fnl_disc_amt.Text = "00";
                }
                else
                {
                    //String Fnl_payble_amt = Convert.ToDouble(dtS.Compute("SUM(Total_Payble_amt)", string.Empty)).ToString();
                    String Fnl_paid_amt = Convert.ToDouble(dtS.Compute("SUM(Total_paid_amt)", string.Empty)).ToString("0.00");
                    // String Fnl_dues_amt = Convert.ToDouble(dtS.Compute("SUM(Total_dues_amt)", string.Empty)).ToString();
                    // String Fnl_disc_amt = Convert.ToDouble(dtS.Compute("SUM(Total_Disc_amt)", string.Empty)).ToString();
                    //  lbl_fnl_payble.Text = Fnl_payble_amt;
                    lbl_fnl_paid.Text = Fnl_paid_amt;
                    // lbl_fnl_duesS.Text = Fnl_dues_amt;
                    //  lbl_fnl_disc_amt.Text = Fnl_disc_amt;
                }



                DataTable dtSS = mycode.FillData(qrySS);
                if (dtSS.Rows.Count == 0)
                {
                    lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_by_deposit.Text = "00";
                    lbl_by_sb.Text = "00";
                    lbl_by_chequeS.Text = "00";
                    lbl_by_neft.Text = "00";
                    lbl_by_debitcard.Text = "00";
                    lbl_by_credit_card.Text = "00";
                    lbl_by_other_card.Text = "00";
                    lbl_by_upi.Text = "00";
                    lbl_by_branch.Text = "00";
                    lbl_pos.Text = "0.00";
                    lbl_online.Text = "0.00";
                }
                else
                {

                    lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_by_deposit.Text = "00";
                    lbl_by_sb.Text = "00";
                    lbl_by_chequeS.Text = "00";
                    lbl_by_neft.Text = "00";
                    lbl_by_debitcard.Text = "00";
                    lbl_by_credit_card.Text = "00";
                    lbl_by_other_card.Text = "00";
                    lbl_by_upi.Text = "00";
                    lbl_by_branch.Text = "00";
                    lbl_pos.Text = "0.00";
                    lbl_online.Text = "0.00";
                    for (int i = 0; i < dtSS.Rows.Count; i++)
                    {
                        if (dtSS.Rows[i]["mode"].ToString() == "Cash")
                        {
                            lbl_by_cash.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Netbanking")
                        {
                            lbl_by_netbanking.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Deposited In Bank")
                        {
                            lbl_by_deposit.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Sbdebit")
                        {
                            lbl_by_sb.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Cheque")
                        {
                            lbl_by_chequeS.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[0]["mode"].ToString() == "NEFT")
                        {
                            lbl_by_neft.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Debitcard")
                        {
                            lbl_by_debitcard.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Creditcard")
                        {
                            lbl_by_credit_card.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Otherdcard")
                        {
                            lbl_by_other_card.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "UPI")
                        {
                            lbl_by_upi.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString() == "Branch")
                        {
                            lbl_by_branch.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                        if (dtSS.Rows[i]["mode"].ToString().ToLower() == "pos")
                        {
                            lbl_pos.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }

                        if (dtSS.Rows[i]["mode"].ToString().ToLower() == "online")
                        {
                            lbl_online.Text = My.toDouble((dtSS.Rows[i]["Paid_amt"].ToString())).ToString("0.00");
                        }
                    }
                }
            }
        }

        protected void lnkviewDt_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_slip_no = (Label)row.FindControl("lbl_slip_no");
                DataTable dt = mycode.FillData("select parameter,Date,feetype,payable,paid,dues from Typewise_fee_collection where admission_no='" + lbl_admissionserialnumber.Text + "' and transection='" + lbl_slip_no.Text + "' order by id asc");
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rp_fee_info.DataSource = null;
                    rp_fee_info.DataBind();
                }
                else
                {
                    rp_fee_info.DataSource = dt;
                    rp_fee_info.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch
            {
            }
        }


        //===========================

        [WebMethod]
        public static List<object> GetChartData(string Session, string From_date, string To_date)
        {
            string sections = get_sections(Session, From_date, To_date);
            string query = "";

            if (From_date == "0")
            { 
                query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Student_Payment_History t1 on t1.Addmission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.Session=ar.Session where t1.Type='Admission' and t1.Adjust_type is null and  sm.Status in (" + sections + ") order by ad.Position asc";//and ar.Session_id='" + Session + "' 
            }
            else
            {
                query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Student_Payment_History t1 on t1.Addmission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.Session=ar.Session where t1.Type='Admission' and t1.Adjust_type is null  and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' and sm.Status in (" + sections + ") order by ad.Position asc";//and ar.Session_id='" + Session + "'
            } 
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Status"].ToString(), From_date, To_date);
                dr["Total"] = total_count;
            }

            List<object> chartData = new List<object>();
            List<string> countries = (from p in dt.AsEnumerable()
                                      select p.Field<string>("Section")).Distinct().ToList();

            countries.Insert(0, "Status");

            //Add the Countries Array to the Chart Array.
            chartData.Add(countries.ToArray());


            //Get the DISTINCT Date.
            List<string> years = (from p in dt.AsEnumerable()
                                  select p.Field<string>("Class")).Distinct().ToList();

            //Loop through the Date.
            foreach (string year in years)
            {

                //Get the Total of Orders for each Status for the Date.
                List<object> totals = (from p in dt.AsEnumerable()
                                       where p.Field<string>("Class") == year
                                       select p.Field<Int32>("Total")).Cast<object>().ToList();

                //Insert the Year value as Label in First position.
                totals.Insert(0, year.ToString());

                //Add the Years Array to the Chart Array.
                chartData.Add(totals.ToArray());
            }
            return chartData;
        }




        private static int get_total(string session_id, string class_id, string section, string From_date, string To_date)
        {
            string query = "";
            if (From_date == "0")
            {

                //if (session["paymentmode"].ToString() == "All")
                //{
                query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t1.Adjust_type is null and t2.Session_id='" + session_id + "' and t2.Class_id='" + class_id + "' and t2.Section='" + section + "') t";
                //}
                //else
                //{
                //    query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t1.Adjust_type is null and t2.Session_id='" + session_id + "' and t2.Class_id='" + class_id + "' and t2.Section='" + section + "' and t1.mode='"+ Session["paymentmode"].ToString() + "') t";
                //}
            }
            else
            {
                //if (Session["paymentmode"].ToString() == "All")
                //{
                query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where  t1.Type='Admission' and t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' and t2.Class_id='" + class_id + "' and t2.Section='" + section + "') t";//t2.Session_id='" + session_id + "' and
                //}
                //else
                //{
                //    query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t2.Session_id='" + session_id + "' and t1.Type='Admission' and t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' and t2.Class_id='" + class_id + "' and t2.Section='" + section + "' and t1.mode='" + Session["paymentmode"].ToString() + "') t";
                //}
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                double ttl = My.toDouble(dt.Rows[0][0].ToString());
                int count = My.toIntS(Math.Round(ttl).ToString());
                return count;
            }
        }

        private static string get_sections(string Session, string From_date, string To_date)
        {
            string query = "";
            if (From_date == "0")
            {
                query = "select  DISTINCT t2.Section from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Admission' and t1.Adjust_type is null and t2.Session_id='" + Session + "' order by t2.Section desc";
            }
            else
            {
                query = "select  DISTINCT t2.Section from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where  t1.Type='Admission' and t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' order by t2.Section desc";//t2.Session_id='" + Session + "' and
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {


                string a = "'" + "A" + "'";
                return a;

            }
            else
            {
                string section = "";
                foreach (DataRow dr in dt.Rows)
                {
                    section = section + "'" + dr["Section"].ToString() + "',";
                }

                section = section.Remove(section.Length - 1);
                return section;
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (ViewState["Is_Print"].ToString() == "1")
                {

                    ((Panel)e.Item.FindControl("Panel2_print")).Visible = true;
                   

                }
                else
                {
                    ((Panel)e.Item.FindControl("Panel2_print")).Visible = false;
                   
                }
            }
        }
    }
}