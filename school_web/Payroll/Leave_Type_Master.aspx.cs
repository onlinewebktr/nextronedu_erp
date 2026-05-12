using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class Leave_Type_Master : System.Web.UI.Page
    {
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

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Leave_Type_Master");
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
            DataTable dt = mycode.FillData("select * from PRL_Leave_Name_Master");
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

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_name.Text = "";
            txt_description.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_name.Text == "")
            {
                Alertme("Please enter leve name ", "warning");
                txt_name.Focus();
                return;
            }



            if (btn_Submit.Text == "Add")
            {
                submit_details();
                empty_form();
                bind_grd_view();
            }
            else
            {
                update_update_details();
                empty_form();
                bind_grd_view();
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Leave_Name_Master where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["Leave_Name"] = txt_name.Text;
                dr["Short_Name"] = txt_description.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Leave Name Updated Successfully", "success");
        }

        private void submit_details()
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from PRL_Leave_Name_Master where Leave_Name='" + txt_name.Text.Trim() + "'   ");
            if (dt.Rows.Count == 0)
            {
                string contentid = get_contentid();
                string query = "INSERT INTO PRL_Leave_Name_Master (Leave_Name,Leave_Name_Id,Short_Name) values (@Leave_Name,@Leave_Name_Id,@Short_Name)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Leave_Name", txt_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Leave_Name_Id", contentid);
                cmd.Parameters.AddWithValue("@Short_Name", txt_description.Text);

                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Leave name has bee created successfully", "success");
                }

            }
            else
            {

                Alertme("Sorry this leave name already esxit ", "warning");
            }


        }

        private string get_contentid()
        {
            bool duplicate = false;
            string session_id = mycode.auto_serial("Leave_Name_Id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Leave_Name_Id from dbo.[PRL_Leave_Name_Master] where Leave_Name_Id='" + session_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    session_id = mycode.auto_serial("Leave_Name_Id");
                }
            }
            return session_id;
        }

        private void empty_form()
        {
            txt_name.Text = "";
            txt_description.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_name = (Label)row.FindControl("lbl_name");
                Label lbl_description = (Label)row.FindControl("lbl_description");

                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;

                txt_name.Text = lbl_name.Text;
                txt_description.Text = lbl_description.Text;

                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_department_id = (Label)row.FindControl("lbl_department_id");

            My.exeSql("delete from PRL_Leave_Name_Master where Id='" + lbl_Id.Text + "'");
            Alertme("Leave name has been deleted successfully", "success");
            bind_grd_view();

        }
    }
}