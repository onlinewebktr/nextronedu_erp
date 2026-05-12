using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{

    public partial class admit_card : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();

                        ViewState["IsPageFound"] = "NO";

                        ViewState["flag"] = "0";
                        student_info(ViewState["regid"].ToString());
                        get_admit_card();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void student_info(string regid)
        {
            string query = "select top 1 Session_id,Class_id,Section,Branch_id from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV'    and Transfer_Status in ('New','NT') and Session_id='" + ViewState["sesssionid"].ToString() + "' order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                hd_session_id.Value = dt.Rows[0]["Session_id"].ToString();
                hd_class.Value = dt.Rows[0]["Class_id"].ToString();
                hd_section.Value = dt.Rows[0]["Section"].ToString();
                hd_Branch_id.Value = dt.Rows[0]["Branch_id"].ToString();
            }
        }

        private void get_admit_card()
        {
             
            string IsFinalRpPublish = checkIsFinalRpPublish();

            if (IsFinalRpPublish == "YES")
            {
                Div1.Visible = true;
                Div2.Visible = false;

                DataTable dt = mycode.FillData("select t1.*,Course_Name,(select top 1 Session from session_details where session_id=t1.Session_id) as Session,(select top 1 Term_Name from Exam_Term_Details where Session_Id=t1.Session_id and Class_id=t1.Class_id and Exam_Term_Id=t1.Term_id) as Term_name,(select top 1 Assessment_Name from Exam_Assessment_Details where Session_Id=t1.Session_id and Class_id=t1.Class_id and Exam_Term_Id=t1.Term_id and Assessment_Id=t1.Exam_id) as Assesment_name from Active_exam_setting t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ViewState["sesssionid"].ToString() + "' and t1.Class_id='" + hd_class.Value + "' and Term_id='" + ViewState["Exam_Term_Id"].ToString() + "' and Exam_id='" + ViewState["Assessment_Id"].ToString() + "' order by t2.Position asc");
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry! No admit cards have been published here.", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }

            }
            else
            {
                Div1.Visible = false;
                Div2.Visible = true;
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
        private string checkIsFinalRpPublish()
        {
            ViewState["Exam_Term_Id"] = "0";
            ViewState["Assessment_Id"] = "0";
            string rtN = "NO";
            ViewState["FinalPubDate"] = "NA";
            DataTable dt = My.dataTable("select top 1 * from Active_exam_setting where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + hd_class.Value + "' order by id desc");
            if (dt.Rows.Count == 0)
            {
                rtN = "NO";

            }
            else
            {
                ViewState["Exam_Term_Id"] = dt.Rows[0]["Term_id"].ToString();
                ViewState["Assessment_Id"] = dt.Rows[0]["Exam_id"].ToString();

                rtN = "YES";
            }
            return rtN;
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_Exam_id = ((Label)e.Item.FindControl("lbl_Exam_id")) as Label;//assessment
                Label lbl_term_id = ((Label)e.Item.FindControl("lbl_term_id")) as Label;
                HtmlAnchor rpcard_link = (HtmlAnchor)e.Item.FindControl("rpcard_link");


                ViewState["ShiftType"] = get_shift();
                string websiteurL = My.url();

                if (ViewState["ShiftType"].ToString() == "2")
                {
                    rpcard_link.HRef = websiteurL + "Examination_Admin/slip/print-termwise-admit-card-double-shift.aspx?session_Id=" + hd_session_id.Value + "&branch_id=" + hd_Branch_id.Value + "&classid=" + hd_class.Value + "&section=" + hd_section.Value + "&examterm=" + ViewState["Exam_Term_Id"].ToString() + "&admin=" + ViewState["regid"].ToString() + "&examid=" + ViewState["Assessment_Id"].ToString() + "&from=stdwise";
                }
                else
                {
                    rpcard_link.HRef = websiteurL + "Examination_Admin/slip/print-termwise-admit-card.aspx?session_Id=" + hd_session_id.Value + "&branch_id=" + hd_Branch_id.Value + "&classid=" + hd_class.Value + "&section=" + hd_section.Value + "&examterm=" + ViewState["Exam_Term_Id"].ToString() + "&admin=" + ViewState["regid"].ToString() + "&examid=" + ViewState["Assessment_Id"].ToString() + "&from=stdwise";
                }





                
            }
        }

        private object get_shift()
        {
            string query = "select  Shift_type from Exam_Time_Table where Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_id='1' and Class_id='" + hd_class.Value + "' and Exam_Term_Id='" + ViewState["Exam_Term_Id"].ToString() + "' and Section='" + hd_section.Value + "' and  Exam_id='" + ViewState["Assessment_Id"].ToString() + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "NO";

            }
            else
            {
                return dt.Rows[0]["Shift_type"].ToString();
            }
        }
    }
}