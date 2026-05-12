using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.LMS_VC_Admin
{
    public partial class Add_Designation : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
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
            string query = "select *  from Designation_master order by Designation_Name desc ";
            DataTable dt = imp.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }
        protected void btn_cncel_Click(object sender, EventArgs e)
        {
            txt_Designation.Text = "";
            btn_submit.Text = "Submit";
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_Designation.Text == "")
                {
                    Alert("Please enter designation name");
                }
                else
                {
                    if (btn_submit.Text == "Submit")
                    {
                        if (imp.IsExist("Select Designation_id from Designation_master where Designation_Name=N'" + txt_Designation.Text + "'"))
                        {
                            SqlCommand cmd = new SqlCommand("insert into Designation_master (Designation_Name,Designation_id, User_id) " +
                                "Values ('" + txt_Designation.Text + "','" + imp.Auto_generate_user_id("Select Designation_id from Designation_master where Designation_id=", 1000, 9999) + "'," +
                                "N'" + ViewState["User"].ToString() + "')");
                            InsertUpdate.InsertUpdateData(cmd);
                            Alert("The designation name has been successfully added.");
                            btn_cncel.Visible = false;
                            Bid_grid();
                        }
                        else { Alert("Duplicate designation name."); }
                    }
                    if (btn_submit.Text == "Update")
                    {
                        if (imp.IsExist("Select Designation_id from Designation_master where Designation_Name='" + txt_Designation.Text + "' and Id!='" + hd_id.Value + "'"))
                        {
                            SqlCommand cmd = new SqlCommand("Update Designation_master set Designation_Name='" + txt_Designation.Text + "', User_id='" + ViewState["User"].ToString() + "' where Id='" + hd_id.Value + "'");
                            InsertUpdate.InsertUpdateData(cmd);
                            btn_submit.Text = "Submit";
                            Alert("The designation name has been successfully updated.");
                            btn_cncel.Visible = false;
                            Bid_grid();
                            //Response.Redirect("ViewLesson.aspx");
                        }
                        else { Alert("Duplicate designation Name."); }
                    }
                    txt_Designation.Text = "";
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label lbl_Designation_Name = (Label)row.FindControl("lbl_Designation_Name");
                hd_id.Value = lbl_id.Text;
                btn_submit.Text = "Update";
                btn_cncel.Visible = true;
                txt_Designation.Text = lbl_Designation_Name.Text;

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label lbl_Designation_id = (Label)row.FindControl("lbl_Designation_id");
                if (check_section_is_in_use_or_not(lbl_Designation_id.Text))
                {
                    SqlCommand cmd = new SqlCommand("Delete from Designation_master where Id='" + lbl_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                    Alert("Designation name has been deleted successfully.");
                    Bid_grid();
                }
                else
                {
                    Alert("This Designation name is already used ");
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private bool check_section_is_in_use_or_not(string lbl_Designation_id)
        {
            DataTable dt = imp.FillTable("select * from Live_Class_User_Master where Designation_id='" + lbl_Designation_id + "'");
            if (dt.Rows.Count == 0)
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