using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Make_session_back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["token"] != null)
                {
                    ViewState["token"] = Request.QueryString["token"];
                    ViewState["userid"] = Request.QueryString["userid"];
                    ViewState["filecount"] = Request.QueryString["filecount"];
                    Session["Admindov"] = ViewState["userid"].ToString();

                    string url = "UpdateProgress.aspx?userid=" + ViewState["userid"].ToString() + "&token=" + ViewState["token"].ToString() + "&filecount=" + ViewState["filecount"].ToString();


                    Response.Redirect(url, false);
                    
                }
            }

        }
    }
}