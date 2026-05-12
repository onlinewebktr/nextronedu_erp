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
    public partial class other_fee_setup : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) ");

                        mycode.bind_all_ddl_with_id(ddl_session_serach, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");
                        mycode.bind_all_ddl_with_id_All_New(ddl_course_search, "Select Course_Name,course_id from Add_course_table order by Position");

                        Bind_course_fee_details();
                        bind_special_fee_type();

                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_serach.SelectedValue = My.get_session_id();





                        Bind_fee();
                        string pagename_current = Path.GetFileName("Admission_Fee_Master.aspx");
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
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Master");
            }
        }

        private void bind_special_fee_type()
        {
            mycode.bind_all_ddl_with_id(ddl_fee_type, "select Fee_head,Fee_head_id from Special_fee_head where Status='1' order by Fee_head asc");
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            { }
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


        private void Bind_fee()
        {
            try
            {
                string query1 = "";
                if (ddl_session_serach.SelectedItem.Text == "Select")
                {
                    ddl_session_serach.Focus();
                    Alertme("Please select session.", "warning");
                    return;
                }
                else
                {
                    if (ddl_course_search.SelectedItem.Text == "ALL")
                    {
                        query1 = "select t2.Course_Name,t1.*,(select top 1 Session from session_details where session_id=t1.session_id)  as session,(select top 1 Fee_head from Special_fee_head where Fee_head_id=t1.content_id)  as Fee_type from Special_fee_master t1 join Add_course_table t2 on t1.class_id=t2.course_id where session_id='" + ddl_session_serach.SelectedValue + "' order by t2.Position asc";
                    }
                    else
                    {
                        query1 = "select t2.Course_Name,t1.*,(select top 1 Session from session_details where session_id=t1.session_id)  as session,(select top 1 Fee_head from Special_fee_head where Fee_head_id=t1.content_id)  as Fee_type from Special_fee_master t1 join Add_course_table t2 on t1.class_id=t2.course_id where session_id='" + ddl_session_serach.SelectedValue + "' and t1.class_id='" + ddl_course_search.SelectedValue + "' order by t2.Position asc";
                    }
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
                    lbl_class22.Text = "Session: " + ddl_session_serach.SelectedItem.Text + " Class : " + ddl_course_search.SelectedItem.Text;
                    rd_viewaddedfee.DataSource = dt1;
                    rd_viewaddedfee.DataBind();
                }
            }
            catch
            {
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
                        txt_fee_amount.Text = "";
                        Alertme(ViewState["msg"].ToString(), "success");
                        btn_cancel.Visible = false;
                        btn_Submit.Text = "Add";
                        Bind_fee();
                        Bind_course_fee_details();
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    ViewState["statusUp"] = "0";
                    save_data();
                    if (ViewState["statusUp"].ToString() == "1")
                    {
                        txt_fee_amount.Text = "";
                        Alertme(ViewState["msg"].ToString(), "success");
                        btn_cancel.Visible = false;
                        btn_Submit.Text = "Add";
                        Bind_fee();
                        Bind_course_fee_details();
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



        private void save_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session name", "warning");
                return;
            }
            if (ddl_fee_type.SelectedItem.Text == "Select")
            {
                ddl_fee_type.Focus();
                Alertme("Please select fee type.", "warning");
                return;
            }
            if (My.toDouble(txt_fee_amount.Text) == 0)
            {
                txt_fee_amount.Focus();
                Alertme("Please enter amount.", "warning");
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
                        DataTable dtc = My.dataTable("select Id from Special_fee_master where session_id='" + ddl_session.SelectedValue + "' and class_id='" + lbl_class_id.Text + "' and content_id='" + ddl_fee_type.SelectedValue + "'");
                        if (dtc.Rows.Count == 0)
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Special_fee_master (content,content_id,amount,session_id,class_id,User_id,Date,time) values (@content,@content_id,@amount,@session_id,@class_id,@User_id,@Date,@time)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@content", ddl_fee_type.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@content_id", ddl_fee_type.SelectedValue);
                            cmd.Parameters.AddWithValue("@amount", txt_fee_amount.Text);
                            cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@class_id", lbl_class_id.Text);
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@time", mycode.time());
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmd;
                            string query = "update Special_fee_master set amount=@amount where content_id=@content_id and session_id=@session_id and class_id=@class_id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@content_id", ddl_fee_type.SelectedValue);
                            cmd.Parameters.AddWithValue("@amount", txt_fee_amount.Text);
                            cmd.Parameters.AddWithValue("@class_id", lbl_class_id.Text);
                            cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                            if (My.InsertUpdateData(cmd))
                            { }
                        }
                        ViewState["statusUp"] = "1";
                        ViewState["msg"] = "Fee master has been created successfully";
                    }
                    else
                    {
                        k++;
                    }
                }
                if (k == growcount)
                {
                    Alertme("Please select class.", "warning");
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
                        SqlCommand cmd;
                        string query = "update Special_fee_master set amount=@amount where content_id=@content_id and session_id=@session_id and class_id=@class_id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@content_id", ddl_fee_type.SelectedValue);
                        cmd.Parameters.AddWithValue("@amount", txt_fee_amount.Text);
                        cmd.Parameters.AddWithValue("@class_id", lbl_class_id.Text);
                        cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                        if (My.InsertUpdateData(cmd))
                        { }
                        ViewState["statusUp"] = "1";
                        ViewState["msg"] = "Fee master has been updated successfully.";
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



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("other-fee-setup.aspx", false);
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
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_content_id = (Label)row.FindControl("lbl_content_id");
                    Label lbl_Amount = (Label)row.FindControl("lbl_Amount");

                    ddl_session.SelectedValue = lbl_session_id.Text;
                    ddl_fee_type.SelectedValue = lbl_content_id.Text;
                    txt_fee_amount.Text = lbl_Amount.Text;


                    ViewState["courseID"] = lbl_class_id.Text;
                    Bind_course_fee_details();
                    btn_Submit.Text = "Update";
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
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_Session = (Label)row.FindControl("lbl_Session");
                    Label lbl_content_id = (Label)row.FindControl("lbl_content_id");


                    mycode.executequery("delete from Special_fee_master where content_id=" + lbl_content_id.Text + " and class_id=" + lbl_class_id.Text + " and session_id='" + lbl_session_id.Text + "' ");
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



        protected void btn_excels_Click1(object sender, EventArgs e)
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_serach.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else
                {
                    Bind_fee();
                }
            }
            catch (Exception ex)
            {
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
                    DataTable dt = My.dataTable("select Fee_head from Special_fee_head where Fee_head='" + txt_fee_head.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        string feeId = "SPF" + My.auto_serialS("entry_id");
                        SqlCommand cmd;
                        string query = "INSERT INTO Special_fee_head (Fee_head,Fee_head_id,Status,Created_by,Created_date,Created_time) values (@Fee_head,@Fee_head_id,@Status,@Created_by,@Created_date,@Created_time)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Fee_head", txt_fee_head.Text);
                        cmd.Parameters.AddWithValue("@Fee_head_id", feeId);
                        cmd.Parameters.AddWithValue("@Status", "1"); 
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                        if (My.InsertUpdateData(cmd))
                        { txt_fee_head.Text = ""; }
                        Alertme("Fee head has been added successfully.", "success");
                        bind_special_fee_type();
                    }
                    else
                    {
                        try
                        {
                            txt_fee_head.Text = "";
                            ddl_fee_type.Focus();
                            bind_special_fee_type();
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