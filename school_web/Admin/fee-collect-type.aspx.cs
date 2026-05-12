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
    public partial class fee_collect_type : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString(); 
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_updated_date(); 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "FeeCollectType");
            }
        }



        private void find_updated_date()
        {

            DataTable dt = mycode.FillData("select *,(select top 1 name from user_details where user_id=Fee_collect_mode_history.Created_by) as Updated_by_name from Fee_collect_mode_history order by id desc");
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

            rd_monthwise.Checked = true;
            rd_inst_wise.Checked = false;
            string feetype = My.get_single_column_data("select top 1 Fee_collect_mode as Column_Name from Firm_Details");
            if (feetype == "Installment")
            {
                rd_monthwise.Checked = false;
                rd_inst_wise.Checked = true;
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
                if (rd_inst_wise.Checked == false && rd_monthwise.Checked == false)
                {
                    Alertme("Please choose method of fee collection.", "warning");
                }
                else
                {
                    string modetype = "Monthly";
                    if (rd_inst_wise.Checked == true)
                    {
                        modetype = "Installment";
                    }
                    My.exeSql("update Firm_Details set Fee_collect_mode='" + modetype + "'");


                    SqlCommand cmd;
                    string query = "INSERT INTO Fee_collect_mode_history (Mode_type,Created_by,Created_date,Created_time) values (@Mode_type,@Created_by,@Created_date,@Created_time)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Mode_type", modetype);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Fee collection method has been updated successfully.", "success");
                        find_updated_date();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}