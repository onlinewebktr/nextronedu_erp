using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Admin
{
    public partial class Hostel_Admission_Fee_or_Annual_Master : System.Web.UI.Page
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
                        btn_Submit.Visible = false;
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();

                        mycode.bind_all_ddl_with_id(ddl_hostel_name, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        mycode.bind_all_ddl_with_id(ddl_hoste_search, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        ddl_hoste_search.SelectedValue = My.get_top_one_hostel_name();
                        mycode.bind_all_ddl_with_id(ddl_session_serach, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session_serach.SelectedValue = My.get_session_id();
                        ddl_session.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_All_New(ddl_class_saerch, "Select Course_Name,course_id from Add_course_table order by Position");





                        string pagename_current = "Hostel_Master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        Bind_course_fee_details();
                        Bind_fee();
                    }


                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Type_Master");
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
        protected void ddl_fee_for_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_hostel_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel name", "warning");
            }
            else
            {
                Bind_fee_details();
            }

        }

        private void Bind_fee_details()
        {
            btn_Submit.Visible = false;
            string query1 = "Select * from Hostel_fee_head_master where group_id=" + ddl_fee_for.SelectedValue + " and  Session_id=" + ddl_session.SelectedValue + "";
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                btn_Submit.Visible = true;
                grd_fee.DataSource = dt1;
                grd_fee.DataBind();
            }
        }
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
            string parameter_id = "0";
            if (ddl_fee_for.SelectedValue == "1") //Hostel Admission Fee
            {
                parameter_id = "5";
            }
            else
            {
                parameter_id = "6";
            }
            string query1 = "Select * from Hostel_Fee_master_content_wise where session_id='" + ddl_session.SelectedValue + "' and parameter_id=" + parameter_id + " and content_id=" + content_id + "   and class_id='" + ViewState["courseID"].ToString() + "' ";
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




        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("Hostel_Admission_Fee_or_Annual_Master.aspx", false);
        }
        #region submit data
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["statusUp"] = "0";
                if (ViewState["Is_add"].ToString() == "1")
                {
                    save_data();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    save_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

                if (ViewState["statusUp"].ToString() == "1")
                {
                    Alertme(ViewState["msg"].ToString(), "success");
                    btn_cancel.Visible = false;
                    btn_Submit.Text = "Add";
                    Bind_fee();
                    empty_grid_fee();
                    Alertme(ViewState["msg"].ToString(), "success");
                    btn_cancel.Visible = false;
                    btn_Submit.Text = "Add";
                    Bind_fee();
                    empty_grid_fee();


                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddl_search_deefor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_fee();
        }
        private void Bind_fee()
        {
            try
            {
                string query1 = "";
                if (ddl_session_serach.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class_saerch.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                else if (ddl_hoste_search.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel", "warning");
                }
                else
                {
                    string parameter_New = "";
                    string parameter_id = "";
                    if (ddl_search_deefor.SelectedValue == "1") //Hostel Admission Fee
                    {
                        parameter_New = "HostelAdmissionFee";//5
                        parameter_id = "5";
                    }
                    else
                    {
                        parameter_New = "HostelAnnualFee";//6
                        parameter_id = "6";
                    }
                    if (ddl_class_saerch.SelectedItem.Text == "ALL")
                    {
                        query1 = "Select t1.*,t2.Course_Name,(Select top 1 Session from session_details where session_id=t1.session_id) as Session, (select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name  from Hostel_Fee_Master t1 join Add_course_table t2 on t1.class_id=t2.course_id where   t1.parameter_id=" + parameter_id + " and t1.session_id='" + ddl_session_serach.SelectedValue + "'  and t1.Hostel_id=" + ddl_hoste_search.SelectedValue + " order by t2.Position asc";
                    }
                    else
                    {
                        query1 = "Select t1.*,t2.Course_Name,(Select top 1 Session from session_details where session_id=t1.session_id) as Session, (select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name  from Hostel_Fee_Master t1 join Add_course_table t2 on t1.class_id=t2.course_id where   t1.parameter_id=" + parameter_id + " and t1.session_id='" + ddl_session_serach.SelectedValue + "' and t1.class_id='" + ddl_class_saerch.SelectedValue + "' and t1.Hostel_id=" + ddl_hoste_search.SelectedValue + " order by t2.Position asc";

                    }


                }
                print1.Visible = false;
                btn_excels.Visible = false;
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    rd_viewaddedfee.DataSource = null;
                    rd_viewaddedfee.DataBind();

                }
                else
                {
                    rd_viewaddedfee.DataSource = dt1;
                    rd_viewaddedfee.DataBind();

                    btn_excels.Visible = true;
                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        print1.Visible = true;
                    }
                    else
                    {
                        print1.Visible = false;
                    }
                }
            }
            catch
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
                ddl_hostel_name.Focus();
                Alertme("Please select session name", "warning");
                return;
            }
            else if (ddl_fee_for.SelectedItem.Text == "Select")
            {
                ddl_fee_for.Focus();
                Alertme("Please select fee for", "warning");
                return;
            }
            else
            {
                string parameter_New = "";
                string parameter_id = "";
                if (ddl_fee_for.SelectedValue == "1") //Hostel Admission Fee
                {
                    parameter_New = "HostelAdmissionFee";//5
                    parameter_id = "5";
                }
                else
                {
                    parameter_New = "HostelAnnualFee";//6
                    parameter_id = "6";
                }
                ddl_search_deefor.SelectedValue = ddl_fee_for.SelectedValue;
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
                            if (isamountfill == true)
                            {
                                bool chek_fee = My.find_fee_collected_hostel(ddl_session.Text, lbl_class_id.Text, parameter_New, ddl_hostel_name.SelectedValue);
                                if (chek_fee == false)
                                {
                                    Alertme("You can't add/update fee because fee has been taken.", "warning");
                                }
                                else
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
                                            insert_into_Fee_master_content_wise(txt_fee.Text, parameter_New, parameter_id, lbl_content_id.Text, lbl_content.Text, lbl_class_id.Text, lbl_course_name.Text, ddl_hostel_name.SelectedValue);
                                        }
                                    }
                                    insert_dataFee_Master(totla, parameter_New, parameter_id, lbl_class_id.Text, ddl_hostel_name.SelectedValue);
                                    ViewState["statusUp"] = "1";
                                    ViewState["msg"] = "Fee master has been created successfully";
                                }
                            }
                            else
                            {
                                Alertme("Please enter fee all fee type", "warning");
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
                            if (isamountfill == true)
                            {
                                double totla = 0;
                                for (int i = 0; i < grd_fee.Rows.Count; i++)
                                {
                                    TextBox txt_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                                    Label lbl_content_id = (Label)grd_fee.Rows[i].FindControl("lbl_content_id");
                                    Label lbl_content = (Label)grd_fee.Rows[i].FindControl("lbl_content");
                                    totla = totla + My.toDouble(txt_fee.Text);

                                    insert_into_Fee_master_content_wise(txt_fee.Text, parameter_New, parameter_id, lbl_content_id.Text, lbl_content.Text, lbl_class_id.Text, lbl_course_name.Text, ddl_hostel_name.SelectedValue);

                                }


                                insert_dataFee_Master(totla, parameter_New, parameter_id, lbl_class_id.Text, ddl_hostel_name.SelectedValue);

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
                }
            }

        }
        private void insert_into_Fee_master_content_wise(string fee, string Parmametername, string Parmameternameid, string content_id, string content, string classiD, string class_name, string hostel_id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Hostel_Fee_master_content_wise where content_id='" + content_id + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + Parmameternameid + "'   and class_id='" + classiD + "' and Hostel_id=" + hostel_id + "  ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Hostel_Fee_master_content_wise (content,content_id,amount,parameter,session,session_id,class_id,parameter_id,User_id,Date,time,Hostel_id) values (@content,@content_id,@amount,@parameter,@session,@session_id,@class_id,@parameter_id,@User_id,@Date,@time,@Hostel_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@content_id", content_id);
                cmd.Parameters.AddWithValue("@amount", My.toDouble(fee).ToString("0.00"));
                cmd.Parameters.AddWithValue("@parameter", Parmametername);
                cmd.Parameters.AddWithValue("@session", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@class_id", classiD);
                cmd.Parameters.AddWithValue("@parameter_id", Parmameternameid);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Hostel_id", hostel_id);
                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string query = "update Hostel_Fee_master_content_wise set amount=@amount,content=@content,User_id=@User_id,Date=@Date,time=@time where Id=@Id";
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
        private void insert_dataFee_Master(double totla, string Parmametername, string Parmameternameid, string class_id, string hostel_id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Hostel_Fee_Master where  session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + Parmameternameid + "'  and class_id='" + class_id + "' and Hostel_id=" + hostel_id + "     ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Hostel_Fee_Master (Parameter,Amount,class_id,session_id,parameter_id,User_id,Date,time,Hostel_id) values (@Parameter,@Amount,@class_id,@session_id,@parameter_id,@User_id,@Date,@time,@Hostel_id)";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Parameter", Parmametername);
                cmd.Parameters.AddWithValue("@Amount", totla.ToString("0.00"));
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@parameter_id", Parmameternameid);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Hostel_id", hostel_id);
                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string query = "update Hostel_Fee_Master set Amount=@Amount,User_id=@User_id,Date=@Date,time=@time where Id=@Id";
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
        #endregion


        #region edit delete 
        protected void txt_fee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bindttl_fee();
            }
            catch (Exception ex)
            {
            }
        }
        private void bindttl_fee()
        {
            int i;
            double totalrate = 0;
            int gridview_rowcount = grd_fee.Rows.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                TextBox lbl_net_fee = (TextBox)grd_fee.Rows[i].FindControl("txt_fee");
                if (lbl_net_fee.Text != "")
                {
                    totalrate = totalrate + Convert.ToDouble(lbl_net_fee.Text);
                }
            }
            lbl_totalmrp.Text = "Total Fee : " + totalrate.ToString("0.00");

            var footerRow = grd_fee.FooterRow;
            Label lbl_full_amount = (footerRow.FindControl("lbl_full_amount")) as Label;
            lbl_full_amount.Text = totalrate.ToString("0.00");
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
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_Type = (Label)row.FindControl("lbl_Type");
                    Label lbl_Hostel_id = (Label)row.FindControl("lbl_Hostel_id");
                    string parameter_New = "";
                    if (lbl_parameter_id.Text == "5") //Hostel Admission Fee
                    {
                        parameter_New = "HostelAdmissionFee";//5

                    }
                    else
                    {
                        parameter_New = "HostelAnnualFee";//6

                    }

                    bool chek_fee = My.find_fee_collected_hostel(ddl_session.Text, lbl_class_id.Text, parameter_New, lbl_Hostel_id.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't edit because fee has been taken.", "warning");
                        return;
                    }

                    ddl_session.SelectedValue = lbl_session_id.Text;
                    //ddl_course.SelectedValue = lbl_class_id.Text;
                    ViewState["courseID"] = lbl_class_id.Text;
                    Bind_fee_details();
                    Bind_course_fee_details();
                    bindttl_fee();
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
                    string parameter_New = "";
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");
                    Label lbl_Hostel_id = (Label)row.FindControl("lbl_Hostel_id");
                    if (lbl_parameter_id.Text == "5") //Hostel Admission Fee
                    {
                        parameter_New = "HostelAdmissionFee";//5

                    }
                    else
                    {
                        parameter_New = "HostelAnnualFee";//6

                    }

                    bool chek_fee = My.find_fee_collected_hostel(ddl_session.Text, lbl_class_id.Text, parameter_New, lbl_Hostel_id.Text);

                    if (chek_fee == false)
                    {
                        Alertme("You can't edit because fee has been taken.", "warning");
                        return;
                    }

                    mycode.executequery("delete from Hostel_Fee_Master where   parameter_id=" + lbl_parameter_id.Text + " and class_id=" + lbl_class_id.Text + " and session_id='" + lbl_session_id.Text + "' and Hostel_id=" + lbl_Hostel_id.Text + " ");
                    mycode.executequery("delete from Hostel_Fee_master_content_wise where   parameter_id=" + lbl_parameter_id.Text + " and class_id=" + lbl_class_id.Text + " and session_id='" + lbl_session_id.Text + "' and Hostel_id=" + lbl_Hostel_id.Text + " ");
                    Alertme("Deletion process has been successfully done", "success");
                    Bind_fee();
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

        #region view Data
        protected void lnk_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_parameter_id = (Label)row.FindControl("lbl_parameter_id");

            Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
            Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
            Label lbl_Hostel_id = (Label)row.FindControl("lbl_Hostel_id");
            Bind_details(lbl_parameter_id.Text, lbl_session_id.Text, lbl_class_id.Text, lbl_Hostel_id.Text);
            Bind_course_fee_details(lbl_parameter_id.Text, lbl_session_id.Text, lbl_class_id.Text, lbl_Hostel_id.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void Bind_details(string parameter_id, string session_id, string class_id, string Hostel_id)
        {
            try
            {
                string query1 = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=Hostel_Fee_Master.class_id) as Course_Name,(Select top 1 Session from session_details where session_id=Hostel_Fee_Master.session_id) as Session, (select top 1 Hostel_name from Hostels_master where Hostel_id=Hostel_Fee_Master.Hostel_id) as Hostel_name from Hostel_Fee_Master where    session_id='" + session_id + "' and class_id='" + class_id + "' and parameter_id='" + parameter_id + "' and  Hostel_id=" + Hostel_id + " ";
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
        private void Bind_course_fee_details(string parameter_id, string session_id, string class_id, string Hostel_id)
        {
            try
            {
                string query1 = "Select * from Hostel_Fee_master_content_wise where  session_id='" + session_id + "' and class_id='" + class_id + "' and parameter_id='" + parameter_id + "' and Hostel_id=" + Hostel_id + "  order by Id asc ";
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
        #endregion

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
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
    }
}