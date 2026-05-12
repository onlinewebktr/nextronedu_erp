using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class My_Attendance_From_Device : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    string sessions = My.get_session();
                    string[] stringSeparatorss = new string[] { "-" };
                    string[] arrs = sessions.Split(stringSeparatorss, StringSplitOptions.None);
                    string Year1 = arrs[0];
                    string Year2 = arrs[1];
                    mycode.bind_ddl_year(ddlyear);
                    ddlyear.Text = mycode.year();
                    mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Position asc");
                    ddl_month.SelectedValue = mycode.get_current_month_id();
                    int starte = My.toint(Year1);
                    int endyaer = My.toint(Year2);
                    ArrayList ar = new ArrayList();
                    ar.Add("Select");
                    for (int i = starte; i <= endyaer; i++)
                    {
                        ar.Add(i);
                    }
                    ddlyear.DataSource = ar;
                    ddlyear.DataBind();

                }
            }
        }
        private void Alertme(string Message)
        {
            lbl_msg.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddlyear.Text == "Select")
            {
                Alertme("Please select year");
            }
            else if (ddl_month.Text == "Select")
            {
                Alertme("Please select month");
            }
            else
            {
                Bind_data_attendance();
            }

        }
        UsesCode code = new UsesCode();
        private void Bind_data_attendance()
        {
            string date = ddlyear.Text+""+ ddl_month.SelectedValue;//My.getMonthS_twoDigit(ddl_month.SelectedValue) + "-" + ddlyear.Text;

            string employee_id = My.get_Employee_id_from_Employe_code(ViewState["regid"].ToString());

            string query = "Select Date,In_Time,Out_Time,AttendanceStatus from HR_Daily_Attendance_Record where Employee_id='" + employee_id + "'  and Idate like '%" + date + "%' and (In_Time!='' or In_Time is not null)	 order by Idate";

            DataTable dt = code.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry data not found");
                // imgexcel2.Visible = false;
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                //imgexcel2.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }

        }
    }
}