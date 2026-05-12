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

namespace school_web.Library_Admin
{
    public partial class Over_Due_List_For_Student : System.Web.UI.Page
    {
        My mycode = new My();
        string student_fine = "Select top 1 fine_for_stuent from lib_fine_details";


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
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["flag"] = "0";

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = Library.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["startidate"] = mycode.idate();
                        find_firm_details();

                        try
                        {
                            if (Request.QueryString["page"] != null)
                            {


                                lbl_heading_print.Text = "Over due list for student";


                                if (Request.QueryString["type"].ToString() == "Today")
                                {
                                    ddl_select_due_list.Text = "Today";
                                    lbl_class22.Text = "Today";
                                    lbl_title.Text = "Today Over due list for student";
                                    date_wise_find();
                                }
                                else if (Request.QueryString["type"].ToString() == "Last3Days")
                                {
                                    ddl_select_due_list.Text = "Last 3 Days";
                                    lbl_class22.Text = "Last 3 days";
                                    lbl_title.Text = "Last 3 days Over due list for student";
                                    date_wise_find();
                                }
                                else if (Request.QueryString["type"].ToString() == "Last7Days")
                                {
                                    ddl_select_due_list.Text = "Last 7 Days";
                                    lbl_class22.Text = "Last 7 days";
                                    lbl_title.Text = "Last 7 days Over due list for student";

                                    date_wise_find();
                                }
                                else if (Request.QueryString["type"].ToString() == "Last15Days")
                                {
                                    ddl_select_due_list.Text = "Last 15 Days";
                                    lbl_class22.Text = "Last 15 days";
                                    lbl_title.Text = "Last 15 days Over due list for student";
                                    date_wise_find();
                                }
                                else if (Request.QueryString["type"].ToString() == "Last30Days")
                                {
                                    ddl_select_due_list.Text = "Last 1 Month";
                                    lbl_class22.Text = "Last 30 days";
                                    lbl_title.Text = "Last 30 days Over due list for student";
                                    date_wise_find();
                                }
                                else
                                {
                                    lbl_title.Text = "Over due list for student";
                                    date_wise_find();
                                }

                            }
                            else
                            {
                                lbl_heading_print.Text = "Over due list for student";
                                date_wise_find();
                            }
                        }
                        catch
                        {
                            lbl_heading_print.Text = "Over due list for student";
                            date_wise_find();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fine_Collection_For_Student");
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
                        Panel2.RenderControl(hw);
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details   ");
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

        Library ly = new Library();
        private void date_wise_find()
        {
            string query = "";
            int back_idate = 0;
            string session_id = ddl_session.SelectedValue;
            string ThreeDaysDate;





            DateTime ThreestartTime = DateTime.ParseExact(mycode.date(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ThreeDaysDate = ThreestartTime.AddDays(-3).ToShortDateString();

            DateTime SevenstartTime = DateTime.ParseExact(ThreeDaysDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string SevenDaysDate = SevenstartTime.AddDays(-7).ToShortDateString();

            DateTime fifteendaysTime = DateTime.ParseExact(SevenDaysDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string fifteendayback = fifteendaysTime.AddDays(-15).ToShortDateString();


            DateTime thirtystartTime = DateTime.ParseExact(fifteendayback, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string thirtydayback = ThreestartTime.AddDays(-30).ToShortDateString();
            if (ddl_select_due_list.Text == "Today")
            {
                query = "select (" + student_fine + ") as fine ,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date,ar.gcm_id from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1  and  lst.Session_id=" + session_id + " and  lst.status='Issued' and lst.Due_idate>=" + mycode.idate() + " and lst.Due_idate<=" + mycode.idate() + " order by ar.rollnumber,lst.issue_date";
                bind_grd_view(query);
            }


            else if (ddl_select_due_list.Text == "Last 3 Days")
            {

                ViewState["startdate"] = ly.Return_back_date_new(mycode.date(), 3);

                back_idate = mycode.ConvertStringToiDate(ViewState["startdate"].ToString());

                query = "select (" + student_fine + ") as fine ,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date,ar.gcm_id from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1  and  lst.Session_id=" + session_id + " and  lst.status='Issued' and lst.Due_idate>=" + back_idate + " and lst.Due_idate<=" + mycode.idate() + " order by ar.rollnumber,lst.issue_date";
                bind_grd_view(query);
            }
            else if (ddl_select_due_list.Text == "Last 7 Days")
            {


                ViewState["startdate"] = ly.Return_back_date_new(ThreeDaysDate, 7);

                back_idate = mycode.ConvertStringToiDate(ViewState["startdate"].ToString());

                int ThreeDaysiDate = mycode.ConvertStringToiDate(ThreeDaysDate);

                query = "select  (" + student_fine + ") as fine ,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date,ar.gcm_id from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1  and  lst.Session_id=" + session_id + " and  lst.status='Issued' and lst.Due_idate>=" + back_idate + " and lst.Due_idate<=" + ThreeDaysiDate + " order by ar.rollnumber,lst.issue_date";
                bind_grd_view(query);
            }
            else if (ddl_select_due_list.Text == "Last 15 Days")
            {

                ViewState["startdate"] = ly.Return_back_date_new(SevenDaysDate, 15);
                int Sevenidateback = mycode.ConvertStringToiDate(SevenDaysDate);

                back_idate = mycode.ConvertStringToiDate(ViewState["startdate"].ToString());


                query = "select  (" + student_fine + ") as fine ,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date,ar.gcm_id from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1  and  lst.Session_id=" + session_id + " and  lst.status='Issued' and lst.Due_idate>=" + back_idate + " and lst.Due_idate<=" + Sevenidateback + " order by ar.rollnumber,lst.issue_date";
                bind_grd_view(query);
            }
            else if (ddl_select_due_list.Text == "Last 1 Month")
            {



                ViewState["startdate"] = ly.Return_back_date_new(fifteendayback, 30);

                int fifteenidateback = mycode.ConvertStringToiDate(fifteendayback);

                back_idate = mycode.ConvertStringToiDate(ViewState["startdate"].ToString());


                query = "select  (" + student_fine + ") as fine ,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date,ar.gcm_id from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1  and  lst.Session_id=" + session_id + " and  lst.status='Issued' and lst.Due_idate>=" + back_idate + " and lst.Due_idate<=" + fifteenidateback + " order by ar.rollnumber,lst.issue_date";
                bind_grd_view(query);
            }
            else
            {
                ViewState["startdate"] = ly.Return_back_date_new(mycode.date(), 30);

                int thirtyidateback = mycode.ConvertStringToiDate(fifteendayback);


                back_idate = mycode.ConvertStringToiDate(ViewState["startdate"].ToString());



                query = "select  (" + student_fine + ") as fine ,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date,ar.gcm_id from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1  and  lst.Session_id=" + session_id + " and  lst.status='Issued' and  lst.Due_idate<=" + mycode.idate() + " order by ar.rollnumber,lst.issue_date";
                bind_grd_view(query);
            }




        }

        private void bind_grd_view(string query)
        {
            btn_send_reminder.Visible = false;
            lbl_totalbook.Text = "0.00";
            double total = 0.00;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_send_reminder.Visible = true;
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

                rd_view.DataSource = dt;
                rd_view.DataBind();

                //int totalcount = rd_view.Items.Count;
                //for (int i = 0; i < totalcount; i++)
                //{
                //    total = total + My.toDouble(rd_view.Items[i].FindControl("lbl_Total_Fine"));
                //}
                int i = 0;
                foreach (RepeaterItem row in rd_view.Items)
                {
                    Label lbl_Total_Fine = rd_view.Items[i].FindControl("lbl_Total_Fine") as Label;
                    total = total + My.toDouble(lbl_Total_Fine.Text);
                    i++;
                }
                lbl_totalbook.Text = total.ToString("0.00");
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string duedate = ((Label)e.Item.FindControl("lbl_book_due_date")).Text;
                string fine_amount = ((Label)e.Item.FindControl("lbl_fine")).Text;


                // string duedate = ViewState["startdate"].ToString();

                string extraday = ly.noof_day_return(duedate, mycode.date());

                ((Label)e.Item.FindControl("lbl_Extra_days")).Text = extraday;

                double getextra_day_fine_amount = My.toDouble(extraday) * My.toDouble(fine_amount);
                ((Label)e.Item.FindControl("lbl_Total_Fine")).Text = getextra_day_fine_amount.ToString("0.00");

            }
        }

        protected void ddl_select_due_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                date_wise_find();
            }

        }

        protected void btn_send_reminder_Click(object sender, EventArgs e)
        {
            try
            {

                int i = 0;
                int rdview = rd_view.Items.Count;
                foreach (RepeaterItem row in rd_view.Items)
                {
                    CheckBox chk = rd_view.Items[i].FindControl("rowChkBox") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        Label lbladmissionserialnumber = rd_view.Items[i].FindControl("lbladmissionserialnumber") as Label;
                        Label lbl_studentname = rd_view.Items[i].FindControl("lbl_studentname") as Label;

                        Label lbl_book_no = rd_view.Items[i].FindControl("lbl_book_no") as Label;

                        Label lbl_NameOfBook = rd_view.Items[i].FindControl("lbl_NameOfBook") as Label;

                        Label lbl_transaction_no = rd_view.Items[i].FindControl("lbl_transaction_no") as Label;
                        Label lbl_book_due_date = rd_view.Items[i].FindControl("lbl_book_due_date") as Label;
                        Label lbl_gcm_id = rd_view.Items[i].FindControl("lbl_gcm_id") as Label;

                        if (lbl_gcm_id.Text != "")
                        {
                            Dictionary<String, String> ss = new Dictionary<string, string>();
                            ss["notification_id"] = Guid.NewGuid().ToString();
                            ss["message"] = "Dear " + lbl_studentname.Text + " , your book issued no.: " + lbl_transaction_no.Text + ", Book No.:  " + lbl_book_no.Text + " your due date is: " + lbl_book_due_date.Text + " Please submit book soon as possible";
                            ss["title"] = "Book Issued Reminder";
                            ss["messagetype"] = "BookIssuedReminder";
                            ss["url"] = "";
                            ss["link_url"] = "";
                            ss["UserId"] = lbladmissionserialnumber.Text;
                            UsesCode.SendNotification(lbl_gcm_id.Text, ss);
                        }
                    }
                    else
                    {
                        i++;
                    }

                }
                if (i == rdview)
                {
                    Alertme("Please select at least one student from list", "warning");
                }
                else
                {
                    Alertme("Book return reminder message has been sent successfully", "success");

                }
            }
            catch
            {

            }
        }
    }
}