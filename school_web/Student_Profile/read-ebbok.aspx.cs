using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class read_ebbok : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ebookid"] != null)
                {
                    ViewState["ebook_id"] = Request.QueryString["ebookid"];
                    get_pdf(); 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalProduct();", true);
                }
            }
        }

        private void get_pdf()
        {
            DataTable dt = mycode.FillData("select Path_of_ebook from EBook_Details where Book_id='" + ViewState["ebook_id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("home.aspx", false);
            }
            else
            {
                hd_pdf_path.Value = dt.Rows[0]["Path_of_ebook"].ToString();
            }
        }
    }
}