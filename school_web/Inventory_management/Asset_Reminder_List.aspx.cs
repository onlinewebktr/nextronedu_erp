using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Inventory_management
{
    public partial class Reminder_Asset_List : System.Web.UI.Page
    {
        My mycode = new My();
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
            if (panel == "Warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
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


                if (!IsPostBack)
                {
                    try
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        Session["list"] = "4";

                        bind_Asset_list();


                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }

        }

        private void bind_Asset_list()
        {
            lnk_excel_download.Visible = false;
            print1.Visible = false;
            SqlCommand cmd = new SqlCommand("sp_asset_list");
            cmd.Parameters.AddWithValue("@sp_status ", "fetch_asset_reminder");
            cmd.Parameters.AddWithValue("@idate1 ", mycode.idate());
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                GrdViewlist.DataSource = null;
                GrdViewlist.DataBind();
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                Alertme("Sorry! There are no any asset reminder list here", "Warning");
            }
            else
            {
                lnk_excel_download.Visible = true;
                print1.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                GrdViewlist.DataSource = dt;
                GrdViewlist.DataBind();

            }
        }

        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=AssetReminderList_" + mycode.date() + "_" + mycode.time() + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GrdViewlist.RenderControl(hw);
                    //string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    //Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DataRowView rowView = (DataRowView)e.Item.DataItem;
                DataList DataList1 = ((DataList)e.Item.FindControl("DataList1"))
                    as DataList;
                Label lbl_Attribute_entry_id = ((Label)e.Item.FindControl("lbl_Attribute_entry_id")) as Label;

                Label lbl_Attribute_valid_to_idate = ((Label)e.Item.FindControl("lbl_Attribute_valid_to_idate")) as Label;

                Label lbl_status = ((Label)e.Item.FindControl("lbl_status")) as Label;

                if (My.toint(lbl_Attribute_valid_to_idate.Text) >= My.toint(mycode.idate()))
                {
                    lbl_status.Text = "Active";
                }
                else
                {
                    lbl_status.Text = "Expired";
                }
                fetch_document(lbl_Attribute_entry_id.Text, DataList1);

            }
        }

        private void fetch_document(string Attribute_entry_id, DataList dataList1)
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Asset_Attribute_attachment]  where Attribute_entry_id='" + Attribute_entry_id + "'");
            if (cdt.Rows.Count == 0)
            {
                dataList1.DataSource = null;
                dataList1.DataBind();
            }
            else
            {
                dataList1.DataSource = cdt;
                dataList1.DataBind();

            }
        }

        protected void GrdViewlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Attribute_valid_to_idate = ((Label)e.Row.FindControl("lbl_Attribute_valid_to_idate")) as Label;

                Label lbl_status = ((Label)e.Row.FindControl("lbl_status")) as Label;

                if (My.toint(lbl_Attribute_valid_to_idate.Text) >= My.toint(mycode.idate()))
                {
                    lbl_status.Text = "Active";
                }
                else
                {
                    lbl_status.Text = "Inactive";
                }
            }
        }
    }
}