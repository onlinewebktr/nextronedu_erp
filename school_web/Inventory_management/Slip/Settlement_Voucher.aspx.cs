using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;

namespace school_web.Inventory_management.Slip
{
    public partial class Settlement_Voucher : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string Payment_Vochar_id = Request.QueryString["Payment_Vochar_id"];
                string party_id = Request.QueryString["partyid"];
                ViewState["firm_id_N"] = My.get_firm_id();
                if (!string.IsNullOrEmpty(Payment_Vochar_id))
                {

                    ViewState["Payment_Vochar_id"] = Payment_Vochar_id;
                    ViewState["party_id"] = party_id;
                    bind_gridview();

                }

                Dictionary<string, object> dc1 = Sale_Purchase.Firm_details_sale_purchase();
                lbl_hospital_name.Text = (String)dc1["firm_name"];
                lbl_address1.Text = (String)dc1["address"];
                lbl_address2.Text = "";
                img_logo.ImageUrl = (String)dc1["logo"];

                lbl_email_mobile.Text = "Email:" + (String)dc1["email"].ToString() + ", Tel No.:" + (String)dc1["contact_no"].ToString();



            }
        }

        private void bind_gridview()
        {
            string query = "select *,(select top 1 mobile from party_details where party_id=t1.party_id ) as mobile,(select top 1 address from party_details where party_id=t1.party_id ) as address,(select top 1 party_name from party_details where party_id=t1.party_id ) as party_name,   format(t1.Date_time, 'dd/MM/yyyy') as Date_time1,(select top 1 session from HMS_Invetory_Sell_details_billwise where Bill_No=t1.Bill_No ) as session from HMS_Inventory_Bill_Payment_Tracking t1 where   Payment_Vochar_id='" + ViewState["Payment_Vochar_id"].ToString() + "' and party_id='" + ViewState["party_id"].ToString() + "'  ";
            DataTable dt = My.dataTable(query);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                GrdView_Generate_PO.DataSource = null;
                GrdView_Generate_PO.DataBind();
            }
            else
            {

                lbl_date.Text = "DATE:- " + dt.Rows[0]["Date_time1"].ToString();
                lbl_partyname.Text = "NAME :" + dt.Rows[0]["party_name"].ToString();
                lbl_mobile_no.Text = "MOBILE NO.:- " + dt.Rows[0]["mobile"].ToString();
                lbl_address.Text = "ADD.:- " + dt.Rows[0]["address"].ToString();
                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();
                lbl_payment_mode.Text = "P. Trn No. :-" + dt.Rows[0]["Payment_transaction"].ToString();
                lbl_user.Text = "BY.:-" + dt.Rows[0]["User_Id"].ToString();
                lbl_remarks.Text = dt.Rows[0]["Remarks"].ToString();
                Get_student_full_details(dt.Rows[0]["session"].ToString());
            }

            

               
                
             


        }

        

         

        private void Get_student_full_details(string session)
        {
            if (ViewState["firm_id_N"].ToString() == "NNI-01")
            {
                if (My.toint(mycode.idate()) >= 20260510)
                {
                    string query2 = "select  *,(SELECT TOP 1 Course_Name FROM Add_course_table WHERE course_id=party_details.class_name) AS CLASSNAME from  party_details where    party_id='" + ViewState["party_id"].ToString() + "' ";
                    DataTable dt1 = My.dataTable(query2);
                    int rowcount1 = dt1.Rows.Count;
                    if (rowcount1 == 0)
                    {

                    }
                    else
                    {

                        lbl_admission_no.Text = "CUSTOMER ID. :- " + ViewState["party_id"].ToString().ToUpper();
                        lbl_class_name.Text = dt1.Rows[0]["CLASSNAME"].ToString();
                        lbl_section.Text = "SECTION :- " + dt1.Rows[0]["Section"].ToString();
                       
                    }
                }
                else
                {
                    string query2 = "select top 1 * from admission_registor  where    admissionserialnumber='" + ViewState["party_id"].ToString() + "' and session='" + session + "'  order by id desc ";
                    DataTable dt1 = My.dataTable(query2);
                    int rowcount1 = dt1.Rows.Count;
                    if (rowcount1 == 0)
                    {

                    }
                    else
                    {
                        lbl_admission_no.Text = "ADMISSION NO. :- " + ViewState["party_id"].ToString().ToUpper();
                        lbl_class_name.Text = dt1.Rows[0]["class"].ToString();
                        lbl_section.Text = "SECTION :- " + dt1.Rows[0]["Section"].ToString();
                        lbl_roll_no.Text = "ROLL NO. :- " + dt1.Rows[0]["rollnumber"].ToString();
                    }
                }
            }
            else
            {
                string query2 = "select top 1 * from admission_registor  where    admissionserialnumber='" + ViewState["party_id"].ToString() + "' and session='" + session + "'  order by id desc ";
                DataTable dt1 = My.dataTable(query2);
                int rowcount1 = dt1.Rows.Count;
                if (rowcount1 == 0)
                {

                }
                else
                {
                    lbl_admission_no.Text = "ADMISSION NO. :- " + ViewState["party_id"].ToString().ToUpper();
                    lbl_class_name.Text = dt1.Rows[0]["class"].ToString();
                    lbl_section.Text = "SECTION :- " + dt1.Rows[0]["Section"].ToString();
                    lbl_roll_no.Text = "ROLL NO. :- " + dt1.Rows[0]["rollnumber"].ToString();
                }

            }
                
        }

        protected void btn_back_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Session["billfromsettelement"].ToString() == "1")
                {
                    Response.Redirect("../Dues_Settlement.aspx", false);

                }

                
                else
                {
                    Response.Redirect("../Dashboard.aspx", false);

                }
            }
            catch
            {

            }

        }
    }
}