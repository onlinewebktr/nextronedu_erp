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
    public partial class UpdateProgress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gb = new Global();
            if (!IsPostBack)
            {
                if (Request.QueryString["token"] != null)
                {
                    ViewState["token"] = Request.QueryString["token"];
                    ViewState["userid"] = Request.QueryString["userid"];
                    ViewState["filecount"] = Request.QueryString["filecount"];

                    Session["Admindov"] = ViewState["userid"].ToString();
                    SqlCommand cmd;
                    string query = "update Update_log_History set Status=@Status,Finsiah_date_time=@Finsiah_date_time where unique_id='" + ViewState["token"].ToString() + "'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Status", "Sucess");
                    cmd.Parameters.AddWithValue("@Finsiah_date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@unique_id", ViewState["token"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        try
                        { 
                            bool tureturn = gb.update_database_and_version();
                            My.exeSql("update School_System_setting set Version_count='" + ViewState["filecount"].ToString() + "'");
                            a1.Visible = false;
                        }
                        catch(Exception ex)
                        {
                            My.submitException(ex, "UpdateProgress");
                        }
                    }
                }

                Bind_log_last();

            }

        }

        private void Bind_log_last()
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter("Select top 1 *,format(Finsiah_date_time, 'dd/MM/yyyy hh:mm:ss tt') as Finsiah_date_time1 from Update_log_History where Status='Sucess' order by id desc", My.con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_update_mesage.Text = "Your school ERP has been updated. Version: " + dt.Rows[0]["Update_version"].ToString() + " Date:"+ dt.Rows[0]["Finsiah_date_time1"].ToString();
            }
                
        }
    }
}