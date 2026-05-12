using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class month_setting_for_monthly_fee : System.Web.UI.Page
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
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
            DataTable dt = mycode.FillData("select * from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_month_position = (Label)e.Item.FindControl("lbl_month_position");
                DropDownList ddl_no_of_month = (DropDownList)e.Item.FindControl("ddl_no_of_month");

                double positions = 13 - My.toDouble(lbl_month_position.Text);

                ArrayList ar = new ArrayList();
                ar.Add("Select");
                for (int i = 1; i <= My.toint(positions); i++)
                {
                    ar.Add(i);
                }
                ddl_no_of_month.DataSource = ar;
                ddl_no_of_month.DataBind();
            }
        }

        protected void ddl_no_of_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int no_of_row = 0;
                int i;
                int gridview_rowcount = rd_view.Items.Count;
                for (i = 0; i < gridview_rowcount; i++)
                {
                    Label lbl_month_position = (Label)rd_view.Items[i].FindControl("lbl_month_position");
                    DropDownList ddl_no_of_month = (DropDownList)rd_view.Items[i].FindControl("ddl_no_of_month");

                    if (ddl_no_of_month.Text != "Select")
                    {
                        no_of_row = i + My.toint(ddl_no_of_month.Text);
                        ddl_no_of_month.Enabled = true;
                    }
                    else
                    {
                        if (no_of_row > i)
                        {
                            ddl_no_of_month.Enabled = false;
                            ddl_no_of_month.CssClass = "form-select";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("month-setting-for-monthly-fee.aspx", false);
            }
            catch (Exception ex)
            {
            }
        }





        protected void btn_save_Click(object sender, EventArgs e)
        {

            try
            {
                ViewState["updateS"] = "0";
                int i; int j;
                int gridview_rowcount = rd_view.Items.Count;
                for (j = 0; j < gridview_rowcount; j++)
                {
                    DropDownList ddl_no_of_month = (DropDownList)rd_view.Items[j].FindControl("ddl_no_of_month");
                    if (ddl_no_of_month.Text != "Select")
                    {
                        mycode.executequery("Delete from Custome_month_selection_setting");
                    }
                }

                int no_of_pair_month = 0;
                for (i = 0; i < gridview_rowcount; i++)
                {
                    Label lbl_month = (Label)rd_view.Items[i].FindControl("lbl_month");
                    Label lbl_month_position = (Label)rd_view.Items[i].FindControl("lbl_month_position");
                    Label lbl_month_id = (Label)rd_view.Items[i].FindControl("lbl_month_id");
                    DropDownList ddl_no_of_month = (DropDownList)rd_view.Items[i].FindControl("ddl_no_of_month");
                    if (ddl_no_of_month.Text != "Select")
                    {
                        no_of_pair_month++;
                        save_custome_month(lbl_month.Text, lbl_month_position.Text, lbl_month_id.Text, ddl_no_of_month.Text, no_of_pair_month);
                        ViewState["updateS"] = "1";
                    }
                    else
                    {
                        if (no_of_pair_month != 0)
                        {
                            save_custome_month(lbl_month.Text, lbl_month_position.Text, lbl_month_id.Text, ddl_no_of_month.Text, no_of_pair_month);
                            ViewState["updateS"] = "1";
                        }
                    }
                }

                if (ViewState["updateS"].ToString() == "1")
                {
                    Alertme("Record has been updated successfullt.", "success");
                }
                else
                {
                    Alertme("Please select months.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_custome_month(string month_name, string position, string month_id, string no_of_month, int no_of_pair_month)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Custome_month_selection_setting (Month_name,Month_id,Month_position,No_of_month_selection,Updated_by,Updated_date,Updated_idate,Pair_group_id) values (@Month_name,@Month_id,@Month_position,@No_of_month_selection,@Updated_by,@Updated_date,@Updated_idate,@Pair_group_id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Month_name", month_name);
            cmd.Parameters.AddWithValue("@Month_id", month_id);
            cmd.Parameters.AddWithValue("@Month_position", position);
            cmd.Parameters.AddWithValue("@No_of_month_selection", no_of_month);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate()); 
            cmd.Parameters.AddWithValue("@Pair_group_id", no_of_pair_month);
            if (My.InsertUpdateData(cmd))
            {
            }
        }
    }
}