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
    public partial class update_dues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string session_id = My.get_session_id();
                //bool flag = false;
                //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                //{
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    DataTable dt = payments.dataTable("select Session_id,Class_id,admissionserialnumber from admission_registor where Session_id='" + session_id + "' and Status='1' order by id desc", con);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            try
                            {
                                dues_update_headwise_transaction.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), "0", "0", con);
                            }
                            catch (Exception ex)
                            {
                                Label1.Text = dr["admissionserialnumber"].ToString() +"-----"+ ex.ToString();
                            }
                        }
                    }
                con.Close();
                //flag = true;
                //con.Close();
                //scope.Complete();
                //}
                //if (flag == true)
                //{
                //    Label1.Text = "Success";
                //}
            }
            catch (Exception ex)
            {

            }
        }
    }
}