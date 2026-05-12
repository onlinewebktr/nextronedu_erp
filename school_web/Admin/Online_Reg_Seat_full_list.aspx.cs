using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Online_Reg_Seat_full_list : System.Web.UI.Page
    {
        string testname = "Select top 1 Test_name from Online_reg_exam_test_master where Test_id=Online_Admission.Test_id";
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
                        string a = "";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["flag"] = "0";
                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();

                        bind_session();
                        bind_class();

                        //bind_all_data();
                        find_by_c_s_a();

                        

                        if (Session["msG"] != null)
                        {
                            Alertme(Session["msG"].ToString(), "success");
                            Session["msG"] = null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }
        protected void btn_btn_find_date_Click(object sender, EventArgs e)
        {
            bind_all_data();
        }
        private void bind_all_data()
        {
            if (ddl_test_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select test name", "warning");
                ddl_test_name.Focus();
            }
            else if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {

                string sdate = txt_s_date.Text;
                string sday = sdate.Substring(0, 2);
                string smonth = sdate.Substring(3, 2);
                string syear = sdate.Substring(6, 4);

                string edate = txt_e_date.Text;
                string eday = edate.Substring(0, 2);
                string emonth = edate.Substring(3, 2);
                string eyear = edate.Substring(6, 4);

                int idate = Convert.ToInt32(syear + smonth + sday);
                int idate2 = Convert.ToInt32(eyear + emonth + eday);

                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where  (Is_transfer is null or Is_transfer='') and Payment_Status='Unpaid' and  idate>='" + idate + "' and idate<='" + idate2 + "' and Test_id=" + ddl_test_name.SelectedValue + " and Payment_remarks='SeatFull'  order by idate asc");
                }
            }



        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
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
                else if (ddl_test_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select test name", "warning");
                    ddl_test_name.Focus();
                }
                else
                {
                    find_by_class();
                    ViewState["flag"] = "3";
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void find_by_class()
        {
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where   (Is_transfer is null or Is_transfer='') and  Class_id='" + ddlclass.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " and Payment_Status='Unpaid' and Session_id=" + ddlsession.SelectedValue + " and Payment_remarks='SeatFull' order by id desc");
        }


        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id from session_details where Old_Use_Mode='2'   order by ID asc");

            ddlsession.SelectedValue = My.get_session_id_onlinereg();


            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddlsession.SelectedValue + "' order by  Test_name asc");
            ddl_test_name.SelectedValue = My.get_top_one_test_name(ddlsession.SelectedValue);

        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
            ddlclass.SelectedValue = My.get_top_one_class();
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
                else if (ddl_test_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select test name", "warning");
                    ddl_test_name.Focus();
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
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + "  and (Is_transfer is null or Is_transfer='') and Payment_Status='Unpaid' and Payment_remarks='SeatFull' order by id desc");
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
            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddlsession.SelectedValue + "' order by  Test_name asc");

            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where   (Is_transfer is null or Is_transfer='')  and  Session_id='" + ddlsession.SelectedValue + "' and Payment_Status='Unpaid'  and Payment_remarks='SeatFull' order by id desc");
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
            bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name ,(" + testname + ") as Test_name from Online_Admission where   (Is_transfer is null or Is_transfer='')  and  Services='" + ddl_days_hostel.SelectedValue + "' and Payment_Status='Unpaid' and Payment_remarks='SeatFull' order by id desc");
        }

        protected void ddl_test_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_test_name.SelectedValue == "")
            {
                Alertme("Please select admission in.", "warning");
            }
            else
            {

                bind_grd_view("select *,(select top 1 Session from session_details where session_id=Online_Admission.Session_id) as Session_name,(" + testname + ") as Test_name from Online_Admission where   (Is_transfer is null or Is_transfer='')  and  Session_id='" + ddlsession.SelectedValue + "' and Test_id=" + ddl_test_name.SelectedValue + " and Payment_Status='Unpaid' and Payment_remarks='SeatFull'  order by id desc");
            }

        }
    }
}