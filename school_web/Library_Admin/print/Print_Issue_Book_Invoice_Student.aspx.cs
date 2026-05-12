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
    public partial class Print_Issue_Book_Invoice : System.Web.UI.Page
    {
        My mycode = new My();

        string location = "Select top 1 location from lib_location_details where Location_id=lbe.Location and Branch_Id=lbe.Branch_id";

        string Sub_Location = "Select top 1 Sub_Location from lib_sub_location_details where Sub_Location_id=lbe.Sub_Location_id and Branch_Id=lbe.Branch_id";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["adm"] != null)
                {
                    ViewState["admissionno"] = Request.QueryString["adm"];
                    ViewState["Slip_no"] = Request.QueryString["Slip_no"];

                    Bind_schoolinfo();
                    student_details();
                    Bind_particular();
                    check_print_type();
                }
            }


        }

        private void Bind_particular()
        {
            string query = "select lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date,lbe.NoOfPages,lbe.Bar_code,("+ location + ") as location,(" + Sub_Location + ") as Sub_Location  from lib_student_transaction_details  lst join Library_Book_Entry lbe on lbe.BookId=lst.book_no join Library_Book_Entry_Master_Uniqe bq on bq.Book_Unique_Identifier=lbe.Book_Unique_Identifier where  lst.transaction_no='" + ViewState["Slip_no"].ToString() + "'  and  lst.student_id='" + ViewState["admissionno"].ToString() + "'  order by  lst.id";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
                grd_fee1.DataSource = null;
                grd_fee1.DataBind();
            }
            else
            {
                grd_fee.DataSource = dt;
                grd_fee.DataBind();

                grd_fee1.DataSource = dt;
                grd_fee1.DataBind();

                lbl_paymentdate1.Text = lbl_paymentdate.Text = dt.Rows[0]["issue_date"].ToString();
                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["transaction_no"].ToString();

                string url = "https://api.qrserver.com/v1/create-qr-code/?data=Name: " + lbl_studentname.Text + ", Admission No. :"+ lbl_aadmissionno.Text + ",  Issue Date: " + lbl_paymentdate1.Text + ", Receipt No: " + lbl_slipno.Text + "&amp;size=100x100";
                qrcode.Src= qrcode2.Src = url;






            }
        }

        private void student_details()
        {

            DataTable dt = mycode.FillData("select top 1 studentname,admissionserialnumber,class,Section,fathername,session,rollnumber,Id  from admission_registor where admissionserialnumber='" + ViewState["admissionno"].ToString() + "' order by id desc  ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["studentname"].ToString();
                lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["admissionserialnumber"].ToString();

                lbl_class.Text = lbl_class1.Text = dt.Rows[0]["class"].ToString() + " Section-" + dt.Rows[0]["Section"].ToString();
                lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["fathername"].ToString();
                lbl_session.Text = lbl_session1.Text = dt.Rows[0]["session"].ToString();
                lbl_rollno.Text = lbl_rollno1.Text = dt.Rows[0]["rollnumber"].ToString();

            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_affiliation_no.Text = lbl_affiliation_no1.Text = dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = lbl_schoolno1.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl = Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = lbl_address1.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = lbl_emaiid1.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = lbl_website1.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = lbl_contact_details1.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = lbl_heading1.Text = dt.Rows[0]["firm_name"].ToString();

                if (dt.Rows[0]["Is_mobile_no_show_in_bill"].ToString() == "True")
                {
                    contact_no.Visible = true;
                    contact_no1.Visible = true;
                }
            }
        }

        private void check_print_type()
        {
            hd_print_type.Value = "1";
            try
            {
                DataTable dt = mycode.FillData("select Monthly_slip_print_type from globle_data");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Monthly_slip_print_type"].ToString() != "")
                    {
                        hd_print_type.Value = dt.Rows[0]["Monthly_slip_print_type"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (hd_print_type.Value == "1")
            {
                rdo_both.Checked = true;
                rdo_office_copy.Checked = false;
                rdo_student_copy.Checked = false;
            }
            else if (hd_print_type.Value == "2")
            {
                rdo_both.Checked = false;
                rdo_office_copy.Checked = true;
                rdo_student_copy.Checked = false;
            }
            else
            {
                rdo_both.Checked = false;
                rdo_office_copy.Checked = false;
                rdo_student_copy.Checked = true;
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["reprintstudent_issuebook"].ToString() == "1")
                {
                    Response.Redirect("../Issue_Book.aspx", false);
                }

                else
                {
                    Response.Redirect("../Student_History.aspx", false);
                }
            }
            catch
            {
                Response.Redirect("../Issue_Book.aspx", false);
            }
        }

        protected void print1_Click(object sender, EventArgs e)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}