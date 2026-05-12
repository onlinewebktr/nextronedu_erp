using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data; 

namespace school_web.Payroll
{
    public partial class Apply_career_Guidelines : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["sdjhfdsgfhjasbdagdag"] != null)
                {

                    //sdjhfdsgfhjasbdagdag=14&  Designation
                    //sfhsdfghjdncjszhfyshf=1& Session
                    //zhfyshfcjzshdyusahds=4& HiringParameterId
                    //ddrdefzshdyusahds=Application Developer& Apply_for
                    //pqrshfcjzshdyusahds=1 //Vacancy_Id


                    ViewState["designation"] = Request.QueryString["sdjhfdsgfhjasbdagdag"];//Designation
                    ViewState["session_id"] = Request.QueryString["sfhsdfghjdncjszhfyshf"];// session id
                    ViewState["HiringTypeId"] = Request.QueryString["zhfyshfcjzshdyusahds"];// hiring parameter id
                    ViewState["Applyfor"] = Request.QueryString["ddrdefzshdyusahds"];//applyefor
                    ViewState["hiring_id"] = Request.QueryString["pqrshfcjzshdyusahds"];// Vacancy_Idid

                    fetch_data();
                    fetch_company_name();
                }
                else
                {


                }

            }
        }

        private void fetch_company_name()
        {
            DataTable dt = PayrollMy.dataTable("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {

                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        } 
        private void fetch_data()
        {

            DataTable dt = PayrollMy.dataTable("select * from dbo.[HR_HiringParameterSetup] where  Vacancy_Id='" + ViewState["hiring_id"].ToString() + "' and HiringParameterId=" + ViewState["HiringTypeId"].ToString() + " ");
            if (dt.Rows.Count == 0)
            {
                lbl_data.Text = "";
                a1.Visible = false;
                a1.Visible = false;
                btn_terms.Visible = false;
                ViewState["amount"] = "0.00";
            }
            else
            {
                lbl_data.Text = dt.Rows[0]["General_Instruction"].ToString();
                ViewState["amount"] = dt.Rows[0]["ApplicationFee"].ToString();
                btn_terms.Visible = true;
                if (dt.Rows[0]["Attachments"].ToString() == "")
                {
                    a2.Visible = false;
                    a1.Visible = false;
                }
                else
                {
                    a1.Visible = true;
                    a2.Visible = true;
                    a1.HRef = dt.Rows[0]["Attachments"].ToString();
                }
            }
        }
        string scrpt;
        protected void btn_terms_Click(object sender, EventArgs e)
        {
            if (squaredTwo.Checked == false)
            {
                squaredTwo.Focus();
                lblmessage.Text = "Please read and accept the admission procedure.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {//,DepartmentId=@DepartmentId,DesignationId=@DesignationId

                // string regi_code = Payrollcode.reg_format_Career("Apply_Career");
                string regi_code = PayrollMy.AutoId("Apply_Career",$"EMP{PayrollMy.Now.ToString("yyMMddHHmm")}","00");
                var dt = PayrollMy.dataTable($"select * from HR_HiringParameterSetup where HiringParameterId='{ViewState["HiringTypeId"].ToString()}' ");
                PayrollMy.Insert("HR_Employee_Online_Apply", new {
                    Branchi_id=1,
                    User_id= regi_code,
                    Apply_for=ViewState["Applyfor"].ToString(),
                    Hiring_id= ViewState["hiring_id"].ToString(),
                    HiringTypeId = ViewState["HiringTypeId"].ToString(),
                    Apply_id= regi_code,
                    Date= PayrollMy.date,
                    idate= PayrollMy.Now.idate(),
                    Pay_Type="Online",
                    Payable_amount= ViewState["amount"].ToString(),
                    Payment_Status="Unpaid",
                    Session_id = ViewState["session_id"].ToString(),
                    Verification_Status= "Pending",
                    Apply_From= "Online",
                    DepartmentId = dt.Rows[0]["DepartmentId"].ToString(),
                    DesignationId = dt.Rows[0]["HiringFor"].ToString(),
                });
                 

                Session["terms"] = "terms";
                Response.Redirect("Apply_Career_Application.aspx?sfhsdfghjdncjszhfyshf=" + regi_code, false);
            }
        }
    }
}