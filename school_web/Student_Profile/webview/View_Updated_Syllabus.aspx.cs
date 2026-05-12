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
    public partial class View_Updated_Syllabus : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        My mycodes = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["sesssionid"] = My.get_session_id();
            if (Request.QueryString["regid"] != null)
            {
                ViewState["regid"] = Request.QueryString["regid"].ToString();
                try
                {
                    if (!IsPostBack)
                    {
                        mycodes.bind_all_ddl_with_id_cap_All(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term where Session_id=" + ViewState["sesssionid"].ToString() + " order by Position asc");
                        Bind_student_details();
                        Bind_data();
                    }
                }
                catch
                { 
                }
            }
        }

        private void Bind_student_details()
        { 
            string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV'    and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alert("Somthing is wrong");

                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                }
                mycodes.bind_all_ddl_with_id_cap_All(ddl_subject, "Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["class_id"].ToString() + "' and smn.Session_id='" + ViewState["Session_id"].ToString() + "' and smn.Section='" + ViewState["Section"].ToString() + "' order by sm.Subject_position");
            }
        }
        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        My mycode1 = new My();
        private void Bind_data()
        {
            string query = "";
            if (ddl_term.SelectedItem.Text == "ALL" && ddl_subject.SelectedItem.Text == "ALL")
            {
                query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=scs.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=scs.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName,isnull((select top 1 Status from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='A'),'Incomplete') as Status,isnull((select top 1 Remarks from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='" + ViewState["Section"].ToString() + "'),'NA') as Remarks from Syllubsh_Chapter_SubChapter scs  where scs.Subject_id in (select Sub_id from Subject_Mapping_New where Session_id=scs.Session_id and Class_id=scs.Class_id and Sub_id=scs.Subject_id and Admission_no='"+ ViewState["regid"].ToString() + "') and  scs.Session_id='" + ViewState["Session_id"].ToString() + "' and scs.Class_id='" + ViewState["class_id"].ToString() + "' order by scs.Position asc";

            }
            else if (ddl_term.SelectedItem.Text != "ALL" && ddl_subject.SelectedItem.Text == "ALL")
            {
                query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=scs.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=scs.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName,isnull((select top 1 Status from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='A'),'Incomplete') as Status,isnull((select top 1 Remarks from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='" + ViewState["Section"].ToString() + "'),'NA') as Remarks from Syllubsh_Chapter_SubChapter scs  where scs.Subject_id in (select Sub_id from Subject_Mapping_New where Session_id=scs.Session_id and Class_id=scs.Class_id and Sub_id=scs.Subject_id and Admission_no='" + ViewState["regid"].ToString() + "') and scs.Session_id='" + ViewState["Session_id"].ToString() + "' and scs.Class_id='" + ViewState["class_id"].ToString() + "' and scs.Term_id='" + ddl_term.SelectedValue + "' order by scs.Position asc";
            }
            else if (ddl_term.SelectedItem.Text == "ALL" && ddl_subject.SelectedItem.Text != "ALL")
            {
                query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=scs.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=scs.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName,isnull((select top 1 Status from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='A'),'Incomplete') as Status,isnull((select top 1 Remarks from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='" + ViewState["Section"].ToString() + "'),'NA') as Remarks from Syllubsh_Chapter_SubChapter scs  where scs.Subject_id in (select Sub_id from Subject_Mapping_New where Session_id=scs.Session_id and Class_id=scs.Class_id and Sub_id=scs.Subject_id and Admission_no='" + ViewState["regid"].ToString() + "') and  scs.Session_id='" + ViewState["Session_id"].ToString() + "' and scs.Class_id='" + ViewState["class_id"].ToString() + "' and scs.Subject_id='" + ddl_subject.SelectedValue + "' order by scs.Position asc";
            }
            else if (ddl_term.SelectedItem.Text != "ALL" && ddl_subject.SelectedItem.Text != "ALL")
            {
                query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=scs.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=scs.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName,isnull((select top 1 Status from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='A'),'Incomplete') as Status,isnull((select top 1 Remarks from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='" + ViewState["Section"].ToString() + "'),'NA') as Remarks from Syllubsh_Chapter_SubChapter scs  where scs.Subject_id in (select Sub_id from Subject_Mapping_New where Session_id=scs.Session_id and Class_id=scs.Class_id and Sub_id=scs.Subject_id and Admission_no='" + ViewState["regid"].ToString() + "') and  scs.Session_id='" + ViewState["Session_id"].ToString() + "' and scs.Class_id='" + ViewState["class_id"].ToString() + "' and scs.Term_id='" + ddl_term.SelectedValue + "' and scs.Subject_id='" + ddl_subject.SelectedValue + "' order by scs.Position asc";
            }
            else
            {
                query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=scs.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=scs.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName,isnull((select top 1 Status from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='A'),'Incomplete') as Status,isnull((select top 1 Remarks from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=scs.Session_id and Term_id=scs.Term_id and Class_id=scs.Class_id and Subject_id=scs.Subject_id and Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and Section='" + ViewState["Section"].ToString() + "'),'NA') as Remarks from Syllubsh_Chapter_SubChapter scs  where scs.Subject_id in (select Sub_id from Subject_Mapping_New where Session_id=scs.Session_id and Class_id=scs.Class_id and Sub_id=scs.Subject_id and Admission_no='" + ViewState["regid"].ToString() + "') and  scs.Session_id='" + ViewState["Session_id"].ToString() + "' and scs.Class_id='" + ViewState["class_id"].ToString() + "' order by scs.Position asc";
            }

            DataTable dt = mycode1.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry there are no data list exist");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
            } 
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bind_data();
        }
    }
}