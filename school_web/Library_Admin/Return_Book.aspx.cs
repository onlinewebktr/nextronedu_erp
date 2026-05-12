using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Library_Admin
{
    public partial class Return_Book : System.Web.UI.Page
    {
        My mycode = new My();
        Library lb = new Library();
        string booktype = "Select top 1 TypeName from Library_Type where TypeId=lbe.Type ";
        string BookStatus = "Select top 1 BookStatus from Library_Book_Status where BookStatusId=lbe.BookStatus  ";
        string Book_Category = "Select top 1 Book_Category from Library_Book_Category where Book_Category_Id=lbe.Book_Category_Id and Branch_Id=lbe.Branch_id";

        string location = "Select top 1 location from lib_location_details where Location_id=lbe.Location and Branch_Id=lbe.Branch_id";
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
                        Session["reprintstudent_returnbook"] = 1;
                        Session["retunpage"] = "1";
                        Txt_returndate.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["flag"] = "0";
                        ViewState["session_id"] = My.get_session_id();
                        ViewState["session"] = My.get_session();
                        Txt_Admission_No.Text = "";
                        // bind_all_data();
                        Panel2.Visible = false;
                        Panel3.Visible = false;
                        Panel4.Visible = false;

                        try
                        {
                            if (Session["msg2"] == null)
                            {

                            }
                            else
                            {
                                Alertme(Session["msg2"].ToString(), "success");
                                Session["msg2"] = null;
                            }
                        }
                        catch
                        {

                        }
                        Lbl_Transaction.Text = "";
                        txt_extraday_book_fine.Text = "0.00";
                        lbl_total_payment.Text = "0.00";
                        hd_extra_day_fine_amount.Value = "0.00";
                        lbl_issue_ate.Text = "xx/xx/xxxx";
                        lbl_due_date_date.Text = "xx/xx/xxxx";
                        txt_extra_day.Text = "0";
                        Txt_Remarks.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }

        }

        protected void txt_extra_day_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Txt_dimage_book_fineamount_TextChanged(object sender, EventArgs e)
        {

            double total = My.toDouble(txt_extraday_book_fine.Text) + My.toDouble(Txt_dimage_book_fineamount.Text);
            lbl_total_payment.Text = total.ToString("0.00");
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel4.Visible = false;
            Panel3.Visible = false;
            Panel2.Visible = false;
            book_details.Visible = false;
            Txt_Admission_No.Text = "";
            if (RadioButtonList1.SelectedValue == "Student")
            {
                lbl_sercheading.Text = "Enter Adm. No./Library/Book Issued No. :";
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
                txtbox.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "Non Teaching/Teaching Staff")
            {
                lbl_sercheading.Text = "Enter Emp./Library/Book Issued No. :";
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

        #region find teacher and student

        protected void Btn_find_student_admission_no_ammployee_code_Click(object sender, EventArgs e)
        {

            ViewState["library_card_no"] = "";

            string user_id_admission_no = Txt_Admission_No.Text;
            if (RadioButtonList1.SelectedValue == "Student")
            {
                string get_admission_no = lb.get_student_staff_userid("Student", user_id_admission_no);
                DataTable dt = mycode.FillData("select top 1 * from admission_registor where (admissionserialnumber ='" + user_id_admission_no + "' or lib_card_no='" + user_id_admission_no + "' or admissionserialnumber='" + get_admission_no + "') and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Session_id=" + ViewState["session_id"].ToString() + "   order by id desc");
                if (dt.Rows.Count == 0)
                {
                    Panel4.Visible = false;
                    Panel3.Visible = false;
                    Panel2.Visible = false;
                    Alertme("Sorry there are no data list exist", "warning");
                    Rd_View1.DataSource = null;
                    Rd_View1.DataBind();
                }
                else
                {
                    if (dt.Rows[0]["Status"].ToString() == "1")
                    {
                        Panel4.Visible = false;
                        Panel2.Visible = true;
                        Panel3.Visible = false;
                        Rd_View1.DataSource = dt;
                        Rd_View1.DataBind();

                        Hd_Library_student.Value = dt.Rows[0]["lib_card_no"].ToString();
                        ViewState["library_card_no"] = dt.Rows[0]["lib_card_no"].ToString();
                        Hd_admission_no.Value = dt.Rows[0]["admissionserialnumber"].ToString();
                        Bind_book_details_details(dt.Rows[0]["admissionserialnumber"].ToString(), "Student", get_admission_no);
                    }
                    else
                    {
                        Panel4.Visible = false;
                        Panel2.Visible = false;
                        Panel3.Visible = false;
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
                string get_userid = lb.get_student_staff_userid("Staff", user_id_admission_no);
                ViewState["library_card_no"] = "";
                hd_staff_libcode.Value = "";

                DataTable dt = mycode.FillData("select *,(Select top 1  Library_Card_No from user_details where user_id=PRL_Employee_Master.Emp_Code) as Library_Card_No ,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where  (Emp_Code='" + user_id_admission_no + "' or Emp_Code='" + get_userid + "') ");

                if (dt.Rows.Count == 0)
                {

                    DataTable dt1 = mycode.FillData("select *,(Select top 1  Library_Card_No from user_details where user_id=PRL_Employee_Master.Emp_Code) as Library_Card_No ,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Emp_Code in (Select user_id from user_details where Library_Card_No='" + user_id_admission_no + "') ");
                    if (dt1.Rows.Count == 0)
                    {
                        hd_staff_emp_code.Value = "";
                        Alertme("Sorry there are no data list exist", "warning");
                        Repeater1_staff.DataSource = null;
                        Repeater1_staff.DataBind();
                        Panel4.Visible = false;
                        Panel3.Visible = false;
                        Panel2.Visible = false;
                    }
                    else
                    {
                        if (dt1.Rows[0]["Status"].ToString() == "Active")
                        {
                            Repeater1_staff.DataSource = dt1;
                            Repeater1_staff.DataBind();
                            Panel3.Visible = true;
                            Panel4.Visible = false;
                            Panel2.Visible = false;
                            hd_staff_emp_code.Value = dt1.Rows[0]["Emp_Code"].ToString();
                            hd_staff_libcode.Value = dt1.Rows[0]["Library_Card_No"].ToString();
                            ViewState["library_card_no"] = hd_staff_libcode.Value;
                            Bind_book_details_details(dt1.Rows[0]["Emp_Code"].ToString(), "Staff", get_userid);

                        }
                        else
                        {
                            Panel4.Visible = false;
                            Panel3.Visible = false;
                            Panel2.Visible = false;
                            hd_staff_emp_code.Value = "";
                            Alertme("Sorry this user is deactivated by school administrator", "warning");
                            Repeater1_staff.DataSource = null;
                            Repeater1_staff.DataBind();
                        }
                    }

                        
                }
                else
                {
                    if (dt.Rows[0]["Status"].ToString() == "Active")
                    {
                        Repeater1_staff.DataSource = dt;
                        Repeater1_staff.DataBind();
                        Panel3.Visible = true;
                        Panel4.Visible = false;
                        Panel2.Visible = false;
                        hd_staff_emp_code.Value = dt.Rows[0]["Emp_Code"].ToString();
                        hd_staff_libcode.Value = dt.Rows[0]["Library_Card_No"].ToString();
                        ViewState["library_card_no"] = hd_staff_libcode.Value;
                        Bind_book_details_details(dt.Rows[0]["Emp_Code"].ToString(), "Staff", get_userid);

                    }
                    else
                    {
                        Panel4.Visible = false;
                        Panel3.Visible = false;
                        Panel2.Visible = false;
                        hd_staff_emp_code.Value = "";
                        Alertme("Sorry this user is deactivated by school administrator", "warning");
                        Repeater1_staff.DataSource = null;
                        Repeater1_staff.DataBind();
                    }
                }
            }
            bind_temp_data();
        }

        private void Bind_book_details_details(string userid, string type, string admission_userid)//admission_userid from when find issue book by transaction id wise
        {
            string query = "";
            if (type == "Student")
            {

                if (admission_userid == "0")
                {
                    query = "select (" + BookStatus + ") as Book_Status, lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,(" + location + ") as location_new,lbe.BookId from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1 and     lst.student_id ='" + userid + "'    and lst.status='Issued'  order by lst.issue_date";

                }
                else
                {

                    query = "select (" + BookStatus + ") as Book_Status, lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,(" + location + ") as location_new,lbe.BookId from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ar.Status=1 and     lst.student_id ='" + userid + "' and  lst.transaction_no='" + Txt_Admission_No.Text + "'   and lst.status='Issued'  order by lst.issue_date";

                }



            }
            else

            {
                if (admission_userid == "0")
                {
                    query = "select  (" + BookStatus + ") as Book_Status,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,(" + location + ") as location_new,lbe.BookId from user_details ur join lib_teacher_trans_action_details  lst on ur.user_id=lst.teacher_id  join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ur.status='Active' and     lst.teacher_id ='" + userid + "'   and lst.status='Issued' order by lst.issue_date";
                }
                else
                {
                    query = "select  (" + BookStatus + ") as Book_Status,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,(" + location + ") as location_new,lbe.BookId from user_details ur join lib_teacher_trans_action_details  lst on ur.user_id=lst.teacher_id  join Library_Book_Entry lbe on lbe.BookId=lst.book_no where   ur.status='Active' and     lst.teacher_id ='" + userid + "'    and   lst.transaction_no='" + Txt_Admission_No.Text + "'   and lst.status='Issued' order by lst.issue_date";

                }
            }

            txtbox.Visible = false;
            book_details.Visible = false;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no books taken by student", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {

                book_details.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }


        }
        #endregion


        #region book selection
        protected void btn_select_Click(object sender, EventArgs e)
        {
            try
            {
                Panel4.Visible = false;
                double getextra_day_fin_amount = 0;
                Lbl_Transaction.Text = "";
                txt_extraday_book_fine.Text = "0.00";
                lbl_total_payment.Text = "0.0";
                double extra_day_fine = 0.00;
                Button btn = (Button)sender;
                RepeaterItem row = (RepeaterItem)btn.NamingContainer;
                Label lbl_uniq_bookid = (Label)row.FindControl("lbl_uniq_bookid");
                Label lbl_book_issue_date = (Label)row.FindControl("lbl_book_issue_date");
                Label lbl_book_due_date = (Label)row.FindControl("lbl_book_due_date");
                Label lbl_transactionno = (Label)row.FindControl("lbl_transaction_no");
                Lbl_Transaction.Text = lbl_transactionno.Text;
                lbl_issue_ate.Text = lbl_book_issue_date.Text;
                lbl_due_date_date.Text = lbl_book_due_date.Text;
                hd_book_id.Value = lbl_uniq_bookid.Text;
                txt_extra_day.Text = lb.noof_day_return(lbl_due_date_date.Text, Txt_returndate.Text);

                if (RadioButtonList1.SelectedValue == "Student")
                {
                    extra_day_fine = lb.get_extra_day_fine("Student", Hd_admission_no.Value);
                }
                else
                {
                    extra_day_fine = lb.get_extra_day_fine("Staff", hd_staff_emp_code.Value);
                }


                if (My.toDouble(txt_extra_day.Text) == 0)
                {
                    hd_extra_day_fine_amount.Value = "0.00";
                }
                else
                {
                    hd_extra_day_fine_amount.Value = extra_day_fine.ToString("0.00");
                }


                Txt_dimage_book_fineamount.Text = "0.00";
                getextra_day_fin_amount = extra_day_fine * My.toDouble(txt_extra_day.Text);
                txt_extraday_book_fine.Text = getextra_day_fin_amount.ToString("0.00");
                lbl_total_payment.Text = txt_extraday_book_fine.Text;
                Panel4.Visible = true;
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Return_Book");
            }


        }

        protected void chk_latefineapplay_CheckedChanged(object sender, EventArgs e)
        {
            double getextra_day_fin_amount = 0;
            
            txt_extraday_book_fine.Text = "0.00";
            lbl_total_payment.Text = "0.0";
            double extra_day_fine = 0.00;
            double totalfee = 0.00;
            if (chk_latefineapplay.Checked == true)
            {
                if (RadioButtonList1.SelectedValue == "Student")
                {
                    extra_day_fine = lb.get_extra_day_fine("Student", Hd_admission_no.Value);
                }
                else
                {
                    extra_day_fine = lb.get_extra_day_fine("Staff", hd_staff_emp_code.Value);
                }

                if (My.toDouble(txt_extra_day.Text) == 0)
                {
                    hd_extra_day_fine_amount.Value = "0.00";
                }
                else
                {
                    hd_extra_day_fine_amount.Value = extra_day_fine.ToString("0.00");
                }

                getextra_day_fin_amount = extra_day_fine * My.toDouble(txt_extra_day.Text);
                txt_extraday_book_fine.Text = getextra_day_fin_amount.ToString("0.00");

                if (Ddl_Book_Status.Text == "Return Damage Book")
                {
                    totalfee = getextra_day_fin_amount + My.toDouble(Txt_dimage_book_fineamount.Text);
                }
                lbl_total_payment.Text = totalfee.ToString("0.00");



            }
            else
            {
                getextra_day_fin_amount = 0.00;
                hd_extra_day_fine_amount.Value = "0.00";
                if (Ddl_Book_Status.Text == "Return Damage Book")
                {
                    totalfee = getextra_day_fin_amount + My.toDouble(Txt_dimage_book_fineamount.Text);
                }
                lbl_total_payment.Text = totalfee.ToString("0.00");

            }
        }
        #endregion

        protected void Ddl_Book_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            Txt_dimage_book_fineamount.Text = "0.00";
            if (Ddl_Book_Status.Text == "Return Damage Book")
            {
                Damagepnl.Visible = true;
            }
            else
            {
                Damagepnl.Visible = false;
            }
        }



        #region temp submit delete
        protected void Btn_Issue_Book_Click(object sender, EventArgs e)
        {

            if (Lbl_Transaction.Text == "")
            {
                Alertme("Please select  retun book", "warning");

            }
            else if (Txt_returndate.Text == "")
            {

                Alertme("Please choose retun book date", "warning");
            }

            else
            {
                if (Ddl_Book_Status.Text == "Return Damage Book")
                {
                    if (Txt_dimage_book_fineamount.Text == "")
                    {
                        Alertme("Please enter damage book fine", "warning");
                    }
                    else
                    {
                        temp_data_save();
                    }

                }
                else
                {
                    temp_data_save();
                }
            }

        }

        private void temp_data_save()
        {
            string userid = "";
            if (RadioButtonList1.SelectedValue == "Student")
            {
                userid = Hd_admission_no.Value;
            }
            else
            {
                userid = hd_staff_emp_code.Value;
            }


            string query1 = "select  * from lib_Temp_return_book lst where   lst.Status='Pending' and    lst.Student_user_id ='" + userid + "' and lst.User_admin='" + ViewState["Userid"].ToString() + "' and Book_id='" + hd_book_id.Value + "' ";
            DataTable dt = mycode.FillData(query1);
            if (dt.Rows.Count == 0)
            {

                SqlCommand cmd;
                string query = "INSERT INTO lib_Temp_return_book (Student_user_id,User_admin,Book_id,Status,Bookstatus,Issue_date,Due_date,Return_date,Extra_day,Extra_Day_Fine,Damage_Book_Fine,Total,transaction_no,Remarks,Extra_day_fine_amount) values (@Student_user_id,@User_admin,@Book_id,@Status,@Bookstatus,@Issue_date,@Due_date,@Return_date,@Extra_day,@Extra_Day_Fine,@Damage_Book_Fine,@Total,@transaction_no,@Remarks,@Extra_day_fine_amount)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Student_user_id", userid);
                cmd.Parameters.AddWithValue("@User_admin", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Book_id", hd_book_id.Value);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@Bookstatus", Ddl_Book_Status.Text);
                cmd.Parameters.AddWithValue("@Issue_date", lbl_issue_ate.Text);
                cmd.Parameters.AddWithValue("@Due_date", lbl_due_date_date.Text);
                cmd.Parameters.AddWithValue("@Return_date", Txt_returndate.Text);
                cmd.Parameters.AddWithValue("@Extra_day", txt_extra_day.Text);
                cmd.Parameters.AddWithValue("@Extra_Day_Fine", My.toDouble(txt_extraday_book_fine.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Damage_Book_Fine", My.toDouble(Txt_dimage_book_fineamount.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total", lbl_total_payment.Text);
                cmd.Parameters.AddWithValue("@transaction_no", Lbl_Transaction.Text);
                cmd.Parameters.AddWithValue("@Remarks", Txt_Remarks.Text);
                cmd.Parameters.AddWithValue("@Extra_day_fine_amount", hd_extra_day_fine_amount.Value);
                if (My.InsertUpdateData(cmd))
                {
                    bind_temp_data();
                    Lbl_Transaction.Text = "";
                    txt_extraday_book_fine.Text = "0.00";
                    lbl_total_payment.Text = "0.00";
                    hd_extra_day_fine_amount.Value = "0.00";
                    lbl_issue_ate.Text = "xx/xx/xxxx";
                    lbl_due_date_date.Text = "xx/xx/xxxx";
                    txt_extra_day.Text = "0";
                    Txt_Remarks.Text = "";
                    Txt_dimage_book_fineamount.Text = "0.00";
                    Ddl_Book_Status.Text = "Return Book";
                }
            }
            else
            {
                string Id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string query = "update lib_Temp_return_book set Bookstatus=@Bookstatus,Return_date=@Return_date,Extra_day=@Extra_day,Extra_Day_Fine=@Extra_Day_Fine,Damage_Book_Fine=@Damage_Book_Fine,Total=@Total,Remarks=@Remarks,Extra_day_fine_amount=@Extra_day_fine_amount where Id=@Id";


                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Bookstatus", Ddl_Book_Status.Text);

                cmd.Parameters.AddWithValue("@Return_date", Txt_returndate.Text);
                cmd.Parameters.AddWithValue("@Extra_day", txt_extra_day.Text);
                cmd.Parameters.AddWithValue("@Extra_Day_Fine", My.toDouble(txt_extraday_book_fine.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Damage_Book_Fine", My.toDouble(Txt_dimage_book_fineamount.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total", lbl_total_payment.Text);
                cmd.Parameters.AddWithValue("@transaction_no", Lbl_Transaction.Text);
                cmd.Parameters.AddWithValue("@Remarks", Txt_Remarks.Text);
                cmd.Parameters.AddWithValue("@Extra_day_fine_amount", hd_extra_day_fine_amount.Value);

                cmd.Parameters.AddWithValue("@Id", Id);
                if (My.InsertUpdateData(cmd))
                {
                    bind_temp_data();
                    Lbl_Transaction.Text = "";
                    txt_extraday_book_fine.Text = "0.00";
                    lbl_total_payment.Text = "0.00";
                    hd_extra_day_fine_amount.Value = "0.00";
                    lbl_issue_ate.Text = "xx/xx/xxxx";
                    lbl_due_date_date.Text = "xx/xx/xxxx";
                    txt_extra_day.Text = "0";
                    Txt_Remarks.Text = "";
                    Txt_dimage_book_fineamount.Text = "0.00";
                    Ddl_Book_Status.Text = "Return Book";
                }
            }

        }

        protected void lnkDel_temp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                mycode.executequery("Delete from lib_Temp_return_book where Id=" + lbl_Id.Text + " and User_admin='" + ViewState["Userid"].ToString() + "'"); ;

                bind_temp_data();
            }
            catch
            {

            }

        }

        private void bind_temp_data()
        {
            string userid = "";
            if (RadioButtonList1.SelectedValue == "Student")
            {
                userid = Hd_admission_no.Value;
            }
            else
            {
                userid = hd_staff_emp_code.Value;
            }
            string query = "select  (" + BookStatus + ") as Book_Status,lst.issue_date,lst.due_date,lst.Book_id,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,(" + location + ") as location_new,lst.Id,lst.Due_date,lst.Issue_date,lst.Return_date,lst.Bookstatus,lst.Extra_day,lst.Extra_Day_Fine,lst.Extra_day_fine_amount,lst.Damage_Book_Fine,lst.Id,lst.Total from lib_Temp_return_book  lst  join Library_Book_Entry lbe on lbe.BookId=lst.Book_id where   lst.Status='Pending' and    lst.Student_user_id ='" + userid + "' and lst.User_admin='" + ViewState["Userid"].ToString() + "' order by  lst.Id";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                addedbook.Visible = false;
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
            else
            {
                Panel4.Visible = true;
                addedbook.Visible = true;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

            }
        }
        #endregion
        protected void btn_final_submit_data_Click(object sender, EventArgs e)
        {

            string confirmValue = string.Empty;
            confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                try
                {

                    finalsubmitdata();
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "return book");

                }
            }
            else
            {
                Alertme("Button Not Clicked", "warning");
            }







        }

        private void finalsubmitdata()
        {

            string userid = "";
            if (RadioButtonList1.SelectedValue == "Student")
            {
                userid = Hd_admission_no.Value;
            }
            else
            {
                userid = hd_staff_emp_code.Value;
            }

            string sessionid = "0";
            bool issubmit = false;
            string query = "select  (" + BookStatus + ") as Book_Status,lst.issue_date,lst.due_date,lst.Book_id,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num,lst.status,lst.transaction_no,(" + location + ") as location_new,lst.Id,lst.Due_date,lst.Issue_date,lst.Return_date,lst.Bookstatus,lst.Extra_day,lst.Extra_Day_Fine,lst.Extra_day_fine_amount,lst.Damage_Book_Fine,lst.Id,lst.Total,lst.Student_user_id,lst.Remarks from lib_Temp_return_book  lst  join Library_Book_Entry lbe on lbe.BookId=lst.Book_id where   lst.Status='Pending' and    lst.Student_user_id ='" + userid + "' and lst.User_admin='" + ViewState["Userid"].ToString() + "' order by  lst.Id";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                if (RadioButtonList1.SelectedValue == "Student")
                {
                    string get_returnslip = "LIB/STU/RE/" + ViewState["session"].ToString() + "/" + cretesessionid1();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string query2 = "Select * from  lib_student_transaction_details where book_no='" + dt.Rows[i]["Book_id"].ToString() + "' and transaction_no='" + dt.Rows[i]["transaction_no"].ToString() + "' and Status='Issued' and student_id='" + dt.Rows[i]["Student_user_id"].ToString() + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "'  ";

                        DataTable dt2 = mycode.FillData(query2);
                        if (dt2.Rows.Count == 1)
                        {
                            SqlCommand cmd;
                            string query1 = "Update lib_student_transaction_details set returned_date = @returned_date,Remarks = @Remarks,fine =@fine,damagebookfine=@damagebookfine,Extra_days=@Extra_days,Total_Fine = @Total_Fine,fine_status=@fine_status,return_idate=@return_idate,Branch_id=@Branch_id,library_card_no=@library_card_no,Book_status=@Book_status,Status=@Status,Book_reurn_slip_id=@Book_reurn_slip_id where book_no=@book_no and transaction_no=@transaction_no and student_id=@student_id";
                            cmd = new SqlCommand(query1);
                            cmd.Parameters.AddWithValue("@returned_date", dt.Rows[i]["Return_date"].ToString());
                            cmd.Parameters.AddWithValue("@Extra_days", dt.Rows[i]["Extra_day"].ToString());
                            cmd.Parameters.AddWithValue("@fine_status", "Paid");
                            cmd.Parameters.AddWithValue("@damagebookfine", dt.Rows[i]["Damage_Book_Fine"].ToString());
                            cmd.Parameters.AddWithValue("@fine", dt.Rows[i]["Extra_day_fine_amount"].ToString());
                            cmd.Parameters.AddWithValue("@Total_Fine", dt.Rows[i]["Total"].ToString());
                            cmd.Parameters.AddWithValue("@Remarks", dt.Rows[i]["Remarks"].ToString());
                            cmd.Parameters.AddWithValue("@return_idate", mycode.ConvertStringToiDate(dt.Rows[i]["Return_date"].ToString()));
                            cmd.Parameters.AddWithValue("@library_card_no", ViewState["library_card_no"].ToString());
                            cmd.Parameters.AddWithValue("@Book_status", dt.Rows[i]["Bookstatus"].ToString());
                            cmd.Parameters.AddWithValue("@book_no", dt.Rows[i]["Book_id"].ToString());
                            cmd.Parameters.AddWithValue("@transaction_no", dt.Rows[i]["transaction_no"].ToString());
                            cmd.Parameters.AddWithValue("@student_id", dt.Rows[i]["Student_user_id"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                            cmd.Parameters.AddWithValue("@Status", "Return");
                            cmd.Parameters.AddWithValue("@Book_reurn_slip_id", get_returnslip);
                            if (My.InsertUpdateData(cmd))
                            {
                                My.exeSql("update Library_Book_Entry set Issued_Status='Not Issued' where BookId='" + dt.Rows[i]["Book_id"].ToString() + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "'");

                                My.exeSql("update lib_Temp_return_book set Status='Return' where Book_id='" + dt.Rows[i]["Book_id"].ToString() + "' and Student_user_id='" + dt.Rows[i]["Student_user_id"].ToString() + "' and transaction_no='" + dt.Rows[i]["transaction_no"].ToString() + "'");


                                //--------------------------------
                                sessionid = My.get_sess_prm(ViewState["session"].ToString());
                                // send amount to mis fee at the time student can month be the auto fetch library fee

                                string fetch_month_name_no_any_moth_fee_taken = My.get_month_name_no_fee_taken(dt.Rows[i]["Student_user_id"].ToString(), ViewState["session"].ToString());
                                //if()
                                if (chk_latefineapplay.Checked == true)
                                {
                                    if (My.toDouble(dt.Rows[i]["Extra_Day_Fine"].ToString()) > 0)
                                    {
                                        string Perticular = "Late Fee, Book No.:" + dt.Rows[i]["Book_id"].ToString() + " Slip Id:" + get_returnslip;
                                        insert_data_Misc_Fee_Master_Studentwise(dt.Rows[i]["Student_user_id"].ToString(), fetch_month_name_no_any_moth_fee_taken, ViewState["session"].ToString(), get_returnslip, dt.Rows[i]["Extra_Day_Fine"].ToString(), "Return Book Fine", Perticular, sessionid, dt.Rows[i]["Book_id"].ToString());
                                    }
                                }
                                if (Ddl_Book_Status.Text == "Return Damage Book")
                                {
                                    string Perticular = "Damage Book, Book No.:" + dt.Rows[i]["Book_id"].ToString() + " Slip Id:" + get_returnslip;
                                    insert_data_Misc_Fee_Master_Studentwise(dt.Rows[i]["Student_user_id"].ToString(), fetch_month_name_no_any_moth_fee_taken, ViewState["session"].ToString(), get_returnslip, dt.Rows[i]["Damage_Book_Fine"].ToString(), "Damage Book Fine", Perticular, sessionid, dt.Rows[i]["Book_id"].ToString());
                                }

                                issubmit = true;
                            }
                        }

                    }
                    if (issubmit == true)
                    {
                        Response.Redirect("print/Print_Return_Book_Invoice_Student.aspx?adm=" + Hd_admission_no.Value + "&Slip_no=" + get_returnslip, false);
                    }




                }
                else
                {
                    string get_returnslip = "LIB/STAFF/RE/" + ViewState["session"].ToString() + "/" + cretesessionid2();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string query2 = "Select * from  lib_teacher_trans_action_details where book_no='" + dt.Rows[i]["Book_id"].ToString() + "' and transaction_no='" + dt.Rows[i]["transaction_no"].ToString() + "' and Status='Issued' and teacher_id='" + dt.Rows[i]["Student_user_id"].ToString() + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "'  ";

                        DataTable dt2 = mycode.FillData(query2);
                        if (dt2.Rows.Count == 1)
                        {
                            SqlCommand cmd;
                            string query1 = "Update lib_teacher_trans_action_details set returned_date = @returned_date,Remarks = @Remarks,fine =@fine,damagebookfine=@damagebookfine,Extra_days=@Extra_days,Total_Fine = @Total_Fine,fine_status=@fine_status,return_idate=@return_idate,Branch_id=@Branch_id,library_card_no=@library_card_no,Book_status=@Book_status,Status=@Status,Book_reurn_slip_id=@Book_reurn_slip_id where book_no=@book_no and transaction_no=@transaction_no and teacher_id=@teacher_id";
                            cmd = new SqlCommand(query1);
                            cmd.Parameters.AddWithValue("@returned_date", dt.Rows[i]["Return_date"].ToString());
                            cmd.Parameters.AddWithValue("@Extra_days", dt.Rows[i]["Extra_day"].ToString());
                            cmd.Parameters.AddWithValue("@fine_status", "Paid");
                            cmd.Parameters.AddWithValue("@damagebookfine", dt.Rows[i]["Damage_Book_Fine"].ToString());
                            cmd.Parameters.AddWithValue("@fine", dt.Rows[i]["Extra_day_fine_amount"].ToString());
                            cmd.Parameters.AddWithValue("@Total_Fine", dt.Rows[i]["Total"].ToString());
                            cmd.Parameters.AddWithValue("@Remarks", dt.Rows[i]["Remarks"].ToString());
                            cmd.Parameters.AddWithValue("@return_idate", mycode.ConvertStringToiDate(dt.Rows[i]["Return_date"].ToString()));
                            cmd.Parameters.AddWithValue("@library_card_no", ViewState["library_card_no"].ToString());
                            cmd.Parameters.AddWithValue("@Book_status", dt.Rows[i]["Bookstatus"].ToString());
                            cmd.Parameters.AddWithValue("@book_no", dt.Rows[i]["Book_id"].ToString());
                            cmd.Parameters.AddWithValue("@transaction_no", dt.Rows[i]["transaction_no"].ToString());
                            cmd.Parameters.AddWithValue("@teacher_id", dt.Rows[i]["Student_user_id"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                            cmd.Parameters.AddWithValue("@Status", "Return");
                            cmd.Parameters.AddWithValue("@Book_reurn_slip_id", get_returnslip);
                            if (My.InsertUpdateData(cmd))
                            {
                                My.exeSql("update Library_Book_Entry set Issued_Status='Not Issued' where BookId='" + dt.Rows[i]["Book_id"].ToString() + "' and Branch_id='" + ViewState["Branch_id"].ToString() + "'");

                                My.exeSql("update lib_Temp_return_book set Status='Return' where Book_id='" + dt.Rows[i]["Book_id"].ToString() + "' and Student_user_id='" + dt.Rows[i]["Student_user_id"].ToString() + "' and transaction_no='" + dt.Rows[i]["transaction_no"].ToString() + "'");

                                issubmit = true;
                            }
                        }

                    }
                    if (issubmit == true)
                    {
                        Response.Redirect("print/Print_Return_Book_Invoice_staff.aspx?adm=" + hd_staff_emp_code.Value + "&Slip_no=" + get_returnslip, false);
                    }

                }
            }

            //if (issubmit == true)
            //{
            //    Session["msg2"] = "Book has been successfully returned";
            //    Response.Redirect("Return_Book.aspx", false);
            //}

        }

        private void insert_data_Misc_Fee_Master_Studentwise(string admissionNo, string monthname, string session, string get_returnslip, string payamount, string Type_Mode, string Perticular, string sessionid, string Book_id)
        {

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Misc_Fee_Master_Studentwise where Admission_No='" + admissionNo + "' and Month='" + monthname + "' and Session_id='" + sessionid + "'  and Perticular='" + Perticular + "' ", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = admissionNo;
                dr[2] = monthname;
                dr[3] = session;
                dr[4] = sessionid;
                dr[5] = Perticular;
                dr[6] = My.toDouble(payamount).ToString("0.00");
                dr["Type_Mode"] = Type_Mode;
                dr["Row_id"] = Book_id;


                dt.Rows.Add(dr);

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = admissionNo;
                    dr[2] = monthname;
                    dr[3] = session;
                    dr[4] = sessionid;
                    dr[5] = Perticular;
                    dr[6] = My.toDouble(payamount).ToString("0.00");
                    dr["Type_Mode"] = Type_Mode;
                    dr["Row_id"] = Book_id;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);



        }

        private string cretesessionid1()
        {
            bool duplicate = false;
            string transaction_no = Library.session_wisl_issue_book("Book_reurn_slip_id", ViewState["Branch_id"].ToString());
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Book_reurn_slip_id from lib_student_transaction_details where Book_reurn_slip_id='" + transaction_no + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    transaction_no = Library.session_wisl_issue_book("Book_reurn_slip_id", ViewState["Branch_id"].ToString());
                }
            }
            return transaction_no;

        }

        private string cretesessionid2()
        {
            bool duplicate = false;
            string transaction_no = Library.session_wisl_issue_book("Book_reurn_slip_id", ViewState["Branch_id"].ToString());
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Book_reurn_slip_id from lib_teacher_trans_action_details where Book_reurn_slip_id='" + transaction_no + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    transaction_no = Library.session_wisl_issue_book("Book_reurn_slip_id", ViewState["Branch_id"].ToString());
                }
            }
            return transaction_no;



        }


    }
}