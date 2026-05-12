using school_web.AppCode;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Upload_Item_by_excel : System.Web.UI.Page
    {
        string scrpt;
        private void Alertme(string msg, string panel)
        {

            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;

            }

        }

        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null )
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());

                    }
                }

            }


        }
        protected void btn_Uplaod_Click(object sender, EventArgs e)
        {
            try
            {
                string file = upload_dummy_data("INVITEM", FileUpload1);
                string csvid = My.auto_serialS("Upload_csvid");
                if (file == "")
                {
                    Alertme("Please attached proper file", "warnig");

                }
                else if (file == "Choose Only CSV File")
                {
                    Alertme("Choose only csv File", "warning");

                }
                else
                {
                    SqlDataAdapter ad = new SqlDataAdapter("Select * from HMS_Upload_csv_file", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "HMS_Upload_csv_file");
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["File_path"] = file;
                    dr["Date"] = mycode.date();
                    dr["Idate"] = mycode.idate();
                    dr["csvid"] = csvid;
                    dr["Status"] = "SUBMITTED";
                    dt.Rows.Add(dr);
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    uploaded_service_data(file);
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }
        }
        private void uploaded_service_data(string file)
        {
            string path = (Server.MapPath("../")).ToString();
            string FileName = path + "/" + file;
            DataTable tblReadCSV = new DataTable();
            tblReadCSV.Columns.Add("Itemtype");
            tblReadCSV.Columns.Add("Item_Name");
            tblReadCSV.Columns.Add("Unit_name");
            tblReadCSV.Columns.Add("Brand");
            tblReadCSV.Columns.Add("HSN");
            tblReadCSV.Columns.Add("GST");
            TextFieldParser csvParser = new TextFieldParser(FileName);
            csvParser.Delimiters = new string[] { "," };
            csvParser.TrimWhiteSpace = true;
            csvParser.ReadLine();
            while (!(csvParser.EndOfData == true))
            {
                tblReadCSV.Rows.Add(csvParser.ReadFields());
            }

            grd_services.DataSource = tblReadCSV;
            grd_services.DataBind();
            btn_final_submit.Visible = true;

        }
        private string upload_dummy_data(string sp_status, FileUpload fileUp)
        {
            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (fileUp.HasFile)
            {

                Session["WorkingImage"] = fileUp.FileName;
                String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

                String[] allowedExtensions = { ".csv" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    k++;
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
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
                    string path = (Server.MapPath("../Master_Img/DocumentAttachment")).ToString();
                    fileUp.SaveAs(path + "/" + Session["WorkingImage"]);
                    string filename = Session["WorkingImage"].ToString();

                    if (check_wrap_or_not((path + "/" + Session["WorkingImage"])))
                    {
                        FileSaved = true;
                    }


                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }
            }
            if (FileSaved)
            {

                string fileName = Path.GetFileName(Session["WorkingImage"].ToString());
                dbfilePath = @"/Master_Img/DocumentAttachment/" + fileName;

            }
            return dbfilePath;
        }
        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();

                tblReadCSV.Columns.Add("Itemtype");
                tblReadCSV.Columns.Add("Item_Name");
                tblReadCSV.Columns.Add("Unit_name");
                tblReadCSV.Columns.Add("Brand");
                tblReadCSV.Columns.Add("HSN");
                tblReadCSV.Columns.Add("GST");

                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
            {
                int rocount = grd_services.Rows.Count;
                if (rocount != 0)
                {
                    for (int i = 0; i < rocount; i++)
                    {
                        string itemtype = grd_services.Rows[i].Cells[0].Text;
                        string Item_Name = grd_services.Rows[i].Cells[1].Text;
                        string Unit_name = grd_services.Rows[i].Cells[2].Text;
                        string Brand = grd_services.Rows[i].Cells[3].Text;
                        string HSN = grd_services.Rows[i].Cells[4].Text;
                        string GST = grd_services.Rows[i].Cells[5].Text;


                        save_items(itemtype, Item_Name, Unit_name, Brand, HSN, GST);
                    }


                    grd_services.DataSource = null;
                    grd_services.DataBind();
                }
                ts.Complete();

            }
        }

        private void save_items(string itemtype, string item_Name, string unit_name, string brand, string hSN, string gST)
        {

            DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date1 = date.ToString("dd/MM/yyyy");
            int idate = Convert.ToInt32(date.ToString("yyyyMMdd"));
            if (item_Name == "&nbsp;")
            {
                Alertme("Please enter item name", "warning");
            }
            else if (unit_name == "&nbsp;")
            {
                Alertme("Please enter unit name", "warning");
            }
            else if (gST == "&nbsp;")
            {
                Alertme("Please enter sub item name", "warning");
            }
            else if (itemtype == "&nbsp;")
            {
                Alertme("Please enter sub item type", "warning");
            }
            else
            {

                string ItemId = My.global_id_creation("Item_Id");
                string Unit_id = fetch_Unit_id(unit_name);
                if (Unit_id == "0")
                {
                    Alertme("Please add unit first.", "warning");
                    return;
                }
                SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_item_Master");
                cmd.Parameters.AddWithValue("@sp_status", "INSERTSERVICES1");
                cmd.Parameters.AddWithValue("@Item_Id", ItemId);
                cmd.Parameters.AddWithValue("@Item_Name", item_Name);
                cmd.Parameters.AddWithValue("@Brand_name", brand);

                cmd.Parameters.AddWithValue("@HSN", hSN);
                cmd.Parameters.AddWithValue("@GST", gST);
                cmd.Parameters.AddWithValue("@Unit_id", Unit_id);
                cmd.Parameters.AddWithValue("@Item_type", itemtype);
                cmd.Parameters.AddWithValue("@Firm", My.firm_id());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_Date", My.datetime_new());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());


                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                    ViewState["Description"] = " Add Invertory item  Name :-" + item_Name;
                    My.send_data_to_user_log_history(Session["name"].ToString() + ViewState["Description"].ToString(), Session["Admin"].ToString());

                    Alertme("Item uploaded successfully", "success");

                }


            }
        }

        private string fetch_Unit_id(string unit_name)
        {
            DataTable dt = My.dataTable("select * from unit_master where  Unit='" + unit_name + "' and firm='" + My.firm_id() + "'");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["unit_id"].ToString();
            }
        }
    }

}
