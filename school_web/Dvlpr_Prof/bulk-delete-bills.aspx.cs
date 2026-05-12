using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class bulk_delete_bills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btn_delete_bill_Click(object sender, EventArgs e)
        {
            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();
                DataTable dt = payments.dataTable("select top 20 * from AAAA_student_temp11 where Status='Pending'", con);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        payments.exeSql("delete from Student_Payment_History where Addmission_no='" + dr["Admission_no"].ToString() + "'; delete from Monthly_Fee_Collection_Slip where adno= '" + dr["Admission_no"].ToString() + "'; delete from Typewise_fee_collection where admission_no= '" + dr["Admission_no"].ToString() + "'; update admission_registor set payment_status='Unpaid' where admissionserialnumber = '" + dr["Admission_no"].ToString() + "'; delete from Admission_fee_collection where Admission_no='" + dr["Admission_no"].ToString() + "'; delete from Annual_fee_collection where Admission_no='" + dr["Admission_no"].ToString() + "';delete from Discount_Master where admission_no='" + dr["Admission_no"].ToString() + "'; delete from Discount_Master_for_bus where admission_no = '" + dr["Admission_no"].ToString() + "'", con);

                        DataTable dtS = payments.dataTable("select * from admission_registor where admissionserialnumber='" + dr["Admission_no"].ToString() + "'", con);
                        if (dtS.Rows.Count > 0)
                        {
                            payments.exeSql("update AAAA_student_temp11 set Status='Success' where Id='" + dr["Id"].ToString() + "'", con);
                            dues_update_headwise_transaction.update_student_dues(dtS.Rows[0]["Session_id"].ToString(), dtS.Rows[0]["Class_id"].ToString(), dr["Admission_no"].ToString(), "0", "0", con);
                        }
                    }
                }

                flag = true;
                con.Close();
                scope.Complete();
            }

            if (flag == true)
            {
                lbl_msg.Text = "Success.";
            }
        }
    }
}