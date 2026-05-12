using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.EazyPay
{
    public partial class Response_online_reg : System.Web.UI.Page
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
                    string gte_regid = My.get_reg_id_from_order_id(ReferenceNo);
                    ViewState["regid"] = gte_regid;
                    Response.Redirect("../../Student_Online_Registration_Apply.aspx", false);


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
                        ViewState["Unique_Ref_Number"] = Unique_Ref_Number;
                        btn_submit();
                    }
                    catch
                    {
                        ViewState["statuS"] = "Failed";
                        Response.Redirect("../../Student_Online_Registration_Apply.aspx", false);
                    }

                  
                }
            }
            catch (Exception ex)
            {

            }


        }

        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                btn_submit();


            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        private void btn_submit()
        {
            if (ViewState["statuS"].ToString() == "Paid")
            {
                //paid process

                DataTable dt = mycode.FillData("select * from Online_Admission where Order_id='" + ViewState["ordertrackingid"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    string Registration_id = dt.Rows[0]["Registration_id"].ToString();
                    string Session_id = dt.Rows[0]["Session_id"].ToString();
                    string Class_id = dt.Rows[0]["Class_id"].ToString();
                    string testid = dt.Rows[0]["Test_id"].ToString();
                    int fill_no_seat = My.get_no_seat_current_session(Session_id, Class_id, testid);
                    int get_no_fill_seat = My.get_no_of_fill_from_seat(Session_id, Class_id, testid);
                    if (get_no_fill_seat >= fill_no_seat)//seat check
                    {

                        string qry = "update Online_Admission set Steps_done='10',Payment_Status='" + ViewState["statuS"].ToString() + "',razorpay_payment_id='"+ ViewState["Unique_Ref_Number"].ToString() + "' where Registration_id='" + Registration_id + "'";
                        mycode.executequery(qry);
                        Response.Redirect("../../print/Print_Page.aspx?PaymentSliP=" + Registration_id, false);
                    }
                    else
                    {
                        string qry = "update Online_Admission set Steps_done='9',Payment_Status='Unpaid',Payment_remarks='SeatFull' where Registration_id='" + Registration_id + "'";
                        mycode.executequery(qry);
                        Response.Redirect("../../print/Print_Page_Seat_full.aspx?PaymentSliP=" + Registration_id, false);

                    }
                }
            }
            else // failed/technila issues ocured
            {
                Response.Redirect("../../Student_Online_Registration_Apply.aspx", false);
            }
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }
    }
}