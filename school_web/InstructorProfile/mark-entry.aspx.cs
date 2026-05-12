using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.InstructorProfile
{
    public partial class mark_entry : System.Web.UI.Page
    {

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        UsesCode mycode = new UsesCode();
        My my = new My();
        Examination myexam = new Examination();
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (Session["teacher"] == null)
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
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["Branchid"] = mycode.getbranchid(Session["teacher"].ToString());
                    ViewState["teacher"] = Session["teacher"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");

                    mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and    UserID='" + ViewState["teacher"].ToString() + "'  order by section");

                    mycode.bind_all_ddl_with_id(ddl_remarks, "select Short_Name,Exam_Marks_Entry_Label_Id from Exam_Marks_Entry_Label where Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Short_Name asc");








                }
            }
        }
        private void get_max_marks()
        {

            string query = "select Maximum_Marks from Exam_Subject_Sub_Level where Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Assessment_Id='" + ddl_assesment.SelectedValue + "' and Class_id=" + ddl_CourseCat.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Subject_Sub_Level_Id=" + ddl_exam_level.SelectedValue + " ";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                lbl_activity_type.Text = ddl_exam_level.SelectedItem.Text + " (" + dt.Rows[0]["Maximum_Marks"].ToString() + ")";
                txt_mm.Text = dt.Rows[0]["Maximum_Marks"].ToString();
            }
        }
        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                mycode.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and    UserID='" + ViewState["teacher"].ToString() + "'  order by section");

                mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RPDetails.DataSource = null;
            RPDetails.DataBind();
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else
            {

            }
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_assesment, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Assessment_Name asc");
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class.");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section.");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alert("Please select term.");
                    ddl_term.Focus();
                }
                else if (ddl_assesment.SelectedItem.Text == "Select")
                {
                    Alert("Please select Assessment.");
                    ddl_assesment.Focus();
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject.");
                    ddl_subject.Focus();
                }
                else if (ddl_exam_level.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject activity.");
                    ddl_exam_level.Focus();
                }
                else
                {
                    find_students();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_students()
        {
            lbl_datevalid.Text = "";
            lbl_activity_type.Text = ddl_exam_level.SelectedItem.Text;

            string get_datevalid = myexam.get_fill_marks_date(ViewState["sesssionid"].ToString(), ViewState["Branchid"].ToString(), ddl_CourseCat.SelectedValue, ddl_term.SelectedValue, ddl_assesment.SelectedValue, ddl_subject.SelectedValue, ddl_exam_level.SelectedValue);

            lbl_datevalid.Text = "Fill marks " + get_datevalid;


            bool chekmarks_entry_saved = myexam.check_marks_save(ViewState["sesssionid"].ToString(), ViewState["Branchid"].ToString(), ddl_CourseCat.SelectedValue, ddl_term.SelectedValue, ddl_assesment.SelectedValue, ddl_subject.SelectedValue, ddl_exam_level.SelectedValue, ddl_section.Text);

            if (chekmarks_entry_saved == true)//save status Marks mins not sav final
            {
                btn_save.Enabled = true;
                btn_fina_saved.Enabled = true;
            }
            else
            {
                btn_save.Enabled = false;
                btn_fina_saved.Enabled = false;

            }


            string query = "Select distinct ar.admissionserialnumber,ar.studentname,ar.rollnumber   from admission_registor ar join Subject_Mapping_New smn on ar.admissionserialnumber=smn.Admission_no and ar.Session_Id=smn.Session_id and ar.Branch_id=smn.Branch_id and ar.Class_Id=smn.Class_id where ar.Session_id='" + ViewState["sesssionid"].ToString() + "' and ar.Class_id='" + ddl_CourseCat.SelectedValue + "' and ar.Section='" + ddl_section.Text + "'  and   ar.Branch_Id='" + ViewState["Branchid"].ToString() + "' and  smn.Sub_id='" + ddl_subject.SelectedValue + "' and ar.Status='1' and  ar.StudentStatus!='TC' order by ar.rollnumber";
            get_max_marks();
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {

                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }


        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class.");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section.");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alert("Please select term.");
                    ddl_term.Focus();
                }
                else if (ddl_assesment.SelectedItem.Text == "Select")
                {
                    Alert("Please select Assessment.");
                    ddl_assesment.Focus();
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject.");
                    ddl_subject.Focus();
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_exam_level, "select Subject_Activity_Name,Subject_Sub_Level_Id from Exam_Subject_Sub_Level where Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Assessment_Id='" + ddl_assesment.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' order by Subject_Activity_Name asc");
                }
            }
            catch (Exception ex)
            {
            }
        }



        protected void lnk_remarks_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                TextBox txt_marks = (TextBox)row.FindControl("txt_marks");
                ViewState["admNo"] = lbl_adm_no.Text;
                myModalSS.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_save_rmrks_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_remarks.SelectedItem.Text == "Select")
                {
                    Alert("please select remark type.");
                    myModalSS.Visible = true;
                }
                else
                {
                    int i;
                    int gridview_rowcount = RPDetails.Items.Count;
                    for (i = 0; i < gridview_rowcount; i++)
                    {
                        Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                        TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                        Label lbl_mark_ids = (Label)RPDetails.Items[i].FindControl("lbl_mark_ids");
                        if (lbl_adm_no.Text == ViewState["admNo"].ToString())
                        {
                            txt_marks.Text = ddl_remarks.SelectedItem.Text;
                            lbl_mark_ids.Text = ddl_remarks.SelectedValue;
                            myModalSS.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                save_marks();
               
            }
            catch (Exception ex)
            {
            }
        }

        private void save_marks()
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class.");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section.");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term.");
                ddl_term.Focus();
            }
            else if (ddl_assesment.SelectedItem.Text == "Select")
            {
                Alert("Please select Assessment.");
                ddl_assesment.Focus();
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject.");
                ddl_subject.Focus();
            }
            else if (ddl_exam_level.SelectedItem.Text == "Select")
            {
                Alert("Please subject activity.");
                ddl_exam_level.Focus();
            }
            else
            {


                bool chekmarks_entry_date = myexam.check_valid_date_examnation(ViewState["sesssionid"].ToString(), ViewState["Branchid"].ToString(), ddl_CourseCat.SelectedValue, ddl_term.SelectedValue, ddl_assesment.SelectedValue, ddl_subject.SelectedValue, ddl_exam_level.SelectedValue);
                if (chekmarks_entry_date == true)// dadline set
                {
                    save_data();
                    Alert("Marks has been saved successfully.");
                }
                else
                {

                    Alert("Sorry marks entry deadline over, So you can't  insert marks.");
                }





            }
        }

        private void save_data()
        {
            string qrys = "delete from Exam_marks where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Assessment='" + ddl_assesment.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' and Subject_activity='" + ddl_exam_level.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
            My.exeSql(qrys);

            string Is_character = "0";
            int i;
            int gridview_rowcount = RPDetails.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                Label lbl_mark_ids = (Label)RPDetails.Items[i].FindControl("lbl_mark_ids");

                string valueinput = "";
                if (txt_marks.Text != "")
                {
                    valueinput = txt_marks.Text;

                    if (My.cheknum_fine(valueinput))
                    {
                        Is_character = "0";
                    }
                    else
                    {
                        Is_character = "1";
                    }
                }
                else
                {
                    valueinput = "";
                    Is_character = "0";

                }



                DataTable dt = mycode.FillData("select Id from Exam_marks where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Assessment='" + ddl_assesment.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' and Subject_activity='" + ddl_exam_level.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'  ");
                if (dt.Rows.Count == 0)
                {
                    if (valueinput == "")
                    {
                    }
                    else
                    {


                        SqlCommand cmd;
                        string query = "INSERT INTO Exam_marks (Session_id,Class_id,Section,Term,Assessment,Subject,Subject_activity,Admission_no,Marks,Branch_id,Created_by,Created_date,Created_idate,Mark_id,Is_character,Is_save_marks) values (@Session_id,@Class_id,@Section,@Term,@Assessment,@Subject,@Subject_activity,@Admission_no,@Marks,@Branch_id,@Created_by,@Created_date,@Created_idate,@Mark_id,@Is_character,@Is_save_marks)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Term", ddl_term.SelectedValue);
                        cmd.Parameters.AddWithValue("@Assessment", ddl_assesment.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject_activity", ddl_exam_level.SelectedValue);
                        cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                        cmd.Parameters.AddWithValue("@Marks", valueinput);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["teacher"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());

                        cmd.Parameters.AddWithValue("@Mark_id", lbl_mark_ids.Text);
                        cmd.Parameters.AddWithValue("@Is_character", Is_character);
                        cmd.Parameters.AddWithValue("@Is_save_marks", 0);

                         
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
                else
                {
                    string id = dt.Rows[0]["Id"].ToString();

                    if (txt_marks.Text != "")
                    {
                        valueinput = txt_marks.Text;

                        if (My.cheknum_fine(valueinput))
                        {
                            Is_character = "0";
                        }
                        else
                        {
                            Is_character = "1";
                        }
                    }
                    else
                    {
                        valueinput = "";
                        Is_character = "0";

                    }


                    SqlCommand cmd;
                    string query = "Update Exam_marks set Marks=@Marks,Mark_id=@Mark_id,Is_character=@Is_character,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate,Is_save_marks=@Is_save_marks where Id=" + id + "";
                    cmd = new SqlCommand(query); 
                    cmd.Parameters.AddWithValue("@Marks", valueinput);
                    cmd.Parameters.AddWithValue("@Mark_id", lbl_mark_ids.Text);

                    cmd.Parameters.AddWithValue("@Is_character", Is_character);

                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["teacher"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Is_save_marks", 0);
                    if (My.InsertUpdateData(cmd))
                    {
                        My.exeSql("update Exam_marks set Created_by='" + ViewState["teacher"].ToString() + "' where id=" + id + " and Created_by='AI'");
                    }
                }
            }
        }

        protected void lnk_close_popup_Click(object sender, EventArgs e)
        {
            myModalSS.Visible = false;
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;
                Label lbl_mark_ids = ((Label)e.Item.FindControl("lbl_mark_ids")) as Label;


                DataTable dt = mycode.FillData("select * from Exam_marks where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Assessment='" + ddl_assesment.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' and Subject_activity='" + ddl_exam_level.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Created_by!='AI' ");
                if (dt.Rows.Count == 0)
                {
                    txt_marks.Text = "";
                    lbl_mark_ids.Text = "0";
                }
                else
                {
                    txt_marks.Text = dt.Rows[0]["Marks"].ToString();
                    lbl_mark_ids.Text = dt.Rows[0]["Mark_id"].ToString();
                }
            }
        }

        protected void ddl_assesment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alert("Please select class.");

                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section.");

                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term.");

                ddl_term.Focus();
            }
            else if (ddl_assesment.SelectedItem.Text == "Select")
            {
                Alert("Please select Assessment.");

                ddl_assesment.Focus();
            }
            else
            {



                mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join Exam_Assessment_Subject_Mapping_Details easm on sm.Subject_id=easm.Subject_id   where sm.course_id='" + ddl_CourseCat.SelectedValue + "' and easm.Session_Id=" + ViewState["sesssionid"].ToString() + " and easm.Branch_Id=" + ViewState["Branchid"].ToString() + " and easm.Exam_Term_Id=" + ddl_term.SelectedValue + " and easm.Assessment_Id=" + ddl_assesment.SelectedValue + " and sm.Subject_id in (Select AssignCourseID from TeacherCourseSubjectMaping where CategoryID=" + ddl_CourseCat.SelectedValue + " and UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ViewState["sesssionid"].ToString() + "' and section='" + ddl_section.Text + "') order by sm.Subject_position  ");
            }

        }

        #region save final
        protected void btn_fina_saved_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alert("Please select class.");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section.");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alert("Please select term.");
                    ddl_term.Focus();
                }
                else if (ddl_assesment.SelectedItem.Text == "Select")
                {
                    Alert("Please select Assessment.");
                    ddl_assesment.Focus();
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alert("Please select subject.");
                    ddl_subject.Focus();
                }
                else if (ddl_exam_level.SelectedItem.Text == "Select")
                {
                    Alert("Please subject activity.");
                    ddl_exam_level.Focus();
                }
                else
                {
                    save_data();

                    mycode.executequery("update Exam_Subject_Sub_Level set  Is_save_marks=1 where Session_Id=" + ViewState["sesssionid"].ToString() + " and Branch_Id=" + ViewState["Branchid"].ToString() + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Exam_Term_Id=" + ddl_term.SelectedValue + " and Assessment_Id=" + ddl_assesment.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Subject_Sub_Level_Id=" + ddl_exam_level.SelectedValue + "");
                    mycode.executequery("update Exam_marks set Is_save_marks=1 where Subject_activity=" + ddl_exam_level.SelectedValue + " and Session_id=" + ViewState["sesssionid"].ToString() + " and Branch_id='" + ViewState["Branchid"].ToString() + "' and Section='" + ddl_section.Text + "'");

                    Alert("Marks has been finally submitted successfully.");


                }
            }
            catch(Exception ex)
            {
                My.submitException(ex, "marks save fina by techer");
            }

        }
        #endregion
    }
}