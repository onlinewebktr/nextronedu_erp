using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace school_web.AppCode
{
    public class Payment_update_after_onlinepayment
    {

        My mycode = new My();
        public bool save_final_payment(string regid, string orderid)
        {
            bool toretun = false;
            Dictionary<string, object> dc1 = getstudentinfo(regid, orderid);
            string Name = (String)dc1["Name"];
            string Class_id = (String)dc1["Class_id"];
            string Session_id = (String)dc1["Session_id"];
            string Session = (String)dc1["Session"];

            string month = (String)dc1["month"];
            string Total_pay = (String)dc1["Total_pay"];
            //string parameter = (String)dc1["parameter"];
            string parameter_id = (String)dc1["parameter_id"];
            string Class_name = (String)dc1["Class_name"];
            string hosteltaken = (String)dc1["hosteltaken"];

            Dictionary<string, object> dc3 = mycode.Bind_hostel_data_for_assined_student(Session_id, Class_id, regid);

            string From_month_name = (String)dc3["From_month_name"];
            string From_month_id = (String)dc3["From_month_id"];
            string Assined_Year_Month = (String)dc3["Assined_Year_Month"];
            string Hostel_assign_id = (String)dc3["Hostel_assign_id"];
            string Hostel_id = (String)dc3["Hostel_id"];
            string Room_Category_id = (String)dc3["Room_Category_id"];


            Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(Session_id, Class_id, regid);
            string Transport_id = (String)dc2["Transport_id"];
            string TransportPath_id = (String)dc2["TransportPath_id"];
            string Boarding_Point_id = (String)dc2["Boarding_Point_id"];

            string parameter = hosteltaken.ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";

            string hostel_id = (String)dc1["hostel_id"];
            string day_boarding = (String)dc1["day_boarding"];
            string day_boarding_lunch = (String)dc1["day_boarding_lunch"];
            string category_id = (String)dc1["category_id"];
            string sub_category_id = (String)dc1["sub_category_id"];
            string transportportation_id = (String)dc1["transportportation_id"];
            string group_id = (String)dc1["group_id"];
            string Section = (String)dc1["Section"];
            string Date = (String)dc1["Date"];
            string pay_idate1 = (String)dc1["Idate"];
            string totaldiscount = (String)dc1["totaldiscount"];
            string totallatefine = (String)dc1["Fine_amount"];


            string Fine_amount = (String)dc1["Fine_amount"];
            string Total_no_of_fine_day_month = (String)dc1["Total_no_of_fine_day_month"];
            string Fine_date_from = (String)dc1["Fine_date_from"];
            string Fine_date_to = (String)dc1["Fine_date_to"];
            string Fine_type = (String)dc1["Fine_type"];
            string Lunch_Boarding_Parmeter = (String)dc1["Lunch_Boarding_Parmeter"];



            int pay_idate = Convert.ToInt32(pay_idate1);
            bool chek_fee = My.find_mnth_fee_taken_date("Student_Payment_History", pay_idate, regid, Class_id, Session);
            if (chek_fee == false)
            {

            }

            else
            {
                // payment process
                List<string> month_lst = new List<string>();
                string slipno = "", entry_id = "";
                DataTable dt = mycode.FillData("Select Month,Month_Id from Month_Index where Month in (" + month + ")   order by Position asc ");//
                if (dt.Rows.Count > 0)
                {
                    slipno = My.invoice_monthly("slip_no"); //My.auto_serial("slip_no");
                    entry_id = My.auto_serialS("entry_id");
                    for (int iS = 0; iS < dt.Rows.Count; iS++)
                    {
                        string monthname = dt.Rows[iS]["Month"].ToString();
                        month_lst.Add(monthname);
                        if (My.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + regid + "' and session='" + Session + "' and month='" + monthname + "' and parameter='" + parameter + "'").Rows.Count > 0)
                        {
                            continue;
                        }
                        else
                        {
                            Dictionary<string, object> dc = new Dictionary<string, object>();
                            dc["admission_no"] = regid;
                            dc["session_id"] = Session_id;
                            dc["class"] = Class_name;
                            dc["session"] = Session;
                            dc["class_id"] = Class_id;
                            dc["hosteltaken"] = hosteltaken;
                            dc["months"] = monthname;
                            dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                            dc["hostel_id"] = hosteltaken.ToString().ToLower() == "yes" ? My.toint(hostel_id) : 0;
                            dc["day_boarding"] = day_boarding;
                            dc["day_boarding_lunch"] = day_boarding_lunch;

                            dc["category_id"] = category_id;
                            dc["sub_category_id"] = sub_category_id;

                            dc["parameter_id"] = Lunch_Boarding_Parmeter;

                            dc["TransportationPath_id"] = TransportPath_id;
                            dc["transportportation_id"] = Transport_id;
                            dc["Boarding_Point_id"] = Boarding_Point_id;

                            dc["hostel_id"] = Hostel_id;
                            dc["Room_Category_id"] = Room_Category_id;
                            dc["Hostel_assig_id"] = Hostel_assign_id;


                            //new08/08/2022

                            string cunrt_session = Session;
                            string session_frst_year = cunrt_session.Substring(0, 4);
                            int session_s_year = My.toint(session_frst_year);
                            int s_year = My.toint(session_frst_year);

                            string monthid = My.tomonth_numberstring(monthname);

                            int pay_month = My.toint(monthid);
                            s_year = My.check_start_months(pay_month, s_year);
                            dc["monthid"] = s_year + monthid;


                            DataTable feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                            send_in_typewise_fee(feedt, Section, regid, Fine_amount, orderid, monthname, Date, pay_idate1, slipno);
                        }
                    }

                    string total_paid = Total_pay;

                    My.exeSql("update Typewise_fee_collection set position=( select top 1 Position from dbo.[Month_Index] where Month=Typewise_fee_collection.month)");


                    send_data_in_student_payment_history("Monthly", slipno, entry_id, month_lst, total_paid, regid, Session, Date, pay_idate, Name, orderid, totaldiscount, totallatefine, Class_id, group_id, Session_id);


                    My.send_data_to_user_log_history("Self", regid + " received Monthly fee :-" + total_paid + " rs,Slip No :- " + slipno + " from " + Name + ", Admission No :-" + regid);

                    try
                    {
                        string input = Date;  // DD/MM/YYYY
                        string FNsession = My.GetFinancialSessionFromString(input);
                        string unique_entry_id = My.unique_id();
                        string VoucherNo = slipno;
                        string feeType = "Student Fee Payment";
                        double amountpaid = My.toDouble(total_paid);
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
                            My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, regid, "SCHOOL PAY", feeType, VoucherNo, "");

                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            dues_update_headwise_transaction.update_student_dues(Session_id, Class_id, regid, "0", "0", con);
                            con.Close();
                        }
                        else
                        {
                        }
                    }
                    catch
                    {
                    }

                    string gcmid = "";
                    string message = "";
                    #region sms and whatsaap
                    // sms & whatsapp
                    try
                    {

                        Dictionary<string, object> dcNotif = My.get_student_info(regid, Session);
                        string mobilesms = (String)dcNotif["father_mob"];
                        string whatsappno = (String)dcNotif["Father_whatsApp_no"];
                        string classname = (String)dcNotif["classname"];
                        string rollnumber = (String)dcNotif["rollnumber"];
                        string studentname = (String)dcNotif["studentname"];
                        gcmid = (String)dcNotif["gcm_id"];


                        string type = "";
                        //  My mycode = new My();
                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Pay Fee");
                        string SMS_Tempate = (String)autosms["SMS_Tempate"];
                        string VariableName = (String)autosms["VariableName"];
                        string SMSType = (String)autosms["SMSType"];
                        string Send_From = (String)autosms["Send_From"];
                        string Is_Send_SMS = (String)autosms["Is_Send_SMS"];
                        string Is_Send_WhatsApp = (String)autosms["Is_Send_WhatsApp"];
                        string Wid = (String)autosms["Wid"];

                        var vrls = VariableName.Split(',');
                        var lst = new String[vrls.Length];
                        //DataTable dtD = My.dataTable("select *,(convert(float, payable)-(convert(float, paid_amt)+convert(float, disc_amt)+convert(float, prev_paid_amt))) as Dues_amt from (select isnull(sum(convert(float, payable)),0) as payable,isnull(sum(convert(float, disc_amt)),0) as disc_amt,isnull(sum(convert(float, paid)),0) as paid_amt,isnull(sum(convert(float, previously_paid)),0) as prev_paid_amt,(select top 1 Month from Monthly_Fee_Collection_Slip where slipno='" + slipno + "' order by month_position desc) as Month from Monthly_Fee_Collection_Slip where slipno='" + slipno + "') t");
                        if (vrls.Length > 0)
                        {
                            lst[0] = studentname;
                        }
                        if (vrls.Length > 1)
                        {
                            lst[1] = classname;
                        }
                        if (vrls.Length > 2)
                        {
                            lst[2] = regid;
                        }
                        if (vrls.Length > 3)
                        {
                            lst[3] = rollnumber;
                        }
                        if (vrls.Length > 4)
                        {
                            lst[4] = month;
                        }
                        if (vrls.Length > 5)
                        {
                            lst[5] = Total_pay;
                        }
                        if (vrls.Length > 6)
                        {
                            lst[6] = "0";
                        }
                        if (vrls.Length > 7)
                        {
                            lst[7] = Date;
                        }
                        if (vrls.Length > 8)
                        {
                            lst[8] = slipno;
                        }
                        message = String.Format(SMS_Tempate, lst);
                        if (SMSType == "Unicode")
                        {
                            type = "unicode";
                        }
                        else
                        {
                            type = "english";
                        }


                        try
                        {
                            string api_key = ""; string Sender_id = "";
                            if (Is_Send_SMS.ToUpper() == "TRUE")
                            {
                                var dtSMS = mycode.FillData("select top 1 * from message_config where Status='running'");
                                if (dtSMS.Rows.Count == 1)
                                {
                                    api_key = dtSMS.Rows[0]["uid"].ToString();
                                    Sender_id = dtSMS.Rows[0]["sender"].ToString();
                                }
                                else
                                {
                                    api_key = "0";
                                    Sender_id = "0";
                                }



                                string msgtype = type;

                                string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + message + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobilesms + "&smsContentType=" + type;

                                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                string results = sr.ReadToEnd();
                                sr.Close();

                                My.Insert("Message_Details", new
                                {
                                    Mobile_No = mobilesms,
                                    Message = message,
                                    Date = mycode.date(),
                                    Idate = mycode.idate(),
                                    Time = mycode.time(),
                                    Result = results,
                                    User_id = regid,
                                    Mesage_Type = msgtype,
                                    Groupcode = "SMS",
                                    Status = "SEND",
                                    Url = url,
                                    Message_to_Type = "Student",
                                    admin_user_id = regid,
                                });
                            }
                            if (Is_Send_WhatsApp.ToUpper() == "TRUE")
                            {
                                string sms = String.Format(SMS_Tempate, lst);
                                try
                                {
                                    string whatsapp_mobile_no = ""; string Whatsapp_api_url = "";
                                    var dtSMS = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                                    if (dtSMS.Rows.Count == 1)
                                    {
                                        whatsapp_mobile_no = dtSMS.Rows[0]["SMS_API"].ToString();
                                        Whatsapp_api_url = dtSMS.Rows[0]["url"].ToString();
                                    }
                                    else
                                    {
                                        whatsapp_mobile_no = "";
                                        Whatsapp_api_url = "";
                                    }


                                    var query = new Dictionary<string, string>()
    {
        { "authkey", whatsapp_mobile_no },
        { "mobile", whatsappno },
        { "country_code", "91" },
        { "wid", Wid},
        { "1", lst[0] },
        { "2", lst[1]},
        { "3", lst[2] },
        { "4", lst[3] },
        { "5", lst[4] },
        { "6", lst[5] },
        { "7",lst[6]},
        { "8", lst[7] },
        { "9", lst[8] }
                                                };

                                    string url = Whatsapp_api_url;
                                    string fullUrl = url + "?" + string.Join("&",
                                        query.Select(kvp => kvp.Key + "=" + Uri.EscapeDataString(kvp.Value)));


                                    if (whatsappno.Length > 9)
                                    {
                                        My.Insert("WhatsApp_send", new
                                        {
                                            Mobile_no = whatsappno,
                                            Message = message,
                                            Message_url = fullUrl,
                                            Session_id = Session_id,
                                            Admission_no = regid,
                                            Status = "Pending",
                                            Date = mycode.date(),
                                            Idate = mycode.idate(),
                                            Time = mycode.time(),
                                            Send_by = regid,
                                            Mesage_Type = type,
                                        });

                                    }
                                }
                                catch (Exception ex)
                                {
                                    My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                                    //return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    catch
                    {
                    }

                    #endregion

                    try
                    {
                        string sub = "Fee Payment Confirmation";
                        string messge = message;// "Dear " + lbl_name.Text + " you have paid monthly fee :- " + monthlYPaid.ToString() + " date :- " + txt_payment_date.Text + " slip no.:-" + slip_no;
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = messge;
                        ss["title"] = sub;
                        ss["messagetype"] = "Message";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = regid;
                        UsesCode.SendNotification(gcmid, ss);
                    }
                    catch (Exception ex) { }

                    toretun = true;


                }

                if (toretun == true)
                {
                    // sucessmessge respons
                    mycode.executequery("update Payment_transaction_process set status='Success' where ordertrackingid='" + orderid + "' and  Admission_no='" + regid + "' ");


                    // month fee account vochar







                }
                else
                {
                    toretun = false;
                    // error message respons
                }

            }











            return toretun;








        }


        private void send_in_typewise_fee(DataTable feedt, string Section, string regid, string fine_amt, string orderid, string monthname, string Date, string pay_idate1, string slipno)
        {
            string std_info = get_branch_id(feedt.Rows[0]["admission_no"].ToString(), feedt.Rows[0]["session"].ToString());
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = std_info.Split(stringSeparatorss, StringSplitOptions.None);
            string Branch_id = arrs[0];
            string Session_id = arrs[1];
            double fine = My.toDouble(fine_amt);
            if (fine > 0)
            {
                int mnth_idss = My.tomonth_number(monthname);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());
                //DataTable dt = mycode.FillData("select Fine_amount from Temp_fine_monthwise where Session_id='" + Session_id + "' and Admission_no='" + feedt.Rows[0]["admission_no"].ToString() + "' and Month_id='" + month_id + "' and Branch_id='" + Branch_id + "'");

                DataTable dt = mycode.FillData("select parameter from Typewise_fee_collection where session='" + feedt.Rows[0]["session"].ToString() + "' and admission_no='" + feedt.Rows[0]["admission_no"].ToString() + "' and transection='" + slipno + "' and content_id='6121' ");
                if (dt.Rows.Count == 0)
                {
                    fine = My.toDouble(fine_amt);


                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + feedt.Rows[0]["admission_no"].ToString() + "','" + feedt.Rows[0]["class"].ToString() + "','" + feedt.Rows[0]["session"].ToString() + "','" + feedt.Rows[0]["parameter"].ToString() + "','" + Date + "','" + pay_idate1 + "','Late Fine','" + My.toDouble(fine).ToString("0.00") + "','0','" + My.toDouble(fine).ToString("0.00") + "','Dues','" + monthname + "','6121','" + slipno + "','School','false','false','false','0.00','" + Section + "','" + regid + "','" + Branch_id + "')");
                }
            }


            bool entrys = false;
            foreach (DataRow dr in feedt.Rows)
            {
                //double fine = My.toDouble(fine_amt);
                //if (fine > 0)
                //{
                //    if (entrys == false)
                //    {
                //        fine = My.toDouble(fine_amt);
                //        My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine','" + My.toDouble(fine).ToString("0.00") + "','0','" + My.toDouble(fine).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','6121','','School','false','false','false','0.00','" + Section + "','" + regid + "','1')");
                //        entrys = true;
                //    }
                //}


                My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + Date + "','" + pay_idate1 + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + Section + "','" + regid + "','" + Branch_id + "')");
            }
        }

        private string get_branch_id(string admission_no, string session)
        {
            string branch_id = "1"; string session_id = "1";
            DataTable dt = mycode.FillData("select top 1 Branch_id,Session_id from admission_registor where session='" + session + "' and admissionserialnumber='" + admission_no + "' order by Id desc");
            if (dt.Rows.Count > 0)
            {
                branch_id = dt.Rows[0]["Branch_id"].ToString();
                session_id = dt.Rows[0]["Session_id"].ToString();
            }
            return branch_id + "/" + session_id;
        }



        public Dictionary<string, object> getstudentinfo(string Admission_no, string orderid)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select * from Payment_transaction_process where Admission_no='" + Admission_no + "' and ordertrackingid='" + orderid + "'";
            SqlDataAdapter ad = new SqlDataAdapter(quiry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "UserRegistrationMaster");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;

            if (rowcount == 0)
            {
                // go_to_Incubators_user(mobileno);

                dc["Name"] = "NO";
                dc["Admission_no"] = "NO";
                dc["Session"] = "NO";
                dc["Class_id"] = "0";
                dc["Session_id"] = "0";
                dc["totalAmount"] = "0";
                dc["totalpaid_perivius"] = "0";
                dc["totaldiscount"] = "0";
                dc["totallatefine"] = "0";
                dc["Total_pay"] = "0";
                dc["month"] = "NO";
                dc["status"] = "NO";
                dc["parameter"] = "NO";
                dc["parameter_id"] = "NO";
                dc["razorpay_payment_id"] = "NO";
                dc["slipno"] = "NO";

            }
            else
            {
                dc["Name"] = dt.Rows[0]["Name"].ToString();
                dc["Admission_no"] = dt.Rows[0]["Admission_no"].ToString();
                dc["Session"] = dt.Rows[0]["Session"].ToString();
                dc["Class_id"] = dt.Rows[0]["Class_id"].ToString();
                dc["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                dc["totalAmount"] = dt.Rows[0]["totalAmount"].ToString();
                dc["totalpaid_perivius"] = dt.Rows[0]["totalpaid_perivius"].ToString();
                dc["totaldiscount"] = dt.Rows[0]["totaldiscount"].ToString();
                dc["totallatefine"] = dt.Rows[0]["totallatefine"].ToString();
                dc["Total_pay"] = dt.Rows[0]["Total_pay"].ToString();
                dc["month"] = dt.Rows[0]["month"].ToString();
                dc["status"] = dt.Rows[0]["status"].ToString();

                dc["parameter"] = dt.Rows[0]["parameter"].ToString();
                dc["parameter_id"] = dt.Rows[0]["parameter_id"].ToString();
                dc["hosteltaken"] = dt.Rows[0]["hosteltaken"].ToString();
                dc["hostel_id"] = dt.Rows[0]["hostel_id"].ToString();
                dc["day_boarding"] = dt.Rows[0]["day_boarding"].ToString();
                dc["day_boarding_lunch"] = dt.Rows[0]["day_boarding_lunch"].ToString();
                dc["category_id"] = dt.Rows[0]["category_id"].ToString();
                dc["sub_category_id"] = dt.Rows[0]["sub_category_id"].ToString();
                dc["transportportation_id"] = dt.Rows[0]["transportportation_id"].ToString();
                dc["group_id"] = dt.Rows[0]["group_id"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                dc["Class_name"] = dt.Rows[0]["Class_name"].ToString();
                dc["Date"] = dt.Rows[0]["Date"].ToString();
                dc["Idate"] = dt.Rows[0]["Idate"].ToString();
                dc["totaldiscount"] = dt.Rows[0]["totaldiscount"].ToString();
                dc["totallatefine"] = dt.Rows[0]["totallatefine"].ToString();

                dc["Fine_amount"] = dt.Rows[0]["Fine_amount"].ToString();
                dc["Total_no_of_fine_day_month"] = dt.Rows[0]["Total_no_of_fine_day_month"].ToString();
                dc["Fine_date_from"] = dt.Rows[0]["Fine_date_from"].ToString();
                dc["Fine_date_to"] = dt.Rows[0]["Fine_date_to"].ToString();
                dc["Fine_type"] = dt.Rows[0]["Fine_type"].ToString();
                dc["razorpay_payment_id"] = dt.Rows[0]["razorpay_payment_id"].ToString();
                dc["Lunch_Boarding_Parmeter"] = dt.Rows[0]["Lunch_Boarding_Parmeter"].ToString();

                dc["slipno"] = mycode.get_slip_no(orderid, Admission_no);
            }

            return dc;








        }


        private void send_data_in_student_payment_history(string type, string slipno, string entry_id, List<string> month_lst, string total_paid, string regid, string Session, string Date, int pay_idate, string Name, string orderid, string totaldiscount, string totallatefine, string classid, string group_id, string Session_id)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select  * from Student_Payment_History where Addmission_no='" + regid + "' and Pay_mode_transaction_no='" + orderid + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Payment_History");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;

            if (rowcount == 0)
            {
                DataRow dr = dt.NewRow();


                dr["Addmission_no"] = regid;
                dr["Session"] = Session;
                dr["Date"] = Date;
                dr["Idate"] = pay_idate;
                dr["Description"] = type + " fee collection for " + Name + " Month " + String.Join(",", month_lst) + ", Paid Amount : " + total_paid + " /-";
                dr["Entry_id"] = entry_id;
                dr["Slip_no"] = slipno;
                dr["Amount"] = My.toDouble(total_paid).ToString("0.00");
                dr["Type"] = type;
                dr["mode"] = "Online";
                dr["Pay_mode_transaction_no"] = orderid;

                dr["discount"] = My.toDouble(totaldiscount).ToString("0.00");
                dr["Discoun_in_School"] = 0;
                dr["Discoun_in_Hostel"] = 0;
                dr["Discoun_in_Transport"] = 0;
                dr["fine"] = My.toDouble(totallatefine).ToString("0.00");
                dr["is_ofline_sync"] = true;
                dr["Is_online_sync"] = false;
                dr["time"] = mycode.time();
                dr["user_id"] = regid;
                dr["Remarks"] = "Online Payment";
                dr["Class_id"] = classid;
                dr["Transection_in"] = "App";
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                //send data in school ledger
                update_School_ledger(slipno, entry_id, total_paid, Name, regid, totaldiscount, totallatefine, Session, Date, pay_idate);
                string app_payment_type = "APP";//My.session("App_fee_collection_type");
                DataTable sdt = My.dataTable("select Section,class,rollnumber,Session_id,Class_id,Transfer_Status,hosteltaken,Hostel_id from dbo.[admission_registor] where admissionserialnumber='" + regid + "' and session='" + Session + "'");

                #region update type wise fee collection
                // fine calculation has been zero
                submit_transection_in_typewise(regid, Session, My.toDouble(totallatefine), Date, My.DateConvertToIdate(Date).ToString(), My.toDouble(total_paid), slipno, entry_id, sdt.Rows[0]["class"].ToString(), sdt.Rows[0]["Section"].ToString(), sdt.Rows[0]["Class_id"].ToString(), sdt.Rows[0]["hosteltaken"].ToString(), sdt.Rows[0]["Hostel_id"].ToString(), "", app_payment_type, group_id, orderid, Session_id);
                #endregion
            }

            //

        }



        private void update_School_ledger(string slipno, string entry_id, string total_paid, string Name, string regid, string totaldiscount, string totallatefine, string Session, string Date, int pay_idate)
        {



            SqlDataAdapter ad_contactus = new SqlDataAdapter("select top 1 * from SchoolLedger ", My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "Month Wise Fee Collection";
            dr[1] = "Monthly  Fee  for  " + Name + "  Adm.No:-" + regid + "Total Bill:- " + My.toDouble(total_paid).ToString("0.00") + " , Paid Amount :-" + My.toDouble(total_paid).ToString("0.00") + " ,  Discount Given:-" + My.toDouble(totaldiscount).ToString("0.00") + ", Fine Amount:-" + My.toDouble(totallatefine).ToString("0.00") + " Slip No:" + slipno;
            dr[2] = My.toDouble(total_paid).ToString("0.00");
            dr[3] = "0";
            dr[4] = pay_idate;
            dr[5] = Date;
            dr[6] = slipno;
            dr["entry_id"] = entry_id;
            dr["session"] = Session;
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = regid;
            dr["Addmission_no"] = regid;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad_contactus);
            ad_contactus.Update(dt);
        }


        private void submit_transection_in_typewise(string adno, string session, double fine, string date, string idate, double paid_amount, string slip_no, string entry_id, string classs, string sction, string class_id, string hostel_taken, string hostel_id, string app_transection_id, string app_payment_type, string groupid, string orderid, string Session_id)
        {
            Dictionary<string, object> dc1 = getstudentinfo(adno, orderid);


            string Fine_amount = (String)dc1["Fine_amount"];
            string Total_no_of_fine_day_month = (String)dc1["Total_no_of_fine_day_month"];
            string Fine_date_from = (String)dc1["Fine_date_from"];
            string Fine_date_to = (String)dc1["Fine_date_to"];
            string Fine_type = (String)dc1["Fine_type"];


            #region update dues amount in typewise fee collection
            string parameter = "", month = "", late_fine_month = "", month_position = "";
            string qry = " select *  from Typewise_fee_collection  where admission_no='" + adno + "' and session='" + session + "' and status='Dues' and parameter like '%MonthlyFee%' order by cast(Position as float)";


            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable tdt = ds.Tables[0];
            if (tdt.Rows.Count == 0)
            {

            }
            else
            {
                late_fine_month = tdt.Rows[0]["month"].ToString();
                month_position = tdt.Rows[0]["position"].ToString();
                string prev_month = "";
                foreach (DataRow dr in tdt.Rows)
                {
                    if (paid_amount >= 0)
                    {
                        month = dr["month"].ToString();
                        parameter = dr["parameter"].ToString();
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";

                        double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                        dr["Date"] = date;
                        dr["idate"] = idate;
                        if (paid_amount >= dues && paid_amount > 0)
                        {
                            string prevpaid = My.toDouble(dr["paid"].ToString()).ToString("0.00");
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = dr["dues"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]).ToString("0.00");
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            #region send in collection slip
                            send_data_in_fee_collection_slip(dr["payable"].ToString(), dues.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, date, idate, Session_id);
                            #endregion
                        }
                        else
                        {
                            if (paid_amount > 0 || (prev_month != "" && prev_month == month))
                            {
                                string prevpaid = dr["paid"].ToString();
                                dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                                dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                                dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                                if (My.toDouble(dr["dues"]) <= 0)
                                {
                                    dr["status"] = "Paid";
                                }
                                else
                                {
                                    dr["status"] = "Dues";
                                }
                                #region send in collection slip
                                send_data_in_fee_collection_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, date, idate, Session_id);
                                #endregion
                                paid_amount = 0;
                            }
                            else
                            {
                                break;
                            }

                        }
                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                        prev_month = month;
                    }
                    else
                    {
                        break;
                    }

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }

            #region submit fine amount in type wise collection

            if (fine > 0)
            {
                My.exeSql("insert into Fine_Fees_collection(Admission_no,Session_id,Date,idate,Description,Slip_no,Amount,Total_no_of_fine_day_month,Fine_date_from,Fine_date_to,Branch_id,User_id,Class_id,Fine_type) values ('" + adno + "','" + Session_id + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine Fees','" + slip_no + "','" + My.toDouble(fine).ToString("0.00") + "','" + Total_no_of_fine_day_month + "','" + Fine_date_from + "','" + Fine_date_to + "','1','" + adno + "','" + class_id + "','" + Fine_type + "')");
            }



            //if (fine > 0)
            //{
            //    double paid = fine;
            //    double dues = 0;
            //    string pstatus = "Paid";
            //    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,group_id,class_id,position) values ('" + adno + "','" + classs + "','" + session + "','" + sction + "','" + parameter + "','" + date + "',N'" + idate + "','Class Fine','" + fine + "','" + paid + "','" + dues + "','" + pstatus + "','" + late_fine_month + "','" + My.user + "','Class Fine','" + slip_no + "','School','" + groupid + "','" + class_id + "','" + month_position + "');insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,Month,disc_amt,previously_paid,month_position,Date,Idate) values ('" + adno + "','" + session + "','" + class_id + "','" + sction + "','" + parameter + "','Class Fine','Class Fine','" + fine + "','" + paid + "','" + slip_no + "','School','" + late_fine_month + "','0','0','" + month_position + "','" + date + "','" + idate + "');");
            //}
            #endregion
            #endregion

        }


        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string date, string idate, string Session_id)
        {

            Dictionary<string, object> dc1 = mycode.Bind_Transport_data_for_assined_student(Session_id, classs, adno);
            string Transport_id = (String)dc1["Transport_id"];
            string TransportPath_id = (String)dc1["TransportPath_id"];
            string Boarding_Point_id = (String)dc1["Boarding_Point_id"];

            Dictionary<string, object> dc2 = mycode.Bind_hostel_data_for_assined_student(Session_id, classs, adno);
            string Hostel_id = (String)dc2["Hostel_id"];
            string Room_Category_id = (String)dc2["Room_Category_id"];

            string Hostel_assign_id = (String)dc2["Hostel_assign_id"];


            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,Date,Idate,Transport_Boarding_Point_id,Transportation_Id,TransportationPath_id,Hostel_id,Room_category) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + My.toDouble(payable).ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + My.toDouble(prevpaid).ToString("0.00") + "','" + date + "','" + idate + "','" + Boarding_Point_id + "','" + Transport_id + "','" + TransportPath_id + "','" + Hostel_id + "','" + Hostel_assign_id + "');";
            My.exeSql(qry);
        }
    }
}