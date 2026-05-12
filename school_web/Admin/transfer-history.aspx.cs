using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

namespace school_web.Admin
{
    public partial class transfer_history : System.Web.UI.Page
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
                        string pagename_current = "transfer-history.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        txt_s_dateS.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        //bind_grd_view(); 
                        Bind_date_wise();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_view_stock");
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
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData("select t1.Transfer_date,t1.Transfer_time,t1.Item_id,t4.Unit_name,t1.Unit_id,t2.Item_name,t2.Modal_no,t1.Serial_no,t1.Working_status,Floor,Section,(select top 1 (Room_name +', (No. : '+ Room_no+')') from Inventory_room_master where Rooom_id=t1.Room_id) as Room_name,t1.Transfer_quantity,t1.Transfer_by,Unique_key,Serial_no,Value,Is_warranty,Expire_date,(select top 1 Material_name from Inventory_Type_Master where Material_id=t2.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=t2.Brand_id) as Brand_name from Inventory_transfer_master t1 join Inventory_item_master t2 on t1.Item_id=t2.Item_id and t1.Unit_id=t2.Unit_id join Inventory_unit_master t4 on t1.Unit_id=t4.Unit_id order by t1.id desc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_excels.Visible = false;
            }
            else
            {

                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
                lbl_class22.Text = " All";
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_date_wise();
        }

        private void Bind_date_wise()
        {
            if (txt_s_dateS.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_dateS.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_dateS.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    string sdate = txt_s_dateS.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_e_date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate1 = Convert.ToInt32(syear + smonth + sday);
                    int idate21 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        final_find_report_by_date(idate1, idate21);
                    }
                }
            }
        }

        private void final_find_report_by_date(int idate1, int idate21)
        {
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData("select t1.Transfer_date,t1.Transfer_time,t1.Item_id,t4.Unit_name,t1.Unit_id,t2.Item_name,t2.Modal_no,t1.Serial_no,t1.Working_status,Floor,Section,(select top 1 (Room_name +', (No. : '+ Room_no+')') from Inventory_room_master where Rooom_id=t1.Room_id) as Room_name,t1.Transfer_quantity,t1.Transfer_by,Unique_key,Serial_no,Value,Is_warranty,Expire_date,(select top 1 Material_name from Inventory_Type_Master where Material_id=t2.Material_id) as Material_name,(select top 1 Brand_name from Inventory_brand_master where Brand_id=t2.Brand_id) as Brand_name from Inventory_transfer_master t1 join Inventory_item_master t2 on t1.Item_id=t2.Item_id and t1.Unit_id=t2.Unit_id join Inventory_unit_master t4 on t1.Unit_id=t4.Unit_id where t1.Transfer_idate>='" + idate1 + "' and t1.Transfer_idate<='" + idate21 + "' order by t1.id desc");
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

                lbl_class22.Text = "Date Period : Start Date " + txt_s_dateS.Text + " End Date :" + txt_e_date.Text;
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
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