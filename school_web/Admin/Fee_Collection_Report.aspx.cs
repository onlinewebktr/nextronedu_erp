using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Fee_Collection_Report : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();


                   
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }


                        ViewState["flag"] = "0";

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by session_id asc");
                        ddlsession.Text = My.get_session_id();

                        mycode.bind_all_ddl_with_id_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();

                        ddlsession.SelectedValue = My.get_session_id();
                        hd_session.Value = ddlsession.SelectedItem.Text;
                        get_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Collection_Report");
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                get_data();
            }
        }

        private void get_data()
        {
            lbl_print_head.Text = "Fee Collection Summary Report of Session : " + ddlsession.SelectedItem.Text + " and Class : " + ddlclass.SelectedItem.Text;
            lbl_print_date.Text = "(Print on : " + mycode.date() + ")";
            //----------------------------total fee colection----------------------
            SqlCommand cmd = new SqlCommand();
            if (ddlclass.SelectedItem.Text == "All")
            {
                cmd.Parameters.AddWithValue("@typestatus", "2");
            }
            else
            {
                cmd.Parameters.AddWithValue("@typestatus", "1");
            }
            cmd.Parameters.AddWithValue("@feetpe", "1");//totalpyment
            cmd.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@classid", ddlclass.SelectedValue);
            cmd.CommandText = "sp_Fee_Collection_Report";
            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (Convert.ToString(dt.Rows.Count) == "0")
            {
                tot_collection1.Text = "0.00";
            }
            else
            {
                tot_collection1.Text = My.toDouble(dt.Rows[0][0].ToString()).ToString("0.00");
            }

            //--------------------totaldues--------------------
            SqlCommand cmd1 = new SqlCommand();
            if (ddlclass.SelectedItem.Text == "All")
            {
                cmd1.Parameters.AddWithValue("@typestatus", "2");
            }
            else
            {
                cmd1.Parameters.AddWithValue("@typestatus", "1");
            }
            cmd1.Parameters.AddWithValue("@feetpe", "0");//totaldues
            cmd1.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
            cmd1.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
            cmd1.Parameters.AddWithValue("@classid", ddlclass.SelectedValue);
            cmd1.CommandText = "sp_Fee_Collection_Report";
            DataTable dt1 = UsesCode.Getdata_sp(cmd1);
            if (dt1.Rows.Count == 0)
            {
                lbl_total_dues1.Text = "0.00";
            }
            else
            {
                lbl_total_dues1.Text = My.toDouble(dt1.Rows[0][0].ToString()).ToString("0.00");
            }

            //--------------------Total Admission Fee--------------------
            SqlCommand cmd2 = new SqlCommand();
            if (ddlclass.SelectedItem.Text == "All")
            {
                cmd2.Parameters.AddWithValue("@typestatus", "2");
            }
            else
            {
                cmd2.Parameters.AddWithValue("@typestatus", "1");
            }
            cmd2.Parameters.AddWithValue("@feetpe", "2");//totaladmission fee colection
            cmd2.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
            cmd2.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
            cmd2.Parameters.AddWithValue("@classid", ddlclass.SelectedValue);
            cmd2.CommandText = "sp_Fee_Collection_Report";
            DataTable dt2 = UsesCode.Getdata_sp(cmd2);
            if (dt2.Rows.Count == 0)
            {
                lbl_totaladmissionfee1.Text = "0.00";
            }
            else
            {
                lbl_totaladmissionfee1.Text = My.toDouble(dt2.Rows[0][0].ToString()).ToString("0.00");
            }
            //--------------------------Total readmission fee-------------------

            //--------------------Total Admission Fee--------------------
            SqlCommand cmd3 = new SqlCommand();
            if (ddlclass.SelectedItem.Text == "All")
            {
                cmd3.Parameters.AddWithValue("@typestatus", "2");
            }
            else
            {
                cmd3.Parameters.AddWithValue("@typestatus", "1");
            }
            cmd3.Parameters.AddWithValue("@feetpe", "3");//totalreadmission fee colection
            cmd3.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
            cmd3.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
            cmd3.Parameters.AddWithValue("@classid", ddlclass.SelectedValue);
            cmd3.CommandText = "sp_Fee_Collection_Report";
            DataTable dt3 = UsesCode.Getdata_sp(cmd3);
            if (dt3.Rows.Count == 0)
            {
                lbl_readmissionfeecolection1.Text = "0.00";
            }
            else
            {
                lbl_readmissionfeecolection1.Text = My.toDouble(dt3.Rows[0][0].ToString()).ToString("0.00");
            }


            //--------------------Total Month Fee--------------------
            SqlCommand cmd4 = new SqlCommand();
            if (ddlclass.SelectedItem.Text == "All")
            {
                cmd4.Parameters.AddWithValue("@typestatus", "2");
            }
            else
            {
                cmd4.Parameters.AddWithValue("@typestatus", "1");
            }
            cmd4.Parameters.AddWithValue("@feetpe", "15");//totalreadmission fee colection
            cmd4.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
            cmd4.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
            cmd4.Parameters.AddWithValue("@classid", ddlclass.SelectedValue);
            cmd4.CommandText = "sp_Fee_Collection_Report";
            DataTable dt4 = UsesCode.Getdata_sp(cmd4);
            if (dt4.Rows.Count == 0)
            {
                lbl_total_feemonth.Text = "0.00";
            }
            else
            {
                lbl_total_feemonth.Text = My.toDouble(dt4.Rows[0][0].ToString()).ToString("0.00");
            }




            fee_head_wise_data();

        }

        private void fee_head_wise_data()
        {
            SqlCommand cmd3 = new SqlCommand();
            if (ddlclass.SelectedItem.Text == "All")
            {
                hd_session.Value = ddlsession.SelectedItem.Text;
                hd_course.Value = "0";
                cmd3.Parameters.AddWithValue("@typestatus", "2");
            }
            else
            {
                hd_session.Value = ddlsession.SelectedItem.Text;
                hd_course.Value = ddlclass.SelectedValue;
                cmd3.Parameters.AddWithValue("@typestatus", "1");
            }
            cmd3.Parameters.AddWithValue("@feetpe", "4");//totalreadmission fee colection
            cmd3.Parameters.AddWithValue("@session_id", ddlsession.SelectedValue);
            cmd3.Parameters.AddWithValue("@session", ddlsession.SelectedItem.Text);
            cmd3.Parameters.AddWithValue("@classid", ddlclass.SelectedValue);
            cmd3.CommandText = "sp_Fee_Collection_Report";
            DataTable dt3 = UsesCode.Getdata_sp(cmd3);
            if (dt3.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                grd_fee.DataSource = dt3;
                grd_fee.DataBind();
            }
        }
        double totalamount = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    totalamount = totalamount + Convert.ToDouble(lbl_payable.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totalamount");

                lbl_totaldiscount.Text = totalamount.ToString("0.00");

            }
        }


        protected string Getamount_comma_seperated(string amount)
        {
            try
            {
                string amt = String.Format("{0:n}", Convert.ToDouble(amount));
                return amt;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        protected void lnk_click_here_toview_total_collection_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Total_Collection.aspx?sessionid=" + ddlsession.SelectedValue + "&classid=" + ddlclass.SelectedValue, false);
        }

        protected void lnk_click_here_toview_total_admissionfee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Total_Admission_Fee_Collection.aspx?sessionid=" + ddlsession.SelectedValue + "&classid=" + ddlclass.SelectedValue, false);
        }

        protected void lnk_click_here_toview_total_readmissionadmissionfee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Total_Re_Admission_Fee_Collection.aspx?sessionid=" + ddlsession.SelectedValue + "&classid=" + ddlclass.SelectedValue, false);
        }

        protected void lnk_click_here_toview_total_dues_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Total_Dues_Fee.aspx?sessionid=" + ddlsession.SelectedValue + "&classid=" + ddlclass.SelectedValue, false);
        }

        protected void lnk_click_here_toview_total_headwise_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Total_Head_Wise_Collection.aspx?sessionid=" + ddlsession.SelectedValue + "&classid=" + ddlclass.SelectedValue, false);
        }

        protected void lnk_total_monyth_fee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Total_Monthly_Fee_Collection.aspx?sessionid=" + ddlsession.SelectedValue + "&classid=" + ddlclass.SelectedValue, false);
        } 
    }
}