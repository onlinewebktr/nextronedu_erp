using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class Book_TakenHistory : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        Get_data_taken();
                        ViewState["flag"] = "0";
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Get_data_taken()
        {
            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)))
            {
                string query = " select ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Transfer_Status in('NT','New') join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1 and lst.student_id='" + ViewState["regid"].ToString() + "' and lst.Session_id='" + My.get_session_id() + "' and issue_idate>=" + My.DateConvertToIdate(txt_from_date.Text) + " and issue_idate<=" + My.DateConvertToIdate(txt_to_date.Text) + "  order by ar.rollnumber,lst.issue_date";

                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {

                    Alertme("Sorry, there is no data list available", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {


                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            else
            {
                Alertme("Please select valid date ", "warning");
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
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)))
                {
                    Get_data_taken();

                }
                else
                {
                    Alertme("Please select valid date", "warning");
                }
            }
            catch
            {

            }
        }
    }
}