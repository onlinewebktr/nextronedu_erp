using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class available_seat_of_vehicle : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();

                        string pagename_current = "available-seat-of-vehicle.aspx"; 
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                        if (ViewState["Is_Download"].ToString() == "1")
                        {
                            excel.Visible = true;
                        }
                        else
                        {
                            excel.Visible = false;
                        }


                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_cap_All(ddl_vehicle_name, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name asc");
                        ddl_route.Items.Insert(0, new ListItem("ALL", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Bed_Availability");
            }
        }
         
      

       

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hostel_bed_availability.aspx", false);
        }


        protected void ddl_vehicle_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_route, "select Rootname,TransportationPath_id from TransportationPath where Transportation_Id=" + ddl_vehicle_name.SelectedValue + " order by Rootname asc");
            }
            catch (Exception ex)
            {
            }
        }
    }
}