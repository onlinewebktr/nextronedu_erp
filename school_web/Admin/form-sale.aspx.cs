using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class form_sale : System.Web.UI.Page
    {
        compLN comP = new compLN();
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
                        if (Session["msG"] != null)
                        {
                            Alertme(Session["msG"].ToString(), "success");
                            Session["msG"] = null;
                        }
                        try
                        {
                            My.exeSql("ALTER TABLE Form_sale_details ADD Second_Language varchar (500);");

                            My.exeSql("ALTER TABLE Firm_Details ADD Is_Exam_date_show_Form_sale int;");
                            My.exeSql("ALTER TABLE Form_sale_details ADD Exam_date varchar (500);");
                            My.exeSql("update Firm_Details set Is_Exam_date_show_Form_sale=0");

                            My.exeSql("ALTER TABLE Form_sale_details ADD Sub_location varchar (500);");


                        }
                        catch
                        {

                        }


                        ViewState["IsFromEnq"] = "0";
                        txt_date.Text = mycode.date();
                        Session["pagew"] = "2";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        txt_s_date.Text = mycode.sevendaysbackseven();
                        txt_e_date.Text = mycode.date();
                        hd_user_Type.Value = My.get_user_type(ViewState["Userid"].ToString());
                        Session["userTypeFee"] = hd_user_Type.Value;

                        try
                        {
                            update_session_class();


                            DataTable dtBnk1 = My.dataTable("select * from bank_details order by bank_name asc");
                            if (dtBnk1.Rows.Count > 0)
                            {
                                ddl_bank.DataSource = dtBnk1;
                                ddl_bank.DataTextField = "bank_name";
                                ddl_bank.DataBind();
                                ddl_bank.Items.Insert(0, new ListItem("Select", "Select"));

                            }
                            else
                            {
                                DataTable dtBnk = My.dataTable("select * from BANK_MASTER where Status='1' order by Bank_name asc");
                                if (dtBnk.Rows.Count > 0)
                                {
                                    ddl_bank.DataSource = dtBnk;
                                    ddl_bank.DataTextField = "Bank_name";
                                    ddl_bank.DataBind();
                                    ddl_bank.Items.Insert(0, new ListItem("Select", "Select"));
                                }
                                else
                                {
                                    compLN.bind_ddl_select(ddl_bank, "select Bank_name from Bank_master order by Bank_name asc");
                                }
                            }
                        }
                        catch
                        {
                        }




                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        string formSaleSession = My.get_single_column_data("select top 1 session_id as Column_Name from session_details where Form_sale_active=1");
                        My.bind_ddl_noselect(ddl_nationality, "select Country_name from Country_list");

                        comP.bind_ddl(ddl_student_mother_tongue, "select Language from Language_Master order by Language asc");

                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");

                        My.bind_ddl_noselect(ddl_cunterycode3, "select Country_code from Country_list order by Country_name asc");
                        ddl_cunterycode3.SelectedValue = "+91";
                        bind_c_country();
                        try
                        {
                            ddl_session.SelectedValue = formSaleSession;
                        }
                        catch (Exception ex)
                        {
                        }
                        mycode.bind_all_ddl_with_id(ddl_class, "select Course_Name,course_id from Add_course_table order by Position");
                        //My.bind_ddl_select(ddl_class, "select Course_Name from Add_course_table order by Position");
                        My.bind_ddl_select(ddl_class_sb1, "select Course_Name from Add_course_table order by Position");
                        My.bind_ddl_select(ddl_class_sb2, "select Course_Name from Add_course_table order by Position");

                        My.bind_ddl_select(ddl_prv_class_from1, "select Course_Name from Add_course_table order by Position");
                        My.bind_ddl_select(ddl_prv_class_to1, "select Course_Name from Add_course_table order by Position");
                        My.bind_ddl_select(ddl_prv_class_from2, "select Course_Name from Add_course_table order by Position");
                        My.bind_ddl_select(ddl_prv_class_to2, "select Course_Name from Add_course_table order by Position");
                        mycode.bind_ddl(ddl_f_education, "select Name from Qualification_master");
                        mycode.bind_ddl(ddl_m_education, "select Name from Qualification_master");
                        mycode.bind_ddl(ddl_g_education, "select Name from Qualification_master");
                        string stateName = My.get_state_name();
                        My.bind_ddl_select(ddl_temp_state, "select State from dbo.[StateList] order by State asc");
                        try
                        {
                            ddl_temp_state.Text = stateName;
                        }
                        catch (Exception ex)
                        {
                        }
                        ViewState["flag"] = "0";
                        bind_formamount();
                        get_prifix();
                        if (ViewState["Is_index_no_auto_create"].ToString() == "1")
                        {
                            get_index_no();
                        }
                        txt_indesx_no.Focus();
                        bind_grd_view();


                        if (Request.QueryString["enqid"] != null) //======================================TransferFormEnquiry
                        {
                            ViewState["IsFromEnq"] = "1";
                            ViewState["FromEnqId"] = Request.QueryString["enqid"].ToString();
                            BindStdEnqDetails(ViewState["FromEnqId"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_form_Master");
            }
        }

        private void BindStdEnqDetails(string enquiry_id)
        {
            DataTable dt = mycode.FillData("select * from Enquiry_Details where Enquiry_Id='" + enquiry_id + "'");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                }
                catch (Exception ex) { }
                txt_student_first_name.Text = dt.Rows[0]["Name"].ToString();
                txt_mobile.Text = dt.Rows[0]["Phone"].ToString();
                txt_address.Text = dt.Rows[0]["Address"].ToString();
                txt_district.Text = dt.Rows[0]["districtname"].ToString();
                txt_pincode.Text = dt.Rows[0]["Pincode"].ToString();
                txt_fathers_first_name.Text = dt.Rows[0]["Father_name"].ToString();
                try
                {
                    ddl_temp_state.SelectedValue = dt.Rows[0]["statecode"].ToString();
                }
                catch (Exception ex) { }
            }
        }

        private void get_index_no()
        {
            try
            {
                DataTable dt = mycode.FillData("select Form_sale_index_no from session_details where session_id='" + ddl_session.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    string sl_no = dt.Rows[0]["Form_sale_index_no"].ToString();
                    if (sl_no == "")
                    {
                        sl_no = increase_index_sl_no();
                    }
                    check_dup_index_no(sl_no);
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void check_dup_index_no(string admNo)
        {
            bool duplicate = true;
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select form_indesx_no from Form_sale_details where form_indesx_no='" + admNo + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    txt_indesx_no.Text = admNo;
                    duplicate = false;
                }
                else
                {
                    string admNoss = increase_index_sl_no();
                    DataTable dt = mycode.FillData("select Form_sale_index_no from session_details where session_id='" + ddl_session.SelectedValue + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string sl_no = dt.Rows[0]["Form_sale_index_no"].ToString();
                        admNo = sl_no;
                    }
                }
            }
        }
        private string increase_index_sl_no()
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from session_details where  session_id='" + ddl_session.SelectedValue + "'", My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["Form_sale_index_no"].ToString() == "")
                        {
                            dr["Form_sale_index_no"] = "1";
                        }
                        else
                        {
                            dr["Form_sale_index_no"] = Convert.ToDouble(dr["Form_sale_index_no"]) + 1;
                        }
                        result = dr["Form_sale_index_no"].ToString();
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(ds.Tables[0]);
            }
            catch (Exception e)
            {
            }
            return result;
        }

        private void update_session_class()
        {
            DataTable dt = My.dataTable("select  * from Form_sale_details where (Session_id is null or Session_id='')");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string session_id = My.get_single_column_data("select top 1 session_id as Column_Name from session_details where Session='" + dr["session"].ToString() + "'");
                    My.exeSql("update Form_sale_details set Session_id='" + session_id + "' where Id='" + dr["Id"].ToString() + "'");
                }
            }


            //===================
            DataTable dtc = My.dataTable("select  * from Form_sale_details where (Class_id is null or Class_id='')");
            if (dtc.Rows.Count > 0)
            {
                foreach (DataRow drc in dtc.Rows)
                {
                    string class_id = My.get_single_column_data("select top 1 course_id as Column_Name from Add_course_table where Course_Name='" + drc["class"].ToString() + "'");
                    My.exeSql("update Form_sale_details set Class_id='" + class_id + "' where Id='" + drc["Id"].ToString() + "'");
                }
            }
        }

        private void bind_c_country()
        {
            My.bind_ddl_noselect(ddl_country_c, "select Country_name from Country_list");
            if (ddl_cunterycode3.SelectedValue == "+91")
            {
                txt_c_state.Visible = false;
                ddl_temp_state.Visible = true;
            }
            else
            {
                txt_c_state.Visible = true;
                ddl_temp_state.Visible = false;
            }
        }
        private void find_firm_details()
        {
            ViewState["isexamdate"] = "0";
            ViewState["IsGuardianMandtry"] = "0";
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                try
                {
                    if (dt.Rows[0]["Is_Exam_date_show_Form_sale"].ToString() == "1")
                    {
                        ViewState["isexamdate"] = "1";
                        doe.Visible = true;
                    }
                    else
                    {
                        ViewState["isexamdate"] = "0";
                        doe.Visible = false;
                    }

                }
                catch
                {
                    ViewState["isexamdate"] = "0";
                    doe.Visible = false;

                }


                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

                try
                {
                    if (dt.Rows[0]["Form_sale_guardian_mandatory"].ToString() == "True")
                    {
                        txt_gaurdian_first_name.CssClass = "form-control mandatory";
                        guardianNameMn.Visible = true;
                        ViewState["IsGuardianMandtry"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    txt_remarks.Text = dt.Rows[0]["Form_sale_remark"].ToString();
                }
                catch (Exception ex)
                {
                }

                try
                {
                    if (hd_user_Type.Value == "Admin")
                    {
                        txt_date.Enabled = true;
                        txt_date.CssClass = "form-control";
                        payDatetxtbx.Attributes.Add("class", "");
                        payDatetxtbxDVS.Attributes.Add("class", "col-md-3");
                    }
                    else
                    {
                        if (dt.Rows[0]["Is_user_change_pay_date"].ToString() == "True")
                        {
                            txt_date.Enabled = false;
                            txt_date.CssClass = "form-control";
                            payDatetxtbx.Attributes.Add("class", "noclick");
                            payDatetxtbxDVS.Attributes.Add("class", "col-md-3 noclick");
                        }
                        else
                        {
                            txt_date.Enabled = true;
                            txt_date.CssClass = "form-control";
                            payDatetxtbx.Attributes.Add("class", "");
                            payDatetxtbxDVS.Attributes.Add("class", "col-md-3");
                        }
                    }
                }
                catch (Exception ex)
                {
                }


                try
                {
                    ViewState["Form_sale_slip_type"] = "A4";
                    if (dt.Rows[0]["Is_form_sale_slip_a5"].ToString() == "True")
                    {
                        ViewState["Form_sale_slip_type"] = "A5";
                    }
                }
                catch (Exception ex)
                {
                    ViewState["Form_sale_slip_type"] = "A4";
                }


                try
                {
                    ViewState["Is_index_no_auto_create"] = "0";
                    if (dt.Rows[0]["Is_form_sale_index_no_auto_create"].ToString() == "True")
                    {
                        ViewState["Is_index_no_auto_create"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    ViewState["Form_sale_prefix"] = dt.Rows[0]["Form_sale_prefix"].ToString();
                    if (dt.Rows[0]["Form_sale_prefix"].ToString() == "" || dt.Rows[0]["Form_sale_prefix"] == null)
                    {
                        ViewState["Form_sale_prefix"] = "0";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                bind_formamount();
                fetch_form_sale_fee();
                get_prifix();
                bind_grd_view();
            }
        }

        private void get_prifix()
        {
            try
            {
                DataTable dt = mycode.FillData("select Form_Seal_Prefix from Firm_Details");
                if (dt.Rows.Count > 0)
                {
                    ViewState["prifixdate"] = dt.Rows[0]["Form_Seal_Prefix"].ToString() + "/" + ddl_session.SelectedItem.Text + "/";
                }
            }
            catch
            {
                ViewState["prifixdate"] = "0";
            }
        }

        private void bind_formamount()
        {
            DataTable dt = mycode.FillData("select Amount from Form_Sale_Fee where session='" + ddl_session.SelectedItem.Text + "'");
            if (dt.Rows.Count == 0)
            {
                txt_amount.Text = "0";
            }
            else
            {
                txt_amount.Text = dt.Rows[0][0].ToString();
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
            DataTable dt = mycode.FillData("select * from Form_sale_details where Session_id='" + ddl_session.SelectedValue + "'");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
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
            bool send = false;
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
                ddl_session.Focus();
                return;
            }
            if (txt_indesx_no.Text == "")
            {
                Alertme("Please Enter index no.", "warning");
                txt_indesx_no.Focus();
                return;
            }
            if (ddl_class.Text == "")
            {
                Alertme("Please select class.", "warning");
                ddl_class.Focus();
                return;
            }
            if (txt_date.Text == "")
            {
                Alertme("Please Enter Date", "warning");
                txt_date.Focus();
                return;
            }

            if (txt_student_first_name.Text == "")
            {
                Alertme("Please Enter Student Name", "warning");
                txt_student_first_name.Focus();
                return;
            }

            //if (ddl_cast.SelectedItem.ToString() == "Select")
            //{
            //    Alertme("Please Select category.", "warning");
            //    ddl_cast.Focus();
            //    return;
            //}




            if (txt_fathers_first_name.Text == "")
            {
                Alertme("Please enter father name.", "warning");
                txt_fathers_first_name.Focus();
                return;
            }
            if (ViewState["IsGuardianMandtry"].ToString() == "1")
            {
                if (txt_gaurdian_first_name.Text == "")
                {
                    Alertme("Please enter guardian Name.", "warning");
                    txt_gaurdian_first_name.Focus();
                    return;
                }
            }
            if (ddl_nationality.SelectedItem.Text == "Select")
            {
                Alertme("Please select nationality.", "warning");
                ddl_nationality.Focus();
                return;
            }

            if (ddl_gender.SelectedItem.ToString() == "Select")
            {
                Alertme("Please Select gender", "warning");
                ddl_gender.Focus();
                return;
            }
            if (txt_mobile.Text == "")
            {
                Alertme("Please enter mobile no.", "warning");
                txt_mobile.Focus();
                return;
            }


            if (txt_amount.Text == "")
            {
                Alertme("Please Enter Amount", "warning");
                txt_amount.Focus();
                return;
            }

            if (ViewState["isexamdate"].ToString() == "1")
            {
                if (txt_date_of_exam.Text == "")
                {
                    Alertme("Please enter date of exam", "warning");
                    txt_date_of_exam.Focus();

                    return;

                }
            }


            //if (txt_mothers_first_name.Text == "")
            //{
            //    Alertme("Please enter mother name.", "warning");
            //    txt_mothers_first_name.Focus();
            //    return;
            //}

            //if (txt_address.Text == "")
            //{
            //    Alertme("Please enter address.", "warning");
            //    txt_address.Focus();
            //    return;
            //}

            //if (txt_po.Text == "")
            //{
            //    Alertme("Please enter P.O.", "warning");
            //    txt_po.Focus();
            //    return;
            //}
            //if (txt_ps.Text == "")
            //{
            //    Alertme("Please enter P.S.", "warning");
            //    txt_ps.Focus();
            //    return;
            //}
            //if (txt_district.Text == "")
            //{
            //    Alertme("Please enter district.", "warning");
            //    txt_district.Focus();
            //    return;
            //}
            //if (txt_city.Text == "")
            //{
            //    Alertme("Please enter city.", "warning");
            //    txt_city.Focus();
            //    return;
            //}
            //if (ddl_temp_state.Text == "")
            //{
            //    Alertme("Please select state.", "warning");
            //    ddl_temp_state.Focus();
            //    return;
            //}
            //if (txt_pincode.Text == "")
            //{
            //    Alertme("Please enter pincode.", "warning");
            //    txt_pincode.Focus();
            //    return;
            //}
            //if (txt_temp_mobileno.Text == "")
            //{
            //    Alertme("Please enter mobile no.", "warning");
            //    txt_temp_mobileno.Focus();
            //    return;
            //}
            else if (ddl_paymentmode.Text == "Select")
            {
                Alertme("Please select mode of payment.", "warning");
                ddl_paymentmode.Focus();
                return;
            }
            else
            {
                if (ddl_paymentmode.Text != "Cash")
                {
                    if (ddl_bank.SelectedItem.Text == "Select")
                    {
                        ddl_bank.Focus();
                        Alertme("Please select bank name", "warning");
                        return;
                    }
                    if (txt_trans_no.Text == "")
                    {
                        txt_trans_no.Focus();
                        Alertme("Please enter transaction No.", "warning");
                        return;
                    }
                }
            }

            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    string recptno = My.auto_serialS("form_sale_recptno");
                    if (ViewState["Form_sale_prefix"].ToString() == "0" || ViewState["Form_sale_prefix"] == null)
                    { }
                    else
                    {
                        recptno = ViewState["Form_sale_prefix"].ToString() + recptno;
                    }
                    submit_details(recptno);

                    // My.auto_serialS("Form_No");
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " sale a registration form Index : " + txt_indesx_no.Text + " in the session " + ddl_session.SelectedItem.Text);
                    empty_form();
                    if (ViewState["flag"].ToString() == "0")
                    {
                        bind_grd_view();
                    }
                    if (ViewState["flag"].ToString() == "1")
                    {
                        find_by_date();
                    }
                    //form_sale_slip child = new form_sale_slip(recptno);
                    //MyDialog m = new MyDialog();
                    //child.Tag = m;
                    //m.Show(child, this);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_update_details();
                    empty_form();
                    if (ViewState["flag"].ToString() == "0")
                    {
                        bind_grd_view();
                    }
                    if (ViewState["flag"].ToString() == "1")
                    {
                        find_by_date();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }

        }



        private void submit_details(string recptno)
        {

            SqlDataAdapter ad = new SqlDataAdapter("select * from Form_sale_details where form_indesx_no='" + txt_indesx_no.Text + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = "";
                dr[2] = txt_student_first_name.Text + " " + txt_student_middle_name.Text + " " + txt_student_last_name.Text;
                dr[3] = txt_fathers_first_name.Text + " " + txt_fathers_middle_name.Text + " " + txt_fathers_last_name.Text;
                dr[4] = txt_mothers_first_name.Text + " " + txt_mothers_middle_name.Text + " " + txt_mothers_last_name.Text;
                dr["Guardian_name"] = txt_gaurdian_first_name.Text + " " + txt_gaurdian_middle_name.Text + " " + txt_gaurdian_last_name.Text;
                dr[5] = txt_address.Text;
                if (ddl_gender.SelectedItem.Text == "Select")
                {
                    dr[6] = "";
                }
                else
                {
                    dr[6] = ddl_gender.SelectedItem.ToString();
                }
                if (ddl_cast.SelectedItem.Text == "Select")
                {
                    dr[7] = "";
                }
                else
                {
                    dr[7] = ddl_cast.SelectedItem.ToString();
                }
                dr[8] = txt_mobile.Text;
                dr[9] = txt_date.Text;
                dr[10] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                dr[11] = My.toDouble(txt_amount.Text).ToString("0.00");
                dr[12] = ViewState["Userid"].ToString();
                dr[13] = txt_dob.Text;
                dr[14] = recptno;
                dr[15] = ddl_class.SelectedItem.Text;
                dr[16] = ddl_session.SelectedItem.Text;
                dr["Session_id"] = ddl_session.SelectedValue;
                dr["Class_id"] = ddl_class.SelectedValue;
                dr["Remarks"] = txt_remarks.Text;

                dr["form_indesx_no"] = txt_indesx_no.Text;
                dr["Student_first_name"] = txt_student_first_name.Text;
                dr["Student_middle_name"] = txt_student_middle_name.Text;
                dr["Student_last_name"] = txt_student_last_name.Text;
                if (ddl_nationality.Text == "Select")
                {
                    dr["Nationality"] = "";
                }
                else
                {
                    dr["Nationality"] = ddl_nationality.Text;
                }
                if (ddl_blood_group.Text == "Select")
                {
                    dr["Blood_group"] = "";
                }
                else
                {
                    dr["Blood_group"] = ddl_blood_group.Text;
                }

                dr["Height"] = txt_height.Text;
                dr["Weight"] = txt_weight.Text;
                if (ddl_religion.Text == "Select")
                {
                    dr["Religion"] = "";
                }
                else
                {
                    dr["Religion"] = ddl_religion.Text;
                }

                dr["Aadhar_no"] = txt_aadhar_no.Text;
                if (ddl_student_mother_tongue.Text == "Select")
                {
                    dr["Mother_tongue"] = "";
                }
                else
                {
                    dr["Mother_tongue"] = ddl_student_mother_tongue.Text;
                }

                dr["Language_spoken_at_home"] = txt_language_spoken_at_home.Text;

                dr["Name_of_sibling1"] = txt_name_of_sibling1.Text;
                dr["Age_of_sibling1"] = txt_age_sibling1.Text;
                dr["School_of_sibling1"] = txt_school_sibling1.Text;

                if (ddl_class_sb1.SelectedItem.Text == "Select")
                {
                    dr["Class_of_sibling1"] = "";
                }
                else
                {
                    dr["Class_of_sibling1"] = ddl_class_sb1.Text;
                }

                dr["Name_of_sibling2"] = txt_name_of_sibling2.Text;
                dr["Age_of_sibling2"] = txt_age_sibling2.Text;
                dr["School_of_sibling2"] = txt_school_sibling2.Text;

                if (ddl_class_sb2.SelectedItem.Text == "Select")
                {
                    dr["Class_of_sibling2"] = "";
                }
                else
                {
                    dr["Class_of_sibling2"] = ddl_class_sb2.Text;
                }
                dr["Prv_school_name1"] = txt_prv_school_name1.Text;
                dr["Prv_school_add1"] = txt_prv_schho_address1.Text;

                if (ddl_prv_class_from1.SelectedItem.Text == "Select")
                {
                    dr["Prv_class_from"] = "";
                }
                else
                {
                    dr["Prv_class_from"] = ddl_prv_class_from1.Text;
                }
                if (ddl_prv_class_to1.SelectedItem.Text == "Select")
                {
                    dr["Prv_class_to1"] = "";
                }
                else
                {
                    dr["Prv_class_to1"] = ddl_prv_class_to1.Text;
                }
                dr["Prv_year_from1"] = txt_prv_school_year_from1.Text;
                dr["Prv_year_to1"] = txt_prv_school_year_to_1.Text;

                if (ddl_prev_board_type.Text.ToUpper() == "SELECT")
                {
                    dr["Prv_board1"] = "";
                }
                else
                {
                    if (ddl_prv_school_board1.SelectedItem.Text == "Select")
                    {
                        dr["Prv_board1"] = "";
                    }
                    else
                    {
                        dr["Prv_board1"] = ddl_prv_school_board1.Text;
                    }
                }
                dr["Prv_medium"] = txt_prv_school_medium.Text;
                dr["Mark_percent1"] = txt_prv_school_marks.Text;

                dr["Prv_school_name2"] = txt_prv_school_name2.Text;
                dr["Prv_school_add2"] = txt_prv_schho_address2.Text;

                if (ddl_prv_class_from2.SelectedItem.Text == "Select")
                {
                    dr["Prv_class_from2"] = "";
                }
                else
                {
                    dr["Prv_class_from2"] = ddl_prv_class_from2.Text;
                }
                if (ddl_prv_class_to2.SelectedItem.Text == "Select")
                {
                    dr["Prv_class_to2"] = "";
                }
                else
                {
                    dr["Prv_class_to2"] = ddl_prv_class_to2.Text;
                }
                dr["Prv_year_from2"] = txt_prv_school_year_from2.Text;
                dr["Prv_year_to2"] = txt_prv_school_year_to_2.Text;

                if (ddl_prev_board_type2.Text.ToUpper() == "SELECT")
                {
                    dr["Prv_board2"] = "";
                }
                else
                {
                    if (ddl_prv_school_board2.SelectedItem.Text == "Select")
                    {
                        dr["Prv_board2"] = "";
                    }
                    else
                    {
                        dr["Prv_board2"] = ddl_prv_school_board2.Text;
                    }

                }

                dr["Prv_medium2"] = txt_prv_school_mediu2.Text;
                dr["Mark_percent2"] = txt_prv_school_mark2.Text;



                //============================
                dr["Father_first_name"] = txt_fathers_first_name.Text;
                dr["Father_middle_name"] = txt_fathers_middle_name.Text;
                dr["Father_last_name"] = txt_fathers_last_name.Text;
                dr["F_aadhar_no"] = txt_fthr_aadhar_no.Text;
                if (ddl_f_education.SelectedItem.Text == "Select")
                {
                    dr["F_qualification"] = "";
                }
                else
                {
                    dr["F_qualification"] = ddl_f_education.Text;
                }
                if (ddl_f_occupation.SelectedItem.Text == "Select")
                {
                    dr["F_occupation"] = "";
                }
                else
                {
                    dr["F_occupation"] = ddl_f_occupation.Text;
                }
                dr["F_annual_income"] = txt_f_annual_income.Text;
                dr["F_contact_no"] = txt_f_contact_no.Text;
                dr["F_email_id"] = txt_f_email_id.Text;


                dr["Mother_first_name"] = txt_mothers_first_name.Text;
                dr["Mother_middle_name"] = txt_mothers_middle_name.Text;
                dr["Mother_last_name"] = txt_mothers_last_name.Text;
                dr["M_aadhar_no"] = txt_m_aadhar_no.Text;
                if (ddl_m_education.SelectedItem.Text == "Select")
                {
                    dr["M_qualification"] = "";
                }
                else
                {
                    dr["M_qualification"] = ddl_m_education.Text;
                }
                if (ddl_m_occupation.SelectedItem.Text == "Select")
                {
                    dr["M_occupation"] = "";
                }
                else
                {
                    dr["M_occupation"] = ddl_m_occupation.Text;
                }
                dr["M_annual_income"] = txt_m_annual_income.Text;
                dr["M_contact_no"] = txt_m_contact_no.Text;
                dr["M_email_id"] = txt_m_email_id.Text;

                dr["Guardian_first_name"] = txt_gaurdian_first_name.Text;
                dr["Guardian_middle_name"] = txt_gaurdian_middle_name.Text;
                dr["Guardian_last_name"] = txt_gaurdian_last_name.Text;
                dr["Guardian_aadhar_no"] = txt_g_aadhar_no.Text;
                if (ddl_g_education.SelectedItem.Text == "Select")
                {
                    dr["Guardian_qualification"] = "";
                }
                else
                {
                    dr["Guardian_qualification"] = ddl_g_education.Text;
                }
                if (ddl_g_occupation.SelectedItem.Text == "Select")
                {
                    dr["Guardian_occupation"] = "";
                }
                else
                {
                    dr["Guardian_occupation"] = ddl_g_occupation.Text;
                }
                dr["Guardian_annual_income"] = txt_g_annual_income.Text;
                dr["Guardian_contact_no"] = txt_g_contact_no.Text;
                dr["Guardian_email_id"] = txt_g_email_id.Text;

                try
                {
                    dr["Prv_board_type1"] = ddl_prev_board_type.Text;
                }
                catch (Exception ex)
                {
                }
                try
                {
                    dr["Prv_board_type2"] = ddl_prev_board_type2.Text;
                }
                catch (Exception ex)
                {
                }
                dr["Post_office"] = txt_po.Text;
                dr["Police_station"] = txt_ps.Text;
                dr["District"] = txt_district.Text;
                dr["City"] = txt_city.Text;
                if (ddl_temp_state.Text == "Select")
                {
                    dr["State"] = "";
                }
                else
                {
                    dr["State"] = ddl_temp_state.Text;
                }

                dr["Pin_code"] = txt_pincode.Text;
                dr["Country"] = ddl_country_c.Text;
                dr["Add_mobile_no"] = txt_temp_mobileno.Text;
                dr["Country_code"] = ddl_cunterycode3.Text;
                dr["Created_by"] = ViewState["Userid"].ToString();
                dr["Created_date"] = mycode.date();
                dr["Created_time"] = mycode.time();
                dr["Created_idate"] = mycode.idate();
                dr["Payment_Mode"] = ddl_paymentmode.Text;
                dr["Bank_name"] = ddl_bank.SelectedItem.Text;
                dr["Bank_tran_no"] = txt_trans_no.Text;
                dr["Alternate_mobile_no"] = txt_alternate_mobile_no.Text;
                if (ddl_Second_Language.Text == "Select")
                {
                    dr["Second_Language"] = "";
                }
                else
                {
                    dr["Second_Language"] = ddl_Second_Language.Text;
                }

                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);

                //
                try
                {
                    mycode.executequery("update Form_sale_details set Exam_date='" + txt_date_of_exam.Text + "'  where form_indesx_no='" + txt_indesx_no.Text + "'");
                }
                catch
                {
                }
                try
                {


                    string Form_No_Generate = My.auto_serialS("Form_No_Generate");
                    string fomno = ViewState["prifixdate"].ToString() + Form_No_Generate;
                    mycode.executequery("update Form_sale_details set Is_Generate=1, Form_no='" + fomno + "' where form_indesx_no='" + txt_indesx_no.Text + "'");

                    try
                    {
                        if (ViewState["IsFromEnq"].ToString() == "1")
                        {
                            My.exeSql("update Enquiry_Details set Status='Form Sold' where Enquiry_Id='" + ViewState["FromEnqId"].ToString() + "'");
                            string query1 = "INSERT INTO Enquiry_flowup (Enquiry_Id,Follow_Up_Date,Next_Follow_Up_Date,Response_Remarks,Created_by,Status) values (@Enquiry_Id,@Follow_Up_Date,@Next_Follow_Up_Date,@Response_Remarks,@Created_by,@Status)";
                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(query1);
                            cmd1.Parameters.AddWithValue("@Enquiry_Id", ViewState["FromEnqId"].ToString());
                            cmd1.Parameters.AddWithValue("@Follow_Up_Date", My.getdate1());
                            cmd1.Parameters.AddWithValue("@Next_Follow_Up_Date", My.getdate1());
                            cmd1.Parameters.AddWithValue("@Response_Remarks", "Form Sold");
                            cmd1.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                            cmd1.Parameters.AddWithValue("@Status", "Form Sold");
                            if (My.InsertUpdateData(cmd1))
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    //-----------create account------------------
                    string name = txt_student_first_name.Text + " " + txt_student_middle_name.Text + " " + txt_student_last_name.Text;
                    try
                    {
                        send_data_Create_ledger(txt_indesx_no.Text, name, ddl_gender.Text, txt_dob.Text, txt_city.Text, txt_district.Text, txt_pincode.Text, txt_mobile.Text, ddl_temp_state.Text, txt_fathers_first_name.Text, txt_date.Text);
                    }
                    catch
                    {

                    }

                    string unique_entry_id = My.unique_id();
                    string VoucherNo = recptno;
                    string feeType = "Form Sale";
                    double amountpaid = My.toDouble(txt_amount.Text);
                    string VoucherType = "Receipt";
                    string Description = "Form sale collection from " + name + " Amount : " + amountpaid + "/-";
                    string PayDate = txt_date.Text + " " + mycode.time();
                    int Idate = My.DateConvertToIdate(txt_date.Text);
                    string alternetacc_id = txt_indesx_no.Text;
                    string session_name = ddl_session.SelectedItem.Text;
                    bool checkbiilentery = check_dup_bill_no_entry(VoucherNo);
                    if (checkbiilentery == true)
                    {
                        if (ddl_paymentmode.Text.ToUpper() == "CASH")
                        {
                            My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                        }
                        else
                        {
                            string toponebank = My.get_bank_id(ddl_bank.Text);
                            My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                if (ViewState["Form_sale_slip_type"].ToString() == "A5")
                {
                    Response.Redirect("slip/form-sale-slip-a5.aspx?transaction=" + recptno, false);
                }
                else
                {
                    Response.Redirect("slip/Form_Sale_Slip.aspx?transaction=" + recptno, false);
                }
            }
            else
            {
                Alertme("Form number is dublicate ", "success");
            }


        }

        private void send_data_Create_ledger(string party_id, string studentname, string gender, string dob, string city, string district, string pin, string father_mob, string state, string fathername, string dateofadmission)
        {
            string statename = mycode.getstatename(state);
            string getstatecode = mycode.getstatename(state);
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  party_id='" + party_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = studentname;
                dr[2] = city;
                dr[3] = district;
                dr[4] = father_mob;
                dr["gstin"] = "UnRegistered";
                dr[6] = party_id;
                dr[7] = mycode.date();
                dr["firm"] = My.firm_id();
                dr["Registration_Type"] = "Customer";
                dr["State_Code"] = getstatecode;
                dr["State"] = statename;
                dr["type"] = "Customer";
                dr["Care_of"] = fathername;
                dr["pan_no"] = "";
                dr["Account_No"] = "";
                dr["Bank_Name"] = "";
                dr["IFSC_Code"] = "";
                dr[12] = "0";
                dr[13] = "NA";
                dt.Rows.Add(dr);
                //My.firm_wise_auto_serial("party_id");
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                My.save_Account_Ledger_Details(studentname, party_id, "26");
                My.update_Ledger_Opening_Balance(studentname, party_id, "26", "Dr", "0.00", dateofadmission, My.get_session());
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = studentname;
                    dr[2] = city;
                    dr[3] = district;
                    dr[4] = father_mob;

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }

        private bool check_dup_bill_no_entry(string voucherNo)
        {
            string query = "Select *   from Account_Voucher_Details where VoucherNo_Manual='" + voucherNo + "' and Bill_from='SCHOOL PAY' and ref_name='Form Sale'  ";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void update_update_details()
        {
            bool send = false;

            if (ViewState["isexamdate"].ToString() == "1")
            {
                if (txt_date_of_exam.Text == "")
                {
                    Alertme("Please enter date of exam", "warning");
                    txt_date_of_exam.Focus();
                    send = false;
                    return;
                }
                else
                {
                    send = true;
                }
            }
            else
            {
                send = true;
            }
            if (send == true)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Form_sale_details where Id='" + hd_id.Value + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr[2] = txt_student_first_name.Text + " " + txt_student_middle_name.Text + " " + txt_student_last_name.Text;
                    dr[3] = txt_fathers_first_name.Text + " " + txt_fathers_middle_name.Text + " " + txt_fathers_last_name.Text;
                    dr[4] = txt_mothers_first_name.Text + " " + txt_mothers_middle_name.Text + " " + txt_mothers_last_name.Text;
                    dr["Guardian_name"] = txt_gaurdian_first_name.Text + " " + txt_gaurdian_middle_name.Text + " " + txt_gaurdian_last_name.Text;

                    dr[5] = txt_address.Text;
                    if (ddl_gender.SelectedItem.Text == "Select")
                    {
                        dr[6] = "";
                    }
                    else
                    {
                        dr[6] = ddl_gender.SelectedItem.Text;
                    }

                    if (ddl_cast.SelectedItem.Text == "Select")
                    {
                        dr[7] = "";
                    }
                    else
                    {
                        dr[7] = ddl_cast.SelectedItem.ToString();
                    }
                    dr[8] = txt_mobile.Text;
                    dr[9] = txt_date.Text;
                    dr[10] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                    dr[11] = My.toDouble(txt_amount.Text).ToString("0.00");
                    dr[13] = txt_dob.Text;
                    dr[15] = ddl_class.SelectedItem.Text;
                    dr["form_indesx_no"] = txt_indesx_no.Text;
                    dr["Remarks"] = txt_remarks.Text;

                    dr[16] = ddl_session.SelectedItem.Text;
                    dr["Session_id"] = ddl_session.SelectedValue;
                    dr["Class_id"] = ddl_class.SelectedValue;

                    dr["Student_first_name"] = txt_student_first_name.Text;
                    dr["Student_middle_name"] = txt_student_middle_name.Text;
                    dr["Student_last_name"] = txt_student_last_name.Text;

                    if (ddl_nationality.Text == "Select")
                    {
                        dr["Nationality"] = "";
                    }
                    else
                    {
                        dr["Nationality"] = ddl_nationality.Text;
                    }
                    dr["Blood_group"] = ddl_blood_group.Text;

                    dr["Height"] = txt_height.Text;
                    dr["Weight"] = txt_weight.Text;
                    if (ddl_religion.Text == "Select")
                    {
                        dr["Religion"] = "";
                    }
                    else
                    {
                        dr["Religion"] = ddl_religion.Text;
                    }

                    dr["Aadhar_no"] = txt_aadhar_no.Text;
                    if (ddl_student_mother_tongue.Text == "Select")
                    {
                        dr["Mother_tongue"] = "";
                    }
                    else
                    {
                        dr["Mother_tongue"] = ddl_student_mother_tongue.Text;
                    }
                    dr["Language_spoken_at_home"] = txt_language_spoken_at_home.Text;

                    dr["Name_of_sibling1"] = txt_name_of_sibling1.Text;
                    dr["Age_of_sibling1"] = txt_age_sibling1.Text;
                    dr["School_of_sibling1"] = txt_school_sibling1.Text;

                    if (ddl_class_sb1.SelectedItem.Text == "Select")
                    {
                        dr["Class_of_sibling1"] = "";
                    }
                    else
                    {
                        dr["Class_of_sibling1"] = ddl_class_sb1.Text;
                    }

                    dr["Name_of_sibling2"] = txt_name_of_sibling2.Text;
                    dr["Age_of_sibling2"] = txt_age_sibling2.Text;
                    dr["School_of_sibling2"] = txt_school_sibling2.Text;

                    if (ddl_class_sb2.SelectedItem.Text == "Select")
                    {
                        dr["Class_of_sibling2"] = "";
                    }
                    else
                    {
                        dr["Class_of_sibling2"] = ddl_class_sb2.Text;
                    }
                    dr["Prv_school_name1"] = txt_prv_school_name1.Text;
                    dr["Prv_school_add1"] = txt_prv_schho_address1.Text;

                    if (ddl_prv_class_from1.SelectedItem.Text == "Select")
                    {
                        dr["Prv_class_from"] = "";
                    }
                    else
                    {
                        dr["Prv_class_from"] = ddl_prv_class_from1.Text;
                    }
                    if (ddl_prv_class_to1.SelectedItem.Text == "Select")
                    {
                        dr["Prv_class_to1"] = "";
                    }
                    else
                    {
                        dr["Prv_class_to1"] = ddl_prv_class_to1.Text;
                    }
                    dr["Prv_year_from1"] = txt_prv_school_year_from1.Text;
                    dr["Prv_year_to1"] = txt_prv_school_year_to_1.Text;

                    if (ddl_prev_board_type.Text.ToUpper() == "SELECT")
                    {
                    }
                    else
                    {
                        dr["Prv_board1"] = ddl_prv_school_board1.Text;
                    }


                    dr["Prv_medium"] = txt_prv_school_medium.Text;
                    dr["Mark_percent1"] = txt_prv_school_marks.Text;

                    dr["Prv_school_name2"] = txt_prv_school_name2.Text;
                    dr["Prv_school_add2"] = txt_prv_schho_address2.Text;

                    if (ddl_prv_class_from2.SelectedItem.Text == "Select")
                    {
                        dr["Prv_class_from2"] = "";
                    }
                    else
                    {
                        dr["Prv_class_from2"] = ddl_prv_class_from2.Text;
                    }
                    if (ddl_prv_class_to2.SelectedItem.Text == "Select")
                    {
                        dr["Prv_class_to2"] = "";
                    }
                    else
                    {
                        dr["Prv_class_to2"] = ddl_prv_class_to2.Text;
                    }
                    dr["Prv_year_from2"] = txt_prv_school_year_from2.Text;
                    dr["Prv_year_to2"] = txt_prv_school_year_to_2.Text;

                    if (ddl_prev_board_type2.Text.ToUpper() == "SELECT")
                    {
                    }
                    else
                    {
                        dr["Prv_board2"] = ddl_prv_school_board2.Text;
                    }

                    dr["Prv_medium2"] = txt_prv_school_mediu2.Text;
                    dr["Mark_percent2"] = txt_prv_school_mark2.Text;



                    //============================
                    dr["Father_first_name"] = txt_fathers_first_name.Text;
                    dr["Father_middle_name"] = txt_fathers_middle_name.Text;
                    dr["Father_last_name"] = txt_fathers_last_name.Text;
                    dr["F_aadhar_no"] = txt_fthr_aadhar_no.Text;
                    if (ddl_f_education.SelectedItem.Text == "Select")
                    {
                        dr["F_qualification"] = "";
                    }
                    else
                    {
                        dr["F_qualification"] = ddl_f_education.Text;
                    }
                    if (ddl_f_occupation.SelectedItem.Text == "Select")
                    {
                        dr["F_occupation"] = "";
                    }
                    else
                    {
                        dr["F_occupation"] = ddl_f_occupation.Text;
                    }
                    dr["F_annual_income"] = txt_f_annual_income.Text;
                    dr["F_contact_no"] = txt_f_contact_no.Text;
                    dr["F_email_id"] = txt_f_email_id.Text;


                    dr["Mother_first_name"] = txt_mothers_first_name.Text;
                    dr["Mother_middle_name"] = txt_mothers_middle_name.Text;
                    dr["Mother_last_name"] = txt_mothers_last_name.Text;
                    dr["M_aadhar_no"] = txt_m_aadhar_no.Text;
                    if (ddl_m_education.SelectedItem.Text == "Select")
                    {
                        dr["M_qualification"] = "";
                    }
                    else
                    {
                        dr["M_qualification"] = ddl_m_education.Text;
                    }
                    if (ddl_m_occupation.SelectedItem.Text == "Select")
                    {
                        dr["M_occupation"] = "";
                    }
                    else
                    {
                        dr["M_occupation"] = ddl_m_occupation.Text;
                    }
                    dr["M_annual_income"] = txt_m_annual_income.Text;
                    dr["M_contact_no"] = txt_m_contact_no.Text;
                    dr["M_email_id"] = txt_m_email_id.Text;

                    dr["Guardian_first_name"] = txt_gaurdian_first_name.Text;
                    dr["Guardian_middle_name"] = txt_gaurdian_middle_name.Text;
                    dr["Guardian_last_name"] = txt_gaurdian_last_name.Text;
                    dr["Guardian_aadhar_no"] = txt_g_aadhar_no.Text;
                    if (ddl_g_education.SelectedItem.Text == "Select")
                    {
                        dr["Guardian_qualification"] = "";
                    }
                    else
                    {
                        dr["Guardian_qualification"] = ddl_g_education.Text;
                    }
                    if (ddl_g_occupation.SelectedItem.Text == "Select")
                    {
                        dr["Guardian_occupation"] = "";
                    }
                    else
                    {
                        dr["Guardian_occupation"] = ddl_g_occupation.Text;
                    }
                    dr["Guardian_annual_income"] = txt_g_annual_income.Text;
                    dr["Guardian_contact_no"] = txt_g_contact_no.Text;
                    dr["Guardian_email_id"] = txt_g_email_id.Text;

                    try
                    {
                        dr["Prv_board_type1"] = ddl_prev_board_type.Text;
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        dr["Prv_board_type2"] = ddl_prev_board_type2.Text;
                    }
                    catch (Exception ex)
                    {
                    }


                    dr["Post_office"] = txt_po.Text;
                    dr["Police_station"] = txt_ps.Text;
                    dr["District"] = txt_district.Text;
                    dr["City"] = txt_city.Text;
                    if (ddl_temp_state.Text == "Select")
                    {
                        dr["State"] = "";
                    }
                    else
                    {
                        dr["State"] = ddl_temp_state.Text;
                    }

                    dr["Pin_code"] = txt_pincode.Text;
                    dr["Country"] = ddl_country_c.Text;
                    dr["Add_mobile_no"] = txt_temp_mobileno.Text;
                    dr["Country_code"] = ddl_cunterycode3.Text;

                    dr["Updated_by"] = ViewState["Userid"].ToString();
                    dr["Updated_date"] = mycode.date();
                    dr["Updated_time"] = mycode.time();
                    dr["Updated_idate"] = mycode.idate();
                    dr["Payment_Mode"] = ddl_paymentmode.Text;
                    dr["Bank_name"] = ddl_bank.SelectedItem.Text;
                    dr["Bank_tran_no"] = txt_trans_no.Text;
                    dr["Alternate_mobile_no"] = txt_alternate_mobile_no.Text;
                    if (ddl_Second_Language.Text == "Select")
                    {
                        dr["Second_Language"] = "";
                    }
                    else
                    {
                        dr["Second_Language"] = ddl_Second_Language.Text;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                // update

                try
                {
                    mycode.executequery("update Form_sale_details set Exam_date='" + txt_date_of_exam.Text + "'  where Id='" + hd_id.Value + "'");

                }
                catch
                {

                }


                try
                {

                    My.exeSql("delete from Account_Voucher_Details where VoucherNo_Manual='" + ViewState["recpt_no"].ToString() + "' and VoucherType='Receipt' and ref_name='Form Sale'");
                    string name = txt_student_first_name.Text + " " + txt_student_middle_name.Text + " " + txt_student_last_name.Text;
                    string unique_entry_id = My.unique_id();
                    string VoucherNo = ViewState["recpt_no"].ToString();
                    string feeType = "Form Sale";
                    double amountpaid = My.toDouble(txt_amount.Text);
                    string VoucherType = "Receipt";
                    string Description = "Form sale collection from " + name + " Amount : " + amountpaid + "/-";
                    string PayDate = txt_date.Text + " " + mycode.time();
                    int Idate = My.DateConvertToIdate(txt_date.Text);
                    string alternetacc_id = txt_indesx_no.Text;
                    string session_name = ddl_session.SelectedItem.Text;
                    bool checkbiilentery = check_dup_bill_no_entry(VoucherNo);
                    if (checkbiilentery == true)
                    {
                        if (ddl_paymentmode.Text.ToUpper() == "CASH")
                        {
                            My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                        }
                        else
                        {
                            string toponebank = My.get_bank_id(ddl_bank.Text);
                            My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, amountpaid.ToString("0.00"), VoucherNo, unique_entry_id, Description, PayDate, Idate.ToString(), VoucherType, My.firm_id(), session_name, ViewState["Userid"].ToString(), "SCHOOL PAY", feeType, VoucherNo,"");
                        }
                    }
                    else
                    {
                    }
                }
                catch
                {

                }
                Alertme("Form Sale Details  Updated Successfully", "success");
            }
        }


        private void empty_form()
        {
            txt_student_first_name.Text = "";
            txt_student_middle_name.Text = "";
            txt_student_last_name.Text = "";

            txt_fathers_first_name.Text = "";
            txt_fathers_middle_name.Text = "";
            txt_fathers_last_name.Text = "";

            txt_mothers_first_name.Text = "";
            txt_mothers_middle_name.Text = "";
            txt_mothers_last_name.Text = "";

            txt_dob.Text = "";
            txt_address.Text = "";
            txt_mobile.Text = "";
            txt_date.Text = "";
            txt_indesx_no.Text = "";

            txt_student_first_name.Text = "";

            txt_student_middle_name.Text = "";
            txt_student_last_name.Text = "";
            txt_height.Text = "";
            txt_weight.Text = "";
            txt_aadhar_no.Text = "";
            txt_language_spoken_at_home.Text = "";
            txt_name_of_sibling1.Text = "";
            txt_age_sibling1.Text = "";
            txt_school_sibling1.Text = "";
            txt_name_of_sibling2.Text = "";
            txt_age_sibling2.Text = "";
            txt_school_sibling2.Text = "";
            txt_prv_school_name1.Text = "";
            txt_prv_schho_address1.Text = "";
            txt_prv_school_year_from1.Text = "";
            txt_prv_school_medium.Text = "";
            txt_prv_school_marks.Text = "";
            txt_prv_school_name2.Text = "";
            txt_prv_schho_address2.Text = "";
            txt_prv_school_year_from2.Text = "";
            txt_prv_school_year_to_2.Text = "";
            txt_prv_school_mediu2.Text = "";
            txt_prv_school_mark2.Text = "";
            //============================
            txt_fathers_first_name.Text = "";
            txt_fathers_middle_name.Text = "";
            txt_fathers_last_name.Text = "";
            txt_fthr_aadhar_no.Text = "";
            txt_f_annual_income.Text = "";
            txt_f_contact_no.Text = "";
            txt_f_email_id.Text = "";
            txt_mothers_first_name.Text = "";
            txt_mothers_middle_name.Text = "";
            txt_mothers_last_name.Text = "";
            txt_m_aadhar_no.Text = "";
            txt_m_annual_income.Text = "";
            txt_m_contact_no.Text = "";
            txt_m_email_id.Text = "";
            txt_gaurdian_first_name.Text = "";
            txt_gaurdian_middle_name.Text = "";
            txt_gaurdian_last_name.Text = "";
            txt_g_aadhar_no.Text = "";
            txt_g_annual_income.Text = "";
            txt_g_contact_no.Text = "";
            txt_g_email_id.Text = "";
            txt_po.Text = "";
            txt_ps.Text = "";
            txt_district.Text = "";
            txt_city.Text = "";
            txt_pincode.Text = "";
            txt_date_of_exam.Text = "";
            txt_temp_mobileno.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_recpt_no = (Label)row.FindControl("lbl_recpt_no");

                    ViewState["recpt_no"] = lbl_recpt_no.Text;
                    hd_id.Value = lbl_Id.Text;
                    DataTable dt = mycode.FillData("select * from Form_sale_details where Id=" + lbl_Id.Text + "");
                    if (dt.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        try
                        {
                            txt_date_of_exam.Text = dt.Rows[0]["Exam_date"].ToString();
                        }
                        catch
                        {

                        }

                        txt_indesx_no.Text = dt.Rows[0]["form_indesx_no"].ToString();
                        //txt_student_first_name.Text = dt.Rows[0]["student_name"].ToString();
                        //txt_fathers_first_name.Text = dt.Rows[0]["fathers_name"].ToString();
                        //txt_mothers_first_name.Text = dt.Rows[0]["Mothers_name"].ToString();
                        txt_dob.Text = dt.Rows[0]["dob"].ToString();
                        txt_address.Text = dt.Rows[0]["Address"].ToString();
                        ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                        ddl_gender.Text = dt.Rows[0]["gender"].ToString();
                        ddl_cast.Text = dt.Rows[0]["cast"].ToString();
                        txt_mobile.Text = dt.Rows[0]["mobile"].ToString();
                        txt_date.Text = dt.Rows[0]["date"].ToString();
                        txt_amount.Text = dt.Rows[0]["Amount"].ToString();
                        ddl_session.SelectedValue = dt.Rows[0]["Session_id"].ToString();

                        txt_student_first_name.Text = dt.Rows[0]["Student_first_name"].ToString();
                        txt_student_middle_name.Text = dt.Rows[0]["Student_middle_name"].ToString();
                        txt_student_last_name.Text = dt.Rows[0]["Student_last_name"].ToString();
                        txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();
                        txt_alternate_mobile_no.Text = dt.Rows[0]["Alternate_mobile_no"].ToString();
                        try
                        {
                            ddl_nationality.Text = dt.Rows[0]["Nationality"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_height.Text = dt.Rows[0]["Height"].ToString();
                        txt_weight.Text = dt.Rows[0]["Weight"].ToString();
                        try
                        {
                            ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_aadhar_no.Text = dt.Rows[0]["Aadhar_no"].ToString();
                        try
                        {
                            ddl_student_mother_tongue.Text = dt.Rows[0]["Mother_tongue"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            ddl_Second_Language.Text = dt.Rows[0]["Second_Language"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_language_spoken_at_home.Text = dt.Rows[0]["Language_spoken_at_home"].ToString();

                        txt_name_of_sibling1.Text = dt.Rows[0]["Name_of_sibling1"].ToString();
                        txt_age_sibling1.Text = dt.Rows[0]["Age_of_sibling1"].ToString();
                        txt_school_sibling1.Text = dt.Rows[0]["School_of_sibling1"].ToString();
                        try
                        {
                            ddl_class_sb1.Text = dt.Rows[0]["Class_of_sibling1"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }

                        txt_name_of_sibling2.Text = dt.Rows[0]["Name_of_sibling2"].ToString();
                        txt_age_sibling2.Text = dt.Rows[0]["Age_of_sibling2"].ToString();
                        txt_school_sibling2.Text = dt.Rows[0]["School_of_sibling2"].ToString();
                        try
                        {
                            ddl_class_sb2.Text = dt.Rows[0]["Class_of_sibling2"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }


                        txt_prv_school_name1.Text = dt.Rows[0]["Prv_school_name1"].ToString();
                        txt_prv_schho_address1.Text = dt.Rows[0]["Prv_school_add1"].ToString();
                        try
                        {
                            ddl_prv_class_from1.Text = dt.Rows[0]["Prv_class_from"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_prv_class_to1.Text = dt.Rows[0]["Prv_class_to1"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_prv_school_year_from1.Text = dt.Rows[0]["Prv_year_from1"].ToString();
                        txt_prv_school_year_to_1.Text = dt.Rows[0]["Prv_year_to1"].ToString();

                        txt_prv_school_medium.Text = dt.Rows[0]["Prv_medium"].ToString();
                        txt_prv_school_marks.Text = dt.Rows[0]["Mark_percent1"].ToString();




                        //==============================
                        txt_prv_school_name2.Text = dt.Rows[0]["Prv_school_name2"].ToString();
                        txt_prv_schho_address2.Text = dt.Rows[0]["Prv_school_add2"].ToString();
                        try
                        {
                            ddl_prv_class_from2.Text = dt.Rows[0]["Prv_class_from2"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_prv_class_to2.Text = dt.Rows[0]["Prv_class_to2"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_prv_school_year_from2.Text = dt.Rows[0]["Prv_year_from2"].ToString();
                        txt_prv_school_year_to_2.Text = dt.Rows[0]["Prv_year_to2"].ToString();



                        txt_prv_school_mediu2.Text = dt.Rows[0]["Prv_medium2"].ToString();
                        txt_prv_school_mark2.Text = dt.Rows[0]["Mark_percent2"].ToString();




                        //============================
                        txt_fathers_first_name.Text = dt.Rows[0]["Father_first_name"].ToString();
                        txt_fathers_middle_name.Text = dt.Rows[0]["Father_middle_name"].ToString();
                        txt_fathers_last_name.Text = dt.Rows[0]["Father_last_name"].ToString();
                        txt_fthr_aadhar_no.Text = dt.Rows[0]["F_aadhar_no"].ToString();
                        try
                        {
                            ddl_f_education.Text = dt.Rows[0]["F_qualification"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_f_occupation.Text = dt.Rows[0]["F_occupation"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_f_annual_income.Text = dt.Rows[0]["F_annual_income"].ToString();
                        txt_f_contact_no.Text = dt.Rows[0]["F_contact_no"].ToString();
                        txt_f_email_id.Text = dt.Rows[0]["F_email_id"].ToString();



                        //============================
                        txt_mothers_first_name.Text = dt.Rows[0]["Mother_first_name"].ToString();
                        txt_mothers_middle_name.Text = dt.Rows[0]["Mother_middle_name"].ToString();
                        txt_mothers_last_name.Text = dt.Rows[0]["Mother_last_name"].ToString();
                        txt_m_aadhar_no.Text = dt.Rows[0]["M_aadhar_no"].ToString();
                        try
                        {
                            ddl_m_education.Text = dt.Rows[0]["M_qualification"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_m_occupation.Text = dt.Rows[0]["M_annual_income"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_m_annual_income.Text = dt.Rows[0]["M_contact_no"].ToString();
                        txt_m_contact_no.Text = dt.Rows[0]["M_contact_no"].ToString();
                        txt_m_email_id.Text = dt.Rows[0]["M_email_id"].ToString();



                        //============================
                        txt_gaurdian_first_name.Text = dt.Rows[0]["Guardian_first_name"].ToString();
                        txt_gaurdian_middle_name.Text = dt.Rows[0]["Guardian_middle_name"].ToString();
                        txt_gaurdian_last_name.Text = dt.Rows[0]["Guardian_last_name"].ToString();
                        txt_g_aadhar_no.Text = dt.Rows[0]["Guardian_aadhar_no"].ToString();
                        try
                        {
                            ddl_g_education.Text = dt.Rows[0]["Guardian_qualification"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_g_occupation.Text = dt.Rows[0]["Guardian_occupation"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_g_annual_income.Text = dt.Rows[0]["Guardian_annual_income"].ToString();
                        txt_g_contact_no.Text = dt.Rows[0]["Guardian_contact_no"].ToString();
                        txt_g_email_id.Text = dt.Rows[0]["Guardian_email_id"].ToString();
                        txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();

                        txt_po.Text = dt.Rows[0]["Post_office"].ToString();
                        txt_ps.Text = dt.Rows[0]["Police_station"].ToString();
                        txt_district.Text = dt.Rows[0]["District"].ToString();
                        txt_city.Text = dt.Rows[0]["City"].ToString();
                        try
                        {
                            ddl_temp_state.Text = dt.Rows[0]["State"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_pincode.Text = dt.Rows[0]["Pin_code"].ToString();
                        try
                        {
                            ddl_country_c.Text = dt.Rows[0]["Country"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_temp_mobileno.Text = dt.Rows[0]["Add_mobile_no"].ToString();
                        try
                        {
                            ddl_cunterycode3.Text = dt.Rows[0]["Country_code"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }




                        try
                        {
                            ddl_prev_board_type.Text = dt.Rows[0]["Prv_board_type1"].ToString();
                            bind_board_list();
                            try
                            {
                                ddl_prv_school_board1.Text = dt.Rows[0]["Prv_board1"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_prev_board_type2.Text = dt.Rows[0]["Prv_board_type2"].ToString();
                            bind_board_list2();
                            try
                            {
                                ddl_prv_school_board2.Text = dt.Rows[0]["Prv_board2"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                        }


                        try
                        {
                            ddl_paymentmode.Text = dt.Rows[0]["Payment_Mode"].ToString();
                            ddl_bank.Text = dt.Rows[0]["Bank_name"].ToString();
                            txt_trans_no.Text = dt.Rows[0]["Bank_tran_no"].ToString();
                        }
                        catch
                        {

                        }
                        btn_cancel.Visible = true;
                        btn_Submit.Text = "Update";
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_recpt_no = (Label)row.FindControl("lbl_recpt_no");

                My.exeSql("delete from Form_sale_details where Id='" + lbl_Id.Text + "'");
                My.exeSql("delete from Account_Voucher_Details where VoucherNo_Manual='" + lbl_recpt_no.Text + "' and VoucherType='Receipt' and ref_name='Form Sale'");

                Alertme("Record has been deleted successfully", "success");
                if (ViewState["flag"].ToString() == "0")
                {
                    bind_grd_view();
                }
                if (ViewState["flag"].ToString() == "1")
                {
                    find_by_date();
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose from date", "warning");
                    txt_s_date.Focus();
                }
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose to date", "warning");
                    txt_e_date.Focus();
                }
                else
                {
                    find_by_date();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_date()
        {
            string sdate = txt_s_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);

            string edate = txt_e_date.Text;
            string eday = edate.Substring(0, 2);
            string emonth = edate.Substring(3, 2);
            string eyear = edate.Substring(6, 4);

            int idate = Convert.ToInt32(syear + smonth + sday);
            int idate2 = Convert.ToInt32(eyear + emonth + eday);

            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                final_find_report_by_date(idate, idate2);
            }
        }

        private void final_find_report_by_date(int idate, int idate2)
        {
            bind_grd_view("select * from Form_sale_details where  idate>=" + idate + " and idate<=" + idate2 + " order by id desc");
        }



        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
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

        protected void lnk_print_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Print"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recpt_no = (Label)row.FindControl("lbl_recpt_no");


                if (ViewState["Form_sale_slip_type"].ToString() == "A5")
                {
                    Response.Redirect("slip/form-sale-slip-a5.aspx?transaction=" + lbl_recpt_no.Text, false);
                }
                else
                {
                    Response.Redirect("slip/Form_Sale_Slip.aspx?transaction=" + lbl_recpt_no.Text, false);
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void lnk_print_form_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Print"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_recpt_no = (Label)row.FindControl("lbl_recpt_no");
                Label lbl_Form_no = (Label)row.FindControl("lbl_Form_no");
                Label lbl_Is_Generate = (Label)row.FindControl("lbl_Is_Generate");

                if (lbl_Is_Generate.Text == "0")
                {
                    string Form_No_Generate = My.auto_serialS("Form_No_Generate");
                    string fomno = ViewState["prifixdate"].ToString() + Form_No_Generate;
                    mycode.executequery("update Form_sale_details set Is_Generate=1, Form_no='" + fomno + "' where recpt_no='" + lbl_recpt_no.Text + "'");
                    Response.Redirect("slip/registration-form.aspx?form_no=" + fomno, false);
                }
                else if (lbl_Is_Generate.Text == "")
                {
                    string Form_No_Generate = My.auto_serialS("Form_No_Generate");
                    string fomno = ViewState["prifixdate"].ToString() + Form_No_Generate;
                    mycode.executequery("update Form_sale_details set Is_Generate=1, Form_no='" + fomno + "' where recpt_no='" + lbl_recpt_no.Text + "'");
                    Response.Redirect("slip/registration-form.aspx?form_no=" + fomno, false);
                }
                else
                {
                    Response.Redirect("slip/registration-form.aspx?form_no=" + lbl_Form_no.Text, false);
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                LinkButton lnkDel = item.FindControl("lnkDel") as LinkButton;
                LinkButton lnkEdit = item.FindControl("lnkEdit") as LinkButton;
                LinkButton lnk_print_form = item.FindControl("lnk_print_form") as LinkButton;
                LinkButton lnk_transfer_to_admission = item.FindControl("lnk_transfer_to_admission") as LinkButton;
                LinkButton lnk_adm_no = item.FindControl("lnk_adm_no") as LinkButton;
                Label lbl_Is_Generate = item.FindControl("lbl_Is_Generate") as Label;
                Label lbl_SL = item.FindControl("lbl_SL") as Label;
                Label lbl_form_no = item.FindControl("lbl_form_no") as Label;
                Label lbl_student_name = item.FindControl("lbl_student_name") as Label;
                Label lbl_fathers_name = item.FindControl("lbl_fathers_name") as Label;
                Label lbl_gender = item.FindControl("lbl_gender") as Label;
                Label lbl_mobile = item.FindControl("lbl_mobile") as Label;
                Label lbl_date = item.FindControl("lbl_date") as Label;
                Label lbl_payment_mode = item.FindControl("lbl_payment_mode") as Label;
                Label lbl_remarks = item.FindControl("lbl_remarks") as Label;
                Label lbl_is_admission_no_updated = item.FindControl("lbl_is_admission_no_updated") as Label;
                lnkDel.Visible = true;
                lnkEdit.Visible = true;
                lnk_adm_no.Visible = true;
                lnk_transfer_to_admission.Visible = true;
                lnk_print_form.Visible = true;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trR");
                if (lbl_is_admission_no_updated.Text == "1")
                {
                    lnkDel.Visible = false;
                    lnkEdit.Visible = false;
                    lnk_print_form.Visible = false;
                    lnk_transfer_to_admission.Visible = false;
                    lnk_adm_no.Visible = false;
                    tr.Attributes.Add("style", "background-color:#00bda3;color:#FFFFFF;");
                }


                //if (lbl_Is_Generate.Text != "")
                //{
                //    lbl_SL.BackColor = Color.Green;
                //    lbl_SL.ForeColor = Color.White;

                //    lbl_form_no.BackColor = Color.Green;
                //    lbl_form_no.ForeColor = Color.White;


                //    lbl_student_name.BackColor = Color.Green;
                //    lbl_student_name.ForeColor = Color.White;


                //    lbl_fathers_name.BackColor = Color.Green;
                //    lbl_fathers_name.ForeColor = Color.White;

                //    lbl_gender.BackColor = Color.Green;
                //    lbl_gender.ForeColor = Color.White;

                //    lbl_mobile.BackColor = Color.Green;
                //    lbl_mobile.ForeColor = Color.White;

                //    lbl_date.BackColor = Color.Green;
                //    lbl_date.ForeColor = Color.White;

                //    lbl_payment_mode.BackColor = Color.Green;
                //    lbl_payment_mode.ForeColor = Color.White;

                //    lbl_remarks.BackColor = Color.Green;
                //    lbl_remarks.ForeColor = Color.White;
                //}
                //else
                //{
                //    lbl_SL.BackColor = Color.White;
                //    lbl_SL.ForeColor = Color.Black;

                //    lbl_form_no.BackColor = Color.White;
                //    lbl_form_no.ForeColor = Color.Black;

                //    lbl_student_name.BackColor = Color.White;
                //    lbl_student_name.ForeColor = Color.Black;

                //    lbl_fathers_name.BackColor = Color.White;
                //    lbl_fathers_name.ForeColor = Color.Black;

                //    lbl_gender.BackColor = Color.White;
                //    lbl_gender.ForeColor = Color.Black;

                //    lbl_mobile.BackColor = Color.White;
                //    lbl_mobile.ForeColor = Color.Black;

                //    lbl_date.BackColor = Color.White;
                //    lbl_date.ForeColor = Color.Black;

                //    lbl_payment_mode.BackColor = Color.White;
                //    lbl_payment_mode.ForeColor = Color.Black;

                //    lbl_remarks.BackColor = Color.White;
                //    lbl_remarks.ForeColor = Color.Black;
                //}
            }
        }


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    grd_data_list.Visible = true;
                    download_excel();
                    grd_data_list.Visible = false;
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        private void download_excel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Form_Sale.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grd_data_list.AllowPaging = false;


            if (ViewState["flag"].ToString() == "0")
            {
                bind_excel_grd_view("0");
            }
            if (ViewState["flag"].ToString() == "1")
            {
                bind_excel_grd_view("1");
            }

            //Change the Header Row back to white color
            grd_data_list.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //grd_enquiry_list.Columns[14].Visible = false;

            //Applying stlye to gridview header cells
            for (int i = 0; i < grd_data_list.HeaderRow.Cells.Count; i++)
            {
                grd_data_list.HeaderRow.Cells[i].Style.Add("background-color", "#EF00FF");
            }

            grd_data_list.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }


        private void bind_excel_grd_view(string type)
        {
            string qry = "";
            if (type == "0")
            {
                qry = "select * from Form_sale_details where Session_id='" + ddl_session.SelectedValue + "'";
            }
            if (type == "1")
            {
                string sdate = txt_s_date.Text;
                string sday = sdate.Substring(0, 2);
                string smonth = sdate.Substring(3, 2);
                string syear = sdate.Substring(6, 4);

                string edate = txt_e_date.Text;
                string eday = edate.Substring(0, 2);
                string emonth = edate.Substring(3, 2);
                string eyear = edate.Substring(6, 4);

                int idate = Convert.ToInt32(syear + smonth + sday);
                int idate2 = Convert.ToInt32(eyear + emonth + eday);

                qry = "select * from Form_sale_details where  idate>=" + idate + " and idate<=" + idate2 + " order by id desc";
            }


            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                grd_data_list.DataSource = null;
                grd_data_list.DataBind();
            }
            else
            {
                grd_data_list.DataSource = dt;
                grd_data_list.DataBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void lnk_adm_no_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_student_name = (Label)row.FindControl("lbl_student_name");
                Label lbl_amts = (Label)row.FindControl("lbl_amts");
                Label lbl_recpt_no = (Label)row.FindControl("lbl_recpt_no");
                Label lbl_id = (Label)row.FindControl("lbl_id");

                lbl_std_name.Text = lbl_student_name.Text;
                lbl_amt_p.Text = lbl_amts.Text;
                lbl_session_p.Text = lbl_session.Text;

                ViewState["sessionS"] = lbl_session.Text;
                ViewState["idS"] = lbl_id.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_update_adm_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    ViewState["isSucceSS"] = "0";
                    update_admission_no();
                    if (ViewState["isSucceSS"].ToString() == "1")
                    {
                        txt_admission_no.Text = "";
                        Alertme("Admsiion no. has been updated successfully.", "success");
                        if (ViewState["flag"].ToString() == "0")
                        {
                            bind_grd_view();
                        }
                        if (ViewState["flag"].ToString() == "1")
                        {
                            find_by_date();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_admission_no()
        {
            if (mycode.IsUserExist("select Admission_no from Form_sale_details where session='" + ViewState["sessionS"].ToString() + "' and Admission_no='" + txt_admission_no.Text + "'"))
            {
                if (mycode.IsUserExist("select admissionserialnumber from admission_registor where session='" + ViewState["sessionS"].ToString() + "' and admissionserialnumber='" + txt_admission_no.Text + "'"))
                {
                    Alertme("Student not exist by this admission no. Please check admission no.", "warning");
                    txt_admission_no.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    DataTable dt = My.dataTable("select * from Form_sale_details where Id='" + ViewState["idS"].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Student_Payment_History (Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,Previous_admission_no,time,user_id,Acamedic_Semester_Id,Branch,Class_id,Pay_mode_transaction_no,Transection_in,parameter_New) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@Previous_admission_no,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@Pay_mode_transaction_no,@Transection_in,@parameter_New)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Addmission_no", txt_admission_no.Text);
                            cmd.Parameters.AddWithValue("@Session", ViewState["sessionS"].ToString());
                            cmd.Parameters.AddWithValue("@Date", dr["date"].ToString());
                            cmd.Parameters.AddWithValue("@Idate", dr["idate"].ToString());
                            cmd.Parameters.AddWithValue("@Description", dr["Remarks"].ToString());
                            cmd.Parameters.AddWithValue("@Entry_id", dr["recpt_no"].ToString());
                            cmd.Parameters.AddWithValue("@Slip_no", dr["Form_no"].ToString());
                            cmd.Parameters.AddWithValue("@Amount", dr["Amount"].ToString());
                            cmd.Parameters.AddWithValue("@Type", "FormSale");
                            cmd.Parameters.AddWithValue("@mode", dr["Payment_Mode"].ToString());
                            cmd.Parameters.AddWithValue("@discount", "0.00");
                            cmd.Parameters.AddWithValue("@Discoun_in_School", "0.00");
                            cmd.Parameters.AddWithValue("@Discoun_in_Hostel", "0.00");
                            cmd.Parameters.AddWithValue("@Discoun_in_Transport", "0.00");
                            cmd.Parameters.AddWithValue("@fine", "0.00");
                            cmd.Parameters.AddWithValue("@Previous_admission_no", "0");
                            cmd.Parameters.AddWithValue("@time", mycode.time());
                            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                            cmd.Parameters.AddWithValue("@Branch", ViewState["Branchid"].ToString());
                            string class_ids = get_class_id(dr["class"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", class_ids);
                            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", dr["Transaction_no"].ToString());
                            cmd.Parameters.AddWithValue("@Transection_in", "Software");
                            cmd.Parameters.AddWithValue("@parameter_New", "FormSaleFee");
                            if (My.InsertUpdateData(cmd))
                            {
                                mycode.executequery("update Form_sale_details set Admission_no='" + txt_admission_no.Text + "', Admission_no_updated_on='" + mycode.date() + "',Is_admission_no_updated='1' where Id=" + dr["Id"].ToString() + "");
                                ViewState["isSucceSS"] = "1";
                            }
                        }
                    }
                }
            }
            else
            {
                Alertme("Admission no. already exist.", "warning");
                txt_admission_no.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        private string get_class_id(string class_name)
        {
            DataTable dt = My.dataTable("select course_id from Add_course_table where Course_Name='" + class_name + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["course_id"].ToString();
            }
        }

        protected void ddl_prev_board_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_board_list();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_board_list()
        {
            mycode.bind_ddl(ddl_prv_school_board1, "select Board_name from Board_details where Type='" + ddl_prev_board_type.Text + "'");
        }

        protected void ddl_prev_board_type2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_board_list2();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_board_list2()
        {
            mycode.bind_ddl(ddl_prv_school_board2, "select Board_name from Board_details where Type='" + ddl_prev_board_type2.Text + "'");
        }

        protected void lnk_transfer_to_admission_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Response.Redirect("admission.aspx?regiDs=" + lbl_id.Text + "&transfrFormSale=1", false);
            }
            catch (Exception ex)
            {
            }
        }


        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_form_sale_fee();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_form_sale_fee()
        {
            DataTable dt = mycode.FillData("select * from Form_sale_fee_classwise where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                txt_amount.Text = dt.Rows[0]["Fee_amount"].ToString();
            }
        }
    }
}