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
    public partial class Entry_Section : System.Web.UI.Page
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
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["firm_id"] = My.get_firm_id();
                    ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    Bind_Session();

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
            DataTable dt = mycode.FillData("Select * from Library_Card_NO_Format where Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry", "warning");
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
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            Bind_Session();

        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;

            if (btn_Submit.Text == "Add" && RadioButtonList1.SelectedValue == "Student")
            {
                string prefix = TextBox1.Text;
                string serialNo = TextBox2.Text;
                string postfix = TextBox3.Text;
                DataTable dt = mycode.FillData("Select * from Library_Card_NO_Format where Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Use_mode='Student'  ");
                if (dt.Rows.Count == 0)
                {
                    string query = "INSERT INTO Library_Card_NO_Format (Prefix,Postfix,serialNo,Use_mode,Branch_id) values (@Prefix,@Postfix,@serialno,'Student',@Branch_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@Postfix", postfix);
                    cmd.Parameters.AddWithValue("@serialno", serialNo);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Student library card has been saved Successfully.", "success");
                        btn_Submit.Text = "Add";
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        Bind_Session();
                    }
                }
                else
                {

                    string query = "update  Library_Card_NO_Format set Prefix=@Prefix,Postfix=@Postfix,serialNo=@serialNo  where Use_mode='Student' and Branch_id=@Branch_id ";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@Postfix", postfix);
                    cmd.Parameters.AddWithValue("@serialNo", serialNo);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Student library card has been update Successfully.", "success");
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        Bind_Session();
                    }

                }

            }
            else if (btn_Submit.Text == "Add" && RadioButtonList1.SelectedValue == "Teacher")
            {

                string prefix = TextBox1.Text;
                string serialNo = TextBox2.Text;
                string postfix = TextBox3.Text;
                DataTable dt = mycode.FillData("Select * from Library_Card_NO_Format where  Branch_id='" + ViewState["Branch_id"].ToString() + "' and Use_mode='Teacher' ");
                if (dt.Rows.Count == 0)
                {
                    string query = "INSERT INTO Library_Card_NO_Format (Prefix,Postfix,serialNo,Use_mode,Branch_id) values (@Prefix,@Postfix,@serialno,'Teacher',@Branch_id)";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@Postfix", postfix);
                    cmd.Parameters.AddWithValue("@serialno", serialNo);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());

                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Teacher library card has been saved Successfully.", "success");
                        btn_Submit.Text = "Add";
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";

                        Bind_Session();
                    }

                }
                else
                {
                    string query = "update  Library_Card_NO_Format set Prefix=@Prefix,Postfix=@Postfix,serialNo=@serialNo  where Use_mode='Teacher' and Branch_id=@Branch_id ";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@Postfix", postfix);
                    cmd.Parameters.AddWithValue("@serialNo", serialNo);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Teacher library card has been update Successfully.", "success");
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        Bind_Session();
                    }

                }


            }
            else if (btn_Submit.Text == "Add" && RadioButtonList1.SelectedValue == "Staff")
            {

                string prefix = TextBox1.Text;
                string serialNo = TextBox2.Text;
                string postfix = TextBox3.Text;
                DataTable dt = mycode.FillData("Select * from Library_Card_NO_Format where  Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Use_mode='Staff' ");
                if (dt.Rows.Count == 0)
                {
                    string query = "INSERT INTO Library_Card_NO_Format (Prefix,Postfix,serialNo,Use_mode,Branch_id) values (@Prefix,@Postfix,@serialno,'Staff',@Branch_id)";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@Postfix", postfix);
                    cmd.Parameters.AddWithValue("@serialno", serialNo);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Staff library card has been save Successfully.", "success");
                        btn_Submit.Text = "Add";
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";

                        Bind_Session();
                    }

                }
                else
                {
                    string query = "update  Library_Card_NO_Format set Prefix=@Prefix,Postfix=@Postfix,serialNo=@serialNo  where Use_mode='Staff' and Branch_id=@Branch_id ";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@Postfix", postfix);
                    cmd.Parameters.AddWithValue("@serialNo", serialNo);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Staff library card has been update successfully.", "success");
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        Bind_Session();
                    }



                }
            }
        }



        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details where Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
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



    }
}