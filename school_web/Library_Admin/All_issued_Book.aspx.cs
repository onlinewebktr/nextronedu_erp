using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin
{
    public partial class All_issued_Book : System.Web.UI.Page
    {

        My mycode = new My();
        Library ly = new Library();
        string booktype = "Select top 1 TypeName from Library_Type where TypeId=lbe.Type and Branch_Id=lbe.Branch_id";
        string BookStatus = "Select top 1 BookStatus from Library_Book_Status where BookStatusId=lbe.BookStatus  ";
        string Book_Category = "Select top 1 Book_Category from Library_Book_Category where Book_Category_Id=lbe.Book_Category_Id and Branch_Id=lbe.Branch_id";

        string location = "Select top 1 location from lib_location_details where Location_id=lbe.Location and Branch_Id=lbe.Branch_id";

        string Sub_Location = "Select top 1 Sub_Location from lib_sub_location_details where Sub_Location_id=lbe.Sub_Location_id and Branch_Id=lbe.Branch_id";
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
                        Session["back"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = Library.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class_wise, "Select Course_Name,course_id from Add_course_table order by Position");

                        mycode.bind_all_ddl_with_id_cap_All(ddl_book_status, "Select BookStatus,BookStatusId from Library_Book_Status");

                        mycode.bind_all_ddl_with_id_cap_All(ddl_lcation, "Select location,Location_id from lib_location_details where Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                        get_firm_name();

                        Bind_Top_All_book();
                    }
                }
            }
            catch
            {

            }
        }

        private void Bind_Top_All_book()
        {
            // string query = "Select   *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new from Library_Book_Entry_Master_Uniqe lbe where  lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and lbe.Is_Delete=1 order by lbe.id desc";



            string query = " Select sum(cast(EnterQuantity as float )) as count,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe  where lbe.Issued_Status='Issued' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and lbe.Is_Delete=1 group by AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,Sub_Location_id ";


            Bind_Data(query);
        }

        private void get_firm_name()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
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
        protected void btn_find_auther_wise_Click(object sender, EventArgs e)
        {
            if (txt_AuthorName.Text == "")
            {
                Alertme("Please selcte autor name", "warning");
            }
            else
            {
                string query = "Select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry_Master_Uniqe lbe where lbe.AuthorName like '%" + txt_AuthorName.Text + "%'";


                Bind_Data(query);
            }
        }



        protected void ddl_book_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_book_status.SelectedItem.Text == "Select")
            {
                Alertme("Please selcte status", "warning");
            }
            else
            {
                if (ddl_book_status.SelectedItem.Text == "ALL")
                {


                    string query = " Select sum(cast(EnterQuantity as float )) as count,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where lbe.Issued_Status='Issued' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and lbe.Is_Delete=1 group by AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,Sub_Location_id ";
                    Bind_Data(query);
                }
                else
                {


                    string query = " Select sum(cast(EnterQuantity as float )) as count,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where lbe.Issued_Status='Issued' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and BookStatus='" + ddl_book_status.SelectedValue + "' and lbe.Is_Delete=1 group by AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,Sub_Location_id ";


                    Bind_Data(query);
                }


            }
        }

        protected void ddl_lcation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_lcation.SelectedItem.Text == "Select")
            {
                Alertme("Please selcte location", "warning");
            }
            else
            {

                if (ddl_lcation.SelectedItem.Text == "ALL" && ddl_class_wise.SelectedItem.Text == "ALL")
                {
                    Bind_Top_All_book();
                }
                else if (ddl_lcation.SelectedItem.Text == "ALL" && ddl_class_wise.SelectedItem.Text != "ALL")
                {
                    class_wise_search_data();
                }

                else
                {
                    string query = " Select sum(cast(EnterQuantity as float )) as count,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where lbe.Issued_Status='Issued' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and Location='" + ddl_lcation.SelectedValue + "' and SelectClass='" + ddl_class_wise.SelectedValue + "'  and lbe.Is_Delete=1 group by AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,Sub_Location_id ";
                    Bind_Data(query);
                }



            }
        }

        protected void ddl_class_wise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                class_wise_search_data();
            }
            catch
            {

            }



        }

        private void class_wise_search_data()
        {
            if (ddl_class_wise.SelectedItem.Text == "Select")
            {
                Alertme("Please selcte class", "warning");
            }
            else
            {

                if (ddl_class_wise.SelectedItem.Text == "ALL")
                {
                    Bind_Top_All_book();
                }
                else
                {
                    string query = " Select sum(cast(EnterQuantity as float )) as count,AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where lbe.Issued_Status='Issued' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and SelectClass='" + ddl_class_wise.SelectedValue + "' and lbe.Is_Delete=1 group by AuthorName,Publication,EnterVolumePart,Edition,PublicationYear,NoOfPages,NameOfBook,Book_Unique_Identifier,ISBN_Num,SelectClass,Branch_id,Type,BookStatus,Book_Category_Id,Location,Sub_Location_id ";
                    Bind_Data(query);
                }
            }
        }

        private void Bind_Data(string qry)
        {
            ViewState["qry"] = qry;
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


        #region WebMethoD
        [WebMethod]
        public static List<string> Getbookauthername(string authername)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct AuthorName from Library_Book_Entry_Master_Uniqe where AuthorName LIKE '%'+@AuthorName+'%'   ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@AuthorName", authername);
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








        #endregion

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



        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string classid = ((Label)e.Item.FindControl("lbl_class_id")).Text;
                    string uniqe_book_no = ((Label)e.Item.FindControl("lbl_uniqe_book_no")).Text;

                    ((Label)e.Item.FindControl("lbl_classname")).Text = ly.get_classname_Name(classid);

                    string bookid= ly.get_all_book_id_via_uniqe_book_no(uniqe_book_no);
                    if(bookid=="0")
                    {
                        ((Label)e.Item.FindControl("lbl_book_id")).Text = "0";
                        ((Label)e.Item.FindControl("lbl_book_id")).Enabled = false;
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lbl_book_id")).Text = bookid;
                    }
                }
            }
            catch { }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            myModal2.Visible = false;

        }

        protected void lnk_view_book_issued_details_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_book_id = (Label)row.FindControl("lbl_book_id");
            if(lbl_book_id.Text=="0")
            {

            }
            else
            {
                string query = " select ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.studentname,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num from admission_registor ar join lib_student_transaction_details  lst on ar.admissionserialnumber=lst.student_id  and ar.Session_Id=lst.Session_id join Library_Book_Entry lbe on lbe.BookId=lst.book_no where lst.status='Issued' and book_no in(" + lbl_book_id.Text + ") order by ar.rollnumber";

                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    student.Visible = false;
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                   
                   
                }
                else
                {
                    student.Visible = true;
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                   
                }
                myModal2.Visible =true;
                bind_staff_details(lbl_book_id.Text);

            }
            
        }

        private void bind_staff_details(string book_id)
        {
            string query = " select  ar.name,ar.user_id,ar.User_Type,lst.issue_date,lst.due_date,lst.book_no,lbe.NameOfBook,lbe.AuthorName,lbe.Publication,lbe.EnterVolumePart,lbe.Edition,lbe.PublicationYear,lbe.ISBN_Num from user_details ar join lib_teacher_trans_action_details  lst on ar.user_id=lst.teacher_id  join Library_Book_Entry lbe on lbe.BookId=lst.book_no where lst.status='Issued' and book_no in(" + book_id + ") order by ar.name";

            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                emp.Visible = false;
                rp_employee.DataSource = null;
                rp_employee.DataBind();
                
                 
            }
            else
            {
                emp.Visible = true;
                rp_employee.DataSource = dt;
                rp_employee.DataBind();
            
            }
        }
    }
}