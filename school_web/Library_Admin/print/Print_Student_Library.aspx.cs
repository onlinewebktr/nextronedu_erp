using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin.print
{
    public partial class Print_Student_Library : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Lib_Bar_code"] != null)
                {
                    A1.HRef = "../Print_Student_Library_card.aspx";
                    string Lib_Bar_code = Request.QueryString["Lib_Bar_code"].ToString();
                    string session = Request.QueryString["session"].ToString();
                    
                    Bind_data_single_multipul(Lib_Bar_code, session);
                    firm_data();
                }
            }
        }

        private void firm_data()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_school_name.Text = dt.Rows[0]["firm_name"].ToString();
                
                schoollogo.ImageUrl = dt.Rows[0]["logo"].ToString();

                if (dt.Rows[0]["firm_name"].ToString() == "DELHI PUBLIC SCHOOL, NTPC FARAKKA")
                {
                    lbl_under.Visible = true;

                }
                try
                {


                    lbl_affilation_no.Text = dt.Rows[0]["Affiliation_by"].ToString() + " Aff No.-" + dt.Rows[0]["Affiliation"].ToString();
                }
                catch
                {
                    lbl_affilation_no.Text = "CBSE Aff. No.-" + dt.Rows[0]["Affiliation"].ToString();
                }


                lbl_address.Text = "" + dt.Rows[0]["address1"].ToString();
                lbl_mobileno_emailid.Text = "Telephone No :" + dt.Rows[0]["contact_no"].ToString() + " , E-mail Address :" + dt.Rows[0]["email"].ToString();

            }
        }

        My mycode = new My();
        private void Bind_data_single_multipul(string admision_no_barcode,string session)
        {
            string query = "Select lib_card_no,studentimagepath,admissionserialnumber,class,rollnumber,Section,session,studentname,fathername,mothername,mobilenumber,lib_card_Issuedate,lib_card_Valid_up_to_Date,Lib_Bar_code_img from  admission_registor where Lib_Bar_code in(" + admision_no_barcode + ") and Session_id='" + session + "' order by rollnumber asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {
                img_barcode.ImageUrl = dt.Rows[0]["Lib_Bar_code_img"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();
                if (dt.Rows[0]["studentimagepath"].ToString() == "")
                {
                    student_image.ImageUrl = "~/Library_Admin/print/dummy-student.jpg";
                }
                else
                {
                    student_image.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();

                }
                lbl_Id_no.Text = dt.Rows[0]["lib_card_no"].ToString();
                lbl_issue_date.Text = dt.Rows[0]["lib_card_Issuedate"].ToString();
                lbl_validupto.Text = dt.Rows[0]["lib_card_Valid_up_to_Date"].ToString();
                lbl_student_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
                lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_father_name.Text = dt.Rows[0]["fathername"].ToString();
                lbl_mother_name.Text = dt.Rows[0]["mothername"].ToString();
                lbl_contact_no.Text = dt.Rows[0]["mobilenumber"].ToString();
                Bind_note();
                Bind_principla_sig();
            }

        }

        private void Bind_principla_sig()
        {
            DataTable dt = mycode.FillData("select Signature from dbo.[user_details] where User_Type='Principal'");
            if (dt.Rows.Count == 0)
            {


            }
            else
            {
                Image3.ImageUrl = dt.Rows[0]["Signature"].ToString();
                Image1_princip.ImageUrl = dt.Rows[0]["Signature"].ToString();
            }
        }

        private void Bind_note()
        {
            DataTable dt = mycode.FillData("select * from dbo.[Slip_note] where Slip_type='Book Issue Slip Note'");
            if (dt.Rows.Count == 0)
            {


            }
            else
            {
                lbl_note1.Text = dt.Rows[0]["Description_note"].ToString();
            }
        }

        protected void ImageButton_print_Click(object sender, ImageClickEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}