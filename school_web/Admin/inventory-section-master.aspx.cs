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
    public partial class inventory_section_master : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "inventory-section-master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        bind_grd_view();
                        txt_section_name.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_section_master");
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
            DataTable dt = mycode.FillData("select * from Inventory_section_master order by Section asc");
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
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_section_name.Text == "")
            {
                Alertme("Please enter section name.", "warning");
                txt_section_name.Focus();
                return;
            }


            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
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
                    update_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }


        private void update_details()
        {
            if (mycode.IsUserExist("select Section from Inventory_section_master where Section='" + txt_section_name.Text + "' and Id!= '" + hd_id.Value + "'"))
            {
                SqlCommand cmd;
                string query = "Update Inventory_section_master set Section=@Section where Id = '" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Section", txt_section_name.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Section details has been updated successfully", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Section already added with this name.", "warning");
            }
        }



        private void submit_details()
        {
            if (mycode.IsUserExist("select Section from Inventory_section_master where Section='" + txt_section_name.Text + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Inventory_section_master (Section) values (@Section)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Section", txt_section_name.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Section master has been created successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Section already added with this name.", "warning");
            }
        }



        private void empty_form()
        {
            txt_section_name.Text = "";
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
                    Label lbl_section_name = (Label)row.FindControl("lbl_section_name");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_section_name.Text = lbl_section_name.Text;

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
                My.exeSql("delete from Inventory_section_master where Id='" + lbl_Id.Text + "'");
                Alertme("Section has been deleted successfully.", "success");
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");

            }
        }
    }
}