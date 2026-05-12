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
    public partial class App_Install_Reportaspx : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    BindCourse();
                    ddl_CourseCat.SelectedValue = code.get_classsid_top1();
                    BindGridView(1);
                };
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        private void BindCourse()
        {
            code.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
            code.bind_all_ddl_with_id(ddl_CourseCat1, "Select Course_Name, course_id from Add_course_table order by Position");

        }
        private void BindGridView(int p)
        {
            try
            {
                ViewState["flag"] = p.ToString();

                if (p == 1)
                    code.BindRepeater("select * from dbo.[admission_registor]  where  Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and Status='1' and gcm_id is null order by rollnumber asc", RPDetails);
                else if (p == 2)
                    code.BindRepeater("select * from dbo.[admission_registor] where Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and Status='1' and Class_id='" + ddl_CourseCat.SelectedValue + "' and gcm_id is not null order by rollnumber asc", RPDetails);
                else if (p == 3)
                    code.BindRepeater("select * from dbo.[admission_registor] where Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and Status='1' and Class_id='" + ddl_CourseCat.SelectedValue + "' and gcm_id is not null order by rollnumber asc", RPDetails);
                else if (p == 4)
                    code.BindRepeater("select * from dbo.[admission_registor] where Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and Status='1' and Class_id='" + ddl_CourseCat.SelectedValue + "' and gcm_id is null order by rollnumber asc", RPDetails);

            }
            catch
            {
            }
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







        #region find student reg id
        protected void btn_find_dtudent_regid_Click(object sender, EventArgs e)
        {
            if (ddl_downloadstatus.Text == "Select")
            {
                Alert("Please select status");
            }
             
            else
            {
                if (ddl_downloadstatus.Text == "Yes")
                {

                    BindGridView(3);//yes
                }
                else if (ddl_downloadstatus.Text == "No")
                {
                    BindGridView(4);//no
                }
                else
                {
                    BindGridView(2);//all
                }
            }

        }
        #endregion

        #region export excel

        protected void img_expor_excel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt;
                string date = DateTime.UtcNow.AddHours(5.0).AddMinutes(30.0).ToString("dd/MM/yyyy");


                if (ViewState["flag"].ToString() == "1")
                {


                    dt = code.FillTable("select * from dbo.[admission_registor] where   Status='1' order by Original_Name asc");
                    export_to_excel(dt, "Student_list");
                }
                else if (ViewState["flag"].ToString() == "2")
                {


                    dt = code.FillTable("select * from dbo.[admission_registor] where Class_id='" + ddl_CourseCat.SelectedValue + "' and Status='1' order by Original_Name Desc");
                    export_to_excel(dt, "Student_list");
                }

                else if (ViewState["flag"].ToString() == "3")
                {

                }




            }
            catch (Exception ex)
            {

            }
        }
        private void export_to_excel(DataTable dt, string file)
        {

            string FileName = file + DateTime.Now + ".xls";

            string attachment = "attachment; filename=" + FileName;

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/vnd.ms-excel";

            Response.ContentEncoding = System.Text.Encoding.Unicode;

            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {

                Response.Write(tab + dc.ColumnName);

                tab = "\t";

            }

            Response.Write("\n");

            int i;

            foreach (DataRow dr in dt.Rows)
            {

                tab = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {

                    Response.Write(tab + dr[i].ToString());

                    tab = "\t";

                }

                Response.Write("\n");

            }

            Response.End();

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("lbl_iStatus")).Text == "")
                {



                    ((Label)e.Item.FindControl("lbl_status")).Text = "No";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-danger ml-2";
                }
                else
                {

                    ((Label)e.Item.FindControl("lbl_status")).Text = "Yes";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-success ml-2";
                }

            }
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
        Message_sending sendMessage = new Message_sending();
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
                    Label lbl_UserID = RPDetails.Items[i].FindControl("lbl_admissionserialnumber") as Label;
                    Label lbl_Password = RPDetails.Items[i].FindControl("lbl_Password") as Label;
                    Label lbl_PhoneNo = RPDetails.Items[i].FindControl("lbl_mobile") as Label;

                   
                 //  string msg + "= "Dear student, " + lbl_Name.Text + " kindly download the School app from Google Play store. https://play.google.com/store/apps/details?id=com.intspvt.stmarymarymalda Your User id is " + lbl_UserID.Text  and your password is " + lbl_Password.Text + ". Regards: ST. MARY'S SCHOOL";

                    string[] stringSeparators = new string[] { "#" };
                    string[] arr = get_sms_templat.Split(stringSeparators, StringSplitOptions.None);
                    string studentname = arr[0];
                    string userid = arr[1];
                    string password = arr[2];
                    string regrads = arr[3];
                    string msg = studentname + lbl_Name.Text + userid + lbl_UserID.Text + password + lbl_Password.Text + regrads;

                   // sendMessage.send_sms_singl(lbl_PhoneNo.Text, msg, lbl_UserID.Text);
                }
                i++;
            }
        }
    }
}