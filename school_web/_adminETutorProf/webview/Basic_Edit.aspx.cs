using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web._adminETutorProf.webview
{
    public partial class Basic_Edit : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["teacherid"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["teacherid"] = Request.QueryString["teacherid"].ToString();

                    mycode.bind_all_ddl_with_id(ddl_state, "select State,Code from StateList");
                    Bind_filldata();

                }
            }
        }

        private void Bind_filldata()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 password from user_details where user_id=PRL_Employee_Master.Emp_Code) as Password from PRL_Employee_Master where Emp_Code='" + ViewState["teacherid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                txt_emp_name.Text = dt.Rows[0]["Employee_Name"].ToString();
                ddl_gender.Text = dt.Rows[0]["Gender"].ToString();
                txt_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                ddl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                ddl_marital_status.Text = dt.Rows[0]["Marital_Status"].ToString();
                txt_father_name.Text = dt.Rows[0]["Father_Name"].ToString();
                txt_pan.Text = dt.Rows[0]["Pan"].ToString();
                txt_address.Text = dt.Rows[0]["Address"].ToString();
                txt_city.Text = dt.Rows[0]["City"].ToString();
                txt_pin.Text = dt.Rows[0]["Pincode"].ToString();
                ddl_state.Text = dt.Rows[0]["State"].ToString();
                txt_mobile.Text = dt.Rows[0]["Mobile"].ToString();
                txt_bank.Text = dt.Rows[0]["Bank_Name"].ToString();
                txt_branch.Text = dt.Rows[0]["Branch"].ToString();
                txt_ifsc.Text = dt.Rows[0]["Ifsc"].ToString();
                txt_micr.Text = dt.Rows[0]["Micr"].ToString();
                txt_ac_no.Text = dt.Rows[0]["Account_no"].ToString();
                txt_emailid.Text = dt.Rows[0]["Email"].ToString();
                Bind_zoomuseridnad_pwd();





            }
        }

        private void Bind_zoomuseridnad_pwd()
        {
            DataTable dt = mycode.FillData("Select *  from Zoom_API  where teacher_id='" + ViewState["teacherid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                txt_zoomuserid.Text = "";
                txt_zoompassword.Text = "";

            }
            else
            {
                txt_zoomuserid.Text = dt.Rows[0]["User_ID"].ToString();
                txt_zoompassword.Text = dt.Rows[0]["Password"].ToString();

            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (txt_emp_name.Text == "")
            {
                Alertme("Please Enter Employee Name", "warning");
                txt_emp_name.Focus();
                return;
            }
            if (ddl_gender.Text == "Select")
            {
                Alertme("Please Select Gender", "warning");
                ddl_gender.Focus();
                return;
            }
            if (txt_dob.Text == "")
            {
                Alertme("Please Enter Date of birth", "warning");
                txt_dob.Focus();
                return;
            }
            if (txt_father_name.Text == "")
            {
                Alertme("Please Enter Father's Name", "warning");
                txt_father_name.Focus();
                return;
            }
            if (txt_address.Text == "")
            {
                Alertme("Please Enter Address", "warning");
                txt_address.Focus();
                return;
            }
            if (txt_city.Text == "")
            {
                Alertme("Please Enter City", "warning");
                txt_city.Focus();
                return;
            }
            if (txt_pin.Text == "")
            {
                Alertme("Please Enter Pin", "warning");
                txt_pin.Focus();
                return;
            }
            if (ddl_state.Text == "")
            {
                Alertme("Please select State", "warning");
                ddl_state.Focus();
                return;
            }
            if (txt_mobile.Text == "")
            {
                Alertme("Please enter mobile", "warning");
                txt_mobile.Focus();
                return;
            }
            else
            {

                SqlCommand cmd;
                string query = "Update user_details set name=@name,mobile=@mobile  where user_id = @user_id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@name", txt_emp_name.Text);
                cmd.Parameters.AddWithValue("@mobile", txt_mobile.Text);
                cmd.Parameters.AddWithValue("@user_id", ViewState["teacherid"].ToString());



                if (My.InsertUpdateData(cmd))
                {
                    update_employee();
                    Alertme("Profile has been updated", "warning");
                }
            }
        }

        private void update_employee()
        {
            SqlConnection conn = new SqlConnection(My.con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Master where Emp_Code='" + ViewState["teacherid"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["Employee_Name"] = txt_emp_name.Text;
                dr["Gender"] = ddl_gender.Text;
                dr["Date_of_birth"] = txt_dob.Text;
                dr["iDOB"] = mycode.ConvertStringToiDateup(txt_dob.Text);
                dr["Blood_group"] = ddl_blood_group.Text;
                dr["Religion"] = ddl_religion.Text;
                dr["Marital_Status"] = ddl_marital_status.Text;
                dr["Father_Name"] = txt_father_name.Text;
                dr["Pan"] = txt_pan.Text;
                dr["Address"] = txt_address.Text;
                dr["City"] = txt_city.Text;
                dr["Pincode"] = txt_pin.Text;
                dr["State"] = ddl_state.Text;
                dr["State_code"] = ddl_state.SelectedValue;

                dr["Mobile"] = txt_mobile.Text;
                dr["Bank_Name"] = txt_bank.Text;
                dr["Branch"] = txt_branch.Text;
                dr["Account_no"] = txt_ac_no.Text;
                dr["Ifsc"] = txt_ifsc.Text;
                dr["Micr"] = txt_micr.Text;
                dr["Email"] = txt_emailid.Text;


            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);

            update_user();
            

        }
       

        UsesCode usercode = new UsesCode();
        private void update_user()
        { 
            DataTable dt = usercode.FillTable("Select sl_no from Zoom_API where teacher_id='" + ViewState["teacherid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                int getslid = usercode.get_slid_max();

                UsesCode.exeSql("INSERT INTO Zoom_API (User_ID,Password,teacher_id,Status,sl_no) values ('" + txt_zoomuserid.Text + "','" + txt_zoompassword.Text + "','" + ViewState["teacherid"].ToString() + "','1','" + getslid + "')");
                UsesCode.exeSql("update user_details set name='" + txt_emp_name.Text + "',mobile='"+txt_mobile.Text+"', Zoom_Api_Sl_No= '" + getslid + "',Individual_universal='Individual',Allow_Virtual_class_creation='1' where user_id='" + ViewState["teacherid"].ToString() + "'");
              
            }
            else
            {
                int getslid = usercode.get_slid_max();
                UsesCode.exeSql("update Zoom_API set  User_ID= '" + txt_zoomuserid.Text + "', Password='" + txt_zoompassword.Text + "',sl_no='" + getslid + "'   where teacher_id='" + ViewState["teacherid"].ToString() + "'");
                UsesCode.exeSql("update user_details set  name='" + txt_emp_name.Text + "',mobile='" + txt_mobile.Text + "',Zoom_Api_Sl_No= '" + getslid + "',Individual_universal='Individual',Allow_Virtual_class_creation='1' where user_id='" + ViewState["teacherid"].ToString() + "'");
                
            }


            //SqlConnection conn1 = new SqlConnection(My.con);
            //SqlDataAdapter ad1 = new SqlDataAdapter("select * from user_details where user_id='" + ViewState["teacherid"].ToString() + "'", conn1);
            //DataSet ds1 = new DataSet();
            //ad1.Fill(ds1);
            //DataTable dt1 = ds1.Tables[0];
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    dr["name"] = txt_emp_name.Text;
            //    dr["mobile"] = txt_mobile.Text;
            //    dr["Allow_Virtual_class_creation"] = "1";
            //    dr["Individual_universal"] = "Individual";
            //    dr["Zoom_Api_Sl_No"] = get_slp();
                
            //}
        }

        private void Alertme(string msg, string p2)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

        }
    }
}