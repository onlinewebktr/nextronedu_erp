using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class update_version : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        Global gb = new Global();

        protected void btn_update_Click(object sender, EventArgs e)
        {
            bool tureturn = gb.update_database_and_version();
        }
    }
}