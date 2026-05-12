using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace school_web.Admin
{
    public partial class Upload_Mothley_Fee_Using_CSV : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_excel_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) ");
                        ddlsession.SelectedValue = My.get_session_id(); 
                        ddl_excel_session.SelectedValue = ddlsession.SelectedValue;
                        mycode.bind_all_ddl_with_id_cap_All(ddl_excel_class, "select Course_Name,course_id from Add_course_table order by Position asc");

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

                tblReadCSV.Columns.Add("Admission_No");
                tblReadCSV.Columns.Add("Payment_Date");
                tblReadCSV.Columns.Add("Payment_Mode");
                tblReadCSV.Columns.Add("Payment_Amount");
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

                    string group_transaction = My.create_random_no_otp();
                    string qry = "";


                    string Admission_No = "";
                    string Payment_Date = "";
                    string Payment_Mode = "";
                    string Payment_Amount = "";


                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        Admission_No = grvExcelData.Rows[i].Cells[0].Text;
                        Payment_Date = grvExcelData.Rows[i].Cells[1].Text;
                        Payment_Mode = grvExcelData.Rows[i].Cells[2].Text;
                        Payment_Amount = grvExcelData.Rows[i].Cells[3].Text;
                        // ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        // ViewState["session_id"] = My.get_session_id();
                        Dictionary<string, object> dc1 = My.get_selected_studentinfo(Admission_No, ViewState["session_id"].ToString(), ViewState["branchid"].ToString());
                        string admissionserialnumber = (String)dc1["admissionserialnumber"];
                        string rollnumber = (String)dc1["rollnumber"];
                        string session = (String)dc1["session"];
                        string Section = (String)dc1["Section"];
                        string Class_id = (String)dc1["Class_id"];
                        string classname = (String)dc1["classname"];

                        Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(ViewState["session_id"].ToString(), Class_id, admissionserialnumber);
                        ViewState["Transport_id"] = (String)dc2["Transport_id"];
                        ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                        ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                        ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                        ViewState["Month_name"] = (String)dc2["Month_name"];
                        ViewState["Month_id"] = (String)dc2["Month_id"];
                        ViewState["Year_month"] = (String)dc2["Year_month"];
                        ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];


                        //==========
                        Dictionary<string, object> dc3 = mycode.Bind_hostel_data_for_assined_student(ViewState["session_id"].ToString(), Class_id, admissionserialnumber);
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


                        ViewState["Sessionid"] = ViewState["session_id"].ToString();
                        if (Admission_No != "0")
                        {
                            string query2 = "Select *  from Payment_transaction_process_bulk where Session_id=" + ViewState["Sessionid"].ToString() + "  and Class_id=" + Class_id + " and Admission_no='" + admissionserialnumber + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='Pending' and User_id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "'  and group_transaction='" + group_transaction + "' ";

                            DataTable dt = mycode.FillData(query2);
                            if (dt.Rows.Count == 0)
                            {
                                string Transaction_Id = My.create_random_no_otp();
                                SqlCommand cmd;
                                string query = "INSERT INTO Payment_transaction_process_bulk (Admission_no,Class_id,Session_id,totalAmount,Branch_id,User_id,Status,createddate,Transaction_Id,Payment_Mode,Payment_Date,group_transaction) values (@Admission_no,@Class_id,@Session_id,@totalAmount,@Branch_id,@User_id,@Status,@createddate,@Transaction_Id,@Payment_Mode,@Payment_Date,@group_transaction); ";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                                cmd.Parameters.AddWithValue("@totalAmount", Payment_Amount);
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Status", "Pending");
                                cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                cmd.Parameters.AddWithValue("@Payment_Mode", Payment_Mode);
                                cmd.Parameters.AddWithValue("@Payment_Date", Payment_Date);
                                cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                    // call payment  payment
                                    Update_student_payment(Transaction_Id, Admission_No, Class_id, Payment_Mode, Payment_Date, Payment_Amount);
                                }
                            }
                            else
                            {
                                string Transaction_Id = dt.Rows[0]["Transaction_Id"].ToString();
                                Payment_Amount = dt.Rows[0]["totalAmount"].ToString();
                                Payment_Mode = dt.Rows[0]["Payment_Mode"].ToString();
                                Payment_Date = dt.Rows[0]["Payment_Date"].ToString();
                                admissionserialnumber = dt.Rows[0]["Admission_no"].ToString();
                                Update_student_payment(Transaction_Id, Admission_No, Class_id, Payment_Mode, Payment_Date, Payment_Amount);
                            }
                        }
                        else
                        {
                            string Transaction_Id = My.create_random_no_otp();
                            SqlCommand cmd;
                            string query = "INSERT INTO Payment_transaction_process_bulk (Admission_no,Class_id,Session_id,totalAmount,Branch_id,User_id,Status,createddate,Transaction_Id,Payment_Mode,Payment_Date,group_transaction) values (@Admission_no,@Class_id,@Session_id,@totalAmount,@Branch_id,@User_id,@Status,@createddate,@Transaction_Id,@Payment_Mode,@Payment_Date,@group_transaction); ";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                            cmd.Parameters.AddWithValue("@Class_id", Class_id);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                            cmd.Parameters.AddWithValue("@totalAmount", Payment_Amount);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Status", "Failed");
                            cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                            cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                            cmd.Parameters.AddWithValue("@Payment_Mode", Payment_Mode);
                            cmd.Parameters.AddWithValue("@Payment_Date", Payment_Date);
                            cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }

                    Alertme("Student has been uploaded successfully.", "success");
                    btn_final_submit.Visible = false;
                    string query23 = "Select Admission_no,totalAmount,Status,Payment_Date,Payment_Mode  from Payment_transaction_process_bulk where Session_id=" + ViewState["Sessionid"].ToString() + " and User_id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and  group_transaction='" + group_transaction + "' ";
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


        private void Update_student_payment(string transaction_Id, string admissionserialnumber, string Class_id, string Payment_Mode, string Payment_Date, string Payment_Amount)
        {
            try
            {
                txttotal.Text = "0.00";
                txt_paid_prev.Text = "0.00";
                txt_discount.Text = "0.00";
                txtfineamount.Text = "0.00";
                txt_other_fee.Text = "0.00";
                txttotalbill.Text = "0.00";
                txt_paid_amount.Text = "0.00";
                txt_total_dues.Text = "0.00";
                ViewState["Payment_Mode"] = Payment_Mode;
                ViewState["Payment_Date"] = Payment_Date;
                ViewState["Payment_Amount"] = Payment_Amount;
                ViewState["no_of_months"] = "1";
                ViewState["more_months_check_status"] = "No";
                ViewState["check_one_more_months"] = "0";
                ViewState["checked_after_frst_mnth"] = "0";
                ViewState["MnthName"] = "0";
                ViewState["checked_frst_mnth"] = "0";
                ViewState["fineAmt"] = "0";
                ViewState["checked_mnth"] = "0";
                ViewState["flags1"] = "0";
                ViewState["fine_inserted"] = "0";
                ViewState["Other_Fees"] = "0";
                ViewState["late_fine_no_of_day_month"] = "0";
                ViewState["fine_date_From"] = "0";
                ViewState["fine_date_To"] = "0";
                ViewState["FineType"] = "0";
                string query = "select * from admission_registor where admissionserialnumber='" + admissionserialnumber + "' and Session_id='" + ViewState["session_id"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc";
                SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad_contactus.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    Alertme("Student details not found...", "warning");
                    My.exeSql("update Payment_transaction_process_bulk set  Status='Failed' where Transaction_Id='" + transaction_Id + "' and Admission_no='" + admissionserialnumber + "'");
                    return;
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ViewState["admission_no"] = dr["admissionserialnumber"].ToString();
                        ViewState["rollnumber"] = dr["rollnumber"].ToString();
                        ViewState["studentname"] = dr["studentname"].ToString();
                        ViewState["fathername"] = dr["fathername"].ToString();
                        ViewState["class"] = dr["class"].ToString();
                        ViewState["class_id"] = dr["Class_id"].ToString();
                        ViewState["Section"] = dr["Section"].ToString();

                        if (dt.Rows[0]["hosteltaken"].ToString() == "")
                        {
                            ViewState["hosteltaken"] = "No";
                        }
                        else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                        {
                            ViewState["hosteltaken"] = "No";
                        }
                        else
                        {
                            ViewState["hosteltaken"] = dt.Rows[0]["hosteltaken"].ToString();
                        }

                        ViewState["parameter"] = ViewState["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                        ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                        ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();
                        ViewState["mobilenumber"] = dr["mobilenumber"].ToString();
                        ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                        ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                        ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                        ViewState["group_id"] = "3";
                        ViewState["category_id"] = dr["category_id"].ToString();
                        ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                        ViewState["classid"] = dr["Class_id"].ToString();
                        ViewState["Section"] = dr["Section"].ToString();
                        ViewState["sessionIDs"] = dr["Session_id"].ToString();
                        ViewState["session"] = dr["session"].ToString();
                        ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();
                        ViewState["IsBoarding"] = "0";
                        ViewState["parameteridS"] = "4";
                        string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                        DataTable dts = My.dataTable(queryS);
                        if (dts.Rows.Count != 0)
                        {
                            ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                            ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                            ViewState["IsBoarding"] = "1";
                        }
                        if (dr["Transportation_Id"].ToString() == "")
                        {
                            ViewState["transportID"] = "0";
                        }
                        else
                        {
                            ViewState["transportID"] = dr["Transportation_Id"].ToString();
                        }
                    }
                    // check_box_check

                    find_all_due_fee();
                    calculate_checkbox();




                    // pay final
                    List<string> month_lst = new List<string>();

                    bool payment = false;

                    if (My.toDouble(txttotalbill.Text) >= My.toDouble(ViewState["Payment_Amount"].ToString()))
                    {
                        int pay_idate = My.DateConvertToIdate(ViewState["Payment_Date"].ToString());
                        string slipno = "", entry_id = "";
                        int growcountS = GridView2.Rows.Count;

                        for (int iS = 0; iS < growcountS; iS++)
                        {
                            Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                            CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");
                            if (My.toBool(chk_month.Checked))
                            {
                                month_lst.Add(lbl_Month.Text);
                            }
                        }

                        string total_paid = ViewState["Payment_Amount"].ToString();
                        slipno = My.invoice_monthly("slip_no");
                        entry_id = My.auto_serialS("entry_id");
                        calculate_dues_for_new_month();
                        My.exeSql("update Typewise_fee_collection set position=( select top 1 Position from dbo.[Month_Index] where Month=Typewise_fee_collection.month)");
                        send_data_in_student_payment_history("Monthly", slipno, entry_id, month_lst, total_paid);


                        My.exeSql("update Payment_transaction_process_bulk set  Status='Update' where Transaction_Id='" + transaction_Id + "' and Admission_no='" + admissionserialnumber + "'");

                        My.exeSql("delete from Typewise_fee_collection where admission_no='" + admissionserialnumber + "' and session='" + ViewState["session"].ToString() + "'  and parameter='MonthlyFee' and month in (select month from ( Select sum(cast(paid as float)) paid,month from   dbo.[Typewise_fee_collection] where admission_no='" + admissionserialnumber + "'  and parameter='MonthlyFee' and session='" + ViewState["session"].ToString() + "' group by month) t where  paid=0 )");

                        string monthname = My.get_monthename_paymnet_slip(admissionserialnumber, slipno);

                        double Payment_Amount2 = Convert.ToDouble(ViewState["Payment_Amount"].ToString());
                        string message = "Monthly fee collection for " + ViewState["studentname"].ToString() + " Month " + monthname + ",Paid Amount :" + Payment_Amount2.ToString("0.00") + "/-";
                        My.exeSql("update Student_Payment_History set Description='" + message + "' where Addmission_no='" + admissionserialnumber + "' and Slip_no='" + slipno + "'");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void calculate_checkbox()
        {

            ViewState["MnthName"] = "";
            //looping 
            try
            {
                int growcountS = GridView2.Rows.Count;
                for (int iS = 0; iS < growcountS; iS++)
                {
                    Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                    CheckBox chk = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");
                    if (chk.Checked == true)
                    {
                        string month = lbl_Month.Text;
                        bind_monthly_fee();
                        ViewState["MnthName"] = lbl_Month.Text;
                        //fine_calculation(lbl_Month.Text, "1");

                    }
                    else
                    {
                        bind_monthly_fee();
                        ViewState["MnthName"] = lbl_Month.Text;
                        //fine_calculation(lbl_Month.Text, "1");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
        #region find dues
        private void find_all_due_fee()
        {
            ViewState["parameter"] = ViewState["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
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
            string temp_month = My.get_start_month();
            //  lst.Add(temp_month);



            for (temp = 1; temp <= 12; temp++)
            {
                DataTable paid_dt = My.dataTable(" select month,status from dbo.[Typewise_fee_collection] where   session='" + ViewState["session"].ToString() + "' and admission_no='" + ViewState["admission_no"] + "' and parameter like '%" + ViewState["parameter"].ToString() + "%' and month='" + temp_month + "'");
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
                //   drNewRow["discount_per"] = find_discount(lst[i].ToString(), txt_admission_no.Text, ddlsession.Text, class_id);
                drNewRow["paid_status"] = "NotCreated";
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                i++;
            }
            find_prev_dues(dtDatas);
            GridView2.DataSource = dtDatas.DefaultView;
            GridView2.DataBind();

        }
        DataTable prevdues_dt = new DataTable();
        private void find_prev_dues(DataTable dtDatas)
        {

            prevdues_dt = My.dataTable(" select sum(isnull(cast(dues as float),'0')) dues,month from dbo.[Typewise_fee_collection] where session='" + ViewState["session"].ToString() + "' and status='Dues' and class_id='" + ViewState["class_id"].ToString() + "' and admission_no='" + ViewState["admission_no"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' group by month");
            foreach (DataRow mr in dtDatas.Rows)
            {
                var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                if (row.Length > 0)
                {
                    mr["paid_status"] = "Created";
                    mr["bac_colour"] = "Yellow";
                }
            }
            show_dues(dtDatas);
        }

        double anula_dues = 0; double prev_session_dues = 0;
        double admission_dues = 0; string adm_transection = "";
        private void show_dues(DataTable dtDatas)
        {
            //double month_dues = 0;
            //foreach (DataRow mr in dtDatas.Rows)
            //{
            //    if (My.toBool(mr["Value"]))
            //    {
            //        var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
            //        if (row.Length > 0)
            //        {
            //            DataRow dr = row[0];
            //            month_dues += My.toDouble(dr["dues"]);
            //        }
            //    }
            //}
            //if (month_dues + admission_dues + anula_dues + prev_session_dues == 0)
            //{
            //    //txt_previousduesmonth.Text = "0";
            //    //chk_prev_dues.Visibility = Visibility.Collapsed;
            //    //chk_prev_dues.Uid = "0";
            //    //txt_view.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    //chk_prev_dues.IsChecked = true;
            //    //chk_prev_dues.Visibility = Visibility.Visible;
            //    //txt_previousduesmonth.Text = (month_dues + admission_dues + anula_dues + prev_session_dues).ToString();
            //    //chk_prev_dues.Content = "Previous dues (Rs. " + txt_previousduesmonth.Text + ")";
            //    //chk_prev_dues.Uid = txt_previousduesmonth.Text;
            //    //txt_view.Visibility = Visibility.Visible;
            //}
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

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["firstrow"] == null)
                {
                    CheckBox chk_month = (CheckBox)e.Row.FindControl("chk_month");
                    chk_month.Enabled = true;
                    ViewState["firstrow"] = "1";
                }
            }
        }




        private void bind_monthly_fee()
        {

            ViewState["parameter"] = ViewState["hosteltaken"].ToString().ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";

            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();
            int growcountS = GridView2.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");

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
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and content_id!='6121' ");
                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = ViewState["admission_no"].ToString();
                        dc["session_id"] = ViewState["sessionIDs"].ToString();
                        dc["class"] = ViewState["class"].ToString();
                        dc["session"] = ViewState["session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = ViewState["hosteltaken"].ToString();
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";


                        dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();

                         
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();

                        dc["parameter_id"] = ViewState["parameteridS"].ToString();

                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                         
                        //new08/08/2022

                        string cunrt_session = ViewState["session"].ToString();
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        //int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);


                        string monthid = My.tomonth_numberstring(lbl_Month.Text);

                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);




                        dc["monthid"] = s_year + monthid;




                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        type = "NotCalculated";
                    }
                    feedt.Columns.Add("total_payable");




                    string month = "";
                    double total = 0, fee = 0, disc = 0, paid_prev = 0;
                    foreach (DataRow dr in feedt.Rows)
                    {
                        //if (type == "Calculated")
                        //{
                        //    My.exeSql("update dbo.[Typewise_fee_collection] set Disc='" + dr["disc_amount"].ToString() + "' where Id='" + dr["id"].ToString() + "'");
                        //}
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

                    //lbl_fee_month.Text = month;
                    //lbl_fee_amount.Text = fee.ToString();
                    //lbl_discount.Text = disc.ToString();
                    //lbl_paid_prev.Text = paid_prev.ToString();
                    //lbl_total.Text = total.ToString();
                }
                else
                {
                }
            }

            if (fdt.Rows.Count.ToString() != "0")
            {
                rp_fee_details.DataSource = fdt.DefaultView;
                rp_fee_details.DataBind();
                bind_ttl_fee();// remove
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

            txtfineamount.Text = ViewState["fineAmt"].ToString();

            txttotalbill.Text = (totalpblE + My.toDouble(ViewState["fineAmt"].ToString()) + My.toDouble(txt_other_fee.Text)).ToString();

            txt_paid_amount.Text = ViewState["Payment_Amount"].ToString();
            double dues = My.toDouble(txttotalbill.Text) - My.toDouble(txt_paid_amount.Text);
            txt_total_dues.Text = dues.ToString("0.00");






        }


        private void find_fine(string date, string month)
        {
            try
            {
                double late_fine = 0;
                //DataRow[] drs = dtDatas.Select(" value='true' ");
                int count = 0;
                string m = (My.toDouble(month) + 00).ToString("00");
                string year1 = ViewState["session"].ToString().Split('-')[0];
                string year2 = ViewState["session"].ToString().Split('-')[1];
                string year = "";
                if (My.toDouble(m) < My.toDouble(My.tomonth_number(My.get_start_month())))
                {
                    year = year2;
                }
                else
                {
                    year = year1;
                }
                DateTime start_date = Convert.ToDateTime("01/" + month + "/" + year);
                DateTime end_date = Convert.ToDateTime(ViewState["Payment_Date"].ToString());
                int days = Convert.ToInt32((end_date - start_date).TotalDays);

                DateTime start_date1 = Convert.ToDateTime(DateTime.Now.ToString("01/MM/yyyy"));
                DateTime end_date1 = Convert.ToDateTime(ViewState["Payment_Date"].ToString());


                int days1 = Convert.ToInt32((end_date1 - start_date1).TotalDays);
                days1 += 1;
                DataTable dt = My.dataTable("select top 1 * from dbo.[Fine_Setup] where Session='" + ViewState["session"].ToString() + "' and Status='Active' and Date_Limitation<='" + days1 + "'");

                int limit_days = 0;
                if (dt.Rows.Count.ToString() != "0")
                {
                    limit_days = Convert.ToInt32(dt.Rows[0]["Date_Limitation"].ToString());
                }

                int growcountS = GridView2.Rows.Count;
                for (int iS = 0; iS < growcountS; iS++)
                {
                    Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                    CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");
                    if (chk_month.Checked == true)
                    {
                        double mm = My.toDouble(My.tomonth_number(lbl_Month.Text));
                        string y1 = ViewState["session"].ToString().Split('-')[0];
                        string y2 = ViewState["session"].ToString().Split('-')[1];
                        string y = "";
                        if (mm < My.toDouble(My.tomonth_number(My.get_start_month())))
                        {
                            y = y2;
                        }
                        else
                        {
                            y = y1;
                        }
                        DateTime sd = Convert.ToDateTime("01/" + mm.ToString("00") + "/" + y);
                        DateTime ed = Convert.ToDateTime(ViewState["Payment_Date"].ToString());
                        int d = Convert.ToInt32((ed - sd).TotalDays);
                        if (d > limit_days)
                        {
                            count += 1;
                        }
                    }
                }


                if (days > limit_days)
                {
                    if (dt.Rows.Count.ToString() != "0")
                    {
                        late_fine = My.toDouble(dt.Rows[0]["Fine"].ToString()) * count;
                        txtfineamount.Text = late_fine.ToString();
                        txttotalbill.Text = (My.toDouble(txttotalbill.Text) + My.toDouble(txtfineamount.Text)).ToString();
                    }
                    else
                    {
                        late_fine = 0;
                        txtfineamount.Text = late_fine.ToString();
                        txttotalbill.Text = (My.toDouble(txttotalbill.Text) + My.toDouble(txtfineamount.Text)).ToString();
                    }
                }
                else
                {
                    if (count == 0)
                    {
                        late_fine = 0;
                    }
                    else
                    {
                        if (dt.Rows.Count.ToString() != "0")
                        {
                            late_fine = My.toDouble(dt.Rows[0]["Fine"].ToString()) * count;
                        }
                        else
                        {
                            late_fine = 0;
                        }
                    }
                    txtfineamount.Text = late_fine.ToString();
                    txttotalbill.Text = (My.toDouble(txttotalbill.Text) + My.toDouble(txtfineamount.Text)).ToString();
                }
            }
            catch
            {
                txtfineamount.Text = "0";
            }
        }

        #endregion




        #region payment 
        double ttlFine = 0;

        private void fine_calculation(string monthName, string from)
        {
            int mnth_idss = My.tomonth_number(monthName);
            string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());
            if (from == "1")
            {
                int mnth_ids = My.tomonth_number(monthName);
                string month_id_in_two_dgt = My.getMonthS_twoDigit(mnth_ids.ToString());
                if (ViewState["checked_mnth"].ToString() == "0")
                {
                    ViewState["checked_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_mnth"] = "1";
                }
                else
                {
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                }
            }

            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                #region DayRanGEWise
                string pay_date = ViewState["Payment_Date"].ToString();
                int payidate = My.DateConvertToIdate(pay_date);


                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = ViewState["session"].ToString();
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);


                int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                s_year = My.check_start_months(pay_month, s_year);

                int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                //Advance Payment Check



                if (crnt_month_with_year >= pay_month_with_year)
                {
                    DataTable dtz = mycode.FillData("select top 1 * from Fine_master_day_range");
                    if (dtz.Rows.Count != 0)
                    {
                        ViewState["FineType"] = "DayWise";
                        //string last_day_of_payment = dtz.Rows[0]["No_of_day"].ToString() + "/" + applicable_month + "/" + session_s_year; 
                        string last_day_of_payments = "01" + "/" + month_id_in_two_dgts + "/" + s_year;


                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);

                        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                        ViewState["late_fine_no_of_day_month"] = totaldays;
                        ViewState["fine_date_From"] = last_day_of_payments;
                        ViewState["fine_date_To"] = pay_date;


                        if (chk_latefineapplay.Checked != true)
                        {
                            ViewState["fineAmt"] = "0.00";
                            bind_ttl_fee();
                            return;
                        }


                        DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                        if (dt_fine.Rows.Count != 0)
                        {
                            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()));
                            bind_ttl_fee();
                        }
                        else
                        {
                            DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                            if (dt_fines.Rows.Count != 0)
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()));
                                bind_ttl_fee();
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                                bind_ttl_fee();
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {

                DataTable dt = mycode.FillData("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "'");
                if (dt.Rows.Count != 0)
                {
                    if (chk_latefineapplay.Checked != true)
                    {

                        ViewState["fineAmt"] = "0.00";
                        bind_ttl_fee();
                        return;
                    }

                    string pay_date = ViewState["Payment_Date"].ToString();
                    int payidate = My.DateConvertToIdate(pay_date);



                    string fineType = dt.Rows[0]["Fine_type"].ToString();


                    //Advance Payment Check
                    string crnt_year = mycode.year();
                    string cunrt_session = ViewState["session"].ToString();
                    string session_frst_year = cunrt_session.Substring(0, 4);
                    int session_s_year = My.toint(session_frst_year);
                    int s_year = My.toint(session_frst_year);


                    int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                    s_year = My.check_start_months(pay_month, s_year);

                    if (fineType == "DayWise")//===== Days
                    {
                        #region DayWise

                        int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                        int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                        //Advance Payment Check

                        if (crnt_month_with_year >= pay_month_with_year)
                        {

                            ViewState["FineType"] = "DayWise";
                            string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                            string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;



                            string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;

                            DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);




                            System.TimeSpan diff = enddate1.Subtract(startdate1);
                            int totaldays = Convert.ToInt32(diff.Days);

                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totaldays;
                            ViewState["fine_date_From"] = last_day_of_payments;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totaldays) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, ttl_fine_amt);
                                bind_ttl_fee();
                            }
                            else
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, 0);
                                bind_ttl_fee();
                            }

                        }
                        #endregion
                    }
                    else if (fineType == "MonthWise")//===== MonthWise
                    {
                        #region MonthWise
                        ViewState["FineType"] = "MonthWise";
                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");

                        string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;
                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);
                        int totalmonths = 0;


                        if (ViewState["RepeatFine"].ToString() == "Yes")
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));

                                        double monthdays = 31;
                                        double reminder = My.toDouble(totaldays) % monthdays;
                                        if (Math.Round(reminder) > 0)
                                        {
                                            totalmonths++;
                                        }
                                    }
                                }
                            }
                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payments;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, ttl_fine_amt);
                                bind_ttl_fee();
                            }
                            else
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, 0);
                                bind_ttl_fee();
                            }
                        }
                        else
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                    }
                                }
                            }
                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payment;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * 1;
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, ttl_fine_amt);
                                bind_ttl_fee();
                            }
                            else
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, 0);
                                bind_ttl_fee();
                            }
                        }
                        #endregion
                    }
                    else
                    {

                        #region QuarterWise
                        ViewState["FineType"] = "QuarterWise";
                        double fnl_fine_amt = 0;
                        SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and Q_start_month<='" + ViewState["checked_after_frst_mnth"].ToString() + "'  order by Q_start_month asc", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "Fine_master");
                        DataTable dtm = ds.Tables[0];
                        int rowcount = ds.Tables[0].Rows.Count;
                        if (rowcount == 0)
                        {
                        }
                        else
                        {
                            foreach (DataRow dr in dtm.Rows)
                            {
                                string endmnth_in_two = My.getMonthS_twoDigit(dr["Q_end_month"].ToString());
                                string last_day_of_payment_q = dr["Last_day_to_deposit_fees"].ToString() + "/" + endmnth_in_two + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");




                                DateTime startdate1q = DateTime.ParseExact(last_day_of_payment_q, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                if (dr["Q_payment_mode"].ToString() == "Day")
                                {
                                    System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                    int totaldays = Convert.ToInt32(diff.Days);
                                    ViewState["late_fine_no_of_day_month"] = totaldays;

                                    if (ViewState["flags1"].ToString() == "0")
                                    {
                                        ViewState["fine_date_From"] = last_day_of_payment_q;
                                        ViewState["flags1"] = "1";
                                    }

                                    ViewState["fine_date_To"] = pay_date;

                                    if (My.toDouble(totaldays) > 0)
                                    {
                                        string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                        double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                    }
                                    else
                                    {
                                        fnl_fine_amt = fnl_fine_amt += 0;
                                    }
                                }
                                else
                                {
                                    System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                    int totaldays = Convert.ToInt32(diff.Days);
                                    ViewState["late_fine_no_of_day_month"] = dtm.Rows.Count.ToString();

                                    if (ViewState["flags1"].ToString() == "0")
                                    {
                                        ViewState["fine_date_From"] = last_day_of_payment_q;
                                        ViewState["flags1"] = "1";
                                    }

                                    ViewState["fine_date_To"] = pay_date;
                                    if (My.toDouble(totaldays) > 0)
                                    {
                                        string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                        //double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        double ttl_fine_amt = My.toDouble(fine_amt);
                                        fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                    }
                                    else
                                    {
                                        fnl_fine_amt = fnl_fine_amt += 0;
                                    }
                                }
                            }

                            ViewState["fineAmt"] = fnl_fine_amt.ToString("0.00");
                            bind_ttl_fee();
                        }
                        #endregion
                    }
                }
            }
        }

        private void save_fine_amount(string session_id, string admission_no, string month_id_in_two_dgts, double ttl_fine_amt)
        {
            if (mycode.IsUserExist("select Id from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Temp_fine_monthwise (Session_id,Admission_no,Month_id,Fine_amount,Branch_id) values (@Session_id,@Admission_no,@Month_id,@Fine_amount,@Branch_id);";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Month_id", month_id_in_two_dgts);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Temp_fine_monthwise set Fine_amount=@Fine_amount  where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                if (My.InsertUpdateData(cmd))
                {
                }
            }


            double total_fine = 0;
            int growcountS = GridView2.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");
                int mnth_idss = My.tomonth_number(lbl_Month.Text);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());

                if (chk_month.Checked == true)
                {
                    DataTable dt = mycode.FillData("select Fine_amount from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        total_fine = total_fine + My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                    }
                    else
                    {
                        total_fine = total_fine + 0;
                    }
                }
            }
            ViewState["fineAmt"] = total_fine.ToString("0.00");
        }

        private void calculate_dues_for_new_month()
        {
            int growcountS = GridView2.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)GridView2.Rows[iS].FindControl("chk_month");

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

                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                    {
                        continue;
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = ViewState["admission_no"].ToString();
                        dc["session_id"] = ViewState["sessionIDs"].ToString();
                        dc["class"] = ViewState["class"].ToString();
                        dc["session"] = ViewState["session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = ViewState["hosteltaken"].ToString();
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = ViewState["hosteltaken"].ToString().ToLower() == "yes" ? My.toint(ViewState["hostel_id"].ToString()) : 0;
                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();

                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();

                        dc["parameter_id"] = ViewState["parameteridS"].ToString();
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();

                        //new08/08/2022

                        string cunrt_session = ViewState["session"].ToString();
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(lbl_Month.Text);
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);
                        dc["monthid"] = s_year + monthid;
                        DataTable feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        send_in_typewise_fee(feedt, lbl_Month.Text);


                    }
                }
            }

        }

        private void send_in_typewise_fee(DataTable feedt, string month_name)
        {
            if (feedt.Rows.Count == 0)
            {

            }
            else
            {

                double fine = My.toDouble(txtfineamount.Text);
                if (fine > 0)
                {
                    int mnth_idss = My.tomonth_number(month_name);
                    string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());

                    DataTable dt = mycode.FillData("select Fine_amount from Temp_fine_monthwise where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + feedt.Rows[0]["admission_no"].ToString() + "' and Month_id='" + month_id + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        fine = My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                        My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + feedt.Rows[0]["admission_no"].ToString() + "','" + feedt.Rows[0]["class"].ToString() + "','" + feedt.Rows[0]["session"].ToString() + "','" + feedt.Rows[0]["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine','" + My.toDouble(fine).ToString("0.00") + "','0','" + My.toDouble(fine).ToString("0.00") + "','Dues','" + month_name + "','6121','','School','false','false','false','0.00','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "')");
                    }
                }
                foreach (DataRow dr in feedt.Rows)
                {

                    double otherfee = My.toDouble(txt_other_fee.Text);
                    if (otherfee > 0)
                    {
                        if (ViewState["Other_Fees"].ToString() == "0")
                        {
                            My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Other Fees','" + My.toDouble(otherfee).ToString("0.00") + "','0','" + My.toDouble(otherfee).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','6122','','School','false','false','false','0.00','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["firm_id"].ToString() + "')");
                            ViewState["Other_Fees"] = "1";
                        }

                    }

                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["firm_id"].ToString() + "')");
                }
            }
        }
        private void send_data_in_student_payment_history(string type, string slip_no, string entry_id, List<string> month_lst, string total_paid)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select top 1 * from Student_Payment_History where Addmission_no='" + ViewState["admission_no"].ToString() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Payment_History");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Addmission_no"] = ViewState["admission_no"].ToString();
            dr["Session"] = ViewState["session"].ToString();
            dr["Date"] = ViewState["Payment_Date"].ToString();
            dr["Idate"] = Convert.ToDateTime(ViewState["Payment_Date"].ToString()).ToString("yyyyMMdd");
            dr["Description"] = type + " fee collection for " + ViewState["studentname"].ToString() + " Month " + String.Join(",", month_lst) + ", Paid Amount : " + total_paid + " /-";
            dr["Entry_id"] = entry_id;
            dr["Slip_no"] = slip_no;
            dr["Amount"] = My.toDouble(total_paid).ToString("0.00");
            dr["Type"] = type;
            dr["mode"] = ViewState["Payment_Mode"].ToString();
            dr["Pay_mode_transaction_no"] = "";

            dr["discount"] = My.toDouble(txt_discount.Text).ToString("0.00");
            dr["Discoun_in_School"] = 0;
            dr["Discoun_in_Hostel"] = 0;
            dr["Discoun_in_Transport"] = 0;
            dr["fine"] = txtfineamount.Text;
            dr["is_ofline_sync"] = true;
            dr["Is_online_sync"] = false;
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Remarks"] = "N/A";
            dr["Class_id"] = ViewState["classid"].ToString();
            dr["Transection_in"] = "Software";
            dr["Branch"] = ViewState["Branchid"].ToString();
            dr["parameter_New"] = ViewState["parameter"].ToString();

            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            //send data in school ledger
            update_School_ledger(slip_no, entry_id, total_paid);

            //
            string app_payment_type = "Software";
            DataTable sdt = My.dataTable("select Section,class,rollnumber,Session_id,Class_id,Transfer_Status,hosteltaken,Hostel_id from dbo.[admission_registor] where admissionserialnumber='" + ViewState["admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "'");

            #region update type wise fee collection
            // fine calculation has been zero
            submit_transection_in_typewise(ViewState["admission_no"].ToString(), ViewState["session"].ToString(), My.toDouble(txtfineamount.Text), ViewState["Payment_Date"].ToString(), My.DateConvertToIdate(ViewState["Payment_Date"].ToString()).ToString(), My.toDouble(txt_paid_amount.Text), slip_no, entry_id, sdt.Rows[0]["class"].ToString(), sdt.Rows[0]["Section"].ToString(), sdt.Rows[0]["Class_id"].ToString(), sdt.Rows[0]["hosteltaken"].ToString(), sdt.Rows[0]["Hostel_id"].ToString(), "", app_payment_type);
            #endregion
        }



        private void update_School_ledger(string slip_no, string entry_id, string paid)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select top 1 * from SchoolLedger ", My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "Month Wise Fee Collection";
            dr[1] = "Monthly  Fee  for  " + ViewState["studentname"].ToString() + "  Adm.No:-" + ViewState["admission_no"].ToString() + "Total Bill:- " + txttotalbill.Text + " , Paid Amount :-" + paid.ToString() + " ,  Discount Given:-" + txt_discount.Text + " Dues:-" + txt_total_dues.Text + " Slip No:-" + slip_no;
            dr[2] = My.toDouble(paid).ToString("0.00");
            dr[3] = "0";
            dr[4] = My.toDateTime(ViewState["Payment_Date"].ToString()).ToString("yyyyMMdd");
            dr[5] = ViewState["Payment_Date"].ToString();
            dr[6] = slip_no;
            dr["entry_id"] = entry_id;
            dr["session"] = ViewState["session"].ToString();
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = ViewState["admission_no"].ToString();
            dr["branchid"] = ViewState["Branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad_contactus);
            ad_contactus.Update(dt);
        }
        private void submit_transection_in_typewise(string adno, string session, double fine, string date, string idate, double paid_amount, string slip_no, string entry_id, string classs, string sction, string class_id, string hostel_taken, string hostel_id, string app_transection_id, string app_payment_type)
        {
            #region update dues amount in typewise fee collection
            string parameter = "", month = "", late_fine_month = "", month_position = "";
            string qry = " select *  from Typewise_fee_collection  where admission_no='" + adno + "' and session='" + session + "' and status='Dues' and parameter like '%" + ViewState["parameter"].ToString() + "%' order by cast(Position as float)";


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
                            send_data_in_fee_collection_slip(dr["payable"].ToString(), dues.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid);
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
                                send_data_in_fee_collection_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid);
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
            if (fine > 0)
            {
                My.exeSql("insert into Fine_Fees_collection(Admission_no,Session_id,Date,idate,Description,Slip_no,Amount,Total_no_of_fine_day_month,Fine_date_from,Fine_date_to,Branch_id,User_id,Class_id,Fine_type) values ('" + adno + "','" + ViewState["sessionIDs"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine Fees','" + slip_no + "','" + My.toDouble(fine).ToString("0.00") + "','" + ViewState["late_fine_no_of_day_month"].ToString() + "','" + ViewState["fine_date_From"].ToString() + "','" + ViewState["fine_date_To"].ToString() + "','" + ViewState["firm_id"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["classid"].ToString() + "','" + ViewState["FineType"].ToString() + "')");
            }
        }

        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,Date,Idate,branchid) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + My.toDouble(payable).ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + My.toDouble(prevpaid).ToString("0.00") + "','" + ViewState["Payment_Date"].ToString() + "','" + My.DateConvertToIdate(ViewState["Payment_Date"].ToString()) + "','" + ViewState["Branchid"].ToString() + "');";
            My.exeSql(qry);
        }

        #endregion

        protected void lnk_download_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_excel_session.SelectedItem.Text == "Select")
                {
                    ddl_excel_session.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    string qry = "";
                    if (ddl_excel_class.SelectedItem.Text == "ALL")
                    {
                        qry = "select admissionserialnumber as Admission_No,studentname as Student_name,class as Class,rollnumber as Roll_no,Section,fathername as Father_name,'' as Payment_Date,'' as Payment_Mode,'' as Payment_Amount from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "'";
                    }
                    else
                    {
                        qry = "select admissionserialnumber as Admission_No,studentname as Student_name,class as Class,rollnumber as Roll_no,Section,fathername as Father_name,'' as Payment_Date,'' as Payment_Mode,'' as Payment_Amount from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "' and Class_id='" + ddl_excel_class.SelectedValue + "'";
                    }
                    DataTable dt = My.dataTable(qry);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string attachment = "attachment; filename=Student-list-for-monthly-fee.xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/vnd.ms-excel";
                            string tab = "";
                            foreach (DataColumn dc in dt.Columns)
                            {
                                Response.Write(tab + dc.ColumnName);
                                tab = "\t";
                            }
                            Response.Write("\n");
                            int i;
                            foreach (DataRow dr in dt.Rows)
                            {
                                tab = "";
                                for (i = 0; i < dt.Columns.Count; i++)
                                {
                                    Response.Write(tab + dr[i].ToString());
                                    tab = "\t";
                                }
                                Response.Write("\n");
                            }
                            Response.End();
                            Alertme("Student list with fee has been downloaded successfully.", "success");
                        }


                    }
                    else
                    {
                        Alertme("Student not found.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}