using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.Admin.slip
{
    public partial class Student_Payment_History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Request.QueryString["admNo"] != null)
                {
                    ViewState["admissionno"] = Request.QueryString["admNo"];
                    ViewState["session_id"] = Request.QueryString["Session"];
                    ViewState["class"] = Request.QueryString["class"];
                    try
                    {
                        ViewState["mobile"] = Request.QueryString["mobile"];
                        if (ViewState["mobile"].ToString().ToLower() == "yes")
                        {
                            btn_back.Visible = false;
                        }
                    }
                    catch
                    {

                    }
                    Bind_schoolinfo();

                    get_student_info();
                    get_student_payment_history();
                    fetch_signature();


                }
                else
                {
                }
            }
        }

        private void fetch_signature()
        {
            DataTable dt = mycode.FillData("select Signature from user_details where User_Type='Accountant' ");
            if (dt.Rows.Count > 0)
            {
                signDVS.Visible = true;
                Image1.ImageUrl = dt.Rows[0]["Signature"].ToString();
            }
            else
            {
                signDVS.Visible = false;
            }
        }

        private void get_student_payment_history()
        {
            DataTable dt = mycode.FillData("select * from (select Slip_no,Date,Type,mode,Description,Amount,Idate,Type as FeeType from Student_Payment_History  where Session='" + ViewState["Session"].ToString() + "' and Addmission_no='" + ViewState["admissionno"].ToString() + "' and Class_id='" + ViewState["class"].ToString() + "'  UNION all select Slipid Slip_no, Payment_date Date, 'Other Fee (' + Content_Name + ')' Type, Payment_mode mode, Remarks Description, Content_Fee Amount, Payment_Idate Idate,'OtherFees' as FeeType from dbo.[Other_Fee_Taken_For_Student] where Session_id = '" + ViewState["session_id"].ToString() + "' and Class_id = '" + ViewState["class"].ToString() + "' and Admission_no = '" + ViewState["admissionno"].ToString() + "') t ORDER BY Idate asc");

            if (dt.Rows.Count == 0)
            {
                lbl_msg.Text = "Sorry! There are no payment history exists";
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                lbl_msg.Text = "";
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
            }
        }

        private void get_student_info()
        {
            string query = "Select class,admissionserialnumber,session,studentname,fathername,mobilenumber,Section,rollnumber,Category_id,SubCategory_id,Class_id,Session_id from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' and Session_id='" + ViewState["session_id"].ToString() + "' and Class_id='" + ViewState["class"].ToString() + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                ViewState["Session"] = dt.Rows[0]["session"].ToString();

                lbl_studentname.Text = dt.Rows[0]["studentname"].ToString();
                lbl_aadmissionno.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_class.Text = dt.Rows[0]["class"].ToString();
                lbl_fathername.Text = dt.Rows[0]["fathername"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();

                if (dt.Rows[0]["rollnumber"].ToString() == "")
                {
                    lbl_rollno.Text = "N/A";
                }
                else if (dt.Rows[0]["rollnumber"].ToString() == "&nbsp;")
                {
                    lbl_rollno.Text = "N/A";
                }
                else
                {
                    lbl_rollno.Text = dt.Rows[0]["rollnumber"].ToString();
                }

                if (dt.Rows[0]["Section"].ToString() == "")
                {
                    lbl_section.Text = "N/A";
                }
                else if (dt.Rows[0]["Section"].ToString() == "&nbsp;")
                {
                    lbl_section.Text = "N/A";
                }
                else
                {
                    lbl_section.Text = dt.Rows[0]["Section"].ToString();
                }





                ViewState["Class_id"] = dt.Rows[0]["Class_id"].ToString();
                ViewState["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["Category_id"] = dt.Rows[0]["Category_id"].ToString();
                ViewState["SubCategory_id"] = dt.Rows[0]["SubCategory_id"].ToString();
            }
        }


        My mycode = new My();
        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_affiliation_no.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

            }
        }

        double total_pay = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Amount = (Label)e.Row.FindControl("lbl_Amount");

                if (lbl_Amount.Text != "")
                {
                    total_pay = total_pay + My.toDouble(lbl_Amount.Text);
                }


            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_paid = (Label)e.Row.FindControl("lbl_total_paid");
                lbl_total_paid.Text = total_pay.ToString("0.00");

            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["pagel"].ToString() == "1")
                {
                    Response.Redirect("../datewise-student.aspx", false);
                }
                else
                {
                    Response.Redirect("../student-list.aspx", false);
                }
            }
            catch
            {
                Response.Redirect("../student-list.aspx", false);
            }
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}