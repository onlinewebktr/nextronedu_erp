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
using System.Transactions;

namespace school_web.Inventory_management
{
    public partial class Update_Sales_Item_Wise_Rate : System.Web.UI.Page
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

                ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                if (!IsPostBack)
                {
                    Fetch_all_stock_sale_price();

                }
            }
        }

        private void Fetch_all_stock_sale_price()
        {
            string query = "SELECT  t2.Item_Name,t4.Brand_name,t3.unit,t2.Item_type,t1.Item_Code,t1.Brand_Id,t1.Unit_id,(select top 1 Sale_rate from HMS_Inventory_stock_details where Item_Code=t1.Item_Code and Brand_Id=t1.Brand_Id and Unit_id=t1.Unit_id )  as Sale_ratenew FROM HMS_Inventory_stock_details  t1 join HMS_Invetory_item_Master t2 on t1.Item_Code=t2.Item_id join unit_master t3 on  t1.unit_id=t3.unit_id left join  HMS_Invetory_Brand_Master t4 on t1.Brand_Id=t4.Brand_id   group by t2.Item_Name,t4.Brand_name,t3.unit,t2.Item_type,t1.Item_Code,t1.Brand_Id,t1.Unit_id";
            bind_grid_data(query);

        }
        private void bind_grid_data(string query)
        {
            btn_submit.Visible = false;
            ViewState["query"] = query;
            pnl_grid.Visible = false;
            DataTable dt1 = mycode.FillData(query);
            ViewState["item_dt"] = dt1;
            if (dt1.Rows.Count == 0)
            {
                Alertme("Sorry, there are no records found.", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {
                btn_submit.Visible = true;
                pnl_grid.Visible = true;
                RPDetails.DataSource = dt1;
                RPDetails.DataBind();

            }
        }
        #region download_in_excel
        protected void btn_excels_Click(object sender, EventArgs e)
        {


            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Update_SaleRate" + mycode.date() + "_" + mycode.time() + ".xls");

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
        #endregion
        protected void btn_submit_Click(object sender, EventArgs e)
        {

            string confirmValue = string.Empty;
            confirmValue = Request.Form["confirm_value"];
            bool submit = false;
            if (confirmValue == "Yes")
            {
                int i = 0;
                int j = 0;
                foreach (RepeaterItem row in RPDetails.Items)
                {
                    CheckBox chk = RPDetails.Items[i].FindControl("rowChkBox") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        Label lbl_Item_Code = RPDetails.Items[i].FindControl("lbl_Item_Code") as Label;
                        Label lbl_Brand_Id = RPDetails.Items[i].FindControl("lbl_Brand_Id") as Label;
                        Label lbl_Unit_id = RPDetails.Items[i].FindControl("lbl_Unit_id") as Label;
                        Label lbl_Sale_rate = RPDetails.Items[i].FindControl("lbl_Sale_rate") as Label;
                        TextBox txt_Sale_rate = RPDetails.Items[i].FindControl("txt_Sale_rate") as TextBox;
                        submit = true;
                        j++;


                    }
                    i++;
                }
                if (j == RPDetails.Items.Count)
                {
                    Alertme("Please select at least one item from item list", "warning");


                }
                if (submit == true)
                {
                    Update_data_sale_rate();
                }
                    
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }

        }

        private void Update_data_sale_rate()
        {
            bool flag = false;
            DateTime date;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    date = My.toDateTime(mycode.date() + " " + mycode.time());
                    int i = 0;
                    foreach (RepeaterItem row in RPDetails.Items)
                    {
                        CheckBox chk = RPDetails.Items[i].FindControl("rowChkBox") as CheckBox;
                        if (chk != null && chk.Checked)
                        {
                            Label lbl_Item_Code = RPDetails.Items[i].FindControl("lbl_Item_Code") as Label;
                            Label lbl_Brand_Id = RPDetails.Items[i].FindControl("lbl_Brand_Id") as Label;
                            Label lbl_Unit_id = RPDetails.Items[i].FindControl("lbl_Unit_id") as Label;
                            Label lbl_Sale_rate = RPDetails.Items[i].FindControl("lbl_Sale_rate") as Label;
                            TextBox txt_Sale_rate = RPDetails.Items[i].FindControl("txt_Sale_rate") as TextBox;

                            update_stock_sale_rate(lbl_Item_Code.Text, lbl_Brand_Id.Text, lbl_Unit_id.Text, lbl_Sale_rate.Text, txt_Sale_rate.Text, date, con);
                            flag = true;

                        }
                        i++;
                    }

                    con.Close();
                    scope.Complete();
                    flag = true;
                }
                if (flag == true)
                {
                    Alertme("Sale Rate has been has updated", "success");
                    // sucess
                    bind_grid_data(ViewState["query"].ToString());
                }
            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }
        }

        private void update_stock_sale_rate(string item_Code, string brand_Id, string unit_id, string sale_rate, string update_Sale_rate, DateTime date, SqlConnection con)
        {
            double updaterate = My.toDouble(update_Sale_rate);
            SqlCommand cmd1;
            string query = "update HMS_Inventory_stock_details set Sale_rate=@Sale_rate  where Item_Code=@Item_Code and Brand_Id=@Brand_Id and Unit_id=@Unit_id ";
            cmd1 = new SqlCommand(query);
            cmd1.Parameters.AddWithValue("@Sale_rate", updaterate.ToString("0.00"));
            cmd1.Parameters.AddWithValue("@Item_Code", item_Code);
            cmd1.Parameters.AddWithValue("@Brand_Id", brand_Id);
            cmd1.Parameters.AddWithValue("@Unit_id", unit_id);
            if (payments.InsertUpdateData(cmd1, con))
            {
                SqlCommand cmd2;
                string query2 = "INSERT INTO HMS_Inventory_Sale_Rate_update_history (Item_Code,Brand_Id,Unit_id,Sale_rate_old,Sale_rate_new,updatedate,Userid) values (@Item_Code,@Brand_Id,@Unit_id,@Sale_rate_old,@Sale_rate_new,@updatedate,@Userid)  ";
                cmd2 = new SqlCommand(query2);

                cmd2.Parameters.AddWithValue("@Item_Code", item_Code);
                cmd2.Parameters.AddWithValue("@Brand_Id", brand_Id);
                cmd2.Parameters.AddWithValue("@Unit_id", unit_id);
                cmd2.Parameters.AddWithValue("@Sale_rate_old", sale_rate);
                cmd2.Parameters.AddWithValue("@Sale_rate_new", updaterate.ToString("0.00"));
                cmd2.Parameters.AddWithValue("@updatedate", date.ToString("yyyy/MM/dd hh:mm:ss tt"));
                cmd2.Parameters.AddWithValue("@Userid", ViewState["Userid"].ToString());
                if (payments.InsertUpdateData(cmd2, con))
                {

                }



            }
        }


    }
}