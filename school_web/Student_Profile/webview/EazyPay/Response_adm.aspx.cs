using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.EazyPay
{
    public partial class Response_adm : System.Web.UI.Page
    {
        My mycode = new My();
        string responseData = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            RecoverResponsePacket();

        }
        private void RecoverResponsePacket()
        {
            string ResponseCode = "";
            string ReferenceNo = "";
            string ServiceTaxAmount = "";
            string TransactionAmount = "";
            string ChallanNo = "";
            string TransactionDate = "";
            string ApplicantName = "";
            string Mobileno = "";
            string paymode = "";
            string UniqueRefNumber = "";
            string ProcessingFeeAmount = "";
            string mandatoryfields = "";
            string mandatory_fields = "";
            string trns_code = "";


            try//1.ResponseCode
            {
                ResponseCode = HttpContext.Current.Request.Form["Response Code"].ToString();

                if (ResponseCode != "E000")// Transaction Failed.
                {
                    ViewState["statuS"] = "Failed";

                    mandatory_fields = HttpContext.Current.Request.Form["mandatory fields"].ToString();
                    //Freehold_DA.Logger.LogMessageToFile("icici ProcessingFeeAmount", ProcessingFeeAmount);
                    string resData = mandatory_fields;
                    string resData1 = resData;
                    string[] tokens = resData.Split('|');
                    string Mobile = tokens[tokens.Length - 2];

                    ReferenceNo = tokens[0];

                    ViewState["Mobile"] = Mobile;
                    string resData2 = resData;
                    string[] tokens1 = resData.Split('|');
                    string mail = tokens[tokens.Length - 1];
                    ViewState["mail"] = mail;
                    string resData3 = resData;
                    string[] tokens2 = resData.Split('|');
                    string Name = tokens[tokens.Length - 3];
                    ViewState["Name"] = Name;
                    string resData4 = resData;
                    string[] tokens3 = resData.Split('|');
                    string reqid = tokens[tokens.Length - 5];
                    ViewState["reqid"] = reqid;
                    ViewState["ordertrackingid"] = ReferenceNo;
                    ViewState["TransactionDate"] = TransactionDate;
                    ViewState["TransactionAmount"] = TransactionAmount;
                    ViewState["paymode"] = paymode;
                    ViewState["TransactionDate"] = TransactionDate;
                    string Remarks = My.get_eg_pay_remarks(ResponseCode);
                    string gte_regid = My.get_reg_id_from_order_id(ReferenceNo);
                    ViewState["regid"] = gte_regid;
                    My.exeSql("update Payment_transaction_process_Admission set razorpay_order_id='" + ViewState["ordertrackingid"].ToString() + "',razorpay_payment_id='',status='Failed',Remarks='" + Remarks + "'  where  ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "'");
                    Response.Redirect("../Student_Annual_Payment.aspx?regid=" + ViewState["regid"].ToString(), false);


                }
                else
                {
                    string Unique_Ref_Number = "";
                    ViewState["statuS"] = "Paid";
                    try
                    {
                        mandatory_fields = HttpContext.Current.Request.Form["mandatory fields"].ToString();
                        //Freehold_DA.Logger.LogMessageToFile("icici ProcessingFeeAmount", ProcessingFeeAmount);
                        string resData = mandatory_fields;
                        string resData1 = resData;
                        string[] tokens = resData.Split('|');
                        string Mobile = tokens[tokens.Length - 2];

                        ReferenceNo = tokens[0];

                        ViewState["Mobile"] = Mobile;
                        string resData2 = resData;
                        string[] tokens1 = resData.Split('|');
                        string mail = tokens[tokens.Length - 1];
                        ViewState["mail"] = mail;
                        string resData3 = resData;
                        string[] tokens2 = resData.Split('|');
                        string Name = tokens[tokens.Length - 3];
                        ViewState["Name"] = Name;
                        string resData4 = resData;
                        string[] tokens3 = resData.Split('|');
                        string reqid = tokens[tokens.Length - 5];
                        ViewState["reqid"] = reqid;
                        ViewState["ordertrackingid"] = ReferenceNo;
                        ViewState["TransactionDate"] = TransactionDate;
                        ViewState["TransactionAmount"] = TransactionAmount;
                        ViewState["paymode"] = paymode;
                        ViewState["TransactionDate"] = TransactionDate;
                        string gte_regid = My.get_reg_id_from_order_id(ReferenceNo);
                        ViewState["regid"] = gte_regid;

                        string resp_code = ResponseCode;
                        try
                        {
                            Unique_Ref_Number = Request.Form["Unique Ref Number"];

                        }
                        catch
                        {
                            Unique_Ref_Number = Request.Form["Unique Ref Number"];
                        }
                    }
                    catch
                    {

                    }

                    My.exeSql("update Payment_transaction_process_Admission set razorpay_order_id='" + ViewState["ordertrackingid"].ToString() + "',razorpay_payment_id='" + Unique_Ref_Number + "',status='Success',Remarks='Received successful confirmation in real time for the transaction.'   where  ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "'");



                    btn_submit();
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void btn_submit()
        {
            try
            {
                DataTable dt = mycode.FillData("Select * from Payment_transaction_process_Admission where ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' ");
                if (dt.Rows.Count == 0)
                {
                    Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);
                }
                else
                {
                    // synch_data();
                    if (ViewState["statuS"].ToString() == "Paid")
                    {
                        //paid process
                        Payment_update_after_onlinepayment_Admission mypayment = new Payment_update_after_onlinepayment_Admission();
                        bool status = mypayment.save_final_payment(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                        if (status == true)
                        {
                            Response.Redirect("../Payment_Success_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                        }
                        else
                        {
                            Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["payFrom"].ToString(), false);
                        }

                    }
                    else // failed/technila issues ocured
                    {
                        Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);



                    }
                }
                // System.Threading.Thread.Sleep(30000);
                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }
    }
}