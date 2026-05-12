using com.awl.MerchantToolKit;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.worldline
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
                        // txtAdd2.Text = objResMsgDTO1.AddField2;//Applicant Name
                        //txtAdd3.Text = objResMsgDTO1.AddField3;//email
                        // txtAdd4.Text = objResMsgDTO1.AddField4;//phone
                        //txtAdd5.Text = objResMsgDTO1.AddField5;//address1
                        //  txtAdd6.Text = objResMsgDTO1.AddField6;//city
                        //  txtAdd7.Text = objResMsgDTO1.AddField7;//state
                        //   txtAdd8.Text = objResMsgDTO1.AddField8;//zipcode
                        // txtZcode.Text = objResMsgDTO1.AuthZCode;//MID
                        ViewState["TransRefNo"] = objResMsgDTO1.PgMeTrnRefNo;//TrnRefNo
                        // txtAmount.Text = objResMsgDTO1.TrnAmt;//Amt
                        //  hd_requestdate.Value = objResMsgDTO1.TrnReqDate;//Date
                        ViewState["rrn"] = objResMsgDTO1.Rrn;//
                        ViewState["orderno"] = objResMsgDTO1.OrderId;//orderid
                        ViewState["Status"] = objResMsgDTO1.StatusCode;//statuscode
                        ViewState["status_des"] = objResMsgDTO1.StatusDesc;//StatusDesc
                        ViewState["Description"] = objResMsgDTO1.ResponseCode;//esponseCode


                        // ViewState["meTrnStatusReq"] = objResMsgDTO1.meTrnStatusReq;//esponseCode

                        My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + ViewState["TransRefNo"].ToString() + "',razorpay_order_id='" + ViewState["rrn"].ToString() + "',razorpay_signature='" + ViewState["Description"].ToString() + "',status='Paid' where Admission_no='" + ViewState["Admission_no"].ToString() + "' and ordertrackingid='" + ViewState["orderno"].ToString() + "'");
                        ViewState["statuS"] = "Paid";
                        btn_send_data();

                    }
                    else
                    {


                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);
                        Response.Redirect("../Monthly_payment_student.aspx", false);
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
                Response.Redirect("../Monthly_payment_student.aspx", false);
            }

        }
        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
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

                bool status = mypayment.save_final_payment(ViewState["Admission_no"].ToString(), ViewState["orderno"].ToString());
                if (status == true)
                {

                    Response.Redirect("Payment_Success_Message.aspx?orderid=" + ViewState["orderno"].ToString() + "&Regid=" + ViewState["Admission_no"].ToString(), false);
                }
                else
                {
                    Response.Redirect("Payment_Error_Message.aspx?orderid=" + ViewState["orderno"].ToString() + "&Regid=" + ViewState["Admission_no"].ToString(), false);
                }
            }
            else// failed/technila issues ocured
            {
                Response.Redirect("Payment_Error_Message.aspx?orderid=" + ViewState["orderno"].ToString() + "&Regid=" + ViewState["Admission_no"].ToString(), false);
            }
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }

    }
}