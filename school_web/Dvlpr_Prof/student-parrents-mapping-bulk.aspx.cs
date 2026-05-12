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
    public partial class student_parrents_mapping_bulk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
        }

        protected void btn_parents_mapping_with_student_Click(object sender, EventArgs e)
        {
            string query = "select  admissionserialnumber,fathername,father_mob from admission_registor where session='"+My.get_session()+"'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {  }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    string mobilenumber = dt.Rows[i]["father_mob"].ToString();
                    string fathername = dt.Rows[i]["fathername"].ToString();
                    insert_data(admissionserialnumber, mobilenumber, fathername);
                }
            }
        }


        My mycode = new My();
        private void insert_data(string admissionserialnumber, string mobilenumber, string fathername)
        {
            mycode.addparentslogin(admissionserialnumber, mobilenumber, fathername);
        }
    }
}