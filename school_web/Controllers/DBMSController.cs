
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace school_web.Controllers
{
    public class DBMSController : Controller
    {
        // GET: DBMS
        [Route("dbms/compare")]
        public ActionResult DB_Compare()
        {
            return View();
        }
        [Route("dbms/comparing")]
        [HttpPost]
        [AllowAnonymous]
        public JsonResult comparing(Dictionary<string, object> data)
        {
            if (data["type"].ToString() == "comparing")
            { 
                string local_con = My.con;
                string server_con = @"Data Source=103.173.192.99,1433\MSE2016;Integrated Security=False;User ID=sa; Password=edunextg2022@#$; Initial Catalog=School_Empty_DB_Edunextg;Max Pool Size=10000;Pooling=true;";
                bool isLocalToServer = false;
                if (data.ContainsKey("isLocalToServer"))
                {
                    isLocalToServer = data["isLocalToServer"].ToString() == "True";
                }
                if (isLocalToServer)
                {
                    local_con = server_con;
                    server_con = My.con;
                }

                var scrp = getCompareScript(server_con, local_con);
                //data copy  
                var tbl_names = new String[] { "Menu_Group_List_web", "MenuMaster_web", "Exam_Menu_Group_List_web", "Exam_MenuMaster_web", "App_Dashboard_Setting", "Cast_category", "Class_Routine", "Color_master", "Complain_Status_Msater", "Complain_Type_Msater", "Day_Master", "Library_Menu_Group_List_web", "Library_MenuMaster_web", "Online_reg_shift_master", "HR_Menu_Master", "SALE_PURCHASE_MENU_GROUP_LIST_WEB", "SALE_PURCHASE_MENUMASTER_WEB", "HMS_attebt_fields", "HMS_Initial_Master", "HR_ShiftSetting", "HR_MasterPageConfig", "Transport_Seat_Model", "HR_SQL_API", "Upload_document_type", "Dashboard_report_menu", "DASHBOARD_REPORT_MENU_ACTIVE", "UQC_Code", "HR_Calendar", "TRANSPORT_VEHICLE_DOCUMENT_MASTER", "Admission_custon_report", "HR_Emp_type", "Transfer_certificate_setting" };
                var backupscript = "";
                foreach (var tbl in tbl_names)
                {
                    backupscript = $"{backupscript}\nGO\n{dataTableScript(tbl, local_con)}";
                }

                foreach (var tbl in tbl_names)
                {
                    scrp = $"{scrp}\nGO\n{dataTableScript(tbl, server_con)}";
                }
                // firm table not exist or data not exit
                // user not exist
                // store not exit

                var batchSeparator = new Regex(@"^\s*GO\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                var qrys = batchSeparator.Split(scrp);
                using (SqlConnection connection = new SqlConnection(local_con))
                {
                    connection.Open();
                    foreach (string batch in qrys)
                    {
                        try
                        {
                            if (batch.Trim() != "")
                            {
                                using (SqlCommand command = new SqlCommand(batch, connection))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                           // My.saveException(ex, batch);
                        }
                    }
                }



                using (SqlConnection connection = new SqlConnection(local_con))
                {
                    connection.Open(); 
                    string sqlInsert = "INSERT INTO UpdateDataBaseHistory (Date,Backup_Data) VALUES (@Date,@BinaryData)"; 
                    using (SqlCommand cmd = new SqlCommand(sqlInsert, connection))
                    {
                        // Add parameter for the binary data
                        cmd.Parameters.AddWithValue("@Date", PayrollMy.Now);
                        cmd.Parameters.AddWithValue("@BinaryData", Compress(Encoding.UTF8.GetBytes(backupscript)));

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }


              
               

                if (!PayrollMy.IsDataExist("select top 1 id from user_details"))
                {
                    PayrollMy.execute("Insert into [user_details] (name,mobile,user_id,password,firm,status,User_Type,Istatus) values (N'Admin', N'9874563210', N'Admin', N'12345', N'1', 'Active', 'Admin', N'1')");
                }

                if (!PayrollMy.IsDataExist("select top 1 id from HR_USERPROFILE"))
                {
                    PayrollMy.execute("Insert into[HR_USERPROFILE] (UserId, Mobile, UserType, Password, Name, ProfileImage, UserTypeId, EmployeeCode, IsHr, HMS_UserId, IsAdmin) values ('Admin', null, N'Admin', N'12345', N'Admin', null, N'0', N'Admin', 1, null, 1)");
                }


                ////"Firm_Branch", "Firm_Details",Parameter_master,, "Month_day", "Month_Index"

                if (!PayrollMy.IsDataExist("select top 1 id from Firm_Branch"))
                {
                    exeGoScript(dataTableScript("Firm_Branch", server_con, false), local_con);
                }
                if (!PayrollMy.IsDataExist("select top 1 id from Firm_Details"))
                {
                    exeGoScript(dataTableScript("Firm_Details", server_con, false), local_con);
                }
                if (!PayrollMy.IsDataExist("select top 1 id from Parameter_master"))
                {
                    exeGoScript(dataTableScript("Parameter_master", server_con, false), local_con);
                }
                if (!PayrollMy.IsDataExist("select top 1 id from Month_day"))
                {
                    exeGoScript(dataTableScript("Month_day", server_con, false), local_con);
                }
                if (!PayrollMy.IsDataExist("select top 1 id from Month_Index"))
                {
                    exeGoScript(dataTableScript("Month_Index", server_con, false), local_con);
                }

                //PayrollMy.execute("update STR_Store_Details set StoreType='Departmental Store' where StoreType='Normal Store'");
                //PayrollMy.execute("update STR_Menu_Master set Store_Type= replace(Store_Type,'Normal','Departmental')");
                var resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Comparing Success",
                    error = false,
                    data = "",
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            var r = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Method Not Allowd",
                error = true,
            });
            return Json(r, JsonRequestBehavior.AllowGet);
        }


        private void exeGoScript(string scrp,string con)
        {
            var batchSeparator = new Regex(@"^\s*GO\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var qrys = batchSeparator.Split(scrp);
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                foreach (string batch in qrys)
                {
                    try
                    {
                        if (batch.Trim() != "")
                        {
                            using (SqlCommand command = new SqlCommand(batch, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // My.saveException(ex, batch);
                    }
                }
            }

        }

        string getCompareScript(string s_con, string t_con)
        {
            var qry = @"SELECT 
  t.name AS TABLE_NAME,
  c.name AS COLUMN_NAME, 
  CASE 
 WHEN ty.[precision] != 0 THEN ty.name WHEN c.max_length=-1 or c.max_length=8000 THEN concat(ty.name,' (MAX)') WHEN ty.name='nvarchar' THEN concat(ty.name,' (',c.max_length/2,')')
  ELSE concat(ty.name,' (',c.max_length,')')
  END AS DATA_TYPE,
  CASE 
  WHEN c.is_nullable = 1 THEN 'YES'
    ELSE 'NO'
  END AS IS_NULLABLE,
  CASE 
    WHEN ic.column_id IS NOT NULL THEN 'YES' 
    ELSE 'NO' 
  END AS IS_PRIMARY_KEY,
  CASE 
    WHEN c.is_identity = 1 THEN 'YES'
    ELSE 'NO'
  END AS IS_AUTO_INCREMENT
FROM 
  sys.tables AS t
INNER JOIN 
  sys.columns AS c ON t.object_id = c.object_id
LEFT JOIN 
  sys.identity_columns AS ic ON t.object_id = ic.object_id AND c.column_id = ic.column_id
INNER JOIN 
  sys.types AS ty ON c.user_type_id = ty.user_type_id
WHERE 
  t.type = 'U' -- User-defined tables
  AND t.name  LIKE '%' or t.name in ('') -- Include system tables
ORDER BY 
  t.name, c.column_id;
";
            var src_dt = PayrollMy.dataTable(qry, s_con);
            var target_dt = PayrollMy.dataTable(qry, t_con);
            var sr = src_dt.AsEnumerable().Select(r => r.Field<string>("TABLE_NAME")).Distinct().ToArray();
            var tr = target_dt.AsEnumerable().Select(r => r.Field<string>("TABLE_NAME").ToUpper()).Distinct().ToArray();
            StringBuilder sb = new StringBuilder();
            var go_statement = "";
            foreach (string table in sr)
            {
                if (!tr.Contains(table.ToUpper()))
                {
                    var drs = src_dt.Select($"TABLE_NAME='{table}'");
                    sb.Append($"{go_statement}Create Table dbo.[{table}] (");
                    var res = "";
                    foreach (var dr in drs)
                    {
                        var primary = "";
                        var auto_increment = "";
                        if (dr["IS_PRIMARY_KEY"].ToString() == "YES")
                        {
                            primary = " Primary Key";
                        }
                        if (dr["IS_AUTO_INCREMENT"].ToString() == "YES")
                        {
                            auto_increment = " Identity(1,1)";
                        }

                        var isnull = "";
                        if (!(dr["IS_NULLABLE"].ToString() == "YES" || dr["IS_NULLABLE"].ToString() == "True"))
                        {
                            isnull = " Not Null";
                        }
                        res += $"\n{dr["COLUMN_NAME"]} {dr["DATA_TYPE"]}{isnull}{primary}{auto_increment},";
                    }
                    sb.AppendLine(res.TrimEnd(',') + ");");
                    go_statement = "\nGO\n\n";
                }
                else
                {
                    var src_drs = src_dt.Select($"TABLE_NAME='{table}'");
                    var trg_drs = target_dt.Select($"TABLE_NAME='{table}'");
                    foreach (DataRow columnRow1 in src_drs)
                    {
                        string columnName1 = columnRow1["COLUMN_NAME"].ToString();
                        string dataType1 = columnRow1["DATA_TYPE"].ToString();
                        bool columnFoundInDatabase2 = false;
                        foreach (DataRow columnRow2 in trg_drs)
                        {
                            string columnName2 = columnRow2["COLUMN_NAME"].ToString();
                            string dataType2 = columnRow2["DATA_TYPE"].ToString();

                            if (columnName1 == columnName2)
                            {
                                columnFoundInDatabase2 = true;
                                if (dataType1 != dataType2)
                                {
                                    sb.AppendLine($"{go_statement}ALTER TABLE {table} ALTER COLUMN {columnName1} {dataType1};");

                                    go_statement = "\nGO\n\n";
                                }
                                break;
                            }
                        }
                        if (!columnFoundInDatabase2)
                        {
                            sb.AppendLine($"{go_statement}ALTER TABLE {table} ADD {columnName1} {dataType1};");
                            go_statement = "\nGO\n\n";
                        }
                    }
                }
            }
            qry = "SELECT name,OBJECT_DEFINITION (object_id) data from sys.procedures  ";

            var src_sp = PayrollMy.dataTable(qry, s_con);
            var target_sp = PayrollMy.dataTable(qry, t_con);

            foreach (DataRow dr in src_sp.Rows)
            {
                var found = false;
                foreach (DataRow dr1 in target_sp.Rows)
                {
                    if (dr["name"].ToString() == dr1["name"].ToString())
                    {
                        found = true;
                        if (!dr["data"].ToString().Trim().Equals(dr1["data"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            var sp_data = dr["data"].ToString();
                            int index = sp_data.IndexOf("create", StringComparison.OrdinalIgnoreCase);
                            if (index >= 0)
                            {
                                string result = "Alter" + sp_data.Substring(index + 6);
                                sb.AppendLine("\nGO\n");
                                sb.AppendLine(result);
                            }
                        }
                        break;
                    }
                }
                if (!found)
                {
                    sb.AppendLine("\nGO\n");
                    sb.AppendLine(dr["data"].ToString());
                }
            }

            return sb.ToString();
        }

        static string dataTableScript(String table_name, string server_con, bool isEmpty = true)
        {
            try
            {

                Dictionary<string, string> col = new Dictionary<String, String>();
                var dt = PayrollMy.dataTable($"select * from {table_name}", server_con); 
                foreach (DataColumn dc in dt.Columns)
                {
                    col[dc.ColumnName] = dc.DataType.Name;
                }
                var sb = new StringBuilder();


                if (dt.Rows.Count > 0 && isEmpty)
                {
                    sb.AppendLine($"truncate table {table_name}; \nGO\n");
                } 
                sb.AppendLine(String.Format("SET IDENTITY_INSERT dbo.[{0}] ON", table_name));

                var scrptformat = "Insert into [" + table_name + "] (" + string.Join(",", col.Keys) + ") values \r\n({0});";


                var datalst = new List<String>();
                foreach (DataRow dr in dt.Rows)
                {
                    var sdata = "";
                    foreach (var key in col.Keys)
                    {

                        if (dr[key] == DBNull.Value)
                            sdata = string.Format("{0}{1},", sdata, "null");
                        else
                            switch (col[key])
                            {
                                case "String":
                                    sdata = string.Format("{0}N'{1}',", sdata, dr[key].ToString().Replace("'", "''"));
                                    break;
                                case "DateTime":
                                    sdata = string.Format("{0}'{1}',", sdata, ((DateTime)dr[key]).ToString("yyyy-MM-dd HH:mm:ss"));
                                    break;
                                case "Boolean":
                                    sdata = string.Format("{0}{1},", sdata, dr[key].ToString().ToLower() == "true" ? "1" : "0");
                                    break;
                                default:
                                    sdata = string.Format("{0}{1},", sdata, dr[key]);
                                    break;
                            };
                    }
                    datalst.Add(sdata.TrimEnd(','));
                    if (datalst.Count == 50)
                    {
                        sb.AppendLine(String.Format(scrptformat, string.Join("),\r\n(", datalst)));
                        datalst = new List<String>();
                    }
                }
                if (datalst.Count > 0)
                {
                    sb.AppendLine(String.Format(scrptformat, string.Join("),\r\n(", datalst)));
                }
                sb.AppendLine(String.Format("SET IDENTITY_INSERT dbo.[{0}] OFF", table_name));

                sb.AppendLine();
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }
        static byte[] Compress(byte[] input)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gzipStream.Write(input, 0, input.Length);
                }

                return outputStream.ToArray();
            }
        }

        static byte[] Decompress(byte[] input)
        {
            using (MemoryStream inputStream = new MemoryStream(input))
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    gzipStream.CopyTo(outputStream);
                }

                return outputStream.ToArray();
            }
        }
    }
}