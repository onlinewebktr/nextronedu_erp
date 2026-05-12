using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.About_School
{
    public partial class School_History : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 DataTable dt = mycode.FillTable("select * from dbo.[Content_about_us_School] ");
                 if (dt.Rows.Count != 0)
                 {
                     lbl_abotschool.Text = dt.Rows[0]["Content_first_section"].ToString();
                     img1.Src = dt.Rows[0]["ImagePath"].ToString();
                     if (dt.Rows[0]["ImagePath"].ToString() == "")
                     {
                         img1.Visible = false;
                     }
                     else
                     {
                         img1.Visible = true;
                         img1.Src = dt.Rows[0]["ImagePath"].ToString();
                     }
                 }
                 else
                 {
                     img1.Visible = true;
                     img1.Src = "No-data-found-banner.png";
                 }
            }
        }
    }
}