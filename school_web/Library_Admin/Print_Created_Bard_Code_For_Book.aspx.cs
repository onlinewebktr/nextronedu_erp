using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin
{
    public partial class Print_Created_Bard_Code_For_Book : System.Web.UI.Page
    {
        Library ly = new Library();
        My mycode = new My();
        string booktype = "Select top 1 TypeName from Library_Type where TypeId=lbe.Type and Branch_Id=lbe.Branch_id";
        string BookStatus = "Select top 1 BookStatus from Library_Book_Status where BookStatusId=lbe.BookStatus and Branch_Id=lbe.Branch_id";
        string Book_Category = "Select top 1 Book_Category from Library_Book_Category where Book_Category_Id=lbe.Book_Category_Id and Branch_Id=lbe.Branch_id";

        string location = "Select top 1 location from lib_location_details where Location_id=lbe.Location and Branch_Id=lbe.Branch_id";
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
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id_cap_NA(ddl_class_wise, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddl_class_wise.SelectedValue = ly.get_top_one_class_for_add_book();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_book_status, "Select BookStatus,BookStatusId from Library_Book_Status");

                        mycode.bind_all_ddl_with_id(ddl_lcation, "Select location,Location_id from lib_location_details where Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                        get_firm_name();

                        Bind_data_class_wise();
                    }
                }
            }
            catch
            {

            }
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

        [WebMethod]
        public static List<string> Getbookauthername(string authername)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct AuthorName from Library_Book_Entry_Master_Uniqe where AuthorName LIKE ''+@AuthorName+'%'  ";
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
        [WebMethod]
        public static List<string> Getbookname(string bookname)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct NameOfBook from Library_Book_Entry where NameOfBook LIKE ''+@AuthorName+'%'  ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@AuthorName", bookname);
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
        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_class_id = (Label)e.Row.FindControl("lbl_class_id");
                Label lbl_classname = (Label)e.Row.FindControl("lbl_classname");
                lbl_classname.Text = ly.get_classname_Name(lbl_class_id.Text);

            }



        }
        protected void ddl_class_wise_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_data_class_wise();


        }

        private void Bind_data_class_wise()
        {
            if (ddl_class_wise.SelectedItem.Text == "Select")
            {
                Alertme("Please selcte class", "warning");
            }
            else
            {

                string query = "Select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new from Library_Book_Entry lbe where lbe.SelectClass='" + ddl_class_wise.SelectedValue + "' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and lbe.Is_Delete=1";
                Bind_Data(query);
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
                string query = "Select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new from  Library_Book_Entry lbe where lbe.Location='" + ddl_lcation.SelectedValue + "' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and lbe.Is_Delete=1";
                Bind_Data(query);

            }
        }
        protected void btn_find_auther_wise_Click(object sender, EventArgs e)
        {
            if (txt_AuthorName.Text == "")
            {
                Alertme("Please selcte book name", "warning");
            }
            else
            {
                string query = "Select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new from Library_Book_Entry lbe where lbe.NameOfBook like '%" + txt_AuthorName.Text + "%'";
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
                    string query = "Select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new from Library_Book_Entry lbe where  lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and lbe.Is_Delete=1";
                    Bind_Data(query);
                }
                else
                {
                    string query = "Select *,(" + booktype + ") as booktype,(" + BookStatus + ") as Book_Status,(" + Book_Category + ") as Book_Category,(" + location + ") as location_new from Library_Book_Entry lbe where lbe.BookStatus='" + ddl_book_status.SelectedValue + "' and lbe.Branch_id=" + ViewState["Branch_id"].ToString() + " and lbe.Is_Delete=1";
                    Bind_Data(query);
                }


            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                print1.Visible = true;
                btn_excels.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void btn_print_barcodes_Click(object sender, EventArgs e)
        {
            string BookId = "";
            int growcount = GrdView.Rows.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {
                    Label lbl_BookId = (Label)GrdView.Rows[i].FindControl("lbl_BookId");
                    BookId = BookId += lbl_BookId.Text + ",";
                    string reslink = "print-barcode.aspx?bookid=" + BookId;
                    Response.Redirect(reslink, false);
                }
                else
                {
                    k++;
                }
            }

            if (k == growcount)
            {
                

            }
        }
    }
}
