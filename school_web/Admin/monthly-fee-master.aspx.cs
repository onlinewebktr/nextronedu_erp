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
    public partial class monthly_fee_master : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!IsPostBack)
                    {
                        ViewState["monthS"] = "0";
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "Admission_Fee_Master.aspx";
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
                        if (ViewState["Is_Download"].ToString() == "1")
                        {
                            LinkButton_excel.Visible = true;
                        }
                        else
                        {
                            LinkButton_excel.Visible = false;
                        }

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) ");
                        //mycode.bind_all_ddl_with_id(ddl_course, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_session_serach, "Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) ");
                        mycode.bind_all_ddl_with_id_All_New(ddl_course_search, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id_All_New(ddl_months, "select Month,Month_Id from Month_Index order by Position asc");
                        string monthname = mycode.get_current_monthname();
                        ddl_months.SelectedValue = My.tomonth_numberstring(monthname);


                        Bind_course_details();
                        Bind_admission_fee_type();
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_serach.SelectedValue = My.get_session_id();
                        ddl_course_search.SelectedValue = My.get_top_one_class();

                        Bind_fee();
                        bind_month();


                        mycode.bind_all_ddl_with_id(ddl_current_session, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");

                        mycode.bind_all_ddl_with_id(ddl_copy_to_session, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");


                        //ddl_session_serach.SelectedValue = My.get_session_id();
                    }
                }
            }

            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Master");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void bind_month()
        {
            DataTable dt = mycode.FillData("select Month,'false' as Value,Month_Id from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();
            }
        }
        private void Bind_course_details()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddl_course_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session_serach.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_course_search.SelectedItem.Text == "Select")
            {
                Alertme("Please select course", "warning");

            }
            else
            {
                Bind_fee();
            }
        }
        private void Bind_fee()
        {
            try
            {
                string query1 = "";
                if (ddl_session_serach.SelectedItem.Text == "Select")
                {
                    query1 = "Select * from Fee_Master where Ledger='School' and parameter_id='" + ddl_fees_type_srch.SelectedValue + "' ";
                }
                else if (ddl_session_serach.SelectedItem.Text != "Select" && ddl_course_search.SelectedItem.Text == "ALL" && ddl_months.SelectedItem.Text == "ALL")
                {
                    query1 = "Select * from Fee_Master where Ledger='School' and parameter_id='" + ddl_fees_type_srch.SelectedValue + "' and session_id='" + ddl_session_serach.SelectedValue + "'    ";
                }
                else if (ddl_session_serach.SelectedItem.Text != "Select" && ddl_course_search.SelectedItem.Text != "ALL" && ddl_months.SelectedItem.Text != "ALL")
                {
                    query1 = "Select * from Fee_Master where Ledger='School' and parameter_id='" + ddl_fees_type_srch.SelectedValue + "' and session_id='" + ddl_session_serach.SelectedValue + "' and class_id='" + ddl_course_search.SelectedValue + "' and Month_id='" + ddl_months.SelectedValue + "'  ";
                }
                else if (ddl_session_serach.SelectedItem.Text != "Select" && ddl_course_search.SelectedItem.Text != "ALL" && ddl_months.SelectedItem.Text == "ALL")
                {
                    query1 = "Select * from Fee_Master where Ledger='School' and parameter_id='" + ddl_fees_type_srch.SelectedValue + "' and session_id='" + ddl_session_serach.SelectedValue + "' and class_id='" + ddl_course_search.SelectedValue + "'   ";
                }
                else if (ddl_session_serach.SelectedItem.Text != "Select" && ddl_course_search.SelectedItem.Text == "ALL" && ddl_months.SelectedItem.Text != "ALL")
                {
                    query1 = "Select * from Fee_Master where Ledger='School' and parameter_id='" + ddl_fees_type_srch.SelectedValue + "' and session_id='" + ddl_session_serach.SelectedValue + "' and Month_id='" + ddl_months.SelectedValue + "'";
                }
                else
                {
                    query1 = "Select * from Fee_Master where Ledger='School' and parameter_id='" + ddl_fees_type_srch.SelectedValue + "' and session_id='" + ddl_session_serach.SelectedValue + "' and class_id='" + ddl_course_search.SelectedValue + "'  ";
                }
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    lbl_class22.Text = "";
                    rd_viewaddedfee.DataSource = null;
                    rd_viewaddedfee.DataBind();
                }
                else
                {
                    lbl_class22.Text = "Fee For :" + ddl_fees_type_srch.SelectedItem.Text + " Session :" + ddl_session_serach.SelectedItem.Text + " Class :" + ddl_course_search.SelectedItem.Text + " Month :" + ddl_months.SelectedItem.Text;
                    rd_viewaddedfee.DataSource = dt1;
                    rd_viewaddedfee.DataBind();
                }
            }
            catch
            {
            }
        }

        private void Bind_admission_fee_type()
        {
            string query1 = "Select * from Content_master where group_id='3' and  Ledger='School'";
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                grd_fee.DataSource = dt1;
                grd_fee.DataBind();
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
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    ViewState["statusUp"] = "0";
                    save_data();
                    if (ViewState["statusUp"].ToString() == "1")
                    {
                        Alertme(ViewState["msg"].ToString(), "success");
                        btn_cancel.Visible = false;
                        btn_Submit.Text = "Add";

                        Bind_fee();
                        Bind_course_details();
                        empty_grid_fee();
                        bind_month();
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    ViewState["statusUp"] = "0";
                    save_data();
                    if (ViewState["statusUp"].ToString() == "1")
                    {
                        Alertme(ViewState["msg"].ToString(), "success");
                        btn_cancel.Visible = false;
                        btn_Submit.Text = "Add";

                        Bind_fee();
                        Bind_course_details();
                        empty_grid_fee();
                        bind_month();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void empty_grid_fee()
        {
            int i;
            int gridview_rowcount = grd_fee.Rows.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox txt_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                txt_fee.Text = "0";
            }
            lbl_totalmrp.Text = "Total Fee : 0";
        }
        private void save_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session name", "warning");
                return;
            }
            if (btn_Submit.Text == "Add")
            {
                int mgrowcount = rp_month.Items.Count;
                int kl = 0;
                for (int ixi = 0; ixi < mgrowcount; ixi++)
                {
                    CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                    if (chkM.Checked == true)
                    {
                        Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                        Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");


                        #region ffffFF
                        int growcount = rd_view.Items.Count;
                        int k = 0;
                        for (int ix = 0; ix < growcount; ix++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                            if (chk.Checked == true)
                            {
                                Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                                Label lbl_course_name = (Label)rd_view.Items[ix].FindControl("lbl_course_name");

                                bool isamountfill = false;
                                for (int i = 0; i < grd_fee.Rows.Count; i++)
                                {
                                    TextBox lbl_reg_id = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                                    if (mycode.chkenum(lbl_reg_id.Text) == true)
                                    {
                                        isamountfill = true;
                                    }
                                }

                                bool chek_fee = My.find_mnthly_fee_collected("MonthlyFee", ddl_session.SelectedItem.Text, lbl_class_id.Text, lbl_month_name.Text);
                                if (chek_fee == false)
                                {
                                    Alertme("You can't add/update fee because fee has been taken.", "warning");
                                }
                                else
                                {
                                    if (isamountfill == true)
                                    {
                                        double totla = 0;
                                        for (int i = 0; i < grd_fee.Rows.Count; i++)
                                        {
                                            TextBox txt_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                                            Label lbl_content_id = (Label)grd_fee.Rows[i].FindControl("lbl_content_id");
                                            Label lbl_content = (Label)grd_fee.Rows[i].FindControl("lbl_content");
                                            totla = totla + My.toDouble(txt_fee.Text);
                                            //==============***************
                                            if (My.toDouble(txt_fee.Text) > 0)
                                            {
                                                insert_into_Fee_master_content_wise(txt_fee.Text, "MonthlyFee", ddl_fees_for.SelectedValue, lbl_content_id.Text, lbl_content.Text, lbl_class_id.Text, lbl_course_name.Text, lbl_month_name.Text, lbl_month_id.Text);
                                            }
                                            else
                                            {
                                                My.exeSql("delete from Fee_master_content_wise where content_id='" + lbl_content_id.Text + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + ddl_fees_for.SelectedValue + "' and class_id='" + lbl_class_id.Text + "' and Month='" + lbl_month_name.Text + "'");
                                            }
                                        }
                                        insert_dataFee_Master(totla, "MonthlyFee", ddl_fees_for.SelectedValue, lbl_class_id.Text, lbl_course_name.Text, lbl_month_name.Text, lbl_month_id.Text);
                                        ViewState["statusUp"] = "1";
                                        ViewState["msg"] = "Fee master has been created successfully";
                                    }
                                    else
                                    {
                                        Alertme("Please enter fee all fee type", "warning");
                                    }
                                }
                            }
                            else
                            {
                                k++;
                            }
                        }

                        if (k == growcount)
                        {
                            Alertme("Please check minimum one course.", "warning");
                            return;
                        }
                        #endregion


                    }
                    else
                    {
                        kl++;
                    }
                }

                if (kl == mgrowcount)
                {
                    Alertme("Please check minimum one month.", "warning");
                    return;
                }
            }
            else
            {


                int mgrowcount = rp_month.Items.Count;
                int kl = 0;
                for (int ixi = 0; ixi < mgrowcount; ixi++)
                {
                    CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                    if (chkM.Checked == true)
                    {
                        Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                        Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");


                        #region ffffFF
                        int growcount = rd_view.Items.Count;
                        int k = 0;
                        for (int ix = 0; ix < growcount; ix++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                            if (chk.Checked == true)
                            {
                                Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                                Label lbl_course_name = (Label)rd_view.Items[ix].FindControl("lbl_course_name");

                                #region trertRR
                                bool isamountfill = false;
                                for (int i = 0; i < grd_fee.Rows.Count; i++)
                                {
                                    TextBox lbl_reg_id = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                                    if (mycode.chkenum(lbl_reg_id.Text) == true)
                                    {
                                        isamountfill = true;
                                    }
                                }
                                if (isamountfill == true)
                                {
                                    double totla = 0;
                                    for (int i = 0; i < grd_fee.Rows.Count; i++)
                                    {
                                        TextBox txt_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                                        Label lbl_content_id = (Label)grd_fee.Rows[i].FindControl("lbl_content_id");
                                        Label lbl_content = (Label)grd_fee.Rows[i].FindControl("lbl_content");
                                        totla = totla + My.toDouble(txt_fee.Text);


                                        if (My.toDouble(txt_fee.Text) > 0)
                                        {
                                            insert_into_Fee_master_content_wise(txt_fee.Text, "MonthlyFee", ddl_fees_for.SelectedValue, lbl_content_id.Text, lbl_content.Text, lbl_class_id.Text, lbl_course_name.Text, lbl_month_name.Text, lbl_month_id.Text);
                                        }
                                        else
                                        {
                                            My.exeSql("delete from Fee_master_content_wise where content_id='" + lbl_content_id.Text + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + ddl_fees_for.SelectedValue + "' and class_id='" + lbl_class_id.Text + "' and Month='" + lbl_month_name.Text + "'");
                                        }


                                    }
                                    insert_dataFee_Master(totla, "MonthlyFee", ddl_fees_for.SelectedValue, lbl_class_id.Text, lbl_course_name.Text, lbl_month_name.Text, lbl_month_id.Text);
                                    ViewState["statusUp"] = "1";
                                    ViewState["msg"] = "Fee master has been updated successfully";
                                }
                                else
                                {
                                    Alertme("Please enter fee all fee type", "warning");
                                }
                                #endregion
                            }
                            else
                            {
                                k++;
                            }
                        }

                        if (k == growcount)
                        {
                            Alertme("Please check minimum one course.", "warning");
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        kl++;
                    }
                }

                if (kl == mgrowcount)
                {
                    Alertme("Please check minimum one month.", "warning");
                    return;
                }
            }
        }


        private void insert_dataFee_Master(double totla, string Parmametername, string Parmameternameid, string class_id, string class_name, string month_name, string month_id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Fee_Master where  session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + Parmameternameid + "'  and class_id='" + class_id + "' and Month_id='" + month_id + "'");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Fee_Master (Session,Class,Parameter,Amount,class_id,session_id,parameter_id,Acamedic_Semester_Id,Type,User_id,Date,time,Ledger,Semester_Year,Month,Month_id) values (@Session,@Class,@Parameter,@Amount,@class_id,@session_id,@parameter_id,@Acamedic_Semester_Id,@Type,@User_id,@Date,@time,@Ledger,@Semester_Year,@Month,@Month_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Class", class_name);
                cmd.Parameters.AddWithValue("@Parameter", Parmametername);
                cmd.Parameters.AddWithValue("@Amount", totla.ToString("0.00"));
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@parameter_id", Parmameternameid);
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Type", "Yearwise");
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time()); 
                cmd.Parameters.AddWithValue("@Ledger", "School");
                cmd.Parameters.AddWithValue("@Semester_Year", "");
                cmd.Parameters.AddWithValue("@Month", month_name);
                cmd.Parameters.AddWithValue("@Month_id", month_id);
                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string query = "update Fee_Master set Amount=@Amount,User_id=@User_id,Date=@Date,time=@time where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Amount", totla.ToString("0.00"));
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        private void insert_into_Fee_master_content_wise(string fee, string Parmametername, string Parmameternameid, string content_id, string content, string classiD, string class_name, string month_name, string month_id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Fee_master_content_wise where content_id='" + content_id + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + Parmameternameid + "'   and class_id='" + classiD + "' and Month_id='" + month_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Fee_master_content_wise (content,content_id,amount,parameter,class,session,session_id,class_id,parameter_id,Ledger,Acamedic_Semester_Id,Type,User_id,Date,time,Semester_Year,Month,Month_id) values (@content,@content_id,@amount,@parameter,@class,@session,@session_id,@class_id,@parameter_id,@Ledger,@Acamedic_Semester_Id,@Type,@User_id,@Date,@time,@Semester_Year,@Month,@Month_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@content_id", content_id);
                cmd.Parameters.AddWithValue("@amount", My.toDouble(fee).ToString("0.00"));
                cmd.Parameters.AddWithValue("@parameter", Parmametername);
                cmd.Parameters.AddWithValue("@class", class_name);
                cmd.Parameters.AddWithValue("@session", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@class_id", classiD);
                cmd.Parameters.AddWithValue("@parameter_id", Parmameternameid);
                cmd.Parameters.AddWithValue("@Ledger", "School");
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Type", "Yearwise");
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Semester_Year", "");

                cmd.Parameters.AddWithValue("@Month", month_name);
                cmd.Parameters.AddWithValue("@Month_id", month_id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string query = "update Fee_master_content_wise set amount=@amount,content=@content,User_id=@User_id,Date=@Date,time=@time where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@amount", My.toDouble(fee).ToString("0.00"));
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("monthly-fee-master.aspx", false);
        }
        #region edit and delete




        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Acamedic_Semester_Id = (Label)row.FindControl("lbl_Acamedic_Semester_Id");
                    Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_Type = (Label)row.FindControl("lbl_Type");
                    Label lbl_month = (Label)row.FindControl("lbl_month");
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");

                    bool chek_fee = My.find_mnthly_fee_collected("MonthlyFee", lbl_Session.Text, lbl_class_id.Text, lbl_month.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't edit because fee has been taken.", "warning");
                        return;
                    }


                    ddl_session.SelectedValue = lbl_session_id.Text;
                    //ddl_course.SelectedValue = lbl_class_id.Text;
                    ViewState["courseID"] = lbl_class_id.Text;
                    ViewState["monthS"] = lbl_month.Text;
                    Bind_admission_fee_type();
                    Bind_course_details();
                    bind_month();
                    btn_Submit.Text = "Update";
                    btn_Submit.Visible = true;
                    btn_cancel.Visible = true;
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
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Acamedic_Semester_Id = (Label)row.FindControl("lbl_Acamedic_Semester_Id");
                    Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");
                    Label lbl_month = (Label)row.FindControl("lbl_month");

                    bool chek_fee = My.find_mnthly_fee_collected("MonthlyFee", lbl_Session.Text, lbl_class_id.Text, lbl_month.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't edit because fee has been taken.", "warning");
                        return;
                    }

                    mycode.executequery("delete from Fee_Master where   parameter_id=" + lbl_parameter_id.Text + " and class_id=" + lbl_class_id.Text + " and session_id='" + lbl_session_id.Text + "' and Month='" + lbl_month.Text + "'");
                    mycode.executequery("delete from Fee_master_content_wise where   parameter_id=" + lbl_parameter_id.Text + " and class_id=" + lbl_class_id.Text + " and session_id='" + lbl_session_id.Text + "' and Month='" + lbl_month.Text + "'");
                    Alertme("Deletion process has been successfully done", "success");
                    Bind_fee();

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
        #endregion



        protected void lnk_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");
            Label lbl_Acamedic_Semester_Id = (Label)row.FindControl("lbl_Acamedic_Semester_Id");
            Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
            Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
            Label lbl_month = (Label)row.FindControl("lbl_month");
            Label lbl_Class = (Label)row.FindControl("lbl_Class");
            hd_class_name.Value = lbl_Class.Text;
            Bind_details(lbl_parameter_id.Text, lbl_month.Text, lbl_session_id.Text, lbl_class_id.Text);
            Bind_course_fee_details(lbl_parameter_id.Text, lbl_month.Text, lbl_session_id.Text, lbl_class_id.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void Bind_course_fee_details(string parameter_id, string month, string session_id, string class_id)
        {
            try
            {
                string query1 = "Select * from Fee_master_content_wise where Ledger='School' and session_id='" + session_id + "' and class_id='" + class_id + "' and parameter_id='" + parameter_id + "' and Month='" + month + "'  order by Id asc ";
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    grid_feedetails.DataSource = null;
                    grid_feedetails.DataBind();

                }
                else
                {
                    grid_feedetails.DataSource = dt1;
                    grid_feedetails.DataBind();

                }
            }
            catch
            {

            }
        }

        private void Bind_details(string parameter_id, string month, string session_id, string class_id)
        {
            try
            {
                string query1 = "Select * from Fee_Master where Ledger='School'  and session_id='" + session_id + "' and class_id='" + class_id + "' and parameter_id='" + parameter_id + "' and Month='" + month + "'  order by Semester_Year asc ";
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
                else
                {
                    Repeater1.DataSource = dt1;
                    Repeater1.DataBind();
                }
            }
            catch
            {

            }
        }

        double total = 0;
        protected void grid_feedetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_amount = (Label)e.Row.FindControl("lbl_amount");

                if (lbl_amount.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_amount.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalamount = (Label)e.Row.FindControl("lbl_totalamount");
                lbl_totalamount.Text = total.ToString("0.00");
            }
        }


        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
                }
            }
        }

        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rd_view.Items.Count; j++)
            {
                CheckBox chk_class = rd_view.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }

        protected void chk_all_month_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_month.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_month.Items[j].FindControl("chk_month_name") as CheckBox;
                if (chk_all_month.Checked)
                {
                    chk_month_name.Checked = true;
                }
                else
                {
                    chk_month_name.Checked = false;
                }
            }
        }

        protected void rp_month_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_month_name")).Text == ViewState["monthS"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_month_name")).Checked = true;
                }
            }
        }

        protected void ddl_months_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session_serach.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_course_search.SelectedItem.Text == "Select")
            {
                Alertme("Please select course", "warning");
            }
            else if (ddl_months.SelectedItem.Text == "Select")
            {
                Alertme("Please select month", "warning");
            }
            else
            {
                Bind_fee();
            }
        }


        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt_fee = (TextBox)e.Row.FindControl("txt_fee");
                Label lbl_content_id = (Label)e.Row.FindControl("lbl_content_id");

                Bind_data_if_add_fee(lbl_content_id.Text, txt_fee);
            }
        }

        private void Bind_data_if_add_fee(string content_id, TextBox txt_fee)
        {
            string query1 = "Select * from Fee_master_content_wise where Ledger='School' and parameter_id='4' and content_id=" + content_id + " and session_id=" + ddl_session.SelectedValue + " and class_id='" + ViewState["courseID"].ToString() + "' and Month='" + ViewState["monthS"].ToString() + "'";
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                txt_fee.Text = "0";
            }
            else
            {
                txt_fee.Text = dt1.Rows[0]["amount"].ToString();
            }
        }

        protected void ddl_fees_type_srch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_fee();
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export_MonthlyFee.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        #region 
        protected void btn_copy_monthley_for_day_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {

                monthly_fee_copy_for_day_scholar();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                monthly_fee_copy_for_day_scholar();
            }

            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void monthly_fee_copy_for_day_scholar()
        {
            try
            {
                if (ddl_current_session.Text == "Select")
                {
                    Alertme("Please Select Copy From Session", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
                else if (ddl_copy_to_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please Select Copy From Session", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
                else
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Copy_Fee_session_Wise_Log_history (User_Id,Copy_Session_From,Copy_Session_To,Copy_Type,Copy_data_date_time) values (@User_Id,@Copy_Session_From,@Copy_Session_To,@Copy_Type,@Copy_data_date_time)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Copy_Session_From", ddl_current_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Copy_Session_To", ddl_copy_to_session.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Copy_Type", "MonthlyFee");
                    cmd.Parameters.AddWithValue("@Copy_data_date_time", My.getdate1());
                    if (My.InsertUpdateData(cmd))
                    {

                        //fee copy content_wise
                        string query1 = " select   fmcw.* from dbo.[Fee_master_content_wise] fmcw join  Add_course_table act on fmcw.class_id=act.course_id  where fmcw.session_id='" + ddl_current_session.SelectedValue + "' and fmcw.Parameter in ('MonthlyFee')  order by fmcw.Id,act.Position";
                        DataTable dt = mycode.FillData(query1);
                        if (dt.Rows.Count == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string content = dt.Rows[i]["content"].ToString();
                                string content_id = dt.Rows[i]["content_id"].ToString();
                                string amount = dt.Rows[i]["amount"].ToString();
                                string parameter = dt.Rows[i]["parameter"].ToString();
                                string class_name = dt.Rows[i]["class"].ToString();
                                string class_id = dt.Rows[i]["class_id"].ToString();
                                string parameter_id = dt.Rows[i]["parameter_id"].ToString();
                                string Ledger = dt.Rows[i]["Ledger"].ToString();
                                string Type = dt.Rows[i]["Type"].ToString();
                                string Month = dt.Rows[i]["Month"].ToString();
                                string Month_id = dt.Rows[i]["Month_id"].ToString();

                                copy_data_new_session_master_content_wise(content, content_id, amount, parameter, class_name, class_id, parameter_id, Ledger, Type, Month, Month_id);
                            }
                        }


                        //fee copy content_wise
                        string query3 = "  select   fmcw.* from dbo.[Fee_Master] fmcw join  Add_course_table act on fmcw.class_id=act.course_id  where fmcw.session_id='" + ddl_current_session.SelectedValue + "' and fmcw.Parameter in ('MonthlyFee')  order by fmcw.Id,act.Position";
                        DataTable dt3 = mycode.FillData(query3);
                        if (dt3.Rows.Count == 0)
                        {
                        }
                        else
                        {
                            for (int i = 0; i < dt3.Rows.Count; i++)
                            {
                                string class_name = dt3.Rows[i]["Class"].ToString();
                                string parameter = dt3.Rows[i]["Parameter"].ToString();
                                string class_id = dt3.Rows[i]["class_id"].ToString();
                                string parameter_id = dt3.Rows[i]["parameter_id"].ToString();
                                string amount = dt3.Rows[i]["amount"].ToString();
                                string Ledger = dt3.Rows[i]["Ledger"].ToString();
                                string Month = dt3.Rows[i]["Month"].ToString();
                                string Month_id = dt3.Rows[i]["Month_id"].ToString();
                                copy_data_new_session_Fee_Master(amount, parameter, class_name, class_id, parameter_id, Ledger, Month, Month_id);
                            }

                        }
                    }
                    Alertme("Your fees has been copied successfully done", "success");


                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Copy monthly fee Master day");
            }
        }

        private void copy_data_new_session_Fee_Master(string amount, string parameter, string class_name, string class_id, string parameter_id, string ledger, string Month, string Month_id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Fee_Master where  session_id='" + ddl_copy_to_session.SelectedValue + "' and parameter_id='" + parameter_id + "'  and class_id='" + class_id + "' and Month_id='" + Month_id + "'");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Fee_Master (Session,Class,Parameter,Amount,class_id,session_id,parameter_id,User_id,Date,time,Ledger,Copy_type,Month,Month_id) values (@Session,@Class,@Parameter,@Amount,@class_id,@session_id,@parameter_id,@User_id,@Date,@time,@Ledger,@Copy_type,@Month,@Month_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session", ddl_copy_to_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Class", class_name);
                cmd.Parameters.AddWithValue("@Parameter", parameter);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@session_id", ddl_copy_to_session.SelectedValue);
                cmd.Parameters.AddWithValue("@parameter_id", parameter_id);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Ledger", ledger);
                cmd.Parameters.AddWithValue("@Copy_type", "bulkCopy");
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Month_id", Month_id);
                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {



            }
        }

        private void copy_data_new_session_master_content_wise(string content, string content_id, string amount, string parameter, string class_name, string class_id, string parameter_id, string ledger, string type, string Month, string Month_id)
        {

            string query = "  Select * from Fee_master_content_wise where session_id='" + ddl_copy_to_session.SelectedValue + "' and class_id='" + class_id + "' and content_id='" + content_id + "' and parameter_id='" + parameter_id + "' and Month_id='" + Month_id + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;

                string query2 = "INSERT INTO Fee_master_content_wise (content,content_id,amount,parameter,class,session,session_id,class_id,parameter_id,Ledger,User_id,Date,time,Copy_type,Month,Month_id) values (@content,@content_id,@amount,@parameter,@class,@session,@session_id,@class_id,@parameter_id,@Ledger,@User_id,@Date,@time,@Copy_type,@Month,@Month_id)";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@content_id", content_id);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@parameter", parameter);
                cmd.Parameters.AddWithValue("@class", class_name);
                cmd.Parameters.AddWithValue("@session", ddl_copy_to_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@session_id", ddl_copy_to_session.SelectedValue);
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@parameter_id", parameter_id);
                cmd.Parameters.AddWithValue("@Ledger", ledger);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Copy_type", "Bulkcopy");
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Month_id", Month_id);
                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {

            }


        }
        #endregion
    }
}