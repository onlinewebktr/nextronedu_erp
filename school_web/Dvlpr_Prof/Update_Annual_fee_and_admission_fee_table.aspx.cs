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
    public partial class Update_Annl_fee_and_admission_fee_table : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                My.update_annual_fee_amission_fee("2023-2024");

                DataTable dt = My.dataTable("select * from Student_Payment_History  where Session='2023-2024' and Type in('Admission','Annual')");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Type"].ToString() == "Admission")
                    {
                        DataTable dtadm = My.dataTable("select * from Admission_fee_collection  where Admission_no='" + dr["Addmission_no"].ToString() + "'  and  session='" + dr["Session"].ToString() + "'");
                        {
                            if (dtadm.Rows.Count > 0)
                            {
                                if (My.toDouble(dtadm.Rows[0]["Dues_amount"].ToString()) == 0)
                                {
                                    My.exeSql("update admission_registor set payment_status= 'Paid' where session='" + dr["Session"].ToString() + "' and admissionserialnumber='" + dr["Addmission_no"].ToString() + "'");
                                }
                            }
                        }
                    }
                    else
                    {
                        DataTable dtadm = My.dataTable("select * from Annual_fee_collection  where Admission_no='" + dr["Addmission_no"].ToString() + "' and  session='" + dr["Session"].ToString() + "'");
                        {
                            if (dtadm.Rows.Count > 0)
                            {
                                if (My.toDouble(dtadm.Rows[0]["Dues_amount"].ToString()) == 0)
                                {
                                    My.exeSql("update admission_registor set payment_status= 'Paid' where session='" + dr["Session"].ToString() + "' and admissionserialnumber='" + dr["Addmission_no"].ToString() + "'");
                                }
                            }
                        }
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Update sucessfully done')", true);
            }
            catch (Exception ex)
            {

            }


        }
    }
}