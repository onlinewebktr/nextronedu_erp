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
    public partial class Add_Zoom_id_For_Admin : System.Web.UI.Page
    {
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

                    ViewState["Admin"] = Session["Admin"].ToString();

                    bind_superadminzoom();


                }
            }

        }
        UsesCode code = new UsesCode();
        private void bind_superadminzoom()
        {
            string query = "Select ip.*,zp.User_ID,zp.Password as pwd   from InstructorProfile ip join Zoom_API zp on ip.UserID=zp.teacher_id  where ip.UserID='" + ViewState["Admin"].ToString() + "'";
            try
            {


                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    hdid.Value = "";

                    RPDetails.DataSource = null;
                    RPDetails.DataBind();

                }
                else
                {


                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Name.Text == "")
                {
                    Alert("Please enter teacher name."); txt_Name.Focus(); return;
                }

                if (txt_zoomuserid.Text == "")
                {
                    Alert("Please enter zoom user id"); return;
                }
                if (txt_pwd.Text == "")
                {
                    Alert("Please enter user id"); return;
                }

                if (btn_submit.Text == "Submit")
                {


                    SqlCommand cmd;
                    if (code.IsExist("Select * from InstructorProfile where UserID='" + ViewState["Admin"].ToString() + "'"))
                    {


                        cmd = new SqlCommand("insert into InstructorProfile (UserID, Name,Istatus,Allow_Virtual_class_creation,Individual_universal,Type) " +
                           "Values ('" + ViewState["Admin"].ToString() + "','" + txt_Name.Text + "','1','1','Individual','Super Admin')");
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {


                            update_zoomid();
                            Alert("Super admin zoom id has been added successfully");
                            txt_Name.Text = "";
                            txt_pwd.Text = "";
                            txt_zoomuserid.Text = "";

                        }



                    }
                    else
                    {
                        SqlCommand cmd1;
                        cmd1 = new SqlCommand("update  InstructorProfile set Name='" + txt_Name.Text + "' where UserID='" + ViewState["Admin"].ToString() + "'");
                        if (InsertUpdate.InsertUpdateData(cmd1))
                        {


                            update_zoomid();
                            Alert("Super admin zoom id has been updated successfully");
                            txt_Name.Text = "";
                            txt_pwd.Text = "";
                            txt_zoomuserid.Text = "";

                        }
                    }
                }
                if (btn_submit.Text == "Update")
                {
                    SqlCommand cmd2;
                    cmd2 = new SqlCommand("update  InstructorProfile  set Name='" + txt_Name.Text + "' where UserID='" + ViewState["Admin"].ToString() + "'");
                    if (InsertUpdate.InsertUpdateData(cmd2))
                    {


                        update_zoomid();
                        Alert("Super admin zoom id has been updated successfully");
                        txt_Name.Text = "";
                        txt_pwd.Text = "";
                        txt_zoomuserid.Text = "";

                    }
                }

                bind_superadminzoom();
            }

            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void update_zoomid()
        {
            int getslid = code.get_slid_max();
            DataTable dt = code.FillTable("Select sl_no from Zoom_API where teacher_id='" + ViewState["Admin"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {

                UsesCode.exeSql("INSERT INTO Zoom_API (User_ID,Password,teacher_id,Status,sl_no) values ('" + txt_zoomuserid.Text + "','" + txt_pwd.Text + "','" + ViewState["Admin"].ToString() + "','1','" + getslid + "')");

            }
            else
            {
                UsesCode.exeSql("update Zoom_API set  User_ID= '" + txt_zoomuserid.Text + "', Password='" + txt_pwd.Text + "',sl_no='" + getslid + "'   where teacher_id='" + ViewState["Admin"].ToString() + "'");

            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                HiddenField hdfID = (HiddenField)row.FindControl("hdUserID");
                HiddenField hdId = (HiddenField)row.FindControl("hdId");
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                Label lblzoomuserid = (Label)row.FindControl("lblzoomuserid");
                Label lbl_Password = (Label)row.FindControl("lbl_Password");
                txt_Name.Text = lbl_Name.Text;
                btn_submit.Text = "Update";
                txt_zoomuserid.Text = lblzoomuserid.Text;
                txt_pwd.Text = lbl_Password.Text;
            }
            catch { }
        }



    }
}