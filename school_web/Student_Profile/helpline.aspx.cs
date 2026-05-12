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
    public partial class helpline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsesCode mycode = new UsesCode();
            if (!IsPostBack)
            {
                img1.Visible = true;
                DataTable dt = mycode.FillTable("select * from dbo.[Firm_Details] ");
                if (dt.Rows.Count != 0)
                {
                    lbl_abotschool.Text = dt.Rows[0]["helpline_no"].ToString();
                    

                    img1.Src = "helpline.png";

                }
                else
                {

                    img1.Src = "helpline.png";
                }
            }

        }
    }
}