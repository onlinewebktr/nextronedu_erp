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
    public partial class room_master : System.Web.UI.Page
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
                        string pagename_current = "room-master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        bind_grd_view();
                        txt_room_name.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_room_master");
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
            DataTable dt = mycode.FillData("select * from Inventory_room_master order by Id desc");
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
            if (txt_room_name.Text == "")
            {
                Alertme("Please enter room name.", "warning");
                txt_room_name.Focus();
                return;
            }
            if (txt_room_no.Text == "")
            {
                Alertme("Please enter room no.", "warning");
                txt_room_no.Focus();
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
            if (mycode.IsUserExist("select Rooom_id from Inventory_room_master where Room_name='" + txt_room_name.Text + "' and Room_no='" + txt_room_no.Text + "'  and Id!= '" + hd_id.Value + "'"))
            {
                SqlCommand cmd;
                string query = "Update Inventory_room_master set Room_name=@Room_name,Room_no=@Room_no,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id = '" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Room_name", txt_room_name.Text);
                cmd.Parameters.AddWithValue("@Room_no", txt_room_no.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Room details has been updated successfully", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Room already added with this name & no.", "warning");
            }
        }


        int Group_id;
        private void submit_details()
        {
            if (mycode.IsUserExist("select Rooom_id from Inventory_room_master where Room_name='" + txt_room_name.Text + "' and Room_no='" + txt_room_no.Text + "'"))
            {
                create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Inventory_room_master (Rooom_id,Room_name,Room_no,Created_by,Created_date,Created_idate) values (@Rooom_id,@Room_name,@Room_no,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Rooom_id", Group_id);
                cmd.Parameters.AddWithValue("@Room_name", txt_room_name.Text);
                cmd.Parameters.AddWithValue("@Room_no", txt_room_no.Text);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Room master has been created successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Room already added with this name & no.", "warning");
            }
        }

        private void create_sl_no()
        {
            bool duplicate = true;
            Group_id = My.toint(My.auto_serialS("Global_sl_id"));
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Rooom_id from Inventory_room_master where Rooom_id='" + Group_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Group_id = My.toint(My.auto_serialS("Global_sl_id"));
                }
            }
        }

        private void empty_form()
        {
            txt_room_name.Text = "";
            txt_room_no.Text = "";
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
                    Label lbl_room_name = (Label)row.FindControl("lbl_room_name");
                    Label lbl_room_no = (Label)row.FindControl("lbl_room_no");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_room_name.Text = lbl_room_name.Text;
                    txt_room_no.Text = lbl_room_no.Text;
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
                Label lbl_rooom_id = (Label)row.FindControl("lbl_rooom_id");
                if (My.dataTable("select Room_id from Inventory_transfer_master where Room_id='" + lbl_rooom_id.Text + "'").Rows.Count == 0)
                {
                    My.exeSql("delete from Inventory_room_master where Id='" + lbl_Id.Text + "'");
                    Alertme("Room has been deleted successfully.", "success");
                }
                else
                {
                    Alertme("You can't delete this Brand. There is a data associated with Item.", "warning");
                }
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }
    }
}