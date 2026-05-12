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
    public partial class Add_Book_Category : System.Web.UI.Page
    {
        My mycode = new My();
        Library ly = new Library();
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
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        Bind_Book_type();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Book_Category");
            }

        }

        private void Bind_Book_type()
        {
            DataTable dt = mycode.FillData("Select * from Library_Book_Category where Branch_Id=" + ViewState["Branch_id"].ToString() + "");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no Book category", "warning");
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



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Book_Category.aspx", false);
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_book_type.Text == "")
            {
                Alertme("Please enter book category", "warning");
            }
            else
            {
                SqlCommand cmd;
                if (btn_Submit.Text == "Add")
                {
                    string Book_Category = get_Book_Category();
                    DataTable dt = mycode.FillData("Select * from Library_Book_Category where Book_Category='" + txt_book_type.Text + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                    if (dt.Rows.Count == 0)
                    {

                        string query = "INSERT INTO Library_Book_Category (Book_Category,Book_Category_Id,Created_user,Created_date,Branch_Id) values (@Book_Category,@Book_Category_Id,@Created_user,@Created_date,@Branch_Id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Book_Category", txt_book_type.Text);
                        cmd.Parameters.AddWithValue("@Book_Category_Id", Book_Category);
                        cmd.Parameters.AddWithValue("@Created_user", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                       
                        cmd.Parameters.AddWithValue("@Branch_Id", ViewState["Branch_id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            txt_book_type.Text = "";
                            Alertme("Book Subject has been update Successfully.", "success");
                            Bind_Book_type();
                        }

                    }
                    else
                    {

                        Alertme("Your entered book subject already exists", "warning");
                    }
                }
                else
                {
                    DataTable dt = mycode.FillData("Select * from Library_Book_Category where Book_Category='" + txt_book_type.Text + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Id!=" + HdID.Value + "");
                    if (dt.Rows.Count == 0)
                    {

                        string query = "update Library_Book_Category set Book_Category=@Book_Category,Updated_user=@Updated_user,Updated_date=@Updated_date where Id=" + HdID.Value + "";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Book_Category", txt_book_type.Text);
                        cmd.Parameters.AddWithValue("@Updated_user", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        if (My.InsertUpdateData(cmd))
                        {
                            btn_Submit.Text = "Add";
                            txt_book_type.Text = "";
                            Alertme("Book subject has been update Successfully.", "success");
                            Bind_Book_type();
                        }

                    }
                    else
                    {
                        Alertme("Your entered book subject already exists", "warning");
                    }
                }
            }
        }

        private string get_Book_Category()
        {
            bool duplicate = false;
            string Book_Category_Id = Library.session_wisl("Book_Category_Id", ViewState["Branch_id"].ToString());
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("select Book_Category_Id from  Library_Book_Category where Book_Category_Id='" + Book_Category_Id + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Book_Category_Id = Library.session_wisl("Book_Category_Id", ViewState["Branch_id"].ToString());
                }
            }
            return Book_Category_Id;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        { 
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_Book_Category = (Label)row.FindControl("lbl_Book_Category");
            HdID.Value = lbl_Id.Text;
            txt_book_type.Text = lbl_Book_Category.Text;
            btn_Submit.Text = "Update";
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {

            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_Book_Category_Id = (Label)row.FindControl("lbl_Book_Category_Id");
            if (is_true(lbl_Book_Category_Id.Text))
            {

                mycode.executequery("delete from Library_Book_Category where  Id=" + lbl_Id.Text + " ");

                Alertme("Book subject has been delete Successfully.", "success");
                Bind_Book_type();

            }
            else
            {
                Alertme("You can't delete this Book subject", "warning");
                return;
            }
        }

        private bool is_true(string Type)
        {
            if (mycode.FillData("select * from Library_Book_Entry  where Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Book_Category_Id='" + Type + "'").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}