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

namespace school_web.Payroll
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
                        ViewState["college_name"] = My.get_college_name();
                      
                        mycode.bind_all_ddl_with_id(ddl_state, "select State,Code from StateList");
                        mycode.bind_all_ddl_with_id(ddl_grade, "select grade_name,grade_id from dbo.[PRL_Grade_Master]");
                        //mycode.bind_all_ddl_with_id(ddl_employee_grade, "select grade_name,grade_id from dbo.[PRL_Grade_Master]");

                        mycode.bind_all_ddl_with_id(ddl_designation, "select name,description_id from dbo.[PRL_Designation_Master]");
                        mycode.bind_all_ddl_with_id(ddl_department, "select name,department_id from dbo.[PRL_Department_Master]");

                        if (Request.QueryString["empCodE"] != null)
                        {
                            btn_Submit.Text = "Update";
                            btn_cancel.Visible = true;
                            ltUsertop.Text = "Edit Employee";
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
            DataTable dt = mycode.FillData("select *,(select top 1 password from user_details where user_id=PRL_Employee_Master.Emp_Code) as Password from PRL_Employee_Master where Emp_Code='" + HdID.Value + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                //txt_form_slno.Text = dt.Rows[0]["formserialnumber"].ToString();
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
                txt_email.Text = dt.Rows[0]["Email"].ToString();
                txt_mobile.Text = dt.Rows[0]["Mobile"].ToString();

                txt_emp_code.Text = dt.Rows[0]["Emp_Code"].ToString();
                txt_emp_code.ReadOnly = true;
                txt_punch_card_no.Text = dt.Rows[0]["Punch_Card_no"].ToString();
                txt_offifial_email.Text = dt.Rows[0]["Official_email_id"].ToString();
                ddl_grade.SelectedValue = dt.Rows[0]["Grade_id"].ToString();
                ddl_department.SelectedValue = dt.Rows[0]["Department_id"].ToString();
                ddl_designation.SelectedValue = dt.Rows[0]["Designation_id"].ToString();
                txt_epf_no.Text = dt.Rows[0]["EPF_no"].ToString();
                txt_joining_date.Text = dt.Rows[0]["EPF_Join_date"].ToString();
                txt_pf_leaving_date.Text = dt.Rows[0]["PF_Leaving_date"].ToString();
                txt_reason.Text = dt.Rows[0]["PF_leaving_Reagion"].ToString();
                txt_esic_no.Text = dt.Rows[0]["ESIC_no"].ToString();
                txt_join_date.Text = dt.Rows[0]["ESIC_join_date"].ToString();
                txt_esic_leaving_date.Text = dt.Rows[0]["ESIC_leaving_date"].ToString();
                txt_reason_esic.Text = dt.Rows[0]["ESIC_leaving_Reagion"].ToString();

                txt_emp_join_date.Text = dt.Rows[0]["Date_of_Joining"].ToString();

                txt_bank.Text = dt.Rows[0]["Bank_Name"].ToString();
                txt_branch.Text = dt.Rows[0]["Branch"].ToString();
                txt_ifsc.Text = dt.Rows[0]["Ifsc"].ToString();
                txt_micr.Text = dt.Rows[0]["Account_no"].ToString();
                txt_ac_no.Text = dt.Rows[0]["Account_no"].ToString();
                txt_qualification.Text = dt.Rows[0]["Qualification"].ToString();
                txt_pwd.Text = dt.Rows[0]["Password"].ToString();
                fetch_docs();
            }
        }

        private void fetch_docs()
        {
            DataTable dt = mycode.FillData("select *  from PRL_Employee_Document_details where Employee_id='" + HdID.Value + "'");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                documentS.Visible = false;
            }
            else
            {
                documentS.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
            //if (ddl_gender.Text == "Select")
            //{
            //    Alertme("Please Select Gender", "warning");
            //    ddl_gender.Focus();
            //    return;
            //}
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
            //if (txt_pin.Text == "")
            //{
            //    Alertme("Please Enter Pin", "warning");
            //    txt_pin.Focus();
            //    return;
            //}
            //if (ddl_state.Text == "")
            //{
            //    Alertme("Please select State", "warning");
            //    ddl_state.Focus();
            //    return;
            //}
            if (txt_mobile.Text == "")
            {
                Alertme("Please enter mobile", "warning");
                txt_mobile.Focus();
                return;
            }


            if (ddl_department.Text == "Select")
            {
                Alertme("Please select department", "warning");
                ddl_department.Focus();
                return;
            }
            if (ddl_designation.Text == "Select")
            {
                Alertme("Please select designation", "warning");
                ddl_designation.Focus();
                return;
            }
            //if (ddl_grade.Text == "")
            //{
            //    Alertme("Please select grade", "warning");
            //    ddl_grade.Focus();
            //    return;
            //}
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
                if (!check_duplicate_employee_code(txt_emp_code.Text))
                {
                    Alertme("Duplicate employee code!", "warning");
                    txt_emp_code.Focus();
                    return;
                }
                try
                {

                    //SqlConnection conn = new SqlConnection(My.conn);
                    //SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Device_Details", conn);
                    //DataSet ds = new DataSet();
                    //ad.Fill(ds);
                    //DataTable dt = ds.Tables[0];
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    if (My.connect_device(dr["Device_Ip"].ToString(), dr["Device_Name"].ToString()))
                    //    {
                    //        My.objZkeeper.SSR_SetUserInfo(1, txt_emp_code.Text, txt_emp_name.Text, "", 0, true);
                    //        My.objZkeeper.Disconnect();
                    //    }
                    //}


                     bool chkempid = mycode.get_empid(txt_emp_code.Text);
                     if (chkempid == true)
                     {

                         send_to_ledger();
                         submit();
                         mycode.executequery("update PRL_Employee_Document_details set Employee_id='" + txt_emp_code.Text + "' where Employee_id='" + hd_temp_id.Value + "';insert into Login_table(User_id,Password,SModule,Name_of_user,Status) values ('" + txt_emp_code.Text + "','" + txt_pwd.Text + "','" + ddl_emp_type.Text + "','" + txt_emp_name.Text + "','1')");
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
                }
            }
            else
            {
                //SqlConnection conn = new SqlConnection(My.conn);
                //SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Device_Details", conn);
                //DataSet ds = new DataSet();
                //ad.Fill(ds);
                //DataTable dt = ds.Tables[0];
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (My.connect_device(dr["Device_Ip"].ToString(), dr["Device_Name"].ToString()))
                //    {
                //        My.objZkeeper.SSR_SetUserInfo(1, txt_emp_code.Text, txt_emp_name.Text, "", 0, true);
                //        My.objZkeeper.Disconnect();
                //    }
                //}
                //UpdateE
                update_employee(); update_in_user_reg(); empty_form();
                Alertme("Employee has been sucessfully updated", "success");
            }
        }

        private void update_in_user_reg()
        {
            SqlCommand cmd;
            string query = "Update user_details set name=@name,mobile=@mobile,password=@password,status=@status,date=@date,firm=@firm,User_Type=@User_Type,is_sync=@is_sync,Branch_id=@Branch_id,create_by=@create_by,Istatus=@Istatus where user_id = @user_id";
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

            if (My.InsertUpdateData(cmd))
            {
                mycode.executequery("update Login_table set Password='" + txt_pwd.Text + "', SModule='" + ddl_emp_type.Text + "', Name_of_user='" + txt_emp_name.Text + "'");
                string msg = ViewState["Userid"].ToString() + " Update user, User id=" + txt_emp_code.Text + " Name=" + txt_emp_name.Text + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["firm_id"].ToString());
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
                dr["Father_Name"] = txt_father_name.Text;
                dr["Pan"] = txt_pan.Text;
                dr["Address"] = txt_address.Text;
                dr["City"] = txt_city.Text;
                dr["Pincode"] = txt_pin.Text;
                dr["State"] = ddl_state.Text;
                dr["State_code"] = ddl_state.SelectedValue;
                dr["Email"] = txt_email.Text;
                dr["Mobile"] = txt_mobile.Text;
                dr["Bank_Name"] = txt_bank.Text;
                dr["Branch"] = txt_branch.Text;
                dr["Account_no"] = txt_ac_no.Text;
                dr["Ifsc"] = txt_ifsc.Text;
                dr["Micr"] = txt_micr.Text;
                if (FileUpload1.HasFile)
                {
                    dr["Employee_image"] = upload_image(FileUpload1, "StudentImg");
                }
                dr["Punch_Card_no"] = txt_punch_card_no.Text;
                dr["Official_email_id"] = txt_offifial_email.Text;
                dr["Grade_id"] = ddl_grade.SelectedValue;
                dr["Department_id"] = ddl_department.SelectedValue;
                dr["Designation_id"] = ddl_designation.SelectedValue;
                dr["EPF_no"] = txt_epf_no.Text;
                dr["EPF_Join_date"] = txt_joining_date.Text;
                dr["PF_Leaving_date"] = txt_pf_leaving_date.Text;
                dr["PF_leaving_Reagion"] = txt_reason.Text;
                dr["ESIC_no"] = txt_esic_no.Text;
                dr["ESIC_join_date"] = txt_join_date.Text;
                dr["ESIC_leaving_date"] = txt_esic_leaving_date.Text;
                dr["ESIC_leaving_Reagion"] = txt_reason_esic.Text;

                dr["Date_of_Joining"] = txt_emp_join_date.Text;
                dr["employee_type"] = ddl_emp_type.Text;
                dr["Qualification"] = txt_qualification.Text;
                dr["Status"] = "Active";
                dr["College_Name"] = ViewState["college_name"].ToString();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void send_to_ledger()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Account_Ledger_Details where Account_id='" + txt_emp_code.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_emp_name.Text;
                dr[2] = txt_emp_code.Text;
                dr[3] = 15;
                dr[4] = ViewState["firm_id"].ToString();
                // dr["alias"] = txt_alias.Text;
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = txt_emp_name.Text;
                    dr[2] = txt_emp_code.Text;
                    dr[3] = 15;
                    dr[4] = ViewState["firm_id"].ToString();
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        string emp_id = "";

        private void empty_form()
        {
            txt_emp_name.Text = "";
            ddl_gender.Text = "";
            txt_dob.Text = mycode.date();
           
            txt_father_name.Text = "";
            txt_ac_no.Text = "";
            txt_pan.Text = "";
            txt_address.Text = "";
            txt_city.Text = "";
            txt_pin.Text = "";
            ddl_state.SelectedValue = "10";
            txt_email.Text = "";
            txt_mobile.Text = "";
            txt_ifsc.Text = "";
            txt_branch.Text = "";
            txt_bank.Text = "";
            txt_micr.Text = "";
            txt_emp_code.Text = "";
            txt_punch_card_no.Text = "";
            txt_offifial_email.Text = "";
            txt_epf_no.Text = "";
            txt_joining_date.Text = mycode.date();
            txt_pf_leaving_date.Text = mycode.date();
            txt_esic_no.Text = "";
            txt_join_date.Text = mycode.date();
            txt_esic_leaving_date.Text = mycode.date();
            txt_dob.Text = "";
            txt_joining_date.Text = "";
            txt_pf_leaving_date.Text = "";
            txt_reason.Text = "";
            txt_join_date.Text = "";
            txt_esic_leaving_date.Text = "";
            txt_reason_esic.Text = "";
            txt_emp_join_date.Text = "";
            txt_pwd.Text = "";
            txt_qualification.Text = "";
            btn_Submit.Text = "Final Submit";

            ddl_blood_group.Text = "N/A";


            ddl_marital_status.Text = "Unmarried";
            ddl_state.SelectedValue = "10";
            btn_cancel.Visible = false;

            documentS.Visible = false;
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
            dr["Email"] = txt_email.Text;
            dr["Mobile"] = txt_mobile.Text;


            if (FileUpload1.HasFile)
            {
                dr["Employee_image"] = upload_image(FileUpload1, "StudentImg");
            }
            else
            {
                dr["Employee_image"] = null;
            }
            dr["Emp_Code"] = txt_emp_code.Text;
            dr["Punch_Card_no"] = txt_punch_card_no.Text;
            dr["Official_email_id"] = txt_offifial_email.Text;
            dr["Grade_id"] = ddl_grade.SelectedValue;
            dr["Department_id"] = ddl_department.SelectedValue;
            dr["Designation_id"] = ddl_designation.SelectedValue;
            dr["EPF_no"] = txt_epf_no.Text;
            dr["EPF_Join_date"] = txt_joining_date.Text;
            dr["PF_Leaving_date"] = txt_pf_leaving_date.Text;
            dr["PF_leaving_Reagion"] = txt_reason.Text;
            dr["ESIC_no"] = txt_esic_no.Text;
            dr["ESIC_join_date"] = txt_join_date.Text;
            dr["ESIC_leaving_date"] = txt_esic_leaving_date.Text;
            dr["ESIC_leaving_Reagion"] = txt_reason_esic.Text;
            dr["Date_of_Joining"] = txt_emp_join_date.Text;
            dr["Employee_id"] = emp_id = My.auto_serialS("Employee_id");
            ddl_employee.SelectedValue = dr["Employee_id"].ToString();
            dr["Bank_Name"] = txt_bank.Text;
            dr["Branch"] = txt_branch.Text;
            dr["Ifsc"] = txt_ifsc.Text;
            dr["Micr"] = txt_micr.Text;
            dr["Account_no"] = txt_ac_no.Text;
            dr["employee_type"] = ddl_emp_type.Text;
            dr["Qualification"] = txt_qualification.Text;
            dr["Status"] = "Active";
            dr["College_Name"] = ViewState["college_name"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            save_user_reg();
        }

        private void save_user_reg()
        {
            SqlCommand cmd;
            string query = "INSERT INTO user_details (name,mobile,user_id,password,status,date,firm,User_Type,is_sync,Branch_id,create_by,Istatus) values (@name,@mobile,@user_id,@password,@status,@date,@firm,@User_Type,@is_sync,@Branch_id,@create_by,@Istatus)";
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
                if (FU.FileBytes.Length < 2000000)
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

        protected void btn_upload_doc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_emp_code.Text == "")
                {
                    Alertme("Please enter employee code.", "warning");
                    txt_emp_code.Focus();
                    return;
                }
                if (txt_doc_name.Text == "")
                {
                    Alertme("Please enter document name.", "warning");
                    txt_doc_name.Focus();
                    return;
                }
                string doc_path = "";
                if (FileUpload2.HasFile)
                {
                    doc_path = upload_image(FileUpload2, "StudentImg");
                }

                if (doc_path == "")
                {
                    Alertme("Please choose document.", "warning");
                    FileUpload2.Focus();
                    return;
                }

                SqlCommand cmd;
                string strQuery = "INSERT INTO PRL_Employee_Document_details (Employee_id,Document_Name,Document) values (@Employee_id,@Document_Name,@Document)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Employee_id", hd_temp_id.Value);
                cmd.Parameters.AddWithValue("@Document_Name", txt_doc_name.Text);
                cmd.Parameters.AddWithValue("@Document", doc_path);
                if (My.InsertUpdateData(cmd))
                {
                    bind_doc_grd();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_doc_grd()
        {
            DataTable dt = mycode.FillData("select *  from PRL_Employee_Document_details where Employee_id='" + hd_temp_id.Value + "'");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                documentS.Visible = false;
            }
            else
            {
                documentS.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            string query = "delete from  PRL_Employee_Document_details where Id=@Id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Document has been delete Successfully.", "success");
                bind_doc_grd();
            }
        }
    }
}