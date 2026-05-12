using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class salary_dashboard : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    mycode.bind_all_ddl_with_id_All(ddl_dep, "select DISTINCT t2.name,t1.Department_id from PRL_Employee_Master t1 join PRL_Department_Master t2 on t1.Department_id=t2.department_id order by t2.name asc");
                    hd_dep_name.Value = "0";
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "DefaultPage");
                }
            }
        }

        protected void ddl_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            hd_dep_name.Value = ddl_dep.SelectedValue;
        }
    }
}