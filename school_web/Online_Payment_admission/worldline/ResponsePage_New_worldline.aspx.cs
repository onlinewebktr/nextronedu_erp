using DotNetIntegrationKit;
using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Payment_admission.worldline
{
    public partial class ResponsePage_New_worldline : System.Web.UI.Page
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
                        //txn_status=0300|txn_msg=success|txn_err_msg=NA|clnt_txn_ref=01022024141959|tpsl_bank_cd=470|tpsl_txn_id=222623138|txn_amt=1.00|clnt_rqst_meta={itc:Saleel_K}{email:prod.provisioning.ind@ingenico.com}{mob:7045909557}{custid:01022024141959}{custname:RAM KUMAR MANDAL}|tpsl_txn_time=01-02-2024 14:20:09|tpsl_rfnd_id=NA|bal_amt=NA|rqst_token=D32EB896-10EB-4170-B088-560C9C04C084
                        string regid = get_regid(custid);
                        if (strPG_TxnStatus == "0300")
                        {
                            ViewState["statuS"] = "Paid";
                            lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;

                            My.exeSql("update Online_Admission set razorpay_payment_id='" + tpsl_txn_id + "',razorpay_order_id='" + clnt_txn_ref+ "',Payment_Status='Paid',Steps_done='10',DecryptedVal='"+ strDecryptedVal + "' where   Order_id='" + custid + "'");
                           

                            Response.Redirect("../../print/Print_Page.aspx?PaymentSliP=" + regid, false);


                        }
                        else if (strPG_TxnStatus == "0200")
                        {
                            ViewState["statuS"] = "Paid";
                            lblValidate.Text = "Transaction Success " + strPGTxnStatusCode;
                            My.exeSql("update Online_Admission set razorpay_payment_id='" + clnt_txn_ref + "',razorpay_order_id='" + tpsl_txn_id + "',Payment_Status='Paid',Steps_done='10',DecryptedVal='" + strDecryptedVal + "' where   Order_id='" + custid + "'");

                           
                            Response.Redirect("../../print/Print_Page.aspx?PaymentSliP=" + regid, false);
                        }
                        else
                        {
                            strPGTxnString = strSplitDecryptedResponse[2].Split('=');
                            lblValidate.Text = "Transaction Fail " + "ERROR:" + strPGTxnString[1];

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);

                            Response.Redirect("../../preview.aspx?regNo="+ regid, false);
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
            DataTable dt = mycode.FillData("Select Registration_id from Online_Admission where Order_id='" + Order_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Registration_id"].ToString();
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
    }
}