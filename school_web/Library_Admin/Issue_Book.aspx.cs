using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
using System.Web.Services;

namespace school_web.Library_Admin
{
    public partial class Issue_Book : System.Web.UI.Page
    {
        My mycode = new My();
        Library lb = new Library();
        string booktype = "Select top 1 TypeName from Library_Type where TypeId=lbe.Type and Branch_Id=lbe.Branch_id";
        string BookStatus = "Select top 1 BookStatus from Library_Book_Status where BookStatusId=lbe.BookStatus  ";
        string Book_Category = "Select top 1 Book_Category from Library_Book_Category where Book_Category_Id=lbe.Book_Category_Id and Branch_Id=lbe.Branch_id";

        string location = "Select top 1 location from lib_location_details where Location_id=lbe.Location and Branch_Id=lbe.Branch_id";

        string Sub_Location = "Select top 1 Sub_Location from lib_sub_location_details where Sub_Location_id=lbe.Sub_Location_id and Branch_Id=lbe.Branch_id";
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
                        Session["retunpage"] = "1";
                        Session["reprintstudent_issuebook"] = "1";
                        Txt_date.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["flag"] = "0";
                        ViewState["session_id"] = My.get_session_id();
                        ViewState["session"] = My.get_session();
                        Txt_Admission_No.Text = "";

