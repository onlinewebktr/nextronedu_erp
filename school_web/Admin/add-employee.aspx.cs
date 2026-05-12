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

namespace school_web.Admin
{
    public partial class add_employee : System.Web.UI.Page
    {
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
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["college_name"] = My.get_college_name();



                        //mycode.bind_all_ddl_with_id(ddl_employee_grade, "select grade_name,grade_id from dbo.[PRL_Grade_Master]"); 
                        //mycode.bind_all_ddl_with_id(ddl_department, "select name,department_id from dbo.[PRL_Department_Master] order by name");

                        mycode.bind_ddl(ddl_emp_type, "select Emp_type from HR_Emp_type order by Emp_type ");
                        string pagename_current = "add-employee.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        chk_auto_generate_emp_code.Visible = true;
                        txt_emp_code.Enabled = true;
                        if (Request.QueryString["empCodE"] != null)
                        {
                            txt_emp_code.Enabled = false;
                            chk_auto_generate_emp_code.Visible = false;
                            btn_Submit.Text = "Update";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Edit User";
                            HdID.Value = Request.QueryString["empCodE"].ToString();
                            hd_temp_id.Value = HdID.Value;
                            BindDetails();
                        }
                        else
                        {
                            hd_temp_id.Value = My.create_random_no_otp();
                        }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(Session["Admin"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StdentRegistration");
            }
        }




