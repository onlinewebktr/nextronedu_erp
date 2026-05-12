using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Add_chat_User_Chat_group : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string scrpt;
        My mycode = new My();
        protected void btn_teachingstaff_Click(object sender, EventArgs e)
        {

            string query = "select Employee_Name,Emp_Code,Department_id,(select  top 1 name   from HR_Department_Master where department_id=HR_Employee_Master.Department_id) department  from HR_Employee_Master where Department_id in (select  department_id  from HR_Department_Master where name='TEACHING STAFF') and Status='Active'";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Employee_Name = dt.Rows[i]["Employee_Name"].ToString();
                    string Emp_Code = dt.Rows[i]["Emp_Code"].ToString();
                    string department = dt.Rows[i]["department"].ToString();
                    string groupid = "1";//Teaching Staff

                    Internal_Chat_group_user(groupid, Emp_Code);

                }
                lbl_groupname.Text = "Teaching Staff";
                string fetchquery = "Select name,user_id,User_Type,User_Type from user_details where user_id in (select User_Id from Internal_Chat_group_users_list where Group_Id='1')";
                Bind_grid_data(fetchquery);
                lblmessage.Text = "All  teaching staff have been successfully added to the teaching staff group.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
        }
        public void Alert(string Message)
        {
            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void Bind_grid_data(string fetchquery)
        {
            DataTable dt = mycode.FillData(fetchquery);
            if (dt.Rows.Count == 0)
            {
                grd_view.DataSource = null;
                grd_view.DataBind();
            }
            else
            {
                grd_view.DataSource = dt;
                grd_view.DataBind();
            }
        }


        private void Internal_Chat_group_user(string groupid, string User_Id)
        {
            string query = " Select * from  Internal_Chat_group_users_list where Group_Id='"+ groupid + "' and User_Id='"+ User_Id + "'";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                string query2 = "INSERT INTO Internal_Chat_group_users_list (Group_Id,User_Id,Added_on_date,Status,Is_Group_Admin) values (@Group_Id,@User_Id,@Added_on_date,@Status,@Is_Group_Admin)";
                SqlCommand cmd  = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@Group_Id", groupid);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@Added_on_date", My.getdate1());
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@Is_Group_Admin", "0");
                if (My.InsertUpdateData(cmd))
                {
                }

            }
            else
            {

            }
        }

        protected void btn_non_teaching_staff_Click(object sender, EventArgs e)
        {
            string query = "select Employee_Name,Emp_Code,Department_id,(select  top 1 name   from HR_Department_Master where department_id=HR_Employee_Master.Department_id) department  from HR_Employee_Master where Department_id in (select  department_id  from HR_Department_Master where name='NON TEACHING STAFF') and Status='Active'";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Employee_Name = dt.Rows[i]["Employee_Name"].ToString();
                    string Emp_Code = dt.Rows[i]["Emp_Code"].ToString();
                    string department = dt.Rows[i]["department"].ToString();
                    string groupid = "2";//Non Teaching Staff

                    Internal_Chat_group_user(groupid, Emp_Code);

                }
                lbl_groupname.Text = "Non-Teaching Staff";
                string fetchquery = "Select name,user_id,User_Type,User_Type from user_details where user_id in (select User_Id from Internal_Chat_group_users_list where Group_Id='1')";
                Bind_grid_data(fetchquery);
                lblmessage.Text = "All non-teaching staff have been successfully added to the non-teaching staff group.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
        }

        protected void btn_Administrative_Click(object sender, EventArgs e)
        {
            string query = "select Employee_Name,Emp_Code,Department_id,(select  top 1 name   from HR_Department_Master where department_id=HR_Employee_Master.Department_id) department  from HR_Employee_Master where Department_id in (select  department_id  from HR_Department_Master where name='Administrative') and Status='Active'";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Employee_Name = dt.Rows[i]["Employee_Name"].ToString();
                    string Emp_Code = dt.Rows[i]["Emp_Code"].ToString();
                    string department = dt.Rows[i]["department"].ToString();
                    string groupid = "3";//Administrative Staff
                    Internal_Chat_group_user(groupid, Emp_Code);

                }
                lbl_groupname.Text = "Administrative";
                string fetchquery = "Select name,user_id,User_Type,User_Type from user_details where user_id in (select User_Id from Internal_Chat_group_users_list where Group_Id='3')";
                Bind_grid_data(fetchquery);
                lblmessage.Text = "All non-teaching staff have been successfully added to the non-teaching staff group.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
        }
    }
}