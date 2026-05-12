using getepay_sdk;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace school_web.Student_Profile.webview.Get_Epay
{
    public class Re_payment_check_payment_Get_pay
    {
        internal static Dictionary<string, object> get_payment_status_Get_pay(string regid, string sessionid, string getpay_order_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            try
            {
               
                GetepayConfig config = new GetepayConfig();
                config.mid = My.Merchant_Key_Get_E_PAY(); //"108";
                config.terminalId = My.TerminalId_Get_E_PAY();// "Getepay.merchant61062@icici";
                config.key = My.Encryptionkey_Get_E_PAY();// "JoYPd+qso9s7T+Ebj8pi4Wl8i+AHLv+5UNJxA3JkDgY=";
                config.iv = My.IV_Get_E_PAY(); //"hlnuyA9b4YxDq6oJSZFl8g==";




                config.url = "https://pay1.getepay.in:8443/getepayPortal/pg/invoiceStatus";
                //local
                                                                                           //config.url = "https://portal.getepay.in:8443/getepayPortal/pg/invoiceStatus";// live
                                                                                           //Requery Reference
                GetepayRequery getepayRequery = new GetepayRequery();
                getepayRequery.paymentId = getpay_order_id;
                getepayRequery.terminalId = config.terminalId;
                getepayRequery.mid = config.mid;

                //added to solve ssl error for requery
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;


                GetepayRequeryResponse requeryResponse = Getepay.requeryRequest(config, getepayRequery);
                //Console.WriteLine("11", requeryResponse.txnStatus);

                string txnStatus = requeryResponse.txnStatus;
                string admission_no = requeryResponse.udf4;
                string paymentstatus = requeryResponse.paymentStatus;
                string order_tracking_id = requeryResponse.merchantOrderNo;
                if (txnStatus.ToUpper() == "SUCCESS")
                {
                    dc["order_id"] = getpay_order_id;
                    dc["Status"] = "Success";
                    dc["Payment_order_id"] = getpay_order_id;
                }
                else
                {
                    dc["order_id"] = getpay_order_id;
                    dc["Status"] = "0";
                    dc["Payment_order_id"] = "0";

                }

            }
            catch
            {
                dc["order_id"] = getpay_order_id;
                dc["Status"] = "0";
                dc["Payment_order_id"] = "0";

            }

            return dc;

        }
    }



}
