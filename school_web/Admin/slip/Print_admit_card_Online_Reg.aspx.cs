using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin.slip
{
    public partial class Print_admit_card_Online_Reg : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["session_Id"] != null && Request.QueryString["classid"] != null && Request.QueryString["admin"] != null)
                {
                    ViewState["branch_id"] = "1";
                    hd_session_id.Value = Request.QueryString["session_Id"].ToString();
                    hd_class_id.Value = Request.QueryString["classid"].ToString();
                    hd_admission_no.Value = Request.QueryString["admin"].ToString();


                    try
                    {
                        A1.HRef = "../../Download_Admit_Card.aspx";
                        hd_from.Value = Request.QueryString["type"].ToString();
                    }
                    catch
                    {
                        hd_from.Value = "In";
                        A1.HRef = "../Print_Online_Reg_Admit_Card.aspx";
                    } 
                }
                else
                { 
                } 
            } 
        } 
    }
}