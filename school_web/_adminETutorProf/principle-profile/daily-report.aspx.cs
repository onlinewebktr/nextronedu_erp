using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.principle_profile
{
    public partial class daily_report : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["session_id"] = My.get_session_id();
                    bind_strength(); fetch_formSale_Dt(ViewState["session_id"].ToString()); fetch_admission_Dt(ViewState["session_id"].ToString());
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void fetch_formSale_Dt(string session_id)
        {
            noSale.Visible = true;
            formSaleList.Visible = false;
            DataTable dt = My.dataTable("select * from Form_sale_details where idate='" + mycode.idate() + "'");
            if (dt.Rows.Count > 0)
            {
                noSale.Visible = false;
                formSaleList.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        private void fetch_admission_Dt(string session_id)
        {
            noAdm.Visible = true;
            newAdmissionList.Visible = false;
            DataTable dt = My.dataTable("select * from admission_registor where admission_idate='" + mycode.idate() + "' and Transfer_Status='New' and Status=1");
            if (dt.Rows.Count > 0)
            {
                noAdm.Visible = false;
                newAdmissionList.Visible = true;
                rpAdmssion.DataSource = dt;
                rpAdmssion.DataBind();
            }
        }

        private void bind_strength()
        {
            string idate = mycode.idate();
            DataTable dt = My.dataTable("select 'Enquiry' as Name,isnull(count(Id),0) as Number from Enquiry_Details where Format(convert(DateTime,Created_date,103), 'yyyyMMdd')='" + idate + "' union all select 'Prospectus Sale' as Name,isnull(count(Id),0) as Number from Form_sale_details where idate='" + idate + "' union all select 'Admission' as Name,isnull(count(Id),0) as Number from admission_registor where admission_idate='" + idate + "' and Transfer_Status='New' and Status=1 union all select 'TC' as Name,isnull(count(Id),0) as Number from Transfer_certificate where Create_idate='" + idate + "' union all select 'Inactive' as Name,isnull(count(Id),0) as Number from admission_registor where Inactive_idate='" + idate + "' and Is_TC_Taken=0 and Status=0 union all select 'Receipt' as Name,isnull(count(Id),0) as Number from Student_Payment_History where Idate='" + idate + "'  union all select 'Prospectus Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Form_sale_details where idate='" + idate + "' union all  select 'School Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Student_Payment_History where Idate='" + idate + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Name"].ToString() == "Enquiry")
                    {
                        lbl_enquiry.Text = dr["Number"].ToString();
                    }
                    if (dr["Name"].ToString() == "Prospectus Sale")
                    {
                        lbl_prospectus.Text = dr["Number"].ToString();
                    }
                    if (dr["Name"].ToString() == "Admission")
                    {
                        lbl_admission.Text = dr["Number"].ToString();
                    }
                    if (dr["Name"].ToString() == "TC")
                    {
                        lbl_tc.Text = dr["Number"].ToString();
                    }
                    if (dr["Name"].ToString() == "Inactive")
                    {
                        lbl_inactive.Text = dr["Number"].ToString();
                    }
                    if (dr["Name"].ToString() == "Receipt")
                    {
                        lbl_receipt.Text = dr["Number"].ToString();
                    }

                    ////=================================
                    if (dr["Name"].ToString() == "Prospectus Fee")
                    {
                        lbl_prospectus_fee.Text = dr["Number"].ToString();
                    }
                    if (dr["Name"].ToString() == "School Fee")
                    {
                        lbl_school_fee.Text = dr["Number"].ToString();
                    }
                }
            }
        }
    }
}