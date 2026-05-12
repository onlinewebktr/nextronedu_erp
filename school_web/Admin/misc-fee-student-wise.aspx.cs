using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class misc_fee_student_wise : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_session_st, "select Session,session_id from dbo.[session_details]");
                        ViewState["sessionid"] = My.get_session_id();
                        ddl_session_st.SelectedValue = ViewState["sessionid"].ToString();
                        //ddl_month.DataSource = My.bindMonthName();
                        //ddl_month.DataBind();
                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");
                        ddl_month.SelectedValue = mycode.get_current_month_id();

                        My.bind_ddl_select(ddl_misc_fee_Type, "select distinct Fee_type from Misc_fee_master order by Fee_type asc");
                        ddl_ledger_st.DataSource = new string[] { "School", "Hostel" };
                        ddl_ledger_st.DataBind();
                        ddl_ledger_st.SelectedIndex = 0;

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
            DataTable dt = mycode.FillData("select * from Misc_Fee_Master_Studentwise where Session_id=" + ViewState["sessionid"].ToString() + " and Old_year_Dues_Type is null");
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
            empty_form();
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please Enter Admission No", "warning");
                txt_admission_no.Focus();
                return;
            }
            if (ddl_month.SelectedItem.Text == "Select")
            {
                Alertme("Please Select Month", "warning");
                ddl_month.Focus();
                return;
            }
            if (ddl_session_st.Text == "Select")
            {
                Alertme("Please Select Session", "warning");
                ddl_session_st.Focus();
                return;
            }
            if (ddl_misc_fee_Type.Text == "Select")
            {
                Alertme("Please select fee type", "warning");
                ddl_misc_fee_Type.Focus();
                return;
            }
            if (txt_amount_st.Text == "")
            {
                Alertme("Please Enter Amount", "warning");
                txt_amount_st.Focus();
                return;
            }
            if (ddl_ledger_st.Text == "")
            {
                Alertme("Please Select Ledger", "warning");
                ddl_ledger_st.Focus();
                return;
            }
            if (ViewState["is_valid_adno"].ToString() == "false")
            {
                Alertme("Please Enter Valid Admission No", "warning");
                txt_admission_no.Focus();
                return;
            }


            if (btn_Submit.Text == "Add")
            {
                submit_details();
                empty_form();
                bind_grd_view();
            }
            else
            {
                update_update_details();
                empty_form();
                bind_grd_view();
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Misc_Fee_Master_Studentwise where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = txt_admission_no.Text;
                dr[2] = ddl_month.SelectedItem.Text;
                dr[3] = ddl_session_st.SelectedItem.Text;
                dr[4] = ddl_session_st.SelectedValue;
                dr[5] = ddl_misc_fee_Type.Text;
                dr[6] = txt_amount_st.Text;
                dr["Ledger"] = ddl_ledger_st.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update " + txt_amount_st.Text + " rs as misc fee master of " + txt_name.Text + "( Admission No :- " + txt_admission_no.Text + " ) of " + ddl_month.SelectedItem.Text + ", session " + ddl_session_st.Text);
            Alertme("Studentwise Misc Fee Updated Successfully.", "success");
        }


        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Misc_Fee_Master_Studentwise where Admission_No='" + txt_admission_no.Text + "' and Month='" + ddl_month.SelectedItem.Text + "' and Session_id='" + ddl_session_st.SelectedValue + "' and Perticular='" + ddl_misc_fee_Type.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_admission_no.Text;
                dr[2] = ddl_month.SelectedItem.Text;
                dr[3] = ddl_session_st.SelectedItem.Text;
                dr[4] = ddl_session_st.SelectedValue;
                dr[5] = ddl_misc_fee_Type.Text;
                dr[6] = txt_amount_st.Text;
                dr["Ledger"] = ddl_ledger_st.Text;
                dt.Rows.Add(dr);
                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " add " + txt_amount_st.Text + " rs as misc fee against " + txt_name.Text + "( Admission No :- " + txt_admission_no.Text + " ) in " + ddl_month.SelectedItem.Text + ", session " + ddl_session_st.Text);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = txt_admission_no.Text;
                    dr[2] = ddl_month.SelectedItem.Text;
                    dr[3] = ddl_session_st.SelectedItem.Text;
                    dr[4] = ddl_session_st.SelectedValue;
                    dr[5] = ddl_misc_fee_Type.Text;
                    dr[6] = txt_amount_st.Text;
                    dr["Ledger"] = ddl_ledger_st.Text;
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update " + txt_amount_st.Text + " rs as misc fee master of " + txt_name.Text + "( Admission No :- " + txt_admission_no.Text + " ) of " + ddl_month.SelectedItem.Text + ", session " + ddl_session_st.Text);
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Studentwise Misc Fee Added Successfully", "success");
        }

        private void empty_form()
        {
            //txt_admission_no.Text = "";
            txt_amount_st.Text = "";
            //txt_class.Text = "";
            //txt_section.Text = "";
            //txt_roll.Text = "";
            //txt_name.Text = "";
            ViewState["is_valid_adno"] = "false";
            txt_amount_st.Focus();
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
                Label lbl_month = (Label)row.FindControl("lbl_month");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");

                Label lbl_perticular = (Label)row.FindControl("lbl_perticular");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");
                Label lbl_ledger = (Label)row.FindControl("lbl_ledger");

                hd_id.Value = lbl_Id.Text;
                txt_admission_no.Text = lbl_admission_no.Text;
                ddl_session_st.SelectedValue = lbl_session_id.Text;
                find_student_details();

                ddl_month.SelectedValue = My.get_month_id_from_month_name(lbl_month.Text);
                ddl_misc_fee_Type.Text = lbl_perticular.Text;
                txt_amount_st.Text = lbl_amount.Text;
                ddl_ledger_st.Text = lbl_ledger.Text;

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
            Label lbl_amount = (Label)row.FindControl("lbl_amount");

            Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
            Label lbl_month = (Label)row.FindControl("lbl_month");
            Label lbl_session = (Label)row.FindControl("lbl_session");

            My.exeSql("delete from Misc_Fee_Master_Studentwise where Id='" + lbl_Id.Text + "'");
            My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " delete " + lbl_amount.Text + " rs as misc fee master of ( Admission No :- " + lbl_admission_no.Text + " ) of " + lbl_month.Text + ", session " + lbl_session.Text);
            Alertme("Studentwise Misc Fee deleted Successfully", "success");
            bind_grd_view();
        }

        protected void txt_admission_no_TextChanged(object sender, EventArgs e)
        {
            if (txt_admission_no.Text != "")
            {
                if (ddl_session_st.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session_st.Focus();
                }
                else
                {
                    find_student_details();
                }
            }
            else
            {
                Alertme("Please enter admission no.", "warning");
                txt_admission_no.Focus();
            }
        }


        bool is_valid_adno = false;
        private void find_student_details()
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session_st.SelectedValue + "' and StudentStatus='AV' and Is_TC_Taken!='true' and Status='1'  order by id asc", My.con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                txt_class.Text = "";
                txt_section.Text = "";
                txt_roll.Text = "";
                txt_name.Text = "";
                is_valid_adno = false;
                ViewState["is_valid_adno"] = "false";
                return;
            }
            else
            {
                is_valid_adno = true;
                ViewState["is_valid_adno"] = "true";
                foreach (DataRow dr in dt.Rows)
                {
                    txt_class.Text = dr["class"].ToString();
                    txt_section.Text = dr["Section"].ToString();
                    txt_roll.Text = dr["rollnumber"].ToString();
                    txt_name.Text = dr["studentname"].ToString();

                    try
                    {
                        Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());
                        ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                        if (ViewState["Hostel_id"].ToString() == "0")
                        {
                            ddl_ledger_st.Text = "School";
                        }
                        else
                        {
                            ddl_ledger_st.Text = "Hostel";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        protected void btn_find_studnt_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text != "")
            {
                if (ddl_session_st.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session_st.Focus();
                }
                else
                {
                    find_student_details();
                }
            }
            else
            {
                Alertme("Please enter admission no.", "warning");
                txt_admission_no.Focus();
            }
        }

        protected void btn_add_fee_head_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_fee_head.Text == "")
                {
                    Alertme("Please enter fee head name.", "warning");
                    txt_fee_head.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalHead();", true);
                }
                else
                {
                    DataTable dt = My.dataTable("select Fee_type from Misc_fee_master where Fee_type='" + txt_fee_head.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        My.exeSql("insert into Misc_fee_master(Fee_type) values ('" + txt_fee_head.Text + "');");
                        My.bind_ddl_select(ddl_misc_fee_Type, "select distinct Fee_type from Misc_fee_master order by Fee_type asc");
                        try
                        {
                            ddl_misc_fee_Type.Text = txt_fee_head.Text;
                            ddl_misc_fee_Type.Focus();
                            txt_fee_head.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                        Alertme("Fee head has been added successfully.", "success");
                    }
                    else
                    {
                        try
                        {
                            ddl_misc_fee_Type.Text = txt_fee_head.Text;
                            ddl_misc_fee_Type.Focus();
                            txt_fee_head.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                        Alertme("Fee head with this name already exists.", "success");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}