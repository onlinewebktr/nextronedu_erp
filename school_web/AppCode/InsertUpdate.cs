using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace school_web.AppCode
{
    public class InsertUpdate
    {
        public static Boolean InsertUpdateData(SqlCommand cmd)
        {

            bool result = false;
            SqlConnection con = new SqlConnection(connection.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            result = true;
            return result;
        }

        public static DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = connection.conn;
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

        public static void submit_exception(Exception exception, string page_name)
        {

            SqlConnection con = new SqlConnection(connection.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Exception", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exception");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            DataRow dr = dt.NewRow();
            dr[1] = exception;
            dr[2] = (DateTime.UtcNow.AddHours(5).AddMinutes(30)).ToString("dd/MM/yyyy");
            dr[3] = (DateTime.UtcNow.AddHours(5).AddMinutes(30)).ToString("yyyyMMdd");
            dr[4] = page_name;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);

        }

        public static void fetch_year(DropDownList ddl)
        {
            ArrayList ar = new ArrayList();
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            int year = dt.AddYears(1).Year;
            for (int i = 2018; i <= year; i++)
            {
                ar.Add(i);
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }



        public static Boolean InsertUpdateData_onlinetest(SqlCommand cmd)
        {

            bool result = false;
            SqlConnection con = new SqlConnection(UsesCode.cononlinetest);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            result = true;
            return result;
        }



        ///===================================
        /// 
        public static DataTable GetDataComp(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = compLN.comp;
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
    }
}