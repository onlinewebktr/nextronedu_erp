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
    public partial class Hostel_Damage_master : System.Web.UI.Page
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
                        txt_service_name.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hostel_Damage_master");
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
        private void bind_grd_view()
        {
            btn_excels.Visible = false;
            print1.Visible = false;
            DataTable dt = mycode.FillData("select * from Hostel_Damage_Item_master order by Item_name asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no damage Item added in the damaged item master", "warning");
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
            if (txt_service_name.Text == "")
            {
                Alertme("Please enter item name.", "warning");
                txt_service_name.Focus();
                return;
            }
            if (txt_amount.Text == "")
            {
                Alertme("Please enter item price.", "warning");
                txt_amount.Focus();
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
                    Alertme("Item name already added ", "warning");

                }
            }
            else
            {

                if (ViewState["Is_add"].ToString() == "1")
                {
                    update_details();
                }
                else
                {
                    Alertme("Item name already added ", "warning");

                }
            }

           
        }
        protected string Getamount_comma_seperated(string amount)
        {
            try
            {
                string amt = String.Format("{0:n}", Convert.ToDouble(amount));
                return amt;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        private void update_details()
        {
            double amt = My.toDouble(txt_amount.Text);
            if (mycode.IsUserExist("select Item_id from Hostel_Damage_Item_master where Item_name='" + txt_service_name.Text + "' and Id!= '" + hd_id.Value + "'"))
            {
                SqlCommand cmd;
                string query = "Update Hostel_Damage_Item_master set Item_name=@Item_name,Price=@Price,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id = '" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Item_name", txt_service_name.Text);
                cmd.Parameters.AddWithValue("@Price", amt.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Item name has been updated successfully", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Item name already added ", "warning");
            }
        }


        int Goods_id;
        private void submit_details()
        {
            double amt = My.toDouble(txt_amount.Text);
            if (mycode.IsUserExist("select Item_id from Hostel_Damage_Item_master where Item_name='" + txt_service_name.Text + "'"))
            {
                create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Hostel_Damage_Item_master (Item_id,Item_name,Price,Created_by,Created_date,Created_idate) values (@Item_id,@Item_name,@Price,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Item_id", Goods_id);
                cmd.Parameters.AddWithValue("@Item_name", txt_service_name.Text);
                cmd.Parameters.AddWithValue("@Price", amt.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Item name has been created successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Item name already added with this name.", "warning");
            }
        }

        private void create_sl_no()
        {
            bool duplicate = true;
            Goods_id = My.toint(My.auto_serialS("Damage_Item_id"));
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Item_id from Hostel_Damage_Item_master where Item_id='" + Goods_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Goods_id = My.toint(My.auto_serialS("Damage_Item_id"));
                }
            }
        }

        private void empty_form()
        {
            txt_service_name.Text = "";
            txt_amount.Text = "";
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
                    Label lbl_item_name = (Label)row.FindControl("lbl_item_name");
                    Label lbl_price = (Label)row.FindControl("lbl_price");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_service_name.Text = lbl_item_name.Text;
                    txt_amount.Text = lbl_price.Text;
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
                Label lbl_item_id = (Label)row.FindControl("lbl_item_id");
                if (My.dataTable("select Damage_goods_item_id from Hostel_student_damage_Item_maping where Damage_goods_item_id='" + lbl_item_id.Text + "'").Rows.Count == 0)
                {
                    My.exeSql("delete from Hostel_Damage_Item_master where Id='" + lbl_Id.Text + "'");
                    Alertme("Your selected item  has been deleted successfully.", "success");
                }
                else
                {
                    Alertme("You can't delete this item. There is   item name associated with student.", "warning");
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export_Damage_item.xls");
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