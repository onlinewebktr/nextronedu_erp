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
    public partial class Print_Bar_Code_Issue_book : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["issuebook"] != null)
                {
                    string issuebook = Request.QueryString["issuebook"].ToString();
                    string type = Request.QueryString["type"].ToString();
                    string sudentadm = Request.QueryString["code"].ToString();
                   
                    find_mltiple_by_book(issuebook, type, sudentadm);

                }
            }
        }
        My mycode = new My();
        private void find_mltiple_by_book(string issuebook, string type,string usercode)
        {
            string query = "";
            if (type == "Student")
            {
                query = "Select Barcode_img from lib_student_transaction_details where student_id='" + usercode + "' and transaction_no='"+ issuebook + "'";
               
            }
            else
            {
                query = "Select Barcode_img from lib_teacher_trans_action_details where teacher_id='" + usercode + "' and transaction_no='" + issuebook + "'";
            }

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
            if (Session["retunpage"].ToString() == "1")
            {
                Response.Redirect("Issue_Book.aspx", false);
            }
            else
            {

            }

        }
    }
}