using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;

namespace school_web.Inventory_management
{
    public partial class Item_Mater_List : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    //string pagename_current = Path.GetFileName(Request.Path);
                    //Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    //ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    //ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    //ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    //ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    //ViewState["Is_add"] = (String)dc1["Is_add"];

                    ViewState["Is_Edit"] = "1"; //(String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = "1";//(String)dc1["Is_delete"];
                    ViewState["Is_Download"] = "1"; //(String)dc1["Is_Download"];
                    ViewState["Is_Print"] = "1"; //(String)dc1["Is_Print"];
                    ViewState["Is_add"] = "1"; //(String)dc1["Is_add"];

                    try
                    {
                        if (Session["msg"] == null)
                        {

                        }
                        else
                        {
                            Alertme(Session["msg"].ToString(), "success");
                            Session["msg"] = null;
                        }
                    }
                    catch
                    {

                    }


                    bind_grd_view();

                }
            }
        }

        private void bind_grd_view()
        {
            btn_excels.Visible = false;
            print1.Visible = false;
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_item_Master");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch");
            cmd.Parameters.AddWithValue("@firm ", My.firm_id());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                print1.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Item_id = (Label)row.FindControl("lbl_Item_id");
                    Label lbl_Item_name = (Label)row.FindControl("lbl_Item_Name");
                    Label lbl_Brand_Id = (Label)row.FindControl("lbl_Brand_Id");
                    Label lbl_Unit_Id = (Label)row.FindControl("lbl_Unit_Id");
                    Label lbl_HSN = (Label)row.FindControl("lbl_HSN");
                    Label lbl_GST = (Label)row.FindControl("lbl_GST");
                    Label lbl_Item_type = (Label)row.FindControl("lbl_Item_type");
                    HdID.Value = lbl_Item_id.Text;
                    Response.Redirect("Create_Item.aspx?itemid=" + lbl_Item_id.Text, false);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }






            }
            catch (Exception ex)
            {

            }

        }
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Item_id = (Label)row.FindControl("lbl_Item_id");
                    Label lbl_Item_name = (Label)row.FindControl("lbl_Item_Name");
                    Label lbl_Brand_name = (Label)row.FindControl("lbl_Brand_Name");
                    Label lbl_Unit_name = (Label)row.FindControl("lbl_Unit_Name");
                    Label lbl_HSN = (Label)row.FindControl("lbl_HSN");
                    Label lbl_GST = (Label)row.FindControl("lbl_GST");



                    if (mycode.FillData("select * from HMS_INVENTORY_PURCHASE_ENTRY_ITEMWISE where Item_Code ='" + lbl_Item_id.Text + "'").Rows.Count == 0)
                    {
                        delete_data(lbl_Item_id.Text);
                        ViewState["Description"] = " Delete Inventory Item Name =" + lbl_Item_name.Text + "</br> Item Id:-" + lbl_Item_id.Text;
                        My.send_data_to_user_log_history(Session["name"].ToString() + ViewState["Description"].ToString() + " on Dated-" + My.datetime_new(), Session["Admin"].ToString(), "", "INVENTORY");

                        return;
                    }
                    else
                    {
                        Alertme("Sorry you can't delete this item because this item already associated with the purchase item", "warning");
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void delete_data(string Item_id)
        {
            SqlCommand cmd;
            string query = "delete from  HMS_Invetory_item_Master where Item_id = @Item_id ;";
            query = query + "delete from  Inventory_Item_Unit_Maping where Item_Code = @Item_id ;";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Item_id", Item_id);

            if (My.InsertUpdateData(cmd))
            {
                Alertme("Item Name has been delete Successfully.", "success");
                bind_grd_view();
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export"+mycode.date()+mycode.idate()+".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_grid.RenderControl(hw);
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