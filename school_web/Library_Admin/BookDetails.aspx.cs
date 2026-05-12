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
    public partial class BookDetails : System.Web.UI.Page
    {

        My mycode = new My();
        string booktype = "Select top 1 TypeName from Library_Type where TypeId=lbe.Type and Branch_Id=lbe.Branch_id";
        string BookStatus = "Select top 1 BookStatus from Library_Book_Status where BookStatusId=lbe.BookStatus  ";
        string Book_Category = "Select top 1 Book_Category from Library_Book_Category where Book_Category_Id=lbe.Book_Category_Id and Branch_Id=lbe.Branch_id";

        string location = "Select top 1 location from lib_location_details where Location_id=lbe.Location and Branch_Id=lbe.Branch_id";


        string Sub_Location = "Select top 1 Sub_Location from lib_sub_location_details where Sub_Location_id=lbe.Sub_Location_id and Branch_Id=lbe.Branch_id";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["BookId"] != null)
                    {
                        Bind_schoolinfo();
                        string BookId = Request.QueryString["BookId"];

                        fetch_data(BookId);

                    }
                    else
                    {
                        Response.Redirect("Lib_Home.aspx", false);
                    }
                }
            }
            catch (Exception exe)
            {
            }
        }

        private void Bind_schoolinfo()
        {

            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_affiliation_no.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                if (dt.Rows[0]["Is_mobile_no_show_in_bill"].ToString() == "True")
                {
                    contact_no.Visible = true;

                }
            }
        }

        private void fetch_data(string bookId)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * ,(" + booktype + ") as booktype_new,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,("+ Sub_Location + ") as Sub_Location  from Library_Book_Entry_Master_Uniqe lbe where BookId='" + bookId + "' ", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Library_Book_Entry_Master_Uniq");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_sublocation.Text= dt.Rows[0]["Sub_Location"].ToString(); 
                lbl_booktype.Text = dt.Rows[0]["booktype_new"].ToString();
                lbl_book_catogery.Text = dt.Rows[0]["Book_Category"].ToString();
                lbl_bookstatus.Text = dt.Rows[0]["Book_Status"].ToString();
                lbl_Location.Text = dt.Rows[0]["location_new"].ToString();
                lbl_class.Text = get_class_name(dt.Rows[0]["SelectClass"].ToString());
                lbl_nameofthebook.Text = dt.Rows[0]["NameOfBook"].ToString();
                lbl_AuthorName.Text = dt.Rows[0]["AuthorName"].ToString();
                lbl_Publication.Text = dt.Rows[0]["Publication"].ToString();
                lbl_EnterVolumePart.Text = dt.Rows[0]["EnterVolumePart"].ToString();




                lbl_Edition.Text = dt.Rows[0]["Edition"].ToString();
                lbl_PublicationYear.Text = dt.Rows[0]["PublicationYear"].ToString();
                lbl_NoOfPages.Text = dt.Rows[0]["NoOfPages"].ToString();
                lbl_ISBN_Num.Text = dt.Rows[0]["ISBN_Num"].ToString();
                lbl_invoice.Text = dt.Rows[0]["InvoiceNo"].ToString();
                double price = My.toDouble(dt.Rows[0]["Price"].ToString());
                lbl_price.Text = price.ToString("0.00");
            }
        }

        private string get_class_name(string classid)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select Course_Name   from Add_course_table where course_id='" + classid + "' ", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "NA";

            }
            else
            {
                return dt.Rows[0]["Course_Name"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["back"].ToString() == "0")
                {
                    Response.Redirect("Edit_Book.aspx", false);
                }
            }
            catch
            {

            }
        }

        protected void print1_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}