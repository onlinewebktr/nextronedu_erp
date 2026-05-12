using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace school_web.AppCode
{
    public class Library
    {


        public static string session_wisl(string column, string Branch_id)
        {

            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data where Branch_Unique_id='" + Branch_id + "' ", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[column] = "2";
                    result = "1";
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[column].ToString() == "")
                        {
                            result = "1";
                            dr[column] = "2";
                        }
                        else
                        {
                            result = dr[column].ToString();
                            dr[column] = Convert.ToDouble(dr[column]) + 1;
                        }
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
                if (e.Message.EndsWith("does not belong to table Table."))
                {

                    exeSql("Alter table globle_data add " + column + " varchar(500)");
                    result = session_wisl(column, Branch_id);
                }
                else
                {

                }
            }
            return result;
        }

        public static string session_wisl_issue_book(string column, string Branch_id)
        {

            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data where Branch_Unique_id='" + Branch_id + "' ", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[column] = "000002";
                    result = "000001";
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[column].ToString() == "")
                        {
                            result = "000001";
                            dr[column] = "000002";
                        }
                        else
                        {
                            result = dr[column].ToString();
                            result = (Convert.ToInt32(result.ToString()) + 1).ToString("000000");
                            dr[column] = result;
                        }
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
                if (e.Message.EndsWith("does not belong to table Table."))
                {

                    exeSql("Alter table globle_data add " + column + " varchar(500)");
                    result = session_wisl(column, Branch_id);
                }
                else
                {

                }
            }
            return result;
        }

        public static void exeSql(string query)
        {
            DataSet dtc = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, My.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {
            }




        }

        internal static string lib_card_format(string session, string pre_fix, string post_fix, string nodig, string crd, string Branch_id)
        {
            string bill = session_wisl(crd, Branch_id);
            DateTime dtm = DateTime.Now;

            bill = My.toDouble(bill).ToString(nodig);

            return pre_fix + "/" + session + "/" + bill + "/" + post_fix;
        }

        internal static string lib_card_format_teacher_staff(string pre_fix, string post_fix, string nodig, string crd, string Branch_id)
        {
            string bill = session_wisl(crd, Branch_id);


            bill = My.toDouble(bill).ToString(nodig);

            return pre_fix + "/" + bill + "/" + post_fix;
        }
        public string nextdate(string noofday, string date)
        {
            DateTime startTime33 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime nextdate = startTime33.AddDays(Convert.ToInt32(noofday));
            return nextdate.ToString("dd/MM/yyyy");

        }

        public string noof_day_return(string start_data, string enddate)
        {
            DateTime start_date1 = Convert.ToDateTime(start_data);
            DateTime end_date1 = Convert.ToDateTime(enddate);

            int days1 = Convert.ToInt32((end_date1 - start_date1).TotalDays);
            days1 += 1;
            if (days1 > 0)
            {

            }
            else
            {
                days1 = 0;
            }
            return days1.ToString();
        }




        My mycode = new My();
        public string get_serialNo(string branch_id, string type1)
        {
            DataTable dt = mycode.FillData("Select serialNo from Library_Card_NO_Format where Use_mode ='" + type1 + "' and Branch_id='" + branch_id + "' ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }





        public string get_session()
        {
            DataTable dt = mycode.FillData("Select Session from session_details ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        public string get_prefix(string branch_id, string type1)
        {
            DataTable dt = mycode.FillData("Select Prefix from Library_Card_NO_Format where Use_mode ='" + type1 + "' and Branch_id='" + branch_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        public string get_postfix(string branch_id, string type1)
        {
            DataTable dt = mycode.FillData("Select Postfix from Library_Card_NO_Format where Use_mode ='" + type1 + "' and Branch_id='" + branch_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }



        public static string create_random_no_tempbookid()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random();
            int tempo = random.Next(100000, 999999);
            return tempo.ToString() + date;
        }

        internal bool get_book_avl(string uniqid, string Branch_id)
        {
            DataTable dt = mycode.FillData("select Book_Unique_Identifier from Library_Book_Entry where Issued_Status ='Issued' and Book_Unique_Identifier='" + uniqid + "' and Branch_id = '" + Branch_id + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        internal string get_classname_Name(string classid)
        {
            DataTable dt = mycode.FillData("Select Course_Name from Add_course_table where course_id='" + classid + "'     ");
            if (dt.Rows.Count == 0)
            {
                return "NA";
            }
            else
            {
                return dt.Rows[0]["Course_Name"].ToString();
            }
        }

        internal string barcode_num(string column, string Branch_id)
        {
            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data where Branch_Unique_id='" + Branch_id + "' ", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "globle_data");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                result = dr[column].ToString();
                dr[column] = Convert.ToDouble(dr[column]) + 1;
                break;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(ds.Tables[0]);
            return result + check_sum(result);


            //string result = "";
            //try
            //{
            //    SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data where Branch_Unique_id='" + Branch_id + "' ", My.conn);
            //    DataSet ds = new DataSet();
            //    ad.Fill(ds);
            //    if (ds.Tables[0].Rows.Count == 0)
            //    {

            //    }
            //    else
            //    {
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            if (dr[column].ToString() == "")
            //            {
            //                result = "111000000001";
            //                dr[column] = "111000000002";
            //            }
            //            else
            //            {
            //                result = dr[column].ToString();
            //                dr[column] = Convert.ToDouble(dr[column]) + 1;
            //            }
            //            break;
            //        }
            //    }
            //    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            //    ad.Update(ds.Tables[0]);
            //}
            //catch (Exception e)
            //{
            //    if (e.Message.EndsWith("does not belong to table Table."))
            //    {

            //        exeSql("Alter table globle_data add " + column + " varchar(500)");
            //        result = barcode_num(column, Branch_id);
            //    }
            //    else
            //    {

            //    }
            //}
            //return result + check_sum(result);




            ////string result = "";
            ////SqlDataAdapter ad = new SqlDataAdapter("select * from GlobalMaster", My.con);
            ////DataSet ds = new DataSet();
            ////ad.Fill(ds, "GlobalMaster");
            ////foreach (DataRow dr in ds.Tables[0].Rows)
            ////{
            ////    result = dr["barcode"].ToString();
            ////    dr["barcode"] = Convert.ToDouble(dr["barcode"]) + 1;
            ////    break;
            ////}
            ////SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ////ad.Update(ds.Tables[0]);
            ////return result + check_sum(result);
        }

       

        private string check_sum(string sTemp)
        {
            int iSum = 0;
            int iDigit = 0;
            // Calculate the checksum digit here.
            for (int i = sTemp.Length; i >= 1; i--)
            {
                iDigit = Convert.ToInt32(sTemp.Substring(i - 1, 1));
                if (i % 2 == 0)
                {	// odd
                    iSum += iDigit * 3;
                }
                else
                {	// even
                    iSum += iDigit * 1;
                }
            }
            int iCheckSum = (10 - (iSum % 10)) % 10;
            return iCheckSum.ToString();
        }



        internal static Dictionary<string, object> get_liery_master_id_Library(string book_Type, string classname, string book_Status, string book_Category, string location, string branch, string sub_location)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();

            string query_book_Type = "Select TypeId from Library_Type where Branch_Id='" + branch + "' and TypeName='" + book_Type + "' ";
            DataTable dt = FillData_data(query_book_Type);
            if (dt.Rows.Count == 0)
            {
                book_Type = "NA";
            }
            else
            {
                book_Type = dt.Rows[0]["TypeId"].ToString();
            }

            string query_Course_Name = "Select course_id from Add_course_table where Branch_id='" + branch + "' and Course_Name='" + classname + "' ";
            DataTable dt1 = FillData_data(query_Course_Name);
            if (dt1.Rows.Count == 0)
            {
                classname = "NA";
            }
            else
            {
                classname = dt1.Rows[0]["course_id"].ToString();
            } 
            string query_book_Status = "Select BookStatusId from Library_Book_Status where BookStatus='" + book_Status + "' ";
            DataTable dt2 = FillData_data(query_book_Status);
            if (dt2.Rows.Count == 0)
            {
                book_Status = "NA";
            }
            else
            {
                book_Status = dt2.Rows[0]["BookStatusId"].ToString();
            }

            string query_book_Category = "Select  Book_Category_Id from Library_Book_Category where Book_Category='" + book_Category + "' and Branch_Id='" + branch + "'";
            DataTable dt3 = FillData_data(query_book_Category);
            if (dt3.Rows.Count == 0)
            {
                book_Category = "NA";
            }
            else
            {
                book_Category = dt3.Rows[0]["Book_Category_Id"].ToString();
            }

            string query_book_location = "Select  Location_id from lib_location_details where Branch_id = '" + branch + "' and location='" + location + "'";
            DataTable dt4 = FillData_data(query_book_location);
            if (dt4.Rows.Count == 0)
            {
                location = "NA";
            }
            else
            {
                location = dt4.Rows[0]["Location_id"].ToString();
            }

            string query_book_sublocation = "Select  Sub_Location_id from LIB_SUB_LOCATION_DETAILS where Branch_id = '" + branch + "' and Location_id='" + location + "' and Sub_Location='" + sub_location + "'";
            DataTable dt5 = FillData_data(query_book_sublocation);
            if (dt5.Rows.Count == 0)
            {
                sub_location = "0";
            }
            else
            {
                sub_location = dt5.Rows[0]["Sub_Location_id"].ToString();
            }
            dc["book_Type"] = book_Type;
            dc["classname"] = classname;
            dc["book_Category"] = book_Category;
            dc["location"] = location;
            dc["book_Status"] = book_Status;
            dc["Sub_location"] = sub_location;
            return dc;
        }




        internal string get_top_one_class_for_add_book()
        {
            string query = " Select  top 1 SelectClass from Library_Book_Entry  ";
            DataTable dt4 = FillData_data(query);
            if (dt4.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt4.Rows[0][0].ToString();
            }
        }
        private static DataTable FillData_data(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, My.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }



        public string get_qrcode(string barcode_number, string volname, string ISBN, string NameOfBook)
        {
            string rrr = barcode_number;
            BarCode b = new BarCode();
            b.Scale = 2f;
            b.Height = 13.4f;
            // b.draw_number = false;
            b.draw_company_name(NameOfBook);
            //b.draw_text_1(itemName);
            b.draw_text_3("Vol : " + volname);
            //b.draw_text_4("AuthorName. : " + AuthorName);
            b.draw_text_5("ISBN : " + ISBN);
            Bitmap loBMP = b.CreateBitmap(rrr);
            MemoryStream ms = new MemoryStream();
            loBMP.Save(ms, ImageFormat.Png);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            string path = "data:image/gif;base64," + base64Data;
            return path;
        }

        internal string get_barcode_img(string barcode_number, string isbn, string mrp, string bookid, string NameOfBook)
        {
            string rrr = barcode_number;
            BarCode b = new BarCode();
            b.Scale = 2f;
            b.Height = 13.4f;
            // b.draw_number = false;
            if(NameOfBook!="")
            {
                b.draw_company_name(NameOfBook);
            }
            if (NameOfBook != "0")
            {
                b.draw_text_1(isbn);
            }
            
            b.draw_text_3("MRP : " + mrp);
            b.draw_text_4("Book Id : " + bookid);

            Bitmap loBMP = b.CreateBitmap(rrr);
            MemoryStream ms = new MemoryStream();
            loBMP.Save(ms, ImageFormat.Png);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            string path = "data:image/gif;base64," + base64Data;
            return path;
        }

        private string get_short_name()
        {
            return My.get_short_school_name();
        }
        internal string get_barcode_img_issuebook(string barcode_number, string issueid, string date)
        {
            string rrr = barcode_number;
            BarCode b = new BarCode();
            b.Scale = 2f;
            b.Height = 13.4f;
            // b.draw_number = false;

            b.draw_text_1(date);
            b.draw_company_name(issueid);
            Bitmap loBMP = b.CreateBitmap(rrr);
            MemoryStream ms = new MemoryStream();
            loBMP.Save(ms, ImageFormat.Png);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            string path = "data:image/gif;base64," + base64Data;
            return path;
        }

        internal string get_all_book_id_via_uniqe_book_no(string uniqe_book_no)
        {
            string BookId = "";


            string query = "Select  BookId from Library_Book_Entry where Book_Unique_Identifier=" + uniqe_book_no + " and Issued_Status='Issued' ";
            DataTable dt4 = FillData_data(query);
            if (dt4.Rows.Count == 0)
            {
                return "0";
            }
            else
            {

                int k = 0;
                for (int i = 0; i < dt4.Rows.Count; i++)
                {

                    string bookid = dt4.Rows[i]["BookId"].ToString(); ;
                    BookId = BookId += bookid + ",";
                }
                return BookId.TrimEnd(',');

            }


        }

        internal double get_extra_day_fine(string type, string userid)
        {

            double fineamount = 0.00;
            string query = "";
            if (type == "Student")
            {
                query = "Select fine_for_stuent as fineamount from lib_fine_details ";


            }
            else
            {
                string get_user_tpe = My.get_user_type(userid);
                if (get_user_tpe == "Coordinator" || get_user_tpe == "Principal" || get_user_tpe == "Teacher")
                {
                    query = "Select fine_for_teacher as fineamount from lib_fine_details ";
                }
                else
                {
                    query = "Select fine_for_staff as fineamount from lib_fine_details ";
                }

            }


            DataTable dt4 = FillData_data(query);
            if (dt4.Rows.Count == 0)
            {
                fineamount = 0.00;
            }
            else
            {

                fineamount = My.toDouble(dt4.Rows[0][0].ToString());
            }

            return fineamount;
        }

        public static Dictionary<string, object> get_user_menu_permission_data(string UserID, string menupagename)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string user_type = get_user_type(UserID);
            if (user_type == "Admin")
            {
                dc["Is_Edit"] = "1";
                dc["Is_delete"] = "1";
                dc["Is_Download"] = "1";
                dc["Is_Print"] = "1";
                dc["Is_add"] = "1";
            }
            else
            {
                string query = " select mp.*,mgl.Group_name as main_menu,mm.Menu_name as submenu,mm.Menu_page from dbo.[MenuPermissionForUser_web] mp join MenuMaster_web mm on mp.MenuID=mm.MenuID and mp.MainMenuId=mm.Group_id join Menu_Group_List_web mgl  on mm.Group_id=mgl.Group_id  where mp.UserID='" + UserID + "' and mm.Menu_page='" + menupagename + "' and mm.Type=1 order by mgl.Position ";
                SqlCommand cmd;

                cmd = new SqlCommand(query);
                DataTable dt = GetDataq(cmd);

                if (dt.Rows.Count == 0)
                {
                    dc["Is_Edit"] = "1";
                    dc["Is_delete"] = "1";
                    dc["Is_Download"] = "1";
                    dc["Is_Print"] = "1";
                    dc["Is_add"] = "1";

                }
                else
                {
                    dc["Is_Edit"] = "1";
                    dc["Is_delete"] = "1";
                    dc["Is_Download"] = "1";
                    dc["Is_Print"] = "1";
                    dc["Is_add"] = "1";

                    //dc["Is_Edit"] = dt.Rows[0]["Is_Edit"].ToString();
                    //dc["Is_delete"] = dt.Rows[0]["Is_delete"].ToString();
                    //dc["Is_Download"] = dt.Rows[0]["Is_Download"].ToString();
                    //dc["Is_Print"] = dt.Rows[0]["Is_Print"].ToString();
                    //dc["Is_add"] = dt.Rows[0]["Is_add"].ToString();
                }
            }


            return dc;

        }
        private static DataTable GetDataq(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = My.conn;
            SqlConnection con = new SqlConnection(strConnString);

            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
        internal static string get_user_type(string userid)
        {

            string query = "Select User_Type from user_details where user_id='" + userid + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["User_Type"].ToString();

            }
        }
        private static DataTable FillDatastatic(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, My.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }


        public string get_overdueslist(string startdate, string currentday, string type, string sessionid)
        {
            string due_date = "";
            string extraday = "0";
            int rowcount = 0;
            double extra_day_fine = 0.00;
            double getextra_day_fine_amount = 0.00;
            int start_idate = ConvertStringToiDate(startdate);
            int curentdate = ConvertStringToiDate(currentday);
            if (type == "Student")
            {
                // lst.return_idate >= " + idate + " and lst.return_idate <= " + idate2 + "

                extra_day_fine = get_extra_day_fine("Student");
                string query = " select book_no,due_date from dbo.[lib_student_transaction_details] where Session_id=" + sessionid + " and  Due_idate>=" + start_idate + " and Due_idate<=" + curentdate + " and " +
                    "status='Issued'";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    rowcount = 0;
                }
                else
                {
                    rowcount = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        due_date = dt.Rows[i]["due_date"].ToString();
                        extraday = noof_day_return(due_date, mycode.date());
                        getextra_day_fine_amount = getextra_day_fine_amount + (extra_day_fine * My.toDouble(extraday));
                    }


                }





            }
            else
            {

            }
            return getextra_day_fine_amount.ToString("0.00");

        }

        internal double get_extra_day_fine(string type)
        {

            double fineamount = 0.00;
            string query = "";
            if (type == "Student")
            {
                query = "Select fine_for_stuent as fineamount from lib_fine_details ";


                //}
                //else
                //{
                //    string get_user_tpe = My.get_user_type(userid);
                //    if (get_user_tpe == "Coordinator" || get_user_tpe == "Principal" || get_user_tpe == "Teacher")
                //    {
                //        query = "Select fine_for_teacher as fineamount from lib_fine_details ";
                //    }
                //    else
                //    {
                //        query = "Select fine_for_staff as fineamount from lib_fine_details ";
                //    }

            }
            DataTable dt4 = FillData_data(query);
            if (dt4.Rows.Count == 0)
            {
                fineamount = 0.00;
            }
            else
            {

                fineamount = My.toDouble(dt4.Rows[0][0].ToString());
            }

            return fineamount;
        }

        private static int ConvertStringToiDate(string DateInString)
        {
            try
            {
                DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
                return Convert.ToInt32(DateInString);
            }
            catch
            {
                return Convert.ToInt32(0);
            }
        }

        public int Return_back_date(string TodaydatEtim, int noday)
        {
            DateTime ThreestartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string DaysDate = ThreestartTime.AddDays(-noday).ToShortDateString();
            return mycode.ConvertStringToiDate(DaysDate);
        }


        public string Return_back_date_new(string TodaydatEtim, int noday)
        {
            DateTime ThreestartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string DaysDate = ThreestartTime.AddDays(-noday).ToShortDateString();
            return DaysDate;
        }
        internal string get_student_staff_userid(string type, string transaction_no)
        {
            string query = "";
            if (type == "Student")
            {
                query = "Select student_id from lib_student_transaction_details where transaction_no='" + transaction_no + "'";

            }
            else
            {
                query = "Select teacher_id from lib_teacher_trans_action_details where transaction_no='" + transaction_no + "'";
            }
            DataTable dt4 = FillData_data(query);
            if (dt4.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt4.Rows[0][0].ToString(); ;
            }
        }

    }
}