using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class Test_Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                hd_userid.Value = Session["User"].ToString(); ;
                Session["student"] = hd_userid.Value;
                string testid = Request.QueryString["tstid"];
                if (!String.IsNullOrEmpty(testid))
                {
                    hd_testid.Value = testid;
                    hd_entry_id.Value = "0";
                    hd_package_id.Value = "0";
                    fetcha_test_list();
                }
                else
                {
                    Response.Redirect("Take_an_Online_Test.aspx", false);
                }

            }
        }
        My mycode = new My();


        private void fetcha_test_list()
        {
            string query = "Select Section,Exam_name,Exam_duration ,(Select top 1 created_date from question_info where test_id='" + hd_testid.Value + "' ) created_date1,(Select top 1 Create_time from question_info where test_id='" + hd_testid.Value + "' ) Create_time1,(Select top 1 created_by from question_info where test_id='" + hd_testid.Value + "' ) created_by1,(Select count(Id) from question_info where test_id='" + hd_testid.Value + "' and Uploding_status='Administrator' ) as noofquestion,(select top 1 Subject_name from Subject_Master where Subject_id=OlineTest_Exam_name.subjectname) as subjectname,(select top 1 Course_Name from Add_course_table where course_id=OlineTest_Exam_name.Class_id) as Course_Name   from OlineTest_Exam_name where Exam_id='" + hd_testid.Value + "'  ";

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {
                img1.Visible = true;
                img1.ImageUrl = "../Online_Test_admin/Doc/testseting.png";
                lbl_testname.Text = dt.Rows[0]["Exam_name"].ToString();
                // lbl_generalinfo.Text = //dt.Rows[0][1].ToString();
                lbl_no_of_question.Text = dt.Rows[0]["noofquestion"].ToString();
                if (dt.Rows[0]["subjectname"].ToString() == "0")
                {
                    lbl_subject.Text = "All Subject";
                }
                else if (dt.Rows[0]["subjectname"].ToString() == "")
                {
                    lbl_subject.Text = "All Subject";
                }
                else
                {
                    lbl_subject.Text = dt.Rows[0]["subjectname"].ToString();
                }
                lbl_tot_time.Text = dt.Rows[0]["Exam_duration"].ToString();
                lbl_tot_time_type.Text = "Minutes";

            }
        }


        My code = new My();
        protected void btn_s_add_Click(object sender, EventArgs e)
        {

            Session["student"] = hd_userid.Value;
            string testmodecode = "101";//code.fetch_sectioniatestid(hd_testid.Value);
            string examcode = hd_testid.Value; //code.fetch_code_via_testcode(hd_testid.Value);
            string examtype_code1 = "4";//code.fetch_exam_type_code_via_testid_testmodecode_examcode(hd_testid.Value, testmodecode, examcode);
            //string Test_id = code.Zip(hd_testid.Value);
            //string Examcode = code.Zip(examcode);
            //string examtype_code = code.Zip(examtype_code1);
            //string testmode = code.Zip(testmodecode);
            //string studentid = code.Zip(hd_userid.Value);
            //string language = code.Zip(ddl_language.Text);


            string Test_id = hd_testid.Value;
            string Examcode = examcode;
            string examtype_code = examtype_code1;
            string testmode = testmodecode;
            string studentid = hd_userid.Value;
            string language = ddl_language.Text;

            string examtype = My.find_examtype(examtype_code1);
            Session["Test_id"] = hd_testid.Value;
            Session["Entry_no"] = hd_entry_id.Value;
            Session["Packageid"] = hd_package_id.Value;
            Session["exam_code"] = examcode;
            Session["examtype_code"] = examtype_code1;
            Session["Examtype"] = examtype;
            Session["language"] = ddl_language.Text;
            Session["examname"] = lbl_testname.Text; //find_examname(hd_testid.Value);
            HttpRequest httpRequest = HttpContext.Current.Request;
            //if (httpRequest.Browser.IsMobileDevice)
            //{
            //    string path = "webview/Your_testM.aspx?studentid=" + Uri.EscapeDataString(studentid) + "&testid=" + Uri.EscapeDataString(Test_id) + "&Examcode=" + Uri.EscapeDataString(Examcode) + "&examtype_code=" + Uri.EscapeDataString(examtype_code) + "&testmode=" + Uri.EscapeDataString(testmode) + "&language=" + Uri.EscapeDataString(language) + "&Entryno=" + Uri.EscapeDataString(hd_entry_id.Value) + "&Packageid=" + Uri.EscapeDataString(hd_package_id.Value) + "&examtype=" + Uri.EscapeDataString(examtype) + "&examname=" + Uri.EscapeDataString(Session["examname"].ToString())+"&mode=web";
            //    Response.Redirect(path, false);
            //}
            //else
            //{
            string path = "webview/Your_testM.aspx?studentid=" + Uri.EscapeDataString(studentid) + "&testid=" + Uri.EscapeDataString(Test_id) + "&Examcode=" + Uri.EscapeDataString(Examcode) + "&examtypecode=" + Uri.EscapeDataString(examtype_code) + "&testmode=" + Uri.EscapeDataString(testmode) + "&language=" + Uri.EscapeDataString(language) + "&Entryno=" + Uri.EscapeDataString(hd_entry_id.Value) + "&Packageid=" + Uri.EscapeDataString(hd_package_id.Value) + "&examtype=" + Uri.EscapeDataString(examtype) + "&examname=" + Uri.EscapeDataString(Session["examname"].ToString()) + "&mode=web";
            Response.Redirect(path, false);
            //}



        }
    }
}