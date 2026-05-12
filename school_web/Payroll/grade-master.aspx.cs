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
    public partial class grade_master : System.Web.UI.Page
    {
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
                        hd_temp_id.Value = My.create_random_no_otp();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Grade_Master");
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
        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select * from PRL_Grade_Master");
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
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            //txt_name.Text = "";
            txt_description.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_grade_name.Text == "")
            {
                Alertme("Please Enter Grade Name", "warning");
                txt_grade_name.Focus();
                return;
            }
            if (txt_nature_of_work.Text == "")
            {
                Alertme("Please Enter Nature Of Work", "warning");
                txt_nature_of_work.Focus();
                return;
            }
            if (txt_description.Text == "")
            {
                Alertme("Please Enter Description", "warning");
                txt_description.Focus();
                return;
            }
            if (txt_min_working_hour.Text == "")
            {
                Alertme("Please Enter Min workig hour", "warning");
                txt_min_working_hour.Focus();
                return;
            }
            if (txt_max_working_hour.Text == "")
            {
                Alertme("Please Enter Max workig hour", "warning");
                txt_max_working_hour.Focus();
                return;
            }
            if (ddl_salary_calculation_method.Text == "Custom")
            {
                if (My.toDouble(txt_days.Text) == 0)
                {
                    Alertme("Please Enter valid Salary calculation days", "warning");
                    txt_days.Focus();
                    return;
                }
            }

            try
            {
                if (btn_Submit.Text == "Add")
                {
                    string grade_id = My.auto_serialS("grade_id");
                    submit_details(grade_id);
                    My.exeSql("update PRL_Grade_wise_weekly_off set Grade_id='" + grade_id + "' where Grade_id='" + hd_temp_id.Value + "' ");
                    empty_form();
                    bind_grd_view();
                    hd_temp_id.Value = My.create_random_no_otp();
                }
                else
                {
                    update_update_details();
                    empty_form();
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {

            }
        }
        string temp_grade_id = "";
        private void submit_details(string grade_id)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Grade_Master", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = txt_grade_name.Text;
            dr[2] = grade_id;
            dr[3] = txt_nature_of_work.Text;
            dr[4] = txt_description.Text;
            dr["Max_working_hour"] = txt_max_working_hour.Text;
            dr["Min_working_hour"] = txt_min_working_hour.Text;

            if (chk_mon.Checked == true)
            {
                dr["Mon"] = 1;
            }
            if (chk_tue.Checked == true)
            {
                dr["Tue"] = 1;
            }
            if (chk_wed.Checked == true)
            {
                dr["Wed"] = 1;
            }
            if (chk_thu.Checked == true)
            {
                dr["Thu"] = 1;
            }
            if (chk_fri.Checked == true)
            {
                dr["Fri"] = 1;
            }
            if (chk_sat.Checked == true)
            {
                dr["Sat"] = 1;
            }
            if (chk_sun.Checked == true)
            {
                dr["Sun"] = 1;
            }
            if (ddl_salary_calculation_method.Text == "Custom")
            {
                dr["salary_calculation_method"] = txt_days.Text;
            }
            else
            {
                dr["salary_calculation_method"] = ddl_salary_calculation_method.Text;
            }
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Grade Master Created Successfully", "success");
        }

        private void empty_form()
        {
            txt_grade_name.Text = "";
            temp_grade_id = "temp" + My.auto_serialS("temp_grade_id");
            txt_nature_of_work.Text = "";
            txt_description.Text = "";
            txt_min_working_hour.Text = "";
            txt_max_working_hour.Text = "";
            chk_mon.Checked = false;
            chk_tue.Checked = false;
            chk_wed.Checked = false;
            chk_thu.Checked = false;
            chk_fri.Checked = false;
            chk_sat.Checked = false;
            chk_sun.Checked = false;
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
            txt_grade_name.Focus();
        }
        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Grade_Master where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = txt_grade_name.Text;
                dr[3] = txt_nature_of_work.Text;
                dr[4] = txt_description.Text;
                dr["Max_working_hour"] = txt_max_working_hour.Text;
                dr["Min_working_hour"] = txt_min_working_hour.Text;
                if (chk_mon.Checked == true)
                {
                    dr["Mon"] = 1;
                }
                if (chk_tue.Checked == true)
                {
                    dr["Tue"] = 1;
                }
                if (chk_wed.Checked == true)
                {
                    dr["Wed"] = 1;
                }
                if (chk_thu.Checked == true)
                {
                    dr["Thu"] = 1;
                }
                if (chk_fri.Checked == true)
                {
                    dr["Fri"] = 1;
                }
                if (chk_sat.Checked == true)
                {
                    dr["Sat"] = 1;
                }
                if (chk_sun.Checked == true)
                {
                    dr["Sun"] = 1;
                }
                if (ddl_salary_calculation_method.Text == "Custom")
                {
                    dr["salary_calculation_method"] = txt_days.Text;
                }
                else
                {
                    dr["salary_calculation_method"] = ddl_salary_calculation_method.Text;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Grade Master Updated Successfully", "success");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_grade_name = (Label)row.FindControl("lbl_grade_name");
                Label lbl_nature_of_work = (Label)row.FindControl("lbl_nature_of_work");
                Label lbl_description = (Label)row.FindControl("lbl_description");
                Label lbl_max_working_hour = (Label)row.FindControl("lbl_max_working_hour");
                Label lbl_min_working_hour = (Label)row.FindControl("lbl_min_working_hour");
                Label lbl_salary_calculation_method = (Label)row.FindControl("lbl_salary_calculation_method");
                Label lbl_grade_id = (Label)row.FindControl("lbl_grade_id");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;

                hd_temp_id.Value = lbl_grade_id.Text;
                txt_grade_name.Text = lbl_grade_name.Text;
                txt_nature_of_work.Text = lbl_nature_of_work.Text;
                txt_description.Text = lbl_description.Text;
                txt_max_working_hour.Text = lbl_max_working_hour.Text;
                txt_min_working_hour.Text = lbl_min_working_hour.Text;

                if (lbl_salary_calculation_method.Text.Length <= 2)
                {
                    ddl_salary_calculation_method.Text = "Custom";
                    txt_days.Text = lbl_salary_calculation_method.Text;
                    scDPnl.Visible = true;
                }
                else
                {
                    scDPnl.Visible = false;
                    ddl_salary_calculation_method.Text = lbl_salary_calculation_method.Text;
                    if (ddl_salary_calculation_method.SelectedItem == null)
                    {
                        ddl_salary_calculation_method.SelectedIndex = 0;
                    }
                }

                DataTable dt = mycode.FillData("select * from PRL_Grade_Master where Id='" + lbl_Id.Text + "'");
                chk_mon.Checked = My.toBool(dt.Rows[0]["Mon"].ToString());
                chk_tue.Checked = My.toBool(dt.Rows[0]["Tue"].ToString());
                chk_wed.Checked = My.toBool(dt.Rows[0]["Wed"].ToString());
                chk_thu.Checked = My.toBool(dt.Rows[0]["Thu"].ToString());
                chk_fri.Checked = My.toBool(dt.Rows[0]["Fri"].ToString());
                chk_sat.Checked = My.toBool(dt.Rows[0]["Sat"].ToString());
                chk_sun.Checked = My.toBool(dt.Rows[0]["Sun"].ToString());
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
            Label lbl_grade_id = (Label)row.FindControl("lbl_grade_id");

            My.exeSql("delete from PRL_Grade_Master where Id='" + lbl_Id.Text + "'; delete from PRL_Grade_wise_weekly_off where Grade_id='" + lbl_grade_id.Text + "'");
            Alertme("Grafe has been deleted successfully", "success");
            bind_grd_view();
        }
        protected void ddl_salary_calculation_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_salary_calculation_method.SelectedItem.Text == "Custom")
                {
                    scDPnl.Visible = true;
                }
                else
                {
                    scDPnl.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void chk_all_week_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_all_week.Checked == true)
                {
                    chk_first_week.Checked = true;
                    chk_scnd_week.Checked = true;
                    chk_thrd_week.Checked = true;
                    chk_frth_week.Checked = true;
                    chk_fth_week.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    chk_first_week.Checked = false;
                    chk_scnd_week.Checked = false;
                    chk_thrd_week.Checked = false;
                    chk_frth_week.Checked = false;
                    chk_fth_week.Checked = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void chk_mon_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_mon.Checked == true)
            {
                lbl_week_of_day_h.Text = "Monday";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (chk_tue.Checked == true)
            {
                lbl_week_of_day_h.Text = "Tuesday";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (chk_wed.Checked == true)
            {
                lbl_week_of_day_h.Text = "Wednesday";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (chk_thu.Checked == true)
            {
                lbl_week_of_day_h.Text = "Thursday";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (chk_fri.Checked == true)
            {
                lbl_week_of_day_h.Text = "Friday";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (chk_sat.Checked == true)
            {
                lbl_week_of_day_h.Text = "Saturday";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (chk_sun.Checked == true)
            {
                lbl_week_of_day_h.Text = "Sunday";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btn_save_week_off_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Grade_wise_weekly_off where Grade_id='" + hd_temp_id.Value + "' and Day='" + lbl_week_of_day_h.Text + "'", My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "PRL_Grade_wise_weekly_off");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["Grade_id"] = hd_temp_id.Value;
                    dr["Day"] = lbl_week_of_day_h.Text;
                    dr["All_week"] = chk_all_week.Checked;
                    dr["First_week"] = chk_first_week.Checked;
                    dr["Second_week"] = chk_scnd_week.Checked;
                    dr["Third_week"] = chk_thrd_week.Checked;
                    dr["Fourth_week"] = chk_frth_week.Checked;
                    dr["Fifth_week"] = chk_fth_week.Checked;
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["All_week"] = chk_all_week.Checked;
                        dr["First_week"] = chk_first_week.Checked;
                        dr["Second_week"] = chk_scnd_week.Checked;
                        dr["Third_week"] = chk_thrd_week.Checked;
                        dr["Fourth_week"] = chk_frth_week.Checked;
                        dr["Fifth_week"] = chk_fth_week.Checked;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
            }
            catch (Exception ex)
            {
            }
        }
    }
}