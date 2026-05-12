using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class ENACH_Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
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

                        ViewState["Is_Edit"] = "1";
                        ViewState["Is_delete"] = "1";
                        ViewState["Is_Download"] = "1";
                        ViewState["Is_Print"] = "1";
                        ViewState["Is_add"] = "1";



                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                        Session["classchange"] = "2";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
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


        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_data();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                ddl_session.Focus();
                Alertme("Please select session.", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter current admission no.", "warning");
            }
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and StudentStatus='AV' and Status='1'";
                find_details(query);
            }
        }

        private void find_details(string query)
        {
            Dictionary<string, object> dc2 = mycode.Firm_details();

            ViewState["fcontact_no"] = (String)dc2["contact_no"];
            ViewState["femail"] = (String)dc2["email"];


            pnldate.Visible = false;
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                pnldate.Visible = false;
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                bool checkenachreg = get_data_enenachreg();
                if (checkenachreg == true)
                {
                    std_basic_infoS.Visible = true;
                    pnldate.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    { 
                        lbl_name.Text = dr["studentname"].ToString();
                        lbl_father_name.Text = dr["fathername"].ToString();
                        lblclass.Text = dr["class"].ToString();
                        ViewState["class_id"] = dr["Class_id"].ToString();
                        txtsection.Text = dr["Section"].ToString();
                        lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                        txt_admission_no.Text = dr["admissionserialnumber"].ToString();

                        ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                        ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";

                        lbl_phone.Text = dr["father_mob"].ToString();
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
                        ViewState["hosteltaken"] = dr["hosteltaken"].ToString();
                        lbl_old_roll_no.Text = dr["rollnumber"].ToString();

                        if (dr["email_id"].ToString() == "")
                        {
                            std_basic_infoS.Visible = false;
                            pnldate.Visible = false;
                            Alertme("Please update father email id.", "warning");
                            return;
                        }
                        else
                        {
                            ViewState["femail"] = dr["email_id"].ToString();
                            std_basic_infoS.Visible = true;
                            pnldate.Visible = true;

                        }
                        if (dr["father_mob"].ToString() == "")
                        {
                            std_basic_infoS.Visible = false;
                            pnldate.Visible = false;
                            Alertme("Please update father mobile no.", "warning");
                            return;
                        }
                        else
                        {
                            ViewState["fcontact_no"] = dr["father_mob"].ToString();
                            std_basic_infoS.Visible = true;
                            pnldate.Visible = true;
                        }
                    }
                }
                else
                {
                    pnldate.Visible = false; 
                    std_basic_infoS.Visible = false;
                    Alertme("The admission number you entered is already registered for e-NACH.", "warning");
                    return;

                }
            }
        }

        private bool get_data_enenachreg()
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select * from enach_registration where admissionserialnumber='" + txt_admission_no.Text + "' and Status='Approved'", My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btn_update_final_Click(object sender, EventArgs e)
        {
            bool send = false;
            if (txt_date_new.Text == "")
            {
                Alertme("Please enter valid upto date", "warning");
            }
            else if (txt_Mandate_amount.Text == "")
            {
                Alertme("Please enter mandate amount", "warning");
            }
            else
            {
                //
                string merchantId = ConfigurationManager.AppSettings["merchantId"].ToString();
                string salt = ConfigurationManager.AppSettings["salt"].ToString();
                Random r = new Random(DateTime.Now.Millisecond);
                string txnId = DateTime.UtcNow.ToString("yyMMddHHMMss") + r.Next(12346, 48749);
                string totalamount = "1";
                string consumerId = txt_admission_no.Text;
                string accountNo = "";
                string consumerMobileNo = ViewState["fcontact_no"].ToString();
                string consumerEmailId = ViewState["femail"].ToString();
                string debitStartDate = mycode.date().Replace("/", "-");
                string debitEndDate = txt_date_new.Text.Replace("/", "-");
                string maxAmount = My.toDouble(txt_Mandate_amount.Text).ToString();
                string amountType = "M";
                string frequency = "ADHO";
                string token = "";
                token = merchantId + "|" + txnId + "|" + totalamount + "|" + accountNo + "|" + consumerId + "|" + consumerMobileNo + "|" + consumerEmailId + "|" + debitStartDate + "|" + debitEndDate + "|" + maxAmount + "|" + amountType + "|" + frequency + "|" + "|" + "|" + "|" + "|" + salt;

                string hash = ComputeSha512Hash(token);

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "bindFunction", "bindSubmitButton();", true);
                //merchantId|txnId|totalamount|accountNo|consumerId|consumerMobileNo|consumerEmailId|debitStartDate|debitEndDate|maxAmount|amountType|frequency|cardNumber|expMonth|expYear|cvvCode|SALT


                SqlCommand cmd2;
                cmd2 = new SqlCommand("select top 1 * from enach_registration where admissionserialnumber='" + consumerId + "'");
                DataTable dt = mycode.GetData(cmd2);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO enach_registration (admissionserialnumber,date_time,created_by,txnId,totalamount,debitStartDate,debitEndDate,token,hash_token,Status,Session_id) values (@admissionserialnumber,@date_time,@created_by,@txnId,@totalamount,@debitStartDate,@debitEndDate,@token,@hash_token,@Status,@Session_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@admissionserialnumber", consumerId);
                    cmd.Parameters.AddWithValue("@date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@txnId", txnId);
                    cmd.Parameters.AddWithValue("@totalamount", maxAmount);
                    cmd.Parameters.AddWithValue("@debitStartDate", debitStartDate);
                    cmd.Parameters.AddWithValue("@debitEndDate", debitEndDate);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.Parameters.AddWithValue("@hash_token", hash);
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    if (My.InsertUpdateData(cmd))
                    {
                        SqlCommand cmd1;
                        string query1 = "INSERT INTO enach_registration_history (admissionserialnumber,date_time,created_by,txnId,totalamount,debitStartDate,debitEndDate,token,hash_token,Session_id) values (@admissionserialnumber,@date_time,@created_by,@txnId,@totalamount,@debitStartDate,@debitEndDate,@token,@hash_token,@Session_id)";
                        cmd1 = new SqlCommand(query1);
                        cmd1.Parameters.AddWithValue("@admissionserialnumber", consumerId);
                        cmd1.Parameters.AddWithValue("@date_time", My.getdate1());
                        cmd1.Parameters.AddWithValue("@created_by", ViewState["Userid"].ToString());
                        cmd1.Parameters.AddWithValue("@txnId", txnId);
                        cmd1.Parameters.AddWithValue("@totalamount", maxAmount);
                        cmd1.Parameters.AddWithValue("@debitStartDate", debitStartDate);
                        cmd1.Parameters.AddWithValue("@debitEndDate", debitEndDate);
                        cmd1.Parameters.AddWithValue("@token", token);
                        cmd1.Parameters.AddWithValue("@hash_token", hash);
                        cmd1.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        if (My.InsertUpdateData(cmd1))
                        {
                            send = true;
                        }
                    }
                }
                else
                {

                    SqlCommand cmd3;
                    string query = "Update enach_registration set date_time=@date_time,txnId=@txnId,totalamount=@totalamount,debitStartDate=@debitStartDate,debitEndDate=@debitEndDate,token=@token,hash_token=@hash_token where admissionserialnumber=@admissionserialnumber";
                    cmd3 = new SqlCommand(query);
                    cmd3.Parameters.AddWithValue("@admissionserialnumber", consumerId);
                    cmd3.Parameters.AddWithValue("@date_time", My.getdate1());
                    cmd3.Parameters.AddWithValue("@created_by", ViewState["Userid"].ToString());
                    cmd3.Parameters.AddWithValue("@txnId", txnId);
                    cmd3.Parameters.AddWithValue("@totalamount", maxAmount);
                    cmd3.Parameters.AddWithValue("@debitStartDate", debitStartDate);
                    cmd3.Parameters.AddWithValue("@debitEndDate", debitEndDate);
                    cmd3.Parameters.AddWithValue("@token", token);
                    cmd3.Parameters.AddWithValue("@hash_token", hash);
                    cmd3.Parameters.AddWithValue("@Status", "Pending");

                    if (My.InsertUpdateData(cmd3))
                    {
                        SqlCommand cmd4;
                        string query1 = "INSERT INTO enach_registration_history (admissionserialnumber,date_time,created_by,txnId,totalamount,debitStartDate,debitEndDate,token,hash_token,Session_id) values (@admissionserialnumber,@date_time,@created_by,@txnId,@totalamount,@debitStartDate,@debitEndDate,@token,@hash_token,@Session_id)";
                        cmd4 = new SqlCommand(query1);
                        cmd4.Parameters.AddWithValue("@admissionserialnumber", consumerId);
                        cmd4.Parameters.AddWithValue("@date_time", My.getdate1());
                        cmd4.Parameters.AddWithValue("@created_by", ViewState["Userid"].ToString());
                        cmd4.Parameters.AddWithValue("@txnId", txnId);
                        cmd4.Parameters.AddWithValue("@totalamount", maxAmount);
                        cmd4.Parameters.AddWithValue("@debitStartDate", debitStartDate);
                        cmd4.Parameters.AddWithValue("@debitEndDate", debitEndDate);
                        cmd4.Parameters.AddWithValue("@token", token);
                        cmd4.Parameters.AddWithValue("@hash_token", hash);
                        cmd4.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        if (My.InsertUpdateData(cmd4))
                        {
                            send = true;
                        }
                    }



                }

                if (send == true)
                {
                    std_basic_infoS.Visible = false;
                    pnldate.Visible = false;
                    string url = "enach_api.aspx?txnId=" + txnId;
                    //                ClientScript.RegisterStartupScript(this.GetType(), "OpenAndRefresh", script);
                    //                string url = "enach_api.aspx?txnId=" + txnId;
                    //                string script = $@"
                    //<script type='text/javascript'>
                    //    window.open('{url}', '_blank');  // open in new tab
                    //    window.location.reload();        // refresh current page
                    //</script>";





                    string script = $"<script type='text/javascript'>window.open('{url}', '_blank');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "openInNewTab", script);
                }
                else
                {
                    Alertme("Something is wrong please try again", "warning");

                }

            }

        }
        static string ComputeSha512Hash(string rawData)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2")); // hex format
                return builder.ToString();
            }
        }
    }
}