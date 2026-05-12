using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class OnlineMyResult : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    Session["student"] = ViewState["regid"].ToString();
                    bind_student_class_and_section();
                    Fil_data_pageload();

                    
                }
            }

        }

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
        My mycode = new My();
        UsesCode code = new UsesCode();
        private void bind_student_class_and_section()
        {
            DataTable dt = code.FillTable("Select Class_id,Section,Branch_id,Session_id from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc  ");
            if (dt.Rows.Count == 0)
            {
                ViewState["classid"] = "0";
                ViewState["section"] = "0";
                ViewState["branchid"] = "1";
                ViewState["sessionid"] = "0";
            }
            else
            {
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                ViewState["section"] = dt.Rows[0]["Section"].ToString();
                ViewState["branchid"] = dt.Rows[0]["Branch_id"].ToString();
                ViewState["sessionid"] = dt.Rows[0]["Session_id"].ToString(); ;
            }
        }

        private void Fil_data_pageload()
        {
            string query1 = " Select ote.*,format(live_date, 'dd/MM/yyyy') as live_date1,utd.Correct_answer,utd.Answered_questions,utd.Unattempted_questions,utd.Full_Marks,utd.Obtains_Marks,(Select count(Id) from question_info where test_id=ote.Exam_id  and Uploding_status='Administrator' ) as noofquestion,utd.icreated_date,utd.Ip_address,utd.Attempt_id,(select top 1 Subject_name from Subject_Master where Subject_id=ote.subjectname) as subject_name from OlineTest_Exam_name  ote join  user_test_total_marks_details utd on ote.Exam_id=utd.Exam_code and ote.Session_id=utd.Session_Id where utd.Studentid='" + ViewState["regid"].ToString() + "' and ote.Class_Id='" + ViewState["classid"].ToString() + "' and ote.Session_id ='" + ViewState["sessionid"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(query1);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                Alertme("Sorry, there are no results", "warning");

            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();


            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_subject = (Label)e.Item.FindControl("lbl_subject");
                Label lbl_subjectname_view = (Label)e.Item.FindControl("lbl_subjectview");
                if (lbl_subject.Text == "")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else if (lbl_subject.Text == "0")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else
                {
                    lbl_subjectname_view.Text = lbl_subject.Text;
                }
                Label lbl_testid = (Label)e.Item.FindControl("lbl_testid");
                Button btn_testans = (Button)e.Item.FindControl("btn_testans");
                bool chk = My.check_question_explanation(lbl_testid.Text);
                if(chk==false)
                {
                    btn_testans.Visible = false;
                }
                else
                {
                    btn_testans.Visible = true;
                }
               




                
            }
        }

        protected void btn_view_result_Click(object sender, EventArgs e)
        {
            try
            {
                Button img = (Button)sender;
                RepeaterItem row = (RepeaterItem)img.NamingContainer;
                Label lbl_testid = (Label)row.FindControl("lbl_testid");
                Label lbl_icreated_date = (Label)row.FindControl("lbl_icreated_date");
                Label lbl_Attempt_id = (Label)row.FindControl("lbl_Attempt_id");
                // Response.Redirect("OnlineResultview.aspx?studentid="+ Uri.EscapeDataString(ViewState["regid"].ToString()) + "&testid=" + Uri.EscapeDataString(lbl_testid.Text) +"&idate=" + Uri.EscapeDataString(lbl_icreated_date.Text) + "&Attemptid=" + Uri.EscapeDataString(lbl_Attempt_id.Text) +"&mode=mobile", false);
                Session["student"] = ViewState["regid"].ToString();
                Session["testid"] = lbl_testid.Text;
                Session["idate"] = lbl_icreated_date.Text;
                Session["Attemptid"] = lbl_Attempt_id.Text;
                Session["mode"] = "mobile";

                Response.Redirect("OnlineResultview.aspx", false);

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Online My Result");
            }
        }

        protected void btn_testans_Click(object sender, EventArgs e)
        {
            Button img = (Button)sender;
            RepeaterItem row = (RepeaterItem)img.NamingContainer;
            Label lbl_testid = (Label)row.FindControl("lbl_testid");
            Label lbl_icreated_date = (Label)row.FindControl("lbl_icreated_date");
            Label lbl_Attempt_id = (Label)row.FindControl("lbl_Attempt_id");
            Session["student"] = ViewState["regid"].ToString();
            Session["testid"] = lbl_testid.Text;
            Session["idate"] = lbl_icreated_date.Text;
            Session["Attemptid"] = lbl_Attempt_id.Text;
            Session["mode"] = "mobile";
            Response.Redirect("OnlineQuestion_Explanation.aspx",false);
        }
    }
}