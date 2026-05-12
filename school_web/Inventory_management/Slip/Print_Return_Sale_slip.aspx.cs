using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Inventory_management.Slip
{
    public partial class Print_Return_Sale_slip : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string unique_entry_id = Request.QueryString["unique_entry_id"];
                string party_id = Request.QueryString["partyid"];
                ViewState["firm_id_N"] = My.get_firm_id();
                if (!string.IsNullOrEmpty(unique_entry_id))
                {

                    ViewState["unique_entry_id"] = unique_entry_id;
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
            string query = "select *,(select top 1 Item_Name from HMS_Invetory_item_Master where Item_id=t1.Item_Code) as Item_name,(select top 1 Brand_name from HMS_Invetory_Brand_Master where Brand_id=t1.Brand_Id) as Brand_name,(select top 1 Unit from unit_master where unit_id=t1.unit_id ) as Unit_name   from HMS_Invetory_Sale_Returen_Item_wise t1 where   unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' and Party_id='" + ViewState["party_id"].ToString() + "'";
            DataTable dt = My.dataTable(query);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                GrdView_Generate_PO.DataSource = null;
                GrdView_Generate_PO.DataBind();
            }
            else
            {

                GrdView_Generate_PO.DataSource = dt;
                GrdView_Generate_PO.DataBind();

            }
            Bind_bill_wise();





        }

        private void Bind_bill_wise()
        {
            string query2 = "select *,format(Returen_date_time, 'dd/MM/yyyy') date1 from HMS_Invetory_Sale_Returen_Bill_wise  where  unique_entry_id='" + ViewState["unique_entry_id"].ToString() + "' and Party_id='" + ViewState["party_id"].ToString() + "'";
            DataTable dt1 = My.dataTable(query2);
            int rowcount1 = dt1.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                lbl_date.Text = "DATE:- " + dt1.Rows[0]["date1"].ToString();
                lbl_Total_rate.Text = dt1.Rows[0]["Grand_total"].ToString();
                lbl_po_no.Text = "INVOICE NO.:-" + dt1.Rows[0]["New_Bill"].ToString();
                lbl_user.Text = "BY.:-" + dt1.Rows[0]["User_id"].ToString();
                lbl_round_of.Text = dt1.Rows[0]["Round_off"].ToString();
                lbl_remarks.Text = dt1.Rows[0]["Remarks_Returen"].ToString();
                lbl_net_total.Text = dt1.Rows[0]["Grand_total"].ToString();
                lbl_payment_mode.Text = "RETURN MODE :-" + dt1.Rows[0]["Pay_Mode"].ToString();

                if(dt1.Rows[0]["Pay_Tran_id"].ToString()=="")
                {
                    lbl_payment_tra.Text = dt1.Rows[0]["Pay_Mode"].ToString() + "/ N/A"  ;
                }
                else
                {
                    lbl_payment_tra.Text = dt1.Rows[0]["Pay_Mode"].ToString() + "/" + dt1.Rows[0]["Pay_Tran_id"].ToString();
                }
                

                ViewState["Session_id"]  = dt1.Rows[0]["Session_id"].ToString();

               
              
            }
            Get_student_full_details();
        }

        private void Get_student_full_details()
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
                        lbl_class_name.Text = "CLASS :- " + dt1.Rows[0]["CLASSNAME"].ToString();
                        lbl_section.Text = "SECTION :- " + dt1.Rows[0]["Section"].ToString();
                       ;
                        lbl_partyname.Text = "NAME :- " + dt1.Rows[0]["party_name"].ToString();
                        lbl_mobile_no.Text = "MOBILE NO.:- " + dt1.Rows[0]["mobile"].ToString();
                        lbl_address.Text = "ADD.:- " + dt1.Rows[0]["address"].ToString();

                    }
                }
                else
                {
                    string session = My.get_session_static(ViewState["Session_id"].ToString());
                    Dictionary<string, object> dc2 = My.get_student_info(ViewState["party_id"].ToString(), session);
                    string studentname = (String)dc2["studentname"];
                    string Admission_no = (String)dc2["Admission_no"];
                    ViewState["Session_id"] = (String)dc2["Session_id"];
                    lbl_session.Text = (String)dc2["Session"];
                    string classname = (String)dc2["classname"];
                    lbl_admission_no.Text = "ADMISSION NO. :- " + ViewState["party_id"].ToString().ToUpper();
                    lbl_class_name.Text = "CLASS :- " + classname;
                    lbl_section.Text = "SECTION :- " + (String)dc2["Section"];
                    lbl_roll_no.Text = "ROLL NO. :- " + (String)dc2["rollnumber"];
                    lbl_partyname.Text = "NAME :-" + studentname;
                    lbl_mobile_no.Text = "MOBILE NO.:- " + (String)dc2["Mobileno"];
                    lbl_address.Text = "ADD.:- " + (String)dc2["careof"];
                }
            }
            else
            {
                string session = My.get_session_static(ViewState["Session_id"].ToString());
                Dictionary<string, object> dc2 = My.get_student_info(ViewState["party_id"].ToString(), session);
                string studentname = (String)dc2["studentname"];
                string Admission_no = (String)dc2["Admission_no"];
                ViewState["Session_id"] = (String)dc2["Session_id"];
                lbl_session.Text = (String)dc2["Session"];
                string classname = (String)dc2["classname"];
                lbl_admission_no.Text = "ADMISSION NO. :- " + ViewState["party_id"].ToString().ToUpper();
                lbl_class_name.Text = "CLASS :- " + classname;
                lbl_section.Text = "SECTION :- " + (String)dc2["Section"];
                lbl_roll_no.Text = "ROLL NO. :- " + (String)dc2["rollnumber"];
                lbl_partyname.Text = "NAME :-" + studentname;
                lbl_mobile_no.Text = "MOBILE NO.:- " + (String)dc2["Mobileno"];
                lbl_address.Text = "ADD.:- " + (String)dc2["careof"];
            }

        }

        protected void btn_back_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (Session["billfromre"].ToString() == "1")
                {
                    Response.Redirect("../Sales_Return.aspx", false);

                }

                else if (Session["billfromre"].ToString() == "2")
                {
                    Response.Redirect("../Student_Wise_Sale_Return_Report.aspx", false);

                }
                else if (Session["billfromre"].ToString() == "3")
                {
                    Response.Redirect("../Student_Wise_Sale_Return_Report.aspx", false);

                }
                else if (Session["billfromre"].ToString() == "4")
                {
                    Response.Redirect("../Student_Wise_Sale_Return_Report.aspx", false);

                }
                else
                {
                    Response.Redirect("../Student_Wise_Sale_Return_Report.aspx", false);

                }
            }
            catch
            {

            }

        }


    }
}