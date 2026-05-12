using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class update_bill_date_in_monthly_fee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = My.dataTable("select * from Student_Payment_History where Session='2024-2025'");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string qry = "update Monthly_Fee_Collection_Slip set Date='" + dr["Date"].ToString() + "',Idate='" + dr["Idate"].ToString() + "' where session='2024-2025' and slipno='" + dr["Slip_no"].ToString() + "'";
                        My.exeSql(qry);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}