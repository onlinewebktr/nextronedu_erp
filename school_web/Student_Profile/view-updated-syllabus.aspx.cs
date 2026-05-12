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
    public partial class view_updated_syllabus : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        Bind_student_details();
                        Bind_data();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
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
                Alertme("Something is wrong", "warning");
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
                mycode.bind_all_ddl_with_id(ddl_subject, "Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["class_id"].ToString() + "' and smn.Session_id='" + ViewState["Session_id"].ToString() + "' and smn.Section='" + ViewState["Section"].ToString() + "' order by sm.Subject_position");
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

        My mycode1 = new My();
        private void Bind_data()
        {
            string query = "";
            if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alertme("Please select valid subject", "Warning");
            }
            else
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)))
                {
                    if(ddl_subject.SelectedItem.Text=="ALL")
                    {

                        query = " Select sc.* ,scs.Chapter_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=sc.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=sc.Class_id and Subject_id=scs.Subject_id) as Subject_name from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher sc join Syllubsh_Chapter_SubChapter scs on sc.Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and sc.Session_id=scs.Session_id where   sc.idate>='" + mycode.ConvertStringToiDate(txt_from_date.Text) + "' and sc.idate<='" + mycode.ConvertStringToiDate(txt_to_date.Text) + "' and sc.Class_id='" + ViewState["class_id"].ToString() + "' and sc.Section='" + ViewState["Section"].ToString() + "' and sc.Subject_id in (Select Sub_id from Subject_Mapping_New where Admission_no='"+ ViewState["regid"].ToString() + "' and Session_id='"+ ViewState["Session_id"].ToString() + "' and Class_id='"+ ViewState["class_id"].ToString() + "' ) order by sc.idate asc";
                    }
                    else
                    {

                        query = " Select sc.* ,scs.Chapter_Name,scs.Subchapter_Name,(Select top 1 Course_Name from Add_course_table where course_id=sc.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=sc.Class_id and Subject_id=scs.Subject_id) as Subject_name from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher sc join Syllubsh_Chapter_SubChapter scs on sc.Chapter_and_Subchapter_id=scs.Chapter_and_Subchapter_id and sc.Session_id=scs.Session_id where   sc.idate>='" + mycode.ConvertStringToiDate(txt_from_date.Text) + "' and sc.idate<='" + mycode.ConvertStringToiDate(txt_to_date.Text) + "' and sc.Class_id='" + ViewState["class_id"].ToString() + "' and sc.Section='" + ViewState["Section"].ToString() + "' and sc.Subject_id='" + ddl_subject.SelectedValue + "' and sc.Session_id='" + ViewState["Session_id"].ToString() + "' order by sc.idate asc";

                    }


                    DataTable dt = mycode1.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        Alertme("Sorry there are no data list exist", "warning");
                        rd_view.DataSource = null;
                        rd_view.DataBind();
                    }
                    else
                    {
                        rd_view.DataSource = dt;
                        rd_view.DataBind();
                    }
                }
                else
                {
                    Alertme("Please select valid date", "warning");
                }
            }

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (txt_from_date.Text == "")
            {
                txt_from_date.Focus();
                Alertme("Please choose from date.", "warning");
            }
            else if (txt_to_date.Text == "")
            {
                txt_to_date.Focus();
                Alertme("Please choose from date.", "warning");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                ddl_subject.Focus();
                Alertme("Please select subject.", "warning");
            }
            else
            {
                Bind_data();
            }
        }
    }
}