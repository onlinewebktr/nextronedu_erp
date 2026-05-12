using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Semester_Settlement : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Admin"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        ViewState["adjestamount"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        if (Request.QueryString["admissionno"] != null)
                        {
                            ViewState["admissionno"] = Request.QueryString["admissionno"];
                            ViewState["sessionid"] = Request.QueryString["sessionid"];
                            ViewState["classid"] = Request.QueryString["classid"];
                            ViewState["session"] = mycode.get_session(ViewState["sessionid"].ToString());
                        }
                        // mycode.executequery("Delete from Temp_Adjust_amount where Admission_no='" + ViewState["admissionno"].ToString() + "'");
                        get_student_details();
                        Bind_fee_details();
                        find_all_paid_fee();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Type_Master");
            }
        }




        private void get_student_details()
        {
            DataTable dt = mycode.FillData("select * from admission_registor  where Session_id='" + ViewState["sessionid"].ToString() + "' and admissionserialnumber='" + ViewState["admissionno"] + "' and Class_id='" + ViewState["classid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_admissionno.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_studentname.Text = dt.Rows[0]["studentname"].ToString();

                lbl_session.Text = dt.Rows[0]["session"].ToString();
                lbl_class.Text = dt.Rows[0]["class"].ToString();
                ViewState["Section"] = dt.Rows[0]["Section"].ToString();
                ViewState["category_id"] = dt.Rows[0]["Section"].ToString();
                ViewState["category_id"] = dt.Rows[0]["Category_id"].ToString();
                ViewState["SubCategory_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                ViewState["hostaltaken"] = dt.Rows[0]["hosteltaken"].ToString();
                ViewState["Transfer_Status"] = dt.Rows[0]["Transfer_Status"].ToString();
            }
        }



        private void Bind_fee_details()
        {
            string parameter_id = "";
            string Discount_on = "";
            //parameter_id='1'==Admission fee for new student
            //parameter_id='2'==Annual Fee fee for Old  student
            string parameter = "";

            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter = ViewState["hostaltaken"].ToString() == "No" ? "AdmissionFee" : "HostelAdmissionFee";
                parameter_id = "1";
                Discount_on = "Admission";



            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString() == "No" ? "AnnualFee" : "HostelAnnualFee";
                parameter_id = "2";
                Discount_on = "Annual";
            }

            string qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and parameter_id=" + parameter_id + " and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id=" + parameter_id + " and session='" + lbl_session.Text + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on=='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + lbl_session.Text + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + lbl_admissionno.Text + "'  and parameter_id=" + parameter_id + " and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on=='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + lbl_session.Text + "' and fee_head_id=cm.content_id and Discount_on=='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + lbl_session.Text + "' and class_id='" + ViewState["classid"].ToString() + "')t";
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
                gt_ptives_amount();
                double totalpay = payble_after_disc + My.toDouble(lbl_previous_dues.Text);
                lbl_paybaleamount.Text = totalpay.ToString("0.00");


                lbl_adjustamount.Text = payble_after_disc.ToString("0.00");
                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");
            }
        }

        private void gt_ptives_amount()
        {
            privius_head.Visible = false;
            privius_value.Visible = false;
            DataTable dt = mycode.FillData("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + ViewState["admissionno"].ToString() + "'  and Class_id='" + ViewState["classid"].ToString() + "'");
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

        private void find_all_paid_fee()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 session_id from session_details where Session=Student_Payment_History.Session) as session_id from Student_Payment_History  where Session='" + ViewState["session"].ToString() + "' and Addmission_no='" + ViewState["admissionno"] + "' and Type='Annual'   ORDER BY id ASC");
            if (dt.Rows.Count == 0)
            {
                grid_payment_history.DataSource = null;
                grid_payment_history.DataBind();
            }
            else
            {
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
        protected void txt_Settlementamount_TextChanged(object sender, EventArgs e)
        {
            lbl_totaldues.Text = (My.toDouble(lbl_paybaleamount.Text) - (My.toDouble(txt_paid_amount.Text) + My.toDouble(txt_Settlementamount.Text) + My.toDouble(ViewState["adjestamount"].ToString()))).ToString("0.00");
        }

        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }

        #region pay
        string type = "";
        public bool payment_status = false;
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_date.Text == "")
                {
                    payment_status = false;
                    Alertme("Please choose payment date.", "warning");
                    txt_date.Focus();
                    return;
                }

                if (txt_paid_amount.Text == "")
                {
                    payment_status = false;
                    Alertme("Please enter paid amount first...", "warning");
                    txt_paid_amount.Focus();
                    return;
                }

                int pay_idate = My.DateConvertToIdate(txt_date.Text);
                bool chek_fee = false;
                if (ViewState["Transfer_Status"].ToString() == "New")
                {

                    chek_fee = My.find_fee_taken_date("Annual_fee_collection", pay_idate, lbl_admissionno.Text, ViewState["session"].ToString());
                    type = "Admission";
                }
                else
                {
                    chek_fee = My.find_fee_taken_date("Admission_fee_collection", pay_idate, lbl_admissionno.Text, ViewState["session"].ToString());
                    type = "Annual";
                }
                if (chek_fee == false)
                {
                    Alertme("Payment is already done on your chosen date.", "warning");
                }
                else
                {

                    string url = "";

                    string slip_no = "";
                    string ad_no = lbl_admissionno.Text;
                    string entry_id = "AD" + cretesessionid();
                    if (slip_no == "")
                    {
                        slip_no = My.invoice_readmission("slip_no"); //My.auto_serial("slip_no"); 
                        //slip_no = My.invoice_format("slip_no"); //My.auto_serial("slip_no"); 
                    }
                    payment(slip_no, entry_id, ad_no, type);
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received " + type + " fee :-" + txt_paid_amount.Text + " Adjust Amount" + ViewState["adjestamount"].ToString() + " rs,Slip No :- " + slip_no + " from " + lbl_studentname.Text + ", Admission No :-" + ad_no);
                    payment_status = true;
                    if (payment_status == true)
                    {
                        //slip
                        if (type == "Annual")
                        {
                            url = "slip/annual-slip.aspx?admissionno=" + ViewState["admissionno"].ToString() + "&sessionid=" + ViewState["sessionid"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&Slip_no=" + slip_no + "";
                        }
                        else
                        {
                            url = "slip/Admission_slip.aspx?admissionno=" + ViewState["admissionno"].ToString() + "&sessionid=" + ViewState["sessionid"].ToString() + "&classid=" + ViewState["classid"].ToString() + "&Slip_no=" + slip_no + "";
                        }



                        Response.Redirect(url, false);
                    }
                }
            }
            catch
            {
            }
        }

        private string hostaltaken = "";
        private void payment(string slip_no, string entry_id, string ad_no, string type)
        {
            string Uid = slip_no;
            string Tag = txt_paid_amount.Text;
            string parameter_id = "";
            string parameter = "";

            parameter = ViewState["hostaltaken"].ToString() == "No" ? "AnnualFee" : "HostelAnnualFee";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter = ViewState["hostaltaken"].ToString() == "No" ? "AdmissionFee" : "HostelAdmissionFee";
                parameter_id = "1";
            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString() == "No" ? "AnnualFee" : "HostelAnnualFee";
                parameter_id = "2";

            }

            send_data_in_student_payment_history(type, slip_no, entry_id, ad_no);
            send_data_to_school_ledger(slip_no, entry_id);

            create_admission_annual_dues(parameter, parameter_id, type);

            send_data_in_feetypewise_collection(slip_no, entry_id, parameter);

            send_data_to_annual_fee_collection(slip_no, entry_id);

            update_Add_Student_Money_receipt();
            update_data_to_admission_registor();
        }

        private void update_Add_Student_Money_receipt()
        {
            int growcountS = grid_adjustamount.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Unique_id = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Unique_id");
                mycode.executequery("update Add_Student_Money_receipt set Status='Used',Used_date='" + txt_date.Text + "',Used_Idate='" + mycode.ConvertStringToiDate(txt_date.Text) + "',Used_Time='" + mycode.time() + "' where Unique_id='" + lbl_Unique_id.Text + "' ");
            }
        }

        private void update_data_to_admission_registor()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber = '" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (My.toDouble(lbl_totaldues.Text) <= 0)
                {
                    dr["payment_status"] = "Paid";
                }
                else if (My.toDouble(lbl_totaldues.Text) > 0)
                {
                    dr["payment_status"] = "Dues";
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void send_data_to_annual_fee_collection(string slip_no, string entry_id)
        {
            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%AnnualFee%' and feetype!='Previous Dues' ");
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = lbl_admissionno.Text;
                dr[2] = hd_totalamount.Value;
                dr[3] = "0";
                dr[4] = lbl_paybaleamount.Text;
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = txt_date.Text;
                dr[8] = ddl_paymentmode.Text;
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString();
                dr["idate"] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                dr["remark"] = txt_remrks.Text;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Slip_no"] = slip_no;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["branchid"].ToString();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);










        }


        private void send_data_in_feetypewise_collection(string slip_no, string entry_id, string parameter)
        {
            //double pyablemaount = My.toDouble(lbl_adjustamount.Text);
            double discount_per = 0;

            if (My.toDouble(txt_Settlementamount.Text) > 0)
            {
                double flat_disc_all = My.toDouble(txt_Settlementamount.Text);
                double gtotal_amt = My.toDouble(lbl_adjustamount.Text);
                discount_per = (flat_disc_all * 100) / gtotal_amt;
            }








            string class_id = ViewState["classid"].ToString();
            double paid_amount = My.toDouble(txt_paid_amount.Text);
            if (My.toDouble(lbl_previous_dues.Text) > 0)
            {




                string previusyear = "Previous Year";
                string previousyearcontent_id = "101";
                double paid = 0;
                double prevdues = My.toDouble(lbl_previous_dues.Text);
                double sattle_disc = Math.Round((prevdues * (discount_per / 100)), 2);
                if (paid_amount >= (prevdues - sattle_disc))
                {

                    paid = prevdues - sattle_disc;
                    //insert
                    paid_amount = paid_amount - My.toDouble(lbl_previous_dues.Text);

                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + lbl_admissionno.Text + "','" + lbl_class.Text + "','" + lbl_session.Text + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(lbl_previous_dues.Text).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + "0.00" + "','Paid','" + My.get_start_month() + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + sattle_disc + "','" + paid.ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')");


                    mycode.executequery("update Previous_Year_Dues set Status='Paid' where  Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + ViewState["admissionno"] + "' and Class_id='" + ViewState["classid"].ToString() + "' ");

                    string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + lbl_admissionno.Text + "','" + lbl_session.Text + "','" + ViewState["classid"].ToString() + "','" + ViewState["Section"] + "','" + parameter + "','" + My.toDouble(previusyear).ToString("0.00") + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + paid + "','" + slip_no + "','School','" + sattle_disc + "','','0','0','" + ViewState["branchid"].ToString() + "','0','" + txt_date.Text + "','" + mycode.ConvertStringToiDate(txt_date.Text) + "');";
                    My.exeSql(qry);
                }
                else
                {
                    paid = paid_amount;
                    //insert
                    double duesamount = My.toDouble(lbl_previous_dues.Text) - paid - sattle_disc;



                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + lbl_admissionno.Text + "','" + lbl_class.Text + "','" + lbl_session.Text + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(lbl_previous_dues.Text).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + duesamount.ToString("0.00") + "','Dues','" + My.get_start_month() + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + sattle_disc + "','" + paid.ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')");
                    mycode.executequery("update Previous_Year_Dues set Status='Dues' where Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + ViewState["admissionno"] + "' and Class_id='" + ViewState["classid"].ToString() + "' ");
                    paid_amount = 0;



                    string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + lbl_admissionno.Text + "','" + lbl_session.Text + "','" + ViewState["classid"].ToString() + "','" + ViewState["Section"] + "','" + parameter + "','" + previusyear + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','School','" + sattle_disc + "','','0','0','" + ViewState["branchid"].ToString() + "','0','" + txt_date.Text + "','" + mycode.ConvertStringToiDate(txt_date.Text) + "');";
                    My.exeSql(qry);
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and status='Dues' and parameter='" + parameter + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable tdt = ds.Tables[0];
            if (tdt.Rows.Count == 0)
            {

            }
            else
            {




                foreach (DataRow dr in tdt.Rows)
                {
                    string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";

                    double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                    double sattlement_disc = Math.Round((dues * (discount_per / 100)), 2);
                    if (sattlement_disc > 0)
                    {
                        dr["Disc"] = My.toDouble(dr["Disc"]) + sattlement_disc;
                    }
                    dr["Date"] = txt_date.Text;
                    dr["idate"] = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
                    if (paid_amount >= dues)
                    {
                        string prevpaid = My.toDouble(dr["paid"].ToString()).ToString("0.00");
                        paid_amount = paid_amount - dues;
                        //paid amt is gratter than dues so dues amt is actual paid.
                        string paid = My.toDouble(dr["dues"].ToString()).ToString("0.00");
                        dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                        dr["paid"] = My.toDouble(dr["Payable_after_disc"]);
                        dr["dues"] = "0";
                        dr["status"] = "Paid";
                        #region send in collection slip
                        send_data_in_fee_collection_slip(dr["payable"].ToString(), dr["paid"].ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid);
                        #endregion
                    }
                    else
                    {
                        string prevpaid = dr["paid"].ToString();
                        dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                        dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                        dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                        dr["status"] = "Dues";
                        paid_amount = 0;
                        #region send in collection slip
                        send_data_in_fee_collection_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid);
                        #endregion
                    }
                    dr["transection"] = slip_no;
                    dr["is_readyfor_sync"] = true;
                    dr["is_sync"] = false;
                    dr["group_id"] = group_id;
                    dr["class_id"] = class_id;

                }

                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }
















        }

        private void create_admission_annual_dues(string parameter, string parameter_id, string type)
        {
            if (My.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + lbl_admissionno.Text + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "'").Rows.Count == 0)
            {
                string query = "select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["admissionno"] + "'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + type + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + lbl_admissionno.Text + "','" + lbl_class.Text + "','" + dr["session"].ToString() + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + My.get_start_month() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')");
                    }
                }
            }
        }

        private void send_data_to_school_ledger(string transcation, string entry_id)
        {


            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Total Bill :-" + lbl_paybaleamount.Text + " Paid Amount :-  " + txt_paid_amount.Text + " Settlement Amount :- " + txt_Settlementamount.Text;




            dr["AllInput"] = My.toDouble(txt_paid_amount.Text).ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
            dr["Date"] = txt_date.Text;
            dr["TransactionId"] = transcation;
            dr["entry_id"] = entry_id;
            dr["session"] = ViewState["session"].ToString();
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = lbl_admissionno.Text;
            dr["branchid"] = ViewState["branchid"].ToString();

            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            // mony recipt
        }


        private void send_data_in_student_payment_history(string type, string slip_no, string entry_id, string ad_no)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Settlement_Amount,Settlement_Type) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,Settlement_Amount,Settlement_Type)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Date", txt_date.Text);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + lbl_studentname.Text + " Paid Amount : " + txt_paid_amount.Text + " /-" + " Settlement  Amount : " + txt_Settlementamount.Text + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", My.toDouble(txt_paid_amount.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", ddl_paymentmode.Text);
            // cmd.Parameters.AddWithValue("@discount", hd_total_discount.Value);
            cmd.Parameters.AddWithValue("@discount", My.toDouble(txt_Settlementamount.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Discoun_in_School", "0.00");
            cmd.Parameters.AddWithValue("@Discoun_in_Hostel", "0.00");
            cmd.Parameters.AddWithValue("@Discoun_in_Transport", "0.00");
            cmd.Parameters.AddWithValue("@fine", "0.00");
            cmd.Parameters.AddWithValue("@is_ofline_sync", 0);
            cmd.Parameters.AddWithValue("@Is_online_sync", 0);
            cmd.Parameters.AddWithValue("@is_update_in_online", 0);
            cmd.Parameters.AddWithValue("@Previous_admission_no", 0);
            cmd.Parameters.AddWithValue("@App_Transection_id", "");
            cmd.Parameters.AddWithValue("@time", mycode.time());
            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
            cmd.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
            cmd.Parameters.AddWithValue("@Remarks", txt_remrks.Text);
            cmd.Parameters.AddWithValue("@User_Slip_no", txt_slip_no.Text);
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", txt_trans_no.Text);
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", txt_trans_no.Text);

            cmd.Parameters.AddWithValue("@Settlement_Amount", My.toDouble(txt_Settlementamount.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Settlement_Type", "Yes");
            if (My.InsertUpdateData(cmd))
            {
                // money recpit
                int growcountS = grid_adjustamount.Rows.Count;
                for (int iS = 0; iS < growcountS; iS++)
                {
                    Label lbl_Unique_id = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Unique_id");
                    Label lbl_date = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_date");
                    Label lbl_Amount = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Amount");
                    Label lbl_idate = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_idate");
                    Label lbl_paymentmode = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Paymentmode");
                    Label lbl_Payment_id = (Label)grid_adjustamount.Rows[iS].FindControl("lbl_Payment_id");

                    SqlCommand cmd1;
                    string query1 = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Money_Receipt_Date,@Money_Receipt_Idate,@Unique_id,@Adjust_type)";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@Addmission_no", ad_no);
                    cmd1.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
                    cmd1.Parameters.AddWithValue("@Date", lbl_date.Text);
                    cmd1.Parameters.AddWithValue("@Idate", lbl_idate.Text);
                    cmd1.Parameters.AddWithValue("@Description", type + " fee collection for(Money Receipt) " + lbl_studentname.Text + " Paid Amount : " + lbl_Amount.Text + " /-");
                    cmd1.Parameters.AddWithValue("@Entry_id", entry_id);
                    cmd1.Parameters.AddWithValue("@Slip_no", slip_no);
                    cmd1.Parameters.AddWithValue("@Amount", My.toDouble(lbl_Amount.Text).ToString("0.00"));
                    cmd1.Parameters.AddWithValue("@Type", type);
                    cmd1.Parameters.AddWithValue("@mode", lbl_paymentmode.Text);
                    cmd1.Parameters.AddWithValue("@discount", "0");
                    cmd1.Parameters.AddWithValue("@Discoun_in_School", "0.00");
                    cmd1.Parameters.AddWithValue("@Discoun_in_Hostel", "0.00");
                    cmd1.Parameters.AddWithValue("@Discoun_in_Transport", "0.00");
                    cmd1.Parameters.AddWithValue("@fine", "0.00");
                    cmd1.Parameters.AddWithValue("@is_ofline_sync", 0);
                    cmd1.Parameters.AddWithValue("@Is_online_sync", 0);
                    cmd1.Parameters.AddWithValue("@is_update_in_online", 0);
                    cmd1.Parameters.AddWithValue("@Previous_admission_no", 0);
                    cmd1.Parameters.AddWithValue("@App_Transection_id", "");
                    cmd1.Parameters.AddWithValue("@time", mycode.time());
                    cmd1.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                    cmd1.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                    cmd1.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
                    cmd1.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                    cmd1.Parameters.AddWithValue("@User_Slip_no", txt_slip_no.Text);
                    cmd1.Parameters.AddWithValue("@Pay_mode_transaction_no", lbl_Payment_id.Text);
                    cmd1.Parameters.AddWithValue("@Money_Receipt_Date", lbl_date.Text);
                    cmd1.Parameters.AddWithValue("@Money_Receipt_Idate", lbl_idate.Text);
                    cmd1.Parameters.AddWithValue("@Unique_id", lbl_Unique_id.Text);
                    cmd1.Parameters.AddWithValue("@Adjust_type", "Adjust");
                    if (My.InsertUpdateData(cmd1))
                    {
                        SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "SchoolLedger");
                        DataTable dt = ds.Tables[0];
                        DataRow dr = dt.NewRow();
                        dr["Particulars"] = type + " Money Receipt";
                        dr["Discription"] = type + " Money Receipt " + lbl_studentname.Text + " Adm.No:-" + lbl_admissionno.Text + " Paid Amount :-  " + My.toDouble(lbl_Amount.Text).ToString("0.00");
                        dr["AllInput"] = My.toDouble(lbl_Amount.Text).ToString("0.00");
                        dr["AllOutput"] = "0";
                        dr["IDate"] = lbl_idate.Text;//Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                        dr["Date"] = lbl_date.Text;
                        dr["TransactionId"] = slip_no;
                        dr["entry_id"] = entry_id;
                        dr["session"] = ViewState["session"].ToString();
                        dr["Ledger_Type"] = "School";
                        dr["time"] = mycode.time();
                        dr["user_id"] = ViewState["Userid"].ToString();
                        dr["Addmission_no"] = lbl_admissionno.Text;
                        dr["branchid"] = ViewState["branchid"].ToString();
                        dr["Unique_id"] = lbl_Unique_id.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                    }
                }
            }
        }

        private string cretesessionid()
        {
            bool duplicate = false;
            string Slip_no = mycode.auto_serial("admfee_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Slip_no from dbo.[Student_Payment_History] where Slip_no='" + Slip_no + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Slip_no = mycode.auto_serial("admfee_id");
                }
            }
            return Slip_no;

        }
        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + ViewState["branchid"].ToString() + "','0','" + txt_date.Text + "','" + My.DateConvertToIdate(txt_date.Text) + "');";
            My.exeSql(qry);
        }
        #endregion

        protected void ddl_paymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_paymentmode.SelectedItem.Text == "Cash")
            {
                pnl_mode_t_nSS.Visible = false;
                pnl_mode_t_nS.Visible = false;
            }
            if (ddl_paymentmode.SelectedItem.Text == "Netbanking")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Transaction No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "Deposited In Bank")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Transaction No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "Sbdebit")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Transaction No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "Cheque")
            {
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Cheque No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "NEFT")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "UTR No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "Debitcard")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Transaction No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "Creditcard")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Transaction No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "Otherdcard")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Transaction No.";
            }
            if (ddl_paymentmode.SelectedItem.Text == "UPI")
            {
                pnl_mode_t_nSS.Visible = true;
                pnl_mode_t_nS.Visible = true;
                lbl_mode_trns_no.Text = "Transaction No.";
            }
        }


        #region adjustamount


        protected void btn_adjustamount_Click(object sender, EventArgs e)
        {
            if (txt_Uniqueno.Text == "")
            {
                Alertme("Please enter unique no", "warning");
            }
            else
            {
                string query = "Select * from Add_Student_Money_receipt where Unique_id='" + txt_Uniqueno.Text + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry you entred unique receipt no. is wrong", "warning");
                }
                else
                {
                    if (dt.Rows[0]["Status"].ToString() == "Pending")
                    {
                        SqlCommand cmd;
                        string query1 = "Select * from Temp_Adjust_amount where Unique_id='" + txt_Uniqueno.Text + "'";
                        DataTable dt1 = mycode.FillData(query1);
                        if (dt1.Rows.Count == 0)
                        {
                            string query2 = "INSERT INTO Temp_Adjust_amount (Branch_id,User_id,Admission_no,Amount,Unique_id,slipdate,slipIdate,Paymentmode,Payment_id) values (@Branch_id,@User_id,@Admission_no,@Amount,@Unique_id,@slipdate,@slipIdate,@Paymentmode,@Payment_id)";
                            cmd = new SqlCommand(query2);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Admission_no", lbl_admissionno.Text);
                            cmd.Parameters.AddWithValue("@Amount", My.toDouble(dt.Rows[0]["Amount"].ToString()).ToString("0.00"));
                            cmd.Parameters.AddWithValue("@Unique_id", txt_Uniqueno.Text);
                            cmd.Parameters.AddWithValue("@slipdate", dt.Rows[0]["Date"].ToString());
                            cmd.Parameters.AddWithValue("@slipIdate", dt.Rows[0]["Idate"].ToString());
                            cmd.Parameters.AddWithValue("@Paymentmode", dt.Rows[0]["Amount_mode"].ToString());
                            cmd.Parameters.AddWithValue("@Payment_id", dt.Rows[0]["payment_id"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                Bind_grid_team_adjustamount();
                            }
                        }
                        else
                        {
                            Alertme("Sorry you entred unique receipt no. is already added", "warning");
                        }

                    }
                    else
                    {
                        Alertme("Sorry you entred unique receipt no. is already used", "warning");
                    }
                }
            }
        }

        private void Bind_grid_team_adjustamount()
        {
            DataTable dt = mycode.FillData("select * from Temp_Adjust_amount  where  Admission_no='" + ViewState["admissionno"] + "'    ");
            if (dt.Rows.Count == 0)
            {
                grid_adjustamount.DataSource = null;
                grid_adjustamount.DataBind();
            }
            else
            {
                grid_adjustamount.DataSource = dt;
                grid_adjustamount.DataBind();
            }
        }

        double total_payableadjust = 0;
        protected void grid_adjustamount_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total_payableadjust = total_payableadjust + Convert.ToDouble(lbl_payable.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totaldiscount");

                lbl_totaldiscount.Text = total_payableadjust.ToString("0.00");
                double finalamount = Convert.ToDouble(lbl_paybaleamount.Text) - Convert.ToDouble(lbl_totaldiscount.Text);
                ViewState["adjestamount"] = lbl_totaldiscount.Text;
                lbl_adjustamount.Text = finalamount.ToString("0.00");
            }
        }
        #endregion




    }
}