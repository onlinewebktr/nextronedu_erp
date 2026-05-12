using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode.Onlineexam
{
    public class Find_and_execute_code
    {
        public bool exeSql(string query)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand(query);
                SqlConnection con = new SqlConnection(My.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable featch_data(string query)
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
    }
}