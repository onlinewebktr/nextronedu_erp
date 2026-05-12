using Newtonsoft.Json.Linq;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Whatsapp_API_Setting : System.Web.UI.Page
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
                        string pagename_current = "SMS_Template_Setting.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        bind_previous_settion();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "SMS_Template_Setting");
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

        private void bind_previous_settion()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Whatsapp_api_config  ", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_url.Text = dr["url"].ToString();
                    txt_SMS_api.Text = dr["SMS_API"].ToString();
                    txt_balance_url.Text = dr["balanceapi"].ToString();
                    txt_scanqrcode.Text = dr["scanqrcode"].ToString();
                    My.Whatsapp_api_url = txt_url.Text;
                    My.whatsapp_mobile_no = txt_SMS_api.Text;

                   // string json = My.exeUrl(dr["scanqrcode"].ToString());
                  //  System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    //createDiv.ID = "createDiv";
                    //createDiv.InnerHtml = json;
                   // Panel1.Controls.Add(createDiv);
                   if(dr["Status"].ToString()== "running")
                    {
                        Panel1.Visible = true;
                        check_balance();
                    }
                   else
                    {
                        Panel1.Visible = false;
                    }

                   

                }
            }
            else
            {
                lbl_balance.Text = "API failed";
                lblsmsmsg.Text = "❌ Your WhatsApp is not connected.";
            }

        }

        private void check_balance()
        {


           // string json = My.exeUrl(txt_balance_url.Text);
            //JObject jar = (JObject)JsonConvert.DeserializeObject(json);
            //lbl_balance.Text = json.Split('|')[1].ToString(); //jar["wallet"].ToString();

            try
            {
                string authKey = txt_SMS_api.Text;
                string url = $"https://console.authkey.io/restapi/getbalance.php?authkey={authKey}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        string json = sr.ReadToEnd();

                        JObject obj = JObject.Parse(json);
                        bool success = obj["success"].Value<bool>();

                        if (success)
                        {
                            string currency = obj["currency"].ToString();
                            string balance = obj["balance"].ToString();
                            string email = obj["email"].ToString();
                            lbl_balance.Text = currency + " " + balance+" "+ email;
                            lblsmsmsg.Text = "✅ Your WhatsApp is connected and working perfectly.";
                        }
                        else
                        {
                            lbl_balance.Text = "API failed";
                            lblsmsmsg.Text = "❌ Your WhatsApp is not connected.";
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                lbl_balance.Text = "HTTP Error: " + webEx.Message;
                lblsmsmsg.Text = "❌ Your WhatsApp is not connected.";
            }
            catch (Exception ex)
            {
                lbl_balance.Text = "Error: " + ex.Message;
                lblsmsmsg.Text = "❌ Your WhatsApp is not connected.";
            }




        }


        protected void btn_save_sms_setting_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_url.Text == "")
                {
                    Alertme("Please enter URL", "warning");
                    txt_url.Focus();
                    return;
                }

                if (!txt_url.Text.StartsWith("http"))
                {
                    Alertme("http url must start with http.", "warning");
                    txt_url.Focus();
                    return;
                }
                //if (!txt_SMS_api.Text.Contains("{0}"))
                //{
                //    Alertme("Please use {0} in the place of  mobile no");
                //    txt_SMS_api.Focus();
                //    return;
                //}
                //if (!txt_SMS_api.Text.Contains("{1}"))
                //{
                //    Alertme("Please use {1} in the place of  message parameter");
                //    txt_SMS_api.Focus();
                //    return;
                //}

                save_sms_setting_details();
                bind_previous_settion();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void save_sms_setting_details()
        {
            SqlCommand cmd = new SqlCommand("sp_Whatsapp_api_config");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@SMS_API", txt_SMS_api.Text);

            cmd.Parameters.AddWithValue("@url", txt_url.Text);
            cmd.Parameters.AddWithValue("@Status", "RUNNING");
            cmd.Parameters.AddWithValue("@balanceapi", txt_balance_url.Text);
            cmd.Parameters.AddWithValue("@scanqrcode", txt_scanqrcode.Text);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                Alertme("Record Updated successfully", "success");

            }


        }
        protected void btn_reload_smms_balance_Click(object sender, EventArgs e)
        {
            try
            {
                check_balance();
            }
            catch (Exception ex)
            {
            }
        }

 

        protected void btn_save_sms_format_Click(object sender, EventArgs e)
        {
            try
            {
                if (dd_sms_format.Text == "Select")
                {
                    Alertme("Please select SMS Format Name", "warning");
                    return;
                }

                save_sms_format();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private void save_sms_format()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_SMS_format_details");
            cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
            cmd.Parameters.AddWithValue("@sms_format_name", dd_sms_format.Text);
            cmd.Parameters.AddWithValue("@button_name", "Save");
            cmd.Parameters.AddWithValue("@sms_formate", txt_message.Text);
            if (chk_sms_enable.Checked == true)
                cmd.Parameters.AddWithValue("@is_enable", 1);
            else
                cmd.Parameters.AddWithValue("@is_enable", 0);
            if (chk_send_to_admin.Checked == true)
                cmd.Parameters.AddWithValue("@Is_send_to_admin", 1);
            else
                cmd.Parameters.AddWithValue("@Is_send_to_admin", 0);

            cmd.Parameters.AddWithValue(@"message_id", "0");
            cmd.Parameters.AddWithValue(@"message_for", "WHATSAPP");
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                Alertme("Record Updated successfully", "success");
                fetch_sms_format();
            }
        }

        protected void dd_sms_format_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_sms_format();
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }

        }

        private void fetch_sms_format()
        {
            SqlCommand cmd = new SqlCommand("sp_HMS_SMS_format_details");
            cmd.Parameters.AddWithValue("@sp_status ", "Fetch1");
            cmd.Parameters.AddWithValue("@sms_format_name", dd_sms_format.Text);
            cmd.Parameters.AddWithValue("@button_name", "Save");
            cmd.Parameters.AddWithValue(@"message_for", "WHATSAPP");
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //txt_message_id.Text = dr["message_id"].ToString();
                    txt_message.Text = dr["sms_formate"].ToString();
                    if (dr["is_enable"].ToString() == "True")
                        chk_sms_enable.Checked = true;
                    else
                        chk_sms_enable.Checked = false;

                    if (dr["Is_send_to_admin"].ToString() == "True")
                        chk_send_to_admin.Checked = true;
                    else
                        chk_send_to_admin.Checked = false;

                }
            }
            else
            {
                //txt_message_id.Text = "";
                txt_message.Text = "";
            }
        }

    }
}