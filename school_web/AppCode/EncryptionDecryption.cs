using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;

namespace SabPaisaDotNetIntregreation
{
    public class SabPaisaRequest
    {
        private string m_clientCode;
        private string m_transUserName;
        private string m_transUserPassword;
        private string m_authKey;
        private string m_authIV;
        private string m_payerName;
        private string m_payerEmail;
        private string m_payerMobile;
        private string m_payerAddress;
        private string m_clientTxnId;
        private string m_amount;
        private string m_amountType;
        private string m_channelId;
        private string m_mcc;
        private string m_callbackUrl;
        private string m_Class;
        private string m_Roll;
        private string m_Session;
        private string m_Admission_no;
        private string m_Payment_type;
        
        public string clientCode
        {
            set
            {
                m_clientCode = value;
            }
            get
            {
                return m_clientCode;
            }
        }
        public string transUserName
        {
            set
            {
                m_transUserName = value;
            }
            get
            {
                return m_transUserName;
            }
        }
        public string transUserPassword
        {
            set
            {
                m_transUserPassword = value;
            }
            get
            {
                return m_transUserPassword;
            }
        }
        public string authKey
        {
            set
            {
                m_authKey = value;
            }
            get
            {
                return m_authKey;
            }
        }
        public string authIV
        {
            set
            {
                m_authIV = value;
            }
            get
            {
                return m_authIV;
            }
        }
        public string payerName
        {
            set
            {
                m_payerName = value;
            }
            get
            {
                return m_payerName;
            }
        }
        public string payerEmail
        {
            set
            {
                m_payerEmail = value;
            }
            get
            {
                return m_payerEmail;
            }
        }
        public string payerMobile
        {
            set
            {
                m_payerMobile = value;
            }
            get
            {
                return m_payerMobile;
            }
        }
        public string payerAddress
        {
            set
            {
                m_payerAddress = value;
            }
            get
            {
                return m_payerAddress;
            }
        }
        public string clientTxnId
        {
            set
            {
                m_clientTxnId = value;
            }
            get
            {
                return m_clientTxnId;
            }
        }
        public string amount
        {
            set
            {
                m_amount = value;
            }
            get
            {
                return m_amount;
            }
        }
        public string amountType
        {
            set
            {
                m_amountType = value;
            }
            get
            {
                return m_amountType;
            }
        }
        public string channelId
        {
            set
            {
                m_channelId = value;
            }
            get
            {
                return m_channelId;
            }
        }
        public string mcc
        {
            set
            {
                m_mcc = value;
            }
            get
            {
                return m_mcc;
            }
        }
        public string callbackUrl
        {
            set
            {
                m_callbackUrl = value;
            }
            get
            {
                return m_callbackUrl;
            }
        }
        public string Class
        {
            set
            {
                m_Class = value;
            }
            get
            {
                return m_Class;
            }
        }
        public string Roll
        {
            set
            {
                m_Roll = value;
            }
            get
            {
                return m_Roll;
            }
        }


        


        public string Session
        {
            set
            {
                m_Session = value;
            }
            get
            {
                return m_Session;
            }
        }
        public string Admission_no
        {
            set
            {
                m_Admission_no = value;
            }
            get
            {
                return m_Admission_no;
            }
        }
        public string Payment_type
        {
            set
            {
                m_Payment_type = value;
            }
            get
            {
                return m_Payment_type;
            }
        }
        public Hashtable QueryValue()
        {
            Hashtable hst = new Hashtable();

            hst.Add(transUserPassword, "arvimd");
            return hst;
        }

    }

    public class SabPaisaIntegration
    {

