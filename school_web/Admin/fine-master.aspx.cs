using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class fine_master : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        rd_day.Checked = true;
                        ViewState["session"] = My.get_session();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        check_fine_added_and_fee_taken();
                        check_rd_btn();
                        mycode.bind_all_ddl_with_id(ddl_applicable_from_month, "select Month,Month_Id from Month_Index order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_quarter, "select Quater_no,Quater_id from Quater_master where Session_id='" + My.get_session_id() + "' order by Quater_no asc");
                        check_qtr_pay_mode();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fees_Master");
            }
        }


        private void bind_grd_view()
        {
            DataTable dtx = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dtx.Rows.Count == 0)
            {
                DataTable dt = mycode.FillData("select *,(select top 1 Quater_no from Quater_master where Quater_id=Fine_master.Quater_id) as Quater_no,(select top 1 Month from Month_Index where Month_Id=Fine_master.Q_start_month) as Start_month,(select top 1 Month from Month_Index where Month_Id=Fine_master.Q_end_month) as End_month from Fine_master where Session_id='" + My.get_session_id() + "' and Fine_type='QuarterWise' order by Q_start_month asc");
                if (dt.Rows.Count == 0)
                {
                    find_from_day_month();
                }
                else
                {
                    pnl_qtr_grid.Visible = true;
                    pnl_day_mnth.Visible = false;
                    pnl_day_range.Visible = false;
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            else
            {
                DataTable dt = mycode.FillData("select * from dbo.[Fine_master_day_range] order by No_of_day asc");
                if (dt.Rows.Count == 0)
                {
                    pnl_day_range.Visible = true;
                    pnl_qtr_grid.Visible = false;
                    pnl_day_mnth.Visible = false;
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                    string qrys = "update globle_data set Fine_mode='1'";
                    My.exeSql(qrys); 
                }
                else
                {
                    pnl_day_range.Visible = true;
                    pnl_qtr_grid.Visible = false;
                    pnl_day_mnth.Visible = false;
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
        }

        private void find_from_day_month()
        {
            DataTable dt = mycode.FillData("select * from Fine_master where Session_id='" + My.get_session_id() + "'");
            if (dt.Rows.Count == 0)
            {
                pnl_day_range.Visible = false;
                pnl_qtr_grid.Visible = false;
                pnl_day_mnth.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                rp_dm.DataSource = null;
                rp_dm.DataBind();
            }
            else
            {
                pnl_day_range.Visible = false;
                pnl_qtr_grid.Visible = false;
                pnl_day_mnth.Visible = true;
                rp_dm.DataSource = dt;
                rp_dm.DataBind();
            }
        }

        private void check_qtr_pay_mode()
        {
            DataTable dt = mycode.FillData("select Q_payment_mode from Fine_master where Fine_type='QuarterWise' and Session_id='" + My.get_session_id() + "'");
            if (dt.Rows.Count != 0)
            {
                string pay_mode = dt.Rows[0]["Q_payment_mode"].ToString();
                if (pay_mode == "Quarter")
                {
                    rd_q_pay_mode_dw.Checked = false;
                    rd_q_pay_mode_qw.Checked = true;

                    rd_q_pay_mode_dw.Enabled = false;
                    rd_q_pay_mode_qw.Enabled = true;
                    rd_q_pay_mode_dw.CssClass = "chkstle";
                    rd_q_pay_mode_qw.CssClass = "chkstle";
                    lbl_fine_mode.Text = "Quarter";
                }
                else
                {
                    rd_q_pay_mode_dw.Checked = true;
                    rd_q_pay_mode_qw.Checked = false;

                    rd_q_pay_mode_dw.Enabled = true;
                    rd_q_pay_mode_qw.Enabled = false;
                    rd_q_pay_mode_dw.CssClass = "chkstle";
                    rd_q_pay_mode_qw.CssClass = "chkstle";
                    lbl_fine_mode.Text = "Day";
                }
            }
        }


        private void check_fine_added_and_fee_taken()
        {
            DataTable dtx = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dtx.Rows.Count == 0)
            {
                DataTable dt = mycode.FillData("select Fine_type from Fine_master");
                if (dt.Rows.Count != 0)
                {
                    string fineType = dt.Rows[0]["Fine_type"].ToString();
                    bool chek_feec = find_fee_collected_fine();
                    if (chek_feec == false)
                    {
                        if (fineType == "DayWise")
                        {
                            rd_day.Checked = true;
                            rd_month.Checked = false;
                            rd_quater.Checked = false;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = false;

                            rd_month.Enabled = false;
                            rd_quater.Enabled = false;
                            rd_day_range.Enabled = false;
                            rd_next_month.Enabled = false;

                            rd_month.CssClass = "chkstle";
                            rd_quater.CssClass = "chkstle";
                            rd_day_range.CssClass = "chkstle";
                            rd_next_month.CssClass = "chkstle";
                        }
                        else if (fineType == "MonthWise")
                        {
                            rd_day.Checked = false;
                            rd_month.Checked = true;
                            rd_quater.Checked = false;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = false;

                            rd_day.Enabled = false;
                            rd_quater.Enabled = false;
                            rd_day_range.Enabled = false;
                            rd_next_month.Enabled = false; 

                            rd_day.CssClass = "chkstle";
                            rd_quater.CssClass = "chkstle";
                            rd_day_range.CssClass = "chkstle";
                            rd_next_month.CssClass = "chkstle";
                        }
                        else if (fineType == "NextMonthWise")
                        {
                            mycode.bind_all_ddl_with_id(ddl_applicable_from_month, "select Month,Month_Id from Month_Index where Position>1 order by Position asc");
                            rd_day.Checked = false;
                            rd_month.Checked = false;
                            rd_quater.Checked = false;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = true;

                            rd_day.Enabled = false;
                            rd_quater.Enabled = false;
                            rd_day_range.Enabled = false;
                            rd_month.Enabled = false;
                            rd_next_month.Enabled = true;

                            rd_day.CssClass = "chkstle";
                            rd_quater.CssClass = "chkstle";
                            rd_day_range.CssClass = "chkstle";
                            rd_month.CssClass = "chkstle";
                        }
                        else if (fineType == "QuarterWise" || fineType == "Quarter")
                        {
                            rd_day.Checked = false;
                            rd_month.Checked = false;
                            rd_quater.Checked = true;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = false;

                            rd_day.Enabled = false;
                            rd_quater.Enabled = true;
                            rd_day_range.Enabled = false;
                            rd_month.Enabled = false;
                            rd_next_month.Checked = false;

                            rd_day.CssClass = "chkstle";
                            rd_quater.CssClass = "chkstle";
                            rd_day_range.CssClass = "chkstle";
                            rd_month.CssClass = "chkstle";
                        }
                        else
                        {
                            rd_day.Checked = false;
                            rd_month.Checked = false;
                            rd_quater.Checked = true;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = false;

                            rd_day.Enabled = false;
                            rd_month.Enabled = false;
                            rd_day_range.Enabled = false;
                            rd_next_month.Enabled = false;

                            rd_day.CssClass = "chkstle";
                            rd_month.CssClass = "chkstle";
                            rd_day_range.CssClass = "chkstle";
                            rd_next_month.CssClass = "chkstle";
                            check_rd_btn();
                        }
                    }
                    else
                    {
                        if (fineType == "DayWise")
                        {
                            rd_day.Checked = true;
                            rd_month.Checked = false;
                            rd_quater.Checked = false;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = false;
                        }
                        else if (fineType == "MonthWise")
                        {
                            rd_day.Checked = false;
                            rd_month.Checked = true;
                            rd_quater.Checked = false;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = false;
                        }
                        else if (fineType == "NextMonthWise")
                        {
                            mycode.bind_all_ddl_with_id(ddl_applicable_from_month, "select Month,Month_Id from Month_Index where Position>1 order by Position asc");
                            rd_day.Checked = false;
                            rd_month.Checked = false;
                            rd_quater.Checked = false;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = true;
                        }
                        else if (fineType == "QuarterWise"|| fineType == "Quarter")
                        {
                            rd_day.Checked = false;
                            rd_month.Checked = false;
                            rd_quater.Checked = true;
                            rd_day_range.Checked = false;
                            rd_next_month.Checked = false;
                        }
                        else
                        {
                            rd_day.Checked = false;
                            rd_month.Checked = false;
                            rd_quater.Checked = false;
                            rd_day_range.Checked = true;
                            rd_next_month.Checked = false;
                        }
                    }
                }
            }
            else
            {
                rd_day_range.Checked = true;
                rd_day.Checked = false;
                rd_month.Checked = false;
                rd_quater.Checked = false;
                rd_next_month.Checked = false;

                rd_day.Enabled = false;
                rd_month.Enabled = false;
                rd_quater.Enabled = false;
                rd_next_month.Enabled = false;

                rd_day.CssClass = "chkstle";
                rd_month.CssClass = "chkstle";
                rd_quater.CssClass = "chkstle";
                rd_next_month.CssClass = "chkstle";
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


        protected void rd_type_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                check_rd_btn();
            }
            catch (Exception ex)
            {
            }
        }

        private void check_rd_btn()
        {
            chk_isnextmonth_fine_apply.Visible = false;
            if (rd_day.Checked == true)
            {
                pnl_day_range_wise.Visible = false;
                lbl_fine_day_mnth.InnerText = "Fine Amount Per Day";
                pnl_day_and_month.Visible = true;
                pnl_quarter.Visible = false;
            }
            else if (rd_month.Checked == true)
            {
                chk_isnextmonth_fine_apply.Visible = true;
                pnl_day_range_wise.Visible = false;
                lbl_fine_day_mnth.InnerText = "Fine Amount Per Month";
                pnl_day_and_month.Visible = true;
                pnl_quarter.Visible = false;
                mycode.bind_all_ddl_with_id(ddl_applicable_from_month, "select Month,Month_Id from Month_Index order by Position asc");
            }

            else if (rd_next_month.Checked == true)
            {
                pnl_day_range_wise.Visible = false;
                lbl_fine_day_mnth.InnerText = "Fine Amount Per Month";
                pnl_day_and_month.Visible = true;
                pnl_quarter.Visible = false;
                mycode.bind_all_ddl_with_id(ddl_applicable_from_month, "select Month,Month_Id from Month_Index where Position>1 order by Position asc");
            }

            else if (rd_day_range.Checked == true)
            {
                pnl_day_and_month.Visible = false;
                pnl_quarter.Visible = false;
                pnl_day_range_wise.Visible = true;
            }
            else
            {
                pnl_day_range_wise.Visible = false;
                pnl_quarter.Visible = true;
                pnl_day_and_month.Visible = false;
                rd_q_pay_mode_dw.Checked = true;
                lbl_fine_mode.Text = "Day";
                check_qtr_pay_mode();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (rd_quater.Checked == true)
                {
                    if (ddl_quarter.SelectedItem.Text == "Select")
                    {
                        Alertme("Please choose quarter", "warning");
                        ddl_quarter.Focus();
                    }
                    else if (txt_q_last_day_to_deposit.Text == "")
                    {
                        Alertme("Please enter last day of month to deposit fees.", "warning");
                        txt_q_last_day_to_deposit.Focus();
                    }
                    else if (txt_q_fine_amt.Text == "")
                    {
                        Alertme("Please enter fine amount.", "warning");
                        txt_q_fine_amt.Focus();
                    }
                    else
                    {
                        go_to_quarter();
                        bind_grd_view();
                    }
                }
                else if (rd_day_range.Checked == true)
                {
                    if (txt_descriptions.Text == "")
                    {
                        Alertme("Please enter description", "warning");
                        txt_descriptions.Focus();
                    }
                    else if (txt_no_of_daye.Text == "")
                    {
                        Alertme("Please enter no of day.", "warning");
                        txt_no_of_daye.Focus();
                    }
                    else if (txt_fine_amount.Text == "")
                    {
                        Alertme("Please enter fine amount.", "warning");
                        txt_fine_amount.Focus();
                    }
                    else
                    {
                        go_to_day_range();
                        bind_grd_view();
                    }
                }
                else
                {
                    if (txt_last_day_to_deposit_fees.Text == "")
                    {
                        Alertme("Please enter last day of month to deposit fees.", "warning");
                        txt_last_day_to_deposit_fees.Focus();
                    }
                    else if (txt_fine_amt.Text == "")
                    {
                        Alertme("Please enter fine amount.", "warning");
                        txt_fine_amt.Focus();
                    }
                    else if (ddl_applicable_from_month.Text == "Select")
                    {
                        Alertme("Please select applicable from month.", "warning");
                        ddl_applicable_from_month.Focus();
                    }
                    else
                    {
                        save_day_month_fine();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void final_submit_data()
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                save_day_month_fine();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                save_day_month_fine();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void save_day_month_fine()
        {
            string fineType = "";
            if (rd_day.Checked == true)
            {
                fineType = "DayWise";
            }
            if (rd_month.Checked == true)
            {
                fineType = "MonthWise";
            }
            if (rd_next_month.Checked == true)
            {
                fineType = "NextMonthWise";
            }

            bool chek_fee = find_fine_added();
            if (chek_fee == false)
            {
                bool chek_feec = find_fee_collected_fine();
                if (chek_feec == false)
                {
                    SqlCommand cmd;
                    string query = "Update Fine_master set Last_day_to_deposit_fees=@Last_day_to_deposit_fees,Fine_amt_per_day_or_month=@Fine_amt_per_day_or_month,Applicable_from_month_or_quater=@Applicable_from_month_or_quater,Applicable_from_month_or_quater_id=@Applicable_from_month_or_quater_id,Status=@Status,Is_next_month_fine_calculate=@Is_next_month_fine_calculate,Created_date=@Created_date,Created_idate=@Created_idate,User_id=@User_id where Fine_type='" + fineType + "' and Session_id='" + My.get_session_id() + "'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Last_day_to_deposit_fees", My.toDouble(txt_last_day_to_deposit_fees.Text).ToString("00"));
                    cmd.Parameters.AddWithValue("@Fine_amt_per_day_or_month", My.toDouble(txt_fine_amt.Text).ToString("0.00"));
                    cmd.Parameters.AddWithValue("@Applicable_from_month_or_quater", ddl_applicable_from_month.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Applicable_from_month_or_quater_id", ddl_applicable_from_month.SelectedValue);
                    cmd.Parameters.AddWithValue("@Status", ddl_staus.SelectedValue);
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());


                    if (chk_isnextmonth_fine_apply.Checked==true)
                    {
                        cmd.Parameters.AddWithValue("@Is_next_month_fine_calculate", "1");

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_next_month_fine_calculate", "0");
                    }

                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Fine details has been updated successfully.", "success");
                        bind_empty();
                    }
                }
                else
                {
                    save_new_row(fineType);
                }
            }
            else
            {
                save_new_row(fineType);
            }
        }

        private void save_new_row(string fineType)
        {
            mycode.executequery("delete from Fine_master where Session_id='" + My.get_session_id() + "'");
            SqlCommand cmd;
            string query = "INSERT INTO Fine_master (Session_id,Fine_id,Fine_type,Last_day_to_deposit_fees,Fine_amt_per_day_or_month,Quater_id,Q_start_month,Q_end_month,Q_payment_mode,Status,Applicable_from_month_or_quater,Created_date,Created_idate,User_id,Firm_id,Applicable_from_month_or_quater_id,Is_next_month_fine_calculate) values (@Session_id,@Fine_id,@Fine_type,@Last_day_to_deposit_fees,@Fine_amt_per_day_or_month,@Quater_id,@Q_start_month,@Q_end_month,@Q_payment_mode,@Status,@Applicable_from_month_or_quater,@Created_date,@Created_idate,@User_id,@Firm_id,@Applicable_from_month_or_quater_id,@Is_next_month_fine_calculate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
            cmd.Parameters.AddWithValue("@Fine_id", My.auto_serialS("group_id"));
            cmd.Parameters.AddWithValue("@Fine_type", fineType);
            cmd.Parameters.AddWithValue("@Last_day_to_deposit_fees", My.toDouble(txt_last_day_to_deposit_fees.Text).ToString("00"));
            cmd.Parameters.AddWithValue("@Fine_amt_per_day_or_month", My.toDouble(txt_fine_amt.Text).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Quater_id", "0");
            cmd.Parameters.AddWithValue("@Q_start_month", "0");
            cmd.Parameters.AddWithValue("@Q_end_month", "0");
            cmd.Parameters.AddWithValue("@Q_payment_mode", "0");
            cmd.Parameters.AddWithValue("@Status", ddl_staus.SelectedValue);
            cmd.Parameters.AddWithValue("@Applicable_from_month_or_quater", ddl_applicable_from_month.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Applicable_from_month_or_quater_id", ddl_applicable_from_month.SelectedValue);
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Firm_id", ViewState["firm_id"].ToString());

            if (chk_isnextmonth_fine_apply.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Is_next_month_fine_calculate", "1");

            }
            else
            {
                cmd.Parameters.AddWithValue("@Is_next_month_fine_calculate", "0");
            }
            if (My.InsertUpdateData(cmd))
            {
                string qrys = "update globle_data set Fine_mode='1'";
                My.exeSql(qrys);
                Alertme("Fine details has been added successfully.", "success");
                bind_empty();
                //BindGrid();
            }
        }

        private bool find_fee_collected_fine()
        {
            SqlCommand cmd;
            string strQuery = "Select * from Typewise_fee_collection where content_id='6121' and session='" + My.get_session() + "'";
            cmd = new SqlCommand(strQuery);
            DataTable dt = mycode.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        private bool find_fine_added()
        {
            SqlCommand cmd;
            string strQuery = "Select * from Fine_master where Status!='555'";
            cmd = new SqlCommand(strQuery);
            DataTable dt = mycode.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void go_to_quarter()
        { 
            string fineType = "QuarterWise";
            bool chek_fee = find_qtr_fine_added();
            if (chek_fee == false)
            {
                bool chek_feec = find_fee_collected_fine();
                if (chek_feec == false)
                {
                    string fineMode = "Quarter";
                    if (rd_q_pay_mode_dw.Checked == true)
                    {
                        fineMode = "Day";
                    }
                    SqlCommand cmd;
                    string query = "Update Fine_master set Last_day_to_deposit_fees=@Last_day_to_deposit_fees,Fine_amt_per_day_or_month=@Fine_amt_per_day_or_month,Quater_id=@Quater_id,Q_start_month=@Q_start_month,Q_end_month=@Q_end_month,Q_payment_mode=@Q_payment_mode,Status=@Status,Q_start_year=@Q_start_year,Q_end_year=@Q_end_year where Fine_type='" + fineType + "' and Quater_id='" + ddl_quarter.SelectedValue + "' and Session_id='" + My.get_session_id() + "'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Last_day_to_deposit_fees", My.toDouble(txt_q_last_day_to_deposit.Text).ToString("00"));
                    cmd.Parameters.AddWithValue("@Fine_amt_per_day_or_month", My.toDouble(txt_q_fine_amt.Text).ToString("0.00"));
                    cmd.Parameters.AddWithValue("@Quater_id", ddl_quarter.SelectedValue);
                    cmd.Parameters.AddWithValue("@Q_start_month", ViewState["SmonthId"].ToString());
                    cmd.Parameters.AddWithValue("@Q_end_month", ViewState["EmonthId"].ToString());
                    cmd.Parameters.AddWithValue("@Q_payment_mode", fineMode);
                    cmd.Parameters.AddWithValue("@Status", ddl_q_staus.SelectedValue); 
                    cmd.Parameters.AddWithValue("@Q_start_year", ViewState["SYear"].ToString());
                    cmd.Parameters.AddWithValue("@Q_end_year", ViewState["EYear"].ToString()); 
                    if (My.InsertUpdateData(cmd))
                    { 
                        Alertme("Fine details has been updated successfully.", "success");
                        bind_empty();
                    }
                }
                else
                {
                    save_new_row_qtrwise(fineType);
                }
            }
            else
            {
                save_new_row_qtrwise(fineType);
            }
        }

        private void save_new_row_qtrwise(string fineType)
        {
            string fineMode = "Quarter";
            if (rd_q_pay_mode_dw.Checked == true)
            {
                fineMode = "Day";
            }
            mycode.executequery("delete from Fine_master where Fine_type!='QuarterWise' and Session_id='" + My.get_session_id() + "'; delete from Fine_master where Fine_type='QuarterWise' and Session_id='" + My.get_session_id() + "' and Quater_id='" + ddl_quarter.SelectedValue + "'");
            SqlCommand cmd;
            string query = "INSERT INTO Fine_master (Session_id,Fine_id,Fine_type,Last_day_to_deposit_fees,Fine_amt_per_day_or_month,Quater_id,Q_start_month,Q_end_month,Q_payment_mode,Status,Applicable_from_month_or_quater,Created_date,Created_idate,User_id,Firm_id,Applicable_from_month_or_quater_id,Q_start_year,Q_end_year) values (@Session_id,@Fine_id,@Fine_type,@Last_day_to_deposit_fees,@Fine_amt_per_day_or_month,@Quater_id,@Q_start_month,@Q_end_month,@Q_payment_mode,@Status,@Applicable_from_month_or_quater,@Created_date,@Created_idate,@User_id,@Firm_id,@Applicable_from_month_or_quater_id,@Q_start_year,@Q_end_year)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
            cmd.Parameters.AddWithValue("@Fine_id", My.auto_serialS("group_id"));
            cmd.Parameters.AddWithValue("@Fine_type", fineType);
            cmd.Parameters.AddWithValue("@Last_day_to_deposit_fees", My.toDouble(txt_q_last_day_to_deposit.Text).ToString("00"));
            cmd.Parameters.AddWithValue("@Fine_amt_per_day_or_month", My.toDouble(txt_q_fine_amt.Text).ToString("0.00"));

            cmd.Parameters.AddWithValue("@Quater_id", ddl_quarter.SelectedValue);
            cmd.Parameters.AddWithValue("@Q_start_month", My.toDouble(ViewState["SmonthId"].ToString()).ToString("00"));
            cmd.Parameters.AddWithValue("@Q_end_month", My.toDouble(ViewState["EmonthId"].ToString()).ToString("00"));
            cmd.Parameters.AddWithValue("@Q_payment_mode", fineMode);
            cmd.Parameters.AddWithValue("@Status", ddl_q_staus.SelectedValue);

            cmd.Parameters.AddWithValue("@Applicable_from_month_or_quater", "0");
            cmd.Parameters.AddWithValue("@Applicable_from_month_or_quater_id", "0");
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Firm_id", ViewState["firm_id"].ToString());
             
            
            cmd.Parameters.AddWithValue("@Q_start_year", ViewState["SYear"].ToString());
            cmd.Parameters.AddWithValue("@Q_end_year", ViewState["EYear"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                string qrys = "update globle_data set Fine_mode='1'";
                My.exeSql(qrys);
                Alertme("Fine details has been added successfully.", "success");
                bind_empty();
                //BindGrid();
            }
        }


        private bool find_qtr_fine_added()
        {
            SqlCommand cmd;
            string strQuery = "Select * from Fine_master where Status!='555' and Fine_type='QuarterWise' and Session_id='" + My.get_session_id() + "' and Quater_id='" + ddl_quarter.SelectedValue + "'";
            cmd = new SqlCommand(strQuery);
            DataTable dt = mycode.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void bind_empty()
        {
            txt_last_day_to_deposit_fees.Text = "";
            txt_fine_amt.Text = "";
            txt_month_from.Text = "";
            txt_month_to.Text = "";
            txt_q_last_day_to_deposit.Text = "";
            txt_q_fine_amt.Text = "";

            txt_descriptions.Text = "";
            txt_no_of_daye.Text = "";
            txt_fine_amount.Text = "";
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            ViewState["isEDT"] = null;
            bind_grd_view();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            bind_empty();
        }

        protected void ddl_quarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_quarter_month();
            }
            catch (Exception ex)
            {
            }
        }

        private void find_quarter_month()
        {
            if (ddl_quarter.SelectedItem.Text == "Select")
            {
                Alertme("Please choose quarter.", "warning");
                ddl_quarter.Focus();
            }
            else
            {
                string cunrt_session = ViewState["session"].ToString();
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);


                



                DataTable dt = mycode.FillData("select *,(select top 1 Month from Month_Index where Month_Id=Quater_master.Start_month_id) as Start_month,(select top 1 Month from Month_Index where Month_Id=Quater_master.End_month_id) as End_month from Quater_master where Session_id='" + My.get_session_id() + "' and  Quater_id='" + ddl_quarter.SelectedValue + "' order by Quater_no asc");
                if (dt.Rows.Count != 0)
                {
                    txt_month_from.Text = dt.Rows[0]["Start_month"].ToString();
                    txt_month_to.Text = dt.Rows[0]["End_month"].ToString();


                    string monthid = My.tomonth_numberstring(txt_month_from.Text);
                    int s_month_id = My.toint(monthid);
                    s_year = My.check_start_months(s_month_id, s_year);


                    string Emonthid = My.tomonth_numberstring(txt_month_to.Text);
                    int E_month_id = My.toint(Emonthid);
                    int e_year = My.check_start_months(E_month_id, s_year);


                    ViewState["SmonthId"] = monthid;
                    ViewState["EmonthId"] = Emonthid;

                    ViewState["SYear"] = s_year;
                    ViewState["EYear"] = e_year;
                }
            }
        }

        protected void rd_q_pay_mode_dw_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_q_pay_mode_dw.Checked == true)
            {
                lbl_fine_mode.Text = "Day";
            }
            else
            {
                lbl_fine_mode.Text = "Quarter";
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_quarter_id = (Label)row.FindControl("lbl_quarter_id");
                rd_day.Checked = false;
                rd_month.Checked = false;
                rd_quater.Checked = true;
                check_rd_btn();
                Label lbl_start_month_id = (Label)row.FindControl("lbl_start_month_id");
                Label lbl_end_month_id = (Label)row.FindControl("lbl_end_month_id");
                Label lbl_status = (Label)row.FindControl("lbl_status");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_q_payment_mode = (Label)row.FindControl("lbl_q_payment_mode");
                Label lbl_fine_amt = (Label)row.FindControl("lbl_fine_amt");
                Label lbl_last_day = (Label)row.FindControl("lbl_last_day");


                hd_id.Value = lbl_Id.Text;
                ddl_quarter.SelectedValue = lbl_quarter_id.Text;
                find_quarter_month();
                txt_q_last_day_to_deposit.Text = lbl_last_day.Text;
                check_qtr_pay_mode();
                txt_q_fine_amt.Text = lbl_fine_amt.Text;
                ddl_q_staus.SelectedValue = lbl_status.Text;


                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {

            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");

            DataTable dsgdt = My.dataTable("Select Id from Typewise_fee_collection where content_id='6121' and session='" + My.get_session() + "'");
            if (dsgdt.Rows.Count == 0)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where  id='" + lbl_Id.Text + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr.Delete();
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Data deleted Successfully", "success");
                bind_grd_view();
            }
            else
            {
                Alertme("You can't delete this fine because there is a data associated with collected fees.", "warning");
            }
        }


        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_status")).Text == "1")
                {
                    ((Label)e.Item.FindControl("lbl_show_status")).Text = "Yes";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_show_status")).Text = "No";
                }

                if (((Label)e.Item.FindControl("lbl_Is_next_month_fine_calculate")).Text == "1")
                {
                    ((Label)e.Item.FindControl("lbl_dis_apply")).Text = "Yes";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_dis_apply")).Text = "No";
                }



            }
        }

        protected void rp_dm_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_status")).Text == "1")
                {
                    ((Label)e.Item.FindControl("lbl_show_status")).Text = "Yes";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_show_status")).Text = "No";
                }

                if (((Label)e.Item.FindControl("lbl_Is_next_month_fine_calculate")).Text == "1")
                {
                    ((Label)e.Item.FindControl("lbl_dis_apply")).Text = "Yes";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_dis_apply")).Text = "No";
                }

            }
        }

        protected void lnkEdit_dm_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

            Label lbl_last_day_to_deposit_fees = (Label)row.FindControl("lbl_last_day_to_deposit_fees");
            Label lbl_fine_amt_per_day_or_month = (Label)row.FindControl("lbl_fine_amt_per_day_or_month");
            Label lbl_status = (Label)row.FindControl("lbl_status");
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_applicable_from_month_or_quater = (Label)row.FindControl("lbl_applicable_from_month_or_quater");
            Label lbl_applicable_from_month_or_quater_id = (Label)row.FindControl("lbl_applicable_from_month_or_quater_id");

            try
            {
                Label lbl_Is_next_month_fine_calculate = (Label)row.FindControl("lbl_Is_next_month_fine_calculate");
                chk_isnextmonth_fine_apply.Visible = true;
                if (lbl_Is_next_month_fine_calculate.Text == "1")
                {
                    chk_isnextmonth_fine_apply.Checked = true;
                }
                else
                {
                    chk_isnextmonth_fine_apply.Checked = false;
                }

            }
            catch
            {

            }
          
            hd_id.Value = lbl_Id.Text;

            txt_last_day_to_deposit_fees.Text = lbl_last_day_to_deposit_fees.Text;
            txt_fine_amt.Text = lbl_fine_amt_per_day_or_month.Text;
            ddl_staus.SelectedValue = lbl_status.Text;
            try
            {
                ddl_applicable_from_month.SelectedValue = lbl_applicable_from_month_or_quater_id.Text;
            }
            catch
            {
            }

            btn_cancel.Visible = true;
            btn_Submit.Text = "Update";
        }



        ///======================================
        private void go_to_day_range()
        {
            if (ViewState["isEDT"] != null)
            {
                SqlCommand cmd;
                string query = "update Fine_master_day_range set Description=@Description,No_of_day=@No_of_day,Fine_amount=@Fine_amount,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id='" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Description", txt_descriptions.Text);
                cmd.Parameters.AddWithValue("@No_of_day", txt_no_of_daye.Text);
                cmd.Parameters.AddWithValue("@Fine_amount", txt_fine_amount.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Fine details has been updated successfully.", "success");
                    bind_empty();
                    //BindGrid();
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "INSERT INTO Fine_master_day_range (Description,No_of_day,Fine_amount,Branch_id,Created_by,Created_date,Created_idate,Session_id) values (@Description,@No_of_day,@Fine_amount,@Branch_id,@Created_by,@Created_date,@Created_idate,@Session_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
                cmd.Parameters.AddWithValue("@Description", txt_descriptions.Text);
                cmd.Parameters.AddWithValue("@No_of_day", txt_no_of_daye.Text);
                cmd.Parameters.AddWithValue("@Fine_amount", txt_fine_amount.Text);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    string qrys = "update globle_data set Fine_mode='2'";
                    My.exeSql(qrys);
                    Alertme("Fine details has been added successfully.", "success");
                    bind_empty();
                    //BindGrid();
                }
            }
        }


        protected void lnkEdit_dy_r_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

            Label lbl_description = (Label)row.FindControl("lbl_description");
            Label lbl_no_of_day = (Label)row.FindControl("lbl_no_of_day");
            Label lbl_fine_amount = (Label)row.FindControl("lbl_fine_amount");
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            hd_id.Value = lbl_Id.Text;
            ViewState["isEDT"] = "1";
            txt_descriptions.Text = lbl_description.Text;
            txt_no_of_daye.Text = lbl_no_of_day.Text;
            txt_fine_amount.Text = lbl_fine_amount.Text;

            btn_cancel.Visible = true;
            btn_Submit.Text = "Update";
        }

        protected void lnkDel_dy_r_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            //DataTable dsgdt = My.dataTable("Select Id from Typewise_fee_collection where content_id='6121' and session='" + My.get_session() + "'");
            //if (dsgdt.Rows.Count == 0)
            //{
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master_day_range where  id='" + lbl_Id.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Data deleted Successfully", "success");
            bind_grd_view();
            //}
            //else
            //{
            //    Alertme("You can't delete this fine because there is a data associated with collected fees.", "warning");
            //}
        }
    }
}