using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using com.awl.MerchantToolKit;

namespace school_web.Online_Payment_admission.worldline
{
    public partial class Response : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    string merchantResponse = Request.Form["merchantResponse"];
                    string key = My.autkeyrozorkey();
                    AWLMEAPI transact = new AWLMEAPI();
                    ResMsgDTO objResMsgDTO1 = transact.parseTrnResMsg(merchantResponse, key);
                    if (objResMsgDTO1.StatusCode == "S")
                    {
                        ViewState["Admission_no"] = objResMsgDTO1.AddField1;//reg_id
                        ViewState["TransRefNo"] = objResMsgDTO1.PgMeTrnRefNo;//TrnRefNo
                        ViewState["rrn"] = objResMsgDTO1.Rrn;//
                        ViewState["orderno"] = objResMsgDTO1.OrderId;//orderid
                        ViewState["Status"] = objResMsgDTO1.StatusCode;//statuscode
                        ViewState["status_des"] = objResMsgDTO1.StatusDesc;//StatusDesc
                        ViewState["Description"] = objResMsgDTO1.ResponseCode;//esponseCode
                        ViewState["payFrom"] = objResMsgDTO1.AddField5;


                        // ViewState["meTrnStatusReq"] = objResMsgDTO1.meTrnStatusReq;//esponseCode

                        My.exeSql("update Online_Admission set razorpay_payment_id='" + ViewState["TransRefNo"].ToString() + "',razorpay_order_id='" + ViewState["rrn"].ToString() + "',razorpay_signature='" + ViewState["Description"].ToString() + "',Payment_Status='Paid' where Registration_id='" + ViewState["Admission_no"].ToString() + "' and Order_id='" + ViewState["orderno"].ToString() + "'");
                        ViewState["statuS"] = "Paid";
                        btn_send_data();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);

                        Response.Redirect("../../registration-guidelines.aspx", false);

                    }
                }
                else
                {
                }

            }
            catch (Exception ex)
            {

                My.submitException(ex, "payment_respons for worldline");


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('There Was Some Error Processing.....Please Check The Data you have Entered')", true);
                Response.Redirect("../../registration-guidelines.aspx", false);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // synch_data();

                btn_send_data();


            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        private void btn_send_data()
        {
            if (ViewState["statuS"].ToString() == "Paid")
            {
                //paid process
                string qry = "update Online_Admission set Steps_done='10' where Registration_id='" + ViewState["Admission_no"].ToString() + "'";
                mycode.executequery(qry);
                Response.Redirect("../../print/Print_Page.aspx?PaymentSliP=" + ViewState["Admission_no"].ToString(), false);


            }
            else // failed/technila issues ocured
            {
                Response.Redirect("../../registration-guidelines.aspx", false);
            }
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }






    }
}