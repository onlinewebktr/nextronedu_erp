using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class package_list : System.Web.UI.Page
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
            if (panel == "warning")
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
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                        ViewState["session_id"] = My.get_session_id();
                        bind_all_data();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }

                    try
                    {
                        if (Session["msg1"] == null)
                        {

                        }
                        else
                        {
                            Alertme(Session["msg1"].ToString(), "success");
                            Session["msg1"] = null;
                        }
                    }
                    catch
                    {

                    }
                  
                }
            }
        }

        private void bind_all_data()
        {
            string query = " select  ps.*,ct.Course_Name,(Select top 1 Session from session_details where session_id = ps.Session_id) as Session from dbo.[HMS_Package_Summary] ps join Add_course_table ct on ct.course_id=ps.Class_id where ps.Session_id='" + ViewState["session_id"].ToString() + "' order by ct.Position asc";
            Bind_grid_view_list(query);
        }

        private void Bind_grid_view_list(string query)
        {
            ViewState["query"] = query;
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                Response.Redirect("add-package.aspx?pck=" + lbl_unique_entry_id.Text, false);
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                mycode.executequery("delete from HMS_Package_Summary where unique_entry_id='" + lbl_unique_entry_id.Text + "'");
                mycode.executequery("delete from HMS_Package_Item_Wise where unique_entry_id='" + lbl_unique_entry_id.Text + "'");
                Alertme("Selected record has been deleted successfully ", "success");
                Bind_grid_view_list(ViewState["query"].ToString());

            }
            catch
            {

            }



        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export_Package_list"+mycode.date()+"_"+mycode.time()+".xls");
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
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void lnk_excel_download1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportPackage_List" + mycode.date() + mycode.time() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                pnl_grid.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        #endregion download_in_excel
        double total_amount = 0;
        protected void grid_feedetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Net_total = (Label)e.Row.FindControl("lbl_Net_total");
                total_amount = total_amount + My.toDouble(lbl_Net_total.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_amount = (Label)e.Row.FindControl("lbl_total_amount");
                lbl_total_amount.Text = total_amount.ToString("0.00");
            }
        }
        protected void lnk_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
            Label lbl_Package_Name = (Label)row.FindControl("lbl_Package_Name");
            hd_class_name.Value = lbl_Package_Name.Text + "_" + mycode.date() + "_" + mycode.time();
            Bind_headingdetails(lbl_unique_entry_id.Text);
            Bind_itemname(lbl_unique_entry_id.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        private void Bind_itemname(string uniqueid)
        {
            DataTable dt1 = mycode.FillData("Select *,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=HMS_Package_Item_Wise.Item_Code and Unit_id=HMS_Package_Item_Wise.Unit_id) as itemname,(select top 1 Unit from unit_master where  Unit_id=HMS_Package_Item_Wise.Unit_id) as Unitname from HMS_Package_Item_Wise where  unique_entry_id='" + uniqueid + "'     ");
            if (dt1.Rows.Count == 0)
            {
                grid_feedetails.DataSource = null;
                grid_feedetails.DataBind();
               
            }
            else
            {
                grid_feedetails.DataSource = dt1;
                grid_feedetails.DataBind();
              

               
            }
        }

        private void Bind_headingdetails(string uniqueid)
        {
            string query = " select  ps.*,ct.Course_Name,(Select top 1 Session from session_details where session_id = ps.Session_id) as Session from dbo.[HMS_Package_Summary] ps join Add_course_table ct on ct.course_id=ps.Class_id where ps.unique_entry_id='" + uniqueid + "' order by ct.Position asc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
            else
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }
    }
}