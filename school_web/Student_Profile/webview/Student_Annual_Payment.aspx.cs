using school_web.AppCode;
using school_web.Student_Profile.webview.EazyPay;
using school_web.Student_Profile.webview.Get_Epay;
using school_web.Student_Profile.webview.RazorPay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Student_Annual_Payment : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_date.Text = mycode.date();
                    Session["reprint_readmissionslip"] = "2";
                    ViewState["adjestamount"] = "0";

                    if (Request.QueryString["regid"] != null)
                    {
                        ViewState["admissionno"] = Request.QueryString["regid"].ToString();
                        ViewState["regid"] = Request.QueryString["regid"].ToString();
                        ViewState["Userid"] = ViewState["regid"].ToString();
                        ViewState["sessionid"] = My.get_session_id(); //My.get_session_from_student(ViewState["admissionno"].ToString());
                        get_student_details();
                        Dictionary<string, object> dc1 = My.get_selected_studentinfo(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), "1");
                        string Class_Id = (String)dc1["Class_id"];
                        ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type_monthly(Class_Id);
                        //check if student payment and payment has not reflected by razor
                        if (ViewState["Getwey_Type"].ToString() != "0")
                        {
                            bool status_paymnet = mycode.bind_not_paymnet_datat(ViewState["regid"].ToString(), ViewState["sessionid"].ToString());
                            if (status_paymnet == false)
                            {
                                a1.Visible = false;
                            }
                            else
                            {
                                a1.Visible = true;
                                automatic_payment();
                            }
                        }
                        else
                        {
                            a1.Visible = false;
                        }



                    }


                    try
                    {
                        string status_paymnet = My.Is_annul_admission_editbile();
                        if (status_paymnet == "0")
                        {
                            txt_paid_amount.Enabled = false;
                            tPayableDV1.Visible = false;
                            tPayableDV2.Visible = false;
                            ttlduesDV.Visible = false;
                        }
                        else
                        {
                            txt_paid_amount.Enabled = true;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Type_Master");
            }
        }
        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

        }
        private void get_student_details()
        {
            DataTable dt = mycode.FillData("select top 1 * from admission_registor  where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and Session_id = '" + ViewState["sessionid"].ToString() + "' and Status='1' order by id desc  ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                ViewState["Branchid"] = dt.Rows[0]["Branch_id"].ToString();
                ViewState["firm_id"] = "1";
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                lbl_admissionno.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_studentname.Text = dt.Rows[0]["studentname"].ToString();
                ViewState["studentname"] = dt.Rows[0]["studentname"].ToString();
                ViewState["Mobile_no"] = dt.Rows[0]["father_mob"].ToString();
                ViewState["email_id"] = dt.Rows[0]["email_id"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();
                lbl_class.Text = dt.Rows[0]["class"].ToString();
                ViewState["Section"] = dt.Rows[0]["Section"].ToString();
                ViewState["category_id"] = dt.Rows[0]["Category_id"].ToString();
                ViewState["SubCategory_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                ViewState["sessionid"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["session"] = dt.Rows[0]["session"].ToString();
                if (dt.Rows[0]["hosteltaken"].ToString() == "")
                {
                    ViewState["hostaltaken"] = "No";
                    lbl_student_type.Text = "Day Scholer";
                }
                else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                {
                    ViewState["hostaltaken"] = "No";
                    lbl_student_type.Text = "Day Scholer";
                }
                else
                {
                    lbl_student_type.Text = "Hostler";
                    ViewState["hostaltaken"] = dt.Rows[0]["hosteltaken"].ToString();
                }

                ViewState["Transfer_Status"] = dt.Rows[0]["Transfer_Status"].ToString();

                //if (ViewState["Transfer_Status"].ToString() == "New")
                //{
                //    ViewState["Feetype_new"] = "AdmissionFee";
                //}
                //else
                //{
                //    ViewState["Feetype_new"] = "ReadmissionFee";
                //    // lbl_heading.Text = "Re-Admission Fee Collect";
                //}

                Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ViewState["sessionid"].ToString(), ViewState["classid"].ToString(), ViewState["admissionno"].ToString());
                ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                ViewState["From_month_name"] = (String)dc1["From_month_name"];
                ViewState["From_month_id"] = (String)dc1["From_month_id"];
                ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];


                Bind_fee_details();
                find_all_paid_fee();
            }
        }



        private void Bind_fee_details()
        {
            string parameter = "";
            string parameter_id = "";

            string Discount_on = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "1" : "5";
                ViewState["parameter"] = parameter;
                Discount_on = "Admission";
                //parameter_id1 = "1";// Normal
                //parameter_id2 = "5";//Hostel
            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "2" : "6";
                ViewState["parameter"] = parameter;
                Discount_on = "Annual";

            }


            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + lbl_session.Text + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "' and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' )t";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + lbl_admissionno.Text + "'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "')t";
                }
                fee_dt = My.dataTable(qry);
            }

            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                grd_fee.DataSource = dt;
                grd_fee.DataBind();

                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                }
                get_previous_amount();
                double totalpay = payble_after_disc + My.toDouble(lbl_previous_dues.Text);
                lbl_paybaleamount.Text = totalpay.ToString("0.00");


                lbl_adjustamount.Text = payble_after_disc.ToString("0.00");
                txt_paid_amount.Text = payble_after_disc.ToString("0.00");
                lbl_totaldues.Text = "0.00";



                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");

                if (payble_after_disc <= 0)
                {
                    btn_Submit.Enabled = false;
                }
            }
        }
        double total_payable = 0, total_disc_amount = 0, total_perviously = 0, total_dues = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_payable");
                Label lbl_disc_amount = (Label)e.Row.FindControl("lbl_disc_amount");
                Label lbl_perviously_paid = (Label)e.Row.FindControl("lbl_perviously_paid");
                Label lbl_dues = (Label)e.Row.FindControl("lbl_dues");
                if (lbl_payable.Text != "")
                {
                    total_payable = total_payable + Convert.ToDouble(lbl_payable.Text);
                }

                if (lbl_disc_amount.Text != "")
                {
                    total_disc_amount = total_disc_amount + Convert.ToDouble(lbl_disc_amount.Text);
                }

                if (lbl_perviously_paid.Text != "")
                {
                    total_perviously = total_perviously + Convert.ToDouble(lbl_perviously_paid.Text);
                }

                if (lbl_dues.Text != "")
                {
                    total_dues = total_dues + Convert.ToDouble(lbl_dues.Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalfeeamount = (Label)e.Row.FindControl("lbl_totalfeeamount");
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");
                Label lbl_totalpreviously = (Label)e.Row.FindControl("lbl_totalpreviously");

                Label lbl_totalpaybale = (Label)e.Row.FindControl("lbl_totalpaybale");


                lbl_totalfeeamount.Text = total_payable.ToString("0.00");
                lbl_totaldiscount.Text = total_disc_amount.ToString("0.00");
                lbl_totalpreviously.Text = total_perviously.ToString("0.00");
                lbl_totalpaybale.Text = total_dues.ToString("0.00");
            }
        }



        private void get_previous_amount()
        {
            privius_head.Visible = false;
            privius_value.Visible = false;
            DataTable dt = mycode.FillData("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + ViewState["admissionno"].ToString() + "'  and Class_id='" + ViewState["classid"].ToString() + "' and Status='Unpaid'");
            if (dt.Rows.Count == 0)
            {
                lbl_previous_dues.Text = "0.00";
            }
            else
            {
                privius_head.Visible = true;
                privius_value.Visible = true;
                lbl_previous_dues.Text = dt.Rows[0][0].ToString();
            }
        }
        double total_amount = 0;
        protected void grid_payment_history_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Amount = (Label)e.Row.FindControl("lbl_Amount");

                if (lbl_Amount.Text != "")
                {
                    total_amount = total_amount + Convert.ToDouble(lbl_Amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");
                lbl_totaldiscount.Text = total_amount.ToString("0.00");
            }
        }

        private void find_all_paid_fee()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 session_id from session_details where Session=Student_Payment_History.Session) as session_id from Student_Payment_History  where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + ViewState["admissionno"].ToString() + "' and Type in('Annual','Admission')   ORDER BY id ASC");
            if (dt.Rows.Count == 0)
            {
                Label1.Visible = false;
                grid_payment_history.DataSource = null;
                grid_payment_history.DataBind();
            }
            else
            {
                Label1.Visible = true;
                grid_payment_history.DataSource = dt;
                grid_payment_history.DataBind();
            }
        }


        protected void txt_paid_amount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lbl_totaldues.Text = (My.toDouble(lbl_paybaleamount.Text) - (My.toDouble(txt_paid_amount.Text) + My.toDouble(ViewState["adjestamount"].ToString()))).ToString("0.00");
            }
            catch
            {
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            Session["regid"] = ViewState["admissionno"].ToString();
            if (txt_paid_amount.Text == "")
            {

                Alert("Please Enter Amount First...");

                txt_paid_amount.Focus();
                return;
            }
            else
            {
                if ((My.toDouble(txt_paid_amount.Text)) == 0)
                {

                    Alert("Sorry, you do not have any dues amount.");
                }
                else
                {


                    if (My.toDouble(lbl_paybaleamount.Text) >= (My.toDouble(txt_paid_amount.Text) + My.toDouble(ViewState["adjestamount"].ToString())))
                    {
                        if (My.toDouble(lbl_totaldues.Text) >= 0)
                        {

                        }
                        else
                        {


                        }
                    }

                    if (My.toDouble(lbl_adjustamount.Text) >= My.toDouble(txt_paid_amount.Text))
                    {

                        if (My.toDouble(lbl_totaldues.Text) >= 0)
                        {
                            // save_amount_onlinepayment();
                            Random r = new Random(DateTime.Now.Millisecond);
                            string order_id = DateTime.UtcNow.ToString("yyMMddHHMMss") + r.Next(12346, 48749);
                            double pay = My.toDouble(txt_paid_amount.Text);
                            SqlCommand cmd;
                            string query = "INSERT INTO Payment_transaction_process_Admission (Admission_no,Total_pay,Date,Time,Idate,ordertrackingid,status,Payment_type,Remarks,profile,Academic_Sem_or_Year_id,discount,totaldues,Branch_id,Name,Emailid,Mobileno,payFrom,Session_id,Session,Class_id,category_id,sub_category_id,hosteltaken,Section,Hostel_id) values (@Admission_no,@Total_pay,@Date,@Time,@Idate,@ordertrackingid,@status,@Payment_type,@Remarks,@profile,@Academic_Sem_or_Year_id,@discount,@totaldues,@Branch_id,@Name,@Emailid,@Mobileno,@payFrom,@Session_id,@Session,@Class_id,@category_id,@sub_category_id,@hosteltaken,@Section,@Hostel_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Admission_no", ViewState["admissionno"].ToString());
                            cmd.Parameters.AddWithValue("@Total_pay", pay.ToString("0.00"));
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@ordertrackingid", order_id);
                            cmd.Parameters.AddWithValue("@status", "Pending");
                            cmd.Parameters.AddWithValue("@Payment_type", ViewState["parameter"].ToString());
                            cmd.Parameters.AddWithValue("@Remarks", txt_remrks.Text);
                            cmd.Parameters.AddWithValue("@profile", "App");
                            cmd.Parameters.AddWithValue("@Academic_Sem_or_Year_id", ViewState["sessionid"].ToString());
                            cmd.Parameters.AddWithValue("@discount", hd_total_discount.Value);
                            cmd.Parameters.AddWithValue("@totaldues", lbl_totaldues.Text);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Name", ViewState["studentname"].ToString());
                            cmd.Parameters.AddWithValue("@Emailid", ViewState["email_id"].ToString());
                            cmd.Parameters.AddWithValue("@Mobileno", ViewState["Mobile_no"].ToString());
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                            cmd.Parameters.AddWithValue("@category_id", ViewState["category_id"].ToString());
                            cmd.Parameters.AddWithValue("@sub_category_id", ViewState["SubCategory_id"].ToString());
                            cmd.Parameters.AddWithValue("@hosteltaken", ViewState["hostaltaken"].ToString());
                            cmd.Parameters.AddWithValue("@payFrom", "1");

                            cmd.Parameters.AddWithValue("@Section", ViewState["Section"].ToString());
                            cmd.Parameters.AddWithValue("@Hostel_id", ViewState["Hostel_id"].ToString());

                            if (My.InsertUpdateData(cmd))
                            {
                                bool isonlinepymnet = My.get_status_ispaymnet_on(ViewState["classid"].ToString());
                                if (isonlinepymnet == true)
                                {
                                    Response.Redirect("payfinal_Admission.aspx?regid=" + ViewState["regid"].ToString() + "&orderid=" + order_id + "&payFrom=1", false);
                                    //Payment_update_after_onlinepayment_Admission mypayment = new Payment_update_after_onlinepayment_Admission();
                                    //bool status = mypayment.save_final_payment(ViewState["regid"].ToString(), order_id);
                                    //if (status == true)
                                    //{
                                    //    Response.Redirect("Payment_Success_Message_admission.aspx?orderid=" + order_id + "&Regid=" + ViewState["regid"].ToString() + "&payFrom=1" , false);
                                    //}
                                    //else
                                    //{
                                    //    Response.Redirect("Payment_Error_Message_admission.aspx?orderid=" + order_id + "&Regid=" + ViewState["regid"].ToString() + "&payFrom=1", false);
                                    //}
                                }
                                else
                                {
                                    Alert("Sorry! Online payment is not enabled");
                                }
                            }

                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        Payment_update_after_onlinepayment_Admission mypayment = new Payment_update_after_onlinepayment_Admission();
        private void automatic_payment()
        {
            string order_id = "";
            string Status = "";
            string Payment_order_id = "";
            string query = " select  top  1 * from dbo.[Payment_transaction_process_Admission] where Admission_no='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "' and status='Pending' order by Id desc ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {

                if (ViewState["Getwey_Type"].ToString() == "Razorpay")
                {
                    try
                    {
                        Dictionary<string, object> dc1 = Re_payment_check_payment.get_payment_status_Razorpay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["razorpay_order_id"].ToString());
                        order_id = (String)dc1["order_id"];
                        Status = (String)dc1["Status"];
                        Payment_order_id = (String)dc1["Payment_order_id"];
                    }
                    catch (Exception ex)
                    {
                        Status = "Unpaid";
                        order_id = dt.Rows[0]["razorpay_order_id"].ToString(); ;
                        Payment_order_id = dt.Rows[0]["ordertrackingid"].ToString();

                    }

                }
                else if (ViewState["Getwey_Type"].ToString() == "EGPay")
                {
                    Dictionary<string, object> dc1 = Re_payment_check_payment_EZpay.get_payment_status_EZ_Pay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    order_id = (String)dc1["order_id"];
                    Status = (String)dc1["Status"];
                    Payment_order_id = (String)dc1["Payment_order_id"];
                }
                else if (ViewState["Getwey_Type"].ToString() == "Getepay")
                {
                    Dictionary<string, object> dc1 = Re_payment_check_payment_Get_pay.get_payment_status_Get_pay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["razorpay_order_id"].ToString());
                    order_id = (String)dc1["order_id"];
                    Status = (String)dc1["Status"];
                    Payment_order_id = (String)dc1["Payment_order_id"];
                }

                if (Status == "Success")
                {

                    My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + Payment_order_id + "',status='Success'  where  razorpay_order_id='" + order_id + "' ");
                    bool status = mypayment.save_final_payment(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    if (status == true)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    My.exeSql("update Payment_transaction_process_Admission set razorpay_payment_id='" + Payment_order_id + "',status='Response Not Found'  where  razorpay_order_id='" + order_id + "'");
                }

            }
            a1.Visible = false;
        }
    }
}