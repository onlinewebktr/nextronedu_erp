using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class discount_group_wsie_student : System.Web.UI.Page
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
                        ViewState["saved_feildDT"] = "0";
                        ViewState["widthtable"] = "1019";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();

                        bind_class(); bind_section();


                        mycode.bind_all_ddl_with_id_cap_All(ddl_discount_group, "select Category_Name,Category_Id from dbo.[Category_Details] order by Category_Name asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_discount_sub_group, "select Sub_CategoryName,Sub_CategoryId from dbo.[Sub_Category_Details] order by Sub_CategoryName asc");
                        lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text;
                        find_firm_details();
                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"]; 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_section()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select distinct Section,Section as Sections from section_master order by Section asc", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_sections.DataTextField = "Section";
                ddl_sections.DataValueField = "Sections";
                ddl_sections.DataSource = reader;
                ddl_sections.DataBind();
            }
        }

        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
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


        private void bind_grd_view(string query)
        {
            try
            {
                //lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text + " Student Type : " + ddl_studenttype.SelectedItem.Text;
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            catch
            {
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    DataTable dt = mycode.FillData(ViewState["query"].ToString());
                    export_to_excel(dt, "Student_list");
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

        private void export_to_excel(DataTable dt, string file)
        {
            if (dt.Rows.Count > 0)
            {
                DateTime dTimet = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dTimet.ToString("dd_MM_yyyy");
                string time = dTimet.ToString("hh_mm_ss");
                String filerename = file + date + time;
                string attachment = "attachment; filename=" + filerename + ".csv";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "text/csv";
                var csvContent = My.DataTableToCsv(dt);
                Response.Write(csvContent);
                Response.End();
            }
            else
            {
                Alertme("Data not found.", "warning");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            lbl_class22.Text = "";
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }

                string qoute = "'";
                //For Class
                bool isClassSelectd = false; string selectClassid = "";
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (item.Selected)
                    {
                        selectClassid = selectClassid + qoute + item.Value + qoute + ",";
                        isClassSelectd = true;
                    }
                }
                if (isClassSelectd == false)
                {
                    ddl_classs.Focus();
                    Alertme("Please select class.", "warning");
                    return;
                }
                if (isClassSelectd == true)
                {
                    selectClassid = selectClassid.Remove(selectClassid.Length - 1);
                }

                //===========================
                bool isSectionSelectd = false; string selectSection = "";
                foreach (ListItem item in ddl_sections.Items)
                {
                    if (item.Selected)
                    {
                        selectSection = selectSection + qoute + item.Value + qoute + ",";
                        isSectionSelectd = true;
                    }
                }
                if (isSectionSelectd == false)
                {
                    ddl_sections.Focus();
                    Alertme("Please select section.", "warning");
                    return;
                }
                if (isSectionSelectd == true)
                {
                    selectSection = selectSection.Remove(selectSection.Length - 1);
                }



                //lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text + " Student Type : " + ddl_studenttype.SelectedItem.Text;
                find_by_c_s_a(selectClassid, selectSection);


                ViewState["flag"] = "1";

            }
            catch (Exception ex)
            {
            }
        }




        private void find_by_c_s_a(string selectClassid, string selectSection)
        {
            if (ddl_studenttype.SelectedItem.Text == "ALL")
            {
                string query = "";
                if (ddl_discount_group.SelectedItem.Text != "ALL" && ddl_discount_sub_group.SelectedItem.Text != "ALL")
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and t1.Category_id='"+ddl_discount_group.SelectedValue+ "' and t1.SubCategory_id='" + ddl_discount_sub_group.SelectedValue + "' and (t1.Transfer_Status='New' or t1.Transfer_Status='NT' or t1.Transfer_Status='Transferred') order by ct.Position,t1.rollnumber asc";
                }
                else if (ddl_discount_group.SelectedItem.Text == "ALL" && ddl_discount_sub_group.SelectedItem.Text != "ALL")
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and t1.SubCategory_id='" + ddl_discount_sub_group.SelectedValue + "' and (t1.Transfer_Status='New' or t1.Transfer_Status='NT' or t1.Transfer_Status='Transferred') order by ct.Position,t1.rollnumber asc";
                }
                else if (ddl_discount_group.SelectedItem.Text != "ALL" && ddl_discount_sub_group.SelectedItem.Text == "ALL")
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and t1.Category_id='" + ddl_discount_group.SelectedValue + "' and (t1.Transfer_Status='New' or t1.Transfer_Status='NT' or t1.Transfer_Status='Transferred') order by ct.Position,t1.rollnumber asc";
                }
                else
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and (t1.Transfer_Status='New' or t1.Transfer_Status='NT' or t1.Transfer_Status='Transferred') order by ct.Position,t1.rollnumber asc";
                }
                bind_grd_view(query);
            }
            else
            {
                string query = "";
                if (ddl_discount_group.SelectedItem.Text != "ALL" && ddl_discount_sub_group.SelectedItem.Text != "ALL")
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and t1.Category_id='" + ddl_discount_group.SelectedValue + "' and t1.SubCategory_id='" + ddl_discount_sub_group.SelectedValue + "' and t1.Transfer_Status='"+ddl_studenttype.SelectedValue+"' order by ct.Position,t1.rollnumber asc";
                }
                else if (ddl_discount_group.SelectedItem.Text == "ALL" && ddl_discount_sub_group.SelectedItem.Text != "ALL")
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and t1.SubCategory_id='" + ddl_discount_sub_group.SelectedValue + "' and t1.Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,t1.rollnumber asc";
                }
                else if (ddl_discount_group.SelectedItem.Text != "ALL" && ddl_discount_sub_group.SelectedItem.Text == "ALL")
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and t1.Category_id='" + ddl_discount_group.SelectedValue + "' and t1.Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,t1.rollnumber asc";
                }
                else
                {
                    query = "select *,CASE WHEN t1.Transfer_Status = 'New' THEN 'New Admission' WHEN t1.Transfer_Status = 'NT' THEN 'Old Admission'  WHEN t1.Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN t1.Status = '0' THEN 'Inactive' WHEN t1.Status = '1' THEN 'Active' END AS Status_name,(select top 1 Category_Name from Category_Details where Category_Id=t1.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=t1.Category_id and Sub_CategoryId=t1.SubCategory_id) as SubCategory_name from admission_registor t1 join Add_course_table ct on t1.Class_id=ct.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id in (" + selectClassid + ") and t1.Section in (" + selectSection + ") and Status='1' and t1.Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,t1.rollnumber asc";
                }
                bind_grd_view(query);
            }
        }
    }
}