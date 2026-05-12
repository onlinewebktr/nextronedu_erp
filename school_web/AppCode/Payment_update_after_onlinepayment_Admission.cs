using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class Payment_update_after_onlinepayment_Admission
    {
        My mycode = new My();
        internal bool save_final_payment(string regid, string orderid)
        {
            bool toretun = false;
            try
            { 
                Dictionary<string, object> dc1 = getstudentinfo(regid, orderid);
                string Name = (String)dc1["Name"];
                string Class_id = (String)dc1["Class_id"];
                string Session_id = (String)dc1["Session_id"];
                string Session = (String)dc1["Session"];
                string Total_pay = (String)dc1["Total_pay"];
                string Payment_type = (String)dc1["Payment_type"];
                string category_id = (String)dc1["category_id"];
                string sub_category_id = (String)dc1["sub_category_id"];
                string Date = (String)dc1["Date"];
                string pay_idate1 = (String)dc1["Idate"];
                string hostaltaken = (String)dc1["hosteltaken"];
                int pay_idate = Convert.ToInt32(pay_idate1);
                string Branch_id = (String)dc1["Branch_id"];
                string Section = (String)dc1["Section"];
                string Hostel_id = (String)dc1["Hostel_id"]; 
                string razorpay_payment_id = (String)dc1["razorpay_payment_id"];
                string classname = My.get_class_name_to_from_class_id(Class_id);
                Bind_fee_details(Payment_type, Session, Session_id, Class_id, regid, category_id, sub_category_id, Total_pay, Date, Name, Branch_id, razorpay_payment_id, Section, orderid,"Online", hostaltaken, Hostel_id, "App", classname);
                toretun = true;
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Payment_update_after_onlinepayment_Admission");
            }
            return toretun;
        }
        public Dictionary<string, object> getstudentinfo(string regid, string orderid)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select   * from Payment_transaction_process_Admission   where Admission_no='" + regid + "' and ordertrackingid='" + orderid + "'";
            SqlDataAdapter ad = new SqlDataAdapter(quiry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "UserRegistrationMaster");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count; 
            if (rowcount == 0)
            {
                // go_to_Incubators_user(mobileno); 
                dc["Name"] = "0";
                dc["Admission_no"] = "0";
                dc["Session"] = "0";
                dc["Class_id"] = "0";
                dc["Session_id"] = "0";
                dc["Total_pay"] = "0";
                dc["status"] = "0";
                dc["Payment_type"] = "0";
                dc["category_id"] = "0";
                dc["sub_category_id"] = "0";
                dc["Date"] = "0";
                dc["Idate"] = "0";
                dc["razorpay_payment_id"] = "0";
                dc["hosteltaken"] = "0";
                dc["Section"] = "0";


            }
            else
            {
                dc["Name"] = dt.Rows[0]["Name"].ToString();
                dc["Admission_no"] = dt.Rows[0]["Admission_no"].ToString();
                dc["Session"] = dt.Rows[0]["Session"].ToString();
                dc["Class_id"] = dt.Rows[0]["Class_id"].ToString();
                dc["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                dc["Total_pay"] = dt.Rows[0]["Total_pay"].ToString();
                dc["status"] = dt.Rows[0]["status"].ToString();
                dc["Payment_type"] = dt.Rows[0]["Payment_type"].ToString();
                dc["category_id"] = dt.Rows[0]["category_id"].ToString();
                dc["sub_category_id"] = dt.Rows[0]["sub_category_id"].ToString();
                dc["Date"] = dt.Rows[0]["Date"].ToString();
                dc["Idate"] = dt.Rows[0]["Idate"].ToString();
                dc["razorpay_payment_id"] = dt.Rows[0]["razorpay_payment_id"].ToString();
                dc["hosteltaken"] = dt.Rows[0]["hosteltaken"].ToString();

                dc["Branch_id"] = dt.Rows[0]["Branch_id"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                dc["payFrom"] = dt.Rows[0]["payFrom"].ToString();
                dc["Hostel_id"] = dt.Rows[0]["Hostel_id"].ToString();


            }
            return dc;
        }


        public void Bind_fee_details(string Payment_type, string Session, string Session_id, string Class_id, string regid, string category_id, string sub_category_id, string Total_pay, string Date, string Name, string Branch_id, string razorpay_payment_id, string Section, string orderid, string paymentmode, string hostaltaken, string Hostel_id, string paymentfrom,string classname)

        {
            string parameter = Payment_type;
            string parameter_id = "";

            string Discount_on = "";
            string type = "";
            if (Payment_type == "AnnualFee" || Payment_type == "HostelAnnualFee")
            {
                parameter = hostaltaken.ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";

                parameter_id = hostaltaken.ToUpper().ToUpper() == "NO" ? "2" : "6";
                type = "Annual";
                Discount_on = "Annual";

            }
            else
            {
                parameter_id = hostaltaken.ToUpper() == "NO" ? "1" : "5";
                type = "Admission";
                Discount_on = "Admission";

            }

            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + regid + "'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + regid + "' and parameter='" + parameter + "' and session='" + Session + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (Hostel_id == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + regid + "'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + sub_category_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + Session + "' and class_id='" + Class_id + "' )t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable  from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + Session + "' and Admission_No='" + regid + "'";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + Hostel_id + " and admission_no='" + regid + "'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + Session + "' and class_id='" + Class_id + "' and fmc.Hostel_Id='" + Hostel_id + "')t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable  from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + Session + "' and Admission_No='" + regid + "'";
                }
                fee_dt = My.dataTable(qry);
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
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
                string previous_dues = get_previous_amount(regid, Session_id, Class_id);
                double totalpay = payble_after_disc + My.toDouble(previous_dues);
                string paybaleamount = totalpay.ToString("0.00");
                string adjustamount = payble_after_disc.ToString("0.00");
                string totalamount = payable.ToString("0.00");
                string total_discount = disc.ToString("0.00");
                string entry_id = "";
                string slip_no = "";
                if (Payment_type == "AnnualFee" || Payment_type == "HostelAnnualFee")
                {
                    entry_id = "AD" + cretesessionid();
                    slip_no = My.invoice_readmission("slip_no");
                }
                else
                {
                    entry_id = "AD" + cretesessionid();
                    slip_no = My.invoice_format_admssion("slip_no");

                }

                string Uid = slip_no;
                string Tag = Total_pay;



                send_data_in_student_payment_history(Payment_type, Session, Session_id, Class_id, regid, category_id, sub_category_id, Total_pay, Date, type, Name, slip_no, entry_id, total_discount, Branch_id, razorpay_payment_id, paymentmode, paymentfrom);

                create_admission_annual_dues(Payment_type, Session, Session_id, Class_id, regid, category_id, sub_category_id, Total_pay, Date, type, Name, slip_no, entry_id, total_discount, Branch_id, razorpay_payment_id, Section, hostaltaken, Hostel_id, classname);

                send_data_in_feetypewise_collection(Payment_type, Session, Session_id, Class_id, regid, category_id, sub_category_id, Total_pay, Date, type, Name, slip_no, entry_id, total_discount, Branch_id, razorpay_payment_id, Section, previous_dues, hostaltaken, Hostel_id);

                send_data_to_annual_fee_collection(Payment_type, Session, Session_id, Class_id, regid, category_id, sub_category_id, Total_pay, Date, type, Name, slip_no, entry_id, total_discount, Branch_id, razorpay_payment_id, Section, previous_dues, totalamount, paybaleamount);

                mycode.executequery("update Payment_transaction_process_Admission set Slip_id='" + slip_no + "',status='Success' where ordertrackingid='" + orderid + "'");

                final_update_admission(Session, Class_id, regid, adjustamount, Total_pay);


                try
                {
                    string input = Date;  // DD/MM/YYYY
                    string FNsession = My.GetFinancialSessionFromString(input);
                    string unique_entry_id = My.unique_id();
                    string VoucherNo = slip_no;
                    string feeType = "Student Fee Payment";
                    double amountpaid = My.toDouble(Total_pay);
                    string VoucherType = "Receipt";
                    string Description = "Fee collection from " + Name + " Amount : " + amountpaid + "/-";
                    string PayDate = Date + " " + mycode.time();
                    int Idate = My.DateConvertToIdate(Date);
                    string alternetacc_id = regid;
                    string session_name = FNsession;

                    bool checkbiilentery = My.check_dup_bill_no_entry(VoucherNo, session_name);
                    if (checkbiilentery == true)
                    {
                        string toponebank = My.get_bank_idtop1();
                        My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, regid, "SCHOOL PAY", feeType, VoucherNo,"");
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        dues_update_headwise_transaction.update_student_dues(Session_id, Class_id, regid, "0", "0", con);
                        con.Close();
                    } 
                }
                catch
                { 
                } 


                try
                {
                    string gcmid = My.get_student_gcm_id(regid);
                    string sub = "";
                    string messge = "";
                    if (Payment_type == "AnnualFee" || Payment_type == "HostelAnnualFee")
                    {
                        sub = "Annual Fee Deposit";
                        messge = "Dear " + Name + " you have paid annual fee:- " + Total_pay + " date:- " + Date + " slip no.:-" + slip_no;
                    }
                    else
                    {
                        sub = "Admission Fee Deposit";

                        messge = "Dear " + Name + " you have paid admission fee:- " + Total_pay + " date:- " + Date + " slip no.:-" + slip_no;
                    }
                    Dictionary<String, String> ss = new Dictionary<string, string>();
                    ss["notification_id"] = Guid.NewGuid().ToString();
                    ss["message"] = messge;
                    ss["title"] = sub;
                    ss["messagetype"] = "Message";
                    ss["url"] = "";
                    ss["link_url"] = "";
                    ss["UserId"] = regid;
                    UsesCode.SendNotification(gcmid, ss);

                    try

                    {
                        My.exeSql("update PAYMENT_TRANSACTION_PROCESS_ADMISSION_BULK set Status='done' where Transaction_Id='"+ razorpay_payment_id + "'");
                    }

                    catch
                    {

                    }





                }
                catch
                {

                }

            }

        }

        private void final_update_admission(string session, string classid, string regid, string adjustamount, string Total_pay)
        {
            double restamount = My.toDouble(adjustamount) - My.toDouble(Total_pay);
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber = '" + regid + "' and session='" + session + "' and Class_id='" + classid + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (My.toDouble(restamount) <= 0)
                {
                    dr["payment_status"] = "Paid";
                }
                else if (My.toDouble(restamount) > 0)
                {
                    dr["payment_status"] = "Dues";
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);

        }


        private void send_data_to_annual_fee_collection(string payment_type, string session, string session_id, string class_id, string regid, string category_id, string sub_category_id, string total_pay, string date, string type, string name, string slip_no, string entry_id, string total_discount, string branch_id, string razorpay_payment_id, string section, string previous_dues, string totalamount, string paybaleamount)
        {
            if (payment_type == "AnnualFee" || payment_type == "HostelAnnualFee")
            {
                DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + regid + "' and session='" + session + "' and  parameter like '%" + payment_type + "%' and feetype!='Previous Dues' ");
                SqlConnection conn2 = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + regid + "' and session='" + session + "'", conn2);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = regid;
                    dr[2] = My.toDouble(totalamount).ToString("0.00");
                    dr[3] = "0";
                    dr[4] = paybaleamount;
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                    dr[7] = date;
                    dr[8] = "Online";
                    dr[9] = slip_no;
                    dr["session"] = session;
                    dr["idate"] = Convert.ToDateTime(date).ToString("yyyyMMdd");
                    dr["remark"] = "Online Payment";
                    dr["time"] = mycode.time();
                    dr["user_id"] = regid;
                    dr["Slip_no"] = slip_no;
                    dr["Acamedic_Semester_Id"] = "0";
                    dr["branchid"] = branch_id;
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
            else
            {
                DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + regid + "' and session='" + session + "' and  parameter like '%" + payment_type + "%' and feetype!='Previous Dues' ");

                SqlConnection conn3 = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + regid + "' and session='" + session + "'", conn3);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = regid;
                    dr[2] = My.toDouble(totalamount).ToString("0.00");
                    dr[3] = My.toDouble(total_discount).ToString("0.00");
                    dr[4] = My.toDouble(paybaleamount).ToString("0.00");
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                    dr[7] = date;
                    dr[8] = "Online";
                    dr[9] = slip_no;
                    dr["session"] = session;

                    dr["idate"] = Convert.ToDateTime(date).ToString("yyyyMMdd");
                    dr["remark"] = "Online Payment";
                    dr["entry_id"] = entry_id;
                    dr["time"] = mycode.time();
                    dr["user_id"] = regid;
                    dr["Slip_no"] = slip_no;
                    dr["Acamedic_Semester_Id"] = "0";
                    dr["branchid"] = branch_id;

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
        }



        private void create_admission_annual_dues(string Payment_type, string session, string session_id, string class_id, string regid, string category_id, string sub_category_id, string total_pay, string date, string type, string name, string slip_no, string entry_id, string total_discount, string branch_id, string razorpay_payment_id, string Section, string hostaltaken, string Hostel_id,string classname)
        {
            string parameter = Payment_type;


            string Discount_on = "";

            string parameter_id = "";
            if (Payment_type == "AnnualFee" || Payment_type == "HostelAnnualFee")
            {
                parameter_id = hostaltaken.ToUpper().ToUpper() == "NO" ? "2" : "6";
                Discount_on = "Annual";


            }
            else
            {
                parameter_id = hostaltaken.ToUpper() == "NO" ? "1" : "5";
                Discount_on = "Admission";

            }
            if (My.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + regid + "' and parameter='" + parameter + "' and session='" + session + "'").Rows.Count == 0)
            {
                string query = "";
                if (Hostel_id == "0")
                {
                    query = "select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + regid + "'  and  parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + class_id + "' and admission_no='All'  and  parameter_id='" + parameter_id + "'  and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + class_id + "'  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + regid + "'";
                }
                else
                {
                    query = "select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and  admission_no='" + regid + "'  and  parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and Class_id='" + class_id + "' and admission_no='All'  and  parameter_id='" + parameter_id + "'  and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + class_id + "' and fmc.Hostel_Id='" + Hostel_id + "'  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + regid + "'";
                }
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + regid + "','" + classname + "','" + dr["session"].ToString() + "','" + Section + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + My.get_start_month() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + branch_id + "','0','" + class_id + "','" + regid + "')");
                    }
                }
            }




        }
        private void send_data_in_feetypewise_collection(string Payment_type, string session, string session_id, string class_id, string regid, string category_id, string sub_category_id, string total_pay, string date, string type, string name, string slip_no, string entry_id, string total_discount, string branch_id, string razorpay_payment_id, string section, string previous_dues, string hostaltaken, string Hostel_id)
        {
            string parameter = Payment_type;
            string parameter_id = "";
            string parameter_id1 = "";
            string parameter_id2 = "";
            string Discount_on = "";

            double paid_amount = My.toDouble(total_pay);
            if (My.toDouble(previous_dues) > 0)
            {
                string previusyear = "Previous Year";
                string previousyearcontent_id = "101";
                double paid = 0;
                if (paid_amount > My.toDouble(previous_dues))
                {
                    paid = My.toDouble(previous_dues);
                    //insert
                    paid_amount = paid_amount - My.toDouble(previous_dues);

                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id,branchid) values ('" + regid + "','" + class_id + "','" + session + "','" + section + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(previous_dues).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + "0.00" + "','Paid','" + My.get_start_month() + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + 0 + "','" + paid.ToString("0.00") + "','" + branch_id + "','0','" + class_id + "','" + regid + "','1')");


                    mycode.executequery("update Previous_Year_Dues set Status='Paid' where  Session_id='" + session_id + "' and AdmissionNumber='" + regid + "'  ");//and Class_id='" + class_id + "'

                    string qry = @"insert into Monthly_Fee_Collection_Slip" +
                        "(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + regid + "','" + session + "','" + class_id + "','" + section + "','" + parameter + "','" + My.toDouble(previusyear).ToString("0.00") + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + paid + "','" + slip_no + "','School','" + "0.00" + "','','0','0','" + branch_id + "','0','" + date + "','" + mycode.ConvertStringToiDate(date) + "');";
                    My.exeSql(qry);
                }
                else
                {
                    paid = paid_amount;
                    //insert
                    double duesamount = My.toDouble(previous_dues) - paid;

                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + regid + "','" + class_id + "','" + session + "','" + section + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(previous_dues).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + duesamount.ToString("0.00") + "','Dues','" + My.get_start_month() + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + 0 + "','" + paid.ToString("0.00") + "','" + branch_id + "','0','" + class_id + "','" + regid + "')");
                    mycode.executequery("update Previous_Year_Dues set Status='Dues' where Session_id='" + session_id + "' and AdmissionNumber='" + regid + "'  ");//and Class_id='" + class_id + "'

                    string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + regid + "','" + session + "','" + class_id + "','" + section + "','" + parameter + "','" + previusyear + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','School','" + "0.00" + "','','0','0','" + branch_id + "','0','" + date + "','" + mycode.ConvertStringToiDate(date) + "');";
                    My.exeSql(qry);
                    paid_amount = 0;
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + regid + "' and session='" + session + "' and parameter='" + parameter + "'", My.conn);//and status='Dues' 
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
                    if (paid_amount >= 0)
                    {
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";

                        double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                        dr["Date"] = date;
                        dr["idate"] = My.toDateTime(date).ToString("yyyyMMdd");
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
                            send_data_in_fee_collection_slip(dr["payable"].ToString(), dues.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], regid, session, class_id, section, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, date, branch_id);
                            #endregion
                        }
                        else
                        {
                            string prevpaid = dr["paid"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                            dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                            dr["status"] = "Dues";

                            #region send in collection slip

                            send_data_in_fee_collection_slip(dr["payable"].ToString(), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], regid, session, class_id, section, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, date, branch_id);

                            #endregion
                            paid_amount = 0;
                        }
                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                    }
                    else
                    {
                        break;
                    }

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);



            }

        }

        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string date, string branch_id)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + branch_id + "','0','" + date + "','" + My.DateConvertToIdate(date) + "');";
            My.exeSql(qry);
        }

        private void send_data_in_student_payment_history(string Payment_type, string Session, string Session_id, string Class_id, string regid, string category_id, string sub_category_id, string Total_pay, string Date, string type, string Name, string slip_no, string entry_id, string total_discount, string Branch_id, string razorpay_payment_id, string paymentmode, string paymentfrom)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", regid);
            cmd.Parameters.AddWithValue("@Session", Session);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + Name + " Paid Amount : " + Total_pay + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", My.toDouble(Total_pay).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", paymentmode);
            cmd.Parameters.AddWithValue("@discount", total_discount);
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
            cmd.Parameters.AddWithValue("@user_id", regid);
            cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
            cmd.Parameters.AddWithValue("@Branch", Branch_id);
            cmd.Parameters.AddWithValue("@Class_id", Class_id);
            cmd.Parameters.AddWithValue("@Remarks", "Online Payment");
            cmd.Parameters.AddWithValue("@User_Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", razorpay_payment_id);
            cmd.Parameters.AddWithValue("@Transection_in", paymentfrom);
            cmd.Parameters.AddWithValue("@parameter_New", Payment_type);
            if (My.InsertUpdateData(cmd))
            {
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

        private string get_previous_amount(string regid, string Session_id, string Class_id)
        {

            DataTable dt = mycode.FillData("select Dues_Amount from Previous_Year_Dues  where Session_id='" + Session_id + "' and AdmissionNumber='" + regid + "' and Status='Unpaid'"); //  and Class_id='" + Class_id + "'
            if (dt.Rows.Count == 0)
            {
                return "0.00";
            }
            else
            {

                return dt.Rows[0][0].ToString();
            }
        }
    }
}