using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class report_card : System.Web.UI.Page
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
                        //Session["stdSsonForRP"] = Session["User"].ToString();
                        ViewState["IsPageFound"] = "NO";
                        get_areport_card();
                        ViewState["flag"] = "0";
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

        private void get_areport_card()
        {
            string websiteurL = getUrls();
            string IsFinalRpPublish = checkIsFinalRpPublish(ViewState["sesssionid"].ToString());

            if (IsFinalRpPublish == "YES")
            {
                get_rp_info("select DISTINCT Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ViewState["sesssionid"].ToString() + "' and t3.Admission_no='" + ViewState["regid"].ToString() + "'  UNION all select DISTINCT top 1 '" + ViewState["FinalPubDate"].ToString() + "' as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, 'FINAL' as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ViewState["sesssionid"].ToString() + "' and t3.Admission_no='" + ViewState["regid"].ToString() + "'");
            }
            else
            {
                get_rp_info("select DISTINCT Format(convert(DateTime,t3.Published_date,103), 'dd/MM/yyyy') as new_date,em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber join Exam_publish_result t3 on em.Admission_no=t3.Admission_no and em.Session_id=t3.Session_id and em.Branch_id=t3.Branch_id and em.Term=t3.Term_id where em.Session_id='" + ViewState["sesssionid"].ToString() + "'  and t3.Admission_no='" + ViewState["regid"].ToString() + "' order by ar.rollnumber asc");
            }
        }

        private string checkIsFinalRpPublish(string sessionId)
        {
            string rtN = "NO";
            ViewState["FinalPubDate"] = "NA";
            DataTable dt = My.dataTable("select * from Exam_report_card_setting where Session_id='" + sessionId + "'");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["Is_final_report_card_publish"].ToString() == "True")
                    {
                        ViewState["FinalPubDate"] = dt.Rows[0]["Final_report_card_publish_date"].ToString();
                        rtN = "YES";
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return rtN;
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


        private void get_rp_info(string query)
        {
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        private string getUrls()
        {
            string returN = "0";
            DataTable dt = My.dataTable("select URL from Global");
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0][0].ToString();
                return returN;
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_branch_id = ((Label)e.Item.FindControl("lbl_branch_id")) as Label;
                Label lbl_term_id = ((Label)e.Item.FindControl("lbl_term_id")) as Label;
                Label lbl_term_name = ((Label)e.Item.FindControl("lbl_term_name")) as Label;
                HtmlAnchor rpcard_link = (HtmlAnchor)e.Item.FindControl("rpcard_link");
                find_rp_card_page(lbl_class_id.Text, lbl_branch_id.Text);

                string websiteurL = getUrls();
                if (lbl_term_name.Text == "FINAL")
                {
                    rpcard_link.HRef = websiteurL + "Examination_Admin/slip/" + ViewState["FinalRC"].ToString() + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Term_id=" + lbl_term_id.Text + "&RequestFrom=1";
                }
                else
                {
                    rpcard_link.HRef = websiteurL + "Examination_Admin/slip/" + ViewState["SingleRC"].ToString() + "?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&ismob=1&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Term_id=" + lbl_term_id.Text + "&RequestFrom=1";
                }
            }
        }


        private void find_rp_card_page(string class_id, string branch_id)
        {
            if (ViewState["IsPageFound"].ToString() == "NO")
            {
                string querym = "select Report_card_single,Final_single from Exam_report_card_setting_classwise where  Class_id=" + class_id + " and Branch_id='" + branch_id + "'";
                DataTable dtm = My.dataTable(querym);
                if (dtm.Rows.Count > 0)
                {
                    ViewState["SingleRC"] = dtm.Rows[0]["Report_card_single"].ToString();
                    try
                    {
                        ViewState["FinalRC"] = dtm.Rows[0]["Final_single"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    ViewState["SingleRC"] = "#!";
                    ViewState["FinalRC"] = "#!";
                }
                ViewState["IsPageFound"] = "YES";
            }
        }
    }
}