using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class online_transfered_student : System.Web.UI.Page
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
                        string pagename_current = "online-transfered-student.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        //bind_all_data();
                        ViewState["flag"] = "0";

                        bind_data();
                        find_by_c_s_a();
                        ViewState["flag"] = "1";

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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
        private void bind_all_data()
        {
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name from Online_Admission where Steps_done='10' and  Payment_Status='Paid' and Is_transfer='1' order by id desc");




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
            // bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name from Online_Admission where Steps_done='10' and Is_transfer='1' and  Payment_Status='Paid' and  Class_id='" + ddlclass.SelectedValue + "'  order by id desc");
            find_by_c_s_a();
            ViewState["flag"] = "1";
            lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
        }


      


        #region CountDataA
        private void bind_data()
        {
            string datEtim = mycode.date();
            DateTime startTime = DateTime.ParseExact(datEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string OneWeekDate = startTime.AddDays(-7).ToShortDateString();
            int oneWeek = My.DateConvertToIdate(OneWeekDate);


            // int oneWeek = 20210801;

            string sql = @"select  count(id) as ttl_admisiion from Online_Admission where Steps_done='10' and Is_transfer='1' and  Payment_Status='Paid';
                                       select  count(id) as ttl_admisiion_last_week from Online_Admission where Steps_done='10' and  Payment_Status='Paid'  and Is_transfer='1' and  idate>" + oneWeek + @";

                                       select count(id) as ttl_admisiion_in_hostel from Online_Admission where Steps_done='10' and  Payment_Status='Paid' and Is_transfer='1' and Services='Hosteler';
                                       select count(id) as ttl_admisiion_in_hostel_lst_week from Online_Admission where Steps_done='10' and  Payment_Status='Paid' and Is_transfer='1' and Services='Hosteler' and idate>" + oneWeek + @";

                                        select count(id) as ttl_admisiion_in_days from Online_Admission where Steps_done='10' and  Payment_Status='Paid' and Is_transfer='1' and Services='Day Scholar';
                                         select count(id) as ttl_admisiion_in_days_lst_week from Online_Admission where Steps_done='10' and  Payment_Status='Paid' and Is_transfer='1' and Services='Day Scholar' and idate>" + oneWeek + @";";

            DataSet ds = mycode.Fill_Data_set(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[0];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlodR.InnerText = dtTemp.Rows[0]["ttl_admisiion"].ToString();
                }
                else
                {
                    ttlodR.InnerText = "00";
                }
            }
            else
            {
                ttlodR.InnerText = "00"; ;
            }


            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[1];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlodRLstWeeK.InnerText = dtTemp.Rows[0]["ttl_admisiion_last_week"].ToString() + "+ from last week";
                }
                else
                {
                    ttlodRLstWeeK.InnerText = "00";
                }
            }
            else
            {
                ttlodRLstWeeK.InnerText = "00"; ;
            }

            //============

            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[2];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlRvnuE.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_hostel"].ToString();
                }
                else
                {
                    ttlRvnuE.InnerText = "00";
                }
            }
            else
            {
                ttlRvnuE.InnerText = "00"; ;
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[3];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlRevenueLstWeeK.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_hostel_lst_week"].ToString() + "+ from last week";
                }
                else
                {
                    ttlRevenueLstWeeK.InnerText = "00";
                }
            }
            else
            {
                ttlRevenueLstWeeK.InnerText = "00"; ;
            }


            //============

            if (ds.Tables[4].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[4];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlCancelAmt.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_days"].ToString();
                }
                else
                {
                    ttlCancelAmt.InnerText = "00";
                }
            }
            else
            {
                ttlCancelAmt.InnerText = "00"; ;
            }


            if (ds.Tables[5].Rows.Count > 0)
            {
                DataTable dtTemp = ds.Tables[5];
                if (dtTemp.Rows.Count != 0)
                {
                    ttlCancelAmtLstWeek.InnerText = dtTemp.Rows[0]["ttl_admisiion_in_days_lst_week"].ToString() + "+ from last week";
                }
                else
                {
                    ttlCancelAmtLstWeek.InnerText = "00";
                }
            }
            else
            {
                ttlCancelAmtLstWeek.InnerText = "00"; ;
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
        #endregion

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_excels.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();

                btn_excels.Visible = true;
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                hd_id.Value = lbl_Id.Text;

                Response.Redirect("student-registration.aspx?stdid=" + lbl_Id.Text + "&admno=" + lbl_admissionserialnumber.Text, false);
            }
            catch
            {

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
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name from Online_Admission where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Steps_done='10' and Is_transfer='1' and  Payment_Status='Paid' order by id desc");

            lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
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
                    mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
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
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name from Online_Admission where Steps_done='10' and Is_transfer='1' and  Session_id='" + ddlsession.SelectedValue + "' and  Payment_Status='Paid'  order by id desc");
            lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text;
        }

        protected void btn_fnd_by_days_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_days_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please select admission in.", "warning");
                    ddl_days_hostel.Focus();
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
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name from Online_Admission where Steps_done='10' and Is_transfer='1' and  Services='" + ddl_days_hostel.SelectedValue + "' and  Session_id='" + ddlsession.SelectedValue + "' and  Payment_Status='Paid' order by id desc");

            lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Services :" + ddl_days_hostel.SelectedItem.Text;


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