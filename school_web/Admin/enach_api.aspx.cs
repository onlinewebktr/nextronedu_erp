using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class enach_api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["txnId"] != null )
                {
                    string txnId = Request.QueryString["txnId"].ToString();
                    Bind_data(txnId);
                }
            }
        }
        My mycode = new My();

        private void Bind_data(string txnId)
        {
            Dictionary<string, object> dc2 = mycode.Firm_details();

            ViewState["logo"] = (String)dc2["logo"];
           
            SqlCommand cmd2;
            cmd2 = new SqlCommand("select   * from enach_registration where txnId='" + txnId + "'");
            DataTable dt = mycode.GetData(cmd2);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                hd_token.Value =dt.Rows[0]["hash_token"].ToString();// "f51e2d3c4c1c6c4fd1c19d7eaceea43ccdd661b3ceaf682f91f8d542909b2cb4313366f02c0b63b2402dca8174a423888fca91ddb89061d2bb598de12e2d95c8";
                hd_student_admission_no.Value = dt.Rows[0]["admissionserialnumber"].ToString();
               
                hd_order_id.Value = dt.Rows[0]["txnId"].ToString();
                hd_startdate.Value = dt.Rows[0]["debitStartDate"].ToString();
                hd_enddate.Value = dt.Rows[0]["debitEndDate"].ToString();

                hd_camount.Value = dt.Rows[0]["totalamount"].ToString();

                hd_logo.Value = ViewState["logo"].ToString();
                hd_merchantId.Value = ConfigurationManager.AppSettings["merchantId"].ToString();


                string query = "select top 1 * from admission_registor where admissionserialnumber='" + hd_student_admission_no.Value + "'  order by id desc  ";
                SqlCommand cmd3 = new SqlCommand(query);
                DataTable dt3 = mycode.GetData(cmd3);
                if (dt3.Rows.Count == 0)
                {
                    hd_email.Value = "0";
                    hd_mobile.Value = "na@gmail.com";
                }
                else
                {

                    hd_email.Value = dt3.Rows[0]["email_id"].ToString();
                    hd_mobile.Value = dt3.Rows[0]["father_mob"].ToString();

                }


            }

        }
    }
}