using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Student_Login_Status_Clear : System.Web.UI.Page
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
                    BindGridView(1);
                };
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        private void BindCourse()
        {
            code.bind_all_ddl_with_id(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
           

        }
        private void BindGridView(int p)
        {
            try
            {
                ViewState["flag"] = p.ToString();

                if (p == 1)
                    code.BindRepeater("select * from dbo.[admission_registor] where Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Status='1' and   Device_Id!='' order by rollnumber asc", RPDetails);
                else if (p == 2)
                    code.BindRepeater("select * from dbo.[admission_registor] where Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Status='1' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Device_Id!=''  order by rollnumber asc", RPDetails);
                else if (p == 3)
                    code.BindRepeater("select * from dbo.[admission_registor] where Session_id=" + ViewState["sesssionid"].ToString() + " and Transfer_Status in ('New','NT') and  StudentStatus='AV' and Status='1' and admissionserialnumber='" + txt_student_regid.Text + "' and Device_Id!='' order by rollnumber asc", RPDetails);

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
        protected void lnk_Clear_login_Click(object sender, EventArgs e)
        {

            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Update admission_registor set Device_Id=null where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                if (ViewState["flag"].ToString() == "1")
                    BindGridView(1);
                else if (ViewState["flag"].ToString() == "2")
                    BindGridView(2);
                else
                    BindGridView(3);
                Alert("successfully cleared login.");

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }





        #region find student reg id
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


                    dt = code.FillTable("select * from dbo.[admission_registor] where   Status='1' order by rollnumber asc");
                    export_to_excel(dt, "Student_list");
                }
                else if (ViewState["flag"].ToString() == "2")
                {


                    dt = code.FillTable("select * from dbo.[admission_registor] where Class_id='" + ddl_CourseCat.SelectedValue + "' and Status='1' order by rollnumber asc");
                    export_to_excel(dt, "Student_list");
                }

                else if (ViewState["flag"].ToString() == "3")
                {
                    dt = code.FillTable("select * from dbo.[admission_registor] where admissionserialnumber='" + txt_student_regid.Text + "' and Status='1' order by rollnumber asc");
                    export_to_excel(dt, "Student_list");
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

                if (((Label)e.Item.FindControl("lbl_iStatus")).Text == "0")
                {

                    string yourHTMLstring = @"<i class='dropdown-icon lnr lnr-rocket' style='color: green;'></i><span color: green;>Active</span>";


                    ((LinkButton)e.Item.FindControl("btn_active")).Text = "Active";
                    ((LinkButton)e.Item.FindControl("btn_active")).Controls.Add(new LiteralControl(yourHTMLstring));

                    ((Label)e.Item.FindControl("lbl_status")).Text = "Inactive";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-danger ml-2";
                }
                else
                {
                    string yourHTMLstring = @"<i class='dropdown-icon lnr lnr-rocket' style='color: red;'></i><span style='color: red;'>Inactive</span>";
                    ((LinkButton)e.Item.FindControl("btn_active")).Text = "Inactive";
                    ((LinkButton)e.Item.FindControl("btn_active")).Controls.Add(new LiteralControl(yourHTMLstring));
                    ((Label)e.Item.FindControl("lbl_status")).Text = "Active";
                    ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-success ml-2";
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
                Alert("successfully cleared login.");
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
                    SqlCommand cmd = new SqlCommand("Update admission_registor set Device_Id=null where admissionserialnumber='" + lbl_admissionserialnumber.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);



                }
                i++;
            }
            if (ViewState["flag"].ToString() == "1")
                BindGridView(1);
            else if (ViewState["flag"].ToString() == "2")
                BindGridView(2);
            else
                BindGridView(3);
            Alert("successfully cleared login.");
        }


    }
}