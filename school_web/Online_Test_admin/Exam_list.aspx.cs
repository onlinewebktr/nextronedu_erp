using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
using System.Transactions;

namespace school_web.Online_Test_admin
{
    public partial class Exam_list : System.Web.UI.Page
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
                        ViewState["bydate"] = "0";
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Exam_list.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_Onlinetest(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        DateTime start = DateTime.Today.AddHours(6); // 6:00 AM
                        DateTime end = DateTime.Today.AddHours(24);  // 6:00 PM

                        while (start <= end)
                        {
                            ddl_timespan.Items.Add(new ListItem(start.ToString("hh:mm tt")));
                            start = start.AddMinutes(5); // Adds all minute options
                        }

 
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_add, "Select Course_Name,course_id from Add_course_table order by Position asc");


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

        protected void btn_find_bydate_Click(object sender, EventArgs e)
        {
            ViewState["bydate"] = "1";
            Bind_data_all();
        }
        private void Bind_data_all()
        {
            try
            {
                var condition = "where 1=1 ";
                condition += $" and  Session_id='{ddlsession.SelectedValue}' ";
                if (ddl_class.SelectedItem.Text != "ALL")
                {
                    condition += $" and Class_id='{ddl_class.SelectedValue}' ";
                }
                if(ViewState["bydate"].ToString()=="1")
                {
                    condition += $" and FORMAT(live_date,'yyyyMMdd')='{mycode.ConvertStringToiDate(txt_date.Text)}' ";
                }
                DataTable dt = My.MydataTable($@"select oen.*,(select top 1 Session from session_details where session_id=oen.Session_id) as session,(Select count(*) from question_info where test_id=(select top 1 Exam_id from OLINETEST_EXAM_NAME where Entry_id=oen.Entry_id)) as toquestion,format(oen.live_date,'dd/MM/yyyy') as live_date_one,format(oen.live_date,'hh:mm tt') as live_time_one,ad.Course_Name,(select top 1 Subject_name from Subject_Master where Subject_id=oen.subjectname) as subject_name,(select top 1 Exam_id from OLINETEST_EXAM_NAME where Entry_id=oen.Entry_id) as top_oneexam_id from  OLINETEST_EXAM_NAME_Murge_Section oen join  Add_course_table ad  on oen.Class_id=ad.course_id  {condition} order by ad.Position,oen.Id  ");
                if (dt.Rows.Count == 0)
                {
                    btn_excels.Visible = false;
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                    lbl_class22.Text = "";
                }
                else
                {
                    btn_excels.Visible = true;

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
                ViewState["bydate"] = "0";
                Bind_data_all();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["bydate"] = "0";
            Bind_data_all();
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            ViewState["bydate"] = "0";
            Bind_data_all();
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_Entry_id = (Label)e.Item.FindControl("lbl_Entry_id");


                if (ViewState["Is_Edit"].ToString() == "1")
                {

                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                }
                else
                {

                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = false;
                }

                if (ViewState["Is_delete"].ToString() == "1")
                {
                    ((LinkButton)e.Item.FindControl("lnkDel")).Visible = true;
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnkDel")).Visible = false;
                }



                ////==========================================////
                ///
                Panel Panel11 = (Panel)e.Item.FindControl("Panel11");

                Label lbl_Status = (Label)e.Item.FindControl("lbl_Status");
                if (lbl_Status.Text == "Active")
                {
                    lbl_Status.Text = "Active";
                    lbl_Status.CssClass = "badge badge-success ml-2";

                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Image { ImageUrl = "~/images/inactiveicon.png", });
                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Label { Text = "Inactive" });
                    ((LinkButton)e.Item.FindControl("lnkDel")).Visible = false;



                }
                else
                {
                    lbl_Status.Text = "Inactive";
                    lbl_Status.CssClass = "badge badge-danger ml-2";

                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Image { ImageUrl = "~/images/activeicon.png" });

                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Label { Text = "Active" });
                    ((LinkButton)e.Item.FindControl("lnkDel")).Visible = true;

                }

                Label lbl_no_question = (Label)e.Item.FindControl("lbl_no_question");
                Label lblquestion_uploadstatus = (Label)e.Item.FindControl("lblquestion_uploadstatus");

                if (lbl_no_question.Text == "0")
                {
                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Visible = false;
                    ((LinkButton)e.Item.FindControl("lnk_deletequestion")).Visible = false;
                    Panel11.Visible = false;
                    lblquestion_uploadstatus.Text = "Not Upload";
                    lblquestion_uploadstatus.CssClass = "badge badge-danger ml-2";
                }
                else if (lbl_no_question.Text == "")
                {
                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Visible = false;
                    ((LinkButton)e.Item.FindControl("lnk_deletequestion")).Visible = false;
                    Panel11.Visible = false;
                    lblquestion_uploadstatus.Text = "Not Upload";
                    lblquestion_uploadstatus.CssClass = "badge badge-danger ml-2";
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Visible = true;

                    if(lbl_Status.Text== "Active")
                    {
                        if (ViewState["Is_delete"].ToString() == "1")
                        {

                            ((LinkButton)e.Item.FindControl("lnk_deletequestion")).Visible = false;
                        }
                        else
                        {

                            ((LinkButton)e.Item.FindControl("lnk_deletequestion")).Visible = false;
                        }
                    }
                    else
                    {
                        if (ViewState["Is_delete"].ToString() == "1")
                        {

                            ((LinkButton)e.Item.FindControl("lnk_deletequestion")).Visible = true;
                        }
                        else
                        {

                            ((LinkButton)e.Item.FindControl("lnk_deletequestion")).Visible = false;
                        }
                    }
                    



                    Panel11.Visible = true;
                    lblquestion_uploadstatus.Text = "Uploaded";
                    lblquestion_uploadstatus.CssClass = "badge badge-success ml-2";
                }
                Label lbl_subject = (Label)e.Item.FindControl("lbl_subject");
                Label lbl_subjectname_view = (Label)e.Item.FindControl("lbl_subjectname_view");
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
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
                    Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                    Label lbl_Entry_id = (Label)row.FindControl("lbl_Entry_id");
                    Label lbl_Status = (Label)row.FindControl("lbl_Status");
                    Label lbl_exam_name = (Label)row.FindControl("lbl_exam_name");
                    Label lbl_marks = (Label)row.FindControl("lbl_marks");
                    Label lbl_livedate = (Label)row.FindControl("lbl_livedate");
                    Label live_time = (Label)row.FindControl("live_time");
                    Label lbl_exam_duration = (Label)row.FindControl("lbl_exam_duration");
                    Label lbl_Section = (Label)row.FindControl("lbl_Section");
                    Label lbl_subject = (Label)row.FindControl("lbl_subject");
                    ddl_class_add.SelectedValue = lbl_Class_id.Text;
                    ViewState["courseID"] = lbl_Class_id.Text;
                    try
                    {
                        if (ddl_class_add.SelectedItem.Text == "ALL")
                        {
                            mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + lbl_Session_id.Text + "'");
                        }
                        else
                        {
                            mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + lbl_Session_id.Text + "' and Class_id='" + ddl_class_add.SelectedValue + "'");

                            mycode.bind_ddlall_subject(ddl_subjectname, "Select Subject_name,Subject_id from Subject_Master where   course_id='" + ddl_class_add.SelectedValue + "' order by Subject_position asc");
                        }
                    }
                    catch
                    {

                    }
                    ddl_section.Text = lbl_Section.Text;
                    hd_id.Value = lbl_Entry_id.Text;
                    ddl_timespan.Text = live_time.Text;
                    txt_Testname.Text = lbl_exam_name.Text;
                    txt_marks.Text = lbl_marks.Text;
                    txt_livedate.Text = lbl_livedate.Text;
                    txt_time_duration.Text = lbl_exam_duration.Text;
                    try
                    {
                        ddl_subjectname.SelectedValue = lbl_subject.Text;

                    }
                    catch
                    {

                    }
                    try
                    {
                        if (lbl_Status.Text == "")
                        {
                            chk_mandatory.Checked = false;
                        }
                        else if (lbl_Status.Text == "Inactive")
                        {
                            chk_mandatory.Checked = false;
                        }
                        else
                        {
                            chk_mandatory.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

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



        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_id = (Label)row.FindControl("lbl_id");
                    Label Entry_id = (Label)row.FindControl("lbl_Entry_id");
                    Label lbl_Section = (Label)row.FindControl("lbl_Section");
                    Label lbl_exam_name = (Label)row.FindControl("lbl_exam_name");
                    Label lbl_class = (Label)row.FindControl("lbl_class");

                    mycode.executequery("delete from OLINETEST_EXAM_NAME_Murge_Section where Id='" + lbl_id.Text + "'");
                    mycode.executequery("delete from OLINETEST_EXAM_NAME where Entry_id='" + Entry_id.Text + "'");
                    DataTable cdt = My.dataTable("Select Exam_id from OlineTest_Exam_name where Entry_id='" + Entry_id.Text + "'");
                    if (cdt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        for (int i = 0; i < cdt.Rows.Count; i++)
                        {
                            string Exam_id = cdt.Rows[i]["Exam_id"].ToString();
                            mycode.executequery("delete from question_info where test_id='" + Exam_id + "'");
                            empty_question(Exam_id);
                        }
                    }
                    try
                    {
                        string msg = "Exam Name :"+ lbl_exam_name.Text + "deleted by= " + ViewState["Userid"].ToString() + " Exam_id=" + hd_id.Value + " Class: " + lbl_class.Text + " Section :" + lbl_Section.Text +" "+ DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                        mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString());

                    }
                    catch
                    {

                    }


                    Alertme("Recode has been deleted successfully", "success");
                    Bind_data_all();
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
        protected void btn_Submit_Click(object sender, EventArgs e)
        {


            if (txt_Testname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                txt_Testname.Focus();
                Alertme("Please enter test name.", "warning");
            }
            else if (txt_livedate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                txt_Testname.Focus();
                Alertme("Please enter exam date", "warning");
            }
            else if (ddl_timespan.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                txt_Testname.Focus();
                Alertme("Please enter exam time", "warning");
            }
            else if (txt_time_duration.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                txt_Testname.Focus();
                Alertme("Please enter exam duration", "warning");
            }
            else
            {
                if (btn_Submit.Text == "Add")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        add_update_exam();
                        ViewState["courseID"] = "0";
                        txt_Testname.Text = "";
                        txt_marks.Text = "0";
                        
                        txt_livedate.Text = "";
                        txt_time_duration.Text = "";
                        chk_mandatory.Checked = false;
                       
                        btn_cancel.Visible = false;
                        btn_Submit.Text = "Add";
                        Bind_data_all();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");

                    }
                }
                else
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        add_update_exam();
                        ViewState["courseID"] = "0";

                        txt_Testname.Text = "";
                        txt_marks.Text = "0";
                        
                        txt_livedate.Text = "";
                        txt_time_duration.Text = "";
                        chk_mandatory.Checked = false;
                        Alertme("Online eaxm has been sucessfully updated", "success");
                        btn_cancel.Visible = false;
                        btn_Submit.Text = "Add";
                        Bind_data_all();

                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");

                    }
                }




            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            ViewState["courseID"] = "0";

            txt_Testname.Text = "";
            txt_marks.Text = "0";
           
            txt_livedate.Text = "";
            txt_time_duration.Text = "";
            chk_mandatory.Checked = false;

            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
            Bind_data_all();

        }
        private void add_update_exam()
        {
            payments p1 = new payments();
            string live_date = My.toDateTime(txt_livedate.Text + " " + ddl_timespan.Text).ToString("MM/dd/yyyy hh:mm tt");

            bool  finalupdate = false;
            if (btn_Submit.Text == "Add")
            {
                string query1 = "";
                string query2 = "";
                
                if (ViewState["Is_add"].ToString() == "1")
                {
                    if (ddl_class_add.SelectedItem.Text == "ALL")
                    {
                        query1 = "Select course_id,Course_Name from Add_course_table order by Position  ";

                    }
                    else
                    {
                        query1 = "Select course_id,Course_Name from Add_course_table where course_id='" + ddl_class_add.SelectedValue + "' order by Position  ";
                    }
                    string sestion = "ALL";
                    if (ddl_section.Text == "ALL")
                    {
                        sestion = "ALL";
                        query2 = "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class_add.SelectedValue + "'";

                    }
                    else
                    {
                        sestion = ddl_section.Text;
                        query2 = "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class_add.SelectedValue + "' and Section='" + ddl_section.Text + "' ";
                    }

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        string Entry_id = create_Entry_id(con);

                        
                        insert_OLINETEST_EXAM_NAME_Murge_Section(ddl_class_add.SelectedValue, sestion, live_date, ddl_class_add.SelectedItem.Text, Entry_id, con);


                        DataTable cdt = payments.dataTable(query1, con);
                        if (cdt.Rows.Count == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < cdt.Rows.Count; i++)
                            {
                                string course_id = cdt.Rows[i]["course_id"].ToString();
                                string Course_Name = cdt.Rows[i]["Course_Name"].ToString();

                                DataTable cd2 = My.dataTable(query2);
                                if (cd2.Rows.Count == 0)
                                {

                                }
                                else
                                {
                                    for (int ii = 0; ii < cd2.Rows.Count; ii++)
                                    {
                                        string Section = cd2.Rows[ii]["Section"].ToString();
                                        insert_update_data(course_id, Section, live_date, Course_Name, Entry_id, con);
                                    }
                                }
                            }
                        }

                        con.Close();
                        scope.Complete();
                        finalupdate = true;
                    }

                    if(finalupdate==true)
                    {
                     
                        Alertme("Exam details has been sucessfully added", "success");
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                
                string query2 = "";
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    string Entry_id = hd_id.Value;

                    query2 = "Select Exam_id from OLINETEST_EXAM_NAME where Entry_id='" + Entry_id + "'  ";
                   
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                      

                        update_OLINETEST_EXAM_NAME_Murge_Section(ddl_class_add.SelectedValue, "ALL", live_date, ddl_class_add.SelectedItem.Text, Entry_id, con);


                        DataTable cdt = payments.dataTable(query2, con);
                        if (cdt.Rows.Count == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < cdt.Rows.Count; i++)
                            {
                                string Exam_id = cdt.Rows[i]["Exam_id"].ToString();
                                SqlCommand cmd;
                                string query = "Update OlineTest_Exam_name set Session_id=@Session_id,Class_id=@Class_id,subjectname=@subjectname,Marks=@Marks,Exam_name=@Exam_name,live_date=@live_date,Exam_duration=@Exam_duration,Paper_type=@Paper_type,Updated_by=@Updated_by,Updated_date=DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())) where Exam_id = @Exam_id";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                                cmd.Parameters.AddWithValue("@Class_id", ddl_class_add.SelectedValue);
                                cmd.Parameters.AddWithValue("@subjectname", ddl_subjectname.Text);
                                cmd.Parameters.AddWithValue("@Marks", txt_marks.Text.Trim());
                                cmd.Parameters.AddWithValue("@Exam_name", txt_Testname.Text);
                                cmd.Parameters.AddWithValue("@live_date", live_date);
                                cmd.Parameters.AddWithValue("@Exam_duration", txt_time_duration.Text);
                                cmd.Parameters.AddWithValue("@Paper_type", "Objective");
                                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Exam_id", Exam_id);
                                if (payments.InsertUpdateData(cmd, con))
                                {
                                    ViewState["IsSuccess"] = "1";
                                    string msg = ViewState["Userid"].ToString() + " Updated onlinetest, Exam id=" + hd_id.Value + " Class Name: " + ddl_class_add.SelectedItem.Text + " Section :" + ddl_section.Text + " Examname=" + txt_Testname.Text + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                                    p1.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString(), con);
                                }
                            }
                        }
                        con.Close();
                        scope.Complete();
                        finalupdate = true;
                    }

                    if (finalupdate == true)
                    {
                        Alertme("Exam details has been updated sucessfully", "success");
                    }


                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void update_OLINETEST_EXAM_NAME_Murge_Section(string course_id, string section, string live_date, string Course_Name, string Entry_id, SqlConnection con)
        {
            SqlCommand cmd;
            string query = "Update OLINETEST_EXAM_NAME_Murge_Section set Session_id=@Session_id,Class_id=@Class_id,subjectname=@subjectname,Marks=@Marks,Exam_name=@Exam_name,live_date=@live_date,Exam_duration=@Exam_duration,Paper_type=@Paper_type,Updated_by=@Updated_by,Updated_date=DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())) where Entry_id = @Entry_id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddl_class_add.SelectedValue);
            cmd.Parameters.AddWithValue("@subjectname", ddl_subjectname.Text);
            cmd.Parameters.AddWithValue("@Marks", txt_marks.Text.Trim());
            cmd.Parameters.AddWithValue("@Exam_name", txt_Testname.Text);
            cmd.Parameters.AddWithValue("@live_date", live_date);
            cmd.Parameters.AddWithValue("@Exam_duration", txt_time_duration.Text);
            cmd.Parameters.AddWithValue("@Paper_type", "Objective");
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Entry_id", Entry_id);
            if (payments.InsertUpdateData(cmd,con))
            {
            }
        }

        private void insert_OLINETEST_EXAM_NAME_Murge_Section(string course_id, string section, string live_date, string Course_Name,string Entry_id, SqlConnection con)
        {
            SqlCommand cmd;
            string query = " INSERT INTO OLINETEST_EXAM_NAME_Murge_Section (Session_id,Class_id,subjectname,Marks,Exam_name,live_date,Exam_duration,Paper_type,Created_by,Created_Date,Entry_id,Section,Status) values (@Session_id,@Class_id,@subjectname,@Marks,@Exam_name,@live_date,@Exam_duration,@Paper_type,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@Entry_id,@Section,@Status)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", course_id);
            cmd.Parameters.AddWithValue("@subjectname", ddl_subjectname.Text);
            cmd.Parameters.AddWithValue("@Marks", txt_marks.Text.Trim());
            cmd.Parameters.AddWithValue("@Exam_name", txt_Testname.Text);
            cmd.Parameters.AddWithValue("@live_date", live_date);
            cmd.Parameters.AddWithValue("@Exam_duration", txt_time_duration.Text);
            cmd.Parameters.AddWithValue("@Paper_type", "Objective");

            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Entry_id", Entry_id);
            cmd.Parameters.AddWithValue("@Section", section);
            cmd.Parameters.AddWithValue("@Status", "Inactive");

            if (payments.InsertUpdateData(cmd, con))
            {
               
            }
        }

        private string create_Entry_id(SqlConnection con)
        {
            bool duplicate = false;
            string Exam_id = pm.auto_serial("On_Entry_id", con);
            while (!duplicate)
            {
                DataTable cdt = pm.table("select Entry_id from dbo.[OLINETEST_EXAM_NAME_Murge_Section] where Entry_id='" + Exam_id + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Exam_id = pm.auto_serial("On_Entry_id", con);
                }
            }
            return Exam_id;
        }

        private void insert_update_data(string course_id, string section, string live_date, string Course_Name,string Entry_id,SqlConnection con)
        {
            SqlCommand cmd;
           

            string Exam_id = create_onlineexamtid(con);
            string query = " INSERT INTO OlineTest_Exam_name (Session_id,Class_id,subjectname,Marks,Exam_name,live_date,Exam_duration,Paper_type,Created_by,Created_Date,Exam_id,Section,Status,Entry_id) values (@Session_id,@Class_id,@subjectname,@Marks,@Exam_name,@live_date,@Exam_duration,@Paper_type,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@Exam_id,@Section,@Status,@Entry_id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", course_id);
            cmd.Parameters.AddWithValue("@subjectname", ddl_subjectname.Text);
            cmd.Parameters.AddWithValue("@Marks", txt_marks.Text.Trim());
            cmd.Parameters.AddWithValue("@Exam_name", txt_Testname.Text);
            cmd.Parameters.AddWithValue("@live_date", live_date);
            cmd.Parameters.AddWithValue("@Exam_duration", txt_time_duration.Text);
            cmd.Parameters.AddWithValue("@Paper_type", "Objective");

            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Exam_id", Exam_id);
            cmd.Parameters.AddWithValue("@Section", section);
            cmd.Parameters.AddWithValue("@Status", "Inactive");
            cmd.Parameters.AddWithValue("@Entry_id", Entry_id);
            if (payments.InsertUpdateData(cmd, con))
            {
                ViewState["IsSuccess"] = "1";
                string msg = ViewState["Userid"].ToString() + " Created  onlinetest, Exam id=" + Exam_id + " Class Name: " + Course_Name + " Section: " + ddl_section.Text + " Examname =" + txt_Testname.Text + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                pm.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString(), con);
            }
           
        }

        payments pm=new payments();
        private string create_onlineexamtid(SqlConnection con)
        {
            bool duplicate = false;
            string Exam_id = pm.auto_serial("OnlineExam_id", con);
            while (!duplicate)
            {
                DataTable cdt = pm.table("select Exam_id from dbo.[OlineTest_Exam_name] where Exam_id='" + Exam_id + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Exam_id = pm.auto_serial("OnlineExam_id", con);
                }
            }
            return Exam_id;
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string exportname = My.with_excel_name("Examlistname");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + exportname + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }



        protected void lnk_active_inactive_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_status = (Label)row.FindControl("lbl_Status");
                Label lbl_Entry_id = (Label)row.FindControl("lbl_Entry_id");
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_Section = (Label)row.FindControl("lbl_Section");
                Label lbl_exam_name = (Label)row.FindControl("lbl_exam_name");
                if (lbl_status.Text == "Active")
                {
                    mycode.executequery("update OLINETEST_EXAM_NAME_Murge_Section set Status='Inactive'  where Id='" + lbl_id.Text + "'");
                    mycode.executequery("update OLINETEST_EXAM_NAME set Status='Inactive'  where Entry_id='" + lbl_Entry_id.Text + "'");

                    string msg = "Exam has been Inactivated by " + ViewState["Userid"].ToString() + " Class:" + lbl_class.Text+" Section :"+ lbl_Section.Text+" Exam Name:"+ lbl_exam_name.Text + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString());
                }
                else
                {
                    mycode.executequery("update OLINETEST_EXAM_NAME_Murge_Section set Status='Active'  where Id='" + lbl_id.Text + "'");
                    mycode.executequery("update OLINETEST_EXAM_NAME set Status='Active'  where Entry_id='" + lbl_Entry_id.Text + "'");
                    string msg = "Exam has been activated by " + ViewState["Userid"].ToString() + " Class:" + lbl_class.Text + " Section :" + lbl_Section.Text + " Exam Name:" + lbl_exam_name.Text + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString());
                }
                Bind_data_all();
            }
            catch
            {

            }
        }
        #region select class section
        protected void ddl_class_add_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class_add.SelectedItem.Text == "ALL")
                {
                    mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "'");
                }
                else
                {
                    mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class_add.SelectedValue + "'");

                    mycode.bind_ddlall_subject(ddl_subjectname, "Select Subject_name,Subject_id from Subject_Master where   course_id='" + ddl_class_add.SelectedValue + "' and Is_mandatory=1 order by Subject_position asc");

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {

            }


        }
        #endregion
        protected void lnk_deletequestion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_id = (Label)row.FindControl("lbl_id");
                    Label Entry_id = (Label)row.FindControl("lbl_Entry_id");
                    Label lbl_exam_name = (Label)row.FindControl("lbl_exam_name");
                    Label lbl_Section = (Label)row.FindControl("lbl_Section");
                    Label lbl_class = (Label)row.FindControl("lbl_class");

                    DataTable cdt = My.dataTable("Select Exam_id from OlineTest_Exam_name where Entry_id='" + Entry_id.Text + "'");
                    if (cdt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        for (int i = 0; i < cdt.Rows.Count; i++)
                        {
                            string Exam_id = cdt.Rows[i]["Exam_id"].ToString();
                            mycode.executequery("delete from question_info where test_id='" + Exam_id + "'");
                            empty_question(Exam_id);
                            
                        }
                    }
                    string msg = "Question Deleted by= " + ViewState["Userid"].ToString() + " Exam_id=" + hd_id.Value + "Exam Name=" + lbl_exam_name.Text + " Class: " + lbl_class.Text + "  Section :" + lbl_Section.Text+" " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString());

                    Alertme("Question has been deleted successfully", "success");
                    Bind_data_all();
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
        private void empty_question(string testid)
        {
            string query1;

            query1 = "Delete from Declaration where declaration_id in (Select Direction_id from question_info where test_id='" + testid + "'  );";
            query1 = query1 + "Delete from Phrase_details where phrases_id in (Select Phrase_id from question_info where test_id='" + testid + "'  );";
            query1 = query1 + "Delete from Question_Explanation where test_id ='" + testid + "' ;";
            query1 = query1 + "Delete from question_answer_Master where test_code ='" + testid + "'  ;";
            query1 = query1 + "Delete from question_info where test_id ='" + testid + "' and Upload_Type='Bulk';";
            query1 = query1 + "Delete from Save_question_url where Test_id ='" + testid + "' ;";
            query1 = query1 + "Delete from Question_Upload_History where Test_id ='" + testid + "' ;";
            query1 = query1 + "update Test_info set isActive='0',Status='Pending',MapStatus='Not Map' where Test_id ='" + testid + "' ;";
            query1 = query1 + "Delete from Section_Arranging where Test_id ='" + testid + "' ;";
            query1 = query1 + "Delete from Upload_Question_Question_Temp where Test_id ='" + testid + "' ;";

            mycode.executequery(query1);
        }

        
    }
}