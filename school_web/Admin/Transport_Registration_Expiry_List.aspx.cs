using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace school_web.Admin
{
    public partial class Transport_Registration_Expiry_List : System.Web.UI.Page
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

                        //if (ViewState["Is_add"].ToString() == "1")
                        //{
                        //    add_bus.Visible = true;
                        //}
                        //else
                        //{
                        //    add_bus.Visible = false;
                        //}

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
                Alertme("Sorry there are no data list exist", "warning");
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

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export_vehicle_Expiry_List.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_view.RenderControl(hw);
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

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string Registration_Expiry_date = ((Label)e.Item.FindControl("lbl_Registration_Expiry_date")).Text;
                string Insurance_Expiry_Date = ((Label)e.Item.FindControl("lbl_Vehicle_Insurance_Expiry_Date")).Text;
                string Pollution_Expiry_Date = ((Label)e.Item.FindControl("lbl_Pollution_Expiry_Date")).Text;
                string Body_Fitness_Expiry_Date = ((Label)e.Item.FindControl("lbl_Body_Fitness_Expiry_Date")).Text;

                try
                {
                    DateTime Registration_Expiry = DateTime.ParseExact(Registration_Expiry_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int Registration_Expiry_idate = Convert.ToInt32(Registration_Expiry.ToString("yyyyMMdd"));
                    if (My.toint(mycode.idate()) > Registration_Expiry_idate)
                    {
                        ((Label)e.Item.FindControl("lbl_Registration_Expiry_message")).Text = "Expired";
                    }
                }
                catch
                {

                }

                try
                {
                    DateTime Insurance_Expiry = DateTime.ParseExact(Insurance_Expiry_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int Insurance_Expiry_idate = Convert.ToInt32(Insurance_Expiry.ToString("yyyyMMdd"));
                    if (My.toint(mycode.idate()) > Insurance_Expiry_idate)
                    {
                        ((Label)e.Item.FindControl("lbl_Vehicle_Insurance_Expiry")).Text = "Expired";
                    }
                }
                catch
                {

                }

                try
                {
                    DateTime Pollution_Expiry = DateTime.ParseExact(Pollution_Expiry_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int Pollution_Expiry_idate = Convert.ToInt32(Pollution_Expiry.ToString("yyyyMMdd"));
                    if (My.toint(mycode.idate()) > Pollution_Expiry_idate)
                    {
                        ((Label)e.Item.FindControl("lbl_Pollution_Expiry_messge")).Text = "Expired";
                    }

                }
                catch
                {

                }

                try
                {
                    DateTime Body_Fitness_Expiry = DateTime.ParseExact(Body_Fitness_Expiry_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    int Body_Fitness_Expiry_idate = Convert.ToInt32(Body_Fitness_Expiry.ToString("yyyyMMdd"));
                    if (My.toint(mycode.idate()) > Body_Fitness_Expiry_idate)
                    {
                        ((Label)e.Item.FindControl("lbl_Body_Fitness_Expiry_messge")).Text = "Expired";
                    }

                }
                catch
                {

                }









            }
        }
    }
}