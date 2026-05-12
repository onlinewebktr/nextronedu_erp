using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin
{
    public partial class Lib_Home : System.Web.UI.Page
    {
        My mycod = new My();
        Library ly = new Library();
        protected void Page_Load(object sender, EventArgs e)
        {
             
            if (!IsPostBack)
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
                            Session["firm"] = My.get_firm_id();
                            hd_session.Value = My.get_session_id();
                            string TodaydatEtim = lbl_date.Text = mycod.date();

                            //Threeday
                            DateTime ThreestartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string ThreeDaysDate = ThreestartTime.AddDays(-3).ToShortDateString();


                            lbl_over_3days_student.InnerText = ly.get_overdueslist(ThreeDaysDate, TodaydatEtim, "Student", hd_session.Value);



                            //7DayS
                            DateTime SevenstartTime = DateTime.ParseExact(ThreeDaysDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            string SevenDaysDate = SevenstartTime.AddDays(-7).ToShortDateString();

                            lbl_over_7days_student.InnerText = ly.get_overdueslist(SevenDaysDate, ThreeDaysDate, "Student", hd_session.Value);

                            //15DayS
                            DateTime fifteenstartTime = DateTime.ParseExact(SevenDaysDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            
                            string fifteendaysTime = fifteenstartTime.AddDays(-15).ToShortDateString();

                            lbl_over_15days_student.InnerText = ly.get_overdueslist(fifteendaysTime, SevenDaysDate, "Student", hd_session.Value);

                            //30DayS
                            DateTime thertystartTime = DateTime.ParseExact(fifteendaysTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string thertydaysTime = thertystartTime.AddDays(-30).ToShortDateString();

                            lbl_over_30days_student.InnerText = ly.get_overdueslist(thertydaysTime, fifteendaysTime, "Student", hd_session.Value);



                            Bind_count_all();//
                            Bind_count_all_today();//
                        }


                    }


                }
                catch (Exception ex)
                {
                    My.submitException(ex, "DefaultPage");
                }
            }

        }



        private void Bind_count_all()
        {
            lbl_tolat_Available_book.InnerText = "0";
            lbl_total_issue_book_student.InnerText = "0";
            lbl_total_issue_book_staff.InnerText = "0";
            lbl_total_return_book_student.InnerText = "0";
            lbl_total_return_book_staff.InnerText = "0";
            lbl_total_return_fine_student.InnerText = "0.00";
            lbl_total_retun_fine_staff.InnerText = "0.00";
            lbl_total_issued_Card_student.InnerText = "0";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@sessionid", hd_session.Value);
            cmd.Parameters.AddWithValue("@idate", mycod.idate());
            cmd.Parameters.AddWithValue("@status", "0");// all
            cmd.CommandText = "sp_library_dashboard";
            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (Convert.ToString(dt.Rows.Count) == "0")
            {
                lbl_tolat_Available_book.InnerText = "0";
                lbl_total_issue_book_student.InnerText = "0";
                lbl_total_issue_book_staff.InnerText = "0";
                lbl_total_return_book_student.InnerText = "0";
                lbl_total_return_book_staff.InnerText = "0";
                lbl_total_return_fine_student.InnerText = "0.00";
                lbl_total_retun_fine_staff.InnerText = "0.00";
            }
            else
            {
                lbl_tolat_Available_book.InnerText = dt.Rows[0]["totalAbvilable"].ToString();
                lbl_total_issue_book_student.InnerText = dt.Rows[0]["issued_book_student"].ToString();
                lbl_total_issue_book_staff.InnerText = dt.Rows[0]["issued_book_staff"].ToString();
                lbl_total_return_book_student.InnerText = dt.Rows[0]["returned_book_student"].ToString();
                lbl_total_return_book_staff.InnerText = dt.Rows[0]["returned_book_staff"].ToString();
                double return_fine_student = My.toDouble(dt.Rows[0]["Total_Fine_student"].ToString());
                lbl_total_return_fine_student.InnerText = return_fine_student.ToString("0.00");
                double retun_fine_staff = My.toDouble(dt.Rows[0]["Total_fine_staff"].ToString());

                lbl_total_retun_fine_staff.InnerText = retun_fine_staff.ToString("0.00");
                lbl_total_issued_Card_student.InnerText = dt.Rows[0]["totall_issued_card_for_student"].ToString();

            }
        }

        private void Bind_count_all_today()
        {
            lbl_today_penind_book_staf.InnerText = "0";
            lbl_today_penind_book_student.InnerText = "0";
            lbl_total_issue_book_student_today.InnerText = "0";
            lbl_total_issue_book_staff_today.InnerText = "0";
            lbl_total_return_book_student_today.InnerText = "0";
            lbl_total_return_book_staff_today.InnerText = "0";
            lbl_total_return_fine_student_today.InnerText = "0.00";
            lbl_total_retun_fine_staff_today.InnerText = "0.00";
            lbl_total_issued_Card_student_today.InnerText = "0";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@sessionid", hd_session.Value);
            cmd.Parameters.AddWithValue("@idate", mycod.idate());
            cmd.Parameters.AddWithValue("@status", "1");// all
            cmd.CommandText = "sp_library_dashboard";
            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (Convert.ToString(dt.Rows.Count) == "0")
            {

            }
            else
            {

                lbl_today_penind_book_staf.InnerText = dt.Rows[0]["returned_book_staff_pending"].ToString();
                lbl_today_penind_book_student.InnerText = dt.Rows[0]["returned_book_student_pending"].ToString();
                lbl_total_issue_book_student_today.InnerText = dt.Rows[0]["issued_book_student"].ToString();
                lbl_total_issue_book_staff_today.InnerText = dt.Rows[0]["issued_book_staff"].ToString();
                lbl_total_return_book_student_today.InnerText = dt.Rows[0]["returned_book_student"].ToString();
                lbl_total_return_book_staff_today.InnerText = dt.Rows[0]["returned_book_staff"].ToString();
                double return_fine_student = My.toDouble(dt.Rows[0]["Total_Fine_student"].ToString());
                lbl_total_return_fine_student_today.InnerText = return_fine_student.ToString("0.00");
                double retun_fine_staff = My.toDouble(dt.Rows[0]["Total_fine_staff"].ToString());

                lbl_total_retun_fine_staff_today.InnerText = retun_fine_staff.ToString("0.00");
                lbl_total_issued_Card_student_today.InnerText = dt.Rows[0]["totall_issued_card_for_student"].ToString();
            }
        }
    }
}