using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace school_web.AppCode
{
    public class onlinetest
    {
        public static string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        public void bind_all_ddl_with_id_new(DropDownList ddl, string query)
        {

            ArrayList al = new ArrayList();
            ArrayList a2 = new ArrayList();

            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "tests");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "Select";

            }

            else
            {
                ddl.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddl.DataValueField = ds.Tables[0].Columns[1].ToString();

                //ddl.DataTextField = dt.Rows[0][0].ToString();
                //ddl.DataValueField = dt.Rows[0][1].ToString();
            }

            ddl.DataSource = ds.Tables[0];
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "Select"));
        }

        public void bind_ddl(DropDownList ddl, string query)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            ArrayList ar = new ArrayList();
            ar.Add("Select");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }

        public static DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = conn; //System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
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

        public static Boolean InsertUpdateData(SqlCommand cmd)
        {
            String strConnString = conn;
            SqlConnection con = new SqlConnection(strConnString);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return true;


        }

        public void bind_all_ddl_with_id(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }

        internal string get_session_id(string session)
        {
            SqlCommand cmd = new SqlCommand("Select  session_id from Session_Master where Sessionname='" + session + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }


        internal string get_session()
        {
            SqlCommand cmd = new SqlCommand("Select  Sessionname from Session_Master where Set_Current_Session='1'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "2020-2021";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
    }
}