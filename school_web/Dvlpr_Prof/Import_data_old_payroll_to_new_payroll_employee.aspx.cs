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
    public partial class Import_data_old_payroll_to_new_payroll_employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        My mycode = new My();

        protected void btn_fetch_Department_master_Click(object sender, EventArgs e)
        {
            try
            {
                My.exeSql("Alter Table dbo.[PRL_Department_Master] Add [import_status] varchar (500)  ;");
                My.exeSql("update PRL_Department_Master set import_status='Pending'");
            }
            catch
            {
                My.exeSql("update PRL_Department_Master set import_status='Pending' where import_status is null");

            }


            DataTable dt = mycode.FillData(" select  *  from dbo.[PRL_Department_Master] where import_status='Pending'   ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string name = dt.Rows[i]["name"].ToString();
                    string description = dt.Rows[i]["description"].ToString();
                    string department_id = dt.Rows[i]["department_id"].ToString();
                    SqlCommand cmd;
                    string query = "INSERT INTO HR_Department_Master (name,description,department_id) values (@name,@description,@department_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@department_id", department_id);
                    if (My.InsertUpdateData(cmd))
                    {

                        My.exeSql("update PRL_Department_Master set import_status='Done' where department_id="+ department_id + "");

                    }

                }
                int lastid = get_last_id("PRL_Department_Master", "department_id")+1;
                My.exeSql("update HR_AutoId set IdValue=" + lastid +" where IdName='department_id'");
            }
        }

        private int get_last_id(string table_name, string prm)
        {
            string query = "Select top 1 " + prm + " from " + table_name + " order by id desc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return My.toint(dt.Rows[0][0].ToString());
            }
        }

        protected void btn_fetch_employe_data_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    My.exeSql("Alter Table dbo.[PRL_Employee_Master] Add [import_status] varchar (500)  ;");
            //    My.exeSql("update PRL_Employee_Master set import_status='Pending'");
            //    My.exeSql("Alter Table dbo.[HR_Employee_Online_Apply] Add [Pwd_Old] varchar (500)  ;");
            //}
            //catch
            //{
              

                
            //    My.exeSql("update PRL_Employee_Master set import_status='Pending' where (import_status='' or import_status is null)");
            //    My.exeSql("Alter Table dbo.[HR_Employee_Online_Apply] Add [Pwd_Old] varchar (500)  ;");
            //}

            DataTable dt = mycode.FillData(" select  *,( select   top 1 Signature  from dbo.[user_details] where user_id=PRL_Employee_Master.Emp_Code) as Signature,( select   top 1 password  from dbo.[user_details] where user_id=PRL_Employee_Master.Emp_Code) as pwd,( select   top 1 date  from dbo.[user_details] where user_id=PRL_Employee_Master.Emp_Code) as date  from dbo.[PRL_Employee_Master] where import_status='Pending'   ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd;

                   string  Status = dt.Rows[i]["Signature"].ToString();
                    string College_Name = dt.Rows[i]["College_Name"].ToString();
                    string Signature = dt.Rows[i]["Signature"].ToString();
                    
                    string ProfilePhoto = dt.Rows[i]["Employee_image"].ToString();
                    string Emp_Code = dt.Rows[i]["Emp_Code"].ToString();
                    string date = dt.Rows[i]["date"].ToString();
                    string pwd = dt.Rows[i]["pwd"].ToString();

                    string Employee_Name = dt.Rows[i]["Employee_Name"].ToString();
                    string Gender = dt.Rows[i]["Gender"].ToString();
                    string Date_of_birth = dt.Rows[i]["Date_of_birth"].ToString();
                    string Blood_group = dt.Rows[i]["Blood_group"].ToString();
                    string Religion = dt.Rows[i]["Religion"].ToString();
                    string Marital_Status = dt.Rows[i]["Marital_Status"].ToString();
                    string Father_Name = dt.Rows[i]["Father_Name"].ToString();
                    string Pan = dt.Rows[i]["Pan"].ToString();
                    string Address = dt.Rows[i]["Address"].ToString();
                    string City = dt.Rows[i]["City"].ToString();
                    string Pincode = dt.Rows[i]["Pincode"].ToString();
                    string State_code = dt.Rows[i]["State_code"].ToString();
                    string State = dt.Rows[i]["State"].ToString();
                    string Email = dt.Rows[i]["Email"].ToString();

                    string Mobile = dt.Rows[i]["Mobile"].ToString();
                    string Official_email_id = dt.Rows[i]["Official_email_id"].ToString();
                    string Department_id = dt.Rows[i]["Department_id"].ToString();
                    string Designation_id = dt.Rows[i]["Designation_id"].ToString();

                    string EPF_no = dt.Rows[i]["EPF_no"].ToString();
                    string EPF_Join_date = dt.Rows[i]["EPF_Join_date"].ToString();
                    string PF_Leaving_date = dt.Rows[i]["PF_Leaving_date"].ToString();
                    string PF_leaving_Reagion = dt.Rows[i]["PF_leaving_Reagion"].ToString();
                    string ESIC_no = dt.Rows[i]["ESIC_no"].ToString();
                    string ESIC_join_date = dt.Rows[i]["ESIC_join_date"].ToString();
                    string ESIC_leaving_date = dt.Rows[i]["ESIC_leaving_date"].ToString();
                    string ESIC_leaving_Reagion = dt.Rows[i]["ESIC_leaving_Reagion"].ToString();
                    string Date_of_Joining = dt.Rows[i]["Date_of_Joining"].ToString();
                    string Bank_Name = dt.Rows[i]["Bank_Name"].ToString();
                    string Branch = dt.Rows[i]["Branch"].ToString();
                    string Ifsc = dt.Rows[i]["Ifsc"].ToString();
                    string Micr = dt.Rows[i]["Micr"].ToString();
                    string Account_no = dt.Rows[i]["Account_no"].ToString();
                    string employee_type = dt.Rows[i]["employee_type"].ToString();
                    
                    string Divie_added_or_not = dt.Rows[i]["Divie_added_or_not"].ToString();

                    string query = "INSERT INTO HR_Employee_Online_Apply (Branchi_id,User_id,Apply_for,Hiring_id,Apply_id,Date,idate,Pay_Type,Payable_amount,Payment_Status,Session_id,subject_name,Salutation,First_Name,Middle_Name,Last_Name,Emailid,Date_birthday,Gender,Place_Of_Birth,Birth_State,Religion,Nationality,Marital_Status,Address_CA,City_CA,State_CA,Pincode_CA,mobile_no_CA,Residence_telephone_no_CA,address_pa,city_pa,state_pa,pin_pa,chiled_name1,chiled_gender1,chiled_age1,chiled_name2,chiled_gender2,chiled_age2,chiled_name3,chiled_gender3,chiled_age3,fathername,father_occupation,mother_name,mother_occupation,Spouse_name,Spouses_job_is_transferable,spouse_qualification,spouse_profession,spouse_organization,spouse_designation,completed_years,teaching_years,Administration_year,any_other,Current_name_of_instituation,instituation_address,Contact_Numbe_instituation,Designation_work,joining_date,place_of_posting,Present_Salary,Basic_Salary_Present,Allowance_Present,Other_Benefits_Present,Under_Service_Bond,years_service_bond,Expected_Salary,English_read,English_write,English_Speak,Hindi_read,Hindi_write,Hindi_speak,Bangla_read,Bangla_write,Bangla_speak,Other_Language,Proficiency_In_Computer,passport_photo,Signature,Order_id,razorpay_payment_id,razorpay_order_id,razorpay_signature,Seat_Remarks,no_of_seat,iam_fresher,Emp_code,Subject_Type,Apply_From,Verification_Status,CurrentStatus,Remarks,HiringTypeId,JoiningDate,Salary,JoiningRemarks,Gross,Deduction,Netsalary,Emp_contribution,CTC_year,CTC_month,income_heads,deduction_head,Grade,DepartmentId,DesignationId,Pwd_Old,ActiveStatus,College_Name) values (@Branchi_id,@User_id,@Apply_for,@Hiring_id,@Apply_id,@Date,@idate,@Pay_Type,@Payable_amount,@Payment_Status,@Session_id,@subject_name,@Salutation,@First_Name,@Middle_Name,@Last_Name,@Emailid,@Date_birthday,@Gender,@Place_Of_Birth,@Birth_State,@Religion,@Nationality,@Marital_Status,@Address_CA,@City_CA,@State_CA,@Pincode_CA,@mobile_no_CA,@Residence_telephone_no_CA,@address_pa,@city_pa,@state_pa,@pin_pa,@chiled_name1,@chiled_gender1,@chiled_age1,@chiled_name2,@chiled_gender2,@chiled_age2,@chiled_name3,@chiled_gender3,@chiled_age3,@fathername,@father_occupation,@mother_name,@mother_occupation,@Spouse_name,@Spouses_job_is_transferable,@spouse_qualification,@spouse_profession,@spouse_organization,@spouse_designation,@completed_years,@teaching_years,@Administration_year,@any_other,@Current_name_of_instituation,@instituation_address,@Contact_Numbe_instituation,@Designation_work,@joining_date,@place_of_posting,@Present_Salary,@Basic_Salary_Present,@Allowance_Present,@Other_Benefits_Present,@Under_Service_Bond,@years_service_bond,@Expected_Salary,@English_read,@English_write,@English_Speak,@Hindi_read,@Hindi_write,@Hindi_speak,@Bangla_read,@Bangla_write,@Bangla_speak,@Other_Language,@Proficiency_In_Computer,@passport_photo,@Signature,@Order_id,@razorpay_payment_id,@razorpay_order_id,@razorpay_signature,@Seat_Remarks,@no_of_seat,@iam_fresher,@Emp_code,@Subject_Type,@Apply_From,@Verification_Status,@CurrentStatus,@Remarks,@HiringTypeId,@JoiningDate,@Salary,@JoiningRemarks,@Gross,@Deduction,@Netsalary,@Emp_contribution,@CTC_year,@CTC_month,@income_heads,@deduction_head,@Grade,@DepartmentId,@DesignationId,@Pwd_Old,@ActiveStatus,@College_Name)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Branchi_id", "1");
                    cmd.Parameters.AddWithValue("@User_id", "edunext2021");
                    cmd.Parameters.AddWithValue("@Apply_for", employee_type);
                    cmd.Parameters.AddWithValue("@Hiring_id", "0");
                    cmd.Parameters.AddWithValue("@Apply_id", Emp_Code);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDate(date));
                    cmd.Parameters.AddWithValue("@Pay_Type", "Online");
                    cmd.Parameters.AddWithValue("@Payable_amount", "0.00");
                    cmd.Parameters.AddWithValue("@Payment_Status", "Paid");
                    cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
                    cmd.Parameters.AddWithValue("@subject_name", "N/A");
                    cmd.Parameters.AddWithValue("@Salutation", "");
                    cmd.Parameters.AddWithValue("@First_Name", Employee_Name);
                    cmd.Parameters.AddWithValue("@Middle_Name", "");
                    cmd.Parameters.AddWithValue("@Last_Name", "");
                    cmd.Parameters.AddWithValue("@Emailid", Email);
                    cmd.Parameters.AddWithValue("@Date_birthday", Date_of_birth);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Place_Of_Birth", "");
                    cmd.Parameters.AddWithValue("@Birth_State", mycode.getstatename(State_code));
                    cmd.Parameters.AddWithValue("@Religion", Religion);
                    cmd.Parameters.AddWithValue("@Nationality", "INDIAN");
                    cmd.Parameters.AddWithValue("@Marital_Status", Marital_Status);
                    cmd.Parameters.AddWithValue("@Address_CA", Address);
                    cmd.Parameters.AddWithValue("@City_CA", City);
                    cmd.Parameters.AddWithValue("@State_CA", mycode.getstatename(State_code));
                    cmd.Parameters.AddWithValue("@Pincode_CA", Pincode);
                    cmd.Parameters.AddWithValue("@mobile_no_CA", Mobile);
                    cmd.Parameters.AddWithValue("@Residence_telephone_no_CA", "");
                    cmd.Parameters.AddWithValue("@address_pa", Address);
                    cmd.Parameters.AddWithValue("@city_pa", City);
                    cmd.Parameters.AddWithValue("@state_pa", mycode.getstatename(State_code));
                    cmd.Parameters.AddWithValue("@pin_pa", Pincode);
                    cmd.Parameters.AddWithValue("@chiled_name1", "");
                    cmd.Parameters.AddWithValue("@chiled_gender1", "");
                    cmd.Parameters.AddWithValue("@chiled_age1", "");
                    cmd.Parameters.AddWithValue("@chiled_name2", "");
                    cmd.Parameters.AddWithValue("@chiled_gender2", "");
                    cmd.Parameters.AddWithValue("@chiled_age2", "");
                    cmd.Parameters.AddWithValue("@chiled_name3", "");
                    cmd.Parameters.AddWithValue("@chiled_gender3", "");
                    cmd.Parameters.AddWithValue("@chiled_age3", "");
                    cmd.Parameters.AddWithValue("@fathername", Father_Name);
                    cmd.Parameters.AddWithValue("@father_occupation", "");
                    cmd.Parameters.AddWithValue("@mother_name", "");
                    cmd.Parameters.AddWithValue("@mother_occupation", "");
                    cmd.Parameters.AddWithValue("@Spouse_name", "");
                    cmd.Parameters.AddWithValue("@Spouses_job_is_transferable", "NO");
                    cmd.Parameters.AddWithValue("@spouse_qualification", "");
                    cmd.Parameters.AddWithValue("@spouse_profession", "");
                    cmd.Parameters.AddWithValue("@spouse_organization", "");
                    cmd.Parameters.AddWithValue("@spouse_designation", "");
                    cmd.Parameters.AddWithValue("@completed_years", "");
                    cmd.Parameters.AddWithValue("@teaching_years", "");
                    cmd.Parameters.AddWithValue("@Administration_year", "");
                    cmd.Parameters.AddWithValue("@any_other", "");
                    cmd.Parameters.AddWithValue("@Current_name_of_instituation", "");
                    cmd.Parameters.AddWithValue("@instituation_address", "");
                    cmd.Parameters.AddWithValue("@Contact_Numbe_instituation", "");
                    cmd.Parameters.AddWithValue("@Designation_work", "");
                    cmd.Parameters.AddWithValue("@joining_date", "");
                    cmd.Parameters.AddWithValue("@place_of_posting", "");
                    cmd.Parameters.AddWithValue("@Present_Salary", "");
                    cmd.Parameters.AddWithValue("@Basic_Salary_Present", "");
                    cmd.Parameters.AddWithValue("@Allowance_Present", "");
                    cmd.Parameters.AddWithValue("@Other_Benefits_Present", "");
                    cmd.Parameters.AddWithValue("@Under_Service_Bond", "NO");
                    cmd.Parameters.AddWithValue("@years_service_bond", "");
                    cmd.Parameters.AddWithValue("@Expected_Salary", "");
                    cmd.Parameters.AddWithValue("@English_read", "Yes");
                    cmd.Parameters.AddWithValue("@English_write", "Yes");
                    cmd.Parameters.AddWithValue("@English_Speak", "Yes");
                    cmd.Parameters.AddWithValue("@Hindi_read", "Yes");
                    cmd.Parameters.AddWithValue("@Hindi_write", "Yes");
                    cmd.Parameters.AddWithValue("@Hindi_speak", "Yes");
                    cmd.Parameters.AddWithValue("@Bangla_read", "Yes");
                    cmd.Parameters.AddWithValue("@Bangla_write", "Yes");
                    cmd.Parameters.AddWithValue("@Bangla_speak", "Yes");
                    cmd.Parameters.AddWithValue("@Other_Language", "");
                    cmd.Parameters.AddWithValue("@Proficiency_In_Computer", "");
                    cmd.Parameters.AddWithValue("@passport_photo", ProfilePhoto);
                    cmd.Parameters.AddWithValue("@Signature", Signature);
                    cmd.Parameters.AddWithValue("@Order_id", "");
                    cmd.Parameters.AddWithValue("@razorpay_payment_id", "");
                    cmd.Parameters.AddWithValue("@razorpay_order_id", "");
                    cmd.Parameters.AddWithValue("@razorpay_signature", "");
                    cmd.Parameters.AddWithValue("@Seat_Remarks", "OK");
                    cmd.Parameters.AddWithValue("@no_of_seat", "0");
                    cmd.Parameters.AddWithValue("@iam_fresher", "No");
                    cmd.Parameters.AddWithValue("@Emp_code", Emp_Code);
                    cmd.Parameters.AddWithValue("@Subject_Type", "N/A");
                    cmd.Parameters.AddWithValue("@Apply_From", "Online");
                    cmd.Parameters.AddWithValue("@Verification_Status", "Selected");
                    cmd.Parameters.AddWithValue("@CurrentStatus", "Selected");
                    cmd.Parameters.AddWithValue("@Remarks", "");
                    cmd.Parameters.AddWithValue("@HiringTypeId", "0");
                    cmd.Parameters.AddWithValue("@JoiningDate", "");
                    cmd.Parameters.AddWithValue("@Salary", "");
                    cmd.Parameters.AddWithValue("@JoiningRemarks", "");
                    cmd.Parameters.AddWithValue("@Gross", "0");
                    cmd.Parameters.AddWithValue("@Deduction", "0");
                    cmd.Parameters.AddWithValue("@Netsalary", "0");
                    cmd.Parameters.AddWithValue("@Emp_contribution", "0");
                    cmd.Parameters.AddWithValue("@CTC_year", "0");
                    cmd.Parameters.AddWithValue("@CTC_month", "0");
                    cmd.Parameters.AddWithValue("@income_heads", "0");
                    cmd.Parameters.AddWithValue("@deduction_head", "0");
                    cmd.Parameters.AddWithValue("@Grade", "0");
                    cmd.Parameters.AddWithValue("@DepartmentId", Department_id);
                    cmd.Parameters.AddWithValue("@DesignationId", Designation_id);
                    
                    cmd.Parameters.AddWithValue("@Pwd_Old", pwd);
                    cmd.Parameters.AddWithValue("@ActiveStatus", Status);
                    cmd.Parameters.AddWithValue("@College_Name", College_Name);


                    
                    if (My.InsertUpdateData(cmd))
                    {
                        My.exeSql("update PRL_Employee_Master set import_status='Done' where Emp_Code='" + Emp_Code + "'");
                    }
                }
            }
        }

        protected void btn_import_session_Click(object sender, EventArgs e)
        {
            

            DataTable dt = mycode.FillData(" select  *  from dbo.[session_details]   ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string name = dt.Rows[i]["Session"].ToString();
                    string session_id = dt.Rows[i]["session_id"].ToString();
                    
                    
                    DataTable dt1 = mycode.FillData(" select  *  from dbo.[HR_SessionList]  where  SessionName='"+ name + "' ");
                    if (dt1.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO HR_SessionList (SessionName,SessionId) values (@SessionName,@SessionId)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@SessionName", name);
                        cmd.Parameters.AddWithValue("@SessionId", session_id);

                        if (My.InsertUpdateData(cmd))
                        {

                        }
                    }
                    else
                    {
                        
                    }


                }
                int lastid = get_last_id("HR_SessionList", "SessionId") + 1;
                My.exeSql("update HR_AutoId set IdValue=" + lastid + " where IdName='SessionId'");
            }
        }
    }
}