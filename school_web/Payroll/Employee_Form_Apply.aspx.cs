using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Data; 

namespace school_web.Payroll
{
    public partial class Employee_Form_Apply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regiDs"] != null)
                {


                    ViewState["Emp_code"] = Request.QueryString["regiDs"];
                    DataTable dt = PayrollMy.dataTable("select *,(select SessionName from HR_SessionList where SessionId=Session_id) as session_name from dbo.[HR_Employee_Online_Apply] where   Apply_id='" + ViewState["Emp_code"].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        Bind_schoolinfo();
                        Bind_apply_details(dt);

                    }
                    else
                    {
                        Response.Redirect("~/home", false);
                    }


                }
                else if (Request.QueryString["empcode"] != null)
                { 
                    ViewState["Emp_code"] = Request.QueryString["empcode"];
                    DataTable dt = PayrollMy.dataTable("select *,(select SessionName from HR_SessionList where SessionId=Session_id) as session_name from dbo.[HR_Employee_Online_Apply] where   Emp_code='" + ViewState["Emp_code"] + "'");
                    var emp_id = PayrollMy.data($"select Employee_id from HR_Employee_Master where Emp_Code='{ ViewState["Emp_code"]}'");
                    DataTable d1t = PayrollMy.dataTable($"select * from HR_Employee_Documents where EmployeeId = '{emp_id}'");
                    rp_doc.DataSource = d1t;
                    rp_doc.DataBind();
                    if (dt.Rows.Count>0)
                    {
                    Bind_schoolinfo();
                    Bind_apply_details(dt);

                    }
                    else
                    {
                        Response.Redirect("../../home", false);
                    }

                }
                else
                {
                    Response.Redirect("../../home", false);
                }
            } 
        } 
        private void Bind_apply_details(DataTable dt)
        {
           
            if (dt.Rows.Count == 0)
            {

            }
            else

            {

                ViewState["Applyid"] = dt.Rows[0]["Apply_id"].ToString();
                if (dt.Rows[0]["iam_fresher"].ToString() == "Yes")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;

                }
                lbl_session1.Text = dt.Rows[0]["session_name"].ToString();
                lbl_registrationid.Text = ViewState["Emp_code"].ToString();
                lbl_date.Text = dt.Rows[0]["Date"].ToString();
                img_s_image.ImageUrl = dt.Rows[0]["passport_photo"].ToString();
                img_s_sig.ImageUrl = dt.Rows[0]["Signature"].ToString();
                lbl_applyfor.Text = dt.Rows[0]["Apply_for"].ToString();
                lbl_subject_name.Text = dt.Rows[0]["subject_name"].ToString();

                lbl_name.Text = dt.Rows[0]["First_Name"].ToString() + " " + dt.Rows[0]["Middle_Name"].ToString() + " " + dt.Rows[0]["Last_Name"].ToString();

                lbl_Emailid.Text = dt.Rows[0]["Emailid"].ToString();

                lbl_Date_birthday.Text = dt.Rows[0]["Date_birthday"].ToString();

                lbl_Gender.Text = dt.Rows[0]["Gender"].ToString();
                lbl_Place_Of_Birth.Text = dt.Rows[0]["Place_Of_Birth"].ToString();

                lbl_Birth_State.Text = dt.Rows[0]["Birth_State"].ToString();
                lbl_Religion.Text = dt.Rows[0]["Religion"].ToString();
                lbl_Nationality.Text = dt.Rows[0]["Nationality"].ToString();

                if (dt.Rows[0]["Nationality"].ToString() == "")
                {
                    lbl_Nationality.Text = dt.Rows[0]["Nationality"].ToString();
                }

                lbl_marital_status.Text = dt.Rows[0]["Marital_Status"].ToString();
                lbl_Address_ca.Text = dt.Rows[0]["Address_CA"].ToString();
                lbl_City_ca.Text = dt.Rows[0]["City_CA"].ToString();
                lbl_State_ca.Text = dt.Rows[0]["State_CA"].ToString();
                lbl_pincode_ca.Text = dt.Rows[0]["Pincode_CA"].ToString();
                lbl_mobile_no_ca.Text = dt.Rows[0]["mobile_no_CA"].ToString();
                lbl_res_tel_no.Text = dt.Rows[0]["Residence_telephone_no_CA"].ToString();
                lbl_Address_pa.Text = dt.Rows[0]["address_pa"].ToString();
                lbl_City_pa.Text = dt.Rows[0]["city_pa"].ToString();
                lbl_State_pa.Text = dt.Rows[0]["state_pa"].ToString();
                lbl_pincode_pa.Text = dt.Rows[0]["pin_pa"].ToString();

                lbl_Child1.Text = dt.Rows[0]["chiled_name1"].ToString();
                lbl_Gender1.Text = dt.Rows[0]["chiled_gender1"].ToString();
                lbl_age1.Text = dt.Rows[0]["chiled_age1"].ToString();

                lbl_Child2.Text = dt.Rows[0]["chiled_name2"].ToString();
                lbl_Gender2.Text = dt.Rows[0]["chiled_gender2"].ToString();
                lbl_age2.Text = dt.Rows[0]["chiled_age2"].ToString();

                lbl_Child3.Text = dt.Rows[0]["chiled_name3"].ToString();
                lbl_Gender3.Text = dt.Rows[0]["chiled_gender3"].ToString();
                lbl_age3.Text = dt.Rows[0]["chiled_age3"].ToString();

                lbl_fathername.Text = dt.Rows[0]["fathername"].ToString();
                lbl_father_Occupation.Text = dt.Rows[0]["father_occupation"].ToString();
                lbl_mothername.Text = dt.Rows[0]["mother_name"].ToString();

                lbl_mother_Occupation.Text = dt.Rows[0]["mother_occupation"].ToString();
                lbl_Spouses_name.Text = dt.Rows[0]["Spouse_name"].ToString();
                lbl_Spouses_job.Text = dt.Rows[0]["Spouses_job_is_transferable"].ToString();


                lbl_Spousequlification.Text = dt.Rows[0]["spouse_qualification"].ToString();
                lbl_Spouses_Profession.Text = dt.Rows[0]["spouse_profession"].ToString();

                lbl_Spouses_Organization.Text = dt.Rows[0]["spouse_organization"].ToString();
                lbl_Spouses_Designation.Text = dt.Rows[0]["spouse_designation"].ToString();

                lbl_Completed_Years.Text = dt.Rows[0]["completed_years"].ToString();

                lbl_Teaching_years.Text = dt.Rows[0]["teaching_years"].ToString();
                lbl_Administration_yers.Text = dt.Rows[0]["Administration_year"].ToString();

                lbl_any_other_yers.Text = dt.Rows[0]["any_other"].ToString();
                lbl_name_Institution.Text = dt.Rows[0]["Current_name_of_instituation"].ToString();

                lbl_Address_institution.Text = dt.Rows[0]["instituation_address"].ToString();
                lbl_Contact_institution.Text = dt.Rows[0]["Contact_Numbe_instituation"].ToString();

                lbl_Present_previous_designation.Text = dt.Rows[0]["Designation_work"].ToString();
                lbl_date_of_joining.Text = dt.Rows[0]["joining_date"].ToString();
                lbl_Place_of_Posting.Text = dt.Rows[0]["place_of_posting"].ToString();
                lbl_present_salery.Text = dt.Rows[0]["Present_Salary"].ToString();
                lbl_Basic_salery.Text = dt.Rows[0]["Basic_Salary_Present"].ToString();
                lbl_Allowance.Text = dt.Rows[0]["Allowance_Present"].ToString();


                lbl_Other_Benefits.Text = dt.Rows[0]["Other_Benefits_Present"].ToString();
                if (dt.Rows[0]["Under_Service_Bond"].ToString() == "Yes")
                {
                    lbl_are_you_uder_service.Text = "YES, No. of Service Bond Year :" + dt.Rows[0]["years_service_bond"].ToString();

                }
                else
                {
                    lbl_are_you_uder_service.Text = "NO";
                }

                lbl_registrationid.Text = dt.Rows[0]["Emp_code"].ToString();
                lbl_Expected_Salsry.Text = dt.Rows[0]["Expected_Salary"].ToString();
                lbl_any_other_langwage.Text = dt.Rows[0]["Other_Language"].ToString();
                lbl_Proficiency_computer.Text = dt.Rows[0]["Proficiency_In_Computer"].ToString();

                Bind_academy_details();
                Bnind_servce_experience();


                grid_english.DataSource = dt;
                grid_english.DataBind();

                grid_hindi.DataSource = dt;
                grid_hindi.DataBind();

                grid_bangali.DataSource = dt;
                grid_bangali.DataBind();
                
            }
        }

        
       
        private void Bnind_servce_experience()
        {
            DataTable dt = PayrollMy.dataTable("  Select * from   HR_Employee_Online_Apply_Work_Experiance    where Apply_id='" + ViewState["Applyid"].ToString() + "'    ");
            if (dt.Rows.Count == 0)
            {

                grid_work_experiance.DataSource = null;
                grid_work_experiance.DataBind();
            }
            else
            {
                grid_work_experiance.DataSource = dt;
                grid_work_experiance.DataBind();
            }
        }
        int total_experianceday = 0;
        protected void grid_work_experiance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Total_Days = (Label)e.Row.FindControl("lbl_Total_Days");


                if (lbl_Total_Days.Text != "")
                {
                    total_experianceday = total_experianceday +lbl_Total_Days.Text.ToInt();
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_Total_Days_row = (Label)e.Row.FindControl("lbl_Total_Days_row");

                lbl_Total_Days_row.Text = total_experianceday.ToString();

            }
        }
        private void Bind_academy_details()
        {
            DataTable dt = PayrollMy.dataTable("select *   from dbo.[HR_Employee_Online_Academy_Details] where    Apply_id='" + ViewState["Applyid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                grid_Academic.DataSource = null;
                grid_Academic.DataBind();
            }
            else

            {
                grid_Academic.DataSource = dt;
                grid_Academic.DataBind();
            }
        }
         
        private void Bind_schoolinfo()
        {
            DataTable dt = PayrollMy.dataTable("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_email_school.Text = dt.Rows[0]["email"].ToString();
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                lbl_contact_no.Text = "Contact Number : " + dt.Rows[0]["contact_no"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pending_Application_List_For_Employee_Hiring.aspx", false);
        }

        protected void rp_doc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var lbl_doc_path = e.Item.FindControl("lbl_doc_path") as Label;
            var lbl_doc_type = e.Item.FindControl("lbl_doc_type") as Label;
            var lbl_doc = e.Item.FindControl("lbl_doc") as Literal;
            var ext = System.IO.Path.GetExtension(lbl_doc_path.Text.ToLower());
            var imgs = new String[] { ".png", ".gif", ".jpg", ".jpeg", ".bmp", ".svg" };
            if (imgs.Contains(ext))
            {
                var dd = $@"<a href='{lbl_doc_path.Text}' target='_blank' >
                                                                <img alt='document' src='{lbl_doc_path.Text}'  style='height:130px'/> 
                                                            </a>";
                lbl_doc.Text = dd;
            }
            else
            {
                var dd = $@"<a href='{lbl_doc_path.Text}' download='{lbl_doc_type.Text}{ext}' > 
                                                                <i class='bx bx-download' style='font-size:35px'></i> 
                                                            </a>";
                lbl_doc.Text = dd;
            }
        }
            
            
        
    }
}