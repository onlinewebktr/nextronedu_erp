using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.AppCode
{
    public class UsesCode
    {

        public string get_firstyear(string session)
        {
            // Split authors separated by a comma followed by space  
            string[] stringSeparators = new string[] { "-" };
            string[] arr = session.Split(stringSeparators, StringSplitOptions.None);

            string second = arr[0];
            return second;
        }
        internal void bind_all_ddl_with_id_cap_NA(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("NA", "0"));
        }


        string sMonth = DateTime.Now.ToString("MM");
        public string getmonthval()
        {
            return DateTime.Now.ToString("MMM");
        }


        public string getdayname(string date)
        {
            try
            {
                DateTime d1 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return d1.ToString("dddd");
            }
            catch
            {
                return "0";
            }
        }

        public string ConvertStringTomonth(string DateInString) //Format ::  MM  
        {

            return DateInString.Substring(3, 2);
        }
        public string ConvertStringToday(string DateInString) //Format ::  DD  
        {

            return DateInString.Substring(0, 2);
        }
        public string ConvertStringToyear(string DateInString) //Format ::  Year  
        {
            return DateInString.Substring(6, 4);
        }


        public string time()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HH:mm:ss tt");
        }
        public DateTime datetime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30);
        }
        public string itime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HHmmss");
        }
        public string date()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
        }
        public string idate()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
        }
        public string geturl()
        {
            string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
            string[] New_originalPath1 = originalPath2.Split('?');
            return New_originalPath1[0].ToString();
        }
        public string ConvertStringToiDate(string DateInString) //Format :: dd/MM/yyyy
        {
            DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
            return DateInString;
        }

        public int ConvertStringToiDateint(string DateInString) //Format :: dd/MM/yyyy
        {
            DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
            return Convert.ToInt32(DateInString);
        }


        public string iMonthBackdate()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddMonths(-1).ToString("yyyyMMdd");
        }
        public string daysback15()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-30).ToString("dd/MM/yyyy");
        }
        public string sevendaysback()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-7).ToString("dd/MM/yyyy");
        }

        public string getdate1()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MMM/yyyy hh:mm:ss tt");
        }
        public DateTime getdate2(string date)
        {
            //return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MMM/yyyy hh:mm:ss tt");
            string mergeStartTime = date + " " + time();
            DateTime startTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
            return startTime;
        }


        public DateTime getdate(string date)
        {

            DateTime d1 = DateTime.ParseExact(date, "dd/MMM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


            return d1;

        }
        internal static string reg_format_Scholarship(string column_name)
        {
            string bill = UsesCode.auto_serial1(column_name);
            string pre_fix = get_Scholarship_prifix();
            return pre_fix + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd") + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss") + bill;
        }

        private static string get_Scholarship_prifix()
        {
            try
            {
                DataTable dt = FillDatastatic("Select Scholarship_prifix from Firm_Details  ");
                if (dt.Rows.Count == 0)
                {
                    return "SCH";
                }
                else
                {
                    return dt.Rows[0]["scholarship_prifix"].ToString();
                }
            }
            catch
            {
                return "SCH";
            }
        }

        public void GrdData(DataTable sourcetable, GridView gridContainer)
        {
            try
            {
                if (sourcetable.Rows.Count > 0)
                {
                    gridContainer.DataSource = sourcetable;
                    gridContainer.DataBind();
                }
                else
                {
                    gridContainer.EmptyDataText = "No records found.";
                    gridContainer.DataSource = null;
                    gridContainer.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }


        public void executequery(string query)
        {


            SqlCommand cmd;
            cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(connection.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();

            con.Close();
            con.Dispose();


        }

        public static void exeSql(string query)
        {

            SqlConnection conn = new SqlConnection(connection.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);

        }
        public static string find_otp()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random(DateTime.Now.Millisecond);

            int tempo = random.Next(100000, 999999);
            return tempo.ToString();
        }
        public string Right(string text, int length)
        {
            string result = text.Substring(text.Length - length, length);
            return result;
        }

        public string GetDefaultImage(string Gender)
        {
            string imagePath = "";
            if (Gender == "Male") { imagePath = "/images/male.png"; }
            else { imagePath = "/images/female.png"; }
            return imagePath;
        }

        public string AlphaNumericPaswd(int KeyLength, bool IsAlphaNumeric)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (IsAlphaNumeric)
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = KeyLength;
            string key = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (key.IndexOf(character) != -1);
                key += character;
            }
            return key;
        }
        public void Save(string message)
        {
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";

        }
        public string GenerateRandomNumber(int start, int end)
        {  //10000000  99999999
            Random random = new Random();
            int temp = random.Next(start, end);
            return temp.ToString();
        }
        public string Auto_generate_user_id(string query, int i, int j)
        {
            string user_id = "";
            bool duplicateid = false;
            Random rn = new Random();
            do
            {
                int k = rn.Next(i, j);
                user_id = k.ToString();
                duplicateid = check_duplicate_regid(query + user_id);

                if (duplicateid == true)
                {

                }
            }
            while (duplicateid == false);

            return user_id;
        }


        public static bool check_duplicate_regid(string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public static void submitexception(string ex)
        {
            SqlCommand cmd;
            string strQuery = @"INSERT INTO Exception_details (ExceptionMessage,Date,Idate,Time) values (@ExceptionMessage,@Date,@Idate,@Time)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@ExceptionMessage", ex);
            cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss"));
            if (InsertUpdate.InsertUpdateData(cmd))
            {
            }
        }


        public static void submitexception1(Exception ex)
        {
            SqlCommand cmd;
            string strQuery = @"INSERT INTO Exception_details (ExceptionMessage,Date,Idate,Time) values (@ExceptionMessage,@Date,@Idate,@Time)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@ExceptionMessage", ex);
            cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss"));
            if (InsertUpdate.InsertUpdateData(cmd))
            {
            }
        }

        public void bind_all_List_with_id(ListBox ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
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

        public void bind_all_ddl_with_id(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
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

        public void bind_all_ddl_with_id_manually(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Manually";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Manually", "0"));
        }

        public void bind_all_ddl_with_id_bus(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Bus";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Bus", "0"));
        }

        public void bind_all_list_with_id(ListBox lst, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                lst.DataTextField = dt.Columns[0].ToString();
                lst.DataValueField = dt.Columns[1].ToString();
            }

            lst.DataSource = dt;
            lst.DataBind();
        }


        public void bind_all_ddl_with_all(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "ALL";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("ALL", "0"));
        }



        public void bind_ddl(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
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



        public void bind_ddl_month(DropDownList ddl)
        {
            for (int month = 1; month <= 12; month++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                ddl.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
            }
        }


        public void bind_txt(TextBox txt, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                txt.Text = "";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    txt.Text = "";
                }
                else
                {
                    txt.Text = dt.Rows[0][0].ToString();
                }
            }

        }
        public static double converttodouble(string p)
        {
            try
            {
                return Convert.ToDouble(p);
            }
            catch
            {
                return 0;
            }
        }
        public bool ValidateNumber(string number)
        {
            try
            {
                double _num = Convert.ToDouble(number.Trim());
            }
            catch
            {
                return false;
            }
            return true;
        }


        public int ValidateNumberint(string number)
        {
            int _num = 0;
            try
            {
                _num = Convert.ToInt32(number.Trim());
            }
            catch
            {

            }
            return _num;
        }



        public void BindRepeater(string sql, Repeater rptr)
        {
            DataTable dt = FillTable(sql);
            if (dt.Rows.Count != 0)
            {
                rptr.DataSource = dt;
                rptr.DataBind();
            }
            else
            {
                rptr.DataSource = null;
                rptr.DataBind();
            }
        }

        public DataTable binddatatable(string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = InsertUpdate.GetData(cmd);
            return dt;
        }

        public void bind_gridview(GridView gridview, string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                //gridview.Visible = false;
                gridview.DataSource = null;
                gridview.EmptyDataText = "Data Not Available";
                gridview.DataBind();
            }
            else
            {
                //gridview.Visible = true;
                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }




        public void bind_Datalist(DataList dl_list, string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dl_list.DataSource = null;
                dl_list.DataBind();
            }
            else
            {
                dl_list.DataSource = dt;
                dl_list.DataBind();
            }
        }
        public string FindNameWithQuery(string Query, string requestData)
        {
            string Name = "";
            SqlCommand cmd = new SqlCommand(Query + requestData + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                Name = " ";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    Name = " ";
                }
                else
                {
                    Name = dt.Rows[0][0].ToString();
                }
            }
            return Name;
        }
        public string[] GetUploadList(string filesName)
        {
            string[] files = Directory.GetFiles(filesName);
            string[] fileNames = new string[files.Length];
            Array.Sort(files);

            for (int i = 0; i < files.Length; i++)
            {
                fileNames[i] = Path.GetFileName(files[i]);
            }

            return fileNames;
        }

        public string UploadImage(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 1000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        if (File.Exists(ServerPath))
                        {
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(ServerPath);

                        }
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    ImagePath = FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }
        // user code creation
        public string code_creation()
        {
            string code = "";
            Random rn = new Random();
            int i = 10000000;
            int j = 99999999;

            int k = rn.Next(i, j);
            code = k.ToString();


            return code;
        }
        public string UploadAudio(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 6000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".mp3", ".avi", ".mp4", ".wmv" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    ImagePath = FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }

        public string UploadPDF(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 10000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".pdf", ".PDF", ".doc", ".docx", ".ppt" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    ImagePath = FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }

        public string Upload_doc_images(FileUpload fileName, string FolderPath)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string ImagePath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileName.HasFile)
            {
                if (fileName.FileBytes.Length < 5000000)
                {
                    string FileExtension = Path.GetExtension(fileName.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".pdf", ".PDF", ".doc", ".docx", ".ppt", ".jpg", ".jpeg", ".png", ".gif" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {
                        string ServerPath = HttpContext.Current.Server.MapPath("~/" + FolderPath);
                        if (File.Exists(ServerPath))
                        {
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(ServerPath);

                        }
                        fileName.SaveAs(ServerPath + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                    string[] New_originalPath1 = originalPath2.Split('?');
                    string originalPath1 = New_originalPath1[0].ToString();

                    ImagePath = originalPath1 + FolderPath + Path.GetFileName(rename);
                }
            }
            return ImagePath;
        }

        public void FileSave(FileUpload fl, string path)
        {
            if (fl.HasFile)
            {
                if (fl.FileBytes.Length < 1000000)
                {
                    string theFileName = Path.Combine(path, fl.FileName);
                    if (File.Exists(theFileName))
                    {
                        File.Delete(theFileName);
                    }
                    fl.SaveAs(theFileName);
                }
                else
                {
                }
            }
        }

        public string Auto_sl_id(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "10001";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return "10001";
                }
                else
                {
                    return (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
                }
            }
        }

        public string sl_id(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "00001";

            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return "00001";
                }
                else
                {
                    string a = Right(dt.Rows[0][0].ToString(), 5);
                    int rno = int.Parse(a) + 1;
                    return rno.ToString("00000");
                }
            }
        }
        public string Filesave(FileUpload fl, string path, string folder_name)
        {
            string itime = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("ddMMyyyyHHmmss");
            string dbfilepath = "";
            string rename = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fl.HasFile)
            {
                if (fl.FileBytes.Length < 1000000)
                {
                    string FileExtension = Path.GetExtension(fl.FileName.ToLower());
                    rename = itime + FileExtension;
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }

                else
                {

                }

                if (FileOK)
                {
                    try
                    {

                        fl.SaveAs(path + "/" + rename);
                        FileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        FileSaved = false;
                    }
                }
                else
                {
                }
                if (FileSaved)
                {
                    string fileName = Path.GetFileName(rename);
                    dbfilepath = folder_name + "/" + fileName;
                }
            }
            return dbfilepath;
        }


        public bool IsExist(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        public DataTable FillTable(string sqlQuery)
        {
            DataTable dtTemp = new DataTable();
            SqlConnection con = new SqlConnection(connection.conn);
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlDataAdapter ad = new SqlDataAdapter(sqlQuery, con);
            ad.Fill(dtTemp);
            if (con.State == ConnectionState.Open) { con.Close(); }
            return dtTemp;
        }


        public bool login_status(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query);
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable GetDatastore(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = connection.conn;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
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

        public void sendemail(string email_id, string subject, string message)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Email_config");
                DataTable dt = InsertUpdate.GetData(cmd);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                }
                else
                {
                    try
                    {
                        var SendMailFrom = dt.Rows[0]["Email_Id"].ToString();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                        MailMessage email = new MailMessage();
                        // START
                        email.From = new MailAddress(SendMailFrom, dt.Rows[0]["Send_From_Name"].ToString());
                        email.To.Add(email_id);

                        email.Subject = subject;
                        email.Body = message;
                        email.IsBodyHtml = true;
                        //END
                        SmtpServer.Timeout = 20000;
                        SmtpServer.EnableSsl = true;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new NetworkCredential(SendMailFrom, dt.Rows[0]["App_Password"].ToString());
                        SmtpServer.Send(email);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch
            {
            }
        }

        public void sendemailwithattachment(string email, string subject, string msg, FileUpload f)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(email);// Email-ID of Receiver  
                message.Subject = subject;// Subject of Email  
                message.From = new System.Net.Mail.MailAddress("noreply@rcsp.in");// Email-ID of Sender  
                message.IsBodyHtml = true;
                message.Attachments.Add(new Attachment(f.FileContent, System.IO.Path.GetFileName(f.FileName)));
                SmtpClient SmtpMail = new SmtpClient();
                SmtpMail.Host = "relay-hosting.secureserver.net";//name or IP-Address of Host used for SMTP transactions  
                SmtpMail.Port = 25;//Port for sending the mail  
                SmtpMail.Credentials = new System.Net.NetworkCredential("integerpatna@gmail.com", "Ints@2017");//username/password of network, if apply  
                SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpMail.EnableSsl = false;
                SmtpMail.ServicePoint.MaxIdleTime = 0;
                SmtpMail.ServicePoint.SetTcpKeepAlive(true, 2000, 2000);
                message.BodyEncoding = Encoding.Default;
                message.Priority = MailPriority.High;
                SmtpMail.Send(message); //Smtpclient to send the mail message  
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private AlternateView Mail_Body(string message)
        {
            string path = HttpContext.Current.Server.MapPath(@"Images/best.jpg");
            LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "MyImage";
            string str = @"  
            <table>  
                <tr>  
                    <td> '" + message + @"'  
                    </td>  
                </tr>  
                <tr>  
                    <td>  
                      <img src=cid:MyImage  id='img' alt='' width='810px' height='450px'/>   
                    </td>  
                </tr>
<tr><b>Regards</b></tr>
<tr> Rashtriya Computer Shiksha Pariyojna </tr>
<tr> Support Team </tr>
<tr>Chanda Complex Mirjanhat Bhagalpur</tr>
<tr> <b>Speak to us :- </b>+91 9031173621, +91 912277801, +91 7050668808 </tr>
</table>  
            ";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(Img);
            return AV;
        }


        public string Find_Name(string Query)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand(Query);
                DataTable dt = InsertUpdate.GetData(cmd);
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch
            {
                return "0";
            }

        }

        public void BindChecklist(CheckBoxList chkList, string Text, string Value, string ColumnName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct " + Text + ", " + Value + " from " + ColumnName + " order by " + Text + "");
                DataTable dt = InsertUpdate.GetData(cmd);
                if (dt.Rows.Count != 0)
                {
                    chkList.DataValueField = "" + Value + "";
                    chkList.DataTextField = "" + Text + "";
                    chkList.DataSource = dt;
                    chkList.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        public string auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from Global ", connection.conn);
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

                    exeSql("Alter table Global add " + column + " varchar(50)");
                    result = auto_serial(column);
                }
                else
                {
                }
            }
            return result;
        }
        public static string SendNotification(string deviceId, Dictionary<String, String> data)
        {
            String sResponseFromServer = "";
            try
            {
                string notification_id = data["notification_id"];
                string message = data["message"];
                string title = data["title"];
                string messagetype = data["messagetype"];
                string UserId = data["UserId"];


                Dictionary<string, object> dc = get_push_credantial();

                string ttype = (String)dc["type"];
                string project_id = (String)dc["project_id"];
                string private_key_id = (String)dc["private_key_id"];
                string client_email = (String)dc["client_email"];
                string client_id = (String)dc["client_id"];
                string private_key = dc["private_key"].ToString().Replace("\\n", "\n");

                var jsonn = new
                {
                    type = ttype,
                    project_id = project_id,
                    private_key_id = private_key_id,
                    private_key = private_key,
                    client_email = client_email,
                    client_id = client_id,
                    token_uri = "https://oauth2.googleapis.com/token"
                };
                var ddata2 = new
                {
                    message = new
                    {
                        token = deviceId,
                        notification = new
                        {
                            body = message,
                            title = title,
                            image = "" // Optional, remove if not required
                        },
                        data = new
                        {
                            notification_id = notification_id,
                            message = message,
                            title = title,
                            url = "",
                            link_url = "",
                            messagetype = messagetype,
                            User_Id = UserId
                        }
                    }
                };
                var accessToken = GoogleCredential.FromJson(JsonConvert.SerializeObject(jsonn))
               .CreateScoped("https://www.googleapis.com/auth/firebase.messaging").UnderlyingCredential.GetAccessTokenForRequestAsync().GetAwaiter().GetResult();
                WebRequest tRequest = WebRequest.Create($"https://fcm.googleapis.com/v1/projects/{project_id}/messages:send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: Bearer {accessToken}");
                var json2 = new JavaScriptSerializer().Serialize(ddata2);
                byte[] byteArray = Encoding.UTF8.GetBytes(json2);
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                sResponseFromServer = tReader.ReadToEnd();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch
            {

            }
            String date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd-MM-yyyy");
            String Idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            SqlCommand cmd = new SqlCommand(" INSERT INTO PushNotification_Details (notification_id,message,title,messagetype,User_Id,Sender_Id,Idate,Date,Time,ResponseFromServer,Session_id) values (@notification_id,@message,@title,@messagetype,@User_Id,@Sender_Id,@Idate,@Date,@Time,@ResponseFromServer,@Session_id)");
            cmd.Parameters.AddWithValue("@notification_id", data["notification_id"]);
            cmd.Parameters.AddWithValue("@message", data["message"]);
            cmd.Parameters.AddWithValue("@title", data["title"]);
            cmd.Parameters.AddWithValue("@messagetype", data["messagetype"]);
            cmd.Parameters.AddWithValue("@User_Id", data["UserId"]);
            cmd.Parameters.AddWithValue("@Sender_Id", "");
            cmd.Parameters.AddWithValue("@Idate", Idate);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@ResponseFromServer", sResponseFromServer);
            cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
            if (InsertUpdate.InsertUpdateData(cmd))
            {
            }

            return sResponseFromServer;
        }

        public static Dictionary<string, object> get_push_credantial()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            try
            {

                string query = "Select * from Push_Credential  ";
                SqlCommand cmd;

                cmd = new SqlCommand(query);
                DataTable dt = GetData(cmd);

                if (dt.Rows.Count == 0)
                {
                    dc["type"] = "0";
                    dc["project_id"] = "0";
                    dc["private_key_id"] = "0";
                    dc["client_email"] = "0";
                    dc["client_id"] = "0";
                    dc["private_key"] = "0";
                }
                else
                {
                    dc["type"] = dt.Rows[0]["type"].ToString();
                    dc["project_id"] = dt.Rows[0]["project_id"].ToString();
                    dc["private_key_id"] = dt.Rows[0]["private_key_id"].ToString();
                    dc["client_email"] = dt.Rows[0]["client_email"].ToString();
                    dc["client_id"] = dt.Rows[0]["client_id"].ToString();
                    dc["private_key"] = dt.Rows[0]["private_key"].ToString();

                }
            }
            catch
            {
                dc["type"] = "0";
                dc["project_id"] = "0";
                dc["private_key_id"] = "0";
                dc["client_email"] = "0";
                dc["client_id"] = "0";
                dc["private_key"] = "0";
            }

            return dc;
        }
        public static Dictionary<string, object> get_pushsenderid()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Comapny_Profile  ";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);

            if (dt.Rows.Count == 0)
            {
                dc["SERVER_API_KEY"] = "0";
                dc["SENDER_ID"] = "0";


            }
            else
            {
                dc["SERVER_API_KEY"] = dt.Rows[0]["SERVER_API_KEY"].ToString();
                dc["SENDER_ID"] = dt.Rows[0]["SENDER_ID"].ToString();



            }
            return dc;

        }


        public void pushnotification(string Message, string Title, string FirebaseId, string ordrid, string userid, string messagetype)
        {
            Dictionary<string, object> dc1 = My.get_push_credantial();
            string type = (String)dc1["type"];
            string project_id = (String)dc1["project_id"];
            string private_key_id = (String)dc1["private_key_id"];
            string client_email = (String)dc1["client_email"];
            string client_id = (String)dc1["client_id"];
            string private_key = dc1["private_key"].ToString().Replace("\\n", "\n");

            string sendnotification = SendNotification1(FirebaseId, Message, Title, ordrid, userid, messagetype, type, project_id, private_key_id, client_email, client_id, private_key);
        }
        public static string SendNotification1(string deviceId, string msg, string tit, string bookingid, string userid, string messagetype, string ttype, string project_id, string private_key_id, string client_email, string client_id, string private_key
)
        {
            String sResponseFromServer = "";
            string msg1 = msg;
            string notificationid = Guid.NewGuid().ToString();
            try
            {



                string message = msg;
                string t = tit;
                var ddata = new
                {
                    message = new
                    {
                        token = deviceId,
                        notification = new
                        {
                            body = message,
                            title = tit,
                            image = "" // Optional, remove if not required
                        },
                        data = new
                        {
                            notification_id = Guid.NewGuid().ToString(),
                            message = message,
                            title = tit,
                            url = "",
                            link_url = "",
                            messagetype = messagetype,
                            User_Id = userid
                        }
                    }
                };


                var json = new JavaScriptSerializer().Serialize(ddata);



                var jsonn = new
                {
                    type = ttype,
                    project_id = project_id,
                    private_key_id = private_key_id,
                    private_key = private_key,
                    client_email = client_email,
                    client_id = client_id,
                    token_uri = "https://oauth2.googleapis.com/token"
                };

                var accessToken = GoogleCredential.FromJson(JsonConvert.SerializeObject(jsonn))
                   .CreateScoped("https://www.googleapis.com/auth/firebase.messaging").UnderlyingCredential.GetAccessTokenForRequestAsync().GetAwaiter().GetResult();

                WebRequest tRequest = WebRequest.Create($"https://fcm.googleapis.com/v1/projects/{project_id}/messages:send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: Bearer {accessToken}");

                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                sResponseFromServer = tReader.ReadToEnd();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();

            }
            catch
            {


            }




            save_push_mesge(sResponseFromServer, deviceId, msg1, userid, messagetype, tit, bookingid, notificationid);
            return sResponseFromServer;


        }

        private static void save_push_mesge(string sResponseFromServer, string deviceId, string message, string userid, string messagetype, string title, string bookingid, string notification_id)
        {
            try
            {
                string date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");
                SqlCommand cmd;
                string strQuery = "INSERT INTO  PushNotification_Details (notification_id,message,title,messagetype,User_Id,Sender_Id,Idate,Date,ResponseFromServer,Time) values (@notification_id,@message,@title,@messagetype,@User_Id,@Sender_Id,@Idate,@Date,@ResponseFromServer,@Time)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@notification_id", notification_id);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@messagetype", messagetype);
                cmd.Parameters.AddWithValue("@User_Id", userid);
                cmd.Parameters.AddWithValue("@Sender_Id", bookingid);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Idate", idate);
                cmd.Parameters.AddWithValue("@ResponseFromServer", sResponseFromServer);
                cmd.Parameters.AddWithValue("@Time", time);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }



        public static DataTable Getdata_sp(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (SqlConnection scon = new SqlConnection(connection.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                if (scon.State == ConnectionState.Closed)
                {
                    scon.Open();
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
                if (scon.State == ConnectionState.Open)
                {
                    scon.Close();
                    scon.Dispose();
                }
                return dt;
            }
        }

        public void bind_all_ddl_with_Allid(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
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
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
        public static string tampid()
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            Random rn = new Random(DateTime.Now.Millisecond);
            int i = 0;
            int j = 1000;
            int k = rn.Next(i, j);
            return k.ToString() + idate + time;

        }
        public string Auto_generate_topic(string query, int i, int j)
        {
            string user_id = "";
            bool duplicateid = false;
            Random rn = new Random();
            do
            {
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                int k = rn.Next(i, j);
                user_id = k.ToString() + idate + time;
                duplicateid = check_duplicate_regid(query + user_id);

                if (duplicateid == true)
                {

                }
            }
            while (duplicateid == false);

            return user_id;
        }
        public static string tempid()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random(DateTime.Now.Millisecond);

            int tempo = random.Next(100000, 999999);
            return tempo.ToString();
        }
        public static DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection con = new SqlConnection(connection.conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                con.Close();
                con.Dispose();
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        public static string create_admission_id()
        {

            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter("select * from Global", connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Globle_Master");
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Online_regid"] = 1;
                result = "1";
                dt.Rows.Add(dr);

            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Online_regid"].ToString() == "")
                    {
                        dr["Online_regid"] = 1;
                        result = "1";
                    }
                    else
                    {
                        dr["Online_regid"] = Convert.ToDouble(dr["Online_regid"]) + 1;
                        result = dr["Online_regid"].ToString();
                    }
                    break;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(ds.Tables[0]);
            return "HAJ" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd") + result;
        }

        public static Boolean InsertUpdateData_sp(SqlCommand cmd)
        {
            using (SqlConnection scon = new SqlConnection(connection.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                if (scon.State == ConnectionState.Closed)
                {
                    scon.Open();
                }
                int rowsAffected = cmd.ExecuteNonQuery();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Close();
                    scon.Dispose();
                }
                if (rowsAffected > 0)
                    return true;
                else
                    return false;
            }
        }


        public static string cononlinetest = connection.onlinetest;

        public static DataTable GetDataonlinedb(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(cononlinetest);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            con.Close();
            con.Dispose();
            return dt;
        }



        internal string password1()
        {

            string pwd = "";


            bool duplicateid = false;
            Random rn = new Random();
            int i = 10000;
            int j = 99999;
            do
            {
                int k = rn.Next(i, j);

                pwd = k.ToString();
                duplicateid = check_dauplicate_id(pwd);

                if (duplicateid == true)
                {

                }
            } while (duplicateid == false);

            return pwd;


        }

        private bool check_dauplicate_id(string pwd)
        {
            DataTable dt = FillTable("Select  Password  from admission_registor where Password='" + pwd + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        public int get_slid_max()
        {

            string coon = connection.conn;
            SqlDataAdapter ad = new SqlDataAdapter(" Select MAX(sl_no) from Zoom_API where  (sl_no is not null  or sl_no!='')", coon);
            DataSet ds = new DataSet();
            ad.Fill(ds, "my");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                }
            }
        }
        public void bind_ddl_all(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("All");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }

        public void bind_ddl_all1(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
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

        internal string get_class_name(string classid)
        {
            SqlCommand cmd = new SqlCommand("Select Course_Name from Add_course_table where course_id='" + classid + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "ALL";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        public string getmontnumber(string monthname)
        {
            if (monthname == "Jan")
            {
                return "01";
            }
            else if (monthname == "Feb")
            {
                return "02";
            }
            else if (monthname == "Mar")
            {
                return "03";
            }
            else if (monthname == "Apr")
            {
                return "04";
            }
            else if (monthname == "May")
            {
                return "05";
            }
            else if (monthname == "Jun")
            {
                return "06";
            }
            else if (monthname == "Jul")
            {
                return "07";
            }
            else if (monthname == "Aug")
            {
                return "08";
            }
            else if (monthname == "Sep")
            {
                return "09";
            }
            else if (monthname == "Oct")
            {
                return "10";
            }
            else if (monthname == "Nov")
            {
                return "11";
            }
            else if (monthname == "Dec")
            {
                return "12";
            }
            else
            {
                return "0";
            }
        }

        public Dictionary<string, object> getseesion()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select  top 1 *  from session_details where use_mode='1' order by id desc   ";


            SqlDataAdapter ad = new SqlDataAdapter(quiry, connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "session_details");
            DataTable dt = ds.Tables[0];


            if (dt.Rows.Count == 0)
            {


                dc["Session"] = "NO";
                dc["session_id"] = "NO";

            }
            else
            {

                dc["Session"] = dt.Rows[0]["Session"].ToString(); ;
                dc["session_id"] = dt.Rows[0]["session_id"].ToString();

            }

            return dc;
        }

        internal string get_student_send_password()
        {
            SqlCommand cmd = new SqlCommand("Select Send_student_userid_and_pwd_with_apk_link from Comapny_Profile");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }

        internal string get_teacher_send_password()
        {
            SqlCommand cmd = new SqlCommand("Select Send_teacher_userid_and_pwd_with_apk_link from Comapny_Profile");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }

        internal string get_sectionid(string Section_Name)
        {
            SqlCommand cmd = new SqlCommand("Select Section_Id from Section_Master   where Section_Name='" + Section_Name + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }

        internal string get_sectionname(string setionid)
        {
            SqlCommand cmd = new SqlCommand("Select Section_Name from Section_Master   where Section_Id='" + setionid + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "ALL";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }



        public bool syncdatastatusyes_or_no()
        {
            bool toreturn = false;
            SqlCommand cmd4 = new SqlCommand("Select Id from Notice_Board_Details where Send_Status='Notsend'");
            DataTable dt4 = InsertUpdate.GetData(cmd4);
            if (dt4.Rows.Count == 0)
            {
                SqlCommand cmd5 = new SqlCommand("Select Id from Private_Messages where Send_Status='Notsend' ");
                DataTable dt5 = InsertUpdate.GetData(cmd5);
                if (dt5.Rows.Count == 0)
                {
                    SqlCommand cmd6 = new SqlCommand("Select top 1 Id from Private_Messages_For_Teacher where Send_Status='Notsend' ");
                    DataTable dt6 = InsertUpdate.GetData(cmd6);
                    if (dt6.Rows.Count == 0)
                    {
                        SqlCommand cmd7 = new SqlCommand("Select top 1 Id from Notice_Board_Details_Teacher where Send_Status='Notsend' ");
                        DataTable dt7 = InsertUpdate.GetData(cmd7);
                        if (dt7.Rows.Count == 0)
                        {
                            SqlCommand cmd8 = new SqlCommand("Select top 1 Id from Attendance_Notification where Send_status='0' ");
                            DataTable dt8 = InsertUpdate.GetData(cmd8);
                           if (dt8.Rows.Count == 0)
                            { 
                                SqlCommand cmd9 = new SqlCommand(" select top 1 parameter_New from dbo.[Student_Payment_History] where (parameter_New is null or parameter_New='') ");
                                DataTable dt9 = InsertUpdate.GetData(cmd9);
                                if (dt9.Rows.Count == 0)
                                {
                                    My.exeSql("update HR_Attendance_Log set import_status='Pending' where import_status is null"); 
                                    //SqlCommand cmd10 = new SqlCommand("select  * from dbo.[PRL_Attendance_Log] where import_status='Pending' ");
                                    SqlCommand cmd10 = new SqlCommand("select  * from dbo.[PRL_Attendance_Log] where (import_status='Pending' or import_status='' or import_status is null )");
                                    DataTable dt10 = InsertUpdate.GetData(cmd10);
                                    if (dt10.Rows.Count == 0)
                                    {
                                        toreturn = true;
                                    }
                                    else
                                    {
                                        toreturn = false;
                                    }
                                }
                                else
                                {
                                    toreturn = false;
                                }
                            }
                            else
                            {
                                toreturn = false;
                            }
                        }
                        else
                        {
                            toreturn = false;
                        }
                    }
                    else
                    {
                        toreturn = false;
                    }
                }
                else
                {
                    toreturn = false;
                }
            }
            else
            {
                toreturn = false;
            }
            return toreturn;
        }

        public bool get_onlintrest_enabled()
        {
            SqlCommand cmd1 = new SqlCommand("Select  Enable_Onine_Test from App_Setting  ");
            DataTable dt = InsertUpdate.GetData(cmd1);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return false;
                }
                else
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Enable_Onine_Test"].ToString()) == true)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public void RptrData(DataTable sourcetable, Repeater rptr)
        {
            try
            {
                if (sourcetable.Rows.Count > 0)
                {
                    rptr.DataSource = sourcetable;
                    rptr.DataBind();
                }
                else
                {

                    rptr.DataSource = null;
                    rptr.DataBind();
                }

            }
            catch (Exception ex)
            {
            }


        }


        public DataSet Fill_Data_set(string query)
        {

            string connectionstring = connection.conn;
            DataSet dtc = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(connectionstring);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, connectionstring);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }


        public Dictionary<string, object> getstudent(string admission_no)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select top 1  class,rollnumber,Section,session,studentname,Class_id  from admission_registor where admissionserialnumber='" + admission_no + "' and  Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc   ";
            SqlDataAdapter ad = new SqlDataAdapter(quiry, connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "session_details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                dc["class"] = "NO";
                dc["rollnumber"] = "NO";
                dc["Section"] = "NO";
                dc["session"] = "NO";
                dc["studentname"] = "NO";
                dc["Class_id"] = "NO";
            }
            else
            {
                dc["class"] = dt.Rows[0]["class"].ToString();
                dc["rollnumber"] = dt.Rows[0]["rollnumber"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                dc["session"] = dt.Rows[0]["session"].ToString();
                dc["studentname"] = dt.Rows[0]["studentname"].ToString();
                dc["Class_id"] = dt.Rows[0]["Class_id"].ToString();
            }
            return dc;
        }

        internal string get_teachername(string teacherid)
        {
            SqlCommand cmd5 = new SqlCommand("Select  name from user_details where user_id='" + teacherid + "' ");
            DataTable dt5 = InsertUpdate.GetData(cmd5);
            if (dt5.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt5.Rows[0]["name"].ToString();
            }
        }

        internal void send_push_to_student_study_material(string Class_id, string Section, string subject, string Topic, string subjectid, string sessionid, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            string gcmid = "";
            SqlCommand cmd = new SqlCommand(" select ar.studentname,ar.admissionserialnumber,ar.gcm_id from Subject_Mapping_New  smn left join dbo.[admission_registor] ar on smn.Class_id=ar.Class_id  and smn.Section=ar.Section and smn.Session=ar.session and smn.Admission_no=ar.admissionserialnumber where ar.Class_id='" + Class_id + "' and ar.Section='" + Section + "' and Sub_id='" + subjectid + "' and  Status='1' and ar.Session_id='" + My.get_session_id() + "'  ");
            DataTable dt = InsertUpdate.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string message = "Dear " + dr["studentname"].ToString() + ", study material has been uploaded of the subject: " + subject + " & Topic: " + Topic + ", Please check now study material ";


                    if (dr["gcm_id"].ToString() == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "Study Material";
                        ss["messagetype"] = "StudyMaterial";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        ss["type"] = type;
                        ss["project_id"] = project_id;
                        ss["private_key_id"] = private_key_id;
                        ss["client_email"] = client_email;
                        ss["client_id"] = client_id;
                        ss["private_key"] = private_key;
                        My.onlypush(dr["gcm_id"].ToString(), ss);
                    }


                }

            }
        }

        internal void send_push_to_student_homework(string Class_id, string Section, string subject, string Topic, string subjectid, string sessionid, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            string gcmid = "";
            SqlCommand cmd = new SqlCommand(" select ar.studentname,ar.admissionserialnumber,ar.gcm_id from Subject_Mapping_New  smn left join dbo.[admission_registor] ar on smn.Class_id=ar.Class_id  and smn.Section=ar.Section and smn.Session=ar.session and smn.Admission_no=ar.admissionserialnumber where ar.Class_id='" + Class_id + "' and ar.Section='" + Section + "' and Sub_id='" + subjectid + "' and ar.Status='1' and  ar.Session_id='" + My.get_session_id() + "'   ");
            DataTable dt = InsertUpdate.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string message = "Dear " + dr["studentname"].ToString() + ", homework assigned of the subject: " + subject + " & Topic: " + Topic;

                    if (dr["gcm_id"].ToString() == "")
                    {
                        gcmid = "0";
                    }
                    if (gcmid != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "Home work";
                        ss["messagetype"] = "Homework";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        ss["type"] = type;
                        ss["project_id"] = project_id;
                        ss["private_key_id"] = private_key_id;
                        ss["client_email"] = client_email;
                        ss["client_id"] = client_id;
                        ss["private_key"] = private_key;
                        My.onlypush(dr["gcm_id"].ToString(), ss);
                    }

                }

            }
        }

        internal void send_push_to_student_ebook(string Class_id, string Section, string subject, string Topic, string subjectid, string sessioid, string type, string project_id, string private_key_id, string client_email, string client_id, string private_key)
        {
            SqlCommand cmd = new SqlCommand(" select ar.studentname,ar.admissionserialnumber,ar.gcm_id from Subject_Mapping_New  smn left join dbo.[admission_registor] ar on smn.Class_id=ar.Class_id  and smn.Section=ar.Section and smn.Session=ar.session and smn.Admission_no=ar.admissionserialnumber where ar.Class_id='" + Class_id + "' and ar.Section='" + Section + "' and Sub_id='" + subjectid + "' and  ar.Session_id='" + sessioid + "' and ar.and Status='1'  ");
            DataTable dt = InsertUpdate.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string message = "Dear " + dr["studentname"].ToString() + ", E-Book has been uploaded of the subject: " + subject + " & Topic: " + Topic + ", Please check now E-Book Section.";

                    if (dr["gcm_id"] != "")
                    {
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = message;
                        ss["title"] = "E-Book";
                        ss["messagetype"] = "E-Book";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = dr["admissionserialnumber"].ToString();
                        ss["type"] = type;
                        ss["project_id"] = project_id;
                        ss["private_key_id"] = private_key_id;
                        ss["client_email"] = client_email;
                        ss["client_id"] = client_id;
                        ss["private_key"] = private_key;

                        My.onlypush(dr["gcm_id"].ToString(), ss);
                    }

                }

            }
        }

        internal string get_class_routine(string session, string classid, string section)
        {
            string querymain = "Select  Day  ";

            //  string query = " Select  *,CONCAT('Period_',Class_period,'__',format(Start_Time, 'hh_mm_ss_tt'), 'TO', format(End_time, 'hh_mm_ss_tt')) as Period   from  Class_Routine_Master   where   Session_id=" + session + " and  Class_id=" + classid + " and  Section='" + section + "' ORDER BY  Class_period";
            string query = " Select  distinct Class_period,CONCAT('Period_',Class_period,'__',format(Start_Time, 'hh_mm_ss_tt'), 'TO', format(End_time, 'hh_mm_ss_tt')) as Period   from  Class_Routine_Master   where   Session_id=" + session + " and  Class_id=" + classid + " and  Section='" + section + "' ORDER BY  Class_period";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string subjectid = getsubjectid(dr["Class_period"].ToString(), session, classid, section);
                    // querymain += ",(Select csm.CourseName from Class_Routine_Master crm join Course_or_Subject_Master csm on crm.Subject_id=csm.CourseID and  crm.Class_id=csm.CategoryID and crm.Section=csm.section and  crm.Session_id=csm.session_id  where   crm.Subject_id='" + dr["Subject_id"] + "'  and crm.Class_id='" + dr["Class_id"] + "' and crm.Section='" + dr["Section"] + "' and crm.Session_id='" + dr["Session_id"] + "' and crm.Class_period='" + dr["Class_period"] + "' and crm.Day=Day_Master.Day   ) " + dr["Period"].ToString();

                    querymain += ",(Select csm.CourseName from Class_Routine_Master crm join Course_or_Subject_Master csm on crm.Subject_id=csm.CourseID and  crm.Class_id=csm.CategoryID and crm.Section=csm.section and  crm.Session_id=csm.session_id  where   crm.Subject_id=" + subjectid + " and  crm.Class_id='" + classid + "' and crm.Section='" + section + "' and crm.Session_id='" + session + "' and crm.Class_period='" + dr["Class_period"] + "' and crm.Day=Day_Master.Day   ) " + dr["Period"].ToString();
                }
            }
            querymain += " from    Day_Master  ";
            return querymain;
        }

        private string getsubjectid(string Class_period, string session, string classid, string section)
        {
            SqlCommand cmd = new SqlCommand("Select  Subject_id from Class_Routine_Master where Session_id='" + session + "' and Class_id='" + classid + "' and Section='" + section + "' and Class_period='" + Class_period + "'");
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

        internal string get_session_id(string session)
        {
            SqlCommand cmd = new SqlCommand("Select  session_id from session_details where Session='" + session + "'");
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
        internal string get_session_id_use()
        {
            SqlCommand cmd = new SqlCommand("Select  session_id from session_details where use_mode='1' ");
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
            SqlCommand cmd = new SqlCommand("Select  Session from session_details where use_mode='1'");
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

        internal string get_teacher_type(object teacherid)
        {
            SqlCommand cmd = new SqlCommand("Select  CategoryID from Ptm_class_teacher_mapping where UserID='" + teacherid + "'");
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

        public string getclassid(string teacherid)
        {

            string CategoryID = "0";
            SqlCommand cmd1 = new SqlCommand("Select  CategoryID from Ptm_class_teacher_mapping where UserID='" + teacherid + "'");
            DataTable dt = GetData(cmd1);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CategoryID1 = dt.Rows[i]["CategoryID"].ToString();
                    if (CategoryID1 == "")
                    {
                        CategoryID = "'" + CategoryID1 + "'";
                    }
                    else
                    {
                        CategoryID = CategoryID + "," + "'" + CategoryID1 + "'";

                    }
                }
            }

            return CategoryID;



        }





        internal string get_sessionid()
        {
            SqlCommand cmd = new SqlCommand("Select  session_id from session_details where use_mode='1'");
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

        internal string getval()
        {
            string monthname = DateTime.Now.ToString("MMMM");

            if (monthname == "Jan")
            {
                return "01";
            }
            else if (monthname == "February")
            {
                return "02";
            }
            else if (monthname == "March")
            {
                return "03";
            }
            else if (monthname == "April")
            {
                return "04";
            }
            else if (monthname == "May")
            {
                return "05";
            }
            else if (monthname == "June")
            {
                return "06";
            }
            else if (monthname == "July")
            {
                return "07";
            }
            else if (monthname == "August")
            {
                return "08";
            }
            else if (monthname == "September")
            {
                return "09";
            }
            else if (monthname == "October")
            {
                return "10";
            }
            else if (monthname == "November")
            {
                return "11";
            }
            else if (monthname == "December")
            {
                return "12";
            }
            else
            {
                return "0";
            }
        }

        internal static bool check_home_work_status(string homeworkid)
        {
            SqlCommand cmd = new SqlCommand("Select  Homework_id from ReplayHomework where Homework_id='" + homeworkid + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal string get_classsid_top1()
        {
            SqlCommand cmd = new SqlCommand("Select  top 1 course_id from Add_course_table order by id asc  ");
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

        public void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
            }
        }


        public string registration_code()
        {


            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter("select * from Global", connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Globle_Master");
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["reg_no"] = 1;
                result = "1";
                dt.Rows.Add(dr);

            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["reg_no"].ToString() == "")
                    {
                        dr["reg_no"] = 1;
                        result = "1";
                    }
                    else
                    {
                        dr["reg_no"] = Convert.ToDouble(dr["reg_no"]) + 1;
                        result = dr["reg_no"].ToString();
                    }
                    break;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(ds.Tables[0]);
            return "SMS" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd") + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss") + result;

        }





        internal string getbranchid(string userid)
        {
            SqlCommand cmd = new SqlCommand("Select Branch_id from user_details where user_id='" + userid + "'   ");
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

        public void bind_all_ddl_with_id_no_select(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
        }
        internal static void bind_ddl_noselect(DropDownList ddl, string strQuery)
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
            }
            ddl.DataSource = dt;
            ddl.DataBind();
        }
        public DataTable FillData(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(connection.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, connection.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }
            }
            catch (Exception ex)
            {
            }
            return dtc;
        }
        internal static string reg_format(string column_name)
        {
            string bill = UsesCode.auto_serial1(column_name);
            string pre_fix = get_reg_prifix();
            return pre_fix + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd") + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss") + bill;
        }

        public static string get_reg_prifix()
        {
            DataTable dt = FillDatastatic("Select Online_reg_prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Online_reg_prefix"].ToString();
            }
        }
        private static DataTable FillDatastatic(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(connection.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, connection.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }
            }
            catch (Exception ex)
            {
            }
            return dtc;
        }
        private static string auto_serial1(string column)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", connection.conn);
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
            }
            return result;
        }

        public void bind_ddl_no_select(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }
        internal static string reg_format_Career(string column_name)
        {
            string bill = UsesCode.auto_serial1(column_name);
            string pre_fix = get_reg_prifix();
            return pre_fix + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd") + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss") + bill;
        }

        internal object get_room_id_live(string templateid)
        {
            DataTable dt = FillDatastatic("Select RoomId from LiveClassCredential where TemplateID='" + templateid + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["RoomId"].ToString();
            }
        }

    }
}