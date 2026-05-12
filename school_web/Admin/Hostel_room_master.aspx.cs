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
    public partial class Hostel_room_master : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        mycode.bind_all_ddl_with_id(ddl_category, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                        bind_grd_view();
                        ddl_hostel.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_room_Master");
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
            btn_excels.Visible = false;
            print1.Visible = false;
            DataTable dt = mycode.FillData("select *,(select top 1 Hostel_name from Hostels_master where Hostel_id=Hostel_room_master.Hostel_id) as Hostel_name,(select top 1 Category_name from Hostel_room_category_master where Category_id=Hostel_room_master.Category_id) as Category_name from Hostel_room_master order by Id desc");
            if (dt.Rows.Count == 0)
            {
                Alertme(" Sorry, there are no room number added", "warning");
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
            empty_form();
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (ddl_hostel.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel.", "warning");
                ddl_hostel.Focus();
                return;
            }
            if (ddl_category.SelectedItem.Text == "Select")
            {
                Alertme("Please select room category.", "warning");
                ddl_category.Focus();
                return;
            }
            if (txt_room_name.Text == "")
            {
                Alertme("Please enter room name.", "warning");
                txt_room_name.Focus();
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
            if (mycode.IsUserExist("select Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Room_name='" + txt_room_name.Text + "' and Id!= '" + hd_id.Value + "'"))
            {
                SqlCommand cmd;
                string query = "Update Hostel_room_master set Hostel_id=@Hostel_id,Category_id=@Category_id,Room_name=@Room_name,Description=@Description,Total_no_of_bed=@Total_no_of_bed,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id = '" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
                cmd.Parameters.AddWithValue("@Category_id", ddl_category.SelectedValue);

                cmd.Parameters.AddWithValue("@Room_name", txt_room_name.Text);
                cmd.Parameters.AddWithValue("@Description", txt_room_desc.Text);
                cmd.Parameters.AddWithValue("@Total_no_of_bed", "0");
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("The room number has been updated successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("This room number is already added with this hostel", "warning");
            }
        }


        int room_id;
        private void submit_details()
        {
            if (mycode.IsUserExist("select Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Room_name='" + txt_room_name.Text + "'"))
            {
                create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Hostel_room_master (Hostel_id,Category_id,Room_id,Room_name,Description,Total_no_of_bed,Created_by,Created_date,Created_idate) values (@Hostel_id,@Category_id,@Room_id,@Room_name,@Description,@Total_no_of_bed,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
                cmd.Parameters.AddWithValue("@Category_id", ddl_category.SelectedValue);
                cmd.Parameters.AddWithValue("@Room_id", room_id);
                cmd.Parameters.AddWithValue("@Room_name", txt_room_name.Text);
                cmd.Parameters.AddWithValue("@Description", txt_room_desc.Text);
                cmd.Parameters.AddWithValue("@Total_no_of_bed", "0");
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("The room number has been added successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("This room number is already added with this hostel", "warning");
            }
        }
        private void create_sl_no()
        {
            bool duplicate = true;
            room_id = My.toint(My.auto_serialS("Room_id"));
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Room_id from Hostel_room_master where Room_id='" + room_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    room_id = My.toint(My.auto_serialS("Room_id"));
                }
            }
        }

        private void empty_form()
        {
            txt_room_name.Text = "";
            txt_room_desc.Text = "";
             
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
                    Label lbl_hostel_id = (Label)row.FindControl("lbl_hostel_id");
                    Label lbl_category_id = (Label)row.FindControl("lbl_category_id");
                    Label lbl_room_name = (Label)row.FindControl("lbl_room_name");
                    Label lbl_description = (Label)row.FindControl("lbl_description");
                    Label lbl_total_no_of_bed = (Label)row.FindControl("lbl_total_no_of_bed");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    ddl_hostel.SelectedValue = lbl_hostel_id.Text;
                    ddl_category.SelectedValue = lbl_category_id.Text;
                    txt_room_name.Text = lbl_room_name.Text;
                    txt_room_desc.Text = lbl_description.Text;

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
                Label lbl_room_id = (Label)row.FindControl("lbl_room_id");


                if (My.dataTable("select Room_id from Hostel_room_bed_master where Room_id='" + lbl_room_id.Text + "'").Rows.Count == 0)
                {
                    My.exeSql("delete from Hostel_room_master where Id='" + lbl_Id.Text + "'");
                    Alertme("The room number has been deleted successfully", "success");
                }
                else
                {
                    Alertme("You can't delete this room because there is data associated with the bed master.", "warning");
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
                    Response.AddHeader("content-disposition", "attachment;filename=ExportHostel_room_master.xls");
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