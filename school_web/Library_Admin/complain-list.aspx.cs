using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
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
            cmd.Parameters.AddWithValue("@User_id ", ViewState["Userid"].ToString());
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
            cmd.Parameters.AddWithValue("@User_id ", ViewState["Userid"].ToString());
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

        int Pending = 0; int Process = 0; int Hold = 0; int Closed = 0;
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
            }

            //=============================
            lbl_pending_comp.Text = Pending.ToString();
            lbl_runing_comp.Text = Process.ToString();
            lbl_ttl_hold_comp.Text = Hold.ToString();
            lbl_closed_comp.Text = Closed.ToString();
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
    }
}