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
    public partial class Update_Class_and_Admission_No : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_name, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_name.SelectedValue = ddl_session.SelectedValue;
                        Session["classchange"] = "2";
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Update_Class_and_Admission_No.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
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




        #region find student data
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_data();

                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_data()
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please enter  current admission no.", "warning");
            }
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and StudentStatus='AV'  and  Status='1'    ";
                find_details(query);
            }
        }

        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                pnl_change_admission_no.Visible = false;
                pnl_change_class.Visible = false;
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    std_basic_infoS.Visible = true;
                    txt_newrollno.Text = dr["rollnumber"].ToString();

                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";
                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["admissionserialnumber"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["id"] = dr["id"].ToString();
                    lbl_old_roll_no.Text = dr["rollnumber"].ToString();


                    mycode.bind_ddl(ddl_section, "select distinct Section from section_master order by Section asc");
                    mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table where course_id!='" + ViewState["classid"].ToString() + "' order by Position");

                    bind_payment_history();

                }

            }
        }

        private void bind_payment_history()
        {
            string query = "  select t1.Date,t1.Slip_no,t1.mode,t1.Amount,t1.Type,t1.Date from Student_Payment_History t1    where     t1.Class_id='" + ViewState["classid"].ToString() + "' and t1.Session='" + ViewState["session"].ToString() + "' and   t1.Addmission_no='" + ViewState["admissionserialnumber"].ToString() + "'  order by t1.Idate desc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_msg.Text = "There are no payment history found";

                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {

                lbl_msg.Text = "";
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
            }
        }
        #endregion

        protected void rd_change_admission_no_CheckedChanged(object sender, EventArgs e)
        {

            pnl_change_admission_no.Visible = true;
            pnl_change_class.Visible = false;
        }

        protected void rd_change_class_CheckedChanged(object sender, EventArgs e)
        {
            pnl_change_admission_no.Visible = false;
            pnl_change_class.Visible = true;
            txt_new_reg_class.Text = lbl_admission_no.Text;
        }

        #region update admission No.
        protected void btn_update_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admissionno_new.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
            }
            else
            { 
                bool newadmission_no = get_admission_no_status(txt_admissionno_new.Text);
                if (newadmission_no == true)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "sp_update_admission_no_and_Class";
                    cmd.Parameters.AddWithValue("@admissionserialnumber_old", lbl_admission_no.Text);
                    cmd.Parameters.AddWithValue("@admissionserialnumber", txt_admissionno_new.Text.Trim());
                    cmd.Parameters.AddWithValue("@session", ViewState["session"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@updated_date", My.getdate1());
                    cmd.Parameters.AddWithValue("@status", "updateadmissionno");
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    if (UsesCode.InsertUpdateData_sp(cmd))
                    {
                        #region subject assined auto 
                        try
                        {
                            payments.student_subject_mapping_no_transaction(ViewState["sessionIDs"].ToString(), ViewState["session"].ToString(), ViewState["classid"].ToString(), txt_admissionno_new.Text, txtsection.Text, ViewState["Branch_id"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        Alertme("Admission no has been successfully changed", "success");
                        pnl_change_admission_no.Visible = false;
                        std_basic_infoS.Visible = false;
                        pnl_change_class.Visible = false;
                        rd_change_admission_no.Checked = false;
                        rd_change_class.Checked = false;
                        txt_admissionno_new.Text = "";
                    }
                }
                else
                {
                    Alertme("Sorry your new admission no. is already exist. ", "warning");
                }
            }
        }

        private bool get_admission_no_status(string regid)
        {
            DataTable dt = mycode.FillData("Select admissionserialnumber  from admission_registor  where admissionserialnumber='" + regid + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        #endregion

        protected void btn_update_class_Click(object sender, EventArgs e)
        {
            if (txt_new_reg_class.Text == "")
            {
                Alertme("Please enter student registration", "warning"); 
            }
            else if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class ", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section ", "warning");
            }
            else
            {
                try
                {
                    final_chnage_class();
                    rd_change_admission_no.Checked = false;
                    rd_change_class.Checked = false;
                }
                catch
                {

                }
            }
        }

        private void final_chnage_class()
        {
            bool chekdublicate_rgid = get_admission_no_status_new(txt_new_reg_class.Text);
            if (chekdublicate_rgid != true)
            {
                Alertme("Sorry your new admission no. is allready exist. Please ", "warning");
            } 
            else
            {
                string slip_no = My.chnage_classuniqno(); //My.auto_serial("slip_no");  
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_update_admission_no_and_Class";
                cmd.Parameters.AddWithValue("@admissionserialnumber_old", lbl_admission_no.Text);
                cmd.Parameters.AddWithValue("@admissionserialnumber", txt_new_reg_class.Text);
                cmd.Parameters.AddWithValue("@session", ViewState["session"].ToString());
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                cmd.Parameters.AddWithValue("@updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@updated_date", My.getdate1());
                cmd.Parameters.AddWithValue("@slip_no", slip_no);
                cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                cmd.Parameters.AddWithValue("@Class_idnew", ddlclass.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_name", ddlclass.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@rollno_old", lbl_old_roll_no.Text);
                cmd.Parameters.AddWithValue("@Roll_no_New", txt_newrollno.Text);
                cmd.Parameters.AddWithValue("@Old_section", txtsection.Text);
                cmd.Parameters.AddWithValue("@New_Section", ddl_section.SelectedValue);
                cmd.Parameters.AddWithValue("@status", "updateclass");
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                if (UsesCode.InsertUpdateData_sp(cmd))
                {
                    #region subject assined auto 
                    try
                    {
                        payments.student_subject_mapping_no_transaction(ViewState["sessionIDs"].ToString(), ViewState["session"].ToString(), ddlclass.SelectedValue, txt_new_reg_class.Text, ddl_section.Text, ViewState["Branch_id"].ToString());
                    }
                    catch
                    {
                    }
                    #endregion
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ddlclass.SelectedValue, txt_new_reg_class.Text, "0", "0", con);
                    con.Close();
                    Alertme("Admission no has been successfully changed", "warning");
                    pnl_change_admission_no.Visible = false;
                    pnl_change_class.Visible = false;
                    string path = "slip/Print_Change_Class_Payment.aspx?admissionno=" + txt_new_reg_class.Text + "&Slip_no=" + slip_no + "&sessionid=" + ViewState["sessionIDs"].ToString();
                    Response.Redirect(path, false);
                }
            }
        }

        private bool chek_rollno()
        {
            DataTable dt = mycode.FillData("Select admissionserialnumber  from admission_registor  where rollnumber='" + txt_newrollno.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and id!=" + ViewState["id"].ToString() + "");
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private bool get_admission_no_status_new(string new_reg)
        {
            DataTable dt = mycode.FillData("Select admissionserialnumber  from admission_registor  where admissionserialnumber='" + new_reg + "' and id!=" + ViewState["id"].ToString() + " and Session_id='" + My.get_session_id() + "'");
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        double total = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_payable.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totalamount");

                lbl_totaldiscount.Text = total.ToString("0.00");
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




        protected void btn_find_by_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_name.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }

                string stdname = txt_student_name.Text.Trim();
                string query = "select * from admission_registor where studentname like '%" + stdname + "%' and session='" + ddl_session_name.SelectedItem.Text + "' and  Status='1' order by id asc";



                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else if (dt.Rows.Count == 1)
                {
                    query = "select * from admission_registor where admissionserialnumber='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Session='" + ddl_session_name.SelectedItem.Text + "' order by studentname asc";
                    find_details(query);
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "studentInfo();", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and  Status='1'    ";

                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
    }
}