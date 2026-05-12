using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.principle_profile
{
    public partial class student_strength : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["session_id"] = My.get_session_id(); 
                    bind_strength();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_strength()
        {
            string sql = @"select isnull(count(Id),0) as TotalActiveStudent from admission_registor where Session_id='" + ViewState["session_id"].ToString() + @"' and Status=1;
                           select isnull(count(Id),0) as NewStudent from admission_registor where Session_id='" + ViewState["session_id"].ToString() + @"' and Status=1 and Transfer_Status='New'; 
                           select isnull(count(Id),0) as OldStudent from admission_registor where Session_id='" + ViewState["session_id"].ToString() + @"' and Status=1 and Transfer_Status='NT';
                           select isnull(count(Id),0) as TCStudent from admission_registor where Session_id='" + ViewState["session_id"].ToString() + @"' and Is_TC_Taken=1 and Status=0;
                           select isnull(count(Id),0) as InactiveStudent from admission_registor where Session_id='" + ViewState["session_id"].ToString() + @"' and Status=0 and Is_TC_Taken=0;";
            DataSet ds = mycode.Fill_Data_set(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        lbl_current_std.Text = dtTemp.Rows[0]["TotalActiveStudent"].ToString();
                    }
                    else
                    {
                        lbl_current_std.Text = "0";
                    }
                }
                else
                {
                    lbl_current_std.Text = "0";
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[1];
                    if (dtTemp.Rows.Count > 0)
                    {
                        lbl_new_student.Text = dtTemp.Rows[0]["NewStudent"].ToString();
                    }
                    else
                    {
                        lbl_new_student.Text = "0";
                    }
                }
                else
                {
                    lbl_new_student.Text = "0";
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[2];
                    if (dtTemp.Rows.Count > 0)
                    {
                        lbl_old_std.Text = dtTemp.Rows[0]["OldStudent"].ToString();
                    }
                    else
                    {
                        lbl_old_std.Text = "0";
                    }
                }
                else
                {
                    lbl_old_std.Text = "0";
                }

                //====================
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[3];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_tc_std.Text = dtTemp.Rows[0]["TCStudent"].ToString();
                    }
                    else
                    {
                        lbl_tc_std.Text = "0";
                    }
                }
                else
                {
                    lbl_tc_std.Text = "0";
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    DataTable dtTemp = ds.Tables[4];
                    if (dtTemp.Rows.Count != 0)
                    {
                        lbl_inactive_std.Text = dtTemp.Rows[0]["InactiveStudent"].ToString();
                    }
                    else
                    {
                        lbl_inactive_std.Text = "0";
                    }
                }
                else
                {
                    lbl_inactive_std.Text = "0";
                }
            }
        }
    }
}