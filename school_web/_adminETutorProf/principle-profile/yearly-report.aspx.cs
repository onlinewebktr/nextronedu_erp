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
    public partial class yearly_report : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["session_id"] = My.get_session_id();
                    hd_session_id.Value = ViewState["session_id"].ToString();
                    hd_session_name.Value = My.get_session();
                    bind_strength(ViewState["session_id"].ToString(), hd_session_name.Value);
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }


        private void bind_strength(string session_id, string session_name)
        {
            string idate = mycode.idate();
            DataTable dt = My.dataTable("select 'Enquiry' as Name,isnull(count(Id),0) as Number from Enquiry_Details where Format(convert(DateTime,Created_date,103), 'yyyyMMdd')>'20250101' union all select 'Prospectus Sale' as Name,isnull(count(Id),0) as Number from Form_sale_details where Session_id='" + session_id + "' union all select 'Admission' as Name,isnull(count(Id),0) as Number from admission_registor where Session_id='" + session_id + "' and Transfer_Status='New' and Status=1 union all select 'TC' as Name,isnull(count(Id),0) as Number from Transfer_certificate where Session_id='" + session_id + "' union all select 'Inactive' as Name,isnull(count(Id),0) as Number from admission_registor where Session_id='" + session_id + "' and Status=0 and Is_TC_Taken=0 union all select 'Receipt' as Name,isnull(count(Id),0) as Number from Student_Payment_History where Session='" + session_name + "' union all select 'Prospectus Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Form_sale_details where Session_id='" + session_id + "' union all  select 'School Fee' as Name,isnull(sum(cast(Amount as float) ),0) as Number from Student_Payment_History where Session='" + session_name + "'");
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