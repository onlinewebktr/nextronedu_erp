using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Payroll
{
    public partial class Leave_Setup : System.Web.UI.Page
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

                        txt_start_period.Text = mycode.date();
                        txt_end_period.Text = mycode.date();

                        ddl_treatement_of_leave_to_next_year.Enabled = false;
                        txt_days_worked_in_month.Enabled = false;
                        txt_earned_leave.Enabled = false;
                        txt_max_leave_cf.Enabled = false;
                        mycode.bind_all_ddl_with_id(ddl_gradename, "select  grade_name,grade_id from dbo.[PRL_Grade_Master] order by grade_name");
                        mycode.bind_all_ddl_with_id(txt_leave_id, "select Leave_Name,Leave_Name_Id from dbo.[PRL_Leave_Name_Master] order by Leave_Name");
                        empty_form();
                         
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Leave_Setup");
            }
        }
        private void empty_form()
        {
            txt_leave_applied.Text = "";
            txt_days_worked_in_month.Text = "";
            txt_earned_leave.Text = "";
            txt_max_leave_cf.Text = "";
            btn_add.Visible = true;
            btn_cancel.Visible = false;

            ddl_gradename.Focus();
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




        protected void ddl_leave_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_leave_type.SelectedItem.ToString() == "Fixed")
            {
                txt_days_worked_in_month.Text = "";
                txt_earned_leave.Text = "";
                txt_days_worked_in_month.Enabled = false;
                txt_earned_leave.Enabled = false;
            }
            else
            {
                txt_days_worked_in_month.Enabled = true;
                txt_earned_leave.Enabled = true;

            }

        }

        protected void chk_cf_n_year_Checked_1_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_cf_n_year_Checked_1.Checked == true)
            {
                string q = "select 'Carry Forward All' Parameter_Name   union all  select 'Lapse & Carry Forward' Parameter_Name";
                mycode.bind_ddl(ddl_treatement_of_leave_to_next_year, q);
                ddl_treatement_of_leave_to_next_year.Text = "Carry Forward All";
                ddl_treatement_of_leave_to_next_year.Enabled = true;

            }
            else
            {
                string q = "select 'Lapse' Parameter_Name   union all  select 'Carry Forward All' Parameter_Name union all  select 'Lapse & Carry Forward' Parameter_Name";
                mycode.bind_ddl(ddl_treatement_of_leave_to_next_year, q);
                ddl_treatement_of_leave_to_next_year.Text = "Lapse";
                ddl_treatement_of_leave_to_next_year.Enabled = true;

            }
        }

        protected void ddl_treatement_of_leave_to_next_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_treatement_of_leave_to_next_year.SelectedItem.ToString() == "Lapse & Carry Forward")
            {
                txt_max_leave_cf.Enabled = true;
            }
            else
            {
                txt_max_leave_cf.Text = "";
                txt_max_leave_cf.Enabled = false;
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (ddl_gradename.Text == "")
            {
                Alertme("Please Select Grade", "warning");
                ddl_gradename.Focus();
                return;
            }
            if (txt_start_period.Text == "")
            {
                Alertme("Please Enter Period", "warning");
                txt_start_period.Focus();
                return;
            }
            if (txt_end_period.Text == "")
            {
                Alertme("Please Enter Period", "warning");
                txt_end_period.Focus();
                return;
            }
            if (txt_leave_id.Text == "")
            {
                Alertme("Please select leave name", "warning");
                txt_leave_id.Focus();
                return;
            }
            if (ddl_leave_type.Text == "")
            {
                Alertme("Please Select Leave Type", "warning");
                ddl_leave_type.Focus();
                return;
            }

            if (txt_leave_applied.Text == "")
            {
                Alertme("Please Select Leave Applied", "warning");
                txt_leave_applied.Focus();
                return;
            }
            if (ddl_treatement_of_leave_to_next_year.Text == "")
            {
                Alertme("Please select treatement of leave", "warning");
                ddl_treatement_of_leave_to_next_year.Focus();
                return;
            }
            if (txt_max_leave_cf.Enabled == true)
            {
                if (txt_max_leave_cf.Text == "")
                {
                    Alertme("Please Enter Max Leave C/F", "warning");
                    txt_max_leave_cf.Focus();
                    return;
                }
            }

            try
            {
                submit_details();
                empty_form();
                bind_grd_view();

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Leasve setup");
            }


        }
        UsesCode uc = new UsesCode();
        private void submit_details()
        {
            string chk = "0";
            if (chk_cf_n_year_Checked_1.Checked == true)
            {
                chk = "1";
            }


            SqlCommand cmd;
            if (btn_add.Text == "Add")
            {
                string query = "INSERT INTO PRL_Staff_Leave_Setup (Grade_id,Start_Period,End_Period,Leave_id,Leave_Type,No_of_Leave,leave_applied,Days_Worked_in_month,Earned_Leave,Leave_CF_in_next_year,Treatement_of_Leave_to_Next_year,Max_Leave_CF,Availed_in_same_year) values (@Grade_id,@Start_Period,@End_Period,@Leave_id,@Leave_Type,@No_of_Leave,@leave_applied,@Days_Worked_in_month,@Earned_Leave,@Leave_CF_in_next_year,@Treatement_of_Leave_to_Next_year,@Max_Leave_CF,@Availed_in_same_year)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Grade_id", ddl_gradename.SelectedValue);
                cmd.Parameters.AddWithValue("@Start_Period", uc.getdate2(txt_start_period.Text));
                cmd.Parameters.AddWithValue("@End_Period", uc.getdate2(txt_end_period.Text));
                cmd.Parameters.AddWithValue("@Leave_id", txt_leave_id.SelectedValue);
                cmd.Parameters.AddWithValue("@Leave_Type", ddl_leave_type.Text);
                cmd.Parameters.AddWithValue("@No_of_Leave", txt_leave_applied.Text);
                cmd.Parameters.AddWithValue("@leave_applied", ddl_leave_unit.Text);
                cmd.Parameters.AddWithValue("@Days_Worked_in_month", My.toIntS(txt_days_worked_in_month.Text));
                cmd.Parameters.AddWithValue("@Earned_Leave", My.toIntS(txt_earned_leave.Text));
                cmd.Parameters.AddWithValue("@Leave_CF_in_next_year", chk);
                cmd.Parameters.AddWithValue("@Treatement_of_Leave_to_Next_year", ddl_treatement_of_leave_to_next_year.Text);
                cmd.Parameters.AddWithValue("@Max_Leave_CF", My.toIntS(txt_max_leave_cf.Text));
                cmd.Parameters.AddWithValue("@Availed_in_same_year", 0);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string query = "Update PRL_Staff_Leave_Setup set Grade_id=@Grade_id,Start_Period=@Start_Period,End_Period=@End_Period,Leave_id=@Leave_id,Leave_Type=@Leave_Type,No_of_Leave=@No_of_Leave,leave_applied=@leave_applied,Days_Worked_in_month=@Days_Worked_in_month,Earned_Leave=@Earned_Leave,Leave_CF_in_next_year=@Leave_CF_in_next_year,Treatement_of_Leave_to_Next_year=@Treatement_of_Leave_to_Next_year,Max_Leave_CF=@Max_Leave_CF,Availed_in_same_year=@Availed_in_same_year where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Grade_id", ddl_gradename.SelectedValue);
                cmd.Parameters.AddWithValue("@Start_Period", uc.getdate2(txt_start_period.Text));
                cmd.Parameters.AddWithValue("@End_Period", uc.getdate2(txt_end_period.Text));
                cmd.Parameters.AddWithValue("@Leave_id", txt_leave_id.SelectedValue);
                cmd.Parameters.AddWithValue("@Leave_Type", ddl_leave_type.Text);
                cmd.Parameters.AddWithValue("@No_of_Leave", txt_leave_applied.Text);
                cmd.Parameters.AddWithValue("@leave_applied", ddl_leave_unit.Text);
                cmd.Parameters.AddWithValue("@Days_Worked_in_month", My.toIntS(txt_days_worked_in_month.Text));
                cmd.Parameters.AddWithValue("@Earned_Leave", My.toIntS(txt_earned_leave.Text));
                cmd.Parameters.AddWithValue("@Leave_CF_in_next_year", chk);
                cmd.Parameters.AddWithValue("@Treatement_of_Leave_to_Next_year", ddl_treatement_of_leave_to_next_year.Text);
                cmd.Parameters.AddWithValue("@Max_Leave_CF", My.toIntS(txt_max_leave_cf.Text));
                cmd.Parameters.AddWithValue("@Availed_in_same_year", 0);
                cmd.Parameters.AddWithValue("@Id", HdID.Value);
                if (My.InsertUpdateData(cmd))
                {
                    btn_add.Text = "Add";
                } 
            }
        }

        private void bind_grd_view()
        {
            DataTable dt1 = mycode.FillData("select sls. *,grade_name,Leave_Name,format(sls.Start_Period, 'dd/MM/yyyy') as Start_Period1,format(sls.End_Period, 'dd/MM/yyyy') as End_Period1 from dbo.[PRL_Staff_Leave_Setup] sls join PRL_Grade_Master gm on sls.Grade_id=gm.grade_id join PRL_Leave_Name_Master lnm on sls.Leave_id=lnm.Leave_Name_Id where sls.Grade_id='" + ddl_gradename.SelectedValue + "'");
            if (dt1.Rows.Count == 0)
            {
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                GrdView.DataSource = dt1;
                GrdView.DataBind();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Leave_Setup.aspx", false);
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            LinkButton lnkDel = (LinkButton)row.FindControl("lnkDel");
            Label Id = (Label)row.FindControl("lbl_Id");
            Bind_addeddata(Id.Text);
        }



        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                LinkButton lnkDel = (LinkButton)row.FindControl("lnkDel");
                Label Id = (Label)row.FindControl("lbl_Id");
                mycode.executequery("select * from Staff_Leave_Setup where Id='" + Id.Text + "'");
                bind_grd_view();
                Alertme("Leave Setup deleted Successfully", "success");

            }
            catch
            {
            }
        }

        private void Bind_addeddata(string id)
        {
            DataTable dt = mycode.FillData("select *,format(Start_Period, 'dd/MM/yyyy') as Start_Period1,format(End_Period, 'dd/MM/yyyy') as End_Period1  from dbo.[PRL_Staff_Leave_Setup] where Id=" + id + "");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                ddl_gradename.SelectedValue = dt.Rows[0]["Grade_id"].ToString();

                txt_start_period.Text = dt.Rows[0]["Start_Period1"].ToString();
                txt_end_period.Text = dt.Rows[0]["End_Period1"].ToString();

                txt_leave_id.SelectedValue = dt.Rows[0]["Leave_id"].ToString();
                ddl_leave_type.Text = dt.Rows[0]["Leave_Type"].ToString();
                //ddl_no_of_leave.Text=dr[6].ToString();
                txt_leave_applied.Text = dt.Rows[0]["No_of_Leave"].ToString();
                ddl_leave_unit.Text = dt.Rows[0]["leave_applied"].ToString();
                txt_days_worked_in_month.Text = dt.Rows[0]["Days_Worked_in_month"].ToString();
                txt_earned_leave.Text = dt.Rows[0]["Earned_Leave"].ToString();

                ddl_treatement_of_leave_to_next_year.Text = dt.Rows[0]["Treatement_of_Leave_to_Next_year"].ToString();
                txt_max_leave_cf.Text = dt.Rows[0]["Max_Leave_CF"].ToString();

                if (Convert.ToBoolean(dt.Rows[0]["Leave_CF_in_next_year"].ToString()) == true)
                {
                    chk_cf_n_year_Checked_1.Checked = true;
                }
                else
                {
                    chk_cf_n_year_Checked_1.Checked = false;
                }




                btn_add.Text = "Update";
                btn_cancel.Visible = true;
                HdID.Value = id;


            }
        }

        protected void ddl_gradename_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_gradename.SelectedItem.Text == "Select")
            {
                Alertme("Please select grade name", "warning");
            }
            else
            {
                bind_grd_view();
            }
        }

    }
}