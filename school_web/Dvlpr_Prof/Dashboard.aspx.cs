using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                fetch_data();
            }
        }

        private void fetch_data()
        {
            int lastupdate = get_last_update();
            Bind_data_update(lastupdate);

        }

        private void Bind_data_update(int lastupdate)
        {
            string query = "Select * from Publish_File_Version where Version_count>" + lastupdate + " order by Version_count asc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, compLN.comp);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                updaate.Visible = false; 
            }
            else
            { 
                updaate.Visible = true; 
            }
        }
        My mycode = new My();
        private int get_last_update()
        {
            string query = "Select * from School_System_setting   ";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return My.toint(dt.Rows[0]["Version_count"].ToString());
            } 
        }
    }
}