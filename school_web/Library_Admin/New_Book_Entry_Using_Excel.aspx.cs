using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace school_web.Library_Admin
{
    public partial class New_Book_Entry_Using_Excel : System.Web.UI.Page
    {
        My mycode = new My();
        Library ly = new Library();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["Admin"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());



                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "New_Book_Entry_Using_Excel");
            }
        }


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





        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                btn_upload.Visible = true;
                ViewState["dupAdmiD"] = "0";
                upload_excel_file();
            }
            else
            {
                Alertme("Please choose excel.csv file.", "warning");

                return;
            }
        }

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            string csvid = My.auto_serialS("Upload_csvid");
            SqlDataAdapter ad = new SqlDataAdapter("Select * from excel_file", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Csv_file");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = file;
            dr[2] = mycode.date();
            dr[3] = mycode.idate();
            dr[4] = csvid;
            dr[5] = "SUBMITTED_BOOK";
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private string upload_csv_data()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("ddMMyyyy");
            string time = dtm.ToString("hhmmss");
            String filerename = date + time;

            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;

            Session["file1"] = FileUpload1.FileName;
            String FileExtension = Path.GetExtension(Session["file1"].ToString()).ToLower();
            Session["file"] = ("Upload_excel_Class_Subject_Mapping" + filerename + FileExtension);
            String[] allowedExtensions = { ".csv" };
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

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["file"]);

                    if (check_wrap_or_not((path + "/" + Session["file"])))
                    {
                        FileSaved = true;
                    }
                    else
                    {
                        File.Delete((path + "/" + Session["file"]));
                        FileSaved = false;
                    }
                }
                catch (Exception ex)
                {
                    FileSaved = false;

                    Alertme(ex.ToString(), "warning");
                }
            }
            else
            {
                dbfilePath = "Choose only csv File";
                return dbfilePath;
            }
            if (FileSaved)
            {

                string fileName = Path.GetFileName(Session["file"].ToString());
                dbfilePath = @"/Master_Img/Student/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }

        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();

                tblReadCSV.Columns.Add("Book_Type");
                tblReadCSV.Columns.Add("Class");
                tblReadCSV.Columns.Add("Book_Status");
                tblReadCSV.Columns.Add("Book_Category");
                tblReadCSV.Columns.Add("Name_of_Book");
                tblReadCSV.Columns.Add("Author_Name");
                tblReadCSV.Columns.Add("Publication");
                tblReadCSV.Columns.Add("Location");
                tblReadCSV.Columns.Add("Volume_Name");
                tblReadCSV.Columns.Add("Edition");
                tblReadCSV.Columns.Add("Publication_Year");
                tblReadCSV.Columns.Add("No_of_Pages");
                tblReadCSV.Columns.Add("Quantity");
                tblReadCSV.Columns.Add("ISBN_No");
                tblReadCSV.Columns.Add("Invoice_No");
                tblReadCSV.Columns.Add("Price");
                tblReadCSV.Columns.Add("Purchase_Date");
                tblReadCSV.Columns.Add("Sub_Location");
                tblReadCSV.Columns.Add("Book_Id");
                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                btn_Submit_final.Visible = true;
                finalsubmitpnl.Visible = true;
                pnl_grid.Visible = true;
                lbl_total.Text = "Total Data :- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();


               
                GridView2.DataSource = null;
                GridView2.DataBind();
                //==============

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        protected void btn_Submit_final_Click1(object sender, EventArgs e)
        {
            try
            {

                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    string qry = "";
                    string Book_Type;
                    string Class;
                    string Book_Status;
                    string Book_Category;
                    string Name_of_Book;
                    string Author_Name;
                    string Publication;
                    string Location;
                    string Volume_Name;
                    string Edition;
                    string Publication_Year;
                    string No_of_Pages;
                    string Quantity;
                    string ISBN_No;
                    string Invoice_No;
                    string Price;
                    string Purchase_Date;
                    string Sub_location = "";
                    string Book_Id = "";
                    

                    string Transaction_Id = My.create_random_no_otp();
                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        Book_Type = grvExcelData.Rows[i].Cells[0].Text;
                        Class = grvExcelData.Rows[i].Cells[1].Text;
                        Book_Status = grvExcelData.Rows[i].Cells[2].Text;
                        Book_Category = grvExcelData.Rows[i].Cells[3].Text;
                        Name_of_Book = grvExcelData.Rows[i].Cells[4].Text;
                        Author_Name = grvExcelData.Rows[i].Cells[5].Text;
                        Publication = grvExcelData.Rows[i].Cells[6].Text;
                        Location = grvExcelData.Rows[i].Cells[7].Text;
                        Volume_Name = grvExcelData.Rows[i].Cells[8].Text;
                        Edition = grvExcelData.Rows[i].Cells[9].Text;
                        Publication_Year = grvExcelData.Rows[i].Cells[10].Text;
                        No_of_Pages = grvExcelData.Rows[i].Cells[11].Text;
                        Quantity = grvExcelData.Rows[i].Cells[12].Text;
                        ISBN_No = grvExcelData.Rows[i].Cells[13].Text;
                        Invoice_No = grvExcelData.Rows[i].Cells[14].Text;
                        Price = grvExcelData.Rows[i].Cells[15].Text;
                        Purchase_Date = grvExcelData.Rows[i].Cells[16].Text;
                        Sub_location = grvExcelData.Rows[i].Cells[17].Text;
                        if(Sub_location== "&nbsp;")
                        {
                            Sub_location = "0";
                        }
                        if(chk_book_id_manually.Checked==true)
                        {
                            Book_Id = grvExcelData.Rows[i].Cells[18].Text;
                        }
                        else
                        {
                            Book_Id = "0";
                        }


                        Dictionary<string, object> dc1 = Library.get_liery_master_id_Library(Book_Type, Class, Book_Status, Book_Category, Location, ViewState["Branch_id"].ToString(), Sub_location);
                        Book_Type = (String)dc1["book_Type"];
                        Class = (String)dc1["classname"];
                        Book_Category = (String)dc1["book_Category"];
                        Location = (String)dc1["location"];
                        Book_Status = (String)dc1["book_Status"];
                        Sub_location = (String)dc1["Sub_location"];

                        // string bookid = cretebookid();
                        string Status = "Pending For Final Upload";
                        if (Book_Type == "NA")
                        {
                            Status = "Book Type Missmatch";
                        }
                        if (Book_Category == "NA")
                        {
                            Status = "Book Catogery Missmatch";
                        }
                        if (Location == "NA")
                        {
                            Status = "Book Location Missmatch";
                        }
                        if (Book_Status == "NA")
                        {
                            Status = "Book Status Missmatch";
                        }

                        SqlCommand cmd;
                        string query = "INSERT INTO Library_Book_Entry_Master_Uniqe_Excel (BookId,Type,BookStatus,SelectClass,Subject,NameOfBook,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,EnterQuantity,Location,ISBN_Num,InvoiceNo,Price,Note,User_Id,Branch_id,Book_Category_Id,book_purchasedate,Created_date,Entry_Id,Is_Delete,Status,Book_Unique_Identifier,Transaction_Id,Sub_location) values (@BookId,@Type,@BookStatus,@SelectClass,@Subject,@NameOfBook,@AuthorName,@Publication,@EnterVolumePart,@Edition,@PublicationYear,@NoOfPages,@EnterQuantity,@Location,@ISBN_Num,@InvoiceNo,@Price,@Note,@User_Id,@Branch_id,@Book_Category_Id,@book_purchasedate,@Created_date,@Entry_Id,@Is_Delete,@Status,@Book_Unique_Identifier,@Transaction_Id,@Sub_location)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@BookId", Book_Id);
                        cmd.Parameters.AddWithValue("@Type", Book_Type);
                        cmd.Parameters.AddWithValue("@BookStatus", Book_Status);
                        cmd.Parameters.AddWithValue("@SelectClass", Class);
                        cmd.Parameters.AddWithValue("@Subject", "0");
                        cmd.Parameters.AddWithValue("@NameOfBook", Name_of_Book);
                        cmd.Parameters.AddWithValue("@AuthorName", Author_Name);
                        cmd.Parameters.AddWithValue("@Publication", Publication);
                        cmd.Parameters.AddWithValue("@EnterVolumePart", Volume_Name);
                        cmd.Parameters.AddWithValue("@Edition", Edition);
                        cmd.Parameters.AddWithValue("@PublicationYear", Publication_Year);
                        cmd.Parameters.AddWithValue("@NoOfPages", No_of_Pages);
                        cmd.Parameters.AddWithValue("@EnterQuantity", Quantity);
                        cmd.Parameters.AddWithValue("@Location", Location);
                        cmd.Parameters.AddWithValue("@ISBN_Num", ISBN_No.TrimEnd(','));
                        cmd.Parameters.AddWithValue("@InvoiceNo", Invoice_No);
                        cmd.Parameters.AddWithValue("@Price", Price);
                        cmd.Parameters.AddWithValue("@Note", "");
                        cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                        cmd.Parameters.AddWithValue("@Book_Category_Id", Book_Category);
                        cmd.Parameters.AddWithValue("@book_purchasedate", Purchase_Date);
                        cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Entry_Id", 0);
                        cmd.Parameters.AddWithValue("@Is_Delete", 1);
                        cmd.Parameters.AddWithValue("@Status", Status);
                        cmd.Parameters.AddWithValue("@Book_Unique_Identifier", 0);
                        cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                        cmd.Parameters.AddWithValue("@Sub_location", Sub_location);
                        
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {

                        }
                    }
                    bool final= false;
                    final_update_data(Transaction_Id);
                   
                    string query23 = "Select  * from Library_Book_Entry_Master_Uniqe_Excel where  User_Id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "' and  Transaction_Id='" + Transaction_Id + "' ";
                    DataTable dt1 = mycode.FillData(query23);
                    if (dt1.Rows.Count == 0)
                    {

                        grvExcelData.DataSource = null;
                        grvExcelData.DataBind();
                        GridView2.DataSource = null;
                        GridView2.DataBind();
                    }
                    else
                    {
                        grvExcelData.DataSource = null;
                        grvExcelData.DataBind();

                        GridView2.DataSource = dt1;
                        GridView2.DataBind();

                         final = dt1.AsEnumerable().All(row => row["Status"].ToString() == "Success");

                       
                    }

                    if(final==true)
                    {
                        Alertme("Book has has been uploaded successfully.", "success");
                        btn_Submit_final.Visible = false;
                        finalsubmitpnl.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        private void final_update_data(string group_transaction)
        {

            DataTable dt = mycode.FillData("Select * from Library_Book_Entry_Master_Uniqe_Excel where User_Id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "' and  Transaction_Id='" + group_transaction + "' and Status='Pending For Final Upload' ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                string Enteryid = cretebookid_enteryid();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Id = dt.Rows[i]["Id"].ToString();
                    string Type = dt.Rows[i]["Type"].ToString();
                    string BookStatus = dt.Rows[i]["BookStatus"].ToString();
                    string SelectClass = dt.Rows[i]["SelectClass"].ToString();

                    string Subject = dt.Rows[i]["Subject"].ToString();
                    string BookINameOfBookd = dt.Rows[i]["NameOfBook"].ToString();

                    string AuthorName = dt.Rows[i]["AuthorName"].ToString();
                    string Publication = dt.Rows[i]["Publication"].ToString();
                    string Volumename = dt.Rows[i]["EnterVolumePart"].ToString();
                    string Edition = dt.Rows[i]["Edition"].ToString();
                    string PublicationYear = dt.Rows[i]["PublicationYear"].ToString();
                    string NoOfPages = dt.Rows[i]["NoOfPages"].ToString();
                    string EnterQuantity = dt.Rows[i]["EnterQuantity"].ToString();
                    string Location = dt.Rows[i]["Location"].ToString();
                    string ISBN_Num = dt.Rows[i]["ISBN_Num"].ToString();
                    string InvoiceNo = dt.Rows[i]["InvoiceNo"].ToString();
                    string Price = dt.Rows[i]["Price"].ToString();
                    string Book_Category_Id = dt.Rows[i]["Book_Category_Id"].ToString();
                    string purchasedate = dt.Rows[i]["book_purchasedate"].ToString();
                    string Sub_location = dt.Rows[i]["Sub_location"].ToString();
                    string Book_Id = dt.Rows[i]["BookId"].ToString();
                    
                    Final_add_update_book_id(Type, BookStatus, SelectClass, BookINameOfBookd, Volumename, Edition, PublicationYear, NoOfPages, EnterQuantity, ISBN_Num, InvoiceNo, Price, purchasedate, Enteryid, AuthorName, Publication, Location, Book_Category_Id, Id, Sub_location, Book_Id);
                }

            }

        }



        private string cretebookid_enteryid()
        {

            bool duplicate = false;
            string Book_Entry_Id = Library.session_wisl("Book_Entry_Id", ViewState["Branch_id"].ToString());// mycode.auto_serial("BookId");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("select Entry_Id from  Library_Book_Entry_Master_Uniqe where Entry_Id='" + Book_Entry_Id + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Book_Entry_Id = Library.session_wisl("Book_Entry_Id", ViewState["Branch_id"].ToString());
                }
            }
            return Book_Entry_Id;
        }
        private string crete_Book_Unique_Identifier()
        {
            bool duplicate = false;
            string Book_Unique_Identifier = Library.session_wisl("Book_Unique_Identifier", ViewState["Branch_id"].ToString());//  
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("select Entry_Id from  Library_Book_Entry_Master_Uniqe where Book_Unique_Identifier='" + Book_Unique_Identifier + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Book_Unique_Identifier = Library.session_wisl("Book_Unique_Identifier", ViewState["Branch_id"].ToString());
                }
            }
            return Book_Unique_Identifier;
        }
        private string cretebookid()
        {
            bool duplicate = false;
            string BookId = Library.session_wisl("BookId", ViewState["Branch_id"].ToString());// mycode.auto_serial("BookId");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("select BookId from  Library_Book_Entry where BookId='" + BookId + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    BookId = Library.session_wisl("BookId", ViewState["Branch_id"].ToString());
                }
            }
            return BookId;
        }

        private void Final_add_update_book_id(string type, string bookStatus, string selectClass, string bookINameOfBookd, string volumename, string edition, string publicationYear, string noOfPages, string qty, string iSBN_Num, string invoiceNo, string price, string purchasedate, string enteryid, string AuthorName, string Publication, string Location, string Book_Category_Id, string Id,string Sub_Location_id,string bookid)
        {

            try
            {



                if (selectClass == "NA")
                {
                    selectClass = "0";
                }
                double price1 = My.toDouble(price);
                string Book_Unique_Identifier = crete_Book_Unique_Identifier();

                int qty2 = Convert.ToInt32(qty);
                for (int i = 0; i < qty2; i++)
                {

                    if (bookid == "0")
                    {
                        bookid = cretebookid();

                    }

                    bool chke_book_id = check_duplicate_data(bookid);
                    if (chke_book_id == true)
                    {


                        string barcode = ly.barcode_num("Bookbarcode", ViewState["Branch_id"].ToString()); //ViewState["Branch_id"].ToString()
                        string barcode_image = ly.get_barcode_img(barcode, iSBN_Num, price1.ToString("0.00"), bookid, "");

                        SqlCommand cmd;
                        string query = "insert into Library_Book_Entry(BookId,Type,BookStatus,SelectClass,Subject,NameOfBook,AuthorName,Publication,EnterVolumePart,StartVolumeRange,EndVolumeRange,Edition,PublicationYear,NoOfPages,EnterQuantity,Location,ISBN_Num,InvoiceNo,Price,Note,book_purchasedate,Branch_id,Book_Category_Id,Created_date,User_id,Entry_Id,Issued_Status,Is_Delete,Book_Unique_Identifier,Bar_code,Barcode_img,Sub_Location_id) values (@BookId,@Type,@BookStatus,@SelectClass,@Subject,@NameOfBook,@AuthorName,@Publication,@EnterVolumePart,@StartVolumeRange,@EndVolumeRange,@Edition,@PublicationYear,@NoOfPages,@EnterQuantity,@Location,@ISBN_Num,@InvoiceNo,@Price,@Note,@book_purchasedate,@Branch_id,@Book_Category_Id,@Created_date,@User_id,@Entry_Id,@Issued_Status,@Is_Delete,@Book_Unique_Identifier,@Bar_code,@Barcode_img,@Sub_Location_id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@BookId", bookid);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@BookStatus", bookStatus);
                        cmd.Parameters.AddWithValue("@SelectClass", selectClass);
                        cmd.Parameters.AddWithValue("@Subject", "0");
                        cmd.Parameters.AddWithValue("@NameOfBook", bookINameOfBookd);
                        cmd.Parameters.AddWithValue("@AuthorName", AuthorName);
                        cmd.Parameters.AddWithValue("@Publication", Publication);
                        cmd.Parameters.AddWithValue("@EnterVolumePart", volumename);
                        cmd.Parameters.AddWithValue("@StartVolumeRange", 0);
                        cmd.Parameters.AddWithValue("@EndVolumeRange", 0);
                        cmd.Parameters.AddWithValue("@Edition", edition);
                        cmd.Parameters.AddWithValue("@PublicationYear", publicationYear);
                        cmd.Parameters.AddWithValue("@NoOfPages", noOfPages);
                        cmd.Parameters.AddWithValue("@EnterQuantity", 1);
                        cmd.Parameters.AddWithValue("@Location", Location);
                        cmd.Parameters.AddWithValue("@ISBN_Num", iSBN_Num);
                        cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                        cmd.Parameters.AddWithValue("@Price", price1.ToString("0.00"));
                        cmd.Parameters.AddWithValue("@Note", "");
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@book_purchasedate", purchasedate);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                        cmd.Parameters.AddWithValue("@Book_Category_Id", Book_Category_Id);
                        cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Entry_Id", enteryid);
                        cmd.Parameters.AddWithValue("@Issued_Status", "Not Issued");
                        cmd.Parameters.AddWithValue("@Is_Delete", 1);
                        cmd.Parameters.AddWithValue("@Book_Unique_Identifier", Book_Unique_Identifier);
                        cmd.Parameters.AddWithValue("@Bar_code", barcode);
                        cmd.Parameters.AddWithValue("@Barcode_img", barcode_image);
                        cmd.Parameters.AddWithValue("@Sub_Location_id", Sub_Location_id);

                        if (My.InsertUpdateData(cmd))
                        {
                            string query2 = "Select * from Library_Book_Entry_Master_Uniqe where Branch_id='" + ViewState["Branch_id"].ToString() + "'  and  EnterVolumePart='" + volumename + "' and Edition='" + edition + "' and  NameOfBook='" + bookINameOfBookd + "' and  ISBN_Num='" + iSBN_Num + "' and  BookId='" + bookid + "'";
                            DataTable dt = mycode.FillData(query2);
                            if (dt.Rows.Count == 0)
                            {
                                SqlCommand cmd1;
                                string query3 = "insert into Library_Book_Entry_Master_Uniqe(BookId,Type,BookStatus,SelectClass,Subject,NameOfBook,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,EnterQuantity,Location,ISBN_Num,InvoiceNo,Price,Note,book_purchasedate,Branch_id,Book_Category_Id,Created_date,User_id,Entry_Id,Is_Delete,Book_Unique_Identifier,Sub_Location_id) values (@BookId,@Type,@BookStatus,@SelectClass,@Subject,@NameOfBook,@AuthorName,@Publication,@EnterVolumePart,@Edition,@PublicationYear,@NoOfPages,@EnterQuantity,@Location,@ISBN_Num,@InvoiceNo,@Price,@Note,@book_purchasedate,@Branch_id,@Book_Category_Id,@Created_date,@User_id,@Entry_Id,@Is_Delete,@Book_Unique_Identifier,@Sub_Location_id)";
                                cmd1 = new SqlCommand(query3);
                                cmd1.Parameters.AddWithValue("@BookId", bookid);
                                cmd1.Parameters.AddWithValue("@Type", type);
                                cmd1.Parameters.AddWithValue("@BookStatus", bookStatus);
                                cmd1.Parameters.AddWithValue("@SelectClass", selectClass);
                                cmd1.Parameters.AddWithValue("@Subject", "0");
                                cmd1.Parameters.AddWithValue("@NameOfBook", bookINameOfBookd);
                                cmd1.Parameters.AddWithValue("@AuthorName", AuthorName);
                                cmd1.Parameters.AddWithValue("@Publication", Publication);
                                cmd1.Parameters.AddWithValue("@EnterVolumePart", volumename);
                                cmd1.Parameters.AddWithValue("@Edition", edition);
                                cmd1.Parameters.AddWithValue("@PublicationYear", publicationYear);
                                cmd1.Parameters.AddWithValue("@NoOfPages", noOfPages);
                                cmd1.Parameters.AddWithValue("@EnterQuantity", qty2);
                                cmd1.Parameters.AddWithValue("@Location", Location);
                                cmd1.Parameters.AddWithValue("@ISBN_Num", iSBN_Num);
                                cmd1.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                                cmd1.Parameters.AddWithValue("@Price", price1.ToString("0.00"));
                                cmd1.Parameters.AddWithValue("@Note", "");
                                cmd1.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd1.Parameters.AddWithValue("@book_purchasedate", purchasedate);
                                cmd1.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                                cmd1.Parameters.AddWithValue("@Book_Category_Id", Book_Category_Id);
                                cmd1.Parameters.AddWithValue("@Created_date", My.getdate1());
                                cmd1.Parameters.AddWithValue("@Entry_Id", enteryid);
                                cmd1.Parameters.AddWithValue("@Is_Delete", 1);
                                cmd1.Parameters.AddWithValue("@Book_Unique_Identifier", Book_Unique_Identifier);
                                cmd1.Parameters.AddWithValue("@Sub_Location_id", Sub_Location_id);

                                if (My.InsertUpdateData(cmd1))
                                {
                                    SqlCommand cmd3;
                                    string query22 = "Select * from Book_Entry_Temp where BookId='" + bookid + "' and Volumename='" + volumename + "' and edition='" + edition + "' ";
                                    DataTable cdt = mycode.FillData(query22);

                                    if (cdt.Rows.Count == 0)
                                    {
                                        string query33 = "INSERT INTO Book_Entry_Temp (BookId,Volumename,start_volumerange,end_volumerange,edition,publication_year,no_of_pages,qty,isbn,Invoice_No,Price,purchasedate) values (@BookId,@Volumename,@start_volumerange,@end_volumerange,@edition,@publication_year,@no_of_pages,@qty,@isbn,@Invoice_No,@Price,@purchasedate)";
                                        cmd3 = new SqlCommand(query33);
                                        cmd3.Parameters.AddWithValue("@BookId", bookid);
                                        cmd3.Parameters.AddWithValue("@Volumename", volumename);
                                        cmd3.Parameters.AddWithValue("@start_volumerange", "");
                                        cmd3.Parameters.AddWithValue("@end_volumerange", "");
                                        cmd3.Parameters.AddWithValue("@edition", edition);
                                        cmd3.Parameters.AddWithValue("@publication_year", publicationYear);
                                        cmd3.Parameters.AddWithValue("@no_of_pages", noOfPages);
                                        cmd3.Parameters.AddWithValue("@qty", qty2);
                                        cmd3.Parameters.AddWithValue("@isbn", iSBN_Num);
                                        cmd3.Parameters.AddWithValue("@Invoice_No", invoiceNo);
                                        cmd3.Parameters.AddWithValue("@Price", price1.ToString("0.00"));
                                        cmd3.Parameters.AddWithValue("@purchasedate", purchasedate);
                                        if (My.InsertUpdateData(cmd3))
                                        {
                                            mycode.executequery("update Library_Book_Entry_Master_Uniqe_Excel set Status='Success' where Id=" + Id + "");

                                        }
                                    }
                                    else
                                    {


                                    }



                                }
                            }
                            else
                            {


                            }

                        }



                    }
                    else
                    {


                        string msg = "Book-item Id=" + bookid + " is dublicate book name=" + bookINameOfBookd;
                        string msg1 = "Book-item Id=" + bookid + " is dublicate";
                        mycode.executequery("update Library_Book_Entry_Master_Uniqe_Excel set Status='" + msg1 + "' where Id=" + Id + "");

                        mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["Branch_id"].ToString());
                    }

                }
            }
             
            catch(Exception)
            {
              
                string msg1 = "Book-item Id=" + bookid + " is  varchar";
                mycode.executequery("update Library_Book_Entry_Master_Uniqe_Excel set Status='" + msg1 + "' where Id=" + Id + "");

               
            }
        }

        private bool check_duplicate_data(string bookid)
        {
            DataTable cdt = mycode.FillData("select BookId from  Library_Book_Entry where BookId='" + bookid + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return true;
            }
            else
            {
                return false;
                 
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                if (string.Equals(status, "Success", StringComparison.OrdinalIgnoreCase))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen; // green for Success
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral; // red for anything else
                }
            }
        }
    }
}
