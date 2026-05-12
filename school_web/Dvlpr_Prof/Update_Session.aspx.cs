using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Chnage_Session : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admindov"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    ViewState["Admindov"] = Session["Admindov"].ToString();
                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");

                }
            }
        }
        string scrpt;
        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtadmission_no.Text == "")
            {
                lblmessage.Text = "Please enter admission";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_session.SelectedValue == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }

            else
            {
                DataTable dt = mycode.FillData("select top 1 Id from admission_registor where admissionserialnumber=" + txtadmission_no.Text + " order by id desc");
                if (dt.Rows.Count == 0)
                {
                    lblmessage.Text = "Sorry your enterd admission number is wrong";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
                else
                {
                    string id = dt.Rows[0][0].ToString();
                    mycode.executequery("update admission_registor set session='" + ddl_session.SelectedValue + "',Session_id='"+ddl_session.SelectedValue+"' where Id=" + id + "");
                    lblmessage.Text = "Session has been updated";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);


                }



            }
        }

        protected void btn_update_session_Click(object sender, EventArgs e)
        {
            if (txtadmission_no.Text == "")
            {
                lblmessage.Text = "Please enter admission";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_session.SelectedValue == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }

            else
            {
                DataTable dt = mycode.FillData("select top 1 Id from admission_registor where admissionserialnumber='" + txtadmission_no.Text + "' order by id desc");
                if (dt.Rows.Count == 0)
                {
                    lblmessage.Text = "Sorry your enterd admission number is wrong";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
                else
                {
                    string id = dt.Rows[0][0].ToString();
                    mycode.executequery("update admission_registor set session='" + ddl_session.SelectedItem.Text + "',Session_id='" + ddl_session.SelectedValue + "' where Id=" + id + "");
                    lblmessage.Text = "Session has been updated";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);


                }



            }
        }
    }
}