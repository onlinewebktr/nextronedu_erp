using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Financial_Report_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_startdate.Text = mycode.date();
                txt_start_date_cl.Text = mycode.date();
                txt_start_date_su.Text = mycode.date();
                txt_enddate.Text = mycode.date();
                txt_end_date_cl.Text = mycode.date();
                txt_end_date_su.Text = mycode.date();

                mycode.bind_all_ddl_with_all(ddl_class, "Select CategoryName, CategoryID from ClassMaster where Istatus='1' order by Position");
                mycode.bind_ddl_all1(dd_section, "Select distinct section  from Course_or_Subject_Master  where Istatus='1'  order by section");
                search_data();
            }
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                mycode.bind_ddl_all1(dd_section, "Select distinct section  from Course_or_Subject_Master  where Istatus='1'  order by section");
                dd_section.Text = "ALL";
            }
            else
            {
                mycode.bind_ddl_all1(dd_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_class.SelectedValue + "' and Istatus='1'  order by section");
            }

        }
        #region radio batn selection wise
        protected void rd_Twodate_wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = true;
            pnl_class_and_section_wise.Visible = false;
            pnl_admission_report_wise.Visible = false;
            pnl_transaction_wise.Visible = false;
            pnl_Success_Failure.Visible = false;
            lbl_total.Text = "0";
        
            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            pnl_view.Visible = false;

        }

        protected void rd_class_and_section_wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = false;
            pnl_class_and_section_wise.Visible = true;
            pnl_admission_report_wise.Visible = false;
            pnl_transaction_wise.Visible = false;
            pnl_Success_Failure.Visible = false;
            lbl_total.Text = "0";
            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            pnl_view.Visible = false;

        }

        protected void rd_Admission_no_wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = false;
            pnl_class_and_section_wise.Visible = false;
            pnl_admission_report_wise.Visible =  true;
            pnl_transaction_wise.Visible = false;
            pnl_Success_Failure.Visible = false;
            lbl_total.Text = "0";
            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            pnl_view.Visible = false;
        }

        protected void rd_Transaction_Id_Wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = false;
            pnl_class_and_section_wise.Visible = false;
            pnl_admission_report_wise.Visible = false;
            pnl_transaction_wise.Visible = true;
            pnl_Success_Failure.Visible = false;
            lbl_total.Text = "0";
            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            pnl_view.Visible = false;

        }

        protected void rd_Success_and_failure_wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = false;
            pnl_class_and_section_wise.Visible = false;
            pnl_admission_report_wise.Visible = false;
            pnl_transaction_wise.Visible = false;
            pnl_Success_Failure.Visible = true;
            lbl_total.Text = "0";
            RpDetailsStudent.DataSource = null;
            RpDetailsStudent.DataBind();
            pnl_view.Visible = false;

        }
        #endregion

        protected void btn_find_Click(object sender, EventArgs e)
        {

            search_data();
        }
        UsesCode mycode = new UsesCode();
        double total_amount = 0;
        private void search_data()
        {
            string status = "ok";
            SqlCommand cmd = new SqlCommand();
            if (rd_Twodate_wise.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                {
                    lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                    cmd.Parameters.AddWithValue("@cmdstatus", "1");
                    cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_startdate.Text));
                    cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_enddate.Text));
                    status = "ok";
                }
                else
                {
                    Alert("Please select valid date");
                }
            }
            else if (rd_class_and_section_wise.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_start_date_cl.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_end_date_cl.Text)))
                {
                    if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text == "ALL")
                    {
                        lbl_month_year.Text = " Class- " + ddl_class.SelectedItem.Text + " Section- " + dd_section.Text + " Start Date - " + txt_startdate.Text + "-" + "End Date - " + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "1");
                        cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_start_date_cl.Text));
                        cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_end_date_cl.Text));
                    }

                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text == "ALL")
                    {
                        lbl_month_year.Text = " Class- " + ddl_class.SelectedItem.Text + " Section- " + dd_section.Text + " Start Date - " + txt_startdate.Text + "-" + "End Date - " + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "2");
                        cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_start_date_cl.Text));
                        cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_end_date_cl.Text));
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        status = "ok";
                    }

                    else if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text != "ALL")
                    {
                        lbl_month_year.Text = " Class- " + ddl_class.SelectedItem.Text + " Section- " + dd_section.Text + " Start Date - " + txt_startdate.Text + "-" + "End Date - " + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "3");
                        cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_start_date_cl.Text));
                        cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_end_date_cl.Text));
                        cmd.Parameters.AddWithValue("@Section", dd_section.Text);
                        status = "ok";
                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text != "ALL")
                    {
                        lbl_month_year.Text = " Class- " + ddl_class.SelectedItem.Text + " Section- " + dd_section.Text + " Start Date - " + txt_start_date_cl.Text + "-" + "End Date - " + txt_end_date_cl.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "4");
                        cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_start_date_cl.Text));
                        cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_end_date_cl.Text));
                        cmd.Parameters.AddWithValue("@Section", dd_section.Text);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        status = "ok";
                    }

                }
                else
                {
                    Alert("Please select valid date");
                }


            }
            else if (rd_Admission_no_wise.Checked == true)
            {
                lbl_month_year.Text = " Admission No.- " + txt_admissiono.Text;
                cmd.Parameters.AddWithValue("@cmdstatus", "5");
                cmd.Parameters.AddWithValue("@admissionserialnumber", txt_admissiono.Text);
                status = "ok";
            }
            else if (rd_Transaction_Id_Wise.Checked == true)
            {

                lbl_month_year.Text = "Reference Id - " + txt_transaction_id_wise.Text;
                cmd.Parameters.AddWithValue("@OrderTrackingID", txt_transaction_id_wise.Text);
                cmd.Parameters.AddWithValue("@cmdstatus", "6");
             
                status = "ok";
            }
            else if (rd_Success_and_failure_wise.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_start_date_su.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_end_date_su.Text)))
                {
                    lbl_month_year.Text = " Status- " + ddl_status.SelectedItem.Text + " Start Date - " + txt_start_date_su.Text + "-" + "End Date - " + txt_end_date_su.Text;
                    cmd.Parameters.AddWithValue("@cmdstatus", "7");
                    cmd.Parameters.AddWithValue("@startdate", mycode.ConvertStringToiDate(txt_start_date_su.Text));
                    cmd.Parameters.AddWithValue("@enddate", mycode.ConvertStringToiDate(txt_end_date_su.Text));
                    cmd.Parameters.AddWithValue("@TransactionStatus", ddl_status.SelectedValue);
                    status = "ok";
                }
                else
                {
                    Alert("Please select valid date");
                }
            }
            if (status == "ok")
            {


                cmd.CommandText = "sp_OnlineTransaction_report";
                DataTable dt = UsesCode.Getdata_sp(cmd);
                if (Convert.ToString(dt.Rows.Count) == "0")
                {
                    lbl_total.Text = "0";
                    Alert("Data Not Available");

                    RpDetailsStudent.DataSource = null;
                    RpDetailsStudent.DataBind();

                    pnl_view.Visible = false;

                }
                else
                {
                    double totalamount = 0;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    pnl_view.Visible = true;
                    RpDetailsStudent.DataSource = dt;
                    RpDetailsStudent.DataBind();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["TransactionStatus"].ToString() == "success")
                        {
                              totalamount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                              total_amount = total_amount + totalamount;
                        }

                       
                     
                    }

                    lbl_total_amount.Text = total_amount.ToString("0.00");
                    



                }

            }




        }

        private void Alert(string p)
        { 
            lbl_msg.Text = p;
             string  scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void RpDetailsStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("lbl_TransactionStatus")).Text == "success")
                {

                    ((Label)e.Item.FindControl("lbl_status")).Text = "Success";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-success ml-2";


                   
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_status")).Text = "Failure";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-danger ml-2";
                }

            }
        }




    }
}