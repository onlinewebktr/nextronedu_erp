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
    public partial class reprint_monthly_fees : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!IsPostBack)
                    {
                        Session["SlipBkSn"] = "MN3";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["sessionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_session_serach, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session_serach.SelectedValue= My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_course_search, "Select Course_Name,course_id from Add_course_table");
                        ddl_course_search.SelectedValue = My.get_top_one_class();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        find_firm_details();



                        try
                        {
                            if (Session["querymonth"] == null)
                            {
                                Bind_class_wise_student();
                            }
                            else
                            {

                                ddl_session_serach.SelectedValue = Session["session_id"].ToString();
                                ddl_course_search.SelectedValue = Session["course_id"].ToString();
                                Bind_data(Session["querymonth"].ToString());
                              

                            }
                        }
                        catch
                        {
                            Bind_class_wise_student();
                        }





                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Admission_Fee_Collection");
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


        private void Bind_data(string query)
        {
            Session["session_id"] = ddl_session_serach.SelectedValue;
            Session["course_id"] = ddl_course_search.SelectedValue;
            Session["querymonth"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                Alertme("Sorry there are no students list exists", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                lbl_class22.Text = "Session :" + ddl_session_serach.SelectedItem.Text + " Class : " + ddl_course_search.SelectedItem.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void ddl_course_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session_serach.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
             
            else
            {

                Bind_class_wise_student();

               
            }
        }

        private void Bind_class_wise_student()
        {
            if (ddl_course_search.SelectedItem.Text == "All")
            {
                string query = "select  t2.studentname,t1.Addmission_no,t2.class,t2.Section,t2.rollnumber,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Monthly'  and t2.Session_id='" + ddl_session_serach.SelectedValue + "' and t2.Status='1' order by t1.id desc";
                Bind_data(query);
            }
            else
            {
                string query = "select  t2.studentname,t1.Addmission_no,t2.class,t2.Section,t2.rollnumber,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='Monthly'  and t2.Session_id='" + ddl_session_serach.SelectedValue + "' and t2.Class_id='" + ddl_course_search.SelectedValue + "' and t2.Status='1' order by t1.id desc ";
                Bind_data(query);
            }
        }

        //protected void btn_collection_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Button lnk = (Button)sender;
        //        RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
        //        Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
        //        Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
        //        Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
        //        Label lbl_Slip_no = (Label)row.FindControl("lbl_Slip_no");
        //        Session["reprintadmission"] = "1";
        //        string url = "slip/monthly-slip.aspx?admissionno=" + lbl_admissionserialnumber.Text + "&sessionid=" + lbl_sessionid.Text + "&classid=" + lbl_Class_id.Text + "&Slip_no=" + lbl_Slip_no.Text + "";
        //        Response.Redirect(url, false);
        //    }
        //    catch
        //    {
        //    }
        //}

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
    }
}