using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Student_Attendance_Chart_Month_Wise : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
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
                    ViewState["Admin"] = Session["Admin"].ToString();
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details  ");
                    code.bind_ddl_month(ddl_month);
                    string monthval = code.getval();
                    ddl_month.SelectedValue = monthval;
                    ddl_session.SelectedValue = code.get_session_id_use();
                    code.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                }

            }
        }
        public void Alert(string Message)
        {
            lblmessage.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        onlinetest online = new onlinetest();
        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else
            {
                online.bind_all_ddl_with_id_new(ddl_exam_activity, "Select Exam_Activity_Name, Exam_Activity_Id from Exam_Activity_Master where Session_id=" + ddl_session.SelectedValue + "  ");
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

                code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");

            }
        }



        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_reg_id = (Label)e.Row.FindControl("lbl_reg_id");
                Label lbl_date1 = (Label)e.Row.FindControl("lbl_date1");
                Label lbl_date2 = (Label)e.Row.FindControl("lbl_date2");
                Label lbl_date3 = (Label)e.Row.FindControl("lbl_date3");
                Label lbl_date4 = (Label)e.Row.FindControl("lbl_date4");
                Label lbl_date5 = (Label)e.Row.FindControl("lbl_date5");
                Label lbl_date6 = (Label)e.Row.FindControl("lbl_date6");
                Label lbl_date7 = (Label)e.Row.FindControl("lbl_date7");
                Label lbl_date8 = (Label)e.Row.FindControl("lbl_date8");
                Label lbl_date9 = (Label)e.Row.FindControl("lbl_date9");
                Label lbl_date10 = (Label)e.Row.FindControl("lbl_date10");
                Label lbl_date11 = (Label)e.Row.FindControl("lbl_date11");
                Label lbl_date12 = (Label)e.Row.FindControl("lbl_date12");
                Label lbl_date13 = (Label)e.Row.FindControl("lbl_date13");
                Label lbl_date14 = (Label)e.Row.FindControl("lbl_date14");
                Label lbl_date15 = (Label)e.Row.FindControl("lbl_date15");
                Label lbl_date16 = (Label)e.Row.FindControl("lbl_date16");
                Label lbl_date17 = (Label)e.Row.FindControl("lbl_date17");
                Label lbl_date18 = (Label)e.Row.FindControl("lbl_date18");
                Label lbl_date19 = (Label)e.Row.FindControl("lbl_date19");
                Label lbl_date20 = (Label)e.Row.FindControl("lbl_date20");
                Label lbl_date21 = (Label)e.Row.FindControl("lbl_date21");
                Label lbl_date22 = (Label)e.Row.FindControl("lbl_date22");
                Label lbl_date23 = (Label)e.Row.FindControl("lbl_date22");
                Label lbl_date24 = (Label)e.Row.FindControl("lbl_date24");
                Label lbl_date25 = (Label)e.Row.FindControl("lbl_date25");
                Label lbl_date26 = (Label)e.Row.FindControl("lbl_date26");
                Label lbl_date27 = (Label)e.Row.FindControl("lbl_date27");
                Label lbl_date28 = (Label)e.Row.FindControl("lbl_date28");
                Label lbl_date29 = (Label)e.Row.FindControl("lbl_date29");
                Label lbl_date30 = (Label)e.Row.FindControl("lbl_date30");
                Label lbl_date31 = (Label)e.Row.FindControl("lbl_date31");
                string year = code.get_firstyear(ddl_session.SelectedItem.Text);
                string date = "01" + "/" + ddl_month.SelectedValue + "/" + year;
                string day = code.getdayname(date);

                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date1);

                date = "02" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date2);

                date = "03" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);

                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date3);

                date = "04" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date4);


                date = "05" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date5);


                date = "06" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date6);


                date = "07" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date7);


                date = "08" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date8);


                date = "09" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date9);


                date = "10" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date10);


                date = "11" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date11);


                date = "12" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date12);


                date = "13" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date13);


                date = "14" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date14);

                date = "15" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date15);

                date = "16" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date16);


                date = "17" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date17);

                date = "18" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date18);

                date = "19" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date19);

                date = "20" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date20);

                date = "21" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date21);


                date = "22" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date22);


                date = "23" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date23);

                date = "24" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date24);


                date = "25" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date25);


                date = "26" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date26);


                date = "27" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date27);


                date = "28" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date28);



                date = "29" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date29);


                date = "30" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date30);


                date = "31" + "/" + ddl_month.SelectedValue + "/" + year;
                day = code.getdayname(date);
                Bind_monthstatus(lbl_reg_id.Text, date, day, lbl_date31);
            }
        }

        private void Bind_monthstatus(string reg_id, string date, string day, Label lbl_date1)
        {
            string query = " Select  Type    from School_Holiday_Calendar   where Date='" + date + "'       ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                lbl_date1.Text = "N/A";
            }
            else
            {
                if (dt.Rows[0]["Type"].ToString().ToUpper() == "CLASS")
                {
                    bind_classstatus(reg_id, date, day, lbl_date1);


                }
                else if (dt.Rows[0]["Type"].ToString().ToUpper() == "CL")
                {
                    bind_classstatus(reg_id, date, day, lbl_date1);


                }


                else
                {
                    lbl_date1.Text = dt.Rows[0]["Type"].ToString();
                }

            }






        }

        private void bind_classstatus(string reg_id, string date, string day, Label lbl_date1)
        {
            string query = " Select    count(Id) from Class_Routine_Master   where  Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and Day='" + day + "'    ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                lbl_date1.Text = "N/A";
            }
            else
            {



                int noofclass = Convert.ToInt32(dt.Rows[0][0].ToString());
                int noof_studenttackenclass = find_nostudentclass(day, date, reg_id);

                if (noof_studenttackenclass == 0)
                {
                    lbl_date1.Text = "--";
                }
                else
                {
                    if (noofclass == noof_studenttackenclass)
                    {
                        lbl_date1.Text = "P";
                    }
                    else
                    {
                        lbl_date1.Text = "A";
                    }
                }


            }
        }

        private int find_nostudentclass(string day, string date, string reg_id)
        {
            string query = " Select    count(Id) from Student_Attendance_saved   where  Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and Attendance_Date='" + date + "' and Admission_no='" + reg_id + "' and Attendance_Status='Present'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }

        }

        protected void imgexcel2_Click(object sender, ImageClickEventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=AttendanceExport" + "_" + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GrdView.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        #region find data
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            if (ddl_exam_activity.SelectedItem.Text == "Select")
            {
                Alert("Please select session tracking head for exam");
            }
                
            else if (ddl_month.Text == "Select")
            {
                Alert("Please select month");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else
            {
                string query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section,(select top 1 CategoryName from ClassMaster where CategoryID=ar.Class_id) as classname,ar.studentname from admission_registor ar  where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "'   order by ar.rollnumber";
                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    btn_save.Visible = false;
                    imgexcel2.Visible = false;
                    GrdView.DataSource = null;
                    GrdView.DataBind();
                    grid111.Visible = false;
                    lbltotal_student.Text = "0";
                    lbl_persenstudent.Text = "0";
                    lbl_totalabsentstudent.Text = "0";

                }
                else
                {
                    btn_save.Visible = true;
                    imgexcel2.Visible = true;
                    lbltotal_student.Text = dt.Rows.Count.ToString();
                    GrdView.DataSource = dt;
                    GrdView.DataBind();
                    grid111.Visible = true;

                    // bind_student_count();



                }
            }
        }

        private void bind_student_count()
        {

        }
        #endregion

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {



                int year = Convert.ToInt32(code.get_firstyear(ddl_session.SelectedItem.Text));
                if (GrdView.Rows.Count == 0)
                {

                }
                else
                {
                    for (int i = 0; i < GrdView.Rows.Count; i++)
                    {
                        Label lbl_reg_id = (Label)GrdView.Rows[i].FindControl("lbl_reg_id");
                        Label lbl_date1 = (Label)GrdView.Rows[i].FindControl("lbl_date1");
                        Label lbl_date2 = (Label)GrdView.Rows[i].FindControl("lbl_date2");
                        Label lbl_date3 = (Label)GrdView.Rows[i].FindControl("lbl_date3");
                        Label lbl_date4 = (Label)GrdView.Rows[i].FindControl("lbl_date4");
                        Label lbl_date5 = (Label)GrdView.Rows[i].FindControl("lbl_date5");
                        Label lbl_date6 = (Label)GrdView.Rows[i].FindControl("lbl_date6");
                        Label lbl_date7 = (Label)GrdView.Rows[i].FindControl("lbl_date7");
                        Label lbl_date8 = (Label)GrdView.Rows[i].FindControl("lbl_date8");
                        Label lbl_date9 = (Label)GrdView.Rows[i].FindControl("lbl_date9");
                        Label lbl_date10 = (Label)GrdView.Rows[i].FindControl("lbl_date10");
                        Label lbl_date11 = (Label)GrdView.Rows[i].FindControl("lbl_date11");
                        Label lbl_date12 = (Label)GrdView.Rows[i].FindControl("lbl_date12");
                        Label lbl_date13 = (Label)GrdView.Rows[i].FindControl("lbl_date13");
                        Label lbl_date14 = (Label)GrdView.Rows[i].FindControl("lbl_date14");
                        Label lbl_date15 = (Label)GrdView.Rows[i].FindControl("lbl_date15");
                        Label lbl_date16 = (Label)GrdView.Rows[i].FindControl("lbl_date16");
                        Label lbl_date17 = (Label)GrdView.Rows[i].FindControl("lbl_date17");
                        Label lbl_date18 = (Label)GrdView.Rows[i].FindControl("lbl_date18");
                        Label lbl_date19 = (Label)GrdView.Rows[i].FindControl("lbl_date19");
                        Label lbl_date20 = (Label)GrdView.Rows[i].FindControl("lbl_date20");
                        Label lbl_date21 = (Label)GrdView.Rows[i].FindControl("lbl_date21");
                        Label lbl_date22 = (Label)GrdView.Rows[i].FindControl("lbl_date22");
                        Label lbl_date23 = (Label)GrdView.Rows[i].FindControl("lbl_date23");
                        Label lbl_date24 = (Label)GrdView.Rows[i].FindControl("lbl_date24");
                        Label lbl_date25 = (Label)GrdView.Rows[i].FindControl("lbl_date25");
                        Label lbl_date26 = (Label)GrdView.Rows[i].FindControl("lbl_date26");
                        Label lbl_date27 = (Label)GrdView.Rows[i].FindControl("lbl_date27");
                        Label lbl_date28 = (Label)GrdView.Rows[i].FindControl("lbl_date28");
                        Label lbl_date29 = (Label)GrdView.Rows[i].FindControl("lbl_date29");
                        Label lbl_date30 = (Label)GrdView.Rows[i].FindControl("lbl_date30");
                        Label lbl_date31 = (Label)GrdView.Rows[i].FindControl("lbl_date31");

                        int days = DateTime.DaysInMonth(year, Convert.ToInt32(ddl_month.SelectedValue));

                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = "sp_Student_attendence_chart_column_wise";
                        cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Admission_no", lbl_reg_id.Text);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@month", ddl_month.SelectedValue);
                        cmd.Parameters.AddWithValue("@_01", lbl_date1.Text);
                        cmd.Parameters.AddWithValue("@_02", lbl_date2.Text);
                        cmd.Parameters.AddWithValue("@_03", lbl_date3.Text);
                        cmd.Parameters.AddWithValue("@_04", lbl_date4.Text);
                        cmd.Parameters.AddWithValue("@_05", lbl_date5.Text);
                        cmd.Parameters.AddWithValue("@_06", lbl_date6.Text);
                        cmd.Parameters.AddWithValue("@_07", lbl_date7.Text);
                        cmd.Parameters.AddWithValue("@_08", lbl_date8.Text);
                        cmd.Parameters.AddWithValue("@_09", lbl_date9.Text);
                        cmd.Parameters.AddWithValue("@_10", lbl_date10.Text);
                        cmd.Parameters.AddWithValue("@_11", lbl_date11.Text);
                        cmd.Parameters.AddWithValue("@_12", lbl_date12.Text);
                        cmd.Parameters.AddWithValue("@_13", lbl_date13.Text);
                        cmd.Parameters.AddWithValue("@_14", lbl_date14.Text);
                        cmd.Parameters.AddWithValue("@_15", lbl_date15.Text);
                        cmd.Parameters.AddWithValue("@_16", lbl_date16.Text);
                        cmd.Parameters.AddWithValue("@_17", lbl_date17.Text);
                        cmd.Parameters.AddWithValue("@_18", lbl_date18.Text);
                        cmd.Parameters.AddWithValue("@_19", lbl_date19.Text);
                        cmd.Parameters.AddWithValue("@_20", lbl_date20.Text);
                        cmd.Parameters.AddWithValue("@_21", lbl_date21.Text);
                        cmd.Parameters.AddWithValue("@_22", lbl_date22.Text);
                        cmd.Parameters.AddWithValue("@_23", lbl_date23.Text);
                        cmd.Parameters.AddWithValue("@_24", lbl_date24.Text);
                        cmd.Parameters.AddWithValue("@_25", lbl_date25.Text);
                        cmd.Parameters.AddWithValue("@_26", lbl_date26.Text);
                        cmd.Parameters.AddWithValue("@_27", lbl_date27.Text);
                        cmd.Parameters.AddWithValue("@_28", lbl_date28.Text);
                        cmd.Parameters.AddWithValue("@_29", lbl_date29.Text);
                        cmd.Parameters.AddWithValue("@_30", lbl_date30.Text);
                        cmd.Parameters.AddWithValue("@_31", lbl_date31.Text);
                        cmd.Parameters.AddWithValue("@Save_date", code.date());
                        cmd.Parameters.AddWithValue("@Save_Idate", code.idate());
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Admin"].ToString());
                        cmd.Parameters.AddWithValue("@days", days);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@Exam_Activity_Id", ddl_exam_activity.SelectedValue);
                        if (UsesCode.InsertUpdateData_sp(cmd))
                        {

                        }
                    }

                    Alert("Attendance saved successfully");
                }
            }
            catch(Exception ex)
            {
                UsesCode.submitexception1(ex);
            }
        }

       
    }
}