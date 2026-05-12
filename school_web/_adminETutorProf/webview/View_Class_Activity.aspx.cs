using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using school_web.AppCode;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace school_web._adminETutorProf.webview
{
    public partial class View_Class_Activity : System.Web.UI.Page
    {
        My mycode = new My();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    ViewState["sessionid"] = code.get_session_id_use();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["teacher"].ToString());
                    txt_date.Text = mycode.date();
                    txt_enddate.Text = mycode.date();
                    ViewState["Usertype"] = My.get_user_type(ViewState["teacher"].ToString());
                    if (ViewState["Usertype"].ToString() == "Teacher")
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "'  and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");
                    }
                    else
                    {
                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table  order by Position asc");
                    }


                    Fil_data_pageload();

                }

            }
        }

        private void Fil_data_pageload()
        {
            string query = "";
            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
            {
                if (ViewState["Usertype"].ToString() == "Teacher")
                {
                    query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Teacher_id='" + ViewState["teacher"].ToString() + "' order by acd.idate asc  ";
                    Bind_grid_data(query);
                }
                else
                {
                    query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "'  order by acd.idate asc  ";
                    Bind_grid_data(query);
                }
            }
            else
            {
                Alert("Please select valid date ", "warning");
            }
        }

        private void Bind_grid_data(string query)
        {
            try
            {
                ViewState["query"] = query;
                DataTable dt = code.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alert("Sorry there are no record exist", "warning");
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }
                else
                {
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
            catch
            {

            }

        }

        private void Fil_data()
        {
            string query = "";
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class", "warning");
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section", "warning");
                }
                else
                {
                    if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                    {
                        if (ViewState["Usertype"].ToString() == "Teacher")
                        {
                            query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Subject_id=" + ddl_subject.SelectedValue + " and acd.Section_data='" + ddl_section.SelectedValue + "' and acd.Teacher_id='" + ViewState["teacher"].ToString() + "' order by acd.idate asc  ";
                            if (ddl_subject.SelectedItem.Text == "ALL")
                            {
                                query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Section_data='" + ddl_section.SelectedValue + "' and acd.Teacher_id='" + ViewState["teacher"].ToString() + "' order by acd.idate asc  ";
                            }
                            Bind_grid_data(query);
                        }
                        else
                        {
                            query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Subject_id=" + ddl_subject.SelectedValue + " and acd.Section_data='" + ddl_section.SelectedValue + "' order by acd.idate asc  ";
                            if (ddl_subject.SelectedItem.Text == "ALL")
                            {
                                query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=acd.Class_id) as Course_Name,(Select top 1 Subject_name from Subject_Master where course_id=acd.Class_id and Subject_id=acd.Subject_id) as Subject_name from Activity_Class_Details acd where   acd.idate>='" + mycode.ConvertStringToiDate(txt_date.Text) + "' and acd.idate<='" + mycode.ConvertStringToiDate(txt_enddate.Text) + "' and acd.Session_id=" + ViewState["sessionid"].ToString() + " and acd.Branch_id=" + ViewState["branchid"].ToString() + " and acd.Class_id=" + ddl_class.SelectedValue + " and acd.Section_data='" + ddl_section.SelectedValue + "' order by acd.idate asc  ";
                            }
                            Bind_grid_data(query);

                        }





                    }
                    else
                    {
                        Alert("Please select valid date ", "warning");
                    }
                }
            }
            catch
            {

            }

        }
        private void Alert(string msg, string panel)
        {
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
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

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class", "warning");
            }
            else
            {
                if (ViewState["Usertype"].ToString() == "Teacher")
                {
                    code.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "'   and  UserID='" + ViewState["teacher"].ToString() + "'   order by section");
                }
                else
                {
                    code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'  and  Session_id='" + ViewState["sessionid"] + "'   order by Section");
                }
            }

        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class", "warning");
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section", "warning");
            }
            else
            {
                if (ViewState["Usertype"].ToString() == "Teacher")
                {
                    mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from TeacherCourseSubjectMaping tcsm join  Subject_Master sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id   where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + ddl_class.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id='" + ViewState["sessionid"].ToString() + "' order by sm.Subject_position");
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_subject, "Select   Subject_name, Subject_id from Subject_Master  where course_id='" + ddl_class.SelectedValue + "' order by  Subject_position");
                }
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Fil_data();
        }



        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_attachment")).Text == "")
                    {
                        HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;
                        a1.Visible = false;
                    }
                    else
                    {
                        HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;
                        a1.Visible = true;
                    }



                }
            }
            catch { }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Delete from Activity_Class_Details where Id='" + lbl_id.Text + "'");
                My.InsertUpdateData(cmd);
                Bind_grid_data(ViewState["query"].ToString());
                Alert("Recode has been successfully deleted.", "success");
            }
            catch (Exception ex) { My.submitexception(ex.ToString()); }
        }
    }
}