        public string forwardToSabPaisa(SabPaisaRequest sabPaisaMember)
        {
            string query = "";
            Page page = new Page();
            query = query + "?clientCode=" + sabPaisaMember.clientCode.Trim() + "";
            query = query + "&transUserName=" + sabPaisaMember.transUserName.Trim() + "";
            query = query + "&transUserPassword=" + sabPaisaMember.transUserPassword.Trim() + "";
            query = query + "&authKey=" + sabPaisaMember.authKey.Trim() + "";
            query = query + "&authIV=" + sabPaisaMember.authIV.Trim() + "";
            query = query + "&payerName=" + sabPaisaMember.payerName.Trim() + "";
            query = query + "&payerEmail=" + sabPaisaMember.payerEmail.Trim() + "";
            query = query + "&payerMobile=" + sabPaisaMember.payerMobile.Trim() + "";
            query = query + "&payerAddress=" + sabPaisaMember.payerAddress.Trim() + "";
            query = query + "&clientTxnId=" + sabPaisaMember.clientTxnId.Trim() + "";
            query = query + "&amount=" + sabPaisaMember.amount.Trim() + "";
            query = query + "&amountType=" + sabPaisaMember.amountType.Trim() + "";
            query = query + "&channelId=" + sabPaisaMember.channelId.Trim() + "";
            query = query + "&mcc=" + sabPaisaMember.mcc.Trim() + "";
            query = query + "&callbackUrl=" + sabPaisaMember.callbackUrl.Trim() + "";
            // Extra Parameter use udf1 to udf20
            query = query + "&udf1=" + sabPaisaMember.Class.Trim() + "";
            query = query + "&udf2=" + sabPaisaMember.Roll.Trim() + "";
            query = query + "&udf3=" + sabPaisaMember.Session.Trim() + "";
            query = query + "&udf4=" + sabPaisaMember.Admission_no.Trim() + "";
            query = query + "&udf5=" + sabPaisaMember.Payment_type.Trim() + "";
            
            query = EncryptString(query, sabPaisaMember.authIV, sabPaisaMember.authKey);

            return query;
        }// sabPaisaIntregration end

        public Dictionary<string, string> subPaisaResponse(string query, string authIV, string authKey)
        {
            string decQuery = "";
            query.Replace("%2B", "+");

            decQuery = DecryptString(query, authIV, authKey);
            Dictionary<string, string> dictParams = new Dictionary<string, string>();

            dictParams = quearyParser(decQuery);

            /*foreach (KeyValuePair<string, string> pair in dictParams)
		    {
			    Console.WriteLine(pair.Key.ToString ()+ "  -  "  + pair.Value.ToString () );
		    }*/
            return dictParams;
        }

        public static string EncryptString(string spURL, string AuthIV, string AuthKey)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                ICryptoTransform e = GetCryptoTransform(csp, true, AuthIV, AuthKey);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(spURL);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

                string encrypted = Convert.ToBase64String(output);

                return encrypted;
            }
        }

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting, String AuthIV, String AuthKey)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            String iv = AuthIV;
            String AESKey1 = AuthKey;
            csp.IV = Encoding.UTF8.GetBytes(iv);
            byte[] inputBuffer = Encoding.UTF8.GetBytes(AESKey1);
            csp.Key = Encoding.UTF8.GetBytes(AESKey1);
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }

        public static string DecryptString(string encrypted, string AuthIV, string AuthKey)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                // encrypted = encrypted.Substring(0, encrypted.IndexOf("&"));
                var d = GetCryptoTransform(csp, false, AuthIV, AuthKey);
                byte[] output = Convert.FromBase64String(encrypted);

                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);
                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }

        private static Dictionary<string, string> quearyParser(String values)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //String values = "pgRespCode=F&PGTxnNo=37811436&SabPaisaTxId=7399702091711511122664015&issuerRefNo=NA&authIdCode=0&amount=57.0&clientTxnId=TESTING020917115040588&firstName=TPK&lastName=Test&payMode=CreditCards&email=test@gmail.com&mobileNo=9908944111&spRespCode=0000&cid=null&bid=null&clientCode=CXY10&payeeProfile=Student&transDate=Sat Sep 02 11:55:00 IST 2017&spRespStatus=success¶m3=BE&challanNo=&reMsg=null&orgTxnAmount=55.0&programId=mtech";

            string[] sites = values.Split('&');
            String[] token;

            foreach (string s in sites)
            {
                token = s.Split('=');
                dict.Add(token.GetValue(0).ToString(), token.GetValue(1).ToString());
            }
            return dict;
        }

        public int randomTxnId()
        {
            Random random = new Random();
            int num = random.Next();
            Console.WriteLine("No >> " + num);
            return num;
        }
    }
}//namesapaceEnd
