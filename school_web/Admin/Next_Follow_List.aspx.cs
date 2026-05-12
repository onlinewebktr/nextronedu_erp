using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Next_Flowup_List : System.Web.UI.Page
    {
        My mycode = new My();
        string Purpose = "Select top 1 Enquiry_Type from Enquiry_Type where Enquiry_Type_Id=ed.Enquiry_Type_Id";
        string Source = "Select top 1 Enquiry_Type from Enquiry_Type where Enquiry_Type_Id=ed.Source_id";
        string Reference = "Select top 1 Enquiry_Type from Enquiry_Type where Enquiry_Type_Id=ed.Reference";

        string coursename = "Select top 1 Course_Name from Add_course_table where course_id=ed.Class_id";

        string Follow_Up_Datelast = "Select top 1 format(Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by id desc";

        string Next_Follow_Up_Date = "Select top 1 format(Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by id desc";

        string lastrowid = "Select top 1  id from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by id desc";
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
                        ViewState["Applicant_Image"] = "";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Next_Follow_List.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        txt_startdate.Text = mycode.date();
                        txt_enddate.Text = mycode.date();
                        txt_date_from.Text = mycode.date();
                        txt_nextflowingdate.Text = mycode.date();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class_form, "Select  Course_Name,course_id from Add_course_table order by Position asc"); 
                        mycode.bind_all_ddl_with_id_cap_All_NA(ddl_Courses, "Select  Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id_cap_NA(ddl_Reference, "Select  Enquiry_Type,Enquiry_Type_Id from Enquiry_Type where Setup_Type='Reference' order by Enquiry_Type asc");
                       // mycode.bind_all_ddl_with_id_cap_NA(ddl_Source_form, "Select  Enquiry_Type,Enquiry_Type_Id from Enquiry_Type where Setup_Type='Source' order by Enquiry_Type asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_Source, "Select  Enquiry_Type,Enquiry_Type_Id from Enquiry_Type where Setup_Type='Source' order by Enquiry_Type asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_Purpose_search, "Select  Enquiry_Type,Enquiry_Type_Id from Enquiry_Type where Setup_Type='Purpose' order by Enquiry_Type asc");
                        mycode.bind_ddl(ddl_district, "select  District from DistrictList where State='Bihar' order by District");
                        mycode.bind_all_ddl_with_id(ddl_Purpose, "Select  Enquiry_Type,Enquiry_Type_Id from Enquiry_Type where Setup_Type='Purpose' order by Enquiry_Type asc");
                        bind_grd_view();
                        Bind_data_firm_detials();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Next_Follow_List");
            }
        }

        private void Bind_data_firm_detials()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            bind_grd_view();
        }
        private void bind_grd_view()
        {

            string query = "";

            if (ddl_searchby.Text == "Last Follow Up")
            {
                if (ddl_Courses.SelectedItem.Text == "ALL" && ddl_Purpose_search.SelectedItem.Text == "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = " Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0   order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and Status='" + ddl_status.Text + "'  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
                else if (ddl_Courses.SelectedItem.Text != "ALL" && ddl_Purpose_search.SelectedItem.Text == "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and   Class_id='" + ddl_Courses.SelectedValue + "'   order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = " Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and   Class_id='" + ddl_Courses.SelectedValue + "' and Status='" + ddl_status.Text + "  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
                else if (ddl_Courses.SelectedItem.Text == "ALL" && ddl_Purpose_search.SelectedItem.Text != "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and     Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "'  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = " Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0       and Status='" + ddl_status.Text + "' and Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "'  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
                else if (ddl_Courses.SelectedItem.Text != "ALL" && ddl_Purpose_search.SelectedItem.Text != "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0   and Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "' and Class_id='" + ddl_Courses.SelectedValue + "' order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0   and Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "' and Class_id='" + ddl_Courses.SelectedValue + "'  and Status='" + ddl_status.Text + "' order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
            }
            else
            {
                if (ddl_Courses.SelectedItem.Text == "ALL" && ddl_Purpose_search.SelectedItem.Text == "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = " Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0   order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and Status='" + ddl_status.Text + "'  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
                else if (ddl_Courses.SelectedItem.Text != "ALL" && ddl_Purpose_search.SelectedItem.Text == "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and   Class_id='" + ddl_Courses.SelectedValue + "'   order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = " Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and   Class_id='" + ddl_Courses.SelectedValue + "' and Status='" + ddl_status.Text + "  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
                else if (ddl_Courses.SelectedItem.Text == "ALL" && ddl_Purpose_search.SelectedItem.Text != "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0  and     Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "'  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = " Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0       and Status='" + ddl_status.Text + "' and Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "'  order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
                else if (ddl_Courses.SelectedItem.Text != "ALL" && ddl_Purpose_search.SelectedItem.Text != "ALL")
                {
                    if (ddl_status.Text == "ALL")
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0   and Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "' and Class_id='" + ddl_Courses.SelectedValue + "' order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                    else
                    {
                        query = "Select * from(Select ed.Enquiry_Id, ed.Name,ed.Father_name, ed.Phone, ed.Email, ed.Address, ed.districtname, ed.Pincode, ed.Applicant_Image,(Select top 1  Status from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) Status, (Select top 1  format( Next_Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date1,( Select top 1  format( Follow_Up_Date, 'dd/MM/yyyy') from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Datelast,(Select top 1  Next_Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Next_Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Next_Follow_Up_Date,(Select top 1  Follow_Up_Date from Enquiry_flowup where Enquiry_Id=ed.Enquiry_Id order by format(Follow_Up_Date, 'yyyyMMddHHmmss') desc) as Follow_Up_Date,Delete_status, (" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name, format(ed.Created_date, 'dd/MM/yyyy') as Created_date1,(" + coursename + ") as Course_Name    from Enquiry_Details ed ) t  where format(Next_Follow_Up_Date, 'yyyyMMdd') >=  " + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and format(Next_Follow_Up_Date, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " and Delete_status = 0   and Enquiry_Type_Id='" + ddl_Purpose_search.SelectedValue + "' and Class_id='" + ddl_Courses.SelectedValue + "'  and Status='" + ddl_status.Text + "' order by format(Follow_Up_Date, 'yyyyMMdd')  desc";
                    }
                }
            }

            bind_grd_view(query);
        }

        private void bind_grd_view(string query)
        {
            ttlProspect.InnerText = "0";
            ttHotprospect.InnerText = "0";
            ttDeferred.InnerText = "0";
            ttClosed.InnerText = "0";
            ttLost.InnerText = "0";
            tttotalenq.InnerText = "0";
            int Prospect = 0;
            int Hot_prospect = 0;
            int Deferred = 0;
            int Closed = 0;
            int Lost = 0;
            int tttotal = 0;
            try
            {
                ViewState["query"] = query;
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry, there are no follow-up list available.", "warning");
                    btn_excels.Visible = false;
                    print1.Visible = false;
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    btn_excels.Visible = true;
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        print1.Visible = true;
                    }
                    else
                    {
                        print1.Visible = false;
                    }

                    ttlProspect.InnerText = "0";
                    ttHotprospect.InnerText = "0";
                    ttDeferred.InnerText = "0";
                    ttClosed.InnerText = "0";
                    ttLost.InnerText = "0";
                    tttotalenq.InnerText = "0";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Status"].ToString() == "Prospect")
                        {
                            Prospect = Prospect + 1;
                            ttlProspect.InnerText = Prospect.ToString();
                        }
                        if (dt.Rows[i]["Status"].ToString() == "Active")
                        {
                            Prospect = Prospect + 1;
                            ttlProspect.InnerText = Prospect.ToString();
                        }
                        if (dt.Rows[i]["Status"].ToString() == "Hot prospect")
                        {
                            Hot_prospect = Hot_prospect + 1;
                            ttHotprospect.InnerText = Hot_prospect.ToString(); 
                        }
                        if (dt.Rows[i]["Status"].ToString() == "Deferred")
                        {
                            Deferred = Deferred + 1;
                            ttDeferred.InnerText = Deferred.ToString();
                        }
                        if (dt.Rows[i]["Status"].ToString() == "Closed")
                        {
                            Closed = Closed + 1;
                            ttClosed.InnerText = Closed.ToString();
                             
                        }
                        if (dt.Rows[i]["Status"].ToString() == "Lost")
                        {
                            Lost = Lost + 1;
                            ttLost.InnerText = Lost.ToString();
                        }
                        
                    }


                    tttotal = Lost + Closed + Deferred + Hot_prospect + Prospect;
                    tttotalenq.InnerText = tttotal.ToString();

                    //if(ddl_searchby.Text== "Next Follow Up")
                    //{
                    //    ttlProspect.InnerText = "0";
                    //    ttHotprospect.InnerText = "0";
                    //    ttDeferred.InnerText = "0";
                    //    ttClosed.InnerText = "0";
                    //    ttLost.InnerText = "0";
                    //    tttotalenq.InnerText = dt.Rows.Count.ToString();
                    //}



                }
            }
            catch(Exception ex)
            {
                Alertme(ex.ToString(), "warning");
                My.submitException(ex, "Next_Follow_List");


            }
           
        }

        protected void lnk_Flow_up_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Enquiry_Id = (Label)row.FindControl("lbl_Enquiry_Id");
            Bind_flowupdata(lbl_Enquiry_Id.Text);
        }

        private void Bind_flowupdata(string Enquiry_Id)
        {

            ViewState["Enquiry_Id"] = Enquiry_Id;
            string query = "Select *,(" + Purpose + ") as Purpose,(" + Source + ") as Source,(" + Reference + ") as Reference_name,format(Created_date, 'dd/MM/yyyy') as Created_date1,(" + Follow_Up_Datelast + ") as Follow_Up_Datelast,(" + Next_Follow_Up_Date + ") as Next_Follow_Up_Date,(" + coursename + ") as Course_Name from Enquiry_Details ed where  ed.Enquiry_Id='" + Enquiry_Id + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_father_name.Text = dt.Rows[0]["Father_name"].ToString();
                if (dt.Rows[0]["Course_Name"].ToString() == "")
                {
                    lbl_class.Text = "NA";
                }
                else
                {
                    lbl_class.Text = dt.Rows[0]["Course_Name"].ToString();
                }
                if (dt.Rows[0]["Applicant_Image"].ToString() == "")
                {
                    Image1.ImageUrl = "../images/dummy-student.jpg";
                }
                else
                {
                    Image1.ImageUrl = dt.Rows[0]["Applicant_Image"].ToString();
                }
                lbl_Enquirydate.Text = dt.Rows[0]["Created_date1"].ToString();

                lbl_lastfloupdate.Text = dt.Rows[0]["Follow_Up_Datelast"].ToString();
                lbl_nextfloupdate.Text = dt.Rows[0]["Next_Follow_Up_Date"].ToString();
                lbl_name.Text = dt.Rows[0]["Name"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["Phone"].ToString();
                lbl_email.Text = dt.Rows[0]["Email"].ToString();
                lbl_address.Text = dt.Rows[0]["Address"].ToString();
                lbl_remarks.Text = dt.Rows[0]["Note"].ToString();

                if (dt.Rows[0]["Reference_name"].ToString() == "")
                {
                    lbl_reffrence.Text = "NA";
                }
                else
                {
                    lbl_reffrence.Text = dt.Rows[0]["Reference_name"].ToString();
                }

                lbl_sources.Text = dt.Rows[0]["Source"].ToString();
                lbl_Purpose.Text = dt.Rows[0]["Purpose"].ToString();
                lbl_enquiry_Purpose.Text = dt.Rows[0]["Purpose"].ToString();
                ddl_Purpose.SelectedValue = dt.Rows[0]["Enquiry_Type_Id"].ToString();

                ddl_Reference.SelectedValue = dt.Rows[0]["Reference"].ToString();
                //ddl_Source_form.SelectedValue = dt.Rows[0]["Source_id"].ToString();
                lbl_created_by.Text = mycode.get_user(dt.Rows[0]["created_by"].ToString()) + " {" + dt.Rows[0]["created_by"].ToString() + "}";


                string query1 = "Select *, format(Follow_Up_Date, 'dd/MM/yyyy') as Follow_Up_Date1q,format(Next_Follow_Up_Date, 'dd/MM/yyyy') as Next_Follow_Up_Date1q  from Enquiry_flowup where  Enquiry_Id='" + Enquiry_Id + "' order by id desc";
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    GrdView_Follow_Up.DataSource = null;
                    GrdView_Follow_Up.DataBind();
                }
                else
                {
                    GrdView_Follow_Up.DataSource = dt1;
                    GrdView_Follow_Up.DataBind();
                }
                try
                {
                    if (dt.Rows[0]["Status"].ToString() == "Closed")
                    {
                        lbl_nextflwodatetitle.Text = "Enquiry Closed Date : ";
                        row1.Visible = false;
                        row2.Visible = false;
                        row3.Visible = false;
                    }
                    else if (dt.Rows[0]["Status"].ToString() == "Lost")
                    {
                        lbl_nextflwodatetitle.Text = "Enquery Lost Date : ";
                        row1.Visible = false;
                        row2.Visible = false;
                        row3.Visible = false;
                    }
                    else
                    {
                        lbl_nextflwodatetitle.Text = "Next Follow Up Date :";
                        row1.Visible = true;
                        row2.Visible = true;
                        row3.Visible = true;
                    }
                }
                catch
                {

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    //string mergeStartTime = mycode.date() + " " + mycode.time();
                    // DateTime Created_date = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Enquiry_Id = (Label)row.FindControl("lbl_Enquiry_Id");
                    My.exeSql("update Enquiry_Details set Delete_status=1,Delete_by='" + ViewState["Userid"].ToString() + "',Delete_date='" + My.getdate1() + "' where Enquiry_Id='" + lbl_Enquiry_Id.Text + "'");
                    bind_grd_view(ViewState["query"].ToString());
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "delete enquirypage");
            }


        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Enquiry_Id = (Label)row.FindControl("lbl_Enquiry_Id");
                Label lbl_Next_Follow_Up_Date1 = (Label)row.FindControl("lbl_Next_Follow_Up_Date1");
                string query = "Select *,format(Created_date, 'dd/MM/yyyy') as Created_date1,(" + lastrowid + ") as lastid from Enquiry_Details ed where ed.Enquiry_Id='" + lbl_Enquiry_Id.Text + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {

                }
                else
                {

                    hd_enqid.Value = dt.Rows[0]["Enquiry_Id"].ToString();
                    txt_name.Text = dt.Rows[0]["Name"].ToString();
                    txt_phone_no.Text = dt.Rows[0]["Phone"].ToString();
                    txt_email.Text = dt.Rows[0]["Email"].ToString();
                    txt_address.Text = dt.Rows[0]["Address"].ToString();
                    txt_Remarks.Text = dt.Rows[0]["Note"].ToString();
                    ddl_Purpose.SelectedValue = dt.Rows[0]["Enquiry_Type_Id"].ToString();
                    ddl_Reference.SelectedValue = dt.Rows[0]["Reference"].ToString();
                    //ddl_Source_form.SelectedValue = dt.Rows[0]["Source_id"].ToString();
                    ddl_district.Text = dt.Rows[0]["districtname"].ToString();
                    txt_date_from.Text = dt.Rows[0]["Created_date1"].ToString();
                    txt_pincode.Text = dt.Rows[0]["Pincode"].ToString();
                    txt_nextflowingdate.Text = lbl_Next_Follow_Up_Date1.Text;
                    ViewState["lastid"] = dt.Rows[0]["lastid"].ToString();
                    ViewState["Applicant_Image"] = dt.Rows[0]["Applicant_Image"].ToString();
                    if (ViewState["Applicant_Image"].ToString() == "")
                    {
                        img_passport_photo.Visible = false;
                    }
                    else
                    {
                        img_passport_photo.Visible = true;
                        img_passport_photo.ImageUrl = dt.Rows[0]["Applicant_Image"].ToString();
                    }

                    btn_save.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        #region save
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Purpose.SelectedItem.Text == "Select")
                {
                    Alertme("Please select purpose", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
                else if (ddl_Reference.SelectedItem.Text == "Select")
                {
                    Alertme("Please select refrence", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
                //else if (ddl_Source_form.SelectedItem.Text == "Select")
                //{
                //    Alertme("Please select enquiry source", "warning");
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                //}
                else if (ddl_class_form.SelectedItem.Text == "Select")
                {
                    Alertme("Please select course", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
                else if (ddl_district.SelectedItem.Text == "Select")
                {
                    Alertme("Please select district name", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }

                else
                {
                    if (btn_save.Text == "Update")
                    {
                        if (ViewState["Is_add"].ToString() == "1")
                        {
                            update_data_on_table();
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }
                    else
                    {
                        if (ViewState["Is_Edit"].ToString() == "1")
                        {
                            send_data_on_table();
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "add student enquiry");
            }

        }

        private void update_data_on_table()
        {
            string mergeStartTime = txt_date_from.Text + " " + mycode.time();
            DateTime Created_date = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string mergeStartTimenextflowingdate = txt_nextflowingdate.Text + " " + mycode.time();
            DateTime nextflowingdate = DateTime.ParseExact(mergeStartTimenextflowingdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string query = "Update Enquiry_Details set  Enquiry_Type_Id=@Enquiry_Type_Id,Name=@Name,Phone=@Phone,Email=@Email,Address=@Address,Note=@Note,Reference=@Reference,Source_id=@Source_id,Class_id=@Class_id,districtname=@districtname,Pincode=@Pincode,Applicant_Image=@Applicant_Image where Enquiry_Id = @Enquiry_Id; ";

            SqlCommand cmd;

            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Enquiry_Id", hd_enqid.Value);
            cmd.Parameters.AddWithValue("@Enquiry_Type_Id", ddl_Purpose.SelectedValue);
            cmd.Parameters.AddWithValue("@Name", txt_name.Text);
            cmd.Parameters.AddWithValue("@Phone", txt_phone_no.Text);
            cmd.Parameters.AddWithValue("@Email", txt_email.Text);
            cmd.Parameters.AddWithValue("@Address", txt_address.Text);
            cmd.Parameters.AddWithValue("@Note", txt_Remarks.Text);
            cmd.Parameters.AddWithValue("@created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Reference", ddl_Reference.SelectedValue);
            cmd.Parameters.AddWithValue("@Source_id", "0");
            cmd.Parameters.AddWithValue("@Class_id", ddl_class_form.SelectedValue);
            cmd.Parameters.AddWithValue("@Created_date", Created_date);
            cmd.Parameters.AddWithValue("@districtname", ddl_district.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Pincode", txt_pincode.Text);
            cmd.Parameters.AddWithValue("@Applicant_Image", ViewState["Applicant_Image"].ToString());


            if (My.InsertUpdateData(cmd))
            {


                SqlCommand cmd1;
                string query1 = "Update Enquiry_flowup set  Follow_Up_Date=@Follow_Up_Date,Next_Follow_Up_Date=@Next_Follow_Up_Date,Response_Remarks=@Response_Remarks where Id = @Id";
                cmd1 = new SqlCommand(query1);
                cmd1.Parameters.AddWithValue("@id", ViewState["lastid"].ToString());
                cmd1.Parameters.AddWithValue("@Follow_Up_Date", Created_date);
                cmd1.Parameters.AddWithValue("@Next_Follow_Up_Date", nextflowingdate);
                cmd1.Parameters.AddWithValue("@Response_Remarks", txt_Remarks.Text);
                if (My.InsertUpdateData(cmd1))
                {
                    ViewState["Applicant_Image"] = "";
                    img_passport_photo.ImageUrl = "";
                    img_passport_photo.Visible = false;
                    txt_name.Text = "";
                    txt_phone_no.Text = "";
                    txt_email.Text = "";
                    txt_address.Text = "";
                    txt_Remarks.Text = "";
                    txt_pincode.Text = "";
                    ddl_class_form.SelectedValue = "0";
                   // ddl_Source_form.SelectedValue = "0";
                    ddl_Reference.SelectedValue = "0";
                    btn_save.Text = "Save";
                    Alertme("Record has been updated successfully", "success");
                    bind_grd_view();

                }
            }
        }

        private void send_data_on_table()
        {

            string mergeStartTime = txt_date_from.Text + " " + mycode.time();
            DateTime Created_date = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            string mergeStartTimenextflowingdate = txt_nextflowingdate.Text + " " + mycode.time();
            DateTime nextflowingdate = DateTime.ParseExact(mergeStartTimenextflowingdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


            string query = "INSERT INTO Enquiry_Details (Enquiry_Id,Enquiry_Type_Id,Name,Phone,Email,Address,Description,Note,Created_date,created_by,Reference,Source_id,Class_id,Status,Delete_status,districtname,Pincode,Applicant_Image) values (@Enquiry_Id,@Enquiry_Type_Id,@Name,@Phone,@Email,@Address,@Description,@Note,@Created_date,@created_by,@Reference,@Source_id,@Class_id,@Status,@Delete_status,@districtname,@Pincode,@Applicant_Image)";
            string Enquiry_Id = create_sl_no();
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Enquiry_Id", Enquiry_Id);
            cmd.Parameters.AddWithValue("@Enquiry_Type_Id", ddl_Purpose.SelectedValue);
            cmd.Parameters.AddWithValue("@Name", txt_name.Text);
            cmd.Parameters.AddWithValue("@Phone", txt_phone_no.Text);
            cmd.Parameters.AddWithValue("@Email", txt_email.Text);
            cmd.Parameters.AddWithValue("@Address", txt_address.Text);
            cmd.Parameters.AddWithValue("@Description", "");
            cmd.Parameters.AddWithValue("@Note", txt_Remarks.Text);
            cmd.Parameters.AddWithValue("@created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Reference", ddl_Reference.SelectedValue);
            cmd.Parameters.AddWithValue("@Source_id", "0");
            cmd.Parameters.AddWithValue("@Class_id", ddl_class_form.SelectedValue);
            cmd.Parameters.AddWithValue("@Created_date", Created_date);
            cmd.Parameters.AddWithValue("@Status", "Active");
            cmd.Parameters.AddWithValue("@Delete_status", "0");
            cmd.Parameters.AddWithValue("@districtname", ddl_district.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Pincode", txt_pincode.Text);
            cmd.Parameters.AddWithValue("@Applicant_Image", ViewState["Applicant_Image"].ToString());

            if (My.InsertUpdateData(cmd))
            {
                string query1 = "INSERT INTO Enquiry_flowup (Enquiry_Id,Follow_Up_Date,Next_Follow_Up_Date,Response_Remarks,Created_by,Status) values (@Enquiry_Id,@Follow_Up_Date,@Next_Follow_Up_Date,@Response_Remarks,@Created_by,@Status)";

                SqlCommand cmd1;
                cmd1 = new SqlCommand(query1);
                cmd1.Parameters.AddWithValue("@Enquiry_Id", Enquiry_Id);
                cmd1.Parameters.AddWithValue("@Follow_Up_Date", Created_date);
                cmd1.Parameters.AddWithValue("@Next_Follow_Up_Date", nextflowingdate);
                cmd1.Parameters.AddWithValue("@Response_Remarks", txt_Remarks.Text);
                cmd1.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd1.Parameters.AddWithValue("@Status", "Active");
                if (My.InsertUpdateData(cmd1))
                {
                    txt_name.Text = "";
                    txt_phone_no.Text = "";
                    txt_email.Text = "";
                    txt_address.Text = "";
                    txt_Remarks.Text = "";
                    txt_pincode.Text = "";
                    ddl_class_form.SelectedValue = "0";
                   // ddl_Source_form.SelectedValue = "0";
                    ddl_Reference.SelectedValue = "0";
                    ViewState["Applicant_Image"] = "";
                    img_passport_photo.ImageUrl = "";
                    img_passport_photo.Visible = false;
                    Alertme("Record has been saved successfully", "success");
                    bind_grd_view();
                }
            }

        }

        private string create_sl_no()
        {
            bool duplicate = true;
            string Enquiry_Type_Id = "ENQ" + My.auto_serialS("stu_Enquiry_Id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Enquiry_Id from dbo.[Enquiry_Details] where Enquiry_Id='" + Enquiry_Type_Id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Enquiry_Type_Id = "ENQ" + My.auto_serialS("stu_Enquiry_Id");
                }
            }
            return Enquiry_Type_Id;
        }

        #endregion


        #region save floupdata
        protected void btn_FollowUp_Click(object sender, EventArgs e)
        {


            try
            {
                if (txt_followupdate.Text == "")
                {
                    txt_followupdate.Text = mycode.date();
                }


                if (txtnextfllowupdate.Text == "")
                {
                    txtnextfllowupdate.Text = mycode.date();
                }
                string mergeStartTime = txt_followupdate.Text + " " + mycode.time();
                DateTime Created_date = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                string mergeStartTimenextflowingdate = txtnextfllowupdate.Text + " " + mycode.time();
                DateTime nextflowingdate = DateTime.ParseExact(mergeStartTimenextflowingdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                string query1 = "INSERT INTO Enquiry_flowup (Enquiry_Id,Follow_Up_Date,Next_Follow_Up_Date,Response_Remarks,Created_by,Status) values (@Enquiry_Id,@Follow_Up_Date,@Next_Follow_Up_Date,@Response_Remarks,@Created_by,@Status)";

                SqlCommand cmd1;
                cmd1 = new SqlCommand(query1);
                cmd1.Parameters.AddWithValue("@Enquiry_Id", ViewState["Enquiry_Id"].ToString());
                cmd1.Parameters.AddWithValue("@Follow_Up_Date", Created_date);
                cmd1.Parameters.AddWithValue("@Next_Follow_Up_Date", nextflowingdate);
                cmd1.Parameters.AddWithValue("@Response_Remarks", txt_remarks_floup.Text);
                cmd1.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd1.Parameters.AddWithValue("@Status", ddl_status_fllowup.Text);
                if (My.InsertUpdateData(cmd1))
                {

                    mycode.executequery("update Enquiry_Details set Status='" + ddl_status_fllowup.Text + "' where Enquiry_Id='" + ViewState["Enquiry_Id"].ToString() + "'");
                    txt_followupdate.Text = "";
                    txtnextfllowupdate.Text = "";
                    txt_remarks_floup.Text = "";
                    ddl_status_fllowup.Text = "Active";
                    Alertme("Record has been saved successfully", "success");
                    Bind_flowupdata(ViewState["Enquiry_Id"].ToString());
                    bind_grd_view(ViewState["query"].ToString());
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
            }
            catch (Exception ex)
            {

            }


        }

        #endregion



        protected void btn_passport_photo_Click(object sender, EventArgs e)
        {
            string otp = My.create_random_no_otp();
            string filepassportphoto = "";
            if (file_passport_photo.HasFile)
            {
                if (file_passport_photo.FileBytes.Length < 500000)
                {
                    filepassportphoto = upload_image(file_passport_photo, "passport_size", otp);
                    if (filepassportphoto == "")
                    {
                        btn_passport_photo.Focus();
                        Alertme("Please upload valid passport size photo", "warning");
                        file_passport_photo.Focus();
                        return;
                    }
                    else
                    {

                        btn_passport_photo.Focus();
                        img_passport_photo.Visible = true;

                        ViewState["Applicant_Image"] = filepassportphoto;
                        img_passport_photo.ImageUrl = filepassportphoto;
                    }
                }
                else
                {
                    Alertme("Please Reduce or compress size of passport size photo max(500kb)", "warning");
                    file_passport_photo.Focus();
                }
            }
            else
            {
                Alertme("Please upload valid passport size photo.", "warning");
                file_passport_photo.Focus();

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
        }

        private string upload_image(FileUpload Files, string name, string Applyid)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = name + date + time + "_" + Applyid;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = Files.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".jpeg", ".jpg", ".JPEG" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;

                    break;
                }
            }


            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("/UploadedImage/Uploads")).ToString();
                    Files.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;

                    Alertme("File has not save.", "warning");

                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = "/UploadedImage/Uploads/" + fileName;
            }
            return dbfilePath;
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {

                string heading = "Export_Next_Flowup_List" + mycode.date() + mycode.itime() + ".xls";

                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + heading);
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }

                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string status = ((Label)e.Item.FindControl("lbl_status")).Text;
                string classname = ((Label)e.Item.FindControl("lbl_class")).Text;
                if (classname == "")
                {
                    ((Label)e.Item.FindControl("lbl_class")).Text = "NA";
                }
                else
                {

                }
                string Reference = ((Label)e.Item.FindControl("lbl_Reference")).Text;
                if (Reference == "")
                {
                    ((Label)e.Item.FindControl("lbl_Reference")).Text = "NA";
                }



                if (status.ToUpper() == "ADMISSION DONE" || status.ToUpper() == "CLOSED" || status.ToUpper() == "LOST")
                {
                    ((LinkButton)e.Item.FindControl("lnk_form_sale")).Visible = false;
                    ((LinkButton)e.Item.FindControl("lnk_form_sale")).Visible = false;
                } 
                if (status.ToUpper() == "FORM SOLD")
                {
                    ((LinkButton)e.Item.FindControl("lnk_form_sale")).Visible = false;
                } 
            }
        }


        protected void lnk_make_admission_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Enquiry_Id = (Label)row.FindControl("lbl_Enquiry_Id");
                Response.Redirect("admission.aspx?enqid=" + lbl_Enquiry_Id.Text, false);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_form_sale_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Enquiry_Id = (Label)row.FindControl("lbl_Enquiry_Id");
                Response.Redirect("form-sale.aspx?enqid=" + lbl_Enquiry_Id.Text, false);
            }
            catch (Exception ex)
            {
            }
        }
    }
}