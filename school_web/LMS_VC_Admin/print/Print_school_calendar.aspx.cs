using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin.print
{
    public partial class Print_school_calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fetch_logo_and_name();
                if (Request.QueryString["date"] != null)
                {
                    ViewState["date"] = Request.QueryString["date"].ToString();
                    Bind_clander();
                }
            }


        }

        private void Bind_clander()
        {
            string query = " select  * from School_Holiday_Calendar where   Date like'%" + ViewState["date"].ToString() + "%' ";

            DataTable dt = imp.FillTable(query);
            try
            {
                if (dt.Rows.Count > 0)
                {


                    grdprint.DataSource = dt;
                    grdprint.DataBind();



                }
                else
                {

                    grdprint.DataSource = null;
                    grdprint.DataBind();


                }

            }
            catch (Exception ex)
            {
            }
        }
        UsesCode imp = new UsesCode();
        private void fetch_logo_and_name()
        {

            string query = "Select * from Firm_Details";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {

                lbl_heading.Text = dtTemp.Rows[0]["firm_name"].ToString();

                log1.Src = dtTemp.Rows[0]["logo"].ToString();
                lbl_address.Text = dtTemp.Rows[0]["address1"].ToString();
                lbl_affilation_no.Text = dtTemp.Rows[0]["Affiliation"].ToString();
                lbl_emaiid.Text = dtTemp.Rows[0]["email"].ToString();
            }
        }

        protected void btn_back_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../School_Calender_List.aspx", false);
        }
    }
}