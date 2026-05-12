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
    public partial class Add_Book_Type : System.Web.UI.Page
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
                My.submitException(ex, "Add_Book_Type");
            }

        }

        private void Bind_Book_type()
        {
            DataTable dt = mycode.FillData("Select * from Library_Type where Branch_Id=" + ViewState["Branch_id"].ToString() + "");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no Book type", "warning");
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
            Response.Redirect("Add_Book_Type.aspx", false);
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_book_type.Text == "")
            {
                Alertme("Please enter book type", "warning");
            }
            else
            {
                SqlCommand cmd;
                if (btn_Submit.Text == "Add")
                {
                    string booktypeid = get_booktypeid();
                    DataTable dt = mycode.FillData("Select * from Library_Type where TypeName='" + txt_book_type.Text + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                    if (dt.Rows.Count == 0)
                    {

                        string query = "INSERT INTO Library_Type (TypeName,TypeId,Branch_Id) values (@TypeName,@TypeId,@Branch_Id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@TypeName", txt_book_type.Text);
                        cmd.Parameters.AddWithValue("@TypeId", booktypeid);
                        cmd.Parameters.AddWithValue("@Branch_Id", ViewState["Branch_id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            txt_book_type.Text = "";
                            Alertme("book type has update  Successfully.", "success");
                            Bind_Book_type();
                        }

                    }
                    else
                    {

                        Alertme("Your entered book type already exists", "warning");
                    }
                }
                else
                {
                    DataTable dt = mycode.FillData("Select * from Library_Type where TypeName='" + txt_book_type.Text + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Id!=" + HdID.Value + "");
                    if (dt.Rows.Count == 0)
                    {

                        string query = "update Library_Type set TypeName=@TypeName where Id=" + HdID.Value + "";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@TypeName", txt_book_type.Text);

                        if (My.InsertUpdateData(cmd))
                        {
                            btn_Submit.Text = "Add";
                            txt_book_type.Text = "";
                            Alertme("Book type has update Successfully.", "success");
                            Bind_Book_type();
                        }

                    }
                    else
                    {
                        Alertme("Your entered book type already exists", "warning");
                    }
                }
            }
        }

        private string get_booktypeid()
        {
            bool duplicate = false;
            string BookType = Library.session_wisl("BookType", ViewState["Branch_id"].ToString());
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select TypeId from  Library_Type where TypeId='" + BookType + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    BookType = Library.session_wisl("BookType", ViewState["Branch_id"].ToString());
                }
            }
            return BookType;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_TypeName = (Label)row.FindControl("lbl_TypeName");
            HdID.Value = lbl_Id.Text;
            txt_book_type.Text = lbl_TypeName.Text;
            btn_Submit.Text = "Update";
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_TypeId = (Label)row.FindControl("lbl_TypeId");
            if (is_true(lbl_TypeId.Text))
            {

                mycode.executequery("delete from Library_Type where  Id=" + lbl_Id.Text + " ");

                Alertme("Book type has been delete Successfully.", "success");
                Bind_Book_type();

            }
            else
            {
                Alertme("You can't delete this Book type", "warning");
                return;
            }
        }

        private bool is_true(string Type)
        {
            if (mycode.FillData("select * from Library_Book_EntryLibrary_Book_Entry  where Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Type='" + Type + "'").Rows.Count > 0)
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