using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class Attendance_Timming_Setting : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {
                        txt_In_times.Text = "09:00 AM";
                        txt_outtime.Text = "05:00 PM";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_gradename, "select  grade_name,grade_id from dbo.[PRL_Grade_Master] order by grade_name");

                        Bind_grid_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Leave_Setup");
            }
        }
        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_gradename.SelectedItem.Text == "Select")
            {
                Alertme("Please select grade ", "warning");
            }
            else if (txt_In_times.Text == "")
            {
                Alertme("Please enter in time ", "warning");
            }
            else if (txt_outtime.Text == "")
            {
                Alertme("Please enter out time ", "warning");
            }
            else if (txt_gracetime.Text == "")
            {
                Alertme("Please enter out time ", "warning");
            }
            else
            {
                send_data();
                Bind_grid_data();

            }
        }

        private void Bind_grid_data()
        {
            DataTable dt = mycode.FillData("select at.id,at.Grade_id,FORMAT(at.Morning_in,'hh:mm tt') Morning_in,FORMAT(at.Morning_out,'hh:mm tt') Morning_out,FORMAT(at.Evening_in,'hh:mm tt') Evening_in,FORMAT(at.Evening_out,'hh:mm tt') Evening_out,gm.grade_name,at.Grace_time  from PRL_Attendance_Timing_Setting at join  PRL_Grade_Master gm on at.Grade_id=gm.grade_id");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        private void send_data()
        {
            try
            {
                if (btn_Submit.Text == "Add")
                {
                    SqlConnection conn = new SqlConnection(My.con);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Attendance_Timing_Setting where Grade_id='" + ddl_gradename.SelectedValue + "'", conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[1] = ddl_gradename.SelectedValue;
                        dr["Morning_in"] = txt_In_times.Text;
                        dr["Morning_out"] = txt_outtime.Text;
                        dr["iMorning_in"] = My.toDateTime(txt_In_times.Text).ToString("HHmm");
                        dr["iMorning_out"] = My.toDateTime(txt_outtime.Text).ToString("HHmm");

                        dr["Grace_time"] = txt_gracetime.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        Alertme("Attendance Timing Saved Successfully", "success");
                        ddl_gradename.SelectedValue = "0";
                        txt_gracetime.Text = "";
                        txt_In_times.Text = "";
                        txt_outtime.Text = "";
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        txt_In_times.Text = "09:00 AM";
                        txt_outtime.Text = "05:00 PM";
                    }
                    else
                    {
                        Alertme("Attendance Timing already added this grade", "warning");


                    }
                }
                else
                {

                    SqlConnection conn = new SqlConnection(My.con);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Attendance_Timing_Setting where Grade_id='" + ddl_gradename.SelectedValue + "'", conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[1] = ddl_gradename.SelectedValue;
                        dr["Morning_in"] = txt_In_times.Text;
                        dr["Morning_out"] = txt_outtime.Text;
                        dr["iMorning_in"] = My.toDateTime(txt_In_times.Text).ToString("HHmm");
                        dr["iMorning_out"] = My.toDateTime(txt_outtime.Text).ToString("HHmm");

                        dr["Grace_time"] = txt_gracetime.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        Alertme("Attendance Timing Saved Successfully", "success");
                        ddl_gradename.SelectedValue = "0";
                        txt_gracetime.Text = "";
                        txt_In_times.Text = "";
                        txt_outtime.Text = "";
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        txt_In_times.Text = "09:00 AM";
                        txt_outtime.Text = "05:00 PM";
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr[1] = ddl_gradename.SelectedValue;
                            dr["Morning_in"] = txt_In_times.Text;
                            dr["Morning_out"] = txt_outtime.Text;
                            dr["iMorning_in"] = My.toDateTime(txt_In_times.Text).ToString("HHmm");
                            dr["iMorning_out"] = My.toDateTime(txt_outtime.Text).ToString("HHmm");
                            dr["Grace_time"] = txt_gracetime.Text;
                        }
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        Alertme("Attendance Timing has been updated Successfully", "success");

                        ddl_gradename.SelectedValue = "0";
                        txt_gracetime.Text = "";
                        txt_In_times.Text = "";
                        txt_outtime.Text = "";
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        txt_In_times.Text = "09:00 AM";
                        txt_outtime.Text = "05:00 PM";
                    }



                }

            }
            catch
            {

            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ddl_gradename.SelectedValue = "0";
            txt_gracetime.Text = "";
            txt_In_times.Text = "";
            txt_outtime.Text = "";
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_In_times.Text = "09:00 AM";
            txt_outtime.Text = "05:00 PM";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");

            DataTable dt = mycode.FillData("select  *,FORMAT(Morning_in,'hh:mm tt') Morning_in1,FORMAT(Morning_out,'hh:mm tt') Morning_out1  from dbo.[PRL_Attendance_Timing_Setting] where Id=" + lbl_Id.Text + "");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                ddl_gradename.SelectedValue = dt.Rows[0]["Grade_id"].ToString();
                txt_In_times.Text = dt.Rows[0]["Morning_in1"].ToString();
                txt_outtime.Text = dt.Rows[0]["Morning_out1"].ToString();
                txt_gracetime.Text = dt.Rows[0]["Grace_time"].ToString();
                btn_Submit.Text = "Update";
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");


                My.exeSql("delete from PRL_Attendance_Timing_Setting where Id='" + lbl_Id.Text + "'");
                Alertme("Attendance Timing deleted Successfully", "success");
                Bind_grid_data();
            }
            catch
            {
            }
        }
    }
}