using com.awl.MerchantToolKit;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview.worldline
{
    public partial class Response_admission : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    string merchantResponse = Request.Form["merchantResponse"];
                    string key = My.Autkeyrozorkey_Get_Worldline();
                    AWLMEAPI transact = new AWLMEAPI();
                    ResMsgDTO objResMsgDTO1 = transact.parseTrnResMsg(merchantResponse, key);
                    // string test = "S";
                    if (objResMsgDTO1.StatusCode == "S")
                    {

                        //if (test == "S")
                        //{


                        //ViewState["Admission_no"] = "6317";//reg_id
                        //ViewState["TransRefNo"] = "309408999239";//TrnRefNo
                        //ViewState["rrn"] = "309408999239";//
                        //ViewState["orderno"] = "23040402045324663";//orderid
                        //ViewState["Status"] = "Test12365545";//statuscode
                        //ViewState["status_des"] = "309408999239";//StatusDesc
                        //ViewState["Description"] = "309408999239";//esponseCode
                        //ViewState["payFrom"] = "1";

                        //---------------------Live data-------------------
                        ViewState["Admission_no"] = objResMsgDTO1.AddField1;//reg_id
                        ViewState["TransRefNo"] = objResMsgDTO1.PgMeTrnRefNo;//TrnRefNo
                        ViewState["rrn"] = objResMsgDTO1.Rrn;//
                        ViewState["orderno"] = objResMsgDTO1.OrderId;//orderid
                        ViewState["Status"] = objResMsgDTO1.StatusCode;//statuscode
                        ViewState["status_des"] = objResMsgDTO1.StatusDesc;//StatusDesc
                        ViewState["Description"] = objResMsgDTO1.ResponseCode;//esponseCode
                        ViewState["payFrom"] = objResMsgDTO1.AddField5;


                        //ViewState["meTrnStatusReq"] = objResMsgDTO1.MeTrnStatusReq;//esponseCode

                        My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + ViewState["TransRefNo"].ToString() + "',razorpay_order_id='" + ViewState["rrn"].ToString() + "',razorpay_signature='" + ViewState["Description"].ToString() + "',status='Paid' where Admission_no='" + ViewState["Admission_no"].ToString() + "' and ordertrackingid='" + ViewState["orderno"].ToString() + "'");
                        ViewState["statuS"] = "Paid";
                        btn_send_data();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction Process Cancelled Please Try Again.')", true);
                        try
                        {
                            if (ViewState["payFrom"].ToString() == "1")
                            {
                                // mobile
                                Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);
                            }
                            else
                            {

                                // paymnet website
                                Response.Redirect("../../student-Annual-payment.aspx", false);
                            }
                        }
                        catch
                        {

                        }

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
                if (ViewState["payFrom"].ToString() == "1")
                {
                    Response.Redirect("../Student_Annual_Payment.aspx?regid=" + Session["regid"].ToString(), false);
                }
                else
                {
                    Response.Redirect("../../student-Annual-payment.aspx", false);
                }
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
        Payment_update_after_onlinepayment_Admission mypayment = new Payment_update_after_onlinepayment_Admission();

        private void btn_send_data()
        {
            if (ViewState["statuS"].ToString() == "Paid")
            {
                //paid process

                bool status = mypayment.save_final_payment(ViewState["Admission_no"].ToString(), ViewState["orderno"].ToString());
                if (status == true)
                {
                    Response.Redirect("../Payment_Success_Message_admission.aspx?orderid=" + ViewState["orderno"].ToString() + "&Regid=" + ViewState["Admission_no"].ToString() + "&payFrom=" + ViewState["payFrom"].ToString(), false);
                }
                else
                {
                    Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + ViewState["orderno"].ToString() + "&Regid=" + ViewState["Admission_no"].ToString() + "&payFrom=" + ViewState["payFrom"].ToString(), false);
                }
            }
            else // failed/technila issues ocured
            {
                Response.Redirect("../Payment_Error_Message_admission.aspx?orderid=" + ViewState["orderno"].ToString() + "&Regid=" + ViewState["Admission_no"].ToString() + "&payFrom=" + ViewState["payFrom"].ToString(), false);
            }
            System.Threading.Thread.Sleep(3000);
            a1.Visible = false;
        }




    }
}