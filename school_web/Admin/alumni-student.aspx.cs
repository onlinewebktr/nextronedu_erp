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
    public partial class alumni_student : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();


                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");

                        bind_class();
                        ddlsession.SelectedValue = My.get_session_id();

                        bind_all_data();
                        ViewState["flag"] = "0";

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
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
            }
            catch (Exception ex)
            {
                //  My.saveExceptionDetails(ex, "StudentList");
            }
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
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
        }


        private void bind_all_data()
        {
            bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "' order by t1.rollnumber asc");

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
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                find_by_session();
                ViewState["flag"] = "2";
            }
            else
            {
                bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "' order by t1.rollnumber asc");

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



        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                lbl_class22.Text = "Session : "+ddlsession.SelectedItem.Text+ " Class : "+ ddlclass.SelectedItem.Text+ " Section : "+ ddl_section.SelectedItem.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
            string query2 = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "'   order by t1.rollnumber asc"); 
            }
            else
            {
                if (ddl_section.Text == "ALL")
                {
                    bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "'  order by t1.rollnumber asc");

                }
                else
                {
                    bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "' and t1.Section='" + ddl_section.Text + "'  order by t1.rollnumber asc");

                }
            }
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
            bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "'  order by t1.rollnumber asc");
        }

        protected void btn_fnd_by_days_Click(object sender, EventArgs e)
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
                    find_by_admission_in();
                    ViewState["flag"] = "4";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission_in()
        {
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "'   order by t1.rollnumber asc");
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "'  and t1.Class_id='" + ddlclass.SelectedValue + "' order by t1.rollnumber asc");
                }
                else
                {
                    bind_grd_view("select t1.*,t2.Remark,t2.Created_date,'ALUMNI' AS Admission_Type from admission_registor t1 join Alumni_history t2 on t1.Session_id=t2.Session_id and t1.admissionserialnumber=t2.Admission_no where t1.Transfer_Status='ALUMNI' and t1.Status='0' and t1.Session_id='" + ddlsession.SelectedValue + "'  and t1.Class_id='" + ddlclass.SelectedValue + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' order by t1.rollnumber asc");
                }
            }
        }

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
                        Panel1.RenderControl(hw);
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
    }
}