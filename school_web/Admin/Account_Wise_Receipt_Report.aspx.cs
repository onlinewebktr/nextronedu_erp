using school_web.AppCode;
using System;
using System.Collections;
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
    public partial class Account_Wise_Receipt_Report : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_particular(string itemName)
        {

            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select  Account_Name  from dbo.[Account_Ledger_Details] where Group_id in(" + HttpContext.Current.Session["groupList"].ToString() + ") and firm='" + My.firm_id() + "' and  Account_Name LIKE '%'+@SearchprojectName+'%' order by Account_Name asc";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Account_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        string VoucherType = "Receipt";
        string scrpt;
        My mycode = new My();
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
                        if (!IsPostBack)
                        {


                            ViewState["Userid"] = Session["Admin"].ToString();

                            ViewState["name"] = mycode.get_user(ViewState["Userid"].ToString());
                            string pagename_current = "Account_Wise_Receipt_Report.aspx";
                            Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                            ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                            ViewState["Is_delete"] = (String)dc1["Is_delete"];
                            ViewState["Is_Download"] = (String)dc1["Is_Download"];
                            ViewState["Is_Print"] = (String)dc1["Is_Print"];
                            ViewState["Is_add"] = (String)dc1["Is_add"];
                            ViewState["usertype"] = My.get_user_type(ViewState["Userid"].ToString());
                            find_firm_details();

                            txt_s_date.Text = mycode.date();
                            txt_e_date.Text = mycode.date();
                            fetch_particular();

                        }



                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_ledger_details");
            }

        }
        private void fetch_particular()
        {
            string groupList = "";
            ArrayList al = new ArrayList();
            SqlDataAdapter ad = new SqlDataAdapter("select distinct Group_id from Account_Ledger_Groups where Group_id not in('27','28') and firm='" + My.firm_id() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "binary_status");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {

            }
            else
            {
                al.Add('0');
                foreach (DataRow dr in dt.Rows)
                {
                    string Group_id = dr[0].ToString();
                    if (!al.Contains(Group_id))
                        al.Add(Group_id);
                    bind_gridview(al);
                }
                ArrayList uniqueList = new ArrayList();

                foreach (var item in al)
                {
                    if (!uniqueList.Contains(item))
                    {
                        uniqueList.Add(item);
                        if (ViewState["groupList"] == null)
                            ViewState["groupList"] = "'" + item + "'";
                        else
                            ViewState["groupList"] = ViewState["groupList"].ToString() + ",'" + item + "'";
                    }
                }
                groupList = groupList + ViewState["groupList"].ToString();

            }
            Session["groupList"] = groupList;

        }
        private void bind_gridview(ArrayList al)
        {

            string group = "";
            for (int i = 0; i < al.Count; i++)
            {
                if (ViewState["groupList"] == null)
                    ViewState["groupList"] = "'" + al[i].ToString() + "'";
                else
                    ViewState["groupList"] = ViewState["groupList"].ToString() + ",'" + al[i].ToString() + "'";


                string qry = "select Group_id from Account_Ledger_Groups where Under='" + al[i].ToString() + "' and firm='" + My.firm_id() + "'";
                SqlDataAdapter ad1 = new SqlDataAdapter(qry, My.conn);
                DataSet ds1 = new DataSet();
                ad1.Fill(ds1, "Account_Ledger_Details");
                DataTable dt1 = ds1.Tables[0];
                int rowcount1 = ds1.Tables[0].Rows.Count;
                if (rowcount1 == 0)
                {
                }
                else
                {
                    group = dt1.Rows[0][0].ToString();
                    if (!al.Contains(group))
                        al.Add(group);
                }
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

                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

            }
        }

        private void bind_gridview()
        {
            double total_debit = 0;
            double total_credit = 0;
            txt_total_debit.Text = "0.00";
            txt_total_credit.Text = "0.00";
            btn_excels.Visible = false;
            print1.Visible = false;
            string fromdate = txt_s_date.Text;
            string todate = txt_e_date.Text;
            DateTime d1 = My.convert_to_datetime(fromdate, "dd/MMM/yyyy");
            DateTime d2 = My.convert_to_datetime(todate, "dd/MMM/yyyy");

            lbl_class22.Text = "Account Name :" + txt_account_name.Text + " From Date : " + txt_s_date.Text + " To Date : " + txt_e_date.Text;
            string alternetacc_id = My.find_account_id(txt_account_name.Text);

            DataTable dt = mycode.FillData(" select  VoucherNo_Manual as VoucherNo,Description,sum(cast(Credit as float)) Credit ,sum(cast(Debit as float)) Debit,unique_entry_id,ref_name,Date  from dbo.[Account_Voucher_Details]   where  VoucherType='Receipt' and firm ='" + My.firm_id() + "' and  Bill_from='SCHOOL'  and IDate>=" + d1.ToString("yyyyMMdd") + " and  IDate<=" + d2.ToString("yyyyMMdd") + " and Account_id='" + alternetacc_id + "'  group by VoucherNo,Date ,idate,unique_entry_id,ref_name,Description   order by idate desc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry! there are no records exist", "warning");
                Panel1.Visible = false;
                grd_view.DataSource = null;
                grd_view.DataBind();

            }
            else
            {
                Panel1.Visible = true;
                grd_view.DataSource = dt;
                grd_view.DataBind();
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    double credit = My.toDouble(dr["Credit"].ToString());
                    double debit = My.toDouble(dr["Debit"].ToString());
                    if (credit > 0)
                    {
                        total_credit += credit;
                    }
                    if (debit > 0)
                    {
                        total_debit += debit;
                    }
                }
                txt_total_debit.Text = total_debit.ToString("0.00");
                txt_total_credit.Text = total_credit.ToString("0.00");

            }


        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (txt_account_name.Text == "")
            {
                Alertme("Please select account name", "warning");
                txt_account_name.Focus();
                return;

            }
            else
            {
                bind_gridview();
            }
        }

        #region download_in_excel
        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Download"].ToString() == "1")
            {
                string pageName = My.with_excel_name("Receipt_vouchar") + ".xls";
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + pageName);
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion download_in_excel
    }
}