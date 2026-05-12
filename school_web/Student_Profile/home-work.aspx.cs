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
    public partial class home_work : System.Web.UI.Page
    {
        My imp = new My();
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
                        txt_date.Text = mycode.date();
                        Bind_student_details();
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

        private void get_study_material(string query)
        {
            DataTable dt = mycode.FillData(query);
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
                    ViewState["Branch_id"] = dr["Branch_id"].ToString();
                }
                imp.bind_all_ddl_with_id_cap_All(ddl_subject, "Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["class_id"].ToString() + "' and smn.Session_id='" + ViewState["Session_id"].ToString() + "' and smn.Section='" + ViewState["Section"].ToString() + "' order by sm.Subject_position");

                imp.bind_all_ddl_with_id_cap_All(ddl_teacher, "select DISTINCT t2.name,t2.user_id from Homework_Details t1 join user_details t2 on t1.Upload_By=t2.user_id where t1.Class='" + ViewState["class_id"].ToString() + "' and t1.Section='" + ViewState["Section"].ToString() + "' and t1.Session_id='" + ViewState["Session_id"].ToString() + "'      order by t2.name asc");

                fet_all_data();

            }
        }

        private void fet_all_data()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "'  order by id desc");
        }

        protected void ddl_teacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_teacher.SelectedItem.Text == "ALL")
                {
                    fet_all_data();
                }
                else
                {
                    find_by_teacher();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_teacher()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "' and Upload_By='" + ddl_teacher.SelectedValue + "' order by id desc");
        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_teacher.SelectedItem.Text == "ALL")
                {
                    if (ddl_subject.SelectedItem.Text == "ALL")
                    {
                        fet_all_data();
                    }
                    else
                    {
                        find_by_subject();
                    }
                }
                else
                {
                    if (ddl_subject.SelectedItem.Text == "ALL")
                    {
                        find_by_teacher();
                    }
                    else
                    {
                        find_by_teacher_and_subject();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_teacher_and_subject()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "'  and Upload_By='" + ddl_teacher.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' order by id desc");
        }

        private void find_by_subject()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "'  and Subject='" + ddl_subject.SelectedValue + "' order by id desc");
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_date.Text == "")
                {
                    Alertme("Please choose date,", "warning");
                    txt_date.Focus();
                    return;
                }

                if (ddl_teacher.SelectedItem.Text == "ALL")
                {
                    if (ddl_subject.SelectedItem.Text == "ALL")
                    {
                        fet_all_data_by_date();
                    }
                    else
                    {
                        fet_all_data_by_date_and_subject();
                    }
                }
                else
                {
                    if (ddl_subject.SelectedItem.Text == "ALL")
                    {
                        find_by_teacher_and_date();
                    }
                    else
                    {
                        find_by_teacher_and_subject_and_date();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_teacher_and_subject_and_date()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "'  and Upload_By='" + ddl_teacher.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' and Upload_Idate='" + imp.ConvertStringToiDate(txt_date.Text) + "' order by id desc");
        }

        private void find_by_teacher_and_date()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "'  and Upload_By='" + ddl_teacher.SelectedValue + "' and Upload_Idate='" + imp.ConvertStringToiDate(txt_date.Text) + "' order by id desc");
        }

        private void fet_all_data_by_date_and_subject()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "'  and Subject='" + ddl_subject.SelectedValue + "' and Upload_Idate='" + imp.ConvertStringToiDate(txt_date.Text) + "' order by id desc");
        }

        private void fet_all_data_by_date()
        {
            get_study_material("select *,Format(convert(DateTime,Upload_Date,103), 'MMM') as Month_name,Format(convert(DateTime,Upload_Date,103), 'dd') as Day_name,(select top 1 Subject_name from Subject_Master where Subject_id=Homework_Details.Subject) as Subject_name,(select top 1 name from user_details where user_id=Homework_Details.Upload_By) as Upload_by_name from Homework_Details where Class='" + ViewState["class_id"].ToString() + "' and Section='" + ViewState["Section"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "' and Upload_Idate='" + imp.ConvertStringToiDate(txt_date.Text) + "' order by id desc");
        }
    }
}