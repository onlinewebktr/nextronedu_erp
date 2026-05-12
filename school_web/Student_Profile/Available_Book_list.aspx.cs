using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class Available_Book_list : System.Web.UI.Page
    {
        My mycode = new My();
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
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        Get_student_info();
                        try
                        {
                            ddl_class.SelectedValue = ViewState["Class_id"].ToString();
                        }
                        catch
                        {

                        }
                        
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

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_data_taken();
        }

        private void Get_data_taken()
        {
            try
            {
                if(ddl_class.SelectedItem.Text=="ALL")
                {
                    bind_grd_view("select distinct NameOfBook,AuthorName,Publication,ISBN_Num,PublicationYear,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where   lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Issued_Status='Not Issued' order by lbe.NameOfBook asc");
                }
                else
                {
                    bind_grd_view("select distinct NameOfBook,AuthorName,Publication,ISBN_Num,PublicationYear,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new,(" + Sub_Location + ") as Sub_Location from Library_Book_Entry lbe where  lbe.SelectClass='" + ddl_class.SelectedValue + "' and lbe.Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Issued_Status='Not Issued' order by lbe.NameOfBook asc");
                }
              
            }
            catch
            {

            }
         
        }

        private void bind_grd_view(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry! there is no available book list available", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
        private void Get_student_info()
        {
            string query = "select top 1 Session_id,Class_id,Section,Branch_id from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and StudentStatus='AV'    and Transfer_Status in ('New','NT') and Session_id='"+ ViewState["sesssionid"].ToString() + "' order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                ViewState["Class_id"]= dt.Rows[0]["Class_id"].ToString();
                ViewState["Branch_id"] = dt.Rows[0]["Branch_id"].ToString();
            }
        }

       
    }
}