using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class VIew_Syllabus_Report : System.Web.UI.Page
    {
        My mycode = new My();
        UsesCode code = new UsesCode();
        string scrpt;
        string teachername = "Select top 1 name from user_details where user_id=sc.Teacher_id";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();

                    code.bind_all_ddl_with_id(ddl_sseion, "Select Session,session_id from session_details order by Use_mode asc ");
                    ddl_sseion.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id_cap_All(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term  where Session_id=" + ddl_sseion.SelectedValue + " order by Position asc ");

                    BindGridView();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
        private void BindGridView()
        {
            if (ddl_sseion.SelectedItem.Text == "Select")
            {
                Alert("Please select session.");
                ddl_sseion.Focus();
            }
            else
            {
                string query = "";
                if (ddl_term.SelectedItem.Text == "ALL")
                {
                    query = "Select (Select top 1 name from user_details where user_id=sc.Teacher_id) as teachername,sc.* ,scs.Chapter_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=sc.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=sc.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN scs.Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN scs.Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher sc join Syllubsh_Chapter_SubChapter scs on sc.Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and sc.Session_id=scs.Session_id join Add_course_table act on sc.Class_id=act.course_id where sc.Session_id=" + ddl_sseion.SelectedValue + " and  sc.idate>='" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + "' and sc.idate<='" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "' order by act.Position asc";
                }
                else if (ddl_CourseCat.SelectedItem.Text == "ALL")
                {
                    query = "Select (Select top 1 name from user_details where user_id=sc.Teacher_id) as teachername,sc.* ,scs.Chapter_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=sc.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=sc.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN scs.Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN scs.Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher sc join Syllubsh_Chapter_SubChapter scs on sc.Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and sc.Session_id=scs.Session_id  join Add_course_table act on sc.Class_id=act.course_id where sc.Session_id=" + ddl_sseion.SelectedValue + " and sc.Term_id=" + ddl_term.SelectedValue + " and  sc.idate>='" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + "' and sc.idate<='" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "' order by act.Position asc";
                }
                else if (ddl_teacher_list.SelectedItem.Text == "ALL")
                {
                    query = "Select (Select top 1 name from user_details where user_id=sc.Teacher_id) as teachername,sc.* ,scs.Chapter_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=sc.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=sc.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN scs.Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN scs.Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher sc join Syllubsh_Chapter_SubChapter scs on sc.Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and sc.Session_id=scs.Session_id  join Add_course_table act on sc.Class_id=act.course_id where sc.Session_id=" + ddl_sseion.SelectedValue + " and sc.Term_id=" + ddl_term.SelectedValue + "  and sc.Class_id=" + ddl_CourseCat.SelectedValue + " and  sc.idate>='" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + "' and sc.idate<='" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "' order by act.Position asc";
                }
                else
                {
                    query = "Select (Select top 1 name from user_details where user_id=sc.Teacher_id) as teachername,sc.* ,scs.Chapter_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=sc.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=sc.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN scs.Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN scs.Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher sc join Syllubsh_Chapter_SubChapter scs on sc.Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and sc.Session_id=scs.Session_id  join Add_course_table act on sc.Class_id=act.course_id where sc.Session_id=" + ddl_sseion.SelectedValue + " and sc.Term_id=" + ddl_term.SelectedValue + "  and sc.Class_id=" + ddl_CourseCat.SelectedValue + " and  sc.idate>='" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + "' and sc.Teacher_id='" + ddl_teacher_list.SelectedValue + "' and sc.idate<='" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "' order by act.Position asc";
                }

                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alert("Sorry! there are no any data found");
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }
                else
                {
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
        }



        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }






        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_CourseCat, "select DISTINCT t2.Course_Name, course_id from Syllubsh_Chapter_SubChapter t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddl_sseion.SelectedValue + "' and t1.Term_id='" + ddl_term.SelectedValue + "' order by t2.Course_Name asc");
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_teacher_list, "select (t2.name+' ('+t1.Crated_by+')') as Name,t1.Crated_by from Syllubsh_Chapter_SubChapter t1 join user_details t2 on t1.Crated_by=t2.user_id where t1.Session_id='" + ddl_sseion.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' order by t2.name asc");
            }
            catch (Exception ex)
            {
            }
        }
    }
}