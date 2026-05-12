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
    public partial class month_index_setting : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id_no_select(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_fee_taken();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthIndexMaster");
            }
        }

        private void find_fee_taken()
        {
            SqlCommand cmd;
           // string strQuery = "Select * from Student_Payment_History where Session=@Session";
            string strQuery = "select   * from dbo.[Student_Payment_History] where Type not in ('Admission','Annual')";
             
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Session", My.get_session());
            DataTable dt = mycode.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                btn_Submit.Visible = true;
                ddl_month.Enabled = true;
                ddl_month.CssClass = "form-control";
            }
            else
            {
                btn_Submit.Visible = false;
                ddl_month.Enabled = false;
                ddl_month.CssClass = "form-control";
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


        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    if (ddl_month.SelectedItem.Text == "Select")
                    {
                        Alertme("Please choose month.", "warning");
                        ddl_month.Focus();
                        return;
                    }
                    string qry = "";
                    int posiion = 1;
                    qry = qry + @"Update Month_Index set Position='" + posiion + "' where Month_Id='" + ddl_month.SelectedValue + "'";
                    SqlDataAdapter ad = new SqlDataAdapter(" (select top 100 * from dbo.[Month_Index] where Month_Id>" + ddl_month.SelectedValue + ") Union ALl((select top 100 * from dbo.[Month_Index] where Month_Id<" + ddl_month.SelectedValue + "))", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Month_Index");
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount == 0)
                    {
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            posiion++;
                            qry = qry + @"Update Month_Index set Position='" + posiion + "' where Month_Id='" + dr["Month_Id"].ToString() + "'";
                        }
                    }
                    My.exeSql(qry);
                    Alertme("Month has been update index", "success");
                    find_fee_taken();
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");

                }



            }
            catch (Exception ex)
            {
            }
        }
    }
}