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
    public partial class update__adm_no_in_exam_marks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_update_adm_no_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = My.dataTable("select * from admission_registor_Change_admission_no_history where Change_type='Admission No. Change' and Format(convert(DateTime,Date_time,103), 'yyyyMMdd')>='20250125'");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        My.exeSql("update Exam_marks set Admission_no='" + dr["Current_admission_no"].ToString() + "' where Admission_no='" + dr["Old_admission_no"].ToString() + "';update Exam_rank_master set Admission_no='" + dr["Current_admission_no"].ToString() + "'  where Admission_no='" + dr["Old_admission_no"].ToString() + "';update Exam_rank_master_final_year set Admission_no='" + dr["Current_admission_no"].ToString() + "' where Admission_no='" + dr["Old_admission_no"].ToString() + "'");
                    }
                    Label1.Text = "Success";
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}