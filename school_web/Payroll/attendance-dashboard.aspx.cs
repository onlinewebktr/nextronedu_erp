using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class attendance_dashboard : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txt_att_date.Text = mycode.date();
                    txt_Dep_date.Text = mycode.date();
                    txt_grade_date.Text = mycode.date();
                    hd_date.Value = txt_att_date.Text;
                    hd_dep_date.Value = txt_Dep_date.Text;
                    hd_grade_date.Value = txt_grade_date.Text;
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "DefaultPage");
                }
            }
        }

        protected void txt_att_date_TextChanged(object sender, EventArgs e)
        {
            hd_date.Value = txt_att_date.Text;
        }

        protected void txt_Dep_date_TextChanged(object sender, EventArgs e)
        {
            hd_dep_date.Value = txt_Dep_date.Text;
        }

        protected void txt_grade_date_TextChanged(object sender, EventArgs e)
        {
            hd_grade_date.Value = txt_grade_date.Text;
        }
    }
}