using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.principle_profile
{
    public partial class make_hostel_attendance : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    try
                    {
                        ViewState["sessionid"] = My.get_session_id();
                        code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details where use_mode='1'");
                        ddl_session.SelectedValue = code.get_session_id(code.get_session());
                        code.bind_all_ddl_with_id(ddl_house, "select house_name,house_id from house_master order by house_name asc");
                        txt_date.Text = code.date();
                    }
                    catch
                    {
                    }
                }
            }
        }


        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }



        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_house.SelectedItem.Text == "Select")
            {
                Alert("Please select house");
            }
            else if (txt_date.Text == "")
            {
                Alert("Please select date");
            }
            else
            {
                finally_open_student_list();
            }
        }

        private void finally_open_student_list()
        {
            string query = "Select Class_id,Section,admissionserialnumber,studentname,rollnumber,CASE WHEN (studentimagepath is null or studentimagepath='') THEN '/images/dummy-student.jpg'  WHEN studentimagepath is not null THEN studentimagepath END AS Student_img,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,father_mob,Father_whatsApp_no   from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and hosteltaken='Yes' and house='" + ddl_house.SelectedValue + "' and status='1' and (Transfer_Status='New' or Transfer_Status='NT') order by rollnumber asc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                Alert("Student not found.");
                GrdView.DataSource = null;
                GrdView.DataBind();
                grid111.Visible = false;
            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
                grid111.Visible = true;
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Bind_option1((RadioButtonList)e.Row.FindControl("RadioButtonList1"));
                Label lbl_reg_id = (Label)e.Row.FindControl("lbl_reg_id");
                Label lbl_class_id = (Label)e.Row.FindControl("lbl_class_id");
                Label lbl_section = (Label)e.Row.FindControl("lbl_section");
                RadioButtonList rb = (RadioButtonList)e.Row.FindControl("RadioButtonList1");

                bind_if_answerd(rb, lbl_reg_id.Text, lbl_class_id.Text, lbl_section.Text);
            }
        }

        private void Bind_option1(RadioButtonList radioButtonList)
        {
            String strConnString = connection.conn;

            using (SqlConnection con = new SqlConnection(strConnString))
            {

                string query = @"select Typeid,Type from Type_of_Attendance";


                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    radioButtonList.DataSource = cmd.ExecuteReader();
                    radioButtonList.DataTextField = "Type";
                    radioButtonList.DataValueField = "Typeid";
                    //radioButtonList.CssClass = "Type";
                    radioButtonList.DataBind();

                    //radioButtonList.SelectedItem.Value = "1";
                    con.Close();
                }
            }
        }
        protected void RadioButtonList1_DataBound(object sender, EventArgs e)
        {
            RadioButtonList list = (RadioButtonList)sender;

        }

        private void bind_if_answerd(RadioButtonList rb, string admission, string class_id, string section)
        {
            string day = code.getdayname(txt_date.Text);
            string query = "Select * from Student_attendance_saved_hostel_Wise where House_id='" + ddl_house.SelectedValue + "' and Session_id='" + ddl_session.SelectedValue + "' and Admission_no='" + admission + "' and Attendance_Date='" + txt_date.Text + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                rb.SelectedValue = "1";
            }
            else
            {
                if (dt.Rows[0]["Attendance_Status"].ToString() == "Present")
                {
                    rb.SelectedValue = "1";
                }
                else if (dt.Rows[0]["Attendance_Status"].ToString() == "Absent")
                {
                    rb.SelectedValue = "2";
                }
                else if (dt.Rows[0]["Attendance_Status"].ToString() == "Leave")
                {
                    rb.SelectedValue = "3";
                }
            }
        }

        protected void btn_save_all_Click(object sender, EventArgs e)
        {
            string student_attendance = "Absent";
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (txt_date.Text == "")
            {
                Alert("Please select date");
            }
            else if (ddl_house.SelectedItem.Text == "Select")
            {
                Alert("Please select house");
            }
            else
            {
                int growcount = GrdView.Rows.Count;
                int k = 0;

                for (int i = 0; i < growcount; i++)
                {
                    RadioButtonList RadioButtonList1 = (RadioButtonList)GrdView.Rows[i].FindControl("RadioButtonList1") as RadioButtonList;
                    if (RadioButtonList1.SelectedItem.Text == "P")
                    {
                        student_attendance = "Present";
                    }

                    else if (RadioButtonList1.SelectedItem.Text == "A")
                    {
                        student_attendance = "Absent";
                    }

                    else if (RadioButtonList1.SelectedItem.Text == "L")
                    {
                        student_attendance = "Leave";
                    }
                    Label lbl_reg_id = (Label)GrdView.Rows[i].FindControl("lbl_reg_id");
                    Label lbl_FullName = (Label)GrdView.Rows[i].FindControl("lbl_FullName");
                    Label lbl_father_mob = (Label)GrdView.Rows[i].FindControl("lbl_father_mob");
                    Label lbl_Father_whatsApp_no = (Label)GrdView.Rows[i].FindControl("lbl_Father_whatsApp_no");
                    Label lbl_classname = (Label)GrdView.Rows[i].FindControl("lbl_classname");

                    Label lbl_class_id = (Label)GrdView.Rows[i].FindControl("lbl_class_id");
                    Label lbl_section = (Label)GrdView.Rows[i].FindControl("lbl_section");

                    finalattendance(student_attendance, lbl_reg_id.Text, lbl_FullName.Text, lbl_father_mob.Text, lbl_Father_whatsApp_no.Text, lbl_classname.Text, lbl_class_id.Text, lbl_section.Text);
                    k++;
                }
                Alert("Attendance has been saved successfully.");
                finally_open_student_list();
            }
        }

        private void finalattendance(string student_attendance, string admission, string studentname, string mobilesms, string whatsappno, string classname, string class_id, string section)
        {
            string message = "";
            string msge1 = "";
            if (student_attendance == "Present")
            {
                msge1 = "Present"; //= "Dear Parent, Your child is in the school and is present in the class today. Date:-" + txt_date.Text;
            }
            else if (student_attendance == "Absent")
            {
                msge1 = "Absent";// msge1 = "Dear Parents your child has not in the school, and absent in the class. Date:-" + txt_date.Text;
            }
            else if (student_attendance == "Leave")
            {
                msge1 = "Leave";//msge1 = "Dear Parents your child has not in the school due to leave. Date:-" + txt_date.Text;
            }

            My mycode = new My();
            string day = code.getdayname(txt_date.Text);
            string query = "Select * from Student_attendance_saved_hostel_Wise where House_id='" + ddl_house.SelectedValue + "' and Session_id='" + ddl_session.SelectedValue + "' and  Admission_no='" + admission + "'  and Attendance_Date='" + txt_date.Text + "'  ";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                string strQuery = "INSERT INTO Student_attendance_saved_hostel_Wise (Session_id,Class_id,Section,Class_period,Day,Attendance_Date,Attendance_IDate,Date,Idate,time,Created_By,Attendance_Status,Admission_no,House_id) values (@Session_id,@Class_id,@Section,@Class_period,@Day,@Attendance_Date,@Attendance_IDate,@Date,@Idate,@time,@Created_By,@Attendance_Status,@Admission_no,@House_id)";
                SqlCommand cmd;
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Section", section);
                cmd.Parameters.AddWithValue("@Class_period", "0");
                cmd.Parameters.AddWithValue("@Day", day);
                cmd.Parameters.AddWithValue("@Attendance_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Attendance_IDate", code.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@time", code.time());
                cmd.Parameters.AddWithValue("@Created_By", ViewState["teacher"].ToString());
                cmd.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd.Parameters.AddWithValue("@Admission_no", admission);
                cmd.Parameters.AddWithValue("@House_id", ddl_house.SelectedValue);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                string strQuery = "Update Student_attendance_saved_hostel_Wise set Updated_Date=@Updated_Date,Updated_IDate=@Updated_IDate,Updated_Time=@Updated_Time,Updated_By=@Updated_By,Attendance_Status=@Attendance_Status where Id = @Id";
                SqlCommand cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Updated_Date", code.date());
                cmd1.Parameters.AddWithValue("@Updated_IDate", code.idate());
                cmd1.Parameters.AddWithValue("@Updated_Time", code.time());
                cmd1.Parameters.AddWithValue("@Updated_By", ViewState["teacher"].ToString());
                cmd1.Parameters.AddWithValue("@Attendance_Status", student_attendance);
                cmd1.Parameters.AddWithValue("@Id", id);
                if (InsertUpdate.InsertUpdateData(cmd1))
                {
                }
            }
        }
    }
}