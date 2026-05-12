using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class categorywise_student : System.Web.UI.Page
    {
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_category, "select Category_Name,Category_Id from dbo.[Category_Details] order by Category_Name asc");

                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        find_by_gender();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_sub_category, "select Sub_CategoryName,Sub_CategoryId from dbo.[Sub_Category_Details] where Category_Id='" + ddl_category.SelectedValue + "' order by Sub_CategoryName asc");
            }
            catch (Exception ex)
            {
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
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }

                else
                {
                    find_by_gender();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void find_by_gender()
        {
            if (ddl_category.SelectedItem.Text == "ALL")
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {
                    bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' order by rollnumber asc");
                }
                else
                {
                    if (ddl_section.SelectedItem.Text == "ALL")
                    {
                        bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");
                    }
                    else
                    {
                        bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");
                    }
                }
            }
            else
            {
                if (ddl_sub_category.SelectedItem.Text == "ALL")
                {
                    if (ddlclass.SelectedItem.Text == "ALL")
                    {
                        bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and  Category_id='" + ddl_category.SelectedValue + "' order by rollnumber asc");
                    }
                    else
                    {
                        if (ddl_section.SelectedItem.Text == "ALL")
                        {
                            bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and  Category_id='" + ddl_category.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and  Category_id='" + ddl_category.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");
                        }
                    }
                }
                else
                {
                    if (ddlclass.SelectedItem.Text == "ALL")
                    {
                        bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'  and  Category_id='" + ddl_category.SelectedValue + "' and SubCategory_id='" + ddl_sub_category.SelectedValue + "' order by rollnumber asc");
                    }
                    else
                    {
                        if (ddl_section.SelectedItem.Text == "ALL")
                        {
                            bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and  Category_id='" + ddl_category.SelectedValue + "' and SubCategory_id='" + ddl_sub_category.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,(select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id) as Category_name,(select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id) as SubCategory_name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and  Category_id='" + ddl_category.SelectedValue + "' and SubCategory_id='" + ddl_sub_category.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");
                        }
                    }
                }
            }
        }

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
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

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
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


    }
}