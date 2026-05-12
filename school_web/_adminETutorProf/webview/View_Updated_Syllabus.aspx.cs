using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web._adminETutorProf.webview
{
    public partial class View_Updated_Syllabus : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
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
                        if (Session["msG"] != null)
                        {
                            Alert(Session["msG"].ToString());
                            Session["msG"] = null;
                        }
                        txt_date.Text = mycode.date();
                        txt_enddate.Text = mycode.date();

                        Bind_data();
                    }
                }
                catch
                {

                }
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
            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
            {
                string query = "Select sc.* ,scs.Chapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=sc.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=sc.Class_id and Subject_id=scs.Subject_id) as Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher sc join Syllubsh_Chapter_SubChapter scs on sc.Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and sc.Session_id=scs.Session_id where sc.Teacher_id='" + ViewState["regid"].ToString() + "' and sc.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and sc.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' order by sc.idate asc";
                DataTable dt = mycode1.FillData(query);
                 if (dt.Rows.Count == 0)
                 {
                     Alert("Sorry there are no data list exist");
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                 }
                 else
                 {
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                 }
            }
            else
            {
                Alert("Please select valid date ");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bind_data();
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Delete from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Id='" + lbl_id.Text + "'");
                My.InsertUpdateData(cmd);
                Bind_data();
                Alert("Recode has been successfully deleted.");
            }
            catch (Exception ex) { My.submitexception(ex.ToString()); }
            
          

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Response.Redirect("Updated_Syllabus_Chapter.aspx?regid="+ ViewState["regid"].ToString() + "&Id=" + lbl_id.Text, false);
            }
            catch
            {

            }
        }
    }
}