using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Send_message_to_teacher : System.Web.UI.Page
    {
        Message_sending sendMessage = new Message_sending();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_gridvew("select * from user_details where (User_Type='Teacher')  order by Id Desc");
                    ViewState["flag"] = "0";
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private void bind_gridvew(string qry)
        {
            code.BindRepeater(qry, RPDetails);

        }

        protected void btn_send_message_Click(object sender, EventArgs e)
        {
            try
            {
                send_message_to_teacher();
                Alert("User id & password successfully sent to teacher.");
            }
            catch (Exception ex)
            {
            }
        }
        My mycode = new My();
        private void send_message_to_teacher()
        {
            string playstorelink = geturl();
            string regrads = mycode.get_firm_name();
           
            int i = 0;
            foreach (RepeaterItem row in RPDetails.Items)
            {
                CheckBox chk = RPDetails.Items[i].FindControl("rowChkBox") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    Label lbl_Name = RPDetails.Items[i].FindControl("lbl_Name") as Label;
                    Label lbl_UserID = RPDetails.Items[i].FindControl("lbl_UserID") as Label;
                    Label lbl_Password = RPDetails.Items[i].FindControl("lbl_Password") as Label;
                    Label lbl_PhoneNo = RPDetails.Items[i].FindControl("lbl_PhoneNo") as Label;

                    string msg = "Dear Teacher your User id :-" + lbl_UserID.Text + " and Password :- " + lbl_Password.Text + ". Download app from given link:- " + playstorelink + " Regards " +regrads ;  
                  

                  sendMessage.send_sms_singl(lbl_PhoneNo.Text, msg, lbl_UserID.Text);
                }
                i++;
            }
        }
        private string getregrds()
        {
            string query = "Select * from Firm_Details  ";
            DataTable dt1 = code.FillTable(query);
            if (dt1.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt1.Rows[0]["firm_name"].ToString();
            }
        }

        private string geturl()
        {
            string query = "Select * from Update_details  ";
            DataTable dt1 = code.FillTable(query);
            if (dt1.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt1.Rows[0]["Update_new_Url"].ToString();
            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_find_by_teacher_name_Click(object sender, EventArgs e)
        {
            try
            {
                bind_gridvew("select *   from Istatus where name like '%" + txt_teacher_name.Text + "%' and (User_Type='Teacher') and Istatus='1' order by Id Desc");
                ViewState["flag"] = "1";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_find_by_mobile_Click(object sender, EventArgs e)
        {
            try
            {
                bind_gridvew("select *  from user_details where mobile like '%" + txt_teacher_mobile.Text + "%' and (User_Type='Teacher') and Istatus='1'    order by Id Desc");
                ViewState["flag"] = "2";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_show_all_Click(object sender, EventArgs e)
        {
            try
            {
                bind_gridvew("select *   from user_details where   (User_Type='Teacher') and Istatus='1' order by Id Desc");
                ViewState["flag"] = "0";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
    }
}