        private void BindDetails()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 password from user_details where user_id=PRL_Employee_Master.Emp_Code) as Password,(select top 1 Signature from user_details where user_id=PRL_Employee_Master.Emp_Code) as Signature from PRL_Employee_Master where Emp_Code='" + HdID.Value + "'");
            if (dt.Rows.Count > 0)
            {
                //txt_form_slno.Text = dt.Rows[0]["formserialnumber"].ToString();
                txt_emp_name.Text = dt.Rows[0]["Employee_Name"].ToString();
                ddl_gender.Text = dt.Rows[0]["Gender"].ToString();
                txt_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                ddl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                ddl_religion.Text = dt.Rows[0]["Religion"].ToString();
                ddl_marital_status.Text = dt.Rows[0]["Marital_Status"].ToString();
                txt_address.Text = dt.Rows[0]["Address"].ToString();
                txt_email.Text = dt.Rows[0]["Email"].ToString();
                txt_mobile.Text = dt.Rows[0]["Mobile"].ToString();
                txt_emp_code.Text = dt.Rows[0]["Emp_Code"].ToString();
                txt_emp_code.ReadOnly = true;
                txt_pwd.Text = dt.Rows[0]["Password"].ToString();
                lbl_signature_path.Text = dt.Rows[0]["Signature"].ToString();
                try
                {
                    ddl_emp_type.Text = dt.Rows[0]["employee_type"].ToString();
                }
                catch
                {
                }
            }
        }




        protected void btn_Submit_Click(object sender, EventArgs e)
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
            if (ddl_religion.Text == "Select")
            {
                Alertme("Please Select Religion", "warning");
                ddl_religion.Focus();
                return;
            }
            if (ddl_marital_status.Text == "Select")
            {
                Alertme("Please Select Marital Status", "warning");
                ddl_marital_status.Focus();
                return;
            }
            //if (txt_dob.Text == "")
            //{
            //    Alertme("Please Enter Date of birth", "warning");
            //    txt_dob.Focus();
            //    return;
            //}
            //if (txt_father_name.Text == "")
            //{
            //    Alertme("Please Enter Father's Name", "warning");
            //    txt_father_name.Focus();
            //    return;
            //}
            //if (txt_address.Text == "")
            //{
            //    Alertme("Please Enter Address", "warning");
            //    txt_address.Focus();
            //    return;
            //}
            //if (txt_city.Text == "")
            //{
            //    Alertme("Please Enter City", "warning");
            //    txt_city.Focus();
            //    return;
            //}
            if (txt_email.Text == "")
            {
                Alertme("Please Enter email", "warning");
                txt_email.Focus();
                return;
            }

            if (txt_mobile.Text == "")
            {
                Alertme("Please enter mobile", "warning");
                txt_mobile.Focus();
                return;
            }

            if (txt_emp_code.Text == "")
            {
                Alertme("Please employee code", "warning");
                txt_emp_code.Focus();
                return;
            }
            if (txt_pwd.Text == "")
            {
                Alertme("Please enter password.", "warning");
                txt_pwd.Focus();
                return;
            }

            if (btn_Submit.Text == "Final Submit")
            {

                if (ViewState["Is_add"].ToString() == "1")
                {
                    if (!check_duplicate_employee_code(txt_emp_code.Text))
                    {
                        Alertme("Duplicate employee code!", "warning");
                        txt_emp_code.Focus();
                        return;
                    }
                    try
                    {
                        bool chkempid = mycode.get_empid(txt_emp_code.Text);
                        if (chkempid == true)
                        {

                            submit();
                            menuPermission.update_menu_permission_for_user(txt_emp_code.Text, ddl_emp_type.Text);
                            mycode.executequery("insert into Login_table(User_id,Password,SModule,Name_of_user,Status) values ('" + txt_emp_code.Text + "','" + txt_pwd.Text + "','" + ddl_emp_type.Text + "','" + txt_emp_name.Text + "','1')");
                            hd_temp_id.Value = My.create_random_no_otp();
                            Alertme("Employee details has been added successfully.", "success");
                            empty_form();
                        }
                        else
                        {
                            Alertme("Duplicate employee code!", "warning");

                        }
                    }
                    catch (Exception ex)
                    {
                        My.submitException(ex, "Add Employe admin");
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }


            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    //UpdateE
                    update_employee();
                    update_in_user_reg();
                    menuPermission.update_menu_permission_for_user(txt_emp_code.Text, ddl_emp_type.Text);
                    try
                    {
                        if (ddl_emp_type.Text.ToUpper() == "TEACHER")
                        {
                            My.is_classTeacher_auto_assign_for_all_class(My.get_session_id(), txt_emp_code.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    empty_form();
                    Alertme("Employee has been sucessfully updated", "success");
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void update_in_user_reg()
        {
            SqlCommand cmd;
            string query = "Update user_details set name=@name,mobile=@mobile,password=@password,status=@status,date=@date,firm=@firm,User_Type=@User_Type,is_sync=@is_sync,Branch_id=@Branch_id,create_by=@create_by,Istatus=@Istatus,Signature=@Signature where user_id = @user_id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@name", txt_emp_name.Text);
            cmd.Parameters.AddWithValue("@mobile", txt_mobile.Text);
            cmd.Parameters.AddWithValue("@user_id", txt_emp_code.Text);
            cmd.Parameters.AddWithValue("@password", txt_pwd.Text);
            cmd.Parameters.AddWithValue("@status", "Active");
            cmd.Parameters.AddWithValue("@date", mycode.date());
            cmd.Parameters.AddWithValue("@firm", ViewState["firm_id"].ToString());
            cmd.Parameters.AddWithValue("@User_Type", ddl_emp_type.Text);
            cmd.Parameters.AddWithValue("@is_sync", 0);
            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
            cmd.Parameters.AddWithValue("@create_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Istatus", "1"); 
            if (FileUpload3.HasFile)
            {
                cmd.Parameters.AddWithValue("@Signature", upload_image(FileUpload3, "EmpSig"));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Signature", lbl_signature_path.Text);
            }

            if (My.InsertUpdateData(cmd))
            {
                mycode.executequery("update Login_table set Password='" + txt_pwd.Text + "', SModule='" + ddl_emp_type.Text + "', Name_of_user='" + txt_emp_name.Text + "'");
                string msg = ViewState["Userid"].ToString() + " Update user, User id=" + txt_emp_code.Text + " Name=" + txt_emp_name.Text + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");

                mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["firm_id"].ToString());
                try
                {
                    if (ddl_emp_type.Text.ToUpper() == "TEACHER")
                    {
                        My.is_classTeacher_auto_assign_for_all_class(My.get_session_id(), txt_emp_code.Text);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void update_employee()
        {
            SqlConnection conn = new SqlConnection(My.con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Master where Emp_Code='" + HdID.Value + "'", conn);
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
                dr["Address"] = txt_address.Text; 
                dr["Email"] = txt_email.Text;
                dr["Mobile"] = txt_mobile.Text;

                if (FileUpload1.HasFile)
                {
                    dr["Employee_image"] = upload_image(FileUpload1, "StudentImg");
                }

                dr["employee_type"] = ddl_emp_type.Text;

                dr["College_Name"] = ViewState["college_name"].ToString();




            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }



        string emp_id = "";

        private void empty_form()
        {
            txt_emp_name.Text = "";

            txt_dob.Text = mycode.date();



            txt_email.Text = "";
            txt_mobile.Text = "";

            txt_emp_code.Text = "";


            txt_dob.Text = "";


            txt_pwd.Text = "";

            btn_Submit.Text = "Final Submit";
            btn_cancel.Visible = false;


            ddl_blood_group.Text = "N/A";

            ddl_religion.Text = "Select";
            ddl_marital_status.Text = "Select";

            txt_address.Text = "";


            // bind_leave_name();
        }

        private void submit()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Master", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "PRL_Employee_Master");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Employee_Name"] = txt_emp_name.Text;
            dr["Gender"] = ddl_gender.Text;
            dr["Date_of_birth"] = txt_dob.Text;
            dr["iDOB"] = mycode.ConvertStringToiDateup(txt_dob.Text);

            dr["Religion"] = ddl_religion.Text;
            dr["Marital_Status"] = ddl_marital_status.Text;
            dr["Email"] = txt_email.Text;
            dr["Mobile"] = txt_mobile.Text;
            dr["Address"] = txt_address.Text;

            if (FileUpload1.HasFile)
            {
                dr["Employee_image"] = upload_image(FileUpload1, "StudentImg");
            }
            else
            {
                dr["Employee_image"] = null;
            }
            dr["Emp_Code"] = txt_emp_code.Text;




            dr["Employee_id"] = emp_id = txt_emp_code.Text;
            ddl_employee.SelectedValue = dr["Employee_id"].ToString();

            dr["employee_type"] = ddl_emp_type.Text;

            dr["College_Name"] = ViewState["college_name"].ToString();
            dr["Status"] = "Active";

            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            save_user_reg();
        }

        private void save_user_reg()
        {
            SqlCommand cmd;
            string query = "INSERT INTO user_details (name,mobile,user_id,password,status,date,firm,User_Type,is_sync,Branch_id,create_by,Istatus,Signature) values (@name,@mobile,@user_id,@password,@status,@date,@firm,@User_Type,@is_sync,@Branch_id,@create_by,@Istatus,@Signature)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@name", txt_emp_name.Text);
            cmd.Parameters.AddWithValue("@mobile", txt_mobile.Text);
            cmd.Parameters.AddWithValue("@user_id", txt_emp_code.Text);
            cmd.Parameters.AddWithValue("@password", txt_pwd.Text);
            cmd.Parameters.AddWithValue("@status", "Active");
            cmd.Parameters.AddWithValue("@date", mycode.date());
            cmd.Parameters.AddWithValue("@firm", ViewState["firm_id"].ToString());
            cmd.Parameters.AddWithValue("@User_Type", ddl_emp_type.Text);
            cmd.Parameters.AddWithValue("@is_sync", 0);
            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
            cmd.Parameters.AddWithValue("@create_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Istatus", "1");

            if (FileUpload3.HasFile)
            {
                cmd.Parameters.AddWithValue("@Signature", upload_image(FileUpload3, "EmpSig"));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Signature", "");
            }

            if (My.InsertUpdateData(cmd))
            {
                Alertme("User has been sucessfully added", "success");
                string msg = ViewState["Userid"].ToString() + " Create user, User id=" + txt_emp_code.Text + " Name=" + txt_emp_name.Text + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["firm_id"].ToString());
            }
        }


        private bool check_duplicate_employee_code(string p)
        {
            DataTable dt = My.dataTable("select Emp_Code from dbo.[PRL_Employee_Master] where Emp_Code='" + p + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
            btn_cancel.Visible = false;
            btn_Submit.Text = "Final Submit";
        }

        #region FileUploaD
        private string upload_image(FileUpload FU, string FNmae)
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            string dbfilepath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;

            int k = 0;
            if (FU.HasFile)
            {
                if (FU.FileBytes.Length < 20000000)
                {
                    Session["WorkingImage"] = FU.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    string[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif", ".webp" };
                    Session["WorkingImage1"] = FNmae + idate + time + FileExtension;
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }
                else
                {
                    Alertme("Please reduce image size (Max 200kb)", "warning");
                    return "";
                }
            }
            else
            {
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    FU.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {
                Alertme("Please select jpg and png image", "warning");
            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                dbfilepath = originalPath1 + "/Master_Img/Student/" + fileName;
            }
            return dbfilepath;
        }
        #endregion

        protected void chk_auto_generate_emp_code_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (chk_auto_generate_emp_code.Checked == true)
                {
                    My.init_payroll();
                    string emp_code = "";
                    txt_emp_code.ReadOnly = true;
                    emp_code = My.emp_code_prefix + My.toDouble(My.view_auto_serial("emp_code")).ToString(My.emp_code) + My.emp_code_postfix;
                    while (!check_duplicate_employee_code(emp_code))
                    {
                        emp_code = My.emp_code_prefix + My.toDouble(My.auto_serialS("emp_code")).ToString(My.emp_code) + My.emp_code_postfix;
                    }
                    txt_emp_code.Text = emp_code;
                }
                else
                {
                    txt_emp_code.ReadOnly = false;
                    txt_emp_code.Text = "";
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}