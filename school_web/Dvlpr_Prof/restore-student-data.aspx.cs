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
    public partial class restore_student_data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = My.dataTable("select count(Id) as Count,admissionserialnumber from admission_registor where session='2023-2024' and Transfer_Status='Transferred' GROUP BY admissionserialnumber");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataTable dtPH = My.dataTable("select distinct Type from Student_Payment_History where Addmission_no='" + dr["admissionserialnumber"].ToString() + "' and Session='2023-2024' and (Type='Admission' or Type='Annual')");
                        if (dtPH.Rows.Count > 0)
                        {
                            string status = "NT";
                            if (dtPH.Rows[0]["Type"].ToString() == "Admission")
                            {
                                status = "New";
                            }
                            My.exeSql("update admission_registor set Transfer_Status='" + status + "',Temp_Is_updated='1' where session='2023-2024' and admissionserialnumber='" + dr["admissionserialnumber"].ToString() + "'");
                        }
                        else
                        {
                            My.exeSql("insert into TempAdmNo(Admission_no) values ('" + dr["admissionserialnumber"].ToString() + "');");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}