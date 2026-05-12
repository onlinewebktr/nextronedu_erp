using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Check_Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admindov"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                     
                       ViewState["Userid"] = Session["Admindov"].ToString();
                    ViewState["branchid"] = "1";
                    int lastupdate = get_last_update();
                    Bind_data_update(lastupdate);



                }
            }

        }

        private int get_last_update()
        {

            ViewState["Previous_Version"] = "";
            string query = "Select * from School_System_setting   ";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                lbl_updateed_version.Text = "";
                ViewState["Previous_Version"] = "1.0.0.0";
                return 0;
            }
            else
            {
                ViewState["Previous_Version"] = dt.Rows[0]["session_value"].ToString();
                lbl_updateed_version.Text = "Current Version: " + dt.Rows[0]["session_value"].ToString();
                return My.toint(dt.Rows[0]["Version_count"].ToString());
            }

        }

        public void Alert(string Message)
        {
            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void Bind_data_update(int lastupdate)
        {

            pnl_payment_history.Visible = false;
            string query = "Select * from Publish_File_Version where Version_count>" + lastupdate + " and Status='Active' order by Version_count asc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, compLN.comp);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {

                pnl_payment_history.Visible = false;
                Alert("No update currently in progress.");
               // lbl_msg.Text = "No update currently in progress.";
                grd_fee.DataSource = null;
                grd_fee.DataBind();
                get_last_update_log();
            }
            else
            {
                pnl_payment_history.Visible = true;
                grd_fee.DataSource = dt;
                grd_fee.DataBind();


            }
        }

        private void get_last_update_log()
        {
            pnl_payment_history.Visible = false;
            string query = "Select top 1 *,format(Finsiah_date_time, 'dd/MM/yyyy hh:mm:ss tt') as Finsiah_date_time1 from Update_log_History where Status='Sucess' order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                GridView1_update.DataSource = null;
                GridView1_update.DataBind();
            }
            else
            {
                pnl_payment_history.Visible = true;
                GridView1_update.DataSource = dt;
                GridView1_update.DataBind();


            }
        }

        My mycode = new My();
        protected void btn_update_data_Click(object sender, EventArgs e)
        {

            try
            {
                 
                //string updateurl = ConfigurationManager.AppSettings["updateurl"];
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Version_count = (Label)row.FindControl("lbl_Version_count");
                Label lbl_Update_Time = (Label)row.FindControl("lbl_Update_Time");
                Label lbl_duration = (Label)row.FindControl("lbl_duration");
                Label lbl_Version_name = (Label)row.FindControl("lbl_Version_name");
                Label lbl_Update_Note = (Label)row.FindControl("lbl_Update_Note");

                string mergeStartTime = mycode.date() + " " + lbl_Update_Time.Text;
                DateTime startDateTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                DateTime nextendtime = startDateTime.AddHours(My.toint(lbl_duration.Text));

                string enddate = nextendtime.ToString("dd/MM/yyyy hh:mm:ss tt");
                DateTime Enddatetime = DateTime.ParseExact(enddate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);



                DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string Datetimesystem = dtm.ToString("dd/MM/yyyy hh:mm:ss tt");

                DateTime Datetimesystemitime = DateTime.ParseExact(Datetimesystem, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                Int64 Datetimesystemitime1 = Convert.ToInt64(Datetimesystemitime.ToString("yyyyMMddHHmmss"));

                Int64 End_Exam_Idatetime = Convert.ToInt64(Enddatetime.ToString("yyyyMMddHHmmss"));
                if (End_Exam_Idatetime >= Datetimesystemitime1)
                {
                    if (Datetimesystemitime1 < End_Exam_Idatetime)
                    {
                        string unique_id = My.create_random_no_otp();
                      
                        SqlCommand cmd;
                        string query = "INSERT INTO Update_log_History (Update_version,filecount,Status,Update_note,unique_id,Previous_Version,Update_By,Update_date_time) values (@Update_version,@filecount,@Status,@Update_note,@unique_id,@Previous_Version,@Update_By,@Update_date_time)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Update_version", lbl_Version_name.Text);
                        cmd.Parameters.AddWithValue("@filecount", lbl_Version_count.Text);
                        cmd.Parameters.AddWithValue("@Status", "Start");
                        cmd.Parameters.AddWithValue("@Update_note", lbl_Update_Note.Text);
                        cmd.Parameters.AddWithValue("@unique_id", unique_id);
                        cmd.Parameters.AddWithValue("@Previous_Version", ViewState["Previous_Version"].ToString());
                        cmd.Parameters.AddWithValue("@Update_By", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Update_date_time", My.getdate1());
                        if (My.InsertUpdateData(cmd))
                        {
                            string url = My.URL() + "Update_file_automatic.aspx?schoolcode=" + My.get_firm_id() + "&filecount=" + lbl_Version_count.Text + "&userid=" + ViewState["Userid"].ToString() + "&token=" + unique_id;
                             Response.Redirect(url, false);

                        }



                    }
                    else
                    {
                        Alert("You are not action the update a specific time. You can only click the update specific time.");
                    }
                }
                else
                {

                    Alert("You are not action the update a specific time. You can only click the update specific time.");
                }





            }
            catch(Exception ex)
            {
                My.submitException(ex, "check for update button click");

            }



        }
        int count = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn_update_data = (Button)e.Row.FindControl("btn_update_data");
                if (count == 0)
                {
                    btn_update_data.Visible = true;
                }
                count = count + 1;

                Label lbl_Update_Time = (Label)e.Row.FindControl("lbl_Update_Time");
                Label lbl_duration = (Label)e.Row.FindControl("lbl_duration");
                Label lbl_updateendtime = (Label)e.Row.FindControl("lbl_updateendtime");

                string mergeStartTime = mycode.date() + " " + lbl_Update_Time.Text;
                DateTime startDateTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                DateTime nextendtime = startDateTime.AddHours(My.toint(lbl_duration.Text));

                string enddate = nextendtime.ToString("hh:mm:ss tt");
                lbl_updateendtime.Text = enddate;
                // DateTime Enddatetime = DateTime.ParseExact(enddate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            }
        }

        protected void lnk_view_Update_Note_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_Update_Note = (Label)row.FindControl("lbl_Update_Note");
            lbl_data.Text = lbl_Update_Note.Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void lnk_view_Update_Note1_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_Update_Note = (Label)row.FindControl("lbl_Update_Note");
            lbl_data.Text = lbl_Update_Note.Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}