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
    public partial class Bithday_list : System.Web.UI.Page
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
                        Dictionary<string, object> dc1 = My.get_push_credantial();
                        ViewState["type"] = (String)dc1["type"];
                        ViewState["project_id"] = (String)dc1["project_id"];
                        ViewState["private_key_id"] = (String)dc1["private_key_id"];
                        ViewState["client_email"] = (String)dc1["client_email"];
                        ViewState["client_id"] = (String)dc1["client_id"];
                        ViewState["private_key"] = dc1["private_key"].ToString().Replace("\\n", "\n");

                        txt_s_date.Text = mycode.date();
                        find_firm_details();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddlmonth, "Select  Month,Month_Id from Month_Index order by Month_Id asc");
                        ddlmonth.SelectedValue = mycode.get_current_month_id();
                        ViewState["today"] = "0";
                        ViewState["sessionid"] = My.get_session_id();
                        if (Request.QueryString["todaybirthday"] != null)
                        {
                            lbl_class22.Text = "Date :- " + mycode.date();
                            ViewState["today"] = "1";
                            Bind_data_birthday();

                        }
                        else
                        {
                            lbl_class22.Text = "Month:- " + ddlmonth.SelectedItem.Text;
                            ViewState["today"] = "0";
                            Bind_data_birthday();
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Active_Inactive_Student");
            }
        }

        private void Bind_data_birthday()
        {

            string query = "";
            if (ViewState["today"].ToString() == "0")
            {
                lbl_class22.Text = "Month:- " + ddlmonth.SelectedItem.Text;
                query = "select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id,dob,gcm_id,Session_id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ViewState["sessionid"].ToString() + "  and Status='1' and  (dob like'%" + mycode.day_month_2_new(txt_s_date.Text) + "%' or dob like'%" + mycode.day_month_new(txt_s_date.Text) + "%')     order by  cast( (Substring (dob,1,2)) as int) asc       ";
                bind_data_ingrid(query);
            }
            else
            {
                lbl_class22.Text = "Date :- " + mycode.date();
                query = "select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id,dob,gcm_id,Session_id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ViewState["sessionid"].ToString() + "  and Status='1' and  (dob like'%" + mycode.day_month_2() + "%' or dob like'%" + mycode.day_month() + "%')  order by  cast( (Substring (dob,1,2)) as int) asc     ";
                bind_data_ingrid(query);
            } 
        }

        private void bind_data_ingrid(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no birthday list found", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind(); 
            }
            else
            { 
                GrdView.DataSource = dt;
                GrdView.DataBind();
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

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Birthdaylist.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GrdView.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
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

        protected void btn_find_Click(object sender, EventArgs e)
        { 
            if (txt_s_date.Text == "")
            {
                Alertme("Please select date", "warning");
            }
            else
            {
                ViewState["today"] = "0";
                Bind_data_birthday();
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

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            lbl_message.Text = "";
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
            Label lbl_sessioinid = (Label)row.FindControl("lbl_sessioinid");
            Label lbl_gcm_id = (Label)row.FindControl("lbl_gcm_id");
            Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
            ViewState["gcm_id"] = lbl_gcm_id.Text;
            ViewState["studentname"] = lbl_studentname.Text;
            ViewState["admission_no"] = lbl_admission_no.Text;
            bind_student_info(lbl_sessioinid.Text, lbl_admission_no.Text);


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        private void bind_student_info(string sessionid, string admission_no)
        {
            DataTable dt = mycode.FillData("select * from admission_registor where admissionserialnumber='" + admission_no + "' and Session_id='" + sessionid + "' ");
            if (dt.Rows.Count == 0)
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                pnl_text.Visible = false;
            }
            else
            {
                pnl_text.Visible = true;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }



        protected void btn_sendpush_Click(object sender, EventArgs e)
        {

            

            if (txt_input.Text == "")
            {
                lbl_message.Text = "Please enter birthday message";
                Alertme("Please enter birthday message", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                //if (ViewState["gcm_id"].ToString() != "")
                //{
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = txt_input.Text;
                ss["title"] = "Birthday";
                ss["messagetype"] = "Message";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = ViewState["admission_no"].ToString();
                ss["type"] = ViewState["type"].ToString();
                ss["project_id"] = ViewState["project_id"].ToString();
                ss["private_key_id"] = ViewState["private_key_id"].ToString();
                ss["client_email"] = ViewState["client_email"].ToString();
                ss["client_id"] = ViewState["client_id"].ToString();
                ss["private_key"] = ViewState["private_key"].ToString();
                My.onlypush(ViewState["gcm_id"].ToString(), ss);
                lbl_message.Text = "";
                Alertme("Birthday message has been sent successfully", "success");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                //}

            }

        }

        protected void lnk_send_message_peridefine_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
            Label lbl_sessioinid = (Label)row.FindControl("lbl_sessioinid");
            Label lbl_gcm_id = (Label)row.FindControl("lbl_gcm_id");
            Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
            string message = "Dear " + lbl_studentname.Text + ", " + My.get_birthday_message();
            Dictionary<String, String> ss = new Dictionary<string, string>();
            ss["notification_id"] = Guid.NewGuid().ToString();
            ss["message"] = message;
            ss["title"] = "Birthday";
            ss["messagetype"] = "Message";
            ss["url"] = "";
            ss["link_url"] = "";
            ss["UserId"] = lbl_admission_no.Text;
            ss["type"] = ViewState["type"].ToString();
            ss["project_id"] = ViewState["project_id"].ToString();
            ss["private_key_id"] = ViewState["private_key_id"].ToString();
            ss["client_email"] = ViewState["client_email"].ToString();
            ss["client_id"] = ViewState["client_id"].ToString();
            ss["private_key"] = ViewState["private_key"].ToString();
            My.onlypush(lbl_gcm_id.Text, ss);
            Alertme("Birthday notification has been sent successfully", "success");
        }
    }
}