using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Admin.slip
{
    public partial class Other_fees_slip : System.Web.UI.Page
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
                    if (Request.QueryString["receiptid"] != null)
                    {
                        ViewState["receiptid"] = Request.QueryString["receiptid"];

                        Bind_schoolinfo();

                        Bind_particular();

                    }
                }
            }
        }

        private void Bind_particular()
        {
            string quer = " select  *    from Other_Fee_Taken_For_Student  where    Slipid='" + ViewState["receiptid"].ToString() + "' ";
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
                student_details(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["Branch_id"].ToString(), dt.Rows[0]["Session_id"].ToString());





                lbl_slipno.Text = lbl_slipno1.Text = dt.Rows[0]["Slipid"].ToString();
                lbl_paymentdate.Text = lbl_paymentdate1.Text = dt.Rows[0]["Payment_date"].ToString();
                lbl_paid_amount.Text = lbl_paid_amount1.Text = dt.Rows[0]["Content_Fee"].ToString();

                int number = (int)Convert.ToDouble(dt.Rows[0]["Content_Fee"].ToString());
                string inword_number = mycode.NumberToWords(number);
                string inword = inword_number + " Only";
                lbl_amountinword.Text = lbl_amountinword1.Text = inword;
                lbl_paymentmode1.Text = lbl_paymentmode.Text = dt.Rows[0]["Payment_mode"].ToString();

                if (dt.Rows[0]["Remarks"].ToString() != "")
                {
                    lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remarks"].ToString();
                    remarksDv.Visible = true;
                    remarksDv1.Visible = true;
                }
                else
                {
                    lbl_remarks.Text = lbl_remarks1.Text = dt.Rows[0]["Remarks"].ToString();
                    remarksDv.Visible = false;
                    remarksDv1.Visible = false;
                }
            }
        }

        private void student_details(string Admission_no, string Branch_id, string Session_id)
        {
            DataTable dt = mycode.FillData("select studentname,admissionserialnumber,class,Section,fathername,session,rollnumber,Academic_Sem_or_Year from admission_registor where admissionserialnumber='" + Admission_no + "' and Session_Id='" + Session_id + "' and Branch_id=" + Branch_id + " ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_studentname.Text = lbl_studentname1.Text = dt.Rows[0]["studentname"].ToString();
                lbl_aadmissionno.Text = lbl_aadmissionno1.Text = dt.Rows[0]["admissionserialnumber"].ToString();

                lbl_class.Text = lbl_class1.Text = dt.Rows[0]["class"].ToString() + ",  " + "   Section-" + dt.Rows[0]["Section"].ToString();
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


                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
                Image3.ImageUrl = dt.Rows[0]["logo"].ToString();
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
            try
            {
                if (Session["reprint_otherfee"].ToString() == "2")
                {
                    Response.Redirect("../Taken_Other_Fee_From_Student.aspx", false);
                }
                else if (Session["reprint_otherfee"].ToString() == "4")
                {
                    Response.Redirect("../Delete_other_fee.aspx", false);
                }
                else
                {
                    Response.Redirect("../Other_Fee_Collection_Report.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../Other_Fee_Collection_Report.aspx", false);
            }

        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }

        double total_payable = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Content_Fee = (Label)e.Row.FindControl("lbl_Content_Fee");

                if (lbl_Content_Fee.Text != "")
                {
                    total_payable = total_payable + Convert.ToDouble(lbl_Content_Fee.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalpaybale = (Label)e.Row.FindControl("lbl_totalpaybale");



                lbl_totalpaybale.Text = total_payable.ToString("0.00");


            }

        }
        double total_payable1 = 0;
        protected void grd_fee1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Content_Fee = (Label)e.Row.FindControl("lbl_Content_Fee");

                if (lbl_Content_Fee.Text != "")
                {
                    total_payable1 = total_payable1 + Convert.ToDouble(lbl_Content_Fee.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totalpaybale = (Label)e.Row.FindControl("lbl_totalpaybale");



                lbl_totalpaybale.Text = total_payable1.ToString("0.00");


            }
        }
    }
}