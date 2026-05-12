using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class Abacus_certificate : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    ViewState["Id"] = Request.QueryString["Id"];
                    Bind_school_data();


                }
            }
        }
        private void Bind_school_data()
        {


            string query = "Select * from Certificate_Master where Certificate_type='Abacus' and Id=" + ViewState["Id"].ToString() + "";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_participated.Text = dt.Rows[0]["Participated_in"].ToString();
                lbl_player.Text = dt.Rows[0]["Remarks"].ToString();
                lbl_date.Text = dt.Rows[0]["Issue_date"].ToString();
                string query1 = "select studentname,admissionserialnumber from admission_registor where admissionserialnumber='" + dt.Rows[0]["Admission_no"].ToString() + "' and Session_id='" + dt.Rows[0]["Session_id"].ToString() + "' and Class_id='" + dt.Rows[0]["Class_id"].ToString() + "' ";
                DataTable dt1 = My.dataTable(query1);
                if (dt1.Rows.Count == 0)
                {
                }
                else
                {

                    //lbl_year1.Text = dt1.Rows[0]["y3"].ToString() + "-" + dt1.Rows[0]["y4"].ToString();
                    //lbl_year2.Text = dt1.Rows[0]["y4"].ToString();

                    lbl_name.Text = dt1.Rows[0]["studentname"].ToString() + " (" + dt1.Rows[0]["admissionserialnumber"].ToString() + ")";


                }
            }
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Print_Abacus_Certificate.aspx", false);
        }
    }
}