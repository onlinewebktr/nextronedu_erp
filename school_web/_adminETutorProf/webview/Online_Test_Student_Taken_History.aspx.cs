using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class Online_Test_Student_Taken_History : System.Web.UI.Page
    {
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


                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table  order by Position asc");
                        txt_date.Text = mycode.date();
                        bind_data();


                    }
                    catch
                    {
                    }
                }
            }
        }

        private void bind_data()
        {
            throw new NotImplementedException();
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "' and Session_id='" + ViewState["sessionid"] + "' order by Section");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                try
                {
                    int Present =0;
                    int Absent = 0;
                    lbltotal_student.Text = "0";
                    lbl_persenstudent.Text = "0";
                    lbl_totalabsentstudent.Text = "0";
                    SqlCommand cmd = new SqlCommand("sp_Student_Test_Attempt_Summary");
                    cmd.Parameters.AddWithValue("@live_date", txt_date.Text);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@classid", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@sp_status", "student_list_a_p");
                    if (ddl_section.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Section", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    }
                   
                    DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        Alert("Sorry, there are no records available.");
                        RPDetails.DataSource = null;
                        RPDetails.DataBind();
                    }
                    else
                    {
                        RPDetails.DataSource = dt;
                        RPDetails.DataBind();
                        lbltotal_student.Text = dt.Rows.Count.ToString();
                      

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];

                            // Use consistent casing for column names
                            string attendanceStatus = dr["attendance_status"].ToString();

                            if (attendanceStatus.Equals("Present", StringComparison.OrdinalIgnoreCase))
                            {
                                Present++;
                            }
                            else if (attendanceStatus.Equals("Absent", StringComparison.OrdinalIgnoreCase))
                            {
                                Absent++;
                            }
                            // No need for an else block if no action is required
                        }

                        lbl_persenstudent.Text = Present.ToString();
                        lbl_totalabsentstudent.Text = Absent.ToString();
                    }

                }
                catch
                {

                }
            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
            
                
                
                Label lbl_status = (Label)e.Item.FindControl("lbl_status");
                Panel Panel2 = (Panel)e.Item.FindControl("Panel2");
                if (lbl_status.Text.ToLower() == "present")
                {
                    Panel2.Visible = true;
                }
                else
                {
                    Panel2.Visible = false;
                }






            }
        }
    }
}