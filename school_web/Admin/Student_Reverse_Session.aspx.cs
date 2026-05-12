using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;

namespace school_web.Admin
{
    public partial class Student_Reverse_Session : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["flagPosition"] = "1";
                        ViewState["flag"] = "0";
                        ViewState["branch_id"] = "1";
                        mycode.bind_all_ddl_with_id(ddl_session, " Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc ");
                        mycode.bind_all_ddl_with_id(ddl_session_adm, "Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_course, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor order by Section ");
                        string pagename_current = Path.GetFileName(Request.Path);
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
                My.submitException(ex, "Student_Reverse_Session");
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



        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            find_data();
            ViewState["flag"] = "1";
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                }
                else if (ddl_course.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                }
                else
                {
                    find_data();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void find_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session ", "warning");
            }
            else if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select course name", "warning");
            }

            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else
            {

                string cunrt_session = ddl_session.SelectedItem.Text;
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int nextsession_first = My.toint(session_frst_year) + 1;
                int nextsession_last = My.toint(session_last_year) + 1;

                string sessionnext = nextsession_first.ToString() + "-" + nextsession_last.ToString();

                string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + "   and (Transfer_Status='Transferred')  and admissionserialnumber not in (Select Addmission_no from Student_Payment_History where Session='"+ sessionnext + "' )   order by rollnumber asc";
                bind_grids(query);
            }
        }
       

    
        private void bind_grids(string query)
        {
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {
                btn_Submit.Visible = false;
                Alertme("Sorry there are no data exist", "warning");
                grd_studentdetails.DataSource = null;
                grd_studentdetails.DataBind();
            }
            else
            {
                btn_Submit.Visible = true;
                grd_studentdetails.DataSource = dt1;
                grd_studentdetails.DataBind();
            }
        }
        #region row databound
        protected void grd_studentdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_transferwithtrasport = (CheckBox)e.Row.FindControl("chk_transferwithtrasport");
                CheckBox chk_transfer_with_hostel = (CheckBox)e.Row.FindControl("chk_transfer_with_hostel");
                Label lbl_is_trasport = (Label)e.Row.FindControl("lbl_is_trasport");
                Label lbl_is_hostel = (Label)e.Row.FindControl("lbl_is_hostel");

                if (lbl_is_trasport.Text.ToUpper() == "YES")
                {
                    chk_transferwithtrasport.Checked = true;
                    chk_transfer_with_hostel.Enabled = true;

                    chk_transfer_with_hostel.Checked = false;
                    chk_transfer_with_hostel.Enabled = false;
                }
                else
                {
                    chk_transferwithtrasport.Checked = false;
                    chk_transferwithtrasport.Enabled = false;
                    chk_transfer_with_hostel.Checked = false;
                    chk_transfer_with_hostel.Enabled = false;
                    if (lbl_is_hostel.Text.ToUpper() == "YES")
                    {
                        chk_transfer_with_hostel.Checked = true;
                        chk_transfer_with_hostel.Enabled = true;
                    }

                }



            }
        }
        #endregion
        protected void btn_find_by_adm_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_adm.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                }

                else if (txt_Adm_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                }
                else
                {
                    find_by_admission();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission()
        {
            string cunrt_session = ddl_session_adm.SelectedItem.Text;
            string[] stringSeparators = new string[] { "-" };
            string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
            string session_frst_year = arr[0];
            string session_last_year = arr[1];
            int nextsession_first = My.toint(session_frst_year) + 1;
            int nextsession_last = My.toint(session_last_year) + 1;

            string sessionnext = nextsession_first.ToString() + "-" + nextsession_last.ToString();
            string query = "Select * from admission_registor where session='" + ddl_session_adm.SelectedItem.Text + "' and admissionserialnumber='" + txt_Adm_no.Text + "'   and (Transfer_Status='Transferred') and admissionserialnumber not in (Select Addmission_no from Student_Payment_History where Session='" + sessionnext + "' )  order by rollnumber asc ";
            bind_grids(query);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                int grdcount = grd_studentdetails.Rows.Count;
                int j = 0;
                for (int i = 0; i < grdcount; i++)
                {

                    Label lbl_admission_no = (Label)grd_studentdetails.Rows[i].FindControl("lbl_admission_no");
                    Label lbl_status = (Label)grd_studentdetails.Rows[i].FindControl("lbl_status");
                    Label lbl_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_id");
                    Label lbl_is_trasport = (Label)grd_studentdetails.Rows[i].FindControl("lbl_is_trasport");
                    Label lbl_is_hostel = (Label)grd_studentdetails.Rows[i].FindControl("lbl_is_hostel");
                    Label lbl_Transfer_Status = (Label)grd_studentdetails.Rows[i].FindControl("lbl_Transfer_Status");
                    Label lbl_session = (Label)grd_studentdetails.Rows[i].FindControl("lbl_session");
                    Label lbl_Session_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_Session_id");

                    CheckBox check = (CheckBox)grd_studentdetails.Rows[i].FindControl("rowChkBox");
                    if (check.Checked == true)
                    {
                        submit_lblmessage(lbl_admission_no.Text, lbl_status.Text, lbl_id.Text, lbl_is_trasport.Text, lbl_is_hostel.Text, lbl_Transfer_Status.Text, lbl_session.Text, lbl_Session_id.Text);
                    }
                    else
                    {
                        j++;
                    }

                }
                if (j == grdcount)
                {
                    Alertme("Please check any one checkbox of admission list list", "warning");
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

                }
                else
                {
                    Alertme("Student has been sucessfully back", "success");
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    find_data();

                }
            }
            catch
            {


              
            }
        }

        private void submit_lblmessage(string admission_no, string status, string id, string is_trasport, string is_hostel, string Transfer_Status, string session, string Session_id)
        {
            try
            {
                string cunrt_session = session;
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];
                int nextsession_first = My.toint(session_frst_year) + 1;
                int nextsession_last = My.toint(session_last_year) + 1;
                string sessionnext = nextsession_first.ToString() + "-" + nextsession_last.ToString();
                string sessionnext_id = My.get_sess_prm(sessionnext);
                mycode.executequery("update admission_registor set Transfer_Status=Transfer_Status_Old,Reverse_by='" + ViewState["Userid"].ToString() + "',Reverse_Date='" + mycode.date() + "',Reverse_time='" + mycode.time() + "' where Session_id='" + Session_id + "' and admissionserialnumber='" + admission_no + "' and id='"+ id + "'");
                mycode.executequery("delete from admission_registor where Session_id='" + sessionnext_id + "' and admissionserialnumber='" + admission_no + "'");
            }
            catch
            {

            }
            
        }
    }
}