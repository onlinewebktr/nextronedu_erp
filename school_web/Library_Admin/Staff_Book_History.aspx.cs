using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using school_web.AppCode;

namespace school_web.Library_Admin
{
    public partial class Staff_Book_History : System.Web.UI.Page
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
                        Session["reprintstudent_returnbook"] = "2";
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
                        find_firm_details();
                        Txt_startdate.Text = mycode.date();
                        txt_end_date.Text = mycode.date();
                        try
                        {
                            if (Request.QueryString["page"] != null)
                            {


                                ViewState["type"] = Request.QueryString["type"].ToString();
                                if (Request.QueryString["page"].ToString() == "today")
                                {
                                    Txt_startdate.Text = mycode.date();
                                    txt_end_date.Text = mycode.date();

                                    lbl_title.Text = "Today Staff Book " + Request.QueryString["type"] + " List";
                                    date_wise_data_find();
                                }
                                else if(Request.QueryString["page"].ToString() == "today")
                                {
                                    Txt_startdate.Text = mycode.date();
                                    txt_end_date.Text = mycode.date();
                                    lbl_title.Text = "Today Staff Book " + Request.QueryString["type"] + " Pending Book for Return List";
                                    date_wise_data_find();
                                }
                                else
                                {
                                    Txt_startdate.Text = "";
                                    txt_end_date.Text = "";
                                    lbl_class22.Text = "";
                                    lbl_heading_print.Text = "Staff Book " + Request.QueryString["type"] + " List";
                                    bind_all_data();
                                }
                            }
                            else
                            {
                                ViewState["type"] = "N/A";
                                lbl_heading_print.Text = "All Staff Issued Book List";
                                bind_all_data();

                            }
                        }
                        catch
                        {
                            ViewState["type"] = "N/A";
                            lbl_heading_print.Text = "All Staff Issued Book List";
                            bind_all_data();
                        }





                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Staff_Book_History");
            }

        }


        private void bind_all_data()
        {
            string query = "";
            string session_id = My.get_session_id();

            if (ViewState["type"].ToString() == "issued")
            {
                query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.status='Active'  and  lst.Session_id=" + session_id + "  and  lst.status='Issued'   order by lst.issue_date";
            }
            else if (ViewState["type"].ToString() == "return")
            {
                query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.status='Active'  and  lst.Session_id=" + session_id + " and  lst.status='Return'   order by lst.issue_date";
            }
            else if (ViewState["type"].ToString() == "todayreturn")
            {
                query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.status='Active'  and  lst.Session_id=" + session_id + "  and  lst.status='Issued' and lst.Due_idate<="+mycode.idate()+"   order by lst.issue_date";
            }
            
            else
            {
                query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.status='Active'  and  lst.Session_id=" + session_id + "    order by lst.issue_date";
            }


            bind_grd_view(query);


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
                print1.Visible = false;
                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
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
            }
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private void date_wise_data_find()
        {
            lbl_class22.Text = "";
            if (Txt_startdate.Text == "")
            {
                Alertme("Please choose from date", "warning");
                Txt_startdate.Focus();
            }
            else if (txt_end_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_end_date.Focus();
            }
            else
            {
                string query = "";
                string sdate = Txt_startdate.Text;
                string sday = sdate.Substring(0, 2);
                string smonth = sdate.Substring(3, 2);
                string syear = sdate.Substring(6, 4);

                string edate = txt_end_date.Text;
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
                    lbl_class22.Text = "Date Period Start Date :" + Txt_startdate.Text + " End Date :" + txt_end_date.Text;
                    if (ViewState["type"].ToString() == "issued")
                    {
                        query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status='Active' and    lst.issue_idate>=" + idate + " and lst.issue_idate<=" + idate2 + " and  lst.status='Issued'  order by  lst.issue_date";
                    }
                    else if (ViewState["type"].ToString() == "return")
                    {
                        query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status='Active' and    lst.return_idate>=" + idate + " and lst.return_idate<=" + idate2 + " and  lst.status='Return'  order by  lst.issue_date";
                    }
                    else if (ViewState["type"].ToString() == "todayreturn")
                    {
                        query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.status='Active'  and  lst.status='Issued' and lst.Due_idate=" + mycode.idate() + "   order by lst.issue_date";
                    }

                    else
                    {
                        query = "select  ar.user_id,ar.name,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,lst.Book_status,lst.Book_reurn_slip_id,lst.returned_date from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status='Active' and    lst.issue_idate>=" + idate + " and lst.issue_idate<=" + idate2 + "   order by  lst.issue_date";

                    }
                    bind_grd_view(query);
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                date_wise_data_find();
            }
            catch
            {

            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                if (ViewState["Is_Print"].ToString() == "1")
                {

                    ((Panel)e.Item.FindControl("pnl_print_issuebook_slip_id")).Visible = true;

                    string Book_status = ((Label)e.Item.FindControl("lbl_Book_status")).Text;

                    if (Book_status == "Return Book")
                    {
                        ((Panel)e.Item.FindControl("pnl_print_returnbook_slip_id")).Visible = true;
                    }
                    else if (Book_status == "Return Damage Book")
                    {
                        ((Panel)e.Item.FindControl("pnl_print_returnbook_slip_id")).Visible = true;
                    }
                    else
                    {
                        ((Panel)e.Item.FindControl("pnl_print_returnbook_slip_id")).Visible = false;
                    }



                }
                else
                {

                    ((Panel)e.Item.FindControl("pnl_print_issuebook_slip_id")).Visible = false;

                    ((Panel)e.Item.FindControl("pnl_print_returnbook_slip_id")).Visible = false;

                }
            }
        }
    }
}