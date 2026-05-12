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
    public partial class Upload_Item_stock_by_excel : System.Web.UI.Page
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
                        bind_sector();
                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());

                    }
                }

            }


        }

        private void bind_sector()
        {
            mycode.bind_all_ddl_with_id(ddl_sector, "Select Sector_Name,Sector_id from HMS_Inventory_Sector_Master");
        }
        protected void ddl_sector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_store();
            }
            catch (Exception ex)
            {

            }
        }

        private void bind_store()
        {
            mycode.bind_all_ddl_with_id(ddl_store, "Select Store_name,Store_Id from HMS_Invetory_Create_Store where Sector_id='" + ddl_sector.SelectedValue + "'");

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

            tblReadCSV.Columns.Add("Item_Name");
            tblReadCSV.Columns.Add("Unit_name");
            tblReadCSV.Columns.Add("HSN");
            tblReadCSV.Columns.Add("GST");
            tblReadCSV.Columns.Add("Qty");
            tblReadCSV.Columns.Add("Purchase_rate");
            tblReadCSV.Columns.Add("Sale_rate");
            tblReadCSV.Columns.Add("Batch");
            tblReadCSV.Columns.Add("Expiry");
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
                tblReadCSV.Columns.Add("Item_Name");
                tblReadCSV.Columns.Add("Unit_name");
                tblReadCSV.Columns.Add("HSN");
                tblReadCSV.Columns.Add("GST");
                tblReadCSV.Columns.Add("Qty");
                tblReadCSV.Columns.Add("Purchase_rate");
                tblReadCSV.Columns.Add("Sale_rate");
                tblReadCSV.Columns.Add("Batch");
                tblReadCSV.Columns.Add("Expiry");

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
        double total_qty = 0;
        double Total_Rate = 0;
        double Total_gst = 0;
        double Final_total = 0;
        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {

                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    int rocount = grd_services.Rows.Count;
                    if (rocount != 0)
                    {
                        for (int i = 0; i < rocount; i++)
                        {

                            string Item_Name = grd_services.Rows[i].Cells[0].Text;
                            string Unit_name = grd_services.Rows[i].Cells[1].Text;
                            string HSN = grd_services.Rows[i].Cells[2].Text;
                            string GST = grd_services.Rows[i].Cells[3].Text;
                            string Qty = grd_services.Rows[i].Cells[4].Text;
                            string Purchase_rate = grd_services.Rows[i].Cells[5].Text;
                            string Sale_rate = grd_services.Rows[i].Cells[6].Text;
                            string Batch = grd_services.Rows[i].Cells[7].Text;
                            string Expiry = grd_services.Rows[i].Cells[8].Text;

                            if (ddl_sector.SelectedValue == "2001")
                                save_items_for_central_stock(Item_Name, Unit_name, HSN, GST, Qty, Purchase_rate, Sale_rate, Batch, Expiry);
                            else
                                save_items(Item_Name, Unit_name, HSN, GST, Qty, Purchase_rate, Sale_rate, Batch, Expiry);
                        }
                        if (ddl_sector.SelectedValue == "2001")
                            save_final_total_bill_for_central_store();
                        else
                            save_final_total_billwise();

                        grd_services.DataSource = null;
                        grd_services.DataBind();
                    }
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }
        }
        private void save_final_total_bill_for_central_store()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_inventory_purchase_entry_billwise");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");

            cmd.Parameters.AddWithValue("@Store_id ", ddl_sector.SelectedValue);
            cmd.Parameters.AddWithValue("@invoice_no ", "OLDSTOCK");
            cmd.Parameters.AddWithValue("@party_id ", "OLDSTOCK");
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            cmd.Parameters.AddWithValue("@Total_items ", grd_services.Rows.Count);
            cmd.Parameters.AddWithValue("@Total_qty ", total_qty);
            cmd.Parameters.AddWithValue("@Total_Purchase_rate ", Total_Rate);
            cmd.Parameters.AddWithValue("@Total_Gst_value ", Total_gst);
            cmd.Parameters.AddWithValue("@Total_IGST ", 0);
            cmd.Parameters.AddWithValue("@Total_CGST ", Total_gst / 2);
            cmd.Parameters.AddWithValue("@Total_SGST ", Total_gst / 2);
            cmd.Parameters.AddWithValue("@Grand_total ", Final_total);
            cmd.Parameters.AddWithValue("@date ", mycode.date());
            cmd.Parameters.AddWithValue("@time ", mycode.time());
            cmd.Parameters.AddWithValue("@idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@user_id ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@unique_entry_id ", ViewState["unique_entry_id"].ToString());
            cmd.Parameters.AddWithValue("@Purchase_date ", mycode.date());
            cmd.Parameters.AddWithValue("@txt_freight ", "0");
            cmd.Parameters.AddWithValue("@Remarks ", "OLD Stock");

            cmd.Parameters.AddWithValue("@roundoff ", "0");
            cmd.Parameters.AddWithValue("@Total_netamount ", "0");
            cmd.Parameters.AddWithValue("@Total_taxable ", "0");

            //cmd.Parameters.AddWithValue("@Invoice_no ", "INSERT");
            //cmd.Parameters.AddWithValue("@Date ", mycode.date());
            //cmd.Parameters.AddWithValue("@Transfer_from ", "2001");
            //cmd.Parameters.AddWithValue("@Transfer_to ", ddl_store.SelectedValue);
            //cmd.Parameters.AddWithValue("@Total_amount ", Total_Rate.ToString());
            //cmd.Parameters.AddWithValue("@Gst_amount", Total_gst.ToString());
            //cmd.Parameters.AddWithValue("@Final_amount ", Final_total.ToString());
            //cmd.Parameters.AddWithValue("@Created_by ", Session["Admin"].ToString());
            //cmd.Parameters.AddWithValue("@Created_date ", mycode.date());
            //cmd.Parameters.AddWithValue("@Created_idate ", mycode.idate());
            //cmd.Parameters.AddWithValue("@Receivername ", "");
            //cmd.Parameters.AddWithValue("@Remarks ", "");
            //cmd.Parameters.AddWithValue("@session ", My.get_session());
            //cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }

        private void save_final_total_billwise()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_Temp_inventory_stock_transfer_details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERTTotal");
            cmd.Parameters.AddWithValue("@Invoice_no ", "OLDSTOCK");
            cmd.Parameters.AddWithValue("@Date ", mycode.date());
            cmd.Parameters.AddWithValue("@Transfer_from ", "2001");
            cmd.Parameters.AddWithValue("@Transfer_to ", ddl_store.SelectedValue);
            cmd.Parameters.AddWithValue("@Total_amount ", Total_Rate.ToString());
            cmd.Parameters.AddWithValue("@Gst_amount", Total_gst.ToString());
            cmd.Parameters.AddWithValue("@Final_amount ", Final_total.ToString());
            cmd.Parameters.AddWithValue("@Created_by ", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Created_date ", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate ", mycode.idate());
            cmd.Parameters.AddWithValue("@Receivername ", "");
            cmd.Parameters.AddWithValue("@Remarks ", "");
            cmd.Parameters.AddWithValue("@session ", My.get_session());
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
            }
        }

        private void save_items(string item_Name, string unit_name, string hSN, string gST, string qty, string purchase_rate, string sale_rate, string batch, string expiry)
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
                Alertme("Please enter gst", "warning");
            }

            else if (qty == "&nbsp;")
            {
                Alertme("Please enter qty", "warning");
            }
            else if (My.convert_amount(qty))
            {
                Alertme("Please enter valid qty", "warning");
            }
            else if (My.convert_amount(purchase_rate))
            {
                Alertme("Please enter valid qty", "warning");
            }
            else if (My.convert_amount(sale_rate))
            {
                Alertme("Please enter valid qty", "warning");
            }
            else
            {

                string ItemId = fetch_item_id(item_Name);
                if (ItemId == "0")
                {
                    Alertme("Please add item first.", "warning");
                    return;
                }

                string Unit_id = fetch_Unit_id(unit_name);
                if (Unit_id == "0")
                {
                    Alertme("Please add unit first.", "warning");
                    return;
                }
                string Stock_id = "OLDSTOCK";
                if (ViewState["unique_entry_id"] == null)
                {
                    ViewState["unique_entry_id"] = My.unique_id();
                }
                SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
                cmd.Parameters.AddWithValue("@sp_status", "INSERT");
                cmd.Parameters.AddWithValue("@Store_id", ddl_store.SelectedValue);
                cmd.Parameters.AddWithValue("@Stock_id", Stock_id);
                cmd.Parameters.AddWithValue("@Item_Code", ItemId);
                cmd.Parameters.AddWithValue("@Brand_Id", ViewState["Brand_id"].ToString());
                cmd.Parameters.AddWithValue("@Unit_id", Unit_id);
                cmd.Parameters.AddWithValue("@Quantity", qty);
                cmd.Parameters.AddWithValue("@Purchase_Rate", purchase_rate);
                cmd.Parameters.AddWithValue("@GST_Percent", gST);
                cmd.Parameters.AddWithValue("@firm", My.firm_id());
                cmd.Parameters.AddWithValue("@session", My.get_session());
                cmd.Parameters.AddWithValue("@hsn_no", hSN);
                cmd.Parameters.AddWithValue("@date", My.datetime_new());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Userid", Session["Admin"].ToString());
                cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                cmd.Parameters.AddWithValue("@Transfer_from_store ", "2001");
                cmd.Parameters.AddWithValue("@Batch_no", batch);
                cmd.Parameters.AddWithValue("@Expiry_date", expiry);
                cmd.Parameters.AddWithValue("@Sale_rate", sale_rate);

                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                    Total_Rate = Total_Rate + Convert.ToDouble(purchase_rate);
                    Total_gst = Total_gst + ((Convert.ToDouble(purchase_rate) * Convert.ToDouble(qty)) * Convert.ToDouble(gST) / 100);
                    Final_total = Final_total + (Convert.ToDouble(purchase_rate) * Convert.ToDouble(qty));


                    ViewState["Description"] = " Add Invertory item  Name :-" + item_Name;
                    My.send_data_to_user_log_history(Session["name"].ToString() + ViewState["Description"].ToString(), Session["Admin"].ToString());

                    Alertme("Item uploaded successfully", "success");

                }


            }
        }
        private void save_items_for_central_stock(string item_Name, string unit_name, string hSN, string gST, string qty, string purchase_rate, string sale_rate, string batch, string expiry)
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
                Alertme("Please enter gst", "warning");
            }

            else if (qty == "&nbsp;")
            {
                Alertme("Please enter qty", "warning");
            }
            else if (My.convert_amount(qty))
            {
                Alertme("Please enter valid qty", "warning");
            }
            else if (My.convert_amount(purchase_rate))
            {
                Alertme("Please enter valid qty", "warning");
            }
            else if (My.convert_amount(sale_rate))
            {
                Alertme("Please enter valid qty", "warning");
            }
            else
            {

                string ItemId = fetch_item_id(item_Name);
                if (ItemId == "0")
                {
                    Alertme("Please add item first.", "warning");
                    return;
                }

                string Unit_id = fetch_Unit_id(unit_name);
                if (Unit_id == "0")
                {
                    Alertme("Please add unit first.", "warning");
                    return;
                }
                string Stock_id = "OLDSTOCK";
                if (ViewState["unique_entry_id"] == null)
                {
                    ViewState["unique_entry_id"] = My.unique_id();
                }
                SqlCommand cmd = new SqlCommand("sp_HMS_Inventory_stock_details");
                cmd.Parameters.AddWithValue("@sp_status", "INSERT");
                cmd.Parameters.AddWithValue("@Store_id", ddl_sector.SelectedValue);
                cmd.Parameters.AddWithValue("@Stock_id", Stock_id);
                cmd.Parameters.AddWithValue("@Item_Code", ItemId);
                cmd.Parameters.AddWithValue("@Brand_Id", ViewState["Brand_id"].ToString());
                cmd.Parameters.AddWithValue("@Unit_id", Unit_id);
                cmd.Parameters.AddWithValue("@Quantity", qty);
                cmd.Parameters.AddWithValue("@Purchase_Rate", purchase_rate);
                cmd.Parameters.AddWithValue("@GST_Percent", gST);
                cmd.Parameters.AddWithValue("@firm", My.firm_id());
                cmd.Parameters.AddWithValue("@session", My.get_session());
                cmd.Parameters.AddWithValue("@hsn_no", hSN);
                cmd.Parameters.AddWithValue("@date", My.datetime_new());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Userid", Session["Admin"].ToString());
                cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                cmd.Parameters.AddWithValue("@Transfer_from_store", "");
                cmd.Parameters.AddWithValue("@Batch_no", batch);
                cmd.Parameters.AddWithValue("@Expiry_date", expiry);
                cmd.Parameters.AddWithValue("@Sale_rate", sale_rate);

                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                    total_qty = total_qty + Convert.ToDouble(qty);
                    Total_Rate = Total_Rate + Convert.ToDouble(purchase_rate);
                    Total_gst = Total_gst + ((Convert.ToDouble(purchase_rate) * Convert.ToDouble(qty)) * Convert.ToDouble(gST) / 100);
                    Final_total = Final_total + (Convert.ToDouble(purchase_rate) * Convert.ToDouble(qty));


                    ViewState["Description"] = " Add Invertory item  Name :-" + item_Name;
                    My.send_data_to_user_log_history(Session["name"].ToString() + ViewState["Description"].ToString(), Session["Admin"].ToString());

                    Alertme("Item uploaded successfully", "success");

                }


            }
        }

        private string fetch_item_id(string item_Name)
        {
            DataTable dt = My.dataTable("select * from HMS_Invetory_item_Master where  Item_Name='" + item_Name + "' and firm='" + My.firm_id() + "'");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "0";
            }
            else
            {
                ViewState["Brand_id"] = dt.Rows[0]["Brand_id"].ToString();
                return dt.Rows[0]["Item_id"].ToString();
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
