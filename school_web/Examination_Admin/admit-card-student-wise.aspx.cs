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

namespace school_web.Examination_Admin
{
    public partial class admit_card_student_wise : System.Web.UI.Page
    {
        Examination em = new Examination();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    ViewState["FirmID"] = My.get_firm_id();
                    Session["userType"] = "Admin";


                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    //Bind_all_data();
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


        private void Bind_all_data()
        {
            string query = "select distinct et.Shift_type,et.Branch_id,et.Class_id,et.Section,et.Session_Id,et.Exam_Term_Id,(select top 1 Term_Name from Exam_Term_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id) as examtermname,(select top 1 Assessment_Name from Exam_Assessment_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id and Session_Id=et.Session_Id and Assessment_Id=et.Exam_id) as exam_name,et.Exam_id,ad.Course_Name classname,format(Created_datetime, 'dd/MM/yyyy') as Created_datetime1,ad.Position,'0'  as admissionnumber  from dbo.[Exam_Time_Table] et join Add_course_table ad on ad.course_id=et.Class_id  where et.Session_Id=" + ddlsession.SelectedValue + " and et.Branch_id='" + ViewState["branchid"].ToString() + "' order by ad.Position,et.Section ";
            bind_grid_data(query);
        }

        private void bind_grid_data(string query)
        {
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                btn_print_all.Visible = false;
                Alertme("Sorry admit not created for this section.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_print_all.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_exam_term, "Select Term_Name,Exam_Term_Id from Exam_Term_Details  where Class_id=" + ddl_class.SelectedValue + " and Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["branchid"].ToString() + "' order by Sequence_No asc");
                mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Class_Id=" + ddl_class.SelectedValue + "  and Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_exam_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else
            {
                string query = "";
                if (chk_days.Checked == true && chk_hostel.Checked == false)  //Days
                {
                    query = "select *,(select top 1 Shift_type from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and  Exam_id='" + ddl_exam.SelectedValue + "') as Shift_type from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and hosteltaken='No' and Section in(select Section from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and  Exam_id='" + ddl_exam.SelectedValue + "') order by rollnumber asc";
                }
                else if (chk_days.Checked == false && chk_hostel.Checked == true)  //Hostel
                {
                    query = "select *,(select top 1 Shift_type from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and  Exam_id='" + ddl_exam.SelectedValue + "') as Shift_type from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and hosteltaken='Yes' and Section in(select Section from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and  Exam_id='" + ddl_exam.SelectedValue + "') order by rollnumber asc";
                }
                else
                {
                    query = "select *,(select top 1 Shift_type from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and  Exam_id='" + ddl_exam.SelectedValue + "') as Shift_type from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and Section in(select Section from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and  Exam_id='" + ddl_exam.SelectedValue + "') order by rollnumber asc";
                }

                bind_grid_data(query);
            }
        }

