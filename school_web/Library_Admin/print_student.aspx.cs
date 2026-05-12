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
    public partial class print_student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Lib_Bar_code"] != null)
                {
                    string lib_card_no = Request.QueryString["Lib_Bar_code"].ToString();
                    find_mltiple_by_book(lib_card_no.TrimEnd(','));

                }
            }
        }
        My mycode = new My();
        private void find_mltiple_by_book(string Lib_Bar_code)
        {
            string query = "Select distinct Lib_Bar_code_img from admission_registor where Lib_Bar_code in(" + Lib_Bar_code + ")";
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
            if(Session["pagelibstu"].ToString()=="1")
            {
                Response.Redirect("Print_Created_Bard_Code_For_Book.aspx", false);
            }
            else
            {

            }
           
        }
    }
}