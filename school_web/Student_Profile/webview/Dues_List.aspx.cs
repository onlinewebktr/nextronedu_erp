using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.HtmlControls;
namespace school_web.Student_Profile.webview
{
    public partial class Dues_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {

                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    Session["regid"] = ViewState["regid"].ToString();

                    Find_student_details();
                    ViewState["month"] = "";






                }
            }
        }

        private void Find_student_details()
        {
            try
            {

                string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV' and Is_TC_Taken!='true' and Transfer_Status in ('New','NT') order by id desc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {


                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {

                    ViewState["Session"] = dr["session"].ToString();
                    ViewState["email_id"] = dr["email_id"].ToString();
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["rollnumber"] = dr["rollnumber"].ToString();
                    ViewState["studentname"] = dr["studentname"].ToString();
                    ViewState["fathername"] = dr["fathername"].ToString();
                    ViewState["class"] = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";
                    ViewState["transportationtaken"] = dr["transportationtaken"].ToString();
                    ViewState["mobilenumber"] = dr["father_mob"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else if (dr["Transportation_Id"].ToString() == "&nbsp;")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }
                    if (dr["Section"].ToString() == "")
                    {

                        ViewState["Section"] = "A";
                    }
                    else if (dr["Section"].ToString() == "&nbsp;")
                    {

                        ViewState["Section"] = "A";
                    }
                    else
                    {
                        ViewState["Section"] = dr["Section"].ToString();
                    }

                }
                find_all_due_fee();
            }
        }

        private void find_all_due_fee()
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("value");
            dtDatas.Columns.Add("fee_amount");
            dtDatas.Columns.Add("discount_per");
            dtDatas.Columns.Add("paid_peev");
            dtDatas.Columns.Add("paid_status");
            dtDatas.Columns.Add("bac_colour");
            int temp;
            List<string> lst = new List<string>();
            string temp_month = My.get_start_month();
            //  lst.Add(temp_month);



            for (temp = 1; temp <= 12; temp++)
            {
                DataTable paid_dt = My.dataTable(" select month,status from dbo.[Typewise_fee_collection] where   session='" + ViewState["Session"].ToString() + "' and admission_no='" + ViewState["regid"].ToString() + "' and parameter like '%MonthlyFee%' and month='" + temp_month + "'");
                if (paid_dt.Rows.Count > 0)
                {
                    string remove_month = "";
                    foreach (DataRow pdr in paid_dt.Rows)
                    {
                        if (pdr["status"].ToString() == "Dues")
                        {
                            lst.Add(temp_month);
                            break;
                        }
                    }
                }
                else
                {
                    lst.Add(temp_month);
                }
                temp_month = find_month(temp_month);
            }

            int i = 0;
            foreach (string str in lst)
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["Month"] = lst[i];
                drNewRow["value"] = false;
                //   drNewRow["discount_per"] = find_discount(lst[i].ToString(), txt_admission_no.Text, ddlsession.Text, class_id);
                drNewRow["paid_status"] = "NotCreated";
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                i++;
            }
            find_prev_dues(dtDatas);
            GridView2.DataSource = dtDatas.DefaultView;
            GridView2.DataBind();


            bind_monthly_fee();




        }



        private string find_month(string month)
        {
            string next = "nn";
            switch (month)
            {
                case "January":
                    next = "February";
                    return next;

                case "February":
                    next = "March";
                    return next;

                case "March":
                    next = "April";
                    return next;

                case "April":
                    next = "May";
                    return next;

                case "May":
                    next = "June";
                    return next;

                case "June":
                    next = "July";
                    return next;

                case "July":
                    next = "August";
                    return next;

                case "August":
                    next = "September";
                    return next;

                case "September":
                    next = "October";
                    return next;

                case "October":
                    next = "November";
                    return next;

                case "November":
                    next = "December";
                    return next;

                case "December":
                    next = "January";
                    return next;

            }
            return next;
        }
        DataTable prevdues_dt = new DataTable();
        private void find_prev_dues(DataTable dtDatas)
        {
            prevdues_dt = My.dataTable(" select sum(isnull(cast(dues as float),'0')) dues,month from dbo.[Typewise_fee_collection] where session='" + ViewState["Session"].ToString() + "' and status='Dues' and Class='" + ViewState["class"].ToString() + "' and admission_no='" + ViewState["regid"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' group by month");
            foreach (DataRow mr in dtDatas.Rows)
            {
                var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                if (row.Length > 0)
                {
                    mr["paid_status"] = "Created";
                    mr["bac_colour"] = "Yellow";
                }
            }
            show_dues(dtDatas);
        }
        double anula_dues = 0; double prev_session_dues = 0;
        double admission_dues = 0; string adm_transection = "";
        private void show_dues(DataTable dtDatas)
        {
            double month_dues = 0;
            foreach (DataRow mr in dtDatas.Rows)
            {
                if (My.toBool(mr["Value"]))
                {
                    var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                    if (row.Length > 0)
                    {
                        DataRow dr = row[0];
                        month_dues += My.toDouble(dr["dues"]);
                    }
                }
            }
            if (month_dues + admission_dues + anula_dues + prev_session_dues == 0)
            {
                //txt_previousduesmonth.Text = "0";
                //chk_prev_dues.Visibility = Visibility.Collapsed;
                //chk_prev_dues.Uid = "0";
                //txt_view.Visibility = Visibility.Collapsed;
            }
            else
            {
                //chk_prev_dues.IsChecked = true;
                //chk_prev_dues.Visibility = Visibility.Visible;
                //txt_previousduesmonth.Text = (month_dues + admission_dues + anula_dues + prev_session_dues).ToString();
                //chk_prev_dues.Content = "Previous dues (Rs. " + txt_previousduesmonth.Text + ")";
                //chk_prev_dues.Uid = txt_previousduesmonth.Text;
                //txt_view.Visibility = Visibility.Visible;
            }
        }




        private void bind_monthly_fee()//parameter_id=4 month 
        {
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();
            int growcountS = GridView2.Rows.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)GridView2.Rows[iS].FindControl("lbl_Month");


                fee_amount = 0; disc_amount = 0; paid_previously = 0;
                DataTable feedt = new DataTable();

                string type = "";
                //dr["paid_status"] = "Created";
                //dr["bac_colour"] = "Yellow";
                if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["regid"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                {
                    feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter,      feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,(isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["regid"].ToString() + "' and month='" + lbl_Month.Text + "'   and parameter_id='4' and session='" + ViewState["Session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All' and month='" + lbl_Month.Text + "'  and parameter_id='4' and session='" + ViewState["Session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))) disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["regid"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' ");
                    type = "Calculated";
                }
                else
                {
                    //dr["bac_colour"] = "White";
                    Dictionary<string, object> dc = new Dictionary<string, object>();
                    dc["admission_no"] = ViewState["regid"].ToString();
                    dc["session_id"] = My.get_session_id();
                    dc["class"] = ViewState["class"].ToString();
                    dc["session"] = My.get_session();
                    dc["class_id"] = ViewState["classid"].ToString();
                    dc["hosteltaken"] = ViewState["hosteltaken"].ToString();
                    dc["months"] = lbl_Month.Text;
                    dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                    dc["hostel_id"] = ViewState["hosteltaken"].ToString().ToLower() == "yes" ? My.toint(ViewState["hostel_id"].ToString()) : 0;
                    dc["day_boarding"] = ViewState["day_bording"].ToString();
                    dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                    dc["category_id"] = ViewState["category_id"].ToString();
                    dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                    dc["transportportation_id"] = ViewState["transportID"].ToString();
                    feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                    feedt.Columns.Add("previously_paid");
                    type = "NotCalculated";
                }
                feedt.Columns.Add("total_payable");
                string month = "";
                double total = 0, fee = 0, disc = 0, paid_prev = 0;
                foreach (DataRow dr in feedt.Rows)
                {
                    if (type == "Calculated")
                    {
                        My.exeSql("update dbo.[Typewise_fee_collection] set Disc='" + dr["disc_amount"].ToString() + "' where Id='" + dr["id"].ToString() + "'");
                    }
                    month = dr["months"].ToString();
                    dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["previously_paid"]);
                    fee += My.toDouble(dr["amount"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    paid_prev += My.toDouble(dr["previously_paid"]);
                    total += My.toDouble(dr["total_payable"]);
                }

                foreach (DataRow dr in feedt.Rows)
                {
                    try
                    {
                        fdt.Rows.Add(dr.ItemArray);
                    }
                    catch
                    {
                        foreach (DataColumn dc in feedt.Columns)
                        {
                            fdt.Columns.Add(dc.ColumnName);
                        }
                        fdt.Rows.Add(dr.ItemArray);
                    }
                }

                //lbl_fee_month.Text = month;
                //lbl_fee_amount.Text = fee.ToString();
                //lbl_discount.Text = disc.ToString();
                //lbl_paid_prev.Text = paid_prev.ToString();
                //lbl_total.Text = total.ToString();

            }

            if (fdt.Rows.Count.ToString() != "0")
            {
                rp_fee_details.DataSource = fdt.DefaultView;
                rp_fee_details.DataBind();
                bind_ttl_fee();
                pnl_month_wise_fee_details.Visible = true;


            }
            else
            {

                pnl_month_wise_fee_details.Visible = false;
                lbl_fee_amount.Text = "0";
                lbl_discount.Text = "0";
                lbl_paid_prev.Text = "0";
                lbl_total.Text = "0";


            }
        }

        private void bind_ttl_fee()
        {
            int i;
            double totalAmt = 0; double totaldisc = 0; double totalPrepAid = 0; double totalpblE = 0;
            int gridview_rowcount = rp_fee_details.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_amount = (Label)rp_fee_details.Items[i].FindControl("lbl_amount");
                Label lbl_disc_amt = (Label)rp_fee_details.Items[i].FindControl("lbl_disc_amt");
                Label lbl_pre_paid = (Label)rp_fee_details.Items[i].FindControl("lbl_pre_paid");
                Label lbl_tot_pble = (Label)rp_fee_details.Items[i].FindControl("lbl_tot_pble");
                if (lbl_amount.Text != "")
                {
                    totalAmt = totalAmt + Convert.ToDouble(lbl_amount.Text);
                }
                if (lbl_disc_amt.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(lbl_disc_amt.Text);
                }
                if (lbl_pre_paid.Text != "")
                {
                    totalPrepAid = totalPrepAid + Convert.ToDouble(lbl_pre_paid.Text);
                }
                if (lbl_tot_pble.Text != "")
                {
                    totalpblE = totalpblE + Convert.ToDouble(lbl_tot_pble.Text);
                }
            }
            lbl_fee_amount.Text = totalAmt.ToString();
            lbl_discount.Text = totaldisc.ToString();
            lbl_paid_prev.Text = totalPrepAid.ToString();
            lbl_total.Text = totalpblE.ToString();



            ViewState["total"] = totalAmt.ToString();
            ViewState["paid_prev"] = totalPrepAid.ToString();
            ViewState["discount"] = totaldisc.ToString();
            ViewState["totalbill"] = totalpblE.ToString();
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["firstrow"] == null)
                {
                    CheckBox chk_month = (CheckBox)e.Row.FindControl("chk_month");
                    chk_month.Enabled = true;
                    ViewState["firstrow"] = "1";
                }
            }
        }

        protected void rp_fee_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string lbl_mnth = ((Label)e.Item.FindControl("lbl_mnth")).Text;
                HtmlTableRow row2 = (HtmlTableRow)e.Item.FindControl("row");

                if (lbl_mnth == "April")
                {
                    row2.Attributes["style"] = "background-color:#CCE1F2";
                }
                if (lbl_mnth == "May")
                {
                    row2.Attributes["style"] = "background-color:#C6F8E5";
                }
                if (lbl_mnth == "June")
                {
                    row2.Attributes["style"] = "background-color:#FBF7D5";
                }
                if (lbl_mnth == "July")
                {
                    row2.Attributes["style"] = "background-color:#F9DED7";
                }
                //===
                if (lbl_mnth == "August")
                {
                    row2.Attributes["style"] = "background-color:#F5CDDE";
                }
                if (lbl_mnth == "September")
                {
                    row2.Attributes["style"] = "background-color:#E2BEF1";
                }
                if (lbl_mnth == "October")
                {
                    row2.Attributes["style"] = "background-color:#FBC5B0";
                }
                if (lbl_mnth == "November")
                {
                    row2.Attributes["style"] = "background-color:#BBD5D3";
                }
                //===
                if (lbl_mnth == "December")
                {
                    row2.Attributes["style"] = "background-color:#FCD0BA";
                }
                if (lbl_mnth == "January")
                {
                    row2.Attributes["style"] = "background-color:#E8CBD9";
                }
                if (lbl_mnth == "February")
                {
                    row2.Attributes["style"] = "background-color:#C9EAE8";
                }
                if (lbl_mnth == "March")
                {
                    row2.Attributes["style"] = "background-color:#99D7D2";
                }
            }
        }
    }
}