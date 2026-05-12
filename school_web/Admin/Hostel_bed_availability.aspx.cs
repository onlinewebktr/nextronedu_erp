using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Hostel_bed_availability : System.Web.UI.Page
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
                       

                        mycode.bind_all_ddl_with_id(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        ddl_hostel.SelectedValue = My.get_top_one_hostel_id();
                        bind_rooms();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Bed_Availability");
            }
        }

        protected void ddl_hostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_rooms();
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Bed_Availability");
            }
        }
        private void bind_rooms()
        {
            mycode.bind_all_ddl_with_id_All(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' order by Room_name asc");
        }

        protected void ddl_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_beds();
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Bed_Availability");
            }
        }

        private void bind_beds()
        {
            mycode.bind_all_ddl_with_id_All(ddl_beds, "select Bed_name,Bed_id from Hostel_room_bed_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Room_id='" + ddl_room.SelectedValue + "' order by Bed_name asc");
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hostel_bed_availability.aspx", false);
        }

    }
}