using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DotNetIntegrationKit;
using System.Configuration;
using school_web.AppCode;
using System.Data;
using Newtonsoft.Json;

namespace school_web.Student_Profile.webview.worldline
{
    public partial class Resp_NewMonthley_worldline : System.Web.UI.Page
    {
        #region Variable Declaration

        //		string str = "";
        //		string lskey = null, lsIv = null;
        //		String uuid = null;
        //		String strRequest = "";	
        //				string strPGActualReponse=PGResponse;
        string strHEX, strPGActualReponseWithChecksum, strPGActualReponseEncrypted, strPGActualReponseDecrypted, strPGresponseChecksum, strPGTxnStatusCode;
        //string strPGActualReponse="status=0300|amount=125.00|hash=3243453454353453";
        //string strPGActualReponse=PGResponse,strPGTxnStatusCode;
        string[] strPGChecksum, strPGTxnString;
        bool isDecryptable = false;

        string strPG_TxnStatus = string.Empty,
        strPG_ClintTxnRefNo = string.Empty,
        strPG_TPSLTxnBankCode = string.Empty,
        strPG_TPSLTxnID = string.Empty,
        strPG_TxnAmount = string.Empty,
        strPG_TxnDateTime = string.Empty,
        strPG_TxnDate = string.Empty,
        strPG_TxnTime = string.Empty;
        string strPGResponse;
        string[] strSplitDecryptedResponse;
        string[] strArrPG_TxnDateTime;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                strPGResponse = Request["msg"].ToString();  //Reading response of PG

                if (strPGResponse != "" || strPGResponse != null)
                {
                    LBL_DisplayResult.Text = "Response :: " + strPGResponse;

                    RequestURL objRequestURL = new RequestURL();    //Creating Object of Class DotNetIntegration_1_1.RequestURL
                    string strDecryptedVal = null;                  //Decrypting the PG response

                    if (!String.IsNullOrEmpty(Convert.ToString(Session["PropertyFile"])))
                    {
                        string strFilePath = ConfigurationSettings.AppSettings["FilePath"];
                        string[] FilePath = strFilePath.Split('\\');
                        string MerchantCode = Convert.ToString(Session["Merchant_Code"]);
                        //strFilePath = FilePath[0] + "\\" + FilePath[2] + "\\" + MerchantCode + "\\" + FilePath[4];

                        strDecryptedVal = objRequestURL.VerifyPGResponse(strPGResponse, strFilePath);
                    }
                    else
                    {
                        string strIsKey = My.Encryptionkey_Worldline_Get_Worldline(); //"6144192292TXADDJ";//Convert.ToString(Session["IsKey"]);
                        string strIsIv = My.Encryption_IV_Get_Worldline(); //"7262785914QOIBQM";//Convert.ToString(Session["IsIv"]);

                        strDecryptedVal = objRequestURL.VerifyPGResponse(strPGResponse, strIsKey, strIsIv);
                    }
                    lblResponseDecrypted.Text = strDecryptedVal;

                    if (strDecryptedVal.StartsWith("ERROR"))
                    {
                        lblValidate.Text = strDecryptedVal;
                    }
                    else
                    {
                        strSplitDecryptedResponse = strDecryptedVal.Split('|');
                        GetPGRespnseData(strSplitDecryptedResponse);


                        dynamic dobj = DeserializeDynamic(strDecryptedVal);
                        string txn_status = dobj.txn_status;
                        string txn_msg = dobj.txn_msg;
                        string clnt_txn_ref = dobj.clnt_txn_ref;
                        string tpsl_txn_id = dobj.tpsl_txn_id;
                        string tpsl_bank_cd = dobj.tpsl_bank_cd;
                        string tpsl_txn_time = dobj.tpsl_txn_time;
                        string rqst_token = dobj.rqst_token;

                        string custid = clnt_txn_ref;
                        ViewState["ordertrackingid"] = custid;
                        //txn_status=0300|txn_msg=success|txn_err_msg=NA|clnt_txn_ref=01022024141959|tpsl_bank_cd=470|tpsl_txn_id=222623138|txn_amt=1.00|clnt_rqst_meta={itc:Saleel_K}{email:prod.provisioning.ind@ingenico.com}{mob:7045909557}{custid:01022024141959}{custname:RAM KUMAR MANDAL}|tpsl_txn_time=01-02-2024 14:20:09|tpsl_rfnd_id=NA|bal_amt=NA|rqst_token=D32EB896-10EB-4170-B088-560C9C04C084
                        string regid = get_regid(custid);
                        Session["regid"] = regid;
                        if (strPG_TxnStatus == "0300")
                        {
                            ViewState["statuS"] = "Paid";
                            lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;

                            

                            ViewState["statuS"] = "Paid";
                            My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + tpsl_txn_id + "', razorpay_order_id='" + clnt_txn_ref + "',status='Success',DecryptedVal='" + strDecryptedVal + "'  where  ordertrackingid='" + custid + "' and Admission_no='" + regid + "'");
                            Bind_data();




                        }
                        else if (strPG_TxnStatus == "0200")
                        {
                            ViewState["statuS"] = "Paid";
                            lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;

                            My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + tpsl_txn_id + "', razorpay_order_id='" + clnt_txn_ref + "',status='Success',DecryptedVal='" + strDecryptedVal + "'  where  ordertrackingid='" + custid + "' and Admission_no='" + regid + "'");
                            Bind_data();


                        }
                        else
                        {
                            strPGTxnString = strSplitDecryptedResponse[2].Split('=');
                            lblValidate.Text = "Transaction Fail " + "ERROR:" + strPGTxnString[1];

                            ViewState["statuS"] = "Failed";
                            My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + tpsl_txn_id + "', razorpay_order_id='" + clnt_txn_ref + "',status='Failed',DecryptedVal='" + strDecryptedVal + "'  where  ordertrackingid='" + custid + "' and Admission_no='" + regid + "'");


