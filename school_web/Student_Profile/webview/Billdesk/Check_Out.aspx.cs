using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace school_web.Student_Profile.webview.Billdesk
{
    public partial class Check_Out : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["sytemid"] == null)
                {

                }
                else
                {

                    Bind_data();
                }
            }
        }

        private void Bind_data()
        {
            string query = "Select * from Payment_transaction_process where ordertrackingid='" + Session["sytemid"] + "'  ";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Session["regid"] = dr["Admission_no"].ToString();
                    double amtForRazorrounD = Math.Round(Convert.ToDouble(dr["Total_pay"].ToString()));
                    // int aftrrounD = (Convert.ToInt32(amtForRazorrounD) * 100);
                    txtTxnAmount.Value = amtForRazorrounD.ToString();
                    txtAdditionalInfo1.Value = dr["Admission_no"].ToString();
                    txtAdditionalInfo3.Value = dr["Name"].ToString();
                    txtAdditionalInfo4.Value = dr["Session"].ToString();
                    txtAdditionalInfo5.Value = dr["Class_name"].ToString();
                    txtAdditionalInfo6.Value = dr["month"].ToString();
                    txtAdditionalInfo7.Value = dr["parameter"].ToString();
                    txtCustomerID.Value = dr["ordertrackingid"].ToString();



                    ViewState["payFrom"] = dr["Pay_from"].ToString();
                    Session["payFrom"] = dr["Pay_from"].ToString();
                    txt_RU.Value = My.url() + "Student_Profile/webview/Billdesk/Billdesk_Response.aspx";
                }
            }
        }
    }
}