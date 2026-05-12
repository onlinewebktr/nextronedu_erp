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
    public partial class ptm_list : System.Web.UI.Page
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

                        txt_from_date.Text = "01/" + mycode.monthYear();
                        txt_to_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_teacher, "select name,user_id from user_details where User_Type='Teacher' order by name asc");

                        get_ptm_list();
                        ViewState["flag"] = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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

        My mycode = new My();
        private void get_ptm_list()
        {
            string query = "";
            if (ddl_teacher.SelectedItem.Text == "ALL")
            {
                query = "select *,(select top 1 name from user_details where user_id=Zoom_Virtual_class_schedule.Teacher_Id) as Teacher_name,(select top 1 Course_Name from Add_course_table where course_id=Zoom_Virtual_class_schedule.Class) as Class_name,format(CreatedOn, 'dd/MM/yyyy hh:mm tt') as CreatedOn_date,format(Approved_On, 'dd/MM/yyyy hh:mm tt') as Approved_On_date from Zoom_Virtual_class_schedule where Metting_Type='PTM' and format(Meeting_start_at, 'yyyyMMdd')>=" + My.DateConvertToIdate(txt_from_date.Text) + " and format(Meeting_start_at, 'yyyyMMdd')<=" + My.DateConvertToIdate(txt_to_date.Text) + "";
            }
            else
            {
                query = "select *,(select top 1 name from user_details where user_id=Zoom_Virtual_class_schedule.Teacher_Id) as Teacher_name,(select top 1 Course_Name from Add_course_table where course_id=Zoom_Virtual_class_schedule.Class) as Class_name,format(CreatedOn, 'dd/MM/yyyy hh:mm tt') as CreatedOn_date,format(Approved_On, 'dd/MM/yyyy hh:mm tt') as Approved_On_date from Zoom_Virtual_class_schedule where Metting_Type='PTM' and format(Meeting_start_at, 'yyyyMMdd')>=" + My.DateConvertToIdate(txt_from_date.Text) + " and format(Meeting_start_at, 'yyyyMMdd')<=" + My.DateConvertToIdate(txt_to_date.Text) + " and Teacher_Id='" + ddl_teacher.SelectedValue + "'";
            }
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


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_from_date.Text == "")
                {
                    Alertme("Please select from date.", "warning");
                    txt_from_date.Focus();
                }
                else if (txt_to_date.Text == "")
                {
                    Alertme("Please select to date.", "warning");
                    txt_to_date.Focus();
                }
                else
                {
                    get_ptm_list();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}