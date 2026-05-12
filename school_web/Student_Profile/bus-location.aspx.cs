using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class bus_location : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        get_student_info();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }  
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void get_student_info()
        {

            


            DataTable dt = mycode.FillData("select smt.Admission_no,tm.transport_name,tm.Bus_no,tm.Bus_driver_name,tm.Bus_driver_mobileno,tm.Warden_Name,tm.Warden_Mobile_No,tbp.Boarding_Point,tbp.KM,(select  top 1 Live_location_path from TransportationPath where TransportationPath_id = smt.TransportPath_id and Transportation_Id = smt.transport_id   ) as livelocation from Student_mapping_with_TransportPath smt join Transport_Master tm on smt.transport_id = tm.transport_id join Transportation_Boarding_Point tbp on smt.transport_id = tbp.Transportation_Id and smt.TransportPath_id = tbp.TransportationPath_id and smt.Boarding_Point_id = tbp.Boarding_Point_id where smt.Admission_no = '" + ViewState["regid"].ToString() + "' and smt.Session_id = '" + ViewState["sesssionid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                pnl_transportation_not_taken.Visible = true;
                pnl_location_view.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                pnl_location_view.Visible = true;
                pnl_transportation_not_taken.Visible = false;
                if(dt.Rows[0]["livelocation"].ToString()=="")
                {
                    iframES.Visible = false;
                }
                else
                {
                    iframES.Visible = true;
                    iframES.Src = dt.Rows[0]["livelocation"].ToString();

                }
               


            }
        }


        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
    }
}