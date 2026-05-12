using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class update_fee : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admindov"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    ViewState["Admindov"] = Session["Admindov"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");

                }
            }
        }
        string scrpt;

        protected void btn_update_fee_Click(object sender, EventArgs e)
        {
            try
            {
                update_feesss();
                lblmessage.Text = "Updated successfully.";
            }
            catch (Exception ex)
            {
            }
        }

        private void update_feesss()
        {
            string payment_mode = ""; string payment_mode_for_std_pay_his = "";
            string payment_mode_or = ""; string payment_mode_for_std_pay_his_or = "";
            if (ddl_payment_type.SelectedValue == "1")
            {
                payment_mode = "AdmissionFee";
                payment_mode_for_std_pay_his = "Admission";

                payment_mode_or = "HostelAdmissionFee";
                payment_mode_for_std_pay_his_or = "HostelAdmission";
            }
            else
            {
                payment_mode = "AnnualFee";
                payment_mode_for_std_pay_his = "Annual";

                payment_mode_or = "HostelAnnual";
                payment_mode_for_std_pay_his_or = "HostelAnnual";
            }

            My.exeSql("update Typewise_fee_collection set status='Dues',paid='0.00',dues=Payable_after_disc where (parameter='" + payment_mode + "' or parameter='" + payment_mode_or + "')  and session='" + ddl_session.SelectedItem.Text + "'; delete from Monthly_Fee_Collection_Slip  where session='" + ddl_session.SelectedItem.Text + "' and (parameter='" + payment_mode + "' or parameter='" + payment_mode_or + "')");
            ViewState["branchid"] = "1";
            ViewState["Userid"] = "1";
            DataTable dt = mycode.FillData("select * from Student_Payment_History where (Type='" + payment_mode_for_std_pay_his + "' or Type='" + payment_mode_for_std_pay_his_or + "') and Session='" + ddl_session.SelectedItem.Text + "' order by Idate asc");
            if (dt.Rows.Count == 0)
            { }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string studentregid = dr["Addmission_no"].ToString();
                    string Session = dr["Session"].ToString();
                    string Date = dr["Date"].ToString();
                    DataTable dt2 = mycode.FillData("select * from admission_registor where session='" + Session + "' and admissionserialnumber='" + studentregid + "'  ");
                    if (dt2.Rows.Count > 0)
                    {
                        ViewState["Section"] = dt2.Rows[0]["Section"].ToString();
                        ViewState["Section"] = dt2.Rows[0]["Section"].ToString();
                        ViewState["category_id"] = dt2.Rows[0]["Category_id"].ToString();
                        ViewState["SubCategory_id"] = dt2.Rows[0]["SubCategory_id"].ToString();
                        ViewState["hostaltaken"] = dt2.Rows[0]["hosteltaken"].ToString();
                        ViewState["session"] = dt2.Rows[0]["session"].ToString();
                        ViewState["Addmission_no"] = dt2.Rows[0]["admissionserialnumber"].ToString();
                        ViewState["classid"] = dt2.Rows[0]["Class_id"].ToString();
                        ViewState["class"] = dt2.Rows[0]["class"].ToString();


                        string type = dr["Type"].ToString();
                        string slip_no = dr["Slip_no"].ToString();
                        string Tag = dr["Amount"].ToString();
                        string entry_id = dr["Entry_id"].ToString();
                        string Amount = dr["Amount"].ToString();
                        string parameter = "";

                        if (type == "Admission")
                        {
                            parameter = ViewState["hostaltaken"].ToString() == "No" ? "AdmissionFee" : "HostelAdmissionFee";
                        }
                        else
                        {
                            parameter = ViewState["hostaltaken"].ToString() == "No" ? "AnnualFee" : "HostelAnnualFee";
                        } 

                        send_data_in_feetypewise_collection(slip_no, entry_id, parameter, Amount, studentregid, Date);
                    }
                }
            }
        }

        private void send_data_in_feetypewise_collection(string slip_no, string entry_id, string parameter, string Amount, string studentregid, string Date)
        {
            string class_id = ViewState["classid"].ToString();
            double paid_amount = My.toDouble(Amount);
            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + studentregid + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable tdt = ds.Tables[0];
            if (tdt.Rows.Count == 0)
            {  }
            else
            {
                foreach (DataRow dr in tdt.Rows)
                {
                    if (paid_amount >= 0)
                    {
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1"; 
                        double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                        dr["Date"] = Date;
                        dr["idate"] = My.toDateTime(Date).ToString("yyyyMMdd");
                        if (paid_amount >= dues)
                        {
                            string prevpaid = dr["paid"].ToString();
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = dr["dues"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]).ToString("0.00");
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            #region send in collection slip
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), dues.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], studentregid, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, Date);
                            #endregion
                        }
                        else
                        {
                            string prevpaid = dr["paid"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                            dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                            dr["status"] = "Dues";

                            #region send in collection slip
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], studentregid, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, Date);
                            #endregion

                            paid_amount = 0;

                        }
                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                    }
                    else
                    {
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }
        }


        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string Date)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + ViewState["branchid"].ToString() + "','0','" + Date + "','" + My.DateConvertToIdate(Date) + "');";
            My.exeSql(qry);
        }
    }
}