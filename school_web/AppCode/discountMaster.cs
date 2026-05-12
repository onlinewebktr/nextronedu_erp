using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class discountMaster
    {
        internal static void save_discount(string session_id, string class_id, string admission_no, string discount_on, string content_id, string month, string payable_amt, string discount_amt, string slip_no, string month_position, string created_by, string content_name, SqlConnection con)
        {
            if (discount_on == "AdmissionFee")
            {
                discount_on = "Admission";
            }
            if (discount_on == "AnnualFee")
            {
                discount_on = "Annual";
            }
            if (discount_on == "MonthlyFee")
            {
                discount_on = "Monthly";
            }
            My mycode = new My();
            DataTable dt = payments.dataTable("select * from Discount_master_report where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Content_id='" + content_id + "' and Month='" + month + "'", con);
            if (dt.Rows.Count > 0)
            {
                double discount_given = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    discount_given = discount_given + My.toDouble(dr["Discount_amt"].ToString());
                }
                if (My.toDouble(discount_amt) > discount_given)
                {
                    double net_payable_amt = My.toDouble(payable_amt) - discount_given;
                    double discount_amts = My.toDouble(discount_amt) - discount_given;
                    double net_amt = (My.toDouble(net_payable_amt) - My.toDouble(discount_amts));
                    double disc_perc = (My.toDouble(discount_amts) / My.toDouble(net_payable_amt) * 100);
                   
                    if (disc_perc.ToString().ToUpper() == "NAN")
                    {
                        disc_perc = 0;
                    }
                    SqlCommand cmd;
                    string query = "INSERT INTO Discount_master_report (Session_id,Class_id,Admission_no,Discount_on,Content_id,Month,Amount,Discount_amt,Discount_percnt,Net_amt,Bill_no,Created_by,Created_date,Created_time,Created_idate,Month_position,Content) values (@Session_id,@Class_id,@Admission_no,@Discount_on,@Content_id,@Month,@Amount,@Discount_amt,@Discount_percnt,@Net_amt,@Bill_no,@Created_by,@Created_date,@Created_time,@Created_idate,@Month_position,@Content)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                    cmd.Parameters.AddWithValue("@Discount_on", discount_on);
                    cmd.Parameters.AddWithValue("@Content_id", content_id);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Amount", net_payable_amt);
                    cmd.Parameters.AddWithValue("@Discount_amt", discount_amts);
                    cmd.Parameters.AddWithValue("@Discount_percnt", disc_perc);
                    cmd.Parameters.AddWithValue("@Net_amt", net_amt);
                    cmd.Parameters.AddWithValue("@Bill_no", slip_no);
                    cmd.Parameters.AddWithValue("@Created_by", created_by);
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Month_position", month_position);
                    cmd.Parameters.AddWithValue("@Content", content_name);
                    if (payments.InsertUpdateData(cmd, con))
                    {
                    }
                }
            }
            else
            {
                double net_amt = (My.toDouble(payable_amt) - My.toDouble(discount_amt));
                double disc_perc = (My.toDouble(discount_amt) / My.toDouble(payable_amt) * 100);
                disc_perc = My.toDouble(disc_perc.ToString());
                if (disc_perc.ToString().ToUpper() == "NAN")
                {
                    disc_perc= 0;
                } 

                SqlCommand cmd;
                string query = "INSERT INTO Discount_master_report (Session_id,Class_id,Admission_no,Discount_on,Content_id,Month,Amount,Discount_amt,Discount_percnt,Net_amt,Bill_no,Created_by,Created_date,Created_time,Created_idate,Month_position,Content) values (@Session_id,@Class_id,@Admission_no,@Discount_on,@Content_id,@Month,@Amount,@Discount_amt,@Discount_percnt,@Net_amt,@Bill_no,@Created_by,@Created_date,@Created_time,@Created_idate,@Month_position,@Content)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Discount_on", discount_on);
                cmd.Parameters.AddWithValue("@Content_id", content_id);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Amount", payable_amt);
                cmd.Parameters.AddWithValue("@Discount_amt", discount_amt);
                cmd.Parameters.AddWithValue("@Discount_percnt", disc_perc);
                cmd.Parameters.AddWithValue("@Net_amt", net_amt);
                cmd.Parameters.AddWithValue("@Bill_no", slip_no);
                cmd.Parameters.AddWithValue("@Created_by", created_by);
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Month_position", month_position);
                cmd.Parameters.AddWithValue("@Content", content_name);
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }
    }
}