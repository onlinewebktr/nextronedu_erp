using Microsoft.VisualBasic.FileIO;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class bulk_fee_update_adm_mnth_with_bill : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) ");
                        ddlsession.SelectedValue = My.get_session_id();
                        ViewState["RepeatFine"] = My.get_fine_repat_no();
                        ViewState["firm_id"] = My.get_firm_id();
                    }
                }
            }
            catch (Exception ex)
            {
            }
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



        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    ViewState["session_id"] = ddlsession.SelectedValue;
                    btn_final_submit.Visible = true;
                    ViewState["dupAdmiD"] = "0";
                    upload_excel_file();
                }
                else
                {
                    Alertme("Please choose excel.csv file.", "warning");
                    return;
                }
            }
            catch (Exception ex)
            { }
        }

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            string csvid = My.auto_serialS("Upload_csvid");
            SqlDataAdapter ad = new SqlDataAdapter("Select * from excel_file", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Csv_file");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = file;
            dr[2] = mycode.date();
            dr[3] = mycode.idate();
            dr[4] = csvid;
            dr[5] = "SUBMITTED";
            dr[6] = "MonthlyFee";
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private string upload_csv_data()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("ddMMyyyy");
            string time = dtm.ToString("hhmmss");
            String filerename = date + time;

            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;

            Session["file1"] = FileUpload1.FileName;
            String FileExtension = Path.GetExtension(Session["file1"].ToString()).ToLower();
            Session["file"] = ("Upload_Monthly_Fee_Mapping" + filerename + FileExtension);
            String[] allowedExtensions = { ".csv" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;
                    break;
                }
                else
                {
                }
            }

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["file"]);

                    if (check_wrap_or_not((path + "/" + Session["file"])))
                    {
                        FileSaved = true;
                    }
                    else
                    {
                        File.Delete((path + "/" + Session["file"]));
                        FileSaved = false;
                    }
                }
                catch (Exception ex)
                {
                    FileSaved = false;

                    Alertme(ex.ToString(), "warning");
                }
            }
            else
            {
                dbfilePath = "Choose only csv File";
                return dbfilePath;
            }
            if (FileSaved)
            {

                string fileName = Path.GetFileName(Session["file"].ToString());
                dbfilePath = @"/Master_Img/Student/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }

        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Admission_no");
                tblReadCSV.Columns.Add("Student_name");
                tblReadCSV.Columns.Add("Admission_fee_paid");
                tblReadCSV.Columns.Add("Monthly_fee_paid");
                //tblReadCSV.Columns.Add("Total_paid");
                tblReadCSV.Columns.Add("Payment_date");
                tblReadCSV.Columns.Add("Bill_no");
                tblReadCSV.Columns.Add("Payment_mode");
                tblReadCSV.Columns.Add("Transaction_id");

                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                pnl_grid.Visible = true;
                lbl_total1.Text = "Total Data :- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();
                //============== 

                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {
                    string Bill_no = grvExcelData.Rows[i].Cells[5].Text;
                    #region check duplicate
                    string adno = Bill_no;
                    DataTable dt = My.dataTable("select Slip_no from Student_Payment_History where Addmission_no='" + adno + "' and  Session='" + ddlsession.SelectedItem.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                        Alertme("Sorry! Duplicate bill no", "warning");
                        btn_final_submit.Visible = false;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string Excel_entry_id = My.create_random_no_otp();
                    string Admission_No = "";
                    string Admission_fee_paid = "";
                    string Monthly_fee_paid = "";
                    string Total_paid = "";
                    string payment_date = "";
                    string Bill_no = "";
                    string Payment_mode = "";
                    string Transaction_id = "";
                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        string isDupBill = "0";
                        Admission_No = grvExcelData.Rows[i].Cells[0].Text;
                        Admission_fee_paid = grvExcelData.Rows[i].Cells[2].Text;
                        Monthly_fee_paid = grvExcelData.Rows[i].Cells[3].Text;
                        //Total_paid = grvExcelData.Rows[i].Cells[7].Text;
                        payment_date = grvExcelData.Rows[i].Cells[4].Text;
                        Bill_no = grvExcelData.Rows[i].Cells[5].Text;
                        Payment_mode = grvExcelData.Rows[i].Cells[6].Text;
                        Transaction_id = grvExcelData.Rows[i].Cells[7].Text;
                        if (Transaction_id == "&nbsp;")
                        {
                            Transaction_id = "";
                        }

                        Total_paid = (My.toDouble(Admission_fee_paid) + My.toDouble(Monthly_fee_paid)).ToString();
                        //=====
                        txt_adm_ann_fee.Text = "";
                        txttotal.Text = "";
                        txt_paid_prev.Text = "";
                        txt_discount.Text = "";
                        txtfineamount.Text = "";
                        txt_other_fee.Text = "";
                        txttotalbill.Text = "";
                        hd_overall_bill.Value = "";
                        hd_total_discount.Value = "";
                        hd_totalamount.Value = "";
                        hd_adjustamount.Value = "";
                        hd_paybaleamount.Value = "";
                        //=====


                        string slip_no = Bill_no, entry_id = ""; string studentName = "";
                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            Dictionary<string, object> dc1 = payments.get_selected_studentinfo(Admission_No, ViewState["session_id"].ToString(), ViewState["branchid"].ToString(), con);
                            studentName = (String)dc1["Name"];
                            string admissionserialnumber = (String)dc1["admissionserialnumber"];
                            string rollnumber = (String)dc1["rollnumber"];
                            string session = (String)dc1["session"];
                            string Section = (String)dc1["Section"];
                            string Class_id = (String)dc1["Class_id"];
                            string classname = (String)dc1["classname"];
                            string lblhostel = (String)dc1["hosteltaken"];
                            string lbltransporttion = (String)dc1["transportationtaken"];
                            string Transfer_Status = (String)dc1["Transfer_Status"];
                            string category_id = (String)dc1["category_id"];
                            string sub_category_id = (String)dc1["sub_category_id"];
                            string parameter = (String)dc1["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                            ViewState["Transfer_Status"] = (String)dc1["Transfer_Status"];
                            ViewState["day_bording"] = My.toBool((String)dc1["is_applied_dayboarding"]);
                            ViewState["day_bording_with_lunch"] = My.toBool(dc1["day_boarding_with_lunch"]);

                            Dictionary<string, object> dc2 = payments.Bind_Transport_data_for_assined_student(ViewState["session_id"].ToString(), Class_id, admissionserialnumber, con);
                            ViewState["Transport_id"] = (String)dc2["Transport_id"];
                            ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                            ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                            ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                            ViewState["Month_name"] = (String)dc2["Month_name"];
                            ViewState["Month_id"] = (String)dc2["Month_id"];
                            ViewState["Year_month"] = (String)dc2["Year_month"];
                            ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];


                            //==========
                            Dictionary<string, object> dc3 = payments.Bind_hostel_data_for_assined_student(ViewState["session_id"].ToString(), Class_id, admissionserialnumber, con);
                            ViewState["Hostel_id"] = (String)dc3["Hostel_id"];
                            ViewState["Room_Category_id"] = (String)dc3["Room_Category_id"];
                            ViewState["From_month_name"] = (String)dc3["From_month_name"];
                            ViewState["From_month_id"] = (String)dc3["From_month_id"];
                            ViewState["Assined_Year_Month"] = (String)dc3["Assined_Year_Month"];
                            ViewState["Hostel_assign_id"] = (String)dc3["Hostel_assign_id"];
                            if (ViewState["Hostel_id"].ToString() == "0")
                            {
                                ViewState["parameterDisc"] = "4";
                            }


                            string fee_group_id = "2";
                            if (Transfer_Status == "New")
                            {
                                find_admission_dues_fee(ViewState["session_id"].ToString(), ViewState["branchid"].ToString(), admissionserialnumber, Class_id, session, category_id, sub_category_id, ViewState["Hostel_id"].ToString(), con);
                                fee_group_id = "1";
                            }
                            else
                            {
                                find_annual_dues_fee(ViewState["session_id"].ToString(), ViewState["branchid"].ToString(), admissionserialnumber, Class_id, session, category_id, sub_category_id, ViewState["Hostel_id"].ToString(), con);
                                fee_group_id = "2";
                            }

                            //==============================
                            ViewState["IsBoarding"] = "0";
                            ViewState["parameteridS"] = "4";
                            string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + ViewState["session_id"].ToString() + "' and Admission_no='" + admissionserialnumber + "' and Class_id='" + Class_id + "'";
                            DataTable dts = payments.dataTable(queryS, con);
                            if (dts.Rows.Count != 0)
                            {
                                ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                                ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                                ViewState["IsBoarding"] = "1";
                            }

                            find_all_due_fee(ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, con);
                            calculate_checkbox(ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, ViewState["Hostel_id"].ToString(), classname, lblhostel, category_id, sub_category_id, con);


                            ///========================PAYMENTS
                            ///
                            ViewState["Transaction_Id"] = My.create_random_no_otp();
                            SqlCommand cmd;
                            string query = "INSERT INTO Bulk_payment_status (Session_id,Class_id,Admission_no,Student_name,Section,Roll_no,Paid_amount,Status,Created_by,Created_date,Created_time,Transaction_id,Excel_entry_id) values (@Session_id,@Class_id,@Admission_no,@Student_name,@Section,@Roll_no,@Paid_amount,@Status,@Created_by,@Created_date,@Created_time,@Transaction_id,@Excel_entry_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", Class_id);
                            cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                            cmd.Parameters.AddWithValue("@Student_name", studentName);
                            cmd.Parameters.AddWithValue("@Section", Section);
                            cmd.Parameters.AddWithValue("@Roll_no", rollnumber);
                            cmd.Parameters.AddWithValue("@Paid_amount", Total_paid);
                            cmd.Parameters.AddWithValue("@Status", "Failure");
                            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                            cmd.Parameters.AddWithValue("@Transaction_id", ViewState["Transaction_Id"].ToString());
                            cmd.Parameters.AddWithValue("@Excel_entry_id", Excel_entry_id);
                            if (payments.InsertUpdateData(cmd, con))
                            {
                            }


                            DataTable dt = payments.dataTable("select Slip_no from Student_Payment_History where Slip_no='" + slip_no + "' and  Session='" + ddlsession.SelectedItem.Text + "'", con);
                            if (dt.Rows.Count > 0)
                            {
                                isDupBill = "1";
                                payments.exeSql("update Bulk_payment_status set Status='Duplicate bill no.' where Session_id='" + ViewState["session_id"].ToString() + "' and Admission_no='" + Admission_No + "' and Transaction_id='" + ViewState["Transaction_Id"].ToString() + "'", con);
                            }
                            if (isDupBill == "0")
                            {
                                #region payCode 
                                List<string> month_lst = new List<string>();
                                double admissionPaid = 0; string paymentType = "Monthly"; double monthlYPaid = 0; string iS_any_payment_done = "0";
                                if (My.toDouble(hd_overall_bill.Value) >= My.toDouble(Total_paid))
                                {
                                    string feeTypes = "";
                                    if (My.toDouble(Admission_fee_paid) > 0)
                                    {
                                        if (My.toDouble(txt_adm_ann_fee.Text) > 0)
                                        {
                                            double ttlPaid = (My.toDouble(Admission_fee_paid) + My.toDouble(Monthly_fee_paid));
                                            #region AdmissionAnnuaL
                                            //if (My.toDouble(Admission_fee_paid) > 0)
                                            //{
                                            iS_any_payment_done = "1";
                                            if (ViewState["Transfer_Status"].ToString() == "New")
                                            {
                                                feeTypes = "Admission";
                                                make_admission_fee(con, My.toDouble(Admission_fee_paid), slip_no, ttlPaid.ToString(), feeTypes, ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, ViewState["Hostel_id"].ToString(), classname, lblhostel, category_id, sub_category_id, payment_date, studentName, Section, Payment_mode, Transaction_id);
                                            }
                                            else
                                            {
                                                feeTypes = "Annual";
                                                make_annual_fee(con, My.toDouble(Admission_fee_paid), slip_no, ttlPaid.ToString(), ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, ViewState["Hostel_id"].ToString(), classname, lblhostel, category_id, sub_category_id, payment_date, studentName, Section, Payment_mode, Transaction_id);
                                            }
                                            //}

                                            //if (My.toDouble(Monthly_fee_paid) > 0)
                                            //{
                                            int growcountS = rd_months.Items.Count; string is_month_payment = "0";
                                            for (int iS = 0; iS < growcountS; iS++)
                                            {
                                                Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                                                CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                                                if (My.toBool(chk_month.Checked))
                                                {
                                                    is_month_payment = "1";
                                                    month_lst.Add(lbl_Month.Text);
                                                }
                                            }


                                            if (is_month_payment == "1")
                                            {
                                                is_month_payment = "0";
                                                int feedtCount = rp_fee_details.Items.Count;
                                                for (int iSS = 0; iSS < feedtCount; iSS++)
                                                {
                                                    CheckBox chk_get_fee = (CheckBox)rp_fee_details.Items[iSS].FindControl("chk_get_fee");
                                                    if (My.toBool(chk_get_fee.Checked))
                                                    {
                                                        is_month_payment = "1";
                                                    }
                                                }
                                            }
                                            //paymentType = "MonthlyAdmission";
                                            if (is_month_payment == "1")
                                            {
                                                iS_any_payment_done = "1";
                                                if (My.toDouble(Admission_fee_paid) > 0)
                                                {
                                                    paymentType = "MonthlyAdmission";
                                                }
                                                entry_id = payments.auto_serialS("entry_id", con);
                                                calculate_dues_for_new_month(slip_no, con, ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, ViewState["Hostel_id"].ToString(), classname, lblhostel, category_id, sub_category_id, payment_date, studentName, Section);
                                                payments.exeSql("update Typewise_fee_collection set position=( select top 1 Position from dbo.[Month_Index] where Month=Typewise_fee_collection.month)", con);
                                                send_data_in_student_payment_history("Monthly", slip_no, entry_id, month_lst, Monthly_fee_paid, con, "1", ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, ViewState["Hostel_id"].ToString(), classname, lblhostel, category_id, sub_category_id, payment_date, studentName, Section, Payment_mode, Transaction_id);
                                            }
                                            //}
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        if (My.toDouble(Monthly_fee_paid) >= 0)
                                        {
                                            int growcountS = rd_months.Items.Count; string is_month_payment = "0";
                                            for (int iS = 0; iS < growcountS; iS++)
                                            {
                                                Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                                                CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                                                if (My.toBool(chk_month.Checked))
                                                {
                                                    is_month_payment = "1";
                                                    month_lst.Add(lbl_Month.Text);
                                                }
                                            }
                                            if (is_month_payment == "1")
                                            {
                                                is_month_payment = "0";
                                                int feedtCount = rp_fee_details.Items.Count;
                                                for (int iSS = 0; iSS < feedtCount; iSS++)
                                                {
                                                    CheckBox chk_get_fee = (CheckBox)rp_fee_details.Items[iSS].FindControl("chk_get_fee");
                                                    if (My.toBool(chk_get_fee.Checked))
                                                    {
                                                        is_month_payment = "1";
                                                    }
                                                }
                                            }

                                            //paymentType = "MonthlyAdmission";
                                            if (is_month_payment == "1")
                                            {
                                                iS_any_payment_done = "1";
                                                string total_paid = Monthly_fee_paid;
                                                entry_id = payments.auto_serialS("entry_id", con);
                                                calculate_dues_for_new_month(slip_no, con, ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, ViewState["Hostel_id"].ToString(), classname, lblhostel, category_id, sub_category_id, payment_date, studentName, Section);
                                                payments.exeSql("update Typewise_fee_collection set position=( select top 1 Position from dbo.[Month_Index] where Month=Typewise_fee_collection.month)", con);
                                                send_data_in_student_payment_history("Monthly", slip_no, entry_id, month_lst, total_paid, con, "0", ViewState["session_id"].ToString(), session, Class_id, admissionserialnumber, parameter, ViewState["Hostel_id"].ToString(), classname, lblhostel, category_id, sub_category_id, payment_date, studentName, Section, Payment_mode, Transaction_id);
                                                payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received Monthly fee :-" + total_paid + " rs,Slip No :- " + slip_no + " from " + studentName + ", Admission No :-" + admissionserialnumber, con);
                                            }
                                        }
                                    }
                                    payments.exeSql("delete from Typewise_fee_collection where admission_no='" + admissionserialnumber + "' and session='" + session + "' and status='Dues' and month in (select month from ( Select sum(cast(paid as float)) paid,month from dbo.[Typewise_fee_collection] where admission_no='" + admissionserialnumber + "' and session='" + session + "' group by month) t where  paid=0 ); delete from Monthly_Fee_Collection_Slip where adno='" + admissionserialnumber + "' and session='" + session + "' and (cast(payable as float)-cast(disc_amt as float))>0 and month in (select month from ( Select sum(cast(paid as float)) paid,month from dbo.[Monthly_Fee_Collection_Slip] where adno='" + admissionserialnumber + "' and session='" + session + "' group by month) t where  paid=0)", con);
                                    dues_update_headwise_transaction.update_student_dues(ViewState["session_id"].ToString(), Class_id, admissionserialnumber, "0", "0", con);
                                }
                                #endregion
                            }
                            ///====================================PAYMENTS
                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                        if (flag == true)
                        {
                            if (isDupBill == "0")
                            {
                                My.exeSql("update Bulk_payment_status set Status='Success' where Session_id='" + ViewState["session_id"].ToString() + "' and Admission_no='" + Admission_No + "' and Transaction_id='" + ViewState["Transaction_Id"].ToString() + "'");
                            }
                            txt_adm_ann_fee.Text = "";
                            txttotal.Text = "";
                            txt_paid_prev.Text = "";
                            txt_discount.Text = "";
                            txtfineamount.Text = "";
                            txt_other_fee.Text = "";
                            txttotalbill.Text = "";
                            hd_overall_bill.Value = "";
                            hd_total_discount.Value = "";
                            hd_totalamount.Value = "";
                            hd_adjustamount.Value = "";
                            hd_paybaleamount.Value = "";
                        }
                    }


                    Alertme("Student payment has been done successfully.", "success");
                    btn_final_submit.Visible = false;
                    string query23 = "select (select top 1 Course_Name from Add_course_table where course_id=Bulk_payment_status.Class_id) as Class_name,Admission_no,Student_name,Section,Roll_no,Paid_amount,Status from Bulk_payment_status where Excel_entry_id='" + Excel_entry_id + "'";
                    DataTable dt1 = mycode.FillData(query23);
                    if (dt1.Rows.Count == 0)
                    {
                        grvExcelData.DataSource = null;
                        grvExcelData.DataBind();
                    }
                    else
                    {
                        grvExcelData.DataSource = dt1;
                        grvExcelData.DataBind();
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        void send_data_in_student_payment_history(string type, string slip_no, string entry_id, List<string> month_lst, string total_paid, SqlConnection con, string withAdm, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section, string Payment_mode, string Transaction_id)
        {
            if (withAdm == "0")
            {
                SqlDataAdapter ad = new SqlDataAdapter("select top 1 * from Student_Payment_History where Addmission_no='" + admission_no + "'", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Student_Payment_History");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Addmission_no"] = admission_no;
                dr["Session"] = ddlsession.SelectedItem.Text;
                dr["Date"] = payment_date;
                dr["Idate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["Description"] = type + " fee collection for " + studentName + " Month " + String.Join(",", month_lst) + ", Paid Amount : " + total_paid + " /-";
                dr["Entry_id"] = entry_id;
                dr["Slip_no"] = slip_no;
                dr["Amount"] = My.toDouble(total_paid).ToString("0.00");
                dr["Type"] = type;
                dr["mode"] = Payment_mode;
                dr["Pay_mode_transaction_no"] = Transaction_id;
                dr["discount"] = My.toDouble(txt_discount.Text).ToString("0.00");
                dr["Discoun_in_School"] = 0;
                dr["Discoun_in_Hostel"] = 0;
                dr["Discoun_in_Transport"] = 0;
                dr["fine"] = "0";
                dr["is_ofline_sync"] = true;
                dr["Is_online_sync"] = false;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString();
                dr["Remarks"] = "";
                dr["Class_id"] = Class_id;
                dr["Transection_in"] = "Software";
                dr["Branch"] = ViewState["Branchid"].ToString();
                dr["parameter_New"] = parameter1;
                dr["Bank_name"] = "";
                dr["Bank_date"] = "";
                dr["Created_date"] = mycode.date();
                dr["Created_idate"] = mycode.idate();
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);

                //send data in school ledger
                update_School_ledger(slip_no, entry_id, total_paid, con, session_id, session, Class_id, admission_no, parameter1, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section);
            }
            //
            string app_payment_type = "Software";//My.session("App_fee_collection_type");
            DataTable sdt = payments.dataTable("select Section,class,rollnumber,Session_id,Class_id,Transfer_Status,hosteltaken,Hostel_id from dbo.[admission_registor] where admissionserialnumber='" + admission_no + "' and session='" + session + "'", con);

            #region update type wise fee collection
            // fine calculation has been zero
            submit_transection_in_typewise(admission_no, session, My.toDouble(txtfineamount.Text), payment_date, My.DateConvertToIdate(payment_date).ToString(), My.toDouble(total_paid), slip_no, entry_id, sdt.Rows[0]["class"].ToString(), sdt.Rows[0]["Section"].ToString(), sdt.Rows[0]["Class_id"].ToString(), sdt.Rows[0]["hosteltaken"].ToString(), sdt.Rows[0]["Hostel_id"].ToString(), "", app_payment_type, payment_date, parameter1, con);
            #endregion
        }

        private void submit_transection_in_typewise(string adno, string session, double fine, string date, string idate, double paid_amount, string slip_no, string entry_id, string classs, string sction, string class_id, string hostel_taken, string hostel_id, string app_transection_id, string app_payment_type, string payment_date, string parameter1, SqlConnection con)
        {
            #region update dues amount in typewise fee collection
            string parameter = "", month = "", late_fine_month = "", month_position = "";
            string qry = " select *  from Typewise_fee_collection  where admission_no='" + adno + "' and session='" + session + "' and status='Dues' and parameter like '%" + parameter1 + "%' order by cast(Position as float)";
            SqlDataAdapter ad = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable tdt = ds.Tables[0];
            if (tdt.Rows.Count > 0)
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
                        if (paid_amount >= dues) // && paid_amount > 0
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
                            send_data_in_fee_collection_slip(dr["payable"].ToString(), dues.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, payment_date, hostel_id, con);
                            #endregion
                        }
                        else
                        {
                            if (paid_amount >= 0 || (prev_month != "" && prev_month == month))
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
                                send_data_in_fee_collection_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, payment_date, hostel_id, con);
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
            #endregion
        }



        private void update_School_ledger(string slip_no, string entry_id, string paid, SqlConnection con, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select top 1 * from SchoolLedger", con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "Month Wise Fee Collection";
            dr[1] = "Monthly  Fee  for  " + studentName + "  Adm.No:-" + admission_no + "Total Bill:- " + txttotalbill.Text + " , Paid Amount :-" + paid.ToString() + " ,  Discount Given:-" + txt_discount.Text + " Slip No:-" + slip_no;
            dr[2] = My.toDouble(paid).ToString("0.00");
            dr[3] = "0";
            dr[4] = My.toDateTime(payment_date).ToString("yyyyMMdd");
            dr[5] = payment_date;
            dr[6] = slip_no;
            dr["entry_id"] = entry_id;
            dr["session"] = ddlsession.SelectedItem.Text;
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = admission_no;
            dr["branchid"] = ViewState["Branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad_contactus);
            ad_contactus.Update(dt);
        }

        private void calculate_dues_for_new_month(string slipno, SqlConnection con, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            int growcountS = rd_months.Items.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");

                if (My.toBool(chk_month.Checked))
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(lbl_Month.Text);
                        if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }

                    if (payments.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + session + "' and month='" + lbl_Month.Text + "' and parameter='" + parameter1 + "' and transection!=''", con).Rows.Count > 0)
                    {
                        continue;
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = admission_no;
                        dc["session_id"] = session_id;
                        dc["class"] = classname;
                        dc["session"] = session;
                        dc["class_id"] = Class_id;
                        dc["hosteltaken"] = hostelTaken;
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = Hostel_id;
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();

                        dc["category_id"] = category_id;
                        dc["sub_category_id"] = sub_category_id;
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                        dc["parameter_id"] = ViewState["parameteridS"].ToString();
                        //new08/08/2022

                        string cunrt_session = session;
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(lbl_Month.Text);
                        int pay_month = My.toint(monthid);
                        s_year = payments.check_start_months(pay_month, s_year, con);
                        dc["monthid"] = s_year + monthid;
                        DataTable feedt = payments.dataTableSP("sp_fetch_monthly_fee", dc, con);
                        send_in_typewise_fee(feedt, lbl_Month.Text, slipno, con, session_id, session, Class_id, admission_no, parameter1, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section);
                    }
                }
            }
        }
        private void send_in_typewise_fee(DataTable feedt, string month_name, string slipno, SqlConnection con, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            foreach (DataRow dr in feedt.Rows)
            {
                string parm = dr["parameter"].ToString();
                payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,parameter2) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + parm + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','" + slipno + "','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + Section + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "','" + dr["parameter"].ToString() + "')", con);
            }
        }


        private void find_all_due_fee(string session_id, string session, string Class_id, string admission_no, string Paramiter, SqlConnection con)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("value");
            dtDatas.Columns.Add("fee_amount");
            dtDatas.Columns.Add("discount_per");
            dtDatas.Columns.Add("paid_peev");
            dtDatas.Columns.Add("paid_status");
            dtDatas.Columns.Add("bac_colour");
            int temp;
            List<string> lst = new List<string>();
            string temp_month = payments.get_start_month(con);
            for (temp = 1; temp <= 12; temp++)
            {
                DataTable paid_dt = payments.dataTable("select month,status from dbo.[Typewise_fee_collection] where session='" + session + "' and admission_no='" + admission_no + "' and parameter like '%" + Paramiter + "%' and month='" + temp_month + "'", con);
                if (paid_dt.Rows.Count > 0)
                {
                    string remove_month = "";
                    foreach (DataRow pdr in paid_dt.Rows)
                    {
                        if (pdr["status"].ToString() == "Dues")
                        {
                            lst.Add(temp_month);
                            break;
                        }
                    }
                }
                else
                {
                    lst.Add(temp_month);
                }
                temp_month = find_month(temp_month);
            }

            int i = 0;
            foreach (string str in lst)
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["Month"] = lst[i];
                drNewRow["value"] = false;
                drNewRow["paid_status"] = "NotCreated";
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                i++;
            }
            rd_months.DataSource = dtDatas.DefaultView;
            rd_months.DataBind();
        }


        private string find_month(string month)
        {
            string next = "nn";
            switch (month)
            {
                case "January":
                    next = "February";
                    return next;

                case "February":
                    next = "March";
                    return next;

                case "March":
                    next = "April";
                    return next;

                case "April":
                    next = "May";
                    return next;

                case "May":
                    next = "June";
                    return next;

                case "June":
                    next = "July";
                    return next;

                case "July":
                    next = "August";
                    return next;

                case "August":
                    next = "September";
                    return next;

                case "September":
                    next = "October";
                    return next;

                case "October":
                    next = "November";
                    return next;

                case "November":
                    next = "December";
                    return next;

                case "December":
                    next = "January";
                    return next;

            }
            return next;
        }


        private void find_admission_dues_fee(string session_id, string branchid, string admission_no, string Class_id, string Session, string category_id, string sub_category_id, string hostel_id, SqlConnection con)
        {
            string qry = "";
            string parameter_id = "";
            string parameter_id2 = "";
            string parameter = "";
            string parameter2 = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter_id = "1"; // annulfee
                parameter_id2 = "5"; // admission fee for hostel

                if (hostel_id == "0")
                {
                    parameter = "AdmissionFee";
                }
                else
                {
                    parameter = "HostelAdmissionFee";
                }
            }
            else
            {
                parameter_id = "2"; // annulfee
                parameter_id2 = "6"; // admission fee for hostel 
                if (hostel_id == "0")
                {
                    parameter = "AnnualFee";
                }
                else
                {
                    parameter = "HostelAnnualFee";
                }
            }
            if (hostel_id == "0")
            {
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and (parameter='" + parameter + "')  and session='" + Session + "' and class_id=" + Class_id + ")t";
            }
            else
            {
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and (parameter='" + parameter + "' )  and session='" + Session + "' and class_id=" + Class_id + ")t";
            }
            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (hostel_id == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable, '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + Session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + Session + "' and class_id='" + Class_id + "') t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable, '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + Session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + Session + "' and class_id='" + Class_id + "' and  fmc.Hostel_Id='" + hostel_id + "' ) t";
                }
                fee_dt = payments.dataTable(qry, con);
            }


            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
            {
                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0; string Payment_status = "Paid";
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                    if (Payment_status == "Paid")
                    {
                        if (dr["status"].ToString() == "Dues")
                        {
                            Payment_status = "Dues";
                        }
                    }
                }
                txt_adm_ann_fee.Text = "0";
                if (Payment_status == "Dues")
                {
                    txt_adm_ann_fee.Text = payble_after_disc.ToString();
                }
            }
        }

        private void find_annual_dues_fee(string session_id, string branchid, string admission_no, string Class_id, string Session, string category_id, string sub_category_id, string hostel_id, SqlConnection con)
        {
            string qry = "";
            string parameter_id = "";
            string parameter_id2 = "";
            string parameter = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                if (hostel_id == "0")
                {
                    parameter = "AdmissionFee";
                }
                else
                {
                    parameter = "HostelAdmissionFee";
                }
            }
            else
            {
                if (hostel_id == "0")
                {
                    parameter = "AnnualFee";
                }
                else
                {
                    parameter = "HostelAnnualFee";
                }
            }

            parameter_id = "2";// annulfee
            parameter_id2 = "6";// admission fee for hostel

            if (hostel_id == "0")
            {
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and (parameter='" + parameter + "')  and session='" + Session + "' and class_id=" + Class_id + ")t";
            }
            else
            {
                parameter = "HostelAnnualFee";
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and (parameter='" + parameter + "' )  and session='" + Session + "' and class_id=" + Class_id + ")t";
            }


            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + Session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "') and session='" + Session + "' and class_id='" + Class_id + "') t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + Session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and admission_no='" + admission_no + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Annual'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + Session + "' and class_id='" + Class_id + "' and  fmc.Hostel_Id='" + hostel_id + "') t";
                }
                fee_dt = payments.dataTable(qry, con);
            }



            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
            {
                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0; string Payment_status = "Paid";
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                    if (Payment_status == "Paid")
                    {
                        if (dr["status"].ToString() == "Dues")
                        {
                            Payment_status = "Dues";
                        }
                    }
                }
                txt_adm_ann_fee.Text = "0";
                if (Payment_status == "Dues")
                {
                    txt_adm_ann_fee.Text = payble_after_disc.ToString();
                }
            }
        }



        private void calculate_checkbox(string session_id, string session, string Class_id, string admission_no, string parameter, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, SqlConnection con)
        {
            ViewState["MnthName"] = "";
            //looping 
            try
            {
                int growcountS = rd_months.Items.Count;
                for (int iS = 0; iS < growcountS; iS++)
                {
                    Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                    CheckBox chk = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                    if (chk.Checked == true)
                    {
                        string month = lbl_Month.Text;
                        bind_monthly_fee(session_id, session, Class_id, admission_no, parameter, Hostel_id, classname, hostelTaken, category_id, sub_category_id, con);
                    }
                    else
                    {
                        bind_monthly_fee(session_id, session, Class_id, admission_no, parameter, Hostel_id, classname, hostelTaken, category_id, sub_category_id, con);
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        private void bind_monthly_fee(string session_id, string session, string Class_id, string admission_no, string parameter, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, SqlConnection con)
        {
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();
            int growcountS = rd_months.Items.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                string cunrt_session = session;
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);


                Label lbl_Month = (Label)rd_months.Items[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)rd_months.Items[iS].FindControl("chk_month");
                fee_amount = 0; disc_amount = 0; paid_previously = 0;
                DataTable feedt = new DataTable();
                if (My.toBool(chk_month.Checked))
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(lbl_Month.Text);
                        if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }
                    string type = "";
                    //dr["paid_status"] = "Created";
                    //dr["bac_colour"] = "Yellow";
                    if (payments.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + session + "' and month='" + lbl_Month.Text + "' and parameter='" + parameter + "' and transection!=''", con).Rows.Count > 0)
                    {
                        feedt = payments.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + admission_no + "' and session='" + session + "' and month='" + lbl_Month.Text + "' and parameter='" + parameter + "' and content_id!='6121'  and transection!=''", con);
                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = admission_no;
                        dc["session_id"] = session_id;
                        dc["class"] = classname;
                        dc["session"] = session;
                        dc["class_id"] = Class_id;
                        dc["hosteltaken"] = hostelTaken.ToLower();
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = Hostel_id;
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                        dc["category_id"] = category_id;
                        dc["sub_category_id"] = sub_category_id;
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                        dc["parameter_id"] = ViewState["parameteridS"].ToString();

                        //new08/08/2022 
                        string monthid = My.tomonth_numberstring(lbl_Month.Text);
                        int pay_month = My.toint(monthid);
                        s_year = payments.check_start_months(pay_month, s_year, con);
                        dc["monthid"] = s_year + monthid;

                        feedt = payments.dataTableSP("sp_fetch_monthly_fee", dc, con);
                        feedt.Columns.Add("previously_paid");
                        type = "NotCalculated";
                    }
                    feedt.Columns.Add("total_payable");




                    string month = "";
                    double total = 0, fee = 0, disc = 0, paid_prev = 0;
                    foreach (DataRow dr in feedt.Rows)
                    {
                        month = dr["months"].ToString();
                        dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["previously_paid"]);
                        fee += My.toDouble(dr["amount"]);
                        disc += My.toDouble(dr["disc_amount"]);
                        paid_prev += My.toDouble(dr["previously_paid"]);

                        total += My.toDouble(dr["total_payable"]);
                    }

                    foreach (DataRow dr in feedt.Rows)
                    {
                        try
                        {
                            fdt.Rows.Add(dr.ItemArray);
                        }
                        catch
                        {
                            foreach (DataColumn dc in feedt.Columns)
                            {
                                fdt.Columns.Add(dc.ColumnName);
                            }
                            fdt.Rows.Add(dr.ItemArray);
                        }
                    }
                }
                else
                {
                }
            }

            if (fdt.Rows.Count.ToString() != "0")
            {
                rp_fee_details.DataSource = fdt.DefaultView;
                rp_fee_details.DataBind();
                bind_ttl_fee();
                pnl_month_wise_fee_details.Visible = true;

            }
            else
            {
                pnl_month_wise_fee_details.Visible = false;
                lbl_fee_amount.Text = "0";
                lbl_discount.Text = "0";
                lbl_paid_prev.Text = "0";
                lbl_total.Text = "0";

                txttotal.Text = "0";
                txt_paid_prev.Text = "0";
                txt_discount.Text = "0";
                txttotalbill.Text = "0";
            }
        }

        private void bind_ttl_fee()
        {
            int i;
            double totalAmt = 0; double totaldisc = 0; double totalPrepAid = 0; double totalpblE = 0;
            int gridview_rowcount = rp_fee_details.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_amount = (Label)rp_fee_details.Items[i].FindControl("lbl_amount");
                Label lbl_disc_amt = (Label)rp_fee_details.Items[i].FindControl("lbl_disc_amt");
                Label lbl_pre_paid = (Label)rp_fee_details.Items[i].FindControl("lbl_pre_paid");
                Label lbl_tot_pble = (Label)rp_fee_details.Items[i].FindControl("lbl_tot_pble");
                if (lbl_amount.Text != "")
                {
                    totalAmt = totalAmt + Convert.ToDouble(lbl_amount.Text);
                }
                if (lbl_disc_amt.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(lbl_disc_amt.Text);
                }
                if (lbl_pre_paid.Text != "")
                {
                    totalPrepAid = totalPrepAid + Convert.ToDouble(lbl_pre_paid.Text);
                }
                if (lbl_tot_pble.Text != "")
                {
                    totalpblE = totalpblE + Convert.ToDouble(lbl_tot_pble.Text);
                }
            }
            lbl_fee_amount.Text = totalAmt.ToString();
            lbl_discount.Text = totaldisc.ToString();
            lbl_paid_prev.Text = totalPrepAid.ToString();
            lbl_total.Text = totalpblE.ToString();
            txttotal.Text = totalAmt.ToString();
            txt_paid_prev.Text = totalPrepAid.ToString();
            txt_discount.Text = totaldisc.ToString();
            txtfineamount.Text = "0";
            txttotalbill.Text = (totalpblE + My.toDouble("0") + My.toDouble(txt_other_fee.Text)).ToString();
            hd_overall_bill.Value = (My.toDouble(txttotalbill.Text) + My.toDouble(txt_adm_ann_fee.Text)).ToString();
        }


        #region MakeAnnualFEE
        private void make_annual_fee(SqlConnection con, double PaidAmt, string slip_no, string Total_paid_amount, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section, string Payment_mode, string Transaction_id)
        {
            string parameter = hostelTaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and parameter='" + parameter + "' and session='" + session + "')t";
            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + admission_no + "'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual'),(select top 1 disc_amount from dbo.[Discount_Master] where   Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + Class_id + "') t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where  Hostel_id=" + Hostel_id + " and admission_no='" + admission_no + "'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + Class_id + "' and fmc.Hostel_Id='" + Hostel_id + "') t";
                }
                fee_dt = payments.dataTable(qry, con);
            }
            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
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
                double totalpay = payble_after_disc;


                hd_paybaleamount.Value = totalpay.ToString("0.00");
                hd_adjustamount.Value = payble_after_disc.ToString("0.00");
                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");



                ///=============================
                ///=============================
                string type = "Annual";
                string ad_no = admission_no;
                string entry_id = "AD" + cretesessionid(con);
                ViewState["yearlYSLipNo"] = slip_no;
                annual_payment(slip_no, entry_id, ad_no, con, PaidAmt, Total_paid_amount, session_id, session, Class_id, admission_no, parameter1, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section, type, Payment_mode, Transaction_id);
                payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received " + type + " fee :-" + Total_paid_amount + " Adjust Amount" + hd_total_discount.Value + " rs,Slip No :- " + slip_no + " from " + studentName + ", Admission No :-" + ad_no, con);


            }
        }


        private void annual_payment(string slip_no, string entry_id, string ad_no, SqlConnection con, double PaidAmt, string Total_paid_amount, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section, string type, string Payment_mode, string Transaction_id)
        {
            string Uid = slip_no;
            string Tag = PaidAmt.ToString();
            string parameter = "";
            parameter = hostelTaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            send_data_in_student_payment_history_annual(type, slip_no, entry_id, ad_no, parameter, con, My.toDouble(Total_paid_amount), session_id, session, Class_id, admission_no, parameter1, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section, type, Payment_mode, Transaction_id);
            send_data_to_school_ledger_annual(slip_no, entry_id, con, My.toDouble(Total_paid_amount), session_id, session, Class_id, admission_no, parameter1, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section, type);
            create_admission_annual_dues_annual(parameter, con, session_id, session, Class_id, admission_no, parameter1, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section);
            send_data_in_feetypewise_collection_annual(slip_no, entry_id, parameter, con, PaidAmt, session_id, session, Class_id, admission_no, parameter1, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section);
        }



        private void send_data_in_feetypewise_collection_annual(string slip_no, string entry_id, string parameter, SqlConnection con, double PaidAmt, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            string class_id = Class_id;
            double paid_amount = PaidAmt;

            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + admission_no + "' and session='" + session + "' and status='Dues' and parameter='" + parameter + "'", con);
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
                        dr["Date"] = payment_date;
                        dr["idate"] = My.toDateTime(payment_date).ToString("yyyyMMdd");
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
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), My.toDouble(dues.ToString()).ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], admission_no, session, Class_id, Section, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, payment_date, Hostel_id, con);
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
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], admission_no, session, Class_id, Section, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, payment_date, Hostel_id, con);
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
        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string payment_date, string Hostel_id, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,Date,Idate,branchid,Hostel_id,Room_category) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + My.toDouble(payable).ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + My.toDouble(prevpaid).ToString("0.00") + "','" + payment_date + "','" + My.DateConvertToIdate(payment_date) + "','" + ViewState["Branchid"].ToString() + "','" + Hostel_id + "','" + ViewState["Room_Category_id"].ToString() + "');";
            payments.exeSql(qry, con);
        }

        private void create_admission_annual_dues_annual(string parameter, SqlConnection con, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            if (payments.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and parameter='" + parameter + "' and session='" + session + "'", con).Rows.Count == 0)
            {
                string query = "";
                if (Hostel_id == "0")
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable, '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + Class_id + "'";
                }
                else
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + Hostel_id + "' and admission_no='" + admission_no + "'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + Hostel_id + "' and  Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + Class_id + "' and fmc.Hostel_Id='" + Hostel_id + "'";
                }
                DataTable dt = payments.dataTable(query, con);
                if (dt.Rows.Count == 0)
                { }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + admission_no + "','" + classname + "','" + dr["session"].ToString() + "','" + Section + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + Class_id + "','" + ViewState["Userid"].ToString() + "')", con);
                    }
                }
            }
        }

        private void send_data_to_school_ledger_annual(string transcation, string entry_id, SqlConnection con, double PaidAmt, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section, string type)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + studentName + " Adm.No:-" + admission_no + " Total Bill :-" + PaidAmt.ToString() + " Paid Amount :-  " + PaidAmt.ToString();
            dr["AllInput"] = PaidAmt.ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
            dr["Date"] = payment_date;
            dr["TransactionId"] = transcation;
            dr["entry_id"] = entry_id;
            dr["session"] = session;
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = admission_no;
            dr["branchid"] = ViewState["branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_in_student_payment_history_annual(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con, double PaidAmt, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section, string type1, string Payment_mode, string Transaction_id)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New,Bank_name,Bank_date,Created_date,Created_idate) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New,@Bank_name,@Bank_date,@Created_date,@Created_idate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", session);
            cmd.Parameters.AddWithValue("@Date", payment_date);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(payment_date).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + studentName + " Paid Amount : " + PaidAmt.ToString() + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", PaidAmt.ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", Payment_mode);
            cmd.Parameters.AddWithValue("@discount", hd_total_discount.Value);
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
            cmd.Parameters.AddWithValue("@Class_id", Class_id);
            cmd.Parameters.AddWithValue("@Remarks", "");
            cmd.Parameters.AddWithValue("@User_Slip_no", "");
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", Transaction_id);
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            cmd.Parameters.AddWithValue("@Bank_name", "");
            cmd.Parameters.AddWithValue("@Bank_date", ""); 
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate()); 
            if (payments.InsertUpdateData(cmd, con))
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "SchoolLedger");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Particulars"] = type + " Money Receipt";
                dr["Discription"] = type + " Money Receipt " + studentName + " Adm.No:-" + admission_no + " Paid Amount :-  " + PaidAmt.ToString("0.00");
                dr["AllInput"] = PaidAmt.ToString("0.00");
                dr["AllOutput"] = "0";
                dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["Date"] = payment_date;
                dr["TransactionId"] = slip_no;
                dr["entry_id"] = entry_id;
                dr["session"] = session;
                dr["Ledger_Type"] = "School";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString();
                dr["Addmission_no"] = admission_no;
                dr["branchid"] = ViewState["branchid"].ToString();
                dr["Unique_id"] = entry_id;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }
        #endregion


        #region MakeAdmissionFEE
        private void make_admission_fee(SqlConnection con, double PaidAmt, string slip_no, string Full_paid_amt, string feeTypes, string session_id, string session, string Class_id, string admission_no, string parameter, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section, string Payment_mode, string Transaction_id)
        {
            string parameter_id = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter = hostelTaken.ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                parameter_id = hostelTaken.ToUpper() == "NO" ? "1" : "5";
            }
            else
            {
                parameter = hostelTaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                parameter_id = hostelTaken.ToUpper() == "NO" ? "2" : "6";
            }
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and parameter='" + parameter + "' and session='" + session + "')t";
            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + Class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + session + "' and class_id='" + Class_id + "' ) t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and admission_no='" + admission_no + "'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + session + "' and class_id='" + Class_id + "' and fmc.Hostel_Id='" + Hostel_id + "') t";
                }
                fee_dt = payments.dataTable(qry, con);
            }

            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
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

                hd_paybaleamount.Value = payble_after_disc.ToString("0.00");
                hd_adjustamount.Value = payble_after_disc.ToString("0.00");
                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");



                string ad_no = admission_no;
                string entry_id = "AD" + cretesessionid(con);
                ViewState["yearlYSLipNo"] = slip_no;
                admission_payment(slip_no, entry_id, ad_no, con, PaidAmt, Full_paid_amt, feeTypes, session_id, session, Class_id, admission_no, parameter, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section, Payment_mode, Transaction_id);
                payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received fee :-" + Full_paid_amt.ToString() + " Adjust Amount" + hd_total_discount.Value + " rs,Slip No :- " + slip_no + " from " + studentName + ", Admission No :-" + ad_no, con);
            }
        }
        payments pays = new payments();
        private string cretesessionid(SqlConnection con)
        {
            bool duplicate = false;
            string Slip_no = pays.auto_serial("admfee_id", con);
            while (!duplicate)
            {
                DataTable cdt = payments.dataTable("select Slip_no from dbo.[Student_Payment_History] where Slip_no='" + Slip_no + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Slip_no = pays.auto_serial("admfee_id", con);
                }
            }
            return Slip_no;
        }

        private void admission_payment(string slip_no, string entry_id, string ad_no, SqlConnection con, double PaidAmt, string Full_paid_amt, string type, string session_id, string session, string Class_id, string admission_no, string parameter, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section, string Payment_mode, string Transaction_id)
        {
            string Uid = slip_no;
            string Tag = PaidAmt.ToString();
            if (type == "Admission")
            {
                parameter = hostelTaken.ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
            }
            else
            {
                parameter = hostelTaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            }

            send_data_in_student_payment_history_admission(type, slip_no, entry_id, ad_no, parameter, con, PaidAmt, Full_paid_amt, session_id, session, Class_id, admission_no, parameter, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Payment_mode, Transaction_id);
            send_data_to_school_ledger_admission(slip_no, entry_id, con, My.toDouble(Full_paid_amt), session_id, session, Class_id, admission_no, parameter, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName);
            create_admission_annual_dues_admission(parameter, con, PaidAmt, session_id, session, Class_id, admission_no, parameter, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section);
            send_data_in_feetypewise_collection_admission(slip_no, entry_id, parameter, con, PaidAmt, session_id, session, Class_id, admission_no, parameter, Hostel_id, classname, hostelTaken, category_id, sub_category_id, payment_date, studentName, Section);
        }


        private void send_data_to_annual_fee_collection(string slip_no, string entry_id, string parameter, SqlConnection con, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + session + "' and  parameter like '%'" + parameter + "'%' and feetype!='Previous Dues'", con);
            //.ToString("0.00") 
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + admission_no + "' and session='" + session + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = admission_no;
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = "0";
                dr[4] = My.toDouble(txt_adm_ann_fee.Text).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = payment_date;
                dr[8] = "Cash";
                dr[9] = slip_no;
                dr["session"] = session;
                dr["idate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["remark"] = "";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
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

        private void send_data_to_admission_fee_collection_admission(string slip_no, string entry_id, string parameter, SqlConnection con, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + session + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues'", con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + admission_no + "' and session='" + session + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = admission_no;
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = My.toDouble(hd_total_discount.Value).ToString("0.00");
                dr[4] = My.toDouble(hd_paybaleamount.Value).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = payment_date;
                dr[8] = "Cash";
                dr[9] = slip_no;
                dr["session"] = session;
                dr["idate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["remark"] = "";
                dr["entry_id"] = entry_id;
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



        private void send_data_in_feetypewise_collection_admission(string slip_no, string entry_id, string parameter, SqlConnection con, double PaidAmt, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            double paid_amount = PaidAmt;
            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + admission_no + "' and session='" + session + "' and parameter='" + parameter + "' and status='Dues'", con);
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
                        dr["Date"] = payment_date;
                        dr["idate"] = My.toDateTime(payment_date).ToString("yyyyMMdd");
                        if (paid_amount >= dues)
                        {
                            string prevpaid = dr["paid"].ToString();
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = dr["dues"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]).ToString("0.00");
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            #region send in collection slip
                            send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), dues.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], admission_no, session, Class_id, Section, My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, payment_date, Hostel_id, con);
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
                            send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], admission_no, session, Class_id, Section, My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, payment_date, Hostel_id, con);
                            #endregion
                            paid_amount = 0;
                        }
                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = Class_id;
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

        private void create_admission_annual_dues_admission(string parameter, SqlConnection con, double PaidAmt, string session_id, string session, string Class_id, string admission_no, string parameter1, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Section)
        {
            if (payments.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and parameter='" + parameter + "' and session='" + session + "'", con).Rows.Count == 0)
            {
                string query = "";
                if (Hostel_id == "0")
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and (parameter_id='1' or parameter_id='5') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + Class_id + "'";
                }
                else
                {
                    query = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + session + "' and Admission_No='" + admission_no + "' UNION ALL select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + Hostel_id + "' and admission_no='" + admission_no + "'  and (parameter_id='1' or parameter_id='5') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id='" + Hostel_id + "' and Class_id='" + Class_id + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + session + "' and class_id='" + Class_id + "' and fmc.Hostel_Id='" + Hostel_id + "'";
                }
                DataTable dt = payments.dataTable(query, con);
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + admission_no + "','" + classname + "','" + session + "','" + Section + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + Class_id + "','" + ViewState["Userid"].ToString() + "')", con);
                    }
                }
            }
        }


        private void send_data_to_school_ledger_admission(string transcation, string entry_id, SqlConnection con, double PaidAmt, string session_id, string session, string Class_id, string admission_no, string parameter, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = "Fee Collection";
            dr["Discription"] = "Fee for " + studentName + " Adm.No:-" + admission_no + " Total Bill :-" + PaidAmt.ToString() + " Paid Amount :-  " + PaidAmt.ToString();
            dr["AllInput"] = PaidAmt.ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
            dr["Date"] = payment_date;
            dr["TransactionId"] = transcation;
            dr["entry_id"] = entry_id;
            dr["session"] = session;
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = admission_no;
            dr["branchid"] = ViewState["branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void send_data_in_student_payment_history_admission(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con, double PaidAmt, string Full_paid_amt, string session_id, string session, string Class_id, string admission_no, string parameter, string Hostel_id, string classname, string hostelTaken, string category_id, string sub_category_id, string payment_date, string studentName, string Payment_mode, string Transaction_id)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New,Bank_name,Bank_date,Created_date,Created_idate) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New,@Bank_name,@Bank_date,@Created_date,@Created_idate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", session);
            cmd.Parameters.AddWithValue("@Date", payment_date);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(payment_date).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + studentName + " Paid Amount : " + Full_paid_amt.ToString() + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", My.toDouble(Full_paid_amt).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", Payment_mode);
            cmd.Parameters.AddWithValue("@discount", My.toDouble(hd_total_discount.Value).ToString("0.00"));
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
            cmd.Parameters.AddWithValue("@Class_id", Class_id);
            cmd.Parameters.AddWithValue("@Remarks", "Bulk Paid");
            cmd.Parameters.AddWithValue("@User_Slip_no", "");
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", Transaction_id);
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            cmd.Parameters.AddWithValue("@Bank_name", "");
            cmd.Parameters.AddWithValue("@Bank_date", "");
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate()); 
            if (payments.InsertUpdateData(cmd, con))
            {
                // money recpit
                SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "SchoolLedger");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Particulars"] = type + " Money Receipt";
                dr["Discription"] = type + " Money Receipt " + studentName + " Adm.No:-" + admission_no + " Paid Amount :-  " + PaidAmt.ToString("0.00");
                dr["AllInput"] = PaidAmt.ToString("0.00");
                dr["AllOutput"] = "0";
                dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["Date"] = payment_date;
                dr["TransactionId"] = slip_no;
                dr["entry_id"] = entry_id;
                dr["session"] = session;
                dr["Ledger_Type"] = "School";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString();
                dr["Addmission_no"] = admission_no;
                dr["branchid"] = ViewState["branchid"].ToString();
                dr["Unique_id"] = entry_id;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }


        private void send_data_in_fee_collection_slip_adm(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string payment_date, string Hostel_id, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Hostel_id) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + ViewState["branchid"].ToString() + "','0','" + payment_date + "','" + My.DateConvertToIdate(payment_date) + "','" + Hostel_id + "');";
            payments.exeSql(qry, con);
        }
        #endregion
    }
}