                        mycode.bind_all_ddl_with_id_cap_NA(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");

                        // bind_all_data();
                        Panel2.Visible = false;
                        Panel3.Visible = false;
                        Panel4.Visible = false;
                        //try
                        //{
                        //    if (Session["msg"] == null)
                        //    {

                        //    }
                        //    else
                        //    {
                        //        Alertme(Session["msg"].ToString(), "success");
                        //        Session["msg"] = null;
                        //    }
                        //}
                        //catch
                        //{ 
                        //}
                    }

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }


        }
        private void bind_all_data()
        {
            bind_grd_view("select * from  Library_Book_Entry where Issued_Status ='Not Issued' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'  order by id desc ");
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }

                else
                {

                    mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "Select distinct lbc.Book_Category,lbc.Book_Category_Id from Library_Book_Category lbc join Library_Book_Entry lbe on  lbe.Book_Category_Id = lbc.Book_Category_Id and lbe.Branch_id = lbc.Branch_id where lbe.SelectClass = '" + ddlclass.SelectedValue + "' and lbe.Branch_id = " + ViewState["Branch_id"].ToString() + "");
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

            bind_grd_view("select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,("+ Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where  lbe.SelectClass='" + ddlclass.SelectedValue + "' and lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Issued_Status='Not Issued' order by lbe.NameOfBook asc");


        }


        protected void btn_item_code_Click(object sender, EventArgs e)
        {
            if(txt_item_code.Text=="")
            {
                Alertme("Please enter item code", "warning");
            }
            else
            {

                bind_grd_view("select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where   lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and BookId='" + txt_item_code.Text + "' and lbe.Issued_Status='Not Issued' order by lbe.NameOfBook asc");
            }
        }
        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            find_by_subject();
        }
        private void find_by_subject()
        {
            if (ddl_subject.SelectedItem.Text == "ALL")
            {
                find_by_class();
            }

            else
            {
                bind_grd_view("select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where  lbe.SelectClass='" + ddlclass.SelectedValue + "' and lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and lbe.Book_Category_Id=" + ddl_subject.SelectedValue + " and lbe.Issued_Status='Not Issued' order by lbe.NameOfBook asc");
            }
        }
        protected void Btn_find_Name_Click(object sender, EventArgs e)
        {
            if (Txt_bookname.Text == "")
            {
                Alertme("Please enter book name then search", "warning");
            }
            else
            {
                bind_grd_view("select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where   lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and NameOfBook='" + Txt_bookname.Text + "' and lbe.Issued_Status='Not Issued' order by lbe.NameOfBook asc");


            }


        }
        protected void Btn_find_Author_Click(object sender, EventArgs e)
        {
            if (Txt_Author.Text == "")
            {
                Alertme("Please enter book auther name then search", "warning");
            }
            else
            {
                bind_grd_view("select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where  lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and   AuthorName='" + Txt_Author.Text + "' and lbe.Issued_Status='Not Issued' order by lbe.NameOfBook asc");
            }


        }
        protected void Btn_find_ISBN_Click(object sender, EventArgs e)
        {
            if (Txt_ISBN.Text == "")
            {
                Alertme("Please enter book ISBN no then search", "warning");
            }
            else
            {
                bind_grd_view("select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where   lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and ISBN_Num='" + Txt_ISBN.Text + "' and lbe.Issued_Status='Not Issued' order by lbe.NameOfBook asc");
            }


        }
        protected void Btn_find_barcode_Click(object sender, EventArgs e)
        {
            if (txt_barcode.Text == "")
            {
                Alertme("Please enter book barcode then search", "warning");
            }
            else
            {
                bind_grd_view("select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where   lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Bar_code='" + txt_barcode.Text + "' and lbe.Issued_Status='Not Issued' order by lbe.NameOfBook asc");
            }
        }
        private void bind_grd_view(string qry)
        {
            txtbox.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            user_and_book_issue_panl.Visible = false;
            book_details.Visible = false;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                user_and_book_issue_panl.Visible = true;
                book_details.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
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









        protected void Btn_Issue_Book_Click(object sender, EventArgs e)
        {
            try
            {
                bool issubmit = false;
                string issuedate = Txt_date.Text;
                int issueIdate = mycode.ConvertStringToiDate(Txt_date.Text);
                string duedate = lbl_due_date.Text;
                int dueIdate = mycode.ConvertStringToiDate(lbl_due_date.Text);
                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (Btn_Issue_Book.Text == "Issue Book")
                    {
                        SqlCommand cmd;
                        if (RadioButtonList1.SelectedValue == "Student")
                        {
                            string createsessionid = "LIB/STU/" + ViewState["session"].ToString() + "/" + cretesessionid1();
                            int i = 0;



                            foreach (RepeaterItem row in rd_view.Items)
                            {
                                CheckBox chk = rd_view.Items[i].FindControl("rowChkBox") as CheckBox;
                                if (chk != null && chk.Checked)
                                {
                                    string barcode = lb.barcode_num("IssueBookbarcode", ViewState["Branch_id"].ToString()); //ViewState["Branch_id"].ToString()
                                    string barcode_image = lb.get_barcode_img_issuebook(barcode, createsessionid, issuedate);

                                    Label lbl_uniq_bookid = rd_view.Items[i].FindControl("lbl_uniq_bookid") as Label;

                                    DataTable dt = mycode.FillData("Select * from lib_student_transaction_details where book_no='" + lbl_uniq_bookid.Text + "' and   status!='Return' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and student_id='" + Hd_admission_no.Value + "' and Session_id='" + ViewState["session_id"].ToString() + "' ");
                                    if (dt.Rows.Count == 0)
                                    {
                                        string query = "Insert into lib_student_transaction_details (transaction_no,book_no,student_id,issue_date,due_date,status,library_card_no,Session_id,Book_issued_by,Bar_code,Barcode_img,issue_idate,Due_idate,Branch_id) values(@transaction_no,@book_no,@student_id,@issue_date,@due_date,@status,@library_card_no,@Session_id,@Book_issued_by,@Bar_code,@Barcode_img,@issue_idate,@Due_idate,@Branch_id)";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@transaction_no", createsessionid);
                                        cmd.Parameters.AddWithValue("@book_no", lbl_uniq_bookid.Text);
                                        cmd.Parameters.AddWithValue("@student_id", Hd_admission_no.Value);
                                        cmd.Parameters.AddWithValue("@issue_date", issuedate);
                                        cmd.Parameters.AddWithValue("@due_date", duedate);
                                        cmd.Parameters.AddWithValue("@status", "Issued");
                                        cmd.Parameters.AddWithValue("@library_card_no", Hd_Library_student.Value);
                                        cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                                        cmd.Parameters.AddWithValue("@Book_issued_by", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@Bar_code", barcode);
                                        cmd.Parameters.AddWithValue("@Barcode_img", barcode_image);
                                        cmd.Parameters.AddWithValue("@issue_idate", issueIdate);
                                        cmd.Parameters.AddWithValue("@Due_idate", dueIdate);
                                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                                        if (My.InsertUpdateData(cmd))
                                        {
                                            mycode.executequery("update Library_Book_Entry set Issued_Status='Issued',Book_issud_To='Student' where Branch_id='" + ViewState["Branch_id"].ToString() + "' and BookId='" + lbl_uniq_bookid.Text + "'");

                                            //  Session["msg"] = "Book has Successfully Issued";
                                            issubmit = true;


                                        }
                                    }
                                }
                                i++;
                            }
                            if (issubmit == true)
                            {
                                try
                                {
                                    string sub = "Book Issued";
                                    string messge = "Dear " + ViewState["name"].ToString() + " you have taken book from library book issued id:- " + createsessionid;


                                    Dictionary<String, String> ss = new Dictionary<string, string>();
                                    ss["notification_id"] = Guid.NewGuid().ToString();
                                    ss["message"] = messge;
                                    ss["title"] = sub;
                                    ss["messagetype"] = "Message";
                                    ss["url"] = "";
                                    ss["link_url"] = "";
                                    ss["UserId"] = Hd_admission_no.Value;
                                    UsesCode.SendNotification(ViewState["gcm_id"].ToString(), ss);
                                }
                                catch
                                {

                                }


                                //Response.Redirect("Print_Bar_Code_Issue_book.aspx?issuebook=" + createsessionid + "&type=Student&code=" + Hd_admission_no.Value, false);


                                Response.Redirect("print/Print_Issue_Book_Invoice_Student.aspx?adm=" + Hd_admission_no.Value + "&Slip_no=" + createsessionid, false);






                            }
                        }

                        else if (RadioButtonList1.SelectedValue == "Non Teaching/Teaching Staff")
                        {
                            string createsessionid = "LIB/STAFF/" + ViewState["session"].ToString() + "/" + cretesessionid2();

                            string barcode = lb.barcode_num("IssueBookbarcode", ViewState["Branch_id"].ToString()); //ViewState["Branch_id"].ToString()
                            string barcode_image = lb.get_barcode_img_issuebook(barcode, createsessionid, issuedate);
                            int i = 0;
                            foreach (RepeaterItem row in rd_view.Items)
                            {
                                CheckBox chk = rd_view.Items[i].FindControl("rowChkBox") as CheckBox;
                                if (chk != null && chk.Checked)
                                {
                                    Label lbl_uniq_bookid = rd_view.Items[i].FindControl("lbl_uniq_bookid") as Label;
                                    DataTable dt = mycode.FillData("Select * from lib_teacher_trans_action_details where book_no='" + lbl_uniq_bookid.Text + "' and   status!='Return' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
                                    if (dt.Rows.Count == 0)
                                    {

                                        string query = "Insert into lib_teacher_trans_action_details (transaction_no,book_no,teacher_id,issue_date,due_date,status,library_card_no,Session_id,Book_issued_by,Bar_code,Barcode_img,issue_idate,due_idate,Branch_id) values(@transaction_no,@book_no,@teacher_id,@issue_date,@due_date,@status,@library_card_no,@Session_id,@Book_issued_by,@Bar_code,@Barcode_img,@issue_idate,@due_idate,@Branch_id)";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@transaction_no", createsessionid);
                                        cmd.Parameters.AddWithValue("@book_no", lbl_uniq_bookid.Text);
                                        cmd.Parameters.AddWithValue("@teacher_id", hd_staff_emp_code.Value);
                                        cmd.Parameters.AddWithValue("@issue_date", issuedate);
                                        cmd.Parameters.AddWithValue("@due_date", duedate);
                                        cmd.Parameters.AddWithValue("@status", "Issued");
                                        cmd.Parameters.AddWithValue("@library_card_no", hd_staff_libcode.Value);
                                        cmd.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                                        cmd.Parameters.AddWithValue("@Book_issued_by", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@Bar_code", barcode);
                                        cmd.Parameters.AddWithValue("@Barcode_img", barcode_image);
                                        cmd.Parameters.AddWithValue("@issue_idate", issueIdate);
                                        cmd.Parameters.AddWithValue("@due_idate", dueIdate);
                                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                                        if (My.InsertUpdateData(cmd))
                                        {
                                            mycode.executequery("update Library_Book_Entry set Issued_Status='Issued',Book_issud_To='Staff' where Branch_id='" + ViewState["Branch_id"].ToString() + "' and BookId='" + lbl_uniq_bookid.Text + "'");
                                            issubmit = true;


                                            //print
                                            //Session["msg"] = "Book has Successfully Issued";
                                            //Response.Redirect("Issue_Book.aspx", false);
                                        }


                                    }
                                }

                                i++;

                            }
                            if (issubmit == true)
                            {
                                string sub = "Book Issued";
                                string messge = "Dear " + ViewState["name"].ToString() + " you have taken book from library book issued id:- " + createsessionid;


                                Dictionary<String, String> ss = new Dictionary<string, string>();
                                ss["notification_id"] = Guid.NewGuid().ToString();
                                ss["message"] = messge;
                                ss["title"] = sub;
                                ss["messagetype"] = "Message";
                                ss["url"] = "";
                                ss["link_url"] = "";
                                ss["UserId"] = hd_staff_emp_code.Value;
                                UsesCode.SendNotification(ViewState["gcm_id"].ToString(), ss);
                                //Response.Redirect("Print_Bar_Code_Issue_book.aspx?issuebook=" + createsessionid + "&type=Staff&code=" + hd_staff_emp_code.Value, false);

                                Response.Redirect("print/Print_Issue_Book_Invoice_staff.aspx?adm=" + hd_staff_emp_code.Value + "&Slip_no=" + createsessionid, false);
                            }
                        }



                        else
                        {
                            Alertme("Button Not Clicked", "warning");

                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "issue book");
            }
        }
        protected void Txt_Days_TextChanged(object sender, EventArgs e)
        {
            if (Txt_date.Text == "")
            {
                lbl_warning.Text = "Please select date";
            }
            else if (Txt_Days.Text == "")
            {
                lbl_warning.Text = "Please assign No. of days";
            }
            else
            {
                lbl_due_date.Text = lb.nextdate(Txt_Days.Text, Txt_date.Text);
            }
        }


        private string cretesessionid1()
        {
            bool duplicate = false;
            string transaction_no = Library.session_wisl_issue_book("transaction_no", ViewState["Branch_id"].ToString());
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select transaction_no from lib_student_transaction_details where transaction_no='" + transaction_no + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    transaction_no = Library.session_wisl_issue_book("transaction_no", ViewState["Branch_id"].ToString());
                }
            }
            return transaction_no;



        }
        private string cretesessionid2()
        {
            bool duplicate = false;
            string transaction_no = Library.session_wisl_issue_book("transaction_no", ViewState["Branch_id"].ToString());
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select transaction_no from lib_teacher_trans_action_details where transaction_no='" + transaction_no + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    transaction_no = Library.session_wisl_issue_book("transaction_no", ViewState["Branch_id"].ToString());
                }
            }
            return transaction_no;



        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {


            Txt_Admission_No.Text = "";
            if (RadioButtonList1.SelectedValue == "Student")
            {
                lbl_sercheading.Text = "Enter Admission No. :";
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
                txtbox.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "Non Teaching/Teaching Staff")
            {
                lbl_sercheading.Text = "Enter Non Teaching/Teaching Staff Employee Code";
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
                txtbox.Visible = true;
            }
            else
            {
                txtbox.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }
        }




        #region autosearch
        [WebMethod]
        public static List<string> GetAuthor(string Author)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct AuthorName from Library_Book_Entry where AuthorName LIKE ''+@AuthorName+'%' and Issued_Status!='Issued' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@AuthorName", Author);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["AuthorName"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        [WebMethod]
        public static List<string> Getbookname(string bookname)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct NameOfBook from Library_Book_Entry where NameOfBook LIKE ''+@NameOfBook+'%' and Issued_Status!='Issued' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@NameOfBook", bookname);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["NameOfBook"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]
        public static List<string> GetISBN(string ISBN)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct ISBN_Num from Library_Book_Entry where ISBN_Num LIKE ''+@BookISBN+'%' and Issued_Status!='Issued' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@BookISBN", ISBN);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["ISBN_Num"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]
        public static List<string> GetBarcode(string barcode)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Bar_code from Library_Book_Entry where Bar_code LIKE ''+@Bar_code+'%' and Issued_Status!='Issued' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Bar_code", barcode);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Bar_code"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        #endregion

        #region item code wise search
        [WebMethod]
        public static List<string> Getitem_code(string BookId)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct BookId from Library_Book_Entry where BookId LIKE ''+@BookId+'%' and Issued_Status!='Issued' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["BookId"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        #endregion




        #region find teacher and student

        protected void Btn_find_student_admission_no_ammployee_code_Click(object sender, EventArgs e)
        {
            ViewState["name"] = "";
            ViewState["gcm_id"] = "";
            int i = 0;
            int rowcount = rd_view.Items.Count;
            foreach (RepeaterItem row in rd_view.Items)
            {
                CheckBox chk = rd_view.Items[i].FindControl("rowChkBox") as CheckBox;
                if (chk != null && chk.Checked)
                {

                }
                else
                {
                    i++;
                }
            }


            if (rowcount != i)
            {


                string user_id_admission_no = Txt_Admission_No.Text;
                if (RadioButtonList1.SelectedValue == "Student")
                {
                    DataTable dt = mycode.FillData("select top 1 * from admission_registor where admissionserialnumber ='" + user_id_admission_no + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Session_id=" + My.get_session_id() + "   order by id desc");
                    if (dt.Rows.Count == 0)
                    {
                        Panel4.Visible = false;
                        Panel2.Visible = false;
                        Alertme("Sorry there are no data list exist", "warning");
                        Rd_View1.DataSource = null;
                        Rd_View1.DataBind();
                    }
                    else
                    {
                        ViewState["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                        ViewState["name"] = dt.Rows[0]["studentname"].ToString();
                        if (dt.Rows[0]["Status"].ToString() == "1")
                        {
                            Panel4.Visible = true;
                            Panel2.Visible = true;
                            Rd_View1.DataSource = dt;
                            Rd_View1.DataBind();

                            Hd_Library_student.Value = dt.Rows[0]["lib_card_no"].ToString();
                            Hd_admission_no.Value = dt.Rows[0]["admissionserialnumber"].ToString();
                          
                        }
                        else
                        {
                            Panel4.Visible = false;
                            Panel2.Visible = false;
                            Hd_Library_student.Value = "";
                            Hd_admission_no.Value = "";
                            Alertme("Sorry this student is deactivated by school administrator", "warning");
                            Rd_View1.DataSource = null;
                            Rd_View1.DataBind();
                        }

                    }
                }
                else if (RadioButtonList1.SelectedValue == "Non Teaching/Teaching Staff")
                {
                    hd_staff_libcode.Value = "";
                    DataTable dt = mycode.FillData("select *,(Select top 1  Library_Card_No from user_details where user_id=PRL_Employee_Master.Emp_Code) as Library_Card_No,(Select top 1  gcm_id from user_details where user_id=PRL_Employee_Master.Emp_Code) as gcm_id ,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Emp_Code='" + user_id_admission_no + "'  ");
                    if (dt.Rows.Count == 0)
                    {
                        hd_staff_emp_code.Value = "";
                        Alertme("Sorry there are no data list exist", "warning");
                        Repeater1_staff.DataSource = null;
                        Repeater1_staff.DataBind();
                        Panel4.Visible = false;
                        Panel3.Visible = false;
                    }
                    else
                    {
                        ViewState["name"] = dt.Rows[0]["Employee_Name"].ToString();
                        ViewState["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                        if (dt.Rows[0]["Status"].ToString() == "Active")
                        {
                            Repeater1_staff.DataSource = dt;
                            Repeater1_staff.DataBind();
                            Panel3.Visible = true;
                            Panel4.Visible = true;
                            hd_staff_emp_code.Value = dt.Rows[0]["Emp_Code"].ToString();
                            hd_staff_libcode.Value = dt.Rows[0]["Library_Card_No"].ToString();


                        }
                        else
                        {
                            Panel4.Visible = false;
                            Panel3.Visible = false;
                            hd_staff_emp_code.Value = "";
                            Alertme("Sorry this user is deactivated by school administrator", "warning");
                            Repeater1_staff.DataSource = null;
                            Repeater1_staff.DataBind();
                        }
                    }
                }
            }
            else
            {
                Alertme("Please select atleast one book", "warning");
            }
        }
        #endregion

        
    }
}


