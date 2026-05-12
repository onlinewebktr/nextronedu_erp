using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.HtmlControls;

namespace school_web.Admin
{
    public partial class Vehicle_Details : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["transportid"] != null)
                    {
                        string transportid = Request.QueryString["transportid"];
                        ViewState["transportid"] = transportid;
                        fetch_data(transportid);
                    }
                    else
                    {
                        Response.Redirect("Vehicle_Details.aspx");
                    }
                }
            }
            catch (Exception exe)
            {
            }
        }

        private void fetch_data(string transportid)
        {
            DataTable dt1 = mycode.FillData("select * from Transport_Master where transport_id='"+ transportid + "'");
            if (dt1.Rows.Count == 0)
            {

            }
            else
            {
                lbl_vehiclename.Text = dt1.Rows[0]["transport_name"].ToString();
                lbl_Vehicle_no.Text = dt1.Rows[0]["Bus_no"].ToString();
                lbl_Vehicle_Owner_Name.Text = dt1.Rows[0]["Bus_owner_name"].ToString();
                lbl_Vehicle_Owner_mobile_no.Text = dt1.Rows[0]["Bus_owner_mobile_no"].ToString();
                lbl_Vehicle_drivername.Text = dt1.Rows[0]["transport_name"].ToString();
                lbl_driver_mobile_no.Text = dt1.Rows[0]["Bus_driver_mobileno"].ToString();
                lbl_no_seat.Text = dt1.Rows[0]["Bus_no_sheet"].ToString();
                lbl_Vehicle_type.Text = dt1.Rows[0]["Bus_type"].ToString();
                //lbl_vehicle_rute.Text = dt1.Rows[0]["transport_name"].ToString();
                lbl_Transport_own.Text = dt1.Rows[0]["Own_Transport"].ToString();
               // lbl_Vehicle_Registration.Text = dt1.Rows[0]["Vehicle_Registration_No"].ToString();
                lbl_Vehicle_Registration_date.Text = dt1.Rows[0]["Vehicle_Registration_No_Expiry_Date"].ToString();
                lbl_vechile_insurance_expirydate.Text = dt1.Rows[0]["Vehicle_Insurance_Expiry_Date"].ToString();
                lbl_Pollutionexpirydate.Text = dt1.Rows[0]["Pollution_Expiry_Date"].ToString();
                lbl_body_Fitness_Expiry.Text = dt1.Rows[0]["Body_Fitness_Expiry_Date"].ToString();
                lbl_driver_licence.Text = dt1.Rows[0]["Driver_licence_no"].ToString();
                lbl_driver_licence_expiry.Text = dt1.Rows[0]["Driver_licence_expiry_Date"].ToString();
                lbl_Vehicle_Warden.Text = dt1.Rows[0]["Vehicle_Warden"].ToString();
                lbl_Vehicle_Warden_name.Text = dt1.Rows[0]["Warden_Name"].ToString();
                lbl_Warden_mobile_no.Text = dt1.Rows[0]["Warden_Mobile_No"].ToString();
                lbl_Warden_aadhar_no.Text = dt1.Rows[0]["Warden_Addhar_No"].ToString();
                lbl_Warden_address.Text = dt1.Rows[0]["Warden_Address"].ToString();
                lbl_Vehicle_Registration_expiry.Text= dt1.Rows[0]["Registration_Expiry_date"].ToString();
                Bind_doscument();
            }
        }

        private void Bind_doscument()
        {
            DataTable dt = mycode.FillData("select * from Transport_Vehicle_Document_Master order by Doc_id asc");
            if (dt.Rows.Count == 0)
            {
                
                grd_doc.DataSource = null;
                grd_doc.DataBind();

            }
            else
            {

                grd_doc.DataSource = dt;
                grd_doc.DataBind();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Transport_List.aspx", false);
        }

        protected void grd_doc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlAnchor lnkVoucher = e.Row.FindControl("a1") as HtmlAnchor;
                Label lbl_Description_ID_No = (Label)e.Row.FindControl("lbl_Description_ID_No");
               
                string file_avl = find_file_Available_data(lbl_Description_ID_No.Text);
                if (file_avl != "")
                {
                    lnkVoucher.Visible = true;
                    lnkVoucher.HRef = file_avl;
                     
                }
                else
                {
                    lnkVoucher.Visible = false;
                    
                }


            }
        }

        private string find_file_Available_data(string Description_ID)
        {
            SqlCommand cmd;
            string strQuery = "Select  * from Transport_Vehicle_Document where transport_id=@transport_id   and Document_id=@Document_id";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@transport_id", ViewState["transportid"].ToString());
            cmd.Parameters.AddWithValue("@Document_id", Description_ID);
            DataTable dt = mycode.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Doument_path"].ToString();
            }
        }
    }
}