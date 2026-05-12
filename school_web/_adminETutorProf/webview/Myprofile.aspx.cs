using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Data;
using school_web.AppCode;
namespace school_web._adminETutorProf.webview
{
    public partial class Myprofile : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["teacherid"] != null)
                {
                    ViewState["teacherid"] = Request.QueryString["teacherid"].ToString();
                    try
                    {
                        Bind_data();
                    }
                    catch
                    {

                    }
                }


            }
        }

        private void Bind_data()
        {
            DataTable dt = mycode.FillTable("Select *,( select  top 1 grade_name from dbo.[PRL_Grade_Master] where grade_id=PRL_Employee_Master.Grade_id ) as gradename,( select  top 1 name from dbo.[PRL_Department_Master] where department_id=PRL_Employee_Master.Department_id ) as Department_name,( select  top 1 name from dbo.[PRL_Designation_Master] where description_id=PRL_Employee_Master.Designation_id ) as Designation_name,(Select State from StateList where Code=PRL_Employee_Master.State_code) as statname from PRL_Employee_Master where Emp_Code='" + ViewState["teacherid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_employeename.Text = dt.Rows[0]["Employee_Name"].ToString();
                lbl_gender.Text = dt.Rows[0]["Gender"].ToString();
                lbl_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                lbl_bloadgroup.Text = dt.Rows[0]["Blood_group"].ToString();
                lbl_Religion.Text = dt.Rows[0]["Religion"].ToString();
                lbl_Marital_status.Text = dt.Rows[0]["Marital_Status"].ToString();
                lbl_fathername.Text = dt.Rows[0]["Father_Name"].ToString();
                lbl_pan_no.Text = dt.Rows[0]["Pan"].ToString();
                lbl_address.Text = dt.Rows[0]["Address"].ToString();
                lbl_city.Text = dt.Rows[0]["City"].ToString();
                lbl_Pincode.Text = dt.Rows[0]["Pincode"].ToString();
                lbl_state.Text = dt.Rows[0]["statname"].ToString();
                lbl_email.Text = dt.Rows[0]["Email"].ToString();
                lbl_mobileno.Text = dt.Rows[0]["Mobile"].ToString();
                lbl_bank_name.Text = dt.Rows[0]["Bank_Name"].ToString();
                lbl_Branch.Text = dt.Rows[0]["Branch"].ToString();
                lbl_account_no.Text = dt.Rows[0]["Account_no"].ToString();
                lbl_ifsc.Text = dt.Rows[0]["Ifsc"].ToString();
                lbl_MICR.Text = dt.Rows[0]["Micr"].ToString();
                lbl_employeecode.Text = dt.Rows[0]["Emp_Code"].ToString();
                lbl_punchcardno.Text = dt.Rows[0]["Punch_Card_no"].ToString();
                lbl_officialemailid.Text = dt.Rows[0]["Official_email_id"].ToString();
                lbl_qualification.Text = dt.Rows[0]["Qualification"].ToString();

                lbl_grade.Text = dt.Rows[0]["gradename"].ToString();
                lbl_department.Text = dt.Rows[0]["Department_name"].ToString();

                lbl_Designation.Text = dt.Rows[0]["Designation_name"].ToString();

                lbl_epfno.Text = dt.Rows[0]["EPF_no"].ToString();
                lbl_joindate.Text = dt.Rows[0]["EPF_Join_date"].ToString();
                lbl_pfleavingdate.Text = dt.Rows[0]["PF_Leaving_date"].ToString();
                lbl_Reason.Text = dt.Rows[0]["PF_leaving_Reagion"].ToString();
                lbl_ESIC_no.Text = dt.Rows[0]["ESIC_no"].ToString();

                lbl_esijoindate.Text = dt.Rows[0]["ESIC_join_date"].ToString();
                lbl_esic_leavingdate.Text = dt.Rows[0]["ESIC_leaving_date"].ToString();
                lbl_Reason_esileaving.Text = dt.Rows[0]["ESIC_leaving_Reagion"].ToString();
                lbl_Date_of_Joining.Text = dt.Rows[0]["Date_of_Joining"].ToString();

                Bind_zoomuseridnad_pwd();
                
            }
        }

        private void Bind_zoomuseridnad_pwd()
        {
            DataTable dt = mycode.FillTable("Select *  from Zoom_API  where teacher_id='" + ViewState["teacherid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                lbl_zoomuserid.Text = "xxxxx";
                lbl_zoompwd.Text = "xxxxx";
            }
            else
            {
                lbl_zoomuserid.Text = dt.Rows[0]["User_ID"].ToString();
               
                lbl_zoompwd.Text = dt.Rows[0]["Password"].ToString();
            }
        }
    }
}