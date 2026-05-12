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
    public partial class Add_Location : System.Web.UI.Page
    {
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
                    if(!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = Library.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        Bind_Session();
                    }
                   

                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Session_Master");
            }
            find_firm_details();
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
        My mycode = new My();
        private void Bind_Session()
        {

            DataTable dt = mycode.FillData("Select * from lib_location_details where Branch_id = '" + ViewState["Branch_id"].ToString() + "'  order by location asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no Location exists", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();

            }
            else
            {
                Btnprint.Visible = false;

                rd_view.DataSource = dt;
                rd_view.DataBind();
                Btnexcel.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    Btnprint.Visible = true;
                }
                else
                {
                    Btnprint.Visible = false;
                }
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_Location.Text = "";

            Bind_Session();

        }
        protected void btn_Submit_Click1(object sender, EventArgs e)
        {


            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {

                    submitdata();

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {

                    update_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");



                }
            }




        }

        private void update_data()
        {
            SqlCommand cmd;
            string Location = txt_Location.Text;
            DataTable dt = mycode.FillData("Select * from lib_location_details where location='" + Location + "' and  Location_id!=" + HdID.Value + " and  Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                string query = "Update lib_location_details set location=@Location,Location_id=@Location_id,User_id=@User_id,Date=@Date,idate=@idate,Time=@Time where Location_id = @Location_id";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Location_id", HdID.Value);
                cmd.Parameters.AddWithValue("@location", Location);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Time", mycode.time());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Location has been update Successfully.", "success");
                    btn_Submit.Text = "Add";
                    btn_cancel.Visible = false;
                    txt_Location.Text = "";

                    Bind_Session();

                }

            }
            else
            {
                txt_Location.Focus();
                Alertme("This Location already exist", "warning");
                return;
            }
        }

        private void submitdata()
        {
            SqlCommand cmd;
            string Location = txt_Location.Text;
            DataTable dt = mycode.FillData("Select * from lib_location_details where location='" + Location + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                string createsessionid = cretesessionid();
                string query = "INSERT INTO lib_location_details (location,Location_id,User_id,Date,idate,Time,Branch_id) values (@Location,@Location_id,@User_id,@Date,@idate,@Time,@Branch_id)";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Location_id", createsessionid);
                cmd.Parameters.AddWithValue("@location", Location);
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Time", mycode.time());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Location has been save Successfully.", "success");
                    btn_Submit.Text = "Add";
                    txt_Location.Text = "";

                    Bind_Session();
                }

            }
            else
            {
                txt_Location.Focus();
                Alertme("This Location already exist", "warning");
                return;
            }
        }

        private string cretesessionid()
        {
            bool duplicate = false;
            string Location_id = mycode.auto_serial("Location_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Location_id from lib_location_details where Location_id='" + Location_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Location_id = mycode.auto_serial("Location_id");
                }
            }
            return Location_id;



        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Download"].ToString() == "1")
            {
                SqlCommand cmd;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Locationid = (Label)row.FindControl("lbl_Locationid");
                 


                string query = "delete from  lib_location_details where Location_id=@Location_id and  Branch_id = '" + ViewState["Branch_id"].ToString() + "'";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@Location_id", lbl_Locationid.Text);

                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Location has been delete Successfully.", "success");
                    Bind_Session();
                }

            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }


        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Locationid = (Label)row.FindControl("lbl_Locationid");
                    Label lbl_Location = (Label)row.FindControl("lbl_Location");
                    HdID.Value = lbl_Locationid.Text;
                    txt_Location.Text = lbl_Location.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
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
        private bool is_true(string Location)
        {
            if (mycode.FillData("select location from lib_location_details ").Rows.Count > 0)
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

    }
}