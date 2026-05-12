using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class vendor_general_expense_history : System.Web.UI.Page
    {
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
        string uploadby = " select top 1 name   from dbo.[user_details] where user_id=Vendor_general_expense.Created_by";
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
                        if (Session["successMsgs"] != null)
                        {
                            Alertme(Session["successMsgs"].ToString(), "success");
                            Session["successMsgs"] = null;
                        }

                        //7DayS
                        string TodaydatEtim = mycode.date();
                        DateTime SevenstartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string SevenDaysDate = SevenstartTime.AddDays(-7).ToShortDateString();
                        find_firm_details();
                        txt_s_date.Text = SevenDaysDate;
                        txt_e_date.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "vendor-general-expense-history.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        mycode.bind_all_ddl_with_id_cap_All(ddl_vendor_type, "select Vendor_type,Vendor_type_id from Vendor_type_master order by Vendor_type asc");
                        ViewState["usertype"] = My.get_user_type(ViewState["Userid"].ToString());
                        //bind_grd_view();
                        find_gridview_data();

                         





                        typeDV.Visible = false;
                        ViewState["verification_status"] = My.get_general_expenses();
                        if (ViewState["verification_status"].ToString() == "1")
                        {
                            typeDV.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Expense_List");
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

        My mycode = new My();
        


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit"].ToString() == "1")
            {

                try
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Response.Redirect("vendor-general-expense.aspx?edtsiD=" + lbl_Id.Text, false);
                }
                catch
                {
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_vendor_id = (Label)row.FindControl("lbl_vendor_id");

                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Vendor_general_expense where  id='" + lbl_Id.Text + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr.Delete();
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("General expense has been deleted Successfully", "success");
                find_gridview_data();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose from date", "warning");
                    txt_s_date.Focus();
                }
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose to date", "warning");
                    txt_e_date.Focus();
                }
                else
                {
                    find_gridview_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_gridview_data()
        {
            int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
            int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    Bind_data_date_wise(idate, idate2);
                }
            }
        }

        private void Bind_data_date_wise(int idate1, int idate21)
        {
            lbl_date.Text = txt_s_date.Text + " To " + txt_e_date.Text;

            string query = "";
            if (ddl_vendor_type.SelectedItem.Text == "ALL")
            {
                if (ddl_type1.Text == "ALL")
                {
                    query = "select *,(select top 1 Company_name + ', (Mobile No. : ' + Mobile_no + ')' from Vendor_master where Vendor_id = Vendor_general_expense.Vendor_id) as Vendor_Name,(select top 1 Session from session_details where session_id = Vendor_general_expense.Financial_year) as Session_name,(" + uploadby + ") as uploadby  from Vendor_general_expense where Payment_idate >= " + idate1 + " and Payment_idate<= " + idate21 + " order by Id desc";
                }
                else
                {
                    query = "select *,(select top 1 Company_name + ', (Mobile No. : ' + Mobile_no + ')' from Vendor_master where Vendor_id = Vendor_general_expense.Vendor_id) as Vendor_Name,(select top 1 Session from session_details where session_id = Vendor_general_expense.Financial_year) as Session_name,(" + uploadby + ") as uploadby  from Vendor_general_expense where Payment_idate >= " + idate1 + " and Payment_idate<= " + idate21 + " and Expense_Approval_Status='" + ddl_type1.Text + "'  order by Id desc";
                }
            }
            else
            {
                if (ddl_type1.Text == "ALL")
                {
                    query = "select * from (select *,(select top 1 Company_name + ', (Mobile No. : ' + Mobile_no + ')' from Vendor_master where Vendor_id = Vendor_general_expense.Vendor_id) as Vendor_Name,(select top 1 Session from session_details where session_id = Vendor_general_expense.Financial_year) as Session_name,(" + uploadby + ") as uploadby,(select top 1 Type_of_vendor from Vendor_master where Vendor_id = Vendor_general_expense.Vendor_id) as Type_of_vendor from Vendor_general_expense where Payment_idate >= " + idate1 + " and Payment_idate<= " + idate21 + ") t where Type_of_vendor='" + ddl_vendor_type.SelectedValue + "' order by Id desc";
                }
                else
                {
                    query = "select * from (select *,(select top 1 Company_name + ', (Mobile No. : ' + Mobile_no + ')' from Vendor_master where Vendor_id = Vendor_general_expense.Vendor_id) as Vendor_Name,(select top 1 Session from session_details where session_id = Vendor_general_expense.Financial_year) as Session_name,(" + uploadby + ") as uploadby,(select top 1 Type_of_vendor from Vendor_master where Vendor_id = Vendor_general_expense.Vendor_id) as Type_of_vendor from Vendor_general_expense where Payment_idate >= " + idate1 + " and Payment_idate<= " + idate21 + " and Expense_Approval_Status='" + ddl_type1.Text + "') t where Type_of_vendor='" + ddl_vendor_type.SelectedValue + "' order by Id desc";
                }
            }
            Bind_maon_grid(query);
        }

        private void Bind_maon_grid(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
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

        #region verify by 
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
            ViewState["slip_no"] = "0";
        }

        protected void lnk_verify_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_slip_no = (Label)row.FindControl("lbl_slip_no");
                ViewState["slip_no"] = lbl_slip_no.Text;
                Bind_data_pipup();
                myModal2.Visible = true;
            }
            catch
            {

            }

        }

        private void Bind_data_pipup()
        {
            string query = "select *,(select top 1 Company_name + ', (Mobile No. : ' + Mobile_no + ')' from Vendor_master where Vendor_id = Vendor_general_expense.Vendor_id) as Vendor_Name,(select top 1 Session from session_details where session_id = Vendor_general_expense.Financial_year) as Session_name,(" + uploadby + ") as uploadby  from Vendor_general_expense where Slip_no = '" + ViewState["slip_no"].ToString() + "'  ";

            DataTable dt = mycode.FillData(query);
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
        #endregion

        protected void btn_conf_remove_Click(object sender, EventArgs e)
        {
            if (ddl_type.Text == "Select")
            {
                Alertme("Please select type", "warning");
                myModal2.Visible = true;
            }
            else if (txt_remarks.Text == "")
            {
                Alertme("Please eneter remarks type", "warning");
                myModal2.Visible = true;

            }
            else
            {
                //My.exeSql("update Vendor_general_expense set Status='0',Remove_cause='" + txt_reason.Text + "',Updated_by='" + ViewState["Userid"].ToString() + "',Updated_date='" + mycode.date() + "',Updated_idate='" + mycode.idate() + "' where Hostel_assign_id='" + ViewState["RmovEID"].ToString() + "'");

                SqlCommand cmd;
                string query = "Update Vendor_general_expense set Expense_Approval_Status=@Expense_Approval_Status,Verify_By=@Verify_By,Expense_Approval_Remarks=@Expense_Approval_Remarks,Expense_Approval_Date=@Expense_Approval_Date,Expense_Approval_Idate=@Expense_Approval_Idate   where Slip_no = @Slip_no";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Expense_Approval_Status", "Verified");
                cmd.Parameters.AddWithValue("@Verify_By", ViewState["Userid"].ToString());

                cmd.Parameters.AddWithValue("@Expense_Approval_Date", mycode.date());
                cmd.Parameters.AddWithValue("@Expense_Approval_Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Expense_Approval_Remarks", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@Slip_no", ViewState["slip_no"]);
                if (My.InsertUpdateData(cmd))
                {
                    if (ddl_type.Text == "Approved")
                    {
                        Alertme("General expenses has been verified successfully.", "success");
                    }
                    else
                    {
                        Alertme("General expenses has been rejected successfully.", "success");
                    }
                    myModal2.Visible = false;
                    Bind_maon_grid(ViewState["query"].ToString());
                }

            }

        }

        double ttl_amt = 0;
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string amount = ((Label)e.Item.FindControl("lbl_payment_amount")).Text;


                if (((Label)e.Item.FindControl("lbl_Expense_Approval_Status")).Text == "Pending")
                {
                    if (ViewState["usertype"].ToString() == "Admin")
                    {
                        ((LinkButton)e.Item.FindControl("lnk_verify")).Visible = true;
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnk_verify")).Visible = false;
                    }

                }
                else if (((Label)e.Item.FindControl("lbl_Expense_Approval_Status")).Text == "")
                {
                    if (ViewState["usertype"].ToString() == "Admin")
                    {
                        ((LinkButton)e.Item.FindControl("lnk_verify")).Visible = true;
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnk_verify")).Visible = false;
                    }

                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnk_verify")).Visible = false;

                }

                ttl_amt = ttl_amt + My.toDouble(amount);
            }
            lbl_ttl_amount.Text = ttl_amt.ToString("0.00");
        }
    }
}