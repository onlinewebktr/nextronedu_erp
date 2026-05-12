using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace school_web.Library_Admin
{
    public partial class New_Book_Entry : System.Web.UI.Page
    {
        My mycode = new My();
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

                        mycode.bind_all_ddl_with_id(DropDownList3, "Select Course_Name,course_id from Add_course_table order by Position");
                        
                        mycode.bind_all_ddl_with_id(DropDownList4, "Select Subject_name,Subject_id from Subject_Master");
                     
                        mycode.bind_all_ddl_with_id(DropDownList2, "Select BookStatus,BookStatusId from Library_Book_Status");
                        
                        mycode.bind_all_ddl_with_id(DropDownList1, "Select TypeName,TypeId from Library_Type ");
                     
                        mycode.bind_all_ddl_with_id(DropDownList5, "Select location,Location_id from lib_location_details where Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                       
                        Bind_Session();

                        // BindDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Session_Master");
            }
            find_firm_details();
            Panel4.Visible = false;

        }


        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                Panel4.Visible = true;
                Panel3.Visible = false;

            }
            else
            {
                Panel4.Visible = false;
                Panel3.Visible = true;

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
        
        private void Bind_Session()
        {
            DataTable dt = mycode.FillData("Select * from Library_Book_Entry");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no Book exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();

            }
            else
            {

                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
           
            Bind_Session();
            clear();

        }
        private void clear() 
        {
            DropDownList1.SelectedValue = "";
            DropDownList2.SelectedValue = "";
            DropDownList3.SelectedValue = "";
            DropDownList4.SelectedValue = "";
            DropDownList5.SelectedValue = "";
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            TextBox14.Text = ""; 
                    }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            SqlCommand cmd;

            if (btn_Submit.Text == "Add")
            {
              
                string Type=  DropDownList1.SelectedValue;
                string BookStatus =DropDownList2.SelectedValue;
                string Class = DropDownList3.SelectedValue;
                string Subject = DropDownList4.SelectedValue;
                string NOOFBooks = TextBox1.Text;
                string AuthorName = TextBox2.Text;
                string Publication = TextBox3.Text;
                string EnterVolumePart = TextBox13.Text;
                string SVR = TextBox8.Text;
                string Evr = TextBox14.Text;
                string Edition = TextBox4.Text;
                string PublicationYear = TextBox5.Text;
                string NoOfPages = TextBox6.Text;
                string Quantity = TextBox7.Text;
                string Location = DropDownList5.SelectedValue;
                string ISBN = TextBox9.Text;
                string Invoice = TextBox10.Text ;
                string Price = TextBox11.Text;
                string Note = TextBox12.Text ;
                DataTable dt = mycode.FillData("Select * from  Library_Book_Entry where Type='" + Type + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'  ");
                if (dt.Rows.Count == 0)
                {
                    string createsessionid = cretesessionid();
                    string query = "insert into Library_Book_Entry(BookId,Type,BookStatus,SelectClass,Subject,NameOfBook,AuthorName,Publication,EnterVolumePart,StartVolumeRange,EndVolumeRange,Edition,PublicationYear,NoOfPages,EnterQuantity,Location,ISBN_Num,InvoiceNo,Price,Note) values ('@BookId','@Type','@BookStatus','@SelectClass','@Subject','@NameOfBook','@AuthorName','@Publication','@EnterVolumePart','@StartVolumeRange','@EndVolumeRange','@Edition','@PublicationYear','@NoOfPages','@EnterQuantity','@Location','@ISBN_Num','@InvoiceNo','@Price','@Note')";
                    cmd = new SqlCommand(query);
                   cmd.Parameters.AddWithValue("@BookId", createsessionid);
                    cmd.Parameters.AddWithValue("@Type",Type);    
                    cmd.Parameters.AddWithValue("@BookStatus",BookStatus); 
                    cmd.Parameters.AddWithValue("@SelectClass",Class); 
                    cmd.Parameters.AddWithValue("@Subject",Subject);
                    cmd.Parameters.AddWithValue("@NameOfBook",NOOFBooks);
                    cmd.Parameters.AddWithValue("@AuthorName",AuthorName);
                    cmd.Parameters.AddWithValue("@Publication",Publication);
                    cmd.Parameters.AddWithValue("@EnterVolumePart",EnterVolumePart);
                    cmd.Parameters.AddWithValue("@StartVolumeRange",SVR);
                    cmd.Parameters.AddWithValue("@EndVolumeRange",Evr);
                    cmd.Parameters.AddWithValue("@Edition",Edition);
                    cmd.Parameters.AddWithValue("@PublicationYear",PublicationYear);
                    cmd.Parameters.AddWithValue("@NoOfPages",NoOfPages);
                    cmd.Parameters.AddWithValue("@EnterQuantity",Quantity);
                    cmd.Parameters.AddWithValue("@Location",Location);
                    cmd.Parameters.AddWithValue("@ISBN_Num",ISBN);
                    cmd.Parameters.AddWithValue("@InvoiceNo",Invoice);
                    cmd.Parameters.AddWithValue("@Price",Price);
                    cmd.Parameters.AddWithValue("@Note",Note);
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());                 
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Book has been save Successfully.", "success");
                        btn_Submit.Text = "Add";
                        clear();

                        Bind_Session();
                    }

                }
                
            }
            else
            {
                string Type = DropDownList1.SelectedValue;
                string BookStatus = DropDownList2.SelectedValue;
                string Class = DropDownList3.SelectedValue;
                string Subject = DropDownList4.SelectedValue;
                string NOOFBooks = TextBox1.Text;
                string AuthorName = TextBox2.Text;
                string Publication = TextBox3.Text;
                string EnterVolumePart = TextBox13.Text;
                string SVR = TextBox8.Text;
                string Evr = TextBox14.Text;
                string Edition = TextBox4.Text;
                string PublicationYear = TextBox5.Text;
                string NoOfPages = TextBox6.Text;
                string Quantity = TextBox7.Text;
                string Location = DropDownList5.SelectedValue;
                string ISBN = TextBox9.Text;
                string Invoice = TextBox10.Text;
                string Price = TextBox11.Text;
                string Note = TextBox12.Text;
                DataTable dt = mycode.FillData("Select * from Library_Book_Entry where BookId!=" + HdID.Value + " and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    string query = "Update Library_Book_Entry set BookId = @BookId,Type = @Type,BookStatus = @BookStatus,SelectClass = @SelectClass,Subject = @Subject,NameOfBook = @NameOfBook,AuthorName = @AuthorName,Publication = @Publication,EnterVolumePart = @EnterVolumePart,StartVolumeRange = @StartVolumeRange,EndVolumeRange = @EndVolumeRange,Edition = @Edition,PublicationYear = @PublicationYear,NoOfPages = @NoOfPages,EnterQuantity = @EnterQuantity,Location = @Location,ISBN_Num = @ISBN_Num,InvoiceNo = @InvoiceNo,Price = @Price,Note = @Note  where BookId = @BookId";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@BookId", HdID.Value);
                    cmd.Parameters.AddWithValue("@Type", Type);
                    cmd.Parameters.AddWithValue("@BookStatus", BookStatus);
                    cmd.Parameters.AddWithValue("@SelectClass", Class);
                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    cmd.Parameters.AddWithValue("@NameOfBook", NOOFBooks);
                    cmd.Parameters.AddWithValue("@AuthorName", AuthorName);
                    cmd.Parameters.AddWithValue("@Publication", Publication);
                    cmd.Parameters.AddWithValue("@EnterVolumePart", EnterVolumePart);
                    cmd.Parameters.AddWithValue("@StartVolumeRange", SVR);
                    cmd.Parameters.AddWithValue("@EndVolumeRange", Evr);
                    cmd.Parameters.AddWithValue("@Edition", Edition);
                    cmd.Parameters.AddWithValue("@PublicationYear", PublicationYear);
                    cmd.Parameters.AddWithValue("@NoOfPages", NoOfPages);
                    cmd.Parameters.AddWithValue("@EnterQuantity", Quantity);
                    cmd.Parameters.AddWithValue("@Location", Location);
                    cmd.Parameters.AddWithValue("@ISBN_Num", ISBN);
                    cmd.Parameters.AddWithValue("@InvoiceNo", Invoice);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Note", Note);

                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Book has been update Successfully.", "success");
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        clear();

                        Bind_Session();

                    }

                }
                


            }


        }
        private string cretesessionid()
        {
            bool duplicate = false;
            string BookId = mycode.auto_serial("BookId");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select BookId from  Library_Book_Entry where BookId='" + BookId + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    BookId = mycode.auto_serial("BookId");
                }
            }
            return BookId;



        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_BookId = (Label)row.FindControl("lbl_BookId");
            Label lbl_Name = (Label)row.FindControl("Label4");
            if (is_true(lbl_Name.Text))
            {

                string query = "delete from  Library_Book_Entry where BookId=@BookId";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@session_id", lbl_BookId.Text);

                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Book Detail has been delete Successfully.", "success");
                    Bind_Session();
                }
            }
            else
            {
                Alertme("You can't delete this Book Detail", "warning");
                return;
            }


        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_BookId = (Label)row.FindControl("lbl_BookId");
                Label lbl_Name = (Label)row.FindControl("Label4");
                HdID.Value = lbl_BookId.Text;

                if (is_true(lbl_BookId.Text))
                {
                    TextBox1.Text = lbl_Name.Text.Split('-')[0];



                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("You can't edit this Book Details", "warning");
                    return;
                }



            }
            catch
            {

            }
        }
        private bool is_true(string Name )
        {
            if (mycode.FillData("select location from lib_location_details  where Branch_id = '" + ViewState["Branch_id"].ToString() + "'").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details where Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
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
        protected void Btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GrdView.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch
            {
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}