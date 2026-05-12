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
    public partial class Test_wise_Question_List_Uploaded_by_Teacher : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                        ViewState["session_id"] = My.get_session_id();

                        mycode.bind_all_ddl_with_id_cap_All(ddl_Test_name, "Select Exam_name,Entry_id from OLINETEST_EXAM_NAME_Murge_Section where  Session_id= '" + ViewState["session_id"] + "'and   Entry_id in (Select Exam_id from Upload_Question_Question_Teacher) order by Exam_name asc");


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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_all();
        }
        private void Bind_data_all()
        {
            try
            {
                var condition = "where 1=1 ";
                condition += $" and  Session_id='{ViewState["session_id"].ToString()}' ";
                condition += $" and  Entry_id in (Select Exam_id from Upload_Question_Question_Teacher) ";
                //condition += $" and  Exam_id not in (select test_id from question_info where Uploding_status='Administrator') ";

                condition += $" ";



                if (ddl_Test_name.SelectedItem.Text != "ALL")
                {
                    condition += $" and oen.Entry_id='{ddl_Test_name.SelectedValue}' ";
                }
                 
                DataTable dt = My.MydataTable($@"select oen.*,(select top 1 Session from session_details where session_id=oen.Session_id) as session,(Select count(*) from Upload_Question_Question_Teacher where Exam_id=oen.Entry_id) as toquestion,format(oen.live_date,'dd/MM/yyyy') as live_date_one,format(oen.live_date,'hh:mm tt') as live_time_one,ad.Course_Name,(select top 1 Subject_name from Subject_Master where Subject_id=oen.subjectname) as subjectname from  OLINETEST_EXAM_NAME_Murge_Section oen join  Add_course_table ad  on oen.Class_id=ad.course_id  {condition} order by ad.Position,oen.Id  ");
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
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel Panel11 = (Panel)e.Item.FindControl("Panel11");
                Panel Panel2 = (Panel)e.Item.FindControl("Panel2");
                Label lbl_Status = (Label)e.Item.FindControl("lbl_Status");
                Label lbl_Exam_id = (Label)e.Item.FindControl("lbl_Exam_id");
                LinkButton lnkDel = (LinkButton)e.Item.FindControl("lnkDel");
                Panel Panel3 = (Panel)e.Item.FindControl("Panel3");
                

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
                    Panel11.Visible = false;
                    Panel2.Visible = false;
                    lnkDel.Visible = false;
                    lblquestion_uploadstatus.Text = "Not Upload";
                    lblquestion_uploadstatus.CssClass = "badge badge-danger ml-2";
                    Panel3.Visible = true;

                }
                else if (lbl_no_question.Text == "")
                {
                    lnkDel.Visible = false;
                    Panel11.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                    lblquestion_uploadstatus.Text = "Not Upload";
                    lblquestion_uploadstatus.CssClass = "badge badge-danger ml-2";

                    if (lbl_Status.Text == "Inactive")
                    {
                        Panel11.Visible = false;
                        Panel2.Visible = false;
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel11.Visible = false;
                        Panel2.Visible = false;
                        Panel3.Visible = false;
                    }
                }
                else
                {
                    Panel11.Visible = true;
                    Panel2.Visible = true;
                    Panel3.Visible = true;
                    bool find_final_submit_status = My.get_question_final_submit(lbl_Exam_id.Text);

                    if (find_final_submit_status == true)
                    {
                        if (lbl_Status.Text == "Inactive")
                        {
                            lnkDel.Visible = true;
                            Panel11.Visible = true;
                            Panel2.Visible = true;
                            Panel3.Visible = false;
                            lblquestion_uploadstatus.Text = "Not Final Submit";
                            lblquestion_uploadstatus.CssClass = "badge badge-success ml-2";
                        }
                        else
                        {
                            Panel2.Visible = true;
                            lnkDel.Visible = false;
                            Panel11.Visible = false;
                            Panel3.Visible = false;

                        }
                    }
                    else
                    {
                        if (lbl_Status.Text == "Inactive")
                        {
                            lnkDel.Visible = true;
                            Panel11.Visible = true;
                            Panel2.Visible = true;
                            Panel3.Visible = true;
                        }
                        else
                        {
                            Panel2.Visible = true;
                            lnkDel.Visible = false;
                            Panel11.Visible = false;
                            Panel3.Visible = false;
                        }


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
                    Label lbl_Exam_id = (Label)row.FindControl("lbl_Exam_id");
                    Label lbl_exam_name = (Label)row.FindControl("lbl_exam_name");
                    empty_question(lbl_Exam_id.Text);
                    string msg = "Exam Name :" + lbl_exam_name.Text + " deleted by= " + ViewState["Userid"].ToString() + " Exam_id=" + lbl_Exam_id.Text + " " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString());
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

        private void empty_question(string Exam_id)
        {
            string query1;
            query1 = "Delete from Upload_Question_Question_Teacher where Exam_id ='" + Exam_id + "' ;";
            mycode.executequery(query1);



        }

        
    }
}