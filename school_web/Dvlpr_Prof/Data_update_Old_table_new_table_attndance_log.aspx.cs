using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Data_update_Old_table_new_table_attndance_log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        My mycode = new My();
        protected void btn_update_attendance_log_Click(object sender, EventArgs e)
        {
            bool abc = PayrollMy.insert_HR_Attendance_log();
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            bool abc = PayrollMy.update_HR_Daily_Attendance_Record("Manual Attendance");
        }

        

        protected void btn_insert_one_row_Click(object sender, EventArgs e)
        {
            bool abc = PayrollMy.update_one_Row_data_Attendance_Record();
        }
    }
}
