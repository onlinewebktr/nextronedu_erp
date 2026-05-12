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
    public partial class Delete_Bill : System.Web.UI.Page
    {
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
                    ViewState["Userid"] = Session["Admindov"].ToString();
                    ViewState["firm_id"] = My.get_firm_id();
                    ViewState["branchid"] = "1";

                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");


                }
            }
        }


        #region find data
        string scrpt;
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {

                lblmessage.Text = "Please enter admission";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_session.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

            }
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and StudentStatus='AV'  and  Status='1'  and Branch_id='" + ViewState["branchid"].ToString() + "'   ";
                find_details(query);


            }

        }
        public void Alert(string Message)
        {
            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void find_details(string query)
        {
            pnl_payment_history.Visible = false;

            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {

                pnl_payment_history.Visible = false;
                Alert("Student details not found...");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    pnl_payment_history.Visible = false;



                    ViewState["class_id"] = dr["Class_id"].ToString();


                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();

                    ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";

                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["admissionserialnumber"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["id"] = dr["id"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();






                    bind_payment_history();

                }

            }
        }
        My mycode = new My();
        private void bind_payment_history()
        {
            pnl_payment_history.Visible = false;
            string type = "Monthly";
            string query = "  select t1.*  from Student_Payment_History t1 where t1.Class_id='" + ViewState["classid"].ToString() + "' and t1.Session='" + ViewState["session"].ToString() + "' and   t1.Addmission_no='" + ViewState["admissionserialnumber"].ToString() + "'   and t1.Type='" + type + "' order by t1.Idate desc,id desc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                // Alertme("There are no payment history found", "warning");
                lbl_msg.Text = "There are no payment history found";
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                pnl_payment_history.Visible = true;
                lbl_msg.Text = "";
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
            }
        }
        #endregion
        double total = 0;
        int count = 0;
        string m = "0";
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_payable.Text);
                }

                Button btn_delete_bill = (Button)e.Row.FindControl("btn_delete_bill");

                if (count == 0)
                {
                    btn_delete_bill.Visible = true;
                }
                count = count + 1;

                Label lbl_monthnam = (Label)e.Row.FindControl("lbl_monthnam");
                Label lbl_slipno = (Label)e.Row.FindControl("lbl_slipno");
                lbl_monthnam.Text = get_monthname(lbl_slipno.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totalamount");

                lbl_totaldiscount.Text = total.ToString("0.00");
            }
        }

        private string get_monthname(string slipno)
        {

            string CategoryID = "";
            DataTable dt = mycode.FillData("Select distinct  Month from Monthly_Fee_Collection_Slip where   slipno='" + slipno + "' ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CategoryID1 = dt.Rows[i]["Month"].ToString();
                    if (CategoryID == "")
                    {
                        CategoryID = CategoryID1;
                    }
                    else
                    {
                        CategoryID = CategoryID + "," + CategoryID1;

                    }
                }
            }

            return CategoryID;
        }

        protected void btn_delete_bill_Click(object sender, EventArgs e)
        {
            string type = "";

            type = "Monthly";


            if (type != "")
            {
                SqlCommand cmd;
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_slipno = (Label)row.FindControl("lbl_slipno");
                Label lbl_Addmission_no = (Label)row.FindControl("lbl_Addmission_no");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Branchid = (Label)row.FindControl("lbl_Branchid");
                Label lbl_Session = (Label)row.FindControl("lbl_Session");
                Label lbl_Amount = (Label)row.FindControl("lbl_Amount");

                Label lbl_Transection_in = (Label)row.FindControl("lbl_Transection_in");


                string query = "INSERT INTO admission_registor_Change_admission_no_history (Current_admission_no,Session_id,Created_By,Date_time,Change_type,Class_Id_New,Roll_no_New,Slip_no,New_Section,Branch_id) values (@Current_admission_no,@Session_id,@Created_By,@Date_time,@Change_type,@Class_Id_New,@Roll_no_New,@Slip_no,@New_Section,@Branch_id)";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Current_admission_no", lbl_Addmission_no.Text);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Change_type", type + " Fees Delete");
                cmd.Parameters.AddWithValue("@Class_Id_New", lbl_Class_id.Text);
                cmd.Parameters.AddWithValue("@Roll_no_New", "");
                cmd.Parameters.AddWithValue("@Slip_no", lbl_slipno.Text);
                cmd.Parameters.AddWithValue("@New_Section", "");
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    string qery = @"INSERT INTO Student_Payment_History_Save_bakup (Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,insert_time_date,Insert_time_user_id,New_Slip_no)
                    SELECT Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Money_Receipt_Date,Money_Receipt_Idate,Unique_id,Adjust_type,Remarks,Transection_in,'" + My.getdate1() + "','" + ViewState["Userid"].ToString() + "','" + lbl_slipno.Text + "' FROM Student_Payment_History where Addmission_no='" + lbl_Addmission_no.Text + "' and Session='" + lbl_Session.Text + "' and Class_id='" + lbl_Class_id.Text + "' and Branch='" + ViewState["branchid"].ToString() + "' and Slip_no='" + lbl_slipno.Text + "'";

                    mycode.executequery(qery);
                    mycode.executequery("delete from Monthly_Fee_Collection_Slip where adno='" + lbl_Addmission_no.Text + "' and class='" + lbl_Class_id.Text + "' and session='" + lbl_Session.Text + "'  and slipno='" + lbl_slipno.Text + "'");
                    mycode.executequery("delete from Student_Payment_History where  Addmission_no='" + lbl_Addmission_no.Text + "' and Session='" + lbl_Session.Text + "' and Class_id='" + lbl_Class_id.Text + "'  and Slip_no='" + lbl_slipno.Text + "'");
                    mycode.executequery("delete from SchoolLedger where Addmission_no='" + lbl_Addmission_no.Text + "'  and TransactionId='" + lbl_slipno.Text + "'");

                    double total_amount = My.toDouble(lbl_Amount.Text);

                    string parameter = "";
                    parameter = ViewState["hosteltaken"].ToString() == "No" ? "MonthlyFee" : "MonthlyFee";

                    #region update dues amount
                    string app_payment_type = My.session("App_fee_collection_type");
                    string qry = "";
                    qry = "  select *  from Typewise_fee_collection  where admission_no='" + lbl_Addmission_no.Text + "' and session='" + lbl_Session.Text + "'   and parameter like '%" + parameter + "%'   order by cast(Position as float) desc,parameter desc,id desc";
                    SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Typewise_fee_collection");
                    DataTable tdt = ds.Tables[0];
                    if (tdt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        string prev_month = "", month = "";
                        foreach (DataRow dr in tdt.Rows)
                        {
                            month = dr["month"].ToString();
                            if (total_amount > 0)
                            {
                                double dues_paid = My.toDouble(dr["paid"]);
                                if (total_amount >= dues_paid)
                                {
                                    total_amount = total_amount - dues_paid;
                                    dr["paid"] = "0";
                                    dr["dues"] = dr["Payable_after_disc"];
                                    dr["status"] = "Dues";
                                    dr.Delete();
                                }
                                else
                                {

                                    dr["paid"] = My.toDouble(dr["paid"]) - total_amount;
                                    dr["dues"] = My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"]);
                                    dr["status"] = "Dues";
                                    total_amount = 0;

                                    break;

                                }
                                prev_month = month;
                            }
                            else
                            {
                                break;
                            }

                        }
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(tdt);
                    }

                    #endregion
                    Alert("Your selected bill no has been deleted sucessfully");

                    string remarks = type + " Fees Delete";
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Bill No :- " + lbl_slipno.Text + "," + remarks + " has been deleted successfully.");
                    bind_payment_history();

                }

            }
        }




    }


}