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
    public partial class Update_Sl_No_For_Student_month_Slip : System.Web.UI.Page
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
                   
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        bind_grd_view();
                        Bind_data_history();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update_Sl_No_For_Student_month_Slip");
            }
        }

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *   from dbo.[globle_data]");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are records available", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_new_slip_no = (Label)row.FindControl("lbl_new_slip_no");
                ViewState["old_slip_no"] = lbl_new_slip_no.Text;
                txt_newslipno.Text = lbl_new_slip_no.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
            catch
            {

            }
        }

        protected void btn_attribute_name_Click(object sender, EventArgs e)
        {
            if (txt_newslipno.Text == "")
            {
                Alertme("Please enter slip No.", "Warning");
            }
            else
            {
                bool send = false;
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    send = true;

                }
                else if (ViewState["Is_add"].ToString() == "1")
                {
                    send = true;

                }
                if (send == true)
                {
                    if (My.toint(txt_newslipno.Text.Trim()) > My.toint(ViewState["old_slip_no"].ToString()))
                    {


                        string query = "INSERT INTO Update_slip_no_history (Old_Slip_no,New_slip_no,Update_by,date_time) values (@Old_Slip_no,@New_slip_no,@Update_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())))";
                        SqlCommand cmd;
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Old_Slip_no", ViewState["old_slip_no"]);
                        cmd.Parameters.AddWithValue("@New_slip_no", txt_newslipno.Text.Trim());
                        cmd.Parameters.AddWithValue("@Update_by", ViewState["Userid"].ToString());

                        if (My.InsertUpdateData(cmd))
                        {
                            My.exeSql("update globle_data set slip_no='" + txt_newslipno.Text.Trim() + "' ");
                            Alertme("Record has been saved successfully", "success");
                            txt_newslipno.Text = "";
                            txt_remarks.Text = "";
                            btn_attribute_name.Text = "Add";
                            Bind_data_history();
                            bind_grd_view();

                        }
                    }
                    else
                    {
                        Alertme("Sorry, Please ensure the new slip ID is always greater than the old slip ID. Please enter a valid slip number.", "Warning");
                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);


        }

        private void Bind_data_history()
        {
            string query = "Select *,format(date_time,'dd/MM/yyyy') as date_time1 from Update_slip_no_history order by format(date_time,'yyyyMMdd')  ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are records available", "warning");
                rd_view_history.DataSource = null;
                rd_view_history.DataBind();
            }
            else
            {
                rd_view_history.DataSource = dt;
                rd_view_history.DataBind();
            }
        }
    }
}