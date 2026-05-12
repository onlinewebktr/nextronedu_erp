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
    public partial class Transport_Fine_Setting : System.Web.UI.Page
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

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Session from session_details where session_id=Transport_Fine_Apply_Setting.Session_id) as Session from Transport_Fine_Apply_Setting where Status!=555");
            if (dt.Rows.Count == 0)
            {
                //Alertme("Sorry there are no data list exist", "warning");
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
                else if (ddl_is_transport_fine.SelectedItem.Text == "NO")
                {
                    Alertme("Please select is transport fine applied", "warning");
                    ddl_is_transport_fine.Focus();
                }
                else
                {
                    bool chek_fine_add = My.check_fine_add();
                    if (chek_fine_add == true)
                    {
                        save_transport_setting();
                    }
                    else
                    {
                        Alertme("Sorry! Please set fine amount  before you have set transport fine amount", "warning");
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_transport_setting()
        {
            DataTable dt = mycode.FillData("select Id from Transport_Fine_Apply_Setting where Session_id='" + ddl_session.SelectedValue + "' and Status!=555");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Transport_Fine_Apply_Setting (Session_id,Is_Transport_fine_apply,Created_by,Created_date,Status,Multiply) values (@Session_id,@Is_Transport_fine_apply,@Created_by,@Created_date,@Status,@Multiply)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Transport_fine_apply", ddl_is_transport_fine.Text);

                cmd.Parameters.AddWithValue("@Created_by", Session["Admin"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@Multiply", ddl_fine_multiplay.Text);
                if (My.InsertUpdateData(cmd))
                {
                    bind_grd_view();
                    Alertme("Record has been saved successfully.", "success");
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string query = "Update Transport_Fine_Apply_Setting set Is_Transport_fine_apply=@Is_Transport_fine_apply,Updated_by=@Updated_by,Updated_Date=@Updated_Date,Status=@Status,Multiply=@Multiply where Id = @Id";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Is_Transport_fine_apply", ddl_is_transport_fine.Text);
                cmd.Parameters.AddWithValue("@Updated_by", Session["Admin"].ToString());
                cmd.Parameters.AddWithValue("@Updated_Date", mycode.date());
                cmd.Parameters.AddWithValue("@Multiply", ddl_fine_multiplay.Text);
                if (My.InsertUpdateData(cmd))
                {
                    bind_grd_view();
                    Alertme("Record has been update successfully.", "success");
                }

                //Alertme("Record already exist for this session.", "warning");




            }


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
            SqlDataAdapter ad = new SqlDataAdapter("select * from Transport_Fine_Apply_Setting where Id='" + Id + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Transport_Fine_Apply_Setting");
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