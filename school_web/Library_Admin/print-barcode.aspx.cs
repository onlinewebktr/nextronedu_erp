using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin
{
    public partial class print_barcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["bookid"] != null)
                {
                    string bookid = Request.QueryString["bookid"].ToString();
                    find_mltiple_by_book(bookid.TrimEnd(','));

                }
            }
        }
        My mycode = new My();
        private void find_mltiple_by_book(string bookid)
        {
            string query = "Select Barcode_img from Library_Book_Entry where BookId in(" + bookid + ")";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Print_Created_Bard_Code_For_Book.aspx", false);
        }
    }
}