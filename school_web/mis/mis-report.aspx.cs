using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.mis
{
    public partial class mis_report : System.Web.UI.Page
    {
        UsesCode uc = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl_date1.Text = mycode.date();
                lbl_date2.Text = mycode.date();
                fetch_records();
                fetch_Expenses_repot();
                check_balance();

            }
        }

        private void fetch_Expenses_repot()
        {
            lbl_total_expenses.Text = "0.00";
            string query = "select ISNULL(SUM(CAST(Credit AS FLOAT)), 0.00) AS total from dbo.[Account_Voucher_Details] t1  where  VoucherType='Payment' and firm ='1' and  Bill_from='SCHOOL'  and IDate>=" + mycode.idate() + " and  IDate<=" + mycode.idate() + "  and    Credit>0 ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_total_expenses.Text = "0.00";


            }
            else
            {
                lbl_total_expenses.Text = dt.Rows[0]["total"].ToString();

            }


        }

        
        private void check_balance()
        {
            try
            {
                DataTable dt = My.dataTable("select * from Whatsapp_api_config");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string json = My.exeUrl(dr["balanceapi"].ToString());
                        //lbl_aval_balance.Text = json.Split('|')[1].ToString(); //jar["wallet"].ToString();

                        lbl_aval_balance.Text = (My.toDouble(lbl_total_given_msg.Text) - My.toDouble(json.Split('|')[1].ToString())).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_records()
        {
            string idate = mycode.idate();
            string session_id = My.get_session_id();
            get_firm_details();
            //TotalCollection-0
            //DeletedBill-1
            //Total Attended Student-2
            //Total Student - 3
            //Total Attended Employee-4
            //Total Employee-5
            //Total Message-6
            //Total Assign Homeword-7
            //Total new student-8
            //Total inactive student-9
            //ModewiseCollection-10
            //total_classes-11
            //total_marked_classes-12
            //total_marked_student-13
            //total_present_student-14
            //total_absent_student-15
            //total_leave_student-16
            //total_leave_staff-17
            //userwisePaymentCollection-18

            //TotalDeletedBillAmount-19
            //TotalModifiedBill-20
            //TotalModifiedBillAmount-21

            //AdditionaFee-22
            //AdditionaFeeStd-23
            //totalDues-24

            // select count(Id) as TotalStudent from admission_registor where Session_id = '" + session_id + @"' and Status = 1;
            //select count(Class_id) as total_classes from(select distinct Class_id from admission_registor where Session_id = '" + session_id + @"') t;

            string sql = @"select isnull(sum(convert(float, AmountPaid)),0) as AmountPaid from (select isnull(sum(convert(float, Amount)),0) as AmountPaid from Student_Payment_History where Idate='" + idate + "' union select isnull(sum(convert(float, Amount)),0) as AmountPaid from Form_sale_details where idate='" + idate + "' union select isnull(sum(convert(float, Content_Fee)),0) as AmountPaid from Other_Fee_Taken_For_Student where Payment_Idate='" + idate + @"') t; 
                           select count(Id) as Deleted_bill from Student_Payment_History_Save_bakup where CONVERT(VARCHAR(20), CAST(insert_time_date AS DATETIME), 112)='" + idate + @"';
                           select count(Id) as TotalAttendedStudent from Student_Attendance_saved_Class_Wise where Attendance_IDate='" + idate + @"';
                           select count(Id) as TotalStudent from admission_registor where Status=1 and Session_id='" + session_id + @"';
            select count(Id) as attended from HR_Daily_Attendance_Record where Idate='" + idate + @"' and (In_Time is not null or In_Time='');
            select count(Id) as TotalEmp from HR_Employee_Master where Status='Active';
            select isnull(sum(convert(float, No_of_message)),0) as No_of_messageAssigned from Message_assign_history;
            select count(Id) as TotalAssignment from Homework_Details where Upload_Idate='" + idate + @"';
            select count(Id) as NewStd from admission_registor where Created_idate = '" + idate + @"' and Transfer_Status='New'
            select count(Id) as InactiveStd from admission_registor where Inactive_idate='" + idate + @"';   
            select isnull(sum(convert(float, AmountPaid)),0) as AmountPaid,Payment_mode from(select isnull(sum(convert(float, Amount)),0) as AmountPaid,mode as Payment_mode from Student_Payment_History where Idate = '" + idate + "' group by mode union select isnull(sum(convert(float, Amount)), 0) as AmountPaid,Payment_Mode as Payment_mode from Form_sale_details where idate = '" + idate + "' group by Payment_Mode union select isnull(sum(convert(float, Content_Fee)), 0) as AmountPaid,Payment_mode from Other_Fee_Taken_For_Student where Payment_Idate = '" + idate + @"' group by Payment_mode)t group by Payment_mode;
            select count(Class_id) as total_classes from (select distinct Class_id from admission_registor where Session_id = '" + session_id + @"') t;
            select count(Class_id) as total_marked_classes from (select distinct Class_id  from Student_Attendance_saved_Class_Wise where Attendance_IDate='" + idate + @"') t;
            select count(Id) as total_marked_student from Student_Attendance_saved_Class_Wise where Attendance_IDate='" + idate + @"';
            select count(Id) as total_present_student from Student_Attendance_saved_Class_Wise where Attendance_Status='Present' and Attendance_IDate='" + idate + @"';
            select count(Id) as total_absent_student from Student_Attendance_saved_Class_Wise where Attendance_Status='Absent' and Attendance_IDate='" + idate + @"';
            select count(Id) as total_leave_student from Student_Attendance_saved_Class_Wise where Attendance_Status='Leave' and Attendance_IDate='" + idate + @"'
            select count(Id) as total_leave_staff from Staff_leave_details where Status='Approved' and Leave_to_idate>='" + idate + "' and Leave_from_idate<='" + idate + @"'
            select isnull(sum(convert(float, AmountPaid)),0) as AmountPaid,UserBy,(select top 1 name from user_details where user_id=t.UserBy) as User_name from(select isnull(sum(convert(float, Amount)),0) as AmountPaid,user_id as UserBy from Student_Payment_History where Idate='" + idate + "' group by user_id union select isnull(sum(convert(float, Amount)), 0) as AmountPaid,user_id as UserBy from Form_sale_details where idate='" + idate + "' group by user_id union select isnull(sum(convert(float, Content_Fee)), 0) as AmountPaid,Created_by as UserBy from Other_Fee_Taken_For_Student where Payment_Idate='" + idate + @"' group by Created_by)t group by UserBy;
            select isnull(sum(convert(float,Amount)),0) as TotalDeletedAmt from Student_Payment_History_Save_bakup where CONVERT(VARCHAR(20), CAST(insert_time_date AS DATETIME), 112)='" + idate + @"';
            select isnull(count(id),0) as TotalModifiedbill from Student_Payment_History where Created_idate='" + idate + "' and Idate!='" + idate + @"';
            select isnull(sum(convert(float,Amount)),0) as TotalModifiedAmt from Student_Payment_History where Created_idate='" + idate + @"' and Idate!='" + idate + @"'; 
            select isnull(sum(convert(float, Amount)),0) as Total_misc_amt from Misc_Fee_Master_Studentwise where Idate='" + idate + @"' and (Old_year_Dues_Type is null or Old_year_Dues_Type='');
            select count(Total_student) as Total_misc_student from (select count(Admission_No) as Total_student from Misc_Fee_Master_Studentwise where Idate='" + idate + @"' and (Old_year_Dues_Type is null or Old_year_Dues_Type='') group by Admission_No) t;
            select isnull(sum(convert(float, Total_dues)),0) as totalDues from (select t1.admissionserialnumber,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=12)) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + session_id + "' and Status=1) t";
            DataSet ds = mycode.Fill_Data_set(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //==== Today collection
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        lbl_total_fee_collection.Text = dtTemp.Rows[0]["AmountPaid"].ToString();
                    }
                    else
                    {
                        lbl_total_fee_collection.Text = "0";
                    }
                }
                else
                {
                    lbl_total_fee_collection.Text = "0";
                }

                //DeletedBill-1
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[1];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_deleted_bill.Text = dtTemp.Rows[0]["Deleted_bill"].ToString();
                    }
                    else
                    {
                        lbl_deleted_bill.Text = "0";
                    }
                }
                else
                {
                    lbl_deleted_bill.Text = "0";
                }
                //Total Attended Student-2
                //if (ds.Tables[2].Rows.Count > 0)
                //{
                //    DataTable dtTemp = ds.Tables[2];
                //    if (dtTemp.Rows.Count != 0)
                //    {
                //        lbl_attended_std.Text = dtTemp.Rows[0]["TotalAttendedStudent"].ToString();
                //    }
                //    else
                //    {
                //        lbl_attended_std.Text = "0";
                //    }
                //}
                //else
                //{
                //    lbl_attended_std.Text = "0";
                //}
                //Total Student - 3
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[3];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_student.Text = dtTemp.Rows[0]["TotalStudent"].ToString();
                    }
                    else
                    {
                        lbl_total_student.Text = "0";
                    }
                }
                else
                {
                    lbl_total_student.Text = "0";
                }

                //Total Attended Employee-4
                if (ds.Tables[4].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[4];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_attended_emp.Text = dtTemp.Rows[0]["attended"].ToString();
                        lbl_ttl_attended_emps.Text = dtTemp.Rows[0]["attended"].ToString();
                    }
                    else
                    {
                        lbl_attended_emp.Text = "0";
                        lbl_ttl_attended_emps.Text = "0";
                    }
                }
                else
                {
                    lbl_attended_emp.Text = "0";
                    lbl_ttl_attended_emps.Text = "0";
                }
                //Total Employee-5
                if (ds.Tables[5].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[5];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_ttl_employee.Text = dtTemp.Rows[0]["TotalEmp"].ToString();
                        lbl_total_emp.Text = dtTemp.Rows[0]["TotalEmp"].ToString();
                    }
                    else
                    {
                        lbl_total_emp.Text = "0";
                        lbl_ttl_employee.Text = "0";
                    }
                }
                else
                {
                    lbl_total_emp.Text = "0";
                    lbl_ttl_employee.Text = "0";
                }

                //Total Message-6
                if (ds.Tables[6].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[6];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_given_msg.Text = dtTemp.Rows[0]["No_of_messageAssigned"].ToString();
                    }
                    else
                    {
                        lbl_total_given_msg.Text = "0";
                    }
                }
                else
                {
                    lbl_total_given_msg.Text = "0";
                }
                //Total Assign Homeword-7
                if (ds.Tables[7].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[7];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_homework_assign.Text = dtTemp.Rows[0]["TotalAssignment"].ToString();
                    }
                    else
                    {
                        lbl_homework_assign.Text = "0";
                    }
                }
                else
                {
                    lbl_homework_assign.Text = "0";
                }
                //Total new student-8
                if (ds.Tables[8].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[8];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_new_std.Text = dtTemp.Rows[0]["NewStd"].ToString();
                    }
                    else
                    {
                        lbl_new_std.Text = "0";
                    }
                }
                else
                {
                    lbl_new_std.Text = "0";
                }
                //Total Inactive student-9
                if (ds.Tables[9].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[9];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_inactive_std.Text = dtTemp.Rows[0]["InactiveStd"].ToString();
                    }
                    else
                    {
                        lbl_inactive_std.Text = "0";
                    }
                }
                else
                {
                    lbl_inactive_std.Text = "0";
                }


                //ModewiseCollection-10
                if (ds.Tables[10].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[10];
                    if (dtTemp.Rows.Count != 0)
                    {
                        rd_view_modewise.DataSource = dtTemp;
                        rd_view_modewise.DataBind();
                        lbl_total_modewise.Text = Convert.ToInt32(dtTemp.Compute("SUM(AmountPaid)", string.Empty)).ToString();
                    }
                    else
                    {
                        lbl_total_modewise.Text = "0";
                        rd_view_modewise.DataSource = null;
                        rd_view_modewise.DataBind();
                    }
                }
                else
                {
                    lbl_total_modewise.Text = "0";
                    rd_view_modewise.DataSource = null;
                    rd_view_modewise.DataBind();
                }
                //total_classes-11
                if (ds.Tables[11].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[11];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_classes.Text = dtTemp.Rows[0]["total_classes"].ToString();
                    }
                    else
                    {
                        lbl_total_classes.Text = "0";
                    }
                }
                else
                {
                    lbl_total_classes.Text = "0";
                }
                //total_marked_classes-12
                if (ds.Tables[12].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[12];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_marked_classes.Text = dtTemp.Rows[0]["total_marked_classes"].ToString();
                    }
                    else
                    {
                        lbl_total_marked_classes.Text = "0";
                    }
                }
                else
                {
                    lbl_total_marked_classes.Text = "0";
                }
                //total_marked_student-13
                if (ds.Tables[13].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[13];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_marked_student.Text = dtTemp.Rows[0]["total_marked_student"].ToString();
                    }
                    else
                    {
                        lbl_total_marked_student.Text = "0";
                    }
                }
                else
                {
                    lbl_total_marked_student.Text = "0";
                }
                //total_present_student-14
                if (ds.Tables[14].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[14];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_present_student.Text = dtTemp.Rows[0]["total_present_student"].ToString(); 
                        lbl_attended_std.Text = dtTemp.Rows[0]["total_present_student"].ToString();
                    }
                    else
                    {
                        lbl_total_present_student.Text = "0";
                        lbl_attended_std.Text = "0";
                    }
                }
                else
                {
                    lbl_attended_std.Text = "0";
                    lbl_total_present_student.Text = "0";
                }
                //total_absent_student-15
                if (ds.Tables[15].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[15];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_absent_student.Text = dtTemp.Rows[0]["total_absent_student"].ToString();
                    }
                    else
                    {
                        lbl_total_absent_student.Text = "0";
                    }
                }
                else
                {
                    lbl_total_absent_student.Text = "0";
                }
                //total_leave_student-16
                if (ds.Tables[16].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[16];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_leave_student.Text = dtTemp.Rows[0]["total_leave_student"].ToString();
                    }
                    else
                    {
                        lbl_total_leave_student.Text = "0";
                    }
                }
                else
                {
                    lbl_total_leave_student.Text = "0";
                }

                //total_leave_staff-17
                if (ds.Tables[17].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[17];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_leave_staff.Text = dtTemp.Rows[0]["total_leave_staff"].ToString();
                    }
                    else
                    {
                        lbl_total_leave_staff.Text = "0";
                    }
                }
                else
                {
                    lbl_total_leave_staff.Text = "0";
                }

                //UserwisePaymentCollection-18
                if (ds.Tables[18].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[18];
                    if (dtTemp.Rows.Count != 0)
                    {
                        rp_userwisecollection.DataSource = dtTemp;
                        rp_userwisecollection.DataBind();
                        lbl_totalusewise.Text = Convert.ToInt32(dtTemp.Compute("SUM(AmountPaid)", string.Empty)).ToString();
                    }
                    else
                    {
                        lbl_totalusewise.Text = "0";
                        rp_userwisecollection.DataSource = null;
                        rp_userwisecollection.DataBind();
                    }
                }
                else
                {
                    lbl_totalusewise.Text = "0";
                    rp_userwisecollection.DataSource = null;
                    rp_userwisecollection.DataBind();
                }


                //TotalDeletedBillAmount-19
                //TotalModifiedBill-20
                //TotalModifiedBillAmount-21

                //TotalDeletedBillAmount-19
                if (ds.Tables[19].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[19];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_deleted_bill_amount.Text = dtTemp.Rows[0]["TotalDeletedAmt"].ToString();
                    }
                    else
                    {
                        lbl_deleted_bill_amount.Text = "0";
                    }
                }
                else
                {
                    lbl_deleted_bill_amount.Text = "0";
                }

                //TotalModifiedBill-20
                if (ds.Tables[20].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[20];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_modified_bill.Text = dtTemp.Rows[0]["TotalModifiedbill"].ToString();
                    }
                    else
                    {
                        lbl_modified_bill.Text = "0";
                    }
                }
                else
                {
                    lbl_modified_bill.Text = "0";
                }
                //TotalModifiedBillAmount-21
                if (ds.Tables[21].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[21];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_modified_bill_amount.Text = dtTemp.Rows[0]["TotalModifiedAmt"].ToString();
                    }
                    else
                    {
                        lbl_modified_bill_amount.Text = "0";
                    }
                }
                else
                {
                    lbl_modified_bill_amount.Text = "0";
                }

                //TotalModifiedBillAmount-22
                if (ds.Tables[22].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[22];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_Additional_fee.Text = dtTemp.Rows[0]["Total_misc_amt"].ToString();
                    }
                    else
                    {
                        lbl_Additional_fee.Text = "0";
                    }
                }
                else
                {
                    lbl_Additional_fee.Text = "0";
                }

                //TotalModifiedBillAmount-23
                if (ds.Tables[23].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[23];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_Additional_fee_std.Text = dtTemp.Rows[0]["Total_misc_student"].ToString();
                    }
                    else
                    {
                        lbl_Additional_fee_std.Text = "0";
                    }
                }
                else
                {
                    lbl_Additional_fee_std.Text = "0";
                }

                ///===============Total Outstainding
                ///
                //Total Outstainding-24
                if (ds.Tables[24].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[24];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_total_outstainding.Text = dtTemp.Rows[0]["totalDues"].ToString();
                    }
                    else
                    {
                        lbl_total_outstainding.Text = "0";
                    }
                }
                else
                {
                    lbl_total_outstainding.Text = "0";
                }

                try
                {
                    lbl_ttl_staff_absent.Text = (My.toDouble(lbl_ttl_employee.Text) - (My.toDouble(lbl_ttl_attended_emps.Text) + My.toDouble(lbl_total_leave_staff.Text))).ToString();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void get_firm_details()
        {
            DataTable dt = My.dataTable("select firm_name,address1,contact_no,logo,Email_for_mis_report from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString() + ", ";
                lbl_school_address.Text = dt.Rows[0]["address1"].ToString();
                ViewState["Email_for_mis_report"] = dt.Rows[0]["Email_for_mis_report"].ToString();
            }
        }

        protected void btn_sendmail_Click(object sender, EventArgs e)
        {
            try
            {
                string email_id = ViewState["Email_for_mis_report"].ToString();
                string subject = "Daily MIS Report of " + mycode.date();
                StringWriter stringWrite = new StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
                pnl_otp_mail.RenderControl(htmlWrite);
                string htmlStr = stringWrite.ToString();
                uc.sendemail(email_id, subject, htmlStr);
                lbl_s_msg.Text = "Send successfully to : " + email_id;
            }
            catch (Exception ex)
            {
            }
        }
    }
}

