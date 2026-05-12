using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Globalization;
namespace school_web.LMS_VC_Admin
{
    public partial class Payment_Entry : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
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
                    if (Request.QueryString["Id"] != null)
                    {
                        hd_id.Value = Request.QueryString["Id"].ToString();
                        BindDetails();
                    }
                }
            }
        }

        private void BindDetails()
        {
            DataTable dt = mycode.FillTable("select *,format(Transaction_date, 'dd/MM/yyyy') as date1,format(Transaction_date, 'dd/MM/yyyy hh:mm:ss tt') as transaciondate1 from Online_Manual_Transaction_details where Id=" + hd_id.Value + "");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                txt_admission_no.Text = dt.Rows[0]["UserId"].ToString();
                txt_discountamount.Text = dt.Rows[0]["discount"].ToString();
                txt_paybalamount.Text = dt.Rows[0]["Amount"].ToString();
                txt_total_amount.Text = dt.Rows[0]["tatal_amount"].ToString();
                txt_TransactionId.Text = dt.Rows[0]["TransactionId"].ToString();
                txt_refrrenceid_OrderTrackingID.Text = dt.Rows[0]["OrderTrackingID"].ToString();
                txt_months.Text = dt.Rows[0]["months"].ToString();
                try
                {
                    txt_date.Text = dt.Rows[0]["date1"].ToString();


                    MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                    DateTime startTime = DateTime.ParseExact(dt.Rows[0]["transaciondate1"].ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string am1 = startTime.ToString("tt");
                    string hh = startTime.ToString("hh");
                    string mm = startTime.ToString("mm");
                    string ss = startTime.ToString("ss");
                    if (am1 == "AM")
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    }
                    else
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    }
                    time_StartTime.SetTime(Convert.ToInt32(hh), Convert.ToInt32(mm), Convert.ToInt32(ss), am_pm);




                }
                catch
                {
                }
                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
            }
        }
        
        protected void txt_total_amount_TextChanged(object sender, EventArgs e)
        {
            if (txt_total_amount.Text == "")
            {
                lblmessage.Text = "Please enter total amount";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                double totalamount = UsesCode.converttodouble(txt_total_amount.Text);
                double paybalamount = UsesCode.converttodouble(txt_paybalamount.Text);
                if (totalamount >= paybalamount)
                {
                    if (txt_paybalamount.Text == "")
                    {
                          paybalamount = UsesCode.converttodouble(txt_total_amount.Text);
                    }
                    
                    double discount = totalamount - paybalamount;
                    txt_discountamount.Text = discount.ToString("0.00");
                }
                else
                {
                    lblmessage.Text = "Sorry! You have can't enter payable not greater than the total amount";
                    string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
        }

        protected void txt_paybalamount_TextChanged(object sender, EventArgs e)
        {
            if (txt_total_amount.Text == "")
            {
                lblmessage.Text = "Please enter total amount";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_paybalamount.Text == "")
            {
                lblmessage.Text = "Please enter payable amount";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                double totalamount = UsesCode.converttodouble(txt_total_amount.Text);
                double paybalamount = UsesCode.converttodouble(txt_paybalamount.Text);
                if (totalamount >= paybalamount)
                {
                    double discount = totalamount - paybalamount;
                    txt_discountamount.Text = discount.ToString("0.00");
                }
                else
                {
                    lblmessage.Text = "Sorry! You have can't enter payable not greater than the total amount";
                    string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                lblmessage.Text = "Please enter admission  no.";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_refrrenceid_OrderTrackingID.Text == "")
            {
                lblmessage.Text = "Please enter reference id";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_TransactionId.Text == "")
            {
                lblmessage.Text = "Please enter software transaction id";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_date.Text == "")
            {
                lblmessage.Text = "Please enter date if transaction";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_months.Text == "")
            {
                lblmessage.Text = "Please enter month name";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_total_amount.Text == "")
            {

                lblmessage.Text = "Please enter total amount";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_paybalamount.Text == "")
            {
                lblmessage.Text = "Please enter payable amount";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                try
                {
                    string date = txt_date.Text;
                    string mergeStartTime = date + " " + time_StartTime.Date.ToString("hh:mm:ss tt");
                    DateTime startTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                  
                    double totalamount = UsesCode.converttodouble(txt_total_amount.Text);
                    double paybalamount = UsesCode.converttodouble(txt_paybalamount.Text);
                    if (btn_Submit.Text == "Add")
                    {

                        SqlCommand cmd;
                        string query = "INSERT INTO Online_Manual_Transaction_details (UserId,Transaction_date,OrderTrackingID,TransactionId,Amount,tatal_amount,discount,months,Date,Idate,time,Istatus,TransactionStatus) values (@UserId,@Transaction_date,@OrderTrackingID,@TransactionId,@Amount,@tatal_amount,@discount,@months,@Date,@Idate,@time,@Istatus,@TransactionStatus)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@UserId", txt_admission_no.Text.Trim());
                        cmd.Parameters.AddWithValue("@Transaction_date", startTime);
                        cmd.Parameters.AddWithValue("@OrderTrackingID", txt_refrrenceid_OrderTrackingID.Text);
                        cmd.Parameters.AddWithValue("@TransactionId", txt_TransactionId.Text);
                        cmd.Parameters.AddWithValue("@Amount", paybalamount.ToString("0.00"));
                        cmd.Parameters.AddWithValue("@tatal_amount", totalamount.ToString("0.00"));
                        cmd.Parameters.AddWithValue("@discount", txt_discountamount.Text);
                        cmd.Parameters.AddWithValue("@months", txt_months.Text);
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@time", mycode.time());
                        cmd.Parameters.AddWithValue("@Istatus", "0");
                        cmd.Parameters.AddWithValue("@TransactionStatus", "success");
                        
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            lblmessage.Text = "Transaction amount has been successfully added";
                            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                            empty_data();

                        }

                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "Update Online_Manual_Transaction_details set UserId=@UserId,Transaction_date=@Transaction_date,OrderTrackingID=@OrderTrackingID,TransactionId=@TransactionId,Amount=@Amount,tatal_amount=@tatal_amount,discount=@discount,months=@months,Date=@Date,Idate=@Idate,time=@time where Id = @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@UserId", txt_admission_no.Text.Trim());
                        cmd.Parameters.AddWithValue("@Transaction_date", startTime);
                        cmd.Parameters.AddWithValue("@OrderTrackingID", txt_refrrenceid_OrderTrackingID.Text);
                        cmd.Parameters.AddWithValue("@TransactionId", txt_TransactionId.Text);
                        cmd.Parameters.AddWithValue("@Amount", paybalamount.ToString("0.00"));
                        cmd.Parameters.AddWithValue("@tatal_amount", totalamount.ToString("0.00"));
                        cmd.Parameters.AddWithValue("@discount", txt_discountamount.Text);
                        cmd.Parameters.AddWithValue("@months", txt_months.Text);
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@time", mycode.time());
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            lblmessage.Text = "Transaction amount has been updated successfully";
                            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                            empty_data();

                        }
                    }
                }
                catch
                {
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_data();

        }

        private void empty_data()
        {
            txt_admission_no.Text = "";
            txt_discountamount.Text = "0";
            txt_paybalamount.Text = "0";
            txt_total_amount.Text = "0";
            txt_TransactionId.Text = "";
            txt_refrrenceid_OrderTrackingID.Text = "";
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
        }
    }
}