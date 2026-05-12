using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class Syllabus_Create_Term : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                    ViewState["User"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["User"].ToString());
                    imp.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Use_mode asc ");
                    imp.bind_all_ddl_with_id(ddl_session_serch, "Select Session,session_id from session_details order by Use_mode asc ");
                    ddl_session.SelectedValue = My.get_session_id();
                    ddl_session_serch.SelectedValue = My.get_session_id();
                    Bid_grid();
                }
            }
        }
        public void Alert(string Message)
        {

            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void Bid_grid()
        {

            string query = " Select st.*,sd.Session from Syllubsh_Term st join session_details sd  on st.Session_id=sd.session_id where st.Session_id=" + ddl_session_serch.SelectedValue + "  order by Position";
            DataTable dt = imp.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                ViewState["position"] = "0";
                Alert("Sorry! there are no any data found");

                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {
                ViewState["position"] = dt.Rows.Count.ToString();
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }


        }

        protected void btn_cncel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Syllabus_Create_Term.aspx", false);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");

            }
            else if (txt_termname.Text == "")
            {
                Alert("Please enter term name");
            }
            else
            {
                if (btn_submit.Text == "Add")
                {
                    string sl = create_sl_no();

                    DataTable cdt = My.dataTable("Select * from Syllubsh_Term where Session_id ='" + ddl_session.SelectedValue + "' and Term_Name=N'" + txt_termname.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "' ");
                    int rowcount = cdt.Rows.Count;
                    if (rowcount == 0)
                    {
                        string query = "INSERT INTO Syllubsh_Term (Session_id,Term_Name,Date,Created_by,Branch_id,Term_id,Position) values (@Session_id,@Term_Name,@Date,@Created_by,@Branch_id,@Term_id,@Position)";
                        SqlCommand cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Term_Name", txt_termname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Date", imp.getdate1());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["User"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Term_id", sl);
                        cmd.Parameters.AddWithValue("@Position", Convert.ToInt32(ViewState["position"].ToString()) + 1);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Bid_grid();
                            Alert("Term name has been successfully added.");
                            txt_termname.Text = "";
                        }

                    }
                    else
                    {
                        Alert("Sorry your enterd term is dublicate.");
                    }
                }
                else
                {
                    DataTable cdt = My.dataTable("Select * from Syllubsh_Term where Session_id ='" + ddl_session.SelectedValue + "' and Term_Name=N'" + txt_termname.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Id!=" + hd_id.Value + " ");
                    if (cdt.Rows.Count == 0)
                    {
                        string query = "Update Syllubsh_Term set Session_id=@Session_id,Term_Name=@Term_Name,Updated_by=@Updated_by,updated_datetime=@updated_datetime where Id = @Id";
                        SqlCommand cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Term_Name", txt_termname.Text.Trim());
                        cmd.Parameters.AddWithValue("@updated_datetime", imp.getdate1());
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["User"].ToString());
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Bid_grid();
                            Alert("Term name has been successfully updated.");
                            txt_termname.Text = "";
                            btn_cncel.Visible = false;
                            btn_submit.Text = "Add";
                        }

                    }
                    else
                    {
                        Alert("Sorry your enterd term name is dublicate.");
                    }
                }
            }
        }



        private string create_sl_no()
        {
            bool duplicate = true;
            string Term_id = My.auto_serialS("group_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Term_id from dbo.[Syllubsh_Term] where Term_id='" + Term_id + "'");

                if (cdt.Rows.Count == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Term_id = My.auto_serialS("group_id");
                }
            }
            return Term_id;
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {

            if (ddl_session_serch.SelectedValue == "Select")
            {
                Alert("Please select session");

            }
            else
            {
                Bid_grid();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Term_Name = (Label)row.FindControl("lbl_Term_Name");
                Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
                txt_termname.Text = lbl_Term_Name.Text;
                ddl_session.SelectedValue = lbl_sessionid.Text;
                hd_id.Value = lbl_Id.Text;
                btn_cncel.Visible = true;
                btn_submit.Text = "Update";
            }
            catch
            {
            }

        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");

                Label lbl_termid = (Label)row.FindControl("lbl_termid");
                bool chketesr = chke_termuse(lbl_termid.Text);
                if (chketesr == true)
                {
                    SqlCommand cmd = new SqlCommand("Delete from Syllubsh_Term where Id='" + lbl_Id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);

                    Alert("Term name has been successfully deleted.");
                    Bid_grid();
                }
                else
                {
                    Alert("Sorry! You can't delete this term because this term already mapped in the chapter");

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private bool chke_termuse(string termid)
        {
            DataTable cdt = My.dataTable("select Term_id from dbo.[Syllubsh_Chapter_SubChapter] where Term_id='" + termid + "'");

            if (cdt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




    }
}