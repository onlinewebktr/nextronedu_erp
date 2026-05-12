using DotNetIntegrationKit;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.worldline
{
    public partial class Request_new_worldline_Admission_Annual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ordertrackingid"] != null)
                {
                    ViewState["ordertrackingid"] = Request.QueryString["ordertrackingid"].ToString();
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    
                    Bind_data_online_reg();
                }
            }

        }
        My mycode = new My();
        private void Bind_data_online_reg()
        {
            try
            {

                TXT_returnURL.Text = My.URL() + "Student_Profile/webview/worldline/ResponsePage_New_Add_annual_worldline.aspx";
                 
                string MERCHANT_KEY = TXT_merchantcode.Text = My.Merchant_Key_Get_Worldline();
                string autkeyrozorkey = TXT_IsKey.Text = My.Encryptionkey_Worldline_Get_Worldline();
                string encryption_IV = TXT_IsIv.Text = My.Encryption_IV_Get_Worldline();

                DataTable dt = mycode.FillData("Select top 1 * from Payment_transaction_process_Admission where ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' and status='Pending'   order by id desc");
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    TXT_Amount.Text = dt.Rows[0]["Total_pay"].ToString(); ;// "1.00";//;

                    TXT_Shoppingcartdetails.Text = "FIRST_" + TXT_Amount.Text + "_0.0";

                    TXT_uniqueCustomerID.Text = dt.Rows[0]["Admission_no"].ToString();
                    TXT_MerchantTxnRefNo.Text = dt.Rows[0]["ordertrackingid"].ToString();
                    TXT_TxnDate.Text = dt.Rows[0]["Date"].ToString().Replace("/", "-"); //rplac"07-02-2024"; //dt.Rows[0]["Date"].ToString();
                    TXT_Email.Text = dt.Rows[0]["Emailid"].ToString();
                    TXT_mobileNo.Text = dt.Rows[0]["Mobileno"].ToString();
                    TXT_customerName.Text = dt.Rows[0]["Name"].ToString();

                }






                String response = "";
                lblResponse.Text = "";
                lblError.Text = "";
                RequestURL objRequestURL = new RequestURL();


                if (TXT_requesttype.Text.ToUpper() == "T" || TXT_requesttype.Text.ToUpper() == "S" || TXT_requesttype.Text.ToUpper() == "O" || TXT_requesttype.Text.ToUpper() == "R")
                {
                    response = objRequestURL.SendRequest
                              (
                              TXT_requesttype.Text
                              , TXT_merchantcode.Text
                              , TXT_MerchantTxnRefNo.Text
                              , TXT_ITC.Text, TXT_Amount.Text
                              , TXT_Currencycode.Text
                              , TXT_uniqueCustomerID.Text
                              , TXT_returnURL.Text
                              , TXT_StoSreturnURL.Text
                              , TXT_TPSLTXNID.Text
                              , TXT_Shoppingcartdetails.Text
                              , TXT_TxnDate.Text
                              , TXT_Email.Text
                              , TXT_mobileNo.Text
                              , TXT_Bankcode.Text
                              , TXT_customerName.Text
                              , TXT_CardID.Text
                              , TXT_AccountNo.Text
                              , TXT_IsKey.Text
                              , TXT_IsIv.Text
                              );
                }
                String strResponse = response.ToUpper();

                bool IsValid = false;

                if (strResponse.StartsWith("ERROR"))
                {
                    if (strResponse == "ERROR073")
                    {
                        IsValid = false;
                        lblError.Text = null;
                        response = objRequestURL.SendRequest
                                   (
                                    TXT_requesttype.Text
                                    , TXT_merchantcode.Text
                                    , TXT_MerchantTxnRefNo.Text
                                    , TXT_ITC.Text, TXT_Amount.Text
                                    , TXT_Currencycode.Text
                                    , TXT_uniqueCustomerID.Text
                                    , TXT_returnURL.Text
                                    , TXT_StoSreturnURL.Text
                                    , TXT_TPSLTXNID.Text
                                    , TXT_Shoppingcartdetails.Text
                                    , TXT_TxnDate.Text
                                    , TXT_Email.Text
                                    , TXT_mobileNo.Text
                                    , TXT_Bankcode.Text
                                    , TXT_customerName.Text
                                    , TXT_CardID.Text
                                    , TXT_AccountNo.Text
                                    , TXT_IsKey.Text
                                    , TXT_IsIv.Text
                                   );
                        strResponse = response.ToUpper();
                    }
                    else
                    {
                        lblResponse.Text = response;
                    }
                }
                else
                {
                    IsValid = true;
                }

                if (TXT_requesttype.Text.Trim() == "T")
                {
                    if (IsValid)
                    {
                        Session["Merchant_Code"] = TXT_merchantcode.Text;
                        Session["IsKey"] = TXT_IsKey.Text;
                        Session["IsIv"] = TXT_IsIv.Text;
                        Response.Write("<form name='s1_2' id='s1_2' action='" + response + "' method='post'> ");
                        Response.Write("<script type='text/javascript' language='javascript' >document.getElementById('s1_2').submit();");
                        Response.Write("</script>");
                        Response.Write("<script language='javascript' >");
                        Response.Write("</script>");
                        Response.Write("</form> ");
                    }
                }
                else
                {
                    if (response == "")
                    {
                        lblResponse.Text = "Transaction Fail " + "ERROR:";
                    }
                    else
                    {
                        lblResponse.Text = response;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}