                            Bind_data();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

     

        My mycode = new My();
        private string get_regid(string Order_id)
        {
            DataTable dt = mycode.FillData("Select Admission_no from Payment_transaction_process where ordertrackingid='" + Order_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Admission_no"].ToString();
            }
        }

        static dynamic DeserializeDynamic(string jsonString)
        {
            var dictionary = new Dictionary<string, string>();

            // Split the string by '|' and '=' to extract key-value pairs
            string[] pairs = jsonString.Split('|');
            foreach (string pair in pairs)
            {
                string[] keyValue = pair.Split('=');
                dictionary[keyValue[0]] = keyValue[1];
            }

            // Convert the dictionary to a dynamic object using JSON.NET
            string json = JsonConvert.SerializeObject(dictionary);
            dynamic deserializedObject = JsonConvert.DeserializeObject<dynamic>(json);

            return deserializedObject;
        }


        public void GetPGRespnseData(string[] parameters)
        {

            string[] strGetMerchantParamForCompare;

            for (int i = 0; i < parameters.Length; i++)
            {
                strGetMerchantParamForCompare = parameters[i].ToString().Split('=');
                if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_STATUS")
                {
                    strPG_TxnStatus = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "CLNT_TXN_REF")
                {
                    strPG_ClintTxnRefNo = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_BANK_CD")
                {
                    strPG_TPSLTxnBankCode = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_ID")
                {
                    strPG_TPSLTxnID = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TXN_AMT")
                {
                    strPG_TxnAmount = Convert.ToString(strGetMerchantParamForCompare[1]);
                }
                else if (Convert.ToString(strGetMerchantParamForCompare[0]).ToUpper().Trim() == "TPSL_TXN_TIME")
                {
                    strPG_TxnDateTime = Convert.ToString(strGetMerchantParamForCompare[1]);
                    strArrPG_TxnDateTime = strPG_TxnDateTime.Split(' ');
                    strPG_TxnDate = Convert.ToString(strArrPG_TxnDateTime[0]);
                    strPG_TxnTime = Convert.ToString(strArrPG_TxnDateTime[1]);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Bind_data();



        }
        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        private void Bind_data()
        {
            try
            {
                DataTable dt = mycode.FillData("Select * from Payment_transaction_process where ordertrackingid='" + ViewState["ordertrackingid"].ToString() + "' ");
                if (dt.Rows.Count == 0)
                {
                    Response.Redirect("../Student_Monthly_Payment.aspx?regid=" + Session["regid"].ToString(), false);
                }
                else
                {
                    // synch_data();
                    if (ViewState["statuS"].ToString() == "Paid")
                    {
                        //paid process

                        bool status = mypayment.save_final_payment(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                        if (status == true)
                        {
                            Response.Redirect("../Payment_Success_Message.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);
                        }
                        else
                        {
                            Response.Redirect("../Payment_Error_Message.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);
                        }
                    }
                    else // failed/technila issues ocured
                    {
                        Response.Redirect("../Payment_Error_Message.aspx?orderid=" + dt.Rows[0]["ordertrackingid"].ToString() + "&Regid=" + dt.Rows[0]["Admission_no"].ToString() + "&payFrom=" + dt.Rows[0]["Pay_from"].ToString(), false);
                    }
                }
                System.Threading.Thread.Sleep(3000);
                a1.Visible = false;

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }

        }
    }
}