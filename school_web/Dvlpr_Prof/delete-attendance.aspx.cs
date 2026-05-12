using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class delete_attendance : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_date.Text = mycode.date();
                    mycode.bind_all_ddl_with_id(ddl_Session, "select Session,session_id from session_details");
                    ddl_Session.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                    ddl_class.SelectedValue = My.get_top_one_class();
                    bind_section();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_section();
            }
            catch (Exception)
            {
            }
        }

        private void bind_section()
        {
            My.bind_ddl_select(ddl_section, "select distinct Section from admission_registor where Session_id='" + ddl_Session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Section asc");
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Session.SelectedItem.Text == "Select")
                {
                    Alert("Please select session.");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class.");
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section.");
                }
                else if (txt_date.Text == "")
                {
                    Alert("Please enter date.");
                }
                else
                {
                    find_attendance();
                }
            }
            catch (Exception ex)
            {
            }
        }


        UsesCode code = new UsesCode();
        private void find_attendance()
        {
            

            find_attendance_grids();
        }

        private void find_attendance_grids()
        {
            string Selectedidate = mycode.idate();
            try
            {
                Selectedidate = My.DateConvertToIdate(txt_date.Text).ToString();
            }
            catch (Exception ex)
            {
            }
            code.BindRepeater("select * from Student_Attendance_saved_Class_Wise where Session_id='" + ddl_Session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Attendance_IDate='" + Selectedidate + "'", RPDetails);
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_dalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Session.SelectedItem.Text == "Select")
                {
                    Alert("Please select session.");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class.");
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alert("Please select section.");
                }
                else if (txt_date.Text == "")
                {
                    Alert("Please enter date.");
                }
                else
                {
                    string Selectedidate = "0";
                    try
                    {
                        Selectedidate = My.DateConvertToIdate(txt_date.Text).ToString();
                    }
                    catch (Exception ex)
                    {
                    }


                    if (Selectedidate.Length == 8)
                    {
                        My.exeSql("Delete from Student_Attendance_saved_Class_Wise where Session_id='" + ddl_Session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Attendance_IDate='" + Selectedidate + "'");
                        find_attendance_grids();
                        Alert("Record has been deleted successfully.");

                        string desc = "Attendance delete for class : " + ddl_class.SelectedItem.Text + " & section : " + ddl_section.SelectedValue + " & date : " + txt_date.Text;
                        log_hostory.delete_log(ddl_Session.SelectedValue, "0", "", "RemoveMenuPermission", desc, "delete-attendance.aspx", "D-Profile");
                    }
                    else
                    {
                        Alert("Please enter correct date.");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}