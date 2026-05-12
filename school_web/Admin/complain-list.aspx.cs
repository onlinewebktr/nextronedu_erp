using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class complain_list : System.Web.UI.Page
    {
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
                        DataTable dt = My.dataTable("select Company from Firm_Details");
                        if (dt.Rows[0]["Company"].ToString() == "EDUNEXTG")
                        {
                            ViewState["companyname"] = "EDUNEXTG";
                        }
                        else
                        {
                            ViewState["companyname"] = "PURNANK";
                        }

                        txt_s_date.Text = mycode.date();
                        txt_e_date.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["user_type"] = My.get_user_type(ViewState["Userid"].ToString());
                        ViewState["firm_id"] = My.get_firm_id();
                        hd_branch.Value = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_request_grd("All", 0, 0);// bind_count();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StdentRegistration");
            }
        }


        private void bind_count()
        {
            SqlCommand cmd = new SqlCommand("sp_Complain_request");
            if (ViewState["user_type"].ToString() == "Admin")
            {
                cmd.Parameters.AddWithValue("@User_id ", "All");
            }
            else
            {
                cmd.Parameters.AddWithValue("@User_id ", "All");
               // cmd.Parameters.AddWithValue("@User_id ", ViewState["Userid"].ToString());
            }
            cmd.Parameters.AddWithValue("@Firm_id ", ViewState["firm_id"].ToString());

            cmd.Parameters.AddWithValue("@sp_status ", "3");
            DataSet ds = compLN.executeReaderDataSet_comp(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                lbl_total_data.Text = dt.Rows[0]["All_req"].ToString();
                lbl_pending_comp.Text = dt.Rows[0]["Pending"].ToString();
                lbl_closed_comp.Text = dt.Rows[0]["Closed"].ToString();

                lbl_runing_comp.Text = dt.Rows[0]["Process"].ToString();
                lbl_ttl_hold_comp.Text = dt.Rows[0]["Hold"].ToString();
            }
            else
            {
                lbl_total_data.Text = "0";
                lbl_pending_comp.Text = "0";
                lbl_closed_comp.Text = "0";
            }
        }



        private void bind_request_grd(string type, int idate1, int idate21)
        {
            SqlCommand cmd = new SqlCommand("sp_Complain_request");

            if (ViewState["user_type"].ToString() == "Admin")
            {
                cmd.Parameters.AddWithValue("@User_id ", "All");
            }
            else
            {
                cmd.Parameters.AddWithValue("@User_id ", "All");
                //cmd.Parameters.AddWithValue("@User_id ", ViewState["Userid"].ToString());
            }


            cmd.Parameters.AddWithValue("@Firm_id ", ViewState["firm_id"].ToString());
            cmd.Parameters.AddWithValue("@Branch_id ", hd_branch.Value);
            if (type == "All")
            {
                if (ddl_complain_status.SelectedValue == "0")
                {
                    cmd.Parameters.AddWithValue("@sp_status ", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status ", ddl_complain_status.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@sp_status ", "2");
                }
            }
            else
            {
                if (ddl_complain_status.SelectedValue == "0")
                {
                    cmd.Parameters.AddWithValue("@fromdate ", idate1);
                    cmd.Parameters.AddWithValue("@todate ", idate21);
                    cmd.Parameters.AddWithValue("@sp_status ", "5");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status ", ddl_complain_status.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@fromdate ", idate1);
                    cmd.Parameters.AddWithValue("@todate ", idate21);
                    cmd.Parameters.AddWithValue("@sp_status ", "4");
                }
            }

            DataSet ds = compLN.executeReaderDataSet_comp(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                lbl_total_data.Text = dt.Rows.Count.ToString();
            }
            else
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();

                lbl_total_data.Text = "0";
                lbl_pending_comp.Text = "0";
                lbl_runing_comp.Text = "0";
                lbl_ttl_hold_comp.Text = "0";
                lbl_closed_comp.Text = "0";
            }
        }


        protected void lnk_attachemnts_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }



        int Pending = 0; int Process = 0; int Hold = 0; int Closed = 0; int forward = 0;
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_msg_count")).Text == "0")
                {
                    ((Label)e.Item.FindControl("lbl_unread_message")).Visible = false;
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_unread_message")).Visible = true;
                }




                if (((Label)e.Item.FindControl("lbl_status")).Text == "Pending")
                {
                    Pending++;
                }
                if (((Label)e.Item.FindControl("lbl_status")).Text == "Process")
                {
                    Process++;
                }
                if (((Label)e.Item.FindControl("lbl_status")).Text == "Hold")
                {
                    Hold++;
                }
                if (((Label)e.Item.FindControl("lbl_status")).Text == "Closed")
                {
                    Closed++;
                }

                if (((Label)e.Item.FindControl("lbl_status")).Text == "Forward")
                {
                    forward++;
                }
                string req_id = ((Label)e.Item.FindControl("lbl_req_id")).Text;
                ///===========================
                if (((Label)e.Item.FindControl("lbl_status")).Text.ToUpper() == "CLOSED")
                {
                    ((Label)e.Item.FindControl("lbl_linksdV")).Visible = false;

                    if (((Label)e.Item.FindControl("lbl_feedback_gien")).Text.ToUpper() == "0")
                    {
                        ((Label)e.Item.FindControl("lbl_feedbackDV")).Visible = true;

                        if (ViewState["companyname"].ToString() == "EDUNEXTG")
                        {
                            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("url");
                            a1.HRef = "https://complain.edunextg.org/feedback.aspx?request=" + req_id; 
                        }
                        else
                        {
                            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("url");
                            a1.HRef = "http://complain.integertechnology.com/feedback.aspx?request=" + req_id; 
                        } 
                    }
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_feedbackDV")).Visible = false;
                    //===============================
                    if (((Label)e.Item.FindControl("lbl_links")).Text == "")
                    {
                        ((Label)e.Item.FindControl("lbl_linksdV")).Visible = false;
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lbl_linksdV")).Visible = true;
                    }
                }
            }

            //=============================
            lbl_pending_comp.Text = Pending.ToString();
            lbl_runing_comp.Text = Process.ToString();
            lbl_ttl_hold_comp.Text = Hold.ToString();
            lbl_closed_comp.Text = Closed.ToString();
            lbl_forward.Text = forward.ToString();
        }




        protected void ddl_complain_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_request_grd("All", 0, 0);
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
                    int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                    int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        string sdate = txt_s_date.Text;
                        string sday = sdate.Substring(0, 2);
                        string smonth = sdate.Substring(3, 2);
                        string syear = sdate.Substring(6, 4);

                        string edate = txt_e_date.Text;
                        string eday = edate.Substring(0, 2);
                        string emonth = edate.Substring(3, 2);
                        string eyear = edate.Substring(6, 4);

                        int idate1 = Convert.ToInt32(syear + smonth + sday);
                        int idate21 = Convert.ToInt32(eyear + emonth + eday);



                        if (idate > idate2)
                        {
                            Alertme("End date cannot be less than start date.", "warning");
                        }
                        else
                        {
                            final_find_report_by_date(idate1, idate21);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void final_find_report_by_date(int idate1, int idate21)
        {
            bind_request_grd("DtaewisE", idate1, idate21);
        }

        protected void lnk_view_Work_Progress_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_req_id = (Label)row.FindControl("lbl_req_id");
                Label lbl_closed_date = (Label)row.FindControl("lbl_closed_date");
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_status = (Label)row.FindControl("lbl_status");
                ViewState["req_id"] = lbl_req_id.Text;
                ViewState["status"] = lbl_status.Text;
                Bind_data_process(lbl_req_id.Text, lbl_status.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            catch
            {

            }

        }
        compLN cln = new compLN();
        public void Bind_data_process(string id, string status)
        {
            string query = "";
            //  string query = "select *,(select count(Id) from Complain_chat where Request_id=Complain_request.Request_id and ShowStatus is null) as msgCount,'hidden' as HideShow,format(Request_date, 'dd/MM/yyyy hh:mm tt') as New_Request_date,format(Last_reply_date, 'dd/MM/yyyy hh:mm tt') as Last_rep_date,format(Last_reply_date_Developer, 'dd/MM/yyyy hh:mm tt') as Last_reply_date_Developer1 from Complain_request where User_id=@User_id and Firm_id=@Firm_id and Branch_id=@Branch_id order by Last_reply_idate desc ";

            if (status == "Pending")
            {
                query = "select *,format(Request_date, 'dd/MM/yyyy hh:mm tt') as New_Request_date,format(Last_reply_date, 'dd/MM/yyyy hh:mm tt') as Last_rep_date,format(Last_reply_date_Developer, 'dd/MM/yyyy hh:mm tt') as Last_reply_date_Developer1 from Complain_request where    Request_id='" + id + "' order by Last_reply_idate desc ";
                DataTable dt = cln.FillDataComp(query);
                if (dt.Rows.Count == 0)
                {
                    lbl_status.Text = "";
                    lbl_date.Text = "";
                    lbl_lastquery.Text = "";
                }
                else
                {
                    lbl_status.Text = status;
                    lbl_date.Text = dt.Rows[0]["New_Request_date"].ToString();
                    lbl_lastquery.Text = dt.Rows[0]["Remarks"].ToString();
                }
                pnl_true_false("1");
            }

            else if (status == "Process")
            {
                query = "select *,format(Request_date, 'dd/MM/yyyy hh:mm tt') as New_Request_date,format(Last_reply_date, 'dd/MM/yyyy hh:mm tt') as Last_rep_date,format(Last_reply_date_Developer, 'dd/MM/yyyy hh:mm tt') as Last_reply_date_Developer1,(Select top 1 Message from Complain_chat where Request_id=Complain_request.Request_id and Docs_From=2 order by id desc) as lastchat from Complain_request where   Request_id='" + id + "' order by Last_reply_idate desc";
                DataTable dt = cln.FillDataComp(query);
                if (dt.Rows.Count == 0)
                {
                    lbl_status.Text = "";
                    lbl_date.Text = "";
                    lbl_lastquery.Text = "";
                }
                else
                {
                    lbl_status.Text = status;
                    lbl_date.Text = dt.Rows[0]["Last_rep_date"].ToString();
                    lbl_lastquery.Text = dt.Rows[0]["lastchat"].ToString();
                }
                pnl_true_false("3");
            }
            else
            {
                query = "select *,format(Request_date, 'dd/MM/yyyy hh:mm tt') as New_Request_date,format(Last_reply_date, 'dd/MM/yyyy hh:mm tt') as Last_rep_date,format(Last_reply_date_Developer, 'dd/MM/yyyy hh:mm tt') as Last_reply_date_Developer1,(Select top 1 Message from Complain_chat where Request_id=Complain_request.Request_id and Docs_From=4 order by id desc) as lastchat from Complain_request where   Request_id='" + id + "' order by Last_reply_idate desc";
                DataTable dt = cln.FillDataComp(query);
                if (dt.Rows.Count == 0)
                {
                    lbl_status.Text = "";
                    lbl_date.Text = "";
                    lbl_lastquery.Text = "";
                }
                else
                {
                    lbl_status.Text = status;
                    lbl_date.Text = dt.Rows[0]["Last_rep_date"].ToString();
                    lbl_lastquery.Text = dt.Rows[0]["lastchat"].ToString();
                }
                pnl_true_false("4");
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void pnl_true_false(string from)
        {
            if (from == "1")
            {
                a1.Attributes.Add("class", "stepper-item active");
                a2.Attributes.Add("class", "stepper-item");
                a3.Attributes.Add("class", "stepper-item");

                pnl_admission_type.Visible = true;
                lnk_running.Enabled = true;
                lnk_running.Enabled = false;
                lnk_closed.Enabled = false;
            }
            else if (from == "2")
            {
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item active");
                a3.Attributes.Add("class", "stepper-item");
                pnl_admission_type.Visible = true;


                lnk_running.Enabled = true;
                lnk_closed.Enabled = false;

            }
            else if (from == "3")
            {
                lnk_running.Enabled = true;
                lnk_closed.Enabled = false;
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item completed");
                a3.Attributes.Add("class", "stepper-item active");

                pnl_admission_type.Visible = true;

            }
            else
            {
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item completed");
                a3.Attributes.Add("class", "stepper-item completed");
                pnl_admission_type.Visible = true;

                lnk_running.Enabled = true;
                lnk_running.Enabled = true;
                lnk_closed.Enabled = true;
            }

            if (ViewState["status"].ToString() == "Closed")
            {
                lnk_running.Enabled = true;
                lnk_running.Enabled = true;
                lnk_closed.Enabled = true;
            }
            else if (ViewState["status"].ToString() == "Hold")
            {
                lnk_running.Enabled = true;
                lnk_running.Enabled = true;
                lnk_closed.Enabled = true;
            }
            else
            {

            }
        }
        protected void lnk_Pending_Click(object sender, EventArgs e)
        {

            Bind_data_process(ViewState["req_id"].ToString(), "Pending");

        }
        protected void lnk_running_Click(object sender, EventArgs e)
        {
            Bind_data_process(ViewState["req_id"].ToString(), "Process");
        }

        protected void lnk_closed_Click(object sender, EventArgs e)
        {
            Bind_data_process(ViewState["req_id"].ToString(), "Closed");
        }


    }
}