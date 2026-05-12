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
    public partial class Student_Attendence_Reprot : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
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
                    try
                    {
                        ViewState["teacher"] = Session["teacher"].ToString();
                        txt_startdate.Text = code.date();
                        txt_enddate.Text = code.date();

                        code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')  order by Position asc");


                        code.bind_ddl_all1(dd_section, "Select distinct Section  from section_master  Section  in (select section from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "')       order by Section");
                        search_data();


                    }
                    catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }


                }



            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            search_data();
        }
        private void search_data()
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (txt_startdate.Text == "")
            {
                Alert("Please select start date");
            }
            else if (txt_enddate.Text == "")
            {
                Alert("Please select end date");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                if (Convert.ToInt32(code.ConvertStringToiDate(txt_startdate.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                {

                    if (dd_section.Text == "ALL")
                    {
                        //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "15");
                        cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);

                    }
                    else
                    {

                        //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "14");
                        cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@section", dd_section.Text);

                    }


                    cmd.Parameters.AddWithValue("@startdate", code.ConvertStringToiDate(txt_startdate.Text));
                    cmd.Parameters.AddWithValue("@enddate", code.ConvertStringToiDate(txt_enddate.Text));
                    cmd.Parameters.AddWithValue("@Teacher_Id", ViewState["teacher"]);
                    cmd.CommandText = "sp_VC_class_report";
                    DataTable dt = UsesCode.Getdata_sp(cmd);
                    if (Convert.ToString(dt.Rows.Count) == "0")
                    {
                        lbl_total.Text = "0";
                        Alert("Data Not Available");

                        RpDetailsStudent.DataSource = null;
                        RpDetailsStudent.DataBind();

                        pnl_view.Visible = false;

                    }
                    else
                    {
                        lbl_total.Text = dt.Rows.Count.ToString();
                        pnl_view.Visible = true;


                        RpDetailsStudent.DataSource = dt;
                        RpDetailsStudent.DataBind();
                    }

                }
                else
                {
                    Alert("Please select date valid");
                }


            }
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                code.bind_ddl_all1(dd_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "'   CourseID in(select AssignCourseID from TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "') order by section");
            }

        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

    }
}