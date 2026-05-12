using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Scholarship
{
    public partial class Scholarship_guidelines : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                if (Request.QueryString["sdjhfdsgfhjasbdagdagdshdghsgdsgdg"] != null)
                {
                    ViewState["classid"] = Request.QueryString["sdjhfdsgfhjasbdagdagdshdghsgdsgdg"];
                    ViewState["session_id"] = Request.QueryString["sfhsdfghjdncjszhfyshfcjzshdyusahds"];

                    ViewState["testid"] = Request.QueryString["testid"];
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
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                Session["SchoolBranch"] = dt.Rows[0]["address1"].ToString();
                pnl_branch.Visible = false;
                lbl_address_2.Visible = false;
                ViewState["IsMoreThanOneBranch"] = "0";
                try
                {
                    if (dt.Rows[0]["Is_2nd_address"].ToString() == "True")
                    {
                        lbl_address_2.Visible = true;
                        pnl_branch.Visible = true;
                        lbl_address_2.Text = dt.Rows[0]["address2"].ToString();

                        lbl_branch_1.Text = dt.Rows[0]["address1"].ToString();
                        lbl_branch_2.Text = dt.Rows[0]["address2"].ToString();
                        ViewState["IsMoreThanOneBranch"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }

                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }


        My mycode = new My();
        private void fetch_data()
        {

            DataTable dt = mycode.FillData("select * from dbo.[Scholarship_Parameter_fees] where Session_id='" + ViewState["session_id"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "' and Test_id='" + ViewState["testid"].ToString() + "' ");
            if (dt.Rows.Count != 0)
            {
                lbl_data.Text = dt.Rows[0]["Scholorship_Guidelines"].ToString();
            }
            else
            {


            }
        }
        string scrpt;
        protected void btn_terms_Click(object sender, EventArgs e)
        {
            if (ViewState["IsMoreThanOneBranch"].ToString() == "1")
            {
                if (rd_sch_branch_1.Checked == false && rd_sch_branch_2.Checked == false)
                {
                    lblmessage.Text = "Please choose school branch.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    return;
                }
            }
            if (squaredTwo.Checked == false)
            {
                squaredTwo.Focus();
                lblmessage.Text = "Please read and accept the admission procedure.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            { 
                string a = "";
                Session["terms"] = "terms";
                if (ViewState["IsMoreThanOneBranch"].ToString() == "1")
                {
                    if (rd_sch_branch_1.Checked == true)
                    {
                        Session["SchoolBranch"] = lbl_branch_1.Text;
                    }
                    if (rd_sch_branch_2.Checked == true)
                    {
                        Session["SchoolBranch"] = lbl_branch_2.Text;
                    }
                }
                Response.Redirect("Scholarship-registration.aspx?classid=" + ViewState["classid"].ToString() + "&regiDs=0&testid=" + ViewState["testid"].ToString(), false);
            }
        }
    }
}