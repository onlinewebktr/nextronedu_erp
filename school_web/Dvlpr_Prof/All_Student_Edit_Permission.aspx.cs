using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class All_Student_Edit_Permission : System.Web.UI.Page
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
                    code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
                    code.bind_all_ddl_with_id(ddl_CourseCat1, "Select Course_Name, course_id from Add_course_table order by Position");
                    ddl_CourseCat.SelectedValue = code.get_classsid_top1();
                    ddl_CourseCat1.SelectedValue = code.get_classsid_top1();
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                    ddl_section.Text = My.get_top_one_section();

                    BindGridView(4);
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
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
                    code.BindRepeater("select * from dbo.[admission_registor]  where Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Status='1' order by rollnumber asc", RPDetails);
                else if (p == 2)
                    code.BindRepeater("select * from dbo.[admission_registor] where Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Class_id='" + ddl_CourseCat.SelectedValue + "' and Status='1' order by rollnumber asc", RPDetails);
                else if (p == 3)
                    code.BindRepeater("select * from dbo.[admission_registor] where Transfer_Status in ('New','NT') and  StudentStatus='AV' and admissionserialnumber='" + txt_student_regid.Text + "' and Status='1' order by rollnumber asc", RPDetails);
                else if (p == 0)//All
                    code.BindRepeater("select * from dbo.[admission_registor] where Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Status='1' order by class,rollnumber", RPDetails);
                else if (p == 4)//section
                    code.BindRepeater("select * from dbo.[admission_registor] where Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' order by rollnumber", RPDetails);

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
        protected void btn_find_dtudent_regid_Click(object sender, EventArgs e)
        {
            if (txt_student_regid.Text == "")
            {

                Alert("Please enter  student admission number ");
            }
            else
            {
                BindGridView(3);
            }
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

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("lbl_iStatus")).Text == "0")
                {
 

                    ((Label)e.Item.FindControl("lbl_status")).Text = "Inactive";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-danger ml-2";
                }
                else if (((Label)e.Item.FindControl("lbl_iStatus")).Text == "")
                {
                    
                    ((Label)e.Item.FindControl("lbl_status")).Text = "Inactive";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-danger ml-2";
                }
                else
                {
                
                    
                    ((Label)e.Item.FindControl("lbl_status")).Text = "Active";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-success ml-2";
                }

                if (((Label)e.Item.FindControl("lbl_edit_Istatus")).Text == "0")
                {
                    ((Label)e.Item.FindControl("lbl_edit_status")).Text = "No";
                    ((Label)e.Item.FindControl("lbl_edit_status")).CssClass = "badge badge-danger ml-2";
                }
                else if (((Label)e.Item.FindControl("lbl_edit_Istatus")).Text == "")
                {
                    ((Label)e.Item.FindControl("lbl_edit_status")).Text = "No";
                    ((Label)e.Item.FindControl("lbl_edit_status")).CssClass = "badge badge-danger ml-2";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_edit_status")).Text = "Yes";
                    ((Label)e.Item.FindControl("lbl_edit_status")).CssClass = "badge badge-success ml-2";
                }

                if (((Label)e.Item.FindControl("lbl_verification_istatus")).Text == "0")
                {
                    ((Label)e.Item.FindControl("lbl_verification_status")).Text = "Pending";
                    ((Label)e.Item.FindControl("lbl_verification_status")).CssClass = "badge badge-danger ml-2";
                }
                else if (((Label)e.Item.FindControl("lbl_verification_istatus")).Text == "")
                {
                    ((Label)e.Item.FindControl("lbl_verification_status")).Text = "Yes";
                    ((Label)e.Item.FindControl("lbl_verification_status")).CssClass = "badge badge-success ml-2";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_verification_status")).Text = "Yes";
                    ((Label)e.Item.FindControl("lbl_verification_status")).CssClass = "badge badge-success ml-2";
                }
            }
        }

        protected void btn_active_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_iStatus = (Label)row.FindControl("lbl_iStatus");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");

                if (lbl_iStatus.Text == "0")
                {
                    string strQuery = "Update admission_registor set Status=1  where Id = @Id";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {


                    }

                }
                else
                {
                    string strQuery = "Update admission_registor set Status=0 where Id = @Id";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {

                    }






                }
                Alert("status has been changed.");
                if (ViewState["flag"].ToString() == "1")
                    BindGridView(1);
                else if (ViewState["flag"].ToString() == "2")
                    BindGridView(2);
                else if (ViewState["flag"].ToString() == "0")
                    BindGridView(2);
                else if (ViewState["flag"].ToString() == "4")
                    BindGridView(4);
                else
                    BindGridView(3);
            }
            catch { }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            BindGridView(1);
        }

        protected void lnk_edit_permission_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;


                Label lbl_Id = (Label)row.FindControl("lbl_Id");



                string strQuery = "Update admission_registor set Edit_Istatus=1  where Id = @Id";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
                if (InsertUpdate.InsertUpdateData(cmd))
                {

                }


                Alert("You have successfully granted permission to edit student information.");
                if (ViewState["flag"].ToString() == "1")
                    BindGridView(1);
                else if (ViewState["flag"].ToString() == "2")
                    BindGridView(2);
                else if (ViewState["flag"].ToString() == "0")
                    BindGridView(2);
                else if (ViewState["flag"].ToString() == "4")
                    BindGridView(4);
                else
                    BindGridView(3);
            }
            catch { }
        }

        protected void btn_Clear_Login_Click(object sender, EventArgs e)
        {
            try
            {
                send_clearlogin();
                Alert("You have successfully granted permission to edit student information.");

                if (ViewState["flag"].ToString() == "1")
                    BindGridView(1);
                else if (ViewState["flag"].ToString() == "2")
                    BindGridView(2);
                else if (ViewState["flag"].ToString() == "0")
                    BindGridView(2);
                else if (ViewState["flag"].ToString() == "4")
                    BindGridView(4);
                else
                    BindGridView(3);
            }
            catch (Exception ex)
            {
            }
        }

        private void send_clearlogin()
        {
            int i = 0;
            foreach (RepeaterItem row in RPDetails.Items)
            {
                CheckBox chk = RPDetails.Items[i].FindControl("rowChkBox") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    Label lbl_admissionserialnumber = RPDetails.Items[i].FindControl("lbl_admissionserialnumber") as Label;
                    SqlCommand cmd = new SqlCommand("Update admission_registor set Edit_Istatus=1,Verification_Istatus='1'  where admissionserialnumber ='" + lbl_admissionserialnumber.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                }
                i++;
            }
        }
    }
}