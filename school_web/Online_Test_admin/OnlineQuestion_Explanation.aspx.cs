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
    public partial class OnlineQuestion_Explanation : System.Web.UI.Page
    {
        My m1 = new My();

        protected void Page_Load(object sender, EventArgs e)
        {


            string testid = Request.QueryString["testid"].ToString();
           
          
            //Session["mode"]
            if (!IsPostBack)
            {
                try
                {
                    //string id = Request.QueryString["id"];
                    string id = testid;
                    ViewState["modepage"] = "web";
                    print1.Visible = true;
                   
                    if (!String.IsNullOrEmpty(id))
                    {
                        string Testid = id;

                        fill_data(Testid);
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }

        private void fill_data(string testid)
        {

            string query = "";

            query = "Select qi.test_id,Question_no,qi.questionid,Question_name,Question_name_HN,qi.Section,Explanation_en,Explanation_hn from question_info qi  join Question_Explanation qe on qi.questionid=qe.questionid where qi.test_id=" + testid + "     order by  cast (Question_no as int) ASC";//and qe.Language_Itype='0'
            bind_data_on_english(query);
            bind_testname(testid);
        }

        private void bind_testname(string testid)
        {
            string query = "select Exam_name from OlineTest_Exam_name  where Exam_id='" + testid + "'  ";
            DataTable dt = new DataTable();
            dt = m1.featch_data(query);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {


                lbl_testname.Text = dt.Rows[0]["Exam_name"].ToString();
            }
        }

        private void bind_data_on_english(string query)
        {
            DataTable dt = new DataTable();
            dt = m1.featch_data(query);
            if (dt.Rows.Count == 0)
            {
                grd_view_english.DataSource = null;
                grd_view_english.DataBind();
                panel_english.Visible = false;
            }
            else
            {
                grd_view_english.DataSource = dt;
                grd_view_english.DataBind();
                panel_english.Visible = true;
            }


        }

        protected void print1_Click(object sender, EventArgs e)
        {
            string url = "";
            try
            {

                url = "Student_Attempted_Exam.aspx";


            }
            catch
            {

            }


            Response.Redirect(url, false);
        }
    }
}