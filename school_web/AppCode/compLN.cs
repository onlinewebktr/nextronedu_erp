using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;

namespace school_web.AppCode
{
    public class compLN
    {
        //======COMPLAIN
        //public static string comp = "Data Source=198.38.95.145;Integrated Security=False;User ID=Request_School_Erp_Edunextg;Password=Evzx63!2;Max Pool Size=10000;Pooling=true";//ConfigurationManager.ConnectionStrings["comp"].ConnectionString;

        //public static string comp = @" Data Source = 103.173.192.99,1433\MSE2016;Integrated Security = False; User ID = sa; Password=edunextg2022@#$; Initial Catalog=Request_School_Erp_Edunextg_New;Max Pool Size=10000;Pooling=true;";//ConfigurationManager.ConnectionStrings["comp"].ConnectionString;



        //public static string comp = @"Data Source=EDUNEXTG\MSE2016;Integrated Security=False;User ID=sa; Password=edunextg2022@#$; Initial Catalog=Request_School_Erp_Edunextg_New;Max Pool Size=10000;Pooling=true;";
        public static string comp = ConfigurationManager.ConnectionStrings["comp"].ConnectionString;



        //======COMPLAIN
      
        public bool IsUserExist(string query)
        {
            bool status = true;
            DataTable dtTemp = FillData(query);
            if (dtTemp.Rows.Count == 0)
            {
                status = false;
            }
            return status;
        }
        public DataTable FillData(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(compLN.comp);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, compLN.comp);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }


        //=========================COMPLAINS
        public static Boolean InsertUpdateDataComp(SqlCommand cmd)
        {
            bool status = false;

            SqlConnection con = new SqlConnection(compLN.comp);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            status = true;
            con.Close();
            con.Dispose();
            return status;
        }

        public DataTable FillDataComp(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(compLN.comp);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, compLN.comp);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {
            }
            return dtc;
        }
        public static DataSet executeReaderDataSet_comp(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            using (SqlConnection scon = new SqlConnection(compLN.comp))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                scon.Open();
                da.Fill(ds);
                scon.Close();
            }

            return ds;
        }
        public void bind_ddl_NA(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetDataComp(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("NA");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }
        public void executequery_comp(string query)
        {
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(compLN.comp);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public static string auto_serial_comp(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(compLN.comp);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Global ", conn);
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

                            dr[column] = Convert.ToDouble(dr[column]) + 1;
                            result = dr[column].ToString();
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
                    My.exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = auto_serial_comp(column);
                }
                else
                {
                }
            }
            return result;
        }
        public static DataTable dataTable_comp(string query)
        {
            SqlConnection conn = new SqlConnection(compLN.comp);
            SqlDataAdapter ad = new SqlDataAdapter(query, comp);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds.Tables[0];
        }

        public static void exeSql_comp(string query)
        {

            SqlConnection conn = new SqlConnection(compLN.comp);
            SqlDataAdapter ad = new SqlDataAdapter(query, comp);
            DataSet ds = new DataSet();
            ad.Fill(ds);

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
        public static DataTable GetData(SqlCommand cmd)
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
        public static string auto_serial1(string column)
        {
            string result = "";
            try
            {

                SqlDataAdapter ad = new SqlDataAdapter("select * from Global ", compLN.comp);
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

                            dr[column] = Convert.ToDouble(dr[column]) + 1;
                            result = dr[column].ToString();
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
                    My.exeSql("Alter table Global add " + column + " varchar(50)");
                    result = auto_serial1(column);
                }
                else
                {

                }
            }
            return result;
        }

        public void bind_all_ddl_with_id_new(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "New";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("New", "0"));
        }


        ///===========================
        ///

        public void bind_ddl_no_select(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetDataComp(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }

        public void bind_ddl(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetDataComp(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("Select");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }



        public static void bind_ddl_select(DropDownList ddl, string query)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, compLN.comp);
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

        public void bind_ddlALL(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetDataComp(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("ALL");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }
    }
}