using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Book_Taken_History : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (Request.QueryString["regid"] != null)
                {
                    if (!IsPostBack)
                    {
                        ViewState["admissionno"] = Request.QueryString["regid"];
                        Dictionary<string, object> dc1 = My.get_selected_studentinfo(ViewState["admissionno"].ToString(),My.get_session_id(),"1");

                        


                        ViewState["rollnumber"] = (String)dc1["rollnumber"];
                        ViewState["Class_id"] = (String)dc1["Class_id"];
                        ViewState["session"] = (String)dc1["session"];
                        
                       

                        Bind_book();
                    }
                }
            }
            catch
            {

            }
        }

        private void Bind_book()
        {
            string query = " select ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Transfer_Status in('NT','New') join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1 and lst.student_id='" + ViewState["admissionno"].ToString() + "' and lst.Session_id='" + My.get_session_id() + "' order by ar.rollnumber,lst.issue_date";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                Alertme("Sorry there are no data list exist");
                GrdViewdata.DataSource = null;
                GrdViewdata.DataBind();
            }
            else
            {


                GrdViewdata.DataSource = dt;
                GrdViewdata.DataBind();
            }
        }

        private void Alertme(string Message)
        {
            lbl_msg.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void GrdViewdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Book_status = (Label)e.Row.FindControl("lbl_Book_status");
                if (lbl_Book_status.Text == "")
                {
                    lbl_Book_status.Text = "Book Issued";
                }
                else
                {

                }
            }
        }
    }
}