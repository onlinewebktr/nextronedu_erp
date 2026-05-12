using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin
{
    public partial class New_Book_Entry_N : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_location, "Select location,Location_id from lib_location_details where Branch_id = '" + ViewState["Branch_id"].ToString() + "'");

                        mycode.bind_all_ddl_with_id_cap_NA(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_book_status, "Select BookStatus,BookStatusId from Library_Book_Status");
                        mycode.bind_all_ddl_with_id(ddl_book_type, "Select TypeName,TypeId from Library_Type where Branch_Id='" + ViewState["Branch_id"].ToString() + "'");
                        mycode.bind_all_ddl_with_id(ddl_book_catogery, "Select Book_Category,Book_Category_Id from Library_Book_Category where Branch_Id='" + ViewState["Branch_id"].ToString() + "' ");

                        mycode.bind_ddl(ddl_publication_year, "Select Year from Library_publication_year_Master order by Year asc");

                        if (Request.QueryString["Book_Unique_Identifier"] != null)
                        {
                            ViewState["booktyepedit"] = "1";

                            hd_id.Value = Request.QueryString["Book_Unique_Identifier"];
                            ViewState["BookIdtemp"] = Request.QueryString["BookId"];

                            Bind_temp_data();
                            Bind_book_info();
                        }
                        else
                        {
                            ViewState["booktyepedit"] = "0";
                            ViewState["BookIdtemp"] = Library.create_random_no_tempbookid();
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "New_Book_Entry_N");
            }

        }

        private void Bind_book_info()
        {
            string query = "Select * from Library_Book_Entry_Master_Uniqe where Book_Unique_Identifier='" + hd_id.Value + "' and Branch_id=" + ViewState["Branch_id"].ToString() + "";
            DataTable cdt = mycode.FillData(query);
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {

            }
            else
            {
                ViewState["Enteryid"] = cdt.Rows[0]["Entry_Id"].ToString();
                ddl_book_type.SelectedValue = cdt.Rows[0]["Type"].ToString();
                ddl_book_status.SelectedValue = cdt.Rows[0]["BookStatus"].ToString();
                ddl_class.SelectedValue = cdt.Rows[0]["SelectClass"].ToString();

                ddl_location.SelectedValue = cdt.Rows[0]["Location"].ToString();
                try
                {
                    mycode.bind_all_ddl_with_id_na(ddl_sublocation, "Select Sub_Location,Sub_Location_id from lib_sub_location_details where Branch_Id='" + ViewState["Branch_id"].ToString() + "' and Location_id='" + ddl_location.SelectedValue + "' ");
                    ddl_sublocation.SelectedValue= cdt.Rows[0]["Sub_Location_id"].ToString();
                }
                catch
                {
                    ddl_sublocation.SelectedValue = "0";
                }
                txt_name_of_the_book.Text = cdt.Rows[0]["NameOfBook"].ToString();
                txt_Author_name.Text = cdt.Rows[0]["AuthorName"].ToString();
                txt_Publication.Text = cdt.Rows[0]["Publication"].ToString();
                ddl_book_catogery.SelectedValue = cdt.Rows[0]["Book_Category_Id"].ToString();
                btn_add_submit_add_book_item.Text = "Update";
                btn_Submit_final.Text = "Update";
            }
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

        #region add_New_Book_Entry
        protected void btn_add_submit_add_book_item_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;

            if (txt_volumename.Text == "")
            {
                Alertme("Please enter Volume Part ", "warning");
            }
            else if (txt_edition.Text == "")
            {
                Alertme("Please enter edition ", "warning");
            }
            else if (ddl_publication_year.SelectedValue == "Select")
            {
                Alertme("Please slect publication year", "warning");
            }
            else if (txt_no_of_pages.Text == "Select")
            {
                Alertme("Please slect publication year", "warning");
            }
            else if (txt_qty.Text == "")
            {
                Alertme("Please enter qty", "warning");
            }
            else if (txt_isbn.Text == "")
            {
                Alertme("Please enter ISBN", "warning");
            }
            else if (txt_Invoice_No.Text == "")
            {
                Alertme("Please enter invoice date", "warning");
            }
            else if (txt_purchasedate.Text == "")
            {
                Alertme("Please purchase date", "warning");
            }
            else
            {
                if (btn_add_submit_add_book_item.Text == "Add Book Item")
                {

                    string query2 = "Select * from Book_Entry_Temp where BookId='" + ViewState["BookIdtemp"].ToString() + "' and Volumename='" + txt_volumename.Text + "' and edition='" + txt_edition.Text + "' ";
                    DataTable cdt = mycode.FillData(query2);

                    if (cdt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Book_Entry_Temp (BookId,Volumename,start_volumerange,end_volumerange,edition,publication_year,no_of_pages,qty,isbn,Invoice_No,Price,purchasedate,Book_Security_Code) values (@BookId,@Volumename,@start_volumerange,@end_volumerange,@edition,@publication_year,@no_of_pages,@qty,@isbn,@Invoice_No,@Price,@purchasedate,@Book_Security_Code)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@BookId", ViewState["BookIdtemp"].ToString());
                        cmd.Parameters.AddWithValue("@Volumename", txt_volumename.Text);
                        cmd.Parameters.AddWithValue("@start_volumerange", "");
                        cmd.Parameters.AddWithValue("@end_volumerange", "");
                        cmd.Parameters.AddWithValue("@edition", txt_edition.Text);
                        cmd.Parameters.AddWithValue("@publication_year", ddl_publication_year.Text);
                        cmd.Parameters.AddWithValue("@no_of_pages", txt_no_of_pages.Text);
                        cmd.Parameters.AddWithValue("@qty", txt_qty.Text);
                        cmd.Parameters.AddWithValue("@isbn", txt_isbn.Text);
                        cmd.Parameters.AddWithValue("@Invoice_No", txt_Invoice_No.Text);
                        cmd.Parameters.AddWithValue("@Price", txt_Price.Text);
                        cmd.Parameters.AddWithValue("@purchasedate", txt_purchasedate.Text);
                        cmd.Parameters.AddWithValue("@Book_Security_Code",txt_Book_Security_Code.Text);
                        
                        if (My.InsertUpdateData(cmd))
                        {
                            btn_add_submit_add_book_item.Text = "Add Book Item";
                            //Alertme("Book has been save Successfully.", "success");
                            clear();
                            Bind_temp_data();
                        }
                    }
                    else
                    {
                        Alertme("Sorry this volume name alredy exist", "warning");

                    }
                }
                else
                {
                    string query2 = "Select * from Book_Entry_Temp where BookId='" + ViewState["BookIdtemp"].ToString() + "' and Volumename='" + txt_volumename.Text + "' and edition='" + txt_edition.Text + "' and  Id!=@Id";
                    DataTable cdt = mycode.FillData(query2);

                    if (cdt.Rows.Count == 0)
                    {
                        string query = "Update Book_Entry_Temp set  Volumename=@Volumename,start_volumerange=@start_volumerange,end_volumerange=@end_volumerange,edition=@edition,publication_year=@publication_year,no_of_pages=@no_of_pages,qty=@qty,isbn=@isbn,Invoice_No=@Invoice_No,Price=@Price,purchasedate=@purchasedate,Book_Security_Code=@Book_Security_Code where Id = @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Id", hd_temp_id.Value);
                        cmd.Parameters.AddWithValue("@Volumename", txt_volumename.Text);
                        cmd.Parameters.AddWithValue("@start_volumerange", "");
                        cmd.Parameters.AddWithValue("@end_volumerange", "");
                        cmd.Parameters.AddWithValue("@edition", txt_edition.Text);
                        cmd.Parameters.AddWithValue("@publication_year", ddl_publication_year.Text);
                        cmd.Parameters.AddWithValue("@no_of_pages", txt_no_of_pages.Text);
                        cmd.Parameters.AddWithValue("@qty", txt_qty.Text);
                        cmd.Parameters.AddWithValue("@isbn", txt_isbn.Text);
                        cmd.Parameters.AddWithValue("@Invoice_No", txt_Invoice_No.Text);
                        cmd.Parameters.AddWithValue("@Price", txt_Price.Text);
                        cmd.Parameters.AddWithValue("@purchasedate", txt_purchasedate.Text);
                        cmd.Parameters.AddWithValue("@Book_Security_Code", txt_Book_Security_Code.Text);
                        if (My.InsertUpdateData(cmd))
                        {
                            if (ViewState["booktyepedit"].ToString() == "1")
                            {
                                btn_add_submit_add_book_item.Text = "Update";
                            }
                            else
                            {
                                btn_add_submit_add_book_item.Text = "Add Book Item";
                            }

                            //Alertme("Book has been update Successfully.", "success");
                            clear();
                            Bind_temp_data();
                        }
                    }
                    else
                    {
                        Alertme("Sorry this volume name alredy exist", "warning");

                    }
                }



            }


        }

        private void Bind_temp_data()
        {
            DataTable dt = mycode.FillData("Select * from Book_Entry_Temp where BookId='" + ViewState["BookIdtemp"].ToString() + "'  ");
            if (dt.Rows.Count == 0)
            {
                finalsubmitpnl.Visible = false;
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                finalsubmitpnl.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        private void clear()
        {
            txt_volumename.Text = "";

            txt_edition.Text = "";

            txt_no_of_pages.Text = "";
            txt_qty.Text = "";
            txt_isbn.Text = "";
            txt_Invoice_No.Text = "";
            txt_Price.Text = "";
            txt_purchasedate.Text = "";
            ddl_publication_year.Text = "Select";
        }
        #endregion

        #region edit delete gride temp
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_EnterVolumePart = (Label)row.FindControl("lbl_EnterVolumePart");
                Label lbl_start_volumerange = (Label)row.FindControl("lbl_start_volumerange");
                Label lbl_end_volumerange = (Label)row.FindControl("lbl_end_volumerange");
                Label lbl_edition = (Label)row.FindControl("lbl_edition");
                Label lbl_publication_year = (Label)row.FindControl("lbl_publication_year");
                Label lbl_no_of_pages = (Label)row.FindControl("lbl_no_of_pages");
                Label lbl_qty = (Label)row.FindControl("lbl_qty");
                Label lbl_isbn = (Label)row.FindControl("lbl_isbn");
                Label lbl_Invoice_No = (Label)row.FindControl("lbl_Invoice_No");
                Label lbl_Price = (Label)row.FindControl("lbl_Price");
                Label lbl_purchasedate = (Label)row.FindControl("lbl_purchasedate");
                Label lbl_Book_Security_Code = (Label)row.FindControl("lbl_Book_Security_Code");
                
                try
                {
                    txt_Book_Security_Code.Text = lbl_Book_Security_Code.Text;
                }
                catch
                {

                }

                btn_add_submit_add_book_item.Text = "Update";
                hd_temp_id.Value = lbl_Id.Text;

                txt_volumename.Text = lbl_EnterVolumePart.Text;

                txt_edition.Text = lbl_edition.Text;
                ddl_publication_year.Text = lbl_publication_year.Text;
                txt_no_of_pages.Text = lbl_no_of_pages.Text;

                txt_qty.Text = lbl_qty.Text;

                if (ViewState["booktyepedit"].ToString() == "1")
                {
                    txt_qty.ReadOnly = true;
                }
                else
                {
                    txt_qty.ReadOnly = false;
                }

                txt_isbn.Text = lbl_isbn.Text;
                txt_Invoice_No.Text = lbl_Invoice_No.Text;
                txt_Price.Text = lbl_Price.Text;
                txt_purchasedate.Text = lbl_purchasedate.Text;
                txt_Book_Security_Code.Text = "";



            }
            catch
            {

            }


        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                mycode.executequery("delete from Book_Entry_Temp where BookId=" + lbl_Id.Text + "");
                Bind_temp_data();
            }
            catch
            {

            }
        }
        #endregion



        #region final submit
        protected void btn_Submit_final_Click(object sender, EventArgs e)
        {
            string Enteryid = "";
            if (ddl_book_type.SelectedItem.Text == "Select")
            {
                Alertme("Please select book type", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_book_status.SelectedItem.Text == "Select")
            {
                Alertme("Please select book status", "warning");
            }
            else if (ddl_book_catogery.SelectedItem.Text == "Select")
            {
                Alertme("Please select book category", "warning");
            }
            else if (ddl_location.SelectedItem.Text == "Select")
            {
                Alertme("Please select book location", "warning");
            }
            else if (ddl_sublocation.SelectedItem.Text == "Select")
            {
                Alertme("Please select book sub location", "warning");
            }
            else
            {
                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (ViewState["booktyepedit"].ToString() == "1")// edit
                    {
                        Enteryid = ViewState["Enteryid"].ToString();
                    }
                    else
                    {
                        Enteryid = cretebookid_enteryid();
                    }

                    DataTable dt = mycode.FillData("Select * from Book_Entry_Temp where BookId='" + ViewState["BookIdtemp"].ToString() + "'  ");
                    if (dt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Id = dt.Rows[i]["Id"].ToString();
                            string BookId = dt.Rows[i]["BookId"].ToString();
                            string Volumename = dt.Rows[i]["Volumename"].ToString();
                            string start_volumerange = dt.Rows[i]["start_volumerange"].ToString();
                            string end_volumerange = dt.Rows[i]["end_volumerange"].ToString();
                            string edition = dt.Rows[i]["edition"].ToString();
                            string publication_year = dt.Rows[i]["publication_year"].ToString();
                            string no_of_pages = dt.Rows[i]["no_of_pages"].ToString();
                            string qty = dt.Rows[i]["qty"].ToString();
                            string isbn = dt.Rows[i]["isbn"].ToString();
                            string Invoice_No = dt.Rows[i]["Invoice_No"].ToString();
                            string Price = dt.Rows[i]["Price"].ToString();
                            string purchasedate = dt.Rows[i]["purchasedate"].ToString();
                            string Book_Security_Code = dt.Rows[i]["Book_Security_Code"].ToString();
                            
                            Final_add_update_book_id(Volumename, start_volumerange, end_volumerange, edition, publication_year, no_of_pages, qty, isbn, Invoice_No, Price, purchasedate, Id, Enteryid, BookId, Book_Security_Code);

                        }




                    }
                    clear_data();
                    btn_Submit_final.Text = "Final Submit";
                    if (ViewState["booktyepedit"].ToString() == "1")// if edit
                    {
                        Alertme("Book has been update Successfully.", "success");
                    }
                    else
                    {
                        Alertme("Book has been save Successfully.", "success");

                    }


                }
                else
                {




                }
            }

        }


        private void Final_add_update_book_id(string volumename, string start_volumerange, string end_volumerange, string edition, string publication_year, string no_of_pages, string qty, string isbn, string invoice_No, string price, string purchasedate, string Id, string Enteryid, string BookId,string Book_Security_Code)
        {
            double price1 = My.toDouble(price);
            if (ViewState["booktyepedit"].ToString() == "1")// if edit
            {
                //  string barcode = ly.get_save_bar_code();
                // string barcode_image = ly.get_barcode_img(barcode, isbn, price1.ToString("0.00"), BookId);

                SqlCommand cmd;

                string query = "Update Library_Book_Entry set Type=@Type,BookStatus=@BookStatus,SelectClass=@SelectClass,Subject=@Subject,NameOfBook=@NameOfBook,AuthorName=@AuthorName,Publication=@Publication,EnterVolumePart=@EnterVolumePart,StartVolumeRange=@StartVolumeRange,EndVolumeRange=@EndVolumeRange,Edition=@Edition,PublicationYear=@PublicationYear,NoOfPages=@NoOfPages,EnterQuantity=@EnterQuantity,Location=@Location,ISBN_Num=@ISBN_Num,InvoiceNo=@InvoiceNo,Price=@Price,Note=@Note,Branch_id=@Branch_id,Book_Category_Id=@Book_Category_Id,book_purchasedate=@book_purchasedate,Update_by=@Update_by,updated_date=@updated_date,Book_Security_Code=@Book_Security_Code,Sub_Location_id=@Sub_Location_id where Book_Unique_Identifier=@Book_Unique_Identifier";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Book_Unique_Identifier", hd_id.Value);
                cmd.Parameters.AddWithValue("@Type", ddl_book_type.SelectedValue);
                cmd.Parameters.AddWithValue("@BookStatus", ddl_book_status.SelectedValue);
                cmd.Parameters.AddWithValue("@SelectClass", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Subject", "0");
                cmd.Parameters.AddWithValue("@NameOfBook", txt_name_of_the_book.Text);
                cmd.Parameters.AddWithValue("@AuthorName", txt_Author_name.Text);
                cmd.Parameters.AddWithValue("@Publication", txt_Publication.Text);
                cmd.Parameters.AddWithValue("@EnterVolumePart", volumename);
                cmd.Parameters.AddWithValue("@StartVolumeRange", start_volumerange);
                cmd.Parameters.AddWithValue("@EndVolumeRange", end_volumerange);
                cmd.Parameters.AddWithValue("@Edition", edition);
                cmd.Parameters.AddWithValue("@PublicationYear", publication_year);
                cmd.Parameters.AddWithValue("@NoOfPages", no_of_pages);
                cmd.Parameters.AddWithValue("@EnterQuantity", 1);
                cmd.Parameters.AddWithValue("@Location", ddl_location.SelectedValue);
                cmd.Parameters.AddWithValue("@ISBN_Num", isbn);
                cmd.Parameters.AddWithValue("@InvoiceNo", invoice_No);
                cmd.Parameters.AddWithValue("@Price", price1.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Note", "");
                cmd.Parameters.AddWithValue("@Update_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@book_purchasedate", purchasedate);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@Book_Category_Id", ddl_book_catogery.SelectedValue);
                cmd.Parameters.AddWithValue("@updated_date", My.getdate1());
                cmd.Parameters.AddWithValue("@Book_Security_Code", Book_Security_Code);
                cmd.Parameters.AddWithValue("@Sub_Location_id",ddl_sublocation.SelectedValue);
                

                if (My.InsertUpdateData(cmd))
                {

                    SqlCommand cmd1;
                    string query3 = "Update Library_Book_Entry_Master_Uniqe set BookId=@BookId,Type=@Type,BookStatus=@BookStatus,SelectClass=@SelectClass,Subject=@Subject,NameOfBook=@NameOfBook,AuthorName=@AuthorName,Publication=@Publication,EnterVolumePart=@EnterVolumePart,Edition=@Edition,PublicationYear=@PublicationYear,NoOfPages=@NoOfPages,EnterQuantity=@EnterQuantity,Location=@Location,ISBN_Num=@ISBN_Num,InvoiceNo=@InvoiceNo,Price=@Price,Note=@Note,Branch_id=@Branch_id,Book_Category_Id=@Book_Category_Id,book_purchasedate=@book_purchasedate,Update_by=@Update_by,updated_date=@updated_date,Sub_Location_id=@Sub_Location_id  where  Book_Unique_Identifier=@Book_Unique_Identifier and BookId=@BookId";
                    cmd1 = new SqlCommand(query3);
                    cmd1.Parameters.AddWithValue("@Book_Unique_Identifier", hd_id.Value);
                    cmd1.Parameters.AddWithValue("@BookId", BookId);
                    cmd1.Parameters.AddWithValue("@Type", ddl_book_type.SelectedValue);
                    cmd1.Parameters.AddWithValue("@BookStatus", ddl_book_status.SelectedValue);
                    cmd1.Parameters.AddWithValue("@SelectClass", ddl_class.SelectedValue);
                    cmd1.Parameters.AddWithValue("@Subject", "0");
                    cmd1.Parameters.AddWithValue("@NameOfBook", txt_name_of_the_book.Text);
                    cmd1.Parameters.AddWithValue("@AuthorName", txt_Author_name.Text);
                    cmd1.Parameters.AddWithValue("@Publication", txt_Publication.Text);
                    cmd1.Parameters.AddWithValue("@EnterVolumePart", volumename);
                    cmd1.Parameters.AddWithValue("@Edition", edition);
                    cmd1.Parameters.AddWithValue("@PublicationYear", publication_year);
                    cmd1.Parameters.AddWithValue("@NoOfPages", no_of_pages);
                    cmd1.Parameters.AddWithValue("@EnterQuantity", qty);
                    cmd1.Parameters.AddWithValue("@Location", ddl_location.SelectedValue);
                    cmd1.Parameters.AddWithValue("@ISBN_Num", isbn);
                    cmd1.Parameters.AddWithValue("@InvoiceNo", invoice_No);
                    cmd1.Parameters.AddWithValue("@Price", price1.ToString("0.00"));
                    cmd1.Parameters.AddWithValue("@Note", "");
                    cmd1.Parameters.AddWithValue("@Update_by", ViewState["Userid"].ToString());
                    cmd1.Parameters.AddWithValue("@book_purchasedate", purchasedate);
                    cmd1.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    cmd1.Parameters.AddWithValue("@Book_Category_Id", ddl_book_catogery.SelectedValue);
                    cmd1.Parameters.AddWithValue("@updated_date", My.getdate1());
                    cmd1.Parameters.AddWithValue("@Entry_Id", Enteryid);
                    cmd1.Parameters.AddWithValue("@Sub_Location_id", ddl_sublocation.SelectedValue);

                   
                    if (My.InsertUpdateData(cmd1))
                    {
                        update_bar_code_update_time(hd_id.Value, isbn, price1);
                    }



                    // Alertme("Book has been update Successfully.", "success");
                    //  btn_Submit_final.Text = "Add";



                }

            }
            else
            {

                string Book_Unique_Identifier = crete_Book_Unique_Identifier();

                int qty2 = Convert.ToInt32(qty);
                for (int i = 0; i < qty2; i++)
                {
                    string bookid = cretebookid();

                    string barcode = ly.barcode_num("Bookbarcode", ViewState["Branch_id"].ToString()); //ViewState["Branch_id"].ToString()
                    string barcode_image = ly.get_barcode_img(barcode, isbn, price1.ToString("0.00"), bookid, txt_name_of_the_book.Text);
                    SqlCommand cmd;

                    string query = "insert into Library_Book_Entry(BookId,Type,BookStatus,SelectClass,Subject,NameOfBook,AuthorName,Publication,EnterVolumePart,StartVolumeRange,EndVolumeRange,Edition,PublicationYear,NoOfPages,EnterQuantity,Location,ISBN_Num,InvoiceNo,Price,Note,book_purchasedate,Branch_id,Book_Category_Id,Created_date,User_id,Entry_Id,Issued_Status,Is_Delete,Book_Unique_Identifier,Bar_code,Barcode_img,Book_Security_Code,Sub_Location_id) values (@BookId,@Type,@BookStatus,@SelectClass,@Subject,@NameOfBook,@AuthorName,@Publication,@EnterVolumePart,@StartVolumeRange,@EndVolumeRange,@Edition,@PublicationYear,@NoOfPages,@EnterQuantity,@Location,@ISBN_Num,@InvoiceNo,@Price,@Note,@book_purchasedate,@Branch_id,@Book_Category_Id,@Created_date,@User_id,@Entry_Id,@Issued_Status,@Is_Delete,@Book_Unique_Identifier,@Bar_code,@Barcode_img,@Book_Security_Code,@Sub_Location_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@BookId", bookid);
                    cmd.Parameters.AddWithValue("@Type", ddl_book_type.SelectedValue);
                    cmd.Parameters.AddWithValue("@BookStatus", ddl_book_status.SelectedValue);
                    cmd.Parameters.AddWithValue("@SelectClass", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject", "0");
                    cmd.Parameters.AddWithValue("@NameOfBook", txt_name_of_the_book.Text);
                    cmd.Parameters.AddWithValue("@AuthorName", txt_Author_name.Text);
                    cmd.Parameters.AddWithValue("@Publication", txt_Publication.Text);
                    cmd.Parameters.AddWithValue("@EnterVolumePart", volumename);
                    cmd.Parameters.AddWithValue("@StartVolumeRange", start_volumerange);
                    cmd.Parameters.AddWithValue("@EndVolumeRange", end_volumerange);
                    cmd.Parameters.AddWithValue("@Edition", edition);
                    cmd.Parameters.AddWithValue("@PublicationYear", publication_year);
                    cmd.Parameters.AddWithValue("@NoOfPages", no_of_pages);
                    cmd.Parameters.AddWithValue("@EnterQuantity", 1);
                    cmd.Parameters.AddWithValue("@Location", ddl_location.SelectedValue);
                    cmd.Parameters.AddWithValue("@ISBN_Num", isbn);
                    cmd.Parameters.AddWithValue("@InvoiceNo", invoice_No);
                    cmd.Parameters.AddWithValue("@Price", price1.ToString("0.00"));
                    cmd.Parameters.AddWithValue("@Note", "");
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@book_purchasedate", purchasedate);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    cmd.Parameters.AddWithValue("@Book_Category_Id", ddl_book_catogery.SelectedValue);
                    cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                    cmd.Parameters.AddWithValue("@Entry_Id", Enteryid);
                    cmd.Parameters.AddWithValue("@Issued_Status", "Not Issued");
                    cmd.Parameters.AddWithValue("@Is_Delete", 1);
                    cmd.Parameters.AddWithValue("@Book_Unique_Identifier", Book_Unique_Identifier);
                    cmd.Parameters.AddWithValue("@Bar_code", barcode);
                    cmd.Parameters.AddWithValue("@Barcode_img", barcode_image);
                    cmd.Parameters.AddWithValue("@Book_Security_Code", Book_Security_Code);
                    cmd.Parameters.AddWithValue("@Sub_Location_id", ddl_sublocation.SelectedValue);
                    if (My.InsertUpdateData(cmd))
                    {
                        string query2 = "Select * from Library_Book_Entry_Master_Uniqe where Branch_id='" + ViewState["Branch_id"].ToString() + "'  and  EnterVolumePart='" + volumename + "' and Edition='" + edition + "' and NameOfBook='" + txt_name_of_the_book.Text + "'  ";
                        DataTable dt = mycode.FillData(query2);
                        if (dt.Rows.Count == 0)
                        {
                            SqlCommand cmd1;
                            string query3 = "insert into Library_Book_Entry_Master_Uniqe(BookId,Type,BookStatus,SelectClass,Subject,NameOfBook,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,EnterQuantity,Location,ISBN_Num,InvoiceNo,Price,Note,book_purchasedate,Branch_id,Book_Category_Id,Created_date,User_id,Entry_Id,Is_Delete,Book_Unique_Identifier,Sub_Location_id) values (@BookId,@Type,@BookStatus,@SelectClass,@Subject,@NameOfBook,@AuthorName,@Publication,@EnterVolumePart,@Edition,@PublicationYear,@NoOfPages,@EnterQuantity,@Location,@ISBN_Num,@InvoiceNo,@Price,@Note,@book_purchasedate,@Branch_id,@Book_Category_Id,@Created_date,@User_id,@Entry_Id,@Is_Delete,@Book_Unique_Identifier,@Sub_Location_id)";
                            cmd1 = new SqlCommand(query3);
                            cmd1.Parameters.AddWithValue("@BookId", bookid);
                            cmd1.Parameters.AddWithValue("@Type", ddl_book_type.SelectedValue);
                            cmd1.Parameters.AddWithValue("@BookStatus", ddl_book_status.SelectedValue);
                            cmd1.Parameters.AddWithValue("@SelectClass", ddl_class.SelectedValue);
                            cmd1.Parameters.AddWithValue("@Subject", "0");
                            cmd1.Parameters.AddWithValue("@NameOfBook", txt_name_of_the_book.Text);
                            cmd1.Parameters.AddWithValue("@AuthorName", txt_Author_name.Text);
                            cmd1.Parameters.AddWithValue("@Publication", txt_Publication.Text);
                            cmd1.Parameters.AddWithValue("@EnterVolumePart", volumename);
                            cmd1.Parameters.AddWithValue("@Edition", edition);
                            cmd1.Parameters.AddWithValue("@PublicationYear", publication_year);
                            cmd1.Parameters.AddWithValue("@NoOfPages", no_of_pages);
                            cmd1.Parameters.AddWithValue("@EnterQuantity", qty2);
                            cmd1.Parameters.AddWithValue("@Location", ddl_location.SelectedValue);
                            cmd1.Parameters.AddWithValue("@ISBN_Num", isbn);
                            cmd1.Parameters.AddWithValue("@InvoiceNo", invoice_No);
                            cmd1.Parameters.AddWithValue("@Price", price1.ToString("0.00"));
                            cmd1.Parameters.AddWithValue("@Note", "");
                            cmd1.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd1.Parameters.AddWithValue("@book_purchasedate", purchasedate);
                            cmd1.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                            cmd1.Parameters.AddWithValue("@Book_Category_Id", ddl_book_catogery.SelectedValue);
                            cmd1.Parameters.AddWithValue("@Created_date", My.getdate1());
                            cmd1.Parameters.AddWithValue("@Entry_Id", Enteryid);
                            cmd1.Parameters.AddWithValue("@Is_Delete", 1);
                            cmd1.Parameters.AddWithValue("@Book_Unique_Identifier", Book_Unique_Identifier);
                            cmd1.Parameters.AddWithValue("@Sub_Location_id", ddl_sublocation.SelectedValue);
                            if (My.InsertUpdateData(cmd1))
                            {

                            }
                        }
                        else
                        {


                        }

                        mycode.executequery("update Book_Entry_Temp set BookId='" + bookid + "' where Id=" + Id + " and BookId='" + ViewState["BookIdtemp"].ToString() + "'");




                    }

                }
            }
        }

        private void update_bar_code_update_time(string Book_Unique_Identifier, string isbn, double price1)
        {
            DataTable dt = mycode.FillData("Select * from Library_Book_Entry where Book_Unique_Identifier=" + Book_Unique_Identifier + "  ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd;
                    string BookId = dt.Rows[i]["BookId"].ToString();
                    string Bar_code = dt.Rows[i]["Bar_code"].ToString();
                    string NameOfBook = dt.Rows[i]["NameOfBook"].ToString();

                    string barcode_image = ly.get_barcode_img(Bar_code, isbn, price1.ToString("0.00"), BookId, NameOfBook);
                    string query = "Update Library_Book_Entry set Barcode_img=@Barcode_img where Book_Unique_Identifier=@Book_Unique_Identifier and BookId=@BookId ";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Book_Unique_Identifier", Book_Unique_Identifier);
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@Barcode_img", barcode_image);
                    if (My.InsertUpdateData(cmd))
                    {

                    }
                }
            }
        }

        private void clear_data()
        {
            finalsubmitpnl.Visible = false;
            btn_add_submit_add_book_item.Text = "Add Book Item";
            btn_Submit_final.Text = "Final Submit";
            ddl_book_type.SelectedValue = "0";
            ddl_book_status.SelectedValue = "0";
            ddl_class.SelectedValue = "0";
            ddl_location.SelectedValue = "0";
            txt_name_of_the_book.Text = "";
            txt_Author_name.Text = "";
            txt_Publication.Text = "";
            ddl_book_catogery.SelectedValue = "0";

            GrdView.DataSource = null;
            GrdView.DataBind();
            ViewState["BookIdtemp"] = Library.create_random_no_tempbookid();
        }
        #endregion

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_Book_Entry_N.aspx", false);
        }

        protected void ddl_location_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_location.SelectedItem.Text == "Select")
            {
                Alertme("Please select location", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id_na(ddl_sublocation, "Select Sub_Location,Sub_Location_id from lib_sub_location_details where Branch_Id='" + ViewState["Branch_id"].ToString() + "' and Location_id='"+ddl_location.SelectedValue+"' ");
            }
        }
    }
}