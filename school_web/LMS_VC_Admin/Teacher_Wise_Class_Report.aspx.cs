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
    public partial class Teacher_Wise_Class_Report : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    code.bind_all_ddl_with_all(ddl_teacher, "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc");
                    DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    string date = dtm.ToString("dd/MM/yyyy");
                    string day = date.Substring(0, 2);
                    string month = date.Substring(3, 2);
                    string year = date.Substring(6, 4);
                    ddl_year.Text = year;
                    ddl_date.Text = day;
                    ddl_month.Text = dtm.ToString("MMM");
                    rd_day.Checked = true;
                    rd_month.Checked = false;
                    rd_year.Checked = false;
                    date1.Visible = true;
                    month1.Visible = true;
                    year1.Visible = true;
                    find_data();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            find_data();
        }
        private void find_data()
        {
            try
            {

                if (rd_day.Checked == false && rd_month.Checked == false && rd_year.Checked == false)
                {
                    Alert("Please select day, month or year");
                    pnl_view.Visible = false;
                    RpDetailsStudent.DataSource = null;
                    RpDetailsStudent.DataBind();

                }
                else
                {
                    string date = ddl_year.Text;
                    if (rd_day.Checked == true)
                    {
                        date = ddl_date.Text + "-" + ddl_month.Text + "-" + ddl_year.Text;
                        lbl_month_year.Text = ddl_date.Text + "-" + ddl_month.Text + "-" + ddl_year.Text;
                    }
                    else if (rd_month.Checked == true)
                    {
                        date = ddl_month.Text + "-" + ddl_year.Text;
                        lbl_month_year.Text = ddl_month.Text + "-" + ddl_year.Text;
                    }
                    else if (rd_year.Checked == true)
                    {
                        date = ddl_year.Text;
                        lbl_month_year.Text = ddl_year.Text;
                    }
                    SqlCommand cmd = new SqlCommand();
                    if (ddl_teacher.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@cmdstatus", "1");
                    }
                    else 
                    {
                        cmd.Parameters.AddWithValue("@cmdstatus", "2");
                        cmd.Parameters.AddWithValue("@Teacher_Id", ddl_teacher.SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@Date", date);

                    cmd.CommandText = "sp_VC_class_report";
                    DataTable dt = UsesCode.Getdata_sp(cmd);
                    if (Convert.ToString(dt.Rows.Count) == "0")
                    {
                        lbl_totalfranchise.Text = "0";
                        Alert("Data Not Available");

                        RpDetailsStudent.DataSource = null;
                        RpDetailsStudent.DataBind();

                        pnl_view.Visible = false;

                    }
                    else
                    {
                        lbl_totalfranchise.Text = dt.Rows.Count.ToString();
                        pnl_view.Visible = true;


                        RpDetailsStudent.DataSource = dt;
                        RpDetailsStudent.DataBind();
                    }





                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void rd_day_CheckedChanged(object sender, EventArgs e)
        {
            rd_day.Checked = true;
            rd_month.Checked = false;
            rd_year.Checked = false;
            date1.Visible = true;
            month1.Visible = true;
            year1.Visible = true;

            pnl_view.Visible = false;

            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            find_data();
        }

        protected void rd_month_CheckedChanged(object sender, EventArgs e)
        {
            rd_day.Checked = false;
            rd_month.Checked = true;
            rd_year.Checked = false;

            date1.Visible = false;
            month1.Visible = true;
            year1.Visible = true;

            pnl_view.Visible = false;

            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            find_data();

        }

        protected void rd_year_CheckedChanged(object sender, EventArgs e)
        {
            date1.Visible = false;
            month1.Visible = false;
            year1.Visible = true;

            pnl_view.Visible = false;

            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            find_data();
        }
    }
}