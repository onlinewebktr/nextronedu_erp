using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace school_web.AppCode
{
    public class Store_procedure_code
    {
        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void LoadStates(DropDownList ddlState)
        {
            try
            {
                using (SqlConnection scon = new SqlConnection(My.conn))
                {
                    SqlCommand cmd = new SqlCommand("[PR_LOAD_STATES]", scon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlState.DataTextField = "STATE";
                    ddlState.DataValueField = "STATE";
                    ddlState.DataSource = dr;
                    ddlState.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// binds date , month and year dropdown 
        /// </summary>
        /// <param name="dateDropDown"></param>
        public static void BindDateDropdown(DropDownList dateDropDown)
        {
            try
            {
                int minDatePart = 0;
                int maxDatePart = 0;

                switch (dateDropDown.ID.Substring(3).ToLower())
                {
                    case "date":
                        minDatePart = 1;
                        maxDatePart = 31;
                        break;
                    case "month":
                        minDatePart = 1;
                        maxDatePart = 12;
                        break;
                    case "year":
                        minDatePart = 1900;
                        maxDatePart = 2099;
                        break;
                    default:
                        break;
                }

                List<int> listOfDateParts = new List<int>();

                for (int datePart = minDatePart; datePart <= maxDatePart; datePart++)
                {
                    listOfDateParts.Add(datePart);
                }

                dateDropDown.DataSource = listOfDateParts;
                dateDropDown.DataBind();
                if (dateDropDown.ID.Substring(3).ToLower() == "year")
                {
                    dateDropDown.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// loads districts of the given state and bind it to the district dropdown list 
        /// </summary>
        public static void LoadDistrictsForState(DropDownList ddlDistrict, string state)
        {
            try
            {
                using (SqlConnection scon = new SqlConnection(My.conn))
                {
                    SqlCommand cmd = new SqlCommand("PR_LOAD_DISTRICTS", scon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STATE", state);
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlDistrict.DataTextField = "DISTRICT";
                    ddlDistrict.DataValueField = "DISTRICT";
                    ddlDistrict.DataSource = dr;
                    ddlDistrict.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// executes sql query and return table of data
        /// </summary>
        public static DataTable executeReaderQuery(string query)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection scon = new SqlConnection(My.conn))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// executes sql query and return table of data
        /// </summary>
        public static DataTable executeReaderQuery(SqlCommand cmd)
        {
            DataTable dt = null;

            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                try
                {
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    string exception = ex.Message;
                }
                finally
                {
                    scon.Close();
                }
            }

            return dt;
        }
        /// <summary>
        /// executes sql stored procedure and return datatable i.e. table of data
        /// </summary>
        public static DataTable executeReaderProcedure(SqlCommand cmd)
        {
            DataTable dt = null;

            using (SqlConnection scon = new SqlConnection(My.conn))
            {

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                        dt = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    string exception = ex.Message;
                }
                finally
                {
                    scon.Close();
                }
            }

            return dt;
        }

        /// <summary>
        /// executes sql stored procedure and return dataset i.e. multiple table of data
        /// </summary>
        public static DataSet executeReaderDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();

            using (SqlConnection scon = new SqlConnection(My.conn))
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

        /// <summary>
        /// executes insert or update stored procedure and returns the number of rows affected
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static int executeNonQuery(SqlCommand cmd)
        {
            int rowsAffected = 0;

            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                scon.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                scon.Close();
            }

            return rowsAffected;
        }

        public static string executeScalar(SqlCommand cmd)
        {
            string result = "";
            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                scon.Open();
                result = Convert.ToString(cmd.ExecuteScalar());
                scon.Close();
            }

            return result;
        }

        /// <summary>
        /// executes sql query and return table of data
        /// </summary>
        public static DataSet executeReaderQueryDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection scon = new SqlConnection(My.conn))
                {
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    scon.Open();
                    da.Fill(ds);
                    scon.Close();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
            return ds;
        }
        /// <summary>
        /// bind_group
        /// </summary>
        public static void bind_group(DropDownList ddl, string procedure)
        {
            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", "DDL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Primary", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist(DropDownList ddl, string procedure)
        {
            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", "DDL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist_withall(DropDownList ddl, string procedure)
        {
            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", "DDL");
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("All", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist(DropDownList ddl, string procedure, string sp_status)
        {
            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(procedure, scon);
                    cmd.Parameters.AddWithValue("@sp_status ", sp_status);
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", "0"));
                    scon.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }
        /// <summary>
        /// loads states of the given country
        /// </summary>
        public static void bind_dropdownlist(DropDownList ddl, SqlCommand cmd)
        {
            using (SqlConnection scon = new SqlConnection(My.conn))
            {
                try
                {
                    cmd.Connection = scon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    scon.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataTextField = "DataTextField";
                    ddl.DataValueField = "DataValueField";
                    ddl.DataSource = dr;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", "0"));
                    scon.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scon.Close();
                }
            }
        }

    }
}