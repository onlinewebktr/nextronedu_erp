using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class online_admission_fee_taken : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["regids"] != null)
                    {
                        ViewState["admissionno"] = Request.QueryString["regids"];
                        ViewState["sessionid"] = Request.QueryString["sessionid"]; 
                        Bind_schoolinfo();
                        student_details();
                        bind_amount();
                        Bind_particular();
                        check_print_type();
                    }
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


        private void bind_amount()
        {
            DataTable dt = mycode.FillData("select * from Online_admission_fee_taken_history where Registration_no='" + ViewState["admissionno"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                ViewState["Amount"] = "0";
            }
            else
            {
                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["Slip_no"].ToString();
                lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["Created_date"].ToString();


                int number = (int)Convert.ToDouble(dt.Rows[0]["Admission_fee_taken"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only";
                lbl_amountinword.Text = lbl_amountinword1.Text = inword;
                ViewState["Amount"] = dt.Rows[0]["Admission_fee_taken"].ToString();

                lbl_paymentmode1.Text = lbl_paymentmode.Text = dt.Rows[0]["Payment_mode"].ToString();
                if (dt.Rows[0]["Payment_transaction_no"].ToString() != "")
                {
                    lbl_trans_no1.Text = lbl_trans_no.Text = dt.Rows[0]["Payment_transaction_no"].ToString();
                    lbl_trans_no1Dv.Visible = true;
                    lbl_trans_noDv.Visible = true;
                }
                else
                {
                    lbl_trans_no1.Text = lbl_trans_no.Text = dt.Rows[0]["Payment_transaction_no"].ToString();
                    lbl_trans_no1Dv.Visible = false;
                    lbl_trans_noDv.Visible = false;
                }

                if (dt.Rows[0]["Remark"].ToString() != "")
                {
                    lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remark"].ToString();
                    remarksDv.Visible = true;
                    remarksDv1.Visible = true;
                }
                else
                {
                    lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remark"].ToString();
                    remarksDv.Visible = false;
                    remarksDv1.Visible = false;
                }
            }
        }

        //ViewState["admissionno"] = Request.QueryString["regids"];
        //                ViewState["sessionid"]

        private void Bind_particular()
        {
            string quer = "select * from Online_admission_fee_taken_history where Registration_no='" + ViewState["admissionno"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "' ";
            DataTable dt = mycode.FillData(quer);
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
            }

        }




        private void student_details()
        {
            DataTable dt = mycode.FillData("select Name,Registration_id,Class,Father_name,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name from Online_Admission where Registration_id='" + ViewState["admissionno"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["Name"].ToString();
                lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["Registration_id"].ToString();
                lbl_class.Text = lbl_class1.Text = dt.Rows[0]["Class"].ToString();
                lbl_fathername.Text = lbl_fathername1.Text = dt.Rows[0]["Father_name"].ToString();
                lbl_session.Text = lbl_session1.Text = dt.Rows[0]["Session_name"].ToString();
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

                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
                Image3.ImageUrl = dt.Rows[0]["logo"].ToString();

                if (dt.Rows[0]["Is_mobile_no_show_in_bill"].ToString() == "True")
                {
                    contact_no.Visible = true;
                    contact_no1.Visible = true;
                }

                try
                {
                    Image2.Visible = true;
                    if (dt.Rows[0]["Is_certificate_watermark_hide"].ToString() == "True")
                    {
                        Image2.Visible = false;
                        Image3.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../online-registration.aspx", false);
        }

    }
}