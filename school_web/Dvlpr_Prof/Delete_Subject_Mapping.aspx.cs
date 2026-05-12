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
    public partial class Delete_Subject_Mapping : System.Web.UI.Page
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

                    ViewState["branchid"] = "1";
                    ViewState["sessionid"] = My.get_session_id();
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
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ViewState["sessionid"].ToString() + "' and StudentStatus='AV'  and  Status='1'  and Branch_id='" + ViewState["branchid"].ToString() + "'   ";
                find_details(query);


            }

        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        My mycode = new My();
        private void find_details(string query)
        {
            ViewState["query"] = query;
            pnl_payment_history.Visible = false;

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {


                Alert("Sorry your enterd admission no. is wrong");
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                pnl_payment_history.Visible = true;
                lbl_msg.Text = "";

                string Class_id = dt.Rows[0]["Class_id"].ToString();
                string Section = dt.Rows[0]["Section"].ToString();
                Bind_subject_data(Class_id, Section);

            }
        }

        private void Bind_subject_data(string class_id, string Section)
        {
            string query = "Select sm.Subject_name,sm.Id,smn.Class_id,smn.Section,smn.Sub_id,smn.Session_id from Subject_Master sm join Subject_Mapping_New smn on smn.Class_id=sm.course_id and smn.Sub_id=sm.Subject_id  where  smn.Section='" + Section + "' and smn.Admission_no='" + txt_admission_no.Text + "' and smn.Class_id=" + class_id + " order by sm.Subject_position ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                // Alertme("There are no payment history found", "warning");
                Alert("Sorry your enter admission no. is not mapped any subject");
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                grd_fee.DataSource = dt;
                grd_fee.DataBind();

            }
        }
        #endregion


        protected void btn_Subject_Click(object sender, EventArgs e)
        {

            try
            {
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Section = (Label)row.FindControl("lbl_Section");
                Label lbl_Sub_id = (Label)row.FindControl("lbl_Sub_id");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                mycode.executequery("delete from Exam_marks where Session_id=" + lbl_Session_id.Text + " and Class_id=" + lbl_Class_id.Text + " and Section='" + lbl_Section.Text + "' and Subject='" + lbl_Sub_id.Text + "' and Admission_no='" + txt_admission_no.Text + "'");
                mycode.executequery("delete from Subject_Mapping_New where Session_id=" + lbl_Session_id.Text + " and Class_id=" + lbl_Class_id.Text + " and Section='" + lbl_Section.Text + "' and Sub_id='" + lbl_Sub_id.Text + "' and Admission_no='" + txt_admission_no.Text + "'");
                Alert("Student subject deleted successfully");

                find_details(ViewState["query"].ToString());
            }
            catch
            {

            }

        }
    }
}