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
    public partial class monthly_fee_dues_report : System.Web.UI.Page
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
                        bind_session();
                        bind_class();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table");
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
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddlclass.Focus();
                }

                else
                {
                    ViewState["flag"] = "0";
                    find_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_data()
        {
            bind_grd_view("select * from (select studentname,admissionserialnumber,class,Section,rollnumber,session,(isnull((select sum(cast(dues as float)) from dbo.[Typewise_fee_collection] where parameter='AnnualFee' and  admission_no=admission_registor.admissionserialnumber and session=admission_registor.session),(select sum(cast(amount as float)) from dbo.[Fee_master_content_wise] where parameter='AnnualFee' and session_id=admission_registor.Session_id and class_id=admission_registor.Class_id))) dues from dbo.[admission_registor] where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status='New') t where dues>0");
        }


        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_fnl_duesS.Text = "00";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                string qryS = "";
                if (ViewState["flag"].ToString() == "0")
                {
                    qryS = "select * from (select studentname,admissionserialnumber,class,Section,rollnumber,session,(isnull((select sum(cast(dues as float)) from dbo.[Typewise_fee_collection] where parameter='AnnualFee' and  admission_no=admission_registor.admissionserialnumber and session=admission_registor.session),(select sum(cast(amount as float)) from dbo.[Fee_master_content_wise] where parameter='AnnualFee' and session_id=admission_registor.Session_id and class_id=admission_registor.Class_id))) dues from dbo.[admission_registor] where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status='New') t where dues>0";
                }
                if (ViewState["flag"].ToString() == "1")
                {
                    qryS = "select * from (select studentname,admissionserialnumber,class,Section,rollnumber,session,(isnull((select sum(cast(dues as float)) from dbo.[Typewise_fee_collection] where parameter='AnnualFee' and  admission_no=admission_registor.admissionserialnumber and session=admission_registor.session),(select sum(cast(amount as float)) from dbo.[Fee_master_content_wise] where parameter='AnnualFee' and session_id=admission_registor.Session_id and class_id=admission_registor.Class_id))) dues from dbo.[admission_registor] where studentname='" + txt_student_name.Text + "' and Transfer_Status='New') t where dues>0";
                }
                DataTable dtS = mycode.FillData(qryS);
                if (dtS.Rows.Count == 0)
                {
                    lbl_fnl_duesS.Text = "00";
                }
                else
                {
                    String Fnl_dues_amt = Convert.ToDouble(dtS.Compute("SUM(dues)", string.Empty)).ToString("0.00");
                    lbl_fnl_duesS.Text = Fnl_dues_amt;
                }
            }
        }

        protected void btn_fnd_student_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_student_name.Text == "")
                {
                    txt_student_name.Focus();
                    Alertme("Please enter student name.", "warning");
                }
                else
                {
                    ViewState["flag"] = "1";
                    find_data_by_student();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_data_by_student()
        {
            bind_grd_view("select * from (select studentname,admissionserialnumber,class,Section,rollnumber,session,(isnull((select sum(cast(dues as float)) from dbo.[Typewise_fee_collection] where parameter='AnnualFee' and  admission_no=admission_registor.admissionserialnumber and session=admission_registor.session),(select sum(cast(amount as float)) from dbo.[Fee_master_content_wise] where parameter='AnnualFee' and session_id=admission_registor.Session_id and class_id=admission_registor.Class_id))) dues from dbo.[admission_registor] where studentname='" + txt_student_name.Text + "' and Transfer_Status='New') t where dues>0");
        }
         
    }
}