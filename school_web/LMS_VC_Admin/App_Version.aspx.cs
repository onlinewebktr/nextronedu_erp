using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class App_Version : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGridView();
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void BindGridView()
        {
            DataTable dt = code.FillTable("select * from App_Version_Details order by Id asc");
            try
            {
                if (dt.Rows.Count == 0)
                {
                    hd_id1.Value = "0";
                    btn_submit.Text = "Add";
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();

                }
                else
                {
                  
                    hd_id1.Value = dt.Rows[0]["Id"].ToString();
                    btn_submit.Text = "Update";
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }

            }
            catch (Exception ex)
            {
            }

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (txt_appversion.Text == "")
            {
                lbl_msg.Text = "Please enter app version";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (txt_versio_detals.Text == "")
            {
                lbl_msg.Text = "Please enter app version details";
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {
                if (btn_submit.Text == "Add")
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO App_Version_Details (App_Version,App_Version_Details,Date,Idate) values (@App_Version,@App_Version_Details,@Date,@Idate)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@App_Version",txt_appversion.Text);
                    cmd.Parameters.AddWithValue("@App_Version_Details", txt_versio_detals.Text);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@Idate", code.idate());
                  
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        lbl_msg.Text = "App version has been added";
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                        txt_appversion.Text = "";
                        txt_versio_detals.Text = "";
                        BindGridView();
                    }
                }
                else
                {


                    SqlCommand cmd;
                    string query = "Update App_Version_Details set App_Version=@App_Version,App_Version_Details=@App_Version_Details,Date=@Date,Idate=@Idate where Id = @Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@App_Version", txt_appversion.Text);
                    cmd.Parameters.AddWithValue("@App_Version_Details", txt_versio_detals.Text);
                    cmd.Parameters.AddWithValue("@Date", code.date());
                    cmd.Parameters.AddWithValue("@Idate", code.idate());
                    cmd.Parameters.AddWithValue("@Id", hd_id1.Value);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        lbl_msg.Text = "App version has been updated";
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                        txt_appversion.Text = "";
                        txt_versio_detals.Text = "";
                        BindGridView();
                    }



                }

            }


        }



    }
}