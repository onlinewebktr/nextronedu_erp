using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Send_Message_to_student : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { if (!IsPostBack) { BindCourse(); BindGridView(1); };}
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        private void BindCourse()
        {
            code.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");

        }
        private void BindGridView(int p)
        {
            if (p == 1)
                code.BindRepeater("select * from dbo.[admission_registor] order by Original_Name asc", RPDetails);
            else if (p == 2)
                code.BindRepeater("select * from dbo.[admission_registor] where Class_id='" + ddl_CourseCat.SelectedValue + "' order by Original_Name Desc", RPDetails);
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                BindGridView(2);

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_message.Text == "")
                {
                    Alert("Please write your message.");
                    txt_message.Text = ""; return;


                }
                else if (rd_english.Checked == false && rd_hndi.Checked == false)
                {
                    Alert("Please check message type");
                }
                else
                {
                    Send_Message();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void Send_Message()
        {
            string T = txt_message.Text;

            System.Threading.Thread.Sleep(100);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                int growcount = RPDetails.Items.Count;
                int k = 0;
                for (int i = 0; i < growcount; i++)
                {
                    CheckBox chk = (CheckBox)RPDetails.Items[i].FindControl("rowChkBox");
                    if (chk.Checked == true)
                    {

                        Label lbl_name = (Label)RPDetails.Items[i].FindControl("lbl_studentname");
                        Label lbl_mobile = (Label)RPDetails.Items[i].FindControl("lbl_mobile");
                        string name = lbl_name.Text;
                        string mobileno = lbl_mobile.Text;
                        string message = txt_message.Text;
                        if (rd_english.Checked == true)
                        {
                            SendMsg(mobileno, message, name);
                        }
                        else
                        {
                            SendMsg_hindi(mobileno, message, name);
                        }
                        Alert("Message send successfully");


                    }
                    else
                    {
                        k++;
                    }
                }

                if (k == growcount)
                {
                    Alert("Please check minimum one member in member list.");

                }
                else
                {

                }
                scope.Complete();
                txt_message.Text = "";
            }



        }

        Message_sending sms = new Message_sending();
        private void SendMsg(string mobileno, string message, string name)
        {
            sms.send_sms_group(mobileno, message, "individual", "Admin");
        }
        private void SendMsg_hindi(string mobileno, string message, string name)
        {

            sms.SendMsg_hindi(mobileno, message, "individual", "Admin");
        }

    }
}