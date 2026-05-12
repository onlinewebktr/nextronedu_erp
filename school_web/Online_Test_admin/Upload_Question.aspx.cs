using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Test_admin
{
    public partial class Upload_Question : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Exam_list.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_Onlinetest(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        //if (ViewState["Is_Print"].ToString() == "1")
                        //{
                        //    print1.Visible = true;
                        //}
                        //else
                        //{
                        //    print1.Visible = false;
                        //}

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        Bind_data_all();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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
        private void Bind_data_all()
        {
            try
            {
                var condition = "where 1=1 ";
                condition += $" and  Session_id='{ddlsession.SelectedValue}' ";

                //condition += $" and  Exam_id not in (select test_id from question_info where Uploding_status='Administrator') ";

                condition += $" and  oen.Status='Inactive' ";
                if (ddl_class.SelectedItem.Text != "ALL")
                {
                    condition += $" and Class_id='{ddl_class.SelectedValue}' ";
                }

                DataTable dt = My.MydataTable($@"select oen.*,(select top 1 Session from session_details where session_id=oen.Session_id) as session,(Select count(*) from question_info where test_id=(select top 1 Exam_id from OLINETEST_EXAM_NAME where Entry_id=oen.Entry_id)) as toquestion,(select top 1 Exam_id from OLINETEST_EXAM_NAME where Entry_id=oen.Entry_id) as top_oneexam_id,format(oen.live_date,'dd/MM/yyyy') as live_date_one,format(oen.live_date,'hh:mm tt') as live_time_one,ad.Course_Name,(select top 1 Subject_name from Subject_Master where Subject_id=oen.subjectname) as subjectname from  OLINETEST_EXAM_NAME_Murge_Section oen join  Add_course_table ad  on oen.Class_id=ad.course_id  {condition} order by ad.Position,oen.Id  ");
                if (dt.Rows.Count == 0)
                {
                    //  btn_excels.Visible = false;
                    Alertme("Sorry there are no record exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                    lbl_class22.Text = "";
                }
                else
                {
                    // btn_excels.Visible = true;

                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    lbl_class22.Text = "";
                }

            }
            catch
            {

            }

        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();

                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                Bind_data_all();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_data_all();
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_all();
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Panel Panel11 = (Panel)e.Item.FindControl("Panel11");
                Panel Panel22 = (Panel)e.Item.FindControl("Panel22");
                Panel Panel2 = (Panel)e.Item.FindControl("Panel2");
                Label lbl_Status = (Label)e.Item.FindControl("lbl_Status");
                Label lbl_Exam_id = (Label)e.Item.FindControl("lbl_Exam_id");
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel");
                Panel2.Visible = false;
                if (lbl_Status.Text == "Active")
                {
                    lbl_Status.Text = "Active";
                    lbl_Status.CssClass = "badge badge-success ml-2";
                     
                }
                else
                {
                   
                    lbl_Status.Text = "Inactive";
                    lbl_Status.CssClass = "badge badge-danger ml-2";
                }
                Label lbl_no_question = (Label)e.Item.FindControl("lbl_no_question");
                Label lblquestion_uploadstatus = (Label)e.Item.FindControl("lblquestion_uploadstatus");
                if (lbl_no_question.Text == "0")
                {
                    lnkDel.Visible = false;
                    Panel11.Visible = false;
                    Panel22.Visible = false;
                    lblquestion_uploadstatus.Text = "Not Upload";
                    lblquestion_uploadstatus.CssClass = "badge badge-danger ml-2";

                    if (lbl_Status.Text == "Inactive")
                    {
                        Panel11.Visible = true;
                        Panel22.Visible = true;
                    }
                    else
                    {
                        Panel11.Visible = false;
                        Panel22.Visible = false;
                    }
                }
                else if (lbl_no_question.Text == "")
                {
                    lnkDel.Visible = false;
                    Panel11.Visible = false;
                    Panel22.Visible = false;
                    lblquestion_uploadstatus.Text = "Not Upload";
                    lblquestion_uploadstatus.CssClass = "badge badge-danger ml-2";

                    if (lbl_Status.Text == "Inactive")
                    {
                        Panel11.Visible = true;
                        Panel22.Visible = true;
                    }
                    else
                    {
                        Panel11.Visible = false;
                        Panel22.Visible = false;
                    }
                }
                else
                {
                    Panel2.Visible = true;
                    bool find_final_submit_status = My.get_question_final_submit(lbl_Exam_id.Text);

                    if (find_final_submit_status == true)
                    {
                        if (lbl_Status.Text == "Inactive")
                        {
                            lnkDel.Visible = true;
                            Panel11.Visible = true;
                            Panel22.Visible = true;
                            lblquestion_uploadstatus.Text = "Not Final Submit";
                            lblquestion_uploadstatus.CssClass = "badge badge-success ml-2";
                        }
                        else
                        {
                            lnkDel.Visible = false;
                            Panel11.Visible = false;
                            Panel22.Visible = false;
                        }
                    }
                    else
                    {
                        if (lbl_Status.Text == "Inactive")
                        {
                            lnkDel.Visible = true; ;
                        }
                        else
                        {
                            lnkDel.Visible = false;
                        }

                        Panel11.Visible = false;
                        Panel22.Visible = false;
                        lblquestion_uploadstatus.Text = "Uploaded";
                        lblquestion_uploadstatus.CssClass = "badge badge-success ml-2";
                    }
                }

                Label lbl_subject = (Label)e.Item.FindControl("lbl_subject");
                Label lbl_subjectname_view = (Label)e.Item.FindControl("lbl_subjectname_view");
                if (lbl_subject.Text == "0")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else if (lbl_subject.Text == "")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else
                {
                    lbl_subjectname_view.Text = lbl_subject.Text;
                }
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Entry_id = (Label)row.FindControl("lbl_Entry_id");
                    empty_question(lbl_Entry_id.Text);
                    Bind_data_all();
                    Alertme("The question that was uploaded has been removed.", "success");
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }
        }

        private void empty_question(string test_id)
        {
            string query1;
            DataTable cdt = mycode.FillData("select Exam_id from OLINETEST_EXAM_NAME where Entry_id='"+ test_id + "'");
            if (cdt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    string testid = cdt.Rows[i]["Exam_id"].ToString();
                    query1 = "Delete from Declaration where declaration_id in (Select Direction_id from question_info where test_id='" + testid + "'  );";
                    query1 = query1 + "Delete from Phrase_details where phrases_id in (Select Phrase_id from question_info where test_id='" + testid + "'  );";
                    query1 = query1 + "Delete from Question_Explanation where test_id ='" + testid + "' ;";
                    query1 = query1 + "Delete from question_answer_Master where test_code ='" + testid + "'  ;";
                    query1 = query1 + "Delete from question_info where test_id ='" + testid + "' ;";
                    query1 = query1 + "Delete from Save_question_url where Test_id ='" + testid + "' ;";
                    query1 = query1 + "Delete from Question_Upload_History where Test_id ='" + testid + "' ;";
                    query1 = query1 + "update Test_info set isActive='0',Status='Pending',MapStatus='Not Map' where Test_id ='" + testid + "' ;";
                    query1 = query1 + "Delete from Section_Arranging where Test_id ='" + testid + "' ;";
                    
                    query1 = query1 + "Delete from Upload_Question_Question_Temp where Test_id ='" + testid + "' ;";

                    mycode.executequery(query1);
                }
            }
        }
    }
}