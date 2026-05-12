using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class salary_slip : System.Web.UI.Page
    {
        My mycode = new My();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["regid"] != null)
                    {
                        ViewState["teacher"] = Request.QueryString["regid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_year, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_year.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_month, "Select Month,Month_Id from Month_Index order by Position");
                        ddl_month.SelectedValue = mycode.get_current_month_id();

                        bind_Payroll();

                        Bind_data_all();
                    }

                }
                catch
                {

                }
               
            }
        }

        private void bind_Payroll()
        {
            string employee_id = PayrollMy.get_employee_id_from_employee_code(ViewState["teacher"]);
            string query = "Select  * from HR_UserProfile where UserId='" + employee_id + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = mycode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

            }
            else
            {
                FormsAuthentication.SetAuthCookie(dtTemp.Rows[0]["UserId"].ToString(), false);
                Session["UserType"] = dtTemp.Rows[0]["UserType"].ToString();
                Session["UserName"] = dtTemp.Rows[0]["Name"].ToString();
                Session["Userid"] = dtTemp.Rows[0]["UserId"].ToString();
                Session["IsHR"] = dtTemp.Rows[0]["IsHr"].ToString();
                Session["UserProfileImage"] = dtTemp.Rows[0]["ProfileImage"].ToString();
                //string url = "../home";
                //Response.Redirect(url, false);

                //string path = "http://localhost:1199/Developer_Profile/New_payroll.aspx";
                //Response.Redirect(path, false);
            }
        }

        private void Bind_data_all()
        {
            string employee_id = PayrollMy.get_employee_id_from_employee_code(ViewState["teacher"]);


            string[] stringSeparatorss = new string[] { "-" };
            string[] arrs = ddl_year.SelectedItem.Text.Split(stringSeparatorss, StringSplitOptions.None);
            string year1 = arrs[0];
            string year2 = arrs[1];

            string query = " select *,mn.Position from dbo.[HR_Salary_Calculation_Table] hrsc join Month_Index mn on hrsc.Month=mn.Month where hrsc.EmployeeId='" + employee_id + "' and hrsc.Year='" + year1 + "'  order by mn.Position";
            DataTable dt = code.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry there are no data list exist");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if(ddl_year.SelectedItem.Text=="Select")
            {
                Alert("Please select year");
            }
            else if(ddl_month.SelectedItem.Text=="Select")
            {
                Alert("Please select month name");
            }
            else
            {
                Bind_data();
            }

        }
        private void Bind_data()
        {
            string employee_id = PayrollMy.get_employee_id_from_employee_code(ViewState["teacher"]);

             
            string[] stringSeparatorss = new string[] { "-" };
            string[] arrs = ddl_year.SelectedItem.Text.Split(stringSeparatorss, StringSplitOptions.None);
            string year1 = arrs[0];
            string year2 = arrs[1];

            string query = " select *,mn.Position from dbo.[HR_Salary_Calculation_Table] hrsc join Month_Index mn on hrsc.Month=mn.Month where hrsc.EmployeeId='" + employee_id + "' and hrsc.Year='" + year1 + "' and hrsc.Month='" + ddl_month.SelectedItem.Text+"' order by mn.Position";
            DataTable dt = code.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry there are no data list exist");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void lnkview_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_EmployeeCode = (Label)row.FindControl("lbl_EmployeeCode");
            Label lbl_Calculation_Id = (Label)row.FindControl("lbl_Calculation_Id");
            Random random = new Random();
            int tempo = random.Next(1080, 9999);

            string url = My.URL();//"http://localhost:1199/"; //;



              string path = url + "salary-slip/" + lbl_EmployeeCode.Text + "/" + lbl_Calculation_Id.Text + "/" + tempo.ToString();

            //string path = "http://localhost:1199/salary-slip/SMI097/SC0009/6531";
            Response.Redirect(path, false);
        }

       
    }
}