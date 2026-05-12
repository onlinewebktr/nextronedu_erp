using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class Add_Session_Master : System.Web.UI.Page
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
                   

                    Bind_Session();

                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Session_Master");
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
        My mycode = new My();
        private void Bind_Session()
        {
            DataTable dt = mycode.FillData("Select * from session_details order by Session asc   ");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no branch list exist", "warning");
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
            txt_session_from.Text = "";
            txtsession_to.Text = "";
            Bind_Session();

        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            SqlCommand cmd;

            if (btn_Submit.Text == "Add")
            {
                string session = txt_session_from.Text + "-" + txtsession_to.Text;
                DataTable dt = mycode.FillData("Select * from session_details where Session='" + session + "'  ");
                if (dt.Rows.Count == 0)
                {
                    string createsessionid = cretesessionid();
                    string query = "INSERT INTO session_details (Session,session_id,User_id,Date,idate,Time) values (@Session,@session_id,@User_id,@Date,@idate,@Time)";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@session_id", createsessionid);
                    cmd.Parameters.AddWithValue("@Session", session);
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Time", mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Session has been save Successfully.", "success");
                        btn_Submit.Text = "Add";
                        txt_session_from.Text = "";
                        txtsession_to.Text = "";
                        Bind_Session();
                    }

                }
                else
                {
                    txt_session_from.Focus();
                    Alertme("This Session already exist", "warning");
                    return;
                }
            }
            else
            {


                string session = txt_session_from.Text + "-" + txtsession_to.Text;
                DataTable dt = mycode.FillData("Select * from session_details where Session='" + session + "' and  session_id!=" + hd_id.Value + " ");
                if (dt.Rows.Count == 0)
                {
                    string query = "Update session_details set Session=@Session,session_id=@session_id,User_id=@User_id,Date=@Date,idate=@idate,Time=@Time where session_id = @session_id";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@session_id", hd_id.Value);
                    cmd.Parameters.AddWithValue("@Session", session);
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Time", mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Session has been update Successfully.", "success");
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        txt_session_from.Text = "";
                        txtsession_to.Text = "";
                        Bind_Session();

                    }

                }
                else
                {
                    txt_session_from.Focus();
                    Alertme("This Session already exist", "warning");
                    return;
                }


            }


        }

        private string cretesessionid()
        {
            bool duplicate = false;
            string session_id = mycode.auto_serial("session_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select session_id from dbo.[session_details] where session_id='" + session_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    session_id = mycode.auto_serial("session_id");
                }
            }
            return session_id;



        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
            Label lbl_session = (Label)row.FindControl("lbl_session");
            if (is_true(lbl_session.Text))
            {

                string query = "delete from  session_details where session_id=@session_id";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@session_id", lbl_sessionid.Text);

                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Session has been delete Successfully.", "success");
                    Bind_Session();
                }
            }
            else
            {
                Alertme("You can't delete this session", "warning");
                return;
            }


        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                hd_id.Value = lbl_sessionid.Text;

                if (is_true(lbl_session.Text))
                {
                    txt_session_from.Text = lbl_session.Text.Split('-')[0];
                    txtsession_to.Text = lbl_session.Text.Split('-')[1];


                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("You can't edit this session", "warning");
                    return;
                }



            }
            catch
            {

            }
        }

        private bool is_true(string session)
        {
            if (mycode.FillData("select session from dbo.[admission_registor] ").Rows.Count > 0)
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