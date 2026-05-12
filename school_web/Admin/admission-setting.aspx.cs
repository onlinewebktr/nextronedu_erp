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
    public partial class admission_setting : System.Web.UI.Page
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
                        try
                        {
                            mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                            ddl_session.SelectedValue = My.get_session_id();
                            bind_grd_view();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
            }
        }

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Session from session_details where session_id=Admission_no_setting.Session_id) as Session from Admission_no_setting where Status!=555");
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session.Focus();
                }
                else if (ddl_is_adm_no_auto_create.SelectedItem.Text == "NO")
                {
                    Alertme("Please select is admission no. auto create.", "warning");
                    ddl_is_adm_no_auto_create.Focus();
                }
                else if (txt_prefix.Text == "")
                {
                    Alertme("Please enter prefix code.", "warning");
                    txt_prefix.Focus();
                }
                else if (txt_session_code.Text == "")
                {
                    Alertme("Please enter session code.", "warning");
                    txt_session_code.Focus();
                }
                else if (txt_adm_start_from.Text == "")
                {
                    Alertme("Please enter admission no start from.", "warning");
                    txt_adm_start_from.Focus();
                }
                else
                {
                    save_admission_setting();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_admission_setting()
        {
            if (mycode.IsUserExist("select Id from Admission_no_setting where Session_id='" + ddl_session.SelectedValue + "' and Status!=555"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Admission_no_setting (Session_id,Is_admission_no_auto_create,Prefix_Code,Session_code,Admission_no_start_from,Created_by,Created_date,Status,Admission_no_start_from_update) values (@Session_id,@Is_admission_no_auto_create,@Prefix_Code,@Session_code,@Admission_no_start_from,@Created_by,@Created_date,@Status,@Admission_no_start_from_update)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_admission_no_auto_create", ddl_is_adm_no_auto_create.Text);
                cmd.Parameters.AddWithValue("@Prefix_Code", txt_prefix.Text);
                cmd.Parameters.AddWithValue("@Session_code", txt_session_code.Text);
                cmd.Parameters.AddWithValue("@Admission_no_start_from", txt_adm_start_from.Text);
                cmd.Parameters.AddWithValue("@Admission_no_start_from_update", txt_adm_start_from.Text);
                cmd.Parameters.AddWithValue("@Created_by", Session["Admin"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Status", 1);
                if (My.InsertUpdateData(cmd))
                {
                    bind_grd_view();
                    txt_prefix.Text = "";
                    txt_session_code.Text = "";
                    txt_adm_start_from.Text = "";
                    Alertme("Record has been saved successfully.", "success");
                }
            }
            else
            {
                Alertme("Record already exist for this session.", "warning");
            }
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        { 
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            mycode.executequery("update Admission_no_setting set Status=555 where Id="+ lbl_Id.Text + "");
            Alertme("Record has been deleted Successfully", "success");
            bind_grd_view();
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnk_bnr_status = (LinkButton)(e.Item.FindControl("lnk_bnr_status"));
            Label lbl_Status = ((Label)(e.Item.FindControl("lbl_show_status")));
            if (lbl_Status.Text == "1")
            {
                lnk_bnr_status.Text = "Active";
                lnk_bnr_status.Attributes.Add("class", "lnk-btn-actv");
            }
            else
            {
                lnk_bnr_status.Text = "DeActive";
                lnk_bnr_status.Attributes.Add("class", "lnk-red-bg");
            }
        }


       
        protected void lnk_bnr_status_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label id = (Label)row.FindControl("lbl_Id");
                Label lbl_show_status = ((Label)(row.FindControl("lbl_show_status")));
                update_product_status(id.Text, lbl_show_status.Text);
            } 
            catch (Exception ex) 
            {

            }
        }

        private void update_product_status(string Id, string c_status)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_no_setting where Id='" + Id + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Admission_no_setting");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (c_status == "1")
                    {
                        dr["Status"] = "0";
                    }
                    else
                    {
                        dr["Status"] = "1";
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    bind_grd_view();
                    Alertme("Status has been updated successfully.", "success");
                }
            }
        }
    }
}