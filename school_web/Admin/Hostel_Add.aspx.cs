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
    public partial class Hostel_Add : System.Web.UI.Page
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
                 

                        string pagename_current = "Hostel_Master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        bind_grd_view();
                        txt_hostel_name.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_hostel_Master");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            print1.Visible = false;
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData("select * from Hostels_master order by Hostel_name asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
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



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_hostel_name.Text = "";
            txt_hostelmobile_no.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_hostel_name.Text == "")
            {
                Alertme("Please Enter Hostel Name.", "warning");
                txt_hostel_name.Focus();
                return;
            }


            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " create hostel master on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_update_details();
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update hostel master on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }


        private void update_update_details()
        {
            SqlCommand cmd;
            string query = "Update Hostels_master set Hostel_type=@Hostel_type,Hostel_name=@Hostel_name,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate,Mobile_no=@Mobile_no where Id = '" + hd_id.Value + "'";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Hostel_type", ddl_hostel_type.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Hostel_name", txt_hostel_name.Text);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Mobile_no", txt_hostelmobile_no.Text);

            if (My.InsertUpdateData(cmd))
            {
                Alertme("Hostel details has been updated successfully", "success");
            }
        }


        int hostel_id;
        private void submit_details()
        {
            create_sl_no();
            SqlCommand cmd;
            string query = "INSERT INTO Hostels_master (Hostel_id,Hostel_type,Hostel_name,Created_by,Created_date,Created_idate,Mobile_no) values (@Hostel_id,@Hostel_type,@Hostel_name,@Created_by,@Created_date,@Created_idate,@Mobile_no)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Hostel_id", hostel_id);
            cmd.Parameters.AddWithValue("@Hostel_type", ddl_hostel_type.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Hostel_name", txt_hostel_name.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Mobile_no", txt_hostelmobile_no.Text);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Hostel Master Created Successfully", "success");
            }
        }

        private void create_sl_no()
        {
            bool duplicate = true;
            hostel_id = My.toint(My.auto_serialS("hostel_id"));
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Hostel_id from dbo.[Hostels_master] where Hostel_id='" + hostel_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    hostel_id = My.toint(My.auto_serialS("hostel_id"));
                }
            }
        }

        private void empty_form()
        {
            txt_hostelmobile_no.Text = "";
            txt_hostel_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_hostel_type = (Label)row.FindControl("lbl_hostel_type");
                    Label lbl_hostel_Name = (Label)row.FindControl("lbl_hostel_Name");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_hostelmobile_no = (Label)row.FindControl("lbl_hostelmobile_no");
                    txt_hostelmobile_no.Text = lbl_hostelmobile_no.Text;
                    hd_id.Value = lbl_Id.Text;
                    txt_hostel_name.Text = lbl_hostel_Name.Text;
                    ddl_hostel_type.Text = lbl_hostel_type.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
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
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_hostel_id = (Label)row.FindControl("lbl_hostel_id");


                if (My.dataTable("select Hostel_id from Hostel_room_master where Hostel_id='" + lbl_hostel_id.Text + "'").Rows.Count == 0)
                {
                    My.exeSql("delete from Hostels_master where Id='" + lbl_Id.Text + "'");
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " delete hostel master on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                    Alertme("Hostel Master deleted Successfully", "success");
                }
                else
                {
                    Alertme("You can't delete this hostel. There is a data associated with room master.", "warning");
                }
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export_Hostel_Master.xls");
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