using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Library_Admin
{
    public partial class Print_Student_Library_card : System.Web.UI.Page
    {
        Library lb = new Library();
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
                        Session["pagelibstu"] = "1";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["flag"] = "0";
                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = Library.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();


                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details ");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor");

                       

                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");

                        try
                        {
                            if (Request.QueryString["page"] != null)
                            {

                                if (Request.QueryString["type"].ToString() == "today")
                                {
                                  
                                    date_wise_find();
                                }
                                else
                                {
                                    find_by_All();
                                }
                            }
                            else
                            {
                                ddl_section.Text = My.get_top_one_section();
                                ddlclass.SelectedValue = My.get_top_one_class();
                                find_by_class();
                            }
                        }
                        catch
                        {
                            ddl_section.Text = My.get_top_one_section();
                            ddlclass.SelectedValue = My.get_top_one_class();
                            find_by_class();
                        }
                        

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void date_wise_find()
        {
            bind_grd_view("select Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Status='1' and (Lib_Bar_code is not null or Lib_Bar_code is not null) and lib_card_IssueIdate='"+mycode.idate()+"'  order by id desc");
        }

        private void find_by_All()
        {
            bind_grd_view("select Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Status='1' and (Lib_Bar_code is not null or Lib_Bar_code is not null)  order by id desc");
        }

        private void find_firm_details()
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
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_by_class();
                ViewState["flag"] = "3";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_class()
        {
            bind_grd_view("select Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Status='1' and (Lib_Bar_code is not null or Lib_Bar_code is not null)  order by id desc");
        }





        #region CountDataA

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
        #endregion

        private void bind_grd_view(string qry)
        {
            ViewState["query"] = qry;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
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
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else
                {
                    find_by_c_s_a();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_c_s_a()
        {
            bind_grd_view("select Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and  Section='" + ddl_section.SelectedItem.Text + "' and (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV'and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Status='1' and (Lib_Bar_code is not null or Lib_Bar_code is not null) order by rollnumber asc");
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
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
                    mycode.bind_all_ddl_with_id(ddlclass, "  Select  Course_Name, course_id  from Add_course_table order by  Position");
                    find_by_session();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_session()
        {
            bind_grd_view("select Session_id, session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and  Session_id='" + ddlsession.SelectedValue + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Status='1' and (Lib_Bar_code is not null or Lib_Bar_code is not null) order by id desc");
        }




        #region ExcelDownloaD
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView.RenderControl(hw);
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

        #endregion

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Panel lbl_istatus = (Panel)e.Row.FindControl("pnl_print_front");
                Panel pnl_print_back = (Panel)e.Row.FindControl("pnl_print_back");

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                    pnl_print_back.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                    pnl_print_back.Visible = false;
                }

            }
        }
    }
}