using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Monthly_Payments
{
    public partial class main : System.Web.UI.MasterPage
    {
        My imp = new My();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    lbl_Name.Text = "Student";
                    BindDetails();



                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Main Master admin");
                }
            }

        }

        private void BindDetails()
        {
            string query = "Select  * from Firm_Details";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                lbl_name13.Text = dtTemp.Rows[0]["firm_name"].ToString() + "{ Monthly Payment }";
                imglogo.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                img_avatar.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                Label1.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
                Session["firm"] = dtTemp.Rows[0]["firm_id"].ToString();
            }
        }


    }
}