        protected void ddl_exam_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_exam, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Sequence_No asc");
            }
            catch (Exception ex)
            {
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trR");
                Label lbl_Session_Id = (Label)e.Item.FindControl("lbl_Session_Id");
                Label lbl_Branch_id = (Label)e.Item.FindControl("lbl_Branch_id");
                Label lbl_Class_id = (Label)e.Item.FindControl("lbl_Class_id");
                Label lbl_shift_type = (Label)e.Item.FindControl("lbl_shift_type");
                Label lbl_admission_no = (Label)e.Item.FindControl("lbl_admission_no");
                HtmlAnchor admitcardLink = (HtmlAnchor)e.Item.FindControl("admitcardLink");
                ViewState["ShiftType"] = lbl_shift_type.Text;

                if (lbl_shift_type.Text == "2")
                {
                    if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    {
                        admitcardLink.HRef = "slip/print-admit-card-double-shift-a5.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    }
                    else if (ddl_page.Text == "5")
                    {
                        admitcardLink.HRef = "slip/print-admit-d-shift-portrait.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    }
                    else
                    {
                        admitcardLink.HRef = "slip/print-termwise-admit-card-double-shift.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    }
                }
                else if (lbl_shift_type.Text == "3")
                {
                    if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    {
                        admitcardLink.HRef = "slip/print-admit-card-three-shift-a5.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    }
                    else
                    {
                        admitcardLink.HRef = "slip/print-admit-card-three-shift.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    }
                }
                else
                {
                    if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    {
                        admitcardLink.HRef = "slip/print-admit-card-a5.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    }
                    else
                    {
                        admitcardLink.HRef = "slip/print-termwise-admit-card.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    }
                }
            }
        }




        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                int isStdChecked = 0;
                string adm_ids = "";
                int growcount = rd_view.Items.Count;
                int k = 0;
                for (int i = 0; i < growcount; i++)
                {
                    CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                    if (chk.Checked == true)
                    {
                        Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                        adm_ids = adm_ids += lbl_id.Text + ",";
                        isStdChecked = 1;
                    }
                    else
                    {
                        k++;
                    }
                }

                if (isStdChecked == 1)
                {
                    //if (k == growcount)
                    //{

                    //    if (ViewState["ShiftType"].ToString() == "2")
                    //    {
                    //        if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    //        {
                    //            string reslink = "slip/print-admit-card-double-shift-a5.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=0&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    //            Response.Redirect(reslink, false);
                    //        }
                    //        else
                    //        {
                    //            string reslink = "slip/print-termwise-admit-card-double-shift.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=0&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    //            Response.Redirect(reslink, false);
                    //        }
                    //    }
                    //    else if (ViewState["ShiftType"].ToString() == "3")
                    //    {
                    //        if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    //        {
                    //            string reslink = "slip/print-admit-card-three-shift-a5.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=0&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    //            Response.Redirect(reslink, false);
                    //        }
                    //        else
                    //        {
                    //            string reslink = "slip/print-admit-card-three-shift.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=0&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    //            Response.Redirect(reslink, false);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    //        {
                    //            string reslink = "slip/print-admit-card-a5.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=0&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    //            Response.Redirect(reslink, false);
                    //        }
                    //        else
                    //        {
                    //            string reslink = "slip/print-termwise-admit-card.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=0&examid=" + ddl_exam.SelectedValue + "&from=stdwises&page=" + ddl_page.Text;
                    //            Response.Redirect(reslink, false);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    if (ViewState["ShiftType"].ToString() == "2")
                    {
                        if (ddl_page.Text == "2" || ddl_page.Text == "4")
                        {
                            string reslink = "slip/print-admit-card-double-shift-a5.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1&page=" + ddl_page.Text;
                            Response.Redirect(reslink, false);
                        }
                        else if (ddl_page.Text == "5")
                        {
                            string reslink = "slip/print-admit-d-shift-portrait.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1&page=" + ddl_page.Text;
                            Response.Redirect(reslink, false);
                        }
                        else
                        {
                            string reslink = "slip/print-termwise-admit-card-double-shift.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1&page=" + ddl_page.Text;
                            Response.Redirect(reslink, false);
                        }
                    }
                    else if (ViewState["ShiftType"].ToString() == "3")
                    {
                        if (ddl_page.Text == "2" || ddl_page.Text == "4")
                        {
                            string reslink = "slip/print-admit-card-three-shift-a5.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1&page=" + ddl_page.Text;
                            Response.Redirect(reslink, false);
                        }
                        else
                        {
                            string reslink = "slip/print-admit-card-three-shift.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1&page=" + ddl_page.Text;
                            Response.Redirect(reslink, false);
                        }
                    }
                    else
                    {
                        if (ddl_page.Text == "2" || ddl_page.Text == "4")
                        {
                            string reslink = "slip/print-admit-card-a5.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1&page=" + ddl_page.Text;
                            Response.Redirect(reslink, false);
                        }
                        else
                        {
                            string reslink = "slip/print-termwise-admit-card.aspx?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&examterm=" + ddl_exam_term.SelectedValue + "&admin=" + adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1&page=" + ddl_page.Text;
                            Response.Redirect(reslink, false);
                        }
                    }
                    //}
                }
                else
                {
                    Alertme("Please select the students.", "warning");
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}