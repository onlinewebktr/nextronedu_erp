using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Send_userid_password_to_student : System.Web.UI.Page
    {
        Message_sending sendMessage = new Message_sending();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    code.bind_all_ddl_with_id(ddl_find_class, "Select Course_Name, course_id from Add_course_table order by Position asc");
                    bind_gridvew("select * from dbo.[admission_registor] where Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Status='1' order by studentname asc");
                    ViewState["flag"] = "0";
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private void bind_gridvew(string qry)
        {
            code.BindRepeater(qry, RPDetails);
            lbl_total_student.Text = "Total no. of Students :- " + RPDetails.Items.Count;

        }

        protected void btn_send_message_Click(object sender, EventArgs e)
        {
            try
            {
                send_message_to_teacher();
                Alert("User id & password successfully sent to student.");
            }
            catch (Exception ex)
            {
            }
        }

        private void send_message_to_teacher()
        {
            string get_sms_templat = code.get_student_send_password();
            int i = 0;
            foreach (RepeaterItem row in RPDetails.Items)
            {
                CheckBox chk = RPDetails.Items[i].FindControl("rowChkBox") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    Label lbl_Name = RPDetails.Items[i].FindControl("lbl_studentname") as Label;
                    Label lbl_UserID = RPDetails.Items[i].FindControl("lbl_userid") as Label;
                    Label lbl_Password = RPDetails.Items[i].FindControl("lbl_password") as Label;
                    Label lbl_PhoneNo = RPDetails.Items[i].FindControl("lbl_mobile") as Label;

                    //string msg = "Dear student, " + lbl_Name.Text + " your user id is " + lbl_UserID.Text + " & your password is " + lbl_Password.Text + ". School upgraded mobile app is live on google Play store. Kindly click on given link and download app.";

                    //string msg = "Dear student, " + lbl_Name.Text + " kindly download the School app from Google Play store. https://play.google.com/store/apps/details?id=com.intspvt.stmarymarymalda Your User id is " + lbl_UserID.Text + " and your password is " + lbl_Password.Text + ". Regards: ST. MARY'S SCHOOL";
                    //string msg = "Dear Student/Parent, your User id :-" + lbl_UserID.Text + " and  Password :- " + lbl_Password.Text + ".Download app from given link tinyurl.com/44vv83sc  or  tinyurl.com/y988krbn Reagrds BD Academy";
                    //string[] stringSeparators = new string[] { "#" };
                    //string[] arr = get_sms_templat.Split(stringSeparators, StringSplitOptions.None);
                    //string studentname = arr[0];
                    //string userid = arr[1];
                    //string password = arr[2];
                    //string regrads = arr[3];
                   // string msg = studentname + lbl_Name.Text + userid + lbl_UserID.Text + password + lbl_Password.Text + regrads;





                 //   sendMessage.send_sms_singl(lbl_PhoneNo.Text, msg, lbl_UserID.Text);
                }
                i++;
            }
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_fnd_by_student_name_Click(object sender, EventArgs e)
        {
            try
            {
                bind_gridvew("select * from dbo.[admission_registor] where Original_Name like '%" + tt_student_name.Text + "%' and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Status='1' order by rollnumber asc");
                ViewState["flag"] = "1";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_find_by_mobile_Click(object sender, EventArgs e)
        {
            try
            {
                bind_gridvew("select * from dbo.[admission_registor] where mobilenumber like '%" + txt_student_mobile.Text + "%' and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Status='1' order by rollnumber asc");
                ViewState["flag"] = "2";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_show_all_Click(object sender, EventArgs e)
        {
            try
            {
                bind_gridvew("select * from dbo.[admission_registor] order by Original_Name asc");
                ViewState["flag"] = "0";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void ddl_find_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                code.bind_ddl_all(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_find_class.SelectedValue + "'   order by section");
                bind_gridvew("select * from dbo.[admission_registor] where Class_id='" + ddl_find_class.SelectedValue + "' and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Status='1' order by rollnumber asc");
                ViewState["flag"] = "3";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_find_class.SelectedItem.Text == "Select")
            {
            }
            else
            {
                if (ddl_section.Text == "All")
                {
                    bind_gridvew("select * from dbo.[admission_registor] where Class_id='" + ddl_find_class.SelectedValue + "' and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Status='1'  order by rollnumber asc");
                    ViewState["flag"] = "3";
                }
                else
                {
                    bind_gridvew("select * from dbo.[admission_registor] where Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Class_id='" + ddl_find_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' order by rollnumber asc");
                    ViewState["flag"] = "4";
                }
            }
        }
    }
}