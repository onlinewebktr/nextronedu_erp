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
    public partial class Transport_List : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        if (ViewState["Is_add"].ToString() == "1")
                        {
                            add_bus.Visible = true;
                        }
                        else
                        {
                            add_bus.Visible = false;
                        }

                        find_firm_details();

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Bus_List_aspx");
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
        private void bind_grd_view()
        {
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData("select * from dbo.[Transport_Master] order by transport_name");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no vehicles list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();

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



        #region grid view edit and delete
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_bus_no = (Label)row.FindControl("lbl_bus_no");

                mycode.executequery("delete from Transport_Master where Id=" + lbl_Id.Text + "");

                string msg = ViewState["Userid"].ToString() + " Delete Transportation , Bus No=" + lbl_bus_no.Text + " Name=" + mycode.get_user(ViewState["Userid"].ToString()) + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                bind_grd_view();
                Alertme("Bus details has been Successfully deleted", "success");
            }
            catch
            {
            }
        }
        #endregion

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_data();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "close();", true);
        }

        private void empty_data()
        {
            btn_Submit.Text = "Add";
            txt_busname.Text = "";
            txt_bus_no.Text = "";
            txt_bus_owner_name.Text = "";
            txt_bus_owner_mobileno.Text = "";
            txt_busdrivername.Text = "";
            txt_busdriver_mobile_no.Text = "";
            ddl_bustype.Text = "Select";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_transport_id = (Label)row.FindControl("lbl_transport_id");
                    Label lbl_transport_name = (Label)row.FindControl("lbl_transport_name");
                    Label lbl_bus_no = (Label)row.FindControl("lbl_bus_no");
                    Label lbl_Bus_owner_name = (Label)row.FindControl("lbl_Bus_owner_name");
                    Label lbl_Bus_owner_mobile_no = (Label)row.FindControl("lbl_Bus_owner_mobile_no");
                    Label lbl_Bus_driver_name = (Label)row.FindControl("lbl_Bus_driver_name");
                    Label lbl_Bus_driver_mobileno = (Label)row.FindControl("lbl_Bus_driver_mobileno");
                    Label lbl_noof_sheet = (Label)row.FindControl("lbl_noof_sheet");
                    Label lbl_Bus_type = (Label)row.FindControl("lbl_Bus_type");

                    Response.Redirect("Add_Vehicle.aspx?transportid=" + lbl_transport_id.Text, false);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
                //btn_Submit.Text = "Update";
                //hd_id.Value = lbl_Id.Text;
                //txt_busname.Text = lbl_transport_name.Text;
                //txt_bus_no.Text = lbl_bus_no.Text;
                //txt_bus_owner_name.Text = lbl_Bus_owner_name.Text;
                //txt_bus_owner_mobileno.Text = lbl_Bus_owner_mobile_no.Text;
                //txt_busdrivername.Text = lbl_Bus_driver_name.Text;
                //txt_busdriver_mobile_no.Text = lbl_Bus_driver_mobileno.Text;
                //ddl_bustype.Text = lbl_Bus_type.Text;

                //txt_no_of_bus_sheet.Text = lbl_noof_sheet.Text;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {
            }
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            if (ddl_bustype.Text == "Select")
            {
                Alertme("Please select bus", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                if (btn_Submit.Text == "Add")
                {
                    DataTable dt1 = My.dataTable("select * from Transport_Master where transport_name='" + txt_busname.Text + "'  ");
                    if (dt1.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Transport_Master (transport_name,transport_id,Bus_no,Bus_owner_name,Bus_owner_mobile_no,Bus_driver_name,Bus_driver_mobileno,Bus_no_sheet,Bus_type,Created_by,Created_date) values (@transport_name,@transport_id,@Bus_no,@Bus_owner_name,@Bus_owner_mobile_no,@Bus_driver_name,@Bus_driver_mobileno,@Bus_no_sheet,@Bus_type,@Created_by,@Created_date)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@transport_name", txt_busname.Text);
                        cmd.Parameters.AddWithValue("@transport_id", My.auto_serialS("group_id"));
                        cmd.Parameters.AddWithValue("@Bus_no", txt_bus_no.Text);
                        cmd.Parameters.AddWithValue("@Bus_owner_name", txt_bus_owner_name.Text);
                        cmd.Parameters.AddWithValue("@Bus_owner_mobile_no", txt_bus_owner_mobileno.Text);
                        cmd.Parameters.AddWithValue("@Bus_driver_name", txt_busdrivername.Text);
                        cmd.Parameters.AddWithValue("@Bus_driver_mobileno", txt_busdriver_mobile_no.Text);
                        cmd.Parameters.AddWithValue("@Bus_no_sheet", txt_no_of_bus_sheet.Text);
                        cmd.Parameters.AddWithValue("@Bus_type", ddl_bustype.Text);
                        cmd.Parameters.AddWithValue("@Created_by", ddl_bustype.Text);
                        cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Bus details has been sucessfully added", "success");
                            bind_grd_view();
                            empty_data();
                        }


                    }
                    else
                    {

                        Alertme("Your Entered bus name is already added ", "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Transport_Master set transport_name=@transport_name,Bus_no=@Bus_no,Bus_owner_name=@Bus_owner_name,Bus_owner_mobile_no=@Bus_owner_mobile_no,Bus_driver_name=@Bus_driver_name,Bus_driver_mobileno=@Bus_driver_mobileno,Bus_no_sheet=@Bus_no_sheet,Bus_type=@Bus_type,Updated_by=@Updated_by,Updated_time=@Updated_time where Id = @Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@transport_name", txt_busname.Text);
                    cmd.Parameters.AddWithValue("@Bus_no", txt_bus_no.Text);
                    cmd.Parameters.AddWithValue("@Bus_owner_name", txt_bus_owner_name.Text);
                    cmd.Parameters.AddWithValue("@Bus_owner_mobile_no", txt_bus_owner_mobileno.Text);
                    cmd.Parameters.AddWithValue("@Bus_driver_name", txt_busdrivername.Text);
                    cmd.Parameters.AddWithValue("@Bus_driver_mobileno", txt_busdriver_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Bus_no_sheet", txt_no_of_bus_sheet.Text);
                    cmd.Parameters.AddWithValue("@Bus_type", ddl_bustype.Text);
                    cmd.Parameters.AddWithValue("@Updated_by", ddl_bustype.Text);
                    cmd.Parameters.AddWithValue("@Updated_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Bus details has been sucessfully updated", "success");
                        bind_grd_view();
                        empty_data();
                    }
                }

            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export_Transport_List.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
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