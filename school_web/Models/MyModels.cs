using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace school_web
{
    public class MyModels
    {

    }
    public class HR_Salary_Calculation_Table
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int Days_in_Month { get; set; }
        public int Working_Days_In_a_Month { get; set; }
        public string Grade { get; set; }
        public int Full_Day_Present { get; set; }
        public int Half_Day_Present { get; set; }
        public int Late_Half_Day_Deduction { get; set; }
        public int Late_Full_Day_Deduction { get; set; }
        public int Late_Coming_Days { get; set; }
        public int Paid_Leave { get; set; }
        public int UnPaidLeave { get; set; }
        public int Half_Pay_leave { get; set; }
        public double Basic { get; set; }
        public double Allwance { get; set; }
        public double Gross { get; set; }
        public double Deduction { get; set; }
        public double Net { get; set; }
        public double Calculeted_Basic { get; set; }
        public double Calculeted_Allwance { get; set; }
        public double Calculeted_Gross { get; set; }
        public double Calculeted_Deduction { get; set; }
        public double Calculeted_Net { get; set; }
        public double Worked_Days { get; set; }
        public double Deduction_Days { get; set; }
        public double Net_Wroked_Days { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Calculation_Id { get; set; }
        public double week_off { get; set; }
        public double Late_Deduction { get; set; }
        public int Calculated_Week_off { get; set; }
        public double Extra_Shift { get; set; }
    }
    public class HR_Employee_Master
    {
        public int Id { get; set; }
        public string Employee_Name { get; set; }
        public string Gender { get; set; }
        public string Date_of_birth { get; set; }
        public string Blood_group { get; set; }
        public string Religion { get; set; }
        public string Marital_Status { get; set; }
        public string Father_Name { get; set; }
        public string Pan { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string State_code { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Employee_image { get; set; }
        public string Emp_Code { get; set; }
        public string Punch_Card_no { get; set; }
        public string Official_email_id { get; set; }
        public string Grade_id { get; set; }
        public string Department_id { get; set; }
        public string Designation_id { get; set; }
        public string EPF_no { get; set; }
        public string EPF_Join_date { get; set; }
        public string PF_Leaving_date { get; set; }
        public string PF_leaving_Reagion { get; set; }
        public string ESIC_no { get; set; }
        public string ESIC_join_date { get; set; }
        public string ESIC_leaving_date { get; set; }
        public string ESIC_leaving_Reagion { get; set; }
        public string Employee_id { get; set; }
        public string document_type { get; set; }
        public string Date_of_Joining { get; set; }
        public string Bank_Name { get; set; }
        public string Branch { get; set; }
        public string Ifsc { get; set; }
        public string Micr { get; set; }
        public string iDOB { get; set; }
        public string Account_no { get; set; }
        public string basic_salary { get; set; }
        public string employee_type { get; set; }
        public string Qualification { get; set; }
        public string deduction_heads { get; set; }
        public string income_heads { get; set; }
    }
    public class Employee_Apply
    {
        public int Id { get; set; }
        public string Branchi_id { get; set; }
        public string User_id { get; set; }
        public string Apply_for { get; set; }
        public string Hiring_id { get; set; }
        public string Apply_id { get; set; }
        public string Date { get; set; }
        public int idate { get; set; }
        public string Pay_Type { get; set; }
        public string Payable_amount { get; set; }
        public string Payment_Status { get; set; }
        public int Session_id { get; set; }
        public string subject_name { get; set; }
        public string Salutation { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Emailid { get; set; }
        public string Date_birthday { get; set; }
        public string Gender { get; set; }
        public string Place_Of_Birth { get; set; }
        public string Birth_State { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Marital_Status { get; set; }
        public string Address_CA { get; set; }
        public string City_CA { get; set; }
        public string State_CA { get; set; }
        public string Pincode_CA { get; set; }
        public string mobile_no_CA { get; set; }
        public string Residence_telephone_no_CA { get; set; }
        public string address_pa { get; set; }
        public string city_pa { get; set; }
        public string state_pa { get; set; }
        public string pin_pa { get; set; }
        public string chiled_name1 { get; set; }
        public string chiled_gender1 { get; set; }
        public string chiled_age1 { get; set; }
        public string chiled_name2 { get; set; }
        public string chiled_gender2 { get; set; }
        public string chiled_age2 { get; set; }
        public string chiled_name3 { get; set; }
        public string chiled_gender3 { get; set; }
        public string chiled_age3 { get; set; }
        public string fathername { get; set; }
        public string father_occupation { get; set; }
        public string mother_name { get; set; }
        public string mother_occupation { get; set; }
        public string Spouse_name { get; set; }
        public string Spouses_job_is_transferable { get; set; }
        public string spouse_qualification { get; set; }
        public string spouse_profession { get; set; }
        public string spouse_organization { get; set; }
        public string spouse_designation { get; set; }
        public string completed_years { get; set; }
        public string teaching_years { get; set; }
        public string Administration_year { get; set; }
        public string any_other { get; set; }
        public string Current_name_of_instituation { get; set; }
        public string instituation_address { get; set; }
        public string Contact_Numbe_instituation { get; set; }
        public string Designation_work { get; set; }
        public string joining_date { get; set; }
        public string place_of_posting { get; set; }
        public string Present_Salary { get; set; }
        public string Basic_Salary_Present { get; set; }
        public string Allowance_Present { get; set; }
        public string Other_Benefits_Present { get; set; }
        public string Under_Service_Bond { get; set; }
        public string years_service_bond { get; set; }
        public string Expected_Salary { get; set; }
        public string English_read { get; set; }
        public string English_write { get; set; }
        public string English_Speak { get; set; }
        public string Hindi_read { get; set; }
        public string Hindi_write { get; set; }
        public string Hindi_speak { get; set; }
        public string Bangla_read { get; set; }
        public string Bangla_write { get; set; }
        public string Bangla_speak { get; set; }
        public string Other_Language { get; set; }
        public string Proficiency_In_Computer { get; set; }
        public string passport_photo { get; set; }
        public string Signature { get; set; }
        public string Order_id { get; set; }
        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
        public string Seat_Remarks { get; set; }
        public string no_of_seat { get; set; }
        public string iam_fresher { get; set; }
        public string Emp_code { get; set; }
        public string Subject_Type { get; set; }
        public string Apply_From { get; set; }
        public string Verification_Status { get; set; }
        public string CurrentStatus { get; set; }
        public string Remarks { get; set; }
        public string HiringTypeId { get; set; }
        public string JoiningDate { get; set; }
        public string Salary { get; set; }
        public string JoiningRemarks { get; set; }
        public string Gross { get; set; }
        public string Deduction { get; set; }
        public string Netsalary { get; set; }
        public string Emp_contribution { get; set; }
        public string CTC_year { get; set; }
        public string CTC_month { get; set; }
        public string income_heads { get; set; }
        public string deduction_head { get; set; }
        public string Grade { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; } 
        public string Pwd_Old { get; set; } 
        public string district_ca { get; set; }
        public string ps_ca { get; set; }
        public string district_pa { get; set; }
        public string ps_pa { get; set; }
        public string spouse_mobile_no { get; set; } 
    }
    public class PRL_Employee_Master
    {
        public int Id { get; set; }
        public string Employee_Name { get; set; }
        public string Gender { get; set; }
        public string Date_of_birth { get; set; }
        public string Blood_group { get; set; }
        public string Religion { get; set; }
        public string Marital_Status { get; set; }
        public string Father_Name { get; set; }
        public string Pan { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public int State_code { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Emp_Code { get; set; }
        public string Punch_Card_no { get; set; }
        public string Official_email_id { get; set; }
        public int Grade_id { get; set; }
        public int Department_id { get; set; }
        public int Designation_id { get; set; }
        public string EPF_no { get; set; }
        public string EPF_Join_date { get; set; }
        public string PF_Leaving_date { get; set; }
        public string PF_leaving_Reagion { get; set; }
        public string ESIC_no { get; set; }
        public string ESIC_join_date { get; set; }
        public string ESIC_leaving_date { get; set; }
        public string ESIC_leaving_Reagion { get; set; }
        public string Employee_id { get; set; }
        public string document_type { get; set; }
        public string Date_of_Joining { get; set; }
        public string Bank_Name { get; set; }
        public string Branch { get; set; }
        public string Ifsc { get; set; }
        public string Micr { get; set; }
        public int iDOB { get; set; }
        public string Account_no { get; set; }
        public string Status { get; set; }
        public string Qualification { get; set; }
        public string employee_type { get; set; }
        public string About_Teacher { get; set; }
        public string ProfilePhoto { get; set; }
        public string College_Name { get; set; }
        public string Divie_added_or_not { get; set; }
        public string basic_salary { get; set; }
        public string loi_idate { get; set; }
        public string loi_serial { get; set; }
        public string loi_date { get; set; }
        public string employee_machine_id { get; set; }
        public string Husband_name { get; set; }
        public string Aadhar_no { get; set; }
        public string Employee_image { get; set; }
    }


    public class Firm_Details
    {
        public int Id { get; set; }
        public string firm_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string gstin { get; set; }
        public string contact_no { get; set; }
        public string firm_id { get; set; }
        public string Registration_Type { get; set; }
        public string State { get; set; }
        public string State_Code { get; set; }
        public string nature_of_business { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string rate_tax { get; set; }
        public string dl_no { get; set; }
        public string Company_Name_Short { get; set; }
        public string logo { get; set; }
    }
    public class HR_MasterPageConfig
    {
        public int Id { get; set; }
        public string pageId { get; set; }
        public string tableName { get; set; }
        public string pageTitle { get; set; }
        public string pageUrl { get; set; }
        public string pageType { get; set; }
        public string formType { get; set; }
        public string methodId { get; set; }
        public ActionModel action { get; set; }
        public List<FieldConfigData> pcd { get; set; }
        public List<GridConfigData> gcd { get; set; }
        public List<Filter> filters { get; set; }
        public List<Events> events { get; set; }
        public int Status { get; set; }
        public string dataQuery { get; set; }
        public string sampleQuery { get; set; }
        public bool isCustomisedGrid { get; set; }
        public Dictionary<string, string> OtherData { get; set; }
    }
    //[{"columnName":"pageId","displayText":"page Id","dataType":"Text","requiredData":"","isShowOnGrid":true}
    public class GridConfigData
    {
        public string columnName { get; set; }
        public string displayText { get; set; }
        public string dataType { get; set; }
        public string requiredData { get; set; }
        public bool isShowOnGrid { get; set; }
    }
    //{"columnName":"pageId","displayText":"page Id","fieldType":"TextBox","requiredData":"dfasdfasd","isRequired":true}
    public class FieldConfigData
    {
        public string columnName { get; set; }
        public string displayText { get; set; }
        public string fieldType { get; set; }
        public string requiredData { get; set; }
        public bool isRequired { get; set; }
        public bool isField { get; set; }
        public bool onChanged { get; set; }
        public string onChangedField { get; set; }
        public string defaultValue { get; set; }
        public string cssclass { get; set; }
    }
    //{"isEditAllow":false,"isDeleteAllow":true,"isAddNewAllow":true}
    public class ActionModel
    {
        public bool isAddNewAllow { get; set; }
        public bool isDeleteAllow { get; set; }
        public bool isEditAllow { get; set; }
        public string duplicate_filter { get; set; }
        public ActionList[] actionList { get; set; }
    }
    public class ActionList
    {
        public string uniqueId { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string type { get; set; }
        public string pageurl { get; set; }
        public string pagetype { get; set; }
        public string btn_text { get; set; }
        public string act_resp_type { get; set; }
        public string action_id { get; set; }
        public string[] col_list { get; set; }
        public FieldConfigData[] act_pcd { get; set; }
    }
    //[{"filterName":"Between Two Date","filterquery":"asda asdf sadf ","btnName":"Find","filteritems"
    public class Filter
    {
        public string uniqueId { get; set; }
        public string filterName { get; set; }
        public string filterquery { get; set; }
        public string btnName { get; set; }
        public FilterItem[] filteritems { get; set; }
    }
    //name":"Start Date","type":"DatePicker","datatype":"","dataquery":"","onchange":""
    public class FilterItem
    {
        public string name { get; set; }
        public string type { get; set; }
        public string datatype { get; set; }
        public string onchange { get; set; }
        public string dataquery { get; set; }
        public string id { get; set; }
    }
    //name":"Start Date","type":"DatePicker","datatype":"","dataquery":"","onchange":""
    public class Events
    {
        public string fieldName { get; set; }
        public string eventType { get; set; }
        public string eventData { get; set; }
    }
}