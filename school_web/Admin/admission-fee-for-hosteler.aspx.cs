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
    public partial class admission_fee_for_hosteler : System.Web.UI.Page
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
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session ");
                        ddl_session.SelectedValue = My.get_session_id();

                        //mycode.bind_all_ddl_with_id(ddl_course, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_session_serach, "Select Session,session_id from session_details order by Session ");
                        ddl_session_serach.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_All_New(ddl_course_search, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddl_hostel_name, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        mycode.bind_all_ddl_with_id(ddl_hostel_search, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        ddl_hostel_name.SelectedValue = My.get_top_one_hostel_id();
                        Bind_course_fee_details();
                        Bind_admission_fee_type();


                        Bind_fee();



                        string pagename_current = "admission-fee-for-hosteler.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        // find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            // print1.Visible = true;
                        }
                        else
                        {
                            //print1.Visible = false;
                        }
                        if (ViewState["Is_Download"].ToString() == "1")
                        {
                            LinkButton_excel.Visible = true;
                        }
                        else
                        {
                            LinkButton_excel.Visible = false;
                        }
                        mycode.bind_all_ddl_with_id(ddl_current_session, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");

                        mycode.bind_all_ddl_with_id(ddl_copy_to_session, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");

                    }
                }
            }

            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Master");
            }
        }

        #region excel
        protected void btn_excels_Click1(object sender, EventArgs e)
        {


            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export_AdmissionFee.xls");
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
        #endregion


        private void Bind_admission_fee_type()
        {
            string query1 = "Select * from Content_master where group_id='1' and  Ledger='Hostel'";
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
        private void Bind_course_fee_details()
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
                lbl_totalmrp.Text = "";
                Bind_fee();
                Bind_admission_fee_type();
            }
        }
        private void Bind_fee()
        {
            try
            {
                string query1 = "";
                if (ddl_course_search.SelectedItem.Text == "ALL")
                {
                    query1 = " Select fm.*,(select top 1 Hostel_name from Hostels_master where Hostel_id=fm.Hostel_id) as Hostel_name,act.Position from Fee_Master fm join  Add_course_table act on fm.class_id=act.course_id where fm.parameter_id='5' and fm.session_id='" + ddl_session_serach.SelectedValue + "' order by act.Position asc";
                }
                else
                {
                    query1 = "Select fm.*,(select top 1 Hostel_name from Hostels_master where Hostel_id=fm.Hostel_id) as Hostel_name,act.Position from Fee_Master fm join  Add_course_table act on fm.class_id=act.course_id where fm.parameter_id='5' and fm.session_id='" + ddl_session_serach.SelectedValue + "' and fm.class_id='" + ddl_course_search.SelectedValue + "' order by act.Position asc ";
                }



                Bind_grid_data(query1);
            }
            catch
            {
            }
        }

        protected void ddl_hostel_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_data_hostel_wise();
        }

        private void Bind_data_hostel_wise()
        {
            try
            {
                if (ddl_session_serach.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_course_search.SelectedItem.Text == "Select")
                {
                    Alertme("Please select course", "warning");

                }
                else if (ddl_hostel_search.SelectedItem.Text == "")
                {
                    Alertme("Please select hostel name", "warning");
                }
                else
                {
                    string query1 = "";
                    if (ddl_course_search.SelectedItem.Text == "ALL")
                    {
                        query1 = " Select fm.*,(select top 1 Hostel_name from Hostels_master where Hostel_id=fm.Hostel_id) as Hostel_name,act.Position from Fee_Master fm join  Add_course_table act on fm.class_id=act.course_id where fm.parameter_id='5' and fm.session_id='" + ddl_session_serach.SelectedValue + "' and Hostel_Id='" + ddl_hostel_search.SelectedValue + "' order by act.Position asc";
                    }
                    else
                    {
                        query1 = "Select fm.*,(select top 1 Hostel_name from Hostels_master where Hostel_id=fm.Hostel_id) as Hostel_name,act.Position from Fee_Master fm join  Add_course_table act on fm.class_id=act.course_id where fm.parameter_id='5' and fm.session_id='" + ddl_session_serach.SelectedValue + "' and fm.class_id='" + ddl_course_search.SelectedValue + "' and Hostel_Id='" + ddl_hostel_search.SelectedValue + "' order by act.Position asc ";
                    }


                    Bind_grid_data(query1);

                }


            }
            catch
            {
            }
        }

        private void Bind_grid_data(string query1)
        {
            ViewState["query1"] = query1;
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                rd_viewaddedfee.DataSource = null;
                rd_viewaddedfee.DataBind();
                btn_excels.Visible = false;
            }
            else
            {
                rd_viewaddedfee.DataSource = dt1;
                rd_viewaddedfee.DataBind();
                btn_excels.Visible = true;

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
                if (ddl_hostel_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel name", "warning");
                }
                else
                { 
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        ViewState["statusUp"] = "0";
                        save_data();
                        if (ViewState["statusUp"].ToString() == "1")
                        {
                            ddl_hostel_search.SelectedValue = ddl_hostel_name.SelectedValue;
                            Alertme(ViewState["msg"].ToString(), "success");
                            chk_all.Checked = false;
                            Bind_check_all();
                            btn_Submit.Visible = true;
                            btn_cancel.Visible = false;
                            btn_Submit.Text = "Add";
                            Bind_admission_fee_type();
                            Bind_grid_data(ViewState["query1"].ToString());
                        }
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        ViewState["statusUp"] = "0";
                        save_data();
                        if (ViewState["statusUp"].ToString() == "1")
                        {
                            ddl_hostel_search.SelectedValue = ddl_hostel_name.SelectedValue;
                            Alertme(ViewState["msg"].ToString(), "success");
                            chk_all.Checked = false;
                            Bind_check_all();
                            btn_Submit.Visible = true;
                            btn_cancel.Visible = false;
                            btn_Submit.Text = "Add";
                            Bind_admission_fee_type();
                            Bind_grid_data(ViewState["query1"].ToString());
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }

                }

            }
            catch (Exception ex)
            {
            }


        }

        private void save_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session name", "warning");
                return;
            }
            else if (ddl_hostel_name.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session name", "warning");
                return;
            }
            if (btn_Submit.Text == "Add")
            {
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

                        bool chek_fee = My.find_fee_collected_hostel("HostelAdmissionFee", ddl_session.SelectedItem.Text, lbl_class_id.Text);
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
                                        insert_into_Fee_master_content_wise(txt_fee.Text, "HostelAdmissionFee", "5", lbl_content_id.Text, lbl_content.Text, lbl_class_id.Text, lbl_course_name.Text);
                                    }
                                    else
                                    {
                                        My.exeSql("delete from Fee_master_content_wise where content_id='" + lbl_content_id.Text + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='5' and class_id='" + lbl_class_id.Text + "' and Hostel_Id=" + ddl_hostel_name.SelectedValue + "");
                                    }
                                }
                                insert_dataFee_Master(totla, "HostelAdmissionFee", "5", lbl_class_id.Text, lbl_course_name.Text);
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
            }
            else
            {
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

                        bool chek_fee = My.find_fee_collected_hostel("HostelAdmissionFee", ddl_session.SelectedItem.Text, lbl_class_id.Text);
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
                                    if (My.toDouble(txt_fee.Text) > 0)
                                    {
                                        insert_into_Fee_master_content_wise(txt_fee.Text, "HostelAdmissionFee", "5", lbl_content_id.Text, lbl_content.Text, lbl_class_id.Text, lbl_course_name.Text);
                                    }
                                    else
                                    {
                                        My.exeSql("delete from Fee_master_content_wise where content_id='" + lbl_content_id.Text + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='5'   and class_id='" + lbl_class_id.Text + "' and Hostel_Id=" + ddl_hostel_name.SelectedValue + "");
                                    }
                                }
                                insert_dataFee_Master(totla, "HostelAdmissionFee", "5", lbl_class_id.Text, lbl_course_name.Text);
                                ViewState["statusUp"] = "1";
                                ViewState["msg"] = "Fee master has been updated successfully";
                            }
                            else
                            {
                                Alertme("Please enter fee all fee type", "warning");
                            }
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
            }
        }


        private void insert_dataFee_Master(double totla, string Parmametername, string Parmameternameid, string class_id, string class_name)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Fee_Master where  session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + Parmameternameid + "'  and class_id='" + class_id + "' and Hostel_Id=" + ddl_hostel_name.SelectedValue + "      ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Fee_Master (Session,Class,Parameter,Amount,class_id,session_id,parameter_id,Acamedic_Semester_Id,Type,User_id,Date,time,Ledger,Semester_Year,Hostel_Id) values (@Session,@Class,@Parameter,@Amount,@class_id,@session_id,@parameter_id,@Acamedic_Semester_Id,@Type,@User_id,@Date,@time,@Ledger,@Semester_Year,@Hostel_Id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Class", class_name);
                cmd.Parameters.AddWithValue("@Parameter", Parmametername);
                cmd.Parameters.AddWithValue("@Amount", My.toDouble(totla).ToString("0.00"));
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@parameter_id", Parmameternameid);
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Type", "Yearwise");
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());

                cmd.Parameters.AddWithValue("@Ledger", "Hostel");
                cmd.Parameters.AddWithValue("@Semester_Year", "");
                cmd.Parameters.AddWithValue("@Hostel_Id", ddl_hostel_name.SelectedValue);

                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string query = "update Fee_Master set Amount=@Amount,User_id=@User_id,Date=@Date,time=@time,Hostel_Id=@Hostel_Id where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Amount", My.toDouble(totla).ToString("0.00"));
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                cmd.Parameters.AddWithValue("@Hostel_Id", ddl_hostel_name.SelectedValue);
                if (My.InsertUpdateData(cmd))
                {

                }


            }
        }

        private void insert_into_Fee_master_content_wise(string fee, string Parmametername, string Parmameternameid, string content_id, string content, string classiD, string class_name)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Fee_master_content_wise where content_id='" + content_id + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + Parmameternameid + "'   and class_id='" + classiD + "' and Hostel_Id=" + ddl_hostel_name.SelectedValue + "  ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Fee_master_content_wise (content,content_id,amount,parameter,class,session,session_id,class_id,parameter_id,Ledger,Acamedic_Semester_Id,Type,User_id,Date,time,Semester_Year,Hostel_Id) values (@content,@content_id,@amount,@parameter,@class,@session,@session_id,@class_id,@parameter_id,@Ledger,@Acamedic_Semester_Id,@Type,@User_id,@Date,@time,@Semester_Year,@Hostel_Id)";
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
                cmd.Parameters.AddWithValue("@Ledger", "Hostel");
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Type", "Yearwise");
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Semester_Year", "");
                cmd.Parameters.AddWithValue("@Hostel_Id", ddl_hostel_name.SelectedValue);

                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string query = "update Fee_master_content_wise set amount=@amount,content=@content,User_id=@User_id,Date=@Date,time=@time,Hostel_Id=@Hostel_Id where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@amount", My.toDouble(fee).ToString("0.00"));
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                cmd.Parameters.AddWithValue("@Hostel_Id", ddl_hostel_name.SelectedValue);

                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("admission-fee-for-hosteler.aspx", false);
        }


        #region edit and delete

        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lbl_content_id = (Label)e.Row.FindControl("lbl_content_id");
                TextBox txt_fee = (TextBox)e.Row.FindControl("txt_fee");
                Label lbl_totalmrp = (Label)e.Row.FindControl("lbl_totalmrp");


                Bind_data_if_add_fee(lbl_content_id.Text, txt_fee);
            }
        }

        private void Bind_data_if_add_fee(string content_id, TextBox txt_fee)
        {

            string query1 = "Select * from Fee_master_content_wise   where  parameter_id='5' and  content_id=" + content_id + " and session_id=" + ddl_session.SelectedValue + " and class_id='" + ddl_course_search.SelectedValue + "' and Hostel_Id='" + ddl_hostel_name.SelectedValue + "' ";
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                txt_fee.Text = "0";
            }
            else
            {
                txt_fee.Text = dt1.Rows[0]["amount"].ToString();
                btn_Submit.Text = "Update";
            }
        }



        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

                    Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_Type = (Label)row.FindControl("lbl_Type");
                    Label lbl_Hostel_Id = (Label)row.FindControl("lbl_Hostel_Id");
                    ddl_session.SelectedValue = lbl_session_id.Text;
                    ddl_session_serach.SelectedValue = lbl_session_id.Text;
                    ddl_course_search.SelectedValue = lbl_class_id.Text;
                    try
                    {
                        ddl_hostel_name.SelectedValue = lbl_Hostel_Id.Text;
                    }
                    catch
                    {

                    }
                    //ddl_course.SelectedValue = lbl_class_id.Text;
                    ViewState["courseID"] = lbl_class_id.Text;
                    Bind_admission_fee_type();
                    Bind_course_fee_details();

                    btn_Submit.Visible = true;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update"; 
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
                    Label lbl_Hostel_Id = (Label)row.FindControl("lbl_Hostel_Id");
                    mycode.executequery("delete from Fee_Master where   parameter_id=" + lbl_parameter_id.Text + " and class_id=" + lbl_class_id.Text + " and session_id='" + lbl_session_id.Text + "' and Hostel_Id='" + lbl_Hostel_Id.Text + "'");


                    mycode.executequery("delete from Fee_master_content_wise where   parameter_id=" + lbl_parameter_id.Text + " and class_id=" + lbl_class_id.Text + " and session_id='" + lbl_session_id.Text + "' and Hostel_Id='" + lbl_Hostel_Id.Text + "'");
                    Alertme("Deletion process has been successfully done", "success");
                    Bind_grid_data(ViewState["query1"].ToString());
                    btn_Submit.Visible = false;
                    btn_cancel.Visible = false;
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
            Label lbl_Hostel_Id = (Label)row.FindControl("lbl_Hostel_Id");
            Label  lbl_Class = (Label)row.FindControl("lbl_Class");
            hd_class_name.Value = lbl_Class.Text;

            Bind_details(lbl_parameter_id.Text, lbl_Acamedic_Semester_Id.Text, lbl_session_id.Text, lbl_class_id.Text, lbl_Hostel_Id.Text);

            Bind_course_fee_details(lbl_parameter_id.Text, lbl_Acamedic_Semester_Id.Text, lbl_session_id.Text, lbl_class_id.Text, lbl_Hostel_Id.Text);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void Bind_course_fee_details(string parameter_id, string Acamedic_Semester_Id, string session_id, string class_id, string Hostel_Id)
        {
            try
            {
                string query1 = "Select * from Fee_master_content_wise where  session_id='" + session_id + "' and class_id='" + class_id + "' and parameter_id='" + parameter_id + "' and Hostel_Id=" + Hostel_Id + "  order by Id asc ";
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

        private void Bind_details(string parameter_id, string Acamedic_Semester_Id, string session_id, string class_id, string Hostel_Id)
        {
            try
            {
                string query1 = "Select *,(select top 1 Hostel_name from Hostels_master where Hostel_id=Fee_Master.Hostel_id) as Hostel_name from Fee_Master where Ledger='Hostel'  and session_id='" + session_id + "' and class_id='" + class_id + "' and parameter_id='" + parameter_id + "' and Hostel_Id=" + Hostel_Id + "  order by Semester_Year asc ";
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
            Bind_check_all();
        }

        private void Bind_check_all()
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


        #region fee copy
        protected void btn_copy_admissionfee_for_day_Click(object sender, EventArgs e)
        {

            if (ViewState["Is_add"].ToString() == "1")
            {

                admission_fee_copy_for_day_scholar();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                admission_fee_copy_for_day_scholar();
            }

            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void admission_fee_copy_for_day_scholar()
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
                    bool check_fee_added = My.get_fee_added("HostelAdmissionFee", ddl_current_session.SelectedValue);
                    if (check_fee_added == false)
                    {
                        Alertme("Apologies, the chosen session does not cover the hostel admission fee, so it cannot be copied", "warning");
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
                        cmd.Parameters.AddWithValue("@Copy_Type", "HostelAdmissionFee");
                        cmd.Parameters.AddWithValue("@Copy_data_date_time", My.getdate1());
                        if (My.InsertUpdateData(cmd))
                        {
                            //fee copy content_wise
                            string query1 = " select   fmcw.* from dbo.[Fee_master_content_wise] fmcw join  Add_course_table act on fmcw.class_id=act.course_id  where fmcw.session_id='" + ddl_current_session.SelectedValue + "' and fmcw.Parameter in ('HostelAdmissionFee')  order by fmcw.Id,act.Position";
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
                                    string Hostel_Id = dt.Rows[i]["Hostel_Id"].ToString();
                                    copy_data_new_session_master_content_wise(content, content_id, amount, parameter, class_name, class_id, parameter_id, Ledger, Type, Hostel_Id);
                                }
                            }


                            //fee copy content_wise
                            string query3 = "  select   fmcw.* from dbo.[Fee_Master] fmcw join  Add_course_table act on fmcw.class_id=act.course_id  where fmcw.session_id='" + ddl_current_session.SelectedValue + "' and fmcw.Parameter in ('HostelAdmissionFee')  order by fmcw.Id,act.Position";
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
                                    string Hostel_Id = dt3.Rows[i]["Hostel_Id"].ToString();
                                    copy_data_new_session_Fee_Master(amount, parameter, class_name, class_id, parameter_id, Ledger, Hostel_Id);
                                }

                            }
                        }
                        Alertme("Your hostel admission fees has been copied successfully done", "success");

                    }




                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Copy hostel admission fee Master day");

            }
        }

        private void copy_data_new_session_Fee_Master(string amount, string parameter, string class_name, string class_id, string parameter_id, string ledger, string hostel_Id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Fee_Master where  session_id='" + ddl_copy_to_session.SelectedValue + "' and parameter_id='" + parameter_id + "'  and class_id='" + class_id + "' and Hostel_Id='" + hostel_Id + "'");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Fee_Master (Session,Class,Parameter,Amount,class_id,session_id,parameter_id,User_id,Date,time,Ledger,Copy_type,Hostel_Id) values (@Session,@Class,@Parameter,@Amount,@class_id,@session_id,@parameter_id,@User_id,@Date,@time,@Ledger,@Copy_type,@Hostel_Id)";
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
                cmd.Parameters.AddWithValue("@Hostel_Id", hostel_Id);

                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {



            }
        }

        private void copy_data_new_session_master_content_wise(string content, string content_id, string amount, string parameter, string class_name, string class_id, string parameter_id, string ledger, string type, string hostel_Id)
        {

            string query = "  Select * from Fee_master_content_wise where session_id='" + ddl_copy_to_session.SelectedValue + "' and class_id='" + class_id + "' and content_id='" + content_id + "' and parameter_id='" + parameter_id + "' and  Hostel_Id='"+ hostel_Id + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;

                string query2 = "INSERT INTO Fee_master_content_wise (content,content_id,amount,parameter,class,session,session_id,class_id,parameter_id,Ledger,User_id,Date,time,Copy_type,Hostel_Id) values (@content,@content_id,@amount,@parameter,@class,@session,@session_id,@class_id,@parameter_id,@Ledger,@User_id,@Date,@time,@Copy_type,@Hostel_Id)";
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
                cmd.Parameters.AddWithValue("@Hostel_Id", hostel_Id);

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