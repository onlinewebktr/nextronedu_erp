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
    public partial class Pending_for_dues_sync_list : System.Web.UI.Page
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

            code.bind_all_ddl_with_all(ddl_CourseCat, "Select CategoryName, CategoryID from ClassMaster order by CategoryName");


        }
        protected void btn_section_wise_Click(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
            {
                BindGridView(0);
            }
            else if (ddl_CourseCat.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
            {
                BindGridView(4);
            }
            else if (ddl_CourseCat.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
            {
                BindGridView(2);
            }
            else if (ddl_CourseCat.SelectedItem.Text == "ALL" && ddl_section.Text == "")
            {
                BindGridView(0);
            }
            else
            {
                BindGridView(2);
            }
        }
        private void BindGridView(int p)
        {
            try
            {
                ViewState["flag"] = p.ToString();

                if (p == 1)
                    BindRepeater(" Select ar.Class_id,ar.fathername, ar.studentname,ar.Section,ar.session,ar.rollnumber,ar.dateofadmission,ar.mobilenumber,ar.admissionserialnumber,ar.class from admission_registor ar where ar.admissionserialnumber not in (select   distinct admission_no from dbo.[Typewise_fee_collection] where admission_no is not null) order by ar.class,ar.Section");
                else if (p == 2)
                    BindRepeater("  Select ar.Class_id,ar.fathername, ar.studentname,ar.Section,ar.session,ar.rollnumber,ar.dateofadmission,ar.mobilenumber,ar.admissionserialnumber,ar.class from admission_registor ar where ar.admissionserialnumber not in (select   distinct admission_no from dbo.[Typewise_fee_collection] where admission_no is not null) and ar.Class_id='" + ddl_CourseCat.SelectedValue + "' order by ar.class,ar.Section");
                else if (p == 3)
                    BindRepeater("Select ar.Class_id,ar.fathername, ar.studentname,ar.Section,ar.session,ar.rollnumber,ar.dateofadmission,ar.mobilenumber,ar.admissionserialnumber,ar.class from admission_registor ar where ar.admissionserialnumber not in (select   distinct admission_no from dbo.[Typewise_fee_collection] where admission_no is not null) and ar.admissionserialnumber='" + txt_student_regid.Text + "' order by ar.class,ar.Section");
                else if (p == 0)//All
                    BindRepeater("Select ar.Class_id,ar.fathername, ar.studentname,ar.Section,ar.session,ar.rollnumber,ar.dateofadmission,ar.mobilenumber,ar.admissionserialnumber,ar.class from admission_registor ar where ar.admissionserialnumber not in (select   distinct admission_no from dbo.[Typewise_fee_collection] where admission_no is not null) and ar.Class_id='" + ddl_CourseCat.SelectedValue + "' order by ar.class,ar.Section");
                else if (p == 4)//section
                   BindRepeater(" Select ar.Class_id,ar.fathername, ar.studentname,ar.Section,ar.session,ar.rollnumber,ar.dateofadmission,ar.mobilenumber,ar.admissionserialnumber,ar.class from admission_registor ar where ar.admissionserialnumber not in (select   distinct admission_no from dbo.[Typewise_fee_collection] where admission_no is not null) and ar.Class_id='" + ddl_CourseCat.SelectedValue + "' and  ar.Section='" + ddl_section.Text + "' order by ar.class,ar.Section");

            }
            catch
            {
            }
        }

        private void BindRepeater(string query)
        {
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                lbl_total.Text = dt.Rows.Count.ToString();
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                
            }
            else
            {
                lbl_total.Text = "0";
            
                RPDetails.DataSource = null;
                RPDetails.DataBind();
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
                if (ddl_CourseCat.SelectedItem.Text == "ALL")
                {
                    ddl_section.Text = "ALL";
                    ddl_section.Enabled = false;
                }
                else
                {
                    ddl_section.Enabled = true;
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                }

                BindGridView(2);

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }





        #region find student reg id
        protected void btn_find_dtudent_regid_Click(object sender, EventArgs e)
        {
            if (txt_student_regid.Text == "")
            {

                Alert("Please enter student admission number ");
            }
            else
            {
                BindGridView(3);
            }
        }
        #endregion








        protected void Button1_Click(object sender, EventArgs e)
        {
            BindGridView(1);
        }
    }
}