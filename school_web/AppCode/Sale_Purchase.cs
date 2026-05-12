using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class Sale_Purchase
    { 
        internal static void HMS_Item_Account_Ledger_udpate(string firmid, string item_Code, string unit_id, string purchase_date, string unique_entry_id, string stockid, string Input_qty, string output_qty, string entryfrom)
        { 
            string idate = "0";
            try
            {
                idate = purchase_date.Substring(6, 4) + purchase_date.Substring(3, 2) + purchase_date.Substring(0, 2); 
            }
            catch
            { 
            } 
            string qury = " select * from dbo.[HMS_Item_Account_Ledger] where  Item_Code='" + item_Code + "' and Unit_id='" + unit_id + "' and unique_entry_id='" + unique_entry_id + "' and stock_item_unique_entry_id='" + stockid + "' and Idate=" + idate + " and Entry_from='" + entryfrom + "'";
            SqlDataAdapter ad = new SqlDataAdapter(qury, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "stock_details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            { 
                DataRow dr = dt.NewRow();
                dr["Item_Code"] = item_Code;
                dr["Unit_id"] = unit_id;
                dr["Date"] = purchase_date;
                dr["Idate"] = idate;
                dr["unique_entry_id"] = unique_entry_id;
                dr["stock_item_unique_entry_id"] = stockid;
                dr["firm"] = firmid;
                dr["System_date"] = My.getdate1();
                dr["Input_qty"] = Input_qty;
                dr["Output_qty"] = output_qty;
                dr["Entry_from"] = entryfrom; 
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["System_updatedate"] = My.getdate1();
                    dr["Input_qty"] = Input_qty;
                    dr["Output_qty"] = output_qty; 
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            } 
        }

        public static Dictionary<string, object> Firm_details_sale_purchase()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Firm_details_sale_purchase   ";
            DataTable dt = My.dataTable(query);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {

                dc["Is_slip_package_wise"] = "0";
                dc["firm_name"] = "";
                dc["address"] = "0";
                dc["logo"] = "0";
                dc["contact_no"] = "0";
                dc["email"] = "0";
                dc["State"] = "0";
                dc["State_code"] = "0";
            }
            else
            {
                dc["firm_name"] = dt.Rows[0]["firm_name"].ToString();
                dc["address"] = dt.Rows[0]["address"].ToString();
                dc["logo"] = dt.Rows[0]["logo"].ToString();
                dc["contact_no"] = dt.Rows[0]["contact_no"].ToString();
                dc["email"] = dt.Rows[0]["email"].ToString();
                dc["State"] = dt.Rows[0]["State"].ToString();
                dc["State_code"] = dt.Rows[0]["State_code"].ToString();
                try
                {
                    dc["Is_slip_package_wise"] = dt.Rows[0]["Is_slip_package_wise"].ToString();
                }
                catch
                {
                    dc["Is_slip_package_wise"] = "0";
                }
            }
            return dc;

        }

        internal static string get_stock_id(string item_Code, string unit_id, string unique_entry_id)
        { 
            DataTable dt = My.dataTable("select stock_item_unique_entry_id from HMS_INVENTORY_STOCK_DETAILS where Item_Code = '" + item_Code + "' and Unit_id='" + unit_id + "' and unique_entry_id='" + unique_entry_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["stock_item_unique_entry_id"].ToString();
            }
        }


        internal static string toDateWithTime(string p)
        {
            try
            {
                return My.toDateTime(p).ToString("yyyy/MM/dd") + DateTime.Now.ToString(" hh:mm:ss tt");
            }
            catch
            {
                return DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + DateTime.Now.ToString(" hh:mm:ss tt");
            }
        }
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

        public static DataTable dataTable(string query)
        {
            SqlConnection conn1 = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn1);
            DataSet ds = new DataSet();

            ad.Fill(ds);
            return ds.Tables[0];
        }
        public static string data(string query)
        {

            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal static double find_unit_conversion(object Item_Code, object unit, object primary_unit)
        {
            double conversion = 1;
            while (unit.ToString() != primary_unit.ToString())
            {
                DataTable dt = My.dataTable(" select * from dbo.[Inventory_Item_Unit_Maping] where  unit_id='" + unit + "' and Item_Code='" + Item_Code + "'");
                if (dt.Rows.Count == 0)
                {
                    break;
                }
                else
                {
                    conversion *= My.toDouble(dt.Rows[0]["unit_conversion"]);
                    unit = dt.Rows[0]["parent_unit"];
                }
            }
            return conversion;
        }
        public static void update_item_account_ledger(string item_code, string secondary_unit, string status, string item_status, double qty, string descripton, string session, string unique_id, string item_unique_id, string group_id, bool is_stockin, string godown_id, DateTime date, double sale_rate, double cost_rate, bool is_default_user_name = true)
        {
            string pri_unit = data("select Unit_id from HMS_Invetory_item_Master where Item_id='" + item_code + "' and Firm ='" + My.firm_id() + "'");
            double unit_conversion = find_unit_conversion(item_code, secondary_unit, pri_unit);
            Dictionary<string, object> dc1 = new Dictionary<string, object>();
            dc1["Item_Code"] = item_code;
            if (is_stockin)
            {
                #region if stock qty incease
                if (item_status == "Delete")
                {
                    if (secondary_unit == pri_unit)
                    {
                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = pri_unit;
                        dc1["Output_in_primary"] = qty;
                        dc1["Output_in_Transectional"] = qty;
                    }
                    else
                    {
                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = secondary_unit;
                        dc1["Output_in_primary"] = Math.Round(qty / unit_conversion, 2);
                        dc1["Output_in_Transectional"] = qty;
                    }
                }
                else
                {
                    if (secondary_unit == pri_unit)
                    {

                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = pri_unit;
                        dc1["Input_in_primary"] = qty;
                        dc1["Input_in_Transectional"] = qty;
                    }
                    else
                    {
                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = secondary_unit;
                        dc1["Input_in_primary"] = Math.Round(qty / unit_conversion, 2);
                        dc1["Input_in_Transectional"] = qty;
                    }
                }
                #endregion
            }
            else
            {
                #region if item stock qty decrese
                if (item_status == "Delete")
                {
                    if (secondary_unit == pri_unit)
                    {
                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = pri_unit;
                        dc1["Input_in_primary"] = qty;
                        dc1["Input_in_Transectional"] = qty;
                    }
                    else
                    {
                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = secondary_unit;
                        dc1["Input_in_primary"] = Math.Round(qty / unit_conversion, 2);
                        dc1["Input_in_Transectional"] = qty;
                    }
                }
                else
                {
                    if (secondary_unit == pri_unit)
                    {

                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = pri_unit;
                        dc1["Output_in_primary"] = qty;
                        dc1["Output_in_Transectional"] = qty;
                    }
                    else
                    {
                        dc1["primary_unit"] = pri_unit;
                        dc1["Transectional_unit"] = secondary_unit;
                        dc1["Output_in_primary"] = Math.Round(qty / unit_conversion, 2);
                        dc1["Output_in_Transectional"] = qty;
                    }
                }
                #endregion
            }
            string user_name = "";
            string user_Id = "";
            string desc = "";
            if (is_default_user_name) { desc = descripton == "" ? "" : user_name + " " + descripton; }
            else { desc = descripton == "" ? "" : descripton; }
            dc1["Description"] = desc;
            dc1["firm"] = My.firm_id();
            dc1["user_id"] = user_Id;
            dc1["Group_id"] = group_id;
            dc1["session"] = session;
            dc1["unique_entry_id"] = unique_id;
            dc1["Item_unique_entry_id"] = item_unique_id;
            dc1["item_status"] = item_status;
            dc1["status"] = status;
            dc1["Godown_id"] = godown_id;
            dc1["sale_rate"] = sale_rate;
            dc1["cost_rate"] = cost_rate;
            dc1["date"] = date;

            try
            {
                // My.exeSP("sp_HMS_Invetory_insert_item_ledger", dc1);
            }
            catch (Exception ex)
            {
              //  My.Save_Exception(ex.Message + "-->" + ex.StackTrace);
            }
        }

        class State
        {
            public string state { get; set; }
            public string state_code { get; set; }
        }
        public static Dictionary<string, string> state_list;
        internal static object getstateCode(string p)
        {
            if (state_list == null)
            {
                var dt = My.dataTable(" select   * from dbo.[StateList]");
                state_list = new Dictionary<String, String>();
                foreach (DataRow dr in dt.Rows)
                {
                    state_list[dr["State"].ToString()] = dr["Code"].ToString();
                }
            }

            if (state_list.ContainsKey(p))
            {
                return state_list[p];
            }
            else
            {
                return "";
            }
        }

        internal static object get_No_of_days_return()
        {
            try
            {
                DataTable dt = My.dataTable("select No_of_days_return_product from Firm_Details  ");
                if (dt.Rows.Count == 0)
                {
                    return 0;
                }
                else
                {
                    if (dt.Rows[0]["No_of_days_return_product"].ToString() == "")
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(dt.Rows[0]["No_of_days_return_product"].ToString());
                    } 
                }
            }
            catch
            {
                return 0;
            }
        }

        internal static void HMS_Item_Account_Ledger_udpate_tarn(string firmid, string item_Code, string unit_id, string purchase_date, string unique_entry_id, string stockid, string Input_qty, string output_qty, string entryfrom, SqlConnection con)
        {
            string idate = "0";
            try
            {
                idate = purchase_date.Substring(6, 4) + purchase_date.Substring(3, 2) + purchase_date.Substring(0, 2);
                string qury = " select * from dbo.[HMS_Item_Account_Ledger] where  Item_Code='" + item_Code + "' and Unit_id='" + unit_id + "' and unique_entry_id='" + unique_entry_id + "' and stock_item_unique_entry_id='" + stockid + "' and Idate=" + idate + " and Entry_from='" + entryfrom + "'";
                SqlDataAdapter ad = new SqlDataAdapter(qury, con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "stock_details");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                { 
                    DataRow dr = dt.NewRow();
                    dr["Item_Code"] = item_Code;
                    dr["Unit_id"] = unit_id;
                    dr["Date"] = purchase_date;
                    dr["Idate"] = idate;
                    dr["unique_entry_id"] = unique_entry_id;
                    dr["stock_item_unique_entry_id"] = stockid;
                    dr["firm"] = firmid;
                    dr["System_date"] = My.getdate1();
                    dr["Input_qty"] = Input_qty;
                    dr["Output_qty"] = output_qty;
                    dr["Entry_from"] = entryfrom;
                    dt.Rows.Add(dr);
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["System_updatedate"] = My.getdate1();
                        dr["Input_qty"] = Input_qty;
                        dr["Output_qty"] = output_qty;
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                }
            }
            catch
            { 
            } 
        }
    }
}